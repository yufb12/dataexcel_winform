//using Feng.Excel.Builder;
//using Feng.Excel.Collections;
//using Feng.Excel.Interfaces;
//using Feng.Script.CBEexpress;
//using Feng.Script.Method;
//using Feng.Utils;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Drawing.Imaging;
//using System.Drawing.Text;

//namespace Feng.Excel.Script
//{
//    [Serializable]
//    public class DrawFunctionContainer : DataExcelMethodContainer
//    {

//        public const string Function_Category = "DrawFunction";
//        public const string Function_Description = "绘制函数";
//        public override string Name
//        {
//            get { return Function_Category; }

//        }
//        public override string Description
//        {
//            get { return Function_Description; }
//        }

//        public DrawFunctionContainer(IMethodCollection methods) :
//            base(methods)
//        {
//            BaseMethod model = null;

//            model = new BaseMethod();
//            model.Name = "CellID";
//            model.Description = @"设置单元格ID CellID(CELL(""A5""),""NAME"")";
//            model.Eg = @"CellID(CELL(""A5""),""NAME"")";
//            model.Function = CellID;
//            MethodList.Add(model);

//        }
//        // 摘要:
//        //     获取或设置 System.Drawing.Region，该对象限定此 System.Drawing.Graphics 的绘图区域。
//        //
//        // 返回结果:
//        //     一个 System.Drawing.Region，它限定此 System.Drawing.Graphics 当前可用的绘图区域。
//        public Region Clip { get; set; }
//        //
//        // 摘要:
//        //     获取一个 System.Drawing.RectangleF 结构，该结构限定此 System.Drawing.Graphics 的剪辑区域。
//        //
//        // 返回结果:
//        //     一个 System.Drawing.RectangleF 结构，它表示此 System.Drawing.Graphics 的剪辑区域的边框。
//        public RectangleF ClipBounds { get; }
//        //
//        // 摘要:
//        //     获取一个值，该值指定如何将合成图像绘制到此 System.Drawing.Graphics。
//        //
//        // 返回结果:
//        //     此属性指定 System.Drawing.Drawing2D.CompositingMode 枚举的成员。
//        public CompositingMode CompositingMode { get; set; }
//        //
//        // 摘要:
//        //     获取或设置绘制到此 System.Drawing.Graphics 的合成图像的呈现质量。
//        //
//        // 返回结果:
//        //     此属性指定 System.Drawing.Drawing2D.CompositingQuality 枚举的成员。
//        public CompositingQuality CompositingQuality { get; set; }
//        //
//        // 摘要:
//        //     获取此 System.Drawing.Graphics 的水平分辨率。
//        //
//        // 返回结果:
//        //     此 System.Drawing.Graphics 支持的水平分辨率的值（以每英寸点数为单位）。
//        public float DpiX { get; }
//        //
//        // 摘要:
//        //     获取此 System.Drawing.Graphics 的垂直分辨率。
//        //
//        // 返回结果:
//        //     此 System.Drawing.Graphics 支持的垂直分辨率的值（以每英寸点数为单位）。
//        public float DpiY { get; }
//        //
//        // 摘要:
//        //     获取或设置与此 System.Drawing.Graphics 关联的插补模式。
//        //
//        // 返回结果:
//        //     System.Drawing.Drawing2D.InterpolationMode 值之一。
//        public InterpolationMode InterpolationMode { get; set; }
//        //
//        // 摘要:
//        //     获取一个值，该值指示此 System.Drawing.Graphics 的剪辑区域是否为空。
//        //
//        // 返回结果:
//        //     如果此 System.Drawing.Graphics 的剪辑区域为空，则为 true；否则为 false。
//        public bool IsClipEmpty { get; }
//        //
//        // 摘要:
//        //     获取一个值，该值指示此 System.Drawing.Graphics 的可见剪辑区域是否为空。
//        //
//        // 返回结果:
//        //     如果此 System.Drawing.Graphics 的剪辑区域的可见部分为空，则为 true；否则为 false。
//        public bool IsVisibleClipEmpty { get; }
//        //
//        // 摘要:
//        //     获取或设置此 System.Drawing.Graphics 的世界单位和页单位之间的比例。
//        //
//        // 返回结果:
//        //     此属性指定此 System.Drawing.Graphics 的世界单位和页单位之间的比例值。
//        public float PageScale { get; set; }
//        //
//        // 摘要:
//        //     获取或设置用于此 System.Drawing.Graphics 中的页坐标的度量单位。
//        //
//        // 返回结果:
//        //     除 System.Drawing.GraphicsUnit.World 以外的 System.Drawing.GraphicsUnit 值之一。
//        //
//        // 异常:
//        //   System.ComponentModel.InvalidEnumArgumentException:
//        //     System.Drawing.Graphics.PageUnit 设置为 System.Drawing.GraphicsUnit.World，此值不是物理单位。
//        public GraphicsUnit PageUnit { get; set; }
//        //
//        // 摘要:
//        //     获取或设置一个值，该值指定在呈现此 System.Drawing.Graphics 的过程中像素如何偏移。
//        //
//        // 返回结果:
//        //     此属性指定 System.Drawing.Drawing2D.PixelOffsetMode 枚举的成员。
//        public PixelOffsetMode PixelOffsetMode { get; set; }
//        //
//        // 摘要:
//        //     为抵色处理和阴影画笔获取或设置此 System.Drawing.Graphics 的呈现原点。
//        //
//        // 返回结果:
//        //     一个 System.Drawing.Point 结构，它表示 8 位/像素和 16 位/像素抖色处理的抖色原点，还用于设置阴影画笔的原点。
//        public Point RenderingOrigin { get; set; }
//        //
//        // 摘要:
//        //     获取或设置此 System.Drawing.Graphics 的呈现质量。
//        //
//        // 返回结果:
//        //     System.Drawing.Drawing2D.SmoothingMode 值之一。
//        public SmoothingMode SmoothingMode { get; set; }
//        //
//        // 摘要:
//        //     获取或设置呈现文本的灰度校正值。
//        //
//        // 返回结果:
//        //     用于呈现抗锯齿和 ClearType 文本的伽玛校正值。
//        public int TextContrast { get; set; }
//        //
//        // 摘要:
//        //     获取或设置与此 System.Drawing.Graphics 关联的文本的呈现模式。
//        //
//        // 返回结果:
//        //     System.Drawing.Text.TextRenderingHint 值之一。
//        public TextRenderingHint TextRenderingHint { get; set; }
//        //
//        // 摘要:
//        //     获取或设置此 System.Drawing.Graphics 的几何世界变换的副本。
//        //
//        // 返回结果:
//        //     一个 System.Drawing.Drawing2D.Matrix 副本，表示此 System.Drawing.Graphics 的几何世界变换。
//        public Matrix Transform { get; set; }
//        //
//        // 摘要:
//        //     获取此 System.Drawing.Graphics 的可见剪辑区域的边框。
//        //
//        // 返回结果:
//        //     一个 System.Drawing.RectangleF 结构，它表示此 System.Drawing.Graphics 的可见剪辑区域的边框。
//        public RectangleF VisibleClipBounds { get; }

//        // 摘要:
//        //     向当前 System.Drawing.Imaging.Metafile 添加注释。
//        //
//        // 参数:
//        //   data:
//        //     包含注释的字节的数组。
//        public void AddMetafileComment(byte[] data);
//        //
//        // 摘要:
//        //     保存具有此 System.Drawing.Graphics 的当前状态的图形容器，然后打开并使用新的图形容器。
//        //
//        // 返回结果:
//        //     此方法返回一个 System.Drawing.Drawing2D.GraphicsContainer，该对象表示该方法调用运行时此 System.Drawing.Graphics
//        //     的状态。
//        public GraphicsContainer BeginContainer();
//        //
//        // 摘要:
//        //     保存具有此 System.Drawing.Graphics 的当前状态的图形容器，然后打开并使用具有指定缩放变形的新图形容器。
//        //
//        // 参数:
//        //   dstrect:
//        //     System.Drawing.Rectangle 结构，它与 srcrect 参数一起为容器指定缩放变换。
//        //
//        //   srcrect:
//        //     System.Drawing.Rectangle 结构，它与 dstrect 参数一起为容器指定缩放变换。
//        //
//        //   unit:
//        //     System.Drawing.GraphicsUnit 枚举的成员，它指定容器的度量单位。
//        //
//        // 返回结果:
//        //     此方法返回一个 System.Drawing.Drawing2D.GraphicsContainer，该对象表示该方法调用运行时此 System.Drawing.Graphics
//        //     的状态。
//        public GraphicsContainer BeginContainer(Rectangle dstrect, Rectangle srcrect, GraphicsUnit unit);
//        //
//        // 摘要:
//        //     保存具有此 System.Drawing.Graphics 的当前状态的图形容器，然后打开并使用具有指定缩放变形的新图形容器。
//        //
//        // 参数:
//        //   dstrect:
//        //     System.Drawing.RectangleF 结构，它与 srcrect 参数一起为新的图形容器指定缩放变换。
//        //
//        //   srcrect:
//        //     System.Drawing.RectangleF 结构，它与 dstrect 参数一起为新的图形容器指定缩放变换。
//        //
//        //   unit:
//        //     System.Drawing.GraphicsUnit 枚举的成员，它指定容器的度量单位。
//        //
//        // 返回结果:
//        //     此方法返回一个 System.Drawing.Drawing2D.GraphicsContainer，该对象表示该方法调用运行时此 System.Drawing.Graphics
//        //     的状态。
//        public GraphicsContainer BeginContainer(RectangleF dstrect, RectangleF srcrect, GraphicsUnit unit);
//        //
//        // 摘要:
//        //     清除整个绘图面并以指定背景色填充。
//        //
//        // 参数:
//        //   color:
//        //     System.Drawing.Color 结构，它表示绘图面的背景色。
//        public void Clear(Color color);

