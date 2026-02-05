
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
namespace Feng.Data.MsSQL
{
    public class MSSQLHelper
    {
        private string _connectionString = string.Empty;
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
            }
        }
        public MSSQLHelper(string connectionString)
        {
            ConnectionString = connectionString;
        }

        #region 公用方法
        public List<string> GetTableNames()
        {
            string strSql = "select [name] from sysobjects where xtype='U'and [name]<>'dtproperties' order by [name]";//order by id
            DataTable table = Feng.Data.MsSQL.DbHelperSQL.QueryDataTable(strSql);
            List<string> list = new List<string>();
            foreach (DataRow row in table.Rows)
            {
                string tablename = Feng.Utils.ConvertHelper.ToString(row["name"]);
                if (!string.IsNullOrWhiteSpace(tablename))
                {
                    list.Add(tablename);
                }
            }
            return list;
        }

        public bool ColumnExists(string tableName, string columnName)
        {
            string sql = "select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'";
            object res = GetSingle(sql);
            if (res == null)
            {
                return false;
            }
            return Convert.ToInt32(res) > 0;
        }
        public int GetMaxID(string FieldName, string TableName)
        {
            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
            object obj = GetSingle(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
        public bool Exists(string strSql)
        {
            object obj = GetSingle(strSql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public void BackDataBase(string database, string path)
        {
            string __sbSQL = "backup database [" + database + "] to disk='" + path + "';";
             ExecuteSql(__sbSQL);
        }
        public bool TabExists(string TableName)
        {
            string strsql = "select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
            //string strsql = "SELECT count(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + TableName + "]') AND type in (N'U')";
            object obj = GetSingle(strsql);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public bool Exists(string strSql, params SqlParameter[] cmdParms)
        {
            object obj = GetSingle(strSql, cmdParms);
            int cmdresult;
            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
            {
                cmdresult = 0;
            }
            else
            {
                cmdresult = int.Parse(obj.ToString());
            }
            if (cmdresult == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public ModleInfo AddColumn(string table, string column, string type)
        {
            string sql = @"if not exists(select * from syscolumns where id=object_id('" + table + @"') and name='" + column + @"') 
begin
	alter table dbo." + table + @" add " + column + @" " + type + @"
end";
            return new ModleInfo(sql);
        }    
        
        public ModleInfo EditColumn(string table, string column, string type)
        {
            string sql = @"if not exists(select * from syscolumns where id=object_id('" + table + @"') and name='" + column + @"') 
begin
ALTER TABLE " + table + @" ALTER column " + column + @" " + type + @"
end";
            return new ModleInfo(sql);
        }

        public ModleInfo RenameColumn(string table, string column, string newcolumn)
        {
            string sql = @"if exists(select * from syscolumns where id=object_id('" + table + @"') and name='" + column + @"') 
begin
	EXEC sp_rename '" + table + @"." + column + @"', '" + newcolumn + @"', 'COLUMN'
end
";
            return new ModleInfo(sql);
        }

        public ModleInfo RenameTableName(string tablenaem, string newtablename)
        {
            string sql = @"--2.3存在表时修改表名称
if exists (select * from sysobjects where xtype='u' and name='" + tablenaem + @"') 
begin 
	EXEC sp_rename 'dbo." + tablenaem + @"', '" + newtablename + @"';
end";
            return new ModleInfo(sql);
        }

        public   List<ColumnInfo> GetColumns(string tablename)
        {
            List<ColumnInfo> list = new List<ColumnInfo>();

            StringBuilder sb = new StringBuilder();
            sb.Append(@"
SELECT
clmns.column_id as cid,
clmns.name AS name,
CAST(ISNULL(cik.index_column_id, 0) AS bit) AS pk,
CAST(ISNULL((select TOP 1 1 from sys.foreign_key_columns AS colfk where colfk.parent_column_id = clmns.column_id and colfk.parent_object_id = clmns.object_id), 0) AS bit) AS fk,
usrt.name AS [type],
ISNULL(baset.name, N'') AS systype,
CAST(CASE WHEN baset.name IN (N'nchar', N'nvarchar') AND clmns.max_length <> -1 THEN clmns.max_length/2 ELSE clmns.max_length END AS int) AS [Length],
CAST(clmns.precision AS int) AS [NumericPrecision],
CAST(clmns.scale AS int) AS [NumericScale],
clmns.is_nullable AS notnull,
clmns.is_computed AS [Computed],
clmns.is_identity AS [identity],
CAST(clmns.is_sparse AS bit) AS [IsSparse],
CAST(clmns.is_column_set AS bit) AS [IsColumnSet]
FROM
sys.tables AS tbl
INNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id
LEFT OUTER JOIN sys.indexes AS ik ON ik.object_id = clmns.object_id and 1=ik.is_primary_key
LEFT OUTER JOIN sys.index_columns AS cik ON cik.index_id = ik.index_id and cik.column_id = clmns.column_id and cik.object_id = clmns.object_id and 0 = cik.is_included_column
LEFT OUTER JOIN sys.types AS usrt ON usrt.user_type_id = clmns.user_type_id
LEFT OUTER JOIN sys.types AS baset ON (baset.user_type_id = clmns.system_type_id and baset.user_type_id = baset.system_type_id) or ((baset.system_type_id = clmns.system_type_id) and (baset.user_type_id = clmns.user_type_id) and (baset.is_user_defined = 0) and (baset.is_assembly_type = 1)) 
LEFT OUTER JOIN sys.xml_schema_collections AS xscclmns ON xscclmns.xml_collection_id = clmns.xml_collection_id
LEFT OUTER JOIN sys.schemas AS s2clmns ON s2clmns.schema_id = xscclmns.schema_id
WHERE
(tbl.name='" + tablename + @"' and SCHEMA_NAME(tbl.schema_id)='dbo')
ORDER BY
clmns.column_id ASC ");
            string sql = sb.ToString();
            DataTable table = this.QueryDataTable (sql);
            foreach (DataRow row in table.Rows)
            {
                ColumnInfo ci = new ColumnInfo();
                ci.CID = Feng.Utils.ConvertHelper.ToInt(row["cid"]);
                ci.ColumnName = row["name"].ToString();
                ci.ColumnType = row["type"].ToString();
                ci.ColumnWidth = Feng.Utils.ConvertHelper.ToInt(row["Length"].ToString());
  
                ci.Nullable = Feng.Utils.ConvertHelper.ToBoolean(row["notnull"]);
                //ci.DefaultValue = row["dflt_value"].ToString();
                ci.PrimaryKey = Feng.Utils.ConvertHelper.ToBoolean(row["pk"]);
                list.Add(ci);
            }

            return list;
        }
        #endregion

        #region  执行简单SQL语句

        public int ExecuteSql(string sqlstring)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlstring, connection))
                {
                    try
                    {
                        connection.Open();
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        public int ExecuteSqlByTime(string sqlstring, int Times)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlstring, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = Times;
                        int rows = cmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        public DataSet GetDataSetTables(string[] strsql, string[] tablename)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                SqlTransaction transaction = null;
                command.Connection = connection;
                command.Connection.Open();
                transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);//开始事务
                command.Connection = connection;
                command.Transaction = transaction;
                SqlDataAdapter da = null;
                for (int i = 0; i < strsql.Length; i++)
                {
                    da = new SqlDataAdapter(strsql[i], command.Connection);

                    da.SelectCommand.Transaction = transaction;
                    da.Fill(ds, tablename[i]);
                }
                transaction.Commit();//提交事务
                da.SelectCommand.Connection.Close();
                return ds;
            }

        }

        public int ExecuteSqlTran(List<String> sqls)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    for (int n = 0; n < sqls.Count; n++)
                    {
                        string strsql = sqls[n];
                        if (strsql.Trim().Length > 1)
                        {
                            cmd.CommandText = strsql;
                            count += cmd.ExecuteNonQuery();
                        }
                    }
                    tx.Commit();
                    return count;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw ex;
                }
            }
        }

        public int ExecuteSqlTran(List<Feng.Data.ModleInfo> list)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                SqlTransaction tx = conn.BeginTransaction();
                cmd.Transaction = tx;
                try
                {
                    int count = 0;
                    foreach (Feng.Data.ModleInfo mi in list)
                    {
                        PrepareCommand(cmd, conn, null, mi.Sql, mi.cmdParms);
                        count += cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                    }
                    tx.Commit();
                    return count;
                }
                catch (Exception ex)
                {
                    tx.Rollback();
                    throw ex;
                }
            }
        }

        public int ExecuteSql(string sqlstring, string content)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlstring, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        public object ExecuteSqlGet(string sqlstring, string content)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(sqlstring, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@content", SqlDbType.NText);
                myParameter.Value = content;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        public int ExecuteSqlInsertImg(string strSQL, byte[] fs)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand(strSQL, connection);
                System.Data.SqlClient.SqlParameter myParameter = new System.Data.SqlClient.SqlParameter("@fs", SqlDbType.Image);
                myParameter.Value = fs;
                cmd.Parameters.Add(myParameter);
                try
                {
                    connection.Open();
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Data.SqlClient.SqlException e)
                {
                    throw e;
                }
                finally
                {
                    cmd.Dispose();
                    connection.Close();
                }
            }
        }

        public object GetSingle(string sqlstring)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlstring, connection))
                {
                    try
                    {
                        connection.Open();
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        public object GetSingle(string sqlstring, int Times)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlstring, connection))
                {
                    try
                    {
                        connection.Open();
                        cmd.CommandTimeout = Times;
                        object obj = cmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        connection.Close();
                        throw e;
                    }
                }
            }
        }

        public SqlDataReader ExecuteReader(string strSQL)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(strSQL, connection);
            try
            {
                connection.Open();
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            }

        }

        public DataSet Query(string sqlstring)
        { 
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sqlstring, connection);
                    //System.Windows.Forms.MessageBox.Show(sqlstring + "{} " + connectionString);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {

                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        public DataTable QueryDataTable(string sqlstring)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataTable ds = new DataTable();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sqlstring, connection); 
                    command.Fill(ds);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        public DataSet Query(string sqlstring, int Times)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sqlstring, connection);
                    command.SelectCommand.CommandTimeout = Times;
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        #endregion

        #region 执行带参数的SQL语句

        public int ExecuteSql(string sqlstring, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sqlstring, cmdParms);
                        int rows = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }

        public void ExecuteSql_Tran(Hashtable sqls)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    { 
                        foreach (DictionaryEntry myDE in sqls)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public int ExecuteSqlTran(Hashtable sqls)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    { 
                        foreach (DictionaryEntry myDE in sqls)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        string str = ex.Message.ToString();
                        return 0;
                    }
                }
            }
        }

        public void ExecuteSqlTranWithIndentity(Hashtable sqlstringList)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        int indentity = 0; 
                        foreach (DictionaryEntry myDE in sqlstringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            foreach (SqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.InputOutput)
                                {
                                    q.Value = indentity;
                                }
                            }
                            PrepareCommand(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            foreach (SqlParameter q in cmdParms)
                            {
                                if (q.Direction == ParameterDirection.Output)
                                {
                                    indentity = Convert.ToInt32(q.Value);
                                }
                            }
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                    }
                    catch
                    {
                        trans.Rollback();
                        throw;
                    }
                }
            }
        }

        public object GetSingle(string sqlstring, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        PrepareCommand(cmd, connection, null, sqlstring, cmdParms);
                        object obj = cmd.ExecuteScalar();
                        cmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Data.SqlClient.SqlException e)
                    {
                        throw e;
                    }
                }
            }
        }

        public SqlDataReader ExecuteReader(string sqlstring, params SqlParameter[] cmdParms)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            try
            {
                PrepareCommand(cmd, connection, null, sqlstring, cmdParms);
                SqlDataReader myReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                cmd.Parameters.Clear();
                return myReader;
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                throw e;
            } 
        }

        public DataSet Query(string sqlstring, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, sqlstring, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        da.Fill(ds, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    return ds;
                }
            }
        }


        private void PrepareCommand(SqlCommand cmd, SqlConnection conn,
            SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans; 
            cmd.CommandType = CommandType.Text; 
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        public DataSet Query(string[] strOleDb)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand command = new SqlCommand();
                SqlTransaction transaction = null;
                command.Connection = connection;
                command.Connection.Open();
                transaction = connection.BeginTransaction(IsolationLevel.ReadCommitted);//开始事务
                command.Connection = connection;
                command.Transaction = transaction;
                SqlDataAdapter da = null;
                for (int i = 0; i < strOleDb.Length; i++)
                {
                    da = new SqlDataAdapter(strOleDb[i], command.Connection);

                    da.SelectCommand.Transaction = transaction;
                    da.Fill(ds, "table" + i.ToString());
                }
                transaction.Commit(); 
                da.SelectCommand.Connection.Close();
                return ds;
            }
        }

        #endregion

        #region 存储过程操作

        public SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataReader returnReader;
            connection.Open();
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return returnReader;

        }

        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet dataSet = new DataSet();
                connection.Open();
                SqlDataAdapter sqlDA = new SqlDataAdapter();
                sqlDA.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                sqlDA.SelectCommand.CommandTimeout = Times;
                sqlDA.Fill(dataSet, tableName);
                connection.Close();
                return dataSet;
            }
        }

        private SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    if (parameter != null)
                    { 
                        if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                            (parameter.Value == null))
                        {
                            parameter.Value = DBNull.Value;
                        }
                        command.Parameters.Add(parameter);
                    }
                }
            }
            return command;
        }

        public int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                int result;
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value; 
                return result;
            }
        }

        private SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion

        public DataTable GetDataTable(string sqlstring)
        {
            DataSet ds = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlstring, connection);

                da.Fill(ds);

                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        return ds.Tables[0];
                    }
                }
                return null;
            }


        }

        public DataSet Query1(string sqlstring)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataSet ds = new DataSet();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sqlstring, connection);
                    command.Fill(ds, "ds");
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                connection.Close();
                return ds;
            }
        }

        public int ExecuteStoredProcedureTran(Hashtable sqlstringList)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    SqlCommand cmd = new SqlCommand();
                    try
                    {
                        //循环
                        foreach (DictionaryEntry myDE in sqlstringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            SqlParameter[] cmdParms = (SqlParameter[])myDE.Value;
                            PrepareCommandStoredProcedure(cmd, conn, trans, cmdText, cmdParms);
                            int val = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();
                        }
                        trans.Commit();
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        string str = ex.Message.ToString();
                        return 0;
                    }
                }
            }
        }

        private void PrepareCommandStoredProcedure(SqlCommand cmd, SqlConnection conn,
                 SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            cmd.CommandType = CommandType.StoredProcedure;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if ((parameter.Direction == ParameterDirection.InputOutput || parameter.Direction == ParameterDirection.Input) &&
                        (parameter.Value == null))
                    {
                        parameter.Value = DBNull.Value;
                    }
                    cmd.Parameters.Add(parameter);
                }
            }
        }

        public SqlDbType GetSqlDbTypeByValue(object obj)
        {
            SqlDbType sqldb = SqlDbType.VarChar;
            if (obj.GetType() == typeof(int))
            {
                sqldb = SqlDbType.Int;
            }
            if (obj.GetType() == typeof(Int64))
            {
                sqldb = SqlDbType.BigInt;
            }
            if (obj.GetType() == typeof(Int16))
            {
                sqldb = SqlDbType.SmallInt;
            }
            if (obj.GetType() == typeof(DateTime))
            {
                sqldb = SqlDbType.DateTime;
            }
            if (obj.GetType() == typeof(decimal))
            {
                sqldb = SqlDbType.Decimal;
            }
            if (obj.GetType() == typeof(double))
            {
                sqldb = SqlDbType.Real;
            }
            if (obj.GetType() == typeof(float))
            {
                sqldb = SqlDbType.Float;
            }
            return sqldb;
        }



        public QueryArgs IDQueryArgs
        {
            get
            {
                return new QueryArgs() { ColumnName = "ID" };
            }
        }

        public void Query(System.Collections.Generic.List<QueryArgs> list)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from dbo.rep_equipment ");
            strSql.Append(" where 1=1 ");
            SqlParameter[] parameters = new SqlParameter[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                QueryArgs arg = list[i];
                switch (arg.QueryMode)
                {
                    case QueryArgs.QueryMode_Default:
                        strSql.Append(" and " + arg.ColumnName + " =@" + arg.ColumnName);
                        break;
                    case QueryArgs.QueryMode_Equals:
                        strSql.Append(" and " + arg.ColumnName + " =@" + arg.ColumnName);
                        break;
                    case QueryArgs.QueryMode_LessThan:
                        strSql.Append(" and " + arg.ColumnName + " <@" + arg.ColumnName);
                        break;
                    case QueryArgs.QueryMode_MoreThan:
                        strSql.Append(" and " + arg.ColumnName + " >@" + arg.ColumnName);
                        break;
                    case QueryArgs.QueryMode_Like:
                        strSql.Append(" and " + arg.ColumnName + " like'%@" + arg.ColumnName + "%' ");
                        break;
                    case QueryArgs.QueryMode_LeftLike:
                        strSql.Append(" and " + arg.ColumnName + " like '%@" + arg.ColumnName + "' ");
                        break;
                    case QueryArgs.QueryMode_RightLike:
                        strSql.Append(" and " + arg.ColumnName + " like '@" + arg.ColumnName + "%'");
                        break;
                    case QueryArgs.QueryMode_LessThanAndEquals:
                        strSql.Append(" and " + arg.ColumnName + " <=@" + arg.ColumnName);
                        break;
                    case QueryArgs.QueryMode_MoreThanAndEquals:
                        strSql.Append(" and " + arg.ColumnName + " >=@" + arg.ColumnName);
                        break;
                    default:
                        break;
                }
                parameters[i] = new SqlParameter("@" + arg.ColumnName, arg.Value);
            }
        }
    }

}
