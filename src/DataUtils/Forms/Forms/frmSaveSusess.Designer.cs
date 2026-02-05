namespace Feng.Forms
{
    partial class frmSaveSusess
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
            this.txttitle = new System.Windows.Forms.Label();
            this.txtdesc = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer();
            this.txtCount = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // txttitle
            // 
            this.txttitle.AutoSize = true;
            this.txttitle.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txttitle.ForeColor = System.Drawing.Color.DimGray;
            this.txttitle.Location = new System.Drawing.Point(117, 23);
            this.txttitle.Name = "txttitle";
            this.txttitle.Size = new System.Drawing.Size(129, 20);
            this.txttitle.TabIndex = 0;
            this.txttitle.Text = "已保存成功！";
            this.txttitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtdesc_MouseDown);
            // 
            // txtdesc
            // 
            this.txtdesc.Location = new System.Drawing.Point(26, 55);
            this.txtdesc.Name = "txtdesc";
            this.txtdesc.Size = new System.Drawing.Size(312, 42);
            this.txtdesc.TabIndex = 1;
            this.txtdesc.Text = "label2";
            this.txtdesc.Visible = false;
            this.txtdesc.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtdesc_MouseDown);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 600;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtCount
            // 
            this.txtCount.AutoSize = true;
            this.txtCount.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.txtCount.Location = new System.Drawing.Point(263, 100);
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(95, 12);
            this.txtCount.TabIndex = 1;
            this.txtCount.Text = "[3]秒后自动关闭";
            this.txtCount.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtdesc_MouseDown);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 125);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(363, 1);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Feng.Utils.Properties.Resources.ToolBarClose;
            this.pictureBox1.Location = new System.Drawing.Point(335, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            this.pictureBox1.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            // 
            // frmSaveSusess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(363, 126);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.txtdesc);
            this.Controls.Add(this.txttitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(382, 154);
            this.MinimizeBox = false;
            this.Name = "frmSaveSusess";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "保存成功";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmSaveSusess_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmSaveSusess_MouseDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label txtdesc;
        private System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Label txttitle;
        public System.Windows.Forms.Label txtCount;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}