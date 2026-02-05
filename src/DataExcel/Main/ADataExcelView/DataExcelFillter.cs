using Feng.Enums;
using Feng.Excel.Actions;
using Feng.Excel.Args;
using Feng.Excel.Collections;
using Feng.Excel.Generic;
using Feng.Excel.Interfaces;
using Feng.Forms;
using Feng.Forms.Command;
using Feng.Forms.Views;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Feng.Excel
{
    partial class DataExcel
    {
        public void FilterTable(ISelectCellCollection selectcells,bool extend)
        { 

        }
        public void FilterTable(ISelectCellCollection selectcells)
        {
            int headerrow = selectcells.MinRow();
            int minrow = headerrow+1;
            int mincolumn = selectcells.MinColumn();
            int maxrow = selectcells.MaxRow();
            int maxcolumn = selectcells.MaxColumn();
            FilterTable filtertable = new FilterTable();
            filtertable.MaxColumn = maxcolumn;
            filtertable.MaxRow = maxrow;
            filtertable.MinColumn = mincolumn;
            filtertable.MinRow = minrow;
            for (int i = mincolumn; i <= maxcolumn; i++)
            {
                ICell cell = this.GetCell(headerrow, i);
                filtertable.HeaderCells.Add(cell);
            }
        }
    }

    public class FilterTable
    {
        public List<ICell> HeaderCells { get; set; }
        public int MinRow { get; set; }
        public int MinColumn { get; set; }
        public int MaxRow { get; set; }
        public int MaxColumn { get; set; }
    }
}
