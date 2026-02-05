using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;
using Feng.Excel.Interfaces;
using System.Drawing;

namespace Feng.Excel
{
    public interface IToJson
    {
        string ToJson();
    }
    public class ToJsonTool
    {
        public static string ToJson(IToJson toJson)
        {
            if (toJson == null)
                return "{}";
            return toJson.ToJson();
        }
        public static string ToJsonBoolean(bool value)
        {
            return value.ToString().ToLower();
        }
        public static int ToJsonInt(bool value)
        {
            if (value)
                return 1;
            return 0;
        }
        public static string ToJsonColor(Color color)
        {
            string txt = System.Drawing.ColorTranslator.ToHtml(color);
            return txt;
        }
        public static string ToJsonFont(Font font)
        {
            string txt = "italic bold 12px/20px arial,sans-serif";
            return txt;
        }
 
        public static Grid Get(Feng.Excel.DataExcel excel)
        {
            Grid grid = new Grid();
            grid.allowchangedfirstdisplaycolumn = ToJsonBoolean(excel.AllowChangedFirstDisplayColumn);
            grid.allowchangedfirstdisplayrow = ToJsonBoolean(excel.AllowChangedFirstDisplayRow);
            grid.backcolor = ToJsonColor(excel.BackColor);
            grid.backimageimagelayout = (int)excel.BackgroundImageLayout;
            grid.bordercolor = ToJsonColor(excel.BorderColor);
            grid.defaultcolumnwidth = excel.DefaultColumnWidth;
            grid.defaultrowheight = excel.DefaultRowHeight;
            grid.firstdisplayedcolumnindex = excel.FirstDisplayedColumnIndex;
            grid.firstdisplayedrowindex = excel.FirstDisplayedRowIndex;
            grid.font = ToJsonFont(excel.Font);
            grid.forecolor = ToJsonColor(excel.ForeColor);
            grid.frozencolumn = excel.FrozenColumn;
            grid.frozenrow = excel.FrozenRow;
            grid.height = excel.Height;
            grid.width = excel.Width;
            if (grid.height < 1)
            {
                grid.height = 800;
            }
            if (grid.width < 1)
            {
                grid.width = 800;
            }
            grid.vscrollvisible = ToJsonBoolean(excel.ShowVerticalScroller);
            grid.hscrollvisible = ToJsonBoolean(excel.ShowHorizontalScroller);
            grid.@readonly = ToJsonInt(excel.ReadOnly);
            grid.showborder = ToJsonBoolean(excel.ShowBorder);
            grid.showcolumnheader = ToJsonBoolean(excel.ShowColumnHeader);
            grid.showfocusedcellborder = ToJsonBoolean(excel.ShowFocusedCellBorder);
            grid.showgridline = ToJsonBoolean(excel.ShowGridColumnLine);
            grid.showrowheader = ToJsonBoolean(excel.ShowRowHeader);
            grid.showselectaddrect = ToJsonBoolean(excel.ShowSelectAddRect);
            grid.showselectborder = ToJsonBoolean(excel.ShowSelectBorder);
            grid.rows = new List<RowsItem>();
            grid.columns = new List<ColumnsItem>();
            foreach (IColumn column in excel.Columns)
            {
                if (column.Index < 1)
                    continue;
                ColumnsItem item = new ColumnsItem();
                item.index = column.Index;
                item.visible = ToJsonBoolean(column.Visible);
                item.width = column.Width;
                grid.columns.Add(item);
            }
            foreach (IRow row in excel.Rows)
            {
                if (row.Index < 1)
                    continue;
                RowsItem item = new RowsItem();
                item.index = row.Index;
                item.visible = ToJsonBoolean(row.Visible);
                item.height = row.Height;
                grid.rows.Add(item);
                item.cells = new List<CellsItem>();
                foreach (ICell cell in row.Cells)
                {
                    CellsItem cellsItem = new CellsItem();
                    cellsItem.columnindex = cell.Column.Index;
                    cellsItem.rowindex = cell.Row.Index;
                    cellsItem.forecolor = ToJsonColor(cell.ForeColor);
                    cellsItem.backcolor = ToJsonColor(cell.BackColor);
                    cellsItem.text =Feng.Json.FJsonBase.ToJsonValue(cell.Text);
                    cellsItem.value = Feng.Json.FJsonBase.ToJsonValue(cell.Value);
                    
                    item.cells.Add(cellsItem);
                }
            }
            return grid;
        }

