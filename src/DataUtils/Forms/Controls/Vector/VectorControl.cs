using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Feng.Forms.Controls.Designer;
using Feng.Drawing;
using Feng.Print;
using Feng.Forms.Views.Vector;

namespace Feng.Forms.Controls.GridControl
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(VectorControl), "VectorControl.bmp")]
    public partial class VectorControl : Control
    {
        public VectorControl()
            : base()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ContainerControl, true);
            this.SetStyle(ControlStyles.StandardClick, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.UpdateStyles();
            this.BackColor = Color.White;
        }

        private Canvas _gridview = null;
        public Canvas GridView
        {
            get
            {
                try
                {

                    if (_gridview == null)
                    {
                        _gridview = new Canvas();
                    }
                }
                catch (Exception ex)
                {
                    Feng.Utils.ExceptionHelper.ShowError(ex);
                }
                return _gridview;
            }

        }
 
        private Feng.Drawing.GraphicsObject currentGraphicsObject = null;
        protected override void OnPaint(PaintEventArgs e)
        {

            try
            {

                if (currentGraphicsObject == null)
                {
                    currentGraphicsObject = new Feng.Drawing.GraphicsObject();
                }
                currentGraphicsObject.Graphics = e.Graphics;
                currentGraphicsObject.MousePoint = System.Windows.Forms.Control.MousePosition;
                currentGraphicsObject.ModifierKeys = System.Windows.Forms.Control.ModifierKeys;
                currentGraphicsObject.MouseButtons = System.Windows.Forms.Control.MouseButtons;
                currentGraphicsObject.Control = this;
                currentGraphicsObject.ClientPoint = this.PointToClient(currentGraphicsObject.MousePoint);
                Graphics g = e.Graphics; 

                if (currentGraphicsObject.Items != null)
                {
                    currentGraphicsObject.Items.Clear();
                }
                this.GridView.OnDraw(this, currentGraphicsObject);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            base.OnPaint(e);
        }
        private Cursor _begincursor = null;
        public virtual void BeginSetCursor(Cursor begincursor)
        {
            _begincursor = begincursor;
        }

        public virtual void EndSetCursor()
        {
            if (this.Cursor != _begincursor)
            {
                this.Cursor = _begincursor;
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        { 
            try
            {
                if (this.GridView != null)
                {
                    this.GridView.Width = this.Width;
                    this.GridView.Height = this.Height;
                    this.GridView.Refresh();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        
            base.OnSizeChanged(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.DesignMode)
            {
                return;
            }
            this.Focus();
            Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
            GridView.OnMouseDown(this, e, ve);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {

            try
            {

                if (this.DesignMode)
                {
                    return;
                }
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                GridView.OnMouseUp(this, e, ve);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                this.BeginSetCursor(System.Windows.Forms.Cursors.Default);
                if (this.DesignMode)
                {
                    return;
                }
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                GridView.OnMouseMove(this, e, ve);
                this.EndSetCursor();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        protected override void OnMouseLeave(EventArgs e)
        {

            try
            {

                if (this.DesignMode)
                {
                    return;
                }
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                GridView.OnMouseLeave(this, e, ve);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        protected override void OnMouseHover(EventArgs e)
        {
            try
            {
                if (this.DesignMode)
                {
                    return;
                }
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                GridView.OnMouseHover(this, e, ve);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {

            try
            {

                if (this.DesignMode)
                {
                    return;
                }
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                GridView.OnMouseEnter(this, e, ve);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {

            try
            {

                if (this.DesignMode)
                {
                    return;
                }
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                GridView.OnMouseDoubleClick(this, e, ve);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            try
            {

                if (this.DesignMode)
                {
                    return;
                }
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                GridView.OnMouseClick(this, e, ve);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        protected override void OnMouseCaptureChanged(EventArgs e)
        {

            try
            {

                if (this.DesignMode)
                {
                    return;
                }
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                GridView.OnMouseCaptureChanged(this, e, ve);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            try
            {

                if (this.DesignMode)
                {
                    return;
                }
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                GridView.OnMouseWheel(this, e, ve);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            try
            {

                if (this.DesignMode)
                {
                    return;
                }
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                GridView.OnClick(this, e, ve);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            try
            {
                if (this.DesignMode)
                {
                    return;
                }
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                GridView.OnKeyDown(this, e, ve);

            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            try
            {

                if (this.DesignMode)
                {
                    return;
                }
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                GridView.OnKeyPress(this, e, ve);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            try
            {

                if (this.DesignMode)
                {
                    return;
                }
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                GridView.OnKeyUp(this, e, ve);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            try
            {
                if (this.DesignMode)
                {
                    return;
                }
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                GridView.OnPreviewKeyDown(this, e, ve);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        protected override void OnDoubleClick(EventArgs e)
        {
            try
            {
                if (this.DesignMode)
                {
                    return;
                }
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                GridView.OnDoubleClick(this, e, ve);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        public override bool PreProcessMessage(ref Message msg)
        {
            try
            {
                if (!this.DesignMode)
                {
                    Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                    return GridView.OnPreProcessMessage(this, ref msg, ve);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return base.PreProcessMessage(ref   msg);
            
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (!this.DesignMode)
                {
                    Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                    return GridView.OnProcessCmdKey(this, ref msg, keyData, ve);
                }

            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return base.ProcessCmdKey(ref   msg, keyData);
            
        }

        protected override bool ProcessDialogChar(char charCode)
        {
            try
            {
                if (!this.DesignMode)
                {
                    Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                    return GridView.OnProcessDialogChar(this, charCode, ve);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return base.ProcessDialogChar(charCode);
           
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                if (!this.DesignMode)
                {
                    Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                    return GridView.OnProcessDialogKey(this, keyData, ve);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return base.ProcessDialogKey(keyData); 
        }

        protected override bool ProcessKeyEventArgs(ref Message m)
        {
            try
            {
                if (!this.DesignMode)
                {
                    Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                    return GridView.OnProcessKeyEventArgs(this, ref m, ve);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return base.ProcessKeyEventArgs(ref   m); 
        }

        protected override bool ProcessKeyMessage(ref Message m)
        {
            try
            {
                if (!this.DesignMode)
                {
                    Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                    return GridView.OnProcessKeyMessage(this, ref m, ve);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return base.ProcessKeyMessage(ref   m); 

        }

        protected override bool ProcessKeyPreview(ref Message m)
        {
            try
            {
                if (!this.DesignMode)
                {
                    Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                    return GridView.OnProcessKeyPreview(this, ref m, ve);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return base.ProcessKeyPreview(ref   m); 
        }
 
        protected override void WndProc(ref Message m)
        {
            try
            {
                if (!this.DesignMode)
                {
                    Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                    GridView.OnWndProc(this, ref m,ve);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            base.WndProc(ref m); 
        }
    }


}
