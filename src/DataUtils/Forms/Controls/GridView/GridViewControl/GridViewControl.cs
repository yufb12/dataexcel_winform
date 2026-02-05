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

namespace Feng.Forms.Controls.GridControl
{
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(GridViewControl), "GridViewControl.bmp")]
    public partial class GridViewControl : Control
    {
        public GridViewControl()
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

        private GridViewControlView _gridview = null;
        public GridViewControlView GridView
        {
            get
            {
                try
                {

                    if (_gridview == null)
                    {
                        _gridview = new GridViewControlView(this);
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
                currentGraphicsObject.WorkArea = new Rectangle(0, 0, this.Width, this.Height);
                Graphics g = e.Graphics; 

                if (currentGraphicsObject.Items != null)
                {
                    currentGraphicsObject.Items.Clear();
                }
                this.GridView.OnDraw(this, currentGraphicsObject);
                currentGraphicsObject.Graphics.ResetClip();
                currentGraphicsObject.Graphics.ResetTransform();
                currentGraphicsObject.Graphics.DrawString(Feng.Drawing.GraphicsObject.DebugText, currentGraphicsObject.DefaultFont, Brushes.Red, currentGraphicsObject.WorkArea);
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                if (this.GridView != null)
                {
                    this.GridView.RefreshAll();
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
            Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
            this.Focus();
            GridView.OnMouseDown(this, e, ve);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {

            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);

                if (this.DesignMode)
                {
                    return;
                }
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                this.BeginSetCursor(System.Windows.Forms.Cursors.Default);
                if (this.DesignMode)
                {
                    return;
                }
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);

                if (this.DesignMode)
                {
                    return;
                }
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                if (this.DesignMode)
                {
                    return;
                }
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);

                if (this.DesignMode)
                {
                    return;
                }
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);

                if (this.DesignMode)
                {
                    return;
                }
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);

                if (this.DesignMode)
                {
                    return;
                }
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);

                if (this.DesignMode)
                {
                    return;
                }
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                if (this.DesignMode)
                {
                    return;
                }
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                if (this.DesignMode)
                {
                    return;
                }
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                if (this.DesignMode)
                {
                    return;
                }
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                if (this.DesignMode)
                {
                    return;
                }
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                if (this.DesignMode)
                {
                    return;
                }
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                if (this.DesignMode)
                {
                    return;
                }
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                if (this.DesignMode)
                {
                    return;
                }
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                if (!this.DesignMode)
                {
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                if (!this.DesignMode)
                {
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                if (!this.DesignMode)
                {
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                if (!this.DesignMode)
                {
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                if (!this.DesignMode)
                {
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                if (!this.DesignMode)
                {
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                if (!this.DesignMode)
                {
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
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                if (!this.DesignMode)
                {
                    GridView.OnWndProc(this, ref m, ve);
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
