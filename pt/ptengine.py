#-*- coding: UTF-8 -*-

import os
import sys

__author__ = 'Joe'

NEW_LINE = "\n"
ENV = {}

def __read_blocks(file_or_text, charset='utf8'):
  try:
    text = pte_readfile(file_or_text, charset)
  except:
    text = file_or_text
  pos = 0
  blocks = []
  while pos >= 0 and pos < len(text):
    (pos_beg, tok) = pte_find(text, pos, ["{{", "{%"])
    if pos_beg < 0:
      blocks.append({"TYPE":"TEXT", "VALUE":text[pos:]})
      pos = -1
    elif tok == "{{":
      pos_end = text.find("}}", pos_beg)
      if pos_end < 0:
        raise LookupError("read_blocks error: Faild to find '}}'")
      else:
        blocks.append({"TYPE":"TEXT", "VALUE":text[pos: pos_beg]})
        blocks.append({"TYPE":"EXPR", "VALUE":text[pos_beg + 2: pos_end]})
        pos = pos_end + 2
    elif tok == "{%":
      pos_end = text.find("%}", pos_beg)
      if pos_end < 0:
        raise LookupError("read_blocks error: Faild to find '%}'")
      else:
        pre_line = text.rfind(NEW_LINE, 0, pos_beg)
        line_start = pre_line + len(NEW_LINE)
        if pre_line >= 0 and line_start >= pos and len(text[line_start : pos_beg].strip('\r\n\t ')) == 0:
          blocks.append({"TYPE":"TEXT", "VALUE":text[pos: line_start]})
        else:
          blocks.append({"TYPE":"TEXT", "VALUE":text[pos: pos_beg]})

        code_offset = pos_beg + 2 - line_start
        blocks.append({"TYPE":"CODE", "VALUE": code_offset * " " + text[pos_beg + 2: pos_end]})
        pos = pos_end + 2
        nxt_line = text.find(NEW_LINE, pos_end)
        if nxt_line >= pos and len(text[pos:nxt_line].strip('\r\n\t ')) == 0:
          pos = nxt_line + len(NEW_LINE)
  return blocks
def __translate(file_or_text, charset='utf8', tab = "  "):
  blocks = __read_blocks(file_or_text, charset)
  code = ""
  depth = 0
  for block in blocks:
    tok_val = block["VALUE"]
    if block["TYPE"] == "TEXT":
      lines = tok_val.splitlines(True)
      for line in lines:
        code += tab * depth + "pte_print(" + pte_str(line) + ")" + NEW_LINE
    elif block["TYPE"] == "CODE":
      strip_code = tok_val.strip()
      words = strip_code.split(" ")
      if words[0] in ["for", "if", "while", "with"]:
        if strip_code[-1] != ":":
          strip_code += ":"
        code += tab * depth + strip_code + NEW_LINE
        depth = depth + 1
        block["depth"] = depth
      elif words[0] in ["elif", "else"]:
        if strip_code[-1] != ":":
          strip_code += ":"
        code += tab * (depth - 1) + strip_code  + NEW_LINE
        block["depth"] = depth
      elif words[0] in ["endfor", "endif", "endwhile", "endwith"]:
        depth = depth - 1
      else:
        lines = tok_val.splitlines(True)
        if len(lines) == 1:
          code += tab * depth + lines[0].strip('\r\n\t ')
          code += NEW_LINE
        elif len(lines) > 1:
          if len(lines[0].strip('\r\n\t ')) > 0:
            raise AssertionError("translate error: The fist line of the code block must be empty line.")
          offset = len(lines[1]) - len(lines[1].lstrip('\t '))
          for i in range(1, len(lines)):
            code += tab * depth + lines[i][offset:]
    elif block["TYPE"] == "EXPR":
      code += tab * depth + "pte_print(pte_eval(" + pte_str(tok_val) + "))" + NEW_LINE
  return code

