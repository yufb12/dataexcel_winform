using Feng.Collections;
using Feng.Script.CBEexpress;
using Feng.Script.Method;
using Feng.Utils.Script.CBScript.Forms;
using System;
using System.Collections.Generic;
using System.Data;

namespace Feng.Script.FunctionContainer
{
    [Serializable]
    public class DataTableFunctionContainer : CBMethodContainer
    {

        public const string Function_Category = "DataTableFunction";
        public const string Function_Description = "内存数据集";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }
        public DataTableFunctionContainer()
        {

            BaseMethod model = null;

            model = new BaseMethod();
            model.Name = "DataTableNew";
            model.Description = @"创建一个数据表 VAR DataTable =DataTableNew(""ColumnA"",""ColumnB"")";
            model.Eg = @"VAR DataTable =DataTableNew(""ColumnA"",""ColumnB"")";
            model.Function = DataTableNew;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "DataTableRowCount";
            model.Description = "返回数据表行数量 DataTableRowCount(datatable)";
            model.Eg = @"DataTableRowCount(datatable)";
            model.Function = DataTableRowCount;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "DataTableColumnCount";
            model.Description = "返回数据表列数量 DataTableColumnCount(datatable)";
            model.Eg = @"DataTableColumnCount(datatable)";
            model.Function = DataTableColumnCount;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DataTableValue";
            model.Description = "返回数据表列数量 DataTableValue(datatable,i,j)";
            model.Eg = @"DataTableValue(datatable,i,j)";
            model.Function = DataTableValue;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "DataTableAddColumn";
            model.Description = @"将数据添加至数据表 DataTableAddColumn(datatable,""columnname"")";
            model.Eg = @"DataTableAddColumn(datatable,""columnname"")";
            model.Function = DataTableAddColumn;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DataTableAdd";
            model.Description = "将数据添加至数据表 DataTableAdd(datatable,column1,column2)";
            model.Eg = @"DataTableAdd(datatable,column1,column2)";
            model.Function = DataTableAdd;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "DataTableRemove";
            model.Description = @"将数据从数据表中删除 DataTableRemove(datatable,""ColumnA"",5)";
            model.Eg = @"DataTableRemove(datatable,""ColumnA"",5)";
            model.Function = DataTableRemove;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DataTableRowFirst";
            model.Description = "返回数据表的第一个值 DataTableRowFirst(datatable)";
            model.Eg = @"DataTableRowFirst(datatable)";
            model.Function = DataTableRowFirst;
            MethodList.Add(model);
            
            model = new BaseMethod();
            model.Name = "DataTableIndex";
            model.Description = @"返回或设置数据表的第某行某列值 DataTableIndex(datatable,3,3)";
            model.Eg = @"DataTableIndex(datatable,3,3)";
            model.Function = DataTableIndex;
            MethodList.Add(model);
             

            model = new BaseMethod();
            model.Name = "DataTableClear";
            model.Description = "删除所有数据";
            model.Eg = @"DataTableClear(datatable )";
            model.Function = DataTableClear;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "DataTableCopy";
            model.Description = "复制一个数据表 DataTableCopy(datatable)";
            model.Eg = @"DataTableCopy(datatable)";
            model.Function = DataTableCopy;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "DataTableMerge";
            model.Description = "将第二个表合并到第一个 DataTableMerge(datatable1,datatable2)";
            model.Eg = @"DataTableMerge(datatable1,datatable2)";
            model.Function = DataTableMerge;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DataTableFilter";
            model.Description = @"返回指定行的表格 DataTableFilter(datatable,""columnname"",""1001"")";
            model.Eg = @"DataTableFilter(datatable,""columnname"",""1001"")";
            model.Function = DataTableFilter;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DataTableSelect";
            model.Description = @"返回指定行的表格 DataTableSelect(datatable,""sex = '男' and age >= 18"")";
            model.Eg = @"DataTableSelect(datatable,""sex = '男' and age >= 18"")";
            model.Function = DataTableSelect;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DataTableCompute";
            model.Description = @"返回指定行的表格 DataTableCompute(datatable,""max(value)"", """")";
            model.Eg = @"DataTableCompute(datatable,""max(value)"", """")";
            model.Function = DataTableCompute;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DataTableRow";
            model.Description = "返回数据表的某行 DataTableRow(datatable,2)";
            model.Eg = @"DataTableRow(datatable,2)";
            model.Function = DataTableRow;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DataTableColumn";
            model.Description = "返回数据表的某列 DataTableColumn(datatable,2)";
            model.Eg = @"DataTableColumn(datatable,2)";
            model.Function = DataTableColumn;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DataTableRowFind";
            model.Description = @"返回数据表的某行 DataTableRowFind(datatable,""columnname"",""1001"")";
            model.Eg = @"DataTableRowFind(datatable,""columnname"",""1001"")";
            model.Function = DataTableRowFind;
            MethodList.Add(model);
 
            model = new BaseMethod();
            model.Name = "DataTableRowRemove";
            model.Description = "删除数据表的某行 DataTableRow(DataTable,DataTableRow(datatable,2))";
            model.Eg = @"DataTableRowRemove(datatable,2)";
            model.Function = DataTableRowRemove;
            MethodList.Add(model);
 
            model = new BaseMethod();
            model.Name = "DataTableRowValue";
            model.Description = @"返回数据表某行列的值 DataTableRowValue(row,""column"")";
            model.Eg = @"DataTableRowValue(row,""column"")";
            model.Function = DataTableRowValue;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DataTableRows";
            model.Description = @"返回数据表行的集合 DataTableRows(datatable)";
            model.Eg = @"DataTableRows(datatable)";
            model.Function = DataTableRows;
            MethodList.Add(model);
 
            model = new BaseMethod();
            model.Name = "DataTableColumns";
            model.Description = @"返回数据表列的集合 DataTableColumns(datatable)";
            model.Eg = @"DataTableColumns(datatable)";
            model.Function = DataTableColumns;
            MethodList.Add(model);
 
            model = new BaseMethod();
            model.Name = "DataTableColumnName";
            model.Description = @"返回数据表列的名称 DataTableColumnName(column)";
            model.Eg = @"DataTableColumnName(column)";
            model.Function = DataTableColumnName;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DataTableHeaderValue";
            model.Description = @"返回数据表第一行，第一列的值 DataTableHeaderValue(datatable)";
            model.Eg = @"DataTableHeaderValue(datatable)";
            model.Function = DataTableHeaderValue;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DataTableToJson";
            model.Description = @"数据表转Json DataTableToJson(datatable)";
            model.Eg = @"DataTableToJson(datatable)";
            model.Function = DataTableToJson;
            MethodList.Add(model);
 
            model = new BaseMethod();
            model.Name = "DataTableFromJson";
            model.Description = @"Json转数据表 DataTableFromJson(json)";
            model.Eg = @"DataTableFromJson(json)";
            model.Function = DataTableFromJson;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DataTableShow";
            model.Description = @"显示预览数据 DataTableShow(table)";
            model.Eg = @"DataTableShow(table)";
            model.Function = DataTableShow;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DataTableTest";
            model.Description = @"获取测试表格 DataTableTest()";
            model.Eg = @"var table=DataTableTest()";
            model.Function = DataTableTest;
            MethodList.Add(model);
        }
        public virtual object DataTableNew(params object[] args)
        {
            DataTable list = new DataTable();
            for (int i = 1; i < args.Length; i++)
            {
                string text = base.GetTextValue(i, args);
                list.Columns.Add(text);
            }
            return list;
        }
        public virtual object DataTableRowCount(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            return list.Rows.Count;
        }
        public virtual object DataTableColumnCount(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            return list.Columns.Count;
        }
        public virtual object DataTableValue(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            int rowindex = base.GetIntValue(2, args);
            int columnindex = base.GetIntValue(3, args);
            DataRow row = list.Rows[rowindex];
            return row[columnindex];
        }
       
