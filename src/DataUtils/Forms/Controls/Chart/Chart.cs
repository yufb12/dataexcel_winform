using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D; 
using Feng.Forms.Controls.Designer;
using Feng.Drawing;

namespace Feng.Forms.Controls
{
    [ToolboxItem(false)]
    public class Chart : System.Windows.Forms.Control 
    {
        public Chart()
        {
            base.SetStyle(ControlStyles.DoubleBuffer
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint, true);

            base.SetStyle(ControlStyles.ContainerControl, true);
            base.SetStyle(ControlStyles.StandardClick, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            base.UpdateStyles();
            Init();
        }
        public void Init()
        {
            _panes = new PaneCollection();
        }
        private PaneCollection _panes = null;
        public virtual PaneCollection Panes
        {
            get {
                return _panes;
            }
        }
 
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {

            try
            { 
                Graphics g = e.Graphics;
                g.SmoothingMode = SmoothingMode.HighQuality;
                OnDraw(g);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

            base.OnPaint(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            this.Invalidate();
            base.OnMouseUp(e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            this.Invalidate();
            base.OnMouseMove(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            this.Invalidate();
            base.OnMouseLeave(e);
        }
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            this.Invalidate();
            base.OnMouseDown(e);
        }
 
        protected override void OnSizeChanged(EventArgs e)
        {
            this.Invalidate();
            base.OnSizeChanged(e);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        { 
            base.OnMouseWheel(e);
        }
 
        public void OnDraw(Graphics g)
        {
 
        }
        public void PropertyChanged()
        {

        }  
        
    }
 
}
