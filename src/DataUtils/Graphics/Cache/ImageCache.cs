using System.Collections.Generic;
using System.Drawing;

namespace Feng.Drawing
{ 

    public class ImageCache
    {
        private static Dictionary<string, Image> dics = new Dictionary<string, Image>();
        public static Image DefaultImage { get; set; }
        public static Image Get(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return DefaultImage;
            if (dics.ContainsKey(key))
            {
                return dics[key];
            }
            return DefaultImage;
        }
        public static void Add(string key, Image value)
        {
            if (dics.ContainsKey(key))
            {
                dics[key] = value;
            }
            else
            {
                dics.Add(key, value);
            }
        }

        public static void Add(string v1, string v2, string key, string v3, Bitmap bmp)
        {
            Add(key, bmp);
        }
    }
}
