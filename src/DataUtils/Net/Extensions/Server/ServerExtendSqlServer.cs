using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading; 
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Data;
using Feng.Data;
using Feng.Net.NetArgs;
using Feng.Net.Packets;

namespace Feng.Net.Extend
{
   
    public class ServerExtendSqlServer : ServerExtendBase
    {
        public ServerExtendSqlServer(Base.NetServer server) : base(server)
        {

        }
        private IDbHelper _dbhelper = null;
        public IDbHelper DbHelper
        {
            get
            {
                if (_dbhelper == null)
                {
                    _dbhelper = new Feng.Data.MsSQL.MsDbHelper();
                }
                return _dbhelper;
            }
            set { _dbhelper = value; }
        }
        public override void DoExtendCommand(NetPacket ph, ReciveEventArgs e)
        {
            this.OnReceiveUserSQLData(ph, e);
            if (e.Cancel)
            {
                return;
            }
            int sqcommand = ph.PacketSubcommand;
            switch (sqcommand)
            {
                case PacketSubCmd_SqlCommandSection.ExecuteSqlBySqlParameter:
                    ExecuteSqlBySqlParameter(ph, e);
                    return;
                case PacketSubCmd_SqlCommandSection.ExecuteSql:
                    ExecuteSql(ph, e);
                    return;
                case PacketSubCmd_SqlCommandSection.ExecuteSqlTran:
                    ExecuteSqlTran(ph, e);
                    return;
                case PacketSubCmd_SqlCommandSection.ExecuteSqlTranListModleInfo:
                    ExecuteSqlTranListModleInfo(ph, e);
                    return;
                case PacketSubCmd_SqlCommandSection.GetSingle:
                    GetSingle(ph, e);
                    return;
                case PacketSubCmd_SqlCommandSection.GetSingleBySqlParameter:
                    GetSingleBySqlParameter(ph, e);
                    return;
                case PacketSubCmd_SqlCommandSection.Query:
                    Query(ph, e);
                    return;
                case PacketSubCmd_SqlCommandSection.QueryTable:
                    QueryTable(ph, e);
                    return;
                case PacketSubCmd_SqlCommandSection.QueryBySqlParameter:
                    QueryBySqlParameter(ph, e);
                    return;
                case PacketSubCmd_SqlCommandSection.BackDataBase:
                    BackDataBase(ph, e);
                    return;
                default:
                    break;
            }
            return;
        }


        public virtual bool DoSQL(ReciveEventArgs e, NetPacket ph)
        {
            //this.Server.OnReceiveUserSQLData(ph, e);
            //if (e.Cancel)
            //{
            //    return false;
            //}
            int sqcommand = ph.PacketSubcommand;
            switch (sqcommand)
            {
                //case PacketSubCmd_SqlCommandSection.ExecuteSqlBySqlParameter:
                //    ExecuteSqlBySqlParameter(ph, e);
                //    return true;
                //case PacketSubCmd_SqlCommandSection.ExecuteSql:
                //    ExecuteSql(ph, e);
                //    return true;
                //case PacketSubCmd_SqlCommandSection.ExecuteSqlTran:
                //    ExecuteSqlTran(ph, e);
                //    return true;
                //case PacketSubCmd_SqlCommandSection.ExecuteSqlTranListModleInfo:
                //    ExecuteSqlTranListModleInfo(ph, e);
                //    return true;
                //case PacketSubCmd_SqlCommandSection.GetSingle:
                //    GetSingle(ph, e);
                //    return true;
                //case PacketSubCmd_SqlCommandSection.GetSingleBySqlParameter:
                //    GetSingleBySqlParameter(ph, e);
                //    return true;
                //case PacketSubCmd_SqlCommandSection.Query:
                //    Query(ph, e);
                //    return true;
                //case PacketSubCmd_SqlCommandSection.QueryTable:
                //    QueryTable(ph, e);
                //    return true;
                //case PacketSubCmd_SqlCommandSection.QueryBySqlParameter:
                //    QueryBySqlParameter(ph, e);
                //    return true;
                //case PacketSubCmd_SqlCommandSection.BackDataBase:
                //    BackDataBase(ph, e);
                //    return true;
                default:
                    break;
            }
            return false;
        }

