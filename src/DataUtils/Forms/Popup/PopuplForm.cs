using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using Feng.Utils;
using System;
using Feng.Forms;
namespace Feng.Forms.Popup
{
    public class PopupForm : PopupBaseForm
    {
        private static MouseHook hook = MouseHook.Instance;
        public PopupForm()
        {
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
                    base.BackColor = Color.White;
                    hook.MouseDown += new MouseHook.MouseDownHookHandler(this.hook_MouseDown);

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
            ClosePopup();
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
                hook.MouseDown -= new MouseHook.MouseDownHookHandler(this.hook_MouseDown);
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
                hook.MouseDown -= new MouseHook.MouseDownHookHandler(this.hook_MouseDown); 
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            base.OnClosed(e);
        }
        private bool firstshow = true;
        public virtual void FirstShow()
        {

        }
        public virtual void Popup(Point pt)
        {
            try
            {

                if (firstshow)
                {
                    firstshow = false;
                    FirstShow();
                }

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
            catch (Exception ex)
            { 
                Feng.Utils.BugReport.Log(ex);
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
                        hook.Start();
                    }
                    else
                    {
                        hook.Stop();
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
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
            try
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
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
            }

            base.WndProc(ref m);
        }


    }


}