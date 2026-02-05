using Feng.Excel.Collections;
using Feng.Excel.Interfaces;
using System;
using System.Collections.Generic;

namespace Feng.Excel.Utils
{
    public class DataExcelTools
    {
        public static SelectCellCollection GetSelectCell(DataExcel grid, string text)
        {
            ICell begincell = null;
            SelectCellCollection selectcell = null;
            string cellreange = text;
            if (!cellreange.Contains(":"))
            {
                begincell = grid.GetCellByNameAndID(text);
                if (begincell == null)
                    return null;
                selectcell = new SelectCellCollection();
                selectcell.BeginCell = begincell;
                return selectcell;
            }
            string[] cells = cellreange.Split(':');
            if (cells.Length != 2)
            {
                return null;
            }
            begincell = grid.GetCellByNameAndID(cells[0]);
            if (begincell == null)
                return null;

            ICell endCell = grid.GetCellByNameAndID(cells[1]);
            if (endCell == null)
            {
                endCell = begincell;
            }
            selectcell = new SelectCellCollection();
            selectcell.BeginCell = begincell;
            selectcell.EndCell = endCell;
            return selectcell;
        }
        public static List<IRow> QueryRows(DataExcel grid, SelectCellCollection selectCelles, QueryArgs arg)
        {
            List<IRow> list = new List<IRow>();
            List<IRow> selrows = selectCelles.GetAllRows();
            foreach (IRow row in selrows)
            {

            }
            return list;
        }
        public static object GetRowValue(IRow row, QueryArgs arg)
        {
            int columnindex = 0;
            if (arg.ColumnIndex < 1)
            {
                foreach (IColumn item in row.Grid.Columns)
                {
                    if (item != null)
                    {
                        if (item.ID == arg.Field)
                        {
                            arg.ColumnIndex = item.Index;
                            break;
                        }
                    }
                } 
            }
            columnindex = arg.ColumnIndex;
            ICell cell = row[columnindex];
            if (cell != null)
            {
                return cell.Value;
            }
            return null;
        }
        public static bool Contains(IRow row, QueryArgs arg)
        {
            bool res = false;
            object value = GetRowValue(row, arg);
            string text = string.Empty;
            string argtext = string.Empty;
            bool isnum = Feng.Utils.ConvertHelper.IsNumber(value);
            bool istime = Feng.Utils.ConvertHelper.IsDateTime(value);
            decimal dvalue = 0;
            decimal dargvalue = 0;
            DateTime timevalue = DateTime.MinValue;
            DateTime timearg = DateTime.MinValue;
            switch (arg.QueryMode)
            {
                case QueryMode.QueryMode_Default: 
                case QueryMode.QueryMode_Equals:
                    res = arg.Equals(value);
                    break;
                case QueryMode.QueryMode_LessThan:
                    if (isnum)
                    {
                        dvalue = Feng.Utils.ConvertHelper.ToDecimal(value);
                        dargvalue = Feng.Utils.ConvertHelper.ToDecimal(arg.Value);
                        res = dvalue < dargvalue;
                    }
                    else if (istime)
                    {
                        timevalue = Feng.Utils.ConvertHelper.ToDateTime(value);
                        timearg = Feng.Utils.ConvertHelper.ToDateTime(arg.Value);
                        res = timevalue < timearg;
                    }
                    else
                    {
                        text = Feng.Utils.ConvertHelper.ToString(value);
                        argtext = Feng.Utils.ConvertHelper.ToString(arg.Value);
                        res = text.CompareTo(argtext) < 0;
                    }
                    break;
                case QueryMode.QueryMode_MoreThan:

                    if (isnum)
                    {
                        dvalue = Feng.Utils.ConvertHelper.ToDecimal(value);
                        dargvalue = Feng.Utils.ConvertHelper.ToDecimal(arg.Value);
                        res = dvalue > dargvalue;
                    }
                    else if (istime)
                    {
                        timevalue = Feng.Utils.ConvertHelper.ToDateTime(value);
                        timearg = Feng.Utils.ConvertHelper.ToDateTime(arg.Value);
                        res = timevalue > timearg;
                    }
                    else
                    {
                        text = Feng.Utils.ConvertHelper.ToString(value);
                        argtext = Feng.Utils.ConvertHelper.ToString(arg.Value);
                        res = text.CompareTo(argtext) > 0;
                    }
                    break;
                case QueryMode.QueryMode_Like:
                    text = Feng.Utils.ConvertHelper.ToString(value);
                    argtext = Feng.Utils.ConvertHelper.ToString(arg.Value);
                    res = text.Contains(argtext);
                    break;
                case QueryMode.QueryMode_LeftLike:
                    text = Feng.Utils.ConvertHelper.ToString(value);
                    argtext = Feng.Utils.ConvertHelper.ToString(arg.Value);
                    res = text.StartsWith(argtext);
                    break;
                case QueryMode.QueryMode_RightLike:
                    text = Feng.Utils.ConvertHelper.ToString(value);
                    argtext = Feng.Utils.ConvertHelper.ToString(arg.Value);
                    res = text.EndsWith(argtext);
                    break;
                case QueryMode.QueryMode_LessThanAndEquals:

                    if (isnum)
                    {
                        dvalue = Feng.Utils.ConvertHelper.ToDecimal(value);
                        dargvalue = Feng.Utils.ConvertHelper.ToDecimal(arg.Value);
                        res = dvalue <= dargvalue;
                    }
                    else if (istime)
                    {
                        timevalue = Feng.Utils.ConvertHelper.ToDateTime(value);
                        timearg = Feng.Utils.ConvertHelper.ToDateTime(arg.Value);
                        res = timevalue <= timearg;
                    }
                    else
                    {
                        text = Feng.Utils.ConvertHelper.ToString(value);
                        argtext = Feng.Utils.ConvertHelper.ToString(arg.Value);
                        res = text.CompareTo(argtext) <= 0;
                    }
                    break;
                case QueryMode.QueryMode_MoreThanAndEquals:

                    if (isnum)
                    {
                        dvalue = Feng.Utils.ConvertHelper.ToDecimal(value);
                        dargvalue = Feng.Utils.ConvertHelper.ToDecimal(arg.Value);
                        res = dvalue >= dargvalue;
                    }
                    else if (istime)
                    {
                        timevalue = Feng.Utils.ConvertHelper.ToDateTime(value);
                        timearg = Feng.Utils.ConvertHelper.ToDateTime(arg.Value);
                        res = timevalue >= timearg;
                    }
                    else
                    {
                        text = Feng.Utils.ConvertHelper.ToString(value);
                        argtext = Feng.Utils.ConvertHelper.ToString(arg.Value);
                        res = text.CompareTo(argtext) >= 0;
                    }
                    break;
                default:
                    break;
            }
            if (arg.RelationOr)
            {
                if (res)
                { 
                    return true;
                }
                else
                {
                    bool res2 = Contains(row, arg.Arg);
                    if (res2)
                    {
                        return true;
                    }
                    return false;
                }
            }
            else
            {
                if (!res)
                { 
                    return false;
                }
                else
                {
                    bool res2 = Contains(row, arg.Arg);
                    if (!res2)
                    {
                        return false;
                    }
                    return true;
                }
            } 
        }


