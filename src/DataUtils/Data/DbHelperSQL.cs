using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Feng.Data.MsSQL
{
    public class DbHelperSQL
    {
        private static string _connectionString = string.Empty;
        public static string ConnectionString
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

        public DbHelperSQL(string connectionString)
        {
            ConnectionString = connectionString;
        }

        #region 公用方法

        public static bool ColumnExists(string tableName, string columnName)
        {
            string sql = "select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'";
            object res = GetSingle(sql);
            if (res == null)
            {
                return false;
            }
            return Convert.ToInt32(res) > 0;
        }

        public static int GetMaxID(string FieldName, string TableName)
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

        public static bool Exists(string strSql)
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

        public static void BackDataBase(string database, string path)
        {
            string __sbSQL = "backup database [" + database + "] to disk='" + path + "';";
            Feng.Data.MsSQL.DbHelperSQL.ExecuteSql(__sbSQL);
        }

        public static bool TabExists(string TableName)
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

        public static bool Exists(string strSql, params SqlParameter[] cmdParms)
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

        #endregion

        #region  执行简单SQL语句

        public static int ExecuteSql(string sqlstring)
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

        public static int ExecuteSqlByTime(string sqlstring, int Times)
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

        public static DataSet GetDataSetTables(string[] strsql, string[] tablename)
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

        public static int ExecuteSqlTran(List<String> sqlstringList)
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
                    for (int n = 0; n < sqlstringList.Count; n++)
                    {
                        string strsql = sqlstringList[n];
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

                    throw new Exception(ex.Message);

                    //return 0;

                }
            }
        }

        public static int ExecuteSqlTran(List<Feng.Data.ModleInfo> mis)
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
                    foreach (Feng.Data.ModleInfo mi in mis)
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
                    throw new Exception(ex.Message);
                }
            }
        }

        public static int ExecuteSql(string sqlstring, string content)
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

        public static object ExecuteSqlGet(string sqlstring, string content)
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

        public static int ExecuteSqlInsertImg(string strSQL, byte[] fs)
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

        public static object GetSingle(string sqlstring)
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

        public static object GetSingle(string sqlstring, int Times)
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

        public static SqlDataReader ExecuteReader(string strSQL)
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

        public static DataTable Query(string sqlstring)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataTable table = new DataTable();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sqlstring, connection);
                    command.Fill(table);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return table;
            }
        }

        public static DataTable QueryDataTable(string sqlstring)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataTable ds = new DataTable();
                try
                {
                    connection.Open();
                    SqlDataAdapter command = new SqlDataAdapter(sqlstring, connection);
                    //System.Windows.Forms.MessageBox.Show(sqlstring + "{} " + connectionString);
                    command.Fill(ds);
                }
                catch (System.Data.SqlClient.SqlException ex)
                {
                    throw new Exception(ex.Message);
                }
                return ds;
            }
        }

        public static DataSet QueryDataSet(string sqlstring)
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
                return ds;
            }
        }

        public static DataSet Query(string sqlstring, int Times)
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

        public static int ExecuteSql(string sqlstring, params SqlParameter[] cmdParms)
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

        public static void ExecuteSql_Tran(Hashtable sqlstringList)
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

        public static int ExecuteSqlTran(Hashtable sqlstringList)
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

        public static void ExecuteSqlTranWithIndentity(Hashtable sqlstringList)
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
                        //循环
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

        public static object GetSingle(string sqlstring, params SqlParameter[] cmdParms)
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

        public static SqlDataReader ExecuteReader(string sqlstring, params SqlParameter[] cmdParms)
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
            //			finally
            //			{
            //				cmd.Dispose();
            //				connection.Close();
            //			}	

        }

        public static DataTable Query(string sqlstring, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, sqlstring, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataTable table = new DataTable();
                    try
                    {
                        da.Fill(table);
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine(sqlstring);
#endif
                        throw new Exception(ex.Message);
                    }
                    return table;
                }
            }
        }

        public static DataSet QueryDataSet(string sqlstring, params SqlParameter[] cmdParms)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                PrepareCommand(cmd, connection, null, sqlstring, cmdParms);
                using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                {
                    DataSet table = new DataSet();
                    try
                    {
                        da.Fill(table, "ds");
                        cmd.Parameters.Clear();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine(sqlstring);
#endif
                        throw new Exception(ex.Message);
                    }
                    return table;
                }
            }
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn,
            SqlTransaction trans, string cmdText, SqlParameter[] cmdParms)
        {
            if (conn.State != ConnectionState.Open)
                conn.Open();
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            if (trans != null)
                cmd.Transaction = trans;
            //cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandType = CommandType.Text;//cmdType;
            if (cmdParms != null)
            {
                foreach (SqlParameter parameter in cmdParms)
                {
                    if (parameter != null)
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
        }

        public static DataSet Query(string[] strOleDb)
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
                transaction.Commit();//提交事务
                da.SelectCommand.Connection.Close();
                return ds;
            }
        }

        #endregion

        #region 存储过程操作

        public static SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            SqlConnection connection = new SqlConnection(ConnectionString);
            SqlDataReader returnReader;
            connection.Open();
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.CommandType = CommandType.StoredProcedure;
            returnReader = command.ExecuteReader(CommandBehavior.CloseConnection);
            return returnReader;

        }

        public static DataTable RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName = "")
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                DataTable table = new DataTable();
                connection.Open();
                SqlDataAdapter ada = new SqlDataAdapter();
                ada.SelectCommand = BuildQueryCommand(connection, storedProcName, parameters);
                ada.Fill(table);
                if (string.IsNullOrWhiteSpace(tableName))
                {
                    tableName = "TABLENAME";
                }
                table.TableName = tableName;
                connection.Close();
                return table;
            }
        }

        public static DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName, int Times)
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

        private static SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = new SqlCommand(storedProcName, connection);
            command.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
            {
                foreach (SqlParameter parameter in parameters)
                {
                    if (parameter != null)
                    {
                        // 检查未分配值的输出参数,将其分配以DBNull.Value.
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

        public static int RunProcedure(string storedProcName, IDataParameter[] parameters, out int rowsAffected)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                int result;
                connection.Open();
                SqlCommand command = BuildIntCommand(connection, storedProcName, parameters);
                rowsAffected = command.ExecuteNonQuery();
                result = (int)command.Parameters["ReturnValue"].Value;
                //Connection.Close();
                return result;
            }
        }

        private static SqlCommand BuildIntCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
        {
            SqlCommand command = BuildQueryCommand(connection, storedProcName, parameters);
            command.Parameters.Add(new SqlParameter("ReturnValue",
                SqlDbType.Int, 4, ParameterDirection.ReturnValue,
                false, 0, 0, string.Empty, DataRowVersion.Default, null));
            return command;
        }
        #endregion

        public static DataTable GetDataTable(string sqlstring)
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

        public static DataSet Query1(string sqlstring)
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

        public static int ExecuteStoredProcedureTran(Hashtable sqlstringList)
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

        private static void PrepareCommandStoredProcedure(SqlCommand cmd, SqlConnection conn,
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

        public static SqlDbType GetSqlDbTypeByValue(object obj)
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



        public class QueryArgs
        {
            public const int QueryMode_Default = 0;
            public const int QueryMode_Equals = 1;
            public const int QueryMode_LessThan = 2;
            public const int QueryMode_MoreThan = 3;
            public const int QueryMode_Like = 4;
            public const int QueryMode_LeftLike = 5;
            public const int QueryMode_RightLike = 6;
            public const int QueryMode_LessThanAndEquals = 7;
            public const int QueryMode_MoreThanAndEquals = 8;
            public int QueryMode { get; set; }
            public string Field { get; set; }
            public object Value { get; set; }
        }

        public QueryArgs IDQueryArgs
        {
            get
            {
                return new QueryArgs() { Field = "ID" };
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
                        strSql.Append(" and " + arg.Field + " =@" + arg.Field);
                        break;
                    case QueryArgs.QueryMode_Equals:
                        strSql.Append(" and " + arg.Field + " =@" + arg.Field);
                        break;
                    case QueryArgs.QueryMode_LessThan:
                        strSql.Append(" and " + arg.Field + " <@" + arg.Field);
                        break;
                    case QueryArgs.QueryMode_MoreThan:
                        strSql.Append(" and " + arg.Field + " >@" + arg.Field);
                        break;
                    case QueryArgs.QueryMode_Like:
                        strSql.Append(" and " + arg.Field + " like'%@" + arg.Field + "%' ");
                        break;
                    case QueryArgs.QueryMode_LeftLike:
                        strSql.Append(" and " + arg.Field + " like '%@" + arg.Field + "' ");
                        break;
                    case QueryArgs.QueryMode_RightLike:
                        strSql.Append(" and " + arg.Field + " like '@" + arg.Field + "%'");
                        break;
                    case QueryArgs.QueryMode_LessThanAndEquals:
                        strSql.Append(" and " + arg.Field + " <=@" + arg.Field);
                        break;
                    case QueryArgs.QueryMode_MoreThanAndEquals:
                        strSql.Append(" and " + arg.Field + " >=@" + arg.Field);
                        break;
                    default:
                        break;
                }
                parameters[i] = new SqlParameter("@" + arg.Field, arg.Value);
            }
        }
    }

}
