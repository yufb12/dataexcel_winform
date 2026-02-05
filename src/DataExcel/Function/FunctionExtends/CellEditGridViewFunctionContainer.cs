using Feng.Excel.Collections;
using Feng.Excel.Edits;
using Feng.Excel.Interfaces;
using Feng.Forms.Controls.GridControl;
using Feng.Script.CBEexpress;
using Feng.Script.Method;
using Feng.Utils;
using System;
using System.Data;

namespace Feng.Excel.Script
{
    [Serializable]
    public class CellEditGridViewFunctionContainer : DataExcelMethodContainer
    {

        public const string Function_Category = "CellEditGridView";
        public const string Function_Description = "表格控件";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }

        public CellEditGridViewFunctionContainer()
        {
            BaseMethod model = null;


            model = new BaseMethod();
            model.Name = "GridViewNew";
            model.Description = @"创建单元格控件 GridViewNew(""CELLID"")";
            model.Eg = @"GridViewNew(tablename,datatable)";
            model.Function = GridViewNew;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridView";
            model.Description = @"获取单元格控件 GridView(""CELLID"")";
            model.Eg = @"GridView(""CELLID"")";
            model.Function = GridView;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridViewDataSource";
            model.Description = @"设置数据源 GridViewDataSource(""CELLID"",DATATABLE)";
            model.Eg = @"GridViewDataSource(""CELLID"",DATATABLE)";
            model.Function = GridViewDataSource;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "GridViewBingdingItem";
            model.Description = @"获取行的绑定值 GridViewBingdingItem(""CELLID"",12)";
            model.Eg = @"GridViewBingdingItem(""CELLID"",12)";
            model.Function = GridViewBingdingItem;
            MethodList.Add(model);
 
            model = new BaseMethod();
            model.Name = "GridViewValue";
            model.Description = @"获取或设置单元格的值 GridViewValue(""CELLID"",12,""ColumnName"")";
            model.Eg = @"GridViewValue(""CELLID"",12,""ColumnName"")";
            model.Function = GridViewValue;
            MethodList.Add(model);
 
            model = new BaseMethod();
            model.Name = "GridViewFocusedValue";
            model.Description = @"获取焦点单元格的值 GridViewFocusedValue(""CELLID"")";
            model.Eg = @"GridViewFocusedValue(""CELLID"")";
            model.Function = GridViewFocusedValue;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridViewFocusedRow";
            model.Description = @"获取焦点单元格的行索引 GridViewFocusedRow(""CELLID"")";
            model.Eg = @"GridViewFocusedRow(""CELLID"")";
            model.Function = GridViewFocusedRow;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridViewFocusedColumn";
            model.Description = @"获取焦点单元格的列索引 GridViewFocusedColumn(""CELLID"")";
            model.Eg = @"GridViewFocusedColumn(""CELLID"")";
            model.Function = GridViewFocusedColumn;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridViewCellRow";
            model.Description = @"获取单元格的行索引 GridViewCellRow(cell)";
            model.Eg = @"GridViewCellRow(cell)";
            model.Function = GridViewCellRow;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridViewCellColumn";
            model.Description = @"获取单元格的列索引 GridViewCellColumn(cell)";
            model.Eg = @"GridViewCellColumn(cell)";
            model.Function = GridViewCellColumn;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridViewRowCount";
            model.Description = @"表格行数量 GridViewRowCount(cell)";
            model.Eg = @"GridViewRowCount(cell)";
            model.Function = GridViewRowCount;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "GridViewColumnCount";
            model.Description = @"表格列数量 GridViewColumnCount(cell)";
            model.Eg = @"GridViewColumnCount(cell)";
            model.Function = GridViewColumnCount;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GridViewColumnVisible";
            model.Description = @"获取或设置列是否可见 GridViewColumnVisible(cell)";
            model.Eg = @"GridViewColumnVisible(cell)";
            model.Function = GridViewColumnVisible;
            MethodList.Add(model);
        }
        public virtual object GridViewFocusedColumn(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
                return null;
            CellGridView cellGridView = null;
            cellGridView = base.GetArgIndex(1, args) as CellGridView;
            if (cellGridView == null)
            {
                ICell cell = FunctionTools.GetCell(proxy, GetArgIndex(1, args));
                if (cell == null)
                    return null;
                cellGridView = cell.OwnEditControl as CellGridView;
            }
            object value = cellGridView.FocusedCell.Column.Index;
            return value;
        }
        public virtual object GridViewFocusedRow(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
                return null;
            CellGridView cellGridView = null;
            cellGridView = base.GetArgIndex(1, args) as CellGridView;
            if (cellGridView == null)
            {
                ICell cell = FunctionTools.GetCell(proxy, GetArgIndex(1, args));
                if (cell == null)
                    return null;
                cellGridView = cell.OwnEditControl as CellGridView;
            }
            object value = cellGridView.FocusedCell.Row.Index;
            return value;
        }
        public virtual object GridViewBingdingItem(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
                return null;
            CellGridView cellGridView = null;
            cellGridView = base.GetArgIndex(1, args) as CellGridView;
            if (cellGridView == null)
            {
                ICell cell = FunctionTools.GetCell(proxy, GetArgIndex(1, args));
                if (cell == null)
                    return null;
                cellGridView = cell.OwnEditControl as CellGridView;
            }
            int rowindex = base.GetIntValue(2, -1);
            if (rowindex < 0)
            {
                return null;
            }
            
            return cellGridView.GetDataBingdingItem(rowindex);
        }
        public virtual object GridViewValue(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
                return null;
            CellGridView cellGridView = null;
            cellGridView = base.GetArgIndex(1, args) as CellGridView;
            if (cellGridView == null)
            {
                ICell cell = FunctionTools.GetCell(proxy, GetArgIndex(1, args));
                if (cell == null)
                    return null;
                cellGridView = cell.OwnEditControl as CellGridView;
            }
            Feng.Forms.Controls.GridControl.GridViewCell gridviewcell = null;
            int rowindex = base.GetIntValue(2, -1, args);
            if (rowindex<0)
            {
                return null;
            }
            int columnindex = base.GetIntValue(3, -1);
            if (args.Length > 4)
            {
                object value = base.GetArgIndex(4, args);
                string columnname = base.GetTextValue(3, args);
                cellGridView.SetDataBingdingValue(rowindex - 1, columnname,value);
                return Feng.Utils.Constants.OK;
            }
 
            if (columnindex > 0)
            {
                return cellGridView.GetDataBingdingValue(rowindex-1, columnindex);
            }
            else
            {
                string columnname = base.GetTextValue(3, args);
                return cellGridView.GetDataBingdingValue(rowindex - 1, columnname);
            }

        }
        public virtual object GridViewFocusedValue(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }
            CellGridView cellGridView = null;
            cellGridView = base.GetArgIndex(1, args) as CellGridView;
            if (cellGridView == null)
            {
                ICell cell = FunctionTools.GetCell(proxy, GetArgIndex(1, args));
                if (cell == null)
                    return null;
                cellGridView = cell.OwnEditControl as CellGridView;
            }
            object value = cellGridView.FocusedCell.Value;
            return value;
        }
        public virtual object GridViewNew(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }
            ICell cell = FunctionTools.GetCell(proxy, GetArgIndex(1, args));
            if (cell == null)
                return null;
            CellGridView gridView = new CellGridView(cell);
            cell.OwnEditControl = gridView;
            return gridView;
        }
        public virtual object GridView(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }
            ICell cell = FunctionTools.GetCell(proxy, GetArgIndex(1, args));
            if (cell == null)
                return null;
            return cell.OwnEditControl;
        }
        public virtual object GridViewDataSource(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return Feng.Utils.Constants.Fail;
            }
            ICell cell = FunctionTools.GetCell(proxy, GetArgIndex(1, args));
            if (cell == null)
                return Feng.Utils.Constants.Fail;
            CellGridView gridView = cell.OwnEditControl as CellGridView;
            if (gridView == null)
                return Feng.Utils.Constants.Fail;
            DataTable dataTable = GetArgIndex(2, args) as DataTable;
            gridView.DataSource = dataTable;
            bool autocolumn = base.GetBooleanValue(2, args);
            if (autocolumn)
            {
                gridView.AutoGenerateColumns=true;

            }
            gridView.InitDataSource();
            gridView.RefreshRowValue();
            return Feng.Utils.Constants.OK;
        }

        public virtual object GridViewCellRow(params object[] args)
        {
            GridViewCell gridviewcell = base.GetArgIndex(1, args) as GridViewCell;
            if (gridviewcell != null)
            {
                return gridviewcell.Row.Index;
            } 
            return null;
        }
        public virtual object GridViewCellColumn(params object[] args)
        {
            GridViewCell gridviewcell = base.GetArgIndex(1, args) as GridViewCell;
            if (gridviewcell != null)
            {
                return gridviewcell.Column.Index;
            }
            return null;
        }

        public virtual object GridViewRowCount(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
                return null;
            CellGridView cellGridView = GetGridView(proxy, args);
            if (cellGridView != null)
            {
                return cellGridView.Rows.Count;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridViewColumnCount(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            } 
            CellGridView cellGridView = GetGridView(proxy, args);
            if (cellGridView != null)
            {
                return cellGridView.Columns.Count;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object GridViewColumnVisible(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }
            CellGridView cellGridView = GetGridView(proxy, args);

            if (cellGridView != null)
            {
                int columnindex = base.GetIntValue(2, args);
                if (args.Length > 3)
                {
                    bool visible = base.GetBooleanValue(3, args);
                    cellGridView.Columns[columnindex].Visible = visible;
                }
                else
                {
                    return cellGridView.Columns[columnindex].Visible;
                }
            }
            return Feng.Utils.Constants.Fail;
        }


        private CellGridView GetGridView(ICBContext proxy,params object[] args)
        {
            CellGridView cellGridView = base.GetArgIndex(1, args) as CellGridView;
            if (cellGridView == null)
            {
                ICell cell = FunctionTools.GetCell(proxy, GetArgIndex(1, args));
                if (cell == null)
                    return null;
                cellGridView = cell.OwnEditControl as CellGridView;
            }
            return cellGridView;
        }
    }
}
