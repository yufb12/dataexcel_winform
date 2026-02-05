using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Runtime;

namespace Feng.Drawing
{
    public static class GraphicsHelper
    {

        public static SizeF GetTextSize(Graphics g, string text, Font font)
        {
            return g.MeasureString(text, font);
        }

        public static void FillRect(Graphics g, Rectangle rect, Color color1, Color color2, LinearGradientMode gradientMode)
        {
            using (Brush brush = new LinearGradientBrush(rect, color1, color2, gradientMode))
                g.FillRectangle(brush, rect);
        }

        public static void FillRectangleLinearGradient(Graphics g, Color color, Rectangle rect, LinearGradientMode gradientMode, bool drawborder, float borderbwidth, int radius)
        {
            if (radius > 0)
            {
                Color syscolor = color;

                Color color1 = ColorHelper.LightLight(syscolor);
                Color color2 = syscolor;

                using (Brush brush = new LinearGradientBrush(rect, color1, color2, gradientMode))
                    g.FillRectangle(brush, rect);
                if (drawborder)
                {
                    Color color3 = Color.FromArgb(200, ColorHelper.Dark(syscolor));
                    using (Pen pen = new Pen(color3, borderbwidth))
                    {
                        g.DrawRectangle(pen, rect);
                    }
                }
            }
            else
            {

            }
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, Rectangle rect, LinearGradientMode gradientMode, bool drawborder)
        {
            FillRectangleLinearGradient(g, color, rect, gradientMode, drawborder, 1);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, Rectangle rect, LinearGradientMode gradientMode)
        {
            FillRectangleLinearGradient(g, color, rect, gradientMode, false, 1);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, Rectangle rect)
        {
            FillRectangleLinearGradient(g, color, rect, LinearGradientMode.Horizontal, false, 1);
        }
 
        public static void FillRectangleLinearGradient(Graphics g, Color color, Rectangle rect, bool drawborder, float borderbwidth)
        {
            FillRectangleLinearGradient(g, color, rect, LinearGradientMode.Horizontal, drawborder, borderbwidth);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, Rectangle rect, bool drawborder)
        {
            FillRectangleLinearGradient(g, color, rect, LinearGradientMode.Horizontal, drawborder, 1);
        }
 
        public static void FillRectangleLinearGradient(Graphics g, Color color, RectangleF rect, LinearGradientMode gradientMode, bool drawborder, float borderbwidth)
        {
            if (rect.Width <= 0)
                return;
            if (rect.Height <= 0)
                return;
            Color syscolor = color;

            Color color1 = Feng.Drawing.ColorHelper.LightColor(syscolor, 0.5f);
            Color color2 = syscolor;

            using (Brush brush = new LinearGradientBrush(rect, color1, color2, gradientMode))
                g.FillRectangle(brush, rect);
            if (drawborder)
            {
                Color color3 = ColorHelper.Light(syscolor);
                //Color color3 = Color.FromArgb(80, ColorHelper.Dark(syscolor));
                using (Pen pen = new Pen(color3, borderbwidth))
                {
                    g.DrawRectangle(pen, rect.Left, rect.Top, rect.Width, rect.Height);
                }
            }
        }

