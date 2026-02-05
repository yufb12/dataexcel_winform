//using Feng.Excel.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace Feng.Excel
//{
//    public static class DataExcelTool
//    {
//        public static ICell GetDownCell(DataExcel grid,ICell cell)
//        {
//            if (grid == null)
//                return null;
//            if (cell == null)
//                return null;

//            int row = cell.MaxRowIndex +1;
//            int column = cell.Column.Index;
//            if (cell.OwnMergeCell != null)
//            {
//                row = cell.OwnMergeCell.MaxRowIndex + 1;
//                column = cell.OwnMergeCell.Column.Index;
//            }
//            return grid[row, column];
//        }
//        public static ICell GetCell(DataExcel grid, IRow row, IColumn column)
//        {
//            ICell cell = row.Cells[column];
//            if (cell == null)
//                return null;
//            if (cell.OwnMergeCell != null)
//            {
//                return cell.OwnMergeCell;
//            }
//            return cell;
//        }
//    }
     
//}
