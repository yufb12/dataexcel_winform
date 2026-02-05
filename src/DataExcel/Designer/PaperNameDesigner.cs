using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Diagnostics;
using System.ComponentModel;
using System.Globalization;

namespace Feng.Excel.Designer
{

    public class PaperNameConverter : StringValuesConverter
    {
        StandardValuesCollection _StandardValues = null;
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        { 
            if (_StandardValues == null)
            {
                DataExcel grid = context.Instance as DataExcel;
                System.Drawing.Printing.PrinterSettings.PaperSizeCollection pss = grid.PrintDocument.PrinterSettings.PaperSizes;
                List<string> list = new List<string>();
                foreach (System.Drawing.Printing.PaperSize ps in pss)
                {
                    list.Add(ps.PaperName);
                }
                list.Sort();
                _StandardValues = new StandardValuesCollection(list);
            }
            return _StandardValues;
        }
    }

    public class PrintNameConverter : StringValuesConverter
    {
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(System.Drawing.Printing.PrinterSettings.InstalledPrinters);
        } 
    }

    public abstract class StringValuesConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
                return value as string;
            return base.ConvertTo(context, culture, value, destinationType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
                return value as string;
            return base.ConvertFrom(context, culture, value);
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }
    }
}