        public delegate void ReceiveUserSQLDataEventHandler(object sender, NetPacket ph, ReciveEventArgs e);
        public event ReceiveUserSQLDataEventHandler E_ReceiveUserSQLData;
        public virtual void OnReceiveUserSQLData(NetPacket ph, ReciveEventArgs e)
        {
            if (E_ReceiveUserSQLData != null)
            {
                E_ReceiveUserSQLData(this, ph, e);
            }
        }
        public delegate void ExecuteSqlTranListModleInfoEventHandler(object sender, NetPacket ph, ReciveEventArgs e);
        public event ExecuteSqlTranListModleInfoEventHandler E_ExecuteSqlTranListModleInfo;
        public virtual void OnExecuteSqlTranListModleInfo(NetPacket ph, ReciveEventArgs e)
        {
            if (E_ExecuteSqlTranListModleInfo != null)
            {
                E_ExecuteSqlTranListModleInfo(this, ph, e);
            }
        }
        public delegate void ExecuteSqlTranEventHandler(object sender, NetPacket ph, ReciveEventArgs e);
        public event ExecuteSqlTranEventHandler E_ExecuteSqlTran;
        public virtual void OnExecuteSqlTran(NetPacket ph, ReciveEventArgs e)
        {
            if (E_ExecuteSqlTran != null)
            {
                E_ExecuteSqlTran(this, ph, e);
            }
        }
        public delegate void ExecuteSqlEventHandler(object sender, NetPacket ph, ReciveEventArgs e);
        public event ExecuteSqlEventHandler E_ExecuteSql;
        public virtual void OnExecuteSql(NetPacket ph, ReciveEventArgs e)
        {
            if (E_ExecuteSql != null)
            {
                E_ExecuteSql(this, ph, e);
            }
        }
        public delegate void QueryEventHandler(object sender, NetPacket ph, ReciveEventArgs e);
        public event QueryEventHandler E_Query;
        public virtual void OnQuery(NetPacket ph, ReciveEventArgs e)
        {
            if (E_Query != null)
            {
                E_Query(this, ph, e);
            }
        }
        public delegate void QueryTableEventHandler(object sender, NetPacket ph, ReciveEventArgs e);
        public event QueryTableEventHandler E_QueryTable;
        public virtual void OnQueryTable(NetPacket ph, ReciveEventArgs e)
        {
            if (E_QueryTable != null)
            {
                E_QueryTable(this, ph, e);
            }
        }
        public delegate void GetSingleEventHandler(object sender, NetPacket ph, ReciveEventArgs e);
        public event GetSingleEventHandler E_GetSingle;
        public virtual void OnGetSingle(NetPacket ph, ReciveEventArgs e)
        {
            if (E_GetSingle != null)
            {
                E_GetSingle(this, ph, e);
            }
        }
        public delegate void QueryBySqlParameterEventHandler(object sender, NetPacket ph, ReciveEventArgs e);
        public event QueryBySqlParameterEventHandler E_QueryBySqlParameter;
        public virtual void OnQueryBySqlParameter(NetPacket ph, ReciveEventArgs e)
        {
            if (E_QueryBySqlParameter != null)
            {
                E_QueryBySqlParameter(this, ph, e);
            }
        }
        public delegate void ExecuteSqlBySqlParameterEventHandler(object sender, NetPacket ph, ReciveEventArgs e);
        public event ExecuteSqlBySqlParameterEventHandler E_ExecuteSqlBySqlParameter;
        public virtual void OnExecuteSqlBySqlParameter(NetPacket ph, ReciveEventArgs e)
        {
            if (E_ExecuteSqlBySqlParameter != null)
            {
                E_ExecuteSqlBySqlParameter(this, ph, e);
            }
        }
        public delegate void GetSingleBySqlParameterEventHandler(object sender, NetPacket ph, ReciveEventArgs e);
        public event GetSingleBySqlParameterEventHandler E_GetSingleBySqlParameter;
        public virtual void OnGetSingleBySqlParameter(NetPacket ph, ReciveEventArgs e)
        {
            if (E_GetSingleBySqlParameter != null)
            {
                E_GetSingleBySqlParameter(this, ph, e);
            }
        }

        public delegate void BackDataBaseEventHandler(object sender, NetPacket ph, ReciveEventArgs e);
        public event BackDataBaseEventHandler E_BackDataBase;
        public virtual void OnBackDataBase(NetPacket ph, ReciveEventArgs e)
        {
            if (E_BackDataBase != null)
            {
                E_BackDataBase(this, ph, e);
            }
        }