        public virtual object DataTableClear(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            list.Clear();
            return Feng.Utils.Constants.OK;
        }
        public virtual object DataTableAdd(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            DataRow row = list.NewRow();
            list.Rows.Add(row);
            for (int i = 0; i < list.Columns.Count && (i+2) < args.Length; i++)
            {
                row[i] = args[i+2];
            }
            return Feng.Utils.Constants.OK;
        }
        public virtual object DataTableAddColumn(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            string columnname = base.GetTextValue(2, args);
            return list.Columns.Add(columnname); 
        }
        public virtual object DataTableRemove(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            string columnname = base.GetTextValue(2, args);
            object value = base.GetArgIndex(3, args);
            for (int i = list.Rows.Count - 1; i >= 0; i--)
            {
                object target = list.Rows[i][columnname];
                if (value.Equals(target))
                {
                    list.Rows.Remove(list.Rows[i]);
                }
            }
            return Feng.Utils.Constants.OK;
        }
        public virtual object DataTableIndex(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            if (list == null)
                return null;
            int index = base.GetIntValue(2, args);

            string columnname = string.Empty;
            int columnindex = -1;
            if (base.GetArgIndex(3, args) is string)
            {
                columnname = base.GetTextValue(3, args);
            }
            else
            {
                columnindex = base.GetIntValue(3, args);
            }
            if (index < list.Rows.Count && index >= 0)
            {
                if (string.IsNullOrWhiteSpace(columnname))
                { 
                    if (args.Length > 4)
                    {
                        list.Rows[index][columnindex] = base.GetArgIndex(4, args);
                    }
                    else
                    {
                        return list.Rows[index][columnindex];
                    }
                }
                else
                {
                    if (args.Length > 4)
                    {
                        list.Rows[index][columnname] = base.GetArgIndex(4, args);
                    }
                    else
                    {
                        return list.Rows[index][columnname];
                    }
                }
            }
            return base.GetArgIndex(4, args);
        }
        public virtual object DataTableCopy(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            DataTable listtargget = list.Clone();
            return listtargget;
        }
        public virtual object DataTableRow(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            int index = base.GetIntValue(2, args);
            return list.Rows[index];
        }
        public virtual object DataTableColumn(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            int index = base.GetIntValue(2, args);
            return list.Columns[index];
        }

