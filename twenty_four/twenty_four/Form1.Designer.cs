namespace twenty_four {
  partial class mainForm {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
      this.txtC = new System.Windows.Forms.TextBox();
      this.txtD = new System.Windows.Forms.TextBox();
      this.txtA = new System.Windows.Forms.TextBox();
      this.txtB = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.lblResult = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // txtC
      // 
      this.txtC.Location = new System.Drawing.Point(147, 35);
      this.txtC.Name = "txtC";
      this.txtC.Size = new System.Drawing.Size(57, 20);
      this.txtC.TabIndex = 3;
      this.txtC.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
      // 
      // txtD
      // 
      this.txtD.Location = new System.Drawing.Point(216, 35);
      this.txtD.Name = "txtD";
      this.txtD.Size = new System.Drawing.Size(57, 20);
      this.txtD.TabIndex = 4;
      this.txtD.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
      // 
      // txtA
      // 
      this.txtA.Location = new System.Drawing.Point(15, 35);
      this.txtA.Name = "txtA";
      this.txtA.Size = new System.Drawing.Size(57, 20);
      this.txtA.TabIndex = 1;
      this.txtA.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
      // 
      // txtB
      // 
      this.txtB.Location = new System.Drawing.Point(84, 35);
      this.txtB.Name = "txtB";
      this.txtB.Size = new System.Drawing.Size(57, 20);
      this.txtB.TabIndex = 2;
      this.txtB.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(15, 76);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(40, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "Result:";
      // 
      // lblResult
      // 
      this.lblResult.AutoSize = true;
      this.lblResult.Location = new System.Drawing.Point(56, 76);
      this.lblResult.Name = "lblResult";
      this.lblResult.Size = new System.Drawing.Size(25, 13);
      this.lblResult.TabIndex = 6;
      this.lblResult.Text = "abc";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(15, 9);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(117, 13);
      this.label2.TabIndex = 0;
      this.label2.Text = "Input 4 numbers(1~13):";
      // 
      // mainForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(295, 107);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.lblResult);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.txtB);
      this.Controls.Add(this.txtA);
      this.Controls.Add(this.txtD);
      this.Controls.Add(this.txtC);
      this.Name = "mainForm";
      this.Text = "twenty four";
      this.Load += new System.EventHandler(this.mainForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox txtC;
    private System.Windows.Forms.TextBox txtD;
    private System.Windows.Forms.TextBox txtA;
    private System.Windows.Forms.TextBox txtB;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label lblResult;
    private System.Windows.Forms.Label label2;
  }
}

