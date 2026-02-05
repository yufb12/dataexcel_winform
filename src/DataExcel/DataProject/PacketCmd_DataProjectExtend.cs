
namespace Feng.DataTool.DataProject
{
    public class PacketCmd_DataProjectExtend
    {
        public const short PacketMainCmd_DataProjectExtend = 0x1030;
        public const short DataProjectSubCmd_GetProjectItems = 0x1031;
        public const short DataProjectSubCmd_SaveFile = 0x1032;
        public const short DataProjectSubCmd_EditTempFile = 0x1033;
        public const short DataProjectSubCmd_GetTempItems = 0x1034;
        public const short DataProjectSubCmd_GetProjectValues = 0x1035;
        public const short DataProjectSubCmd_GetTempllateFields = 0x1036;
        public const short DataProjectSubCmd_GetLoginTemplate = 0x1037;
        public const short DataProjectSubCmd_Login = 0x1038;
        public const short DataProjectSubCmd_FileDel = 0x1039;
        public const short DataProjectSubCmd_FileNew = 0x1040;
        public const short DataProjectSubCmd_Project_Update = 0x1041;

        public const short DataProjectSubCmd_FExecUrlScript = 0x1043;
        public const short DataProjectSubCmd_SaveTemplateData = 0x1051;
        public const short DataProjectSubCmd_QueryDirectory = 0x1052;
        public const short DataProjectSubCmd_EditPermission = 0x1053;
        public const short DataProjectSubCmd_GetPermission = 0x1054;
        public const short DataProjectSubCmd_GetRoleList = 0x1061;
        public const short DataProjectSubCmd_GetSetting = 0x1071;
        public const short DataProjectSubCmd_ChangedPwd = 0x1072;
        public const short DataProjectSubCmd_GetUrlData = 0x1073;
        public const short DataProjectSubCmd_FileData = 0x1083;
        public const short DataProjectSubCmd_FileEdit = 0x1084; 
        public const short DataProjectSubCmd_OpenUrl = 0x1085; 
        public const short DataProjectSubCmd_FExec = 0x1086; 
        public const short DataProjectSubCmd_FDataFill = 0x1087; 
        public const short DataProjectSubCmd_FExecUrlID = 0x1088; 
        public const short DataProjectSubCmd_FExecModeles = 0x1089; 
        public const short DataProjectSubCmd_FExecModel = 0x1090; 
        public const short DataProjectSubCmd_LoginOut = 0x1091; 
        public const short DataProjectSubCmd_PFDataExist = 0x1092; 
        public const short DataProjectSubCmd_PFGetUrlID = 0x1093;
        public const short DataProjectSubCmd_PFNUM = 0x1094;
        public const short DataProjectSubCmd_PFQueryFile = 0x1095;
        public const short DataProjectSubCmd_QueryProjectValues = 0x1096;
        public const short DataProjectSubCmd_TemplateExits = 0x1097;
        /// <summary>
        /// 加
        /// </summary>
        public const short DataProjectSubCmd_PFADD = 0x1101;
        /// <summary>
        /// 减
        /// </summary>
        public const short DataProjectSubCmd_PFSubtract = 0x1102;
        /// <summary>
        /// 乘
        /// </summary>
        public const short DataProjectSubCmd_PFMultiply = 0x1103;
        /// <summary>
        /// 除
        /// </summary>
        public const short DataProjectSubCmd_PFDivide = 0x1104;
        /// <summary>
        /// 加
        /// </summary>
        public const short DataProjectSubCmd_PFRowValueADD = 0x1111;
        /// <summary>
        /// 减
        /// </summary>
        public const short DataProjectSubCmd_PFRowValueSubtract = 0x1112;
        /// <summary>
        /// 乘
        /// </summary>
        public const short DataProjectSubCmd_PFRowValueMultiply = 0x1113;
        /// <summary>
        /// 除
        /// </summary>
        public const short DataProjectSubCmd_PFRowValueDivide = 0x1114;
        public const short DataProjectSubCmd_GetUserList = 0x1215;
        public const short DataProjectSubCmd_FileSendTo = 0x1216;
        public const short DataProjectSubCmd_GetUserRecvFile = 0x1217;
        public const short DataProjectSubCmd_GetUserSendFile = 0x1218;
        public const short DataProjectSubCmd_GetUserFavFile = 0x1219;
        public const short DataProjectSubCmd_UrlLocked = 0x1220;
        public const short DataProjectSubCmd_UrlUnLocked = 0x1221;
        public const short DataProjectSubCmd_GetUrlLocked = 0x1222;
        public const short DataProjectSubCmd_GetServerName = 0x1223;
        public const short DataProjectSubCmd_EditPermissionButton = 0x1224;
        public const short DataProjectSubCmd_GetPermissionButton = 0x1225;
        public const short DataProjectSubCmd_GetDataPermission = 0x1227;
        public const short DataProjectSubCmd_EditDataPermission = 0x1229;
        public const short DataProjectSubCmd_UrlUnLockRequest = 0x1230;
        public const short DataProjectSubCmd_GetUserFile = 0x1231;
        public const short DataProjectSubCmd_FileNewPart = 0x1232;
        public const short DataProjectSubCmd_GetCellDataTable = 0x1233;
        public const short DataProjectSubCmd_GetIDDataTable = 0x1234;
        public const short DataProjectSubCmd_GetIDAndCellDataTable = 0x1235;
        public const short DataProjectSubCmd_PFFindRow = 0x1236;
        public const short DataProjectSubCmd_PFGridGetDataTable = 0x1237;
        public const short DataProjectSubCmd_PFDataTotal = 0x1238;
        public const short DataProjectSubCmd_PFGetTable = 0x1239;
        public const short DataProjectSubCmd_PFFindValue = 0x1250;
        public const short DataProjectSubCmd_FExecScript = 0x1251;
        public const short DataProjectSubCmd_GetProjectValues2 = 0x1252;
        public const short DataProjectSubCmd_PFSUM = 0x1253;
        public const short DataProjectSubCmd_QueryFiles = 0x1254;
        public const short DataProjectSubCmd_PFGroup = 0x1255;
        public const short DataProjectSubCmd_PFQuery = 0x1256;
        public const short DataProjectSubCmd_DoCommand = 0x1258;
        public const short DataProjectSubCmd_QueryBackupFiles = 0x1259;
        public const short DataProjectSubCmd_DelBackupFiles = 0x1260;
        public const short DataProjectSubCmd_GetUserListByStream = 0x1261;
        public const short DataProjectSubCmd_GetAppUdateInfo = 0x1262;
        public const short DataProjectSubCmd_GetAppUdateFile = 0x1263;
        public const short DataProjectSubCmd_UploadAppUdateFile = 0x1265;
        public const short DataProjectSubCmd_GetServerVersionInfo = 0x1266;
        public const short DataProjectSubCmd_GetUserHttpSession = 0x1267;
        public const short DataProjectSubCmd_GetUserInof = 0x1268;

        
        public static string CmdText(short cmd)
        {
            switch (cmd)
            {
                case DataProjectSubCmd_QueryBackupFiles: return "DataProjectSubCmd_QueryBackupFiles";
                case PacketMainCmd_DataProjectExtend: return "PacketMainCmd_DataProjectExtend";
                case DataProjectSubCmd_GetProjectItems: return "DataProjectSubCmd_GetProjectItems";
                case DataProjectSubCmd_SaveFile: return "DataProjectSubCmd_SaveFile";
                case DataProjectSubCmd_EditTempFile: return "DataProjectSubCmd_EditTempFile";
                case DataProjectSubCmd_GetTempItems: return "DataProjectSubCmd_GetTempItems";
                case DataProjectSubCmd_QueryFiles: return "DataProjectSubCmd_QueryFiles";
                case DataProjectSubCmd_GetProjectValues: return "DataProjectSubCmd_GetProjectValues";
                case DataProjectSubCmd_GetTempllateFields: return "DataProjectSubCmd_GetTempllateFields";
                case DataProjectSubCmd_GetLoginTemplate: return "DataProjectSubCmd_GetLoginTemplate";
                case DataProjectSubCmd_Login: return "DataProjectSubCmd_Login";
                case DataProjectSubCmd_FileDel: return "DataProjectSubCmd_FileDel";
                case DataProjectSubCmd_FileNew: return "DataProjectSubCmd_FileNew";
                case DataProjectSubCmd_Project_Update: return "DataProjectSubCmd_Project_Update";
                case DataProjectSubCmd_FExecUrlScript: return "DataProjectSubCmd_FExecUrlScript";
                case DataProjectSubCmd_SaveTemplateData: return "DataProjectSubCmd_SaveTemplateData";
                case DataProjectSubCmd_QueryDirectory: return "DataProjectSubCmd_QueryDirectory";
                case DataProjectSubCmd_EditPermission: return "DataProjectSubCmd_EditPermission";
                case DataProjectSubCmd_GetPermission: return "DataProjectSubCmd_GetPermission";
                case DataProjectSubCmd_GetRoleList: return "DataProjectSubCmd_GetRoleList";
                case DataProjectSubCmd_GetSetting: return "DataProjectSubCmd_GetSetting";
                case DataProjectSubCmd_ChangedPwd: return "DataProjectSubCmd_ChangedPwd";
                case DataProjectSubCmd_GetUrlData: return "DataProjectSubCmd_GetUrlData";
                case DataProjectSubCmd_FileData: return "DataProjectSubCmd_FileData";
                case DataProjectSubCmd_FileEdit: return "DataProjectSubCmd_FileEdit";
                case DataProjectSubCmd_OpenUrl: return "DataProjectSubCmd_OpenUrl";
                case DataProjectSubCmd_DoCommand: return "DataProjectSubCmd_DoCommand";
                case DataProjectSubCmd_FExec: return "DataProjectSubCmd_FExec";
                case DataProjectSubCmd_FDataFill: return "DataProjectSubCmd_FDataFill";
                case DataProjectSubCmd_FExecUrlID: return "DataProjectSubCmd_FExecUrlID";
                case DataProjectSubCmd_FExecModeles: return "DataProjectSubCmd_FExecModeles";
                case DataProjectSubCmd_FExecModel: return "DataProjectSubCmd_FExecModel";
                case DataProjectSubCmd_LoginOut: return "DataProjectSubCmd_LoginOut";
                case DataProjectSubCmd_PFDataExist: return "DataProjectSubCmd_PFDataExist";
                case DataProjectSubCmd_PFGetUrlID: return "DataProjectSubCmd_PFGetUrlID";
                case DataProjectSubCmd_PFNUM: return "DataProjectSubCmd_PFNUM";
                case DataProjectSubCmd_PFQueryFile: return "DataProjectSubCmd_PFQueryFile";
                case DataProjectSubCmd_QueryProjectValues: return "DataProjectSubCmd_QueryProjectValues";
                case DataProjectSubCmd_TemplateExits: return "DataProjectSubCmd_TemplateExits";
                case DataProjectSubCmd_PFADD: return "DataProjectSubCmd_PFADD";
                case DataProjectSubCmd_PFSUM: return "DataProjectSubCmd_PFSUM";
                case DataProjectSubCmd_PFSubtract: return "DataProjectSubCmd_PFSubtract";
                case DataProjectSubCmd_PFMultiply: return "DataProjectSubCmd_PFMultiply";
                case DataProjectSubCmd_PFDivide: return "DataProjectSubCmd_PFDivide";
                case DataProjectSubCmd_PFRowValueADD: return "DataProjectSubCmd_PFRowValueADD";
                case DataProjectSubCmd_PFRowValueSubtract: return "DataProjectSubCmd_PFRowValueSubtract";
                case DataProjectSubCmd_PFRowValueMultiply: return "DataProjectSubCmd_PFRowValueMultiply";
                case DataProjectSubCmd_PFRowValueDivide: return "DataProjectSubCmd_PFRowValueDivide";
                case DataProjectSubCmd_GetUserList: return "DataProjectSubCmd_GetUserList";
                case DataProjectSubCmd_FileSendTo: return "DataProjectSubCmd_FileSendTo";
                case DataProjectSubCmd_GetUserRecvFile: return "DataProjectSubCmd_GetUserRecvFile";
                case DataProjectSubCmd_GetUserSendFile: return "DataProjectSubCmd_GetUserSendFile";
                case DataProjectSubCmd_GetUserFavFile: return "DataProjectSubCmd_GetUserFavFile";
                case DataProjectSubCmd_UrlLocked: return "DataProjectSubCmd_UrlLocked";
                case DataProjectSubCmd_UrlUnLocked: return "DataProjectSubCmd_UrlUnLocked";
                case DataProjectSubCmd_GetUrlLocked: return "DataProjectSubCmd_GetUrlLocked";
                case DataProjectSubCmd_GetServerName: return "DataProjectSubCmd_GetServerName";
                case DataProjectSubCmd_EditPermissionButton: return "DataProjectSubCmd_EditPermissionButton";
                case DataProjectSubCmd_GetPermissionButton: return "DataProjectSubCmd_GetPermissionButton";
                case DataProjectSubCmd_GetDataPermission: return "DataProjectSubCmd_GetDataPermission";
                case DataProjectSubCmd_EditDataPermission: return "DataProjectSubCmd_EditDataPermission";
                case DataProjectSubCmd_UrlUnLockRequest: return "DataProjectSubCmd_UrlUnLockRequest";
                case DataProjectSubCmd_GetUserFile: return "DataProjectSubCmd_GetUserFile";
                case DataProjectSubCmd_FileNewPart: return "DataProjectSubCmd_FileNewPart";
                case DataProjectSubCmd_GetCellDataTable: return "DataProjectSubCmd_GetCellDataTable";
                case DataProjectSubCmd_GetIDDataTable: return "DataProjectSubCmd_GetIDDataTable";
                case DataProjectSubCmd_GetIDAndCellDataTable: return "DataProjectSubCmd_GetIDAndCellDataTable";
                case DataProjectSubCmd_PFFindRow: return "DataProjectSubCmd_PFFindRow";
                case DataProjectSubCmd_PFGridGetDataTable: return "DataProjectSubCmd_PFGridGetDataTable";
                case DataProjectSubCmd_PFDataTotal: return "DataProjectSubCmd_PFDataTotal";
                case DataProjectSubCmd_PFGetTable: return "DataProjectSubCmd_PFGetTable";
                case DataProjectSubCmd_PFFindValue: return "DataProjectSubCmd_PFFindValue";
                case DataProjectSubCmd_FExecScript: return "DataProjectSubCmd_FExecScript";
                case DataProjectSubCmd_GetProjectValues2: return "DataProjectSubCmd_GetProjectValues2";
                case DataProjectSubCmd_PFGroup: return "DataProjectSubCmd_PFGroup";
                case DataProjectSubCmd_PFQuery: return "DataProjectSubCmd_PFQuery";
                case DataProjectSubCmd_GetAppUdateInfo: return "DataProjectSubCmd_GetAppUdateInfo";
                case DataProjectSubCmd_GetAppUdateFile: return "DataProjectSubCmd_GetAppUdateFile";
                case DataProjectSubCmd_UploadAppUdateFile: return "DataProjectSubCmd_UploadAppUdateFile";
                case DataProjectSubCmd_GetServerVersionInfo: return "DataProjectSubCmd_GetServerVersionInfo";
                case DataProjectSubCmd_GetUserHttpSession: return "DataProjectSubCmd_GetUserHttpSession";
                case DataProjectSubCmd_GetUserInof: return "DataProjectSubCmd_GetUserInof";

                default: return "Error";

            }
        }
    }
}
