//using System.Windows.Forms;
//namespace Feng.Assistant
//{
//    partial class frmSysEventMainFormTree
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>

//        private void InitializeComponent()
//        {
//            this.components = new System.ComponentModel.Container();
//            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSysEventMainFormTree));
//            this.button3 = new System.Windows.Forms.Button();
//            this.label2 = new System.Windows.Forms.Label();
//            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
//            this.插入动作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.删除动作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.定时执行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.执行次数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.导出单个脚本ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.btnExport = new System.Windows.Forms.LinkLabel();
//            this.btnMKSys = new System.Windows.Forms.LinkLabel();
//            this.btnAddAction = new System.Windows.Forms.LinkLabel();
//            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
//            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
//            this.隐藏HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.显示SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.退出EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.btnBegin = new System.Windows.Forms.Button();
//            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
//            this.btnPause = new System.Windows.Forms.Button();
//            this.labStartTime = new System.Windows.Forms.LinkLabel();
//            this.treeView1 = new System.Windows.Forms.TreeView();
//            this.修改名称ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
//            this.contextMenuStrip1.SuspendLayout();
//            this.contextMenuStrip2.SuspendLayout();
//            this.SuspendLayout();
//            // 
//            // button3
//            // 
//            this.button3.Location = new System.Drawing.Point(164, 9);
//            this.button3.Name = "button3";
//            this.button3.Size = new System.Drawing.Size(71, 23);
//            this.button3.TabIndex = 0;
//            this.button3.Text = "新脚本";
//            this.button3.UseVisualStyleBackColor = true;
//            this.button3.Click += new System.EventHandler(this.btnNewScript_Click);
//            // 
//            // label2
//            // 
//            this.label2.AutoSize = true;
//            this.label2.ForeColor = System.Drawing.Color.Red;
//            this.label2.Location = new System.Drawing.Point(12, 35);
//            this.label2.Name = "label2";
//            this.label2.Size = new System.Drawing.Size(119, 12);
//            this.label2.TabIndex = 5;
//            this.label2.Text = "常按住CTL键可以暂停";
//            // 
//            // contextMenuStrip1
//            // 
//            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
//            this.插入动作ToolStripMenuItem,
//            this.删除动作ToolStripMenuItem,
//            this.定时执行ToolStripMenuItem,
//            this.执行次数ToolStripMenuItem,
//            this.导出单个脚本ToolStripMenuItem,
//            this.修改名称ToolStripMenuItem});
//            this.contextMenuStrip1.Name = "contextMenuStrip1";
//            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 158);
//            // 
//            // 插入动作ToolStripMenuItem
//            // 
//            this.插入动作ToolStripMenuItem.Name = "插入动作ToolStripMenuItem";
//            this.插入动作ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
//            this.插入动作ToolStripMenuItem.Text = "插入动作";
//            this.插入动作ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemInsertAction_Click);
//            // 
//            // 删除动作ToolStripMenuItem
//            // 
//            this.删除动作ToolStripMenuItem.Name = "删除动作ToolStripMenuItem";
//            this.删除动作ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
//            this.删除动作ToolStripMenuItem.Text = "删除动作";
//            this.删除动作ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemDelAction_Click);
//            // 
//            // 定时执行ToolStripMenuItem
//            // 
//            this.定时执行ToolStripMenuItem.Name = "定时执行ToolStripMenuItem";
//            this.定时执行ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
//            this.定时执行ToolStripMenuItem.Text = "定时执行";
//            // 
//            // 执行次数ToolStripMenuItem
//            // 
//            this.执行次数ToolStripMenuItem.Name = "执行次数ToolStripMenuItem";
//            this.执行次数ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
//            this.执行次数ToolStripMenuItem.Text = "执行次数";
//            this.执行次数ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemExecTimes_Click);
//            // 
//            // 导出单个脚本ToolStripMenuItem
//            // 
//            this.导出单个脚本ToolStripMenuItem.Name = "导出单个脚本ToolStripMenuItem";
//            this.导出单个脚本ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
//            this.导出单个脚本ToolStripMenuItem.Text = "导出单个脚本";
//            this.导出单个脚本ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemExportScript_Click);
//            // 
//            // btnExport
//            // 
//            this.btnExport.AutoSize = true;
//            this.btnExport.Location = new System.Drawing.Point(11, 52);
//            this.btnExport.Name = "btnExport";
//            this.btnExport.Size = new System.Drawing.Size(29, 12);
//            this.btnExport.TabIndex = 10;
//            this.btnExport.TabStop = true;
//            this.btnExport.Text = "导入";
//            this.btnExport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnExport_LinkClicked);
//            // 
//            // btnMKSys
//            // 
//            this.btnMKSys.AutoSize = true;
//            this.btnMKSys.Location = new System.Drawing.Point(158, 52);
//            this.btnMKSys.Name = "btnMKSys";
//            this.btnMKSys.Size = new System.Drawing.Size(77, 12);
//            this.btnMKSys.TabIndex = 10;
//            this.btnMKSys.TabStop = true;
//            this.btnMKSys.Text = "打开脚本中心";
//            // 
//            // btnAddAction
//            // 
//            this.btnAddAction.AutoSize = true;
//            this.btnAddAction.Location = new System.Drawing.Point(43, 52);
//            this.btnAddAction.Name = "btnAddAction";
//            this.btnAddAction.Size = new System.Drawing.Size(53, 12);
//            this.btnAddAction.TabIndex = 10;
//            this.btnAddAction.TabStop = true;
//            this.btnAddAction.Text = "添加动作";
//            this.btnAddAction.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnAddAction_LinkClicked);
//            // 
//            // notifyIcon1
//            // 
//            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
//            this.notifyIcon1.BalloonTipText = "模拟鼠标，键盘点击操作。";
//            this.notifyIcon1.BalloonTipTitle = "小熊鼠标键盘辅助工具V1.0.1.1";
//            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip2;
//            this.notifyIcon1.Text = "小熊鼠标键盘辅助工具V1.0.1.1";
//            this.notifyIcon1.Visible = true;
//            // 
//            // contextMenuStrip2
//            // 
//            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
//            this.隐藏HToolStripMenuItem,
//            this.显示SToolStripMenuItem,
//            this.退出EToolStripMenuItem});
//            this.contextMenuStrip2.Name = "contextMenuStrip2";
//            this.contextMenuStrip2.Size = new System.Drawing.Size(118, 70);
//            // 
//            // 隐藏HToolStripMenuItem
//            // 
//            this.隐藏HToolStripMenuItem.Name = "隐藏HToolStripMenuItem";
//            this.隐藏HToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
//            this.隐藏HToolStripMenuItem.Text = "隐藏(&H)";
//            this.隐藏HToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemHide_Click);
//            // 
//            // 显示SToolStripMenuItem
//            // 
//            this.显示SToolStripMenuItem.Name = "显示SToolStripMenuItem";
//            this.显示SToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
//            this.显示SToolStripMenuItem.Text = "显示(&S)";
//            this.显示SToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemShow_Click);
//            // 
//            // 退出EToolStripMenuItem
//            // 
//            this.退出EToolStripMenuItem.Name = "退出EToolStripMenuItem";
//            this.退出EToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
//            this.退出EToolStripMenuItem.Text = "退出(&E)";
//            this.退出EToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemExit_Click);
//            // 
//            // btnBegin
//            // 
//            this.btnBegin.Location = new System.Drawing.Point(13, 9);
//            this.btnBegin.Name = "btnBegin";
//            this.btnBegin.Size = new System.Drawing.Size(59, 23);
//            this.btnBegin.TabIndex = 0;
//            this.btnBegin.Text = "执行(&S)";
//            this.btnBegin.UseVisualStyleBackColor = true;
//            this.btnBegin.Click += new System.EventHandler(this.btnBegin_Click);
//            // 
//            // linkLabel2
//            // 
//            this.linkLabel2.AutoSize = true;
//            this.linkLabel2.Location = new System.Drawing.Point(102, 52);
//            this.linkLabel2.Name = "linkLabel2";
//            this.linkLabel2.Size = new System.Drawing.Size(29, 12);
//            this.linkLabel2.TabIndex = 10;
//            this.linkLabel2.TabStop = true;
//            this.linkLabel2.Text = "导出";
//            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnImort_LinkClicked);
//            // 
//            // btnPause
//            // 
//            this.btnPause.Location = new System.Drawing.Point(78, 9);
//            this.btnPause.Name = "btnPause";
//            this.btnPause.Size = new System.Drawing.Size(53, 23);
//            this.btnPause.TabIndex = 0;
//            this.btnPause.Text = "暂停";
//            this.btnPause.UseVisualStyleBackColor = true;
//            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
//            // 
//            // labStartTime
//            // 
//            this.labStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
//            this.labStartTime.AutoSize = true;
//            this.labStartTime.Location = new System.Drawing.Point(89, 569);
//            this.labStartTime.Name = "labStartTime";
//            this.labStartTime.Size = new System.Drawing.Size(0, 12);
//            this.labStartTime.TabIndex = 10;
//            this.labStartTime.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnExport_LinkClicked);
//            // 
//            // treeView1
//            // 
//            this.treeView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
//            | System.Windows.Forms.AnchorStyles.Left) 
//            | System.Windows.Forms.AnchorStyles.Right)));
//            this.treeView1.BackColor = System.Drawing.Color.RoyalBlue;
//            this.treeView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
//            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
//            this.treeView1.Location = new System.Drawing.Point(12, 67);
//            this.treeView1.Name = "treeView1";
//            this.treeView1.Size = new System.Drawing.Size(223, 505);
//            this.treeView1.TabIndex = 11;
//            // 
//            // 修改名称ToolStripMenuItem
//            // 
//            this.修改名称ToolStripMenuItem.Name = "修改名称ToolStripMenuItem";
//            this.修改名称ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
//            this.修改名称ToolStripMenuItem.Text = "修改名称";
//            this.修改名称ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemEditName_Click);
//            // 
//            // frmSysEventMainFormTree
//            // 
//            this.BackColor = System.Drawing.Color.RoyalBlue;
//            this.ClientSize = new System.Drawing.Size(247, 584);
//            this.Controls.Add(this.treeView1);
//            this.Controls.Add(this.btnMKSys);
//            this.Controls.Add(this.labStartTime);
//            this.Controls.Add(this.btnAddAction);
//            this.Controls.Add(this.btnExport);
//            this.Controls.Add(this.linkLabel2);
//            this.Controls.Add(this.label2);
//            this.Controls.Add(this.button3);
//            this.Controls.Add(this.btnBegin);
//            this.Controls.Add(this.btnPause);
//            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
//            this.MaximizeBox = false;
//            this.MinimizeBox = false;
//            this.MinimumSize = new System.Drawing.Size(263, 229);
//            this.Name = "frmSysEventMainFormTree";
//            this.Text = "小熊鼠标键盘辅助工具V1.0.1.1";
//            this.Load += new System.EventHandler(this.frmSysEventMainForm_Load);
//            this.contextMenuStrip1.ResumeLayout(false);
//            this.contextMenuStrip2.ResumeLayout(false);
//            this.ResumeLayout(false);
//            this.PerformLayout();

//        }
//        private Button button3;

//        private Label label2;
//        private LinkLabel btnExport;
//        private LinkLabel btnMKSys;
//        private LinkLabel btnAddAction;
//        private ContextMenuStrip contextMenuStrip1;
//        private ToolStripMenuItem 插入动作ToolStripMenuItem;
//        private ToolStripMenuItem 删除动作ToolStripMenuItem;
//        private NotifyIcon notifyIcon1;
//        private ContextMenuStrip contextMenuStrip2;
//        private ToolStripMenuItem 退出EToolStripMenuItem;
//        private ToolStripMenuItem 显示SToolStripMenuItem;
//        private ToolStripMenuItem 隐藏HToolStripMenuItem;
//        private Button btnBegin;
//        private LinkLabel linkLabel2;
//        private Button btnPause;
//        private LinkLabel labStartTime; 
//        #endregion
//        private TreeView treeView1;
//        private ToolStripMenuItem 定时执行ToolStripMenuItem;
//        private ToolStripMenuItem 执行次数ToolStripMenuItem;
//        private ToolStripMenuItem 导出单个脚本ToolStripMenuItem;
//        private ToolStripMenuItem 修改名称ToolStripMenuItem;
//    }
//}