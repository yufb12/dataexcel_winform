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
    public class ColorHelper
    {
        public static Color[] DefaultColors 
        {
            get
            {
                return new Color[] { Color.Red ,Color.Green,Color.Blue
            ,Color.Yellow
            ,Color.Wheat
            ,Color.Purple
            };
            }
        }
        public static Color DarkColor(Color color, float dark)
        {
            return System.Windows.Forms.ControlPaint.Dark(color, dark);
        }
        public static Color LightColor(Color color, float dark)
        {
            return System.Windows.Forms.ControlPaint.Light(color, dark);
        }
        public static Color Light(Color color)
        {
            return System.Windows.Forms.ControlPaint.Light(color);
        }
        public static Color LightLight(Color color)
        {
            return System.Windows.Forms.ControlPaint.LightLight(color);
        }
        public static Color Dark(Color color)
        {
            return System.Windows.Forms.ControlPaint.Dark(color);
        }
        public static Color DarkDark(Color color)
        {
            return System.Windows.Forms.ControlPaint.DarkDark(color);
        }
        public static Color InvertColors(Color color)
        {
            return Color.FromArgb(color.A, (255 - color.R), (255 - color.G), (255 - color.B));
        }
        public static Color MidColor(Color color1, Color color2)
        {
            if (color1.R > 128 && color1.R > 128 && color1.R > 128)
            {
                return Dark(color1);
            }
            if (color1.R < 128 && color1.R <128 && color1.R < 128)
            {
                return Light(color1);
            }
            return Color.FromArgb(color1.A, color1.R+(color1.R - color2.R) / 2, (color1.G - color2.G) / 2, (color1.B - color2.B) / 2);
        }
        public static Color InvertColors(Color color,Color color1)
        {
            return Color.FromArgb(color.A, (color1.R + color.R) / 2, (color1.G + color.G) / 2, (color1.B + color.B) / 2);
        }
        public static Color InvertColors(Color color,int A)
        {
            return Color.FromArgb(A,255 - color.R, 255 - color.G, 255 - color.B);
        }
        public static Color Gray(Color color)
        {
            byte a = color.A;
            byte al = (byte)((color.R + color.G + color.B) / 3);
            return Color.FromArgb(a, al, al, al);
        }
        public static Color FocusColor {
            get {
                return Color.FromArgb(80, Color.Orange);
            }
        }
        public static Color Opacity(Color color, int alpha)
        {
            return Color.FromArgb((int)(alpha/ 100d * 255 ), color);
        }
        //private const int ShadowAdj = -333;
        //private const int HilightAdj = 500;
        //private const int WatermarkAdj = -50;
        //private const int Range = 240;
        //private const int HLSMax = 240;
        //private const int RGBMax = 0xff;
        //private const int Undefined = 160;
        //private int hue;
        //private int saturation;
        //private int luminosity;
        //public ColorHelper(Color color)
        //{
        //    int r = color.R;
        //    int g = color.G;
        //    int b = color.B;
        //    int num4 = Math.Max(Math.Max(r, g), b);
        //    int num5 = Math.Min(Math.Min(r, g), b);
        //    int num6 = num4 + num5;
        //    this.luminosity = ((num6 * 240) + 0xff) / 510;
        //    int num7 = num4 - num5;
        //    if (num7 == 0)
        //    {
        //        this.saturation = 0;
        //        this.hue = 160;
        //    }
        //    else
        //    {
        //        if (this.luminosity <= 120)
        //        {
        //            this.saturation = ((num7 * 240) + (num6 / 2)) / num6;
        //        }
        //        else
        //        {
        //            this.saturation = ((num7 * 240) + ((510 - num6) / 2)) / (510 - num6);
        //        }
        //        int num8 = (((num4 - r) * 40) + (num7 / 2)) / num7;
        //        int num9 = (((num4 - g) * 40) + (num7 / 2)) / num7;
        //        int num10 = (((num4 - b) * 40) + (num7 / 2)) / num7;
        //        if (r == num4)
        //        {
        //            this.hue = num10 - num9;
        //        }
        //        else if (g == num4)
        //        {
        //            this.hue = (80 + num8) - num10;
        //        }
        //        else
        //        {
        //            this.hue = (160 + num9) - num8;
        //        }
        //        if (this.hue < 0)
        //        {
        //            this.hue += 240;
        //        }
        //        if (this.hue > 240)
        //        {
        //            this.hue -= 240;
        //        }
        //    }
        //}

        //public int Luminosity
        //{
        //    get
        //    {
        //        return this.luminosity;
        //    }
        //}
        //public Color Darker(float percDarker)
        //{
        //    int num4 = 0;
        //    int num5 = this.NewLuma(-333, true);
        //    return this.ColorFromHLS(this.hue, num5 - ((int)((num5 - num4) * percDarker)), this.saturation);
        //}

        //public override bool Equals(object o)
        //{
        //    if (!(o is ColorHelper))
        //    {
        //        return false;
        //    }
        //    ColorHelper color = (ColorHelper)o;
        //    return ((((this.hue == color.hue) && (this.saturation == color.saturation)) && (this.luminosity == color.luminosity)));
        //}

        //public static bool operator ==(ColorHelper a, ColorHelper b)
        //{
        //    return a.Equals(b);
        //}

        //public static bool operator !=(ColorHelper a, ColorHelper b)
        //{
        //    return !a.Equals(b);
        //}

        //public override int GetHashCode()
        //{
        //    return (((this.hue << 6) | (this.saturation << 2)) | this.luminosity);
        //}

        //public Color Lighter(float percLighter)
        //{
        //    int luminosity = this.luminosity;
        //    int num5 = this.NewLuma(500, true);
        //    return this.ColorFromHLS(this.hue, luminosity + ((int)((num5 - luminosity) * percLighter)), this.saturation);
        //}

        //private int NewLuma(int n, bool scale)
        //{
        //    return this.NewLuma(this.luminosity, n, scale);
        //}

        //private int NewLuma(int luminosity, int n, bool scale)
        //{
        //    if (n == 0)
        //    {
        //        return luminosity;
        //    }
        //    if (scale)
        //    {
        //        if (n > 0)
        //        {
        //            return (((luminosity * (0x3e8 - n)) + (0xf1 * n)) / 0x3e8);
        //        }
        //        return ((luminosity * (n + 0x3e8)) / 0x3e8);
        //    }
        //    int num = luminosity;
        //    num += (n * 240) / 0x3e8;
        //    if (num < 0)
        //    {
        //        num = 0;
        //    }
        //    if (num > 240)
        //    {
        //        num = 240;
        //    }
        //    return num;
        //}

        //private Color ColorFromHLS(int hue, int luminosity, int saturation)
        //{
        //    byte num;
        //    byte num2;
        //    byte num3;
        //    if (saturation == 0)
        //    {
        //        num = num2 = num3 = (byte)((luminosity * 0xff) / 240);
        //        if (hue == 160)
        //        {
        //        }
        //    }
        //    else
        //    {
        //        int num5;
        //        if (luminosity <= 120)
        //        {
        //            num5 = ((luminosity * (240 + saturation)) + 120) / 240;
        //        }
        //        else
        //        {
        //            num5 = (luminosity + saturation) - (((luminosity * saturation) + 120) / 240);
        //        }
        //        int num4 = (2 * luminosity) - num5;
        //        num = (byte)(((this.HueToRGB(num4, num5, hue + 80) * 0xff) + 120) / 240);
        //        num2 = (byte)(((this.HueToRGB(num4, num5, hue) * 0xff) + 120) / 240);
        //        num3 = (byte)(((this.HueToRGB(num4, num5, hue - 80) * 0xff) + 120) / 240);
        //    }
        //    return Color.FromArgb(num, num2, num3);
        //}

        //private int HueToRGB(int n1, int n2, int hue)
        //{
        //    if (hue < 0)
        //    {
        //        hue += 240;
        //    }
        //    if (hue > 240)
        //    {
        //        hue -= 240;
        //    }
        //    if (hue < 40)
        //    {
        //        return (n1 + ((((n2 - n1) * hue) + 20) / 40));
        //    }
        //    if (hue < 120)
        //    {
        //        return n2;
        //    }
        //    if (hue < 160)
        //    {
        //        return (n1 + ((((n2 - n1) * (160 - hue)) + 20) / 40));
        //    }
        //    return n1;
        //}
        public static void calc()
        {
            //max = max(R, G, B)；
            //min = min(R, G, B)；
            //V = max(R, G, B)；
            //S = (max - min) / max；

            //if (R = max) H = (G - B) / (max - min) * 60；
            //if (G = max) H = 120 + (B - R) / (max - min) * 60；
            //if (B = max) H = 240 + (R - G) / (max - min) * 60；
            //if (H < 0) H = H + 360；
        }

        public static Color HexColor(string hex)
        {
            if (hex.StartsWith("#"))
            {
                hex = hex.Substring(1);
            }
            int alpha = 255;
            int startIndex = 0;
            if (hex.Length == 8)
            {
                alpha = int.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
                startIndex = 2;
            }
            int r = int.Parse(hex.Substring(startIndex, 2), System.Globalization.NumberStyles.HexNumber);
            int g = int.Parse(hex.Substring(startIndex + 2, 2), System.Globalization.NumberStyles.HexNumber);
            int b = int.Parse(hex.Substring(startIndex + 4, 2), System.Globalization.NumberStyles.HexNumber);
            return Color.FromArgb(alpha, r, g, b);
        }
    }

    public class ColorHSV
    {
#if DEBUG
        static ColorHSV()
        {
            ColorHSV colorHSV = ColorHSV.FromColor(Color.FromArgb(234,123,56));

        }
#endif
        /// <summary>
        /// 色相
        /// </summary>
        public decimal Hue { get; set; }

        /// <summary>
        /// 饱和度
        /// </summary>
        public decimal Saturation { get; set; }

        /// <summary>
        /// 亮度 
        /// </summary>
        public decimal Value { get; set; }

        public static ColorHSV FromColor(System.Drawing.Color colorrgb)
        {
            ColorHSV colorHSL = new ColorHSV();
            decimal R = colorrgb.R;
            decimal B = colorrgb.B;
            decimal G = colorrgb.G;
            decimal max = Math.Max(Math.Max(colorrgb.R, colorrgb.G), colorrgb.B);
            decimal min = Math.Max(Math.Max(colorrgb.R, colorrgb.G), colorrgb.B);
            decimal V = max;
            decimal S = 0;
            if (max > 0)
            {
                S = (max - min) / max;
            }
            decimal H = 0;
            if (R == max) H = (G - B) / (max - min) * 60;
            if (G == max) H = 120 + (B - R) / (max - min) * 60;
            if (B == max) H = 240 + (R - G) / (max - min) * 60;
            if (H < 0) H = H + 360;
            colorHSL.Hue = H;
            colorHSL.Saturation = S;
            colorHSL.Value = V;

            return colorHSL;
        }

        public HSLMODE HSLMODE
        {
            get
            {
                decimal value = Hue;
                if (value <= 30 && value >= 0)
                    return HSLMODE.Red;

                if (value <= 360 && value >= 330)
                    return HSLMODE.Red;


                if (value <= 90 && value >= 30)
                    return HSLMODE.Yellow;

                if (value <= 150 && value >= 90)
                    return HSLMODE.Green;

                if (value <= 210 && value >= 150)
                    return HSLMODE.Cyan;

                if (value <= 270 && value >= 210)
                    return HSLMODE.Blue;

                if (value <= 330 && value >= 270)
                    return HSLMODE.Magenta;


                throw new Exception(" ColorHSL  HSLMODE Hue Error");
            }
        }

        public Color ToColor()
        {
            int R = 0;
            int B = 0;
            int G = 0;

            if (this.Saturation != 0)
            {
                int i = (int)this.Hue / 60;
                decimal f = Hue - i;
                int a = (int)(Value * (1 - Saturation));
                int b = (int)(Value * (1 - Saturation * f));
                int c = (int)(Value * (1 - Saturation * (1 - f)));
                switch (i)
                {
                    case 0:
                        R = (int)Value; G = c; B = a;
                        break;
                    case 1:
                        R = b; G = (int)Value; B = a;
                        break;
                    case 2:
                        R = a; G = (int)Value; B = c;
                        break;
                    case 3:
                        R = a; G = b; B = (int)Value;
                        break;
                    case 4:
                        R = c; G = a; B = (int)Value;
                        break;
                    case 5:
                        R = (int)Value; G = a; B = b;
                        break;
                }
            }
            return System.Drawing.Color.FromArgb(R, G, B);
        }

        public override string ToString()
        {
            return string.Format("{0:0},{1:0},{2:0},{3}", Hue, Saturation, Value, HSLMODE);
        }
    }

    public class ColorHSL
    {
        /// <summary>
        /// 色相
        /// </summary>
        public float Hue { get; set; }
 
        /// <summary>
        /// 饱和度
        /// </summary>
        public float Saturation { get; set; }
 
        /// <summary>
        /// 亮度 
        /// </summary>
        public float Lightness { get; set; }


        public static ColorHSL FromColor(System.Drawing.Color color)
        {
            ColorHSL colorHSL = new ColorHSL();
            int _hue = 0;
            double _saturation = 1d;
            double _lightness = 1d;
            double r = ((double)color.R) / 255;
            double g = ((double)color.G) / 255;
            double b = ((double)color.B) / 255;
            double min = Math.Min(Math.Min(r, g), b);
            double max = Math.Max(Math.Max(r, g), b);
            double distance = max - min;
            _lightness = (max + min) / 2;
            if (distance == 0)
            {
                _hue = 0;
                _saturation = 0;
            }
            else
            {
                double hueTmp;
                _saturation = (_lightness < 0.5) ? (distance / (max + min)) : (distance / ((2 - max) - min));
                double tempR = (((max - r) / 6) + (distance / 2)) / distance;
                double tempG = (((max - g) / 6) + (distance / 2)) / distance;
                double tempB = (((max - b) / 6) + (distance / 2)) / distance;
                if (r == max)
                {
                    hueTmp = tempB - tempG;
                }
                else if (g == max)
                {
                    hueTmp = (0.33333333333333331 + tempR) - tempB;
                }
                else
                {
                    hueTmp = (0.66666666666666663 + tempG) - tempR;
                }
                if (hueTmp < 0)
                {
                    hueTmp += 1;
                }
                if (hueTmp > 1)
                {
                    hueTmp -= 1;
                }
                _hue = (int)(hueTmp * 360);

            }
            colorHSL.Hue = _hue;
            colorHSL.Lightness = (float)_lightness * 100;
            colorHSL.Saturation = (float)_saturation * 100;
            return colorHSL;
        }

        public HSLMODE HSLMODE 
        {
            get {
                if (Lightness <20)
                    return HSLMODE.Black;
                if (Lightness > 248)
                    return HSLMODE.White;
                float value = Hue;
                if (value <=  30 && value >=0)
                    return HSLMODE.Red;

                if (value <= 360 && value >= 330)
                    return HSLMODE.Red;


                if (value <= 90 && value >= 30)
                    return HSLMODE.Yellow;

                if (value <= 150 && value >= 90)
                    return HSLMODE.Green;

                if (value <= 210 && value >= 150)
                    return HSLMODE.Cyan;

                if (value <= 270 && value >= 210)
                    return HSLMODE.Blue;

                if (value <= 330 && value >= 270)
                    return HSLMODE.Magenta;


                throw new Exception(" ColorHSL  HSLMODE Hue Error");
            }
        }

        public Color ToColor()
        {
            byte r;
            byte g;
            byte b;
            float _hue = this.Hue;
            double _saturation = this.Saturation / 100;
            double _lightness = this.Lightness / 100;
            if (_saturation == 0) 
            { 
                r = (byte)(_lightness * 255); 
                g = r; 
                b = r; 
            } 
            else 
            {

                double vH = ((double)_hue) / 360;
                double v2 =(_lightness < 0.5) ?(_lightness * (1 + _saturation)) :((_lightness + _saturation) - (_lightness * _saturation));
                double v1 = (2 * _lightness) - v2;
                r = (byte)(255 * HueToRGB(v1, v2, vH + 0.33333333333333331));
                g = (byte)(255 * HueToRGB(v1, v2, vH));
                b = (byte)(255 * HueToRGB(v1, v2, vH - 0.33333333333333331));
            } 
            return Color.FromArgb(r, g, b); 
        } 
        private double HueToRGB(double v1, double v2, double vH)
        {
            if (vH < 0)
            { 
                vH += 1; 
            } 
            if (vH > 1) 
            { 
                vH -= 1; 
            } 
            if ((6 * vH) < 1) 
            { 
                return v1 + (((v2 - v1) * 6) * vH); 
            } 
            if ((2 * vH) < 1) 
            { 
                return v2; 
            } 
            if ((3 * vH) < 2) 
            { 
                return v1 + (((v2 - v1) * (0.66666666666666663 - vH)) * 6); 
            } 
            return v1;

        }


        public override string ToString()
        {
            return string.Format("{0:0},{1:0},{2:0},{3}", Hue, Saturation, Lightness, HSLMODE);
        }
    }

    public enum HSLMODE
    {
        Red,
        Yellow,
        Green,
        Cyan,
        Blue,
        Magenta,
        White,
        Black
    }

}
