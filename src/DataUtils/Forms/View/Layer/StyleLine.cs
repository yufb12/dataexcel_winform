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
    public class StyleLine
    {
        public StyleLine()
        { 
        }

        //
        // 摘要:
        //     获取或设置通过此 System.Drawing.Pen 绘制的两条连续直线的端点的联接样式。
        //
        // 返回结果:
        //     一个 System.Drawing.Drawing2D.LineJoin，表示通过此 System.Drawing.Pen 绘制的两条连续直线的端点的联接样式。
        //
        // 异常:
        //   T:System.ArgumentException:
        //     在不可变的 System.Drawing.Pen 上设置 System.Drawing.Pen.LineJoin 属性，如由 System.Drawing.Pens
        //     类所返回。
        public LineJoin LineJoin { get; set; }
        //
        // 摘要:
        //     获取或设置要在通过此 System.Drawing.Pen 绘制的直线起点使用的自定义线帽。
        //
        // 返回结果:
        //     一个 System.Drawing.Drawing2D.CustomLineCap，表示在通过此 System.Drawing.Pen 绘制的直线起点使用的线帽。
        //
        // 异常:
        //   T:System.ArgumentException:
        //     在不可变的 System.Drawing.Pen 上设置 System.Drawing.Pen.CustomStartCap 属性，如由 System.Drawing.Pens
        //     类所返回。
        public CustomLineCap CustomStartCap { get; set; }
        //
        // 摘要:
        //     获取或设置要在通过此 System.Drawing.Pen 绘制的直线终点使用的自定义线帽。
        //
        // 返回结果:
        //     一个 System.Drawing.Drawing2D.CustomLineCap，表示在通过此 System.Drawing.Pen 绘制的直线终点使用的线帽。
        //
        // 异常:
        //   T:System.ArgumentException:
        //     在不可变的 System.Drawing.Pen 上设置 System.Drawing.Pen.CustomEndCap 属性，如由 System.Drawing.Pens
        //     类所返回。
        public CustomLineCap CustomEndCap { get; set; }
        //
        // 摘要:
        //     获取或设置斜接角上联接宽度的限制。
        //
        // 返回结果:
        //     斜接角上联接宽度的限制。
        //
        // 异常:
        //   T:System.ArgumentException:
        //     在不可变的 System.Drawing.Pen 上设置 System.Drawing.Pen.MiterLimit 属性，如由 System.Drawing.Pens
        //     类所返回。
        public float MiterLimit { get; set; }
        //
        // 摘要:
        //     获取或设置此 System.Drawing.Pen 的对齐方式。
        //
        // 返回结果:
        //     表示该 System.Drawing.Pen 的对齐方式的 System.Drawing.Drawing2D.PenAlignment。
        //
        // 异常:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     指定值不是 System.Drawing.Drawing2D.PenAlignment 的成员。
        //
        //   T:System.ArgumentException:
        //     对不可变的 System.Drawing.Pen（比如由 System.Drawing.Pens 类所返回）设置 System.Drawing.Pen.Alignment
        //     属性。
        public PenAlignment Alignment { get; set; }
        //
        // 摘要:
        //     获取或设置此 System.Drawing.Pen 的颜色。
        //
        // 返回结果:
        //     一个 System.Drawing.Color 结构，表示此 System.Drawing.Pen 的颜色。
        //
        // 异常:
        //   T:System.ArgumentException:
        //     在不可变的 System.Drawing.Pen 上设置 System.Drawing.Pen.Color 属性，如由 System.Drawing.Pens
        //     类所返回。
        public Color Color { get; set; }
        //
        // 摘要:
        //     获取用此 System.Drawing.Pen 绘制的直线的样式。
        //
        // 返回结果:
        //     System.Drawing.Drawing2D.PenType 枚举，指定用此 System.Drawing.Pen 绘制的直线的样式。
        public PenType PenType { get; }
        //
        // 摘要:
        //     获取或设置用在短划线终点的线帽样式，这些短划线构成通过此 System.Drawing.Pen 绘制的虚线。
        //
        // 返回结果:
        //     System.Drawing.Drawing2D.DashCap 值之一，表示用在短划线起点和终点的线帽样式，这些短划线构成通过此 System.Drawing.Pen
        //     绘制的虚线。
        //
        // 异常:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     指定值不是 System.Drawing.Drawing2D.DashCap 的成员。
        //
        //   T:System.ArgumentException:
        //     在不可变的 System.Drawing.Pen 上设置 System.Drawing.Pen.DashCap 属性，如由 System.Drawing.Pens
        //     类所返回。
        public DashCap DashCap { get; set; }
        //
        // 摘要:
        //     获取或设置 System.Drawing.Brush，用于确定此 System.Drawing.Pen 的属性。
        //
        // 返回结果:
        //     一个 System.Drawing.Brush，用于确定此 System.Drawing.Pen 的属性
        //
        // 异常:
        //   T:System.ArgumentException:
        //     在不可变的 System.Drawing.Pen 上设置 System.Drawing.Pen.Brush 属性，如由 System.Drawing.Pens
        //     类所返回。
        public StyleFill Brush { get; set; }
        //
        // 摘要:
        //     获取或设置用于通过此 System.Drawing.Pen 绘制的虚线的样式。
        //
        // 返回结果:
        //     一个 System.Drawing.Drawing2D.DashStyle，表示用于通过此 System.Drawing.Pen 绘制的虚线的样式。
        //
        // 异常:
        //   T:System.ArgumentException:
        //     在不可变的 System.Drawing.Pen 上设置 System.Drawing.Pen.DashStyle 属性，如由 System.Drawing.Pens
        //     类所返回。
        public DashStyle DashStyle { get; set; }
        //
        // 摘要:
        //     获取或设置直线的起点到短划线图案起始处的距离。
        //
        // 返回结果:
        //     直线的起点到短划线图案起始处的距离。
        //
        // 异常:
        //   T:System.ArgumentException:
        //     在不可变的 System.Drawing.Pen 上设置 System.Drawing.Pen.DashOffset 属性，如由 System.Drawing.Pens
        //     类所返回。
        public float DashOffset { get; set; }
        //
        // 摘要:
        //     获取或设置此 System.Drawing.Pen 的几何变换的副本。
        //
        // 返回结果:
        //     一个 System.Drawing.Drawing2D.Matrix 副本，表示此 System.Drawing.Pen 的几何变换。
        //
        // 异常:
        //   T:System.ArgumentException:
        //     在不可变的 System.Drawing.Pen 上设置 System.Drawing.Pen.Transform 属性，如由 System.Drawing.Pens
        //     类所返回。
        public Matrix Transform { get; set; }
        //
        // 摘要:
        //     获取或设置要在通过此 System.Drawing.Pen 绘制的直线终点使用的线帽样式。
        //
        // 返回结果:
        //     System.Drawing.Drawing2D.LineCap 值之一，表示在通过此 System.Drawing.Pen 绘制的直线终点使用的线帽样式。
        //
        // 异常:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     指定值不是 System.Drawing.Drawing2D.LineCap 的成员。
        //
        //   T:System.ArgumentException:
        //     在不可变的 System.Drawing.Pen 上设置 System.Drawing.Pen.EndCap 属性，如由 System.Drawing.Pens
        //     类所返回。
        public LineCap EndCap { get; set; }
        //
        // 摘要:
        //     获取或设置用于指定复合钢笔的值数组。复合钢笔绘制由平行直线和空白区域组成的复合直线。
        //
        // 返回结果:
        //     用于指定复合数组的实数组。该数组中的元素必须按升序排列，不能小于 0，也不能大于 1。
        //
        // 异常:
        //   T:System.ArgumentException:
        //     在不可变的 System.Drawing.Pen 上设置 System.Drawing.Pen.CompoundArray 属性，如由 System.Drawing.Pens
        //     类所返回。
        public float[] CompoundArray { get; set; }
        //
        // 摘要:
        //     获取或设置此 System.Drawing.Pen 的宽度，以用于绘图的 System.Drawing.Graphics 对象为单位。
        //
        // 返回结果:
        //     此 System.Drawing.Pen 的宽度。
        //
        // 异常:
        //   T:System.ArgumentException:
        //     在不可变的 System.Drawing.Pen 上设置 System.Drawing.Pen.Width 属性，如由 System.Drawing.Pens
        //     类所返回。
        public float Width { get; set; }
        //
        // 摘要:
        //     获取或设置自定义的短划线和空白区域的数组。
        //
        // 返回结果:
        //     实数数组，指定虚线中交替出现的短划线和空白区域的长度。
        //
        // 异常:
        //   T:System.ArgumentException:
        //     在不可变的 System.Drawing.Pen 上设置 System.Drawing.Pen.DashPattern 属性，如由 System.Drawing.Pens
        //     类所返回。
        public float[] DashPattern { get; set; }
        //
        // 摘要:
        //     获取或设置在通过此 System.Drawing.Pen 绘制的直线起点使用的线帽样式。
        //
        // 返回结果:
        //     System.Drawing.Drawing2D.LineCap 值之一，表示在通过此 System.Drawing.Pen 绘制的直线起点使用的线帽样式。
        //
        // 异常:
        //   T:System.ComponentModel.InvalidEnumArgumentException:
        //     指定值不是 System.Drawing.Drawing2D.LineCap 的成员。
        //
        //   T:System.ArgumentException:
        //     在不可变的 System.Drawing.Pen 上设置 System.Drawing.Pen.StartCap 属性，如由 System.Drawing.Pens
        //     类所返回。
        public LineCap StartCap { get; set; }

        //
        // 摘要:
        //     创建此 System.Drawing.Pen 的一个精确副本。
        //
        // 返回结果:
        //     一个可以强制转换为 System.Drawing.Pen 的 System.Object。
        public object Clone()
        {
            return new StyleLine();
        }
        //
        // 摘要:
        //     释放此 System.Drawing.Pen 使用的所有资源。
        public void Dispose()
        {
        }
    }
}

