//using Feng.Excel.Base;
//using Feng.Excel.Collections;
//using Feng.Excel.Interfaces;
//using Feng.Script.CBEexpress;
//using System;
//using System.Collections.Generic;

//namespace Feng.Excel.Script
//{
//    public class DataExcelScriptStmtProxy : NetOperatorProxyBase
//    {
//        public DataExcelScriptStmtProxy() : base()
//        {

//        }
//        public DataExcel Grid { get; set; }
//        public ICell CurrentCell { get; set; }
//        public FunctionBody FunctionBody { get; set; }

//        public Object CArg1 { get; set; }
//        public Object CArg2 { get; set; }
//        public Object CArg3 { get; set; }
//        public Object CArg4 { get; set; }
//        public Object CArg5 { get; set; }
//        public Object CArg6 { get; set; }
//        public Object CArg7 { get; set; }
//        public object GetCellValue(object value)
//        {
//            if (value is ICell)
//            {
//                return (value as ICell).Value;
//            }
//            return value;
//        }

//        public const string Default_KeyWords_THIS = "this"; 
//        public const string Default_KeyWords_ME = "me";

//        public const string Default_KeyWords_CArg1 = "CArg1";
//        public const string Default_KeyWords_CArg2 = "CArg2";
//        public const string Default_KeyWords_CArg3 = "CArg3";
//        public const string Default_KeyWords_CArg4 = "CArg4";
//        public const string Default_KeyWords_CArg5 = "CArg5";
//        public const string Default_KeyWords_CArg6 = "CArg6";
//        public const string Default_KeyWords_CArg7 = "CArg7";

//        public const string Default_ARG_NUMBBER1 = "NUM1_0";
//        public const string Default_ARG_NUMBBER2 = "NUM2_0";
//        public const string Default_ARG_NUMBBER3 = "NUM3_0";
//        public const string Default_ARG_NUMBBER4 = "NUM4_0";
//        public const string Default_ARG_NUMBBER5 = "NUM5_0";
//        public const string Default_ARG_NUMBBER6 = "NUM6_0";
//        public const string Default_ARG_NUMBBER7 = "NUM7_0";
//        public const string Default_ARG_NUMBBER8 = "NUM8_0";
//        public const string Default_ARG_NUMBBER9 = "NUM9_0";
//        public const string Default_ARG_NUMBBER10 = "NUM10_0";
//        public const string Default_ARG_NUMBBER11 = "NUM11_0";
//        public const string Default_ARG_NUMBBER12 = "NUM12_0";
//        public const string Default_ARG_NUMBBER13 = "NUM13_0";
//        public const string Default_ARG_NUMBBER14 = "NUM14_0";
//        public const string Default_ARG_NUMBBER15 = "NUM15_0";
//        public const string Default_ARG_NUMBBER16 = "NUM16_0";
//        public const string Default_ARG_NUMBBER17 = "NUM17_0";
//        public const string Default_ARG_NUMBBER18 = "NUM18_0";
//        public const string Default_ARG_NUMBBER19 = "NUM19_0";
        
