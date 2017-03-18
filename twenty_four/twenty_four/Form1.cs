using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace twenty_four {
  public partial class mainForm : Form {
    public mainForm() {
      InitializeComponent();

     
    }

    private String twenty_four(int[] nums) {
      String expr = "";
      double val = 0;
      int state = -1;
      return calc(nums, 24.00000f, ref expr, ref val, ref state) ? expr : "";
    }
    private bool calc(int[] nums, double result, ref string expr, ref double val, ref int state) {
      if (nums.Length == 0)  return val == result;
      // state=-1 ----  empty(start)
      // state=0  ----  single number, 
      // state=1  ----  + or -, 
      // state=2  ----  * or /
      String newExpr;
      double newVal;
      int newState;
      for (int i = 0; i < nums.Length; i++) {
        int num = nums[i];
        List<int> temp = new List<int>(nums);
        temp.RemoveAt(i);
        int[] newNums = temp.ToArray();
        if (state == -1) { 
          newExpr = num.ToString();
          newVal = num;
          newState = 0;
          if (calc(newNums, result, ref newExpr, ref newVal, ref newState)) goto success;
        } else { 
          // 1. expr + num
          newExpr = expr + "+" + num;
          newVal = val + num;
          newState = 1;
          if (calc(newNums, result, ref newExpr, ref newVal, ref newState)) goto success;
          // 2. expr - num
          newExpr = expr + "-" + num;
          newVal = val - num;
          newState = 1;
          if (calc(newNums, result, ref newExpr, ref newVal, ref newState)) goto success;
          // 3. num - expr
          newExpr = num + "-" + (state == 1 ? "(" + expr + ")" : expr);
          newVal = num - val;
          newState = 1;
          if (calc(newNums, result, ref newExpr, ref newVal, ref newState)) goto success;
          // 4. expr * num
          newExpr = (state == 1? "(" + expr + ")" : expr) + "*" + num;
          newVal = val * num;
          newState = 2;
          if (calc(newNums, result, ref newExpr, ref newVal, ref newState)) goto success;
          // 5. expr / num
          newExpr = (state == 1 ? "(" + expr + ")" : expr) + "/" + num;
          newVal = val / num;
          newState = 2;
          if (calc(newNums, result, ref newExpr, ref newVal, ref newState))  goto success;
          // 6. num / expr
          newExpr = num + "/" + (state >= 1 ? "(" + expr + ")" : expr);
          newVal = num / val;
          newState = 2;
          if (calc(newNums, result, ref newExpr, ref newVal, ref newState))  goto success;
        }
      }
      return false;
success:
      expr = newExpr;
      val = newVal;
      return true;
    }

    private void textBox2_TextChanged(object sender, EventArgs e) {
      try {
        if (txtA.Text.Trim().Length == 0 ||
          txtB.Text.Trim().Length == 0 ||
          txtC.Text.Trim().Length == 0 ||
          txtD.Text.Trim().Length == 0 ) return;
        int a = int.Parse(txtA.Text);
        int b = int.Parse(txtB.Text);
        int c = int.Parse(txtC.Text);
        int d = int.Parse(txtD.Text);
        if (a < 1 || a >13 ||
          b < 1 || b >13 ||
          c < 1 || c >13 ||
          d < 1 || d >13 ) throw new Exception("Number must be 1~13");

        lblResult.Text = twenty_four(new int[] {a, b, c, d});
        if (String.IsNullOrEmpty(lblResult.Text)) lblResult.Text = "No Result";
      } catch (Exception ex) {
        lblResult.Text = ex.Message;
      }
      this.Cursor = Cursors.Default;
    }
  }
}
