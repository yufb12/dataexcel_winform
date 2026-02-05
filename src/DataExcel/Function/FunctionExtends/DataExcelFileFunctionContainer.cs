//using Feng.Excel.Base;
//using Feng.Excel.Builder;
//using Feng.Excel.Interfaces;
//using Feng.Script.CBEexpress;
//using Feng.Script.Method;
//using System;
//using System.Collections.Generic;
//using System.Data;

//namespace Feng.Excel.Script
//{
//    [Serializable]
//    public class DataExcelFileFunctionContainer : DataExcelMethodContainer
//    {
//        public const string Function_Category = "DataExcelFile";
//        public const string Function_Description = "DataExcel统计函数";
//        public override string Name
//        {
//            get { return Function_Category; }

//        }
//        public override string Description
//        {
//            get { return Function_Description; }
//        }

//        public DataExcelFileFunctionContainer()
//        {

//            BaseMethod model = new BaseMethod();
//            model.Name = "DataExcelFile_FileRunExpress";
//            model.Description = "DataExcelFile_FileRunExpress";
//            model.Eg = @"CellValue(""B2"",DataExcelFile_FileRunExpress(""I:\Test\Project\A.fexm"",""CellValue(""""B2""))"")";
//            model.Function = DataExcelFile_FileRunExpress;
//            MethodList.Add(model);

//            model = new BaseMethod();
//            model.Name = "DataExcelFile_FileRunScript";
//            model.Description = "DataExcelFile_FileRunScript";
//            model.Eg = @"DataExcelFile_FileRunScript(""I:\Test\Project\A.fexm"",""CellValue(""""B2""),""START"",CELL(""B2""))";
//            model.Function = DataExcelFile_FileRunScript;
//            MethodList.Add(model);

//            model = new BaseMethod();
//            model.Name = "DataExcelFile_DirectoryRunScript";
//            model.Description = "DataExcelFile_DirectoryRunScript";
//            model.Eg = @"DataExcelFile_DirectoryRunScript(""I:\Test\Project\A.fexm"",""CellValue(""""B2""),""START"",CELL(""B2""))";
//            model.Function = DataExcelFile_DirectoryRunScript;
//            MethodList.Add(model);


//            model = new BaseMethod();
//            model.Name = "DataExcelFile_Fill";
//            model.Description = "DataExcelFile_Fill";
//            model.Function = DataExcelFile_Fill;
//            MethodList.Add(model);
//        }

//        public virtual object DataExcelFile_FileRunExpress(params object[] args)
//        {
//            ICBContext proxy = args[0] as ICBContext;
//            if (proxy == null)
//            {
//                return null;
//            }
//            object value1 = base.GetArgIndex(1, args);
//            string file = Feng.Utils.ConvertHelper.ToString(value1);
//            if (System.IO.File.Exists(file))
//            {
//                DataExcel grid = new DataExcel();
//                grid.Init();
//                grid.Open(file);
//                string express = base.GetTextValue(2, args);
//                ScriptBuilder.Exec(grid, grid.FocusedCell, express);
//                return Feng.Utils.Constants.OK;
//            }

//            return null;
//        }

//        public virtual object DataExcelFile_FileRunScript(params object[] args)
//        {
//            ICBContext proxy = args[0] as ICBContext;
//            if (proxy == null)
//            {
//                return null;
//            }
//            object value1 = base.GetArgIndex(1, args);
//            string file = Feng.Utils.ConvertHelper.ToString(value1);
//            if (System.IO.File.Exists(file))
//            {
//                DataExcel grid = new DataExcel();
//                grid.Init();
//                grid.Open(file);
//                string express = base.GetTextValue(2, args);
//                int len = args.Length - 3;
//                if (len > 0)
//                {
//                    object[] values = new object[len];
//                    for (int i = 0; i < len; i++)
//                    {
//                        values[i] = args[i + 2];
//                    }
//                    ScriptBuilder.Exec(grid, grid.FocusedCell, express, values);
//                    return Feng.Utils.Constants.OK;
//                }

//                ScriptBuilder.Exec(grid, grid.FocusedCell, express);
//                return Feng.Utils.Constants.OK;
//            }

//            return null;
//        }