        public static object DataProjectSubCmd_PFFindValue(DataExcel tempgrid
            , string columnname
            , string value
            , string targetcolumn)
        {
            IColumn column = null;
            foreach (IColumn item in tempgrid.Columns)
            {
                string id = item.ID;
                if (item.Index < 1)
                    continue;
                if (string.IsNullOrWhiteSpace(id))
                {
                    id = item.Name;
                }
                if (id == columnname)
                {
                    column = item;
                }
            }
            if (column != null)
            {
                foreach (IRow row in tempgrid.Rows)
                {
                    ICell cell = row[column];
                    if (cell == null)
                        continue;
                    string txt = Feng.Utils.ConvertHelper.ToString(cell.Value);
                    if (txt == value)
                    {
                        foreach (IColumn item in tempgrid.Columns)
                        {
                            string id = item.ID;
                            if (item.Index < 1)
                                continue;
                            if (string.IsNullOrWhiteSpace(id))
                            {
                                id = item.Name;
                            }
                            if (string.IsNullOrWhiteSpace(id))
                                continue;
                            if (id == targetcolumn)
                            {
                                cell = row[item];
                                if (cell != null)
                                {
                                    object cellvalue = cell.Value;
                                    return cellvalue;
                                }
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
    public enum QueryMode
    {
        QueryMode_Default,
        QueryMode_Equals,
        QueryMode_LessThan,
        QueryMode_MoreThan,
        QueryMode_Like,
        QueryMode_LeftLike,
        QueryMode_RightLike,
        QueryMode_LessThanAndEquals,
        QueryMode_MoreThanAndEquals,
    }
    public class QueryArgs
    {
        public QueryArgs()
        {
            ColumnIndex = -1;
        }
        public QueryMode QueryMode { get; set; }
        public string Field { get; set; }
        public string Script { get; set; }
        public object Value { get; set; }
        public int ColumnIndex { get; set; }
        public QueryArgs Arg { get; set; }
        public bool RelationOr { get; set; }
    }
}
