namespace Feng.Excel.Forms
{
    partial class frmDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDialog));
            this.dataExcel1 = new Feng.Excel.DataExcelControl();
            this.SuspendLayout();
            // 
            // dataExcelControl1
            // 
            this.dataExcel1.BorderColor = System.Drawing.Color.Empty;
            this.dataExcel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataExcel1.Location = new System.Drawing.Point(0, 0);
            this.dataExcel1.Name = "dataExcelControl1";
            this.dataExcel1.Size = new System.Drawing.Size(648, 508);
            this.dataExcel1.TabIndex = 0;
            this.dataExcel1.Text = "dataExcelControl1";
            // 
            // frmDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 508);
            this.Controls.Add(this.dataExcel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDialog";
            this.ResumeLayout(false);

        }

        #endregion

        private DataExcelControl dataExcel1;
    }
}