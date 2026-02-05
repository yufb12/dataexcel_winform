//using System;
//using System.Net;
//using System.Net.Sockets;
//using System.Threading;
//using System.Text;
//using System.Collections.Generic;

//using System.Data;
//using System.Data.SqlClient;
//namespace Feng.Net.Tcp
//{

//    public class SqlClient : TcpClient, Feng.Net.Tcp.ISqlClient
//    {
  
//        public SqlClient()
//            : base()
//        {
//        } 
 

//        public SqlClient(string ip)
//            : base(ip)
//        {

//        }

//        public void PostCallBac(IAsyncResult ar)
//        {
//            AsyncResultPost arp = ar as AsyncResultPost;
//            byte[] data = arp.Value as byte[];
//            DataTable table = null;
//            if (data != null)
//            {
//                PacketHelper ph = PacketHelper.Get(data);
//                table = Feng.IO.SerializableHelper.DataTableFromBinary(ph.PacketContents) as System.Data.DataTable;
//            }
//            NetCallBacks.QueryTableCallBack callback = arp.State as NetCallBacks.QueryTableCallBack;
//            callback.Invoke(this, table);
//        }

//        public System.Data.DataTable Query(string sqlstring)
//        {
//            DataTable result = null;

//            PacketHelper ph = new PacketHelper(PacketMainCmd.SQL,
//                PacketSubCmd_SqlCommandSection.Query, System.Text.Encoding.Unicode.GetBytes(sqlstring));
//            ph.PacketMode = (byte)PacketMode.Send;
//            ph.PacketType = PacketType.ToServer;
//            ph.Compress = true;
//            byte[] data;
//            if (this.Send(ph, out  data))
//            {
//                ph = PacketHelper.Get(data);
//                NetResult res = ph.GetResult();
//                if (!res.Success)
//                {
//                    throw new Exception(res.ErrorCode);
//                }
//                return Feng.IO.SerializableHelper.DataTableFromBinary(res.Value) as System.Data.DataTable;
//            }

//            return result;
//        }

//        public System.Data.DataTable QueryTable(string sqlstring)
//        {
//            DataTable result = null;

//            PacketHelper ph = new PacketHelper(PacketMainCmd.SQL,
//                PacketSubCmd_SqlCommandSection.QueryTable, System.Text.Encoding.Unicode.GetBytes(sqlstring));
//            ph.PacketMode = (byte)PacketMode.Send;
//            ph.PacketType = PacketType.ToServer;
//            ph.Compress = true;
//            byte[] data;
//            if (this.Send(ph, out  data))
//            {
//                ph = PacketHelper.Get(data);
//                NetResult res = ph.GetResult();
//                if (!res.Success)
//                {
//                    throw new Exception(res.ErrorCode);
//                }
//                return Feng.IO.SerializableHelper.DataTableFromBinary(res.Value) as System.Data.DataTable;
//            }

//            return result;
//        }

//        public int ExecuteSql(string sqlstring)
//        {
//            int result = 0;
//            PacketHelper ph = new PacketHelper(PacketMainCmd.SQL,
//                PacketSubCmd_SqlCommandSection.ExecuteSql, System.Text.Encoding.Unicode.GetBytes(sqlstring));
//            ph.PacketMode = (byte)PacketMode.Send;
//            ph.PacketType = PacketType.ToServer;
//            byte[] data;
//            if (this.Send(ph, out  data))
//            {
                
//                ph = PacketHelper.Get(data);
//                NetResult res = ph.GetResult();
//                if (!res.Success)
//                {
//                    throw new Exception(res.ErrorCode);
//                }
//                return BitConverter.ToInt32(res.Value, 0);
//            }
//            return result;
//        }

//        public object GetSingle(string sqlstring)
//        {
//            object result = null;

//            PacketHelper ph = new PacketHelper(PacketMainCmd.SQL,
//                PacketSubCmd_SqlCommandSection.GetSingle, System.Text.Encoding.Unicode.GetBytes(sqlstring));
//            ph.PacketMode = (byte)PacketMode.Send;
//            ph.PacketType = PacketType.ToServer;
//            byte[] data;
//            if (this.Send(ph, out  data))
//            {
//                ph = PacketHelper.Get(data); 
//                NetResult res = ph.GetResult();
//                if (!res.Success)
//                {
//                    throw new Exception(res.ErrorCode);
//                }
//                return Feng.IO.SerializableHelper.DeSerializeFromBinary(res.Value);
//            }

