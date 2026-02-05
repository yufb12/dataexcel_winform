using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Feng.Forms
{
    public class MouseDoubleClick
    {
        public MouseDoubleClick()
        {

        }
        private static MouseDoubleClick mousedoubleclick = null;
        public static MouseDoubleClick Default
        {
            get {
                if (mousedoubleclick==null)
                {
                    mousedoubleclick = new MouseDoubleClick();
                }
                return mousedoubleclick;
            }
        }

        public bool CheckDoubleClick(Point point)
        {
            if ((DateTime.Now - ClickTime).TotalMilliseconds < 500)
            {
                if (ClickRectangle == Rectangle.Empty)
                {
                    clickrectangle = new Rectangle(point.X - 2, point.Y - 2, 5, 5);
                    return false;
                }
                else
                {
                    clickrectangle.X = point.X - 2;
                    clickrectangle.Y = point.Y - 2;
                    clickrectangle.Width = 5;
                    clickrectangle.Height = 5;
                }
                if (ClickRectangle.Contains(point))
                {
                    return true;
                }
            }
            ClickTime = DateTime.Now;
            return false;
        }

        private Rectangle clickrectangle = Rectangle.Empty;
        public Rectangle ClickRectangle { get { return clickrectangle; }  }
        public DateTime ClickTime { get; set; }
    }
}
