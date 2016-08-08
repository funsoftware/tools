
def swagger_ref_type(ref, lang="cs"):
  if lang == "cs":
    pos = ref.rfind("/")
    return ref[pos+1:]
  elif lang == "java":
    pass

def swagger_type_info(prop, lang="cs"):
  info = {"type":"string", "is_array":"false", "item_type":"", "is_object":"false", "type_cast":""}
  if lang == "cs":
    if prop["type"] == "integer":
      info["type"] = "int"
      info["type_cast"] = "int.Parse"
    elif prop["type"] == "number":
      info["type"] = "double"
      info["type_cast"] = "double.Parse"
    elif prop["type"] == "array":
      info["is_array"] = "true"
      item = prop["items"]
      if "$ref" in item:
        info["is_object"] = "true"
        info["item_type"] = swagger_ref_type(item["$ref"], lang)
        info["type"] = "List<" + info["item_type"] + ">"
    else:
      info["type"] = "string"
    return info
  elif lang == "java":
    pass

def swagger_type_name(param):
  if param['type'] == "number":
    if param['format'] == "double":
      return "double"
  elif param['type'] == "integer":
    if param['format'] == "int32":
      return "int"
  else:
    return param['type']
def swagger_method_params(params):
  param_list = []
  for param in params:
    param_list.append(swagger_type_name(param) + " " + param["name"])
  return param_list

def swagger_param_str(param):
  if param["type"] == "string":
    return param["name"]
  else:
    return param["name"] + ".ToString()"

def swagger_response_obj(resp, lang="cs"):
  schema = resp["schema"]
  if "type" in schema and schema["type"] == "array":
    return swagger_ref_type(schema["items"]["$ref"], lang) + '.ArrayFromApiclient(apiclient, "/")'
  elif "$ref" in schema:
    return swagger_ref_type(schema["$ref"], lang) + '.FromApiclient(apiclient, "/")'

if __name__ == "__main__":
  m = 0