using System;
using Feng.Net;
namespace Feng.Net.Tcp
{
    public interface ISqlClient
    {
        //void BeginQueryTable(string sqlstring, NetCallBacks.QueryTableCallBack callback);
        bool ColumnExists(string tableName, string columnName);
        bool ColumnExists(string tableName, string columnName, short waittime);
        int ExecuteSql(string sqlstring);
        int ExecuteSql(string sqlstring, params global::System.Data.SqlClient.SqlParameter[] cmdParms);
        int ExecuteSql(string sqlstring, short waittime);
        int ExecuteSql(string sqlstring, short waittime, params global::System.Data.SqlClient.SqlParameter[] cmdParms);
        int ExecuteSqlTran(global::System.Collections.Generic.List<global::Feng.Data.ModleInfo> list);
        int ExecuteSqlTran(global::System.Collections.Generic.List<global::Feng.Data.ModleInfo> list, short waittime);
        int ExecuteSqlTran(global::System.Collections.Generic.List<string> sqlstringList);
        int ExecuteSqlTran(global::System.Collections.Generic.List<string> sqlstringList, short waittime);
        bool Exists(string strSql);
        bool Exists(string strSql, params global::System.Data.SqlClient.SqlParameter[] cmdParms);
        bool Exists(string strSql, short waittime);
        bool Exists(string strSql, short waittime, params global::System.Data.SqlClient.SqlParameter[] cmdParms);
        int GetMaxID(string FieldName, string TableName);
        int GetMaxID(string FieldName, string TableName, short waittime);
        object GetSingle(string sqlstring);
        object GetSingle(string sqlstring, params global::System.Data.SqlClient.SqlParameter[] cmdParms);
        object GetSingle(string sqlstring, short waittime);
        object GetSingle(string sqlstring, short waittime, params global::System.Data.SqlClient.SqlParameter[] cmdParms);
        void PostCallBac(IAsyncResult ar);
        global::System.Data.DataTable Query(string sqlstring);
        global::System.Data.DataTable Query(string sqlstring, params global::System.Data.SqlClient.SqlParameter[] cmdParms);
        global::System.Data.DataTable Query(string sqlstring, short waittime);
        global::System.Data.DataTable Query(string sqlstring, short waittime, params global::System.Data.SqlClient.SqlParameter[] cmdParms);
        global::System.Data.DataTable QueryTable(string sqlstring);
        bool TabExists(string TableName);
        bool TabExists(string TableName, short waittime);
    }
}
