using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Excel.Generic
{ 
    public class DataExcelFileEnum
    {
        public const byte DataExcelFile_End = 0;
        public const byte DataExcelFile_All = 1;
        public const byte DataExcelFile_DATAVALUE = 2;
        public const byte DataExcelFile_UPDATEINFO = 3;
        public const byte DataExcelFile_POSITION = 4;
        public const byte DataExcelFile_CODE = 5;
        public const byte DataExcelFile_CELLS = 6;
    }

    public class CellDataType
    {
        public const byte Text = 0x01;
        public const byte Int = 0x02;
        public const byte Long = 0x03;
        public const byte Decima = 0x04; 
    }

}
