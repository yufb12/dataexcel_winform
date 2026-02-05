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

namespace Feng.Forms.Skins
{ 
    public class HScrollerSkin : ScrollerSkin
    {
        private static HScrollerSkin _default = null;
        public static HScrollerSkin Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new HScrollerSkin();
                }
                return _default;
            }
        }

        public void DrawArea(Graphics g, Rectangle rect)
        {
            SolidBrush sb = SolidBrushCache.GetSolidBrush(BackAreaColor);
            Feng.Drawing.GraphicsHelper.FillRectangle(g, sb, rect);
             
        }

        public void DrawThumdBack(Graphics g, Rectangle rect, bool MoveSelected)
        {
            Color thumdbackcolor = ThumdBackColor;
            if (MoveSelected)
            {
                thumdbackcolor = SelectThumdBackcolor;
            }
            SolidBrush sb = SolidBrushCache.GetSolidBrush(thumdbackcolor);
            Feng.Drawing.GraphicsHelper.FillRectangle(g, sb, rect);
            //GraphicsHelper.FillRectangleLinearGradient(g, thumdbackcolor, rect, LinearGradientMode.Vertical, true);
        }

        public void DrawThumd(Graphics g, Rectangle rect, int Thickness)
        {
            if (Thickness > 15)
            {
                Color dc = Color.FromArgb(100, Color.Black);
                using (Pen p = new Pen(dc))
                {
                    int x = -3;
                    PointF pt1 = new PointF(rect.Left + rect.Width / 2 - x, rect.Top + 2.5f);
                    PointF pt2 = new PointF(rect.Left + rect.Width / 2 - x, rect.Bottom - 2.5f);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + rect.Width / 2 - x, rect.Top + 3);
                    pt2 = new PointF(rect.Left + rect.Width / 2 - x, rect.Bottom - 3);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + rect.Width / 2 - x, rect.Top + 2);
                    pt2 = new PointF(rect.Left + rect.Width / 2 - x, rect.Bottom - 2);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + rect.Width / 2 - x, rect.Top + 3);
                    pt2 = new PointF(rect.Left + rect.Width / 2 - x, rect.Bottom - 3);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + rect.Width / 2 - x, rect.Top + 2.5f);
                    pt2 = new PointF(rect.Left + rect.Width / 2 - x, rect.Bottom - 2.5f);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                }
            }
            else if (Thickness > 6)
            {
                Color dc = Color.FromArgb(100, Color.Black);
                using (Pen p = new Pen(dc))
                {
                    int x = -1;
                    PointF pt1 = new PointF(rect.Left + rect.Width / 2 - x, rect.Top + 2.5f);
                    PointF pt2 = new PointF(rect.Left + rect.Width / 2 - x, rect.Bottom - 2.5f);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + rect.Width / 2 - x, rect.Top + 2.5f);
                    pt2 = new PointF(rect.Left + rect.Width / 2 - x, rect.Bottom - 2.5f);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                }
            }
        }

        public void DrawUpArrow(Graphics g, Rectangle rects)
        { 
            RectangleF rectt = new RectangleF(rects.Left, rects.Top, rects.Height, rects.Height);

            GraphicsHelper.FillRectangleLinearGradient(g, BackDirectionColor, rectt,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            rectt.Inflate(-3, -4);
            GraphicsPath path = GraphicsHelper.GetTriangle(rectt
, Feng.Drawing.Orientation.Right);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias; 
            GraphicsHelper.FillColorPath(g, ArrowColor, rects, path,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical, Feng.Drawing.Orientation.Bottom);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
 
        }

        public void DrawDownArrow(Graphics g, Rectangle rects)
        {
            RectangleF rectt = new RectangleF(rects.Right - rects.Height, rects.Top, rects.Height, rects.Height);
            GraphicsHelper.FillRectangleLinearGradient(g, BackDirectionColor, rectt,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            rectt.Inflate(-3, -4);
            GraphicsPath path = GraphicsHelper.GetTriangle(rectt
, Feng.Drawing.Orientation.Left);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
          
            GraphicsHelper.FillColorPath(g, ArrowColor, rects, path,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical, Feng.Drawing.Orientation.Top);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
 
        }
    } 
}
