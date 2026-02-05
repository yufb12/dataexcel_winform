using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Utils
{
    public enum ResultObjectType
    {
        Sussess,
        Failed,
        Unkown
    }

    public class ResultObject
    {
        public ResultObjectType Type
        {
            get;
            set;
        }
        public object Value { get; set; }
        public object Tag { get; set; }
    }

    public class MathHelper
    {
        public static object Add(object value1, object value2)
        { 
                object value = null;
            if (Feng.Utils.ConvertHelper.IsIntNumber(value1)
&& Feng.Utils.ConvertHelper.IsIntNumber(value2))
            {
                value = Add(Feng.Utils.ConvertHelper.ToInt64(value1), Feng.Utils.ConvertHelper.ToInt64(value2));
            } 
            else if (
Feng.Utils.ConvertHelper.IsDecimalNumber(value1)
&& Feng.Utils.ConvertHelper.IsDecimalNumber(value2))
            {
                value = Add(Feng.Utils.ConvertHelper.ToDecimal(value1), Feng.Utils.ConvertHelper.ToDecimal(value2));
            }
            else if (
            Feng.Utils.ConvertHelper.IsNumber(value1)
&& Feng.Utils.ConvertHelper.IsNumber(value2))
            {
                value = Add(Feng.Utils.ConvertHelper.ToDouble(value1), Feng.Utils.ConvertHelper.ToDouble(value2));
            }
            else
            {
                decimal dvalue1 = 0;
                decimal dvalue2 = 0;
                if (!StringIsNumber(value1, out dvalue1) || !StringIsNumber(value2, out dvalue2))
                {
                    return Feng.Utils.ConvertHelper.ToString(value1) + Feng.Utils.ConvertHelper.ToString(value2);
                }
                else
                {
                    value = Add(dvalue1, dvalue2);
                }
            }

            return value;
        }
        public static bool StringIsNumber(object value, out decimal d)
        {
            d = 0;
            if (value == null)
                return false;
            if (Feng.Utils.ConvertHelper.IsNumber(value.GetType()))
            {
                d = Feng.Utils.ConvertHelper.ToDecimal(value);
                return true;
            }
            if (value is string)
            { 
                if (decimal.TryParse(value.ToString(), out d))
                {
                    return true;
                }    
            }
            return false;
        }
        public static int Add(sbyte value1, sbyte value2)
        {
            return value1 + value2;
        }
        public static int Add(byte value1, byte value2)
        {
            return value1 + value2;
        }
        public static int Add(short value1, short value2)
        {
            return value1 + value2;
        }
        public static int Add(ushort value1, ushort value2)
        {
            return value1 + value2;
        }
        public static int Add(int value1, int value2)
        {
            return value1 + value2;
        }
        public static uint Add(uint value1, uint value2)
        {
            return value1 + value2;
        }
        public static long Add(long value1, long value2)
        {
            return value1 + value2;
        }
        public static ulong Add(ulong value1, ulong value2)
        {
            return value1 + value2;
        }
        public static float Add(float value1, float value2)
        {
            return value1 + value2;
        }
        public static double Add(double value1, double value2)
        {
            return value1 + value2;
        }
        public static decimal Add(decimal value1, decimal value2)
        {
            return value1 + value2;
        }


        public static object Minus(object value1, object value2)
        {
            if (value1 is string)
            {
                return Minus(Feng.Utils.ConvertHelper.ToDecimal(value1), Feng.Utils.ConvertHelper.ToDecimal(value2));
            }
            object vlaue = null;
            if ((value1 is sbyte
|| value1 is byte
|| value1 is byte
|| value1 is short
|| value1 is ushort
|| value1 is int
|| value1 is uint)
&& (value2 is sbyte
|| value2 is byte
|| value2 is byte
|| value2 is short
|| value2 is ushort
|| value2 is int
|| value2 is uint))
            {
                vlaue = Minus(Feng.Utils.ConvertHelper.ToInt64(value1), Feng.Utils.ConvertHelper.ToInt64(value2));
            }
            else if (
    (
    value1 is sbyte
    || value1 is byte
    || value1 is byte
    || value1 is short
    || value1 is ushort
    || value1 is int
    || value1 is uint
    || value1 is long
    || value1 is ulong)
    && (
    value2 is sbyte
    || value2 is byte
    || value2 is byte
    || value2 is short
    || value2 is ushort
    || value2 is int
    || value2 is uint
    || value2 is long
    || value2 is ulong)

    )
            {
                vlaue = Minus(Feng.Utils.ConvertHelper.ToInt64(value1), Feng.Utils.ConvertHelper.ToInt64(value2));
            }
            else if (
(
value1 is sbyte
|| value1 is byte
|| value1 is byte
|| value1 is short
|| value1 is ushort
|| value1 is int
|| value1 is uint
|| value1 is long
|| value1 is ulong
|| value1 is decimal)
&& (
value2 is sbyte
|| value2 is byte
|| value2 is byte
|| value2 is short
|| value2 is ushort
|| value2 is int
|| value2 is uint
|| value2 is long
|| value2 is ulong
|| value2 is decimal)

)
            {
                vlaue = Minus(Feng.Utils.ConvertHelper.ToDecimal(value1), Feng.Utils.ConvertHelper.ToDecimal(value2));
            }
            else if (
            (
            value1 is sbyte
            || value1 is byte
            || value1 is byte
            || value1 is short
            || value1 is ushort
            || value1 is int
            || value1 is uint
            || value1 is long
            || value1 is ulong
            || value1 is decimal
            || value1 is float
            || value1 is double)
            && (
            value2 is sbyte
            || value2 is byte
            || value2 is byte
            || value2 is short
            || value2 is ushort
            || value2 is int
            || value2 is uint
            || value2 is long
            || value2 is ulong
            || value2 is decimal
            || value2 is float
            || value2 is double)

            )
            {
                vlaue = Minus(Feng.Utils.ConvertHelper.ToDouble(value1), Feng.Utils.ConvertHelper.ToDouble(value2));
            }
            
            return vlaue;
        }
        public static int Minus(sbyte value1, sbyte value2)
        {
            return value1 - value2;
        }
        public static int Minus(byte value1, byte value2)
        {
            return value1 - value2;
        }
        public static int Minus(short value1, short value2)
        {
            return value1 - value2;
        }
        public static int Minus(ushort value1, ushort value2)
        {
            return value1 - value2;
        }
        public static int Minus(int value1, int value2)
        {
            return value1 - value2;
        }
        public static uint Minus(uint value1, uint value2)
        {
            return value1 - value2;
        }
        public static long Minus(long value1, long value2)
        {
            return value1 - value2;
        }
        public static ulong Minus(ulong value1, ulong value2)
        {
            return value1 - value2;
        }
        public static float Minus(float value1, float value2)
        {
            return value1 - value2;
        }
        public static double Minus(double value1, double value2)
        {
            return value1 - value2;
        }
        public static decimal Minus(decimal value1, decimal value2)
        {
            return value1 - value2;
        }



        public static object Multiply(object value1, object value2)
        {
            if (value1 is string)
            {
                return Multiply(Feng.Utils.ConvertHelper.ToDecimal(value1), Feng.Utils.ConvertHelper.ToDecimal(value2));
            }
            object vlaue = null;
            if ((value1 is sbyte
|| value1 is byte
|| value1 is byte
|| value1 is short
|| value1 is ushort
|| value1 is int
|| value1 is uint)
&& (value2 is sbyte
|| value2 is byte
|| value2 is byte
|| value2 is short
|| value2 is ushort
|| value2 is int
|| value2 is uint))
            {
                vlaue = Multiply(Feng.Utils.ConvertHelper.ToInt64(value1), Feng.Utils.ConvertHelper.ToInt64(value2));
            }
            else if (
    (
    value1 is sbyte
    || value1 is byte
    || value1 is byte
    || value1 is short
    || value1 is ushort
    || value1 is int
    || value1 is uint
    || value1 is long
    || value1 is ulong)
    && (
    value2 is sbyte
    || value2 is byte
    || value2 is byte
    || value2 is short
    || value2 is ushort
    || value2 is int
    || value2 is uint
    || value2 is long
    || value2 is ulong)

    )
            {
                vlaue = Multiply(Feng.Utils.ConvertHelper.ToInt64(value1), Feng.Utils.ConvertHelper.ToInt64(value2));
            }
            else if (
(
value1 is sbyte
|| value1 is byte
|| value1 is byte
|| value1 is short
|| value1 is ushort
|| value1 is int
|| value1 is uint
|| value1 is long
|| value1 is ulong
|| value1 is decimal)
&& (
value2 is sbyte
|| value2 is byte
|| value2 is byte
|| value2 is short
|| value2 is ushort
|| value2 is int
|| value2 is uint
|| value2 is long
|| value2 is ulong
|| value2 is decimal)

)
            {
                vlaue = Multiply(Feng.Utils.ConvertHelper.ToDecimal(value1), Feng.Utils.ConvertHelper.ToDecimal(value2));
            }
            else if (
            (
            value1 is sbyte
            || value1 is byte
            || value1 is byte
            || value1 is short
            || value1 is ushort
            || value1 is int
            || value1 is uint
            || value1 is long
            || value1 is ulong
            || value1 is decimal
            || value1 is float
            || value1 is double)
            && (
            value2 is sbyte
            || value2 is byte
            || value2 is byte
            || value2 is short
            || value2 is ushort
            || value2 is int
            || value2 is uint
            || value2 is long
            || value2 is ulong
            || value2 is decimal
            || value2 is float
            || value2 is double)

            )
            {
                vlaue = Multiply(Feng.Utils.ConvertHelper.ToDouble(value1), Feng.Utils.ConvertHelper.ToDouble(value2));
            }
            return vlaue;
        }
        public static int Multiply(sbyte value1, sbyte value2)
        {
            return value1 * value2;
        }
        public static int Multiply(byte value1, byte value2)
        {
            return value1 * value2;
        }
        public static int Multiply(short value1, short value2)
        {
            return value1 * value2;
        }
        public static int Multiply(ushort value1, ushort value2)
        {
            return value1 * value2;
        }
        public static int Multiply(int value1, int value2)
        {
            return value1 * value2;
        }
        public static uint Multiply(uint value1, uint value2)
        {
            return value1 * value2;
        }
        public static long Multiply(long value1, long value2)
        {
            return value1 * value2;
        }
        public static ulong Multiply(ulong value1, ulong value2)
        {
            return value1 * value2;
        }
        public static float Multiply(float value1, float value2)
        {
            return value1 * value2;
        }
        public static double Multiply(double value1, double value2)
        {
            return value1 * value2;
        }
        public static decimal Multiply(decimal value1, decimal value2)
        {
            return value1 * value2;
        }



        public static object Divide(object value1, object value2)
        {
            if (value1 is string)
            {
                return Divide(Feng.Utils.ConvertHelper.ToDecimal(value1), Feng.Utils.ConvertHelper.ToDecimal(value2));
            }
            object vlaue = null;
            if ((value1 is sbyte
|| value1 is byte
|| value1 is byte
|| value1 is short
|| value1 is ushort
|| value1 is int
|| value1 is uint)
&& (value2 is sbyte
|| value2 is byte
|| value2 is byte
|| value2 is short
|| value2 is ushort
|| value2 is int
|| value2 is uint))
            {
                vlaue = Divide(Feng.Utils.ConvertHelper.ToInt64(value1), Feng.Utils.ConvertHelper.ToInt64(value2));
            }
            else if (
    (
    value1 is sbyte
    || value1 is byte
    || value1 is byte
    || value1 is short
    || value1 is ushort
    || value1 is int
    || value1 is uint
    || value1 is long
    || value1 is ulong)
    && (
    value2 is sbyte
    || value2 is byte
    || value2 is byte
    || value2 is short
    || value2 is ushort
    || value2 is int
    || value2 is uint
    || value2 is long
    || value2 is ulong)

    )
            {
                vlaue = Divide(Feng.Utils.ConvertHelper.ToInt64(value1), Feng.Utils.ConvertHelper.ToInt64(value2));
            }
            else if (
(
value1 is sbyte
|| value1 is byte
|| value1 is byte
|| value1 is short
|| value1 is ushort
|| value1 is int
|| value1 is uint
|| value1 is long
|| value1 is ulong
|| value1 is decimal)
&& (
value2 is sbyte
|| value2 is byte
|| value2 is byte
|| value2 is short
|| value2 is ushort
|| value2 is int
|| value2 is uint
|| value2 is long
|| value2 is ulong
|| value2 is decimal)

)
            {
                vlaue = Divide(Feng.Utils.ConvertHelper.ToDecimal(value1), Feng.Utils.ConvertHelper.ToDecimal(value2));
            }
            else if (
            (
            value1 is sbyte
            || value1 is byte
            || value1 is byte
            || value1 is short
            || value1 is ushort
            || value1 is int
            || value1 is uint
            || value1 is long
            || value1 is ulong
            || value1 is decimal
            || value1 is float
            || value1 is double)
            && (
            value2 is sbyte
            || value2 is byte
            || value2 is byte
            || value2 is short
            || value2 is ushort
            || value2 is int
            || value2 is uint
            || value2 is long
            || value2 is ulong
            || value2 is decimal
            || value2 is float
            || value2 is double)

            )
            {
                vlaue = Divide(Feng.Utils.ConvertHelper.ToDouble(value1), Feng.Utils.ConvertHelper.ToDouble(value2));
            }
            return vlaue;
        }
        public static int Divide(sbyte value1, sbyte value2)
        {
            return value1 / value2;
        }
        public static int Divide(byte value1, byte value2)
        {
            return value1 / value2;
        }
        public static int Divide(short value1, short value2)
        {
            return value1 / value2;
        }
        public static int Divide(ushort value1, ushort value2)
        {
            return value1 / value2;
        }
        public static int Divide(int value1, int value2)
        {
            return value1 / value2;
        }
        public static uint Divide(uint value1, uint value2)
        {
            return value1 / value2;
        }
        public static long Divide(long value1, long value2)
        {
            return value1 / value2;
        }
        public static ulong Divide(ulong value1, ulong value2)
        {
            return value1 / value2;
        }
        public static float Divide(float value1, float value2)
        {
            return value1 / value2;
        }
        public static double Divide(double value1, double value2)
        {
            return value1 / value2;
        }
        public static decimal Divide(decimal value1, decimal value2)
        {
            return value1 / value2;
        }
    }

}
