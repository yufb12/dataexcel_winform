using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Reflection;
using Feng.Utils;
using Feng.Excel.Interfaces;
using Feng.Excel.Builder;
using Feng.Excel.Collections;
using System.Drawing;
using Feng.Script.CBEexpress;
using Feng.Script.Method;

namespace Feng.Excel.Script
{
    [Serializable]
    public class DataExcelColumnFunctionContainer : DataExcelMethodContainer
    {

        public const string Function_Name = "DataExcelColumn";
        public const string Function_Description = "表格列函数";
        public override string Name
        {
            get { return Function_Name; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }

        public DataExcelColumnFunctionContainer()
        {
            BaseMethod model = null;


            model = new BaseMethod();
            model.Name = "Column";
            model.Description = @"获取列 Column(""A5"")";
            model.Eg = @"Column(CELL(""A5""))";
            model.Function = Column;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ColumnLeft";
            model.Description = @"获取左列 ColumnLeft(me)";
            model.Eg = @"ColumnLeft(me)";
            model.Function = ColumnLeft;
            MethodList.Add(model);
 
            model = new BaseMethod();
            model.Name = "ColumnRight";
            model.Description = @"获取右列 ColumnRight(me)";
            model.Eg = @"ColumnRight(me)";
            model.Function = ColumnRight;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Columns";
            model.Description = @"获取创建列集合 Columns";
            model.Eg = @"Columns()";
            model.Function = Columns;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ColumnCaption";
            model.Description = @"设置单元格标题 ColumnCaption(CELL(""A5""),""Caption"")";
            model.Eg = @"ColumnCaption(CELL(""A5""),""Caption"")";
            model.Function = ColumnCaption;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ColumnIndex";
            model.Description = @"获取列 ColumnIndex(""A5"")";
            model.Eg = @"ColumnIndex(CELL(""A5""))";
            model.Function = ColumnIndex;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ColumnName";
            model.Description = @"获取列 ColumnName(""A5"")";
            model.Eg = @"ColumnName(CELL(""A5""))";
            model.Function = ColumnName;
            MethodList.Add(model);
 
            model = new BaseMethod();
            model.Name = "ColumnCell";
            model.Description = @"返回单元格 ColumnCell(column,5)";
            model.Eg = @"ColumnCell(column,5)";
            model.Function = ColumnCell;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ColumnCells";
            model.Description = @"返回单元格集合 ColumnCells(me,3,10);";
            model.Eg = @"ColumnCells(me,3,10);//返回当前单元格,第3行到第10的单元格集合";
            model.Function = ColumnCells;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ColumnWidth";
            model.Description = @"设置列宽 ColumnWidth(CELL(""A5""),72)";
            model.Eg = @"ColumnWidth(CELL(""A5""),72)";
            model.Function = ColumnWidth;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ColumnVisible";
            model.Description = @"设置列是否可见 ColumnVisible(CELL(""A5""),1)";
            model.Eg = @"ColumnVisible(CELL(""A5""),0)";
            model.Function = ColumnVisible;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ColumnReadOnly";
            model.Description = @"设置列是否只读 ColumnReadOnly(CELL(""A5""),1)";
            model.Eg = @"ColumnReadOnly(CELL(""A5""),0)";
            model.Function = ColumnReadOnly;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ColumnDelete";
            model.Description = @"删除列 ColumnDelete(CELL(""A5""))";
            model.Eg = @"ColumnDelete(CELL(""A5""))";
            model.Function = ColumnDelete;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ColumnInsert";
            model.Description = @"插入列 ColumnInsert(3)";
            model.Eg = @"ColumnInsert(3)";
            model.Function = ColumnInsert;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ColumnCopyInsert";
            model.Description = @"插入复制列 ColumnCopyInsert(CELL(""A5""))";
            model.Eg = @"ColumnCopyInsert(CELL(""A5""))";
            model.Function = ColumnCopyInsert;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ColumnID";
            model.Description = @"设置列ID ColumnID(CELL(""A5""),""ID"")";
            model.Eg = @"ColumnID(CELL(""A5""),""ID"")";
            model.Function = ColumnID;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ColumnBoundLeft";
            model.Description = "左边距";
            model.Eg = @"ColumnBoundLeft(Cell(""B3""))";
            model.Function = ColumnBoundLeft;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ColumnBoundTop";
            model.Description = "上边距";
            model.Eg = @"ColumnBoundTop(Cell(""B3""))";
            model.Function = ColumnBoundTop;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ColumnBoundWidth";
            model.Description = "宽度";
            model.Eg = @"ColumnBoundWidth(Cell(""B3""))";
            model.Function = ColumnBoundWidth;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ColumnBoundHeight";
            model.Description = "高度";
            model.Eg = @"ColumnBoundHeight(Cell(""B3""))";
            model.Function = ColumnBoundHeight;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ColumnBounds";
            model.Description = "大小和位置";
            model.Eg = @"ColumnBounds(Cell(""B3""))";
            model.Function = ColumnBounds;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ColumnFind";
            model.Description = "在当前列中查找相等的单元格";
            model.Eg = @"ColumnFind(""AAA"")";
            model.Function = ColumnFind;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "ColumnFind";
            model.Description = "在当前列中查找相等的单元格";
            model.Eg = @"ColumnFind(""AAA"")";
            model.Function = ColumnFind;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ColumnClearCell";
            model.Description = "清除某列单元格的内容";
            model.Eg = @"ColumnClearCell(""D"")";
            model.Function = ColumnClearCell;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "ColumnTrimCell";
            model.Description = "去除某列单元格内容的空格";
            model.Eg = @"ColumnTrimCell(""D"")";
            model.Function = ColumnTrimCell;
            MethodList.Add(model);
        }
        public virtual object ColumnBoundLeft(params object[] args)
        {
            IColumn item = base.GetArgIndex(1, args) as IColumn;
            if (item == null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    item = cell.Column;
                }
            }
            if (item != null)
            {
                return item.Left;
            }
            return null;
        }
        public virtual object ColumnBoundTop(params object[] args)
        {
            IColumn item = base.GetArgIndex(1, args) as IColumn;
            if (item == null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    item = cell.Column;
                }
            }
            if (item != null)
            {
                return item.Top;
            }
            return null;
        }
        public virtual object ColumnBoundWidth(params object[] args)
        {
            IColumn item = base.GetArgIndex(1, args) as IColumn;
            if (item == null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    item = cell.Column;
                }
            }
            if (item != null)
            {
                return item.Width;
            }
            return null;
        }
        public virtual object ColumnBoundHeight(params object[] args)
        {
            IColumn item = base.GetArgIndex(1, args) as IColumn;
            if (item == null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    item = cell.Column;
                }
            }
            if (item != null)
            {
                return item.Height;
            }
            return null;
        }
        public virtual object ColumnBounds(params object[] args)
        {
            IColumn item = base.GetArgIndex(1, args) as IColumn;
            if (item == null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    item = cell.Column;
                }
            }
            if (item != null)
            {
                return item.Rect;
            }
            return null;
        }
 
        public virtual object ColumnCopyInsert(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            object indexobj = ColumnIndex(args);
            if (indexobj != null)
            {
                int index = ConvertHelper.ToInt32(indexobj);
                IColumn item = proxy.Grid.ClassFactory.CreateDefaultColumn(proxy.Grid, index);
                proxy.Grid.Columns.Insert(index, item);
                return item;
            }
            return null;
        }
        public virtual object ColumnInsert(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            object indexobj = ColumnIndex(args);
            if (indexobj != null)
            {
                int index = ConvertHelper.ToInt32(indexobj);
                IColumn item = proxy.Grid.ClassFactory.CreateDefaultColumn(proxy.Grid, index);
                proxy.Grid.Columns.Insert(index, item);
                return item;
            }
            return null;
        }
        public virtual object ColumnDelete(params object[] args)
        {
            IColumn item = base.GetArgIndex(1,args)as IColumn;

            if (item != null)
            {
                item.Grid.Columns.Remove(item);
                item.Grid.Columns.Refresh();
            }
            return null;
        }
        public virtual object ColumnID(params object[] args)
        {
            IColumn item = base.GetArgIndex(1,args)as IColumn;

            if (args.Length == 3)
            {
                string value = base.GetTextValue(2, args);
                item.ID = value;
                return Feng.Utils.Constants.OK;
            }
            if (item != null)
            {
                return item.ID;
            }
            return null;
        }
        public virtual object ColumnReadOnly(params object[] args)
        {
            IColumn item = base.GetArgIndex(1,args)as IColumn;
            if (args.Length == 3)
            {
                bool value = base.GetBooleanValue(2, args);
                item.ReadOnly = value;
                item.InhertReadOnly = false;
                return Feng.Utils.Constants.OK;
            }
            if (item != null)
            {
                return item.ReadOnly;
            }
            return null;
        }
        public virtual object ColumnVisible(params object[] args)
        {
            IColumn item = base.GetArgIndex(1,args)as IColumn;
            if (args.Length == 3)
            {
                bool value = base.GetBooleanValue(2, args);
                item.Visible = value;
                return Feng.Utils.Constants.OK;
            }
            if (item != null)
            {
                return item.Visible;
            }
            return null;
        }
        public virtual object ColumnWidth(params object[] args)
        {
            IColumn item = base.GetArgIndex(1,args)as IColumn;

            if (args.Length == 3)
            {
                int value = base.GetIntValue(2, args);
                item.Width = value;
                return Feng.Utils.Constants.OK;
            }
            if (item != null)
            {
                return item.Width;
            }
            return null;
        }
        public virtual object ColumnName(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    return cell.Column.Name;
                }
                IColumn item = base.GetArgIndex(1, args) as IColumn;
                if (item != null)
                {
                    return item.Name;
                }
                int index = base.GetIntValue(1, args);
                if (index < -1000 && index > 100000)
                {
                    return null;
                }
                return proxy.Grid.Columns[index].Name;
            }
            return null;
        }
        public virtual object ColumnCaption(params object[] args)
        {
            IColumn item = base.GetArgIndex(1,args)as IColumn;

            if (args.Length == 3)
            {
                string value = base.GetTextValue(2, args);
                item.Caption = value;
                return Feng.Utils.Constants.OK;
            }
            if (item != null)
            {
                return item.Caption;
            }
            return null;
        }
        public virtual object ColumnIndex(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    return cell.Column.Index;
                }
                IColumn item =base.GetArgIndex(1,args) as IColumn;
                if (item != null)
                {
                    return item.Index;
                }
            }
            return null;
        }
        public virtual object Column(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    return cell.Column;
                }
                int index = base.GetIntValue(1, args);
                if (index > 0 && index < 10000)
                {
                    return proxy.Grid.Columns[index];
                }
                string id = base.GetTextValue(1, args);
                foreach (IColumn item in proxy.Grid.Columns)
                {
                    if (string.IsNullOrWhiteSpace(item.ID))
                        continue;
                    if (item.ID == id)
                    {
                        return item;
                    }
                }
                return null;
            }
            return null;
        }
        public virtual object ColumnLeft(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    int col = cell.Column.Index - 1;
                    if (col > 0)
                    {
                        return proxy.Grid.Columns[col];
                    }
                    return null;
                } 
                string id = base.GetTextValue(1, args);
                foreach (IColumn item in proxy.Grid.Columns)
                {
                    if (string.IsNullOrWhiteSpace(item.ID))
                        continue;
                    if (item.ID == id)
                    {
                        int col = item.Index - 1;
                        if (col > 0)
                        {
                            return proxy.Grid.Columns[col];
                        }
                        return null;
                    }
                } 
            }
            return null;
        }
        public virtual object ColumnRight(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    int col = cell.Column.Index + 1;
                    if (col > 0)
                    {
                        return proxy.Grid.Columns[col];
                    }
                    return null;
                }
                string id = base.GetTextValue(1, args);
                foreach (IColumn item in proxy.Grid.Columns)
                {
                    if (string.IsNullOrWhiteSpace(item.ID))
                        continue;
                    if (item.ID == id)
                    {
                        int col = item.Index + 1;
                        if (col > 0)
                        {
                            return proxy.Grid.Columns[col];
                        }
                        return null;
                    }
                }
            }
            return null;
        }
        public virtual object Columns(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            Feng.Collections.ListEx<IColumn> list = new Feng.Collections.ListEx<IColumn>();
            if (proxy != null)
            {
                foreach (IColumn item in proxy.Grid.Columns)
                {
                    list.Add(item);
                }
            }
            return list;
        }
        public virtual object ColumnFind(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                ICell cell = null;
                IColumn column = Column(args[0], args[1]) as IColumn;
                if (column == null)
                    return null;
                object value = base.GetArgIndex(2, args);
                bool fun = base.GetBooleanValue(3, args);
                DataExcel grid = proxy.Grid;
                foreach (IRow item in grid.Rows)
                {
                    cell = item.Cells[column];
                    if (cell != null)
                    {
                        if (fun)
                        {
                            string script = Feng.Utils.ConvertHelper.ToString(value);
                            if (!string.IsNullOrWhiteSpace(script))
                            {
                                object result = ScriptBuilder.Exec(grid, cell, script);
                                bool res = Feng.Utils.ConvertHelper.ToBoolean(result);
                                if (res)
                                {
                                    return cell;
                                }
                            }
                        }
                        if (value.Equals(cell.Value))
                        {
                            return cell;
                        }
                    }
                }
                return null;
            }
            return null;
        }

        public virtual object ColumnClearCell(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                ICell cell = null;
                IColumn column = Column(args[0], args[1]) as IColumn;
                if (column == null)
                    return null; 
                DataExcel grid = proxy.Grid;
                foreach (IRow item in grid.Rows)
                {
                    if (item.Index > 0)
                    {
                        cell = item.Cells[column];
                        if (cell != null)
                        {
                            cell.Clear();
                        }
                    }
                } 
            } 
            return Feng.Utils.Constants.OK;
        }
        public virtual object ColumnTrimCell(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                ICell cell = null;
                IColumn column = Column(args[0], args[1]) as IColumn;
                if (column == null)
                    return null;
                DataExcel grid = proxy.Grid;
                foreach (IRow item in grid.Rows)
                {
                    if (item.Index > 0)
                    {
                        cell = item.Cells[column];
                        if (cell != null)
                        {
                            cell.Value = cell.Text.Trim();
                        }
                    }
                }
            }
            return Feng.Utils.Constants.OK;
        }

        public virtual object ColumnCell(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                IColumn column = null;
                column = base.GetArgIndex(1, args) as IColumn;
                if (column == null)
                {
                    ICell cell = base.GetCell(1, args);
                    if (cell != null)
                    {
                        column = cell.Column;
                    }
                }
                if (column == null)
                {
                    return null;
                }
                object obj = base.GetArgIndex(2, args);
                IRow row = obj as IRow;
                if (row != null)
                {
                    return row[column];
                }
                string id = base.GetTextValue(2, args);
                if (!string.IsNullOrWhiteSpace(id))
                {
                    foreach (IRow item in proxy.Grid.Rows)
                    {
                        string columnid = item.ID;
                        if (string.IsNullOrWhiteSpace(columnid))
                        {
                            columnid = item.Name;
                        }
                        if (item.ID == id)
                        {
                            return item[column];
                        }
                    }
                }

                int index = base.GetIntValue(2, args);
                if (index > 0)
                {
                    IRow row1 = proxy.Grid.Rows[index];
                    if (row1 != null)
                    {
                        return row1[column];
                    }
                }
                return null;
            }
            return null;
        }

        public virtual object ColumnCells(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                IColumn column = null;
                int rowindexbegin = 0;
                int rowindexend = 0;
                int columnindex = 0;
                column = base.GetArgIndex(1, args) as IColumn;
                if (column == null)
                {
                    ICell cell = base.GetCell(1, args);
                    if (cell != null)
                    {
                        column = cell.Column;
                    }
                }
                if (column != null)
                {
                    columnindex = column.Index;
                }
                else
                {
                    columnindex = base.GetIntValue(1, 0, args);
                }
                object obj = base.GetArgIndex(2, args);
                IRow row = obj as IRow;
                if (row != null)
                {
                    rowindexbegin = row.Index;
                }
                else
                {
                    rowindexbegin = base.GetIntValue(2, 0, args);
                }
                obj = base.GetArgIndex(3, args);
                row = obj as IRow;
                if (row != null)
                {
                    rowindexend = row.Index;
                }
                else
                {
                    rowindexend = base.GetIntValue(3, 0, args);
                }
                if (rowindexbegin < 1)
                    return null;
                if (rowindexend < 1)
                    return null;
                if (columnindex < 1)
                    return null;
                List<ICell> list = new List<ICell>();
                for (int i = rowindexbegin; i <= rowindexend; i++)
                {
                    ICell cell = proxy.Grid[i, columnindex];
                    list.Add(cell);
                }
                return list;
            }
            return null;
        }
    }
}
