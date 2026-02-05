using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Diagnostics;
using System.ComponentModel;
using System.Globalization;
using System.Collections;
using System.Drawing.Printing;

namespace Feng.Excel.Designer
{
    //public class EnumTypeConverter : EnumConverter
    //{
    //    protected class HashEntry
    //    {
    //        public string[] names;
    //        public string[] displayNames;
    //        public HashEntry(Type enumType, Type resourceFinder, string resourceFile)
    //        {
    //            names = Enum.GetNames(enumType);
    //            Array values = Enum.GetValues(enumType);
    //            displayNames = new string[names.Length];

    //        }
    //        public string NameToDisplayName(string name)
    //        {
    //            int index = Array.IndexOf(names, name.Trim());
    //            return index >= 0 ? displayNames[index] : null;
    //        }
    //        public string DisplayNameToName(string displayName)
    //        {
    //            int index = Array.IndexOf(displayNames, displayName.Trim());
    //            return index >= 0 ? names[index] : null;
    //        }

    //        static string GetResourceName(object enumValue)
    //        {
    //            return string.Concat(enumValue.GetType().FullName, ".", enumValue);
    //        }
    //    }
    //    #region static
    //    protected static Dictionary<Type, HashEntry> data = new Dictionary<Type, HashEntry>();
    //    static string[] GetDisplayNames(Type enumType, string[] names)
    //    {
    //        List<string> result = new List<string>();
    //        foreach (string item in names)
    //        {
    //            string s = GetDisplayName(enumType, item);
    //            if (!string.IsNullOrEmpty(s))
    //                result.Add(s);
    //        }
    //        return result.ToArray();
    //    }
    //    static string GetDisplayName(Type enumType, string name)
    //    {
    //        HashEntry entry;
    //        return data.TryGetValue(enumType, out entry) ? entry.NameToDisplayName(name) : null;
    //    }
    //    static string[] GetNames(Type enumType, string[] displayNames)
    //    {
    //        List<string> result = new List<string>();
    //        foreach (string item in displayNames)
    //        {
    //            string s = GetName(enumType, item);
    //            if (!string.IsNullOrEmpty(s))
    //                result.Add(s);
    //        }
    //        return result.ToArray();
    //    }
    //    static string GetName(Type enumType, string displayName)
    //    {
    //        HashEntry entry;
    //        return data.TryGetValue(enumType, out entry) ? entry.DisplayNameToName(displayName) : null;
    //    }
    //    protected static void Initialize(Type enumType)
    //    {

