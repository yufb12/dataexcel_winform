using System.Collections.Generic;
using System.Drawing;

namespace Feng.Drawing
{

    public class PenCache
    {
        private static Dictionary<Color, Pen> dics = new Dictionary<Color, Pen>();
        public static int CacheMax = 500;
        public static Pen GetPen(Color color)
        {
            if (dics.Count > CacheMax)
            {
                Pen p2 = new Pen(color); 
                return p2;
            }
            if (dics.ContainsKey(color))
            {
                return dics[color];
            }
            Pen p = new Pen(color);
            dics.Add(color, p);
            return p;
        }
        private static Dictionary<string, Pen> dics2 = new Dictionary<string, Pen>();
        public static Pen GetPen(string key, Color color)
        {
            if (dics.Count > CacheMax)
            {
                Pen p2 = new Pen(color);
                return p2;
            }
            if (dics2.ContainsKey(key))
            {
                return dics2[key];
            }
            Pen p = new Pen(color);
            dics2.Add(key, p);
            return p;
        }

        public static Pen BorderGray
        {
            get
            {
                return GetPen(Color.FromArgb(192, 192, 192));
            }
        }
    }

}
