using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Utils
{
    public class TextHelper
    {
        public static bool Contains(string text, string keyword, bool caseSensitiveMatch, bool fullWordMatch)
        {
            if (!fullWordMatch)
            {
                return Contains(text, keyword, caseSensitiveMatch);
            }
            else 
            {
                string[] words = text.Split(new[] { ' ', ',', '.', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in words)
                {
                    if (caseSensitiveMatch)
                    {
                        if (word == keyword)
                        {
                            return true; 
                        }
                    }
                    else
                    {
                        if (word.ToLower() == keyword.ToLower())
                        {
                            return true; 
                        }
                    }
                }
            }
            return false;
        }
        public static bool Contains(string text, string keyword, bool caseSensitiveMatch)
        {
            if (caseSensitiveMatch)
            {
                return text.Contains(keyword);
            }
            else
            {
                string txt = text.ToLower();
                string key = keyword.ToLower();
                return txt.Contains(key);
            }
        }

        public static string ReplaceIgnoreCase(string text, string oldValue, string newValue, bool caseSensitiveMatch)
        {
            int index = 0;
            string str = text;
            if (caseSensitiveMatch)
            {
                while ((index = str.IndexOf(oldValue, index)) != -1)
                {
                    str = str.Remove(index, oldValue.Length).Insert(index, newValue);
                    index += newValue.Length;
                }
            }
            else
            {
                while ((index = str.IndexOf(oldValue, index, StringComparison.OrdinalIgnoreCase)) != -1)
                {
                    str = str.Remove(index, oldValue.Length).Insert(index, newValue);
                    index += newValue.Length;
                }
            }
            return str;
        }
 
        public static string Format(object value, FormatType formattype, string format)
        {
            if (value == null)
                return string.Empty;
            switch (formattype)
            {
                case FormatType.Null:
                    Type t = value.GetType();
                    if (Feng.Utils.ConvertHelper.IsNumber(t))
                    {
                        return FormatNumber(value, format);
                    }
                    if (Feng.Utils.ConvertHelper.IsDateTime(t))
                    {
                        return FormatDateTime(value, format);
                    }
                    return value.ToString();
                case FormatType.Numberic:
                    return FormatNumber(value, format);
                case FormatType.DateTime:
                    return FormatDateTime(value, format);
                default:
                    break;
            }
            return string.Empty;
        }

        public static string RemoveStart(string url, string v)
        {
            if (url.StartsWith(v))
            {
                return url.Substring(v.Length, url.Length - v.Length);
            }
            return url;
        }

        public static string RemoveLast(string url, string v)
        {
            int index = url.LastIndexOf(v);
            if(index>=0)
            {
                return url.Substring(0, index);
            }
            return url ;
        }

        public static string Comine(string url1, string url2)
        {
            if (url1.EndsWith("/"))
            {
                if (url2.StartsWith("/"))
                {
                    return url1 + url2.TrimStart('/');
                }
                return url1 + url2;
            }
            else
            {
                if (url2.StartsWith("/"))
                {
                    return url1 + url2;
                }
                return url1 + "/" + url2;

            }
        }

        public static string Trope(string v)
        {
            v = v.Replace("\\", "\\\\");
            v = v.Replace("\"", "\\\"");
            return v;
        }

        private static string FormatDateTime(object value, string format)
        {
            Type t = value.GetType();
            if (Feng.Utils.ConvertHelper.IsDateTime(t))
            {
                return FormatDateTime(t, value, format);
            }
            else
            {
                string text = value.ToString();
                DateTime d = DateTime.Now;
                if (DateTime.TryParse(text, out d))
                {
                    return string.Format(format, d);
                }
            }
            return string.Empty;
        }

        public static bool StartEqual(string text, int v1, string v2)
        {
            if (text.Length < v1)
                return false;
            string t = text.Substring(0, v1);
            return string.Equals(t, v2);
        }

        private static string FormatDateTime(Type t, object value, string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "{0:yyyy-MM-dd}";
            }
            if (t == typeof(DateTime))
            {
                return string.Format(format, (DateTime)value);
            }

            if (t == typeof(DateTime?))
            {
                DateTime? obj = (DateTime?)value;
                if (obj.HasValue)
                {
                    return string.Format(format, obj.Value);
                }
            }
            return value.ToString();
        }
        private static string FormatNumber(object value, string format)
        {
            if (string.IsNullOrEmpty(format))
            {
                format = "{0:#.##}";
            }
            Type t = value.GetType();
            if (Feng.Utils.ConvertHelper.IsNumber(t))
            {
                return FormatNumber(t, value, format);
            }
            else
            {
                string text = value.ToString();
                Decimal d = 0;
                if (decimal.TryParse(text, out d))
                {
                    return string.Format(format, d);
                }
            }
            return string.Empty;
        }
        private static string FormatNumber(Type t, object value, string format)
        {
            if (t == typeof(Int16))
            {
                return string.Format(format, ((Int16)value));
            }
            if (t == typeof(Int32))
            {
                return string.Format(format, ((Int32)value));
            }
            if (t == typeof(Int64))
            {
                return string.Format(format, ((Int64)value));
            }
            if (t == typeof(UInt16))
            {
                return string.Format(format, ((UInt16)value));
            }
            if (t == typeof(UInt32))
            {
                return string.Format(format, ((UInt32)value));
            }
            if (t == typeof(UInt64))
            {
                return string.Format(format, ((UInt64)value));
            }
            if (t == typeof(float))
            {
                return string.Format(format, ((float)value));
            }
            if (t == typeof(double))
            {
                return string.Format(format, ((double)value));
            }
            if (t == typeof(decimal))
            {
                return string.Format(format, (decimal)value);
            }
            if (t == typeof(byte))
            {
                return string.Format(format, ((byte)value));
            }
            if (t == typeof(sbyte))
            {
                return string.Format(format, ((sbyte)value));
            }
            //-------------------------
            if (t == typeof(Int16?))
            {
                return ((Int16?)value).HasValue ? string.Format(format, ((Int16?)value).Value) : string.Empty;
            }
            if (t == typeof(Int32))
            {
                return ((Int32?)value).HasValue ? string.Format(format, ((Int32?)value).Value) : string.Empty;
            }
            if (t == typeof(Int64))
            {
                return ((Int64?)value).HasValue ? string.Format(format, ((Int64?)value).Value) : string.Empty;
            }
            if (t == typeof(UInt16))
            {
                return ((UInt16?)value).HasValue ? string.Format(format, ((UInt16?)value).Value) : string.Empty;
            }
            if (t == typeof(UInt32))
            {
                return ((UInt32?)value).HasValue ? string.Format(format, ((UInt32?)value).Value) : string.Empty;
            }
            if (t == typeof(UInt64))
            {
                return ((UInt64?)value).HasValue ? string.Format(format, ((UInt64?)value).Value) : string.Empty;
            }
            if (t == typeof(float))
            {
                return ((float?)value).HasValue ? string.Format(format, ((float?)value).Value) : string.Empty;
            }
            if (t == typeof(double))
            {
                return ((double?)value).HasValue ? string.Format(format, ((double?)value).Value) : string.Empty;
            }
            if (t == typeof(decimal))
            {
                return ((decimal?)value).HasValue ? string.Format(format, ((decimal?)value).Value) : string.Empty;
            }
            if (t == typeof(byte))
            {
                return ((byte?)value).HasValue ? string.Format(format, ((byte?)value).Value) : string.Empty;
            }
            if (t == typeof(sbyte))
            {
                return ((sbyte?)value).HasValue ? string.Format(format, ((sbyte?)value).Value) : string.Empty;
            }
            return value.ToString();
        }

        public static string[] Split(string text, string v)
        {
            return text.Split(v.ToCharArray());
        }
        public static string[] Split(string text)
        {
            string v = ",;|，";
            return text.Split(v.ToCharArray());
        }
        public static List<int> SplitToInt(string text, string v)
        {
            string[] values = text.Split(v.ToCharArray());
            List<int> list = new List<int>();
            foreach (string item in values)
            {
                list.Add(Feng.Utils.ConvertHelper.ToInt32(item.Trim()));
            }
            return list;
        }

        public static void SplitToIntMinMax(string text, string v,ref int min,ref int max)
        {
            string[] values = text.Split(v.ToCharArray());
            if (values.Length == 2)
            {
                min = Feng.Utils.ConvertHelper.ToInt32(values[0].Trim());
                max = Feng.Utils.ConvertHelper.ToInt32(values[1].Trim());
                return;
            }
            min = Feng.Utils.ConvertHelper.ToInt32(text.Trim());

        }

        public static string ToUTF8(string text)
        {
            string result;
            byte[] buffer = Encoding.Default.GetBytes(text);
            result = Encoding.UTF8.GetString(buffer);
            return result;
        }


        public static string GetArraryIndex(string[] values, int index)
        {
            if (index < values.Length)
            {
                return values[index];
            }
            return string.Empty;
        }

        public static string GetArraryIndex(string[] values, int index,string defaultvalue)
        {
            if (index < values.Length)
            {
                return values[index];
            }
            return defaultvalue;
        }
        public static string GetArraryIndex(string value ,string sysfol, int index)
        {
            string[] values = Split(value, sysfol);
            return GetArraryIndex(values, index);
        }
    }
}
