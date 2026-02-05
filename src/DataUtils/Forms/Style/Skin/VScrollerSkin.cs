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
    public class VScrollerSkin : ScrollerSkin
    {
        private static VScrollerSkin _default = null;
        public static VScrollerSkin Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new VScrollerSkin();
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
            //GraphicsHelper.FillRectangleLinearGradient(g, thumdbackcolor, rect, LinearGradientMode.Horizontal, true);
        }

        public void DrawThumd(Graphics g, Rectangle rect, int Thickness)
        {
            //GraphicsHelper.DrawDebugRect(g, rect, string.Empty);
            if (Thickness > 15)
            {
                Color dc = Color.FromArgb(100, Color.Black);
                using (Pen p = new Pen(dc))
                {

                    int x = -3;
                    PointF pt1 = new PointF(rect.Left + 2.5f, rect.Top + rect.Height / 2 - x);
                    PointF pt2 = new PointF(rect.Right - 2.5f, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + 3, rect.Top + rect.Height / 2 - x);
                    pt2 = new PointF(rect.Right - 3, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + 2, rect.Top + rect.Height / 2 - x);
                    pt2 = new PointF(rect.Right - 2, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + 3, rect.Top + rect.Height / 2 - x);
                    pt2 = new PointF(rect.Right - 3, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + 2.5f, rect.Top + rect.Height / 2 - x);
                    pt2 = new PointF(rect.Right - 2.5f, rect.Top + rect.Height / 2 - x);
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
                    PointF pt1 = new PointF(rect.Left + 2.5f, rect.Top + rect.Height / 2 - x);
                    PointF pt2 = new PointF(rect.Right - 2.5f, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                    pt1 = new PointF(rect.Left + 2.5f, rect.Top + rect.Height / 2 - x);
                    pt2 = new PointF(rect.Right - 2.5f, rect.Top + rect.Height / 2 - x);
                    x += 2;
                    g.DrawLine(p, pt1, pt2);
                }
            }
        }

        public void DrawUpArrow(Graphics g, Rectangle rect)
        {
            Rectangle rectt = new Rectangle(rect.Left, rect.Top, rect.Width, rect.Width);
            GraphicsHelper.FillRectangleLinearGradient(g, BackDirectionColor, rectt,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            rectt.Inflate(-3, -4);
            GraphicsPath path = GraphicsHelper.GetTriangle(rectt
, Feng.Drawing.Orientation.Bottom);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            GraphicsHelper.FillColorPath(g, ArrowColor, rect, path,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical, Feng.Drawing.Orientation.Bottom);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
        }

        public void DrawDownArrow(Graphics g, Rectangle rects)
        {
            Rectangle rectt = new Rectangle(rects.Left, rects.Bottom - rects.Width, rects.Width, rects.Width);
            GraphicsHelper.FillRectangleLinearGradient(g, BackDirectionColor, rectt,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            rectt.Inflate(-3, -4);
            GraphicsPath path = GraphicsHelper.GetTriangle(rectt
, Feng.Drawing.Orientation.Top);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
   
            GraphicsHelper.FillColorPath(g, ArrowColor, rects, path,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical, Feng.Drawing.Orientation.Top);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

        }
    }
}
