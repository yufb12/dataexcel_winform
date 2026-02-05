using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Drawing
{

    public static class Coordinate
    {
        public static SizeF Scale(SizeF value, float zoom)
        {
            return new SizeF(zoom * value.Width, zoom * value.Height);
        }
        public static RectangleF Scale(RectangleF value, float zoom)
        {
            return new RectangleF(value.Left * zoom, value.Top * zoom, zoom * value.Width, zoom * value.Height);
        }
        public static Size Scale(Size value, int zoom)
        {
            return new Size(zoom * value.Width, zoom * value.Height);
        }
        public static Rectangle Scale(Rectangle value, int zoom)
        {
            return new Rectangle(value.Left * zoom, value.Top * zoom, zoom * value.Width, zoom * value.Height);
        }

        public static PointF PixelToDoc(PointF val)
        {
            float scale = Coordinate.Pixel / Coordinate.Document;
            return new PointF(val.X * scale, val.Y * scale);
        }

        public const float Display = 75f,
			Inch = 1f,
			Document = 300f,
			Millimeter = 25.4f,
			Point = 72f,
			HundredthsOfAnInch = 100f,
			TenthsOfAMillimeter = 254f,
			Twips = 1440f;
		public static readonly float Pixel = 96f;
 
		public static float UnitToDpi(GraphicsUnit unit) {
			switch(unit) {
				case GraphicsUnit.Display :
					return Display;
				case GraphicsUnit.Inch :
					return Inch;
				case GraphicsUnit.Document :
					return Document;
				case GraphicsUnit.Millimeter :
					return Millimeter;
				case GraphicsUnit.Pixel :
				case GraphicsUnit.World :
					return Pixel;
				case GraphicsUnit.Point :
					return Point;
			}
			throw new ArgumentException("unit");
		}
		public static GraphicsUnit DpiToUnit(float dpi) {
			if(dpi.Equals(Display))
				return GraphicsUnit.Display;
			if(dpi.Equals(Inch))
				return GraphicsUnit.Inch;
			if(dpi.Equals(Document))
				return GraphicsUnit.Document;
			if(dpi.Equals(Millimeter))
				return GraphicsUnit.Millimeter;
			if(dpi.Equals(Pixel))
				return GraphicsUnit.Pixel;
			if(dpi.Equals(Point))
				return GraphicsUnit.Point;
			throw new ArgumentException("dpi");
		}
    }
}