        public virtual void GetSingleBySqlParameter(NetPacket ph, ReciveEventArgs e)
        {
            try
            {
                this.OnGetSingleBySqlParameter(ph, e);
                if (e.Cancel)
                {
                    return;
                }
                string sql = string.Empty;
                byte[] buufer = null;
                using (Feng.IO.BufferReader bw = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    sql = bw.ReadString();
                    int len = bw.ReadInt32();
                    List<SqlParameter> listsqm = new List<SqlParameter>();
                    SqlParameter sqm = null;
                    for (int i = 0; i < len; i++)
                    {
                        sqm = bw.ReadSqlParameter();
                        listsqm.Add(sqm);
                    }
                    object ds = this.DbHelper.GetSingle(sql, listsqm.ToArray());
                    buufer = Feng.IO.SerializableHelper.SerializeToBinary(ds); 
                }
                ph.WriteResult(true, string.Empty, buufer);
            }
            catch (Exception ex)
            {
                ph.WriteResult(false, ex.Message, null);

            }
            e.ClientProxy.Respond(ph);

        }

        public virtual void ExecuteSqlBySqlParameter(NetPacket ph, ReciveEventArgs e)
        {
            try
            {
                this.OnExecuteSqlBySqlParameter(ph, e);
                if (e.Cancel)
                {
                    return;
                }

                string sql = string.Empty;
                using (Feng.IO.BufferReader bw = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    sql = bw.ReadString();
                    int len = bw.ReadInt32();
                    List<SqlParameter> listsqm = new List<SqlParameter>();
                    SqlParameter sqm = null;
                    for (int i = 0; i < len; i++)
                    {
                        sqm = bw.ReadSqlParameter();
                        listsqm.Add(sqm);
                    }
                    int ds = this.DbHelper.ExecuteSql(sql, listsqm.ToArray());
                    byte[] buufer = Feng.IO.SerializableHelper.SerializeToBinary(ds); 
                    ph.WriteResult(true, string.Empty, buufer);
                }
            }
            catch (Exception ex)
            {
                ph.WriteResult(false, ex.Message, null);

            }
            e.ClientProxy.Respond(ph);
        }

