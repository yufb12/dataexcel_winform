using Feng.Data;
using Feng.Drawing;
using Feng.Forms.Interface;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Forms.Views
{
    public abstract class StyleFill
    {
        public StyleFill()
        { 
        }
        public const int SOLIDBRUSH = 1;
        
        public const int TEXTUREBRUSH = 2;
        public const int HATCHBRUSH = 2;
        public const int LINEARGRADIENTBRUSH = 2;
        public const int PATHGRADIENTBRUSH = 2;
 
        public abstract byte Mode { get; }
        public abstract Brush GetBrush();
    }
    public class StyleFillSolidBrush: StyleFill
    {
        public StyleFillSolidBrush()
        { 
        }  
        public Color SOLIDBRUSH_Color { get; set; }

        public override byte Mode { get { return SOLIDBRUSH; } }

        public override Brush GetBrush()
        {
            return new SolidBrush(SOLIDBRUSH_Color);
        }
    }
    public class StylePathGradientBrush: StyleFill
    {
        public StylePathGradientBrush()
        {

        }
        public override Brush GetBrush()
        {
            return new PathGradientBrush(Path);
        }
        public override byte Mode { get { return TEXTUREBRUSH; } }

        public GraphicsPath Path { get; set; }
        //
        // 摘要:
        //     获取或设置一个用于定义此 System.Drawing.Drawing2D.PathGradientBrush 的局部几何变换的 System.Drawing.Drawing2D.Matrix
        //     的副本。
        //
        // 返回结果:
        //     定义几何变换的 System.Drawing.Drawing2D.Matrix 的副本，该变换仅适用于通过此 System.Drawing.Drawing2D.PathGradientBrush
        //     绘制的填充。
        public Matrix Transform { get; set; }
        //
        // 摘要:
        //     获取或设置一个定义多色线性渐变的 System.Drawing.Drawing2D.ColorBlend。
        //
        // 返回结果:
        //     一个 System.Drawing.Drawing2D.ColorBlend，定义多色线性渐变。
        public ColorBlend InterpolationColors { get; set; }
        //
        // 摘要:
        //     获取或设置一个 System.Drawing.Drawing2D.Blend，它指定定义渐变自定义过渡的位置和因子。
        //
        // 返回结果:
        //     一个 System.Drawing.Drawing2D.Blend，表示渐变的自定义过渡。
        public Blend Blend { get; set; }
        //
        // 摘要:
        //     获取此 System.Drawing.Drawing2D.PathGradientBrush 的边框。
        //
        // 返回结果:
        //     一个 System.Drawing.RectangleF，表示限定此 System.Drawing.Drawing2D.PathGradientBrush
        //     填充的路径的矩形区域。
        public RectangleF Rectangle { get; }
        //
        // 摘要:
        //     获取或设置路径渐变的中心点。
        //
        // 返回结果:
        //     一个 System.Drawing.PointF，表示路径渐变的中心点。
        public PointF CenterPoint { get; set; }
        //
        // 摘要:
        //     获取或设置与此 System.Drawing.Drawing2D.PathGradientBrush 填充的路径中的点相对应的颜色的数组。
        //
        // 返回结果:
        //     一个 System.Drawing.Color 结构的数组，表示与此 System.Drawing.Drawing2D.PathGradientBrush
        //     填充的路径中的各点相关联的颜色。
        public Color[] SurroundColors { get; set; }
        //
        // 摘要:
        //     获取或设置路径渐变的中心处的颜色。
        //
        // 返回结果:
        //     一个 System.Drawing.Color，表示路径渐变的中心处的颜色。
        public Color CenterColor { get; set; }
        //
        // 摘要:
        //     获取或设置渐变过渡的焦点。
        //
        // 返回结果:
        //     一个 System.Drawing.PointF，表示渐变过渡的焦点。
        public PointF FocusScales { get; set; }
        //
        // 摘要:
        //     获取或设置 System.Drawing.Drawing2D.WrapMode，它指示该 System.Drawing.Drawing2D.PathGradientBrush
        //     的环绕模式。
        //
        // 返回结果:
        //     一个 System.Drawing.Drawing2D.WrapMode，指定使用此 System.Drawing.Drawing2D.PathGradientBrush
        //     绘制的填充的平铺方式。
        public WrapMode WrapMode { get; set; }
    }
}

