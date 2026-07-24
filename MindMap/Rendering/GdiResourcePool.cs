using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MindMap.Rendering
{
    /// <summary>
    /// GDI资源池，复用Pen、Brush、GraphicsPath等对象，避免频繁创建销毁
    /// </summary>
    internal sealed class GdiResourcePool : IDisposable
    {
        private readonly Dictionary<Color, Pen> _penCache = new Dictionary<Color, Pen>();
        private readonly Dictionary<Color, Brush> _brushCache = new Dictionary<Color, Brush>();
        private bool _disposed;

        /// <summary>
        /// 获取指定颜色和宽度的Pen（复用缓存）
        /// </summary>
        /// <param name="color">颜色</param>
        /// <param name="width">宽度</param>
        /// <returns>Pen对象</returns>
        public Pen GetPen(Color color, float width)
        {
            // 对于不同宽度的Pen，使用颜色+宽度作为键
            string key = color.ToArgb() + "_" + width.ToString();
            
            // 简化处理：如果颜色相同但宽度不同，直接创建新Pen
            // 实际项目中可以使用更复杂的缓存策略
            Pen pen;
            if (!_penCache.TryGetValue(color, out pen))
            {
                pen = new Pen(color, width);
                _penCache[color] = pen;
            }
            else if (pen.Width != width)
            {
                pen.Dispose();
                pen = new Pen(color, width);
                _penCache[color] = pen;
            }
            
            return pen;
        }

        /// <summary>
        /// 获取指定颜色的Brush（复用缓存）
        /// </summary>
        /// <param name="color">颜色</param>
        /// <returns>SolidBrush对象</returns>
        public Brush GetBrush(Color color)
        {
            Brush brush;
            if (!_brushCache.TryGetValue(color, out brush))
            {
                brush = new SolidBrush(color);
                _brushCache[color] = brush;
            }
            return brush;
        }

        /// <summary>
        /// 创建新的GraphicsPath（避免共享路径导致的异常）
        /// </summary>
        /// <returns>GraphicsPath对象</returns>
        public GraphicsPath GetSharedPath()
        {
            // 每次创建新的GraphicsPath，避免共享路径导致的异常
            return new GraphicsPath();
        }

        /// <summary>
        /// 创建圆角矩形路径
        /// </summary>
        /// <param name="rect">矩形区域</param>
        /// <param name="radius">圆角半径</param>
        /// <returns>GraphicsPath对象</returns>
        public GraphicsPath CreateRoundedRectangle(RectangleF rect, float radius)
        {
            GraphicsPath path = GetSharedPath();
            
            float diameter = radius * 2;
            RectangleF arcRect = new RectangleF(rect.Location, new SizeF(diameter, diameter));

            // 左上角
            path.AddArc(arcRect, 180, 90);
            
            // 右上角
            arcRect.X = rect.Right - diameter;
            path.AddArc(arcRect, 270, 90);
            
            // 右下角
            arcRect.Y = rect.Bottom - diameter;
            path.AddArc(arcRect, 0, 90);
            
            // 左下角
            arcRect.X = rect.Left;
            path.AddArc(arcRect, 90, 90);
            
            path.CloseAllFigures();
            return path;
        }

        /// <summary>
        /// 释放所有资源
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                foreach (Pen pen in _penCache.Values)
                {
                    pen.Dispose();
                }
                _penCache.Clear();

                foreach (Brush brush in _brushCache.Values)
                {
                    brush.Dispose();
                }
                _brushCache.Clear();

                _disposed = true;
            }
        }
    }
}