        public static void AppendText(StringBuilder sb, string key, string value, string defaultvalue)
        {
            string txt = GetText(key, value, defaultvalue);
            if (!string.IsNullOrEmpty(txt))
            {
                sb.Append(txt);
            }
        }
        public static string GetText(string key,string value,string defaultvalue)
        {
            if (value.Equals(defaultvalue))
            {
                return string.Empty;
            }
            return "\""+ key + "\": " + "\"" + value + "\"";
        }
    }
    public class Style : IToJson
    {
        public string ToJson()
        {
            return "\"";
        }
    }

    public class CellsItem : IToJson
    {
        /// <summary>
        /// 
        /// </summary>
        public int rowindex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int columnindex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Style style { get; set; }
        public string text { get; set; }
        public string value { get; set; }
        public string backcolor { get; set; }
        public string forecolor { get; set; }
        public string ToJson()
        {
            //if (text.Contains("\n"))
            //{

            //}
            //if (value.Contains("\n"))
            //{

            //}
            //if (value.Contains("销售金额"))
            //{

            //}
            //if (text.Contains("销售金额"))
            //{

            //}
            string strtext = text.Replace("\n", "\\n");
            string strvalue = value.Replace("\n", "\\n");
            strtext = strtext.Replace("\r", "");
            strvalue = strvalue.Replace("\r", ""); 

            if (string.IsNullOrWhiteSpace(backcolor))
            {
                backcolor = "white";
            }
            else
            {

            }
            string txt = " {" +
                "\"rowindex\": " + rowindex + "," +
            "\"columnindex\": " + columnindex + "," +
            "\"text\": " + strtext + "," +
            "\"value\": " + strvalue + "," +
            "\"backcolor\": " + "\"" + backcolor + "\"" + "," +
            "\"forecolor\": " + "\"" + forecolor + "\"" + "," +
            "\"style\": " + ToJsonTool.ToJson(style) + "}";

            return txt;
        }
    }

