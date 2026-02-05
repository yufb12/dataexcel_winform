using System.Windows.Forms;
namespace Feng.Assistant
{
    partial class frmSysEventSet
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
            this.button2 = new System.Windows.Forms.Button();
            this.btnCtl_A = new System.Windows.Forms.Button();
            this.btnCtl_C = new System.Windows.Forms.Button();
            this.btnAlt_C = new System.Windows.Forms.Button();
            this.btnEnter = new System.Windows.Forms.Button();
            this.btnTab = new System.Windows.Forms.Button();
            this.btnDown = new System.Windows.Forms.Button();
            this.btnText = new System.Windows.Forms.Button();
            this.btnAlt_S = new System.Windows.Forms.Button();
            this.btnCtl_V = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button18 = new System.Windows.Forms.Button();
            this.btnSpit = new System.Windows.Forms.Button();
            this.button23 = new System.Windows.Forms.Button();
            this.btnVersion = new System.Windows.Forms.Button();
            this.btnCurrentTime = new System.Windows.Forms.Button();
            this.button30 = new System.Windows.Forms.Button();
            this.button33 = new System.Windows.Forms.Button();
            this.button32 = new System.Windows.Forms.Button();
            this.button31 = new System.Windows.Forms.Button();
            this.button22 = new System.Windows.Forms.Button();
            this.btnRandom = new System.Windows.Forms.Button();
            this.button25 = new System.Windows.Forms.Button();
            this.button35 = new System.Windows.Forms.Button();
            this.button34 = new System.Windows.Forms.Button();
            this.buttonHide = new System.Windows.Forms.Button();
            this.button20 = new System.Windows.Forms.Button();
            this.button17 = new System.Windows.Forms.Button();
            this.button10 = new System.Windows.Forms.Button();
            this.button29 = new System.Windows.Forms.Button();
            this.button26 = new System.Windows.Forms.Button();
            this.button28 = new System.Windows.Forms.Button();
            this.button11 = new System.Windows.Forms.Button();
            this.button27 = new System.Windows.Forms.Button();
            this.button12 = new System.Windows.Forms.Button();
            this.button13 = new System.Windows.Forms.Button();
            this.button16 = new System.Windows.Forms.Button();
            this.button15 = new System.Windows.Forms.Button();
            this.button14 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button19 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button21 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button24 = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.White;
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button2.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(10, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(86, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "确定(&S)";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button3_Click);
            // 
            // btnCtl_A
            // 
            this.btnCtl_A.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCtl_A.BackColor = System.Drawing.Color.White;
            this.btnCtl_A.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCtl_A.ForeColor = System.Drawing.Color.Black;
            this.btnCtl_A.Location = new System.Drawing.Point(10, 110);
            this.btnCtl_A.Name = "btnCtl_A";
            this.btnCtl_A.Size = new System.Drawing.Size(44, 23);
            this.btnCtl_A.TabIndex = 2;
            this.btnCtl_A.Text = "Ctl+A";
            this.toolTip1.SetToolTip(this.btnCtl_A, "发送全选");
            this.btnCtl_A.UseVisualStyleBackColor = false;
            this.btnCtl_A.Click += new System.EventHandler(this.btnCtl_A_Click);
            // 
            // btnCtl_C
            // 
            this.btnCtl_C.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCtl_C.BackColor = System.Drawing.Color.White;
            this.btnCtl_C.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCtl_C.ForeColor = System.Drawing.Color.Black;
            this.btnCtl_C.Location = new System.Drawing.Point(52, 110);
            this.btnCtl_C.Name = "btnCtl_C";
            this.btnCtl_C.Size = new System.Drawing.Size(44, 23);
            this.btnCtl_C.TabIndex = 2;
            this.btnCtl_C.Text = "Ctl+C";
            this.toolTip1.SetToolTip(this.btnCtl_C, "复制");
            this.btnCtl_C.UseVisualStyleBackColor = false;
            this.btnCtl_C.Click += new System.EventHandler(this.btnCtl_C_Click);
            // 
            // btnAlt_C
            // 
            this.btnAlt_C.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAlt_C.BackColor = System.Drawing.Color.White;
            this.btnAlt_C.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAlt_C.ForeColor = System.Drawing.Color.Black;
            this.btnAlt_C.Location = new System.Drawing.Point(52, 141);
            this.btnAlt_C.Name = "btnAlt_C";
            this.btnAlt_C.Size = new System.Drawing.Size(44, 23);
            this.btnAlt_C.TabIndex = 2;
            this.btnAlt_C.Text = "Alt+C";
            this.btnAlt_C.UseVisualStyleBackColor = false;
            this.btnAlt_C.Click += new System.EventHandler(this.btnAlt_C_Click);
            // 
            // btnEnter
            // 
            this.btnEnter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnEnter.BackColor = System.Drawing.Color.White;
            this.btnEnter.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnEnter.ForeColor = System.Drawing.Color.Black;
            this.btnEnter.Location = new System.Drawing.Point(52, 172);
            this.btnEnter.Name = "btnEnter";
            this.btnEnter.Size = new System.Drawing.Size(44, 23);
            this.btnEnter.TabIndex = 2;
            this.btnEnter.Text = "Enter";
            this.btnEnter.UseVisualStyleBackColor = false;
            this.btnEnter.Click += new System.EventHandler(this.btnEnter_Click);
            // 
            // btnTab
            // 
            this.btnTab.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnTab.BackColor = System.Drawing.Color.White;
            this.btnTab.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnTab.ForeColor = System.Drawing.Color.Black;
            this.btnTab.Location = new System.Drawing.Point(10, 203);
            this.btnTab.Name = "btnTab";
            this.btnTab.Size = new System.Drawing.Size(44, 23);
            this.btnTab.TabIndex = 2;
            this.btnTab.Text = "Tab";
            this.btnTab.UseVisualStyleBackColor = false;
            this.btnTab.Click += new System.EventHandler(this.btnTab_Click);
            // 
            // btnDown
            // 
            this.btnDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnDown.BackColor = System.Drawing.Color.White;
            this.btnDown.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDown.ForeColor = System.Drawing.Color.Black;
            this.btnDown.Location = new System.Drawing.Point(137, 264);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(36, 22);
            this.btnDown.TabIndex = 2;
            this.btnDown.Text = "↓";
            this.toolTip1.SetToolTip(this.btnDown, "向下按钮");
            this.btnDown.UseVisualStyleBackColor = false;
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // btnText
            // 
            this.btnText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnText.BackColor = System.Drawing.Color.White;
            this.btnText.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnText.ForeColor = System.Drawing.Color.Black;
            this.btnText.Location = new System.Drawing.Point(10, 79);
            this.btnText.Name = "btnText";
            this.btnText.Size = new System.Drawing.Size(86, 23);
            this.btnText.TabIndex = 2;
            this.btnText.Text = "自定义文本";
            this.toolTip1.SetToolTip(this.btnText, "发送文本");
            this.btnText.UseVisualStyleBackColor = false;
            this.btnText.Click += new System.EventHandler(this.btnText_Click);
            // 
            // btnAlt_S
            // 
            this.btnAlt_S.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAlt_S.BackColor = System.Drawing.Color.White;
            this.btnAlt_S.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAlt_S.ForeColor = System.Drawing.Color.Black;
            this.btnAlt_S.Location = new System.Drawing.Point(10, 172);
            this.btnAlt_S.Name = "btnAlt_S";
            this.btnAlt_S.Size = new System.Drawing.Size(44, 23);
            this.btnAlt_S.TabIndex = 2;
            this.btnAlt_S.Text = "Alt+S";
            this.btnAlt_S.UseVisualStyleBackColor = false;
            this.btnAlt_S.Click += new System.EventHandler(this.btnAlt_S_Click);
            // 
            // btnCtl_V
            // 
            this.btnCtl_V.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCtl_V.BackColor = System.Drawing.Color.White;
            this.btnCtl_V.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCtl_V.ForeColor = System.Drawing.Color.Black;
            this.btnCtl_V.Location = new System.Drawing.Point(10, 141);
            this.btnCtl_V.Name = "btnCtl_V";
            this.btnCtl_V.Size = new System.Drawing.Size(44, 23);
            this.btnCtl_V.TabIndex = 2;
            this.btnCtl_V.Text = "Ctl+V";
            this.toolTip1.SetToolTip(this.btnCtl_V, "粘贴");
            this.btnCtl_V.UseVisualStyleBackColor = false;
            this.btnCtl_V.Click += new System.EventHandler(this.btnCtl_V_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Red;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.button18);
            this.panel1.Controls.Add(this.btnSpit);
            this.panel1.Controls.Add(this.button23);
            this.panel1.Controls.Add(this.btnVersion);
            this.panel1.Controls.Add(this.btnCurrentTime);
            this.panel1.Controls.Add(this.button30);
            this.panel1.Controls.Add(this.button33);
            this.panel1.Controls.Add(this.button32);
            this.panel1.Controls.Add(this.button31);
            this.panel1.Controls.Add(this.button22);
            this.panel1.Controls.Add(this.btnRandom);
            this.panel1.Controls.Add(this.button25);
            this.panel1.Controls.Add(this.button35);
            this.panel1.Controls.Add(this.button34);
            this.panel1.Controls.Add(this.buttonHide);
            this.panel1.Controls.Add(this.button20);
            this.panel1.Controls.Add(this.button17);
            this.panel1.Controls.Add(this.button10);
            this.panel1.Controls.Add(this.button29);
            this.panel1.Controls.Add(this.button26);
            this.panel1.Controls.Add(this.button28);
            this.panel1.Controls.Add(this.button11);
            this.panel1.Controls.Add(this.button27);
            this.panel1.Controls.Add(this.button12);
            this.panel1.Controls.Add(this.button13);
            this.panel1.Controls.Add(this.button16);
            this.panel1.Controls.Add(this.button15);
            this.panel1.Controls.Add(this.button14);
            this.panel1.Controls.Add(this.button9);
            this.panel1.Controls.Add(this.btnText);
            this.panel1.Controls.Add(this.button8);
            this.panel1.Controls.Add(this.btnDown);
            this.panel1.Controls.Add(this.btnCtl_A);
            this.panel1.Controls.Add(this.button7);
            this.panel1.Controls.Add(this.button19);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button6);
            this.panel1.Controls.Add(this.btnTab);
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.btnCtl_C);
            this.panel1.Controls.Add(this.button4);
            this.panel1.Controls.Add(this.button21);
            this.panel1.Controls.Add(this.btnEnter);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.btnCtl_V);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btnAlt_S);
            this.panel1.Controls.Add(this.btnAlt_C);
            this.panel1.Controls.Add(this.button24);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(185, 525);
            this.panel1.TabIndex = 3;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 423);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(0, 15);
            this.label3.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.LightSlateGray;
            this.label2.Location = new System.Drawing.Point(3, 503);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(202, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "此程序仅为你提供模拟功能。";
            // 
            // button18
            // 
            this.button18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button18.BackColor = System.Drawing.Color.White;
            this.button18.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button18.ForeColor = System.Drawing.Color.Black;
            this.button18.Location = new System.Drawing.Point(10, 263);
            this.button18.Name = "button18";
            this.button18.Size = new System.Drawing.Size(86, 23);
            this.button18.TabIndex = 2;
            this.button18.Tag = "F1";
            this.button18.Text = "向下滚动";
            this.button18.UseVisualStyleBackColor = false;
            this.button18.Click += new System.EventHandler(this.button18_Click);
            // 
            // btnSpit
            // 
            this.btnSpit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSpit.BackColor = System.Drawing.Color.White;
            this.btnSpit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnSpit.ForeColor = System.Drawing.Color.Black;
            this.btnSpit.Location = new System.Drawing.Point(10, 351);
            this.btnSpit.Name = "btnSpit";
            this.btnSpit.Size = new System.Drawing.Size(85, 23);
            this.btnSpit.TabIndex = 2;
            this.btnSpit.Tag = "";
            this.btnSpit.Text = "执行间隔";
            this.btnSpit.UseVisualStyleBackColor = false;
            this.btnSpit.Click += new System.EventHandler(this.btnSpit_Click);
            // 
            // button23
            // 
            this.button23.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button23.BackColor = System.Drawing.Color.White;
            this.button23.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button23.Font = new System.Drawing.Font("宋体", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button23.ForeColor = System.Drawing.Color.Black;
            this.button23.Location = new System.Drawing.Point(102, 381);
            this.button23.Name = "button23";
            this.button23.Size = new System.Drawing.Size(71, 23);
            this.button23.TabIndex = 2;
            this.button23.Tag = "";
            this.button23.Text = "关闭程序";
            this.button23.UseVisualStyleBackColor = false;
            this.button23.Click += new System.EventHandler(this.btnVersion_Click);
            // 
            // btnVersion
            // 
            this.btnVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnVersion.BackColor = System.Drawing.Color.White;
            this.btnVersion.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnVersion.ForeColor = System.Drawing.Color.Black;
            this.btnVersion.Location = new System.Drawing.Point(52, 292);
            this.btnVersion.Name = "btnVersion";
            this.btnVersion.Size = new System.Drawing.Size(44, 23);
            this.btnVersion.TabIndex = 2;
            this.btnVersion.Tag = "";
            this.btnVersion.Text = "版本";
            this.btnVersion.UseVisualStyleBackColor = false;
            this.btnVersion.Click += new System.EventHandler(this.btnVersion_Click);
            // 
            // btnCurrentTime
            // 
            this.btnCurrentTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCurrentTime.BackColor = System.Drawing.Color.White;
            this.btnCurrentTime.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCurrentTime.ForeColor = System.Drawing.Color.Black;
            this.btnCurrentTime.Location = new System.Drawing.Point(10, 322);
            this.btnCurrentTime.Name = "btnCurrentTime";
            this.btnCurrentTime.Size = new System.Drawing.Size(85, 23);
            this.btnCurrentTime.TabIndex = 2;
            this.btnCurrentTime.Tag = "";
            this.btnCurrentTime.Text = "当前时间";
            this.btnCurrentTime.UseVisualStyleBackColor = false;
            this.btnCurrentTime.Click += new System.EventHandler(this.btnCurrentTime_Click);
            // 
            // button30
            // 
            this.button30.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button30.BackColor = System.Drawing.Color.White;
            this.button30.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button30.ForeColor = System.Drawing.Color.Black;
            this.button30.Location = new System.Drawing.Point(10, 468);
            this.button30.Name = "button30";
            this.button30.Size = new System.Drawing.Size(86, 23);
            this.button30.TabIndex = 2;
            this.button30.Tag = "";
            this.button30.Text = "关机";
            this.button30.UseVisualStyleBackColor = false;
            this.button30.Click += new System.EventHandler(this.button30_Click);
            // 
            // button33
            // 
            this.button33.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button33.BackColor = System.Drawing.Color.White;
            this.button33.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button33.ForeColor = System.Drawing.Color.Black;
            this.button33.Location = new System.Drawing.Point(102, 439);
            this.button33.Name = "button33";
            this.button33.Size = new System.Drawing.Size(71, 23);
            this.button33.TabIndex = 2;
            this.button33.Tag = "%( )n";
            this.button33.Text = "点击图片";
            this.button33.UseVisualStyleBackColor = false;
            this.button33.Click += new System.EventHandler(this.button33_Click);
            // 
            // button32
            // 
            this.button32.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button32.BackColor = System.Drawing.Color.White;
            this.button32.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button32.ForeColor = System.Drawing.Color.Black;
            this.button32.Location = new System.Drawing.Point(102, 468);
            this.button32.Name = "button32";
            this.button32.Size = new System.Drawing.Size(71, 23);
            this.button32.TabIndex = 2;
            this.button32.Tag = "%( )n";
            this.button32.Text = "最小化";
            this.button32.UseVisualStyleBackColor = false;
            this.button32.Click += new System.EventHandler(this.button11_Click);
            // 
            // button31
            // 
            this.button31.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button31.BackColor = System.Drawing.Color.White;
            this.button31.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button31.ForeColor = System.Drawing.Color.Black;
            this.button31.Location = new System.Drawing.Point(10, 439);
            this.button31.Name = "button31";
            this.button31.Size = new System.Drawing.Size(86, 23);
            this.button31.TabIndex = 2;
            this.button31.Tag = "%( )x";
            this.button31.Text = "最大化";
            this.button31.UseVisualStyleBackColor = false;
            this.button31.Click += new System.EventHandler(this.button11_Click);
            // 
            // button22
            // 
            this.button22.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button22.BackColor = System.Drawing.Color.White;
            this.button22.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button22.ForeColor = System.Drawing.Color.Black;
            this.button22.Location = new System.Drawing.Point(10, 410);
            this.button22.Name = "button22";
            this.button22.Size = new System.Drawing.Size(86, 23);
            this.button22.TabIndex = 2;
            this.button22.Tag = "%( )c";
            this.button22.Text = "关闭窗口";
            this.button22.UseVisualStyleBackColor = false;
            this.button22.Click += new System.EventHandler(this.button11_Click);
            // 
            // btnRandom
            // 
            this.btnRandom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRandom.BackColor = System.Drawing.Color.White;
            this.btnRandom.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRandom.ForeColor = System.Drawing.Color.Black;
            this.btnRandom.Location = new System.Drawing.Point(10, 292);
            this.btnRandom.Name = "btnRandom";
            this.btnRandom.Size = new System.Drawing.Size(44, 23);
            this.btnRandom.TabIndex = 2;
            this.btnRandom.Tag = "";
            this.btnRandom.Text = "随机";
            this.toolTip1.SetToolTip(this.btnRandom, "发送随机数");
            this.btnRandom.UseVisualStyleBackColor = false;
            this.btnRandom.Click += new System.EventHandler(this.btnRandom_Click);
            // 
            // button25
            // 
            this.button25.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button25.BackColor = System.Drawing.Color.White;
            this.button25.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button25.ForeColor = System.Drawing.Color.Black;
            this.button25.Location = new System.Drawing.Point(64, 381);
            this.button25.Name = "button25";
            this.button25.Size = new System.Drawing.Size(32, 23);
            this.button25.TabIndex = 2;
            this.button25.Tag = "";
            this.button25.Text = "5S";
            this.button25.UseVisualStyleBackColor = false;
            this.button25.Click += new System.EventHandler(this.button25_Click);
            // 
            // button35
            // 
            this.button35.BackColor = System.Drawing.Color.White;
            this.button35.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button35.ForeColor = System.Drawing.Color.Black;
            this.button35.Location = new System.Drawing.Point(124, 36);
            this.button35.Name = "button35";
            this.button35.Size = new System.Drawing.Size(50, 23);
            this.button35.TabIndex = 2;
            this.button35.Tag = "";
            this.button35.Text = "结束";
            this.button35.UseVisualStyleBackColor = false;
            this.button35.Click += new System.EventHandler(this.button35_Click);
            // 
            // button34
            // 
            this.button34.BackColor = System.Drawing.Color.White;
            this.button34.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button34.ForeColor = System.Drawing.Color.Black;
            this.button34.Location = new System.Drawing.Point(63, 36);
            this.button34.Name = "button34";
            this.button34.Size = new System.Drawing.Size(59, 23);
            this.button34.TabIndex = 2;
            this.button34.Tag = "";
            this.button34.Text = "开始";
            this.button34.UseVisualStyleBackColor = false;
            this.button34.Click += new System.EventHandler(this.button34_Click);
            // 
            // buttonHide
            // 
            this.buttonHide.BackColor = System.Drawing.Color.White;
            this.buttonHide.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonHide.ForeColor = System.Drawing.Color.Black;
            this.buttonHide.Location = new System.Drawing.Point(10, 36);
            this.buttonHide.Name = "buttonHide";
            this.buttonHide.Size = new System.Drawing.Size(51, 23);
            this.buttonHide.TabIndex = 2;
            this.buttonHide.Tag = "";
            this.buttonHide.Text = "隐藏";
            this.buttonHide.UseVisualStyleBackColor = false;
            this.buttonHide.Click += new System.EventHandler(this.buttonHide_Click);
            // 
            // button20
            // 
            this.button20.BackColor = System.Drawing.Color.White;
            this.button20.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button20.ForeColor = System.Drawing.Color.Black;
            this.button20.Location = new System.Drawing.Point(10, 381);
            this.button20.Name = "button20";
            this.button20.Size = new System.Drawing.Size(29, 23);
            this.button20.TabIndex = 2;
            this.button20.Tag = "";
            this.button20.Text = "1S";
            this.toolTip1.SetToolTip(this.button20, "点击一次增加间隔1秒");
            this.button20.UseVisualStyleBackColor = false;
            this.button20.Click += new System.EventHandler(this.button20_Click);
            // 
            // button17
            // 
            this.button17.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button17.BackColor = System.Drawing.Color.White;
            this.button17.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button17.ForeColor = System.Drawing.Color.Black;
            this.button17.Location = new System.Drawing.Point(10, 234);
            this.button17.Name = "button17";
            this.button17.Size = new System.Drawing.Size(86, 23);
            this.button17.TabIndex = 2;
            this.button17.Tag = "F1";
            this.button17.Text = "向上滚动";
            this.button17.UseVisualStyleBackColor = false;
            this.button17.Click += new System.EventHandler(this.button17_Click);
            // 
            // button10
            // 
            this.button10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button10.BackColor = System.Drawing.Color.White;
            this.button10.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button10.ForeColor = System.Drawing.Color.Black;
            this.button10.Location = new System.Drawing.Point(102, 79);
            this.button10.Name = "button10";
            this.button10.Size = new System.Drawing.Size(36, 22);
            this.button10.TabIndex = 2;
            this.button10.Tag = "{F1}";
            this.button10.Text = "F1";
            this.toolTip1.SetToolTip(this.button10, "发送按键F1");
            this.button10.UseVisualStyleBackColor = false;
            this.button10.Click += new System.EventHandler(this.button11_Click);
            // 
            // button29
            // 
            this.button29.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button29.BackColor = System.Drawing.Color.White;
            this.button29.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button29.ForeColor = System.Drawing.Color.Black;
            this.button29.Location = new System.Drawing.Point(102, 352);
            this.button29.Name = "button29";
            this.button29.Size = new System.Drawing.Size(36, 22);
            this.button29.TabIndex = 2;
            this.button29.Tag = "{HOME}";
            this.button29.Text = "△";
            this.toolTip1.SetToolTip(this.button29, "Home");
            this.button29.UseVisualStyleBackColor = false;
            this.button29.Click += new System.EventHandler(this.button11_Click);
            // 
            // button26
            // 
            this.button26.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button26.BackColor = System.Drawing.Color.White;
            this.button26.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button26.ForeColor = System.Drawing.Color.Black;
            this.button26.Location = new System.Drawing.Point(102, 322);
            this.button26.Name = "button26";
            this.button26.Size = new System.Drawing.Size(36, 22);
            this.button26.TabIndex = 2;
            this.button26.Tag = "{PGUP}";
            this.button26.Text = "∧";
            this.toolTip1.SetToolTip(this.button26, "PGUP");
            this.button26.UseVisualStyleBackColor = false;
            this.button26.Click += new System.EventHandler(this.button11_Click);
            // 
            // button28
            // 
            this.button28.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button28.BackColor = System.Drawing.Color.White;
            this.button28.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button28.ForeColor = System.Drawing.Color.Black;
            this.button28.Location = new System.Drawing.Point(137, 352);
            this.button28.Name = "button28";
            this.button28.Size = new System.Drawing.Size(36, 22);
            this.button28.TabIndex = 2;
            this.button28.Tag = "{END}";
            this.button28.Text = "▽";
            this.toolTip1.SetToolTip(this.button28, "End");
            this.button28.UseVisualStyleBackColor = false;
            this.button28.Click += new System.EventHandler(this.button11_Click);
            // 
            // button11
            // 
            this.button11.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button11.BackColor = System.Drawing.Color.White;
            this.button11.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button11.ForeColor = System.Drawing.Color.Black;
            this.button11.Location = new System.Drawing.Point(102, 292);
            this.button11.Name = "button11";
            this.button11.Size = new System.Drawing.Size(36, 22);
            this.button11.TabIndex = 2;
            this.button11.Tag = "{LEFT}";
            this.button11.Text = "←";
            this.toolTip1.SetToolTip(this.button11, "向左按钮");
            this.button11.UseVisualStyleBackColor = false;
            this.button11.Click += new System.EventHandler(this.button11_Click);
            // 
            // button27
            // 
            this.button27.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button27.BackColor = System.Drawing.Color.White;
            this.button27.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button27.ForeColor = System.Drawing.Color.Black;
            this.button27.Location = new System.Drawing.Point(137, 322);
            this.button27.Name = "button27";
            this.button27.Size = new System.Drawing.Size(36, 22);
            this.button27.TabIndex = 2;
            this.button27.Tag = "{PGDN}";
            this.button27.Text = "∨";
            this.toolTip1.SetToolTip(this.button27, "Page Down");
            this.button27.UseVisualStyleBackColor = false;
            this.button27.Click += new System.EventHandler(this.button11_Click);
            // 
            // button12
            // 
            this.button12.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button12.BackColor = System.Drawing.Color.White;
            this.button12.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button12.ForeColor = System.Drawing.Color.Black;
            this.button12.Location = new System.Drawing.Point(137, 292);
            this.button12.Name = "button12";
            this.button12.Size = new System.Drawing.Size(36, 22);
            this.button12.TabIndex = 2;
            this.button12.Tag = "{RIGHT}";
            this.button12.Text = "→";
            this.toolTip1.SetToolTip(this.button12, "向右按钮");
            this.button12.UseVisualStyleBackColor = false;
            this.button12.Click += new System.EventHandler(this.button11_Click);
            // 
            // button13
            // 
            this.button13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button13.BackColor = System.Drawing.Color.White;
            this.button13.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button13.ForeColor = System.Drawing.Color.Black;
            this.button13.Location = new System.Drawing.Point(102, 264);
            this.button13.Name = "button13";
            this.button13.Size = new System.Drawing.Size(36, 22);
            this.button13.TabIndex = 2;
            this.button13.Tag = "{UP}";
            this.button13.Text = "↑";
            this.toolTip1.SetToolTip(this.button13, "向上按钮");
            this.button13.UseVisualStyleBackColor = false;
            this.button13.Click += new System.EventHandler(this.button11_Click);
            // 
            // button16
            // 
            this.button16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button16.BackColor = System.Drawing.Color.White;
            this.button16.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button16.ForeColor = System.Drawing.Color.Black;
            this.button16.Location = new System.Drawing.Point(137, 234);
            this.button16.Name = "button16";
            this.button16.Size = new System.Drawing.Size(36, 22);
            this.button16.TabIndex = 2;
            this.button16.Tag = "{F12}";
            this.button16.Text = "F12";
            this.button16.UseVisualStyleBackColor = false;
            this.button16.Click += new System.EventHandler(this.button11_Click);
            // 
            // button15
            // 
            this.button15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button15.BackColor = System.Drawing.Color.White;
            this.button15.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button15.ForeColor = System.Drawing.Color.Black;
            this.button15.Location = new System.Drawing.Point(102, 234);
            this.button15.Name = "button15";
            this.button15.Size = new System.Drawing.Size(36, 22);
            this.button15.TabIndex = 2;
            this.button15.Tag = "{F11}";
            this.button15.Text = "F11";
            this.button15.UseVisualStyleBackColor = false;
            this.button15.Click += new System.EventHandler(this.button11_Click);
            // 
            // button14
            // 
            this.button14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button14.BackColor = System.Drawing.Color.White;
            this.button14.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button14.ForeColor = System.Drawing.Color.Black;
            this.button14.Location = new System.Drawing.Point(137, 203);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(36, 22);
            this.button14.TabIndex = 2;
            this.button14.Tag = "{F10}";
            this.button14.Text = "F10";
            this.button14.UseVisualStyleBackColor = false;
            this.button14.Click += new System.EventHandler(this.button11_Click);
            // 
            // button9
            // 
            this.button9.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button9.BackColor = System.Drawing.Color.White;
            this.button9.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button9.ForeColor = System.Drawing.Color.Black;
            this.button9.Location = new System.Drawing.Point(102, 203);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(36, 22);
            this.button9.TabIndex = 2;
            this.button9.Tag = "{F9}";
            this.button9.Text = "F9";
            this.button9.UseVisualStyleBackColor = false;
            this.button9.Click += new System.EventHandler(this.button11_Click);
            // 
            // button8
            // 
            this.button8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button8.BackColor = System.Drawing.Color.White;
            this.button8.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button8.ForeColor = System.Drawing.Color.Black;
            this.button8.Location = new System.Drawing.Point(137, 79);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(36, 22);
            this.button8.TabIndex = 2;
            this.button8.Tag = "{F2}";
            this.button8.Text = "F2";
            this.button8.UseVisualStyleBackColor = false;
            this.button8.Click += new System.EventHandler(this.button11_Click);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button7.BackColor = System.Drawing.Color.White;
            this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button7.ForeColor = System.Drawing.Color.Black;
            this.button7.Location = new System.Drawing.Point(137, 172);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(36, 22);
            this.button7.TabIndex = 2;
            this.button7.Tag = "{F8}";
            this.button7.Text = "F8";
            this.button7.UseVisualStyleBackColor = false;
            this.button7.Click += new System.EventHandler(this.button11_Click);
            // 
            // button19
            // 
            this.button19.BackColor = System.Drawing.Color.White;
            this.button19.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button19.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button19.ForeColor = System.Drawing.Color.Black;
            this.button19.Location = new System.Drawing.Point(101, 10);
            this.button19.Name = "button19";
            this.button19.Size = new System.Drawing.Size(75, 23);
            this.button19.TabIndex = 0;
            this.button19.Text = "执行";
            this.button19.UseVisualStyleBackColor = false;
            this.button19.Click += new System.EventHandler(this.button19_Click);
            // 
            // button6
            // 
            this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button6.BackColor = System.Drawing.Color.White;
            this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button6.ForeColor = System.Drawing.Color.Black;
            this.button6.Location = new System.Drawing.Point(102, 110);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(36, 22);
            this.button6.TabIndex = 2;
            this.button6.Tag = "{F3}";
            this.button6.Text = "F3";
            this.button6.UseVisualStyleBackColor = false;
            this.button6.Click += new System.EventHandler(this.button11_Click);
            // 
            // button5
            // 
            this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button5.BackColor = System.Drawing.Color.White;
            this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button5.ForeColor = System.Drawing.Color.Black;
            this.button5.Location = new System.Drawing.Point(102, 172);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(36, 22);
            this.button5.TabIndex = 2;
            this.button5.Tag = "{F7}";
            this.button5.Text = "F7";
            this.button5.UseVisualStyleBackColor = false;
            this.button5.Click += new System.EventHandler(this.button11_Click);
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button4.BackColor = System.Drawing.Color.White;
            this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button4.ForeColor = System.Drawing.Color.Black;
            this.button4.Location = new System.Drawing.Point(137, 110);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(36, 22);
            this.button4.TabIndex = 2;
            this.button4.Tag = "{F4}";
            this.button4.Text = "F4";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button11_Click);
            // 
            // button21
            // 
            this.button21.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button21.BackColor = System.Drawing.Color.White;
            this.button21.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button21.ForeColor = System.Drawing.Color.Black;
            this.button21.Location = new System.Drawing.Point(52, 203);
            this.button21.Name = "button21";
            this.button21.Size = new System.Drawing.Size(44, 23);
            this.button21.TabIndex = 2;
            this.button21.Tag = "{ESC}";
            this.button21.Text = "Esc";
            this.button21.UseVisualStyleBackColor = false;
            this.button21.Click += new System.EventHandler(this.button11_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button3.BackColor = System.Drawing.Color.White;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(137, 141);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(36, 22);
            this.button3.TabIndex = 2;
            this.button3.Tag = "{F6}";
            this.button3.Text = "F6";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button11_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(102, 141);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(36, 22);
            this.button1.TabIndex = 2;
            this.button1.Tag = "{F5}";
            this.button1.Text = "F5";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button11_Click);
            // 
            // button24
            // 
            this.button24.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.button24.BackColor = System.Drawing.Color.White;
            this.button24.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.button24.ForeColor = System.Drawing.Color.Black;
            this.button24.Location = new System.Drawing.Point(38, 381);
            this.button24.Name = "button24";
            this.button24.Size = new System.Drawing.Size(29, 23);
            this.button24.TabIndex = 2;
            this.button24.Tag = "";
            this.button24.Text = "3S";
            this.button24.UseVisualStyleBackColor = false;
            this.button24.Click += new System.EventHandler(this.button24_Click);
            // 
            // frmSysEventSet
            // 
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(185, 525);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimumSize = new System.Drawing.Size(182, 512);
            this.Name = "frmSysEventSet";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseClickGetPostionForm_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MouseClickGetPostionForm_MouseDoubleClick);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button button2;
        private Button btnCtl_A;
        private Button btnCtl_C;
        private Button btnAlt_C;
        private Button btnEnter;
        private Button btnTab;
        private Button btnDown;
        private Button btnText;
        private Button btnAlt_S;
        private Button btnCtl_V;
        private Button button10;
        private Button button9;
        private Button button8;
        private Button button7;
        private Button button6;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button1;
        private Button button11;
        private Button button12;
        private Button button13;
        private ToolTip toolTip1; 
        private Button button16;
        private Button button15;
        private Button button14;
        private Label label2; 
        private Label label3;
        private Button button17;
        private Button button18;
        private Button button19;
        private Button button20;
        private Button btnRandom;
        private Button btnVersion;
        private Button btnSpit;
        private Button btnCurrentTime;
        private Button button23;
        private Button button22;
        private Button button25;
        private Button button24;
        private Button button21;
        private Button button26;
        private Button button27;
        private Button button29;
        private Button button28;
        private Button button30;
        private Button button32;
        private Button button31;
        private Button button33;
        private Panel panel1;
        private Button buttonHide;
        private Button button34;
        private Button button35;
    }
}