namespace Feng.Excel.Designer
{
    partial class DataExcelDesignerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataExcelDesignerForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ToolStripButton_New = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButton_Open = new System.Windows.Forms.ToolStripButton();
            this.ToolStripButton_Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripButton_New,
            this.ToolStripButton_Open,
            this.ToolStripButton_Save,
            this.toolStripSeparator});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(555, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(555, 353);
            this.panel1.TabIndex = 1;
            // 
            // ToolStripButton_New
            // 
            this.ToolStripButton_New.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButton_New.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton_New.Image")));
            this.ToolStripButton_New.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton_New.Name = "ToolStripButton_New";
            this.ToolStripButton_New.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButton_New.Text = "新建(&N)";
            this.ToolStripButton_New.Click += new System.EventHandler(this.ToolStripButton_New_Click);
            // 
            // ToolStripButton_Open
            // 
            this.ToolStripButton_Open.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButton_Open.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton_Open.Image")));
            this.ToolStripButton_Open.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton_Open.Name = "ToolStripButton_Open";
            this.ToolStripButton_Open.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButton_Open.Text = "打开(&O)";
            this.ToolStripButton_Open.Click += new System.EventHandler(this.ToolStripButton_Open_Click);
            // 
            // ToolStripButton_Save
            // 
            this.ToolStripButton_Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ToolStripButton_Save.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButton_Save.Image")));
            this.ToolStripButton_Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ToolStripButton_Save.Name = "ToolStripButton_Save";
            this.ToolStripButton_Save.Size = new System.Drawing.Size(23, 22);
            this.ToolStripButton_Save.Text = "保存(&S)";
            this.ToolStripButton_Save.Click += new System.EventHandler(this.ToolStripButton_Save_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 378);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "Form1";
            this.Text = "设计器";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripButton ToolStripButton_New;
        private System.Windows.Forms.ToolStripButton ToolStripButton_Open;
        private System.Windows.Forms.ToolStripButton ToolStripButton_Save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
    }
}