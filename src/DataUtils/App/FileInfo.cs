using System.Drawing;
using System.Windows.Forms;

namespace Feng.App
{
    public static class FileExtension_DataExcel
    {
        /// <summary>
        /// 主文件  DataExcelMain打开的文件 带设计器 工具栏
        /// </summary>
        public const string DataExcel = ".fexm";
        /// <summary>
        /// 浏览文件 浏览器打开的文件 在线时会提示更新到最新版
        /// </summary>
        public const string DataExcelBrower = ".fbxm";
 
        /// <summary>
        /// 模版文件 制作输入模板 
        /// </summary>
        public const string DataExcelTemplate = ".ftxm";

        /// <summary>
        /// 由模版保存的文件
        /// </summary>
        public const string DataExcelTemplateData = ".ftdm";
 
        /// <summary>
        /// 引用文件 引用其他文件数据
        /// </summary>
        public const string DataExcelProjectRef = ".fref";
 
        /// <summary>
        /// 模版文件
        /// </summary>
        public const string FILE_EXTEND_DATAEXCEL_TEMPLATE = "ftxm";
        public static bool IsDataExcelFile(string fileextents)
        {
            switch (fileextents)
            {
                case DataExcel:
                case DataExcelBrower: 
                case DataExcelTemplate:
                case DataExcelTemplateData: 
                case DataExcelProjectRef:
                case FILE_EXTEND_DATAEXCEL_TEMPLATE: 
                    return true;
                default:
                    break;
            }
            return false;
        }
        public static bool IsDataExcelFileExtents(string file )
        {
            string fileextents = System.IO.Path.GetExtension(file);
            switch (fileextents)
            {
                case DataExcel:
                case DataExcelBrower:
                case DataExcelTemplate:
                case DataExcelTemplateData:
                case DataExcelProjectRef:
                case FILE_EXTEND_DATAEXCEL_TEMPLATE:
                    return true;
                default:
                    break;
            }
            return false;
        }

        public const string FILE_EXTEND_CBASIC_SCRIPT = ".cbs";
        public static bool IsCBScriptFile(string fileextents)
        {
            switch (fileextents)
            {
                case FILE_EXTEND_CBASIC_SCRIPT: 
                    return true;
                default:
                    break;
            }
            return false;
        }
        /// <summary>
        /// excel
        /// </summary>
        public const string excel = ".xls";
        public const string DataExcelAndExcelFillter = ""
    + "*" + "数据表格  DataExcelMain打开的文件 带设计器 工具栏" + "|*" + DataExcel
    + "|*" + "excel文件" + "|*" + excel ;


        public const string ExcelXLSFillter = "" 
    + "|*" + "excel文件" + "|*" + excel;

        public const string SelDataExcelFile = ""
            + "" + "数据表格  DataExcelMain打开的文件 带设计器 工具栏" + "|*" + DataExcel
            + "|" + "浏览文件 浏览器打开的文件 在线时会提示更新到最新版" + "|*" + DataExcelBrower 
            + "|" + "模版文件 制作输入模板" + "|*" + DataExcelTemplate
            + "|" + "模版文件 制作输入模板 默认打开 无设计器 工具栏等" + "|*" + DataExcelTemplateData 
            + "|" + "引用文件 引用其他文件数据" + "|*" + DataExcelProjectRef;

        public const string DataExcelFiles = ""
                + "主文件  DataExcelMain打开的文件 带设计器 工具栏" + "(" + DataExcel + ")" + "\r\v"
                + "浏览文件 浏览器打开的文件 在线时会提示更新到最新版" + "(" + DataExcelBrower + ")" + "\r\v"  
                + "模版文件 制作输入模板" + "(" + DataExcelTemplate + ")" + "\r\v"
                + "模版文件 制作输入模板 默认打开 无设计器 工具栏等" + "(" + DataExcelTemplateData + ")" + "\r\v" 
                + "引用文件 引用其他文件数据" + "(" + DataExcelProjectRef + ")" + "\r\v";


        public const string SelDataExcelFileAndAll = "*" + DataExcel + "|*" + DataExcel
            + "|*" + DataExcelBrower + "|*" + DataExcelBrower  
            + "|*" + DataExcelTemplateData + "|*" + DataExcelTemplateData
            + "|*" + DataExcelProjectRef + "|*" + DataExcelProjectRef
            + "|*" + ".*" + "|*" + ".*";

        public const string SelDataExcelFileAndExcel = "*" + DataExcel + "|*" + DataExcel + "|*.xls|*.xls";

        public const string DATAEXCELPROJECTSHOTCUT_FILTER = "*.deshut|*.deshut";
        public const string DATAEXCELPROJECTSHOTCUT = ".deshut";

        public const string SelExcel = "*.xls";
        public const string FILELINK = "*.link";
        public const string FILELINK_NO = "link";
        public const string FILELINK_EXTENTS = ".link";
        public const string PAK = "*.pak";
        public const string LOCK = "*.lock";
        public const string LOCK_EXTENTS = ".lock";
        public const string LOCK_NO = "lock";
        public const string SELPAK = "*.pak|*.pak";
        public static bool Contain(string ext)
        {
            ext = ext.ToLower();
            if (FileExtension_DataExcel.DataExcel.Contains(ext))
            {
                return true;
            }
            if (FileExtension_DataExcel.DataExcelBrower.Contains(ext))
            {
                return true;
            } 
 
            if (FileExtension_DataExcel.DataExcelTemplate.Contains(ext))
            {
                return true;
            }
            return false;
        }


    }
    public static class AppInfo
    {  
        public const string DataProject_APP_Brower_TITLE = "D浏览器";
        public static Icon Icon { get; set; }
    }
}

