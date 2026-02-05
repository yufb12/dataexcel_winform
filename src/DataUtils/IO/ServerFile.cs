namespace Feng.IO
{
    public class ServerFile
    {
        public string PermissionButtonPath = "系统管理\\按钮权限" + Feng.App.FileExtension_DataExcel.DataExcel;
        public string PermissionPath = "系统管理\\权限" + Feng.App.FileExtension_DataExcel.DataExcel;
        public string RolePath = "系统管理\\角色" + Feng.App.FileExtension_DataExcel.DataExcel;
        public string UserPath = "系统管理\\用户" + Feng.App.FileExtension_DataExcel.DataExcel;
        public string Setting = "系统管理\\设置" + Feng.App.FileExtension_DataExcel.DataExcel;
        public string Department = "系统管理\\部门" + Feng.App.FileExtension_DataExcel.DataExcel;
        public string SqlPath = "系统管理\\脚本" + Feng.App.FileExtension_DataExcel.DataExcel;
        private static string ds_path_file_or_update_version_P_dspath = string.Empty;
        /// <summary>
        /// Client Or Server ApplicationStartPath
        /// </summary>
        public static string DS_PATH_FILE_OR_UPDATE_VERSION_PATH { get { return ds_path_file_or_update_version_P_dspath; } }
        public static void InitDS_PATH_FILE_OR_UPDATE_VERSION_P_DSPATH(string path)
        {
            ds_path_file_or_update_version_P_dspath = path;
            ds_server_packet_upload_path = Feng.IO.FileHelper.Combine(path, "update");
        }

        private static string dataclient_app_runpath = string.Empty;
        public static string DATACLIENT_APP_RUNPATH { get { return dataclient_app_runpath; } }
        public static void InitDATACLIENT_APP_RUNPATH(string path)
        {
            dataclient_app_runpath = path;
        }
        private static string ds_server_packet_upload_path = string.Empty;
        public static string DS_SERVER_PACKET_UPLOAD_PATH { get { return ds_server_packet_upload_path; } }

        public static string GetRoot()
        {
            if (!string.IsNullOrWhiteSpace(DS_PATH_FILE_OR_UPDATE_VERSION_PATH))
            {
                return DS_PATH_FILE_OR_UPDATE_VERSION_PATH;
            }
            //string path = string.Empty;
            //string parent = System.IO.Directory.GetParent(System.Windows.Forms.Application.StartupPath).FullName;
            //path = parent;
            return string.Empty;
        }

        public static string DataServer_FunctionPlus = Feng.IO.FileHelper.Combine(GetRoot(), "\\Config\\Server\\PLUS\\Function\\");
        public static string DataServer_Setting
        {
            get
            {
                return
Feng.IO.FileHelper.Combine(GetRoot(),
"\\Config\\Server\\Setting" + Feng.App.FileExtension_DataExcel.DataExcel);
            }
        }

        public static string DataServer_Root
        {
            get
            {
                return Feng.IO.FileHelper.Combine(GetRoot(), "\\File\\Root");
            }
        }
        public static string DataServer_Data
        {
            get
            {
                return Feng.IO.FileHelper.Combine(GetRoot(), "\\File\\Root\\Data");
            }
        }
        public static string DataServer_BackUp
        {
            get
            {
                return Feng.IO.FileHelper.Combine(GetRoot(), "\\File\\Root\\备份");
            }
        }
        public static string DataServer_RecyFilePath
        {
            get
            {
                return Feng.IO.FileHelper.Combine(GetRoot(), "\\File\\Root\\回收站");
            }
        }
        public static string DataServer_UserFilePath
        {
            get
            {
                return Feng.IO.FileHelper.Combine(GetRoot(), "\\File\\Root\\用户文件");
            }
        }
        public static string DataServer_LogFilePath
        {
            get
            {
                return Feng.IO.FileHelper.Combine(GetRoot(), "\\File\\Root\\日志");
            }
        }

    }
}
