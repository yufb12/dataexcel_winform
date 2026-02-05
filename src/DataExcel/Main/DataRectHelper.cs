using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing ;
namespace Feng.Excel.Drawing
{
    [Serializable]
    public class DataRectHelper
    {
        //public static Rectangle GetRectBetweenTwoRect(Rectangle rectfrom, Rectangle rectto)
        //{
        //    int x = 0, y = 0, w = 0, h = 0;
        //    if (rectfrom.Location.X < rectto.Location.X)
        //    {
        //        x = rectfrom.Location.X;
        //        w = rectto.Right - rectfrom.Left;
        //    }
        //    else
        //    {
        //        x = rectto.Location.X;
        //        w = rectfrom.Right - rectto.Left;
        //    }

        //    if (rectfrom.Y < rectto.Y)
        //    {
        //        y = rectfrom.Y;
        //        h = rectto.Bottom - rectfrom.Top;
        //    }
        //    else
        //    {
        //        y = rectto.Y;
        //        h = rectfrom.Bottom - rectto.Top;
        //    }
        //    return new Rectangle(x, y, w, h);
        //}

        //public static Rectangle GetRectBetweenTwoRectFreen(Rectangle rectfrom, Rectangle rectto)
        //{
        //    int x = 0, y = 0, w = 0, h = 0;
        //    if (rectfrom.Location.X < rectto.Location.X)
        //    {
        //        x = rectfrom.Location.X;
        //        w = rectfrom.Width;
        //    }
        //    else
        //    {
        //        x = rectto.Location.X;
        //        w = rectfrom.Width;
        //    }

        //    if (rectfrom.Y < rectto.Y)
        //    {
        //        y = rectfrom.Y;
        //        h = rectfrom.Height;
        //    }
        //    else
        //    {
        //        y = rectto.Y;
        //        h = rectfrom.Height;
        //    }
        //    return new Rectangle(x, y, w, h);
        //}

        public static bool IsEmpty(Rectangle rect)
        {  
            if (rect.Width == 0 || rect.Height == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

}
