namespace Feng.Forms.DataProject
{
    partial class FillDataDialog
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
            this.radUp = new System.Windows.Forms.RadioButton();
            this.radDown = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.radDownFill = new System.Windows.Forms.RadioButton();
            this.radUpFill = new System.Windows.Forms.RadioButton();
            this.radLeftFill = new System.Windows.Forms.RadioButton();
            this.radRightFill = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.numTextBox1 = new Feng.Forms.Controls.NumTextBox();
            this.numTextBox2 = new Feng.Forms.Controls.NumTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(159, 201);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "确定(&O)";
            this.btnOk.UseVisualStyleBackColor = true;
            // 
            // radUp
            // 
            this.radUp.AutoSize = true;
            this.radUp.Location = new System.Drawing.Point(9, 7);
            this.radUp.Name = "radUp";
            this.radUp.Size = new System.Drawing.Size(47, 16);
            this.radUp.TabIndex = 3;
            this.radUp.TabStop = true;
            this.radUp.Text = "递增";
            this.radUp.UseVisualStyleBackColor = true;
            // 
            // radDown
            // 
            this.radDown.AutoSize = true;
            this.radDown.Location = new System.Drawing.Point(110, 6);
            this.radDown.Name = "radDown";
            this.radDown.Size = new System.Drawing.Size(47, 16);
            this.radDown.TabIndex = 3;
            this.radDown.TabStop = true;
            this.radDown.Text = "递减";
            this.radDown.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "起始数：";
            // 
            // radDownFill
            // 
            this.radDownFill.AutoSize = true;
            this.radDownFill.Location = new System.Drawing.Point(12, 13);
            this.radDownFill.Name = "radDownFill";
            this.radDownFill.Size = new System.Drawing.Size(71, 16);
            this.radDownFill.TabIndex = 6;
            this.radDownFill.TabStop = true;
            this.radDownFill.Text = "向下填充";
            this.radDownFill.UseVisualStyleBackColor = true;
            // 
            // radUpFill
            // 
            this.radUpFill.AutoSize = true;
            this.radUpFill.Location = new System.Drawing.Point(111, 13);
            this.radUpFill.Name = "radUpFill";
            this.radUpFill.Size = new System.Drawing.Size(71, 16);
            this.radUpFill.TabIndex = 6;
            this.radUpFill.TabStop = true;
            this.radUpFill.Text = "向上填充";
            this.radUpFill.UseVisualStyleBackColor = true;
            // 
            // radLeftFill
            // 
            this.radLeftFill.AutoSize = true;
            this.radLeftFill.Location = new System.Drawing.Point(10, 47);
            this.radLeftFill.Name = "radLeftFill";
            this.radLeftFill.Size = new System.Drawing.Size(71, 16);
            this.radLeftFill.TabIndex = 6;
            this.radLeftFill.TabStop = true;
            this.radLeftFill.Text = "向左填充";
            this.radLeftFill.UseVisualStyleBackColor = true;
            // 
            // radRightFill
            // 
            this.radRightFill.AutoSize = true;
            this.radRightFill.Location = new System.Drawing.Point(111, 47);
            this.radRightFill.Name = "radRightFill";
            this.radRightFill.Size = new System.Drawing.Size(71, 16);
            this.radRightFill.TabIndex = 6;
            this.radRightFill.TabStop = true;
            this.radRightFill.Text = "向右填充";
            this.radRightFill.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radDown);
            this.panel1.Controls.Add(this.radUp);
            this.panel1.Location = new System.Drawing.Point(22, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 31);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.radDownFill);
            this.panel2.Controls.Add(this.radLeftFill);
            this.panel2.Controls.Add(this.radUpFill);
            this.panel2.Controls.Add(this.radRightFill);
            this.panel2.Location = new System.Drawing.Point(22, 70);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 77);
            this.panel2.TabIndex = 8;
            // 
            // numTextBox1
            // 
            this.numTextBox1.Location = new System.Drawing.Point(93, 43);
            this.numTextBox1.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.numTextBox1.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.numTextBox1.Name = "numTextBox1";
            this.numTextBox1.Size = new System.Drawing.Size(107, 21);
            this.numTextBox1.TabIndex = 4;
            this.numTextBox1.Text = "0";
            this.numTextBox1.Value1 = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // numTextBox2
            // 
            this.numTextBox2.Location = new System.Drawing.Point(93, 162);
            this.numTextBox2.MaxValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.numTextBox2.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.numTextBox2.Name = "numTextBox2";
            this.numTextBox2.Size = new System.Drawing.Size(107, 21);
            this.numTextBox2.TabIndex = 4;
            this.numTextBox2.Text = "0";
            this.numTextBox2.Value1 = new decimal(new int[] {
            0,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 165);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "填充数量：";
            // 
            // FillDataDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 233);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.numTextBox2);
            this.Controls.Add(this.numTextBox1);
            this.Controls.Add(this.btnOk);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FillDataDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "填充数字";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.RadioButton radUp;
        private System.Windows.Forms.RadioButton radDown;
        private Controls.NumTextBox numTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radDownFill;
        private System.Windows.Forms.RadioButton radUpFill;
        private System.Windows.Forms.RadioButton radLeftFill;
        private System.Windows.Forms.RadioButton radRightFill;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Controls.NumTextBox numTextBox2;
        private System.Windows.Forms.Label label2;
    }
}