//        public const string Default_ARG_EMPTYSTRING1 = "EMPTYSTRING1";
//        public const string Default_ARG_EMPTYSTRING2 = "EMPTYSTRING2";
//        public const string Default_ARG_EMPTYSTRING3 = "EMPTYSTRING3";
//        public const string Default_ARG_EMPTYSTRING4 = "EMPTYSTRING4";
//        public const string Default_ARG_EMPTYSTRING5 = "EMPTYSTRING5";
//        public const string Default_ARG_EMPTYSTRING6 = "EMPTYSTRING6";
//        public const string Default_ARG_EMPTYSTRING7 = "EMPTYSTRING7";
//        public const string Default_ARG_EMPTYSTRING8 = "EMPTYSTRING8";
//        public const string Default_ARG_EMPTYSTRING9 = "EMPTYSTRING9";
//        public const string Default_ARG_EMPTYSTRING10 = "EMPTYSTRING10";
//        public const string Default_ARG_EMPTYSTRING11 = "EMPTYSTRING11";
//        public const string Default_ARG_EMPTYSTRING12 = "EMPTYSTRING12";
//        public const string Default_ARG_EMPTYSTRING13 = "EMPTYSTRING13";
//        public const string Default_ARG_EMPTYSTRING14 = "EMPTYSTRING14";
//        public const string Default_ARG_EMPTYSTRING15 = "EMPTYSTRING15";
//        public const string Default_ARG_EMPTYSTRING16 = "EMPTYSTRING16";
//        public const string Default_ARG_EMPTYSTRING17 = "EMPTYSTRING17";
//        public const string Default_ARG_EMPTYSTRING18 = "EMPTYSTRING18";
//        public const string Default_ARG_EMPTYSTRING19 = "EMPTYSTRING19";
//        //public override object GetKeyValue(string key)
//        //{
//        //    key = key.ToLower();
//        //    switch (key)
//        //    {
//        //        case Default_KeyWords_THIS:
//        //            return this.Grid;
//        //        case Default_KeyWords_ME:
//        //            return this.CurrentCell;
//        //        case Default_KeyWords_CArg1:
//        //            return this.CArg1;
//        //        case Default_KeyWords_CArg2:
//        //            return this.CArg2;
//        //        case Default_KeyWords_CArg3:
//        //            return this.CArg3;
//        //        case Default_KeyWords_CArg4:
//        //            return this.CArg4;
//        //        case Default_KeyWords_CArg5:
//        //            return this.CArg5;
//        //        case Default_KeyWords_CArg6:
//        //            return this.CArg6;
//        //        case Default_KeyWords_CArg7:
//        //            return this.CArg7; 
//        //        case Default_ARG_NUMBBER1:
//        //        case Default_ARG_NUMBBER2:
//        //        case Default_ARG_NUMBBER3:
//        //        case Default_ARG_NUMBBER4:
//        //        case Default_ARG_NUMBBER5:
//        //        case Default_ARG_NUMBBER6:
//        //        case Default_ARG_NUMBBER7:
//        //        case Default_ARG_NUMBBER8:
//        //        case Default_ARG_NUMBBER9:
//        //        case Default_ARG_NUMBBER10:
//        //        case Default_ARG_NUMBBER11:
//        //        case Default_ARG_NUMBBER12:
//        //        case Default_ARG_NUMBBER13:
//        //        case Default_ARG_NUMBBER14:
//        //        case Default_ARG_NUMBBER15:
//        //        case Default_ARG_NUMBBER16:
//        //        case Default_ARG_NUMBBER17:
//        //        case Default_ARG_NUMBBER18:
//        //        case Default_ARG_NUMBBER19:
//        //            return 0;
//        //        case Default_ARG_EMPTYSTRING1:
//        //        case Default_ARG_EMPTYSTRING2:
//        //        case Default_ARG_EMPTYSTRING3:
//        //        case Default_ARG_EMPTYSTRING4:
//        //        case Default_ARG_EMPTYSTRING5:
//        //        case Default_ARG_EMPTYSTRING6:
//        //        case Default_ARG_EMPTYSTRING7:
//        //        case Default_ARG_EMPTYSTRING8:
//        //        case Default_ARG_EMPTYSTRING9:
//        //        case Default_ARG_EMPTYSTRING10:
//        //        case Default_ARG_EMPTYSTRING11:
//        //        case Default_ARG_EMPTYSTRING12:
//        //        case Default_ARG_EMPTYSTRING13:
//        //        case Default_ARG_EMPTYSTRING14:
//        //        case Default_ARG_EMPTYSTRING15:
//        //        case Default_ARG_EMPTYSTRING16:
//        //        case Default_ARG_EMPTYSTRING17:
//        //        case Default_ARG_EMPTYSTRING18:
//        //        case Default_ARG_EMPTYSTRING19:
//        //            return string.Empty;
//        //        default:
//        //            if (this.KeyValues.ContainsKey(key))
//        //            {
//        //                return this.KeyValues[key];
//        //            }
//        //            break;
//        //    }
//        //    return null;
//        //}
//        public override object Combine(object value1, object value2)
//        {
//            return Feng.Utils.ConvertHelper.ToString(GetCellValue(value1)) + Feng.Utils.ConvertHelper.ToString(GetCellValue(value2));
//        }
//        public override object Add(object value1, object value2)
//        {
//            object value = null;
//            object v1 = GetCellValue(value1);
//            object v2 = GetCellValue(value2);
//            value = Feng.Utils.MathHelper.Add(v1, v2);

