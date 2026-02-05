namespace Feng.Excel.Forms
{
    partial class frmColumnWidth
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.chkfullColumn = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "列宽：";
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(88, 15);
            this.txtInput.Margin = new System.Windows.Forms.Padding(4);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(344, 25);
            this.txtInput.TabIndex = 1;
            this.txtInput.Text = "36";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(328, 82);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 29);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定(&E)";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // chkfullColumn
            // 
            this.chkfullColumn.AutoSize = true;
            this.chkfullColumn.Location = new System.Drawing.Point(88, 47);
            this.chkfullColumn.Name = "chkfullColumn";
            this.chkfullColumn.Size = new System.Drawing.Size(74, 19);
            this.chkfullColumn.TabIndex = 5;
            this.chkfullColumn.Text = "全部列";
            this.chkfullColumn.UseVisualStyleBackColor = true;
            // 
            // frmColumnWidth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 124);
            this.Controls.Add(this.chkfullColumn);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmColumnWidth";
            this.ShowIcon = false;
            this.Text = "更改列宽";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.CheckBox chkfullColumn;
    }
}