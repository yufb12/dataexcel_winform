
using System;
using System.Collections;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using System.Collections.Generic;


namespace Feng.Data.MsSQL
{
    public class MsDbHelperSQL
    {
        public static Feng.Net.Tcp.ISqlClient client = null;
        public static int ExecuteSql(string sqlstring, params System.Data.SqlClient.SqlParameter[] cmdParms)
        {
            return client.ExecuteSql(sqlstring, cmdParms);
        }
        public static int ExecuteSql(string sqlstring)
        {
            return client.ExecuteSql(sqlstring);
        }
        public static int ExecuteSqlTran(System.Collections.Generic.List<Feng.Data.ModleInfo> sqlstringList)
        {
            return client.ExecuteSqlTran(sqlstringList);
        }
        public static int ExecuteSqlTran(System.Collections.Generic.List<string> sqlstringList)
        {
            return client.ExecuteSqlTran(sqlstringList);
        }
        public static object GetSingle(string sqlstring, params System.Data.SqlClient.SqlParameter[] cmdParms)
        {
            return client.GetSingle(sqlstring, cmdParms);
        }
        public static object GetSingle(string sqlstring)
        {
            return client.GetSingle(sqlstring);
        }
        public static System.Data.DataTable Query(string sqlstring, params System.Data.SqlClient.SqlParameter[] cmdParms)
        {
            return client.Query(sqlstring, cmdParms);
        }
        public static System.Data.DataTable Query(string sqlstring)
        {
            return client.Query(sqlstring);
        }
        public static bool Exists(string sqlstring, params System.Data.SqlClient.SqlParameter[] cmdParms)
        {
            return client.Exists(sqlstring, cmdParms);
        }
    }
}