//            return result;
//        }

//        public System.Data.DataTable Query(string sqlstring, params SqlParameter[] cmdParms)
//        {
//            System.Data.DataTable result = null;
//            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
//            {
//                bw.Write(sqlstring);
//                bw.Write(cmdParms.Length);
//                foreach (SqlParameter sqm in cmdParms)
//                {
//                    bw.Write(sqm);
//                }
//                PacketHelper ph = new PacketHelper(PacketMainCmd.SQL,
//                    PacketSubCmd_SqlCommandSection.QueryBySqlParameter, bw.GetData());
//                ph.PacketMode = (byte)PacketMode.Send;
//                ph.PacketType = PacketType.ToServer;
//                ph.Compress = true;
//                byte[] data;
//                if (this.Send(ph, out  data))
//                {
//                    ph = PacketHelper.Get(data);
//                    NetResult res = ph.GetResult();
//                    if (!res.Success)
//                    {
//                        throw new Exception(res.ErrorCode);
//                    }
//                    return Feng.IO.SerializableHelper.DataTableFromBinary(res.Value) as System.Data.DataTable;
//                }

//                return result;
//            }
//        }

//        public object GetSingle(string sqlstring, params SqlParameter[] cmdParms)
//        {
//            object result = null;
//            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
//            {
//                bw.Write(sqlstring);
//                bw.Write(cmdParms.Length);
//                foreach (SqlParameter sqm in cmdParms)
//                {
//                    bw.Write(sqm);
//                }
//                PacketHelper ph = new PacketHelper(PacketMainCmd.SQL,
//                    PacketSubCmd_SqlCommandSection.GetSingleBySqlParameter, bw.GetData());
//                ph.PacketMode = (byte)PacketMode.Send;
//                ph.PacketType = PacketType.ToServer;
//                byte[] data;
//                if (this.Send(ph, out  data))
//                {
//                    ph = PacketHelper.Get(data);
//                    NetResult res = ph.GetResult();
//                    if (!res.Success)
//                    {
//                        throw new Exception(res.ErrorCode);
//                    }
//                    return Feng.IO.SerializableHelper.DeSerializeFromBinary(res.Value);
//                }

//                return result;
//            }
//        }

//        public int ExecuteSql(string sqlstring, params SqlParameter[] cmdParms)
//        {
//            int result = 0;
//            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
//            {
//                bw.Write(sqlstring);
//                bw.Write(cmdParms.Length);
//                foreach (SqlParameter sqm in cmdParms)
//                {
//                    bw.Write(sqm);
//                }
//                PacketHelper ph = new PacketHelper(PacketMainCmd.SQL,
//                    PacketSubCmd_SqlCommandSection.ExecuteSqlBySqlParameter, bw.GetData());
//                ph.PacketMode = (byte)PacketMode.Send;
//                ph.PacketType = PacketType.ToServer;
//                byte[] data;
//                if (this.Send(ph, out  data))
//                {
//                    ph = PacketHelper.Get(data);
//                    NetResult res = ph.GetResult();
//                    if (!res.Success)
//                    {
//                        throw new Exception(res.ErrorCode);
//                    }
//                    return BitConverter.ToInt32(res.Value, 0);
//                }

//                return result;
//            }
//        }

//        public int ExecuteSqlTran(List<String> sqlstringList)
//        {
//            int result = 0;
//            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
//            {
//                bw.Write(sqlstringList.Count);
//                foreach (string sqm in sqlstringList)
//                {
//                    bw.Write(sqm);
//                }
//                PacketHelper ph = new PacketHelper(PacketMainCmd.SQL,
//    PacketSubCmd_SqlCommandSection.ExecuteSqlTran, bw.GetData());
//                ph.PacketMode = (byte)PacketMode.Send;
//                ph.PacketType = PacketType.ToServer;
//                byte[] data;
//                if (this.Send(ph, out  data))
//                {
//                    ph = PacketHelper.Get(data);

//                    NetResult res = ph.GetResult();
//                    if (!res.Success)
//                    {
//                        throw new Exception(res.ErrorCode);
//                    }
//                    return BitConverter.ToInt32(res.Value, 0);
//                }

//                return result;
//            }
//        }

