# 1. Introduction
	PT(python template) Engine is a tool that used to genrate some code. It 
	use some simple synatax to adding some python code into the template, 
	then the template be translated to python code and be executed by Python.
	At last, it generate the pure text data.

# 2. Overview
	1. Tokennier  (Parsing all Tokennier)
	2. Translator (Translate engine code to python code)
	3. Execute python code

# 3. Features
	1. Variable. It looks like  "{{var}}", the var will be evaluated by python.
		-	{{exp}}
		-	{{ex}}
