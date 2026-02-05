
using Feng.Excel.Interfaces;
using Feng.Forms.Interface;
using System;
using System.Collections.Generic;
using System.Data;

namespace Feng.Excel.Table
{ 
    public class TableTools
    {
        public static CellDataBase GetCellDataBase(DataExcel grid)
        {
            CellDataBase cellDataBase = new CellDataBase(grid);
            foreach (IRow row in grid.Rows)
            {
                foreach (IColumn column in grid.Columns)
                {
                    ICell cell = row[column];
                    if (cell != null)
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(cell.TableName))
                    {
                        continue;
                    }
                    if (string.IsNullOrWhiteSpace(cell.TableColumnName))
                    {
                        continue;
                    }
                    if (cell.TableRowIndex < 0)
                    {
                        continue;
                    }
                    //CellTable cellTable = cellDataBase.GetTable(cell.TableName);
                    //CellTableRow cellTableRow = cellTable.GetRow(cell.TableRowIndex);
                    //cellTableRow.Add(cell);
                }
            }
            return cellDataBase;
        }
        public static void ClearTable(DataExcel grid,string tablename)
        {
            CellDataBase cellDataBase = new CellDataBase(grid);
            foreach (IRow row in grid.Rows)
            {
                foreach (IColumn column in grid.Columns)
                {
                    ICell cell = row[column];
                    if (cell == null)
                    {
                        continue;
                    }
                    if (cell.TableName.Equals(tablename))
                    {
                        cell.Value = null;
                    }
                }
            } 
        }
    }

}