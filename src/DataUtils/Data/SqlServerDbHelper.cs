using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Feng.Data.MsSQL
{
    public class ExecuteSqlTranModel
    {
        public string Sqlstring { get; set; }

        public List<object> List { get; set; }
    }
    public class SqlServerDbHelper
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

        public SqlServerDbHelper(string connectionString)
        {
            ConnectionString = connectionString;
        }

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

        public int ExecuteSql(string sqlstring, List<object> list)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        SqlParameter[] cmdParms = new SqlParameter[list.Count];
                        for (int i = 0; i < list.Count; i++)
                        {
                            SqlParameter sqlParameter = new SqlParameter();
                            sqlParameter.Value = list[i];
                            sqlParameter.ParameterName = "@ARG" + (i + 1).ToString();
                            cmdParms[i] = sqlParameter;
                        }
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
        public int ExecuteSql(string sqlstring, List<object> list, List<CheckValue> listargs)
        {
            for (int i = 0; i < listargs.Count; i++)
            {
                CheckValue av = listargs[i];
                if (av.Check)
                {
                    sqlstring = sqlstring.Replace("{@ARG" + (i + 1) + "}", listargs[i].Text);
                }
                else
                {
                    string text = Feng.Utils.ConvertHelper.ToString(list[i]);
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        sqlstring = sqlstring.Replace("{@ARG" + (i + 1) + "}", string.Empty);
                    }
                    else
                    {
                        sqlstring = sqlstring.Replace("{@ARG" + (i + 1) + "}", listargs[i].Text);
                    }
                }
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        SqlParameter[] cmdParms = new SqlParameter[list.Count];
                        for (int i = 0; i < list.Count; i++)
                        {
                            SqlParameter sqlParameter = new SqlParameter();
                            sqlParameter.Value = list[i];
                            sqlParameter.ParameterName = "@ARG" + (i + 1).ToString();
                            cmdParms[i] = sqlParameter;
                        }
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
        public int ExecuteSql(string sqlstring, Feng.Collections.HashtableEx hashtable)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        SqlParameter[] cmdParms = null;
                        if (hashtable != null)
                        {
                            cmdParms = new SqlParameter[hashtable.Count];
                            int i = 0;
                            foreach (DictionaryEntry item in hashtable)
                            {
                                cmdParms[i]=new SqlParameter() { ParameterName = Feng.Utils.ConvertHelper.ToString(item.Key), Value = item.Value };
                                i++;
                            }
                        }
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
        public int ExecuteSql(ExecuteSqlTranModel model)
        {
            string sqlstring = model.Sqlstring;
            List<object> list = model.List;

            SqlParameter[] cmdParms = new SqlParameter[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                SqlParameter sqlParameter = new SqlParameter();
                sqlParameter.Value = list[i];
                sqlParameter.ParameterName = "@ARG" + (i + 1).ToString();
                cmdParms[i] = sqlParameter;
            }
            return ExecuteSql(sqlstring, cmdParms);
        }

        public int ExecuteSqlTran(List<ExecuteSqlTranModel> models)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                int rows =0;
                connection.Open();
                SqlTransaction tran = connection.BeginTransaction();

                try
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.Connection = connection;
                        cmd.Transaction = tran;

                        foreach (ExecuteSqlTranModel item in models)
                        {
                            string sqlstring = item.Sqlstring;
                            List<object> list = item.List;

                            SqlParameter[] cmdParms = new SqlParameter[list.Count];
                            for (int i = 0; i < list.Count; i++)
                            {
                                SqlParameter sqlParameter = new SqlParameter();
                                sqlParameter.Value = list[i];
                                sqlParameter.ParameterName = "@ARG" + (i + 1).ToString();
                                cmdParms[i] = sqlParameter;
                            }
                            PrepareCommand(cmd, connection, tran, sqlstring, cmdParms);

                            Feng.Utils.TraceHelper.WriteTrace("SqlServerDbHelper", "ExecuteSqlTran", "ExecuteSqlTran", true , sqlstring);
                            int count = cmd.ExecuteNonQuery();
                            rows = rows + count;
                            cmd.Parameters.Clear();
                        }
                        tran.Commit();
                    }
                }
                catch (Exception e)
                {
                    Feng.Utils.TraceHelper.WriteTrace("SqlServerDbHelper", "ExecuteSqlTran", "", e);
                    tran.Rollback();
                    throw e;
                }
                return rows;
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
        public object GetSingle(string sqlstring, List<object> list)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sqlstring, connection))
                {
                    try
                    {
                        connection.Open();
                        SqlParameter[] cmdParms = new SqlParameter[list.Count];
                        for (int i = 0; i < list.Count; i++)
                        {
                            SqlParameter sqlParameter = new SqlParameter();
                            sqlParameter.Value = list[i];
                            sqlParameter.ParameterName = "@ARG" + (i + 1).ToString();
                            cmdParms[i] = sqlParameter;
                        }
                        PrepareCommand(cmd, connection, null, sqlstring, cmdParms);
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
        public object GetSingle(string sqlstring, Feng.Collections.HashtableEx hashtable)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        int index = 0;
                        SqlParameter[] cmdParms = null;
                        if (hashtable != null)
                        {
                            cmdParms = new SqlParameter[hashtable.Count];
                            foreach (DictionaryEntry item in hashtable)
                            {
                                cmdParms[index] = new SqlParameter()
                                {
                                    ParameterName = Feng.Utils.ConvertHelper.ToString(item.Key),
                                    Value = item.Value
                                };
                            }
                        }
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

        public DataTable Query(string sqlstring)
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
        public DataTable Query(string sqlstring, params SqlParameter[] cmdParms)
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
        public DataTable Query(string sqlstring, List<object> list)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameter[] cmdParms = new SqlParameter[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.Value = list[i];
                    sqlParameter.ParameterName = "@ARG" + (i+1).ToString();
                    cmdParms[i] = sqlParameter;
                }
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
        public DataTable Query(string sqlstring, Feng.Json.FJsonArray jsonarr)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameter[] cmdParms = new SqlParameter[jsonarr.Count];
                for (int i = 0; i < jsonarr.Count; i++)
                {
                    Feng.Json.FJson json  = jsonarr[i] as Feng.Json.FJson;
                    string conditionname = Feng.Utils.ConvertHelper.ToString(json["ConditionName"].Value);
                    string condition = Feng.Utils.ConvertHelper.ToString(json["Condition"].Value);
                    string paraname= Feng.Utils.ConvertHelper.ToString(json["ParameterName"].Value);
                    object paravalue =  json.GetKey("Value"); 
                    if (paravalue == null)
                    {
                        sqlstring = sqlstring.Replace(conditionname, condition);
                        continue;
                    }
                    sqlstring = sqlstring.Replace(conditionname, condition);
                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.Value = paravalue;
                    sqlParameter.ParameterName = paraname;
                    cmdParms[i] = sqlParameter;
                }
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
        public DataTable Query(string sqlstring, Feng.Collections.HashtableEx hashtable)
        {

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();
                SqlParameter[] cmdParms = new SqlParameter[hashtable.Count];
                int index = 0;
                foreach (DictionaryEntry item in hashtable)
                {
                    cmdParms[index] = new SqlParameter()
                    {
                        ParameterName = Feng.Utils.ConvertHelper.ToString(item.Key),
                        Value = item.Value
                    };
                }
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
        public DataTable Query(string sqlstring, List<object> values, List<CheckValue> listargs)
        {
            List<SqlParameter> cmdParms = new  List<SqlParameter> ();
 
            for (int i = 0; i < listargs.Count; i++)
            {
                if (values.Count <= i)
                {
                    sqlstring = sqlstring.Replace("{@ARG" + (i + 1) + "}", string.Empty);
                    continue;
                }
                CheckValue av = listargs[i];

                if (av.Check)
                {
                    SqlParameter sqlParameter = new SqlParameter();
                    sqlParameter.Value = values[i];
                    sqlParameter.ParameterName = "@ARG" + (i + 1).ToString();
                    cmdParms.Add(sqlParameter);
                    sqlstring = sqlstring.Replace("{@ARG" + (i + 1) + "}", listargs[i].Text);
                }
                else
                {
                    string text = Feng.Utils.ConvertHelper.ToString(values[i]);
                    if (string.IsNullOrWhiteSpace(text))
                    {
                        sqlstring = sqlstring.Replace("{@ARG" + (i + 1) + "}", string.Empty);
                    }
                    else
                    {
                        SqlParameter sqlParameter = new SqlParameter();
                        sqlParameter.Value = values[i];
                        sqlParameter.ParameterName = "@ARG" + (i + 1).ToString();
                        cmdParms.Add(sqlParameter);
                        sqlstring = sqlstring.Replace("{@ARG" + (i + 1) + "}", listargs[i].Text);
                    }
                }
            }
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                SqlCommand cmd = new SqlCommand();

                PrepareCommand(cmd, connection, null, sqlstring, cmdParms.ToArray());
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
        public int ExecuteSqlTran(List<Feng.Data.ModleInfo> mis)
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

        private void PrepareCommand(SqlCommand cmd, SqlConnection conn,
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
 
    }

}