        public virtual object DataTableRowRemove(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            DataRow row = base.GetArgIndex(2, args) as DataRow;
            if (row != null)
            {
                list.Rows.Remove(row);
            }
            int beginindex = base.GetIntValue(2,-1, args);
            int endindex = base.GetIntValue(3, -1, args);
            for (int i = beginindex; i <= endindex; i++)
            {
                list.Rows.RemoveAt(i);
            }
            return list;
        }
        public virtual object DataTableRowValue(params object[] args)
        {
            DataRow row = base.GetArgIndex(1, args) as DataRow;
 
            int columnindex = base.GetIntValue(2, -1, args);
            if (columnindex >= 0)
            { 
                return row[columnindex];
            }
            else
            {
                string columnname = base.GetTextValue(2, args);
                return row[columnname];
            }
        }
        public virtual object DataTableRows(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            return list.Rows;
        }
        public virtual object DataTableRowFirst(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            if (list == null)
            {
                return null;
            }

            if (list.Rows.Count > 0)
            {
                return list.Rows[0];
            }
            return null;
        }
        public virtual object DataTableColumns(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            return list.Columns;
        }
        public virtual object DataTableColumnName(params object[] args)
        {
            DataColumn list = base.GetArgIndex(1, args) as DataColumn; 
            return list.ColumnName;
        }
        public virtual object DataTableHeaderValue(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            if (list != null)
            {
                if (list.Rows.Count > 0)
                {
                    if (list.Columns.Count > 0)
                    {
                        return list.Rows[0][0];
                    }
                }
            }
            return null;
        }    
        public virtual object DataTableToJson(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            Feng.Json.FJson fJson = new Json.FJson();
            if (list != null)
            {
                fJson.Add("TableName",list.TableName);
                Feng.Json.FJsonArray rows = new Json.FJsonArray();
                for (int i = 0; i < list.Rows.Count; i++)
                {
                    Feng.Json.FJson jsonrow = new Json.FJson();
                    DataRow row = list.Rows[i];
                    foreach (DataColumn item in list.Columns)
                    {
                        jsonrow.Add(item.ColumnName, row[item]);
                    }
                    rows.Add(jsonrow);
                }
                fJson.Add("Rows", rows);
            }
            return fJson;
        }
        public virtual object DataTableFromJson(params object[] args)
        {
            Feng.Json.FJson fJson = base.GetArgIndex(1, args) as Feng.Json.FJson;
            DataTable list = new DataTable();
            if (fJson != null)
            {
                string tablename = Feng.Utils.ConvertHelper.ToString(fJson["TableName"].Value);
                Feng.Json.FJsonArray rows = fJson["Rows"] as Feng.Json.FJsonArray;
                for (int i = 0; i < fJson.Items.Count; i++)
                {
                    Feng.Json.FJson jsonrow = rows[i] as Feng.Json.FJson;
                    if (list.Columns.Count < 1)
                    {
                        foreach (Feng.Json.FJsonItem item in jsonrow.Items)
                        {
                            if (!list.Columns.Contains(item.Key))
                            {
                                list.Columns.Add(item.Key);
                            }
                        }
                    }
                    DataRow row = list.NewRow();
                    foreach (Feng.Json.FJsonItem item in jsonrow.Items)
                    {
                        Json.FJsonBase fJsonBase = item.Value as Json.FJsonBase;
                        if (fJsonBase != null)
                        { 
                            row[item.Key] = fJsonBase.Value;
                        }
                    }
                    list.Rows.Add(row);
                }
            }
            return list;
        }

