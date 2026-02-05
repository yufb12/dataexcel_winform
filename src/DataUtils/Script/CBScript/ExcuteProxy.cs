using Feng.Script.Method;
using System;
using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{
    public class ExcuteProxy : OperatorExecBase
    {
        public ExcuteProxy() : base()
        {

        }

        public object Current { get; set; }
        private IRunMethod runMethod = null;
        public IRunMethod RunMethod 
        {
            get {
                if (runMethod == null)
                {
                    runMethod = new CBMethod();
                }
                return runMethod;
            }
            set
            {
                runMethod = value;
            }
        }
        public object ME { get; set; }
 
        public const string Default_KeyWords_THIS = "this";
        public const string Default_KeyWords_Current = "Current";
        public const string Default_KeyWords_ME = "me";
        public const string Default_KeyWords_Yes = "是";
        public const string Default_KeyWords_No = "否";
        public override object GetKeyValue(string key)
        {
            key = key.ToLower();
            switch (key)
            {
                case Default_KeyWords_THIS:
                    return this.RunMethod;
                case Default_KeyWords_ME:
                    return this.ME;
                case Default_KeyWords_Yes:
                    return true;
                case Default_KeyWords_No:
                    return false;
                case Default_KeyWords_Current:
                    return Current;
                default:
                    if (this.KeyValues.ContainsKey(key))
                    {
                        return this.KeyValues[key];
                    }
                    break;
            }
            throw new SyntacticException(key + " 未定义");
        }
        public override object Combine(object value1, object value2)
        {
            return Feng.Utils.ConvertHelper.ToString(value1) + Feng.Utils.ConvertHelper.ToString(value2);
        }
        public override object Add(object value1, object value2)
        {
            object value = null;
            object v1 = value1;
            object v2 = value2;
            value = Feng.Utils.MathHelper.Add(v1, v2);

            return value;
        }
        public override object LogicalAND(object value1, object value2)
        {
            bool value = false;
            bool bvalue1 = Feng.Utils.ConvertHelper.ToBoolean(value1, true);
            bool bvalue2 = Feng.Utils.ConvertHelper.ToBoolean(value2, true);
            value = bvalue1 && bvalue2;
            return value;
        }
        public override object Range(object value1, object value2)
        {
            object value = null; 
            return value;
        }
        public override object Divide(object value1, object value2)
        {
            object value = null;
            value = Feng.Utils.MathHelper.Divide(value1, value2);
            return value;
        }
        public override object Equal(object value1, object value2)
        {
            object value = null;

            value = value1.ToString() == value2.ToString();
            return value;
        }
        public override object Function(object value1, List<object> values)
        {
            object value = null;
            List<object> list = values;
            if (list == null)
            {
                list = new List<object>();
            }
            list.Insert(0, this);
            string method = Feng.Utils.ConvertHelper.ToString(value1);
            value = this.RunMethod.RunMethod(ME, method, list.ToArray());
            return value;
        }
        public override object Index(object value1, object value2)
        {
            object value = null;
            if (value1 != null && value2 != null)
            {
                Type type = value1.GetType();
                if (type.IsArray)
                {
                    Array arr = (Array)value1;
                    int index = Feng.Utils.ConvertHelper.ToInt32(value2, -1);
                    if (index >= 0)
                    {
                        return arr.GetValue(index);
                    }
                } 
                else
                {
                    System.Reflection.PropertyInfo[] pis = type.GetProperties();
                    if (pis != null)
                    {
                        string p = value2.ToString().ToLower();
                        foreach (System.Reflection.PropertyInfo pi in pis)
                        {
                            if (pi.Name.ToLower() == p)
                            {
                                return pi.GetValue(value1, null);
                            }
                        }
                    }
                }
            }
            return value;
        }
        public override object LessThan(object value1, object value2)
        {
            bool value = false;
            decimal dvalue1 = Feng.Utils.ConvertHelper.ToDecimal(value1, true);
            decimal dvalue2 = Feng.Utils.ConvertHelper.ToDecimal(value2, true);
            value = dvalue1 < dvalue2;
            return value;
        }
        public override object LessThanEqual(object value1, object value2)
        {
            bool value = false;
            decimal dvalue1 = Feng.Utils.ConvertHelper.ToDecimal(value1, true);
            decimal dvalue2 = Feng.Utils.ConvertHelper.ToDecimal(value2, true);
            value = dvalue1 <= dvalue2;
            return value;
        }
        public override object Minus(object value1, object value2)
        {
            object value = null;
            value = Feng.Utils.MathHelper.Minus(value1, value2);
            return value;
        }
        public override object Mod(object value1, object value2)
        {
            object value = null;
            int ivalue1 = Feng.Utils.ConvertHelper.ToInt32(value1);
            int ivalue2 = Feng.Utils.ConvertHelper.ToInt32(value2);
            value = ivalue1 % ivalue2;
            return value;
        }
        public override object MoreThan(object value1, object value2)
        {
            bool value = false;
            decimal dvalue1 = Feng.Utils.ConvertHelper.ToDecimal(value1, true);
            decimal dvalue2 = Feng.Utils.ConvertHelper.ToDecimal(value2, true);
            value = dvalue1 > dvalue2;
            return value;
        }
        public override object MoreThanEqual(object value1, object value2)
        {
            bool value = false;
            decimal dvalue1 = Feng.Utils.ConvertHelper.ToDecimal(value1, true);
            decimal dvalue2 = Feng.Utils.ConvertHelper.ToDecimal(value2, true);
            value = dvalue1 >= dvalue2;
            return value;
        }
        public override object Multiply(object value1, object value2)
        {
            object value = null;
            value = Feng.Utils.MathHelper.Multiply(value1, value2);
            return value;
        }
        public override object Negative(object value1)
        {
            object value = null;
            if (value1 is byte)
            {
                return (byte)value1 * -1;
            }
            else if (value1 is sbyte)
            {
                return (sbyte)value1 * -1;
            }
            else if (value1 is short)
            {
                return (short)value1 * -1;
            }
            else if (value1 is ushort)
            {
                return (ushort)value1 * -1;
            }
            else if (value1 is int)
            {
                return (int)value1 * -1;
            }
            else if (value1 is uint)
            {
                return (uint)value1 * -1;
            }
            else if (value1 is long)
            {
                return (long)value1 * -1;
            }
            else if (value1 is ulong)
            {
                return (long)value1 * -1;
            }
            else if (value1 is int)
            {
                return (int)value1 * -1;
            }
            else if (value1 is double)
            {
                return (double)value1 * -1;
            }
            else if (value1 is decimal)
            {
                return (decimal)value1 * -1;
            }
            return value;
        }
        public override object Not(object value1)
        {
            bool value = false;
            bool bvalue1 = Feng.Utils.ConvertHelper.ToBoolean(value1, true);
            value = !bvalue1;
            return value;
        }
        public override object NotEqual(object value1, object value2)
        {
            bool value = false;
            value = value1.ToString() != value2.ToString();
            return value;
        }
        public override object LogicalOR(object value1, object value2)
        {
            bool value = false;
            bool bvalue1 = Feng.Utils.ConvertHelper.ToBoolean(value1, true);
            bool bvalue2 = Feng.Utils.ConvertHelper.ToBoolean(value2, true);
            value = bvalue1 || bvalue2;
            return value;
        }
        public override object Parenthesis(object value1)
        {
            return value1;
        }
        public override object Property(object value1, object value2)
        {
            object value = null;
            if (value1 != null && value2 != null)
            {
                Type type = value1.GetType();
                if (type != null)
                {
                    System.Reflection.PropertyInfo[] pis = type.GetProperties();
                    if (pis != null)
                    {
                        string p = value2.ToString().ToLower();
                        foreach (System.Reflection.PropertyInfo pi in pis)
                        {
                            if (pi.Name.ToLower() == p)
                            {
                                return pi.GetValue(value1, null);
                            }
                        }
                    }
                }
            }
            return value;
        }
        public override object PropertyFunction(object value1, object value2, List<object> values)
        {
            object value = null;
            if (value1 != null && value2 != null)
            {
                Type type = value1.GetType();
                if (type != null)
                {
                    System.Reflection.MethodInfo[] pis = type.GetMethods();
                    if (pis != null)
                    {
                        string p = value2.ToString().ToLower();
                        foreach (System.Reflection.MethodInfo pi in pis)
                        {
                            System.Reflection.ParameterInfo[] pifs = pi.GetParameters();
                            if (pi.Name.ToLower() == p)
                            {
                                return pi.Invoke(value1, values.ToArray());
                            }
                        }
                    }
                }
            }
            return value;
        }
        public override object BitwiseShiftLeft(object value1, object value2)
        {
            int res1 = Feng.Utils.ConvertHelper.ToInt32(value1);
            int res2 = Feng.Utils.ConvertHelper.ToInt32(value2);
            return res1 << res2;
        }
        public override object BitwiseShiftRight(object value1, object value2)
        {
            int res1 = Feng.Utils.ConvertHelper.ToInt32(value1);
            int res2 = Feng.Utils.ConvertHelper.ToInt32(value2);
            return res1 >> res2;
        }
        public override object BitwiseOR(object value1, object value2)
        {
            int res1 = Feng.Utils.ConvertHelper.ToInt32(value1);
            int res2 = Feng.Utils.ConvertHelper.ToInt32(value2);
            return res1 | res2;
        }
        public override object BitwiseAnd(object value1, object value2)
        {
            int res1 = Feng.Utils.ConvertHelper.ToInt32(value1);
            int res2 = Feng.Utils.ConvertHelper.ToInt32(value2);
            return res1 & res2;
        }
        public override object BitwiseXOR(object value1, object value2)
        {
            int res1 = Feng.Utils.ConvertHelper.ToInt32(value1);
            int res2 = Feng.Utils.ConvertHelper.ToInt32(value2);
            return res1 ^ res2;
        }
        public override object BitwiseNOT(object value1)
        {
            int res1 = Feng.Utils.ConvertHelper.ToInt32(value1);
            return ~res1;
        }
        public override object ToNumber(object value1)
        {
            decimal res1 = Feng.Utils.ConvertHelper.ToDecimal(value1);
            return res1;
        }
        public override object Increment(object value1)
        {
            int res1 = Feng.Utils.ConvertHelper.ToInt32(value1);
            res1++;
            return res1;
        }
        public override object Decrement(object value1)
        {
            int res1 = Feng.Utils.ConvertHelper.ToInt32(value1);
            res1--;
            return res1;
        }
        public override object ToString(object value1)
        {
            string res1 = Feng.Utils.ConvertHelper.ToString(value1);
            return res1;
        }
    }
}
