using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using Feng.Data;


namespace Feng.Assistant
{
    public class ImageClickSysEvent : SysEvents
    {
        public Point Point { get; set; }

        public override string Name
        {
            get
            {
                return "ImageClickSysEvent";
            }
        }

        private Bitmap _clickimage = null;

        public Bitmap ClickImage
        {
            get {
                return _clickimage;
            }
            set
            {
                _clickimage = value;
            }
        }

        private Rectangle _searchrect = Rectangle.Empty;

        public Rectangle SearchRect
        {
            get {
                return _searchrect;
            }
            set
            {
                _searchrect = value;
            }
        }

        public override void Excute()
        {
            if (this.ClickImage != null)
            {
                Bitmap bmp1 = GetScreenImage();
                Bitmap bmp2 = ClickImage;
                if (bmp1 != null && bmp2 != null)
                {
                    //bmp1.Save("bmp1.jpg");
                    //bmp2.Save("bmp2.jpg");
                    Point pt = Feng.Drawing.ImageHelper.GetImagePoint(bmp1, bmp2);
                    if (pt != Point.Empty)
                    {
                        Feng.Utils.UnsafeNativeMethods.MouseClick(pt);
                    }
                }
            }
        }

        public Bitmap GetScreenImage()
        {
            Bitmap screen = null;
            if (SearchRect != Rectangle.Empty)
            {
                screen = Feng.Drawing.ImageHelper.GetScreenImage(SearchRect.Location, SearchRect.Size);
            }
            else
            {
                screen = Feng.Drawing.ImageHelper.GetScreenImage();
            }

            return screen;
        }

        public override string ToString()
        {
            return string.Format("{0}", "点击图片");
        }

        public override Data.DataStruct Data
        {
            get
            {
                DataStruct data = new DataStruct();
                using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
                {
                    bw.Write(1, base.Data);
                    bw.Write(2, Point.X);
                    bw.Write(3, Point.Y);
                    bw.Write(4,this.ClickImage);
                    data.Data = bw.GetData();
                }
                return data;
            }
        }

        public override void ReadData(DataStruct data)
        {
            using (Feng.IO.BufferReader read = new IO.BufferReader(data.Data))
            {
                DataStruct database = read.ReadIndex(1, (DataStruct)null);
                int x = read.ReadIndex(2, 0);
                int y = read.ReadIndex(3, 0);
                Point = new Point(x, y);
                this.ClickImage = read.ReadIndex(4, this.ClickImage);
            }
        }
 
    }
}