    //    }
    //    protected static void Initialize(Type enumType, Type resourceFinder, string resourceFile)
    //    {
    //        if (!data.ContainsKey(enumType))
    //            data.Add(enumType, new HashEntry(enumType, resourceFinder, resourceFile));
    //    }
    //    #endregion
    //    public EnumTypeConverter(Type type)
    //        : base(type)
    //    {
    //    }
    //    public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
    //    {
    //        if (context != null && value is string)
    //            value = DisplayNameToName(this.EnumType, (string)value);
    //        return base.ConvertFrom(context, culture, value);
    //    }
    //    protected string DisplayNameToName(Type enumType, string displayName)
    //    {
    //        displayName = displayName.Trim();
    //        InitializeInternal(enumType);
    //        if (IsFlagsDefined(enumType))
    //        {
    //            string[] names = GetNames(enumType, displayName.Split(','));
    //            return string.Join(",", names);
    //        }
    //        return GetName(enumType, displayName);
    //    }
    //    bool IsFlagsDefined(Type type)
    //    {
    //        return type.IsDefined(typeof(FlagsAttribute), false);
    //    }
    //    protected virtual void InitializeInternal(Type enumType)
    //    {
    //        Initialize(enumType);
    //    }
    //    public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
    //    {
    //        if (context != null && destinationType == typeof(string))
    //        {
    //            if (value is string)
    //                return value;
    //            string s = (string)base.ConvertTo(context, culture, value, destinationType);
    //            return NameToDisplayName(this.EnumType, s);
    //        }
    //        return base.ConvertTo(context, culture, value, destinationType);
    //    }
    //    protected string NameToDisplayName(Type enumType, string name)
    //    {
    //        InitializeInternal(enumType);
    //        if (IsFlagsDefined(enumType))
    //        {
    //            string[] displayNames = GetDisplayNames(enumType, name.Split(','));
    //            return string.Join(",", displayNames);
    //        }
    //        return GetDisplayName(enumType, name);
    //    }
    //}
    //public class PaperKindConverter : EnumTypeConverter
    //{
    //    #region inner classes
    //    protected class PaperKindComparer : IComparer
    //    {
    //        public int Compare(object x, object y)
    //        {
    //            return System.Collections.Comparer.Default.Compare(x.ToString(), y.ToString());
    //        }
    //    }
    //    #endregion
    //    #region static
    //    static IComparer comparer = new PaperKindComparer();
    //    #endregion
    //    protected override IComparer Comparer
    //    {
    //        get { return comparer; }
    //    }
    //    public PaperKindConverter(Type type)
    //        : base(type)
    //    {
    //    }
    //    protected override void InitializeInternal(Type enumType)
    //    {
    //        //if (!data.ContainsKey(enumType))
    //        //    Initialize(enumType, typeof(ResFinder), DXDisplayNameAttribute.DefaultResourceFile);
    //    }
    //}
    //public class ReportPaperKindConverter : PaperKindConverter
    //{
    //    #region static
    //    static PaperKind[] GetValues(string printerName)
    //    {
    //        PrinterSettings sets = new PrinterSettings();
    //        sets.PrinterName = printerName;
    //        ArrayList values = new ArrayList();
    //        foreach (PaperSize paperSize in sets.PaperSizes)
    //        {
    //            if (!values.Contains(paperSize.Kind))
    //                values.Add(paperSize.Kind);
    //        }
    //        return (PaperKind[])values.ToArray(typeof(PaperKind));
    //    }
    //    #endregion
    //    public ReportPaperKindConverter(Type type)
    //        : base(type)
    //    {
    //    }
    //    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    //    {
    //        return base.CanConvertFrom(context, sourceType);
    //    }
    //    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    //    {
    //        return base.ConvertTo(context, culture, value, destinationType);
    //    }
    //    public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
    //    {
    //        return base.GetStandardValuesSupported(context);
    //    }
    //    public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
    //    {
    //        DataExcel grid = context.Instance as DataExcel;
    //        if (grid != null && grid.PrinterName.Length > 0)
    //        {
    //            InitializeInternal(context.PropertyDescriptor.PropertyType);
    //            PaperKind[] paperKinds = GetValues(grid.PrinterName);
    //            if (paperKinds.Length > 0)
    //            {
    //                if (Comparer != null)
    //                    Array.Sort(paperKinds, 0, paperKinds.Length, Comparer);
    //                return new StandardValuesCollection(paperKinds);
    //            }
    //        }
    //        return base.GetStandardValues(context);
    //    }
    //}

    public class ReportPaperKindConverter : EnumConverter
    {
        public ReportPaperKindConverter(Type type)
            :base(type)
        {

        }
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return base.CanConvertTo(context, destinationType);
        }
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            return base.ConvertFrom(context, culture, value);
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            return base.CreateInstance(context, propertyValues);
        }
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return base.GetCreateInstanceSupported(context);
        }
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return base.GetProperties(context, value, attributes);
        }
        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return base.GetPropertiesSupported(context);
        }
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return base.GetStandardValues(context);
        }
        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return base.GetStandardValuesExclusive(context);
        }
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return base.GetStandardValuesSupported(context);
        }
        public override bool IsValid(ITypeDescriptorContext context, object value)
        {
            return base.IsValid(context, value);
        }
    }
}
