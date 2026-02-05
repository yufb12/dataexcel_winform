using System.Windows.Forms;
namespace Feng.Assistant
{
    partial class frmSysEventMainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSysEventMainForm));
            this.button3 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.txtEventCount = new System.Windows.Forms.NumericUpDown();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeaderTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.插入动作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除动作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnExport = new System.Windows.Forms.LinkLabel();
            this.btnMKSys = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.btnAddAction = new System.Windows.Forms.LinkLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.隐藏HToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.显示SToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.退出EToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnBegin = new System.Windows.Forms.Button();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.btnPause = new System.Windows.Forms.Button();
            this.labStartTime = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.txtEventCount)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(13, 38);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(81, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "设置动作";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "动作执行次数：";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(102, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "常按住CTL键可以暂停";
            // 
            // txtEventCount
            // 
            this.txtEventCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEventCount.Location = new System.Drawing.Point(169, 61);
            this.txtEventCount.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.txtEventCount.Name = "txtEventCount";
            this.txtEventCount.Size = new System.Drawing.Size(105, 25);
            this.txtEventCount.TabIndex = 4;
            this.txtEventCount.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.txtEventCount.ValueChanged += new System.EventHandler(this.txtEventCount_ValueChanged);
            // 
            // listView1
            // 
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderTitle});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(12, 107);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(262, 405);
            this.listView1.TabIndex = 7;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderTitle
            // 
            this.columnHeaderTitle.Text = "内容";
            this.columnHeaderTitle.Width = 255;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.插入动作ToolStripMenuItem,
            this.删除动作ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(139, 52);
            // 
            // 插入动作ToolStripMenuItem
            // 
            this.插入动作ToolStripMenuItem.Name = "插入动作ToolStripMenuItem";
            this.插入动作ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.插入动作ToolStripMenuItem.Text = "插入动作";
            this.插入动作ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemInsertAction_Click);
            // 
            // 删除动作ToolStripMenuItem
            // 
            this.删除动作ToolStripMenuItem.Name = "删除动作ToolStripMenuItem";
            this.删除动作ToolStripMenuItem.Size = new System.Drawing.Size(138, 24);
            this.删除动作ToolStripMenuItem.Text = "删除动作";
            this.删除动作ToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemDelAction_Click);
            // 
            // btnExport
            // 
            this.btnExport.AutoSize = true;
            this.btnExport.Location = new System.Drawing.Point(11, 89);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(37, 15);
            this.btnExport.TabIndex = 10;
            this.btnExport.TabStop = true;
            this.btnExport.Text = "导入";
            this.btnExport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnExport_LinkClicked);
            // 
            // btnMKSys
            // 
            this.btnMKSys.AutoSize = true;
            this.btnMKSys.Location = new System.Drawing.Point(175, 89);
            this.btnMKSys.Name = "btnMKSys";
            this.btnMKSys.Size = new System.Drawing.Size(97, 15);
            this.btnMKSys.TabIndex = 10;
            this.btnMKSys.TabStop = true;
            this.btnMKSys.Text = "打开脚本中心";
            this.btnMKSys.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnMKSys_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(11, 517);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(67, 15);
            this.linkLabel1.TabIndex = 10;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "问题反馈";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnExport_LinkClicked);
            // 
            // btnAddAction
            // 
            this.btnAddAction.AutoSize = true;
            this.btnAddAction.Location = new System.Drawing.Point(55, 89);
            this.btnAddAction.Name = "btnAddAction";
            this.btnAddAction.Size = new System.Drawing.Size(67, 15);
            this.btnAddAction.TabIndex = 10;
            this.btnAddAction.TabStop = true;
            this.btnAddAction.Text = "添加动作";
            this.btnAddAction.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnAddAction_LinkClicked);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "模拟鼠标，键盘点击操作。";
            this.notifyIcon1.BalloonTipTitle = "小熊鼠标键盘辅助工具V1.0.1.1";
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip2;
            this.notifyIcon1.Text = "小熊鼠标键盘辅助工具V1.0.1.1";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.隐藏HToolStripMenuItem,
            this.显示SToolStripMenuItem,
            this.退出EToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(131, 76);
            // 
            // 隐藏HToolStripMenuItem
            // 
            this.隐藏HToolStripMenuItem.Name = "隐藏HToolStripMenuItem";
            this.隐藏HToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.隐藏HToolStripMenuItem.Text = "隐藏(&H)";
            this.隐藏HToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemHide_Click);
            // 
            // 显示SToolStripMenuItem
            // 
            this.显示SToolStripMenuItem.Name = "显示SToolStripMenuItem";
            this.显示SToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.显示SToolStripMenuItem.Text = "显示(&S)";
            this.显示SToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemShow_Click);
            // 
            // 退出EToolStripMenuItem
            // 
            this.退出EToolStripMenuItem.Name = "退出EToolStripMenuItem";
            this.退出EToolStripMenuItem.Size = new System.Drawing.Size(130, 24);
            this.退出EToolStripMenuItem.Text = "退出(&E)";
            this.退出EToolStripMenuItem.Click += new System.EventHandler(this.ToolStripMenuItemExit_Click);
            // 
            // btnBegin
            // 
            this.btnBegin.Location = new System.Drawing.Point(13, 9);
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(81, 23);
            this.btnBegin.TabIndex = 0;
            this.btnBegin.Text = "执行(&S)";
            this.btnBegin.UseVisualStyleBackColor = true;
            this.btnBegin.Click += new System.EventHandler(this.btnBegin_Click);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Location = new System.Drawing.Point(132, 89);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(37, 15);
            this.linkLabel2.TabIndex = 10;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "导出";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnImort_LinkClicked);
            // 
            // btnPause
            // 
            this.btnPause.Location = new System.Drawing.Point(108, 9);
            this.btnPause.Name = "btnPause";
            this.btnPause.Size = new System.Drawing.Size(71, 23);
            this.btnPause.TabIndex = 0;
            this.btnPause.Text = "暂停";
            this.btnPause.UseVisualStyleBackColor = true;
            this.btnPause.Click += new System.EventHandler(this.btnPause_Click);
            // 
            // labStartTime
            // 
            this.labStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labStartTime.AutoSize = true;
            this.labStartTime.Location = new System.Drawing.Point(89, 519);
            this.labStartTime.Name = "labStartTime";
            this.labStartTime.Size = new System.Drawing.Size(0, 15);
            this.labStartTime.TabIndex = 10;
            this.labStartTime.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnExport_LinkClicked);
            // 
            // frmSysEventMainForm
            // 
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(286, 534);
            this.Controls.Add(this.btnMKSys);
            this.Controls.Add(this.labStartTime);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnAddAction);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.linkLabel2);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtEventCount);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.btnBegin);
            this.Controls.Add(this.btnPause);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(263, 229);
            this.Name = "frmSysEventMainForm";
            this.Text = "鼠标键盘辅助工具V1.0.1.1";
            this.Load += new System.EventHandler(this.frmSysEventMainForm_Load);
            this.Click += new System.EventHandler(this.MouseClickTestForm_Click);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MouseClickTestForm_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.txtEventCount)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.contextMenuStrip2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        private Button button3;
        private Label label1;
        private System.Windows.Forms.Timer timer1;
        private Label label2;
        private NumericUpDown txtEventCount;
        private ListView listView1;
        private ColumnHeader columnHeaderTitle;
        private LinkLabel btnExport;
        private LinkLabel btnMKSys;
        private LinkLabel linkLabel1;
        private LinkLabel btnAddAction;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem 插入动作ToolStripMenuItem;
        private ToolStripMenuItem 删除动作ToolStripMenuItem;
        private NotifyIcon notifyIcon1;
        private ContextMenuStrip contextMenuStrip2;
        private ToolStripMenuItem 退出EToolStripMenuItem;
        private ToolStripMenuItem 显示SToolStripMenuItem;
        private ToolStripMenuItem 隐藏HToolStripMenuItem;
        private Button btnBegin;
        private LinkLabel linkLabel2;
        private Button btnPause;
        private LinkLabel labStartTime; 
        #endregion
    }
}