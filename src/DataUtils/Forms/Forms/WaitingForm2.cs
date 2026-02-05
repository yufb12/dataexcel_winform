using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Feng.Utils; 
using System.Drawing.Drawing2D;
using Feng.Drawing;
using Feng.Forms.EventHandlers;

namespace Feng.Forms
{
    //[EditorBrowsable(false)]
    public class WaitingForm2 : Form  
    {
        public WaitingForm2()
        {
            InitializeComponent();
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen; 
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            GraphicsHelper.FillRectangleLinearGradient(e.Graphics, Color.FromArgb(230, 224, 224, 224), Color.FromArgb(224, 224, 224), 0, 0, this.Width, this.Height, LinearGradientMode.Vertical, false, 1, Color.Firebrick, 1);
            base.OnPaint(e);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= Utils.UnsafeNativeMethods.WS_EX_NOACTIVATE;
                return cp;
            }
        }

        private bool _AutoGetActivate = false;
        private Controls.WaintingBox waintingBox1;
        private Timer timer1;
        private IContainer components;
        private ProgressBar progressBar1;
        private LinkLabel btnCancel;
        private Label txtTitle;
         

        public bool AutoGetActivate
        {
            get { return _AutoGetActivate; }
            set { _AutoGetActivate = value; }
        }

        protected override bool ShowWithoutActivation
        {
            get
            {
                return !_AutoGetActivate;
            }
        }

        protected override void SetVisibleCore(bool newVisible)
        { 
            if (!this.DesignMode)
            {
                this.SetVisibleCore(this, newVisible); 
            }
            base.SetVisibleCore(newVisible);
        }
 
        bool SetVisibleCore(Control control, bool newVisible)
        {
            if (newVisible)
            {
                UnsafeNativeMethods.ShowWindow(control.Handle, 8);
            }
            return true;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.txtTitle = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.waintingBox1 = new Feng.Forms.Controls.WaintingBox();
            this.btnCancel = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // txtTitle
            // 
            this.txtTitle.AutoSize = true;
            this.txtTitle.BackColor = System.Drawing.Color.Transparent;
            this.txtTitle.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTitle.ForeColor = System.Drawing.Color.Gray;
            this.txtTitle.Location = new System.Drawing.Point(58, 23);
            this.txtTitle.Name = "txtTitle";
            this.txtTitle.Size = new System.Drawing.Size(119, 14);
            this.txtTitle.TabIndex = 1;
            this.txtTitle.Text = "正在加载，请稍候";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.BackColor = System.Drawing.Color.Blue;
            this.progressBar1.Location = new System.Drawing.Point(0, 66);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(543, 11);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 2;
            // 
            // waintingBox1
            // 
            this.waintingBox1.BackColor = System.Drawing.Color.Transparent;
            this.waintingBox1.BorderColor = System.Drawing.Color.DarkGray;
            this.waintingBox1.Color1 = System.Drawing.Color.White;
            this.waintingBox1.Color2 = System.Drawing.Color.Lavender;
            this.waintingBox1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.waintingBox1.Location = new System.Drawing.Point(3, 8);
            this.waintingBox1.Name = "waintingBox1";
            this.waintingBox1.Size = new System.Drawing.Size(39, 39);
            this.waintingBox1.TabIndex = 0;
            this.waintingBox1.Text = "waintingBox1";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.AutoSize = true;
            this.btnCancel.Location = new System.Drawing.Point(494, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(37, 15);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.TabStop = true;
            this.btnCancel.Text = "取消";
            this.btnCancel.Visible = false;
            this.btnCancel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnCancel_LinkClicked);
            // 
            // WaitingForm2
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(172)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(543, 68);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.txtTitle);
            this.Controls.Add(this.waintingBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "WaitingForm2";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.WaitingForm2_Load);
            this.DoubleClick += new System.EventHandler(this.WaitingForm2_DoubleClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void ColoseForm()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new ObjectNullEventEventHandler(ColoseForm), null);
            }
            else
            {
                this.Close();
            }
        }

        private static Feng.Forms.WaitingForm2 frm = null;
        public static WaitingObj WaitingObj { get; set; }
 
        private static bool Begining = false;
        private static int Instances = 0;
        public static void BeginWaiting(string title)
        {
            Instances = Instances + 1;
            if (Begining)
            {
                LastText = title;
                return;
            }
            CurrentThread = System.Threading.Thread.CurrentThread;
            WaitingObj = null;
            LastText = string.Empty;
            closform = false;
            System.Threading.Thread th = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(ShowFormTitle));
            th.IsBackground = true;
            th.Start(title);

        }
        public static string LastText = string.Empty;
        private static System.Threading.Thread CurrentThread = null;
        public static void UpdateText(string text)
        {
            LastText = text;
        }
        public static void UpdateWait()
        {
            if (WaitingObj == null)
            {
                WaitingObj = new WaitingObj();
            }
        }
 
        public static void UpdateWait(WaitingObj wait)
        {
            WaitingObj = wait;
        }
        private static void ShowForm()
        {
            frm = new Feng.Forms.WaitingForm2();
            frm.TopLevel = true;
            frm.TopMost = true;
            frm.ShowDialog();
        }
        private static bool lck = false;
        private static void ShowFormTitle(object title)
        {
            if (lck)
                return;
            try
            {
                lck = true;
                System.Threading.Thread.Sleep(500);
                if (closform)
                {
                    return;
                }
                string t = title.ToString();
                if (frm == null)
                {
                    try
                    {
                        frm = new Feng.Forms.WaitingForm2();
                        frm.TopLevel = true;
                        frm.TopMost = true;
                        frm.txtTitle.Text = t;
                        frm.ShowDialog();

                    }
                    catch (Exception ex)
                    {
                        Feng.Utils.BugReport.Log(ex);
                        Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);
                    }
                }
            }
            finally
            {
                lck = false;
            }
        }
        public static bool closform = false;
        public static void EndWaiting()
        {
            try
            { 
                Begining = false;
                closform = true;
                if (frm != null)
                {
                    Instances = Instances - 1;
                    if (Instances < 1)
                    {
                        frm.ColoseForm();
                        frm = null;
                        Instances = 0;
                    }
                }
            }
            catch (Exception)
            { 
            }

        }
        public static void Finish()
        {
            try
            {
                Begining = false;
                closform = true;
                if (frm != null)
                {
                    frm.ColoseForm();
                    frm = null;
                    Instances = 0;
                }
            }
            catch (Exception)
            {
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Visible = true;
                this.Show();
                this.TopLevel = true;
                this.TopMost = true;
                if (!string.IsNullOrEmpty(LastText))
                {
                    this.txtTitle.Text = LastText;
                }
                if (WaitingObj != null)
                {
                    this.btnCancel.Visible = true;
                }

                if (closform)
                {
                    this.timer1.Enabled = false;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);
            }
        }

        private void WaitingForm2_Load(object sender, EventArgs e)
        {

        }

        private void WaitingForm2_DoubleClick(object sender, EventArgs e)
        {
            closform = true;
        }

        private void btnCancel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                closform = true;
                if (WaitingObj != null)
                {
                    WaitingObj.Cancel = true;
                }
            }
            catch (Exception ex)
            { 
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "WaitingForm2", "btnCancel_LinkClicked", ex);
            }
        }
    }

    public class WaitingObj
    {
        public WaitingObj()
        {

        }
        public object Sender { get; set; }
        public bool Cancel { get; set; }
    }
}
