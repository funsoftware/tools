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

    private bool calc(int[] nums, ref string expr, ref double val, ref int state) {
      if (nums.Length == 0) {
        return val == 24.000000000000f;
      }
      for (int i = 0; i < nums.Length; i++) {
        int num = nums[i];
        List<int> temp = new List<int>(nums);
        temp.RemoveAt(i);
        int[] newNums = temp.ToArray();
        String newExpr = expr;
        double newVal = val;
        int newState = state;
        if (state == -1) { // Not start
          newExpr = num.ToString();
          newVal = num;
          newState = 0;
          if (calc(newNums, ref newExpr, ref newVal, ref newState)) {
            expr = newExpr;
            val = newVal;
            return true;
          }
        } else {

          newExpr = expr + "-" + num;
          newVal = val - num;
          newState = 1;
          if (calc(newNums, ref newExpr, ref newVal, ref newState)) {
            expr = newExpr;
            val = newVal;
            return true;
          }

          newExpr = num + "+" + expr;
          newVal = num + val;
          newState = 1;
          if (calc(newNums, ref newExpr, ref newVal, ref newState)) {
            expr = newExpr;
            val = newVal;
            return true;
          }

          newExpr = num + "-" + (state == 1 ? "(" + expr + ")" : expr);
          newVal = num - val;
          newState = 1;
          if (calc(newNums, ref newExpr, ref newVal, ref newState)) {
            expr = newExpr;
            val = newVal;
            return true;
          }

          newExpr = (state == 1? "(" + expr + ")" : expr) + "*" + num;
          newVal = val * num;
          newState = 2;
          if (calc(newNums, ref newExpr, ref newVal, ref newState)) {
            expr = newExpr;
            val = newVal;
            return true;
          }

          newExpr = (state == 1 ? "(" + expr + ")" : expr) + "/" + num;
          newVal = val / num;
          newState = 2;
          if (calc(newNums, ref newExpr, ref newVal, ref newState)) {
            expr = newExpr;
            val = newVal;
            return true;
          }

          newExpr = num + "/" + (state >= 1 ? "(" + expr + ")" : expr);
          newVal = num / val;
          newState = 2;
          if (calc(newNums, ref newExpr, ref newVal, ref newState)) {
            expr = newExpr;
            val = newVal;
            return true;
          }
        }
      }
      return false;
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
        checkNum(a);
        checkNum(b);
        checkNum(c);
        checkNum(d);
        String expr = "";
        double val = 0;
        int state = -1;
        this.Cursor = Cursors.WaitCursor;
        bool m = calc(new int[] {a, b, c, d}, ref expr, ref val, ref state);

        if (m) lblResult.Text = expr;
        else lblResult.Text = "No Result";
      } catch (Exception ex) {
        lblResult.Text = ex.Message;
      }
      this.Cursor = Cursors.Default;
    }
    private void checkNum(int num) {
      if (num < 1 || num >13) throw new Exception("Number must be 1~13");
    }

    private void mainForm_Load(object sender, EventArgs e) {
      lblResult.Text = "";
    }


  }
}
