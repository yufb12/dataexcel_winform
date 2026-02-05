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
    public partial class FrmReplace : System.Windows.Forms.Form
    {
        public FrmReplace()
        {
            InitializeComponent();
        }
        public DataExcel Grid { get; set; }
        private int rowindex = 1;
        private int columnindex = 1;
        private void btnReplaceAll_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.Grid == null)
                    return;
                int ri = rowindex;
                int ci = columnindex;
                ICell cell = null;
                for (int row = ri; row <= Grid.Rows.Max; row++)
                {
                    IRow datarow = Grid.Rows[row];
                    if (datarow == null)
                        continue;
                    if (ri != row)
                    {
                        ci = 0;
                    }
                    for (int column = ci + 1; column <= Grid.Columns.Max; column++)
                    {

                        IColumn datacolumn = Grid.Columns[column];
                        if (datacolumn == null)
                            continue;
                        cell = datarow[datacolumn];
                        if (cell == null)
                            continue;
                        if (cell.Value == null)
                            continue;
                        if (cell.ReadOnly)
                            continue;
                        string text = Feng.Utils.ConvertHelper.ToString(cell.Value);
                        if (string.IsNullOrWhiteSpace(text))
                        {
                            continue;
                        }
                        bool contains = TextHelper.Contains(text, this.txtFindText.Text,
                this.chkcaseSensitiveMatch.Checked);
                        if (contains)
                        {
                            rowindex = row;
                            columnindex = column;
                            string str = TextHelper.ReplaceIgnoreCase(text, this.txtFindText.Text, this.txtReplaceText.Text, this.chkcaseSensitiveMatch.Checked);
                            cell.Value = str;
                        }
                    }
                }
                if (cell != null)
                {
                    this.Grid.ShowCell(cell);
                    this.Grid.FocusedCell = cell;
                    this.Grid.SelectCell(cell);
                    this.Grid.CloseEdit();
                    this.Grid.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("Feng.Excel.Forms", "FrmFind", "btnNext_Click", true, ex);
            }
        }
 
        private void btnReplaceNext_Click(object sender, EventArgs e)
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
                    for (int column = ci + 1; column <= Grid.Columns.Max; column++)
                    {

                        IColumn datacolumn = Grid.Columns[column];
                        if (datacolumn == null)
                            continue;
                        ICell cell = datarow[datacolumn];
                        if (cell == null)
                            continue;
                        if (cell.Value == null)
                            continue;
                        if (cell.ReadOnly)
                            continue; 
                        string text = Feng.Utils.ConvertHelper.ToString(cell.Value);
                        if (string.IsNullOrWhiteSpace(text))
                        {
                            continue;
                        }
                        bool contains = TextHelper.Contains(text, this.txtFindText.Text,
                this.chkcaseSensitiveMatch.Checked);
                        if (contains)
                        {
                            rowindex = row;
                            columnindex = column;
                            string str = TextHelper.ReplaceIgnoreCase(text, this.txtFindText.Text, this.txtReplaceText.Text,this.chkcaseSensitiveMatch.Checked);

                            cell.Value = str;
                            this.Grid.ShowCell(cell);
                            this.Grid.FocusedCell = cell;
                            this.Grid.SelectCell(cell);
                            this.Grid.CloseEdit();
                            this.Grid.Invalidate();
                            return;
                        }
                    }
                }
                rowindex = 1;
                columnindex = 1;
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("Feng.Excel.Forms", "FrmFind", "btnNext_Click", true, ex);
            }
        }

        private void txtCurrentCell_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            rowindex = 1;
            columnindex = 1;
        }
    }
}


//using System;
//using System.Text.RegularExpressions;

//class Program
//{
//    static void Main(string[] args)
//    {

//        // 输入文本和关键词
//        string text = "This is a sample Text with Sample text within it.";
//        string keyword = "sample";
//        string replaceValue = "new";

//        // 替换文本
//        string result = Regex.Replace(text, "\\b" + keyword + "\\b", replaceValue, RegexOptions.IgnoreCase);

//        // 打印结果
//        Console.WriteLine("Original text: " + text);
//        Console.WriteLine("Replaced text: " + result);
//    }
//}