        public virtual object DataTableRowFind(params object[] args)
        {
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            string column= base.GetTextValue(2, args);
            object value = base.GetValue(3, args);
            string column2 = base.GetTextValue(4, args);
            object value2 = base.GetValue(5, args);
            for (int i = 0; i < list.Rows.Count; i++)
            {
                DataRow row = list.Rows[i];
                object rowvalue = row[column];
                Console.WriteLine(rowvalue);
                if (value.Equals(rowvalue))
                {
                    if (string.IsNullOrWhiteSpace(column2))
                    {
                        return row;
                    }
                    if (value2.Equals(rowvalue))
                    {
                        return row;
                    }
                }
            }
            return null;
        }
        public virtual object DataTableFilter(params object[] args)
        {
            DataTable dataTable = base.GetArgIndex(1, args) as DataTable;
            DataTable list = dataTable.Copy();
            string column = base.GetTextValue(2, args);
            object value = base.GetValue(3, args);
            string column2 = base.GetTextValue(4, args);
            object value2 = base.GetValue(5, args);
            for (int i = list.Rows.Count; i >=0; i--)
            {
                DataRow row = list.Rows[i];
                object rowvalue = row[column];
                if (value == rowvalue)
                {
                    if (!string.IsNullOrWhiteSpace(column2))
                    {
                        if (value2 == rowvalue)
                        {
                            continue;
                        }
                    }
                }
                list.Rows.Remove(row);
            }
            return list;
        }

