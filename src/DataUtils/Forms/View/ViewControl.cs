using Feng.Drawing;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Forms.Views
{
    public class ViewControl : System.Windows.Forms.Control
    {
        public ViewControl()
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
        }
        private ViewCollection viewes = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ViewCollection Viewes
        {
            get
            {
                if (viewes == null)
                {
                    viewes = new ViewCollection();
                }
                return viewes;
            }
            set
            {
                viewes = value;
            }
        }

        public virtual void AddView(DivView view)
        {
            if (!this.Viewes.Contains(view))
            {
                this.Viewes.Add(view);
            }
        }

        private Cursor _begincursor = null;
        public virtual void BeginSetCursor(Cursor begincursor)
        {
            _begincursor = begincursor; 
        }

        public virtual void EndSetCursor()
        {
            if (this._begincursor == null)
                return;
            if (_begincursor != this.Cursor)
            {
                this.Cursor = _begincursor;
            }
        }

        private Feng.Forms.Caret Caret = null;
        public virtual void ShowCaret(int heigth, int x, int y)
        {
            if (Caret == null)
            {
                Caret = new Feng.Forms.Caret();
                Caret.Handle = this.Handle;
            }
            if (Caret != null)
            {
                Caret.Show(this.Handle, heigth, x, y);
            }
        }

        protected override void WndProc(ref Message m)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnWndProc(this, ref m,ve);
                    if (res)
                    {
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "WndProc", ex);
            }

            base.WndProc(ref m);
        }
        protected override void OnMouseCaptureChanged(EventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnMouseCaptureChanged(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseCaptureChanged", ex);
            }
            base.OnMouseCaptureChanged(e);
        }
        protected override void OnMouseClick(MouseEventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                ve.ControlPoint = e.Location;
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnMouseClick(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseClick", ex);
            }
            base.OnMouseClick(e);
        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnMouseDoubleClick(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }

                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseDoubleClick", ex);
            }
            base.OnMouseDoubleClick(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                this.Focus();
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    int x = ve.ControlPoint.X - item.Left;
                    x = x * 100 / (100 + item.Zoom);
                    ve.X = x;
                    int y = ve.ControlPoint.Y - item.Top;
                    y = y * 100 / (100 + item.Zoom);
                    ve.Y = y;
                    bool res = item.OnMouseDown(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseDown", ex);
            }
            base.OnMouseDown(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            { 
                try
                {
                    Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                    foreach (BaseView item in Viewes)
                    {
                        int x = ve.ControlPoint.X - item.Left;
                        x = x * 100 / (100 + item.Zoom);
                        ve.X = x;
                        int y = ve.ControlPoint.Y - item.Top;
                        y = y * 100 / (100 + item.Zoom);
                        ve.Y = y;
                        bool res = item.OnMouseMove(this, e, ve);
                        if (res)
                        { 
                            break;
                        }
                    }

                    if (ve.Invalate)
                    {
                        this.Invalidate();
                    }
                }
                finally
                { 
                    this.EndSetCursor();
                }

            }
            catch (Exception ex)
            { 
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseMove", ex);
            }
            base.OnMouseMove(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    ve.X = ve.ControlPoint.X - item.Left;
                    ve.Y = ve.ControlPoint.Y - item.Top;
                    bool res = item.OnMouseUp(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseUp", ex);
            }
            base.OnMouseUp(e);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnMouseEnter(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseEnter", ex);
            }
            base.OnMouseEnter(e);
        }
        protected override void OnMouseHover(EventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnMouseHover(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseHover", ex);
            }
            base.OnMouseHover(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnMouseLeave(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseLeave", ex);
            }
            base.OnMouseLeave(e);
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnMouseWheel(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            base.OnMouseWheel(e);
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnKeyDown(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            base.OnKeyDown(e);
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnKeyPress(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            base.OnKeyPress(e);
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnKeyUp(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            base.OnKeyUp(e);
        } 
        protected override void OnClick(EventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnClick(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            base.OnClick(e);
        }
        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnPreviewKeyDown(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            base.OnPreviewKeyDown(e);
        }
        protected override void OnDoubleClick(EventArgs e)
        {
            try
            { 
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnDoubleClick(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            base.OnDoubleClick(e);
        }
        public override bool PreProcessMessage(ref Message msg)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnPreProcessMessage(this, ref msg, ve);
                    if (res)
                    {
                        if (ve.Invalate)
                        {
                            this.Invalidate();
                        }
                        return true;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.PreProcessMessage(ref msg);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnProcessCmdKey(this, ref msg, keyData, ve);
                    if (res)
                    {
                        if (ve.Invalate)
                        {
                            this.Invalidate();
                        }
                        return true;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        protected override bool ProcessDialogChar(char charCode)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnProcessDialogChar(this, charCode, ve);
                    if (res)
                    {
                        if (ve.Invalate)
                        {
                            this.Invalidate();
                        }
                        return true;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.ProcessDialogChar(charCode);
        }
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnProcessDialogKey(this, keyData, ve);
                    if (res)
                    {
                        if (ve.Invalate)
                        {
                            this.Invalidate();
                        }
                        return true;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.ProcessDialogKey(keyData);
        }
        protected override bool ProcessKeyEventArgs(ref Message msg)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnProcessKeyEventArgs(this, ref msg, ve);
                    if (res)
                    {
                        if (ve.Invalate)
                        {
                            this.Invalidate();
                        }
                        return true;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.ProcessKeyEventArgs(ref msg);
        }
        protected override bool ProcessKeyMessage(ref Message msg)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnProcessKeyMessage(this, ref msg, ve);
                    if (res)
                    {
                        if (ve.Invalate)
                        {
                            this.Invalidate();
                        }
                        return true;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.ProcessKeyMessage(ref msg);
        }
        protected override bool ProcessKeyPreview(ref Message msg)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnProcessKeyPreview(this, ref msg, ve);
                    if (res)
                    {
                        if (ve.Invalate)
                        {
                            this.Invalidate();
                        }
                        return true;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.ProcessKeyPreview(ref msg);
        }
        protected override void OnDragEnter(DragEventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnDragEnter(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            base.OnDragEnter(e);
        }
        protected override void OnDragDrop(DragEventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnSizeChanged(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            base.OnDragDrop(e);
        }
        protected override void OnDragLeave(EventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnSizeChanged(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            base.OnDragLeave(e);
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnSizeChanged(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            base.OnHandleCreated(e);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            try
            {
                Views.EventViewArgs ve = Views.EventViewArgs.GetEventViewArgs(this,e);
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnSizeChanged(this, e, ve);
                    if (res)
                    {
                        break;
                    }
                }
                if (ve.Invalate)
                {
                    this.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            base.OnSizeChanged(e);
        }
        private Feng.Drawing.GraphicsObject currentGraphicsObject = null;
        private Color bordercolor = Color.Empty;
        public Color BorderColor
        {
            get { return bordercolor; }
            set { bordercolor = value; }
        }
        //protected override void OnPaintBackground(PaintEventArgs e)
        //{
        //    try
        //    {
        //        if (currentGraphicsObject == null)
        //        {
        //            currentGraphicsObject = new Feng.Drawing.GraphicsObject();
        //        }
        //        currentGraphicsObject.Graphics = e.Graphics;
        //        currentGraphicsObject.MousePoint = System.Windows.Forms.Control.MousePosition;
        //        currentGraphicsObject.ModifierKeys = System.Windows.Forms.Control.ModifierKeys;
        //        currentGraphicsObject.MouseButtons = System.Windows.Forms.Control.MouseButtons;
        //        currentGraphicsObject.Control = this;
        //        currentGraphicsObject.ClientPoint = this.PointToClient(currentGraphicsObject.MousePoint);
        //        currentGraphicsObject.ClipRectangle = this.Bounds;
        //        Graphics g = e.Graphics;
        //        GraphicsState gs = g.Save();
        //        g.PageUnit = GraphicsUnit.Pixel;
        //        g.ResetTransform();
        //        if (currentGraphicsObject.Items != null)
        //        {
        //            currentGraphicsObject.Items.Clear();
        //        }
 
        //        foreach (BaseView item in Viewes)
        //        {
        //            bool res = item.OnDrawBack(this, currentGraphicsObject);
        //            if (res)
        //            {
        //                return;
        //            }
        //        }
        //        g.Restore(gs); 
        //    }
        //    catch (Exception ex)
        //    {
        //        Feng.Utils.BugReport.Log(ex);
        //        Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
        //    } 
        //    base.OnPaintBackground(e);
        //}
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
                currentGraphicsObject.ClipRectangle = this.Bounds;
                Graphics g = e.Graphics;
                GraphicsState gs = g.Save();
                g.PageUnit = GraphicsUnit.Pixel;
                g.ResetTransform();
                if (currentGraphicsObject.Items != null)
                {
                    currentGraphicsObject.Items.Clear();
                }
                if (this.BorderColor != Color.Empty)
                {
                    Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);
                    Feng.Drawing.GraphicsHelper.DrawRectangle(g, PenCache.GetPen(this.BorderColor), rect);
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnDrawBack(this, currentGraphicsObject);
                    if (res)
                    {
                        g.Restore(gs);
                        return;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnDraw(this,currentGraphicsObject);
                    if (res)
                    {
                        g.Restore(gs);
                        return;
                    }
                }
                g.Restore(gs);
                //currentGraphicsObject.Graphics.ResetClip();
                //currentGraphicsObject.Graphics.ResetTransform();
                //currentGraphicsObject.Graphics.DrawString(currentGraphicsObject.DebugText, 
                //    currentGraphicsObject.DefaultFont, Brushes.Red, currentGraphicsObject.WorkArea);
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            base.OnPaint(e);
        }

 
        public virtual void RefreshSize()
        {
            this.OnSizeChanged(new EventArgs() { });
        }
    }
}

