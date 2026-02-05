using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using Feng.Utils;
using System;
using Feng.Forms;
namespace Feng.Forms.Popup
{
    public class ToolTipForm : PopupBaseForm
    {
        private MouseHook hook = MouseHook.Instance;
        public ToolTipForm()
        {
            InitializeComponent();
            Init();
        } 

        public void Init()
        {

            try
            {

                if (!this.DesignMode)
                {
                    this.TopLevel = true;
                    //this.TopMost = true;
                    base.ShowInTaskbar = false;
                    base.FormBorderStyle = FormBorderStyle.None;
                    base.StartPosition = FormStartPosition.Manual;
                    this.hook.MouseDown += new MouseHook.MouseDownHookHandler(this.hook_MouseDown);
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        return;
                    } 
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }
 
        private bool hook_MouseDown(object sender, MouseEventArgs e)
         {
            Point pt = e.Location;
            if (!base.Bounds.Contains(pt))
            {
                if (OnBeforeMouseClickHide(e))
                { 
                    return true;
                }
                this.Cancel();
                return true;
            }
            return false;
        }

        public override void Cancel()
        {
            CancelEventArgs e = new CancelEventArgs();
            this.OnBeforeHide(e);
            if (e.Cancel)
            {
                return;
            }
            base.Cancel();
        }

        public virtual void SelectFirst()
        {

        }

        public virtual void MoveToFirst()
        {

        }

        public virtual void SetFocus()
        {

        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            try
            {
                this.hook.MouseDown -= new MouseHook.MouseDownHookHandler(this.hook_MouseDown);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            base.OnFormClosed(e);
        }
        protected override void OnClosed(EventArgs e)
        { 
            try
            { 
                this.hook.MouseDown -= new MouseHook.MouseDownHookHandler(this.hook_MouseDown); 
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            base.OnClosed(e);
        }

        private Label label1;
        private Timer timer1;
        private IContainer components;
        private ProgressBar progressBar1;
        private bool firstshow = true;
        public virtual void FirstShow()
        {

        }
        public virtual void Popup(Point pt)
        {
            if (firstshow)
            {
                firstshow = false;
                FirstShow();
            }
            Rectangle rect = Screen.PrimaryScreen.WorkingArea;
            this.Location = pt;
            if (!this.Visible)
            {
                if (this.ParentEditForm != null)
                {
                    this.TopLevel = true;
                    this.Show(this.ParentEditForm);
                }
                else
                {
                    this.TopLevel = true;
                
                    this.TopMost = true;
                    this.Show();
                }
                if (this.ParentEditForm != null)
                {
                    UnsafeNativeMethods.SendMessage(this.ParentEditForm.Handle, 0x86, 1, 0);
                }
            }
        }
        public virtual void ClosePopup()
        {
            this.Hide();
        }
        protected override void OnVisibleChanged(EventArgs e)
        {

            try
            {

                base.OnVisibleChanged(e);
                if (!this.DesignMode)
                {
#if DEBUG
                    if (System.Diagnostics.Debugger.IsAttached)
                    {
                        return;
                    }
#else 

#endif
                    if (this.Visible)
                    {
                        this.hook.Start();
                    }
                    else
                    {
                        this.hook.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

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
 
        protected override bool ShowWithoutActivation
        {
            get
            {
                return true;
            }
        }

        public delegate bool BeforeMouseClickHideEventHandler(object sender, MouseEventArgs e);
        public event BeforeMouseClickHideEventHandler BeforeMouseClickHide;
        public bool OnBeforeMouseClickHide(MouseEventArgs e)
        {
            if (BeforeMouseClickHide != null)
            {
                return BeforeMouseClickHide(this, e);
            }
            return false;
        }
        public event CancelEventHandler BeforeHide;
        public virtual void OnBeforeHide(CancelEventArgs e)
        {
            if (BeforeHide != null)
            {
                BeforeHide(this, e); 
            } 
        }
        public Form ParentEditForm { get; set; }
  
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case 0x86:
                    if (this.ParentEditForm != null)
                    {
                        if (!this.ParentEditForm.IsDisposed)
                        {
                            UnsafeNativeMethods.SendMessage(this.ParentEditForm.Handle, 0x86, 1, 0);
                        }
                    }
                    break;

                default:
                    break;
            }
            base.WndProc(ref m);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("微软雅黑", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(366, 104);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 101);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(366, 3);
            this.progressBar1.Step = 3;
            this.progressBar1.TabIndex = 1;
            // 
            // ToolTipForm
            // 
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(366, 104);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ToolTipForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }
        public static void Show(string caption, string text, int time)
        {
            ToolTipForm toolTipForm = new ToolTipForm();
            toolTipForm.label1.Text = text;
            toolTipForm.ShowTime = time;
            toolTipForm.StartPosition = FormStartPosition.CenterScreen;
            toolTipForm.TopMost = true;
            toolTipForm.Show();
        }
        public static void Show(string caption, string text, int time,int x,int y,float opacity)
        {
            ToolTipForm toolTipForm = new ToolTipForm();
            toolTipForm.label1.Text = text;
            toolTipForm.ShowTime = time;
            toolTipForm.StartPosition = FormStartPosition.CenterScreen;
            toolTipForm.TopMost = true;
            toolTipForm.Opacity = opacity;
            toolTipForm.Show();
        }
        public int ShowTime { get; set; }
        private int showtime = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                showtime++;
                if (showtime > ShowTime)
                {
                    this.timer1.Tick -= this.timer1_Tick;
                    this.timer1.Enabled = false;
                    this.Close();
                }
            }
            catch (Exception)
            { 
            }
        }
    }


}