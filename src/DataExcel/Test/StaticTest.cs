using System;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Test
{
    [System.Diagnostics.Conditional("DEBUG")]
    public class StaticTest : Attribute
    {
        [System.Diagnostics.Conditional("DEBUG")]
        public static void TestFile()
        {

            try
            {
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile",true, "执行静态测试");
                string file = Feng.IO.FileHelper.GetStartUpFileUSER(@"F:\file\testdir\TEST", "\\StaticTest\\test.file");
                Feng.Excel.DataExcel grid = new DataExcel();
                grid[1, 11].OwnEditControl = new Feng.Excel.Edits.CellFileSelectEdit(grid);
                grid[1, 12].OwnEditControl = new Feng.Excel.Edits.CellDropDownDataExcel(grid);
                grid[1, 13].OwnEditControl = new Feng.Excel.Edits.CellCnNumber(grid);
                grid[1, 14].OwnEditControl = new Feng.Excel.Edits.CellButton(grid);
                grid[1, 15].OwnEditControl = new Feng.Excel.Edits.CellCheckBox();
                grid[1, 17].OwnEditControl = new Feng.Excel.Edits.CellCnCurrency(grid);
                grid[1, 18].OwnEditControl = new Feng.Excel.Edits.CellComboBox(grid);
                //grid[1, 19].OwnEditControl = new Feng.Excel.Edits.CellDateTime(grid);

                grid[1, 21].OwnEditControl = new Feng.Excel.Edits.CellDropDownDataExcel(grid);
                grid[1, 22].OwnEditControl = new Feng.Excel.Edits.CellGridView(grid);
                grid[1, 23].OwnEditControl = new Feng.Excel.Edits.CellImage(grid);
                grid[1, 24].OwnEditControl = new Feng.Excel.Edits.CellButton(grid);
                grid[1, 25].OwnEditControl = new Feng.Excel.Edits.CellLabel(grid);
                grid[1, 27].OwnEditControl = new Feng.Excel.Edits.CellLinkLabel(grid);
                grid[1, 28].OwnEditControl = new Feng.Excel.Edits.CellNumber(grid);
                grid[1, 29].OwnEditControl = new Feng.Excel.Edits.CellFileSelectEdit(grid);

                grid[1, 31].OwnEditControl = new Feng.Excel.Edits.CellFileSelectEdit(grid);
                grid[1, 32].OwnEditControl = new Feng.Excel.Edits.CellPassword(grid);
                grid[1, 33].OwnEditControl = new Feng.Excel.Edits.CellRadioCheckBox(grid);
                grid[1, 34].OwnEditControl = new Feng.Excel.Edits.CellSpText(grid);
                grid[1, 35].OwnEditControl = new Feng.Excel.Edits.CellTextBoxEdit(grid);
                grid[1, 37].OwnEditControl = new Feng.Excel.Edits.CellFileSelectEdit(grid);
                grid[1, 38].OwnEditControl = new Feng.Excel.Edits.CellTreeView(grid);
                grid[1, 39].OwnEditControl = new Feng.Excel.Edits.CellRowHeader(grid);
                grid[1, 40].OwnEditControl = new Feng.Excel.Edits.CellColor(grid);
                grid[1, 41].OwnEditControl = new Feng.Excel.Edits.CellSwitch(grid);
                grid[1, 42].OwnEditControl = new Feng.Excel.Edits.CellVector(grid);
                grid[1, 43].OwnEditControl = new Feng.Excel.Edits.CellFileSelectEdit(grid);
                grid[1, 44].OwnEditControl = new Feng.Excel.Edits.CellProcess(grid);
                //grid[1, 45].OwnEditControl = new Feng.Excel.Edits.CellDropDownDateTime(grid); 
                grid[1, 46].OwnEditControl = new Feng.Excel.Edits.CellExcel(grid);
                grid[1, 47].OwnEditControl = new Feng.Excel.Edits.CellDropDownFillter(grid);
                grid[1, 47].OwnEditControl = new Feng.Excel.Edits.CellTimer(grid);


                grid[2, 1].Text = "AAAAAAAAAAAAA";
                grid[2, 2].Value = "AAAAAAAAAAAAA";
                grid[2, 3].Text = "AAAAAAAAAAAAA";
                grid[2, 3].Value = "AAAAAAAAAAAAA";

                IMergeCell mergecell = grid.MergeCell(grid[2, 4], grid[3, 9]);
                mergecell.Text = "mergecell";
                grid[4, 1].PropertyOnClick = "Print";
                grid[4, 2].ID = "IDTEST";
                grid[4, 3].BorderStyle.BottomLineStyle.Visible = true;
                grid[4, 3].BorderStyle.TopLineStyle.Visible = true;
                grid[4, 3].BorderStyle.LeftLineStyle.Visible = true;
                grid[4, 3].BorderStyle.RightLineStyle.Visible = true;
                grid[4, 4].Font = new System.Drawing.Font("宋体", 12f);
                grid[4, 5].FormatType = Feng.Utils.FormatType.DateTime;
                grid[4, 5].FormatString = "yyyy-MM-dd";
                grid[4, 5].Value = new DateTime(1985, 12, 21);
                grid.Save(file);
                grid = new DataExcel();
                grid.Open(file);
                System.IO.File.Delete(file);


                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile01", grid[1, 11].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellFileSelectEdit), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile02", grid[1, 12].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellDropDownDataExcel), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile03", grid[1, 13].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellCnNumber), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile04", grid[1, 14].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellButton), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile05", grid[1, 15].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellCheckBox), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile06", grid[1, 17].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellCnCurrency), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile07", grid[1, 18].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellComboBox), "失败");
                //Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile08", grid[1, 19].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellDateTime), "失败");

                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile11", grid[1, 21].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellDropDownDataExcel), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile12", grid[1, 22].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellGridView), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile13", grid[1, 23].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellImage), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile14", grid[1, 24].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellButton), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile15", grid[1, 25].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellLabel), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile16", grid[1, 27].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellLinkLabel), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile17", grid[1, 28].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellNumber), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile18", grid[1, 29].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellFileSelectEdit), "失败");

                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile21", grid[1, 32].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellPassword), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile22", grid[1, 33].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellRadioCheckBox), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile23", grid[1, 34].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellSpText), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile24", grid[1, 35].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellTextBoxEdit), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile25", grid[1, 31].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellFileSelectEdit), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile26", grid[1, 37].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellFileSelectEdit), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile27", grid[1, 38].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellTreeView), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile28", grid[1, 39].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellRowHeader), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile29", grid[1, 40].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellColor), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile29", grid[1, 41].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellSwitch),  "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile29", grid[1, 42].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellVector),  "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile29", grid[1, 43].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellFileSelectEdit),  "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile29", grid[1, 44].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellProcess), "失败");
                //Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile29", grid[1, 45].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellDropDownDateTime), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile29", grid[1, 46].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellExcel), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile29", grid[1, 47].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellDropDownFillter), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile29", grid[1, 47].OwnEditControl.GetType() != typeof(Feng.Excel.Edits.CellTimer), "失败");

                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile31", grid[2, 1].Text != "AAAAAAAAAAAAA", "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile32", !grid[2, 2].Value.Equals("AAAAAAAAAAAAA"), "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile33", grid[2, 3].Text != "AAAAAAAAAAAAA", "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile34", !grid[2, 3].Value.Equals("AAAAAAAAAAAAA"), "失败");

                mergecell = grid[2, 4].OwnMergeCell;
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile41", mergecell.Text != "mergecell", "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile42", grid[4, 1].PropertyOnClick != "Print", "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile43", grid[4, 2].ID != "IDTEST", "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile44", !grid[4, 3].BorderStyle.BottomLineStyle.Visible, "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile45", !grid[4, 3].BorderStyle.TopLineStyle.Visible, "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile46", !grid[4, 3].BorderStyle.LeftLineStyle.Visible, "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile47", !grid[4, 3].BorderStyle.RightLineStyle.Visible, "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile48", grid[4, 4].Font.Name != "宋体", "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile49", grid[4, 5].FormatType != Feng.Utils.FormatType.DateTime, "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile50", grid[4, 5].FormatString != "yyyy-MM-dd", "失败");
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile51", !(grid[4, 5].Value is DateTime), "失败");

                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile52",true, "执行静态测试完成");
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "StaticTest", "TestFile", ex);
            }


        }

    }
}