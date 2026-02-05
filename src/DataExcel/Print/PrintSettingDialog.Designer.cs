namespace Feng.Excel.Print
{
    partial class PrintSettingDialog
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtPrinter = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCopies = new System.Windows.Forms.NumericUpDown();
            this.txtPaperKind = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.txtLandscape2 = new System.Windows.Forms.RadioButton();
            this.txtLandscape1 = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.txtdown = new System.Windows.Forms.NumericUpDown();
            this.txttop = new System.Windows.Forms.NumericUpDown();
            this.txtright = new System.Windows.Forms.NumericUpDown();
            this.txtleft = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtToPage = new System.Windows.Forms.NumericUpDown();
            this.txtFromPage = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCopies)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtdown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttop)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtright)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtleft)).BeginInit();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtToPage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromPage)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(118, 418);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "确定(&E)";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(204, 418);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "取消(&C)";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(118, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(146, 109);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPrinter);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(12, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(356, 50);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印机 ";
            // 
            // txtPrinter
            // 
            this.txtPrinter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtPrinter.FormattingEnabled = true;
            this.txtPrinter.Location = new System.Drawing.Point(65, 17);
            this.txtPrinter.Name = "txtPrinter";
            this.txtPrinter.Size = new System.Drawing.Size(285, 20);
            this.txtPrinter.TabIndex = 0;
            this.txtPrinter.SelectedIndexChanged += new System.EventHandler(this.txtPrinter_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "打印机:";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtCopies);
            this.groupBox2.Controls.Add(this.txtPaperKind);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Location = new System.Drawing.Point(12, 189);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(356, 50);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "纸张";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(333, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "份";
            // 
            // txtCopies
            // 
            this.txtCopies.Location = new System.Drawing.Point(273, 17);
            this.txtCopies.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtCopies.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtCopies.Name = "txtCopies";
            this.txtCopies.Size = new System.Drawing.Size(60, 21);
            this.txtCopies.TabIndex = 1;
            this.txtCopies.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtPaperKind
            // 
            this.txtPaperKind.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.txtPaperKind.FormattingEnabled = true;
            this.txtPaperKind.Location = new System.Drawing.Point(65, 18);
            this.txtPaperKind.Name = "txtPaperKind";
            this.txtPaperKind.Size = new System.Drawing.Size(199, 20);
            this.txtPaperKind.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(35, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "大小:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.txtLandscape2);
            this.groupBox3.Controls.Add(this.txtLandscape1);
            this.groupBox3.Location = new System.Drawing.Point(12, 250);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(100, 95);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "方向";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(7, 72);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(90, 16);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "角度垂直(&V)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // txtLandscape2
            // 
            this.txtLandscape2.AutoSize = true;
            this.txtLandscape2.Location = new System.Drawing.Point(6, 43);
            this.txtLandscape2.Name = "txtLandscape2";
            this.txtLandscape2.Size = new System.Drawing.Size(65, 16);
            this.txtLandscape2.TabIndex = 1;
            this.txtLandscape2.TabStop = true;
            this.txtLandscape2.Text = "横向(&A)";
            this.txtLandscape2.UseVisualStyleBackColor = true;
            // 
            // txtLandscape1
            // 
            this.txtLandscape1.AutoSize = true;
            this.txtLandscape1.Location = new System.Drawing.Point(6, 17);
            this.txtLandscape1.Name = "txtLandscape1";
            this.txtLandscape1.Size = new System.Drawing.Size(65, 16);
            this.txtLandscape1.TabIndex = 0;
            this.txtLandscape1.TabStop = true;
            this.txtLandscape1.Text = "纵向(&O)";
            this.txtLandscape1.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.txtdown);
            this.groupBox4.Controls.Add(this.txttop);
            this.groupBox4.Controls.Add(this.txtright);
            this.groupBox4.Controls.Add(this.txtleft);
            this.groupBox4.Controls.Add(this.label6);
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(118, 250);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(250, 95);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "页边距";
            // 
            // txtdown
            // 
            this.txtdown.DecimalPlaces = 2;
            this.txtdown.Location = new System.Drawing.Point(161, 59);
            this.txtdown.Name = "txtdown";
            this.txtdown.Size = new System.Drawing.Size(78, 21);
            this.txtdown.TabIndex = 3;
            this.txtdown.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // txttop
            // 
            this.txttop.DecimalPlaces = 2;
            this.txttop.Location = new System.Drawing.Point(161, 25);
            this.txttop.Name = "txttop";
            this.txttop.Size = new System.Drawing.Size(78, 21);
            this.txttop.TabIndex = 1;
            this.txttop.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // txtright
            // 
            this.txtright.DecimalPlaces = 2;
            this.txtright.Location = new System.Drawing.Point(36, 59);
            this.txtright.Name = "txtright";
            this.txtright.Size = new System.Drawing.Size(78, 21);
            this.txtright.TabIndex = 2;
            this.txtright.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // txtleft
            // 
            this.txtleft.DecimalPlaces = 2;
            this.txtleft.Location = new System.Drawing.Point(36, 25);
            this.txtleft.Name = "txtleft";
            this.txtleft.Size = new System.Drawing.Size(78, 21);
            this.txtleft.TabIndex = 0;
            this.txtleft.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(138, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(23, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "下:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(138, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(23, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "上:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(23, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "右:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "左:";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.checkBox2);
            this.groupBox5.Controls.Add(this.label9);
            this.groupBox5.Controls.Add(this.label8);
            this.groupBox5.Controls.Add(this.label7);
            this.groupBox5.Controls.Add(this.txtToPage);
            this.groupBox5.Controls.Add(this.txtFromPage);
            this.groupBox5.Location = new System.Drawing.Point(12, 351);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(356, 50);
            this.groupBox5.TabIndex = 2;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "打印页范围:";
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Checked = true;
            this.checkBox2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox2.Location = new System.Drawing.Point(8, 23);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(60, 16);
            this.checkBox2.TabIndex = 2;
            this.checkBox2.Text = "全部页";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Enabled = false;
            this.label9.Location = new System.Drawing.Point(332, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(17, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "页";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Enabled = false;
            this.label8.Location = new System.Drawing.Point(253, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "-";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Enabled = false;
            this.label7.Location = new System.Drawing.Point(110, 25);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "打印页范围:";
            // 
            // txtToPage
            // 
            this.txtToPage.Enabled = false;
            this.txtToPage.Location = new System.Drawing.Point(267, 21);
            this.txtToPage.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtToPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtToPage.Name = "txtToPage";
            this.txtToPage.ReadOnly = true;
            this.txtToPage.Size = new System.Drawing.Size(60, 21);
            this.txtToPage.TabIndex = 1;
            this.txtToPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // txtFromPage
            // 
            this.txtFromPage.Enabled = false;
            this.txtFromPage.Location = new System.Drawing.Point(187, 21);
            this.txtFromPage.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtFromPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtFromPage.Name = "txtFromPage";
            this.txtFromPage.ReadOnly = true;
            this.txtFromPage.Size = new System.Drawing.Size(60, 21);
            this.txtFromPage.TabIndex = 1;
            this.txtFromPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // PrintDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(380, 454);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(388, 488);
            this.MinimumSize = new System.Drawing.Size(388, 488);
            this.Name = "PrintDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "打印设置";
            this.Load += new System.EventHandler(this.PrintDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtCopies)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtdown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txttop)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtright)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtleft)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtToPage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFromPage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label10;
        public System.Windows.Forms.ComboBox txtPrinter;
        public System.Windows.Forms.ComboBox txtPaperKind;
        public System.Windows.Forms.NumericUpDown txtleft;
        public System.Windows.Forms.RadioButton txtLandscape1;
        public System.Windows.Forms.NumericUpDown txtdown;
        public System.Windows.Forms.NumericUpDown txttop;
        public System.Windows.Forms.NumericUpDown txtright;
        public System.Windows.Forms.RadioButton txtLandscape2;
        public System.Windows.Forms.CheckBox checkBox1;
        public System.Windows.Forms.NumericUpDown txtCopies;
        public System.Windows.Forms.Button btnOk;
        public System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label7;
        public System.Windows.Forms.NumericUpDown txtFromPage;
        public System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Label label8;
        public System.Windows.Forms.NumericUpDown txtToPage;
        private System.Windows.Forms.Label label9;
    }
}