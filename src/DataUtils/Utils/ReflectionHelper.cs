using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;
using System.Data;

namespace Feng.Utils
{
    public class ReflectionHelper
    {
        public static object CreateInstance(string modules, string typename)
        {
            string __path = Feng.IO.FileHelper.GetStartUpFileUSER("Temp", "\\" + modules);
            System.Reflection.Assembly __assembly = System.Reflection.Assembly.LoadFrom(__path);

            Type[] types = __assembly.GetTypes();
            object form = null;
            foreach (Type type in types)
            {
                if (type.FullName == typename)
                {
                    form = __assembly.CreateInstance(typename, false);

                    break;
                }
            }
            return form;

        }

        public static object CreateInstance(string modules, string typename, string[] args)
        {
            string __path = Feng.IO.FileHelper.GetStartUpFileUSER("Temp", "\\" + modules);
            System.Reflection.Assembly __assembly = System.Reflection.Assembly.LoadFrom(__path);

            object[] parameters = new object[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                parameters[i] = args[i];
            }
            object form = __assembly.CreateInstance(typename, true, System.Reflection.BindingFlags.Default, null, parameters, null, null);

            return form;
        }

        public static object CreateInstance(string modules, string typename, string args)
        {
            if (args == string.Empty)
            {
                return CreateInstance(modules, typename);
            }

            string[] obj = System.Text.RegularExpressions.Regex.Split(args, ",");

            return CreateInstance(modules, typename, obj);

        }

        public static object GetValue(object obj, string valuemember)
        {
            if (obj == null)
                return null;
            if (valuemember == null)
                return null;
            if (string.IsNullOrWhiteSpace(valuemember))
                return null;
            DataRow row = obj as DataRow;
            if (row != null)
            {
                if (row.Table.Columns.Contains(valuemember))
                {
                    return row[valuemember];
                }
            }
            else
            {
                object valuep1 = obj.GetType().GetProperty(valuemember).GetValue(obj, null);
                return valuep1;
            }
            return null;
        }
        public static void SetValue(object obj, string field, object value)
        {
            if (obj == null)
                return;
            PropertyInfo pi = obj.GetType().GetProperty(field);
            if (pi == null)
                return;
            pi.SetValue(obj, value, null);
        }
        public static void SetValue(PropertyInfo pt, object obj, object strvalue)
        {
            if (strvalue != null)
            {
                if (pt.PropertyType == strvalue.GetType())
                {
                    pt.SetValue(obj, strvalue, null);
                    return;
                }

                if (strvalue.GetType() == typeof(string))
                {
                    SetingValue(pt, obj, (string)strvalue);
                    return;
                }
                if (strvalue.GetType() == typeof(decimal))
                {
                    SetingValue(pt, obj, (decimal)strvalue);
                    return;
                }
                if (strvalue.GetType() == typeof(double))
                {
                    SetingValue(pt, obj, (double)strvalue);
                    return;
                }
                if (strvalue.GetType() == typeof(float))
                {
                    SetingValue(pt, obj, (float)strvalue);
                    return;
                }
                if (strvalue.GetType() == typeof(bool))
                {
                    SetingValue(pt, obj, (bool)strvalue);
                    return;
                }
                if (strvalue.GetType() == typeof(byte))
                {
                    SetingValue(pt, obj, (byte)strvalue);
                    return;
                }
                if (strvalue.GetType() == typeof(sbyte))
                {
                    SetingValue(pt, obj, (sbyte)strvalue);
                    return;
                }
                if (strvalue.GetType() == typeof(Int16))
                {
                    SetingValue(pt, obj, (Int16)strvalue);
                    return;
                }
                if (strvalue.GetType() == typeof(Int32))
                {
                    SetingValue(pt, obj, (Int32)strvalue);
                    return;
                }
                if (strvalue.GetType() == typeof(Int64))
                {
                    SetingValue(pt, obj, (Int64)strvalue);
                    return;
                }
                if (strvalue.GetType() == typeof(UInt16))
                {
                    SetingValue(pt, obj, (UInt16)strvalue);
                    return;
                }
                if (strvalue.GetType() == typeof(UInt32))
                {
                    SetingValue(pt, obj, (UInt32)strvalue);
                    return;
                }
                if (strvalue.GetType() == typeof(UInt64))
                {
                    SetingValue(pt, obj, (UInt64)strvalue);
                    return;
                }
                if (strvalue.GetType() == typeof(DateTime))
                {
                    SetingValue(pt, obj, (DateTime)strvalue);
                    return;
                }
                pt.SetValue(obj, strvalue, null);
            }
            else
            {
                pt.SetValue(obj, null, null);
            }
        }

