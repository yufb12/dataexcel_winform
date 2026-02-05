using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Excel.App
{
    [Serializable]
    public class ConstantValue 
    {//DataExcelSetting

        public const short FileHeader = 0x1111;
        public const short FileFooter = 0x1112;  
        public const short FileOver = 0x1123;    

        public const short FileEndMergeCell = 0x1128;
        public const short FileEndBackCell = 0x1129;
        public const short FileEndDataTable = 0x1130;
        public const short FileDisplayArea = 0x1131;
        public const short ListExtendCells = 0x1132;
        public const short ExtendData = 0x1134;
        public const short FilePrintArea = 0x1135;

        public const int MinRowIndex = 1;
        public const int MinColumnIndex = 1;
        public const short DATAEXCEL = 0x1301;
        public const int DATAEXCEL_VSERION = 0x2101;
        public const int DATAEXCEL_UPGRADE = 7;
        public const string CopySplitSymbolRow = "\n";
        public const string CopySplitSymbolColumn = "\t";
   
        public const string PirateEdition = "BAD BABY";
        public const int NullValueIndex = -1;
        public const int NullSize = -1;
        public const int RowHeaderSplit = 5;
        public const int ColumnHeaderSplit = 5;
        public const string ErrorValue = "#Error";

        public const string DateTimeDeafultFormat = "yyyy-MM-dd";
        public const int ZeroLen = 0;
        
    }


}