        public virtual void BackDataBase(NetPacket ph, ReciveEventArgs e)
        {
            this.OnBackDataBase(ph, e);

            if (e.Cancel)
            {
                return;
            }

            using (Feng.IO.BufferReader bw = new Feng.IO.BufferReader(ph.PacketContents))
            {
                string database = bw.ReadString();
                string path =Feng.IO.FileHelper.GetStartUpFileUSER("ServerExtend",  "\\Temp\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".bak");
                string sql = "backup database [" + database + "] to disk='" + path + "';";
                Feng.Data.MsSQL.DbHelperSQL.ExecuteSql(sql);
                byte[] data = System.IO.File.ReadAllBytes(path);
                ph.PacketContents = data;
                e.ClientProxy.Respond(ph);
            }
        }

        public virtual void QueryBySqlParameter(NetPacket ph, ReciveEventArgs e)
        {
            try
            {
                this.OnQueryBySqlParameter(ph, e);
                if (e.Cancel)
                {
                    return;
                }

                string sql = string.Empty;
                byte[] buufer = null;
                using (Feng.IO.BufferReader bw = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    sql = bw.ReadString();
                    int len = bw.ReadInt32();
                    List<SqlParameter> listsqm = new List<SqlParameter>();
                    SqlParameter sqm = null;
                    for (int i = 0; i < len; i++)
                    {
                        sqm = bw.ReadSqlParameter();
                        listsqm.Add(sqm);
                    }
                    DataTable ds = this.DbHelper.Query(sql, listsqm.ToArray());
                    buufer = Feng.IO.SerializableHelper.DataTableToBinary(ds);
                } 
                ph.WriteResult(true, string.Empty, buufer);
            }
            catch (Exception ex)
            {
                ph.WriteResult(false, ex.Message, null);

            }
            e.ClientProxy.Respond(ph); 
        }

        public virtual void GetSingle(NetPacket ph, ReciveEventArgs e)
        {
            try
            {
                this.OnGetSingle(ph, e);
                if (e.Cancel)
                {
                    return;
                }
                string sql = Feng.IO.BitConver.GetString(ph.PacketContents);
                ph.PacketContents = new byte[0]; 
                object ds = this.DbHelper.GetSingle(sql);
                byte[] buufer = Feng.IO.SerializableHelper.SerializeToBinary(ds);
                ph.WriteResult(true, string.Empty, buufer);
            }
            catch (Exception ex)
            {
                ph.WriteResult(false, ex.Message, null);

            }
            e.ClientProxy.Respond(ph);



        }

        public virtual void QueryTable(NetPacket ph, ReciveEventArgs e)
        {
            try
            {

                string sql = Feng.IO.BitConver.GetString(ph.PacketContents);
                ph.PacketContents = new byte[0];
                ph.PacketMode = 3;
                System.Data.DataTable ds = this.DbHelper.Query(sql);
                byte[] buufer = Feng.IO.SerializableHelper.DataTableToBinary(ds);
                ph.WriteResult(true, string.Empty, buufer);
            }
            catch (Exception ex)
            {
                ph.WriteResult(false, ex.Message, null);

            }
            e.ClientProxy.Send(ph);
            this.OnQueryTable(ph, e);
            if (e.Cancel)
            {
                return;
            }



        }

        public virtual void Query(NetPacket ph, ReciveEventArgs e)
        {
            try
            {
                this.OnQuery(ph, e);
                if (e.Cancel)
                {
                    return;
                }
                string sql = Feng.IO.BitConver.GetString(ph.PacketContents);
                ph.PacketContents = new byte[0]; 
                System.Data.DataTable ds = this.DbHelper.Query(sql);
                byte[] buufer = Feng.IO.SerializableHelper.DataTableToBinary(ds);
                ph.WriteResult(true, string.Empty, buufer);
            }
            catch (Exception ex)
            {
                ph.WriteResult(false, ex.Message, null);

            }
            e.ClientProxy.Respond(ph);
        }

        public virtual void ExecuteSql(NetPacket ph, ReciveEventArgs e)
        {

            try
            {
                this.OnExecuteSql(ph, e);
                if (e.Cancel)
                {
                    return;
                }
                string sql = Feng.IO.BitConver.GetString(ph.PacketContents);
                ph.PacketContents = new byte[0];
                ph.PacketMode = 3;
                int resexec = this.DbHelper.ExecuteSql(sql);
                byte[] buufer = BitConverter.GetBytes(resexec);
                ph.WriteResult(true, string.Empty, buufer);
            }
            catch (Exception ex)
            {
                ph.WriteResult(false, ex.Message, null);

            }
            e.ClientProxy.Respond(ph);
        }

        public virtual void ExecuteSqlTran(NetPacket ph, ReciveEventArgs e)
        {
            try
            { 
                this.OnExecuteSqlTran(ph, e);
                if (e.Cancel)
                {
                    ph.WriteResult(false, "Cancel", null);
                    e.ClientProxy.Respond(ph); 
                    return;
                }
                using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
                {
                    int len = br.ReadInt32();
                    List<string> sqllist = new List<string>();
                    for (int i = 0; i < len; i++)
                    {
                        sqllist.Add(br.ReadString()); 
                    }

                    ph.PacketContents = new byte[0]; 
                    int resexec = this.DbHelper.ExecuteSqlTran(sqllist);
                    byte[] buufer = BitConverter.GetBytes(resexec);
                    ph.WriteResult(true, string.Empty, buufer);
                }
            }
            catch (Exception ex)
            {
                ph.WriteResult(false, ex.Message, null); 
            }
            e.ClientProxy.Respond(ph); 
        }

        public virtual void ExecuteSqlTranListModleInfo(NetPacket ph, ReciveEventArgs e)
        {

            this.OnExecuteSqlTranListModleInfo(ph, e);
            if (e.Cancel)
            {
                ph.WriteResult(false, "Cancel", null);
                e.ClientProxy.Respond(ph);
                return;
            }

            using (Feng.IO.BufferReader br = new Feng.IO.BufferReader(ph.PacketContents))
            {
                try
                {
                    int len = br.ReadInt32();
                    List<Feng.Data.ModleInfo> sqllist = new List<Feng.Data.ModleInfo>();
                    for (int i = 0; i < len; i++)
                    {
                        sqllist.Add(br.ReadModleInfo());

                    }

                    ph.PacketContents = new byte[0];
                    ph.PacketMode = 3;
                    int resexec = this.DbHelper.ExecuteSqlTran(sqllist);
                    byte[] buufer = BitConverter.GetBytes(resexec);
                    ph.WriteResult(true, string.Empty, buufer);
                }
                catch (Exception ex)
                {
                    ph.WriteResult(false, ex.Message, null);

                }
                e.ClientProxy.Respond(ph);

            }
        }
    }

}
