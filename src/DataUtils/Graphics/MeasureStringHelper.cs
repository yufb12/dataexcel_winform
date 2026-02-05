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
    public class MeasureStringHelper
    {
        public static List<RectangleF> MeasureString(Graphics g, string measureString, Font stringFont,
           StringFormat stringFormat, Rectangle layoutRect)
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            List<RectangleF> list = new List<RectangleF>();

            RectangleF measureRect1 = RectangleF.Empty;
            for (int j = 0; j < measureString.Length; )
            {
                int length = measureString.Length - j;
                if (length > 31)
                {
                    length = 31;
                }
                CharacterRange[] characterRanges = GetCharRange(measureString, j, length);
                j = j + length;
                stringFormat.FormatFlags = stringFormat.FormatFlags | StringFormatFlags.MeasureTrailingSpaces;
                stringFormat.SetMeasurableCharacterRanges(characterRanges);
                // Measure two ranges in string.
                Region[] stringRegions = g.MeasureCharacterRanges(measureString,
            stringFont, layoutRect, stringFormat);
                bool mh = false;
                for (int i = 0; i < stringRegions.Length; i++)
                {
                    if (list.Count == 61)
                    {

                    }
                    RectangleF measureRect2 = stringRegions[i].GetBounds(g);
                    if ((measureRect2.Width > 0) && (measureRect2.X > 0))
                    {
                        measureRect1 = measureRect2;
                    }
                    else
                    {
                        //mh = true;
                        measureRect2.X = measureRect1.Left+ measureRect1.Width;
                        measureRect2.Y = measureRect1.Top;
                        measureRect2.Width = 3;
                    }
                    
                    list.Add(measureRect2);
                    if (measureRect2.Top > layoutRect.Top + layoutRect.Height)
                    {
                        mh = true;
                    }
                }
                if (mh)
                {
                    break;
                }
            }
            Feng.Utils.TraceHelper.WriteTrace("DataExcel", "Draw", "MeasureString", stopwatch.Elapsed.TotalSeconds > 0.02, "OnDrawLayerBottom:" + stopwatch.Elapsed.TotalSeconds.ToString("0.###"));
            return list;
        }
        private static CharacterRange[] GetCharRange(string text, int start, int length)
        {
            if (length > 31)
                throw new Exception(" > Length Max Num ");
            InitRanges(start + length);
            CharacterRange[] ranges = new CharacterRange[length];
            for (int i = 0; i < length; i++)
            {
                ranges[i] = List[start + i];
            }
            return ranges;
        }
        private static void InitRanges(int count)
        {
            for (int i = List.Count; i < count; i++)
            {
                List.Add(new CharacterRange(i, 1));
            }
        }
        private static List<CharacterRange> List = new List<CharacterRange>();
        
    }
}
