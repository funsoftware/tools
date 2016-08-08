pte_print("\n")
for i in range(0, 100):
  pte_print(pte_eval("i+1"))
  
  if i != 99:
    pte_print(", ")
  if (i+1) % 10 == 0:
    pte_print("\n")
