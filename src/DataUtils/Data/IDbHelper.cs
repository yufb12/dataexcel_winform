using System;
namespace Feng.Data
{
    public interface IDbHelper
    {
        string ConnectionString { get; set; }
        int ExecuteSql(string sqlstring, params System.Data.SqlClient.SqlParameter[] cmdParms);
        int ExecuteSql(string sqlstring);
        int ExecuteSqlTran(System.Collections.Generic.List<Feng.Data.ModleInfo> sqlstringList);
        int ExecuteSqlTran(System.Collections.Generic.List<string> sqlstringList);
        object GetSingle(string sqlstring, params System.Data.SqlClient.SqlParameter[] cmdParms);
        object GetSingle(string sqlstring);
        System.Data.DataTable Query(string sqlstring, params System.Data.SqlClient.SqlParameter[] cmdParms);
        System.Data.DataTable Query(string sqlstring);
    }
}
