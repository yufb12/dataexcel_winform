using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Excel.Generic
{
    public static class ExceptionString
    {
        public const string RowHeaderCellNew = "没有初始化行!错误代码：10023";
        public const string RowHeaderCellNew2 = "没有初始化行!错误代码：10024";
        public const string SetCellTop = "Cell Size为只读!错误代码：30010";
        public const string SetCellLeft = "Cell Size为只读!错误代码：30011";
        public const string SetCellBoom = "Cell Size为只读!错误代码：30012";
        public const string SetCellRight = "Cell Size为只读!错误代码：30013";
        public const string SetCellWidth = "Cell Size为只读!错误代码：30014";
        public const string SetCellHeight = "Cell Size为只读!错误代码：30015";

        public const string SetSelectTop = "Cell Size为只读!错误代码：30020";
        public const string SetSelectLeft = "Cell Size为只读!错误代码：30021";
        public const string SetSelectBoom = "Cell Size为只读!错误代码：30022";
        public const string SetSelectRight = "Cell Size为只读!错误代码：30023";
        public const string SetSelectWidth = "Cell Size为只读!错误代码：30024";
        public const string SetSelectHeight = "Cell Size为只读!错误代码：30025";


        public const string SetRowVisible = "RowVisible为只读!错误代码：30025";
        public const string NewCellRowExists = "Cell 行已经存在!错误代码：40021";
        public const string InvalidFunctionName = "无效的函数名!错误代码: 50110";
        public const string RowCollectionInsert = "RowCollectionInsert!错误代码: 10041";
        public const string FunctionFormatException = "FunctionFormatException!错误代码: 10441";
    }
}
