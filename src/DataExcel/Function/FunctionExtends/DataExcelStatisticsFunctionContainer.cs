using Feng.Excel.Actions;
using Feng.Excel.Builder;
using Feng.Excel.Collections;
using Feng.Excel.Forms;
using Feng.Excel.Interfaces;
using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;

namespace Feng.Excel.Script
{ 
    public class DataExcelStatisticsFunctionContainer : DataExcelMethodContainer
    {

        public const string Function_Name = "DataExcelStatistics";
        public const string Function_Description = "表格统计函数";
        public override string Name
        {
            get { return Function_Name; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }

        public DataExcelStatisticsFunctionContainer()
        {
            BaseMethod model = null;
 
            model = new BaseMethod();
            model.Name = "GSGroup";
            model.Description = @"分组 GSGroup()";
            model.Eg = @"var datatable=GSGroup();";
            model.Function = GSGroup;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "GSSelectRowEqualText";
            model.Description = @"选择等相行 GSSelectRowEqualText(""A,B,C,D"",""3:100"",""Equalkeycolumn"",""Equalvalue"")";
            model.Eg = @"var datatable=GSSelectRowEqualText(""A,B,C,D"",""3:100"",""Equalkeycolumn"",""Equalvalue"");";
            model.Function = GSSelectRowEqualText;
            MethodList.Add(model);

        }

        public class GroupColumn
        {
            public string Name { get; set; }
            public List<int> Columns { get; set; }
        }
        public class CalcColumn
        {
            public string Name { get; set; }
            public int Column { get; set; }
            public bool MIN { get; set; }
            public bool MAX { get; set; }
            public bool AVG { get; set; }
            public bool SUM { get; set; }
            public bool COUNT { get; set; }
        }
        public virtual object GSGroup(params object[] args)
        {
            //GSGroup(CELLS("A2:H100"),"2:NAME,3:NAME,4&5&G&H&I&COLID&COLID","G:SUM&AVG&MIN,H:SUM&COUNT,",FIRSTROW IS TITLE,PROCNAME);
            //ID，COLUMN1,COLUMN2,COLUMN4_5_G_H_I,MIN,MAX,COUNT,AVG,SUM
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                DataTable table = new DataTable();
                string cellstext = base.GetTextValue(1, args);
                string groupcolumnstext = base.GetTextValue(2, args);
                string calccolumnstext = base.GetTextValue(3, args);
                string titletext = base.GetTextValue(4, args);
                string procnametext = base.GetTextValue(5, args);
                SelectCellCollection cells = Feng.Excel.Utils.DataExcelTools.GetSelectCell(proxy.Grid, cellstext);
                if (cells == null)
                    throw new Exception("参数[" + cellstext + "] 不正确 ");
                List<GroupColumn> groupcolumns = new List<GroupColumn>();
                List<CalcColumn> claccolumns = new List<CalcColumn>();
                int minrow = 0;
                int maxrow = 0;
                int mincolumn = 0;
                int maxcolumn = 0;
                minrow = cells.MinRow();
                maxrow = cells.MaxRow();
                mincolumn = cells.MinColumn();
                maxcolumn = cells.MaxColumn();
 
                DataExcel dataExcel = proxy.Grid;
 
                for (int i = minrow; i <= maxrow; i++)
                {
                    IRow row = dataExcel.Rows[i];
                    if (row == null)
                    {
                        continue;
                    }
                    GetRow(table, row, groupcolumns, claccolumns); 
                }
                return table;
            }
            return null;
        }
        public void GetRow(DataTable table, List<GroupColumn> groupcolumns, List<CalcColumn> claccolumns)
        {  
            foreach (GroupColumn item in groupcolumns)
            {
                table.Columns.Add(item.Name); 
            }
            foreach (CalcColumn item in claccolumns)
            {
                table.Columns.Add(item.Name);
            }  
        }
        public void GetRow(DataTable table,IRow row, List<GroupColumn> groupcolumns, List<CalcColumn> claccolumns)
        {
            List<GroupColumn> groupcolumnstemp = new List<GroupColumn>();
            DataRow dataRow = table.NewRow();
            foreach (GroupColumn item in groupcolumns)
            {
                if (item.Columns.Count > 0)
                {
                    ICell cell = row[item.Columns[0]];
                    dataRow[item.Name] = cell.Value;
                }
                if (item.Columns.Count > 1)
                {
                    GroupColumn groupcolumn = new GroupColumn()
                    {
                        Name = item.Name,
                        Columns = new List<int>()
                    };
                    groupcolumn.Columns.AddRange(item.Columns.ToArray());
                    groupcolumn.Columns.RemoveAt(0);
                }
            }
            foreach (CalcColumn item in claccolumns)
            {
                ICell cell = row[item.Column];
                dataRow[item.Name] = cell.Value;
            }
            table.Rows.Add(dataRow);
            if (groupcolumnstemp.Count > 0)
            {
                CopyInsertGroupColumn(table, row, dataRow, groupcolumnstemp);
            } 
        }
        public void CopyInsertGroupColumn(DataTable table, IRow row, DataRow dtrow, List<GroupColumn> groupcolumns)
        {
            List<GroupColumn> groupcolumnstemp = new List<GroupColumn>();
            DataRow dataRow = table.NewRow();
            foreach (DataColumn item in table.Columns)
            {
                dataRow[item] = dtrow[item];
            }
            foreach (GroupColumn item in groupcolumns)
            {
                if (item.Columns.Count > 0)
                {
                    ICell cell = row[item.Columns[0]];
                    dataRow[item.Name] = cell.Value;
                }
                if (item.Columns.Count > 1)
                {
                    GroupColumn groupcolumn = new GroupColumn()
                    {
                        Name = item.Name,
                        Columns = new List<int>()
                    };
                    groupcolumn.Columns.AddRange(item.Columns.ToArray());
                    groupcolumn.Columns.RemoveAt(0);
                    groupcolumnstemp.Add(groupcolumn);
                }
            }
            table.Rows.Add(dataRow);
            if (groupcolumnstemp.Count > 0)
            {
                CopyInsertGroupColumn(table, row, dataRow, groupcolumnstemp);
            }
        }
        public virtual object GSSelectRowEqualText(params object[] args)
        {
            ///GSFIND("A,B,C,D","3:100","Equalkeycolumn","Equalvalue")
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                DataTable table = new DataTable();
                string inputcolumns = base.GetTextValue(1, args);
                string inputrows = base.GetTextValue(2, args);
                string equalcolumnname = base.GetTextValue(3, args);
                string equaltext = base.GetTextValue(4, args);
                //SelectCellCollection cells = Feng.Excel.Utils.DataExcelTools.GetSelectCell(proxy.Grid, cellstext);
                //if (cells == null)
                //    throw new Exception("参数["+ cellstext + "] 不正确 ");

                List<int> columns = new List<int>();
                int minrow = 0;
                int maxrow = 0;
                string[] inputcolumn = inputcolumns.Split(',');
                for (int i = 0; i < inputcolumn.Length; i++)
                {
                    columns.Add(DataExcel.GetColumnIndexByColumnHeader(inputcolumn[i]));
                }
                string[] inputrow = inputrows.Split(':');
                if (inputrow.Length!=2)
                {
                    throw new Exception("参数[" + inputrows + "] 不正确 如 3:100  ");
                }
                minrow = Feng.Utils.ConvertHelper.ToInt(inputrow[0]);
                maxrow = Feng.Utils.ConvertHelper.ToInt(inputrow[1]);
                int keycolumn = DataExcel.GetColumnIndexByColumnHeader(equalcolumnname);
                string text = equaltext;
                DataExcel dataExcel = proxy.Grid;
                foreach (int item in columns)
                {
                    table.Columns.Add(dataExcel.Columns[item].Name);
                }
 
                for (int i = minrow; i <= maxrow; i++)
                {
                    IRow row = dataExcel.Rows[i];
                    if (row == null)
                    {
                        continue;
                    }
                    ICell cell = row[keycolumn];
                    if (cell == null)
                    {
                        continue;
                    }
                    if (!text.Equals(cell.Text.Trim()))
                    {
                        continue;
                    }
                    DataRow dataRow = table.NewRow();

                    string columnname = dataExcel.Columns[keycolumn].Name;
           
                    foreach (int column in columns)
                    {
                        columnname = dataExcel.Columns[column].Name;
                        cell = row[column];
                        dataRow[columnname] = cell.Value;
                    }
                    table.Rows.Add(dataRow);
                }
                return table;
            }
            return null;
        }
 
 
    }
}
