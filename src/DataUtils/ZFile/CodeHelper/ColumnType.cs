using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Feng.DevTools.Code
{
    public class SQliteColumnType
    {
        public const string t_Text = "";
        public const string t_DateTime = "";
        public const string t_Decimal = "";
        public const string t_Int16 = "";
        public const string t_Int32 = "";
        public const string t_Int64 = "";
        public const string t_UInt16 = "";
        public const string t_UInt32 = "";
        public const string t_UInt64 = "";
    }
    public class ColumnType
    {
        public string Text { get; set; }
        public string DateTime { get; set; }
        public string Int16 { get; set; }
        public string Int32 { get; set; }
        public string Int64 { get; set; }
        public string UInt16 { get; set; }
        public string UInt32 { get; set; }
        public string UInt64 { get; set; }

        public static String GetCsTypeString()
        {
            return "";
        }
        public static String GetJaveTypeString(string text)
        {
            switch (text)
            {
                case "System.Int64":
                    text = "long";
                    break;
                case "System.Int16":
                    text = "short";
                    break;
                case "System.Int32":
                    text = "int";
                    break;
                case "System.Byte[]":
                    text = "byte[]";
                    break;
                case "System.Boolean":
                    text = "boolean";
                    break;
                case "System.DateTime":
                    text = "Date";
                    break;
                case "System.Double":
                    text = "double";
                    break;
                case "System.Decimal":
                    text = "double";
                    break;
                case "System.String":
                    text = "String";
                    break;
                case "System.Byte":
                    text = "byte";
                    break;
                default:
                    text = "byte[]";
                    break;
            }
            return text;
        }

        public static string GetSqliteTypeString(string text, SysCodes col)
        { 
            switch (text)
            {
                case "System.Int64":
                    text = "integer";
                    break; 
                case "System.Int16":
                    text = "smallint";
                    break; 
                case "System.Int32":
                    text = "int";
                    break;
                case "System.Byte[]":
                    text = "binary";
                    break; 
                case "System.Boolean":
                    text = "int";
                    break;
                case "System.DateTime":
                    text = "varchar(20)";
                    break;
                case "System.Double":
                    text = "real";
                    break;
                case "System.Decimal":
                    text = "decimal";
                    break;
                case "System.String":
                    text = "varchar(" + col.Length + ")";
                    break;
                case "System.Byte":
                    text = "tinyint";
                    break; 
                default:
                    text = "binary";
                    break;
            }
            return text;
        }

        public static string GetArgsString(string dbtype, string text)
        {
            switch (dbtype)
            { 
                case "System.Boolean":
                    text = text + "? 1 : 0";
                    break;
                case "System.DateTime":
                    text = text + ".toLocaleString()";
                    break; 
                default: 
                    break;
            }
            return text;
        }
        public static string GetGetDBResultString(string text, int i)
        {
            switch (text)
            {
                case "System.Int64":
                    text = "result.getLong(" + i + ");";
                    break;
                case "System.Int16":
                    text = "result.getShort(" + i + ");";
                    break;
                case "System.Int32":
                    text = "result.getInt(" + i + ");";
                    break;
                case "System.Byte[]":
                    text = "result.getBlob(" + i + ");";
                    break;
                case "System.Boolean":
                    text = "result.getInt(" + i + ")==1?true:false;";
                    break;
                case "System.DateTime":
                    text = "new Date(Date.parse(result.getString(" + i + ")));";
                    break;
                case "System.Double":
                    text = "result.getDouble(" + i + ");";
                    break;
                case "System.Decimal":
                    text = "result.getDouble(" + i + ");";
                    break;
                case "System.String":
                    text = "result.getString(" + i + ");";
                    break;
                case "System.Byte":
                    text = "(byte)result.getInt(" + i + ");";
                    break;
                default:
                    text = "result.getBlob(" + i + ");";
                    break;
            }
            return text;
        }

    }
}