        private static void SetingValue(PropertyInfo pt, object obj, string strvalue)
        {
            if (pt == null)
                return;
            if (pt.PropertyType == typeof(bool))
            {
                bool value = false;
                if (bool.TryParse(strvalue, out value))
                {
                    pt.SetValue(obj, value, null);
                }
                else
                {

                    pt.SetValue(obj, false, null);
                }
                return;
            }

            if (pt.PropertyType == typeof(Int16))
            {
                Int16 value;
                if (Int16.TryParse(strvalue, out value))
                {
                    pt.SetValue(obj, value, null);
                }
                else
                {
                    pt.SetValue(obj, value, null);
                }
                return;
            }
            if (pt.PropertyType == typeof(Int32))
            {
                Int32 value;
                if (Int32.TryParse(strvalue, out value))
                {
                    pt.SetValue(obj, value, null);
                }
                else
                {
                    pt.SetValue(obj, value, null);
                }
                return;
            }
            if (pt.PropertyType == typeof(Int64))
            {
                Int64 value;
                if (Int64.TryParse(strvalue, out value))
                {
                    pt.SetValue(obj, value, null);
                }
                else
                {
                    pt.SetValue(obj, value, null);
                }
                return;
            }
            if (pt.PropertyType == typeof(UInt16))
            {
                UInt16 value;
                if (UInt16.TryParse(strvalue, out value))
                {
                    pt.SetValue(obj, value, null);
                }
                else
                {
                    pt.SetValue(obj, value, null);
                }
                return;
            }
            if (pt.PropertyType == typeof(UInt32))
            {
                UInt32 value;
                if (UInt32.TryParse(strvalue, out value))
                {
                    pt.SetValue(obj, value, null);
                }
                else
                {
                    pt.SetValue(obj, value, null);
                }
                return;
            }
            if (pt.PropertyType == typeof(UInt64))
            {
                UInt64 value;
                if (UInt64.TryParse(strvalue, out value))
                {
                    pt.SetValue(obj, value, null);
                }
                else
                {
                    pt.SetValue(obj, value, null);
                }
                return;
            }
            if (pt.PropertyType == typeof(float))
            {
                float value;
                if (float.TryParse(strvalue, out value))
                {
                    pt.SetValue(obj, value, null);
                }
                else
                {
                    pt.SetValue(obj, value, null);
                }
                return;
            }
            if (pt.PropertyType == typeof(double))
            {
                double value;
                if (double.TryParse(strvalue, out value))
                {
                    pt.SetValue(obj, value, null);
                }
                else
                {
                    pt.SetValue(obj, value, null);
                }
                return;
            }
            if (pt.PropertyType == typeof(decimal))
            {
                decimal value;
                if (decimal.TryParse(strvalue, out value))
                {
                    pt.SetValue(obj, value, null);
                }
                else
                {
                    pt.SetValue(obj, value, null);
                }
                return;
            }
            if (pt.PropertyType == typeof(DateTime))
            {
                DateTime value;
                if (DateTime.TryParse(strvalue, out value))
                {
                    pt.SetValue(obj, value, null);
                }
                else
                {
                    pt.SetValue(obj, value, null);
                }
                return;
            }
            if (pt.PropertyType == typeof(Enum))
            {
                Int32 value;
                if (Int32.TryParse(strvalue, out value))
                {
                    pt.SetValue(obj, value, null);
                }
                else
                {
                    pt.SetValue(obj, value, null);
                }
                return;
            }
            if (pt.PropertyType == typeof(string))
            {
                pt.SetValue(obj, strvalue, null);
            }
            else
            {
                pt.SetValue(obj, null, null);
            }
            pt.SetValue(obj, null, null);
        }