//        public int ExecuteSqlTran(List<Feng.Data.ModleInfo> list)
//        {
//            int result = 0;
//            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
//            {
//                bw.Write(list.Count);
//                foreach (Feng.Data.ModleInfo sqm in list)
//                {
//                    bw.Write(sqm);
//                }
//                PacketHelper ph = new PacketHelper(PacketMainCmd.SQL,
//    PacketSubCmd_SqlCommandSection.ExecuteSqlTranListModleInfo, bw.GetData());
//                ph.PacketMode = (byte)PacketMode.Send;
//                ph.PacketType = PacketType.ToServer;
//                byte[] data;
//                if (this.Send(ph, out  data))
//                {
//                    ph = PacketHelper.Get(data);
//                    NetResult res = ph.GetResult();
//                    if (!res.Success)
//                    {
//                        throw new Exception(res.ErrorCode);
//                    }
//                    return BitConverter.ToInt32(res.Value, 0);
//                }

//                return result;
//            }
//        }

//        public bool ColumnExists(string tableName, string columnName)
//        {
//            string sql = "select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'";
//            object res = GetSingle(sql);
//            if (res == null)
//            {
//                return false;
//            }
//            return Convert.ToInt32(res) > 0;
//        }

//        public int GetMaxID(string FieldName, string TableName)
//        {
//            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
//            object obj = GetSingle(strsql);
//            if (obj == null)
//            {
//                return 1;
//            }
//            else
//            {
//                return int.Parse(obj.ToString());
//            }
//        }

//        public bool Exists(string strSql)
//        {
//            object obj = GetSingle(strSql);
//            int cmdresult;
//            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
//            {
//                cmdresult = 0;
//            }
//            else
//            {
//                cmdresult = int.Parse(obj.ToString());
//            }
//            if (cmdresult == 0)
//            {
//                return false;
//            }
//            else
//            {
//                return true;
//            }
//        }

//        public bool TabExists(string TableName)
//        {
//            string strsql = "select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
//            //string strsql = "SELECT count(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + TableName + "]') AND type in (N'U')";
//            object obj = GetSingle(strsql);
//            int cmdresult;
//            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
//            {
//                cmdresult = 0;
//            }
//            else
//            {
//                cmdresult = int.Parse(obj.ToString());
//            }
//            if (cmdresult == 0)
//            {
//                return false;
//            }
//            else
//            {
//                return true;
//            }
//        }

//        public bool Exists(string strSql, params SqlParameter[] cmdParms)
//        {
//            object obj = GetSingle(strSql, cmdParms);
//            int cmdresult;
//            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
//            {
//                cmdresult = 0;
//            }
//            else
//            {
//                cmdresult = int.Parse(obj.ToString());
//            }
//            if (cmdresult == 0)
//            {
//                return false;
//            }
//            else
//            {
//                return true;
//            }
//        }

//        public System.Data.DataTable Query(string sqlstring, short waittime)
//        {
//            DataTable result = null;

//            PacketHelper ph = new PacketHelper(PacketMainCmd.SQL,
//                PacketSubCmd_SqlCommandSection.Query, System.Text.Encoding.Unicode.GetBytes(sqlstring));
//            ph.WaitTime = waittime;
//            ph.PacketType = PacketType.ToServer;
//            ph.PacketMode = (byte)PacketMode.Send;
//            ph.Compress = true;
//            byte[] data;
//            if (this.Send(ph, out  data))
//            {
//                ph = PacketHelper.Get(data);

//                NetResult res = ph.GetResult();
//                if (!res.Success)
//                {
//                    throw new Exception(res.ErrorCode);
//                }
//                return Feng.IO.SerializableHelper.DataTableFromBinary(res.Value) as System.Data.DataTable;
//            }

//            return result;
//        }

//        public int ExecuteSql(string sqlstring, short waittime)
//        {
//            int result = 0;

//            PacketHelper ph = new PacketHelper(PacketMainCmd.SQL,
//                PacketSubCmd_SqlCommandSection.ExecuteSql, System.Text.Encoding.Unicode.GetBytes(sqlstring));
//            ph.WaitTime = waittime;
//            ph.PacketType = PacketType.ToServer;
//            ph.PacketMode = (byte)PacketMode.Send;
//            byte[] data;
//            if (this.Send(ph, out  data))
//            {
//                ph = PacketHelper.Get(data);

