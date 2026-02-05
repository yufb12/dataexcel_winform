namespace Feng.Net.Packets
{
    public class PacketSystemSubCmd
    {
        public const int Connection = 1;
        public const int RegeditSession = 2;
        public const int UserChanged = 3;
        public const int GetOnlineUser = 4;
        public const int Heartbeat = 5;
        public const int Close = 6;
        public const int CheckVersion = 7;
        public const int Text = 8;
        public const int Regedit = 9;
        public const int Login = 10;
        public const int CheckOnLine = 11;
        public const int AutoUpdate = 12;
        public const int RecvFile = 13;
        public const int Changed = 14;
        public const int OtherLogin = 15;//其他地方登录 
        public const int Execute = 16;
        public const int Attach = 17;
        public const int ComeInRoom = 18;
        public const int ComeOutRoom = 19;
        public const int AppUpdate = 20;
        public const int ServerTime = 21;
        public const int ModifyPassword = 22;
        public const int Ping = 23;
        public const int Pang = 24;
        public const int GetServer = 25;
        public const int ChangedServer = 26;
        public const int DoCommand = 27;
    }

    public static class PacketFileSubCmd
    {
        public const int CreatePath = 0x0003;
        public const int GetDirectorys = 0x0004;
        public const int GetFiles = 0x0005;
        public const int GetFileInfo = 0x0006;
        public const int DeleteDirectory = 0x0007;
        public const int DeleteFile = 0x0008;
        public const int SendFile = 0x0009;
        public const int DownFileName = 0x000B;
        public const int Default = 0x000D; 
        public const int Query = 0x0012;
        public const int Execute = 0x0013;
        public const int GetUdpServerInfo = 0x0019;
        public const int GetUdpServerNatInfo = 0x0020; 
         
        public const int DecryptFile = 0x0022;
        public const int EncryptFile = 0x0023; 
        public const int ExistsFile = 0x0025;
        public const int CopyFile = 0x0026;
        public const int PasteFile = 0x0027;
        public const int CutFile = 0x0028;
        public const int MoveDirectorye = 0x0029;
        public const int GetDirectoryInfo = 0x0030;
        public const int MoveFile = 0x0031;
        public const int DownloadFile = 0x0032;
        public const int CreateFile = 0x0034;
        public const int EditFile = 0x0035;
        public const int BeginUploadFile= 0x0051;
        public const int SequenceUploadFile = 0x0052;
        public const int EndUploadFile = 0x0053;


        public const int BeginDownLoadFile = 0x0061;
        public const int SequenceDownLoadFile = 0x0062;
        public const int EndDownLoadFile = 0x0063;
        public const int DownLoadFilePart = 0x0132;
    }
    public static class PacketSubCmd_SqlCommandSection
    {
        public const int ExecuteSql = 0x1001;
        public const int ExecuteSqlBySqlParameter = 0x1002;
        public const int ExecuteSqlTran = 0x1003;
        public const int ExecuteSqlTranListModleInfo = 0x1004;
        public const int GetSingle = 0x1005;
        public const int GetSingleBySqlParameter = 0x1006;
        public const int Query = 0x1007;
        public const int QueryBySqlParameter = 0x1008;
        public const int QueryTable = 0x1009;
        public const int QueryTableBySqlParameter = 0x1010;
        public const int BackDataBase = 0x1011;
    }
    public static class PacketSubCmd_UdpCommandSection
    {
        public const int ConnectionUdpServer = 0x2001;
        public const int CheckClientIP = 0x2002;
        public const int OpenUdpServer = 0x2003;
        public const int Ping = 0x2004;
        public const int Pang = 0x2006;  
        public const int GetUdpRemoteLoaclInfo = 0x2009;
        public const int GetRemoteClientAddInfo = 0x2010;
        public const int TellRomteAddress = 0x2011;
        public const int RespondTLRMMyAdd = 0x2012;
        public const int FirstP2PConnect = 0x2013;
        public const int FirstP2P = 0x2014;
        public const int P2PSuccess = 0x2015; 
        public const int SC_TLRMMyAdd = 0x3011;  
        public const int SC_BeginP2P = 0x3015;
    }
    public static class PacketSubCmd_DataProjectCommandSection 
    { 
        public const int GetRoles = 0x0001;
        public const int Getusers = 0x0002;
        public const int CreateRole = 0x0003;
        public const int CreateUser = 0x0004;
        public const int DeleteRole = 0x0005;
        public const int DeleteUser = 0x0006;
        public const int UpdateRole = 0x0007;
        public const int UpdateUser = 0x0008;
        public const int ChangedPassword = 0x0009;
        public const int RoleFilePermission = 0x0010;
        public const int UserFilePermission = 0x0011;
        public const int GetRolePermission = 0x0012;
        public const int GetUserFilePermission = 0x0013;
        public const int GetRoleDirectoryPermission = 0x0014;
        public const int GetUserDirectoryPermission = 0x0015;
        public const int EditRoleDirectoryPermission = 0x0016;
        public const int EditUserDirectoryPermission = 0x0017;

        public const int GetRole = 0x0018;
        public const int Getuser = 0x0019;
        public const int GetUserByName = 0x0020;
        public const int GetLogs = 0x0021;
        public const int GetUserDirectoryAllPermission = 0x0022;
        public const int GetRoleDirectoryAllPermission = 0x0023;

        public const int GetUserDownLoadPermission = 0x0024;
        public const int GetUserUpLoadPermission = 0x0025;
        public const int GetCompany = 0x0026;
        public const int Regedit = 0x0027;
        public const int GetRegedit = 0x0028;
        public const int AddProject = 0x0029;
        public const int AddSubProject = 0x0030;
        public const int AddModule = 0x0031;
        public const int GetNumID = 0x0032;
        public const int GetProjectList = 0x0033;
        public const int AddTable = 0x0034;
        public const int UpdateTable = 0x0035;
        public const int DelTable = 0x0036;
        public const int GetRegeditEmplyee = 0x0037;
        public const int GetTableList = 0x0038;
        public const int GetTabletColumnsList = 0x0039;
        public const int UpLoadtemplatesFile = 0x0040;
        public const int GetNubIDList = 0x0041;
        public const int AddNubIDList = 0x0042;
        public const int GetNumIDByModuleID = 0x0043;
        public const int GetTemplates = 0x0044;
        public const int SetPrimaryModuleTable = 0x0045;
        public const int GetPrimaryModuleTable = 0x0046;
        public const int GetTemplateFile = 0x0047;
        public const int InsertTableValue = 0x0048;
        public const int GetSetting = 0x0049;
        public const int SetSetting = 0x0050;
        public const int DelTemplatesFile = 0x0051;
        public const int GetAllTableList = 0x0052;
        public const int QueryTable = 0x0053;
        public const int UpLoadQueryFile = 0x0054;
        public const int EditFileName = 0x0055;
        public const int QueryTableByArgs = 0x0056;
        public const int DelTableRow = 0x0057;
        public const int QueryTableByTableNameAndArgs = 0x0058;
        public const int QueryTableBySql = 0x0059;
        public const int DeleteSubProject = 0x0060;
        public const int DeleteModule = 0x0061;
        public const int DeleteProject = 0x0062;
        public const int ReNameProjectName = 0x0063;
        public const int GetNumIDByName = 0x0064;
        public const int ImportTable = 0x0065;
        public const int ClearTableData = 0x0066;
        public const int ClearTable = 0x0067;
        public const int QuerCustomFunction = 0x0068;
        public const int CustomFunction_Add = 0x0069;
        public const int CustomFunction_Execute = 0x0070;
        public const int DeleteTableRowBySys_ID = 0x0071;
        public const int GetAutoNumber = 0x0072;
        public const int DownLoadServerFile = 0x0073;
        public const int GetAllUserTemplates = 0x0074;
        public const int GetAllTemplates = 0x0075;
        public const int GetSystemPaths = 0x0076;
        public const int GetSystemPathFiles = 0x0077;
    }

    public class NetState
    {
        public static long SendCount = 0;
        public static long SendTimes = 0;
        public static long RecvCount = 0;
        public static long RecvTimes = 0; 
        public static long RecvCacheTimes = 0;
        public static long DiscardCount = 0;
        public static long DiscardTimes = 0;
        public static object lck = new object();
        public static void Send(int count)
        {
            lock (lck)
            {
                SendCount += count;
                SendTimes++;
            }
        }
        public static void Recv(int count)
        {
            lock (lck)
            {
                RecvCount += count;
                RecvTimes++;
                RecvCacheTimes++;
            }
        }
        public static void RecvDo()
        {
            lock (lck)
            { 
                RecvCacheTimes--;
            }
        }
 
        public static void Discard(int count)
        {
            lock (lck)
            {
                DiscardCount += count;
                DiscardTimes++;
            }
        }
    }
     
}
//3000以下不可丢协议
//3000~20000 缓冲协议
//20000~50000 Query
//50000~100000 Log