        private static void SetingValue(PropertyInfo pt, object obj, decimal rvalue)
        {
            if (pt == null)
                return;
            if (pt.PropertyType == typeof(bool))
            {
                bool value = Convert.ToBoolean(rvalue);
                pt.SetValue(obj, false, null);

                return;
            }

            if (pt.PropertyType == typeof(Int16))
            {
                Int16 value = Convert.ToInt16(rvalue);
                pt.SetValue(obj, value, null);

                return;
            }
            if (pt.PropertyType == typeof(Int32))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Int64))
            {
                Int64 value = Convert.ToInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt16))
            {
                UInt16 value = Convert.ToUInt16(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt32))
            {
                UInt32 value = Convert.ToUInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt64))
            {
                UInt64 value = Convert.ToUInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(float))
            {
                float value = Convert.ToSingle(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(double))
            {
                double value = Convert.ToDouble(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(decimal))
            {
                decimal value = Convert.ToDecimal(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(DateTime))
            {
                DateTime value = Convert.ToDateTime(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Enum))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(string))
            {
                string value = Convert.ToString(rvalue);
                pt.SetValue(obj, rvalue, null);
                return;
            }
            pt.SetValue(obj, null, null);
        }

        private static void SetingValue(PropertyInfo pt, object obj, double rvalue)
        {
            if (pt == null)
                return;
            if (pt.PropertyType == typeof(bool))
            {
                bool value = Convert.ToBoolean(rvalue);
                pt.SetValue(obj, false, null);

                return;
            }

            if (pt.PropertyType == typeof(Int16))
            {
                Int16 value = Convert.ToInt16(rvalue);
                pt.SetValue(obj, value, null);

                return;
            }
            if (pt.PropertyType == typeof(Int32))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Int64))
            {
                Int64 value = Convert.ToInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt16))
            {
                UInt16 value = Convert.ToUInt16(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt32))
            {
                UInt32 value = Convert.ToUInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt64))
            {
                UInt64 value = Convert.ToUInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(float))
            {
                float value = Convert.ToSingle(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(double))
            {
                double value = Convert.ToDouble(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(decimal))
            {
                decimal value = Convert.ToDecimal(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(DateTime))
            {
                DateTime value = Convert.ToDateTime(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Enum))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(string))
            {
                string value = Convert.ToString(rvalue);
                pt.SetValue(obj, rvalue, null);
                return;
            }
            pt.SetValue(obj, null, null);
        }

        private static void SetingValue(PropertyInfo pt, object obj, float rvalue)
        {
            if (pt == null)
                return;
            if (pt.PropertyType == typeof(bool))
            {
                bool value = Convert.ToBoolean(rvalue);
                pt.SetValue(obj, false, null);

                return;
            }

            if (pt.PropertyType == typeof(Int16))
            {
                Int16 value = Convert.ToInt16(rvalue);
                pt.SetValue(obj, value, null);

                return;
            }
            if (pt.PropertyType == typeof(Int32))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Int64))
            {
                Int64 value = Convert.ToInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt16))
            {
                UInt16 value = Convert.ToUInt16(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt32))
            {
                UInt32 value = Convert.ToUInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt64))
            {
                UInt64 value = Convert.ToUInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(float))
            {
                float value = Convert.ToSingle(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(double))
            {
                double value = Convert.ToDouble(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(decimal))
            {
                decimal value = Convert.ToDecimal(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(DateTime))
            {
                DateTime value = Convert.ToDateTime(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Enum))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(string))
            {
                string value = Convert.ToString(rvalue);
                pt.SetValue(obj, rvalue, null);
                return;
            }
            pt.SetValue(obj, null, null);
        }

        private static void SetingValue(PropertyInfo pt, object obj, UInt64 rvalue)
        {
            if (pt == null)
                return;
            if (pt.PropertyType == typeof(bool))
            {
                bool value = Convert.ToBoolean(rvalue);
                pt.SetValue(obj, false, null);

                return;
            }

            if (pt.PropertyType == typeof(Int16))
            {
                Int16 value = Convert.ToInt16(rvalue);
                pt.SetValue(obj, value, null);

                return;
            }
            if (pt.PropertyType == typeof(Int32))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Int64))
            {
                Int64 value = Convert.ToInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt16))
            {
                UInt16 value = Convert.ToUInt16(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt32))
            {
                UInt32 value = Convert.ToUInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt64))
            {
                UInt64 value = Convert.ToUInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(float))
            {
                float value = Convert.ToSingle(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(double))
            {
                double value = Convert.ToDouble(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(decimal))
            {
                decimal value = Convert.ToDecimal(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(DateTime))
            {
                DateTime value = Convert.ToDateTime(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Enum))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(string))
            {
                string value = Convert.ToString(rvalue);
                pt.SetValue(obj, rvalue, null);
                return;
            }
            pt.SetValue(obj, null, null);
        }

        private static void SetingValue(PropertyInfo pt, object obj, UInt32 rvalue)
        {
            if (pt == null)
                return;
            if (pt.PropertyType == typeof(bool))
            {
                bool value = Convert.ToBoolean(rvalue);
                pt.SetValue(obj, false, null);

                return;
            }

            if (pt.PropertyType == typeof(Int16))
            {
                Int16 value = Convert.ToInt16(rvalue);
                pt.SetValue(obj, value, null);

                return;
            }
            if (pt.PropertyType == typeof(Int32))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Int64))
            {
                Int64 value = Convert.ToInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt16))
            {
                UInt16 value = Convert.ToUInt16(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt32))
            {
                UInt32 value = Convert.ToUInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt64))
            {
                UInt64 value = Convert.ToUInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(float))
            {
                float value = Convert.ToSingle(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(double))
            {
                double value = Convert.ToDouble(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(decimal))
            {
                decimal value = Convert.ToDecimal(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(DateTime))
            {
                DateTime value = Convert.ToDateTime(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Enum))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(string))
            {
                string value = Convert.ToString(rvalue);
                pt.SetValue(obj, rvalue, null);
                return;
            }
            pt.SetValue(obj, null, null);
        }

        private static void SetingValue(PropertyInfo pt, object obj, UInt16 rvalue)
        {
            if (pt == null)
                return;
            if (pt.PropertyType == typeof(bool))
            {
                bool value = Convert.ToBoolean(rvalue);
                pt.SetValue(obj, false, null);

                return;
            }

            if (pt.PropertyType == typeof(Int16))
            {
                Int16 value = Convert.ToInt16(rvalue);
                pt.SetValue(obj, value, null);

                return;
            }
            if (pt.PropertyType == typeof(Int32))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Int64))
            {
                Int64 value = Convert.ToInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt16))
            {
                UInt16 value = Convert.ToUInt16(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt32))
            {
                UInt32 value = Convert.ToUInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt64))
            {
                UInt64 value = Convert.ToUInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(float))
            {
                float value = Convert.ToSingle(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(double))
            {
                double value = Convert.ToDouble(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(decimal))
            {
                decimal value = Convert.ToDecimal(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(DateTime))
            {
                DateTime value = Convert.ToDateTime(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Enum))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(string))
            {
                string value = Convert.ToString(rvalue);
                pt.SetValue(obj, rvalue, null);
                return;
            }
            pt.SetValue(obj, null, null);
        }

        private static void SetingValue(PropertyInfo pt, object obj, Int64 rvalue)
        {
            if (pt == null)
                return;
            if (pt.PropertyType == typeof(bool))
            {
                bool value = Convert.ToBoolean(rvalue);
                pt.SetValue(obj, false, null);

                return;
            }

            if (pt.PropertyType == typeof(Int16))
            {
                Int16 value = Convert.ToInt16(rvalue);
                pt.SetValue(obj, value, null);

                return;
            }
            if (pt.PropertyType == typeof(Int32))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Int64))
            {
                Int64 value = Convert.ToInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt16))
            {
                UInt16 value = Convert.ToUInt16(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt32))
            {
                UInt32 value = Convert.ToUInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt64))
            {
                UInt64 value = Convert.ToUInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(float))
            {
                float value = Convert.ToSingle(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(double))
            {
                double value = Convert.ToDouble(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(decimal))
            {
                decimal value = Convert.ToDecimal(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(DateTime))
            {
                DateTime value = Convert.ToDateTime(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Enum))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(string))
            {
                string value = Convert.ToString(rvalue);
                pt.SetValue(obj, rvalue, null);
                return;
            }
            pt.SetValue(obj, null, null);
        }

        private static void SetingValue(PropertyInfo pt, object obj, Int32 rvalue)
        {
            if (pt == null)
                return;
            if (pt.PropertyType == typeof(bool))
            {
                bool value = Convert.ToBoolean(rvalue);
                pt.SetValue(obj, false, null);

                return;
            }

            if (pt.PropertyType == typeof(Int16))
            {
                Int16 value = Convert.ToInt16(rvalue);
                pt.SetValue(obj, value, null);

                return;
            }
            if (pt.PropertyType == typeof(Int32))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Int64))
            {
                Int64 value = Convert.ToInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt16))
            {
                UInt16 value = Convert.ToUInt16(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt32))
            {
                UInt32 value = Convert.ToUInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt64))
            {
                UInt64 value = Convert.ToUInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(float))
            {
                float value = Convert.ToSingle(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(double))
            {
                double value = Convert.ToDouble(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(decimal))
            {
                decimal value = Convert.ToDecimal(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(DateTime))
            {
                DateTime value = Convert.ToDateTime(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Enum))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(string))
            {
                string value = Convert.ToString(rvalue);
                pt.SetValue(obj, rvalue, null);
                return;
            }
            pt.SetValue(obj, null, null);
        }

        private static void SetingValue(PropertyInfo pt, object obj, Int16 rvalue)
        {
            if (pt == null)
                return;
            if (pt.PropertyType == typeof(bool))
            {
                bool value = Convert.ToBoolean(rvalue);
                pt.SetValue(obj, false, null);

                return;
            }

            if (pt.PropertyType == typeof(Int16))
            {
                Int16 value = Convert.ToInt16(rvalue);
                pt.SetValue(obj, value, null);

                return;
            }
            if (pt.PropertyType == typeof(Int32))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Int64))
            {
                Int64 value = Convert.ToInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt16))
            {
                UInt16 value = Convert.ToUInt16(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt32))
            {
                UInt32 value = Convert.ToUInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt64))
            {
                UInt64 value = Convert.ToUInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(float))
            {
                float value = Convert.ToSingle(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(double))
            {
                double value = Convert.ToDouble(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(decimal))
            {
                decimal value = Convert.ToDecimal(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(DateTime))
            {
                DateTime value = Convert.ToDateTime(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Enum))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(string))
            {
                string value = Convert.ToString(rvalue);
                pt.SetValue(obj, rvalue, null);
                return;
            }
            pt.SetValue(obj, null, null);
        }

        private static void SetingValue(PropertyInfo pt, object obj, bool rvalue)
        {
            if (pt == null)
                return;
            if (pt.PropertyType == typeof(bool))
            {
                bool value = Convert.ToBoolean(rvalue);
                pt.SetValue(obj, false, null);

                return;
            }

            if (pt.PropertyType == typeof(Int16))
            {
                Int16 value = Convert.ToInt16(rvalue);
                pt.SetValue(obj, value, null);

                return;
            }
            if (pt.PropertyType == typeof(Int32))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Int64))
            {
                Int64 value = Convert.ToInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt16))
            {
                UInt16 value = Convert.ToUInt16(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt32))
            {
                UInt32 value = Convert.ToUInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt64))
            {
                UInt64 value = Convert.ToUInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(float))
            {
                float value = Convert.ToSingle(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(double))
            {
                double value = Convert.ToDouble(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(decimal))
            {
                decimal value = Convert.ToDecimal(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(DateTime))
            {
                DateTime value = Convert.ToDateTime(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Enum))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(string))
            {
                string value = Convert.ToString(rvalue);
                pt.SetValue(obj, rvalue, null);
                return;
            }
            pt.SetValue(obj, null, null);
        }

        private static void SetingValue(PropertyInfo pt, object obj, byte rvalue)
        {
            if (pt == null)
                return;
            if (pt.PropertyType == typeof(bool))
            {
                bool value = Convert.ToBoolean(rvalue);
                pt.SetValue(obj, false, null);

                return;
            }

            if (pt.PropertyType == typeof(Int16))
            {
                Int16 value = Convert.ToInt16(rvalue);
                pt.SetValue(obj, value, null);

                return;
            }
            if (pt.PropertyType == typeof(Int32))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Int64))
            {
                Int64 value = Convert.ToInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt16))
            {
                UInt16 value = Convert.ToUInt16(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt32))
            {
                UInt32 value = Convert.ToUInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt64))
            {
                UInt64 value = Convert.ToUInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(float))
            {
                float value = Convert.ToSingle(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(double))
            {
                double value = Convert.ToDouble(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(decimal))
            {
                decimal value = Convert.ToDecimal(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(DateTime))
            {
                DateTime value = Convert.ToDateTime(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Enum))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(string))
            {
                string value = Convert.ToString(rvalue);
                pt.SetValue(obj, rvalue, null);
                return;
            }
            pt.SetValue(obj, null, null);
        }

        private static void SetingValue(PropertyInfo pt, object obj, sbyte rvalue)
        {
            if (pt == null)
                return;
            if (pt.PropertyType == typeof(bool))
            {
                bool value = Convert.ToBoolean(rvalue);
                pt.SetValue(obj, false, null);

                return;
            }

            if (pt.PropertyType == typeof(Int16))
            {
                Int16 value = Convert.ToInt16(rvalue);
                pt.SetValue(obj, value, null);

                return;
            }
            if (pt.PropertyType == typeof(Int32))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Int64))
            {
                Int64 value = Convert.ToInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt16))
            {
                UInt16 value = Convert.ToUInt16(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt32))
            {
                UInt32 value = Convert.ToUInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt64))
            {
                UInt64 value = Convert.ToUInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(float))
            {
                float value = Convert.ToSingle(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(double))
            {
                double value = Convert.ToDouble(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(decimal))
            {
                decimal value = Convert.ToDecimal(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(DateTime))
            {
                DateTime value = Convert.ToDateTime(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Enum))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(string))
            {
                string value = Convert.ToString(rvalue);
                pt.SetValue(obj, rvalue, null);
                return;
            }
            pt.SetValue(obj, null, null);
        }

        private static void SetingValue(PropertyInfo pt, object obj, DateTime rvalue)
        {
            if (pt == null)
                return;
            if (pt.PropertyType == typeof(bool))
            {
                bool value = Convert.ToBoolean(rvalue);
                pt.SetValue(obj, false, null);

                return;
            }

            if (pt.PropertyType == typeof(Int16))
            {
                Int16 value = Convert.ToInt16(rvalue);
                pt.SetValue(obj, value, null);

                return;
            }
            if (pt.PropertyType == typeof(Int32))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Int64))
            {
                Int64 value = Convert.ToInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt16))
            {
                UInt16 value = Convert.ToUInt16(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt32))
            {
                UInt32 value = Convert.ToUInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(UInt64))
            {
                UInt64 value = Convert.ToUInt64(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(float))
            {
                float value = Convert.ToSingle(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(double))
            {
                double value = Convert.ToDouble(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(decimal))
            {
                decimal value = Convert.ToDecimal(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(DateTime))
            {
                DateTime value = Convert.ToDateTime(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(Enum))
            {
                Int32 value = Convert.ToInt32(rvalue);
                pt.SetValue(obj, value, null);
                return;
            }
            if (pt.PropertyType == typeof(string))
            {
                string value = Convert.ToString(rvalue);
                pt.SetValue(obj, rvalue, null);
                return;
            }
            pt.SetValue(obj, null, null);
        }


        //public static void SetValue(PropertyInfo pt, object obj, object strvalue)
        //{
        //    if (obj == null)
        //        return;
        //    if (strvalue != null)
        //    {
        //        if (pt.PropertyType == strvalue.GetType())
        //        {
        //            pt.SetValue(obj, strvalue, null);
        //            return;
        //        }

        //        if (strvalue.GetType() == typeof(string))
        //        {
        //            SetingValue(pt, obj, (string)strvalue);
        //            return;
        //        }
        //        if (strvalue.GetType() == typeof(decimal))
        //        {
        //            SetingValue(pt, obj, (decimal)strvalue);
        //            return;
        //        }
        //        if (strvalue.GetType() == typeof(double))
        //        {
        //            SetingValue(pt, obj, (double)strvalue);
        //            return;
        //        }
        //        if (strvalue.GetType() == typeof(float))
        //        {
        //            SetingValue(pt, obj, (float)strvalue);
        //            return;
        //        }
        //        if (strvalue.GetType() == typeof(bool))
        //        {
        //            SetingValue(pt, obj, (bool)strvalue);
        //            return;
        //        }
        //        if (strvalue.GetType() == typeof(byte))
        //        {
        //            SetingValue(pt, obj, (byte)strvalue);
        //            return;
        //        }
        //        if (strvalue.GetType() == typeof(sbyte))
        //        {
        //            SetingValue(pt, obj, (sbyte)strvalue);
        //            return;
        //        }
        //        if (strvalue.GetType() == typeof(Int16))
        //        {
        //            SetingValue(pt, obj, (Int16)strvalue);
        //            return;
        //        }
        //        if (strvalue.GetType() == typeof(Int32))
        //        {
        //            SetingValue(pt, obj, (Int32)strvalue);
        //            return;
        //        }
        //        if (strvalue.GetType() == typeof(Int64))
        //        {
        //            SetingValue(pt, obj, (Int64)strvalue);
        //            return;
        //        }
        //        if (strvalue.GetType() == typeof(UInt16))
        //        {
        //            SetingValue(pt, obj, (UInt16)strvalue);
        //            return;
        //        }
        //        if (strvalue.GetType() == typeof(UInt32))
        //        {
        //            SetingValue(pt, obj, (UInt32)strvalue);
        //            return;
        //        }
        //        if (strvalue.GetType() == typeof(UInt64))
        //        {
        //            SetingValue(pt, obj, (UInt64)strvalue);
        //            return;
        //        }
        //        if (strvalue.GetType() == typeof(DateTime))
        //        {
        //            SetingValue(pt, obj, (DateTime)strvalue);
        //            return;
        //        }
        //        pt.SetValue(obj, strvalue, null);
        //    }
        //    else
        //    {
        //        pt.SetValue(obj, null, null);
        //    }
        //}

        //private static void SetingValue(PropertyInfo pt, object obj, string strvalue)
        //{
        //    if (pt == null)
        //        return;
        //    if (pt.PropertyType == typeof(bool))
        //    {
        //        bool value = false;
        //        if (bool.TryParse(strvalue, out value))
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        else
        //        {

        //            pt.SetValue(obj, false, null);
        //        }
        //        return;
        //    }

        //    if (pt.PropertyType == typeof(Int16))
        //    {
        //        Int16 value;
        //        if (Int16.TryParse(strvalue, out value))
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        else
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int32))
        //    {
        //        Int32 value;
        //        if (Int32.TryParse(strvalue, out value))
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        else
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int64))
        //    {
        //        Int64 value;
        //        if (Int64.TryParse(strvalue, out value))
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        else
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt16))
        //    {
        //        UInt16 value;
        //        if (UInt16.TryParse(strvalue, out value))
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        else
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt32))
        //    {
        //        UInt32 value;
        //        if (UInt32.TryParse(strvalue, out value))
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        else
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt64))
        //    {
        //        UInt64 value;
        //        if (UInt64.TryParse(strvalue, out value))
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        else
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(float))
        //    {
        //        float value;
        //        if (float.TryParse(strvalue, out value))
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        else
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(double))
        //    {
        //        double value;
        //        if (double.TryParse(strvalue, out value))
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        else
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(decimal))
        //    {
        //        decimal value;
        //        if (decimal.TryParse(strvalue, out value))
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        else
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(DateTime))
        //    {
        //        DateTime value;
        //        if (DateTime.TryParse(strvalue, out value))
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        else
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Enum))
        //    {
        //        Int32 value;
        //        if (Int32.TryParse(strvalue, out value))
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        else
        //        {
        //            pt.SetValue(obj, value, null);
        //        }
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(string))
        //    {
        //        pt.SetValue(obj, strvalue, null);
        //    }
        //    else
        //    {
        //        pt.SetValue(obj, null, null);
        //    }
        //    pt.SetValue(obj, null, null);
        //}

        //private static void SetingValue(PropertyInfo pt, object obj, decimal rvalue)
        //{
        //    if (pt == null)
        //        return;
        //    if (pt.PropertyType == typeof(bool))
        //    {
        //        bool value = Convert.ToBoolean(rvalue);
        //        pt.SetValue(obj, false, null);

        //        return;
        //    }

        //    if (pt.PropertyType == typeof(Int16))
        //    {
        //        Int16 value = Convert.ToInt16(rvalue);
        //        pt.SetValue(obj, value, null);

        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int32))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int64))
        //    {
        //        Int64 value = Convert.ToInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt16))
        //    {
        //        UInt16 value = Convert.ToUInt16(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt32))
        //    {
        //        UInt32 value = Convert.ToUInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt64))
        //    {
        //        UInt64 value = Convert.ToUInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(float))
        //    {
        //        float value = Convert.ToSingle(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(double))
        //    {
        //        double value = Convert.ToDouble(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(decimal))
        //    {
        //        decimal value = Convert.ToDecimal(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(DateTime))
        //    {
        //        DateTime value = Convert.ToDateTime(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Enum))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(string))
        //    {
        //        string value = Convert.ToString(rvalue);
        //        pt.SetValue(obj, rvalue, null);
        //        return;
        //    }
        //    pt.SetValue(obj, null, null);
        //}

        //private static void SetingValue(PropertyInfo pt, object obj, double rvalue)
        //{
        //    if (pt == null)
        //        return;
        //    if (pt.PropertyType == typeof(bool))
        //    {
        //        bool value = Convert.ToBoolean(rvalue);
        //        pt.SetValue(obj, false, null);

        //        return;
        //    }

        //    if (pt.PropertyType == typeof(Int16))
        //    {
        //        Int16 value = Convert.ToInt16(rvalue);
        //        pt.SetValue(obj, value, null);

        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int32))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int64))
        //    {
        //        Int64 value = Convert.ToInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt16))
        //    {
        //        UInt16 value = Convert.ToUInt16(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt32))
        //    {
        //        UInt32 value = Convert.ToUInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt64))
        //    {
        //        UInt64 value = Convert.ToUInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(float))
        //    {
        //        float value = Convert.ToSingle(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(double))
        //    {
        //        double value = Convert.ToDouble(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(decimal))
        //    {
        //        decimal value = Convert.ToDecimal(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(DateTime))
        //    {
        //        DateTime value = Convert.ToDateTime(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Enum))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(string))
        //    {
        //        string value = Convert.ToString(rvalue);
        //        pt.SetValue(obj, rvalue, null);
        //        return;
        //    }
        //    pt.SetValue(obj, null, null);
        //}

        //private static void SetingValue(PropertyInfo pt, object obj, float rvalue)
        //{
        //    if (pt == null)
        //        return;
        //    if (pt.PropertyType == typeof(bool))
        //    {
        //        bool value = Convert.ToBoolean(rvalue);
        //        pt.SetValue(obj, false, null);

        //        return;
        //    }

        //    if (pt.PropertyType == typeof(Int16))
        //    {
        //        Int16 value = Convert.ToInt16(rvalue);
        //        pt.SetValue(obj, value, null);

        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int32))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int64))
        //    {
        //        Int64 value = Convert.ToInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt16))
        //    {
        //        UInt16 value = Convert.ToUInt16(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt32))
        //    {
        //        UInt32 value = Convert.ToUInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt64))
        //    {
        //        UInt64 value = Convert.ToUInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(float))
        //    {
        //        float value = Convert.ToSingle(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(double))
        //    {
        //        double value = Convert.ToDouble(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(decimal))
        //    {
        //        decimal value = Convert.ToDecimal(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(DateTime))
        //    {
        //        DateTime value = Convert.ToDateTime(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Enum))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(string))
        //    {
        //        string value = Convert.ToString(rvalue);
        //        pt.SetValue(obj, rvalue, null);
        //        return;
        //    }
        //    pt.SetValue(obj, null, null);
        //}

        //private static void SetingValue(PropertyInfo pt, object obj, UInt64 rvalue)
        //{
        //    if (pt == null)
        //        return;
        //    if (pt.PropertyType == typeof(bool))
        //    {
        //        bool value = Convert.ToBoolean(rvalue);
        //        pt.SetValue(obj, false, null);

        //        return;
        //    }

        //    if (pt.PropertyType == typeof(Int16))
        //    {
        //        Int16 value = Convert.ToInt16(rvalue);
        //        pt.SetValue(obj, value, null);

        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int32))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int64))
        //    {
        //        Int64 value = Convert.ToInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt16))
        //    {
        //        UInt16 value = Convert.ToUInt16(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt32))
        //    {
        //        UInt32 value = Convert.ToUInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt64))
        //    {
        //        UInt64 value = Convert.ToUInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(float))
        //    {
        //        float value = Convert.ToSingle(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(double))
        //    {
        //        double value = Convert.ToDouble(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(decimal))
        //    {
        //        decimal value = Convert.ToDecimal(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(DateTime))
        //    {
        //        DateTime value = Convert.ToDateTime(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Enum))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(string))
        //    {
        //        string value = Convert.ToString(rvalue);
        //        pt.SetValue(obj, rvalue, null);
        //        return;
        //    }
        //    pt.SetValue(obj, null, null);
        //}

        //private static void SetingValue(PropertyInfo pt, object obj, UInt32 rvalue)
        //{
        //    if (pt == null)
        //        return;
        //    if (pt.PropertyType == typeof(bool))
        //    {
        //        bool value = Convert.ToBoolean(rvalue);
        //        pt.SetValue(obj, false, null);

        //        return;
        //    }

        //    if (pt.PropertyType == typeof(Int16))
        //    {
        //        Int16 value = Convert.ToInt16(rvalue);
        //        pt.SetValue(obj, value, null);

        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int32))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int64))
        //    {
        //        Int64 value = Convert.ToInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt16))
        //    {
        //        UInt16 value = Convert.ToUInt16(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt32))
        //    {
        //        UInt32 value = Convert.ToUInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt64))
        //    {
        //        UInt64 value = Convert.ToUInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(float))
        //    {
        //        float value = Convert.ToSingle(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(double))
        //    {
        //        double value = Convert.ToDouble(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(decimal))
        //    {
        //        decimal value = Convert.ToDecimal(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(DateTime))
        //    {
        //        DateTime value = Convert.ToDateTime(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Enum))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(string))
        //    {
        //        string value = Convert.ToString(rvalue);
        //        pt.SetValue(obj, rvalue, null);
        //        return;
        //    }
        //    pt.SetValue(obj, null, null);
        //}

        //private static void SetingValue(PropertyInfo pt, object obj, UInt16 rvalue)
        //{
        //    if (pt == null)
        //        return;
        //    if (pt.PropertyType == typeof(bool))
        //    {
        //        bool value = Convert.ToBoolean(rvalue);
        //        pt.SetValue(obj, false, null);

        //        return;
        //    }

        //    if (pt.PropertyType == typeof(Int16))
        //    {
        //        Int16 value = Convert.ToInt16(rvalue);
        //        pt.SetValue(obj, value, null);

        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int32))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int64))
        //    {
        //        Int64 value = Convert.ToInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt16))
        //    {
        //        UInt16 value = Convert.ToUInt16(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt32))
        //    {
        //        UInt32 value = Convert.ToUInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt64))
        //    {
        //        UInt64 value = Convert.ToUInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(float))
        //    {
        //        float value = Convert.ToSingle(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(double))
        //    {
        //        double value = Convert.ToDouble(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(decimal))
        //    {
        //        decimal value = Convert.ToDecimal(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(DateTime))
        //    {
        //        DateTime value = Convert.ToDateTime(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Enum))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(string))
        //    {
        //        string value = Convert.ToString(rvalue);
        //        pt.SetValue(obj, rvalue, null);
        //        return;
        //    }
        //    pt.SetValue(obj, null, null);
        //}

        //private static void SetingValue(PropertyInfo pt, object obj, Int64 rvalue)
        //{
        //    if (pt == null)
        //        return;
        //    if (pt.PropertyType == typeof(bool))
        //    {
        //        bool value = Convert.ToBoolean(rvalue);
        //        pt.SetValue(obj, false, null);

        //        return;
        //    }

        //    if (pt.PropertyType == typeof(Int16))
        //    {
        //        Int16 value = Convert.ToInt16(rvalue);
        //        pt.SetValue(obj, value, null);

        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int32))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int64))
        //    {
        //        Int64 value = Convert.ToInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt16))
        //    {
        //        UInt16 value = Convert.ToUInt16(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt32))
        //    {
        //        UInt32 value = Convert.ToUInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt64))
        //    {
        //        UInt64 value = Convert.ToUInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(float))
        //    {
        //        float value = Convert.ToSingle(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(double))
        //    {
        //        double value = Convert.ToDouble(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(decimal))
        //    {
        //        decimal value = Convert.ToDecimal(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(DateTime))
        //    {
        //        DateTime value = Convert.ToDateTime(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Enum))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(string))
        //    {
        //        string value = Convert.ToString(rvalue);
        //        pt.SetValue(obj, rvalue, null);
        //        return;
        //    }
        //    pt.SetValue(obj, null, null);
        //}

        //private static void SetingValue(PropertyInfo pt, object obj, Int32 rvalue)
        //{
        //    if (pt == null)
        //        return;
        //    if (pt.PropertyType == typeof(bool))
        //    {
        //        bool value = Convert.ToBoolean(rvalue);
        //        pt.SetValue(obj, false, null);

        //        return;
        //    }

        //    if (pt.PropertyType == typeof(Int16))
        //    {
        //        Int16 value = Convert.ToInt16(rvalue);
        //        pt.SetValue(obj, value, null);

        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int32))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int64))
        //    {
        //        Int64 value = Convert.ToInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt16))
        //    {
        //        UInt16 value = Convert.ToUInt16(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt32))
        //    {
        //        UInt32 value = Convert.ToUInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt64))
        //    {
        //        UInt64 value = Convert.ToUInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(float))
        //    {
        //        float value = Convert.ToSingle(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(double))
        //    {
        //        double value = Convert.ToDouble(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(decimal))
        //    {
        //        decimal value = Convert.ToDecimal(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(DateTime))
        //    {
        //        DateTime value = Convert.ToDateTime(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Enum))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(string))
        //    {
        //        string value = Convert.ToString(rvalue);
        //        pt.SetValue(obj, rvalue, null);
        //        return;
        //    }
        //    pt.SetValue(obj, null, null);
        //}

        //private static void SetingValue(PropertyInfo pt, object obj, Int16 rvalue)
        //{
        //    if (pt == null)
        //        return;
        //    if (pt.PropertyType == typeof(bool))
        //    {
        //        bool value = Convert.ToBoolean(rvalue);
        //        pt.SetValue(obj, false, null);

        //        return;
        //    }

        //    if (pt.PropertyType == typeof(Int16))
        //    {
        //        Int16 value = Convert.ToInt16(rvalue);
        //        pt.SetValue(obj, value, null);

        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int32))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int64))
        //    {
        //        Int64 value = Convert.ToInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt16))
        //    {
        //        UInt16 value = Convert.ToUInt16(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt32))
        //    {
        //        UInt32 value = Convert.ToUInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt64))
        //    {
        //        UInt64 value = Convert.ToUInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(float))
        //    {
        //        float value = Convert.ToSingle(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(double))
        //    {
        //        double value = Convert.ToDouble(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(decimal))
        //    {
        //        decimal value = Convert.ToDecimal(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(DateTime))
        //    {
        //        DateTime value = Convert.ToDateTime(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Enum))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(string))
        //    {
        //        string value = Convert.ToString(rvalue);
        //        pt.SetValue(obj, rvalue, null);
        //        return;
        //    }
        //    pt.SetValue(obj, null, null);
        //}

        //private static void SetingValue(PropertyInfo pt, object obj, bool rvalue)
        //{
        //    if (pt == null)
        //        return;
        //    if (pt.PropertyType == typeof(bool))
        //    {
        //        bool value = Convert.ToBoolean(rvalue);
        //        pt.SetValue(obj, false, null);

        //        return;
        //    }

        //    if (pt.PropertyType == typeof(Int16))
        //    {
        //        Int16 value = Convert.ToInt16(rvalue);
        //        pt.SetValue(obj, value, null);

        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int32))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int64))
        //    {
        //        Int64 value = Convert.ToInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt16))
        //    {
        //        UInt16 value = Convert.ToUInt16(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt32))
        //    {
        //        UInt32 value = Convert.ToUInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt64))
        //    {
        //        UInt64 value = Convert.ToUInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(float))
        //    {
        //        float value = Convert.ToSingle(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(double))
        //    {
        //        double value = Convert.ToDouble(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(decimal))
        //    {
        //        decimal value = Convert.ToDecimal(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(DateTime))
        //    {
        //        DateTime value = Convert.ToDateTime(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Enum))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(string))
        //    {
        //        string value = Convert.ToString(rvalue);
        //        pt.SetValue(obj, rvalue, null);
        //        return;
        //    }
        //    pt.SetValue(obj, null, null);
        //}

        //private static void SetingValue(PropertyInfo pt, object obj, byte rvalue)
        //{
        //    if (pt == null)
        //        return;
        //    if (pt.PropertyType == typeof(bool))
        //    {
        //        bool value = Convert.ToBoolean(rvalue);
        //        pt.SetValue(obj, false, null);

        //        return;
        //    }

        //    if (pt.PropertyType == typeof(Int16))
        //    {
        //        Int16 value = Convert.ToInt16(rvalue);
        //        pt.SetValue(obj, value, null);

        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int32))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int64))
        //    {
        //        Int64 value = Convert.ToInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt16))
        //    {
        //        UInt16 value = Convert.ToUInt16(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt32))
        //    {
        //        UInt32 value = Convert.ToUInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt64))
        //    {
        //        UInt64 value = Convert.ToUInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(float))
        //    {
        //        float value = Convert.ToSingle(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(double))
        //    {
        //        double value = Convert.ToDouble(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(decimal))
        //    {
        //        decimal value = Convert.ToDecimal(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(DateTime))
        //    {
        //        DateTime value = Convert.ToDateTime(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Enum))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(string))
        //    {
        //        string value = Convert.ToString(rvalue);
        //        pt.SetValue(obj, rvalue, null);
        //        return;
        //    }
        //    pt.SetValue(obj, null, null);
        //}

        //private static void SetingValue(PropertyInfo pt, object obj, sbyte rvalue)
        //{
        //    if (pt == null)
        //        return;
        //    if (pt.PropertyType == typeof(bool))
        //    {
        //        bool value = Convert.ToBoolean(rvalue);
        //        pt.SetValue(obj, false, null);

        //        return;
        //    }

        //    if (pt.PropertyType == typeof(Int16))
        //    {
        //        Int16 value = Convert.ToInt16(rvalue);
        //        pt.SetValue(obj, value, null);

        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int32))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int64))
        //    {
        //        Int64 value = Convert.ToInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt16))
        //    {
        //        UInt16 value = Convert.ToUInt16(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt32))
        //    {
        //        UInt32 value = Convert.ToUInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt64))
        //    {
        //        UInt64 value = Convert.ToUInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(float))
        //    {
        //        float value = Convert.ToSingle(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(double))
        //    {
        //        double value = Convert.ToDouble(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(decimal))
        //    {
        //        decimal value = Convert.ToDecimal(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(DateTime))
        //    {
        //        DateTime value = Convert.ToDateTime(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Enum))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(string))
        //    {
        //        string value = Convert.ToString(rvalue);
        //        pt.SetValue(obj, rvalue, null);
        //        return;
        //    }
        //    pt.SetValue(obj, null, null);
        //}

        //private static void SetingValue(PropertyInfo pt, object obj, DateTime rvalue)
        //{
        //    if (pt == null)
        //        return;
        //    if (pt.PropertyType == typeof(bool))
        //    {
        //        bool value = Convert.ToBoolean(rvalue);
        //        pt.SetValue(obj, false, null);

        //        return;
        //    }

        //    if (pt.PropertyType == typeof(Int16))
        //    {
        //        Int16 value = Convert.ToInt16(rvalue);
        //        pt.SetValue(obj, value, null);

        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int32))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Int64))
        //    {
        //        Int64 value = Convert.ToInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt16))
        //    {
        //        UInt16 value = Convert.ToUInt16(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt32))
        //    {
        //        UInt32 value = Convert.ToUInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(UInt64))
        //    {
        //        UInt64 value = Convert.ToUInt64(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(float))
        //    {
        //        float value = Convert.ToSingle(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(double))
        //    {
        //        double value = Convert.ToDouble(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(decimal))
        //    {
        //        decimal value = Convert.ToDecimal(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(DateTime))
        //    {
        //        DateTime value = Convert.ToDateTime(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(Enum))
        //    {
        //        Int32 value = Convert.ToInt32(rvalue);
        //        pt.SetValue(obj, value, null);
        //        return;
        //    }
        //    if (pt.PropertyType == typeof(string))
        //    {
        //        string value = Convert.ToString(rvalue);
        //        pt.SetValue(obj, rvalue, null);
        //        return;
        //    }
        //    pt.SetValue(obj, null, null);
        //}
    }
}
