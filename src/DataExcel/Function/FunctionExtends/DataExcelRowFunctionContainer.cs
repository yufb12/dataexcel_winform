using Feng.Excel.Builder;
using Feng.Excel.Interfaces;
using Feng.Script.CBEexpress;
using Feng.Script.Method;
using Feng.Utils;
using System;
using System.Reflection;

namespace Feng.Excel.Script
{
    [Serializable]
    public class DataExcelRowFunctionContainer : DataExcelMethodContainer
    {

        public const string Function_Name = "DataExcelRow";
        public const string Function_Description = "表格行函数";
        public override string Name
        {
            get { return Function_Name; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }

        public DataExcelRowFunctionContainer()
        {
            BaseMethod model = null;

          
            model = new BaseMethod();
            model.Name = "Row";
            model.Description = @"获取行 Row(""A5"")";
            model.Eg = @"Row(CELL(""A5""))";
            model.Function = Row;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "RowUp";
            model.Description = @"获取上一行 RowUp(me)";
            model.Eg = @"RowUp (me))";
            model.Function = RowUp;
            MethodList.Add(model);
 
            model = new BaseMethod();
            model.Name = "RowDown";
            model.Description = @"获取下一行 RowDown(me)";
            model.Eg = @"RowDown(me))";
            model.Function = RowDown;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Rows";
            model.Description = @"获取创建行集合 Rows";
            model.Eg = @"Rows()";
            model.Function = Rows;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "RowCaption";
            model.Description = @"设置行标题 RowCaption(CELL(""A5""),""Caption"")";
            model.Eg = @"RowCaption(CELL(""A5""),""Caption"")";
            model.Function = RowCaption;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "RowIndex";
            model.Description = @"获取行 RowIndex(""A5"")";
            model.Eg = @"RowIndex(CELL(""A5""))";
            model.Function = RowIndex;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "RowFind";
            model.Description = @"获取行 RowFind(""单元格A5"")";
            model.Eg = @"RowFind(""单元格A5"")";
            model.Function = RowFind;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "RowName";
            model.Description = @"获取行 RowName(""A5"")";
            model.Eg = @"RowName(CELL(""A5""))";
            model.Function = RowName;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "RowWidth";
            model.Description = @"设置行宽 RowWidth(CELL(""A5""),72)";
            model.Eg = @"RowWidth(CELL(""A5""),72)";
            model.Function = RowHeight;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "RowVisible";
            model.Description = @"设置行是否可见 RowVisible(CELL(""A5""),1)";
            model.Eg = @"RowVisible(CELL(""A5""),0)";
            model.Function = RowVisible;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "RowReadOnly";
            model.Description = @"设置行是否只读 RowReadOnly(CELL(""A5""),1)";
            model.Eg = @"RowReadOnly(CELL(""A5""),0)";
            model.Function = RowReadOnly;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "RowDelete";
            model.Description = @"删除行 RowDelete(CELL(""A5""))";
            model.Eg = @"RowDelete(CELL(""A5""))";
            model.Function = RowDelete;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "RowInsert";
            model.Description = @"插入行 RowInsert(3)";
            model.Eg = @"RowInsert(3)";
            model.Function = RowInsert;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "RowCopyInsert";
            model.Description = @"插入复制行 RowCopyInsert(CELL(""A5""))";
            model.Eg = @"RowCopyInsert(CELL(""A5""))";
            model.Function = RowCopyInsert;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "RowID";
            model.Description = @"设置行ID RowID(CELL(""A5""),""ID"")";
            model.Eg = @"RowID(CELL(""A5""),""ID"")";
            model.Function = RowID;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "RowCell";
            model.Description = @"返回单元格 RowCell(row,""ID"")";
            model.Eg = @"RowCell(row,""ID"")";
            model.Function = RowCell;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "RowCellText";
            model.Description = @"返回单元格文本 RowCellText(row,""ID"")";
            model.Eg = @"RowCellText(row,""ID"")";
            model.Function = RowCellText;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "RowCellValue";
            model.Description = @"返回单元格值 RowCellValue(row,""ID"")";
            model.Eg = @"RowCellValue(row,""ID"")";
            model.Function = RowCellValue;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "RowBoundLeft";
            model.Description = "左边距";
            model.Eg = @"RowBoundLeft(Cell(""B3""))";
            model.Function = RowBoundLeft;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "RowBoundTop";
            model.Description = "上边距";
            model.Eg = @"RowBoundTop(Cell(""B3""))";
            model.Function = RowBoundTop;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "RowBoundWidth";
            model.Description = "宽度";
            model.Eg = @"RowBoundWidth(Cell(""B3""))";
            model.Function = RowBoundWidth;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "RowBoundHeight";
            model.Description = "高度";
            model.Eg = @"RowBoundHeight(Cell(""B3""))";
            model.Function = RowBoundHeight;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "RowBounds";
            model.Description = "大小和位置";
            model.Eg = @"RowBounds(Cell(""B3""))";
            model.Function = RowBounds;
            MethodList.Add(model);
        }
        public virtual object RowBoundLeft(params object[] args)
        {
            IRow item = base.GetArgIndex(1, args) as IRow;
            if (item == null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    item = cell.Row;
                }
            }
            if (item != null)
            {
                return item.Left;
            }
            return null;
        }
        public virtual object RowBoundTop(params object[] args)
        {
            IRow item = base.GetArgIndex(1, args) as IRow;
            if (item == null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    item = cell.Row;
                }
            }
            if (item != null)
            {
                return item.Top;
            }
            return null;
        }
        public virtual object RowBoundWidth(params object[] args)
        {
            IRow item = base.GetArgIndex(1, args) as IRow;
            if (item == null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    item = cell.Row;
                }
            }
            if (item != null)
            {
                return item.Width;
            }
            return null;
        }
        public virtual object RowBoundHeight(params object[] args)
        {
            IRow item = base.GetArgIndex(1, args) as IRow;
            if (item == null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    item = cell.Row;
                }
            }
            if (item != null)
            {
                return item.Height;
            }
            return null;
        }
        public virtual object RowBounds(params object[] args)
        {
            IRow item = base.GetArgIndex(1, args) as IRow;
            if (item == null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    item = cell.Row;
                }
            }
            if (item != null)
            {
                return item.Rect;
            }
            return null;
        }

        public virtual object RowCopyInsert(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            object indexobj = RowIndex(args);
            if (indexobj != null)
            {
                int index = ConvertHelper.ToInt32(indexobj);
                IRow item = proxy.Grid.ClassFactory.CreateDefaultRow(proxy.Grid, index);
                proxy.Grid.Rows.Insert(index, item);
                return item;
            }
            return null;
        }
        public virtual object RowInsert(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            object indexobj = RowIndex(args);
            if (indexobj != null)
            {
                int index = ConvertHelper.ToInt32(indexobj);
                IRow item = proxy.Grid.ClassFactory.CreateDefaultRow(proxy.Grid, index);
                proxy.Grid.Rows.Insert(index, item);
                return item;
            }
            return null;
        }
        public virtual object RowDelete(params object[] args)
        {
            IRow item = base.GetArgIndex(1,args) as IRow;

            if (item != null)
            {
                item.Grid.Rows.Remove(item);
            }
            return null;
        }
        public virtual object RowID(params object[] args)
        {
            IRow item = base.GetArgIndex(1,args) as IRow;
            if (item == null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    item = cell.Row;
                }
            }
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
        public virtual object RowReadOnly(params object[] args)
        {
            IRow item = base.GetArgIndex(1,args) as IRow;
            if (item == null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    item = cell.Row;
                }
            }
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
        public virtual object RowVisible(params object[] args)
        {
            IRow item = base.GetArgIndex(1,args) as IRow;
            if (item == null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    item = cell.Row;
                }
            }
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
        public virtual object RowHeight(params object[] args)
        {
            IRow item = base.GetArgIndex(1,args) as IRow;
            if (item == null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    item = cell.Row;
                }
            }
            if (args.Length == 3)
            { 
                int value = base.GetIntValue(2, args);
                item.Height = value;
                return Feng.Utils.Constants.OK;
            }
            if (item != null)
            {
                return item.Height;
            }
            return null;
        }
        public virtual object RowName(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                IRow item = base.GetArgIndex(1,args) as IRow;
                if (item == null)
                {
                    ICell cell = base.GetCell(1, args);
                    if (cell != null)
                    {
                        item = cell.Row;
                    }
                }
                if (item != null)
                {
                    return item.Name;
                } 
            }
            return null;
        }
        public virtual object RowCaption(params object[] args)
        {
            IRow item = base.GetArgIndex(1,args) as IRow;
            if (item == null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell != null)
                {
                    item = cell.Row;
                }
            }
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
        public virtual object RowIndex(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            { 
                IRow item = base.GetArgIndex(1,args) as IRow;
                if (item == null)
                {
                    ICell cell = base.GetCell(1, args);
                    if (cell != null)
                    {
                        item = cell.Row;
                    }
                    else
                    {
                        if (proxy.CurrentCell != null)
                        {
                            item = proxy.CurrentCell.Row;
                        }
                    }
                }
                if (item != null)
                {
                    return item.Index;
                }
            }
            return null;
        }
 
        public virtual object RowFind(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            { 
                object value = base.GetArgIndex(1, args);
                string columnname= base.GetTextValue(2, args);
                bool fun = base.GetBooleanValue(3, args);
                DataExcel grid = proxy.Grid;
                foreach (IRow item in grid.Rows)
                {
                    foreach (IColumn column in grid.Columns)
                    { 
                        if (!string.IsNullOrEmpty(columnname))
                        {
                            if (!(column.ID == columnname || column.Name == columnname ||
                                column.ID.Equals(columnname) || column.Name.Equals(columnname)))
                            {
                                continue;
                            }
                        }
                        ICell cell = item.Cells[column];
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
                }
                return null;
            }
            return null;
        }
        public virtual object Row(params object[] args)
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
                    return cell.Row;
                }
                int index = base.GetIntValue(1, args);
                if (index > -1000)
                {
                    return proxy.Grid.Rows[index];
                }
                string id = base.GetTextValue(1, args);
                foreach (IRow item in proxy.Grid.Rows)
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

        public virtual object RowUp(params object[] args)
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
                    int rowindex = cell.Row.Index - 1;
                    if (rowindex > 0)
                    {
                        return proxy.Grid.Rows[rowindex];
                    }
                    return null;
                }
                string id = base.GetTextValue(1, args);
                foreach (IRow item in proxy.Grid.Rows)
                {
                    if (string.IsNullOrWhiteSpace(item.ID))
                        continue;
                    if (item.ID == id)
                    {
                        int rowindex = item.Index - 1;
                        if (rowindex > 0)
                        {
                            return proxy.Grid.Rows[rowindex];
                        }
                        return null;
                    }
                }
            }
            return null;
        }
        public virtual object RowDown(params object[] args)
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
                    int rowindex = cell.Row.Index + 1;
                    if (rowindex > 0)
                    {
                        return proxy.Grid.Rows[rowindex];
                    }
                    return null;
                }
                string id = base.GetTextValue(1, args);
                foreach (IRow item in proxy.Grid.Rows)
                {
                    if (string.IsNullOrWhiteSpace(item.ID))
                        continue;
                    if (item.ID == id)
                    {
                        int rowindex = item.Index + 1;
                        if (rowindex > 0)
                        {
                            return proxy.Grid.Rows[rowindex];
                        }
                        return null;
                    }
                }
            }
            return null;
        }
        public virtual object Rows(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            Feng.Collections.ListEx<IRow> list = new Feng.Collections.ListEx<IRow>();

            if (proxy != null)
            {
                foreach (IRow item in proxy.Grid.Rows)
                {
                    list.Add(item);
                }       
            }
            return list;
        }
        public virtual object RowCell(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                IRow row = null;
                row = base.GetArgIndex(1, args) as IRow;
                if (row == null)
                {
                    ICell cell = base.GetCell(1, args);
                    if (cell != null)
                    {
                        row = cell.Row;
                    }
                }
                if (row == null)
                {
                    return null;
                }
                object obj= base.GetArgIndex(2, args);
                IColumn column = obj as IColumn;
                if (column != null)
                {
                    return row[column];
                }
                string id = base.GetTextValue(2, args);
                if (!string.IsNullOrWhiteSpace(id))
                {
                    foreach (IColumn item in proxy.Grid.Columns)
                    {
                        string columnid = item.ID;
                        if (string.IsNullOrWhiteSpace(columnid))
                        {
                            columnid = item.Name;
                        }
                        if (item.ID == id)
                        {
                            return row[item];
                        }
                    }
                }

                int index = base.GetIntValue(2, args);
                if (index > 0)
                {
                    return row[index];
                }
                return null;
            }
            return null;
        }
        public virtual object RowCellText(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                IRow row = null;
                row = base.GetArgIndex(1, args) as IRow;
                if (row == null)
                {
                    ICell cell = base.GetCell(1, args);
                    if (cell != null)
                    {
                        row = cell.Row;
                    }
                }
                if (row == null)
                {
                    return null;
                }
 
                string id = base.GetTextValue(2, args);
                if (!string.IsNullOrWhiteSpace(id))
                {
                    foreach (IColumn item in proxy.Grid.Columns)
                    {
                        if (string.IsNullOrWhiteSpace(item.ID))
                            continue;
                        if (item.ID == id)
                        {
                            return row[item].Text;
                        }
                    }
                }
                else
                { 
                    int index = base.GetIntValue(1, args);
                    if (index > 0)
                    {
                        return row[index].Text;
                    }
                } 
            }
            return null;
        }
        public virtual object RowCellValue(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            object res = null;
            if (proxy != null)
            {
                IRow row = null;
                row = base.GetArgIndex(1, args) as IRow;
                if (row == null)
                {
                    ICell cell = base.GetCell(1, args);
                    if (cell != null)
                    {
                        row = cell.Row;
                    }
                }
                if (row == null)
                {
                    res = null;
                }
                string id = base.GetTextValue(2, args);
                if (!string.IsNullOrWhiteSpace(id))
                {
                    foreach (IColumn item in proxy.Grid.Columns)
                    {
                        if (string.IsNullOrWhiteSpace(item.ID))
                            continue;
                        if (item.ID == id)
                        {
                            if (args.Length == 4)
                            {
                                row[item].Value = base.GetArgIndex(3, args);
                            }
                            else
                            {
                                res = row[item].Value;
                                return res;
                            }
         
                        }
                    }
                }

                int index = base.GetIntValue(1, args);
                if (index > 0)
                {
                    if (args.Length == 4)
                    {
                        row[index].Value = base.GetArgIndex(3, args);
                    }
                    else
                    {
                        res = row[index].Value;
                        return res;
                    }
                }
                return res ;
            }
            return res ;
        }
 
    }
}
