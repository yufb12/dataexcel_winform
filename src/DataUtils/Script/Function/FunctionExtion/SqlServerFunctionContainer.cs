using Feng.Collections;
using Feng.Data;
using Feng.Script.CBEexpress;
using Feng.Script.Method;
using Feng.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Feng.Script.FunctionContainer
{
    [Serializable]
    public class SqlServerFunctionContainer : CBMethodContainer
    {
        public const string Function_Category = "SqlServerFunction";
        public const string Function_Description = "Sql Server";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }
        public static string sqlconnection = "";

        public SqlServerFunctionContainer()
        {

            BaseMethod model = null;   

            model = new BaseMethod();
            model.Name = "SqlExecute";
            model.Description = "执行脚本";
            model.Function = SqlExecute;
            MethodList.Add(model);
 
            model = new BaseMethod();
            model.Name = "SqlExecuteTran";
            model.Description = "执行事务";
            model.Function = SqlExecuteTran;
            MethodList.Add(model);
 
            model = new BaseMethod();
            model.Name = "SqlExecuteScalar";
            model.Description = "查询一个值";
            model.Function = SqlExecuteScalar;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "SqlFill";
            model.Description = "填充数据表";
            model.Description = @"SqlFill(celltext(""conection""),""select * from table where 1=1 "")";
            model.Function = SqlFill;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "SqlFillCondition";
            model.Description = "填充数据表 SqlFillCondition(select,jsonarr)";
            model.Eg = @"SqlFillCondition(select,jsonarr)";
            model.Function = SqlFillCondition;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "SqlExecArgs";
            model.Description = "执行脚本参数";
            model.Eg = @"SqlExecArgs(""insert into table(id,no)VALUES(@ARG1,@ARG2)"",123,""NO123"")";
            model.Function = SqlExecArgs;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "SqlFillArgs";
            model.Description = "执行脚本参数";
            model.Eg = @"SqlFillArgs(""SELECT ID,NO FROM table WHERE 1=1 AND ID=@ARG1 AND NO LIKE @ARG +'%' ORDER BY ID"",123,""NO123"")";
            model.Function = SqlFillArgs;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "SqlScalarArgs";
            model.Description = "获取一个值";
            model.Eg = @"SqlScalarArgs(""SELECT TOP 1 NO FROM table WHERE 1=1 AND ID=@ARG1 "",123)";
            model.Function = SqlScalarArgs;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "SqlHeaderRowArgs";
            model.Description = "获取第一行的值，保存到HASH表";
            model.Eg = @"SqlHeaderRowArgs(""SELECT ID,NO,NAME FROM table WHERE 1=1 AND ID=@ARG1 AND NO LIKE @ARG +'%' ORDER BY ID"",123,""NO123"")";
            model.Function = SqlHeaderRowArgs;
            MethodList.Add(model);
        }

        public virtual object SqlAdd(params object[] args)
        {
            //sqladd("table1","name,age,add","zhangsan",12,"南京路")
            string ConnectionString = base.GetTextValue(1, args);
            string tablename = base.GetTextValue(2, args);
            string columnstext = base.GetTextValue(3, args);
            StringBuilder sql = new StringBuilder();
            Feng.Collections.HashtableEx hashtable = new HashtableEx();
            StringBuilder sbcolumn = new StringBuilder();
            StringBuilder sbcolumnargs = new StringBuilder();
            string[] columns = columnstext.Split(',');
            int index = 4;
            foreach (string item in columns)
            {
                if (string.IsNullOrWhiteSpace(item))
                {
                    continue;
                }
                sbcolumn.Append("["+item + "],");
                sbcolumnargs.Append("@" + item + ",");
                
                object value = base.GetArgIndex(index, args);
                hashtable.Add(item, value);
                index++;
            }
            if (sbcolumn.Length < 1)
                throw new Exception("列不能空");
            sbcolumn.Length = sbcolumn.Length - 1;
            sbcolumnargs.Length = sbcolumnargs.Length - 1;
            sql.Append("insert into [" + tablename + "](" + sbcolumn.ToString() + ")value(" + sbcolumnargs.ToString() + ")");
            Feng.Data.MsSQL.SqlServerDbHelper sqlServerDbHelper = new Data.MsSQL.SqlServerDbHelper(ConnectionString);
            return sqlServerDbHelper.Query(sql.ToString(), hashtable); 
        }

        public virtual object SqlUpdateEqual(params object[] args)
        {
            //SqlUpdate("table1","name,age,add","zhangsan",12,"南京路","id=")
            string ConnectionString = base.GetTextValue(1, args);
            string tablename = base.GetTextValue(2, args);
            string columnstext = base.GetTextValue(3, args);
            StringBuilder sql = new StringBuilder();
            Feng.Collections.HashtableEx hashtable = new HashtableEx();
            StringBuilder sbcolumn = new StringBuilder();
            StringBuilder sbcolumnargs = new StringBuilder();
            string[] columns = columnstext.Split(',');
            int index = 4;
            foreach (string item in columns)
            {
                if (string.IsNullOrWhiteSpace(item))
                {
                    continue;
                }
                sbcolumn.Append("[" + item + "],");
                sbcolumnargs.Append("@" + item + ",");

                object value = base.GetArgIndex(index, args);
                hashtable.Add(item, value);
                index++;
            }
            if (sbcolumn.Length < 1)
                throw new Exception("列不能空");
            sbcolumn.Length = sbcolumn.Length - 1;
            sbcolumnargs.Length = sbcolumnargs.Length - 1;
            sql.Append("insert into [" + tablename + "](" + sbcolumn.ToString() + ")value(" + sbcolumnargs.ToString() + ")");
            Feng.Data.MsSQL.SqlServerDbHelper sqlServerDbHelper = new Data.MsSQL.SqlServerDbHelper(ConnectionString);
            return sqlServerDbHelper.Query(sql.ToString(), hashtable);
        }
 

        public virtual object SqlSelect(params object[] args)
        {
            string tablename = string.Empty;
            string varnames = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.Append("select ");
            sql.Append(varnames);
            sql.Append(" from  [" + tablename + "] Where 1=1 ");
            Feng.Collections.HashtableEx hashtableEx = null;
            Feng.Collections.HashtableEx hash = new HashtableEx();
            foreach (DictionaryEntry item in hashtableEx)
            {
                string key = Feng.Utils.ConvertHelper.ToString(item.Key);
                sql.Append(" and "+ key);
                key= key.Trim('=', '>', '<');
                string value = Feng.Utils.ConvertHelper.ToString(item.Value);
                hash.Add(key, value);
            }
 
            return null;
        }

        public virtual object SqlSelectTop1(params object[] args)
        {
            string tablename = string.Empty;
            string varnames = string.Empty;
            StringBuilder sql = new StringBuilder();
            sql.Append("select top 1 ");
            sql.Append(varnames);
            sql.Append(" from  [" + tablename + "] Where 1=1 ");
            Feng.Collections.HashtableEx hashtableEx = null;
            Feng.Collections.HashtableEx hash = new HashtableEx();
            foreach (DictionaryEntry item in hashtableEx)
            {
                string key = Feng.Utils.ConvertHelper.ToString(item.Key);
                sql.Append(" and " + key);
                key = key.Trim('=', '>', '<');
                string value = Feng.Utils.ConvertHelper.ToString(item.Value);
                hash.Add(key, value);
            }

            return null;
        }

        public virtual object SqlHeaderRowArgs(params object[] args)
        {
            string ConnectionString = base.GetTextValue(1, args);
            string sqlstring = base.GetTextValue(2, args);
            Feng.Data.MsSQL.SqlServerDbHelper sqlServerDbHelper = new Data.MsSQL.SqlServerDbHelper(ConnectionString);
            Feng.Collections.ListEx<object> list = new Feng.Collections.ListEx<object>();
            for (int i = 3; i < args.Length; i++)
            {
                object obj = base.GetArgIndex(i, args);
                list.Add(obj);
            }
            DataTable table = sqlServerDbHelper.Query(sqlstring, list);
            Feng.Collections.HashtableEx hash = new HashtableEx();
            DataRow row = null;
            if (table.Rows.Count > 0)
            {
                row = table.Rows[0];
            }
            foreach (DataColumn item in table.Columns)
            {
                if (row == null)
                {
                    hash.Add(item.ColumnName, null);
                }
                else
                {
                    hash.Add(item.ColumnName, row[item]);
                }
            }
            return hash;            
        }
        public virtual object SqlScalarArgs(params object[] args)
        {
            string ConnectionString = base.GetTextValue(1, args);
            string sqlstring = base.GetTextValue(2, args);
            Feng.Data.MsSQL.SqlServerDbHelper sqlServerDbHelper = new Data.MsSQL.SqlServerDbHelper(ConnectionString);
            Feng.Collections.ListEx<object> list = new Feng.Collections.ListEx<object>();
            for (int i = 2; i < args.Length; i++)
            {
                object obj = base.GetArgIndex(i, args);
                list.Add(obj);
            }
            return sqlServerDbHelper.GetSingle(sqlstring, list);
        }
        public virtual object SqlExecArgs(params object[] args)
        {
            string ConnectionString = base.GetTextValue(1, args);
            string sqlstring = base.GetTextValue(2, args);
            Feng.Data.MsSQL.SqlServerDbHelper sqlServerDbHelper = new Data.MsSQL.SqlServerDbHelper(ConnectionString);
            Feng.Collections.ListEx<object> list = new Feng.Collections.ListEx<object>();
            for (int i = 2; i < args.Length; i++)
            {
                object obj = base.GetArgIndex(i, args);
                list.Add(obj);
            }
            return sqlServerDbHelper.ExecuteSql(sqlstring, list);
        }
        public virtual object SqlFillArgs(params object[] args)
        {
            string ConnectionString = base.GetTextValue(1, args);
            string sqlstring = base.GetTextValue(2, args);
            Feng.Data.MsSQL.SqlServerDbHelper sqlServerDbHelper = new Data.MsSQL.SqlServerDbHelper(ConnectionString);
            Feng.Collections.ListEx<object> list = new Feng.Collections.ListEx<object>();
            for (int i = 3; i < args.Length; i++)
            {
                object obj = base.GetArgIndex(i, args);
                list.Add(obj);
            }
            return sqlServerDbHelper.Query(sqlstring, list);
        }
        public virtual object SqlFillCondition(params object[] args)
        {
            string ConnectionString = base.GetTextValue(1, args);
            string sqlstring = base.GetTextValue(2, args);
            Feng.Data.MsSQL.SqlServerDbHelper sqlServerDbHelper = new Data.MsSQL.SqlServerDbHelper(ConnectionString);
            Feng.Json.FJsonArray json= base.GetArgIndex(3, args) as Feng.Json.FJsonArray;
 
            return sqlServerDbHelper.Query(sqlstring, json);
        }
        public virtual object SqlExecuteScalar(params object[] args)
        {
            string ConnectionString = base.GetTextValue(1, args);
            string sqlstring = base.GetTextValue(2, args);
            Feng.Collections.HashtableEx hashtable= base.GetArgIndex(3, args) as Feng.Collections.HashtableEx;
            Feng.Data.MsSQL.SqlServerDbHelper sqlServerDbHelper = new Data.MsSQL.SqlServerDbHelper(ConnectionString);
            return sqlServerDbHelper.GetSingle(sqlstring, hashtable);
        }

        public virtual object SqlFill(params object[] args)
        {
            string ConnectionString = base.GetTextValue(1, args);
            string sqlstring = base.GetTextValue(2, args);
            Feng.Collections.HashtableEx hashtable = base.GetArgIndex(3, args) as Feng.Collections.HashtableEx;
            Feng.Data.MsSQL.SqlServerDbHelper sqlServerDbHelper = new Data.MsSQL.SqlServerDbHelper(ConnectionString);
            return sqlServerDbHelper.Query(sqlstring, hashtable);
        }

        public virtual object SqlExecute(params object[] args)
        {
            string ConnectionString = base.GetTextValue(1, args);
            string sqlstring = base.GetTextValue(2, args);
            Feng.Collections.HashtableEx hashtable = base.GetArgIndex(3, args) as Feng.Collections.HashtableEx;
            Feng.Data.MsSQL.SqlServerDbHelper sqlServerDbHelper = new Data.MsSQL.SqlServerDbHelper(ConnectionString);
            return sqlServerDbHelper.ExecuteSql(sqlstring, hashtable);
        }
 
        public virtual object SqlExecuteTran(params object[] args)
        {
            string ConnectionString = base.GetTextValue(1, args);
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                connection.Open();
                SqlTransaction tran=connection.BeginTransaction();
                int rows = 0;
                try
                {
                    for (int i = 2; i < args.Length; i++)
                    {
                        string sqlstring = base.GetTextValue(i, args);
                        i++;
                        Feng.Collections.HashtableEx hashtable = base.GetArgIndex(i, args) as Feng.Collections.HashtableEx;
                        using (SqlCommand cmd = new SqlCommand(sqlstring, connection))
                        { 
                            if (hashtable != null)
                            {
                                foreach (DictionaryEntry item in hashtable)
                                {
                                    cmd.Parameters.Add(new SqlParameter() { ParameterName = Feng.Utils.ConvertHelper.ToString(item.Key), Value = item.Value });
                                }
                            }
                            cmd.Transaction = tran;
                            rows = rows + cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                { 
                    tran.Rollback();
                    throw ex;
                }
                tran.Commit();
                return rows;

            }
        }

        public virtual object SqlGetModel(params object[] args)
        {
            Feng.Collections.ListEx<object> list = base.GetArgIndex(1, args) as Feng.Collections.ListEx<object>;
            string sql = base.GetTextValue(2, args);
            if (list == null)
            {
                list = new Feng.Collections.ListEx<object>();
            }
            Feng.Collections.HashtableEx hash = new Collections.HashtableEx();
            object[] values = new object[args.Length -2];
            for (int i = 2; i < args.Length; i++)
            {
                object obj = base.GetArgIndex(i, args);
                values[i-2]=obj;
            }
            hash.Add("sql", sql);
            hash.Add("values", values);
            list.Add(hash);
            return list;
        }

        public virtual object SqlExecuteModeles(params object[] args)
        { 
            string ConnectionString = base.GetTextValue(1, args);
            Feng.Collections.ListEx<object> list = base.GetArgIndex(1, args) as Feng.Collections.ListEx<object>;
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            { 
                connection.Open();
                SqlTransaction tran = connection.BeginTransaction();
                int rows = 0;
                try
                {
                    foreach (Feng.Collections.HashtableEx item in list)
                    {
                        string sqlstring = Feng.Utils.ConvertHelper.ToString(item["sql"]);

                        using (SqlCommand cmd = new SqlCommand(sqlstring, connection))
                        {

                            foreach (DictionaryEntry key in item)
                            {
                                cmd.Parameters.Add(new SqlParameter() { ParameterName = Feng.Utils.ConvertHelper.ToString(key.Key), Value = key.Value });
                            }

                            cmd.Transaction = tran;
                            rows = rows + cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    tran.Rollback();
                    throw ex;
                }
                tran.Commit();
                return rows;

            }
        }
    }
}
