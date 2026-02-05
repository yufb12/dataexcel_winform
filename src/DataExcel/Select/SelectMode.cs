using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Excel.Generic
{

    public static class  SelectMode
    {
        public const int Null = 0;
        public const int RowHeaderSelected = 1;
        public const int RowHeaderSplitSelected = 2;
        public const int FullRowSelected = 3;
        public const int ColumnHeaderSelected = 4;
        public const int ColumnHeaderSplitSelected = 5;
        public const int FullColumnSelected = 6;
        public const int CellAddSelected = 7;
        public const int CellSelected = 8;
        public const int MergeCellSelected = 9;
        public const int ImageCellSelected = 10;
        public const int TextCellSelected = 11;
        public const int MergeCellAddSelected = 12;
        //public const int ImageCellSizeRectSelected = 13;
        public const int ExtendCellSizeRectSelected = 14;
        public const int VScrollMoveSelected = 15;
        public const int HScrollMoveSelected = 16;
        public const int VDataTableScrollerMoveSelected = 17;
        public const int ChangedSize = 18;
        public const int Drag = 19;
        public const int CellRangeFunctionCellSelected = 20;
        public const int RowHeightChangedMode = 1001; 
        public const int ColumnWidthChangedMode = 1002; 
    }      
}        
