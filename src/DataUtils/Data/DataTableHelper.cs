using System;
using System.Data;
namespace Feng.Data
{  
    public class DataTableHelper 
    {
        //public static DataTable MergeTable(DataTable tablea, DataTable tableb)
        //{
        //    DataTable tablemain = null;
        //    if (tablea.Rows.Count > tableb.Rows.Count)
        //    {
        //        tablemain = tablea.Copy();
        //        foreach (DataColumn column in tableb.Columns)
        //        {
        //            tablemain.Columns.Add(column.ColumnName, column.DataType);
        //        }
        //        foreach (var item in collection)
        //        {

        //        }
        //    }
        //    else
        //    {

        //    }
        //}

        public static int Count(DataTable table, string field)
        {
            if (table == null)
                return 0;
            return table.Rows.Count; 
        }

        public static decimal Sum(DataTable table, string field)
        {
            if (table == null)
                return decimal.Zero;
            if (table.Rows.Count < 1)
                return decimal.Zero;
            if (Exists(table, field))
            {
                DataColumn col = table.Columns[field];
                if (Feng.Utils.ConvertHelper.IsNumber(col.DataType))
                {
                    decimal d = decimal.Zero;
                    foreach (DataRow row in table.Rows)
                    {
                        d = d + Feng.Utils.ConvertHelper.ToDecimal(row[field]);
                    }
                    return d;
                }
            }
            return decimal.Zero;
        }
 
        public static decimal Avg(DataTable table, string field)
        {
            if (table == null)
                return decimal.Zero;
            if (table.Rows.Count < 1)
                return decimal.Zero;
            if (Exists(table, field))
            {
                DataColumn col = table.Columns[field];
                if (Feng.Utils.ConvertHelper.IsNumber(col.DataType))
                {
                    decimal d = decimal.Zero;
                    foreach (DataRow row in table.Rows)
                    {
                        d = d + Feng.Utils.ConvertHelper.ToDecimal(row[field]);
                    }
                    return d / table.Rows.Count;
                }
            }
            return decimal.Zero;
        }

        public static object Max(DataTable table, string field)
        {
            if (table == null)
                return null;
            if (table.Rows.Count < 1)
                return null; 
            if (Exists(table, field))
            {
                DataColumn col = table.Columns[field];
                if (Feng.Utils.ConvertHelper.IsNumber(col.DataType))
                {
                    decimal d = decimal.MinValue;
                    foreach (DataRow row in table.Rows)
                    {
                        decimal value = Feng.Utils.ConvertHelper.ToDecimal(row[field]);
                        if (d < value)
                        {
                            d = value;
                        }
                    }
                    return d;
                }
                else if (Feng.Utils.ConvertHelper.IsDateTime(col.DataType))
                {
                    DateTime? d = null;
                    foreach (DataRow row in table.Rows)
                    {
                        DateTime? value = Feng.Utils.ConvertHelper.ToDateTimeNullable(row[field]);
                        if (value.HasValue)
                        {
                            if (d == null)
                            {
                                d = value;
                            }
                            else
                            {
                                if (d < value)
                                {
                                    d = value;
                                }
                            }
                        } 
                    }
                    return d;
                }
                else  
                {
                    string d = null;
                    foreach (DataRow row in table.Rows)
                    {
                        string value = Feng.Utils.ConvertHelper.ToString(row[field]);
                        if (d == null)
                        {
                            d = value;
                            continue;
                        }
                        int res=Feng.Utils.CompareHelper.Compare(d,value);
 
                        if (res <0)
                        {
                            d = value;
                        }
                         
                    }
                    return d;
                }
            }
            return null;
        }

        public static object Min(DataTable table, string field)
        {
            if (table == null)
                return null;
            if (table.Rows.Count < 1)
                return null;
            if (Exists(table, field))
            {
                DataColumn col = table.Columns[field];
                if (Feng.Utils.ConvertHelper.IsNumber(col.DataType))
                {
                    decimal d = decimal.MaxValue;
                    foreach (DataRow row in table.Rows)
                    {
                        decimal value = Feng.Utils.ConvertHelper.ToDecimal(row[field]);
                        if (d > value)
                        {
                            d = value;
                        }
                    }
                    return d;
                }
                else if (Feng.Utils.ConvertHelper.IsDateTime(col.DataType))
                {
                    DateTime? d = null;
                    foreach (DataRow row in table.Rows)
                    {
                        DateTime? value = Feng.Utils.ConvertHelper.ToDateTimeNullable(row[field]);
                        if (value.HasValue)
                        {
                            if (d == null)
                            {
                                d = value;
                            }
                            else
                            {
                                if (d > value)
                                {
                                    d = value;
                                }
                            }
                        }
                    }
                    return d;
                }
                else
                {
                    string d = null;
                    foreach (DataRow row in table.Rows)
                    { 
                        string value = Feng.Utils.ConvertHelper.ToString(row[field]);
                        if (d == null)
                        {
                            d = value;
                            continue;
                        }
                        if (Feng.Utils.CompareHelper.Compare(d, value) > 0)
                        {
                            d = value;
                        }

                    }
                    return d;
                }
            }
            return null;
        }

        public static bool Exists(DataTable table, string columnname)
        {
            if (table == null)
                return false;
            bool res = false;
            foreach (DataColumn col in table.Columns)
            {
                if (col.ColumnName.ToLower().Equals(columnname.ToLower()))
                {
                    res = true;
                }
            }
            return res;
        }

        public static object GetValue(DataRow row , string column)
        {
            DataTable table = row.Table;
            if (!Exists(table, column))
            {
                return null;
            }
            return row[column];
        }

