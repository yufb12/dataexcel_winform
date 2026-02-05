using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Constant
{ 
    public class DataSetting
    {//DataExcelSetting

        public const ushort FileHeader = 0x8686;
        public const ushort FileFooter = 0x6868;
        public const ushort FileNodeHeader = 0xAAAA;
        public const ushort FileNodeFooter = 0xFFFF;
        public const ushort FileOver = 0x6666;
        public const ushort FileNotOver = 0x8888;
        public const ushort FileHasValue = 0xCCEE;
        public const ushort FileNoValue = 0xEECC;
        public const ushort FileTableHeader = 0xAABB;

        public const ushort FileEndMergeCell = 0xDDAA;
        public const ushort FileEndBackCell = 0xDDBB;
        public const ushort FileEndDataTable = 0xDDCC;

        public const int MinRowIndex = 1;
        public const int MinColumnIndex = 1;
        public const uint End = uint.MaxValue;

        public const string CopySplitSymbolRow = "\n";
        public const string CopySplitSymbolColumn = "\t";
        public const string Name = "DataWord";  
        public const int VersionIndex = 1;
        public const string PirateEdition="pirate edition";
        public const int NullValueIndex = -1;
        public const float NullSize = -1;
        public const int RowHeaderSplit = 5;
        public const int ColumnHeaderSplit = 5;
        public const string ErrorValue = "#Error";
        public const string PropertyCollection = "Collection";
        public const string PropertyExpress = "Express";
        public const string PropertyPrintSetting = "PrintSetting";
        public const string PropertyVisible = "Visible";
        public const string DateTimeDeafultFormat = "yyyy-MM-dd";
        public const int ZeroLen = 0;

        
    }

}
