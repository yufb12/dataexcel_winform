namespace Feng.Excel.Forms
{
    partial class FrmFind
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
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.chkcaseSensitiveMatch = new System.Windows.Forms.CheckBox();
            this.chkfullWordMatch = new System.Windows.Forms.CheckBox();
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
            this.label1.Text = "内容：";
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(88, 15);
            this.txtInput.Margin = new System.Windows.Forms.Padding(4);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(344, 25);
            this.txtInput.TabIndex = 1;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(328, 82);
            this.btnNext.Margin = new System.Windows.Forms.Padding(4);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(100, 29);
            this.btnNext.TabIndex = 2;
            this.btnNext.Text = "下一个";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(220, 82);
            this.btnPrev.Margin = new System.Windows.Forms.Padding(4);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(100, 29);
            this.btnPrev.TabIndex = 2;
            this.btnPrev.Text = "上一个";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // chkcaseSensitiveMatch
            // 
            this.chkcaseSensitiveMatch.AutoSize = true;
            this.chkcaseSensitiveMatch.Location = new System.Drawing.Point(216, 47);
            this.chkcaseSensitiveMatch.Name = "chkcaseSensitiveMatch";
            this.chkcaseSensitiveMatch.Size = new System.Drawing.Size(104, 19);
            this.chkcaseSensitiveMatch.TabIndex = 4;
            this.chkcaseSensitiveMatch.Text = "区分大小写";
            this.chkcaseSensitiveMatch.UseVisualStyleBackColor = true;
            // 
            // chkfullWordMatch
            // 
            this.chkfullWordMatch.AutoSize = true;
            this.chkfullWordMatch.Location = new System.Drawing.Point(88, 47);
            this.chkfullWordMatch.Name = "chkfullWordMatch";
            this.chkfullWordMatch.Size = new System.Drawing.Size(89, 19);
            this.chkfullWordMatch.TabIndex = 5;
            this.chkfullWordMatch.Text = "全字匹配";
            this.chkfullWordMatch.UseVisualStyleBackColor = true;
            // 
            // FrmFind
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 124);
            this.Controls.Add(this.chkcaseSensitiveMatch);
            this.Controls.Add(this.chkfullWordMatch);
            this.Controls.Add(this.btnPrev);
            this.Controls.Add(this.btnNext);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmFind";
            this.ShowIcon = false;
            this.Text = "查找";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.CheckBox chkcaseSensitiveMatch;
        private System.Windows.Forms.CheckBox chkfullWordMatch;
    }
}