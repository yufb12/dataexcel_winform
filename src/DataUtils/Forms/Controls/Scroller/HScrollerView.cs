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

    public class HScrollerView : ScrollerView
    {
        public HScrollerView()
        {
        }
        public override int Height
        {
            get
            {
                if (base.Height < 18)
                { return 18; }
                return base.Height;
            }
            set
            {
                base.Height = value;
            }
        }
        public override int Header
        {
            get { return (int)this.Height; }
        }
 
        public override bool OnMouseMove(Point pt)
        {
            return base.OnMouseMove(pt);
        }
        public override Point GetMovePoint(Point pt, Size sf)
        {
            Point pd = new Point(pt.X - sf.Width, pt.Y); 
            return pd;
        }
        public override Rectangle BodyArea
        {
            get
            {
                return new Rectangle(this.Left + this.Header, this.Top, this.Width - this.Header - this.Header, this.Height);
            }
        }
        public override void PointToIndex(Point pt)
        {
            int count = this.Count;
            int height = this.BodyArea.Width;
            int large = this.LargeChange;
            int thickness = (int)(1f * height * large / count);
            float smallwidth = 1f * (height - thickness) / count;
            int pheight = pt.X - this.Left - this.Header;
       
            int topcount = (int)(pheight / smallwidth);
            int i = topcount;

            this.Value = this.Min + i;
        }
        public override Rectangle UpArrowArea
        {
            get
            {
                Rectangle rects = this.Rect;
                return new Rectangle (rects.Left, rects.Top, rects.Height, rects.Height);
            }
        }
        public override Rectangle DownArrowArea
        {
            get
            {
                Rectangle rects = this.Rect;
                return new Rectangle(rects.Right - rects.Height, rects.Top, rects.Height, rects.Height);
            }
        }
        public override Rectangle ThumdArea
        {
            get
            {
                Rectangle  rects = this.Rect;
                return new Rectangle(rects.Left + this.Header + this.ScrollTop,
                    rects.Top, this.Thickness, this.Rect.Height);
            }
        }
        public override Rectangle Rect
        {
            get
            {
                return new Rectangle (this.Left, this.Top, this.Width, this.Height);
            }
        }
        public override bool OnDraw(Graphics g)
        {
            if (this.Visible)
            {
                DrawArea(g);
                DrawArrow(g);
                DrawThumdBack(g);
                DrawThumd(g);
                DrawBorder(g);
            }
            return false;
        }
        public override void RefreshScrollThumd()
        {
            int count = this.Count;
            if (count < 1)
            {
                count = 1;
            }
            int height = this.BodyArea.Width;
            int large = this.LargeChange;
            int thickness = (int)(1f * height * large / count);
            float smallwidth = 1f * (height - thickness) / count;
            this.Thickness = thickness;
            if (Thickness < 16)
            {
                Thickness = 16;
            }
            int top = (int)((this.Value - this.Min) * smallwidth);
            if ((top + thickness) > height)
            {
                top = height - thickness;
            }
            this.ScrollTop = top;
 
        }
        private void DrawArrow(Graphics g)
        {
            DrawUpArrow(g);
            DrawDownArrow(g);
        }
        private void DrawArea(Graphics g)
        {
            Rectangle rect = this.Rect; 
            Feng.Forms.Skins.HScrollerSkin.Default.DrawArea(g, rect);
        }
        private void DrawBorder(Graphics g)
        {
            return;

        }
        private void DrawThumdBack(Graphics g)
        {
 
             

            Rectangle rects = this.Rect;
            Rectangle rect = new Rectangle(rects.Left + this.Header + this.ScrollTop, rects.Top, this.Thickness, this.Rect.Height);
            Feng.Forms.Skins.HScrollerSkin.Default.DrawThumdBack(g, rect, this.MoveSelected);

        }
        private void DrawThumd(Graphics g)
        {
            Rectangle rects = this.Rect;
            Rectangle rect = new Rectangle(rects.Left + this.Header + this.ScrollTop, rects.Top, this.Thickness, this.Rect.Height);
            Feng.Forms.Skins.HScrollerSkin.Default.DrawThumd(g, rect, this.Thickness);
        }
        private void DrawUpArrow(Graphics g)
        {
            Rectangle rects = this.Rect;  
            Feng.Forms.Skins.HScrollerSkin.Default.DrawUpArrow(g, rects);
        }
        private void DrawDownArrow(Graphics g)
        {
            Rectangle rects = this.Rect;  
            Feng.Forms.Skins.HScrollerSkin.Default.DrawDownArrow(g, rects);
        }
    }

    public class VScrollerView : ScrollerView
    {
        public VScrollerView()
        {

        }
        public override int Width
        {
            get
            {
                if (base.Width < 18)
                { return 18; }
                return base.Width;
            }
            set
            {
                base.Width = value;
            }
        }
        private void DrawArrow(Graphics g)
        {
            DrawUpArrow(g);
            DrawDownArrow(g);
        }
        private void DrawArea(Graphics g)
        {
            Rectangle rect = this.Rect;
            Feng.Forms.Skins.VScrollerSkin.Default.DrawArea(g, rect);
        }
        private void DrawBorder(Graphics g)
        {
            return;

        }
        private void DrawThumdBack(Graphics g)
        {
            if (this.Thickness <= 0)
                return; 
            Feng.Forms.Skins.VScrollerSkin.Default.DrawThumdBack(g, this.ThumdArea, this.MoveSelected);

        }
        private void DrawThumd(Graphics g)
        { 
            Feng.Forms.Skins.VScrollerSkin.Default.DrawThumd(g, this.ThumdArea, this.Thickness);
        }
        private void DrawUpArrow(Graphics g)
        {
            Rectangle rects = this.Rect;
            Rectangle rectt = new Rectangle(rects.Left, rects.Top, rects.Height, rects.Height);

            Feng.Forms.Skins.VScrollerSkin.Default.DrawUpArrow(g, rects);
        }
        private void DrawDownArrow(Graphics g)
        {
            Rectangle rects = this.Rect;
            Feng.Forms.Skins.VScrollerSkin.Default.DrawDownArrow(g, rects);
        }
        public override bool OnDraw(Graphics g)
        {
            if (this.Visible)
            {
                DrawArea(g);
                DrawThumdBack(g);
                DrawArrow(g);
                DrawThumd(g);
                DrawBorder(g);
            }
            return false;
        }
 
    }

}