//                NetResult res = ph.GetResult();
//                if (!res.Success)
//                {
//                    throw new Exception(res.ErrorCode);
//                }
//                return BitConverter.ToInt32(res.Value, 0);
//            }

//            return result;
//        }

//        public object GetSingle(string sqlstring, short waittime)
//        {
//            object result = null;

//            PacketHelper ph = new PacketHelper(PacketMainCmd.SQL,
//                PacketSubCmd_SqlCommandSection.GetSingle, System.Text.Encoding.Unicode.GetBytes(sqlstring));
//            ph.WaitTime = waittime;
//            ph.PacketType = PacketType.ToServer;
//            ph.PacketMode = (byte)PacketMode.Send;
//            byte[] data;
//            if (this.Send(ph, out  data))
//            {
//                ph = PacketHelper.Get(data);
//                NetResult res = ph.GetResult();
//                if (!res.Success)
//                {
//                    throw new Exception(res.ErrorCode);
//                }
//                return Feng.IO.SerializableHelper.DeSerializeFromBinary(res.Value);
//            }

//            return result;
//        }

//        public System.Data.DataTable Query(string sqlstring, short waittime, params SqlParameter[] cmdParms)
//        {
//            System.Data.DataTable result = null;
//            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
//            {
//                bw.Write(sqlstring);
//                bw.Write(cmdParms.Length);
//                foreach (SqlParameter sqm in cmdParms)
//                {
//                    bw.Write(sqm);
//                }
//                PacketHelper ph = new PacketHelper(PacketMainCmd.SQL,
//                    PacketSubCmd_SqlCommandSection.QueryBySqlParameter, bw.GetData());
//                ph.WaitTime = waittime;
//                ph.PacketType = PacketType.ToServer;
//                ph.PacketMode = (byte)PacketMode.Send;
//                ph.Compress = true;
//                byte[] data;
//                if (this.Send(ph, out  data))
//                {
//                    ph = PacketHelper.Get(data);
//                    NetResult res = ph.GetResult();
//                    if (!res.Success)
//                    {
//                        throw new Exception(res.ErrorCode);
//                    }

//                    return Feng.IO.SerializableHelper.DataTableFromBinary(res.Value) as System.Data.DataTable;
//                }

//                return result;
//            }
//        }

//        public object GetSingle(string sqlstring, short waittime, params SqlParameter[] cmdParms)
//        {
//            object result = null;
//            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
//            {
//                bw.Write(sqlstring);
//                bw.Write(cmdParms.Length);
//                foreach (SqlParameter sqm in cmdParms)
//                {
//                    bw.Write(sqm);
//                }
//                PacketHelper ph = new PacketHelper(PacketMainCmd.SQL,
//                    PacketSubCmd_SqlCommandSection.GetSingleBySqlParameter, bw.GetData());
//                ph.WaitTime = waittime;
//                ph.PacketType = PacketType.ToServer;
//                ph.PacketMode = (byte)PacketMode.Send;
//                byte[] data;
//                if (this.Send(ph, out  data))
//                {
//                    ph = PacketHelper.Get(data);
//                    NetResult res = ph.GetResult();
//                    if (!res.Success)
//                    {
//                        throw new Exception(res.ErrorCode);
//                    }
//                    return Feng.IO.SerializableHelper.DeSerializeFromBinary(res.Value);
//                }

//                return result;
//            }
//        }

//        public int ExecuteSql(string sqlstring, short waittime, params SqlParameter[] cmdParms)
//        {
//            int result = 0;
//            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
//            {
//                bw.Write(sqlstring);
//                bw.Write(cmdParms.Length);
//                foreach (SqlParameter sqm in cmdParms)
//                {
//                    bw.Write(sqm);
//                }
//                PacketHelper ph = new PacketHelper(PacketMainCmd.SQL,
//                    PacketSubCmd_SqlCommandSection.ExecuteSqlBySqlParameter, bw.GetData());
//                ph.WaitTime = waittime;
//                ph.PacketType = PacketType.ToServer;
//                ph.PacketMode = (byte)PacketMode.Send;
//                byte[] data;
//                if (this.Send(ph, out  data))
//                {
//                    ph = PacketHelper.Get(data);
//                    NetResult res = ph.GetResult();
//                    if (!res.Success)
//                    {
//                        throw new Exception(res.ErrorCode);
//                    }
//                    return BitConverter.ToInt32(res.Value, 0);
//                }

