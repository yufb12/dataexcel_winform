using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using Feng.Utils;

using System.Collections;
using System.Reflection;
namespace Feng.Data
{
    public abstract class SortData
    {
        [NonSerialized]
        private Feng.Forms.ComponentModel.SortInfo _SortInfo = null;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Feng.Forms.ComponentModel.SortInfo SortInfo
        {
            get
            {
                return _SortInfo;
            }
            set
            {
                _SortInfo = value;
            }
        }

        public virtual void Sort()
        {

        }

        public virtual object Source { get; set; }
        public abstract object GetValue(int index);

        public int Compare(object oldvalue, object newvalue, byte type)
        {
            if (oldvalue == null && newvalue == null)
            {
                return 0;
            }
            else if (oldvalue == null)
            {
                return -1;
            }
            else if (newvalue == null)
            {
                return 1;
            }

            if (oldvalue == DBNull.Value && newvalue == DBNull.Value)
            {
                return 0;
            }
            else if (oldvalue == DBNull.Value)
            {
                return -1;
            }
            else if (newvalue == DBNull.Value)
            {
                return 1;
            }
            int result = 0;
            switch (type)
            {
                case TypeEnum.Tbool:
                    result = CompareHelper.Compare((bool)oldvalue, (bool)newvalue);
                    break;
                case TypeEnum.Tstring:
                    result = CompareHelper.Compare((string)oldvalue, (string)newvalue);
                    break;
                case TypeEnum.Tdecimal:
                    result = CompareHelper.Compare((decimal)oldvalue, (decimal)newvalue);
                    break;
                case TypeEnum.Tbyte:
                    result = CompareHelper.Compare((byte)oldvalue, (byte)newvalue);
                    break;
                case TypeEnum.Tchar:
                    result = CompareHelper.Compare((char)oldvalue, (char)newvalue);
                    break;
                case TypeEnum.TDateTime:
                    result = CompareHelper.Compare((DateTime)oldvalue, (DateTime)newvalue);
                    break;
                case TypeEnum.TNullableDateTime:
                    result = CompareHelper.Compare((DateTime?)oldvalue, (DateTime?)newvalue);
                    break;
                case TypeEnum.Tdouble:
                    result = CompareHelper.Compare((double)oldvalue, (double)newvalue);
                    break;
                case TypeEnum.Tfloat:
                    result = CompareHelper.Compare((float)oldvalue, (float)newvalue);
                    break;
                case TypeEnum.Tint:
                    result = CompareHelper.Compare((int)oldvalue, (int)newvalue);
                    break;
                case TypeEnum.Tlong:
                    result = CompareHelper.Compare((long)oldvalue, (long)newvalue);
                    break;
                case TypeEnum.Tsbyte:
                    result = CompareHelper.Compare((sbyte)oldvalue, (sbyte)newvalue);
                    break;
                case TypeEnum.Tshort:
                    result = CompareHelper.Compare((short)oldvalue, (short)newvalue);
                    break;
                case TypeEnum.Tuint:
                    result = CompareHelper.Compare((uint)oldvalue, (uint)newvalue);
                    break;
                case TypeEnum.Tulong:
                    result = CompareHelper.Compare((ulong)oldvalue, (ulong)newvalue);
                    break;
                case TypeEnum.Tushort:
                    result = CompareHelper.Compare((ushort)oldvalue, (ushort)newvalue);
                    break;

                case TypeEnum.TNullablebool:
                    result = CompareHelper.Compare((bool?)oldvalue, (bool?)newvalue);
                    break; 
                case TypeEnum.TNullabledecimal:
                    result = CompareHelper.Compare((decimal?)oldvalue, (decimal?)newvalue);
                    break;
                case TypeEnum.TNullablebyte:
                    result = CompareHelper.Compare((byte?)oldvalue, (byte?)newvalue);
                    break;
                case TypeEnum.TNullablechar:
                    result = CompareHelper.Compare((char?)oldvalue, (char?)newvalue);
                    break;
                case TypeEnum.TNullabledouble:
                    result = CompareHelper.Compare((double?)oldvalue, (double?)newvalue);
                    break;
                case TypeEnum.TNullablefloat:
                    result = CompareHelper.Compare((float?)oldvalue, (float?)newvalue);
                    break;
                case TypeEnum.TNullableint:
                    result = CompareHelper.Compare((int?)oldvalue, (int?)newvalue);
                    break;
                case TypeEnum.TNullablelong:
                    result = CompareHelper.Compare((long?)oldvalue, (long?)newvalue);
                    break;
                case TypeEnum.TNullablesbyte:
                    result = CompareHelper.Compare((sbyte?)oldvalue, (sbyte?)newvalue);
                    break;
                case TypeEnum.TNullableshort:
                    result = CompareHelper.Compare((short?)oldvalue, (short?)newvalue);
                    break;
                case TypeEnum.TNullableuint:
                    result = CompareHelper.Compare((uint?)oldvalue, (uint?)newvalue);
                    break;
                case TypeEnum.TNullableulong:
                    result = CompareHelper.Compare((ulong?)oldvalue, (ulong?)newvalue);
                    break;
                case TypeEnum.TNullableushort:
                    result = CompareHelper.Compare((ushort?)oldvalue, (ushort?)newvalue);
                    break;
                default:
                    break;
            }
            return result;
        }
         
    }
    public class SortDataTable : SortData   
    {
        public override object GetValue(int index)
        {
            if (index < list.Count)
                return list[index];
            return null;
        }
 