        public static DataTable Group(DataTable table, string showids
            , string groupid, string countids, string sumids
            , string avgids, string maxids, string minids)
        {
            DataTable tableres = new DataTable();
            string[] groupids = Feng.Utils.TextHelper.Split(groupid);
            Feng.Collections.DictionaryEx<string, DataRow> dicsgroup = new Collections.DictionaryEx<string, DataRow>();
            foreach (DataRow row in table.Rows)
            {
                string groupvalue = string.Empty;
                foreach (string group in groupids)
                {
                    string rowvaluegroup = Feng.Utils.ConvertHelper.ToString(row[group]);
                    groupvalue = groupvalue + "_" + rowvaluegroup;
                }

                DataRow grouprow = dicsgroup[groupvalue];
                if (grouprow == null)
                {
                    grouprow = tableres.NewRow();
                    tableres.Rows.Add(grouprow);
                    dicsgroup.Add(groupvalue, grouprow);
                }
                 
                foreach (string group in groupids)
                {
                    if (!string.IsNullOrWhiteSpace(group))
                    {
                        string fieldname = "group_" + group;
                        if (!tableres.Columns.Contains(fieldname))
                        {
                            tableres.Columns.Add(fieldname);
                        }
                        string targetvalue = Feng.Utils.ConvertHelper.ToString(row[group]);

                        grouprow[fieldname] = targetvalue;
                    }
                }

                string[] showides = Feng.Utils.TextHelper.Split(showids);
                foreach (string showid in showides)
                {
                    if (!string.IsNullOrWhiteSpace(showid))
                    {
                        string fieldname = "show_" + showid;
                        if (!tableres.Columns.Contains(fieldname))
                        {
                            tableres.Columns.Add(fieldname);
                        }
                        string targetvalue = Feng.Utils.ConvertHelper.ToString(row[showid]);

                        grouprow[fieldname] = targetvalue;
                    }
                }
                string[] countides = Feng.Utils.TextHelper.Split(countids);
                foreach (string countid in countides)
                {

                    if (!string.IsNullOrWhiteSpace(countid))
                    {
                        string fieldname = "count_" + countid;
                        if (!tableres.Columns.Contains(fieldname))
                        {
                            tableres.Columns.Add(fieldname);
                        }
                        int countvalue = Feng.Utils.ConvertHelper.ToInt32(grouprow[fieldname]);
                        grouprow[fieldname] = countvalue + 1;
                    }
                }
                string[] sumides = Feng.Utils.TextHelper.Split(sumids);
                foreach (string sumid in sumides)
                {
                    if (!string.IsNullOrWhiteSpace(sumid))
                    {
                        string fieldname = "sum_" + sumid;
                        if (!tableres.Columns.Contains(fieldname))
                        {
                            tableres.Columns.Add(fieldname);
                        }
                        decimal sumsourcevalue = Feng.Utils.ConvertHelper.ToDecimal(row[sumid], 0);
                        decimal sumtargetvalue = Feng.Utils.ConvertHelper.ToDecimal(grouprow[fieldname], 0);
                        grouprow[fieldname] = sumsourcevalue + sumtargetvalue;
                    }
                }
                string[] avgides = Feng.Utils.TextHelper.Split(avgids);
                foreach (string avgid in avgides)
                {
                    if (!string.IsNullOrWhiteSpace(avgid))
                    {
                        string fieldname = "avg_sum_" + avgid;
                        if (!tableres.Columns.Contains(fieldname))
                        {
                            tableres.Columns.Add(fieldname);
                        }
                        decimal sumsourcevalue = Feng.Utils.ConvertHelper.ToDecimal(row[avgid], 0);
                        decimal sumtargetvalue = Feng.Utils.ConvertHelper.ToDecimal(grouprow[fieldname], 0);
                        grouprow[fieldname] = sumsourcevalue + sumtargetvalue;


                        fieldname = "avg_count_" + avgid;
                        if (!tableres.Columns.Contains(fieldname))
                        {
                            tableres.Columns.Add(fieldname);
                        }
                        int avg_count_ = Feng.Utils.ConvertHelper.ToInt32(grouprow[fieldname], 0);
                        grouprow[fieldname] = avg_count_ + 1;
                    }
                }
                string[] maxides = Feng.Utils.TextHelper.Split(maxids);
                foreach (string maxid in maxides)
                {
                    if (!string.IsNullOrWhiteSpace(maxid))
                    {
                        string fieldname = "max_" + maxid;
                        if (!tableres.Columns.Contains(fieldname))
                        {
                            tableres.Columns.Add(fieldname);
                        }
                        decimal sourcevalue = Feng.Utils.ConvertHelper.ToDecimal(row[maxid], 0);
                        decimal targetvalue = Feng.Utils.ConvertHelper.ToDecimal(grouprow[fieldname], decimal.MinValue);
                        if (sourcevalue > targetvalue)
                        {
                            grouprow[fieldname] = sourcevalue;
                        }
                    }
                }
                string[] minides = Feng.Utils.TextHelper.Split(minids);
                foreach (string minid in minides)
                {
                    if (!string.IsNullOrWhiteSpace(minid))
                    {
                        string fieldname = "min_" + minid;
                        if (!tableres.Columns.Contains(fieldname))
                        {
                            tableres.Columns.Add(fieldname);
                        }
                        decimal sourcevalue = Feng.Utils.ConvertHelper.ToDecimal(row[minid], 0);
                        decimal targetvalue = Feng.Utils.ConvertHelper.ToDecimal(grouprow[fieldname], decimal.MaxValue);
                        if (sourcevalue < targetvalue)
                        {
                            grouprow[fieldname] = sourcevalue;
                        }
                    }
                }
            }
            return tableres;
        }
    }
}