//        //
//        // 摘要:
//        //     绘制一段弧线，它表示 System.Drawing.Rectangle 结构指定的椭圆的一部分。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定弧线的颜色、宽度和样式。
//        //
//        //   rect:
//        //     System.Drawing.RectangleF 结构，它定义椭圆的边界。
//        //
//        //   startAngle:
//        //     从 x 轴到弧线的起始点沿顺时针方向度量的角（以度为单位）。
//        //
//        //   sweepAngle:
//        //     从 startAngle 参数到弧线的结束点沿顺时针方向度量的角（以度为单位）。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawArc(Pen pen, Rectangle rect, float startAngle, float sweepAngle);
//        //
//        // 摘要:
//        //     绘制一段弧线，它表示 System.Drawing.RectangleF 结构指定的椭圆的一部分。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定弧线的颜色、宽度和样式。
//        //
//        //   rect:
//        //     System.Drawing.RectangleF 结构，它定义椭圆的边界。
//        //
//        //   startAngle:
//        //     从 x 轴到弧线的起始点沿顺时针方向度量的角（以度为单位）。
//        //
//        //   sweepAngle:
//        //     从 startAngle 参数到弧线的结束点沿顺时针方向度量的角（以度为单位）。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawArc(Pen pen, RectangleF rect, float startAngle, float sweepAngle);
//        //
//        // 摘要:
//        //     绘制一段弧线，它表示由一对坐标、宽度和高度指定的椭圆部分。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定弧线的颜色、宽度和样式。
//        //
//        //   x:
//        //     定义椭圆的矩形的左上角的 x 坐标。
//        //
//        //   y:
//        //     定义椭圆的矩形的左上角的 y 坐标。
//        //
//        //   width:
//        //     定义椭圆的矩形的宽度。
//        //
//        //   height:
//        //     定义椭圆的矩形的高度。
//        //
//        //   startAngle:
//        //     从 x 轴到弧线的起始点沿顺时针方向度量的角（以度为单位）。
//        //
//        //   sweepAngle:
//        //     从 startAngle 参数到弧线的结束点沿顺时针方向度量的角（以度为单位）。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawArc(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle);
//        //
//        // 摘要:
//        //     绘制一段弧线，它表示由一对坐标、宽度和高度指定的椭圆部分。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定弧线的颜色、宽度和样式。
//        //
//        //   x:
//        //     定义椭圆的矩形的左上角的 x 坐标。
//        //
//        //   y:
//        //     定义椭圆的矩形的左上角的 y 坐标。
//        //
//        //   width:
//        //     定义椭圆的矩形的宽度。
//        //
//        //   height:
//        //     定义椭圆的矩形的高度。
//        //
//        //   startAngle:
//        //     从 x 轴到弧线的起始点沿顺时针方向度量的角（以度为单位）。
//        //
//        //   sweepAngle:
//        //     从 startAngle 参数到弧线的结束点沿顺时针方向度量的角（以度为单位）。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawArc(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle);
//        //
//        // 摘要:
//        //     绘制由 4 个 System.Drawing.Point 结构定义的贝塞尔样条。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen 结构，它确定曲线的颜色、宽度和样式。
//        //
//        //   pt1:
//        //     System.Drawing.Point 结构，它表示曲线的起始点。
//        //
//        //   pt2:
//        //     System.Drawing.Point 结构，它表示曲线的第一个控制点。
//        //
//        //   pt3:
//        //     System.Drawing.Point 结构，它表示曲线的第二个控制点。
//        //
//        //   pt4:
//        //     System.Drawing.Point 结构，它表示曲线的结束点。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawBezier(Pen pen, Point pt1, Point pt2, Point pt3, Point pt4);
//        //
//        // 摘要:
//        //     绘制由 4 个 System.Drawing.PointF 结构定义的贝塞尔样条。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和样式。
//        //
//        //   pt1:
//        //     System.Drawing.PointF 结构，它表示曲线的起始点。
//        //
//        //   pt2:
//        //     System.Drawing.PointF 结构，它表示曲线的第一个控制点。
//        //
//        //   pt3:
//        //     System.Drawing.PointF 结构，它表示曲线的第二个控制点。
//        //
//        //   pt4:
//        //     System.Drawing.PointF 结构，它表示曲线的结束点。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawBezier(Pen pen, PointF pt1, PointF pt2, PointF pt3, PointF pt4);
//        //
//        // 摘要:
//        //     绘制由四个表示点的有序坐标对定义的贝塞尔样条。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和样式。
//        //
//        //   x1:
//        //     曲线起始点的 X 坐标。
//        //
//        //   y1:
//        //     曲线起始点的 Y 坐标。
//        //
//        //   x2:
//        //     曲线的第一个控制点的 X 坐标。
//        //
//        //   y2:
//        //     曲线的第一个控制点的 Y 坐标。
//        //
//        //   x3:
//        //     曲线的第二个控制点的 X 坐标。
//        //
//        //   y3:
//        //     曲线的第二个控制点的 Y 坐标。
//        //
//        //   x4:
//        //     曲线的结束点的 X 坐标。
//        //
//        //   y4:
//        //     曲线的结束点的 Y 坐标。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawBezier(Pen pen, float x1, float y1, float x2, float y2, float x3, float y3, float x4, float y4);
//        //
//        // 摘要:
//        //     用 System.Drawing.Point 结构数组绘制一系列贝塞尔样条。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和样式。
//        //
//        //   points:
//        //     System.Drawing.Point 结构的数组，这些结构表示确定曲线的点。此数组中的点数应为 3 的倍数加 1，如 4、7 或 10。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -points 为 null。
//        public void DrawBeziers(Pen pen, Point[] points);
//        //
//        // 摘要:
//        //     用 System.Drawing.PointF 结构数组绘制一系列贝塞尔样条。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和样式。
//        //
//        //   points:
//        //     System.Drawing.PointF 结构的数组，这些结构表示确定曲线的点。此数组中的点数应为 3 的倍数加 1，如 4、7 或 10。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -points 为 null。
//        public void DrawBeziers(Pen pen, PointF[] points);
//        //
//        // 摘要:
//        //     绘制由 System.Drawing.Point 结构的数组定义的闭合基数样条。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和高度。
//        //
//        //   points:
//        //     System.Drawing.Point 结构数组，这些结构定义样条。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -points 为 null。
//        public void DrawClosedCurve(Pen pen, Point[] points);
//        //
//        // 摘要:
//        //     绘制由 System.Drawing.PointF 结构的数组定义的闭合基数样条。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和高度。
//        //
//        //   points:
//        //     System.Drawing.PointF 结构数组，这些结构定义样条。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -points 为 null。
//        public void DrawClosedCurve(Pen pen, PointF[] points);
//        //
//        // 摘要:
//        //     使用指定的张力绘制由 System.Drawing.Point 结构数组定义的闭合基数样条。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和高度。
//        //
//        //   points:
//        //     System.Drawing.Point 结构数组，这些结构定义样条。
//        //
//        //   tension:
//        //     大于或等于 0.0F 的值，该值指定曲线的张力。
//        //
//        //   fillmode:
//        //     System.Drawing.Drawing2D.FillMode 枚举的成员，它确定填充曲线的方式。需要此参数但被忽略。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -points 为 null。
//        public void DrawClosedCurve(Pen pen, Point[] points, float tension, FillMode fillmode);
//        //
//        // 摘要:
//        //     使用指定的张力绘制由 System.Drawing.PointF 结构数组定义的闭合基数样条。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和高度。
//        //
//        //   points:
//        //     System.Drawing.PointF 结构数组，这些结构定义样条。
//        //
//        //   tension:
//        //     大于或等于 0.0F 的值，该值指定曲线的张力。
//        //
//        //   fillmode:
//        //     System.Drawing.Drawing2D.FillMode 枚举的成员，它确定填充曲线的方式。需要此参数但被忽略。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -points 为 null。
//        public void DrawClosedCurve(Pen pen, PointF[] points, float tension, FillMode fillmode);
//        //
//        // 摘要:
//        //     绘制经过一组指定的 System.Drawing.Point 结构的基数样条。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和高度。
//        //
//        //   points:
//        //     System.Drawing.Point 结构数组，这些结构定义样条。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -points 为 null。
//        public void DrawCurve(Pen pen, Point[] points);
//        //
//        // 摘要:
//        //     绘制经过一组指定的 System.Drawing.PointF 结构的基数样条。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和高度。
//        //
//        //   points:
//        //     System.Drawing.PointF 结构数组，这些结构定义样条。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -points 为 null。
//        public void DrawCurve(Pen pen, PointF[] points);
//        //
//        // 摘要:
//        //     使用指定的张力绘制经过一组指定的 System.Drawing.Point 结构的基数样条。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和高度。
//        //
//        //   points:
//        //     System.Drawing.Point 结构数组，这些结构定义样条。
//        //
//        //   tension:
//        //     大于或等于 0.0F 的值，该值指定曲线的张力。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -points 为 null。
//        public void DrawCurve(Pen pen, Point[] points, float tension);
//        //
//        // 摘要:
//        //     使用指定的张力绘制经过一组指定的 System.Drawing.PointF 结构的基数样条。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和高度。
//        //
//        //   points:
//        //     System.Drawing.PointF 结构的数组，这些结构表示定义曲线的点。
//        //
//        //   tension:
//        //     大于或等于 0.0F 的值，该值指定曲线的张力。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -points 为 null。
//        public void DrawCurve(Pen pen, PointF[] points, float tension);
//        //
//        // 摘要:
//        //     绘制经过一组指定的 System.Drawing.PointF 结构的基数样条。从相对于数组开始位置的偏移量开始绘制。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和高度。
//        //
//        //   points:
//        //     System.Drawing.PointF 结构数组，这些结构定义样条。
//        //
//        //   offset:
//        //     从 points 参数数组中的第一个元素到曲线中起始点的偏移量。
//        //
//        //   numberOfSegments:
//        //     起始点之后要包含在曲线中的段数。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -points 为 null。 
//        public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments);
//        //
//        // 摘要:
//        //     使用指定的张力绘制经过一组指定的 System.Drawing.Point 结构的基数样条。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和高度。
//        //
//        //   points:
//        //     System.Drawing.Point 结构数组，这些结构定义样条。
//        //
//        //   offset:
//        //     从 points 参数数组中的第一个元素到曲线中起始点的偏移量。
//        //
//        //   numberOfSegments:
//        //     起始点之后要包含在曲线中的段数。
//        //
//        //   tension:
//        //     大于或等于 0.0F 的值，该值指定曲线的张力。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -points 为 null。
//        public void DrawCurve(Pen pen, Point[] points, int offset, int numberOfSegments, float tension);
//        //
//        // 摘要:
//        //     使用指定的张力绘制经过一组指定的 System.Drawing.PointF 结构的基数样条。从相对于数组开始位置的偏移量开始绘制。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和高度。
//        //
//        //   points:
//        //     System.Drawing.PointF 结构数组，这些结构定义样条。
//        //
//        //   offset:
//        //     从 points 参数数组中的第一个元素到曲线中起始点的偏移量。
//        //
//        //   numberOfSegments:
//        //     起始点之后要包含在曲线中的段数。
//        //
//        //   tension:
//        //     大于或等于 0.0F 的值，该值指定曲线的张力。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -points 为 null。
//        public void DrawCurve(Pen pen, PointF[] points, int offset, int numberOfSegments, float tension);
//        //
//        // 摘要:
//        //     绘制边界 System.Drawing.Rectangle 结构指定的椭圆。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和样式。
//        //
//        //   rect:
//        //     System.Drawing.Rectangle 结构，它定义椭圆的边界。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawEllipse(Pen pen, Rectangle rect);
//        //
//        // 摘要:
//        //     绘制边界 System.Drawing.RectangleF 定义的椭圆。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和样式。
//        //
//        //   rect:
//        //     System.Drawing.RectangleF 结构，它定义椭圆的边界。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawEllipse(Pen pen, RectangleF rect);
//        //
//        // 摘要:
//        //     绘制一个由边框（该边框由一对坐标、高度和宽度指定）定义的椭圆。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和样式。
//        //
//        //   x:
//        //     定义椭圆的边框的左上角的 X 坐标。
//        //
//        //   y:
//        //     定义椭圆的边框的左上角的 Y 坐标。
//        //
//        //   width:
//        //     定义椭圆的边框的宽度。
//        //
//        //   height:
//        //     定义椭圆的边框的高度。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawEllipse(Pen pen, float x, float y, float width, float height);
//        //
//        // 摘要:
//        //     绘制一个由边框定义的椭圆，该边框由矩形的左上角坐标、高度和宽度指定。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定曲线的颜色、宽度和样式。
//        //
//        //   x:
//        //     定义椭圆的边框的左上角的 X 坐标。
//        //
//        //   y:
//        //     定义椭圆的边框的左上角的 Y 坐标。
//        //
//        //   width:
//        //     定义椭圆的边框的宽度。
//        //
//        //   height:
//        //     定义椭圆的边框的高度。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawEllipse(Pen pen, int x, int y, int width, int height);
//        //
//        // 摘要:
//        //     在 System.Drawing.Rectangle 结构指定的区域内绘制指定的 System.Drawing.Icon 表示的图像。
//        //
//        // 参数:
//        //   icon:
//        //     要绘制的 System.Drawing.Icon。
//        //
//        //   targetRect:
//        //     System.Drawing.Rectangle 结构，它指定显示面上结果图像的位置和大小。将 icon 参数中包含的图像缩放为此矩形区域的尺寸。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     icon 为 null。
//        public void DrawIcon(Icon icon, Rectangle targetRect);
//        //
//        // 摘要:
//        //     在指定坐标处绘制由指定的 System.Drawing.Icon 表示的图像。
//        //
//        // 参数:
//        //   icon:
//        //     要绘制的 System.Drawing.Icon。
//        //
//        //   x:
//        //     所绘制图像的左上角的 x 坐标。
//        //
//        //   y:
//        //     所绘制图像的左上角的 y 坐标。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     icon 为 null。
//        public void DrawIcon(Icon icon, int x, int y);
//        //
//        // 摘要:
//        //     绘制指定的 System.Drawing.Icon 表示的图像，而不缩放该图像。
//        //
//        // 参数:
//        //   icon:
//        //     要绘制的 System.Drawing.Icon。
//        //
//        //   targetRect:
//        //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。不缩放图像以适合此矩形的大小，但保留其原始大小。如果该图像比该矩形大，将它剪裁到适合它的大小。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     icon 为 null。
//        public void DrawIconUnstretched(Icon icon, Rectangle targetRect);
//        //
//        // 摘要:
//        //     在指定的位置使用原始物理大小绘制指定的 System.Drawing.Image。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   point:
//        //     System.Drawing.Point 结构，它表示所绘制图像的左上角的位置。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImage(Image image, Point point);
//        //
//        // 摘要:
//        //     在指定位置并且按指定形状和大小绘制指定的 System.Drawing.Image。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   destPoints:
//        //     由三个 System.Drawing.Point 结构组成的数组，这三个结构定义一个平行四边形。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImage(Image image, Point[] destPoints);
//        //
//        // 摘要:
//        //     在指定的位置使用原始物理大小绘制指定的 System.Drawing.Image。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   point:
//        //     System.Drawing.PointF 结构，它指定所绘制图像的左上角。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImage(Image image, PointF point);
//        //
//        // 摘要:
//        //     在指定位置并且按指定形状和大小绘制指定的 System.Drawing.Image。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   destPoints:
//        //     由三个 System.Drawing.PointF 结构组成的数组，这三个结构定义一个平行四边形。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImage(Image image, PointF[] destPoints);
//        //
//        // 摘要:
//        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   rect:
//        //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImage(Image image, Rectangle rect);
//        //
//        // 摘要:
//        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   rect:
//        //     System.Drawing.RectangleF 结构，它指定所绘制图像的位置和大小。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImage(Image image, RectangleF rect);
//        //
//        // 摘要:
//        //     在指定的位置使用原始物理大小绘制指定的 System.Drawing.Image。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   x:
//        //     所绘制图像的左上角的 x 坐标。
//        //
//        //   y:
//        //     所绘制图像的左上角的 y 坐标。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImage(Image image, float x, float y);
//        //
//        // 摘要:
//        //     在由坐标对指定的位置，使用图像的原始物理大小绘制指定的图像。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   x:
//        //     所绘制图像的左上角的 x 坐标。
//        //
//        //   y:
//        //     所绘制图像的左上角的 y 坐标。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImage(Image image, int x, int y);
//        //
//        // 摘要:
//        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   destPoints:
//        //     由三个 System.Drawing.Point 结构组成的数组，这三个结构定义一个平行四边形。
//        //
//        //   srcRect:
//        //     System.Drawing.Rectangle 结构，它指定 image 对象中要绘制的部分。
//        //
//        //   srcUnit:
//        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。 
//        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit);
//        //
//        // 摘要:
//        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   destPoints:
//        //     由三个 System.Drawing.PointF 结构组成的数组，这三个结构定义一个平行四边形。
//        //
//        //   srcRect:
//        //     System.Drawing.RectangleF 结构，它指定 image 对象中要绘制的部分。
//        //
//        //   srcUnit:
//        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit);
//        //
//        // 摘要:
//        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   destRect:
//        //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。将图像进行缩放以适合该矩形。
//        //
//        //   srcRect:
//        //     System.Drawing.Rectangle 结构，它指定 image 对象中要绘制的部分。
//        //
//        //   srcUnit:
//        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImage(Image image, Rectangle destRect, Rectangle srcRect, GraphicsUnit srcUnit);
//        //
//        // 摘要:
//        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   destRect:
//        //     System.Drawing.RectangleF 结构，它指定所绘制图像的位置和大小。将图像进行缩放以适合该矩形。
//        //
//        //   srcRect:
//        //     System.Drawing.RectangleF 结构，它指定 image 对象中要绘制的部分。
//        //
//        //   srcUnit:
//        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImage(Image image, RectangleF destRect, RectangleF srcRect, GraphicsUnit srcUnit);
//        //
//        // 摘要:
//        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   x:
//        //     所绘制图像的左上角的 x 坐标。
//        //
//        //   y:
//        //     所绘制图像的左上角的 y 坐标。
//        //
//        //   width:
//        //     所绘制图像的宽度。
//        //
//        //   height:
//        //     所绘制图像的高度。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImage(Image image, float x, float y, float width, float height);
//        //
//        // 摘要:
//        //     在指定的位置绘制图像的一部分。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   x:
//        //     所绘制图像的左上角的 x 坐标。
//        //
//        //   y:
//        //     所绘制图像的左上角的 y 坐标。
//        //
//        //   srcRect:
//        //     System.Drawing.RectangleF 结构，它指定 System.Drawing.Image 中要绘制的部分。
//        //
//        //   srcUnit:
//        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImage(Image image, float x, float y, RectangleF srcRect, GraphicsUnit srcUnit);
//        //
//        // 摘要:
//        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   x:
//        //     所绘制图像的左上角的 x 坐标。
//        //
//        //   y:
//        //     所绘制图像的左上角的 y 坐标。
//        //
//        //   width:
//        //     所绘制图像的宽度。
//        //
//        //   height:
//        //     所绘制图像的高度。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImage(Image image, int x, int y, int width, int height);
//        //
//        // 摘要:
//        //     在指定的位置绘制图像的一部分。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   x:
//        //     所绘制图像的左上角的 x 坐标。
//        //
//        //   y:
//        //     所绘制图像的左上角的 y 坐标。
//        //
//        //   srcRect:
//        //     System.Drawing.Rectangle 结构，它指定 image 对象中要绘制的部分。
//        //
//        //   srcUnit:
//        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImage(Image image, int x, int y, Rectangle srcRect, GraphicsUnit srcUnit);
//        //
//        // 摘要:
//        //     在指定位置绘制指定的 System.Drawing.Image 的指定部分。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   destPoints:
//        //     由三个 System.Drawing.Point 结构组成的数组，这三个结构定义一个平行四边形。
//        //
//        //   srcRect:
//        //     System.Drawing.Rectangle 结构，它指定 image 对象中要绘制的部分。
//        //
//        //   srcUnit:
//        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
//        //
//        //   imageAttr:
//        //     System.Drawing.Imaging.ImageAttributes，它指定 image 对象的重新着色和伽玛信息。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。 
//        public void DrawImage(Image image, Point[] destPoints, Rectangle srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr);
//        //
//        // 摘要:
//        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   destPoints:
//        //     由三个 System.Drawing.PointF 结构组成的数组，这三个结构定义一个平行四边形。
//        //
//        //   srcRect:
//        //     System.Drawing.RectangleF 结构，它指定 image 对象中要绘制的部分。
//        //
//        //   srcUnit:
//        //     System.Drawing.GraphicsUnit 枚举的成员，它指定 srcRect 参数所用的度量单位。
//        //
//        //   imageAttr:
//        //     System.Drawing.Imaging.ImageAttributes，它指定 image 对象的重新着色和伽玛信息。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。 
//        public void DrawImage(Image image, PointF[] destPoints, RectangleF srcRect, GraphicsUnit srcUnit, ImageAttributes imageAttr);
//        //
//        // 摘要:
//        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   destRect:
//        //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。将图像进行缩放以适合该矩形。
//        //
//        //   srcX:
//        //     要绘制的源图像部分的左上角的 x 坐标。
//        //
//        //   srcY:
//        //     要绘制的源图像部分的左上角的 y 坐标。
//        //
//        //   srcWidth:
//        //     要绘制的源图像部分的宽度。
//        //
//        //   srcHeight:
//        //     要绘制的源图像部分的高度。
//        //
//        //   srcUnit:
//        //     System.Drawing.GraphicsUnit 枚举的成员，它指定用于确定源矩形的度量单位。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。 
//        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit);
//        //
//        // 摘要:
//        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   destRect:
//        //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。将图像进行缩放以适合该矩形。
//        //
//        //   srcX:
//        //     要绘制的源图像部分的左上角的 x 坐标。
//        //
//        //   srcY:
//        //     要绘制的源图像部分的左上角的 y 坐标。
//        //
//        //   srcWidth:
//        //     要绘制的源图像部分的宽度。
//        //
//        //   srcHeight:
//        //     要绘制的源图像部分的高度。
//        //
//        //   srcUnit:
//        //     System.Drawing.GraphicsUnit 枚举的成员，它指定用于确定源矩形的度量单位。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。 
//        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit);
//        //
//        // 摘要:
//        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   destRect:
//        //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。将图像进行缩放以适合该矩形。
//        //
//        //   srcX:
//        //     要绘制的源图像部分的左上角的 x 坐标。
//        //
//        //   srcY:
//        //     要绘制的源图像部分的左上角的 y 坐标。
//        //
//        //   srcWidth:
//        //     要绘制的源图像部分的宽度。
//        //
//        //   srcHeight:
//        //     要绘制的源图像部分的高度。
//        //
//        //   srcUnit:
//        //     System.Drawing.GraphicsUnit 枚举的成员，它指定用于确定源矩形的度量单位。
//        //
//        //   imageAttrs:
//        //     System.Drawing.Imaging.ImageAttributes，它指定 image 对象的重新着色和伽玛信息。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。 
//        public void DrawImage(Image image, Rectangle destRect, float srcX, float srcY, float srcWidth, float srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttrs);
//        //
//        // 摘要:
//        //     在指定位置并且按指定大小绘制指定的 System.Drawing.Image 的指定部分。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   destRect:
//        //     System.Drawing.Rectangle 结构，它指定所绘制图像的位置和大小。将图像进行缩放以适合该矩形。
//        //
//        //   srcX:
//        //     要绘制的源图像部分的左上角的 x 坐标。
//        //
//        //   srcY:
//        //     要绘制的源图像部分的左上角的 y 坐标。
//        //
//        //   srcWidth:
//        //     要绘制的源图像部分的宽度。
//        //
//        //   srcHeight:
//        //     要绘制的源图像部分的高度。
//        //
//        //   srcUnit:
//        //     System.Drawing.GraphicsUnit 枚举的成员，它指定用于确定源矩形的度量单位。
//        //
//        //   imageAttr:
//        //     System.Drawing.Imaging.ImageAttributes，它指定 image 对象的重新着色和伽玛信息。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。 
//        public void DrawImage(Image image, Rectangle destRect, int srcX, int srcY, int srcWidth, int srcHeight, GraphicsUnit srcUnit, ImageAttributes imageAttr);
//        //
//        // 摘要:
//        //     在指定的位置使用图像的原始物理大小绘制指定的图像。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   point:
//        //     System.Drawing.Point 结构，它指定所绘制图像的左上角。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImageUnscaled(Image image, Point point);
//        //
//        // 摘要:
//        //     在指定的位置使用图像的原始物理大小绘制指定的图像。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   rect:
//        //     System.Drawing.Rectangle，它指定了所绘制图像的左上角。该矩形的 X 和 Y 属性指定左上角。宽度和高度属性被忽略。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImageUnscaled(Image image, Rectangle rect);
//        //
//        // 摘要:
//        //     在由坐标对指定的位置，使用图像的原始物理大小绘制指定的图像。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   x:
//        //     所绘制图像的左上角的 x 坐标。
//        //
//        //   y:
//        //     所绘制图像的左上角的 y 坐标。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。 
//        public void DrawImageUnscaled(Image image, int x, int y);
//        //
//        // 摘要:
//        //     在指定的位置使用图像的原始物理大小绘制指定的图像。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   x:
//        //     所绘制图像的左上角的 x 坐标。
//        //
//        //   y:
//        //     所绘制图像的左上角的 y 坐标。
//        //
//        //   width:
//        //     未使用。
//        //
//        //   height:
//        //     未使用。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。 
//        public void DrawImageUnscaled(Image image, int x, int y, int width, int height);
//        //
//        // 摘要:
//        //     在不进行缩放的情况下绘制指定的图像，并在需要时剪辑该图像以适合指定的矩形。
//        //
//        // 参数:
//        //   image:
//        //     要绘制的 System.Drawing.Image。
//        //
//        //   rect:
//        //     要在其中绘制图像的 System.Drawing.Rectangle。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        public void DrawImageUnscaledAndClipped(Image image, Rectangle rect);
//        //
//        // 摘要:
//        //     绘制一条连接两个 System.Drawing.Point 结构的线。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定线条的颜色、宽度和样式。
//        //
//        //   pt1:
//        //     System.Drawing.Point 结构，它表示要连接的第一个点。
//        //
//        //   pt2:
//        //     System.Drawing.Point 结构，它表示要连接的第二个点。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawLine(Pen pen, Point pt1, Point pt2);
//        //
//        // 摘要:
//        //     绘制一条连接两个 System.Drawing.PointF 结构的线。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定线条的颜色、宽度和样式。
//        //
//        //   pt1:
//        //     System.Drawing.PointF 结构，它表示要连接的第一个点。
//        //
//        //   pt2:
//        //     System.Drawing.PointF 结构，它表示要连接的第二个点。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawLine(Pen pen, PointF pt1, PointF pt2);
//        //
//        // 摘要:
//        //     绘制一条连接由坐标对指定的两个点的线条。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定线条的颜色、宽度和样式。
//        //
//        //   x1:
//        //     第一个点的 x 坐标。
//        //
//        //   y1:
//        //     第一个点的 y 坐标。
//        //
//        //   x2:
//        //     第二个点的 x 坐标。
//        //
//        //   y2:
//        //     第二个点的 y 坐标。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawLine(Pen pen, float x1, float y1, float x2, float y2);
//        //
//        // 摘要:
//        //     绘制一条连接由坐标对指定的两个点的线条。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定线条的颜色、宽度和样式。
//        //
//        //   x1:
//        //     第一个点的 x 坐标。
//        //
//        //   y1:
//        //     第一个点的 y 坐标。
//        //
//        //   x2:
//        //     第二个点的 x 坐标。
//        //
//        //   y2:
//        //     第二个点的 y 坐标。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawLine(Pen pen, int x1, int y1, int x2, int y2);
//        //
//        // 摘要:
//        //     绘制一系列连接一组 System.Drawing.Point 结构的线段。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定线段的颜色、宽度和样式。
//        //
//        //   points:
//        //     System.Drawing.Point 结构数组，这些结构表示要连接的点。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -points 为 null。
//        public void DrawLines(Pen pen, Point[] points);
//        //
//        // 摘要:
//        //     绘制一系列连接一组 System.Drawing.PointF 结构的线段。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定线段的颜色、宽度和样式。
//        //
//        //   points:
//        //     System.Drawing.PointF 结构数组，这些结构表示要连接的点。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -points 为 null。
//        public void DrawLines(Pen pen, PointF[] points);
//        //
//        // 摘要:
//        //     绘制 System.Drawing.Drawing2D.GraphicsPath。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定路径的颜色、宽度和样式。
//        //
//        //   path:
//        //     要绘制的 System.Drawing.Drawing2D.GraphicsPath。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -path 为 null。
//        public void DrawPath(Pen pen, GraphicsPath path);
//        //
//        // 摘要:
//        //     绘制由一个 System.Drawing.Rectangle 结构和两条射线所指定的椭圆定义的扇形。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定扇形的颜色、宽度和样式。
//        //
//        //   rect:
//        //     System.Drawing.Rectangle 结构，它表示定义该扇形所属的椭圆的边框。
//        //
//        //   startAngle:
//        //     从 x 轴到扇形的第一条边沿顺时针方向度量的角（以度为单位）。
//        //
//        //   sweepAngle:
//        //     从 startAngle 参数到扇形的第二条边沿顺时针方向度量的角（以度为单位）。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawPie(Pen pen, Rectangle rect, float startAngle, float sweepAngle);
//        //
//        // 摘要:
//        //     绘制由一个 System.Drawing.RectangleF 结构和两条射线所指定的椭圆定义的扇形。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定扇形的颜色、宽度和样式。
//        //
//        //   rect:
//        //     System.Drawing.RectangleF 结构，它表示定义该扇形所属的椭圆的边框。
//        //
//        //   startAngle:
//        //     从 x 轴到扇形的第一条边沿顺时针方向度量的角（以度为单位）。
//        //
//        //   sweepAngle:
//        //     从 startAngle 参数到扇形的第二条边沿顺时针方向度量的角（以度为单位）。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawPie(Pen pen, RectangleF rect, float startAngle, float sweepAngle);
//        //
//        // 摘要:
//        //     绘制一个扇形，该形状由一个坐标对、宽度、高度以及两条射线所指定的椭圆定义。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定扇形的颜色、宽度和样式。
//        //
//        //   x:
//        //     边框的左上角的 x 坐标，该边框定义扇形所属的椭圆。
//        //
//        //   y:
//        //     边框的左上角的 y 坐标，该边框定义扇形所属的椭圆。
//        //
//        //   width:
//        //     边框的宽度，该边框定义扇形所属的椭圆。
//        //
//        //   height:
//        //     边框的高度，该边框定义扇形所属的椭圆。
//        //
//        //   startAngle:
//        //     从 x 轴到扇形的第一条边沿顺时针方向度量的角（以度为单位）。
//        //
//        //   sweepAngle:
//        //     从 startAngle 参数到扇形的第二条边沿顺时针方向度量的角（以度为单位）。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawPie(Pen pen, float x, float y, float width, float height, float startAngle, float sweepAngle);
//        //
//        // 摘要:
//        //     绘制一个扇形，该形状由一个坐标对、宽度、高度以及两条射线所指定的椭圆定义。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定扇形的颜色、宽度和样式。
//        //
//        //   x:
//        //     边框的左上角的 x 坐标，该边框定义扇形所属的椭圆。
//        //
//        //   y:
//        //     边框的左上角的 y 坐标，该边框定义扇形所属的椭圆。
//        //
//        //   width:
//        //     边框的宽度，该边框定义扇形所属的椭圆。
//        //
//        //   height:
//        //     边框的高度，该边框定义扇形所属的椭圆。
//        //
//        //   startAngle:
//        //     从 x 轴到扇形的第一条边沿顺时针方向度量的角（以度为单位）。
//        //
//        //   sweepAngle:
//        //     从 startAngle 参数到扇形的第二条边沿顺时针方向度量的角（以度为单位）。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawPie(Pen pen, int x, int y, int width, int height, int startAngle, int sweepAngle);
//        //
//        // 摘要:
//        //     绘制由一组 System.Drawing.Point 结构定义的多边形。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定多边形的颜色、宽度和样式。
//        //
//        //   points:
//        //     System.Drawing.Point 结构数组，这些结构表示多边形的顶点。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawPolygon(Pen pen, Point[] points);
//        //
//        // 摘要:
//        //     绘制由一组 System.Drawing.PointF 结构定义的多边形。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定多边形的颜色、宽度和样式。
//        //
//        //   points:
//        //     System.Drawing.PointF 结构数组，这些结构表示多边形的顶点。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -points 为 null。
//        public void DrawPolygon(Pen pen, PointF[] points);
//        //
//        // 摘要:
//        //     绘制由 System.Drawing.Rectangle 结构指定的矩形。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定矩形的颜色、宽度和样式。
//        //
//        //   rect:
//        //     表示要绘制的矩形的 System.Drawing.Rectangle 结构。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawRectangle(Pen pen, Rectangle rect);
//        //
//        // 摘要:
//        //     绘制由坐标对、宽度和高度指定的矩形。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定矩形的颜色、宽度和样式。
//        //
//        //   x:
//        //     要绘制的矩形的左上角的 x 坐标。
//        //
//        //   y:
//        //     要绘制的矩形的左上角的 y 坐标。
//        //
//        //   width:
//        //     要绘制的矩形的宽度。
//        //
//        //   height:
//        //     要绘制的矩形的高度。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawRectangle(Pen pen, float x, float y, float width, float height);
//        //
//        // 摘要:
//        //     绘制由坐标对、宽度和高度指定的矩形。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定矩形的颜色、宽度和样式。
//        //
//        //   x:
//        //     要绘制的矩形的左上角的 x 坐标。
//        //
//        //   y:
//        //     要绘制的矩形的左上角的 y 坐标。
//        //
//        //   width:
//        //     要绘制的矩形的宽度。
//        //
//        //   height:
//        //     要绘制的矩形的高度。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。
//        public void DrawRectangle(Pen pen, int x, int y, int width, int height);
//        //
//        // 摘要:
//        //     绘制一系列由 System.Drawing.Rectangle 结构指定的矩形。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定矩形轮廓线的颜色、宽度和样式。
//        //
//        //   rects:
//        //     System.Drawing.Rectangle 结构数组，这些结构表示要绘制的矩形。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -rects 为 null。
//        public void DrawRectangles(Pen pen, Rectangle[] rects);
//        //
//        // 摘要:
//        //     绘制一系列由 System.Drawing.RectangleF 结构指定的矩形。
//        //
//        // 参数:
//        //   pen:
//        //     System.Drawing.Pen，它确定矩形轮廓线的颜色、宽度和样式。
//        //
//        //   rects:
//        //     System.Drawing.RectangleF 结构数组，这些结构表示要绘制的矩形。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -rects 为 null。
//        public void DrawRectangles(Pen pen, RectangleF[] rects);
//        //
//        // 摘要:
//        //     在指定位置并且用指定的 System.Drawing.Brush 和 System.Drawing.Font 对象绘制指定的文本字符串。
//        //
//        // 参数:
//        //   s:
//        //     要绘制的字符串。
//        //
//        //   font:
//        //     System.Drawing.Font，它定义字符串的文本格式。
//        //
//        //   brush:
//        //     System.Drawing.Brush，它确定所绘制文本的颜色和纹理。
//        //
//        //   point:
//        //     System.Drawing.PointF 结构，它指定所绘制文本的左上角。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。- 或 -s 为 null。
//        public void DrawString(string s, Font font, Brush brush, PointF point);
//        //
//        // 摘要:
//        //     在指定矩形并且用指定的 System.Drawing.Brush 和 System.Drawing.Font 对象绘制指定的文本字符串。
//        //
//        // 参数:
//        //   s:
//        //     要绘制的字符串。
//        //
//        //   font:
//        //     System.Drawing.Font，它定义字符串的文本格式。
//        //
//        //   brush:
//        //     System.Drawing.Brush，它确定所绘制文本的颜色和纹理。
//        //
//        //   layoutRectangle:
//        //     System.Drawing.RectangleF 结构，它指定所绘制文本的位置。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。- 或 -s 为 null。 
//        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle);
//        //
//        // 摘要:
//        //     在指定位置并且用指定的 System.Drawing.Brush 和 System.Drawing.Font 对象绘制指定的文本字符串。
//        //
//        // 参数:
//        //   s:
//        //     要绘制的字符串。
//        //
//        //   font:
//        //     System.Drawing.Font，它定义字符串的文本格式。
//        //
//        //   brush:
//        //     System.Drawing.Brush，它确定所绘制文本的颜色和纹理。
//        //
//        //   x:
//        //     所绘制文本的左上角的 x 坐标。
//        //
//        //   y:
//        //     所绘制文本的左上角的 y 坐标。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。- 或 -s 为 null。
//        public void DrawString(string s, Font font, Brush brush, float x, float y);
//        //
//        // 摘要:
//        //     使用指定 System.Drawing.StringFormat 的格式化属性，用指定的 System.Drawing.Brush 和 System.Drawing.Font
//        //     对象在指定的位置绘制指定的文本字符串。
//        //
//        // 参数:
//        //   s:
//        //     要绘制的字符串。
//        //
//        //   font:
//        //     System.Drawing.Font，它定义字符串的文本格式。
//        //
//        //   brush:
//        //     System.Drawing.Brush，它确定所绘制文本的颜色和纹理。
//        //
//        //   point:
//        //     System.Drawing.PointF 结构，它指定所绘制文本的左上角。
//        //
//        //   format:
//        //     System.Drawing.StringFormat，它指定应用于所绘制文本的格式化属性（如行距和对齐方式）。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。- 或 -s 为 null。
//        public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format);
//        //
//        // 摘要:
//        //     使用指定 System.Drawing.StringFormat 的格式化属性，用指定的 System.Drawing.Brush 和 System.Drawing.Font
//        //     对象在指定的矩形绘制指定的文本字符串。
//        //
//        // 参数:
//        //   s:
//        //     要绘制的字符串。
//        //
//        //   font:
//        //     System.Drawing.Font，它定义字符串的文本格式。
//        //
//        //   brush:
//        //     System.Drawing.Brush，它确定所绘制文本的颜色和纹理。
//        //
//        //   layoutRectangle:
//        //     System.Drawing.RectangleF 结构，它指定所绘制文本的位置。
//        //
//        //   format:
//        //     System.Drawing.StringFormat，它指定应用于所绘制文本的格式化属性（如行距和对齐方式）。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。- 或 -s 为 null。
//        public void DrawString(string s, Font font, Brush brush, RectangleF layoutRectangle, StringFormat format);
//        //
//        // 摘要:
//        //     使用指定 System.Drawing.StringFormat 的格式化属性，用指定的 System.Drawing.Brush 和 System.Drawing.Font
//        //     对象在指定的位置绘制指定的文本字符串。
//        //
//        // 参数:
//        //   s:
//        //     要绘制的字符串。
//        //
//        //   font:
//        //     System.Drawing.Font，它定义字符串的文本格式。
//        //
//        //   brush:
//        //     System.Drawing.Brush，它确定所绘制文本的颜色和纹理。
//        //
//        //   x:
//        //     所绘制文本的左上角的 x 坐标。
//        //
//        //   y:
//        //     所绘制文本的左上角的 y 坐标。
//        //
//        //   format:
//        //     System.Drawing.StringFormat，它指定应用于所绘制文本的格式化属性（如行距和对齐方式）。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。- 或 -s 为 null。
//        public void DrawString(string s, Font font, Brush brush, float x, float y, StringFormat format);
//        //
//        // 摘要:
//        //     关闭当前图形容器，并将此 System.Drawing.Graphics 的状态还原到通过调用 System.Drawing.Graphics.BeginContainer()
//        //     方法保存的状态。
//        //
//        // 参数:
//        //   container:
//        //     System.Drawing.Drawing2D.GraphicsContainer，它表示此方法还原的容器。
//        public void EndContainer(GraphicsContainer container);
//        //
//        // 摘要:
//        //     更新此 System.Drawing.Graphics 的剪辑区域，以排除 System.Drawing.Rectangle 结构所指定的区域。
//        //
//        // 参数:
//        //   rect:
//        //     System.Drawing.Rectangle 结构，它指定要从剪辑区域排除的矩形。
//        public void ExcludeClip(Rectangle rect);
//        //
//        // 摘要:
//        //     更新此 System.Drawing.Graphics 的剪辑区域，以排除 System.Drawing.Region 所指定的区域。
//        //
//        // 参数:
//        //   region:
//        //     System.Drawing.Region，它指定要从剪辑区域排除的区域。
//        public void ExcludeClip(Region region);
//        //
//        // 摘要:
//        //     填充由 System.Drawing.Point 结构数组定义的闭合基数样条曲线的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   points:
//        //     System.Drawing.Point 结构数组，这些结构定义样条。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。- 或 -points 为 null。
//        public void FillClosedCurve(Brush brush, Point[] points);
//        //
//        // 摘要:
//        //     填充由 System.Drawing.PointF 结构数组定义的闭合基数样条曲线的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   points:
//        //     System.Drawing.PointF 结构数组，这些结构定义样条。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。- 或 -points 为 null。
//        public void FillClosedCurve(Brush brush, PointF[] points);
//        //
//        // 摘要:
//        //     使用指定的填充模式填充 System.Drawing.Point 结构数组定义的闭合基数样条曲线的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   points:
//        //     System.Drawing.Point 结构数组，这些结构定义样条。
//        //
//        //   fillmode:
//        //     System.Drawing.Drawing2D.FillMode 枚举的成员，它确定填充曲线的方式。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。- 或 -points 为 null。 
//        public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode);
//        //
//        // 摘要:
//        //     使用指定的填充模式填充 System.Drawing.PointF 结构数组定义的闭合基数样条曲线的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   points:
//        //     System.Drawing.PointF 结构数组，这些结构定义样条。
//        //
//        //   fillmode:
//        //     System.Drawing.Drawing2D.FillMode 枚举的成员，它确定填充曲线的方式。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。- 或 -points 为 null。 
//        public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode);
//        //
//        // 摘要:
//        //     使用指定的填充模式和张力填充 System.Drawing.Point 结构数组定义的闭合基数样条曲线的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   points:
//        //     System.Drawing.Point 结构数组，这些结构定义样条。
//        //
//        //   fillmode:
//        //     System.Drawing.Drawing2D.FillMode 枚举的成员，它确定填充曲线的方式。
//        //
//        //   tension:
//        //     大于或等于 0.0F 的值，该值指定曲线的张力。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。- 或 -points 为 null。
//        public void FillClosedCurve(Brush brush, Point[] points, FillMode fillmode, float tension);
//        //
//        // 摘要:
//        //     使用指定的填充模式和张力填充 System.Drawing.PointF 结构数组定义的闭合基数样条曲线的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   points:
//        //     System.Drawing.PointF 结构数组，这些结构定义样条。
//        //
//        //   fillmode:
//        //     System.Drawing.Drawing2D.FillMode 枚举的成员，它确定填充曲线的方式。
//        //
//        //   tension:
//        //     大于或等于 0.0F 的值，该值指定曲线的张力。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。- 或 -points 为 null。
//        public void FillClosedCurve(Brush brush, PointF[] points, FillMode fillmode, float tension);
//        //
//        // 摘要:
//        //     填充 System.Drawing.Rectangle 结构指定的边框所定义的椭圆的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   rect:
//        //     System.Drawing.Rectangle 结构，它表示定义椭圆的边框。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。
//        public void FillEllipse(Brush brush, Rectangle rect);
//        //
//        // 摘要:
//        //     填充 System.Drawing.RectangleF 结构指定的边框所定义的椭圆的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   rect:
//        //     System.Drawing.RectangleF 结构，它表示定义椭圆的边框。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。
//        public void FillEllipse(Brush brush, RectangleF rect);
//        //
//        // 摘要:
//        //     填充边框所定义的椭圆的内部，该边框由一对坐标、一个宽度和一个高度指定。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   x:
//        //     定义椭圆的边框的左上角的 X 坐标。
//        //
//        //   y:
//        //     定义椭圆的边框的左上角的 Y 坐标。
//        //
//        //   width:
//        //     定义椭圆的边框的宽度。
//        //
//        //   height:
//        //     定义椭圆的边框的高度。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。
//        public void FillEllipse(Brush brush, float x, float y, float width, float height);
//        //
//        // 摘要:
//        //     填充边框所定义的椭圆的内部，该边框由一对坐标、一个宽度和一个高度指定。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   x:
//        //     定义椭圆的边框的左上角的 X 坐标。
//        //
//        //   y:
//        //     定义椭圆的边框的左上角的 Y 坐标。
//        //
//        //   width:
//        //     定义椭圆的边框的宽度。
//        //
//        //   height:
//        //     定义椭圆的边框的高度。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。
//        public void FillEllipse(Brush brush, int x, int y, int width, int height);
//        //
//        // 摘要:
//        //     填充 System.Drawing.Drawing2D.GraphicsPath 的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   path:
//        //     System.Drawing.Drawing2D.GraphicsPath，它表示要填充的路径。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     pen 为 null。- 或 -path 为 null。
//        public void FillPath(Brush brush, GraphicsPath path);
//        //
//        // 摘要:
//        //     填充椭圆所定义的扇形区的内部，该椭圆由 System.Drawing.RectangleF 结构和两条射线指定。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   rect:
//        //     System.Drawing.Rectangle 结构，它表示定义该扇形区所属的椭圆的边框。
//        //
//        //   startAngle:
//        //     从 x 轴沿顺时针方向旋转到扇形区第一个边所测得的角度（以度为单位）。
//        //
//        //   sweepAngle:
//        //     从 startAngle 参数沿顺时针方向旋转到扇形区第二个边所测得的角度（以度为单位）。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。
//        public void FillPie(Brush brush, Rectangle rect, float startAngle, float sweepAngle);
//        //
//        // 摘要:
//        //     填充由一对坐标、一个宽度、一个高度以及两条射线指定的椭圆所定义的扇形区的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   x:
//        //     边框左上角的 x 坐标，该边框定义扇形区所属的椭圆。
//        //
//        //   y:
//        //     边框左上角的 y 坐标，该边框定义扇形区所属的椭圆。
//        //
//        //   width:
//        //     边框的宽度，该边框定义扇形区所属的椭圆。
//        //
//        //   height:
//        //     边框的高度，该边框定义扇形区所属的椭圆。
//        //
//        //   startAngle:
//        //     从 x 轴沿顺时针方向旋转到扇形区第一个边所测得的角度（以度为单位）。
//        //
//        //   sweepAngle:
//        //     从 startAngle 参数沿顺时针方向旋转到扇形区第二个边所测得的角度（以度为单位）。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。
//        public void FillPie(Brush brush, float x, float y, float width, float height, float startAngle, float sweepAngle);
//        //
//        // 摘要:
//        //     填充由一对坐标、一个宽度、一个高度以及两条射线指定的椭圆所定义的扇形区的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   x:
//        //     边框左上角的 x 坐标，该边框定义扇形区所属的椭圆。
//        //
//        //   y:
//        //     边框左上角的 y 坐标，该边框定义扇形区所属的椭圆。
//        //
//        //   width:
//        //     边框的宽度，该边框定义扇形区所属的椭圆。
//        //
//        //   height:
//        //     边框的高度，该边框定义扇形区所属的椭圆。
//        //
//        //   startAngle:
//        //     从 x 轴沿顺时针方向旋转到扇形区第一个边所测得的角度（以度为单位）。
//        //
//        //   sweepAngle:
//        //     从 startAngle 参数沿顺时针方向旋转到扇形区第二个边所测得的角度（以度为单位）。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。
//        public void FillPie(Brush brush, int x, int y, int width, int height, int startAngle, int sweepAngle);
//        //
//        // 摘要:
//        //     填充 System.Drawing.Point 结构指定的点数组所定义的多边形的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   points:
//        //     System.Drawing.Point 结构数组，这些结构表示要填充的多边形的顶点。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。- 或 -points 为 null。 
//        public void FillPolygon(Brush brush, Point[] points);
//        //
//        // 摘要:
//        //     填充 System.Drawing.PointF 结构指定的点数组所定义的多边形的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   points:
//        //     System.Drawing.PointF 结构数组，这些结构表示要填充的多边形的顶点。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。- 或 -points 为 null。 
//        public void FillPolygon(Brush brush, PointF[] points);
//        //
//        // 摘要:
//        //     使用指定的填充模式填充 System.Drawing.Point 结构指定的点数组所定义的多边形的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   points:
//        //     System.Drawing.Point 结构数组，这些结构表示要填充的多边形的顶点。
//        //
//        //   fillMode:
//        //     确定填充样式的 System.Drawing.Drawing2D.FillMode 枚举的成员。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。- 或 -points 为 null。
//        public void FillPolygon(Brush brush, Point[] points, FillMode fillMode);
//        //
//        // 摘要:
//        //     使用指定的填充模式填充 System.Drawing.PointF 结构指定的点数组所定义的多边形的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   points:
//        //     System.Drawing.PointF 结构数组，这些结构表示要填充的多边形的顶点。
//        //
//        //   fillMode:
//        //     确定填充样式的 System.Drawing.Drawing2D.FillMode 枚举的成员。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。- 或 -points 为 null。
//        public void FillPolygon(Brush brush, PointF[] points, FillMode fillMode);
//        //
//        // 摘要:
//        //     填充 System.Drawing.Rectangle 结构指定的矩形的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   rect:
//        //     System.Drawing.Rectangle 结构，它表示要填充的矩形。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。
//        public void FillRectangle(Brush brush, Rectangle rect);
//        //
//        // 摘要:
//        //     填充 System.Drawing.RectangleF 结构指定的矩形的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   rect:
//        //     System.Drawing.RectangleF 结构，它表示要填充的矩形。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。
//        public void FillRectangle(Brush brush, RectangleF rect);
//        //
//        // 摘要:
//        //     填充由一对坐标、一个宽度和一个高度指定的矩形的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   x:
//        //     要填充的矩形的左上角的 x 坐标。
//        //
//        //   y:
//        //     要填充的矩形的左上角的 y 坐标。
//        //
//        //   width:
//        //     要填充的矩形的宽度。
//        //
//        //   height:
//        //     要填充的矩形的高度。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。
//        public void FillRectangle(Brush brush, float x, float y, float width, float height);
//        //
//        // 摘要:
//        //     填充由一对坐标、一个宽度和一个高度指定的矩形的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   x:
//        //     要填充的矩形的左上角的 x 坐标。
//        //
//        //   y:
//        //     要填充的矩形的左上角的 y 坐标。
//        //
//        //   width:
//        //     要填充的矩形的宽度。
//        //
//        //   height:
//        //     要填充的矩形的高度。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。
//        public void FillRectangle(Brush brush, int x, int y, int width, int height);
//        //
//        // 摘要:
//        //     填充由 System.Drawing.Rectangle 结构指定的一系列矩形的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   rects:
//        //     System.Drawing.Rectangle 结构数组，这些结构表示要填充的矩形。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。
//        public void FillRectangles(Brush brush, Rectangle[] rects);
//        //
//        // 摘要:
//        //     填充由 System.Drawing.RectangleF 结构指定的一系列矩形的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   rects:
//        //     System.Drawing.RectangleF 结构数组，这些结构表示要填充的矩形。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。
//        public void FillRectangles(Brush brush, RectangleF[] rects);
//        //
//        // 摘要:
//        //     填充 System.Drawing.Region 的内部。
//        //
//        // 参数:
//        //   brush:
//        //     确定填充特性的 System.Drawing.Brush。
//        //
//        //   region:
//        //     System.Drawing.Region，它表示要填充的区域。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     brush 为 null。- 或 -region 为 null。
//        public void FillRegion(Brush brush, Region region);
//        //
//        // 摘要:
//        //     强制执行所有挂起的图形操作并立即返回而不等待操作完成。 
//        public void Flush();
//        //
//        // 摘要:
//        //     用此方法强制执行所有挂起的图形操作，按照指定，等待或者不等待，在操作完成之前返回。
//        //
//        // 参数:
//        //   intention:
//        //     System.Drawing.Drawing2D.FlushIntention 枚举的成员，它指定该方法是立即返回还是等待所有现有的操作都完成。
//        public void Flush(FlushIntention intention);
//        //
//        // 摘要:
//        //     从指定的 System.Drawing.Image 创建新的 System.Drawing.Graphics。
//        //
//        // 参数:
//        //   image:
//        //     从中创建新 System.Drawing.Graphics 的 System.Drawing.Image。
//        //
//        // 返回结果:
//        //     此方法为指定的 System.Drawing.Image 返回一个新的 System.Drawing.Graphics。
//        //
//        // 异常:
//        //   System.ArgumentNullException:
//        //     image 为 null。
//        //
//        //   System.Exception:
//        //     image 具有索引像素格式，或者格式未定义。
//        public static Graphics FromImage(Image image);
//        //
//        // 摘要:
//        //     获取与指定的 System.Drawing.Color 结构最接近的颜色。
//        //
//        // 参数:
//        //   color:
//        //     System.Drawing.Color 结构，为其查找匹配项。
//        //
//        // 返回结果:
//        //     一个 System.Drawing.Color 结构，它表示与 color 参数指定的颜色最接近的颜色。
//        public Color GetNearestColor(Color color);
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 的剪辑区域更新为当前剪辑区域与指定 System.Drawing.Rectangle 结构的交集。
//        //
//        // 参数:
//        //   rect:
//        //     与当前剪辑区域相交的 System.Drawing.Rectangle 结构。
//        public void IntersectClip(Rectangle rect);
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 的剪辑区域更新为当前剪辑区域与指定 System.Drawing.RectangleF 结构的交集。
//        //
//        // 参数:
//        //   rect:
//        //     与当前剪辑区域相交的 System.Drawing.RectangleF 结构。
//        public void IntersectClip(RectangleF rect);
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 对象的剪辑区域更新为当前剪辑区域与指定 System.Drawing.Region 的交集。
//        //
//        // 参数:
//        //   region:
//        //     要与当前区域交叉的 System.Drawing.Region。
//        public void IntersectClip(Region region);
//        //
//        // 摘要:
//        //     指示指定的 System.Drawing.Point 结构是否包含在此 System.Drawing.Graphics 的可见剪辑区域内。
//        //
//        // 参数:
//        //   point:
//        //     要测试其可见性的 System.Drawing.Point 结构。
//        //
//        // 返回结果:
//        //     如果 point 参数指定的点包含在此 System.Drawing.Graphics 的可见剪辑区域内，则为 true；否则为 false。
//        public bool IsVisible(Point point);
//        //
//        // 摘要:
//        //     指示指定的 System.Drawing.PointF 结构是否包含在此 System.Drawing.Graphics 的可见剪辑区域内。
//        //
//        // 参数:
//        //   point:
//        //     要测试其可见性的 System.Drawing.PointF 结构。
//        //
//        // 返回结果:
//        //     如果 point 参数指定的点包含在此 System.Drawing.Graphics 的可见剪辑区域内，则为 true；否则为 false。
//        public bool IsVisible(PointF point);
//        //
//        // 摘要:
//        //     指示 System.Drawing.Rectangle 结构指定的矩形是否包含在此 System.Drawing.Graphics 的可见剪辑区域内。
//        //
//        // 参数:
//        //   rect:
//        //     要测试其可见性的 System.Drawing.Rectangle 结构。
//        //
//        // 返回结果:
//        //     如果 rect 参数指定的矩形包含在此 System.Drawing.Graphics 的可见剪辑区域内，则为 true；否则为 false。
//        public bool IsVisible(Rectangle rect);
//        //
//        // 摘要:
//        //     指示 System.Drawing.RectangleF 结构指定的矩形是否包含在此 System.Drawing.Graphics 的可见剪辑区域内。
//        //
//        // 参数:
//        //   rect:
//        //     要测试其可见性的 System.Drawing.RectangleF 结构。
//        //
//        // 返回结果:
//        //     如果 rect 参数指定的矩形包含在此 System.Drawing.Graphics 的可见剪辑区域内，则为 true；否则为 false。
//        public bool IsVisible(RectangleF rect);
//        //
//        // 摘要:
//        //     指示由一对坐标指定的点是否包含在此 System.Drawing.Graphics 的可见剪辑区域内。
//        //
//        // 参数:
//        //   x:
//        //     要测试其可见性的点的 x 坐标。
//        //
//        //   y:
//        //     要测试其可见性的点的 y 坐标。
//        //
//        // 返回结果:
//        //     如果由 x 和 y 参数定义的点包含在此 System.Drawing.Graphics 的可见剪辑区域内，则为 true；否则为 false。
//        public bool IsVisible(float x, float y);
//        //
//        // 摘要:
//        //     指示由一对坐标指定的点是否包含在此 System.Drawing.Graphics 的可见剪辑区域内。
//        //
//        // 参数:
//        //   x:
//        //     要测试其可见性的点的 x 坐标。
//        //
//        //   y:
//        //     要测试其可见性的点的 y 坐标。
//        //
//        // 返回结果:
//        //     如果由 x 和 y 参数定义的点包含在此 System.Drawing.Graphics 的可见剪辑区域内，则为 true；否则为 false。
//        public bool IsVisible(int x, int y);
//        //
//        // 摘要:
//        //     指示由一对坐标、一个宽度和一个高度指定的矩形是否包含在此 System.Drawing.Graphics 的可见剪辑区域内。
//        //
//        // 参数:
//        //   x:
//        //     要测试其可见性的矩形的左上角的 x 坐标。
//        //
//        //   y:
//        //     要测试其可见性的矩形的左上角的 y 坐标。
//        //
//        //   width:
//        //     要测试其可见性的矩形的宽度。
//        //
//        //   height:
//        //     要测试其可见性的矩形的高度。
//        //
//        // 返回结果:
//        //     如果 x、y、width 和 height 参数定义的矩形包含在此 System.Drawing.Graphics 的可见剪辑区域内，则为 true；否则为
//        //     false。
//        public bool IsVisible(float x, float y, float width, float height);
//        //
//        // 摘要:
//        //     指示由一对坐标、一个宽度和一个高度指定的矩形是否包含在此 System.Drawing.Graphics 的可见剪辑区域内。
//        //
//        // 参数:
//        //   x:
//        //     要测试其可见性的矩形的左上角的 x 坐标。
//        //
//        //   y:
//        //     要测试其可见性的矩形的左上角的 y 坐标。
//        //
//        //   width:
//        //     要测试其可见性的矩形的宽度。
//        //
//        //   height:
//        //     要测试其可见性的矩形的高度。
//        //
//        // 返回结果:
//        //     如果 x、y、width 和 height 参数定义的矩形包含在此 System.Drawing.Graphics 的可见剪辑区域内，则为 true；否则为
//        //     false。
//        public bool IsVisible(int x, int y, int width, int height);
//        //
//        // 摘要:
//        //     获取 System.Drawing.Region 对象的数组，其中每个对象将字符位置的范围限定在指定字符串内。
//        //
//        // 参数:
//        //   text:
//        //     要测量的字符串。
//        //
//        //   font:
//        //     System.Drawing.Font，它定义字符串的文本格式。
//        //
//        //   layoutRect:
//        //     System.Drawing.RectangleF 结构，它指定字符串的布局矩形。
//        //
//        //   stringFormat:
//        //     System.Drawing.StringFormat，它表示字符串的格式化信息（如行距）。
//        //
//        // 返回结果:
//        //     此方法返回 System.Drawing.Region 对象的数组，其中每个对象将字符位置的范围限定在指定字符串内。
//        public Region[] MeasureCharacterRanges(string text, Font font, RectangleF layoutRect, StringFormat stringFormat);
//        //
//        // 摘要:
//        //     测量用指定的 System.Drawing.Font 绘制的指定字符串。
//        //
//        // 参数:
//        //   text:
//        //     要测量的字符串。
//        //
//        //   font:
//        //     System.Drawing.Font，它定义字符串的文本格式。
//        //
//        // 返回结果:
//        //     此方法返回 System.Drawing.SizeF 结构，该结构表示 text 参数指定的、使用 font 参数绘制的字符串的大小，单位由 System.Drawing.Graphics.PageUnit
//        //     属性指定。
//        //
//        // 异常:
//        //   System.ArgumentException:
//        //     font 为 null。
//        public SizeF MeasureString(string text, Font font);
//        //
//        // 摘要:
//        //     测量用指定的 System.Drawing.Font 绘制的指定字符串。
//        //
//        // 参数:
//        //   text:
//        //     要测量的字符串。
//        //
//        //   font:
//        //     System.Drawing.Font，它定义字符串的格式。
//        //
//        //   width:
//        //     字符串的最大宽度（以像素为单位）。
//        //
//        // 返回结果:
//        //     此方法返回 System.Drawing.SizeF 结构，该结构表示在 text 参数中指定的、使用 font 参数绘制的字符串的大小，单位由
//        //     System.Drawing.Graphics.PageUnit 属性指定。
//        //
//        // 异常:
//        //   System.ArgumentException:
//        //     font 为 null。
//        public SizeF MeasureString(string text, Font font, int width);
//        //
//        // 摘要:
//        //     当在指定的布局区域内以指定的 System.Drawing.Font 绘制时，测量指定的字符串。
//        //
//        // 参数:
//        //   text:
//        //     要测量的字符串。
//        //
//        //   font:
//        //     System.Drawing.Font 定义字符串的文本格式。
//        //
//        //   layoutArea:
//        //     System.Drawing.SizeF 结构，它指定文本的最大布局区域。
//        //
//        // 返回结果:
//        //     此方法返回 System.Drawing.SizeF 结构，该结构表示 text 参数指定的、使用 font 参数绘制的字符串的大小，单位由 System.Drawing.Graphics.PageUnit
//        //     属性指定。
//        //
//        // 异常:
//        //   System.ArgumentException:
//        //     font 为 null。 
//        public SizeF MeasureString(string text, Font font, SizeF layoutArea);
//        //
//        // 摘要:
//        //     测量用指定的 System.Drawing.Font 绘制并用指定的 System.Drawing.StringFormat 格式化的指定字符串。
//        //
//        // 参数:
//        //   text:
//        //     要测量的字符串。
//        //
//        //   font:
//        //     System.Drawing.Font，它定义字符串的文本格式。
//        //
//        //   width:
//        //     字符串的最大宽度。
//        //
//        //   format:
//        //     System.Drawing.StringFormat，它表示字符串的格式化信息（如行距）。
//        //
//        // 返回结果:
//        //     此方法返回 System.Drawing.SizeF 结构，该结构表示在 text 参数中指定的、用 font 参数和 stringFormat
//        //     参数绘制的字符串的大小，单位由 System.Drawing.Graphics.PageUnit 属性指定。
//        //
//        // 异常:
//        //   System.ArgumentException:
//        //     font 为 null。
//        public SizeF MeasureString(string text, Font font, int width, StringFormat format);
//        //
//        // 摘要:
//        //     测量用指定的 System.Drawing.Font 绘制并用指定的 System.Drawing.StringFormat 格式化的指定字符串。
//        //
//        // 参数:
//        //   text:
//        //     要测量的字符串。
//        //
//        //   font:
//        //     System.Drawing.Font 定义字符串的文本格式。
//        //
//        //   origin:
//        //     System.Drawing.PointF 结构，它表示字符串的左上角。
//        //
//        //   stringFormat:
//        //     System.Drawing.StringFormat，它表示字符串的格式化信息（如行距）。
//        //
//        // 返回结果:
//        //     此方法返回 System.Drawing.SizeF 结构，该结构表示 text 参数指定的、使用 font 参数和 stringFormat 参数绘制的字符串的大小，单位由
//        //     System.Drawing.Graphics.PageUnit 属性指定。
//        //
//        // 异常:
//        //   System.ArgumentException:
//        //     font 为 null。
//        public SizeF MeasureString(string text, Font font, PointF origin, StringFormat stringFormat);
//        //
//        // 摘要:
//        //     测量用指定的 System.Drawing.Font 绘制并用指定的 System.Drawing.StringFormat 格式化的指定字符串。
//        //
//        // 参数:
//        //   text:
//        //     要测量的字符串。
//        //
//        //   font:
//        //     System.Drawing.Font 定义字符串的文本格式。
//        //
//        //   layoutArea:
//        //     System.Drawing.SizeF 结构，它指定文本的最大布局区域。
//        //
//        //   stringFormat:
//        //     System.Drawing.StringFormat，它表示字符串的格式化信息（如行距）。
//        //
//        // 返回结果:
//        //     此方法返回 System.Drawing.SizeF 结构，该结构表示在 text 参数中指定的、用 font 参数和 stringFormat
//        //     参数绘制的字符串的大小，单位由 System.Drawing.Graphics.PageUnit 属性指定。
//        //
//        // 异常:
//        //   System.ArgumentException:
//        //     font 为 null。
//        public SizeF MeasureString(string text, Font font, SizeF layoutArea, StringFormat stringFormat);
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 的世界变换乘以指定的 System.Drawing.Drawing2D.Matrix。
//        //
//        // 参数:
//        //   matrix:
//        //     乘以世界变换的 4x4 System.Drawing.Drawing2D.Matrix。 
//        public void MultiplyTransform(Matrix matrix);
//        //
//        // 摘要:
//        //     以指定顺序将此 System.Drawing.Graphics 的世界变换乘以指定的 System.Drawing.Drawing2D.Matrix。
//        //
//        // 参数:
//        //   matrix:
//        //     乘以世界变换的 4x4 System.Drawing.Drawing2D.Matrix。
//        //
//        //   order:
//        //     System.Drawing.Drawing2D.MatrixOrder 枚举的成员，它确定乘法的顺序。
//        public void MultiplyTransform(Matrix matrix, MatrixOrder order);
//        //
//        // 摘要:
//        //     释放通过以前对此 System.Drawing.Graphics 的 System.Drawing.Graphics.GetHdc() 方法的调用获得的设备上下文句柄。
//        public void ReleaseHdc();
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 的剪辑区域重置为无限区域。
//        public void ResetClip();
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 的世界变换矩阵重置为单位矩阵。
//        public void ResetTransform();
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 的状态还原到 System.Drawing.Drawing2D.GraphicsState
//        //     表示的状态。
//        //
//        // 参数:
//        //   gstate:
//        //     System.Drawing.Drawing2D.GraphicsState，它表示要将此 System.Drawing.Graphics 还原到的状态。
//        public void Restore(GraphicsState gstate);
//        //
//        // 摘要:
//        //     将指定旋转应用于此 System.Drawing.Graphics 的变换矩阵。
//        //
//        // 参数:
//        //   angle:
//        //     旋转角度（以度为单位）。 
//        public void RotateTransform(float angle);
//        //
//        // 摘要:
//        //     以指定顺序将指定旋转应用到此 System.Drawing.Graphics 的变换矩阵。
//        //
//        // 参数:
//        //   angle:
//        //     旋转角度（以度为单位）。
//        //
//        //   order:
//        //     System.Drawing.Drawing2D.MatrixOrder 枚举的成员，它指定是将旋转追加到矩阵变换之后还是添加到矩阵变换之前。
//        public void RotateTransform(float angle, MatrixOrder order);
//        //
//        // 摘要:
//        //     保存此 System.Drawing.Graphics 的当前状态，并用 System.Drawing.Drawing2D.GraphicsState
//        //     标识保存的状态。
//        //
//        // 返回结果:
//        //     此方法返回 System.Drawing.Drawing2D.GraphicsState，该对象表示此 System.Drawing.Graphics
//        //     的保存状态。
//        public GraphicsState Save();
//        //
//        // 摘要:
//        //     将指定的缩放操作应用于此 System.Drawing.Graphics 的变换矩阵，方法是将该对象的变换矩阵左乘该缩放矩阵。
//        //
//        // 参数:
//        //   sx:
//        //     x 方向的缩放比例。
//        //
//        //   sy:
//        //     y 方向的缩放比例。 
//        public void ScaleTransform(float sx, float sy);
//        //
//        // 摘要:
//        //     以指定顺序将指定的缩放操作应用到此 System.Drawing.Graphics 的变换矩阵。
//        //
//        // 参数:
//        //   sx:
//        //     x 方向的缩放比例。
//        //
//        //   sy:
//        //     y 方向的缩放比例。
//        //
//        //   order:
//        //     System.Drawing.Drawing2D.MatrixOrder 枚举的成员，它指定是将缩放操作添加到变换矩阵前还是追加到变换矩阵后。
//        public void ScaleTransform(float sx, float sy, MatrixOrder order);
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 的剪辑区域设置为指定 System.Drawing.Graphics 的 Clip 属性。
//        //
//        // 参数:
//        //   g:
//        //     System.Drawing.Graphics，从该对象中获取新剪辑区域。 
//        public void SetClip(Graphics g);
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 的剪辑区域设置为指定的 System.Drawing.Drawing2D.GraphicsPath。
//        //
//        // 参数:
//        //   path:
//        //     System.Drawing.Drawing2D.GraphicsPath，它表示新的剪辑区域。 
//        public void SetClip(GraphicsPath path);
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 的剪辑区域设置为 System.Drawing.Rectangle 结构指定的矩形。
//        //
//        // 参数:
//        //   rect:
//        //     System.Drawing.Rectangle 结构，它表示新的剪辑区域。 
//        public void SetClip(Rectangle rect);
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 的剪辑区域设置为 System.Drawing.RectangleF 结构指定的矩形。
//        //
//        // 参数:
//        //   rect:
//        //     System.Drawing.RectangleF 结构，它表示新的剪辑区域。 
//        public void SetClip(RectangleF rect);
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 的剪辑区域设置为当前剪辑区域和指定的 System.Drawing.Graphics 的 System.Drawing.Graphics.Clip
//        //     属性指定的组合操作的结果。
//        //
//        // 参数:
//        //   g:
//        //     System.Drawing.Graphics，它指定要组合的剪辑区域。
//        //
//        //   combineMode:
//        //     System.Drawing.Drawing2D.CombineMode 枚举的成员，它指定要使用的组合操作。
//        public void SetClip(Graphics g, CombineMode combineMode);
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 的剪辑区域设置为当前剪辑区域与指定 System.Drawing.Drawing2D.GraphicsPath
//        //     的组合结果。
//        //
//        // 参数:
//        //   path:
//        //     要组合的 System.Drawing.Drawing2D.GraphicsPath。
//        //
//        //   combineMode:
//        //     System.Drawing.Drawing2D.CombineMode 枚举的成员，它指定要使用的组合操作。
//        public void SetClip(GraphicsPath path, CombineMode combineMode);
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 的剪辑区域设置为当前剪辑区域与 System.Drawing.Rectangle 结构所指定矩形的组合结果。
//        //
//        // 参数:
//        //   rect:
//        //     要组合的 System.Drawing.Rectangle 结构。
//        //
//        //   combineMode:
//        //     System.Drawing.Drawing2D.CombineMode 枚举的成员，它指定要使用的组合操作。
//        public void SetClip(Rectangle rect, CombineMode combineMode);
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 的剪辑区域设置为当前剪辑区域与 System.Drawing.RectangleF 结构所指定矩形的组合结果。
//        //
//        // 参数:
//        //   rect:
//        //     要组合的 System.Drawing.RectangleF 结构。
//        //
//        //   combineMode:
//        //     System.Drawing.Drawing2D.CombineMode 枚举的成员，它指定要使用的组合操作。
//        public void SetClip(RectangleF rect, CombineMode combineMode);
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 的剪辑区域设置为当前剪辑区域与指定 System.Drawing.Region 的组合结果。
//        //
//        // 参数:
//        //   region:
//        //     要组合的 System.Drawing.Region。
//        //
//        //   combineMode:
//        //     System.Drawing.Drawing2D.CombineMode 枚举的成员，它指定要使用的组合操作。
//        public void SetClip(Region region, CombineMode combineMode);
//        //
//        // 摘要:
//        //     使用此 System.Drawing.Graphics 的当前世界变换和页变换，将点数组从一个坐标空间转换到另一个坐标空间。
//        //
//        // 参数:
//        //   destSpace:
//        //     System.Drawing.Drawing2D.CoordinateSpace 枚举成员，它指定目标坐标空间。
//        //
//        //   srcSpace:
//        //     System.Drawing.Drawing2D.CoordinateSpace 枚举成员，它指定源坐标空间。
//        //
//        //   pts:
//        //     System.Drawing.Point 结构数组，这些结构表示要变换的点。
//        public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, Point[] pts);
//        //
//        // 摘要:
//        //     使用此 System.Drawing.Graphics 的当前世界变换和页变换，将点数组从一个坐标空间转换到另一个坐标空间。
//        //
//        // 参数:
//        //   destSpace:
//        //     System.Drawing.Drawing2D.CoordinateSpace 枚举成员，它指定目标坐标空间。
//        //
//        //   srcSpace:
//        //     System.Drawing.Drawing2D.CoordinateSpace 枚举成员，它指定源坐标空间。
//        //
//        //   pts:
//        //     System.Drawing.PointF 结构数组，这些结构表示要变换的点。
//        public void TransformPoints(CoordinateSpace destSpace, CoordinateSpace srcSpace, PointF[] pts);
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 的剪辑区域沿水平方向和垂直方向平移指定的量。
//        //
//        // 参数:
//        //   dx:
//        //     平移的 x 坐标。
//        //
//        //   dy:
//        //     平移的 y 坐标。
//        public void TranslateClip(float dx, float dy);
//        //
//        // 摘要:
//        //     将此 System.Drawing.Graphics 的剪辑区域沿水平方向和垂直方向平移指定的量。
//        //
//        // 参数:
//        //   dx:
//        //     平移的 x 坐标。
//        //
//        //   dy:
//        //     平移的 y 坐标。
//        public void TranslateClip(int dx, int dy);
//        //
//        // 摘要:
//        //     通过使此 System.Drawing.Graphics 的变换矩阵左乘指定的平移来更改坐标系统的原点。
//        //
//        // 参数:
//        //   dx:
//        //     平移的 x 坐标。
//        //
//        //   dy:
//        //     平移的 y 坐标。 
//        public void TranslateTransform(float dx, float dy);
//        //
//        // 摘要:
//        //     通过以指定顺序将指定平移应用于此 System.Drawing.Graphics 的变换矩阵来更改坐标系统的原点。
//        //
//        // 参数:
//        //   dx:
//        //     平移的 x 坐标。
//        //
//        //   dy:
//        //     平移的 y 坐标。
//        //
//        //   order:
//        //     System.Drawing.Drawing2D.MatrixOrder 枚举的成员，它指定是将平移添加到变换矩阵前还是追加到变换矩阵后。
//        public void TranslateTransform(float dx, float dy, MatrixOrder order);

//    }
//}
