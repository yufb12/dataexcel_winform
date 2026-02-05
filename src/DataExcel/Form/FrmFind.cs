using Feng.Excel.Interfaces;
using Feng.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Feng.Excel.Forms
{
    public partial class FrmFind : System.Windows.Forms.Form
    {
        public FrmFind()
        {
            InitializeComponent();
        }
        public DataExcel Grid { get; set; }
        private int rowindex = 1;
        private int columnindex = 1;
        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Grid == null)
                    return;
                int ri = rowindex;
                int ci = columnindex;
                for (int row = ri; row <= Grid.Rows.Max; row++)
                {
                    IRow datarow = Grid.Rows[row];
                    if (datarow == null)
                        continue;
                    if (ri != row)
                    {
                        ci = 0;
                    }
                    for (int column = ci+1; column <= Grid.Columns.Max; column++)
                    {
    
                        IColumn datacolumn= Grid.Columns[column];
                        if (datacolumn == null)
                            continue;
                        ICell cell = datarow[datacolumn];
                        if (cell == null)
                            continue;
                        if (cell.Value == null)
                            continue;
                        string text = Feng.Utils.ConvertHelper.ToString(cell.Value);
                        if (string.IsNullOrWhiteSpace(text))
                        {
                            continue;
                        }
                        bool contains = TextHelper.Contains(text, this.txtInput.Text,
                            this.chkcaseSensitiveMatch.Checked, this.chkfullWordMatch.Checked);
                        if (contains)
                        {
                            rowindex = row;
                            columnindex = column;
               
                            this.Grid.ShowCell(cell);
                            this.Grid.FocusedCell = cell;
                            this.Grid.SelectCell(cell);
                            this.Grid.CloseEdit();
                            this.Grid.Invalidate();
                            return;
                        }
                    }
                } 
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("Feng.Excel.Forms", "FrmFind", "btnNext_Click", true, ex);
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Grid == null)
                    return;
                int ri = rowindex;
                int ci = columnindex;
                for (int row = ri; row >=1; row--)
                {
                    IRow datarow = Grid.Rows[row];
                    if (datarow == null)
                        continue;
                    if (ri != row)
                    {
                        ci = Grid.Columns.Max;
                    }
                    for (int column = ci-1; column >= 1; column--)
                    {

                        IColumn datacolumn = Grid.Columns[column];
                        if (datacolumn == null)
                            continue;
                        ICell cell = datarow[datacolumn];
                        if (cell == null)
                            continue;
                        if (cell.Value == null)
                            continue;
                        string text = Feng.Utils.ConvertHelper.ToString(cell.Value);
                        if (string.IsNullOrWhiteSpace(text))
                        {
                            continue;
                        }
                        bool contains = TextHelper.Contains(text, this.txtInput.Text,
             this.chkcaseSensitiveMatch.Checked, this.chkfullWordMatch.Checked);
                        if (contains)
                        {
                            rowindex = row;
                            columnindex = column;
                            this.Grid.ShowCell(cell);
                            this.Grid.FocusedCell = cell;
                            this.Grid.SelectCell(cell);
                            this.Grid.CloseEdit();
                            this.Grid.Invalidate();
                            return;
                        }
                    }
                } 
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("Feng.Excel.Forms", "FrmFind", "btnPrev_Click", true, ex);
            }
        }


    }
}

//using System;

//class Program
//{
//    static void Main(string[] args)
//    {

//        // 输入文本和关键词
//        string text = "This is a sample Text with Sample text within it.";
//        string keyword = "sample";

//        // 搜索区分大小写
//        bool caseSensitiveMatch = text.Contains(keyword);

//        // 搜索全字区配
//        bool fullWordMatch = false;
//        string[] words = text.Split(new[] { ' ', ',', '.', ';', ':' }, StringSplitOptions.RemoveEmptyEntries);
//        foreach (string word in words)
//        {
//            if (caseSensitiveMatch)
//            {
//                if (word == keyword)
//                {
//                    fullWordMatch = true;
//                    break;
//                }
//            }
//            else
//            {
//                if (word.ToLower() == keyword.ToLower())
//                {
//                    fullWordMatch = true;
//                    break;
//                }
//            }
//        }

//        // 打印结果
//        Console.WriteLine("Case sensitive match: " + caseSensitiveMatch);
//        Console.WriteLine("Full word match: " + fullWordMatch);
//    }
//}