        public override void Sort()
        {
            Sort(Source as DataTable);
        }
        public List<DataRow> list = new List<DataRow>();
        private void Sort(System.Data.DataTable table)
        {
            if (this.SortInfo == null)
            {
                return;
            }
            if (table == null)
                return;
            list.Clear();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                
                list.Add(table.Rows[i]);
            }
            list.Sort(Compare);
        }
        public int Compare(DataRow old, DataRow newobj)
        {
            foreach (Feng.Forms.ComponentModel.SortColumn col in this.SortInfo)
            {
                if (col.Field.Length < 1)
                {
                    return 0;
                }
                if (Compare(old[col.Field], newobj[col.Field], col.Type) > 0)
                {
                    if (col.SortOrder == Feng.Forms.ComponentModel.SortOrder.Ascending)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else if (Compare(old[col.Field], newobj[col.Field], col.Type) < 0)
                {
                    if (col.SortOrder == Feng.Forms.ComponentModel.SortOrder.Ascending)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }
        //public int Compare(object oldvalue, object newvalue, byte type)
        //{
        //    if (oldvalue == null && newvalue == null)
        //    {
        //        return 0;
        //    }
        //    else if (oldvalue == null)
        //    {
        //        return -1;
        //    }
        //    else if (newvalue == null)
        //    {
        //        return 1;
        //    }

        //    if (oldvalue == DBNull.Value && newvalue == DBNull.Value)
        //    {
        //        return 0;
        //    }
        //    else if (oldvalue == DBNull.Value)
        //    {
        //        return -1;
        //    }
        //    else if (newvalue == DBNull.Value)
        //    {
        //        return 1;
        //    }
        //    int result = 0;
        //    switch (type)
        //    {
        //        case TypeEnum.Tbool:
        //            result = CompareHelper.Compare((bool)oldvalue, (bool)newvalue);
        //            break;
        //        case TypeEnum.Tstring:
        //            result = CompareHelper.Compare((string)oldvalue, (string)newvalue);
        //            break;
        //        case TypeEnum.Tdecimal:
        //            result = CompareHelper.Compare((decimal)oldvalue, (decimal)newvalue);
        //            break;
        //        case TypeEnum.Tbyte:
        //            result = CompareHelper.Compare((byte)oldvalue, (byte)newvalue);
        //            break;
        //        case TypeEnum.Tchar:
        //            result = CompareHelper.Compare((char)oldvalue, (char)newvalue);
        //            break;
        //        case TypeEnum.TDateTime:
        //            result = CompareHelper.Compare((DateTime)oldvalue, (DateTime)newvalue);
        //            break;
        //        case TypeEnum.TNullableDateTime:
        //            result = CompareHelper.Compare((DateTime?)oldvalue, (DateTime?)newvalue);
        //            break;
        //        case TypeEnum.Tdouble:
        //            result = CompareHelper.Compare((double)oldvalue, (double)newvalue);
        //            break;
        //        case TypeEnum.Tfloat:
        //            result = CompareHelper.Compare((float)oldvalue, (float)newvalue);
        //            break;
        //        case TypeEnum.Tint:
        //            result = CompareHelper.Compare((int)oldvalue, (int)newvalue);
        //            break;
        //        case TypeEnum.Tlong:
        //            result = CompareHelper.Compare((long)oldvalue, (long)newvalue);
        //            break;
        //        case TypeEnum.Tsbyte:
        //            result = CompareHelper.Compare((sbyte)oldvalue, (sbyte)newvalue);
        //            break;
        //        case TypeEnum.Tshort:
        //            result = CompareHelper.Compare((short)oldvalue, (short)newvalue);
        //            break;
        //        case TypeEnum.Tuint:
        //            result = CompareHelper.Compare((uint)oldvalue, (uint)newvalue);
        //            break;
        //        case TypeEnum.Tulong:
        //            result = CompareHelper.Compare((ulong)oldvalue, (ulong)newvalue);
        //            break;
        //        case TypeEnum.Tushort:
        //            result = CompareHelper.Compare((ushort)oldvalue, (ushort)newvalue);
        //            break;

        //        case TypeEnum.TNullablebool:
        //            result = CompareHelper.Compare((bool?)oldvalue, (bool?)newvalue);
        //            break; 
        //        case TypeEnum.TNullabledecimal:
        //            result = CompareHelper.Compare((decimal?)oldvalue, (decimal?)newvalue);
        //            break;
        //        case TypeEnum.TNullablebyte:
        //            result = CompareHelper.Compare((byte?)oldvalue, (byte?)newvalue);
        //            break;
        //        case TypeEnum.TNullablechar:
        //            result = CompareHelper.Compare((char?)oldvalue, (char?)newvalue);
        //            break; 
        //        case TypeEnum.TNullabledouble:
        //            result = CompareHelper.Compare((double?)oldvalue, (double?)newvalue);
        //            break;
        //        case TypeEnum.TNullablefloat:
        //            result = CompareHelper.Compare((float?)oldvalue, (float?)newvalue);
        //            break;
        //        case TypeEnum.TNullableint:
        //            result = CompareHelper.Compare((int?)oldvalue, (int?)newvalue);
        //            break;
        //        case TypeEnum.TNullablelong:
        //            result = CompareHelper.Compare((long?)oldvalue, (long?)newvalue);
        //            break;
        //        case TypeEnum.TNullablesbyte:
        //            result = CompareHelper.Compare((sbyte?)oldvalue, (sbyte?)newvalue);
        //            break;
        //        case TypeEnum.TNullableshort:
        //            result = CompareHelper.Compare((short?)oldvalue, (short?)newvalue);
        //            break;
        //        case TypeEnum.TNullableuint:
        //            result = CompareHelper.Compare((uint?)oldvalue, (uint?)newvalue);
        //            break;
        //        case TypeEnum.TNullableulong:
        //            result = CompareHelper.Compare((ulong?)oldvalue, (ulong?)newvalue);
        //            break;
        //        case TypeEnum.TNullableushort:
        //            result = CompareHelper.Compare((ushort?)oldvalue, (ushort?)newvalue);
        //            break;
        //        default:
        //            break;
        //    }
        //    return result;
        //}
    }


    public class SortModel : SortData
    {
        public override object GetValue(int index)
        {
            if (index < list.Count)
                return list[index];
            return null;
        }

        public override void Sort()
        {
            Sort(Source as IList);
        }
        public List<object> list = new List<object>();
        public void Sort(IList table)
        {
            if (this.SortInfo == null)
            {
                return;
            }
            if (table == null)
            {
                return;
            }
            List<object> list = new List<object>();
            for (int i = 0; i < table.Count; i++)
            {
                list.Add(table[i]);
            }

            list.Sort(Comparer); 
        }
        int Comparer(object value1, object value2)
        {
            object old = value1; object newobj = value2;
            if (old == null)
            {
                old = newobj;
            }
            if (newobj == null)
            {
                newobj = old;
            }
            if (old == null)
            {
                return 0;
            }
            Type t = old.GetType();

            foreach (Feng.Forms.ComponentModel.SortColumn col in SortInfo)
            { 
                object valuep1 = ReflectionHelper.GetValue(old, col.Field);
                object valuep2 = ReflectionHelper.GetValue(newobj, col.Field);
                int rescom = Compare(valuep1, valuep2, col.Type);
                if (rescom > 0)
                {
                    if (col.SortOrder == Feng.Forms.ComponentModel.SortOrder.Ascending)
                    {
                        return 1;
                    }
                    else
                    {
                        return -1;
                    }
                }
                else if (rescom < 0)
                {
                    if (col.SortOrder == Feng.Forms.ComponentModel.SortOrder.Ascending)
                    {
                        return -1;
                    }
                    else
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }
    }
}