//        public virtual object DataExcelFile_DirectoryRunScript(params object[] args)
//        {
//            ICBContext proxy = args[0] as ICBContext;
//            if (proxy == null)
//            {
//                return null;
//            }
//            string directory = base.GetTextValue(1, args);
//            if (System.IO.Directory.Exists(directory))
//            {
//                string[] files = System.IO.Directory.GetFiles(directory);
//                foreach (string file in files)
//                {
//                    DataExcel grid = new DataExcel();
//                    grid.Init();
//                    grid.Open(file);
//                    string express = base.GetTextValue(2, args);
//                    int len = args.Length - 3;
//                    if (len > 0)
//                    {
//                        object[] values = new object[len];
//                        for (int i = 0; i < len; i++)
//                        {
//                            values[i] = args[i + 2];
//                        }
//                        ScriptBuilder.Exec(grid, grid.FocusedCell, express, values);
//                    }
//                    else
//                    {
//                        ScriptBuilder.Exec(grid, grid.FocusedCell, express);
//                    }

//                }
//            }
//            return Feng.Utils.Constants.OK;
//        }


//        public virtual object FillFileValue(params object[] args)
//        {
//            //ICBContext proxy = args[0] as ICBContext;
//            //if (proxy == null)
//            //{
//            //    return null;
//            //}
//            //object value1 = base.GetArgIndex(1, args);

//            //string url = Feng.Utils.ConvertHelper.ToString(value1);
//            //if (url.Contains("?"))
//            //{
//            //    string[] temps = url.Split('?');

//            //    string[] names = temps[1].Split('&');
//            //    List<string> listname = new List<string>();
//            //    foreach (string name in names)
//            //    {
//            //        int index = name.IndexOf("=");
//            //        if (index > 0)
//            //        {
//            //            string str = name.Substring(0, index);
//            //            listname.Add(str);
//            //        }
//            //    }
//            //    Dictionary<string, string> dics = new Dictionary<string, string>();
//            //    for (int i = 0; i < listname.Count; i++)
//            //    {
//            //        if (args.Length > i + 2)
//            //        {
//            //            dics.Add(listname[i],
//            //                Feng.Utils.ConvertHelper.ToString(
//            //               base.GetArgIndex(i + 2, args)));
//            //        }
//            //    }
//            //    return Feng.Net.Http.WebService.GetWebServiceValueByPost(temps[0], dics);
//            //}
//            //return Feng.Net.Http.WebService.GetWebServiceValueByPost(url);

//            return null;
//        }

//        public virtual object DataExcelFile_Fill(params object[] args)
//        {
//            ICBContext proxy = args[0] as ICBContext;
//            if (proxy == null)
//            {
//                return null;
//            }

//            ICell cell = proxy.CurrentCell;
//            if (cell.OwnMergeCell != null)
//            {
//                cell = cell.OwnMergeCell;
//            }
//            try
//            {
//                string sql = @"SELECT  *
//  FROM  [dbo].[SysInfo]";
//                DataTable table = Feng.Data.MsSQL.DbHelperSQL.QueryDataTable(sql);
//                int columnindex = cell.MaxColumnIndex;
//                int rowindex = cell.MaxRowIndex;
//                List<IRow> delrows = new List<IRow>();
//                foreach (IRow irow in cell.Grid.Rows)
//                {
//                    if (irow.Tag != null && irow.Tag.Equals("TableName"))
//                    {
//                        delrows.Add(irow);
//                    }
//                }
//                foreach (IRow irow in delrows)
//                {
//                    cell.Grid.Rows.Remove(irow);
//                }
//                delrows.Clear();
//                Row row = new Row(cell.Grid, rowindex + 1);
//                row.Tag = "TableName";
//                cell.Grid.Rows.Insert(rowindex + 1, row);
//                int begincolindex = columnindex - (table.Columns.Count / 2);
//                if (begincolindex < 1)
//                {
//                    begincolindex = 1;
//                }
//                for (int i = 0; i < table.Columns.Count; i++)
//                {
//                    DataColumn datacol = table.Columns[i];
//                    ICell rowcell = row[begincolindex + i];
//                    if (rowcell == null)
//                    {
//                        rowcell = new Cell(cell.Grid, row.Index, begincolindex + i);
//                        row.Cells.Add(rowcell);
//                    }
//                    rowcell.Value = datacol.ColumnName;
//                }
//                for (int i = 0; i < table.Rows.Count; i++)
//                {
//                    rowindex = cell.MaxRowIndex + 1;
//                    row = new Row(cell.Grid, rowindex + 1);
//                    row.Tag = "TableName";
//                    cell.Grid.Rows.Insert(rowindex + 1, row);

//                    for (int j = 0; j < table.Columns.Count; j++)
//                    {
//                        ICell rowcell = row[begincolindex + j];
//                        if (rowcell == null)
//                        {
//                            rowcell = new Cell(cell.Grid, row.Index, begincolindex + j);
//                            row.Cells.Add(rowcell);
//                        }
//                        row[begincolindex + j].Value = table.Rows[i][j];
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                cell.Text = "error:" + ex.Message;
//            }
//            return null;

//        }

//    }
//}
