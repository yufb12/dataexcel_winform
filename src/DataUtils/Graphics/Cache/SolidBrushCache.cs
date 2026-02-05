using System.Collections.Generic;
using System.Drawing;

namespace Feng.Drawing
{
    public class SolidBrushCache
    {
        private static Dictionary<Color, SolidBrush> dics = new Dictionary<Color, SolidBrush>();
        public static Brush GetFocusBrush()
        {
            return GetSolidBrush(ColorHelper.FocusColor);
        }

        public static SolidBrush GetSolidBrush(Color color)
        {
            if (dics.ContainsKey(color))
            {
                return dics[color];
            }
            SolidBrush p = new SolidBrush(color);
            dics.Add(color, p);
            return p;
        }
    }
     
}
