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

    public static class StringFormatCache
    {
        private static List<StringFormat> list = new List<StringFormat>();
        public static StringFormat GetStringFormat()
        {
            return GetStringFormat(StringAlignment.Center, StringAlignment.Center, false);
        }
        public static StringFormat GetStringFormatAlignLeft()
        {
            return GetStringFormat(StringAlignment.Near, StringAlignment.Center, false);
        }
 
        private static StringFormat stringformatalignleftnowrap = null;
        public static StringFormat GetStringFormatAlignLeftNoWrap()
        {
            if (stringformatalignleftnowrap == null)
            {
                StringFormat stringformat = StringFormat.GenericDefault.Clone() as StringFormat;
                stringformat.FormatFlags = StringFormatFlags.DisplayFormatControl;
                stringformat.Alignment = StringAlignment.Near;
                stringformat.LineAlignment = StringAlignment.Center;
                stringformat.Trimming = StringTrimming.EllipsisCharacter; 
                stringformat.FormatFlags = StringFormatFlags.NoWrap;
                stringformatalignleftnowrap = stringformat;
            }
            return stringformatalignleftnowrap;
        }

        private static StringFormat MeasureStringstringformat = null;
        public static StringFormat GetMeasureStringFormat()
        {
            if (stringformatalignleftnowrap == null)
            {
                StringFormat stringformat = StringFormat.GenericDefault.Clone() as StringFormat; 
                stringformat.Alignment = StringAlignment.Near;
                stringformat.LineAlignment = StringAlignment.Near;
                stringformat.Trimming = StringTrimming.None;
                stringformat.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.DisplayFormatControl | StringFormatFlags.NoClip | StringFormatFlags.FitBlackBox;
               
                MeasureStringstringformat = stringformat;
            }
            return MeasureStringstringformat;
        }
        public static StringFormat GetStringFormat(StringAlignment hStringAlignment, StringAlignment vStringAlignment
            , bool DirectionVertical)
        {
#warning nextdo
            switch (hStringAlignment)
            {
                case StringAlignment.Near:
                    break;
                case StringAlignment.Center:
                    break;
                case StringAlignment.Far:
                    break;
                default:
                    break;
            }
            StringFormatFlags sff = StringFormat.GenericDefault.FormatFlags;
            if (DirectionVertical)
            {
                sff = sff | StringFormatFlags.DirectionVertical;
            }
            //foreach (StringFormat sf in list)
            //{
            //    if (sf.FormatFlags == sff && sf.Alignment == hStringAlignment
            //        && sf.LineAlignment == vStringAlignment)
            //    {
            //        return sf;
            //    }
            //}

            StringFormat stringformat = StringFormat.GenericDefault.Clone() as StringFormat;
            stringformat.Alignment = hStringAlignment;
            stringformat.LineAlignment = vStringAlignment;
            stringformat.Trimming = StringTrimming.EllipsisCharacter;
            stringformat.FormatFlags = sff;
            //list.Add(stringformat);
            return stringformat;
        }
    }


}
