namespace Feng.Excel.Forms
{
    partial class selDropDownForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(selDropDownForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainerMainChild = new System.Windows.Forms.SplitContainer();
            this.dataExcel1 = new  DataExcelControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainChild)).BeginInit();
            this.splitContainerMainChild.Panel1.SuspendLayout();
            this.splitContainerMainChild.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            this.splitContainer1.Panel1Collapsed = true;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(345, 335);
            this.splitContainer1.SplitterDistance = 39;
            this.splitContainer1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.splitContainer2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(345, 335);
            this.panel1.TabIndex = 0;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainerMainChild);
            this.splitContainer2.Panel2Collapsed = true;
            this.splitContainer2.Size = new System.Drawing.Size(345, 335);
            this.splitContainer2.SplitterDistance = 266;
            this.splitContainer2.TabIndex = 1;
            // 
            // splitContainerMainChild
            // 
            this.splitContainerMainChild.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMainChild.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMainChild.Name = "splitContainerMainChild";
            this.splitContainerMainChild.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerMainChild.Panel1
            //  
            this.splitContainerMainChild.Panel2Collapsed = true;
            this.splitContainerMainChild.Size = new System.Drawing.Size(345, 335);
            this.splitContainerMainChild.SplitterDistance = 170;
            this.splitContainerMainChild.TabIndex = 1;
            // 
            // dataExcel1
            //  
            this.dataExcel1.EditView.BorderColor = System.Drawing.Color.Gray; 
            this.dataExcel1.EditView.Font = new System.Drawing.Font("Tahoma", 9F); 
            this.dataExcel1.EditView.Location = new System.Drawing.Point(0, 0); 
            this.dataExcel1.EditView.Text = "dataExcel1";
            // 
            // selDropDownForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 335);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "selDropDownForm";
            this.Text = "选择";
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainerMainChild.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMainChild)).EndInit();
            this.splitContainerMainChild.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private Feng.Excel.DataExcelControl dataExcel1;
        private System.Windows.Forms.SplitContainer splitContainer2;

        private System.Windows.Forms.SplitContainer splitContainerMainChild;

    }
}