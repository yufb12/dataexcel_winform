using System;
using System.Collections.Generic;
using System.Text;
using Feng.IO;
using System.Drawing; 

namespace Feng.Utils
{ 
  
    public class ConvertHelper
    {
        public static bool IsNullOrStringEmpty(object value)
        {
            if (value == null)
                return true;
            string text = ToString(value);
            if (string.IsNullOrWhiteSpace(text))
            {
                return true;
            }
            return false;
        }
        public static string ToFileSize(long length)
        {  
            int g = 1024 * 1024 * 1024;
            int m = 1024 * 1024;
            int k = 1024;
            if (length / g >= 1)
            {
                return ((length * 100 / g) * 0.01d).ToString("#0.00") + "GB";
            }
            else if (length / m >= 1)
            {
                return ((length * 100 / m) * 0.01d).ToString("#0.00") + "MB";
            }
            else if (length / k >= 1)
            {
                return ((length * 100 / k) * 0.01d).ToString("#0.00") + "KB";
            }
            else
            {
                return length.ToString() + "B";
            }
        }
        public static DateTime ToDateTime(DateTime? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return obj.Value;
                }
            }
            return DateTime.MinValue;
        }

        public static DateTime ToDateTime(byte[] obj)
        {
            using (BufferReader reader = new BufferReader(obj))
            {
                DateTime dt = reader.ReadDateTime(); 
                return dt;
            }
          
        }
        public static DateTime ToDateTime(object obj)
        { 
            if (obj != null)
            {
                DateTime dt = DateTime.MinValue;
                if (DateTime.TryParse(obj.ToString(), out dt))
                {
                    return dt;
                }
            }
            return DateTime.MinValue;
        }
        public static DateTime ToDateTime(object obj,DateTime defaulttime)
        {
            if (obj != null)
            {
                DateTime dt = DateTime.MinValue;
                if (DateTime.TryParse(obj.ToString(), out dt))
                {
                    return dt;
                }
            }
            return defaulttime;
        }
        public static DateTime ToDateTime(object obj, bool throwex)
        {
            if (obj != null)
            {
                DateTime dt = DateTime.MinValue;
                if (DateTime.TryParse(obj.ToString(), out dt))
                {
                    return dt;
                }
            }
            if (throwex)
            {
                return Convert.ToDateTime(obj);
            } 
            return DateTime.MinValue;
        }
        public static DateTime? ToDateTimeNullable(object obj)
        {
            if (obj != null)
            {
                if (obj is DateTime)
                {
                    return (DateTime)obj;
                } 
                DateTime dt = DateTime.MinValue;
                if (DateTime.TryParse(obj.ToString(), out dt))
                {
                    return dt;
                }
            }
            return null;
        }
        public static DateTime? ToDateTime(object obj, DateTime? value)
        {
            if (obj != null)
            {
                DateTime dt = DateTime.MinValue;
                if (DateTime.TryParse(obj.ToString(), out dt))
                {
                    return dt;
                }
            }
            return value;
        }
        public static string ToString(byte[] obj)
        {
            return Feng.IO.BitConver.GetString(obj);
        }
        public static string ToString(int obj)
        {
            return obj.ToString();
        }
        public static string ToString(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            return obj.ToString();
        }
        public static string ToString(object obj,string nulltext)
        {
            if (obj == null)
            {
                return nulltext;
            }
            return obj.ToString();
        }
        public static string ToString(DateTime? obj)
        {
            return ToString(obj, "yyyy-MM-dd HH:mm:ss");
        }
        public static string ToString(decimal obj)
        {
            return obj.ToString("#");
        }
 
        public static string ToString(int obj, int decimalplace)
        {
            return "0x" + Convert.ToString(obj, 16);
        }
        public static string ToString(decimal? obj)
        {
            decimal d = ConvertHelper.ToDecimal(obj);
            return d.ToString("#.00");
        }
        public static string ToString(int? obj)
        {
            if (obj.HasValue)
            {
                return obj.Value.ToString();
            }
            return "0";
        }
        public static string ToString(decimal? obj,int decimalplace)
        {
            decimal d = ConvertHelper.ToDecimal(obj);
            return d.ToString("#.".PadRight(decimalplace, '0'));
        }
        public static string ToString(DateTime? obj,string dateformat)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return obj.Value.ToString(dateformat);
                }
            }
            return DateTime.MinValue.ToString(dateformat);
        }
        public static string BoolToString(bool value)
        {
            if (value)
            {
                return Feng.Utils.Constants.StringTrue;
            }
            else
            {
                return Feng.Utils.Constants.StringFalse;
            }
        }
        public static string ToString(bool value)
        {
            if (value)
            {
                return Feng.Utils.Constants.StringTrue;
            }
            else
            {
                return Feng.Utils.Constants.StringFalse;
            }
        }
        public static decimal ToDecimal(sbyte obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static decimal ToDecimal(sbyte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return 0;
        }
        public static decimal ToDecimal(byte obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static decimal ToDecimal(byte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return 0;
        }
 
        public static decimal ToDecimal(Int16 obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static decimal ToDecimal(Int16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return 0;
        }
        public static decimal ToDecimal(Int32 obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static decimal ToDecimal(Int32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return 0;
        }
        public static decimal ToDecimal(Int64 obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static decimal ToDecimal(Int64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return 0;
        }
        public static decimal ToDecimal(UInt16 obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static decimal ToDecimal(UInt16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return 0;
        }
        public static decimal ToDecimal(UInt32 obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static decimal ToDecimal(UInt32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return 0;
        }
        public static decimal ToDecimal(UInt64 obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static decimal ToDecimal(UInt64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return 0;
        }
        public static decimal ToDecimal(decimal obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static decimal ToDecimal(decimal? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return 0;
        }
        public static decimal ToDecimal(float obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static decimal ToDecimal(float? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return 0;
        }
        public static decimal ToDecimal(double obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static decimal ToDecimal(double? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return 0;
        }
        public static decimal ToDecimal(char obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static decimal ToDecimal(char? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return 0;
        }
        public static decimal ToDecimal(DateTime obj)
        {
            return Convert.ToDecimal(obj);
        }
        public static decimal ToDecimal(DateTime? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return 0;
        }
        public static decimal ToDecimal(string obj)
        {
            decimal d = 0;
            if (decimal.TryParse((string)obj, out d))
            {
                return d;
            }
            return d;
        }
        public static decimal TryToDecimal(string value,ref bool res)
        {
            decimal d = 0;
            res=decimal.TryParse(value, out d);
            return d;
        }
        public static decimal ToDecimal(object obj, bool throwException)
        {

            if (obj == null)
            {

            }
            else
            {
                Type t = obj.GetType();
                if (t == typeof(sbyte))
                {
                    return Convert.ToDecimal((sbyte)obj);
                }
                else if (t == typeof(byte))
                {
                    return Convert.ToDecimal((byte)obj);
                }
                else if (t == typeof(Int16))
                {
                    return Convert.ToDecimal((Int16)obj);
                }
                else if (t == typeof(Int32))
                {
                    return Convert.ToDecimal((Int32)obj);
                }
                else if (t == typeof(Int64))
                {
                    return Convert.ToDecimal((Int64)obj);
                }
                else if (t == typeof(UInt16))
                {
                    return Convert.ToDecimal((UInt16)obj);
                }
                else if (t == typeof(UInt32))
                {
                    return Convert.ToDecimal((UInt32)obj);
                }
                else if (t == typeof(UInt64))
                {
                    return Convert.ToDecimal((Int64)obj);
                }
                else if (t == typeof(decimal))
                {
                    return Convert.ToDecimal((decimal)obj);
                }
                else if (t == typeof(float))
                {
                    return Convert.ToDecimal((float)obj);
                }
                else if (t == typeof(double))
                {
                    return Convert.ToDecimal((double)obj);
                }
                else if (t == typeof(string))
                {
                    decimal d = 0;
                    if (decimal.TryParse((string)obj, out d))
                    {
                        return d;
                    }
                }
                else if (t == typeof(char))
                {
                    return Convert.ToDecimal((char)obj);
                }
                else if (t == typeof(DateTime))
                {
                    return Convert.ToDecimal(((DateTime)obj).Ticks);
                }
                else if (t == typeof(sbyte?))
                {
                    return Convert.ToDecimal((sbyte?)obj);
                }
                else if (t == typeof(byte?))
                {
                    return Convert.ToDecimal((byte?)obj);
                }
                else if (t == typeof(Int16?))
                {
                    return Convert.ToDecimal((Int16?)obj);
                }
                else if (t == typeof(Int32?))
                {
                    return Convert.ToDecimal((Int32?)obj);
                }
                else if (t == typeof(Int64?))
                {
                    return Convert.ToDecimal((Int64?)obj);
                }
                else if (t == typeof(UInt16?))
                {
                    return Convert.ToDecimal((UInt16?)obj);
                }
                else if (t == typeof(UInt32?))
                {
                    return Convert.ToDecimal((UInt32?)obj);
                }
                else if (t == typeof(UInt64?))
                {
                    return Convert.ToDecimal((Int64?)obj);
                }
                else if (t == typeof(decimal?))
                {
                    return Convert.ToDecimal((decimal?)obj);
                }
                else if (t == typeof(float?))
                {
                    return Convert.ToDecimal((float?)obj);
                }
                else if (t == typeof(double?))
                {
                    return Convert.ToDecimal((double?)obj);
                }
            }
            if (throwException)
            {
                return Convert.ToDecimal(obj);
            }
            else
            {
                if (obj == null)
                {
                    return 0;
                }
                decimal d = 0;
                decimal.TryParse(obj.ToString(), out d);
                return d;
            }
        }
        public static decimal ToDecimal(object obj, decimal defaultvalue)
        { 
            if (obj == null)
            {
                return defaultvalue;
            }

            Type t = obj.GetType();
            if (t == typeof(sbyte))
            {
                return Convert.ToDecimal((sbyte)obj);
            }
            else if (t == typeof(byte))
            {
                return Convert.ToDecimal((byte)obj);
            }
            else if (t == typeof(Int16))
            {
                return Convert.ToDecimal((Int16)obj);
            }
            else if (t == typeof(Int32))
            {
                return Convert.ToDecimal((Int32)obj);
            }
            else if (t == typeof(Int64))
            {
                return Convert.ToDecimal((Int64)obj);
            }
            else if (t == typeof(UInt16))
            {
                return Convert.ToDecimal((UInt16)obj);
            }
            else if (t == typeof(UInt32))
            {
                return Convert.ToDecimal((UInt32)obj);
            }
            else if (t == typeof(UInt64))
            {
                return Convert.ToDecimal((Int64)obj);
            }
            else if (t == typeof(decimal))
            {
                return Convert.ToDecimal((decimal)obj);
            }
            else if (t == typeof(float))
            {
                return Convert.ToDecimal((float)obj);
            }
            else if (t == typeof(double))
            {
                return Convert.ToDecimal((double)obj);
            } 
            else if (t == typeof(char))
            {
                return Convert.ToDecimal((char)obj);
            }
            else if (t == typeof(DateTime))
            {
                return Convert.ToDecimal(((DateTime)obj).Ticks);
            }
            else if (t == typeof(sbyte?))
            {
                return Convert.ToDecimal((sbyte?)obj);
            }
            else if (t == typeof(byte?))
            {
                return Convert.ToDecimal((byte?)obj);
            }
            else if (t == typeof(Int16?))
            {
                return Convert.ToDecimal((Int16?)obj);
            }
            else if (t == typeof(Int32?))
            {
                return Convert.ToDecimal((Int32?)obj);
            }
            else if (t == typeof(Int64?))
            {
                return Convert.ToDecimal((Int64?)obj);
            }
            else if (t == typeof(UInt16?))
            {
                return Convert.ToDecimal((UInt16?)obj);
            }
            else if (t == typeof(UInt32?))
            {
                return Convert.ToDecimal((UInt32?)obj);
            }
            else if (t == typeof(UInt64?))
            {
                return Convert.ToDecimal((Int64?)obj);
            }
            else if (t == typeof(decimal?))
            {
                return Convert.ToDecimal((decimal?)obj);
            }
            else if (t == typeof(float?))
            {
                return Convert.ToDecimal((float?)obj);
            }
            else if (t == typeof(double?))
            {
                return Convert.ToDecimal((double?)obj);
            }

            decimal d = 0;
            if (decimal.TryParse(obj.ToString(), out d))
            {
                return d;
            }
            return defaultvalue;

        }
        public static decimal ToDecimal(object obj)
        {
            return ToDecimal(obj, false);
        }
        public static decimal ToDecimal(byte[] obj)
        {
            using (BufferReader reader = new BufferReader(obj))
            {
                return reader.ReadDecimal();
            }
        }


        #region DECIMAL NULL ABLE

        public static decimal? ToDecimalNullable(object obj)
        {
            if (obj == null)
                return null;
            return ToDecimalNullable(obj.ToString());
        }

        public static decimal? ToDecimalNullable(string obj)
        {
            decimal d = decimal.Zero;
            if (decimal.TryParse(obj, out d))
            {
                return d;
            }
            return null;
        }

        public static decimal? ToDecimalNullable(sbyte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return null;
        }

        public static decimal? ToDecimalNullable(byte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return null;
        }

        public static decimal? ToDecimalNullable(Int16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return null;
        }

        public static decimal? ToDecimalNullable(Int32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return null;
        }

        public static decimal? ToDecimalNullable(Int64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return null;
        }

        public static decimal? ToDecimalNullable(UInt16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return null;
        }

        public static decimal? ToDecimalNullable(UInt32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return null;
        }

        public static decimal? ToDecimalNullable(UInt64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return null;
        }

        public static decimal? ToDecimalNullable(decimal? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return null;
        }

        public static decimal? ToDecimalNullable(float? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return null;
        }

        public static decimal? ToDecimalNullable(double? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return null;
        }

        public static decimal? ToDecimalNullable(char? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDecimal(obj.Value);
                }
            }
            return null;
        }
  

        #endregion

        public static bool ToBoolean(object obj, bool throwException)
        {

            if (obj == null)
            {
                return false;
            }
            if (obj is Boolean)
            {
                return (Boolean)obj;
            }
            Type t = obj.GetType();
            if (t == typeof(sbyte))
            {
                return Convert.ToBoolean((sbyte)obj);
            }
            else if (t == typeof(byte))
            {
                return Convert.ToBoolean((byte)obj);
            }
            else if (t == typeof(Int16))
            {
                return Convert.ToBoolean((Int16)obj);
            }
            else if (t == typeof(Int32))
            {
                return Convert.ToBoolean((Int32)obj);
            }
            else if (t == typeof(Int64))
            {
                return Convert.ToBoolean((Int64)obj);
            }
            else if (t == typeof(UInt16))
            {
                return Convert.ToBoolean((UInt16)obj);
            }
            else if (t == typeof(UInt32))
            {
                return Convert.ToBoolean((UInt32)obj);
            }
            else if (t == typeof(UInt64))
            {
                return Convert.ToBoolean((Int64)obj);
            }
            else if (t == typeof(decimal))
            {
                return Convert.ToBoolean((decimal)obj);
            }
            else if (t == typeof(float))
            {
                return Convert.ToBoolean((float)obj);
            }
            else if (t == typeof(double))
            {
                return Convert.ToBoolean((double)obj);
            }
            else if (t == typeof(string))
            {
                bool d = false;
                if (bool.TryParse((string)obj, out d))
                {
                    return d;
                }
                return false;
            }
            else if (t == typeof(char))
            {
                return Convert.ToBoolean((char)obj);
            }
            else if (t == typeof(DateTime))
            {
                return Convert.ToBoolean((DateTime)obj);
            }
            else if (t == typeof(sbyte?))
            {
                return Convert.ToBoolean((sbyte?)obj);
            }
            else if (t == typeof(byte?))
            {
                return Convert.ToBoolean((byte?)obj);
            }
            else if (t == typeof(Int16?))
            {
                return Convert.ToBoolean((Int16?)obj);
            }
            else if (t == typeof(Int32?))
            {
                return Convert.ToBoolean((Int32?)obj);
            }
            else if (t == typeof(Int64?))
            {
                return Convert.ToBoolean((Int64?)obj);
            }
            else if (t == typeof(UInt16?))
            {
                return Convert.ToBoolean((UInt16?)obj);
            }
            else if (t == typeof(UInt32?))
            {
                return Convert.ToBoolean((UInt32?)obj);
            }
            else if (t == typeof(UInt64?))
            {
                return Convert.ToBoolean((Int64?)obj);
            }
            else if (t == typeof(decimal?))
            {
                return Convert.ToBoolean((decimal?)obj);
            }
            else if (t == typeof(float?))
            {
                return Convert.ToBoolean((float?)obj);
            }
            else if (t == typeof(double?))
            {
                return Convert.ToBoolean((double?)obj);
            }
            if (throwException)
            {
                return Convert.ToBoolean(obj);
            }
            else
            { 
                bool d = false;
                bool.TryParse(obj.ToString(), out d);
                return d;
            }
        }
        public static bool ToBoolean(object obj)
        {
            return ToBoolean(obj, false);
        }
        public static bool ToBoolean(byte[] obj)
        {
            return BitConverter.ToBoolean(obj, 0);
        }
        public static sbyte ToSByte(object obj, bool throwException)
        {

            if (obj == null)
            {

            }
            else
            {
                Type t = obj.GetType();
                if (t == typeof(sbyte))
                {
                    return Convert.ToSByte((sbyte)obj);
                }
                else if (t == typeof(byte))
                {
                    return Convert.ToSByte((byte)obj);
                }
                else if (t == typeof(Int16))
                {
                    return Convert.ToSByte((Int16)obj);
                }
                else if (t == typeof(Int32))
                {
                    return Convert.ToSByte((Int32)obj);
                }
                else if (t == typeof(Int64))
                {
                    return Convert.ToSByte((Int64)obj);
                }
                else if (t == typeof(UInt16))
                {
                    return Convert.ToSByte((UInt16)obj);
                }
                else if (t == typeof(UInt32))
                {
                    return Convert.ToSByte((UInt32)obj);
                }
                else if (t == typeof(UInt64))
                {
                    return Convert.ToSByte((Int64)obj);
                }
                else if (t == typeof(decimal))
                {
                    return Convert.ToSByte((decimal)obj);
                }
                else if (t == typeof(float))
                {
                    return Convert.ToSByte((float)obj);
                }
                else if (t == typeof(double))
                {
                    return Convert.ToSByte((double)obj);
                }
                else if (t == typeof(string))
                {
                    sbyte d = 0;
                    if (sbyte.TryParse((string)obj, out d))
                    {
                        return d;
                    }
                }
                else if (t == typeof(char))
                {
                    return Convert.ToSByte((char)obj);
                }
                else if (t == typeof(DateTime))
                {
                    return Convert.ToSByte((DateTime)obj);
                }
                else if (t == typeof(sbyte?))
                {
                    return Convert.ToSByte((sbyte?)obj);
                }
                else if (t == typeof(byte?))
                {
                    return Convert.ToSByte((byte?)obj);
                }
                else if (t == typeof(Int16?))
                {
                    return Convert.ToSByte((Int16?)obj);
                }
                else if (t == typeof(Int32?))
                {
                    return Convert.ToSByte((Int32?)obj);
                }
                else if (t == typeof(Int64?))
                {
                    return Convert.ToSByte((Int64?)obj);
                }
                else if (t == typeof(UInt16?))
                {
                    return Convert.ToSByte((UInt16?)obj);
                }
                else if (t == typeof(UInt32?))
                {
                    return Convert.ToSByte((UInt32?)obj);
                }
                else if (t == typeof(UInt64?))
                {
                    return Convert.ToSByte((Int64?)obj);
                }
                else if (t == typeof(decimal?))
                {
                    return Convert.ToSByte((decimal?)obj);
                }
                else if (t == typeof(float?))
                {
                    return Convert.ToSByte((float?)obj);
                }
                else if (t == typeof(double?))
                {
                    return Convert.ToSByte((double?)obj);
                }
            }
            if (throwException)
            {
                return Convert.ToSByte(obj);
            }
            else
            {
                if (obj == null)
                {
                    return 0;
                }
                sbyte d = 0;
                sbyte.TryParse(obj.ToString(), out d);
                return d;
            }
        }
        public static sbyte ToSByte(object obj)
        {
            return ToSByte(obj, false);
        }
        public static byte ToByte(object obj, bool throwException)
        {

            if (obj == null)
            {

            }
            else
            {
                Type t = obj.GetType();
                if (t == typeof(sbyte))
                {
                    return Convert.ToByte((sbyte)obj);
                }
                else if (t == typeof(byte))
                {
                    return Convert.ToByte((byte)obj);
                }
                else if (t == typeof(Int16))
                {
                    return Convert.ToByte((Int16)obj);
                }
                else if (t == typeof(Int32))
                {
                    return Convert.ToByte((Int32)obj);
                }
                else if (t == typeof(Int64))
                {
                    return Convert.ToByte((Int64)obj);
                }
                else if (t == typeof(UInt16))
                {
                    return Convert.ToByte((UInt16)obj);
                }
                else if (t == typeof(UInt32))
                {
                    return Convert.ToByte((UInt32)obj);
                }
                else if (t == typeof(UInt64))
                {
                    return Convert.ToByte((Int64)obj);
                }
                else if (t == typeof(decimal))
                {
                    return Convert.ToByte((decimal)obj);
                }
                else if (t == typeof(float))
                {
                    return Convert.ToByte((float)obj);
                }
                else if (t == typeof(double))
                {
                    return Convert.ToByte((double)obj);
                }
                else if (t == typeof(string))
                {
                    byte d = 0;
                    if (byte.TryParse((string)obj, out d))
                    {
                        return d;
                    }
                }
                else if (t == typeof(char))
                {
                    return Convert.ToByte((char)obj);
                }
                else if (t == typeof(DateTime))
                {
                    return Convert.ToByte((DateTime)obj);
                }
                else if (t == typeof(sbyte?))
                {
                    return Convert.ToByte((sbyte?)obj);
                }
                else if (t == typeof(byte?))
                {
                    return Convert.ToByte((byte?)obj);
                }
                else if (t == typeof(Int16?))
                {
                    return Convert.ToByte((Int16?)obj);
                }
                else if (t == typeof(Int32?))
                {
                    return Convert.ToByte((Int32?)obj);
                }
                else if (t == typeof(Int64?))
                {
                    return Convert.ToByte((Int64?)obj);
                }
                else if (t == typeof(UInt16?))
                {
                    return Convert.ToByte((UInt16?)obj);
                }
                else if (t == typeof(UInt32?))
                {
                    return Convert.ToByte((UInt32?)obj);
                }
                else if (t == typeof(UInt64?))
                {
                    return Convert.ToByte((Int64?)obj);
                }
                else if (t == typeof(decimal?))
                {
                    return Convert.ToByte((decimal?)obj);
                }
                else if (t == typeof(float?))
                {
                    return Convert.ToByte((float?)obj);
                }
                else if (t == typeof(double?))
                {
                    return Convert.ToByte((double?)obj);
                }
            }
            if (throwException)
            {
                return Convert.ToByte(obj);
            }
            else
            {
                if (obj == null)
                {
                    return 0;
                }
                byte d = 0;
                byte.TryParse(obj.ToString(), out d);
                return d;
            }
        }
        public static byte ToByte(object obj)
        {
            return ToByte(obj, false);
        }
        public static Int16 ToInt16(object obj, bool throwException)
        {

            if (obj == null)
            {

            }
            else
            {
                Type t = obj.GetType();
                if (t == typeof(sbyte))
                {
                    return Convert.ToInt16((sbyte)obj);
                }
                else if (t == typeof(byte))
                {
                    return Convert.ToInt16((byte)obj);
                }
                else if (t == typeof(Int16))
                {
                    return Convert.ToInt16((Int16)obj);
                }
                else if (t == typeof(Int32))
                {
                    return Convert.ToInt16((Int32)obj);
                }
                else if (t == typeof(Int64))
                {
                    return Convert.ToInt16((Int64)obj);
                }
                else if (t == typeof(UInt16))
                {
                    return Convert.ToInt16((UInt16)obj);
                }
                else if (t == typeof(UInt32))
                {
                    return Convert.ToInt16((UInt32)obj);
                }
                else if (t == typeof(UInt64))
                {
                    return Convert.ToInt16((Int64)obj);
                }
                else if (t == typeof(decimal))
                {
                    return Convert.ToInt16((decimal)obj);
                }
                else if (t == typeof(float))
                {
                    return Convert.ToInt16((float)obj);
                }
                else if (t == typeof(double))
                {
                    return Convert.ToInt16((double)obj);
                }
                else if (t == typeof(string))
                {
                    Int16 d = 0;
                    if (Int16.TryParse((string)obj, out d))
                    {
                        return d;
                    }
                }
                else if (t == typeof(char))
                {
                    return Convert.ToInt16((char)obj);
                }
                else if (t == typeof(DateTime))
                {
                    return Convert.ToInt16((DateTime)obj);
                }
                else if (t == typeof(sbyte?))
                {
                    return Convert.ToInt16((sbyte?)obj);
                }
                else if (t == typeof(byte?))
                {
                    return Convert.ToInt16((byte?)obj);
                }
                else if (t == typeof(Int16?))
                {
                    return Convert.ToInt16((Int16?)obj);
                }
                else if (t == typeof(Int32?))
                {
                    return Convert.ToInt16((Int32?)obj);
                }
                else if (t == typeof(Int64?))
                {
                    return Convert.ToInt16((Int64?)obj);
                }
                else if (t == typeof(UInt16?))
                {
                    return Convert.ToInt16((UInt16?)obj);
                }
                else if (t == typeof(UInt32?))
                {
                    return Convert.ToInt16((UInt32?)obj);
                }
                else if (t == typeof(UInt64?))
                {
                    return Convert.ToInt16((Int64?)obj);
                }
                else if (t == typeof(decimal?))
                {
                    return Convert.ToInt16((decimal?)obj);
                }
                else if (t == typeof(float?))
                {
                    return Convert.ToInt16((float?)obj);
                }
                else if (t == typeof(double?))
                {
                    return Convert.ToInt16((double?)obj);
                }
            }
            if (throwException)
            {
                return Convert.ToInt16(obj);
            }
            else
            {
                if (obj == null)
                {
                    return 0;
                }
                Int16 d = 0;
                Int16.TryParse(obj.ToString(), out d);
                return d;
            }
        }

        public static Color ToColor(int v)
        {
            return Color.FromArgb(v);
        }

        public static Int16 ToInt16(object obj)
        {
            return ToInt16(obj, false);
        }
        public static Int16 ToShort(object obj)
        {
            return ToInt16(obj, false);
        }
        public static Int32 ToInt32(object obj, int defaultvalue, bool throwException)
        {

            if (obj != null)
            { 
                if (obj is System.Int32)
                {
                    return (int)obj;
                }
                Type t = obj.GetType();

                if (t == typeof(sbyte))
                {
                    return Convert.ToInt32((sbyte)obj);
                }
                else if (t == typeof(byte))
                {
                    return Convert.ToInt32((byte)obj);
                }
                else if (t == typeof(Int16))
                {
                    return Convert.ToInt32((Int16)obj);
                }
                else if (t == typeof(Int32))
                {
                    return Convert.ToInt32((Int32)obj);
                }
                else if (t == typeof(Int64))
                {
                    return Convert.ToInt32((Int64)obj);
                }
                else if (t == typeof(UInt16))
                {
                    return Convert.ToInt32((UInt16)obj);
                }
                else if (t == typeof(UInt32))
                {
                    return Convert.ToInt32((UInt32)obj);
                }
                else if (t == typeof(UInt64))
                {
                    return Convert.ToInt32((Int64)obj);
                }
                else if (t == typeof(decimal))
                {
                    return Convert.ToInt32((decimal)obj);
                }
                else if (t == typeof(float))
                {
                    return Convert.ToInt32((float)obj);
                }
                else if (t == typeof(double))
                {
                    return Convert.ToInt32((double)obj);
                }
                else if (t == typeof(string))
                {
                    Int32 d = 0;
                    if (Int32.TryParse((string)obj, out d))
                    {
                        return d;
                    }
                    return defaultvalue;
                }
                else if (t == typeof(char))
                {
                    return Convert.ToInt32((char)obj);
                }
                else if (t == typeof(DateTime))
                {
                    return Convert.ToInt32((DateTime)obj);
                }
                else if (t == typeof(sbyte?))
                {
                    return Convert.ToInt32((sbyte?)obj);
                }
                else if (t == typeof(byte?))
                {
                    return Convert.ToInt32((byte?)obj);
                }
                else if (t == typeof(Int16?))
                {
                    return Convert.ToInt32((Int16?)obj);
                }
                else if (t == typeof(Int32?))
                {
                    return Convert.ToInt32((Int32?)obj);
                }
                else if (t == typeof(Int64?))
                {
                    return Convert.ToInt32((Int64?)obj);
                }
                else if (t == typeof(UInt16?))
                {
                    return Convert.ToInt32((UInt16?)obj);
                }
                else if (t == typeof(UInt32?))
                {
                    return Convert.ToInt32((UInt32?)obj);
                }
                else if (t == typeof(UInt64?))
                {
                    return Convert.ToInt32((Int64?)obj);
                }
                else if (t == typeof(decimal?))
                {
                    return Convert.ToInt32((decimal?)obj);
                }
                else if (t == typeof(float?))
                {
                    return Convert.ToInt32((float?)obj);
                }
                else if (t == typeof(double?))
                {
                    return Convert.ToInt32((double?)obj);
                }
            }
            if (throwException)
            {
                return Convert.ToInt32(obj);
            }
            else
            {
                if (obj == null)
                {
                    return defaultvalue;
                }
                Int32 d = defaultvalue;
                Int32.TryParse(obj.ToString(), out d);
                return d;
            }
        }
        public static Int32 ToInt32(object obj)
        {
            return ToInt32(obj, 0, false);
        }
        public static Int32 ToInt32(object obj,int defaultvalue)
        {
            return ToInt32(obj, defaultvalue, false);
        }
        public static Int32 ToInt32(byte[] data)
        {
            return BitConverter.ToInt32(data,0);
        }
        public static Int32 ToInt(object obj)
        {
            return ToInt32(obj,0, false);
        }
        public static Int32 ToInt(object obj, int defaultvalue)
        {
            return ToInt32(obj, defaultvalue, false);
        }
        public static Int32 ToInt(string obj)
        {
            int o = 0;
            if (Int32.TryParse(obj, out o))
            {
                return o;
            }
            return o;
        }
        public static Int64 ToInt64(object obj, bool throwException)
        {

            if (obj == null)
            {

            }
            else
            {
                Type t = obj.GetType();
                if (t == typeof(sbyte))
                {
                    return Convert.ToInt64((sbyte)obj);
                }
                else if (t == typeof(byte))
                {
                    return Convert.ToInt64((byte)obj);
                }
                else if (t == typeof(Int16))
                {
                    return Convert.ToInt64((Int16)obj);
                }
                else if (t == typeof(Int32))
                {
                    return Convert.ToInt64((Int32)obj);
                }
                else if (t == typeof(Int64))
                {
                    return Convert.ToInt64((Int64)obj);
                }
                else if (t == typeof(UInt16))
                {
                    return Convert.ToInt64((UInt16)obj);
                }
                else if (t == typeof(UInt32))
                {
                    return Convert.ToInt64((UInt32)obj);
                }
                else if (t == typeof(UInt64))
                {
                    return Convert.ToInt64((Int64)obj);
                }
                else if (t == typeof(decimal))
                {
                    return Convert.ToInt64((decimal)obj);
                }
                else if (t == typeof(float))
                {
                    return Convert.ToInt64((float)obj);
                }
                else if (t == typeof(double))
                {
                    return Convert.ToInt64((double)obj);
                }
                else if (t == typeof(string))
                {
                    Int64 d = 0;
                    if (Int64.TryParse((string)obj, out d))
                    {
                        return d;
                    }
                }
                else if (t == typeof(char))
                {
                    return Convert.ToInt64((char)obj);
                }
                else if (t == typeof(DateTime))
                {
                    return Convert.ToInt64((DateTime)obj);
                }
                else if (t == typeof(sbyte?))
                {
                    return Convert.ToInt64((sbyte?)obj);
                }
                else if (t == typeof(byte?))
                {
                    return Convert.ToInt64((byte?)obj);
                }
                else if (t == typeof(Int16?))
                {
                    return Convert.ToInt64((Int16?)obj);
                }
                else if (t == typeof(Int32?))
                {
                    return Convert.ToInt64((Int32?)obj);
                }
                else if (t == typeof(Int64?))
                {
                    return Convert.ToInt64((Int64?)obj);
                }
                else if (t == typeof(UInt16?))
                {
                    return Convert.ToInt64((UInt16?)obj);
                }
                else if (t == typeof(UInt32?))
                {
                    return Convert.ToInt64((UInt32?)obj);
                }
                else if (t == typeof(UInt64?))
                {
                    return Convert.ToInt64((Int64?)obj);
                }
                else if (t == typeof(decimal?))
                {
                    return Convert.ToInt64((decimal?)obj);
                }
                else if (t == typeof(float?))
                {
                    return Convert.ToInt64((float?)obj);
                }
                else if (t == typeof(double?))
                {
                    return Convert.ToInt64((double?)obj);
                }
            }
            if (throwException)
            {
                return Convert.ToInt64(obj);
            }
            else
            {
                if (obj == null)
                {
                    return 0;
                }
                Int64 d = 0;
                Int64.TryParse(obj.ToString(), out d);
                return d;
            }
        }
        public static Int64 ToInt64(object obj)
        {
            return ToInt64(obj, false);
        }
        public static float ToSingle(object obj, bool throwException)
        {

            if (obj == null)
            {

            }
            else
            {
                Type t = obj.GetType();
                if (t == typeof(sbyte))
                {
                    return Convert.ToSingle((sbyte)obj);
                }
                else if (t == typeof(float))
                {
                    return Convert.ToSingle((float)obj);
                }
                else if (t == typeof(byte))
                {
                    return Convert.ToSingle((byte)obj);
                }
                else if (t == typeof(Int16))
                {
                    return Convert.ToSingle((Int16)obj);
                }
                else if (t == typeof(Int32))
                {
                    return Convert.ToSingle((Int32)obj);
                }
                else if (t == typeof(Int64))
                {
                    return Convert.ToSingle((Int64)obj);
                }
                else if (t == typeof(UInt16))
                {
                    return Convert.ToSingle((UInt16)obj);
                }
                else if (t == typeof(UInt32))
                {
                    return Convert.ToSingle((UInt32)obj);
                }
                else if (t == typeof(UInt64))
                {
                    return Convert.ToSingle((Int64)obj);
                }
                else if (t == typeof(decimal))
                {
                    return Convert.ToSingle((decimal)obj);
                }

                else if (t == typeof(double))
                {
                    return Convert.ToSingle((double)obj);
                }
                else if (t == typeof(string))
                {
                    float d = 0;
                    if (float.TryParse((string)obj, out d))
                    {
                        return d;
                    }
                }
                else if (t == typeof(char))
                {
                    return Convert.ToSingle((char)obj);
                }
                else if (t == typeof(DateTime))
                {
                    return Convert.ToSingle((DateTime)obj);
                }
                else if (t == typeof(sbyte?))
                {
                    return Convert.ToSingle((sbyte?)obj);
                }
                else if (t == typeof(byte?))
                {
                    return Convert.ToSingle((byte?)obj);
                }
                else if (t == typeof(Int16?))
                {
                    return Convert.ToSingle((Int16?)obj);
                }
                else if (t == typeof(Int32?))
                {
                    return Convert.ToSingle((Int32?)obj);
                }
                else if (t == typeof(Int64?))
                {
                    return Convert.ToSingle((Int64?)obj);
                }
                else if (t == typeof(UInt16?))
                {
                    return Convert.ToSingle((UInt16?)obj);
                }
                else if (t == typeof(UInt32?))
                {
                    return Convert.ToSingle((UInt32?)obj);
                }
                else if (t == typeof(UInt64?))
                {
                    return Convert.ToSingle((Int64?)obj);
                }
                else if (t == typeof(decimal?))
                {
                    return Convert.ToSingle((decimal?)obj);
                }
                else if (t == typeof(float?))
                {
                    return Convert.ToSingle((float?)obj);
                }
                else if (t == typeof(double?))
                {
                    return Convert.ToSingle((double?)obj);
                }
            }
            if (throwException)
            {
                return Convert.ToSingle(obj);
            }
            else
            {
                if (obj == null)
                {
                    return 0;
                }
                float d = 0;
                float.TryParse(obj.ToString(), out d);
                return d;
            }
        }
        public static float ToSingle(object obj)
        {
            return ToSingle(obj, false);
        }
        public static double ToDouble(object obj, bool throwException)
        {

            if (obj == null)
            {

            }
            else
            {
                Type t = obj.GetType();
                if (t == typeof(sbyte))
                {
                    return Convert.ToDouble((sbyte)obj);
                }
                else if (t == typeof(byte))
                {
                    return Convert.ToDouble((byte)obj);
                }
                else if (t == typeof(Int16))
                {
                    return Convert.ToDouble((Int16)obj);
                }
                else if (t == typeof(Int32))
                {
                    return Convert.ToDouble((Int32)obj);
                }
                else if (t == typeof(Int64))
                {
                    return Convert.ToDouble((Int64)obj);
                }
                else if (t == typeof(UInt16))
                {
                    return Convert.ToDouble((UInt16)obj);
                }
                else if (t == typeof(UInt32))
                {
                    return Convert.ToDouble((UInt32)obj);
                }
                else if (t == typeof(UInt64))
                {
                    return Convert.ToDouble((Int64)obj);
                }
                else if (t == typeof(decimal))
                {
                    return Convert.ToDouble((decimal)obj);
                }
                else if (t == typeof(float))
                {
                    return Convert.ToDouble((float)obj);
                }
                else if (t == typeof(double))
                {
                    return Convert.ToDouble((double)obj);
                }
                else if (t == typeof(string))
                {
                    double d = 0;
                    if (double.TryParse((string)obj, out d))
                    {
                        return d;
                    }
                }
                else if (t == typeof(char))
                {
                    return Convert.ToDouble((char)obj);
                }
                else if (t == typeof(DateTime))
                {
                    return Convert.ToDouble((DateTime)obj);
                }
                else if (t == typeof(sbyte?))
                {
                    return Convert.ToDouble((sbyte?)obj);
                }
                else if (t == typeof(byte?))
                {
                    return Convert.ToDouble((byte?)obj);
                }
                else if (t == typeof(Int16?))
                {
                    return Convert.ToDouble((Int16?)obj);
                }
                else if (t == typeof(Int32?))
                {
                    return Convert.ToDouble((Int32?)obj);
                }
                else if (t == typeof(Int64?))
                {
                    return Convert.ToDouble((Int64?)obj);
                }
                else if (t == typeof(UInt16?))
                {
                    return Convert.ToDouble((UInt16?)obj);
                }
                else if (t == typeof(UInt32?))
                {
                    return Convert.ToDouble((UInt32?)obj);
                }
                else if (t == typeof(UInt64?))
                {
                    return Convert.ToDouble((Int64?)obj);
                }
                else if (t == typeof(decimal?))
                {
                    return Convert.ToDouble((decimal?)obj);
                }
                else if (t == typeof(float?))
                {
                    return Convert.ToDouble((float?)obj);
                }
                else if (t == typeof(double?))
                {
                    return Convert.ToDouble((double?)obj);
                }
            }
            if (throwException)
            {
                return Convert.ToDouble(obj);
            }
            else
            {
                if (obj == null)
                {
                    return 0;
                }
                double d = 0;
                double.TryParse(obj.ToString(), out d);
                return d;
            }
        }
        public static double ToDouble(object obj)
        {
            return ToDouble(obj, false);
        }
        public static UInt16 ToUInt16(object obj, bool throwException)
        {

            if (obj == null)
            {

            }
            else
            {
                Type t = obj.GetType();
                if (t == typeof(sbyte))
                {
                    return Convert.ToUInt16((sbyte)obj);
                }
                else if (t == typeof(byte))
                {
                    return Convert.ToUInt16((byte)obj);
                }
                else if (t == typeof(Int16))
                {
                    return Convert.ToUInt16((Int16)obj);
                }
                else if (t == typeof(Int32))
                {
                    return Convert.ToUInt16((Int32)obj);
                }
                else if (t == typeof(Int64))
                {
                    return Convert.ToUInt16((Int64)obj);
                }
                else if (t == typeof(UInt16))
                {
                    return Convert.ToUInt16((UInt16)obj);
                }
                else if (t == typeof(UInt32))
                {
                    return Convert.ToUInt16((UInt32)obj);
                }
                else if (t == typeof(UInt64))
                {
                    return Convert.ToUInt16((Int64)obj);
                }
                else if (t == typeof(decimal))
                {
                    return Convert.ToUInt16((decimal)obj);
                }
                else if (t == typeof(float))
                {
                    return Convert.ToUInt16((float)obj);
                }
                else if (t == typeof(double))
                {
                    return Convert.ToUInt16((double)obj);
                }
                else if (t == typeof(string))
                {
                    UInt16 d = 0;
                    if (UInt16.TryParse((string)obj, out d))
                    {
                        return d;
                    }
                }
                else if (t == typeof(char))
                {
                    return Convert.ToUInt16((char)obj);
                }
                else if (t == typeof(DateTime))
                {
                    return Convert.ToUInt16((DateTime)obj);
                }
                else if (t == typeof(sbyte?))
                {
                    return Convert.ToUInt16((sbyte?)obj);
                }
                else if (t == typeof(byte?))
                {
                    return Convert.ToUInt16((byte?)obj);
                }
                else if (t == typeof(Int16?))
                {
                    return Convert.ToUInt16((Int16?)obj);
                }
                else if (t == typeof(Int32?))
                {
                    return Convert.ToUInt16((Int32?)obj);
                }
                else if (t == typeof(Int64?))
                {
                    return Convert.ToUInt16((Int64?)obj);
                }
                else if (t == typeof(UInt16?))
                {
                    return Convert.ToUInt16((UInt16?)obj);
                }
                else if (t == typeof(UInt32?))
                {
                    return Convert.ToUInt16((UInt32?)obj);
                }
                else if (t == typeof(UInt64?))
                {
                    return Convert.ToUInt16((Int64?)obj);
                }
                else if (t == typeof(decimal?))
                {
                    return Convert.ToUInt16((decimal?)obj);
                }
                else if (t == typeof(float?))
                {
                    return Convert.ToUInt16((float?)obj);
                }
                else if (t == typeof(double?))
                {
                    return Convert.ToUInt16((double?)obj);
                }
            }
            if (throwException)
            {
                return Convert.ToUInt16(obj);
            }
            else
            {
                if (obj == null)
                {
                    return 0;
                }
                UInt16 d = 0;
                UInt16.TryParse(obj.ToString(), out d);
                return d;
            }
        }
        public static UInt16 ToUInt16(object obj)
        {
            return ToUInt16(obj, false);
        }
        public static UInt32 ToUInt32(object obj, bool throwException)
        {

            if (obj == null)
            {

            }
            else
            {
                Type t = obj.GetType();
                if (t == typeof(sbyte))
                {
                    return Convert.ToUInt32((sbyte)obj);
                }
                else if (t == typeof(byte))
                {
                    return Convert.ToUInt32((byte)obj);
                }
                else if (t == typeof(Int16))
                {
                    return Convert.ToUInt32((Int16)obj);
                }
                else if (t == typeof(Int32))
                {
                    return Convert.ToUInt32((Int32)obj);
                }
                else if (t == typeof(Int64))
                {
                    return Convert.ToUInt32((Int64)obj);
                }
                else if (t == typeof(UInt16))
                {
                    return Convert.ToUInt32((UInt16)obj);
                }
                else if (t == typeof(UInt32))
                {
                    return Convert.ToUInt32((UInt32)obj);
                }
                else if (t == typeof(UInt64))
                {
                    return Convert.ToUInt32((Int64)obj);
                }
                else if (t == typeof(decimal))
                {
                    return Convert.ToUInt32((decimal)obj);
                }
                else if (t == typeof(float))
                {
                    return Convert.ToUInt32((float)obj);
                }
                else if (t == typeof(double))
                {
                    return Convert.ToUInt32((double)obj);
                }
                else if (t == typeof(string))
                {
                    UInt32 d = 0;
                    if (UInt32.TryParse((string)obj, out d))
                    {
                        return d;
                    }
                }
                else if (t == typeof(char))
                {
                    return Convert.ToUInt32((char)obj);
                }
                else if (t == typeof(DateTime))
                {
                    return Convert.ToUInt32((DateTime)obj);
                }
                else if (t == typeof(sbyte?))
                {
                    return Convert.ToUInt32((sbyte?)obj);
                }
                else if (t == typeof(byte?))
                {
                    return Convert.ToUInt32((byte?)obj);
                }
                else if (t == typeof(Int16?))
                {
                    return Convert.ToUInt32((Int16?)obj);
                }
                else if (t == typeof(Int32?))
                {
                    return Convert.ToUInt32((Int32?)obj);
                }
                else if (t == typeof(Int64?))
                {
                    return Convert.ToUInt32((Int64?)obj);
                }
                else if (t == typeof(UInt16?))
                {
                    return Convert.ToUInt32((UInt16?)obj);
                }
                else if (t == typeof(UInt32?))
                {
                    return Convert.ToUInt32((UInt32?)obj);
                }
                else if (t == typeof(UInt64?))
                {
                    return Convert.ToUInt32((Int64?)obj);
                }
                else if (t == typeof(decimal?))
                {
                    return Convert.ToUInt32((decimal?)obj);
                }
                else if (t == typeof(float?))
                {
                    return Convert.ToUInt32((float?)obj);
                }
                else if (t == typeof(double?))
                {
                    return Convert.ToUInt32((double?)obj);
                }
            }
            if (throwException)
            {
                return Convert.ToUInt32(obj);
            }
            else
            {
                if (obj == null)
                {
                    return 0;
                }
                UInt32 d = 0;
                UInt32.TryParse(obj.ToString(), out d);
                return d;
            }
        }
        public static UInt32 ToUInt32(object obj)
        {
            return ToUInt32(obj, false);
        }
        public static UInt64 ToUInt64(object obj, bool throwException)
        {

            if (obj == null)
            {

            }
            else
            {
                Type t = obj.GetType();
                if (t == typeof(sbyte))
                {
                    return Convert.ToUInt64((sbyte)obj);
                }
                else if (t == typeof(byte))
                {
                    return Convert.ToUInt64((byte)obj);
                }
                else if (t == typeof(Int16))
                {
                    return Convert.ToUInt64((Int16)obj);
                }
                else if (t == typeof(Int32))
                {
                    return Convert.ToUInt64((Int32)obj);
                }
                else if (t == typeof(Int64))
                {
                    return Convert.ToUInt64((Int64)obj);
                }
                else if (t == typeof(UInt16))
                {
                    return Convert.ToUInt64((UInt16)obj);
                }
                else if (t == typeof(UInt32))
                {
                    return Convert.ToUInt64((UInt32)obj);
                }
                else if (t == typeof(UInt64))
                {
                    return Convert.ToUInt64((Int64)obj);
                }
                else if (t == typeof(decimal))
                {
                    return Convert.ToUInt64((decimal)obj);
                }
                else if (t == typeof(float))
                {
                    return Convert.ToUInt64((float)obj);
                }
                else if (t == typeof(double))
                {
                    return Convert.ToUInt64((double)obj);
                }
                else if (t == typeof(string))
                {
                    UInt64 d = 0;
                    if (UInt64.TryParse((string)obj, out d))
                    {
                        return d;
                    }
                }
                else if (t == typeof(char))
                {
                    return Convert.ToUInt64((char)obj);
                }
                else if (t == typeof(DateTime))
                {
                    return Convert.ToUInt64((DateTime)obj);
                }
                else if (t == typeof(sbyte?))
                {
                    return Convert.ToUInt64((sbyte?)obj);
                }
                else if (t == typeof(byte?))
                {
                    return Convert.ToUInt64((byte?)obj);
                }
                else if (t == typeof(Int16?))
                {
                    return Convert.ToUInt64((Int16?)obj);
                }
                else if (t == typeof(Int32?))
                {
                    return Convert.ToUInt64((Int32?)obj);
                }
                else if (t == typeof(Int64?))
                {
                    return Convert.ToUInt64((Int64?)obj);
                }
                else if (t == typeof(UInt16?))
                {
                    return Convert.ToUInt64((UInt16?)obj);
                }
                else if (t == typeof(UInt32?))
                {
                    return Convert.ToUInt64((UInt32?)obj);
                }
                else if (t == typeof(UInt64?))
                {
                    return Convert.ToUInt64((Int64?)obj);
                }
                else if (t == typeof(decimal?))
                {
                    return Convert.ToUInt64((decimal?)obj);
                }
                else if (t == typeof(float?))
                {
                    return Convert.ToUInt64((float?)obj);
                }
                else if (t == typeof(double?))
                {
                    return Convert.ToUInt64((double?)obj);
                }
            }
            if (throwException)
            {
                return Convert.ToUInt64(obj);
            }
            else
            {
                if (obj == null)
                {
                    return 0;
                }
                UInt64 d = 0;
                UInt64.TryParse(obj.ToString(), out d);
                return d;
            }
        }
        public static UInt64 ToUInt64(object obj)
        {
            return ToUInt64(obj, false);
        }
        public static Int32 ToInt(bool obj)
        {
            if (obj)
                return 1;
            return 0;
        }



        public static bool ToBoolean(sbyte obj)
        {
            return Convert.ToBoolean(obj);
        }
        public static bool ToBoolean(sbyte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToBoolean(obj.Value);
                }
            }
            return false;
        }
        public static bool ToBoolean(byte obj)
        {
            return Convert.ToBoolean(obj);
        }
        public static bool ToBoolean(byte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToBoolean(obj.Value);
                }
            }
            return false;
        }
        public static bool ToBoolean(Int16 obj)
        {
            return Convert.ToBoolean(obj);
        }
        public static bool ToBoolean(Int16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToBoolean(obj.Value);
                }
            }
            return false;
        }
        public static bool ToBoolean(Int32 obj)
        {
            return Convert.ToBoolean(obj);
        }
        public static bool ToBoolean(Int32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToBoolean(obj.Value);
                }
            }
            return false;
        }
        public static bool ToBoolean(Int64 obj)
        {
            return Convert.ToBoolean(obj);
        }
        public static bool ToBoolean(Int64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToBoolean(obj.Value);
                }
            }
            return false;
        }
        public static bool ToBoolean(UInt16 obj)
        {
            return Convert.ToBoolean(obj);
        }
        public static bool ToBoolean(UInt16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToBoolean(obj.Value);
                }
            }
            return false;
        }
        public static bool ToBoolean(UInt32 obj)
        {
            return Convert.ToBoolean(obj);
        }
        public static bool ToBoolean(UInt32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToBoolean(obj.Value);
                }
            }
            return false;
        }
        public static bool ToBoolean(UInt64 obj)
        {
            return Convert.ToBoolean(obj);
        }
        public static bool ToBoolean(UInt64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToBoolean(obj.Value);
                }
            }
            return false;
        }
        public static bool ToBoolean(decimal obj)
        {
            return Convert.ToBoolean(obj);
        }
        public static bool ToBoolean(decimal? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToBoolean(obj.Value);
                }
            }
            return false;
        }
        public static bool ToBoolean(float obj)
        {
            return Convert.ToBoolean(obj);
        }
        public static bool ToBoolean(float? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToBoolean(obj.Value);
                }
            }
            return false;
        }
        public static bool ToBoolean(double obj)
        {
            return Convert.ToBoolean(obj);
        }
        public static bool ToBoolean(double? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToBoolean(obj.Value);
                }
            }
            return false;
        }
        public static bool ToBoolean(char obj)
        {
            return Convert.ToBoolean(obj);
        }
        public static bool ToBoolean(char? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToBoolean(obj.Value);
                }
            }
            return false;
        }
        public static bool ToBoolean(DateTime obj)
        {
            return Convert.ToBoolean(obj);
        }
        public static bool ToBoolean(DateTime? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToBoolean(obj.Value);
                }
            }
            return false;
        }
        public static bool ToBoolean(string obj)
        {
            if (string.IsNullOrWhiteSpace(obj))
                return false;
            switch (obj.ToUpper ())
            {
                case "1":
                    return true;
                case "Y":
                    return true;
                case "是":
                    return true;
                case "OK":
                    return true;
                case "Yes":
                    return true;
                case "有":
                    return true;
                case "-1":
                    return false;
                case "0":
                    return false;
                case "N":
                    return false;
                case "NO":
                    return false;
                case "否":
                    return false;
                case "不是":
                    return false;
                case "没有":
                    return false;
                default:
                    break;
            }
            Boolean d = false;
            if (Boolean.TryParse((string)obj, out d))
            {
                return d;
            }
            return Convert.ToBoolean(obj);
        }

        public static sbyte ToSByte(sbyte obj)
        {
            return Convert.ToSByte(obj);
        }
        public static sbyte ToSByte(sbyte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSByte(obj.Value);
                }
            }
            return 0;
        }
        public static sbyte ToSByte(byte obj)
        {
            return Convert.ToSByte(obj);
        }
        public static sbyte ToSByte(byte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSByte(obj.Value);
                }
            }
            return 0;
        }
        public static sbyte ToSByte(Int16 obj)
        {
            return Convert.ToSByte(obj);
        }
        public static sbyte ToSByte(Int16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSByte(obj.Value);
                }
            }
            return 0;
        }
        public static sbyte ToSByte(Int32 obj)
        {
            return Convert.ToSByte(obj);
        }
        public static sbyte ToSByte(Int32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSByte(obj.Value);
                }
            }
            return 0;
        }
        public static sbyte ToSByte(Int64 obj)
        {
            return Convert.ToSByte(obj);
        }
        public static sbyte ToSByte(Int64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSByte(obj.Value);
                }
            }
            return 0;
        }
        public static sbyte ToSByte(UInt16 obj)
        {
            return Convert.ToSByte(obj);
        }
        public static sbyte ToSByte(UInt16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSByte(obj.Value);
                }
            }
            return 0;
        }
        public static sbyte ToSByte(UInt32 obj)
        {
            return Convert.ToSByte(obj);
        }
        public static sbyte ToSByte(UInt32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSByte(obj.Value);
                }
            }
            return 0;
        }
        public static sbyte ToSByte(UInt64 obj)
        {
            return Convert.ToSByte(obj);
        }
        public static sbyte ToSByte(UInt64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSByte(obj.Value);
                }
            }
            return 0;
        }
        public static sbyte ToSByte(decimal obj)
        {
            return Convert.ToSByte(obj);
        }
        public static sbyte ToSByte(decimal? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSByte(obj.Value);
                }
            }
            return 0;
        }
        public static sbyte ToSByte(float obj)
        {
            return Convert.ToSByte(obj);
        }
        public static sbyte ToSByte(float? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSByte(obj.Value);
                }
            }
            return 0;
        }
        public static sbyte ToSByte(double obj)
        {
            return Convert.ToSByte(obj);
        }
        public static sbyte ToSByte(double? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSByte(obj.Value);
                }
            }
            return 0;
        }
        public static sbyte ToSByte(char obj)
        {
            return Convert.ToSByte(obj);
        }
        public static sbyte ToSByte(char? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSByte(obj.Value);
                }
            }
            return 0;
        }
        public static sbyte ToSByte(DateTime obj)
        {
            return Convert.ToSByte(obj);
        }
        public static sbyte ToSByte(DateTime? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSByte(obj.Value);
                }
            }
            return 0;
        }
        public static sbyte ToSByte(string obj)
        {
            sbyte d = 0;
            if (sbyte.TryParse((string)obj, out d))
            {
                return d;
            }
            return Convert.ToSByte(obj);
        }



        public static byte ToByte(sbyte obj)
        {
            return Convert.ToByte(obj);
        }
        public static byte ToByte(sbyte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToByte(obj.Value);
                }
            }
            return 0;
        }
        public static byte ToByte(byte obj)
        {
            return Convert.ToByte(obj);
        }
        public static byte ToByte(byte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToByte(obj.Value);
                }
            }
            return 0;
        }
        public static byte ToByte(Int16 obj)
        {
            return Convert.ToByte(obj);
        }
        public static byte ToByte(Int16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToByte(obj.Value);
                }
            }
            return 0;
        }
        public static byte ToByte(Int32 obj)
        {
            return Convert.ToByte(obj);
        }
        public static byte ToByte(Int32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToByte(obj.Value);
                }
            }
            return 0;
        }
        public static byte ToByte(Int64 obj)
        {
            return Convert.ToByte(obj);
        }
        public static byte ToByte(Int64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToByte(obj.Value);
                }
            }
            return 0;
        }
        public static byte ToByte(UInt16 obj)
        {
            return Convert.ToByte(obj);
        }
        public static byte ToByte(UInt16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToByte(obj.Value);
                }
            }
            return 0;
        }
        public static byte ToByte(UInt32 obj)
        {
            return Convert.ToByte(obj);
        }
        public static byte ToByte(UInt32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToByte(obj.Value);
                }
            }
            return 0;
        }
        public static byte ToByte(UInt64 obj)
        {
            return Convert.ToByte(obj);
        }
        public static byte ToByte(UInt64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToByte(obj.Value);
                }
            }
            return 0;
        }
        public static byte ToByte(decimal obj)
        {
            return Convert.ToByte(obj);
        }
        public static byte ToByte(decimal? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToByte(obj.Value);
                }
            }
            return 0;
        }
        public static byte ToByte(float obj)
        {
            return Convert.ToByte(obj);
        }
        public static byte ToByte(float? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToByte(obj.Value);
                }
            }
            return 0;
        }
        public static byte ToByte(double obj)
        {
            return Convert.ToByte(obj);
        }
        public static byte ToByte(double? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToByte(obj.Value);
                }
            }
            return 0;
        }
        public static byte ToByte(char obj)
        {
            return Convert.ToByte(obj);
        }
        public static byte ToByte(char? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToByte(obj.Value);
                }
            }
            return 0;
        }
        public static byte ToByte(DateTime obj)
        {
            return Convert.ToByte(obj);
        }
        public static byte ToByte(DateTime? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToByte(obj.Value);
                }
            }
            return 0;
        }
        public static byte ToByte(string obj)
        {
            byte d = 0;
            if (byte.TryParse((string)obj, out d))
            {
                return d;
            }
            return Convert.ToByte(obj);
        }


        public static Int16 ToInt16(sbyte obj)
        {
            return Convert.ToInt16(obj);
        }
        public static Int16 ToInt16(sbyte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt16(obj.Value);
                }
            }
            return 0;
        }
        public static Int16 ToInt16(byte obj)
        {
            return Convert.ToInt16(obj);
        }
        public static Int16 ToInt16(byte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt16(obj.Value);
                }
            }
            return 0;
        }
        public static Int16 ToInt16(Int16 obj)
        {
            return Convert.ToInt16(obj);
        }
        public static Int16 ToInt16(Int16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt16(obj.Value);
                }
            }
            return 0;
        }
        public static Int16 ToInt16(Int32 obj)
        {
            return Convert.ToInt16(obj);
        }
        public static Int16 ToInt16(Int32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt16(obj.Value);
                }
            }
            return 0;
        }
        public static Int16 ToInt16(Int64 obj)
        {
            return Convert.ToInt16(obj);
        }
        public static Int16 ToInt16(Int64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt16(obj.Value);
                }
            }
            return 0;
        }
        public static Int16 ToInt16(UInt16 obj)
        {
            return Convert.ToInt16(obj);
        }
        public static Int16 ToInt16(UInt16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt16(obj.Value);
                }
            }
            return 0;
        }
        public static Int16 ToInt16(UInt32 obj)
        {
            return Convert.ToInt16(obj);
        }
        public static Int16 ToInt16(UInt32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt16(obj.Value);
                }
            }
            return 0;
        }
        public static Int16 ToInt16(UInt64 obj)
        {
            return Convert.ToInt16(obj);
        }
        public static Int16 ToInt16(UInt64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt16(obj.Value);
                }
            }
            return 0;
        }
        public static Int16 ToInt16(decimal obj)
        {
            return Convert.ToInt16(obj);
        }
        public static Int16 ToInt16(decimal? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt16(obj.Value);
                }
            }
            return 0;
        }
        public static Int16 ToInt16(float obj)
        {
            return Convert.ToInt16(obj);
        }
        public static Int16 ToInt16(float? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt16(obj.Value);
                }
            }
            return 0;
        }
        public static Int16 ToInt16(double obj)
        {
            return Convert.ToInt16(obj);
        }
        public static Int16 ToInt16(double? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt16(obj.Value);
                }
            }
            return 0;
        }
        public static Int16 ToInt16(char obj)
        {
            return Convert.ToInt16(obj);
        }
        public static Int16 ToInt16(char? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt16(obj.Value);
                }
            }
            return 0;
        }
        public static Int16 ToInt16(DateTime obj)
        {
            return Convert.ToInt16(obj);
        }
        public static Int16 ToInt16(DateTime? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt16(obj.Value);
                }
            }
            return 0;
        }
        public static Int16 ToInt16(string obj)
        {
            Int16 d = 0;
            if (Int16.TryParse((string)obj, out d))
            {
                return d;
            }
            return Convert.ToInt16(obj);
        }
 
        public static Int32 ToInt32(sbyte obj)
        {
            return Convert.ToInt32(obj);
        }
        public static Int32 ToInt32(sbyte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt32(obj.Value);
                }
            }
            return 0;
        }
        public static Int32 ToInt32(byte obj)
        {
            return Convert.ToInt32(obj);
        }
        public static Int32 ToInt32(byte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt32(obj.Value);
                }
            }
            return 0;
        }
        public static Int32 ToInt32(Int16 obj)
        {
            return Convert.ToInt32(obj);
        }
        public static Int32 ToInt32(Int16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt32(obj);
                }
            }
            return 0;
        }
        public static Int32 ToInt32(Int32 obj)
        {
            return Convert.ToInt32(obj);
        }
        public static Int32 ToInt32(Int32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt32(obj.Value);
                }
            }
            return 0;
        }
        public static Int32 ToInt32(Int64 obj)
        {
            return Convert.ToInt32(obj);
        }
        public static Int32 ToInt32(Int64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt32(obj.Value);
                }
            }
            return 0;
        }
        public static Int32 ToInt32(UInt16 obj)
        {
            return Convert.ToInt32(obj);
        }
        public static Int32 ToInt32(UInt16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt32(obj.Value);
                }
            }
            return 0;
        }
        public static Int32 ToInt32(UInt32 obj)
        {
            return Convert.ToInt32(obj);
        }
        public static Int32 ToInt32(UInt32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt32(obj.Value);
                }
            }
            return 0;
        }
        public static Int32 ToInt32(UInt64 obj)
        {
            return Convert.ToInt32(obj);
        }
        public static Int32 ToInt32(UInt64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt32(obj.Value);
                }
            }
            return 0;
        }
        public static Int32 ToInt32(decimal obj)
        {
            return Convert.ToInt32(obj);
        }
        public static Int32 ToInt32(decimal? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt32(obj.Value);
                }
            }
            return 0;
        }
        public static Int32 ToInt32(float obj)
        {
            return Convert.ToInt32(obj);
        }
        public static Int32 ToInt32(float? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt32(obj.Value);
                }
            }
            return 0;
        }
        public static Int32 ToInt32(double obj)
        {
            return Convert.ToInt32(obj);
        }
        public static Int32 ToInt32(double? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt32(obj.Value);
                }
            }
            return 0;
        }
        public static Int32 ToInt32(char obj)
        {
            return Convert.ToInt32(obj);
        }
        public static Int32 ToInt32(char? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt32(obj.Value);
                }
            }
            return 0;
        }
        public static Int32 ToInt32(DateTime obj)
        {
            int value = obj.Year * 100 * 100
                     + obj.Month * 100
                     + obj.Day;
            return value;
        }
        public static Int32 ToInt32(DateTime? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return ToInt32(obj.Value);
                }
            }
            return 0;
        }
        public static Int32 ToInt32(string obj)
        {
            Int32 d = 0;
            if (Int32.TryParse((string)obj, out d))
            {
                return d;
            }
            return Convert.ToInt32(obj);
        }

        public static Int64 ToInt64(sbyte obj)
        {
            return Convert.ToInt64(obj);
        }
        public static Int64 ToInt64(sbyte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt64(obj.Value);
                }
            }
            return 0;
        }
        public static Int64 ToInt64(byte obj)
        {
            return Convert.ToInt64(obj);
        }
        public static Int64 ToInt64(byte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt64(obj.Value);
                }
            }
            return 0;
        }
        public static Int64 ToInt64(Int16 obj)
        {
            return Convert.ToInt64(obj);
        }
        public static Int64 ToInt64(Int16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt64(obj.Value);
                }
            }
            return 0;
        }
        public static Int64 ToInt64(Int32 obj)
        {
            return Convert.ToInt64(obj);
        }
        public static Int64 ToInt64(Int32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt64(obj.Value);
                }
            }
            return 0;
        }
        public static Int64 ToInt64(Int64 obj)
        {
            return Convert.ToInt64(obj);
        }
        public static Int64 ToInt64(Int64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt64(obj.Value);
                }
            }
            return 0;
        }
        public static Int64 ToInt64(UInt16 obj)
        {
            return Convert.ToInt64(obj);
        }
        public static Int64 ToInt64(UInt16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt64(obj.Value);
                }
            }
            return 0;
        }
        public static Int64 ToInt64(UInt32 obj)
        {
            return Convert.ToInt64(obj);
        }
        public static Int64 ToInt64(UInt32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt64(obj.Value);
                }
            }
            return 0;
        }
        public static Int64 ToInt64(UInt64 obj)
        {
            return Convert.ToInt64(obj);
        }
        public static Int64 ToInt64(UInt64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt64(obj.Value);
                }
            }
            return 0;
        }
        public static Int64 ToInt64(decimal obj)
        {
            return Convert.ToInt64(obj);
        }
        public static Int64 ToInt64(decimal? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt64(obj.Value);
                }
            }
            return 0;
        }
        public static Int64 ToInt64(float obj)
        {
            return Convert.ToInt64(obj);
        }
        public static Int64 ToInt64(float? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt64(obj.Value);
                }
            }
            return 0;
        }
        public static Int64 ToInt64(double obj)
        {
            return Convert.ToInt64(obj);
        }
        public static Int64 ToInt64(double? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt64(obj.Value);
                }
            }
            return 0;
        }
        public static Int64 ToInt64(char obj)
        {
            return Convert.ToInt64(obj);
        }
        public static Int64 ToInt64(char? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToInt64(obj.Value);
                }
            }
            return 0;
        }
        public static Int64 ToInt64(DateTime obj)
        {
            Int64 value = ((long)obj.Year) * 1000 * 100 * 100 * 100 * 100 * 100;
            value = value + ((long)obj.Month) * 1000 * 100 * 100 * 100 * 100;
            value = value + ((long)obj.Day) * 1000 * 100 * 100 * 100;
            value = value + obj.Hour * 1000 * 100 * 100;
            value = value + obj.Minute * 1000 * 100;
            value = value + obj.Second * 1000;
            value = value + obj.Millisecond;
            return value;
        }
        public static Int64 ToInt64(DateTime? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return ToInt64(obj.Value);
                }
            }
            return 0;
        }
        public static Int64 ToInt64(string obj)
        {
            Int64 d = 0;
            if (Int64.TryParse((string)obj, out d))
            {
                return d;
            }
            return Convert.ToInt64(obj);
        }

        public static float ToSingle(sbyte obj)
        {
            return Convert.ToSingle(obj);
        }
        public static float ToSingle(sbyte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSingle(obj.Value);
                }
            }
            return 0;
        }
        public static float ToSingle(byte obj)
        {
            return Convert.ToSingle(obj);
        }
        public static float ToSingle(byte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSingle(obj.Value);
                }
            }
            return 0;
        }
        public static float ToSingle(Int16 obj)
        {
            return Convert.ToSingle(obj);
        }
        public static float ToSingle(Int16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSingle(obj.Value);
                }
            }
            return 0;
        }
        public static float ToSingle(Int32 obj)
        {
            return Convert.ToSingle(obj);
        }
        public static float ToSingle(Int32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSingle(obj.Value);
                }
            }
            return 0;
        }
        public static float ToSingle(Int64 obj)
        {
            return Convert.ToSingle(obj);
        }
        public static float ToSingle(Int64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSingle(obj.Value);
                }
            }
            return 0;
        }
        public static float ToSingle(UInt16 obj)
        {
            return Convert.ToSingle(obj);
        }
        public static float ToSingle(UInt16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSingle(obj.Value);
                }
            }
            return 0;
        }
        public static float ToSingle(UInt32 obj)
        {
            return Convert.ToSingle(obj);
        }
        public static float ToSingle(UInt32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSingle(obj.Value);
                }
            }
            return 0;
        }
        public static float ToSingle(UInt64 obj)
        {
            return Convert.ToSingle(obj);
        }
        public static float ToSingle(UInt64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSingle(obj.Value);
                }
            }
            return 0;
        }
        public static float ToSingle(decimal obj)
        {
            return Convert.ToSingle(obj);
        }
        public static float ToSingle(decimal? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSingle(obj.Value);
                }
            }
            return 0;
        }
        public static float ToSingle(float obj)
        {
            return Convert.ToSingle(obj);
        }
        public static float ToSingle(float? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSingle(obj.Value);
                }
            }
            return 0;
        }
        public static float ToSingle(double obj)
        {
            return Convert.ToSingle(obj);
        }
        public static float ToSingle(double? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSingle(obj.Value);
                }
            }
            return 0;
        }
        public static float ToSingle(char obj)
        {
            return Convert.ToSingle(obj);
        }
        public static float ToSingle(char? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSingle(obj.Value);
                }
            }
            return 0;
        }
        public static float ToSingle(DateTime obj)
        {
            return Convert.ToSingle(obj);
        }
        public static float ToSingle(DateTime? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToSingle(obj.Value);
                }
            }
            return 0;
        }
        public static float ToSingle(string obj)
        {
            float d = 0;
            if (float.TryParse((string)obj, out d))
            {
                return d;
            }
            return Convert.ToSingle(obj);
        }


        public static double ToDouble(sbyte obj)
        {
            return Convert.ToDouble(obj);
        }
        public static double ToDouble(sbyte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDouble(obj.Value);
                }
            }
            return 0;
        }
        public static double ToDouble(byte obj)
        {
            return Convert.ToDouble(obj);
        }
        public static double ToDouble(byte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDouble(obj.Value);
                }
            }
            return 0;
        }
        public static double ToDouble(Int16 obj)
        {
            return Convert.ToDouble(obj);
        }
        public static double ToDouble(Int16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDouble(obj.Value);
                }
            }
            return 0;
        }
        public static double ToDouble(Int32 obj)
        {
            return Convert.ToDouble(obj);
        }
        public static double ToDouble(Int32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDouble(obj.Value);
                }
            }
            return 0;
        }
        public static double ToDouble(Int64 obj)
        {
            return Convert.ToDouble(obj);
        }
        public static double ToDouble(Int64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDouble(obj.Value);
                }
            }
            return 0;
        }
        public static double ToDouble(UInt16 obj)
        {
            return Convert.ToDouble(obj);
        }
        public static double ToDouble(UInt16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDouble(obj.Value);
                }
            }
            return 0;
        }
        public static double ToDouble(UInt32 obj)
        {
            return Convert.ToDouble(obj);
        }
        public static double ToDouble(UInt32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDouble(obj.Value);
                }
            }
            return 0;
        }
        public static double ToDouble(UInt64 obj)
        {
            return Convert.ToDouble(obj);
        }
        public static double ToDouble(UInt64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDouble(obj.Value);
                }
            }
            return 0;
        }
        public static double ToDouble(decimal obj)
        {
            return Convert.ToDouble(obj);
        }
        public static double ToDouble(decimal? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDouble(obj.Value);
                }
            }
            return 0;
        }
        public static double ToDouble(float obj)
        {
            return Convert.ToDouble(obj);
        }
        public static double ToDouble(float? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDouble(obj.Value);
                }
            }
            return 0;
        }
        public static double ToDouble(double obj)
        {
            return Convert.ToDouble(obj);
        }
        public static double ToDouble(double? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDouble(obj.Value);
                }
            }
            return 0;
        }
        public static double ToDouble(char obj)
        {
            return Convert.ToDouble(obj);
        }
        public static double ToDouble(char? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDouble(obj.Value);
                }
            }
            return 0;
        }
        public static double ToDouble(DateTime obj)
        {
            return Convert.ToDouble(obj);
        }
        public static double ToDouble(DateTime? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToDouble(obj.Value);
                }
            }
            return 0;
        }
        public static double ToDouble(string obj)
        {
            double d = 0;
            if (double.TryParse((string)obj, out d))
            {
                return d;
            }
            return Convert.ToDouble(obj);
        }

        public static UInt16 ToUInt16(sbyte obj)
        {
            return Convert.ToUInt16(obj);
        }
        public static UInt16 ToUInt16(sbyte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt16(obj.Value);
                }
            }
            return 0;
        }
        public static UInt16 ToUInt16(byte obj)
        {
            return Convert.ToUInt16(obj);
        }
        public static UInt16 ToUInt16(byte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt16(obj.Value);
                }
            }
            return 0;
        }
        public static UInt16 ToUInt16(Int16 obj)
        {
            return Convert.ToUInt16(obj);
        }
        public static UInt16 ToUInt16(Int16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt16(obj.Value);
                }
            }
            return 0;
        }
        public static UInt16 ToUInt16(Int32 obj)
        {
            return Convert.ToUInt16(obj);
        }
        public static UInt16 ToUInt16(Int32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt16(obj.Value);
                }
            }
            return 0;
        }
        public static UInt16 ToUInt16(Int64 obj)
        {
            return Convert.ToUInt16(obj);
        }
        public static UInt16 ToUInt16(Int64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt16(obj.Value);
                }
            }
            return 0;
        }
        public static UInt16 ToUInt16(UInt16 obj)
        {
            return Convert.ToUInt16(obj);
        }
        public static UInt16 ToUInt16(UInt16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt16(obj.Value);
                }
            }
            return 0;
        }
        public static UInt16 ToUInt16(UInt32 obj)
        {
            return Convert.ToUInt16(obj);
        }
        public static UInt16 ToUInt16(UInt32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt16(obj.Value);
                }
            }
            return 0;
        }
        public static UInt16 ToUInt16(UInt64 obj)
        {
            return Convert.ToUInt16(obj);
        }
        public static UInt16 ToUInt16(UInt64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt16(obj.Value);
                }
            }
            return 0;
        }
        public static UInt16 ToUInt16(decimal obj)
        {
            return Convert.ToUInt16(obj);
        }
        public static UInt16 ToUInt16(decimal? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt16(obj.Value);
                }
            }
            return 0;
        }
        public static UInt16 ToUInt16(float obj)
        {
            return Convert.ToUInt16(obj);
        }
        public static UInt16 ToUInt16(float? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt16(obj.Value);
                }
            }
            return 0;
        }
        public static UInt16 ToUInt16(double obj)
        {
            return Convert.ToUInt16(obj);
        }
        public static UInt16 ToUInt16(double? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt16(obj.Value);
                }
            }
            return 0;
        }
        public static UInt16 ToUInt16(char obj)
        {
            return Convert.ToUInt16(obj);
        }
        public static UInt16 ToUInt16(char? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt16(obj.Value);
                }
            }
            return 0;
        }
        public static UInt16 ToUInt16(DateTime obj)
        {
            return Convert.ToUInt16(obj);
        }
        public static UInt16 ToUInt16(DateTime? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt16(obj.Value);
                }
            }
            return 0;
        }
        public static UInt16 ToUInt16(string obj)
        {
            UInt16 d = 0;
            if (UInt16.TryParse((string)obj, out d))
            {
                return d;
            }
            return Convert.ToUInt16(obj);
        }


        public static UInt32 ToUInt32(sbyte obj)
        {
            return Convert.ToUInt32(obj);
        }
        public static UInt32 ToUInt32(sbyte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt32(obj.Value);
                }
            }
            return 0;
        }
        public static UInt32 ToUInt32(byte obj)
        {
            return Convert.ToUInt32(obj);
        }
        public static UInt32 ToUInt32(byte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt32(obj.Value);
                }
            }
            return 0;
        }
        public static UInt32 ToUInt32(Int16 obj)
        {
            return Convert.ToUInt32(obj);
        }
        public static UInt32 ToUInt32(Int16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt32(obj.Value);
                }
            }
            return 0;
        }
        public static UInt32 ToUInt32(Int32 obj)
        {
            return Convert.ToUInt32(obj);
        }
        public static UInt32 ToUInt32(Int32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt32(obj.Value);
                }
            }
            return 0;
        }
        public static UInt32 ToUInt32(Int64 obj)
        {
            return Convert.ToUInt32(obj);
        }
        public static UInt32 ToUInt32(Int64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt32(obj.Value);
                }
            }
            return 0;
        }
        public static UInt32 ToUInt32(UInt16 obj)
        {
            return Convert.ToUInt32(obj);
        }
        public static UInt32 ToUInt32(UInt16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt32(obj.Value);
                }
            }
            return 0;
        }
        public static UInt32 ToUInt32(UInt32 obj)
        {
            return Convert.ToUInt32(obj);
        }
        public static UInt32 ToUInt32(UInt32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt32(obj.Value);
                }
            }
            return 0;
        }
        public static UInt32 ToUInt32(UInt64 obj)
        {
            return Convert.ToUInt32(obj);
        }
        public static UInt32 ToUInt32(UInt64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt32(obj.Value);
                }
            }
            return 0;
        }
        public static UInt32 ToUInt32(decimal obj)
        {
            return Convert.ToUInt32(obj);
        }
        public static UInt32 ToUInt32(decimal? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt32(obj.Value);
                }
            }
            return 0;
        }
        public static UInt32 ToUInt32(float obj)
        {
            return Convert.ToUInt32(obj);
        }
        public static UInt32 ToUInt32(float? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt32(obj.Value);
                }
            }
            return 0;
        }
        public static UInt32 ToUInt32(double obj)
        {
            return Convert.ToUInt32(obj);
        }
        public static UInt32 ToUInt32(double? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt32(obj.Value);
                }
            }
            return 0;
        }
        public static UInt32 ToUInt32(char obj)
        {
            return Convert.ToUInt32(obj);
        }
        public static UInt32 ToUInt32(char? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt32(obj.Value);
                }
            }
            return 0;
        }
        public static UInt32 ToUInt32(DateTime obj)
        {
            return Convert.ToUInt32(obj);
        }
        public static UInt32 ToUInt32(DateTime? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt32(obj.Value);
                }
            }
            return 0;
        }
        public static UInt32 ToUInt32(string obj)
        {
            UInt32 d = 0;
            if (UInt32.TryParse((string)obj, out d))
            {
                return d;
            }
            return Convert.ToUInt32(obj);
        }



        public static UInt64 ToUInt64(sbyte obj)
        {
            return Convert.ToUInt64(obj);
        }
        public static UInt64 ToUInt64(sbyte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt64(obj.Value);
                }
            }
            return 0;
        }
        public static UInt64 ToUInt64(byte obj)
        {
            return Convert.ToUInt64(obj);
        }
        public static UInt64 ToUInt64(byte? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt64(obj.Value);
                }
            }
            return 0;
        }
        public static UInt64 ToUInt64(Int16 obj)
        {
            return Convert.ToUInt64(obj);
        }
        public static UInt64 ToUInt64(Int16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt64(obj.Value);
                }
            }
            return 0;
        }
        public static UInt64 ToUInt64(Int32 obj)
        {
            return Convert.ToUInt64(obj);
        }
        public static UInt64 ToUInt64(Int32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt64(obj.Value);
                }
            }
            return 0;
        }
        public static UInt64 ToUInt64(Int64 obj)
        {
            return Convert.ToUInt64(obj);
        }
        public static UInt64 ToUInt64(Int64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt64(obj.Value);
                }
            }
            return 0;
        }
        public static UInt64 ToUInt64(UInt16 obj)
        {
            return Convert.ToUInt64(obj);
        }
        public static UInt64 ToUInt64(UInt16? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt64(obj.Value);
                }
            }
            return 0;
        }
        public static UInt64 ToUInt64(UInt32 obj)
        {
            return Convert.ToUInt64(obj);
        }
        public static UInt64 ToUInt64(UInt32? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt64(obj.Value);
                }
            }
            return 0;
        }
        public static UInt64 ToUInt64(UInt64 obj)
        {
            return Convert.ToUInt64(obj);
        }
        public static UInt64 ToUInt64(UInt64? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt64(obj.Value);
                }
            }
            return 0;
        }
        public static UInt64 ToUInt64(decimal obj)
        {
            return Convert.ToUInt64(obj);
        }
        public static UInt64 ToUInt64(decimal? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt64(obj.Value);
                }
            }
            return 0;
        }
        public static UInt64 ToUInt64(float obj)
        {
            return Convert.ToUInt64(obj);
        }
        public static UInt64 ToUInt64(float? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt64(obj.Value);
                }
            }
            return 0;
        }
        public static UInt64 ToUInt64(double obj)
        {
            return Convert.ToUInt64(obj);
        }
        public static UInt64 ToUInt64(double? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt64(obj.Value);
                }
            }
            return 0;
        }
        public static UInt64 ToUInt64(char obj)
        {
            return Convert.ToUInt64(obj);
        }
        public static UInt64 ToUInt64(char? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt64(obj.Value);
                }
            }
            return 0;
        }
        public static UInt64 ToUInt64(DateTime obj)
        {
            return Convert.ToUInt64(obj);
        }
        public static UInt64 ToUInt64(DateTime? obj)
        {
            if (obj != null)
            {
                if (obj.HasValue)
                {
                    return Convert.ToUInt64(obj.Value);
                }
            }
            return 0;
        }
        public static UInt64 ToUInt64(string obj)
        {
            UInt64 d = 0;
            if (UInt64.TryParse((string)obj, out d))
            {
                return d;
            }
            return Convert.ToUInt64(obj);
        }
        public static bool IsNumber(object obj)
        {
            if (obj == null)
                return false;
            return IsNumber(obj.GetType());
        }

        public static bool IsIntNumber(object obj)
        {
            if (obj == null)
                return false;
            return IsIntNumber(obj.GetType());
        }

        public static bool IsDecimalNumber(object obj)
        {
            if (obj == null)
                return false;
            return IsDecimalNumber(obj.GetType());
        }
        public static bool IsDoubleNumber(object obj)
        {
            if (obj == null)
                return false;
            return IsDoubleNumber(obj.GetType());
        }
        public static bool IsNumber(Type t)
        {
            if (t == typeof(Int16))
            {
                return true;
            }
            if (t == typeof(Int32))
            {
                return true;
            }
            if (t == typeof(Int64))
            {
                return true;
            }
            if (t == typeof(UInt16))
            {
                return true;
            }
            if (t == typeof(UInt32))
            {
                return true;
            }
            if (t == typeof(UInt64))
            {
                return true;
            }
            if (t == typeof(float))
            {
                return true;
            }
            if (t == typeof(double))
            {
                return true;
            }
            if (t == typeof(decimal))
            {
                return true;
            }
            if (t == typeof(byte))
            {
                return true;
            }
            if (t == typeof(sbyte))
            {
                return true;
            }

            if (t == typeof(Int16?))
            {
                return true;
            }
            if (t == typeof(Int32?))
            {
                return true;
            }
            if (t == typeof(Int64?))
            {
                return true;
            }
            if (t == typeof(UInt16?))
            {
                return true;
            }
            if (t == typeof(UInt32?))
            {
                return true;
            }
            if (t == typeof(UInt64?))
            {
                return true;
            }
            if (t == typeof(float?))
            {
                return true;
            }
            if (t == typeof(double?))
            {
                return true;
            }
            if (t == typeof(decimal?))
            {
                return true;
            }
            if (t == typeof(byte?))
            {
                return true;
            }
            if (t == typeof(sbyte?))
            {
                return true;
            }
            return false;
        }
        public static bool IsIntNumber(Type t)
        {
            if (t == typeof(Int16))
            {
                return true;
            }
            if (t == typeof(Int32))
            {
                return true;
            }
            if (t == typeof(Int64))
            {
                return true;
            }
            if (t == typeof(UInt16))
            {
                return true;
            }
            if (t == typeof(UInt32))
            {
                return true;
            }
            if (t == typeof(UInt64))
            {
                return true;
            } 
            if (t == typeof(byte))
            {
                return true;
            }
            if (t == typeof(sbyte))
            {
                return true;
            }

            if (t == typeof(Int16?))
            {
                return true;
            }
            if (t == typeof(Int32?))
            {
                return true;
            }
            if (t == typeof(Int64?))
            {
                return true;
            }
            if (t == typeof(UInt16?))
            {
                return true;
            }
            if (t == typeof(UInt32?))
            {
                return true;
            }
            if (t == typeof(UInt64?))
            {
                return true;
            } 
            if (t == typeof(byte?))
            {
                return true;
            }
            if (t == typeof(sbyte?))
            {
                return true;
            }
            return false;
        }
        public static bool IsDecimalNumber(Type t)
        {
            if (t == typeof(Int16))
            {
                return true;
            }
            if (t == typeof(Int32))
            {
                return true;
            }
            if (t == typeof(Int64))
            {
                return true;
            }
            if (t == typeof(UInt16))
            {
                return true;
            }
            if (t == typeof(UInt32))
            {
                return true;
            }
            if (t == typeof(UInt64))
            {
                return true;
            } 
            if (t == typeof(decimal))
            {
                return true;
            }
            if (t == typeof(byte))
            {
                return true;
            }
            if (t == typeof(sbyte))
            {
                return true;
            }

            if (t == typeof(Int16?))
            {
                return true;
            }
            if (t == typeof(Int32?))
            {
                return true;
            }
            if (t == typeof(Int64?))
            {
                return true;
            }
            if (t == typeof(UInt16?))
            {
                return true;
            }
            if (t == typeof(UInt32?))
            {
                return true;
            }
            if (t == typeof(UInt64?))
            {
                return true;
            } 
            if (t == typeof(decimal?))
            {
                return true;
            }
            if (t == typeof(byte?))
            {
                return true;
            }
            if (t == typeof(sbyte?))
            {
                return true;
            }
            return false;
        }
        public static bool IsDoubleNumber(Type t)
        { 
            if (t == typeof(float))
            {
                return true;
            }
            if (t == typeof(double))
            {
                return true;
            }   
            if (t == typeof(float?))
            {
                return true;
            }
            if (t == typeof(double?))
            {
                return true;
            } 
            return false;
        }

        public static byte[] ToBytes(bool value)
        {
            return BitConverter.GetBytes(value);
        }
        public static byte[] ToBytes(Int32 value)
        {
            return BitConverter.GetBytes(value);
        }
        public static byte[] ToBytes(decimal value)
        {
            using (BufferWriter reader = new BufferWriter())
            {
                reader.Write(value);
                return reader.GetData();
            }
        }
        public static byte[] ToBytes(String value)
        {
            return Feng.IO.BitConver.GetBytes(value);
        }
        public static byte[] ToBytes(DateTime value)
        {
            using (BufferWriter bw = new BufferWriter())
            {
                bw.Write(value);
                return bw.GetData();
            }
          
        }
        public static string GetText(string text1, string text2)
        {
            if (string.IsNullOrEmpty(text2))
            {
                return text1;
            }
            return text2;
        }

        public static bool GetResult(string text, string arg1, string arg2, string defaultvalue, ref  string result)
        {
            if (text.Contains(arg1))
            {
                if (text.Contains(arg2))
                {
                    result = defaultvalue;
                    return true;
                }
            }
            return false;
        }
        public static bool GetResult(string text, string arg1, string arg2, string arg3, string defaultvalue, ref  string result)
        {
            if (text.Contains(arg1))
            {
                if (text.Contains(arg2))
                {
                    if (text.Contains(arg3))
                    {
                        result = defaultvalue;
                        return true;
                    }
                }
            }
            return false;
        }
        public static bool GetResult(string text, string arg1, string arg2, string arg3, string arg4, string defaultvalue, ref  string result)
        {
            if (text.Contains(arg1))
            {
                if (text.Contains(arg2))
                {
                    if (text.Contains(arg3))
                    {
                        if (text.Contains(arg4))
                        {
                            result = defaultvalue;
                            return true;
                        }
                    }
                }
            }
            return false;
        }
        public static bool GetResult(string text, string arg1, string arg2, string arg3, string arg4, string arg5, string defaultvalue, ref  string result)
        {
            if (text.Contains(arg1))
            {
                if (text.Contains(arg2))
                {
                    if (text.Contains(arg3))
                    {
                        if (text.Contains(arg4))
                        {
                            if (text.Contains(arg5))
                            {
                                result = defaultvalue;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        public static bool GetResult(string text, string arg1, string arg2, string arg3, string arg4, string arg5, string arg6, string defaultvalue, ref  string result)
        {
            if (text.Contains(arg1))
            {
                if (text.Contains(arg2))
                {
                    if (text.Contains(arg3))
                    {
                        if (text.Contains(arg4))
                        {
                            if (text.Contains(arg5))
                            {
                                if (text.Contains(arg6))
                                {
                                    result = defaultvalue;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
        }
 
        public static string GetFormatString(object value, FormatType ft, string format)
        {
            if (value == null)
                return string.Empty;
            string text = value.ToString();
            if (ft == FormatType.DateTime)
            {
                if (format != string.Empty)
                {
                    if (value is DateTime)
                    {
                        DateTime dt = (DateTime)value;
                        text = dt.ToString(format);
                    }
                    return text;
                }
            }
            else if (ft == FormatType.Numberic)
            {
                if (format != string.Empty)
                {
                    if (value is decimal)
                    {
                        decimal dt = (decimal)value;
                        text = dt.ToString(format);
                    }
                    else if (value is int)
                    {
                        int dt = (int)value;
                        text = dt.ToString(format);
                    }
                    else if (value is float)
                    {
                        float dt = (float)value;
                        text = dt.ToString(format);
                    }
                    else if (value is double)
                    {
                        double dt = (double)value;
                        text = dt.ToString(format);
                    }
                    else if (value is uint)
                    {
                        uint dt = (uint)value;
                        text = dt.ToString(format);
                    }
                    else if (value is ushort)
                    {
                        ushort dt = (ushort)value;
                        text = dt.ToString(format);
                    }
                    else if (value is short)
                    {
                        short dt = (short)value;
                        text = dt.ToString(format);
                    }
                    return text;
                }
            }
          
            if (ft == FormatType.Null)
            {
                if (value is DateTime)
                {
                    DateTime dt = (DateTime)value;
                    text = dt.ToString("yyyy-MM-dd");
                }
                else if (value is decimal)
                {
                    decimal dt = (decimal)value;
                    text = dt.ToString("#0.00");
                }
                else if (value is int)
                {
                    int dt = (int)value;
                    text = dt.ToString("#0");
                }
                else if (value is float)
                {
                    float dt = (float)value;
                    text = dt.ToString("#0.00");
                }
                else if (value is double)
                {
                    double dt = (double)value;
                    text = dt.ToString("#0.00");
                }
                else if (value is uint)
                {
                    uint dt = (uint)value;
                    text = dt.ToString("#0");
                }
                else if (value is ushort)
                {
                    ushort dt = (ushort)value;
                    text = dt.ToString("#0");
                }
                else if (value is short)
                {
                    short dt = (short)value;
                    text = dt.ToString("#0");
                }
                else if (value is bool)
                {
                    text = string.Empty;
                }
            }

            return text;
        }
        public static bool IsNumberType(object obj)
        {
            if (obj is Int16)
            {
                return true;
            }
            if (obj is Int32)
            {
                return true;
            }
            if (obj is Int64)
            {
                return true;
            }
            if (obj is UInt16)
            {
                return true;
            }
            if (obj is UInt32)
            {
                return true;
            }
            if (obj is UInt64)
            {
                return true;
            }
            if (obj is float)
            {
                return true;
            }
            if (obj is double)
            {
                return true;
            }
            if (obj is decimal)
            {
                return true;
            }
            if (obj is byte)
            {
                return true;
            }
            if (obj is sbyte)
            {
                return true;
            }
            return false;
        }
        public static bool IsNumberType(string obj)
        {
            if (obj == typeof(Int16).FullName)
            {
                return true;
            }
            if (obj == typeof( Int32).FullName)
            {
                return true;
            }
            if (obj == typeof( Int64).FullName)
            {
                return true;
            }
            if (obj == typeof( UInt16).FullName)
            {
                return true;
            }
            if (obj == typeof( UInt32).FullName)
            {
                return true;
            }
            if (obj == typeof( UInt64).FullName)
            {
                return true;
            }
            if (obj == typeof( float).FullName)
            {
                return true;
            }
            if (obj == typeof( double).FullName)
            {
                return true;
            }
            if (obj == typeof( decimal).FullName)
            {
                return true;
            }
            if (obj == typeof( byte).FullName)
            {
                return true;
            }
            if (obj == typeof(sbyte).FullName)
            {
                return true;
            }
            return false;
        }
        public static bool IsNumber(string text)
        {
            decimal d = 0;
            if (decimal.TryParse(text, out d))
            {
                return true;
            } 
            return false;
        }
        public static bool IsNumberType(Type t)
        {
            if (t == typeof(Int16))
            {
                return true;
            }
            if (t == typeof(Int32))
            {
                return true;
            }
            if (t == typeof(Int64))
            {
                return true;
            }
            if (t == typeof(UInt16))
            {
                return true;
            }
            if (t == typeof(UInt32))
            {
                return true;
            }
            if (t == typeof(UInt64))
            {
                return true;
            }
            if (t == typeof(float))
            {
                return true;
            }
            if (t == typeof(double))
            {
                return true;
            }
            if (t == typeof(decimal))
            {
                return true;
            }
            if (t == typeof(byte))
            {
                return true;
            }
            if (t == typeof(sbyte))
            {
                return true;
            }
            return false;
        }
        public static bool IsDecimal(Type t)
        { 
            if (t == typeof(float))
            {
                return true;
            }
            if (t == typeof(double))
            {
                return true;
            }
            if (t == typeof(decimal))
            {
                return true;
            } 
            return false;
        }
        public static bool CanConverToDecimal(string t)
        {
            decimal d = 0;
            if (decimal.TryParse(t, out d))
            {
                return true;
            }
            return false;
        }
        public static bool IsDateTime(Type t)
        {
            if (t == typeof(DateTime))
            {
                return true;
            }
            if (t == typeof(DateTime?))
            {
                return true;
            } 
            return false;
        }
        public static bool IsDateTime(object t)
        {
            return IsDateTime(t.GetType());
        }
        //public static string TrimStart(string text, string value)
        //{
        //    string str = text;
        //    if (text.StartsWith(value))
        //    {
        //        int index = value.Length;
        //        str = text.Substring(index, text.Length - index);
        //    }
        //    return str;
        //}
        //public static string TrimEnd(string text, string trimstr)
        //{
        //    int index = text.IndexOf(trimstr);
        //    if (index > 0)
        //    {
        //        text = text.Substring(0, index);
        //    }
        //    return text;
        //}
        public static bool Contains(string text, char c)
        {
            foreach (char cc in text)
            {
                if (cc == c)
                {
                    return true;
                }
            }
            return false;
        }


        public static DateTime Max(params DateTime[] args)
        {
            DateTime value = DateTime.MinValue;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] > value)
                {
                    value = args[i];
                }
            }
            return value;
        }
        public static DateTime Min(params DateTime[] args)
        {
            DateTime value = DateTime.MaxValue;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] < value)
                {
                    value = args[i];
                }
            }
            return value;
        }

        public static int Max(params int[] args)
        {
            int value = int.MinValue;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] > value)
                {
                    value = args[i];
                }
            }
            return value;
        }
        public static byte Max(params byte[] args)
        {
            byte value = byte.MinValue;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] > value)
                {
                    value = args[i];
                }
            }
            return value;
        }
        public static byte Min(params byte[] args)
        {
            byte value = byte.MaxValue;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] < value)
                {
                    value = args[i];
                }
            }
            return value;
        }
        public static int Min(params int[] args)
        {
            int value = int.MaxValue;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] < value)
                {
                    value = args[i];
                }
            }
            return value;
        }
        public static byte AVG(params byte[] args)
        {
            if (args.Length < 1)
                return 0;
            long value = 0;
            for (int i = 0; i < args.Length; i++)
            {
                value = value + args[i];
            }
            return Convert.ToByte(value / args.Length);
        }

        public static int Abs(int value)
        {
            return Math.Abs(value);
        }
        public static decimal Max(params decimal[] args)
        {
            decimal value = decimal.MinValue;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] > value)
                {
                    value = args[i];
                }
            }
            return value;
        }
        public static decimal Min(params decimal[] args)
        {
            decimal value = decimal.MaxValue;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] < value)
                {
                    value = args[i];
                }
            }
            return value;
        }
        public static decimal AVG(params decimal[] args)
        {
            if (args.Length < 1)
                return 0;
            decimal value = 0;
            for (int i = 0; i < args.Length; i++)
            {
                value = value + args[i];
            }
            return Convert.ToDecimal(value / args.Length);
        }

        public static int AtLeastLessThan(int minvalue, int value)
        {
            if (minvalue < value)
            {
                return value; 
            }
            return minvalue;
        }
        /// <summary>
        /// eq. minvalue=10 value=11 return 10
        /// </summary>
        /// <param name="maxvalue"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int AtleastMoreThan(int minvalue, int value)
        {
            if (minvalue < value)
            {
                return value;
            }
            return minvalue;
        }
        public static decimal Divide(decimal d1, decimal d2)
        {
            if (d2 == 0)
                return 0;
            return d1 / d2;
        }
        public static string GetFirstText(string[] texts)
        {
            if (texts.Length > 0)
                return texts[0];
            return string.Empty;
        }
        public static string GetSecondText(string[] texts)
        {
            if (texts.Length > 1)
                return texts[1];
            return string.Empty;
        }
        public static string GetThirdText(string[] texts)
        {
            if (texts.Length > 2)
                return texts[2];
            return string.Empty;
        }

        public static string Trim(string source)
        {
            return Trim(source, "");
        }
        public static string Trim(string source, string text)
        {
            if (text == "")
            {
                return source.Trim();
            }
            return source.Trim(text.ToCharArray());
        }
        public static string TrimStart(string source)
        {
            return TrimStart(source, "");
        }
        public static string TrimStart(string source, string text)
        {
            if (text == "")
            {
                return source.TrimStart();
            }
            return source.TrimStart(text.ToCharArray());
        }
        public static string TrimEnd(string source)
        {
            return TrimEnd(source, "");
        }
        public static string TrimEnd(string source, string text)
        {
            if (text == "")
            {
                return source.TrimEnd();
            }
            return source.TrimEnd(text.ToCharArray());
        }

        public static string GetFixedSizeText(string s, string symbol, int len)
        {
            if (s.Length <= len)
            {
                return s;
            }
            int index = s.IndexOf(symbol, s.Length - len);
            if (index > 0)
            {
                index = index + 1;
            }
            else
            {
                index = (s.Length - len + 1);
            }
            if (index < s.Length)
            {
                return s.Substring(index);
            }
            return s;
        }

        /// <summary>
        /// 角度转弧度
        /// </summary>
        /// <param name="degrees">角度</param>
        /// <returns>180度结果3.14151926</returns>
        public static double ConvertDegreesToRadians(double degrees)
        {
            double radians = (Math.PI / 180) * degrees;
            return (radians);
        }

        /// <summary>
        /// 弧度转角度
        /// </summary>
        /// <param name="degrees">角度</param>
        /// <returns></returns>
        public static double ConvertRadiansToDegrees(double radians)
        {
            double degrees = radians / (Math.PI / 180);
            return (degrees);
        }

        public static string TrimInChars(string source, string text)
        {
            string value = TrimStartInChars(source, text);
            value = TrimEndInChars(value, text);
            return value;
        }

        public static string TrimStartInChars(string source, string text)
        {
            int start = -1;
            for (int i = 0; i < source.Length; i++)
            {
                if (text.Contains(source[i].ToString()))
                {
                    start = i;
                }
                else
                {
                    break;
                }
            }
            if (start > 0)
            {
                string value = source.Substring(start + 1, source.Length - start - 1);
                return value;
            }
            return source;
        }

        public static string TrimEndInChars(string source, string text)
        {
            int start = -1;
            for (int i = source.Length - 1; i >= 0; i--)
            {
                if (text.Contains(source[i].ToString()))
                {
                    start = i;
                }
                else
                {
                    break;
                }
            }
            if (start > 0)
            {
                string value = source.Substring(0, start);
                return value;
            }
            return source;
        }

        public static Size ToSize(SizeF sf)
        {
            return new Size(ConvertHelper.ToInt32(sf.Width), ConvertHelper.ToInt32(sf.Height));
        }

        public static int Round(double value, int digit)
        {
            double d = Math.Pow(10, digit);
            double dvalue = value / d;
            dvalue = Math.Ceiling(dvalue) * d;
            return (int)dvalue;
        }

        public static int Round(decimal value, int digit)
        {
            double d = Math.Pow(10, digit);
            double dvalue = Convert.ToDouble(value) / d;
            dvalue = Math.Round(dvalue);
            dvalue = dvalue * d;
            return (int)dvalue;
        }

        public static void SortValue(Dictionary<string, int> dics, List<string> list)
        {
            Dictionary<string, int> dics2 = new Dictionary<string, int>();
            List<int> list2 = new List<int>();
            foreach (var k in dics)
            {
                list2.Add(k.Value);
                dics2.Add(k.Key, k.Value);
            }
            list2.Sort();
            int cindex = 0;
            for (int i = 0; i < list2.Count; i++)
            {
                cindex++;
                foreach (var k in dics2)
                {
                    if (k.Value <= list2[i])
                    {
                        dics[k.Key] = cindex;
                        dics2.Remove(k.Key);
                        list.Add(k.Key);
                        break;
                    }
                }
            }
        }

        public static bool IsEmpty(byte[] p)
        {
            if (p == null)
                return true;
            if (p.Length < 1)
                return true;
            return false;
        }

        public static new bool Equals(object obj1,object obj2)
        {
            if (obj1 == obj2)
                return true;
            if (obj1 == null)
                return false;
            if (obj2 == null)
                return false;
            if (obj1.GetType() == obj2.GetType())
            {
                return obj1.Equals(obj2);
            }
            if (IsDecimalNumber(obj1) && IsDecimalNumber(obj2))
            {
                return ToDecimal(obj1) == ToDecimal(obj2);
            }
            if (IsDoubleNumber(obj1) && IsDoubleNumber(obj2))
            {
                return ToDecimal(obj1) == ToDecimal(obj2);
            }
            return obj1 == obj2;
        }

        public static bool IsString(object obj)
        {
            return obj.GetType() == typeof(string);
        }

        public static char Get36Char(int value)
        {
            char[] chars = {'0', '1', '2', '3', '4', '5', '6', '7', '8', '9',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N',
            'O','P','Q','R','S','T','U','V','W','X','Y','Z'
            };
            char c = chars[value];
            return c;
        }

        public static string IntTo36(int data)
        {
            string str = string.Empty;
            int value = data;
            for (int i = 0; i < 20; i++)
            {
                int index = 0;
                if (value >= 36)
                {
                    value = Math.DivRem(value, 36, out index);
                    str = str + Get36Char(index);
                }
                else
                {
                    str = str + Get36Char(value);
                    break;
                }
            }
            return str;
        }

        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.Now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public static string GetTimeStamp(DateTime now)
        {
            TimeSpan ts = now - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
    public enum FormatType
    {
        Null,
        Numberic,
        DateTime 
    }
}
