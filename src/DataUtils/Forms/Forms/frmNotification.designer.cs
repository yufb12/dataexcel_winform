
namespace Feng.Forms
{
    partial class frmNotification
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNotification));
            this.txtTitle = new System.Windows.Forms.Label();
            this.txtMsg = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.btnClose = new System.Windows.Forms.PictureBox();
            this.btnCopy = new System.Windows.Forms.PictureBox();
            this.picMsgType = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCopy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMsgType)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.BackColor = System.Drawing.Color.Azure;
            this.txtTitle.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTitle.Location = new System.Drawing.Point(44, 11);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(270, 29);
            this.txtTitle.TabIndex = 1;
            this.txtTitle.Text = "label1";
            this.txtTitle.Click += new System.EventHandler(this.txtMsg_Click);
            this.txtTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtTitle_MouseDown);
            // 
            // txtMsg
            // 
            this.txtMsg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMsg.BackColor = System.Drawing.Color.Azure;
            this.txtMsg.Location = new System.Drawing.Point(34, 54);
            this.txtMsg.Name = "txtMsg";
            this.txtMsg.Size = new System.Drawing.Size(344, 97);
            this.txtMsg.TabIndex = 2;
            this.txtMsg.Text = "label2";
            this.txtMsg.Click += new System.EventHandler(this.txtMsg_Click);
            this.txtMsg.MouseClick += new System.Windows.Forms.MouseEventHandler(this.txtMsg_MouseClick);
            this.txtMsg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtMsg_MouseDown);
            this.txtMsg.MouseHover += new System.EventHandler(this.txtMsg_MouseHover);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.BackColor = System.Drawing.Color.Azure;
            this.btnClose.Image = global::Feng.Utils.Properties.Resources.Notification_close;
            this.btnClose.Location = new System.Drawing.Point(359, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(25, 25);
            this.btnClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnClose.TabIndex = 0;
            this.btnClose.TabStop = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            this.btnClose.MouseEnter += new System.EventHandler(this.btnClose_MouseEnter);
            this.btnClose.MouseLeave += new System.EventHandler(this.btnClose_MouseLeave);
            // 
            // btnCopy
            // 
            this.btnCopy.Image = global::Feng.Utils.Properties.Resources.notification_copy;
            this.btnCopy.Location = new System.Drawing.Point(368, 135);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(16, 16);
            this.btnCopy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnCopy.TabIndex = 0;
            this.btnCopy.TabStop = false;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // picMsgType
            // 
            this.picMsgType.Image = ((System.Drawing.Image)(resources.GetObject("picMsgType.Image")));
            this.picMsgType.Location = new System.Drawing.Point(8, 9);
            this.picMsgType.Name = "picMsgType";
            this.picMsgType.Size = new System.Drawing.Size(26, 26);
            this.picMsgType.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMsgType.TabIndex = 0;
            this.picMsgType.TabStop = false;
            this.picMsgType.Click += new System.EventHandler(this.picMsgType_Click);
            this.picMsgType.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtTitle_MouseDown);
            // 
            // frmNotification
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Azure;
            this.ClientSize = new System.Drawing.Size(390, 160);
            this.Controls.Add(this.btnCopy);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.picMsgType);
            this.Controls.Add(this.txtMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmNotification";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmNotification_Load);
            this.Shown += new System.EventHandler(this.frmNotification_Shown);
            this.Click += new System.EventHandler(this.txtMsg_Click);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.frmNotification_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmNotification_MouseMove);
            ((System.ComponentModel.ISupportInitialize)(this.btnClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCopy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picMsgType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox btnClose;
        public System.Windows.Forms.PictureBox picMsgType;
        public System.Windows.Forms.Label txtTitle;
        public System.Windows.Forms.Label txtMsg;
        public System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Timer timer2;
        public System.Windows.Forms.PictureBox btnCopy;
    }
}