//            return value;
//        }
//        public override object LogicalAND(object value1, object value2)
//        {
//            bool value = false;
//            bool bvalue1 = Feng.Utils.ConvertHelper.ToBoolean(GetCellValue(value1), true);
//            bool bvalue2 = Feng.Utils.ConvertHelper.ToBoolean(GetCellValue(value2), true);
//            value = bvalue1 && bvalue2;
//            return value;
//        }
//        public override object Range(object value1, object value2)
//        {
//            object value = null;
//            ICell begincell = value1 as ICell;
//            ICell endcell = value2 as ICell;
//            if (begincell != null && endcell != null)
//            {
//                SelectCellCollection scl = new SelectCellCollection();
//                scl.BeginCell = begincell;
//                scl.EndCell = endcell;
//                return scl.GetAllCells();
//            }
//            return value;
//        }
//        public override object Divide(object value1, object value2)
//        {
//            object value = null;
//            value = Feng.Utils.MathHelper.Divide(GetCellValue(value1), GetCellValue(value2));
//            return value;
//        }
//        public override object Equal(object value1, object value2)
//        { 
//            bool res = Feng.Utils.ConvertHelper.Equals(value1, value2);
//            return res; 
//        }
//        //public override object Function(object value1, List<object> values)
//        //{
//        //    object value = null;
//        //    List<object> list = values;
//        //    if (list == null)
//        //    {
//        //        list = new List<object>();
//        //    }
//        //    list.Insert(0, this);
//        //    string method = Feng.Utils.ConvertHelper.ToString(value1);
//        //    value = this.Grid.RunMethod(this.CurrentCell, method, list.ToArray());
//        //    return value;
//        //}
//        public override object Index(object value1, object value2)
//        {
//            object value = null;
//            if (value1 != null && value2 != null)
//            {
//                Type type = value1.GetType();
//                if (type.IsArray)
//                {
//                    Array arr = (Array)value1;
//                    int index = Feng.Utils.ConvertHelper.ToInt32(value2, -1);
//                    if (index >= 0)
//                    {
//                        return arr.GetValue(index);
//                    }
//                }
//                else if (type == typeof(DataExcel))
//                {
//                    DataExcel grid = value1 as DataExcel;
//                    if (grid != null)
//                    {
//                        return grid[value2.ToString()];
//                    }
//                }
//                else if (type == typeof(Row))
//                {
//                    Row grid = value1 as Row;
//                    if (grid != null)
//                    {
//                        int index = Feng.Utils.ConvertHelper.ToInt32(value2, -1);
//                        if (index >= 0)
//                        {
//                            return grid[index];
//                        }
//                    }
//                }
//                else
//                {
//                    System.Reflection.PropertyInfo[] pis = type.GetProperties();
//                    if (pis != null)
//                    {
//                        string p = value2.ToString().ToLower();
//                        foreach (System.Reflection.PropertyInfo pi in pis)
//                        {
//                            if (pi.Name.ToLower() == p)
//                            {
//                                return pi.GetValue(value1, null);
//                            }
//                        }
//                    }
//                }
//            }
//            return value;
//        }
//        public override object LessThan(object value1, object value2)
//        {
//            bool value = false;
//            decimal dvalue1 = Feng.Utils.ConvertHelper.ToDecimal(GetCellValue(value1), true);
//            decimal dvalue2 = Feng.Utils.ConvertHelper.ToDecimal(GetCellValue(value2), true);
//            value = dvalue1 < dvalue2;
//            return value;
//        }
//        public override object LessThanEqual(object value1, object value2)
//        {
//            bool value = false;
//            decimal dvalue1 = Feng.Utils.ConvertHelper.ToDecimal(GetCellValue(value1), true);
//            decimal dvalue2 = Feng.Utils.ConvertHelper.ToDecimal(GetCellValue(value2), true);
//            value = dvalue1 <= dvalue2;
//            return value;
//        }
//        public override object Minus(object value1, object value2)
//        {
//            object value = null;
//            value = Feng.Utils.MathHelper.Minus(GetCellValue(value1), GetCellValue(value2));
//            return value;
//        }
//        public override object Mod(object value1, object value2)
//        {
//            object value = null;
//            int ivalue1 = Feng.Utils.ConvertHelper.ToInt32(GetCellValue(value1));
//            int ivalue2 = Feng.Utils.ConvertHelper.ToInt32(GetCellValue(value2));
//            value = ivalue1 % ivalue2;
//            return value;
//        }
//        public override object MoreThan(object value1, object value2)
//        {
//            bool value = false;
//            decimal dvalue1 = Feng.Utils.ConvertHelper.ToDecimal(GetCellValue(value1), true);
//            decimal dvalue2 = Feng.Utils.ConvertHelper.ToDecimal(GetCellValue(value2), true);
//            value = dvalue1 > dvalue2;
//            return value;
//        }
//        public override object MoreThanEqual(object value1, object value2)
//        {
//            bool value = false;
//            decimal dvalue1 = Feng.Utils.ConvertHelper.ToDecimal(GetCellValue(value1), true);
//            decimal dvalue2 = Feng.Utils.ConvertHelper.ToDecimal(GetCellValue(value2), true);
//            value = dvalue1 >= dvalue2;
//            return value;
//        }
//        public override object Multiply(object value1, object value2)
//        {
//            object value = null;
//            value = Feng.Utils.MathHelper.Multiply(GetCellValue(value1), GetCellValue(value2));
//            return value;
//        }
//        public override object Negative(object value1)
//        {
//            object value = null;
//            if (value1 is byte)
//            {
//                return (byte)value1 * -1;
//            }
//            else if (value1 is sbyte)
//            {
//                return (sbyte)value1 * -1;
//            }
//            else if (value1 is short)
//            {
//                return (short)value1 * -1;
//            }
//            else if (value1 is ushort)
//            {
//                return (ushort)value1 * -1;
//            }
//            else if (value1 is int)
//            {
//                return (int)value1 * -1;
//            }
//            else if (value1 is uint)
//            {
//                return (uint)value1 * -1;
//            }
//            else if (value1 is long)
//            {
//                return (long)value1 * -1;
//            }
//            else if (value1 is ulong)
//            {
//                return (long)value1 * -1;
//            }
//            else if (value1 is int)
//            {
//                return (int)value1 * -1;
//            }
//            else if (value1 is double)
//            {
//                return (double)value1 * -1;
//            }
//            else if (value1 is decimal)
//            {
//                return (decimal)value1 * -1;
//            }
//            return value;
//        }
//        public override object Not(object value1)
//        {
//            bool value = false;
//            bool bvalue1 = Feng.Utils.ConvertHelper.ToBoolean(GetCellValue(value1), true);
//            value = !bvalue1;
//            return value;
//        }
//        public override object NotEqual(object value1, object value2)
//        {
//            bool res = Feng.Utils.ConvertHelper.Equals(value1, value2);
//            return !res;
//        }
//        public override object LogicalOR(object value1, object value2)
//        {
//            bool value = false;
//            bool bvalue1 = Feng.Utils.ConvertHelper.ToBoolean(GetCellValue(value1), true);
//            bool bvalue2 = Feng.Utils.ConvertHelper.ToBoolean(GetCellValue(value2), true);
//            value = bvalue1 || bvalue2;
//            return value;
//        }
//        public override object Parenthesis(object value1)
//        {
//            return value1;
//        }
//        public override object Property(object value1, object value2)
//        {
//            object value = null;
//            if (value1 != null && value2 != null)
//            {
//                Type type = value1.GetType();
//                if (type != null)
//                {
//                    System.Reflection.PropertyInfo[] pis = type.GetProperties();
//                    if (pis != null)
//                    {
//                        string p = value2.ToString().ToLower();
//                        foreach (System.Reflection.PropertyInfo pi in pis)
//                        {
//                            if (pi.Name.ToLower() == p)
//                            {
//                                return pi.GetValue(value1, null);
//                            }
//                        }
//                    }
//                }
//            }
//            return value;
//        }
//        public override object PropertyFunction(object value1, object value2, List<object> values)
//        {
//            object value = null;
//            if (value1 != null && value2 != null)
//            {
//                Type type = value1.GetType();
//                if (type != null)
//                {
//                    System.Reflection.MethodInfo[] pis = type.GetMethods();
//                    if (pis != null)
//                    {
//                        string p = value2.ToString().ToLower();
//                        foreach (System.Reflection.MethodInfo pi in pis)
//                        {
//                            System.Reflection.ParameterInfo[] pifs = pi.GetParameters();
//                            if (pi.Name.ToLower() == p)
//                            {
//                                return pi.Invoke(value1, values.ToArray());
//                            }
//                        }
//                    }
//                }
//            }
//            return value;
//        }
//        public override object BitwiseShiftLeft(object value1, object value2)
//        {
//            int res1 = Feng.Utils.ConvertHelper.ToInt32(GetCellValue(value1));
//            int res2 = Feng.Utils.ConvertHelper.ToInt32(GetCellValue(value2));
//            return res1 << res2;
//        }
//        public override object BitwiseShiftRight(object value1, object value2)
//        {
//            int res1 = Feng.Utils.ConvertHelper.ToInt32(GetCellValue(value1));
//            int res2 = Feng.Utils.ConvertHelper.ToInt32(GetCellValue(value2));
//            return res1 >> res2;
//        }
//        public override object BitwiseOR(object value1, object value2)
//        {
//            int res1 = Feng.Utils.ConvertHelper.ToInt32(GetCellValue(value1));
//            int res2 = Feng.Utils.ConvertHelper.ToInt32(GetCellValue(value2));
//            return res1 | res2;
//        }
//        public override object BitwiseAnd(object value1, object value2)
//        {
////            if (Feng.Utils.ConvertHelper.IsString(value1)
////|| Feng.Utils.ConvertHelper.IsString(value2))
////            {
//                return Feng.Utils.ConvertHelper.ToString(GetCellValue(value1)) + Feng.Utils.ConvertHelper.ToString(GetCellValue(value2));
//            //}
//            //int res1 = Feng.Utils.ConvertHelper.ToInt32(GetCellValue(value1));
//            //int res2 = Feng.Utils.ConvertHelper.ToInt32(GetCellValue(value2));
//            //return res1 & res2;
//        }
//        public override object BitwiseXOR(object value1, object value2)
//        {
//            int res1 = Feng.Utils.ConvertHelper.ToInt32(GetCellValue(value1));
//            int res2 = Feng.Utils.ConvertHelper.ToInt32(GetCellValue(value2));
//            return res1 ^ res2;
//        }
//        public override object BitwiseNOT(object value1)
//        {
//            int res1 = Feng.Utils.ConvertHelper.ToInt32(GetCellValue(value1));
//            return ~res1;
//        }
//        public override object ToNumber(object value1)
//        {
//            object obj= GetCellValue(value1);
//            if (obj is string)
//            { 
//                string text = Feng.Utils.ConvertHelper.ToString(obj);
//                if (!text.Contains("."))
//                {
//                    return Feng.Utils.ConvertHelper.ToInt32(text);
//                }
//            }
//            decimal res1 = Feng.Utils.ConvertHelper.ToDecimal(obj);
//            return res1;
//        }
//        public override object Increment(object value1)
//        {
//            int res1 = Feng.Utils.ConvertHelper.ToInt32(GetCellValue(value1));
//            res1++;
//            return res1;
//        }
//        public override object Decrement(object value1)
//        {
//            int res1 = Feng.Utils.ConvertHelper.ToInt32(GetCellValue(value1));
//            res1--;
//            return res1;
//        }
//        public override object ToString(object value1)
//        {
//            string res1 = Feng.Utils.ConvertHelper.ToString(GetCellValue(value1));
//            return res1;
//        }
//    }
//}
