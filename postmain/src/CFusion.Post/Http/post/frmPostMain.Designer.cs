
namespace CFusion.Http.post
{
    partial class frmPostMain
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPostMain));
            this.panel1 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.treeProject = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnProjectTreeRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnNodeAddFile = new System.Windows.Forms.ToolStripButton();
            this.btnNodeRemove = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnClosePage = new System.Windows.Forms.Button();
            this.tabControlPage = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ctrlPostControl1 = new CFusion.Http.post.ctrlPostControl();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnUrl = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControlPage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1031, 37);
            this.panel1.TabIndex = 0;
            this.panel1.Visible = false;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 37);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.treeProject);
            this.splitContainer1.Panel1.Controls.Add(this.toolStrip1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(1031, 563);
            this.splitContainer1.SplitterDistance = 213;
            this.splitContainer1.TabIndex = 1;
            // 
            // treeProject
            // 
            this.treeProject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeProject.HideSelection = false;
            this.treeProject.ImageIndex = 0;
            this.treeProject.ImageList = this.imageList1;
            this.treeProject.Location = new System.Drawing.Point(0, 27);
            this.treeProject.Name = "treeProject";
            this.treeProject.SelectedImageKey = "postmain_projecttreereselect.png";
            this.treeProject.Size = new System.Drawing.Size(213, 536);
            this.treeProject.TabIndex = 1;
            this.treeProject.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeProject_NodeMouseClick);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "DIRECTORY");
            this.imageList1.Images.SetKeyName(1, "FILE");
            this.imageList1.Images.SetKeyName(2, "postmain_projecttreereselect.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnProjectTreeRefresh,
            this.btnNodeAddFile,
            this.btnNodeRemove,
            this.toolStripButton1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(213, 27);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "Open Explorer";
            // 
            // btnProjectTreeRefresh
            // 
            this.btnProjectTreeRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnProjectTreeRefresh.Image = global::CFusion.Post.Properties.Resources.postmain_projecttreerefresh;
            this.btnProjectTreeRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnProjectTreeRefresh.Name = "btnProjectTreeRefresh";
            this.btnProjectTreeRefresh.Size = new System.Drawing.Size(29, 24);
            this.btnProjectTreeRefresh.Text = "Refresh";
            this.btnProjectTreeRefresh.Click += new System.EventHandler(this.btnProjectTreeRefresh_Click);
            // 
            // btnNodeAddFile
            // 
            this.btnNodeAddFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNodeAddFile.Image = global::CFusion.Post.Properties.Resources.NodeAddFile_16x16;
            this.btnNodeAddFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNodeAddFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNodeAddFile.Name = "btnNodeAddFile";
            this.btnNodeAddFile.Size = new System.Drawing.Size(29, 24);
            this.btnNodeAddFile.Text = "New Url";
            this.btnNodeAddFile.Click += new System.EventHandler(this.btnNodeAddFile_Click);
            // 
            // btnNodeRemove
            // 
            this.btnNodeRemove.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnNodeRemove.Image = global::CFusion.Post.Properties.Resources.NodeRemove_16x16;
            this.btnNodeRemove.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnNodeRemove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNodeRemove.Name = "btnNodeRemove";
            this.btnNodeRemove.Size = new System.Drawing.Size(29, 24);
            this.btnNodeRemove.Text = "Delete";
            this.btnNodeRemove.Click += new System.EventHandler(this.btnNodeRemove_Click);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = global::CFusion.Post.Properties.Resources.OpenHyperlink_16x16;
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(29, 24);
            this.toolStripButton1.Text = "Open Explorer";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnClosePage);
            this.panel2.Controls.Add(this.tabControlPage);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(814, 563);
            this.panel2.TabIndex = 0;
            // 
            // btnClosePage
            // 
            this.btnClosePage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClosePage.Image = global::CFusion.Post.Properties.Resources.Close_12x_16x;
            this.btnClosePage.Location = new System.Drawing.Point(782, 0);
            this.btnClosePage.Name = "btnClosePage";
            this.btnClosePage.Size = new System.Drawing.Size(30, 23);
            this.btnClosePage.TabIndex = 1;
            this.btnClosePage.UseVisualStyleBackColor = true;
            this.btnClosePage.Click += new System.EventHandler(this.btnClosePage_Click);
            // 
            // tabControlPage
            // 
            this.tabControlPage.Controls.Add(this.tabPage1);
            this.tabControlPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPage.ImageList = this.imageList1;
            this.tabControlPage.Location = new System.Drawing.Point(0, 0);
            this.tabControlPage.Name = "tabControlPage";
            this.tabControlPage.SelectedIndex = 0;
            this.tabControlPage.Size = new System.Drawing.Size(814, 563);
            this.tabControlPage.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ctrlPostControl1);
            this.tabPage1.ImageIndex = 2;
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(806, 534);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "New";
            this.tabPage1.ToolTipText = "New";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // ctrlPostControl1
            // 
            this.ctrlPostControl1.BackColor = System.Drawing.Color.Transparent;
            this.ctrlPostControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ctrlPostControl1.Location = new System.Drawing.Point(3, 3);
            this.ctrlPostControl1.Name = "ctrlPostControl1";
            this.ctrlPostControl1.Size = new System.Drawing.Size(800, 528);
            this.ctrlPostControl1.TabIndex = 0;
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel4,
            this.btnUrl,
            this.toolStripStatusLabel5});
            this.statusStrip1.Location = new System.Drawing.Point(0, 600);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1031, 26);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(112, 20);
            this.toolStripStatusLabel1.Text = "TCP MAPPING";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(0, 20);
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(94, 20);
            this.toolStripStatusLabel3.Text = "Http Server";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(134, 20);
            this.toolStripStatusLabel4.Text = "Version:1.0.12.13";
            // 
            // btnUrl
            // 
            this.btnUrl.Name = "btnUrl";
            this.btnUrl.Size = new System.Drawing.Size(194, 20);
            this.btnUrl.Text = "https://www.dataexcel.cn";
            this.btnUrl.Click += new System.EventHandler(this.btnUrl_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(441, 20);
            this.toolStripStatusLabel5.Text = "OpenSource:https://github.com/yufb12/dataexcel_winform";
            this.toolStripStatusLabel5.Click += new System.EventHandler(this.toolStripStatusLabel5_Click);
            // 
            // frmPostMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 626);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmPostMain";
            this.Text = "CFusion.Post PostMain";
            this.Load += new System.EventHandler(this.frmPostMain_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControlPage.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.TreeView treeProject;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControlPage;
        private System.Windows.Forms.TabPage tabPage1;
        private ctrlPostControl ctrlPostControl1;
        private System.Windows.Forms.ToolStripButton btnNodeAddFile;
        private System.Windows.Forms.ToolStripButton btnNodeRemove;
        private System.Windows.Forms.ToolStripButton btnProjectTreeRefresh;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnClosePage;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ToolStripStatusLabel btnUrl;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
    }
}