using Feng.Script.CBEexpress;
using Feng.Script.Method;
using Feng.Utils;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Feng.Script.FunctionContainer
{
    [Serializable]
    public class MathematicsFunctionContainer : CBMethodContainer
    {
        public const string Function_Category = "MathematicsFunction";
        public const string Function_Description = "数学函数";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }
        public MathematicsFunctionContainer()
        {
            BaseMethod model = new BaseMethod();
            model.Name = "Sum";
            model.Description = "合计";
            model.Function = Sum;
            model.Eg = "Sum";
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "AVG";
            model.Description = "平均";
            model.Function = AVG;
            model.Eg = "AVG";
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "Count";
            model.Description = "计算";
            model.Function = Count;
            model.Eg = "Count";
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Mul";
            model.Description = "乘";
            model.Function = Mul;
            model.Eg = "Mul";
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Ceiling";
            model.Description = "返回大于或等于指定的双精度浮点数的最小整数值";
            model.Function = Ceiling;
            model.Eg = "Ceiling";
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "DivRem";
            model.Description = "计算有符号整数的商";
            model.Function = DivRem;
            model.Eg = "DivRem";
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DivRemResult";
            model.Description = "计算有符号整数的商返回余数";
            model.Function = DivRemResult;
            model.Eg = "DivRemResult";
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "Exp";
            model.Description = "返回 e 的指定次幂";
            model.Function = Exp;
            model.Eg = "Exp";
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "Floor";
            model.Description = "返回小于或等于指定小数的最大整数";
            model.Function = Floor;
            model.Eg = "Floor";
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "IEEERemainder";
            model.Description = "返回一指定数字被另一指定数字相除的余数";
            model.Function = IEEERemainder;
            model.Eg = "IEEERemainder";
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Log";
            model.Description = "返回指定数字的自然对数（底为 e）";
            model.Function = Log;
            model.Eg = "Log";
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "Log10";
            model.Description = "返回指定数字以 10 为底的对数";
            model.Function = Log10;
            model.Eg = "Log10";
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "Max";
            model.Description = "返回最大值";
            model.Function = Max;
            model.Eg = "Max";
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "Min";
            model.Description = "返回最小值";
            model.Function = Min;
            model.Eg = "Min";
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Pow";
            model.Description = "返回指定数字的指定次幂";
            model.Function = Pow;
            model.Eg = "Pow";
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "Round";
            model.Description = "将小数值舍入到最接近的整数值";
            model.Function = Round;
            model.Eg = "Round";
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Sign";
            model.Description = "返回表示数字符号的值";
            model.Function = Sign;
            model.Eg = "Sign";
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Sqrt";
            model.Description = "返回指定数字的平方根";
            model.Function = Sqrt;
            model.Eg = "Sqrt";
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Truncate";
            model.Description = "计算指定双精度浮点数的整数部分";
            model.Function = Truncate;
            model.Eg = "Truncate";
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Random";
            model.Description = @"产生随机数 Random(10,100)";
            model.Eg = @"Random(10,100)";
            model.Function = Random;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "BitwiseAnd";
            model.Description = @"按位与 BitwiseAnd(1234,8)";
            model.Eg = @"BitwiseAnd(1234,8)";
            model.Function = BitwiseAnd;
            MethodList.Add(model);
        }

        public object Abs(params object[] args)
        {
            object value = base.GetArgIndex(1, args);

            object result = null;
            Type t = value.GetType();
            switch (t.FullName)
            {
                case "System.Int32":
                    result = Math.Abs(ConvertHelper.ToInt32(value));
                    break;
                case "System.Int64":
                    result = Math.Abs(ConvertHelper.ToInt32(value));
                    break;
                case "System.Int16":
                    result = Math.Abs(ConvertHelper.ToInt16(value));
                    break;
                case "System.Single":
                    result = Math.Abs(ConvertHelper.ToSingle(value));
                    break;
                case "System.Decimal":
                    result = Math.Abs(ConvertHelper.ToDecimal(value));
                    break;
                case "System.Double":
                    result = Math.Abs(ConvertHelper.ToDouble(value));
                    break;
                case "System.SByte":
                    result = Math.Abs(ConvertHelper.ToSByte(value));
                    break;
                case "System.String":
                    result = Math.Abs(Convert.ToDouble(value));
                    break;
                default:
                    break;
            }
            return result;
        }
        public object AVG(params object[] args)
        {
            double result = 0;
            object obj = base.GetArgIndex(1, args);
            bool hasnull = base.GetBooleanValue(2, false, args);
            int count = 0;
            IEnumerable enumerator = obj as IEnumerable;
            if (enumerator != null)
            {
                foreach (var item in enumerator)
                {
                    if (item == null)
                    {
                        if (!hasnull)
                        {
                            continue;
                        }
                    }
                    double d1 = Feng.Utils.ConvertHelper.ToDouble(item);
                    result = result + d1;
                    count++;
                }
            }
            else
            {
                for (int i = 1; i < args.Length; i++)
                {
                    double d1 = base.GetDoubleValue(i, args);
                    result = result + d1;
                    count++;
                }
            }
            if (count < 1)
                return 0;
            return result / count;
        }
        public object Sum(params object[] args)
        {
            decimal result = 0;
            object obj = base.GetArgIndex(1, args);
            IEnumerable enumerator = obj as IEnumerable;
            if (enumerator != null)
            {
                foreach (var item in enumerator)
                {
                    decimal d1 = Feng.Utils.ConvertHelper.ToDecimal(item);
                    result = result + d1;
                }
            }
            else
            {
                for (int i = 1; i < args.Length; i++)
                {
                    decimal d1 = base.GetDecimalValue(i, args);
                    result = result + d1;
                }
            }
            return result;
        }
        public object Count(params object[] args)
        {
            object obj = base.GetArgIndex(1, args);
            bool hasnull = base.GetBooleanValue(2, false, args);
            int count = 0;
            IEnumerable enumerator = obj as IEnumerable;
            if (enumerator != null)
            {
                foreach (var item in enumerator)
                {
                    if (item == null)
                    {
                        if (!hasnull)
                        {
                            continue;
                        }
                    }
                    count++;
                }
            }
            else
            {
                for (int i = 1; i < args.Length; i++)
                {
                    count++;
                }
            }
            return count;
        }
        public object Mul(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            object value2 = base.GetArgIndex(2, args);
            double result = ConvertHelper.ToDouble(value1) * ConvertHelper.ToDouble(value2);
            return result;
        }
        public object Ceiling(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            return Math.Ceiling(ConvertHelper.ToDouble(value));
        }
        public object DivRem(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            object value2 = base.GetArgIndex(2, args);
            long i = 0;
            return Math.DivRem(ConvertHelper.ToInt64(value1), ConvertHelper.ToInt64(value2), out i);
        }
        public object DivRemResult(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            object value2 = base.GetArgIndex(2, args);
            long i = 0;
            Math.DivRem(ConvertHelper.ToInt64(value1), ConvertHelper.ToInt64(value2), out i);
            return i;
        }
        public object Exp(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            return Math.Exp(ConvertHelper.ToDouble(value));
        }
        public object Floor(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            return Math.Floor(ConvertHelper.ToDouble(value));
        }
        public object IEEERemainder(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            object value2 = base.GetArgIndex(2, args);
            return Math.IEEERemainder(ConvertHelper.ToDouble(value1), ConvertHelper.ToDouble(value2));
        }
        public object Log(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            return Math.Log(ConvertHelper.ToDouble(value));
        }
        public object Log10(params object[] args)
        {
            object value = base.GetArgIndex(1, args);
            return Math.Log10(ConvertHelper.ToDouble(value));
        }
        public object Max(params object[] args)
        {
            decimal result = 0;
            bool first = true;
            object obj = base.GetArgIndex(1, args);
            IEnumerable enumerator = obj as IEnumerable;
            if (enumerator != null)
            {
                foreach (var item in enumerator)
                {
                    if (item == null)
                        continue;
                    decimal d1 = Feng.Utils.ConvertHelper.ToDecimal(item);
                    if (first)
                    {
                        result = d1;
                        first = false;
                        continue;
                    }
                    result = Feng.Utils.ConvertHelper.Max(d1, result);
                }
            }
            else
            {
                for (int i = 1; i < args.Length; i++)
                {
                    decimal d1 = base.GetDecimalValue(i, args);
                    if (first)
                    {
                        result = d1;
                        first = false;
                        continue;
                    }
                    result = Feng.Utils.ConvertHelper.Max(d1, result);
                }
            }
            return result;
        }
        public object Min(params object[] args)
        {
            decimal result = 0;
            bool first = true;
            object obj = base.GetArgIndex(1, args);
            IEnumerable enumerator = obj as IEnumerable;
            if (enumerator != null)
            {
                foreach (var item in enumerator)
                {
                    if (item == null)
                        continue;
                    decimal d1 = Feng.Utils.ConvertHelper.ToDecimal(item);
                    if (first)
                    {
                        result = d1;
                        first = false;
                        continue;
                    }
                    result = Feng.Utils.ConvertHelper.Min(d1, result);
                }
            }
            else
            {
                for (int i = 1; i < args.Length; i++)
                {
                    decimal d1 = base.GetDecimalValue(i, args);
                    if (first)
                    {
                        result = d1;
                        first = false;
                        continue;
                    }
                    result = Feng.Utils.ConvertHelper.Min(d1, result);
                }
            }
            return result;
        }
        public object Pow(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            object value2 = base.GetArgIndex(2, args);
            return Math.Pow(ConvertHelper.ToDouble(value1), ConvertHelper.ToDouble(value2));
        }
        public object Round(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            object value2 = base.GetArgIndex(2, args);
            return Math.Round(ConvertHelper.ToDouble(value1), ConvertHelper.ToInt32(value2));
        }
        public object Sign(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            return Math.Sign(ConvertHelper.ToDouble(value1));
        }
        public object Sqrt(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            return Math.Sqrt(ConvertHelper.ToDouble(value1));
        }
        public object Truncate(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            return Math.Truncate(ConvertHelper.ToDouble(value1));
        }
        public object Random(params object[] args)
        {
            int result = 0;
            int Begin = base.GetIntValue(1, args);
            int end = base.GetIntValue(2, args);
            if (Begin + end < 1)
            {
                result = Feng.Utils.RandomCache.Next(0, 100000);
            }
            else
            {
                result = Feng.Utils.RandomCache.Next(Begin, end);
            }

            return result;
        }     
        
        public object BitwiseAnd(params object[] args)
        {
            object value1 = base.GetArgIndex(1, args);
            object value2 = base.GetArgIndex(2, args); 

            int res1 = Feng.Utils.ConvertHelper.ToInt32(value1);
            int res2 = Feng.Utils.ConvertHelper.ToInt32(value2);
            return res1 & res2;
        }
    }
}
