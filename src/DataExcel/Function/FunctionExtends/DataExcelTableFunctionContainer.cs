//using Feng.Excel.Collections;
//using Feng.Excel.Interfaces;
//using Feng.Script.CBEexpress;
//using Feng.Script.Method;
//using Feng.Utils;
//using System;
//using System.Data;

//namespace Feng.Excel.Script
//{
//    [Serializable]
//    public class DataExcelTableFunctionContainer : DataExcelMethodContainer
//    {

//        public const string Function_Category = "DataExcelTable";
//        public const string Function_Description = "表格函数";
//        public override string Name
//        {
//            get { return Function_Category; }

//        }
//        public override string Description
//        {
//            get { return Function_Description; }
//        }

//        public DataExcelTableFunctionContainer()
//        {
//            BaseMethod model = null;


//            model = new BaseMethod();
//            model.Name = "TableFill";
//            model.Description = @"表格填充数据 TableFill(tablename,datatable)";
//            model.Eg = @"TableFill(tablename,datatable)";
//            model.Function = TableFill;
//            MethodList.Add(model);


//            model = new BaseMethod();
//            model.Name = "TableFillSchema";
//            model.Description = @"填充表格架构 TableFillSchema(tablename,datatable)";
//            model.Eg = @"TableFillSchema(tablename,datatable)";
//            model.Function = TableFillSchema;
//            MethodList.Add(model);

//            model = new BaseMethod();
//            model.Name = "TableCellValue";
//            model.Description = @"返回表格某行某列值 TableCellValue(tablename,1,""ColumnName"")";
//            model.Eg = @"TableCellValue(tablename,1,""ColumnName"")";
//            model.Function = TableCellValue;
//            MethodList.Add(model);
//        }

//        public virtual object TableFill(params object[] args)
//        {
//            ICBContext proxy = args[0] as ICBContext;
//            if (proxy != null)
//            {
//                string tablename = base.GetTextValue(1, args);
//                DataTable datatable = base.GetArgIndex(2, args) as DataTable;
//                if (datatable == null)
//                {
//                    return Feng.Utils.Constants.Fail;
//                }
//                Table.CellDataBase cellDataBase = Table.TableTools.GetCellDataBase(proxy.Grid);
//                if (cellDataBase == null)
//                {
//                    return Feng.Utils.Constants.Fail;
//                }
//                Table.CellTable cellTable = null;
//                if (cellTable == null)
//                {
//                    return Feng.Utils.Constants.Fail;
//                }
//                for (int i = 0; i < datatable.Rows.Count && i < cellTable.Rows.Count; i++)
//                {
//                    DataRow datarow = datatable.Rows[i];
//                    Table.CellTableRow cellTableRow = cellTable.Rows[i];
//                    //foreach (Table.CellTableCell item in cellTableRow.Cells)
//                    //{
//                    //    object value = null;
//                    //    if (datatable.Columns.Contains(item.TableColumnName))
//                    //    {
//                    //        value = datarow[item.TableColumnName];
//                    //    }
//                    //    item.Value = value;
//                    //}
//                }
//                return Feng.Utils.Constants.OK;
//            }
//            return Feng.Utils.Constants.Fail;
//        }

//        public virtual object TableFillSchema(params object[] args)
//        {

//            ICBContext proxy = args[0] as ICBContext;

//            if (proxy != null)
//            {
//                SelectCellCollection selectCelles = base.GetArgIndex(1, args) as SelectCellCollection;
//                DataTable dataTable = base.GetArgIndex(2, args) as DataTable;
//                string tablename = base.GetTextValue(3, args);
//                int minrow = selectCelles.MinRow();
//                int mincolumn = selectCelles.MinColumn();
//                int maxrow = selectCelles.MaxRow();
//                int maxcolumn = selectCelles.MaxColumn();

//                int row = 0;
//                int column = 0;
//                for (int irow = minrow; irow <= maxrow; irow++)
//                {
//                    for (int icolumn = mincolumn; icolumn <= maxcolumn; icolumn++)
//                    {
//                        DataColumn dataColumn = null;
//                        string columnname = string.Empty;

//                        if (column < dataTable.Columns.Count)
//                        {
//                            dataColumn = dataTable.Columns[column];
//                            columnname = dataColumn.ColumnName;
//                        }
//                        ICell cell = proxy.Grid[irow, icolumn];
//                        if (cell.OwnMergeCell != null)
//                        {
//                            cell = cell.OwnMergeCell;
//                            icolumn = icolumn + cell.MaxColumnIndex - cell.Column.Index;
//                        }
//                        cell.TableName = tablename;
//                        cell.TableRowIndex = row;
//                        cell.TableColumnName = columnname;
//                        column = column + 1;
//                    }
//                    ICell cellmin = proxy.Grid[irow, mincolumn];
//                    if (cellmin.OwnMergeCell != null)
//                    {
//                        cellmin = cellmin.OwnMergeCell;
//                        irow = irow + cellmin.MaxRowIndex - cellmin.Row.Index;
//                    }
//                    row = row + 1;
//                    column = 0;
//                }
//            }
//            return Feng.Utils.Constants.OK;
//        }

//        public virtual object TableCellValue(params object[] args)
//        {
//            ICBContext proxy = args[0] as ICBContext;
//            if (proxy != null)
//            {
//                string tablename = base.GetTextValue(1, args);
//                int rowindex = base.GetIntValue(2, args);
//                string columnname = base.GetTextValue(3, args);
//                foreach (IRow row in proxy.Grid.Rows)
//                {
//                    foreach (IColumn column in proxy.Grid.Columns)
//                    {
//                        ICell cell = row[column];
//                        if (cell == null)
//                            continue;
//                        if (cell.OwnMergeCell != null)
//                        {
//                            cell = cell.OwnMergeCell;
//                        }
//                        if (cell.TableName == tablename)
//                        {
//                            if (cell.TableRowIndex == rowindex)
//                            {
//                                if (cell.TableColumnName.ToUpper() == columnname.ToUpper())
//                                {
//                                    return cell.Value;
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            return null;
//        }

//    }
//}