    public class RowsItem : IToJson
    {
        /// <summary>
        /// 
        /// </summary>
        public int index { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int height { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string visible { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<CellsItem> cells { get; set; }
        public string ToJson()
        {
            string txt = " {" +
                "\"index\": " + index + "," +
            "\"height\": " + height + "," +
            "\"visible\": " + visible + "," +
            "\"cells\": [";
            if (cells != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (CellsItem item in cells)
                {
                    sb.Append(item.ToJson());
                    sb.Append(",");
                }
                if (sb.Length > 0)
                {
                    sb.Length = sb.Length - 1;
                }
                txt = txt + sb.ToString();
            }
            txt = txt + "]}";
            return txt;
        }
    }

    public class ColumnsItem : IToJson
    {
        /// <summary>
        /// 
        /// </summary>
        public int index { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string visible { get; set; }

        public string ToJson()
        {
            string txt = " {" +
                "\"index\": " + index + "," +
            "\"width\": " + width + "," +
            "\"visible\": " + visible + "}";
            return txt;
        }
    }

    public class MergecellsItem : IToJson
    {
        /// <summary>
        /// 
        /// </summary>
        public int begincellrowindex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int begincellcolumnindex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int endcellrowindex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int endcellcolumnindex { get; set; }

        public string ToJson()
        {
            string txt = " {" +
                "\"begincellrowindex\": " + begincellrowindex + "," +
            "\"begincellcolumnindex\": " + begincellcolumnindex + "," +
            "\"endcellrowindex\": " + endcellrowindex + "," +
            "\"endcellcolumnindex\": " + endcellcolumnindex + "";
            return txt;
        }
    }

    public class Grid : IToJson
    {
        public string ToJson()
        {
            string txt = " {" +
                "\"height\": " + height + "," +
            "\"allowchangedfirstdisplaycolumn\": " + allowchangedfirstdisplaycolumn + "," +
            "\"allowchangedfirstdisplayrow\": " + allowchangedfirstdisplayrow + "," +
            "\"backcolor\": " + "\"" + backcolor + "\"" + "," +
            "\"backimage\": " + "\"" + backimage + "\"" + "," +
            "\"backimageimagelayout\": " + backimageimagelayout + "," +
            "\"bordercolor\": " + "\"" + bordercolor + "\"" + "," +
            "\"firstdisplayedcolumnindex\": " + firstdisplayedcolumnindex + "," +
            "\"firstdisplayedrowindex\": " + firstdisplayedrowindex + "," +
            "\"forecolor\": " + "\""+forecolor + "\"" + "," +
            "\"frozencolumn\": " + frozencolumn + "," +
            "\"frozenrow\": " + frozenrow + "," +
            "\"font\": " + "\"" + font + "\"" + "," +
            "\"multiselect\": " + "\"" + multiselect + "\"" + "," +
            "\"readonly\": " + @readonly + "," +
            "\"showborder\": " + showborder + "," +
            "\"showcolumnheader\": " + showcolumnheader + "," +
            "\"showfocusedcellborder\": " + showfocusedcellborder + "," +
            "\"showgridline\": " + showgridline + "," +
            "\"showrowheader\": " + showrowheader + "," +
            "\"showselectaddrect\": " + showselectaddrect + "," +
            "\"showselectborder\": " + showselectborder + "," +
            "\"zoom\": " + "\"" + zoom + "\"" + ","  +
            "\"vscrollvisible\": " + "\"" + vscrollvisible + "\"" + "," +
            "\"hscrollvisible\": " + hscrollvisible + "," +
            "\"width\": " + width + "," +
            "\"defaultrowheight\": " + defaultrowheight + "," +
            "\"defaultcolumnwidth\": " + defaultcolumnwidth + "," +
            "\"rows\": [";
            if (rows != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (RowsItem item in rows)
                {
                    sb.Append(item.ToJson());
                    sb.Append(",");
                }
                if (sb.Length > 0)
                {
                    sb.Length = sb.Length - 1;
                }
                txt = txt + sb.ToString();
            }
            txt = txt + "]," +
             "\"columns\": [";
            if (rows != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (ColumnsItem item in columns)
                {
                    sb.Append(item.ToJson());
                    sb.Append(",");
                }
                if (sb.Length > 0)
                {
                    sb.Length = sb.Length - 1;
                }
                txt = txt + sb.ToString();
            }
            txt = txt + "]," +
             "\"mergecells\": [";
            if (mergecells != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (MergecellsItem item in mergecells)
                {
                    sb.Append(item.ToJson());
                    sb.Append(",");
                }
                if (sb.Length > 0)
                {
                    sb.Length = sb.Length - 1;
                }
                txt = txt + sb.ToString();
            }
            txt = txt + "]}";
            return txt;
        }
        /// <summary>
        /// 
        /// </summary>
        public int height { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<RowsItem> rows { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<ColumnsItem> columns { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<MergecellsItem> mergecells { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> backcells { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> charts { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> primitives { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string allowchangedfirstdisplaycolumn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string allowchangedfirstdisplayrow { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string backcolor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string backimage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int backimageimagelayout { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string bordercolor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int firstdisplayedcolumnindex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int firstdisplayedrowindex { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string forecolor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int frozencolumn { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int frozenrow { get; set; }
        /// <summary>
        /// 14px 宋体
        /// </summary>
        public string font { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string multiselect { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int @readonly { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showborder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showcolumnheader { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showfocusedcellborder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showgridline { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showrowheader { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showselectaddrect { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string showselectborder { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string zoom { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string vscrollvisible { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string hscrollvisible { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int defaultrowheight { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int defaultcolumnwidth { get; set; }
    }
}