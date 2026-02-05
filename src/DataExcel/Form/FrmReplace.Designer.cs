namespace Feng.Excel.Forms
{
    partial class FrmReplace
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
            this.txtFindText = new System.Windows.Forms.TextBox();
            this.btnReplaceAll = new System.Windows.Forms.Button();
            this.btnReplaceNext = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReplaceText = new System.Windows.Forms.TextBox();
            this.chkcaseSensitiveMatch = new System.Windows.Forms.CheckBox();
            this.txtCurrentCell = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 18);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "查找内容：";
            // 
            // txtFindText
            // 
            this.txtFindText.Location = new System.Drawing.Point(112, 13);
            this.txtFindText.Margin = new System.Windows.Forms.Padding(4);
            this.txtFindText.Name = "txtFindText";
            this.txtFindText.Size = new System.Drawing.Size(344, 25);
            this.txtFindText.TabIndex = 1;
            // 
            // btnReplaceAll
            // 
            this.btnReplaceAll.Location = new System.Drawing.Point(356, 109);
            this.btnReplaceAll.Margin = new System.Windows.Forms.Padding(4);
            this.btnReplaceAll.Name = "btnReplaceAll";
            this.btnReplaceAll.Size = new System.Drawing.Size(100, 29);
            this.btnReplaceAll.TabIndex = 2;
            this.btnReplaceAll.Text = "替换全部";
            this.btnReplaceAll.UseVisualStyleBackColor = true;
            this.btnReplaceAll.Click += new System.EventHandler(this.btnReplaceAll_Click);
            // 
            // btnReplaceNext
            // 
            this.btnReplaceNext.Location = new System.Drawing.Point(220, 109);
            this.btnReplaceNext.Margin = new System.Windows.Forms.Padding(4);
            this.btnReplaceNext.Name = "btnReplaceNext";
            this.btnReplaceNext.Size = new System.Drawing.Size(100, 29);
            this.btnReplaceNext.TabIndex = 2;
            this.btnReplaceNext.Text = "替换下一个";
            this.btnReplaceNext.UseVisualStyleBackColor = true;
            this.btnReplaceNext.Click += new System.EventHandler(this.btnReplaceNext_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 51);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 15);
            this.label2.TabIndex = 0;
            this.label2.Text = "替换内容：";
            // 
            // txtReplaceText
            // 
            this.txtReplaceText.Location = new System.Drawing.Point(112, 48);
            this.txtReplaceText.Margin = new System.Windows.Forms.Padding(4);
            this.txtReplaceText.Name = "txtReplaceText";
            this.txtReplaceText.Size = new System.Drawing.Size(344, 25);
            this.txtReplaceText.TabIndex = 1;
            // 
            // chkcaseSensitiveMatch
            // 
            this.chkcaseSensitiveMatch.AutoSize = true;
            this.chkcaseSensitiveMatch.Location = new System.Drawing.Point(112, 82);
            this.chkcaseSensitiveMatch.Name = "chkcaseSensitiveMatch";
            this.chkcaseSensitiveMatch.Size = new System.Drawing.Size(104, 19);
            this.chkcaseSensitiveMatch.TabIndex = 3;
            this.chkcaseSensitiveMatch.Text = "区分大小写";
            this.chkcaseSensitiveMatch.UseVisualStyleBackColor = true;
            // 
            // txtCurrentCell
            // 
            this.txtCurrentCell.AutoSize = true;
            this.txtCurrentCell.Location = new System.Drawing.Point(25, 124);
            this.txtCurrentCell.Name = "txtCurrentCell";
            this.txtCurrentCell.Size = new System.Drawing.Size(0, 15);
            this.txtCurrentCell.TabIndex = 4;
            this.txtCurrentCell.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.txtCurrentCell_LinkClicked);
            // 
            // FrmReplace
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 151);
            this.Controls.Add(this.txtCurrentCell);
            this.Controls.Add(this.chkcaseSensitiveMatch);
            this.Controls.Add(this.btnReplaceNext);
            this.Controls.Add(this.btnReplaceAll);
            this.Controls.Add(this.txtReplaceText);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtFindText);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmReplace";
            this.ShowIcon = false;
            this.Text = "替换";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox txtFindText;
        private System.Windows.Forms.Button btnReplaceAll;
        private System.Windows.Forms.Button btnReplaceNext;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox txtReplaceText;
        private System.Windows.Forms.CheckBox chkcaseSensitiveMatch;
        private System.Windows.Forms.LinkLabel txtCurrentCell;
    }
}