def pte_find(str, beg, sub_strs):
  pos, tok = -1, ""
  for sub_str in sub_strs:
    pos_new = str.find(sub_str, beg)
    if (pos >= 0 and pos_new < pos) or (pos < 0 and pos_new >= 0):
      pos, tok = pos_new, sub_str
  return (pos, tok)

def pte_readfile(filename, charset="utf-8"):
  with open(filename, 'r', encoding=charset) as fh:
    return fh.read()

def pte_writefile(filename, data):
  with open(filename, 'w') as fh:
    fh.write(data)

def pte_include(filename):
  if os.path.exists(filename):
    with open(filename, 'r', encoding="utf-8") as fh:
      exec(fh.read(), globals())

def pte_eval(expr):
  exprs = expr.split("|")
  expr = exprs[0]
  attrs = expr.split(".")
  parts = []
  for attr in attrs:
    props = attr.split("[")
    parts.append({"TYPE" : "ATTR", "EXPR" : props[0].strip('[]\'"\t ')})
    for i in range(1, len(props)):
      parts.append({"TYPE" : "PROP", "EXPR" : props[i].strip('[]\'"\t ')})
  try:
    val = eval(parts[0]["EXPR"])
  except NameError:
    val = eval("ENV['" + parts[0]["EXPR"] + "']")

  for i in range(1, len(parts)):
    part = parts[i]
    if part["TYPE"] == "ATTR":
      try:
        val = eval("val." + part["EXPR"])
      except AttributeError:
        val = eval("val['" + part["EXPR"] + "']")
      except KeyError:
        raise AttributeError("pte_eval error: Failed to eval value of attribute '" + part["EXPR"] + "'")
    else: # "PROP"
      try:
        val = eval("val['" + part["EXPR"] + "']")
      except KeyError:
        val = eval("val." + part["EXPR"])
      except AttributeError:
        raise KeyError("pte_eval error: Failed to eval value of key '" + part["EXPR"] + "'")
  # process pipes
  for i in range(1,len(exprs)):
    val = eval(exprs[i].replace("self", "val"))
  return val

def pte_str(val):
  if isinstance(val, str):
    if len(val) == 0:
      return '""'
    ret = val.replace("\\", "\\\\")
    ret = ret.replace('"', '\\"')
    ret = ret.replace('\r', '\\r')
    ret = ret.replace('\n', '\\n')
    return '"' + ret + '"'
  elif isinstance(val, list):
    result = []
    for v in val:
      result.append(pte_str(v))
    return result
  else:
    return pte_str(str(val))

def pte_print(text):
  global ENV
  if not isinstance(text, str):
    text = str(text)
  if ENV and ENV['out']:
    ENV['out'].write(text)

def pte_exec(template_file, output_file = None, args = None):
  global ENV
  if not output_file:
    output_file = template_file + '.out'
  ENV['output_file'] = output_file
  ENV['template_file'] = template_file
  code = __translate(template_file)
  pte_writefile("./python_GEN.py", code)
  if args:
    for (key, val) in args.items():
      ENV[key] = val
  ENV['out'] = open(output_file, 'w')
  exec(code, globals())
  ENV['out'].close()

if __name__ == "__main__":
  usage = """
    Usage:
      ptengine.py  <template_file> [<output_file>] [<-key1> <value1>] [<-key2> <value2>] ......
    """
  argc = len(sys.argv)
  if (argc < 2):
    print(usage)
  else:
    template_file = sys.argv[1]
    output_file = None
    args = None
    if argc > 2:
      output_file = sys.argv[2]
    if argc > 3:
      key_id = 3
      val_id = 4
      while val_id < argc:
        if sys.argv[key_id][0] != "-":
          print(usage)
          break
        else:
          if not args:
            args = {}
          args[sys.argv[key_id][1:]] = sys.argv[val_id]
          key_id, val_id = key_id + 2, val_id + 2
    pte_exec(template_file, output_file, args)



  #pte_exec("./UberAPI.pt", "./UberAPI_GEN.cs",  {"class_name" : "UberAPI", "swagger_file" : "./swagger.json"})