//                return result;
//            }
//        }

//        public int ExecuteSqlTran(List<String> sqlstringList, short waittime)
//        {
//            int result = 0;
//            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
//            {
//                bw.Write(sqlstringList.Count);
//                foreach (string sqm in sqlstringList)
//                {
//                    bw.Write(sqm);
//                }
//                PacketHelper ph = new PacketHelper(PacketMainCmd.SQL,
//    PacketSubCmd_SqlCommandSection.ExecuteSqlTran, bw.GetData());
//                ph.PacketType = PacketType.ToServer;
//                ph.WaitTime = waittime;
//                ph.PacketMode = (byte)PacketMode.Send;
//                byte[] data;
//                if (this.Send(ph, out  data))
//                {
//                    ph = PacketHelper.Get(data);
//                    NetResult res = ph.GetResult();
//                    if (!res.Success)
//                    {
//                        throw new Exception(res.ErrorCode);
//                    }
//                    return BitConverter.ToInt32(res.Value, 0);
//                }

//                return result;
//            }
//        }

//        public int ExecuteSqlTran(List<Feng.Data.ModleInfo> list, short waittime)
//        {
//            int result = 0;
//            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
//            {
//                bw.Write(list.Count);
//                foreach (Feng.Data.ModleInfo sqm in list)
//                {
//                    bw.Write(sqm);
//                }
//                PacketHelper ph = new PacketHelper(PacketMainCmd.SQL,
//    PacketSubCmd_SqlCommandSection.ExecuteSqlTran, bw.GetData());
//                ph.PacketType = PacketType.ToServer;
//                ph.WaitTime = waittime;
//                ph.PacketMode = (byte)PacketMode.Send;
//                byte[] data;
//                if (this.Send(ph, out  data))
//                {
//                    ph = PacketHelper.Get(data);
//                    NetResult res = ph.GetResult();
//                    if (!res.Success)
//                    {
//                        throw new Exception(res.ErrorCode);
//                    }
//                    return BitConverter.ToInt32(res.Value, 0);
//                }

//                return result;
//            }
//        }

//        public bool ColumnExists(string tableName, string columnName, short waittime)
//        {
//            string sql = "select count(1) from syscolumns where [id]=object_id('" + tableName + "') and [name]='" + columnName + "'";
//            object res = GetSingle(sql, waittime);
//            if (res == null)
//            {
//                return false;
//            }
//            return Convert.ToInt32(res) > 0;
//        }

//        public int GetMaxID(string FieldName, string TableName, short waittime)
//        {
//            string strsql = "select max(" + FieldName + ")+1 from " + TableName;
//            object obj = GetSingle(strsql, waittime);
//            if (obj == null)
//            {
//                return 1;
//            }
//            else
//            {
//                return int.Parse(obj.ToString());
//            }
//        }

//        public bool Exists(string strSql, short waittime)
//        {
//            object obj = GetSingle(strSql, waittime);
//            int cmdresult;
//            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
//            {
//                cmdresult = 0;
//            }
//            else
//            {
//                cmdresult = int.Parse(obj.ToString());
//            }
//            if (cmdresult == 0)
//            {
//                return false;
//            }
//            else
//            {
//                return true;
//            }
//        }

//        public bool TabExists(string TableName, short waittime)
//        {
//            string strsql = "select count(*) from sysobjects where id = object_id(N'[" + TableName + "]') and OBJECTPROPERTY(id, N'IsUserTable') = 1";
//            //string strsql = "SELECT count(*) FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + TableName + "]') AND type in (N'U')";
//            object obj = GetSingle(strsql, waittime);
//            int cmdresult;
//            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
//            {
//                cmdresult = 0;
//            }
//            else
//            {
//                cmdresult = int.Parse(obj.ToString());
//            }
//            if (cmdresult == 0)
//            {
//                return false;
//            }
//            else
//            {
//                return true;
//            }
//        }

//        public bool Exists(string strSql, short waittime, params SqlParameter[] cmdParms)
//        {
//            object obj = GetSingle(strSql, waittime, cmdParms);
//            int cmdresult;
//            if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
//            {
//                cmdresult = 0;
//            }
//            else
//            {
//                cmdresult = int.Parse(obj.ToString());
//            }
//            if (cmdresult == 0)
//            {
//                return false;
//            }
//            else
//            {
//                return true;
//            }
//        }

//    }
//}