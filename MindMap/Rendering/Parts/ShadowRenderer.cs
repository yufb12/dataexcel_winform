using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace MindMap.Rendering
{
    /// <summary>
    /// 阴影渲染器（SRP：单一职责原则）
    /// 职责：专门负责绘制节点阴影、发光、光晕等视觉特效
    /// </summary>
    internal static class ShadowRenderer
    {
        #region 常量定义
        private const float SHADOW_OFFSET_X = 2f;
        private const float SHADOW_OFFSET_Y = 3f;
        #endregion

        #region 节点阴影绘制
        /// <summary>
        /// 绘制XMind风格的两层柔和扩散阴影
        /// </summary>
        public static void DrawNodeShadow(Graphics graphics, GraphicsPath path)
        {
            if (graphics == null) throw new ArgumentNullException("graphics");
            if (path == null) return;

            GraphicsState state = graphics.Save();
            
            try
            {
                // 第一层阴影：透明度25%，3px线宽（扩散层）
                using (Pen shadowPen1 = new Pen(Color.FromArgb(25, Color.Black), 3f))
                {
                    shadowPen1.LineJoin = LineJoin.Round;
                    graphics.TranslateTransform(SHADOW_OFFSET_X, SHADOW_OFFSET_Y);
                    graphics.DrawPath(shadowPen1, path);
                }

                // 第二层阴影：透明度12%，2px线宽（柔和层）
                using (Pen shadowPen2 = new Pen(Color.FromArgb(12, Color.Black), 2f))
                {
                    shadowPen2.LineJoin = LineJoin.Round;
                    graphics.DrawPath(shadowPen2, path);
                }
            }
            finally
            {
                graphics.Restore(state);
            }
        }
        #endregion

        #region 选中发光特效
        /// <summary>
        /// 绘制选中状态的两层柔和发光特效
        /// </summary>
        public static void DrawSelectionGlow(Graphics graphics, GraphicsPath path, Color glowColor)
        {
            if (graphics == null) throw new ArgumentNullException("graphics");
            if (path == null) return;

            // 外层光晕：透明度50%，4px蓝线
            using (Pen glowPen1 = new Pen(Color.FromArgb(50, glowColor), 4f))
            {
                glowPen1.LineJoin = LineJoin.Round;
                graphics.DrawPath(glowPen1, path);
            }

            // 内层光晕：透明度25%，2px蓝线
            using (Pen glowPen2 = new Pen(Color.FromArgb(25, glowColor), 2f))
            {
                glowPen2.LineJoin = LineJoin.Round;
                graphics.DrawPath(glowPen2, path);
            }

            // 内边框：透明度100%，1.5px蓝线
            using (Pen borderPen = new Pen(Color.FromArgb(100, glowColor), 1.5f))
            {
                borderPen.LineJoin = LineJoin.Round;
                graphics.DrawPath(borderPen, path);
            }
        }
        #endregion

        #region 图片阴影
        /// <summary>
        /// 绘制图片节点的柔和阴影
        /// </summary>
        public static void DrawImageShadow(Graphics graphics, RectangleF bounds)
        {
            if (graphics == null) throw new ArgumentNullException("graphics");

            GraphicsState state = graphics.Save();
            
            try
            {
                graphics.TranslateTransform(SHADOW_OFFSET_X, SHADOW_OFFSET_Y);
                
                // 图片阴影：圆角矩形阴影
                using (GraphicsPath shadowPath = CreateRoundedRectangle(bounds, 6))
                {
                    using (Pen shadowPen = new Pen(Color.FromArgb(30, Color.Black), 3f))
                    {
                        shadowPen.LineJoin = LineJoin.Round;
                        graphics.DrawPath(shadowPen, shadowPath);
                    }
                }
            }
            finally
            {
                graphics.Restore(state);
            }
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 创建圆角矩形路径
        /// </summary>
        private static GraphicsPath CreateRoundedRectangle(RectangleF rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();

            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            float diameter = radius * 2;
            RectangleF arcRect = new RectangleF(rect.X, rect.Y, diameter, diameter);

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
        #endregion
    }
}
