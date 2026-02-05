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
using Feng.Forms.Views;

namespace Feng.Forms.Controls
{
    [ToolboxItem(true)]
    public class ToolBarViewControl : System.Windows.Forms.Control
    {
        public ToolBarViewControl()
        {
            base.SetStyle(ControlStyles.DoubleBuffer
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint, true);

            base.SetStyle(ControlStyles.ContainerControl, true);
            base.SetStyle(ControlStyles.StandardClick, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            base.UpdateStyles();
            this.Height = 30;
        }

        ToolBarView toolbarview = null ;

        public virtual ToolBarView ToolBarView
        {
            get {
                return toolbarview;
            }
        }
        public virtual void Init()
        {
            toolbarview = new ToolBarView();
            ToolBarView.BackColor = Color.White;
            toolbarview.Width = this.Width;
            toolbarview.Height = this.Height;
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        { 
            try
            {
                ToolBarView.OnDraw(this, new GraphicsObject() { Graphics = e.Graphics });
            }
            catch (Exception ex)
            {Feng.Utils.BugReport.Log(ex);
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

            base.OnPaint(e);
        }

    
 
    }
     

}
