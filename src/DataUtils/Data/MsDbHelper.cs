
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

    public class MsDbHelper : Feng.Data.IDbHelper
    {
        public int ExecuteSql(string sqlstring)
        {
            return DbHelperSQL.ExecuteSql(sqlstring);
        }

        public int ExecuteSqlTran(List<String> sqlstringList)
        {
            return DbHelperSQL.ExecuteSqlTran(sqlstringList);
        }

        public int ExecuteSqlTran(List<Feng.Data.ModleInfo> sqlstringList)
        {
            return DbHelperSQL.ExecuteSqlTran(sqlstringList);
        }

        public object GetSingle(string sqlstring)
        {
            return DbHelperSQL.GetSingle(sqlstring);
        }

        public DataTable Query(string sqlstring)
        {
            return DbHelperSQL.Query(sqlstring);
        }

        public int ExecuteSql(string sqlstring, params SqlParameter[] cmdParms)
        {
            return DbHelperSQL.ExecuteSql(sqlstring, cmdParms);
        }

        public object GetSingle(string sqlstring, params SqlParameter[] cmdParms)
        {
            return DbHelperSQL.GetSingle(sqlstring, cmdParms);
        }

        public DataTable Query(string sqlstring, params SqlParameter[] cmdParms)
        {
            return DbHelperSQL.Query(sqlstring, cmdParms);
        }


        #region IDbHelper 成员
         
        public string ConnectionString
        {
            get
            {
                return DbHelperSQL.ConnectionString;
            }
            set
            {
                DbHelperSQL.ConnectionString = value;
            }
        }

        #endregion
 
    }

}