        public static void DrawRectangle(Graphics graphics, Pen pen, int left, int top, int width, int height)
        {
            graphics.DrawRectangle(pen, left, top, width, height);
        }
        public static void DrawRectangle(Graphics graphics, Pen pen, Rectangle rect)
        {
            graphics.DrawRectangle(pen, rect);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, RectangleF rect, LinearGradientMode gradientMode, bool drawborder)
        {
            FillRectangleLinearGradient(g, color, rect, gradientMode, drawborder, 1);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, RectangleF rect, LinearGradientMode gradientMode)
        {
            FillRectangleLinearGradient(g, color, rect, gradientMode, false, 1);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, RectangleF rect)
        {
            FillRectangleLinearGradient(g, color, rect, LinearGradientMode.Horizontal, false, 1);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, RectangleF rect, bool drawborder, float borderbwidth)
        {
            FillRectangleLinearGradient(g, color, rect, LinearGradientMode.Horizontal, drawborder, borderbwidth);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, RectangleF rect, bool drawborder)
        {
            FillRectangleLinearGradient(g, color, rect, LinearGradientMode.Horizontal, drawborder, 1);
        }
 
        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, Rectangle rect, LinearGradientMode gradientMode, bool drawborder, float borderbwidth)
        {  

            using (Brush brush = new LinearGradientBrush(rect, color1, color2, gradientMode))
                g.FillRectangle(brush, rect);
            if (drawborder)
            {
                Color color3 = Color.FromArgb(200, ColorHelper.Dark(color1));
                using (Pen pen = new Pen(color3, borderbwidth))
                {
                    g.DrawRectangle(pen, rect);
                }
            }
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, Rectangle rect, LinearGradientMode gradientMode, bool drawborder)
        {
            FillRectangleLinearGradient(g, color1, color2, rect, gradientMode, drawborder, 1);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, Rectangle rect, LinearGradientMode gradientMode)
        {
            FillRectangleLinearGradient(g, color1, color2, rect, gradientMode, false, 1);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, Rectangle rect)
        {
            FillRectangleLinearGradient(g, color1, color2, rect, LinearGradientMode.Horizontal, false, 1);
        }
 
        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, Rectangle rect, bool drawborder, float borderbwidth)
        {
            FillRectangleLinearGradient(g, color1, color2, rect, LinearGradientMode.Horizontal, drawborder, borderbwidth);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, Rectangle rect, bool drawborder)
        {
            FillRectangleLinearGradient(g, color1, color2, rect, LinearGradientMode.Horizontal, drawborder, 1);
        }

        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, RectangleF rect, LinearGradientMode gradientMode, bool drawborder, float borderbwidth)
        {
 

            using (Brush brush = new LinearGradientBrush(rect, color1, color2, gradientMode))
                g.FillRectangle(brush, rect);
            if (drawborder)
            {
                Color color3 = Color.FromArgb(200, ColorHelper.Dark(color1));
                using (Pen pen = new Pen(color3, borderbwidth))
                {
                    g.DrawRectangle(pen, rect.Left, rect.Top, rect.Width, rect.Height);
                }
            }
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, RectangleF rect, LinearGradientMode gradientMode, bool drawborder)
        {
            FillRectangleLinearGradient(g, color1, color2, rect, gradientMode, drawborder, 1);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, RectangleF rect, LinearGradientMode gradientMode)
        {
            FillRectangleLinearGradient(g, color1, color2, rect, gradientMode, false, 1);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, RectangleF rect)
        {
            FillRectangleLinearGradient(g, color1, color2, rect, LinearGradientMode.Horizontal, false, 1);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, RectangleF rect, bool drawborder, float borderbwidth)
        {
            FillRectangleLinearGradient(g, color1, color2, rect, LinearGradientMode.Horizontal, drawborder, borderbwidth);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, RectangleF rect, bool drawborder)
        {
            FillRectangleLinearGradient(g, color1, color2, rect, LinearGradientMode.Horizontal, drawborder, 1);
        }
 
        public static void FillRectangleLinearGradient(Graphics g, Color color, int lef, int top, int width, int height, LinearGradientMode gradientMode, bool drawborder, float borderbwidth)
        {
            Rectangle rect = new Rectangle(lef, top, width, height);
            Color syscolor = color;

            Color color1 = ColorHelper.LightLight(syscolor);
            Color color2 = syscolor;

            using (Brush brush = new LinearGradientBrush(rect, color1, color2, gradientMode))
                g.FillRectangle(brush, rect);
            if (drawborder)
            {
                Color color3 = Color.FromArgb(200, ColorHelper.Dark(syscolor));
                using (Pen pen = new Pen(color3, borderbwidth))
                {
                    g.DrawRectangle(pen, rect);
                }
            }
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, int lef, int top, int width, int height, LinearGradientMode gradientMode, bool drawborder)
        {
            FillRectangleLinearGradient(g, color, lef, top, width, height, gradientMode, drawborder, 1);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, int lef, int top, int width, int height, LinearGradientMode gradientMode)
        {
            FillRectangleLinearGradient(g, color, lef, top, width, height, gradientMode, false, 1);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, int lef, int top, int width, int height)
        {
            FillRectangleLinearGradient(g, color, lef, top, width, height, LinearGradientMode.Horizontal, false, 1);
        }
 
        public static void FillRectangleLinearGradient(Graphics g, Color color, int lef, int top, int width, int height, bool drawborder, float borderbwidth)
        {
            FillRectangleLinearGradient(g, color, lef, top, width, height, LinearGradientMode.Horizontal, drawborder, borderbwidth);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, int lef, int top, int width, int height, bool drawborder)
        {
            FillRectangleLinearGradient(g, color, lef, top, width, height, LinearGradientMode.Horizontal, drawborder, 1);
        }

        public static void FillRectangleLinearGradient(Graphics g, Color color, float lef, float top, float width, float height, LinearGradientMode gradientMode, bool drawborder, float borderbwidth)
        {
            Color syscolor = color;
            RectangleF rect = new RectangleF(lef, top, width, height);
            Color color1 = ColorHelper.LightLight(syscolor);
            Color color2 = syscolor;

            using (Brush brush = new LinearGradientBrush(rect, color1, color2, gradientMode))
                g.FillRectangle(brush, rect);
            if (drawborder)
            {
                Color color3 = Color.FromArgb(200, ColorHelper.LightLight(syscolor));
                using (Pen pen = new Pen(color3, borderbwidth))
                {
                    g.DrawRectangle(pen, rect.Left, rect.Top, rect.Width, rect.Height);
                }
            }
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, float lef, float top, float width, float height, LinearGradientMode gradientMode, bool drawborder)
        {
            FillRectangleLinearGradient(g, color, lef, top, width, height, gradientMode, drawborder, 1);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, float lef, float top, float width, float height, LinearGradientMode gradientMode)
        {
            FillRectangleLinearGradient(g, color, lef, top, width, height, gradientMode, false, 1);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, float lef, float top, float width, float height)
        {
            FillRectangleLinearGradient(g, color, lef, top, width, height, LinearGradientMode.Horizontal, false, 1);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, float lef, float top, float width, float height, bool drawborder, float borderbwidth)
        {
            FillRectangleLinearGradient(g, color, lef, top, width, height, LinearGradientMode.Horizontal, drawborder, borderbwidth);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color, float lef, float top, float width, float height, bool drawborder)
        {
            FillRectangleLinearGradient(g, color, lef, top, width, height, LinearGradientMode.Horizontal, drawborder, 1);
        }

        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, int lef, int top, int width, int height, LinearGradientMode gradientMode, bool drawborder, float borderbwidth)
        {
            Rectangle rect = new Rectangle(lef, top, width, height);
            using (Brush brush = new LinearGradientBrush(rect, color1, color2, gradientMode))
                g.FillRectangle(brush, rect);
            if (drawborder)
            {
                Color color3 = Color.FromArgb(200, ColorHelper.Dark(color1));
                using (Pen pen = new Pen(color3, borderbwidth))
                {
                    g.DrawRectangle(pen, rect);
                }
            }
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, int lef, int top, int width, int height, LinearGradientMode gradientMode, bool drawborder)
        {
            FillRectangleLinearGradient(g, color1, color2, lef, top, width, height, gradientMode, drawborder, 1);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, int lef, int top, int width, int height, LinearGradientMode gradientMode)
        {
            FillRectangleLinearGradient(g, color1, color2, lef, top, width, height, gradientMode, false, 1);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, int lef, int top, int width, int height)
        {
            FillRectangleLinearGradient(g, color1, color2, lef, top, width, height, LinearGradientMode.Horizontal, false, 1);
        }

        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, int lef, int top, int width, int height, bool drawborder, float borderbwidth)
        {
            FillRectangleLinearGradient(g, color1, color2, lef, top, width, height, LinearGradientMode.Horizontal, drawborder, borderbwidth);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, int lef, int top, int width, int height, bool drawborder)
        {
            FillRectangleLinearGradient(g, color1, color2, lef, top, width, height, LinearGradientMode.Horizontal, drawborder, 1);
        }
 
        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, float lef, float top, float width, float height, LinearGradientMode gradientMode, bool drawborder, float borderbwidth, Color bordercolor, byte bordercolora)
        {

            RectangleF rect = new RectangleF(lef, top, width, height);
            using (Brush brush = new LinearGradientBrush(rect, color1, color2, gradientMode))
                g.FillRectangle(brush, rect);
            if (drawborder)
            {
                bordercolor = Color.FromArgb(200, ColorHelper.Dark(bordercolor));
                using (Pen pen = new Pen(bordercolor, borderbwidth))
                {
                    g.DrawRectangle(pen, rect.Left, rect.Top, rect.Width, rect.Height);
                }
            }
        }

        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, float lef, float top, float width, float height, LinearGradientMode gradientMode, bool drawborder, float borderbwidth, Color bordercolor, int radius)
        {
            if (radius <= 0)
            {
                RectangleF rect = new RectangleF(lef, top, width, height);
                using (Brush brush = new LinearGradientBrush(rect, color1, color2, gradientMode))
                    g.FillRectangle(brush, rect);
                if (drawborder)
                {
                    using (Pen pen = new Pen(bordercolor, borderbwidth))
                    {
                        g.DrawRectangle(pen, rect.Left, rect.Top, rect.Width - borderbwidth, rect.Height - borderbwidth);
                    }
                }
            }
            else
            {
                RectangleF rect = new RectangleF(lef, top, width, height);
                GraphicsPath path = CreateRoundedRectanglePath(rect, radius);
                using (Brush brush = new LinearGradientBrush(rect, color1, color2, gradientMode))
                    g.FillPath(brush, path);
                if (drawborder)
                {
                    using (Pen pen = new Pen(bordercolor, borderbwidth))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }
        }

        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, float lef, float top, float width, float height, LinearGradientMode gradientMode, bool drawborder, float borderbwidth)
        {
            Color color3 = Color.FromArgb(200, ColorHelper.Dark(color1));
            FillRectangleLinearGradient(g, color1, color2, lef, top, width, height, gradientMode, drawborder, borderbwidth, color3,0);
        }

        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, float lef, float top, float width, float height, LinearGradientMode gradientMode, bool drawborder)
        {
            FillRectangleLinearGradient(g, color1, color2, lef, top, width, height, gradientMode, drawborder, 1);
        }

        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, float lef, float top, float width, float height, LinearGradientMode gradientMode)
        {
            FillRectangleLinearGradient(g, color1, color2, lef, top, width, height, gradientMode, false, 1);
        }

        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, float lef, float top, float width, float height)
        {
            FillRectangleLinearGradient(g, color1, color2, lef, top, width, height, LinearGradientMode.Horizontal, false, 1);
        }

        //
        // 摘要:
        //     使用指定 System.Drawing.StringFormat 的格式化属性，用指定的 System.Drawing.Brush 和 System.Drawing.Font
        //     对象在指定的矩形绘制指定的文本字符串。
        //
        // 参数:
        //   s:
        //     要绘制的字符串。
        //
        //   font:
        //     System.Drawing.Font，它定义字符串的文本格式。
        //
        //   brush:
        //     System.Drawing.Brush，它确定所绘制文本的颜色和纹理。
        //
        //   layoutRectangle:
        //     System.Drawing.RectangleF 结构，它指定所绘制文本的位置。
        //
        //   format:
        //     System.Drawing.StringFormat，它指定应用于所绘制文本的格式化属性（如行距和对齐方式）。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     brush 为 null。- 或 -s 为 null。
        public static void DrawString(Feng.Drawing.GraphicsObject g, string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format)
        {
            g.Graphics.DrawString(s, font, brush, layoutRectangle, format);
        }
        //
        // 摘要:
        //     在指定矩形并且用指定的 System.Drawing.Brush 和 System.Drawing.Font 对象绘制指定的文本字符串。
        //
        // 参数:
        //   s:
        //     要绘制的字符串。
        //
        //   font:
        //     System.Drawing.Font，它定义字符串的文本格式。
        //
        //   brush:
        //     System.Drawing.Brush，它确定所绘制文本的颜色和纹理。
        //
        //   layoutRectangle:
        //     System.Drawing.RectangleF 结构，它指定所绘制文本的位置。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     brush 为 null。- 或 -s 为 null。
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public static void DrawString(Feng.Drawing.GraphicsObject g, string s, Font font, Brush brush, RectangleF layoutRectangle)
        {
            g.Graphics.DrawString(s, font, brush, layoutRectangle);
        }
        //
        // 摘要:
        //     使用指定 System.Drawing.StringFormat 的格式化属性，用指定的 System.Drawing.Brush 和 System.Drawing.Font
        //     对象在指定的位置绘制指定的文本字符串。
        //
        // 参数:
        //   s:
        //     要绘制的字符串。
        //
        //   font:
        //     System.Drawing.Font，它定义字符串的文本格式。
        //
        //   brush:
        //     System.Drawing.Brush，它确定所绘制文本的颜色和纹理。
        //
        //   point:
        //     System.Drawing.PointF 结构，它指定所绘制文本的左上角。
        //
        //   format:
        //     System.Drawing.StringFormat，它指定应用于所绘制文本的格式化属性（如行距和对齐方式）。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     brush 为 null。- 或 -s 为 null。
        public static void DrawString(Feng.Drawing.GraphicsObject g, string s, Font font, Brush brush, PointF point, StringFormat format)
        {
            g.Graphics.DrawString(s, font, brush, point, format);
        }
        //
        // 摘要:
        //     使用指定 System.Drawing.StringFormat 的格式化属性，用指定的 System.Drawing.Brush 和 System.Drawing.Font
        //     对象在指定的位置绘制指定的文本字符串。
        //
        // 参数:
        //   s:
        //     要绘制的字符串。
        //
        //   font:
        //     System.Drawing.Font，它定义字符串的文本格式。
        //
        //   brush:
        //     System.Drawing.Brush，它确定所绘制文本的颜色和纹理。
        //
        //   x:
        //     所绘制文本的左上角的 x 坐标。
        //
        //   y:
        //     所绘制文本的左上角的 y 坐标。
        //
        //   format:
        //     System.Drawing.StringFormat，它指定应用于所绘制文本的格式化属性（如行距和对齐方式）。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     brush 为 null。- 或 -s 为 null。
        public static void DrawString(Feng.Drawing.GraphicsObject g, string s, Font font, Brush brush, float x, float y, StringFormat format)
        {
            g.Graphics.DrawString(s, font, brush, x, y, format);
        }
        //
        // 摘要:
        //     在指定位置并且用指定的 System.Drawing.Brush 和 System.Drawing.Font 对象绘制指定的文本字符串。
        //
        // 参数:
        //   s:
        //     要绘制的字符串。
        //
        //   font:
        //     System.Drawing.Font，它定义字符串的文本格式。
        //
        //   brush:
        //     System.Drawing.Brush，它确定所绘制文本的颜色和纹理。
        //
        //   point:
        //     System.Drawing.PointF 结构，它指定所绘制文本的左上角。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     brush 为 null。- 或 -s 为 null。
        public static void DrawString(Feng.Drawing.GraphicsObject g, string s, Font font, Brush brush, PointF point)
        {
            g.Graphics.DrawString(s, font, brush, point);
        }
        //
        // 摘要:
        //     在指定位置并且用指定的 System.Drawing.Brush 和 System.Drawing.Font 对象绘制指定的文本字符串。
        //
        // 参数:
        //   s:
        //     要绘制的字符串。
        //
        //   font:
        //     System.Drawing.Font，它定义字符串的文本格式。
        //
        //   brush:
        //     System.Drawing.Brush，它确定所绘制文本的颜色和纹理。
        //
        //   x:
        //     所绘制文本的左上角的 x 坐标。
        //
        //   y:
        //     所绘制文本的左上角的 y 坐标。
        //
        // 异常:
        //   T:System.ArgumentNullException:
        //     brush 为 null。- 或 -s 为 null。
        public static void DrawString(Feng.Drawing.GraphicsObject g, string s, Font font, Brush brush, float x, float y)
        {
            g.Graphics.DrawString( s, font, brush, x, y);
        }

        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, float lef, float top, float width, float height, bool drawborder, float borderbwidth)
        {
            FillRectangleLinearGradient(g, color1, color2, lef, top, width, height, LinearGradientMode.Horizontal, drawborder, borderbwidth);
        }
        public static void FillRectangleLinearGradient(Graphics g, Color color1, Color color2, float lef, float top, float width, float height, bool drawborder)
        {
            FillRectangleLinearGradient(g, color1, color2, lef, top, width, height, LinearGradientMode.Horizontal, drawborder, 1);
        }

        public static void FillRectangle(Graphics g, Brush sb, int left,int top,int width,int height)
        {
            if (left == 40 && top == 20)
            {

            }
            g.FillRectangle(sb, left, top, width, height);
        }
        public static void FillRectangle(Graphics g, Brush sb, Rectangle rect)
        {
            if (rect.Width>300)
            { 
            }
            g.FillRectangle(sb, rect);
            //g.FillRectangle(Brushes.Red, rect);
        }
        public static void FillRectangle(Graphics graphics, Rectangle rect, Color color)
        {
              color = ColorHelper.Opacity(color,70);
            Brush sb = SolidBrushCache.GetSolidBrush(color);
            graphics.FillRectangle(sb, rect);
        }
        public static void FillRectangle(Graphics g, Brush sb, RectangleF rect)
        {
            g.FillRectangle(sb, rect);
        }
        public static void FillRectangle(Graphics g, Color backColor, float lef, float top, float width, float height, 
            bool drawborder, float borderbwidth, Color bordercolor, int radius)
        {
            RectangleF rect = new RectangleF(lef, top, width, height);
            if (radius <= 0)
            {
                using (SolidBrush sb = new SolidBrush(backColor))
                {
                    g.FillRectangle(sb, rect);
                }
                if (drawborder)
                {
                    using (Pen pen = new Pen(bordercolor, borderbwidth))
                    {
                        g.DrawRectangle(pen, rect.Left, rect.Top, rect.Width - borderbwidth, rect.Height - borderbwidth);
                    }
                }
            }
            else
            { 
                GraphicsPath path = CreateRoundedRectanglePath(rect, radius);
                using (SolidBrush sb = new SolidBrush(backColor))
                {
                    g.FillPath(sb, path);
                }
                if (drawborder)
                {
                    using (Pen pen = new Pen(bordercolor, borderbwidth))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }
        }
        public static void FillRectangle(Graphics g, Color backColor, int lef, int top, int width, int height,
    bool drawborder, int borderbwidth, Color bordercolor, int radius)
        {
            RectangleF rect = new RectangleF(lef, top, width, height);
            if (radius <= 0)
            {
                using (SolidBrush sb = new SolidBrush(backColor))
                {
                    g.FillRectangle(sb, rect);
                }
                if (drawborder)
                {
                    using (Pen pen = new Pen(bordercolor, borderbwidth))
                    {
                        g.DrawRectangle(pen, rect.Left, rect.Top, rect.Width - borderbwidth, rect.Height - borderbwidth);
                    }
                }
            }
            else
            {
                GraphicsPath path = CreateRoundedRectanglePath(rect, radius);
                using (SolidBrush sb = new SolidBrush(backColor))
                {
                    g.FillPath(sb, path);
                }
                if (drawborder)
                {
                    using (Pen pen = new Pen(bordercolor, borderbwidth))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }
        }
        public static void DrawRectangle(Graphics g, float lef, float top, float width, float height, float borderbwidth, Color bordercolor, int radius)
        {
            if (radius <= 0)
            {
                RectangleF rect = new RectangleF(lef, top, width, height);
                using (Pen pen = new Pen(bordercolor, borderbwidth))
                {
                    g.DrawRectangle(pen, rect.Left, rect.Top, rect.Width - borderbwidth, rect.Height - borderbwidth);
                } 
            }
            else
            {
                RectangleF rect = new RectangleF(lef, top, width, height);
                GraphicsPath path = CreateRoundedRectanglePath(rect, radius);

                using (Pen pen = new Pen(bordercolor, borderbwidth))
                {
                    g.DrawPath(pen, path);
                } 
            }
        }

        public static void DrawBorder(Graphics g,Rectangle rect)
        {
            g.DrawRectangle(Pens.Gray, rect);
        }

        public static RectangleF DrawCheckBox(Graphics g, RectangleF bounds, int state, string text
            , StringFormat stringformat, Color forecolor, Font font)
        {
            RectangleF rect = new RectangleF(new PointF(bounds.Left + 2, bounds.Top + (bounds.Height / 2 - 13 / 2 - 1)),
                new Size(13, 13));

            if (state == 1)
            {
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                float zoom = 1.4F;
                float x = rect.Left;
                float y = rect.Top + 50;
                y = rect.Top + rect.Height / 2 - 4;
                path.AddPolygon(new PointF[] {
            new PointF (){ X =2*zoom+x ,Y =3*zoom+y },
             new PointF (){ X =4*zoom+x,Y =5*zoom+y},
                         new PointF (){ X =8*zoom+x,Y =1*zoom+y},           
                         new PointF (){ X =8*zoom+x,Y =3*zoom+y},           
                         new PointF (){ X =4*zoom+x,Y =7*zoom+y},          
                         new PointF (){ X =2*zoom+x,Y =5*zoom+y},         
 
            });

                g.FillPath(Brushes.Green, path);
            }
            g.DrawRectangle(Pens.SlateBlue, rect.X, rect.Y, rect.Width, rect.Height);

 
            RectangleF rectf = new RectangleF(new PointF(rect.Left + rect.Width, bounds.Top + 2),
                new SizeF(bounds.Width - rect.Width, bounds.Height));


            if (text != string.Empty)
            {
                using (SolidBrush brush = new SolidBrush(forecolor))
                {
                    g.DrawString(text, font, brush, rectf, stringformat);
                }
            }
            return rect;
        }

        public static RectangleF DrawCheckBox(Graphics g, RectangleF bounds, int state)
        {
            RectangleF rect = new RectangleF(new PointF(bounds.Left + 2, bounds.Top + (bounds.Height / 2 - 13 / 2 - 1)),
                new Size(13, 13));

            if (state == 1)
            {
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                float zoom = 1.4F;
                float x = rect.Left;
                float y = rect.Top + 50;
                y = rect.Top + rect.Height / 2 - 4;
                path.AddPolygon(new PointF[] {
            new PointF (){ X =2*zoom+x ,Y =3*zoom+y },
             new PointF (){ X =4*zoom+x,Y =5*zoom+y},
                         new PointF (){ X =8*zoom+x,Y =1*zoom+y},
                         new PointF (){ X =8*zoom+x,Y =3*zoom+y},
                         new PointF (){ X =4*zoom+x,Y =7*zoom+y},
                         new PointF (){ X =2*zoom+x,Y =5*zoom+y},

            });

                g.FillPath(Brushes.Green, path);
            }
            g.DrawRectangle(Pens.SlateBlue, rect.X, rect.Y, rect.Width, rect.Height);
            return rect;
        }
        public static void DrawUpArrowButton(Graphics g, RectangleF rect, Color color, Color arrowcolor)
        {
            GraphicsState gs = g.Save();
            RectangleF rects = rect;
            RectangleF rectt = new RectangleF(rects.Left, rects.Top, rects.Width, rects.Width);
            GraphicsHelper.FillRectangleLinearGradient(g, color, rectt,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            rectt.Inflate(-3, -4);
            GraphicsPath path = GraphicsHelper.GetTriangle(rectt
, Feng.Drawing.Orientation.Bottom);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //g.FillRectangle(Brushes.Red, Rectangle.Round(rectt));
            GraphicsHelper.FillColorPath(g, arrowcolor, rects, path,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical, Feng.Drawing.Orientation.Bottom);
            g.Restore(gs);
        }

        public static void DrawDownArrowButton(Graphics g, RectangleF rect, Color color, Color arrowcolor)
        {
            GraphicsState gs = g.Save();
            RectangleF rects = rect;
            RectangleF rectt = new RectangleF(rects.Left, rects.Bottom - rects.Width, rects.Width, rects.Width);
            GraphicsHelper.FillRectangleLinearGradient(g, color, rectt,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            rectt.Inflate(-3, -4);
            GraphicsPath path = GraphicsHelper.GetTriangle(rectt
, Feng.Drawing.Orientation.Top);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //g.FillRectangle(Brushes.Red, Rectangle.Round(rectt));
            GraphicsHelper.FillColorPath(g, arrowcolor, rects, path,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical, Feng.Drawing.Orientation.Top);
            g.Restore(gs);
        }
 
        public static void DrawPageUpArrowButton(Graphics g, RectangleF rect, Color color, Color arrowcolor)
        {
            GraphicsState gs = g.Save();
            RectangleF rects = rect;
            RectangleF rectt = new RectangleF(rects.Left, rects.Top, rects.Width, rects.Width);
            GraphicsHelper.FillRectangleLinearGradient(g, color, rectt,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            rectt.Inflate(-3, -4);
            GraphicsPath path = GraphicsHelper.GetTriangle(rectt
, Feng.Drawing.Orientation.Bottom);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            GraphicsHelper.FillColorPath(g, arrowcolor, rects, path,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical, Feng.Drawing.Orientation.Bottom);
            rectt.Offset(0, -3);
            path = GraphicsHelper.GetTriangle(rectt
, Feng.Drawing.Orientation.Bottom);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            GraphicsHelper.FillColorPath(g, arrowcolor, rects, path,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical, Feng.Drawing.Orientation.Bottom);
            g.Restore(gs);
        }

        public static void DrawPageDownArrowButton(Graphics g, RectangleF rect, Color color, Color arrowcolor)
        {
            GraphicsState gs = g.Save();
            RectangleF rects = rect;
            RectangleF rectt = new RectangleF(rects.Left, rects.Bottom - rects.Width, rects.Width, rects.Width);
            GraphicsHelper.FillRectangleLinearGradient(g, color, rectt,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            rectt.Inflate(-3, -4);
            GraphicsPath path = GraphicsHelper.GetTriangle(rectt
, Feng.Drawing.Orientation.Top);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //g.FillRectangle(Brushes.Red, Rectangle.Round(rectt));
            GraphicsHelper.FillColorPath(g, arrowcolor, rects, path,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical, Feng.Drawing.Orientation.Top);
            rectt.Offset(0, 3);
            path = GraphicsHelper.GetTriangle(rectt
, Feng.Drawing.Orientation.Top);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            //g.FillRectangle(Brushes.Red, Rectangle.Round(rectt));
            GraphicsHelper.FillColorPath(g, arrowcolor, rects, path,
                System.Drawing.Drawing2D.LinearGradientMode.Vertical, Feng.Drawing.Orientation.Top);
            g.Restore(gs);
        }

        public static void FillColorPath(Graphics g, Color color, RectangleF rect, GraphicsPath path,
            LinearGradientMode gradientMode, Orientation or)
        {
            if (rect.Width <= 0)
                return;
            if (rect.Height <= 0)
                return;
            Color syscolor = color;
            if (or == Orientation.Bottom)
            {
                Color color1 = syscolor;
                Color color2 =ColorHelper.LightLight(syscolor);  

                using (Brush brush = new LinearGradientBrush(rect, color1, color2, gradientMode))
                    g.FillPath(brush, path);
            }
            else if (or == Orientation.Right)
            {
                Color color1 = syscolor;
                Color color2 = ColorHelper.LightLight(syscolor);  

                using (Brush brush = new LinearGradientBrush(rect, color1, color2, gradientMode))
                    g.FillPath(brush, path);
            }
            else
            {
                Color color1 = ColorHelper.LightLight(syscolor);
                Color color2 = syscolor;

                using (Brush brush = new LinearGradientBrush(rect, color1, color2, gradientMode))
                    g.FillPath(brush, path);
            }
  
        }

  
        public static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius, cornerRadius, 180, 90);
            roundedRect.AddArc(rect.Right - cornerRadius - 1, rect.Y, cornerRadius, cornerRadius, 270, 90);
            roundedRect.AddArc(rect.Right - cornerRadius - 1, rect.Bottom - cornerRadius - 1, cornerRadius, cornerRadius, 0, 90);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius - 1, cornerRadius, cornerRadius, 90, 90);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        public static GraphicsPath CreateRoundedRectanglePath(RectangleF rect, int cornerRadius)
        { 
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius, cornerRadius, 180, 90);
            roundedRect.AddArc(rect.Right - cornerRadius - 1, rect.Y, cornerRadius, cornerRadius, 270, 90);
            roundedRect.AddArc(rect.Right - cornerRadius - 1, rect.Bottom - cornerRadius - 1, cornerRadius, cornerRadius, 0, 90);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius - 1, cornerRadius, cornerRadius, 90, 90);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        public static void FillColorRadiusRectangle(Graphics g, Rectangle rect, Color cor)
        {
            Color c2 = ColorHelper.LightLight(cor);
            Color c1 = ColorHelper.DarkColor(cor, 0.8f);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            SolidBrush sb = new SolidBrush(cor);
            GraphicsPath rtt = CreateRoundedRectanglePath(rect, 6);
#if DEBUG
            GraphicsState gs = g.Save();
            g.SetClip(rect);
            //g.FillRectangle(Brushes.Red, rect);
#endif
            g.FillPath(sb, rtt);

            g.DrawPath(new Pen(Color.FromArgb(120, c2)), rtt);

#if DEBUG
            g.Restore(gs);
#endif
            return;
        }

        public static void FillColorRadiusRectangle(Graphics g, RectangleF rect, Color cor)
        {
            Color c2 = ColorHelper.LightLight(cor);
            Color c1 = ColorHelper.DarkColor(cor, 0.8f);
            g.SmoothingMode = SmoothingMode.AntiAlias;
            SolidBrush sb = new SolidBrush(cor);
            GraphicsPath rtt = CreateRoundedRectanglePath(rect, 6);
#if DEBUG
            GraphicsState gs = g.Save();
            g.SetClip(rect);
            //g.FillRectangle(Brushes.Red, rect);
#endif
            g.FillPath(sb, rtt);

            g.DrawPath(new Pen(Color.FromArgb(120, c2)), rtt);

#if DEBUG
            g.Restore(gs);
#endif
            return;
            //GraphicsPath rt = CreateRoundedRectanglePath(rect, 1);

            //GraphicsPath path = new GraphicsPath(FillMode.Alternate);
            //path.AddPath(rt, true);

            //g.SmoothingMode = SmoothingMode.AntiAlias;

            //LinearGradientBrush lgb = new LinearGradientBrush(rect, c1, c2, 240);
          
            //ColorBlend cb = new ColorBlend(4);
            //cb.Colors = new Color[] { c1, c2, c1 };
            //cb.Positions = new float[] { 0f, 0.5f, 1f };
            //lgb.InterpolationColors = cb;
            //lgb.SetBlendTriangularShape(1f, 0.1f);
            //g.DrawPath(Pens.White, path);
            //g.FillPath(lgb, path);
        }

        public static PointF MovePointX(PointF point,float value)
        {
            return new PointF(point.X + value, point.Y);
        }

        public static PointF MovePointToX(PointF point, float value)
        {
            return new PointF(value, point.Y);
        }
 
        public static PointF MovePointY(PointF point, float value)
        {
            return new PointF(point.X, point.Y + value);
        }

        public static PointF MovePointToY(PointF point, float value)
        {
            return new PointF(point.X, value);
        }

        public static GraphicsPath GetTriangle(RectangleF rect, Orientation or)
        {
            GraphicsPath path = new GraphicsPath();
            PointF[] pntArr = new PointF[3];
            switch (or )
            {
                case Orientation.Left:
                    pntArr[0] = new PointF(rect.Left, rect.Top);
                    pntArr[1] = new PointF(rect.Left, rect.Bottom);
                    pntArr[2] = new PointF(rect.Right, rect.Top + rect.Height / 2);
                    path.AddLines(pntArr);
                    break;
                case Orientation.Right:
                    pntArr[0] = new PointF(rect.Right, rect.Top);
                    pntArr[1] = new PointF(rect.Right, rect.Bottom);
                    pntArr[2] = new PointF(rect.Left, rect.Top + rect.Height / 2);
                    path.AddLines(pntArr);
                    break;
                case Orientation.Top:
                    pntArr[0] = new PointF(rect.Left, rect.Top );
                    pntArr[1] = new PointF(rect.Right, rect.Top);
                    pntArr[2] = new PointF(rect.Left + rect.Width / 2, rect.Bottom);
                    path.AddLines(pntArr);
                    break;
                case Orientation.Bottom:
                    pntArr[0] = new PointF(rect.Left, rect.Bottom);
                    pntArr[1] = new PointF(rect.Right, rect.Bottom);
                    pntArr[2] = new PointF(rect.Left + rect.Width / 2, rect.Top);
                    path.AddLines(pntArr);
                    break;
                default:
                    break;
            }

  
            return path;
        }

        public static void FillGradients(Graphics g, RectangleF rect, Color color)
        {
            Color syscolor = color;

            Color color1 = ColorHelper.LightLight(syscolor);
            Color color2 = syscolor;
 
            RectangleF rect1 = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height / 3);
                using (Brush brush = new LinearGradientBrush(rect, color1, color2, LinearGradientMode.Vertical))
                    g.FillRectangle(brush, rect1);
                rect1 = new RectangleF(rect.X, rect.Y + rect.Height / 3, rect.Width, rect.Height * 2 / 3);
                using (Brush brush = new LinearGradientBrush(rect, color2, color1, LinearGradientMode.Vertical))
                    g.FillRectangle(brush, rect1); 
        }


        public static void DrawText(Graphics g, Font font, string text, Color forecolor,
Rectangle bounds, bool multiline, StringAlignment rowAlingment, StringAlignment lineAlignment)
        {
            using (SolidBrush sb = new SolidBrush(forecolor))
            {
                DrawText(g, font, text, sb,
                  bounds, 0, 0, 0, 0, multiline
                , rowAlingment, lineAlignment, false);
            }
        }

        public static void DrawText(Graphics g, Font font, string text, Color forecolor, Rectangle bounds, int padding)
        {
            using (SolidBrush sb = new SolidBrush(forecolor))
            {
                DrawText(g, font, text, sb,
                  bounds, padding, padding, padding, padding, true 
                , StringAlignment.Center, StringAlignment.Center, false);
            }
        }
 
        public static void DrawText(Graphics g, Font font, string text, Color forecolor, Rectangle bounds)
        {
            SolidBrush sb = SolidBrushCache.GetSolidBrush(forecolor);

            DrawText(g, font, text, sb,
              bounds, 0, 0, 0, 0, true
            , StringAlignment.Center, StringAlignment.Center, false);

        }
 
        public static void DrawText(Graphics g, Font font, string text, Color forecolor,
Rectangle bounds, int padding, bool multiline)
        {
            using (SolidBrush sb = new SolidBrush(forecolor))
            {
                DrawText(g, font, text, sb,
                  bounds, padding, padding, padding, padding, multiline
                , StringAlignment.Center, StringAlignment.Center, false);
            }
        }

        public static void DrawText(Graphics g, Font font, string text, Color forecolor,
Rectangle bounds, int padding, bool multiline, StringAlignment rowAlingment, StringAlignment lineAlignment)
        {
            using (SolidBrush sb = new SolidBrush(forecolor))
            {
                DrawText(g, font, text, sb,
                  bounds, padding, padding, padding, padding, multiline
                , rowAlingment, lineAlignment, false);
            }
        }

        public static void DrawText(Graphics g, Font font, string text, Color forecolor,
Rectangle bounds, int padding, bool multiline, StringAlignment rowAlingment, StringAlignment lineAlignment, bool direction)
        {
            using (SolidBrush sb = new SolidBrush(forecolor))
            {
                DrawText(g, font, text, sb,
                  bounds, padding, padding, padding, padding, multiline
                , rowAlingment, lineAlignment, direction);
            }
        }

        public static void DrawText(Graphics g, Font font, string text, Brush brush,
       Rectangle bounds, int padding, bool multiline, StringAlignment rowAlingment, StringAlignment lineAlignment, bool direction)
        {
            DrawText(g, font, text, brush,
              bounds, padding, padding, padding, padding, multiline
            , rowAlingment, lineAlignment, direction);
        }
        public static void DrawText(Graphics g, Font font, string text, Brush brush,
            Rectangle bounds, int  paddingleft,int paddingtop, int paddingright,int paddingbottom, bool multiline
            , StringAlignment rowAlingment, StringAlignment lineAlignment, bool direction)
        {
            StringFormat sf = Feng.Drawing.StringFormatCache.GetStringFormat(
                rowAlingment, lineAlignment, direction);

            
            Rectangle rect =
                new Rectangle(
                    bounds.Left + paddingleft, bounds.Top + paddingtop,
                    bounds.Width - paddingleft - paddingright, bounds.Height - paddingtop - paddingbottom);


            rect.Location = new Point(rect.Location.X, rect.Location.Y);
            g.DrawString(text, font, brush, rect, sf); 
        }
        public static void DrawBorderLeft(Graphics g, Rectangle bounds)
        {
            g.DrawLine(PenCache.GetPen(Color.FromArgb(100, 192, 192, 192)), bounds.Left, bounds.Top, bounds.Left, bounds.Bottom);
        }
        public static void DrawBorderRight(Graphics g, Rectangle bounds)
        {
            g.DrawLine(PenCache.GetPen(Color.FromArgb(100, 192, 192, 192)), bounds.Right, bounds.Top, bounds.Right, bounds.Bottom);
        }
        public static Color GetColorInstead(Color color)
        {
            return Color.FromArgb(255 - color.R, 255 - color.G, 255 - color.B);
        }
        public static void DrawImage(Graphics g, Image img, Rectangle bounds, ImageLayout mode)
        {
            Rectangle  rect = Feng.Drawing.ImageHelper.ImageRectangleFromSizeMode(mode, img, bounds);
            g.DrawImage(img, rect);
        }
 
        #region 用户函数

        public const TextFormatFlags textFormatFlags = TextFormatFlags.NoPadding | TextFormatFlags.NoPrefix | TextFormatFlags.PreserveGraphicsClipping;
        public static Size Sizeof(char c, Font style, Graphics g)
        {
            Size strsizef = TextRenderer.MeasureText(g, c.ToString(), style,
                new Size(short.MaxValue, short.MaxValue), textFormatFlags);
            return strsizef;
        }
        public static Size Sizeof(string c, Font style, Graphics g)
        {
            Size strsizef = Size.Empty;
            try
            { 
                strsizef = TextRenderer.MeasureText(g, c, style, new Size(short.MaxValue, short.MaxValue), textFormatFlags);
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "Sizeof", "Sizeof", ex);
            }
            return strsizef;
        }
        public static Size Sizeof(string c, Font style, Graphics g, TextFormatFlags textformatflags)
        {
            Size strsizef = TextRenderer.MeasureText(g, c, style, new Size(short.MaxValue, short.MaxValue), textformatflags);
            return strsizef;
        }
        public static Size Sizeof(string c, Font style, Control ctl)
        {

            try
            { 
                Graphics g = ctl.CreateGraphics();
                Size strsizef = TextRenderer.MeasureText(g, c, style, new Size(short.MaxValue, short.MaxValue), textFormatFlags);
                g.Dispose();
                return strsizef;
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
            }
            return Size.Empty;
        }
 
        #endregion
 
        public static int GetPointX(Rectangle bounds, Size sf, Margin margin, Padding padding, AlignMode align)
        {
            int x = bounds.X;
            Size sz = GetMarginSize(sf, margin);
            sz = GetMarginSize(sf, padding);

            switch (align)
            {
                case AlignMode.Center:
                    x = bounds.Left + sz.Width / 2;
                    break;
                case AlignMode.Near:
                    x = bounds.Left + sz.Width + sf.Width;
                    break;
                case AlignMode.Far:
                    x = bounds.Right - sz.Width - (sz.Width - sf.Width);
                    break;
                default:
                    x = bounds.X;
                    break;
            }

            return x;
        }
        public static int GetPointY(Rectangle bounds, Size sf, Margin margin, Padding padding, AlignMode align)
        {
            int y = bounds.Top;
            Size sz = GetMarginSize(sf, margin);
            sz = GetMarginSize(sf, padding);

            switch (align)
            {
                case AlignMode.Center:
                    y = bounds.Top + sz.Height / 2;
                    break;
                case AlignMode.Near:
                    y = bounds.Top + sz.Height + sf.Height;
                    break;
                case AlignMode.Far:
                    y = bounds.Bottom - sz.Height - (sz.Height - sf.Height);
                    break;
                default:
                    y = bounds.X;
                    break;
            }

            return y;
        }
        public static Size GetMarginSize(Size sf, Margin margin)
        {
            int width = sf.Width;
            int height = sf.Height;
            if (margin != null)
            {
                width = width + margin.Left + margin.Right;
                height = height + margin.Top + margin.Bottom;
            }
            return new Size(width, height);
        }
        public static Size GetMarginSize(Size sf, Padding padding)
        {
            int width = sf.Width;
            int height = sf.Height;
            if (padding != null)
            {
                width = width - padding.Left - padding.Right;
                height = height - padding.Top - padding.Bottom;
            }
            return new Size(width, height);
        }

        public static Rectangle GetPaddingRectPositionLeft(Rectangle bounds, Size sf, Padding padding)
        {
            Size sz = sf;
            if (bounds.Width > sf.Width && bounds.Height > sf.Height)
            {
                return new Rectangle(bounds.Left + padding.Left,
                    bounds.Top + padding.Top, sz.Width, sz.Height);
            }
            return Rectangle.Empty;
        }

        public static Rectangle GetMarginRectPositionLeft(Rectangle bounds, Size sf, Margin margin)
        {
            Size sz = GetMarginSize(sf, margin);
            if (bounds.Width > sz.Width && bounds.Height > sz.Height)
            {
                return new Rectangle(bounds.Left - margin.Left,
                    bounds.Top - margin.Top, sf.Width, sf.Height);
            }
            return Rectangle.Empty;
        }

        public static Rectangle GetPaddingRectPositionTop(Rectangle bounds, Size sf, Padding padding)
        {
            Size sz = sf;
            if (bounds.Width > sf.Width && bounds.Height > sf.Height)
            {
                return new Rectangle(bounds.Left + padding.Left + bounds.Width / 2,
                    bounds.Top + padding.Top, sz.Width, sz.Height);
            }
            return Rectangle.Empty;
        }

        public static Rectangle GetMarginRectPositionTop(Rectangle bounds, Size sf, Margin margin)
        {
            Size sz = GetMarginSize(sf, margin);
            if (bounds.Width > sz.Width && bounds.Height > sz.Height)
            {
                return new Rectangle(bounds.Left - margin.Left + bounds.Width / 2,
                    bounds.Top + margin.Top, sf.Width, sf.Height);
            }
            return Rectangle.Empty;
        }

        public static Rectangle GetPaddingRectPositionRight(Rectangle bounds, Size sf, Padding padding)
        {
            Size sz = sf;
            if (bounds.Width > sf.Width && bounds.Height > sf.Height)
            {
                return new Rectangle(bounds.Right - sz.Width - padding.Right,
                    bounds.Top + padding.Top, sz.Width, sz.Height);
            }
            return Rectangle.Empty;
        }

        public static Rectangle GetMarginRectPositionRight(Rectangle bounds, Size sf, Margin margin)
        {
            Size sz = GetMarginSize(sf, margin);
            if (bounds.Width > sz.Width && bounds.Height > sz.Height)
            {
                return new Rectangle(bounds.Right - sf.Width - margin.Right,
                    bounds.Top - margin.Top, sf.Width, sf.Height);
            }
            return Rectangle.Empty;
        }

        public static Rectangle GetPaddingRectPositionBottom(Rectangle bounds, Size sf, Padding padding)
        {
            Size sz = sf;
            if (bounds.Width > sf.Width && bounds.Height > sf.Height)
            {
                return new Rectangle(bounds.Left + padding.Left + bounds.Width / 2,
                    bounds.Bottom - sz.Height - padding.Bottom, sz.Width, sz.Height);
            }
            return Rectangle.Empty;
        }

        public static Rectangle GetMarginRectPositionBottom(Rectangle bounds, Size sf, Margin margin)
        {
            Size sz = GetMarginSize(sf, margin);
            if (bounds.Width > sz.Width && bounds.Height > sz.Height)
            {
                return new Rectangle(bounds.Left - margin.Left + bounds.Width / 2,
                    bounds.Top - sf.Height - margin.Bottom, sf.Width, sf.Height);
            }
            return Rectangle.Empty;
        }

        private static bool _ShowDebugDrawRect = false;

        public static bool ShowDebugDrawRect
        {
            get
            {
                return _ShowDebugDrawRect;
            }
        }

        /// <summary>
        /// 将宽度或高度为负数的矩形转换为标准的正矩形
        /// </summary>
        /// <param name="rect">要处理的矩形</param>
        /// <returns>处理后的标准矩形</returns>
        public static Rectangle NormalizeRectangle(Rectangle rect)
        {
            // 检查宽度是否为负数
            if (rect.Width < 0)
            {
                rect.X += rect.Width;  // 调整X坐标
                rect.Width = Math.Abs(rect.Width);  // 宽度转为正数
            }

            // 检查高度是否为负数
            if (rect.Height < 0)
            {
                rect.Y += rect.Height;  // 调整Y坐标
                rect.Height = Math.Abs(rect.Height);  // 高度转为正数
            }

            return rect;
        }

        [System.Diagnostics.Conditional("DEBUG")]
        public static void DrawDebugRect(Graphics g, Rectangle rect, string key, string value)
        {
            Dictionary<string, Color> dics = new Dictionary<string, Color>();
            dics.Add("DataTreeRow", Color.FromArgb(100, 120, 231, 36));
            dics.Add("DataTreeViewControl", Color.FromArgb(100, 120, 211, 136));
            Color c = Color.Fuchsia;
            if (dics.ContainsKey(key))
            {
                c = dics[key];
            }
            FillRectangleLinearGradient(g, c, rect);
        }
        [System.Diagnostics.Conditional("DEBUG")]
        public static void DrawDebugRect(Graphics g, Rectangle rect, string key)
        {
            DrawDebugRect(g, rect, key, string.Empty);
        }
    }
}