        public virtual object DataTableSelect(params object[] args)
        {
            DataTable dataTable = base.GetArgIndex(1, args) as DataTable;
            DataTable list = dataTable.Copy();
            string filterExpression = base.GetTextValue(2, args);
            string sort = base.GetTextValue(3, args);
            DataRow[] rows = list.Select(filterExpression, sort);
            for (int i = list.Rows.Count-1; i > 0; i--)
            {
                bool hasrow = false;
                DataRow rowlist = list.Rows[i];
                for (int j = 0; j < rows.Length; j++)
                {
                    DataRow rowsel = rows[i];
                    if (rowlist == rowsel)
                    {
                        hasrow = true;
                        break;
                    }
                }
                if (hasrow)
                {
                    list.Rows.Remove(rowlist);
                }
            }
            return list;
        }

        public virtual object DataTableCompute(params object[] args)
        {
            DataTable dataTable = base.GetArgIndex(1, args) as DataTable;
            string expression = base.GetTextValue(2, args);
            string filter = base.GetTextValue(2, args);
            object value = dataTable.Compute(expression, filter);
            return value;
        }
        public virtual object DataTableMerge(params object[] args)
        {
            DataTable dataTable = base.GetArgIndex(1, args) as DataTable;
            DataTable dataTable2 = base.GetArgIndex(2, args) as DataTable;
            dataTable.Merge(dataTable2);
            return dataTable;
        }

        public virtual object DataTableFilte(params object[] args)
        {  
            DataTable dataTable = base.GetArgIndex(1, args) as DataTable;
            DataTable list = dataTable.Copy(); 
            for (int i = list.Rows.Count; i >= 0; i--)
            {
                DataRow row = list.Rows[i];
 
            }
            return list;
        }
        public virtual object DataTableGroup(params object[] args)
        {
#warning DataTableGroup
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            int index = base.GetIntValue(2, args);
            return list.Rows[index];
        }
        public virtual object DataTableOrder(params object[] args)
        {
#warning DataTableOrder
            DataTable list = base.GetArgIndex(1, args) as DataTable;
            int index = base.GetIntValue(2, args);
            
            return list.Rows[index];
        }

        public virtual object DataTableShow(params object[] args)
        {
            DataTable table = base.GetArgIndex(1, args) as DataTable;
            if (table == null)
                return this.Fail;
            using (FrmTable frm = new FrmTable())
            {
                frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                frm.InitDataSource(table);
                frm.ShowDialog();
            }
            return this.OK;
        }

        public virtual object DataTableTest(params object[] args)
        {
            DataTable table = new DataTable();
            for (int i = 0; i < 20; i++)
            {
                table.Columns.Add("COLUMN" + i.ToString());
            }
            for (int i = 0; i < 3000; i++)
            {
                DataRow row = table.NewRow();
                table.Rows.Add(row);
                row[0] = i;
                row[1] = "No" + (10000 + i).ToString();
                row[2] = "Node" + Feng.Utils.RandomCache.Next(10, 19)+"00";
                for (int j = 3; j < 6; j++)
                {
                    row[j] = (i % 5).ToString() + Feng.Utils.RandomCache.Next(1000000, 1000000 * 10).ToString();
                }
                row[7] = System.Guid.NewGuid().ToString();
                for (int j = 8; j < 12; j++)
                {
                    row[j] = (i % 7) + Feng.Utils.RandomCache.Next(1000000, 1000000 * 10);
                }
                for (int j = 12; j < 15; j++)
                {
                    row[j] = (i % 7) + Feng.Utils.RandomCache.Next(1000000, 1000000 * 10) * 0.1m / 1000 ;
                }
                for (int j = 15; j < 18; j++)
                {
                    row[j] = DateTime.Now.AddSeconds(Feng.Utils.RandomCache.Next(1000000, 1000000 * 10)*-1);
                }

                row[18] = "S" + Feng.Utils.RandomCache.Next(10, 15);
                row[19] = System.Guid.NewGuid().ToString()+ System.Guid.NewGuid().ToString();
            }
            return table;
        }
    }
}
