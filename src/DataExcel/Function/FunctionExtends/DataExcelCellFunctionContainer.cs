using Feng.Excel.Builder;
using Feng.Excel.Collections;
using Feng.Excel.Interfaces;
using Feng.Excel.Utils;
using Feng.Script.CBEexpress;
using Feng.Script.Method;
using Feng.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Feng.Excel.Script
{
    [Serializable]
    public class DataExcelCellFunctionContainer : DataExcelMethodContainer
    {

        public const string Function_Name = "DataExcelCell";
        public const string Function_Description = "单元格函数";
        public override string Name
        {
            get { return Function_Name; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }

        public DataExcelCellFunctionContainer()
        {
            BaseMethod model = null;

            model = new BaseMethod();
            model.Name = "CellID";
            model.Description = @"设置单元格ID CellID(CELL(""A5""),""NAME"")";
            model.Eg = @"var res=CellID(CELL(""A5""),""NAME"")";
            model.Function = CellID;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellColumnIndex";
            model.Description = @"获取单元格列索引 CellColumnIndex(CELL(""A5"")) 返回 1";
            model.Eg = @"var columnindex=CellColumnIndex(CELL(""A5""))";
            model.Function = CellColumnIndex;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellColumnName";
            model.Description = @"获取单元格列名称 CellColumnName(CELL(""A5"")) 返回 A";
            model.Eg = @"var columnname=CellColumnName(CELL(""A5""))";
            model.Function = CellColumnName;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellRowIndex";
            model.Description = @"获取单元格列名称 CellRowIndex(CELL(""A5"")) 返回 5";
            model.Eg = @"CellRowIndex(CELL(""A5""))";
            model.Function = CellRowIndex;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellColumnMaxIndex";
            model.Description = @"获取合并单元格最大列索引 CellColumnMaxIndex(CELL(""A5"")) 返回 1";
            model.Eg = @"CellColumnMaxIndex(CELL(""A5""))";
            model.Function = CellColumnMaxIndex;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellRowMaxIndex";
            model.Description = @"获取合并单元格最大列名称 CellRowMaxIndex(CELL(""A5"")) 返回 5";
            model.Eg = @"CellRowMaxIndex(CELL(""A5""))";
            model.Function = CellRowMaxIndex;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellUp";
            model.Description = @"获取单元格的上一个单元格 CellUp(CELL(""A5"")) 返回 A4";
            model.Eg = @"CellUp(CELL(""A5""))";
            model.Function = CellUp;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellDown";
            model.Description = @"获取单元格的下一个单元格 CellDown(CELL(""A5"")) 返回 A6";
            model.Eg = @"CellDown(CELL(""A5""))";
            model.Function = CellDown;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellLeft";
            model.Description = @"获取单元格的左一个单元格 CellLeft(CELL(""B5"")) 返回 A5";
            model.Eg = @"CellLeft(CELL(""B5""))";
            model.Function = CellLeft;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellRight";
            model.Description = @"获取单元格的左一个单元格 CellRight(CELL(""B5"")) 返回 C5";
            model.Eg = @"CellRight(CELL(""B5""))";
            model.Function = CellRight;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "CellsLeft";
            model.Description = @"获取合并单元格的左单元格 CellsLeft(mcell,2)";
            model.Eg = @"CellsLeft(mcell,2)";
            model.Function = CellsLeft;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellsRight";
            model.Description = @"获取合并单元格的右单元格 CellsRight(mcell,2)";
            model.Eg = @"CellsRight(mcell,2)";
            model.Function = CellsRight;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellProperty";
            model.Description = "获取单元格属性值";
            model.Function = CellProperty;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellName";
            model.Description = @"获取单元格名称 CellName(CellRight(CELL(""B5""))) 返回""A5""";
            model.Eg = @"CellName(CellRight(CELL(""B5"")))";
            model.Function = CellName;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellID";
            model.Description = @"获取或设置单元格ID CellID(CELL(""B5""))";
            model.Eg = @"CellID(CELL(""B5""))";
            model.Function = CellID;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellCaption";
            model.Description = @"获取单元格标题 CellCaption(CELL(""B5""))";
            model.Eg = @"CellCaption(CELL(""B5""))";
            model.Function = CellCaption;
            MethodList.Add(model);
 

            model = new BaseMethod();
            model.Name = "CellAction";
            model.Description = @"获取单元格标题 CellAction(CELL(""B5""),""CellClick"")";
            model.Eg = @"CellAction(CELL(""B5""),""CellClick"")";
            model.Function = CellAction;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellTable";
            model.Description = @"获取或设置单元格 表格名称，行索引，列名称 CellTable(""B5"",""TableName"",1,""ColumnName"")";
            model.Eg = @"CellTable(""B5"",""TableName"",1,""ColumnName"")";
            model.Function = CellTable;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellTableName";
            model.Description = @"获取或设置单元格 表格名称 CellTableName(""B5"",""TableName"")";
            model.Eg = @"CellTableName(""B5"",""TableName"")";
            model.Function = CellTableName;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellTableRowIndex";
            model.Description = @"获取或设置单元格 表格行索引 CellTableRowIndex(""B5"",1)";
            model.Eg = @"CellTableRowIndex(CELL(""B5"",1)";
            model.Function = CellTableRowIndex;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellTableColumnName";
            model.Description = @"获取或设置单元格 表格列名称 CellTableColumnName(""B5"",""ColumnName"")";
            model.Eg = @"CellTableColumnName(""B5"",""ColumnName"")";
            model.Function = CellTableColumnName;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellFocused";
            model.Description = "获取或设置焦点单元格 CellFocused()";
            model.Eg = "CellFocused(Cell(2,2))";
            model.Function = CellFocused;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Cell";
            model.Description = "获取单元格 Cell(\"A5\") Cell(\"ID\") CELL(2,2)";
            model.Eg = @"Cell(""A1"")";
            model.Function = Cell;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellFind";
            model.Description = "查找单元格 CellFind(\"A5\",3,5)";
            model.Eg = @"CellFind(""文本值"",""C"",5)";
            model.Function = CellFind;
            MethodList.Add(model);
            
            model = new BaseMethod();
            model.Name = "CellLike";
            model.Description = @"查找符合条件的所有单元格 CellLike(""好%"")";
            model.Eg = @"CellLike(""好%"")";
            model.Function = CellLike;
            MethodList.Add(model);
 
            model = new BaseMethod();
            model.Name = "CellValue";
            model.Description = "获取或设置单元格值 CellValue(\"A5\") CellValue(\"ID\")";
            model.Eg = @"CellValue(""A1"",1024)";
            model.Function = CellValue;
            MethodList.Add(model);
 
            model = new BaseMethod();
            model.Name = "CellText";
            model.Description = "获取或设置单元格文本 CellText(\"A5\") CellText(\"ID\")";
            model.Eg = @"CellText(""A1"",""1024"")";
            model.Function = CellText;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellScript";
            model.Description = "获取或设置单元格脚本 CellScript(\"A5\") CellScript(\"ID\")";
            model.Eg = @"CellScript(""A1"",""1024"")";
            model.Function = CellScript;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellText1";
            model.Description = "获取或设置单元格文本1 CellText1(\"A5\") CellText1(\"ID\")";
            model.Eg = @"CellText1(""A1"",""1024"")";
            model.Function = CellText1;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "CellText2";
            model.Description = "获取或设置单元格文本2 CellText2(\"A5\") CellText1(\"ID\")";
            model.Eg = @"CellText2(""A1"",""1024"")";
            model.Function = CellText2;
            MethodList.Add(model);




            model = new BaseMethod();
            model.Name = "CellText3";
            model.Description = "获取或设置单元格文本3 CellText3(\"A5\") CellText1(\"ID\")";
            model.Eg = @"CellText3(""A1"",""1024"")";
            model.Function = CellText3;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellInt";
            model.Description = "获取或设置单元格文本 CellInt(\"A5\") CellInt(\"ID\")";
            model.Eg = @"CellInt(""A1"",""1024"")";
            model.Function = CellInt;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellDateTime";
            model.Description = "获取或设置单元格文本 CellDateTime(\"A5\") CellDateTime(\"ID\")";
            model.Eg = @"CellDateTime(""A1"",""2021-12-1"")";
            model.Function = CellDateTime;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellLong";
            model.Description = "获取或设置单元格文本 CellLong(\"A5\") CellLong(\"ID\")";
            model.Eg = @"CellLong(""A1"",""1024"")";
            model.Function = CellLong;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellDecimal";
            model.Description = "获取或设置单元格文本 CellDecimal(\"A5\") CellDecimal(\"ID\")";
            model.Eg = @"CellDecimal(""A1"",""1024"")";
            model.Function = CellDecimal;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellBool";
            model.Description = "获取或设置单元格文本 CellBool(\"A5\") CellBool(\"ID\")";
            model.Eg = @"CellBool(""A1"",true)";
            model.Function = CellBool;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "Cells";
            model.Description = @"获取单元格集合 Cells(""A1,A2:B10,C3"")";
            model.Eg = @"Cells(""A1,A2:B10,C3"")";
            model.Function = Cells;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellValues";
            model.Description = "获取单元格集合的值";
            model.Eg = @"CellValues(""A1,A2:B10,C3"")";
            model.Function = CellValues;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CelLRange";
            model.Description = @"获取单元格范围 CelLRange(""A1:A10"")";
            model.Eg = @"CelLRange(""A1:A10"")";
            model.Function = CellRange;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellList";
            model.Description = @"获取单元格集合 CellList(""A1:A10"")";
            model.Eg = @"CellList(""A1:A10"")";
            model.Function = CellList;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellRangeExecScript";
            model.Description = "单元格集合中的每个单元格执行脚本,每个单元格使用ME代指";
            model.Eg = @"CellRangeExecScript(""A1:10"",""CELLVALUE(ME,CELLNAME(ME))"")";
            model.Function = CellRangeExecScript;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellRangeExecFunction";
            model.Description = "单元格集合中的每个单元格执行脚本,每个单元格使用ME代指";
            model.Eg = @"CellRangeExecFunction(""A1:10"",""Add"")";
            model.Function = CellRangeExecFunction;
            MethodList.Add(model);
 
            model = new BaseMethod();
            model.Name = "CellClear";
            model.Description = "清除单元格内容";
            model.Function = CellClear;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellTrim";
            model.Description = "清除单元格内容包含的空格";
            model.Eg = "CellTrim()";
            model.Function = CellTrim;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellShow";
            model.Description = "显示单元格";
            model.Function = CellShow;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellHide";
            model.Description = "隐藏单元格";
            model.Function = CellHide;
            MethodList.Add(model);
 

            model = new BaseMethod();
            model.Name = "CellVisible";
            model.Description = @"获取或设置单元格可见 CellVisible(cell(""name""),false)";
            model.Eg = @"CellVisible(cell(""name""),false)";
            model.Function = CellVisible;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "CellMerge";
            model.Description = @"合并单元格CellMerge(Cell(""A2""),Cell(""B5""))";
            model.Eg = @"CellMerge(Cell(""A2""),Cell(""B5""))";
            model.Function = CellMerge;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellUnMerge";
            model.Description = "取消合并单元格";
            model.Eg = @"CellUnMerge(""A2"")";
            model.Function = CellUnMerge;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellGetMergeCell";
            model.Description = @"CellGetMergeCell(Cell(""A2""))";
            model.Eg = @"CellGetMergeCell(Cell(""A2""))";
            model.Function = CellGetMergeCell;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellReadOnly";
            model.Description = "单元格只读";
            model.Eg = @"";
            model.Function = CellReadOnly;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellEdit";
            model.Description = "单元格编辑控件";
            model.Eg = @"";
            model.Function = CellEdit;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellBackImage";
            model.Description = "单元格背景图片";
            model.Eg = @"";
            model.Function = CellBackImage;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellBackColor";
            model.Description = "单元格背景颜色";
            model.Eg = @"";
            model.Function = CellBackColor;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellBorderOutside";
            model.Description = "单元格外边框";
            model.Eg = @"CellBorderOutside(CELL(""A5""))";
            model.Function = CellBorderOutside;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellBorderTop";
            model.Description = "单元格上边框";
            model.Eg = @"CellBorderTop(CELL(""A5""))";
            model.Function = CellBorderTop;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellBorderBottom";
            model.Description = "单元格下边框";
            model.Eg = @"CellBorderTop(CELL(""A5""))";
            model.Function = CellBorderBottom;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellBorderLeft";
            model.Description = "单元格下边框";
            model.Eg = @"CellBorderLeft(CELL(""A5""))";
            model.Function = CellBorderLeft;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellBorderRight";
            model.Description = "单元格下边框";
            model.Eg = @"CellBorderRight(CELL(""A5""))";
            model.Function = CellBorderRight;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellFormatNumber";
            model.Description = @"单元格格式 CellFormatNumber(cell(""num""),""0.00"")";
            model.Eg = @"CellFormatNumber(cell(""num""),""0.00"")";
            model.Function = CellFormatNumber;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellFormatDateTime";
            model.Description = @"单元格日期格式 CellFormatDateTime(cell(""time""),""yyyy-MM-dd HH:mm:ss"")";
            model.Eg = @"CellFormatDateTime(cell(""time""),""yyyy-MM-dd HH:mm:ss"")";
            model.Function = CellFormatDateTime;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellFormatClear";
            model.Description = "清除单元格格式";
            model.Eg = @"";
            model.Function = CellFormatClear;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellAlignmentCenter";
            model.Description = "单元格居中对齐";
            model.Eg = @"";
            model.Function = CellAlignmentCenter;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellAlignmentLeft";
            model.Description = "单元格左对齐";
            model.Eg = @"";
            model.Function = CellAlignmentLeft;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellAlignmentRight";
            model.Description = "单元格右对齐";
            model.Eg = @"";
            model.Function = CellAlignmentRight;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellAlignmentHorizontalCenter";
            model.Description = "单元格水平居中对齐";
            model.Eg = @"";
            model.Function = CellAlignmentHorizontalCenter;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellAlignmentTop";
            model.Description = "单元格上对齐";
            model.Eg = @"";
            model.Function = CellAlignmentTop;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellAlignmentBottom";
            model.Description = "单元格上对齐";
            model.Eg = @"";
            model.Function = CellAlignmentBottom;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellAlignmentVerticalCenter";
            model.Description = "单元格垂直居中对齐";
            model.Eg = @"";
            model.Function = CellAlignmentVerticalCenter;
            MethodList.Add(model);

            //model = new BaseMethod();
            //model.Name = "CellEditMode";
            //model.Description = "单元格对齐";
            //model.Eg = @"";
            //model.Function = CellEditMode;
            //MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellAutoMultiline";
            model.Description = "单元格自动换行";
            model.Eg = @"";
            model.Function = CellAutoMultiline;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellForeColor";
            model.Description = "单元格前景色";
            model.Eg = @"";
            model.Function = CellForeColor;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellTabStop";
            model.Description = "单元格Tab键顺序";
            model.Eg = @"";
            model.Function = CellTabStop;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellTextDirection";
            model.Description = "单元格文字顺序";
            model.Eg = @"";
            model.Function = CellTextDirection;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellFont";
            model.Description = "单元格字体";
            model.Eg = @"CellFont(""宋体"",2)";
            model.Function = CellFont;
            MethodList.Add(model);




            model = new BaseMethod();
            model.Name = "CellListFill";
            model.Description = "显示集合内容 参数1:单元格,参数2:数据集合,参数3:超点，参数4:长度";
            model.Eg = @"CellListFill(LIST,Cells(""B3:B10""),5)";
            model.Function = CellListFill;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellBoundLeft";
            model.Description = "左边距";
            model.Eg = @"CellBoundLeft(Cell(""B3""))";
            model.Function = CellBoundLeft;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellBoundTop";
            model.Description = "上边距";
            model.Eg = @"CellBoundTop(Cell(""B3""))";
            model.Function = CellBoundTop;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellBoundWidth";
            model.Description = "宽度";
            model.Eg = @"CellBoundWidth(Cell(""B3""))";
            model.Function = CellBoundWidth;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CellBoundHeight";
            model.Description = "高度";
            model.Eg = @"CellBoundHeight(Cell(""B3""))";
            model.Function = CellBoundHeight;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellBounds";
            model.Description = "大小和位置";
            model.Eg = @"CellBounds(Cell(""B3""))";
            model.Function = CellBounds;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "CellMoveUp";
            model.Description = "移动单元格 向上移动";
            model.Eg = @"CellMoveUp(Cell(""B3""))";
            model.Function = CellMoveUp;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellMoveDown";
            model.Description = "移动单元格 向下移动";
            model.Eg = @"CellMoveDown(Cell(""B3""),1)";
            model.Function = CellMoveDown;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellMoveLeft";
            model.Description = "移动单元格 向左移动";
            model.Eg = @"CellMoveLeft(Cell(""B3""),1)";
            model.Function = CellMoveLeft;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CellMoveRight";
            model.Description = "移动单元格 向右移动";
            model.Eg = @"CellMoveRight(Cell(""B3""),1)";
            model.Function = CellMoveRight;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "CellSwap";
            model.Description = @"交换单元格 CellSwap(Cell(""B3""),Cell(""C9""))";
            model.Eg = @"CellSwap(Cell(""B2""),Cell(""C3""));GridRefresh();";
            model.Function = CellSwap;
            MethodList.Add(model);
        }

        public virtual object CellBoundLeft(params object[] args)
        {
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (cell != null)
            {
                return cell.Left;
            }
            return null;
        }
        public virtual object CellBoundTop(params object[] args)
        {
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (cell != null)
            {
                return cell.Top;
            }
            return null;
        }
        public virtual object CellBoundWidth(params object[] args)
        {
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (cell != null)
            {
                return cell.Width;
            }
            return null;
        }
        public virtual object CellBoundHeight(params object[] args)
        {
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (cell != null)
            {
                return cell.Height;
            }
            return null;
        }

        public virtual object CellBounds(params object[] args)
        {
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            } 
            if (cell != null)
            {
                return cell.Rect;
            }
            return null;
        }
        public virtual object Cell(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                ICell cell = null;
                if (args.Length == 3)
                {
                    ICell cel = null;
                    int row = Feng.Utils.Constants.NOINT999_;
                    cel = base.GetCell(1, args);
                    if (cel != null)
                    {
                        row = cel.Row.Index;
                    }
                    if (row < 0)
                    {
                        row = base.GetIntValue(1, Feng.Utils.Constants.NOINT999_, args);
                    }

                    int column = Feng.Utils.Constants.NOINT999_;
                    cel = base.GetCell(2, args);
                    if (cel != null)
                    {
                        column = cel.Column.Index;
                    }
                    if (column < 0)
                    {
                        column = base.GetIntValue(2, Feng.Utils.Constants.NOINT999_, args);
                    }

                    if (column >= 0 && row >= 0)
                    {
                        cell = proxy.Grid[row, column];
                        if (cell.OwnMergeCell != null)
                        {
                            return cell.OwnMergeCell;
                        }
                        return cell;
                    }
                    string fieldname = base.GetTextValue(2, args);
                    if (row > 0 && (!string.IsNullOrWhiteSpace(fieldname)))
                    {
                        cell = proxy.Grid[row, fieldname];
                        if (cell != null)
                        {
                            if (cell.OwnMergeCell != null)
                            {
                                return cell.OwnMergeCell;
                            }
                        }
                        return cell;
                    }
                }
                if (cell ==null)
                {
                    cell = null;
                    string ct = base.GetTextValue(1, args);
                    cell = proxy.Grid.GetCellByNameAndID(ct);
                    if (cell != null)
                    {
                        if (cell.OwnMergeCell != null)
                        {
                            return cell.OwnMergeCell;
                        }
                        return cell;
                    }
                }
            }
            return null;
        }
        public virtual object CellFind(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                string text = base.GetTextValue(1, args);
                int mincolumnindex = base.GetIntValue(2, 0, args);
                int minrowindex = base.GetIntValue(3, 0, args);
                int maxcolumnindex = base.GetIntValue(4, 0, args);
                int maxrowindex = base.GetIntValue(5, 0, args);

                Point pt = Point.Empty;
                if (maxrowindex == 0)
                {
                    if (minrowindex == 0)
                    {
                        pt = proxy.Grid.ReSetHasValue();
                        minrowindex = 1;
                        maxrowindex = pt.X;
                    }
                    else
                    {
                        maxrowindex = minrowindex;
                    }
                }
                if (maxcolumnindex == 0)
                {
                    if (mincolumnindex == 0)
                    {
                        pt = proxy.Grid.ReSetHasValue();
                        mincolumnindex = 1;
                        maxcolumnindex = pt.Y;
                    }
                    else
                    {
                        maxcolumnindex = mincolumnindex;
                    }
                }
                for (int row = minrowindex; row <= maxrowindex; row++)
                {
                    for (int column = mincolumnindex; column <= maxcolumnindex; column++)
                    {
                        ICell cell = proxy.Grid.GetCell(row, column);
                        if (cell != null)
                        {
                            if (cell.OwnMergeCell != null)
                            {
                                cell = cell.OwnMergeCell;
                            }
                            string txt = Feng.Utils.ConvertHelper.ToString(cell.Value);
                            Feng.Utils.TraceHelper.WriteTrace("", "", "FindCell", true, txt);
                            if (text == txt)
                            {
                                return cell;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public virtual object CellLike(params object[] args)
        {
#warning 未完成 返回集合            
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                string text = base.GetTextValue(1, args);
                SelectCellCollection cells = base.GetArgIndex(2, args) as SelectCellCollection;
                if (cells == null)
                {
                    foreach (IRow row in proxy.Grid.Rows)
                    {
                        foreach (IColumn column in proxy.Grid.Columns)
                        {
                            ICell cell = row.Cells[column];
                            if (cell == null)
                            {
                                continue;
                            }

                        }
                    }
                }
            }
            return null;
        }
        public virtual object CellValue(params object[] args)
        {
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                if (cell != null)
                {
                    object value = base.GetArgIndex(2, args);
                    cell.Value = value;
                    return Feng.Utils.Constants.OK;
                }
                else
                {
                    return Feng.Utils.Constants.Fail;
                }
            }
            if (cell != null)
            {
                return cell.Value;
            }
            return null;
        }
        public virtual object CellText(params object[] args)
        {
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                string value = base.GetTextValue(2, args);
                cell.Text = value;
                return Feng.Utils.Constants.OK;
            }
            if (cell != null)
            {
                return cell.Text;
            }
            return null;
        } 
        public virtual object CellScript(params object[] args)
        {
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                string value = base.GetTextValue(2, args);
                cell.Expression = value;
                return Feng.Utils.Constants.OK;
            }
            if (cell != null)
            {
                return cell.Expression;
            }
            return null;
        }
        public virtual object CellText1(params object[] args)
        {
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                string value = base.GetTextValue(2, args);
                cell.Text1 = value;
                return Feng.Utils.Constants.OK;
            }
            if (cell != null)
            {
                return cell.Text1;
            }
            return null;
        }
        public virtual object CellText2(params object[] args)
        {
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                string value = base.GetTextValue(2, args);
                cell.Text2 = value;
                return Feng.Utils.Constants.OK;
            }
            if (cell != null)
            {
                return cell.Text2;
            }
            return null;
        }
        public virtual object CellText3(params object[] args)
        {
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                string value = base.GetTextValue(2, args);
                cell.Text3 = value;
                return Feng.Utils.Constants.OK;
            }
            if (cell != null)
            {
                return cell.Text3;
            }
            return null;
        }
 
        public virtual object CellDateTime(params object[] args)
        {
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                object obj = base.GetArgIndex(2, args);
                DateTime? value = Feng.Utils.ConvertHelper.ToDateTimeNullable(obj);
 
                cell.Value = value;
                return Feng.Utils.Constants.OK;
            }
            if (cell != null)
            {
                return Feng.Utils.ConvertHelper.ToDateTimeNullable(cell.Value);
            }
            return null;
        }
        public virtual object CellInt(params object[] args)
        {
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                int value = base.GetIntValue(2, args);
                cell.Value = value;
                return Feng.Utils.Constants.OK;
            }
            if (cell != null)
            {
                return Feng.Utils.ConvertHelper.ToInt32(cell.Value);
            }
            return null;
        }
        public virtual object CellLong(params object[] args)
        {
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                long value = base.GetLongValue(2, args);
                cell.Value = value;
                return Feng.Utils.Constants.OK;
            }
            if (cell != null)
            {
                return Feng.Utils.ConvertHelper.ToInt64(cell.Value);
            }
            return null;
        }
        public virtual object CellDecimal(params object[] args)
        {
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                decimal value = base.GetDecimalValue(2, args);
                cell.Value = value;
                return Feng.Utils.Constants.OK;
            }
            if (cell != null)
            {
                return Feng.Utils.ConvertHelper.ToDecimal(cell.Value);
            }
            return null;
        }
        public virtual object CellBool(params object[] args)
        {
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                bool value = base.GetBooleanValue(2, args);
                cell.Value = value;
                return Feng.Utils.Constants.OK;
            }
            if (cell != null)
            {
                return Feng.Utils.ConvertHelper.ToBoolean(cell.Value);
            }
            return null;
        }
        public virtual object CellID(params object[] args)
        {
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                string id = base.GetTextValue(2, args);
                cell.ID = id;
                return Feng.Utils.Constants.OK;
            }
            if (cell != null)
            {
                return cell.ID;
            }
            return null;
        }
        public virtual object Cells(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;


            ICell begincell = null;
            ICell endCell = null;
            SelectCellCollection selectcell = this.GetCell(1, args) as SelectCellCollection;
            if (selectcell != null)
            {
                return selectcell.GetAllCells();
            }
            begincell = this.GetCell(1, args);
            endCell = this.GetCell(2, args);
            if (endCell == null)
            {
                endCell = begincell;
            }
            if (endCell != null)
            {
                selectcell = new SelectCellCollection();
                selectcell.BeginCell = begincell;
                selectcell.EndCell = endCell;
                return selectcell.GetAllCells();
            }

            if (true)
            { 
                string cellreange = base.GetTextValue(1, args);
                if (cellreange.Contains(":"))
                {
                    string[] cells = cellreange.Split(':');
                    if (cells.Length != 2)
                    {
                        return null;
                    }
                    begincell = proxy.Grid.GetCellByNameAndID(cells[0]);
                    if (begincell == null)
                        return null;

                    endCell = proxy.Grid.GetCellByNameAndID(cells[1]);
                    if (endCell == null)
                    {
                        endCell = begincell;
                    }
                    selectcell = new SelectCellCollection();
                    selectcell.BeginCell = begincell;
                    selectcell.EndCell = endCell;
                    return selectcell.GetAllCells();
                }
            }
            List<ICell> list = new List<ICell>();
            for (int m = 1; m < args.Length; m++)
            {
                string text = base.GetTextValue(m, args);
                string[] textes = Feng.Utils.TextHelper.Split(text, ",");

                for (int i = 0; i < textes.Length; i++)
                {
                    string cellreange = textes[i];
                    selectcell = DataExcelTools.GetSelectCell(proxy.Grid, cellreange);
                    if (selectcell != null)
                    {
                        list.AddRange(selectcell.GetAllCells());
                    }
                }
            }
            return list;
        }
        public virtual object CellValues(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            List<object> values = new List<object>();
            ISelectCellCollection selectCellCollection = base.GetArgIndex(1, args) as ISelectCellCollection;
            if (selectCellCollection!=null)
            {
                List<ICell> cells = selectCellCollection.GetAllCells();
                foreach (ICell item in cells)
                {
                    if (item != null)
                    {
                        values.Add(item.Value);
                    }
                }
                return values;
            }
 
            IEnumerable<ICell> list = base.GetArgIndex(1, args) as IEnumerable<ICell>;
            if (list == null)
            {
                list = Cells(args) as List<ICell>;
            }
            if (list != null)
            {
                foreach (ICell item in list)
                {
                    if (item != null)
                    {
                        values.Add(item.Value);
                    }
                }
            }
            return values;
        }
        public virtual object CellRange(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {

                ICell begincell = null;
                ICell endCell = null;
                SelectCellCollection selectcell = null;
                begincell = this.GetCell(1, args);
                endCell = this.GetCell(2, args);
                if (endCell == null)
                {
                    endCell = begincell;
                }
                if (endCell != null)
                {
                    selectcell = new SelectCellCollection();
                    selectcell.BeginCell = begincell;
                    selectcell.EndCell = endCell;
                    return selectcell;
                }

                string cellreange = base.GetTextValue(1, args);
                if (!cellreange.Contains(":"))
                {
                    return null;
                }
                string[] cells = cellreange.Split(':');
                if (cells.Length != 2)
                {
                    return null;
                }
                begincell = proxy.Grid.GetCellByNameAndID(cells[0]);
                if (begincell == null)
                    return null;

                endCell = proxy.Grid.GetCellByNameAndID(cells[1]);
                if (endCell == null)
                {
                    endCell = begincell;
                }
                selectcell = new SelectCellCollection();
                selectcell.BeginCell = begincell;
                selectcell.EndCell = endCell;
                return selectcell;
            }
            return null;
        }
        public virtual object CellList(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {

                ICell begincell = null;
                ICell endCell = null;
                SelectCellCollection selectcell = null;
                begincell = this.GetCell(1, args);
                endCell = this.GetCell(2, args);
                if (endCell == null)
                {
                    endCell = begincell;
                }
                if (endCell != null)
                {
                    selectcell = new SelectCellCollection();
                    selectcell.BeginCell = begincell;
                    selectcell.EndCell = endCell;
                    return selectcell.GetAllCells();
                }

                string cellreange = base.GetTextValue(1, args);
                if (!cellreange.Contains(":"))
                {
                    return null;
                }
                string[] cells = cellreange.Split(':');
                if (cells.Length != 2)
                {
                    return null;
                }
                begincell = proxy.Grid.GetCellByNameAndID(cells[0]);
                if (begincell == null)
                    return null;

                endCell = proxy.Grid.GetCellByNameAndID(cells[1]);
                if (endCell == null)
                {
                    endCell = begincell;
                }
                selectcell = new SelectCellCollection();
                selectcell.BeginCell = begincell;
                selectcell.EndCell = endCell;
                return selectcell.GetAllCells();
            }
            return null;
        }
        public virtual object CellRangeExecScript(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                SelectCellCollection selectcell = CellRange(args[0], args[1]) as SelectCellCollection;
                if (selectcell == null)
                {
                    return Feng.Utils.Constants.Fail;
                }

                string script = base.GetTextValue(2, args);
                if (!string.IsNullOrWhiteSpace(script))
                {
                    List<ICell> list = selectcell.GetAllCells();
                    foreach (ICell item in list)
                    {
                        Feng.Excel.Script.Function.RunExpress(proxy.Grid, item, script);
                    }
                    return Feng.Utils.Constants.OK;
                }
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object CellRangeExecFunction(params object[] args)
        { 
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                List<ICell> list = null;
                SelectCellCollection selectcell = CellRange(args[0], args[1]) as SelectCellCollection;
                if (selectcell != null)
                {
                    list = selectcell.GetAllCells();
                }
                else
                {
                    list = base.GetArgIndex(1, args) as List<ICell>;
                }
                if (list != null)
                {
                    return Feng.Utils.Constants.Fail;
                }
                string function = base.GetTextValue(2, args);
                if (!string.IsNullOrWhiteSpace(function))
                {
                    object scriptobj = proxy.Grid.FunctionList[function];
                    if (scriptobj == null)
                    {
                        return Feng.Utils.Constants.Fail;
                    }
                    string script = scriptobj.ToString();
                    foreach (ICell item in list)
                    {
                        Feng.Excel.Script.Function.RunExpress(proxy.Grid, item, script);
                    }
                    return Feng.Utils.Constants.OK;
                }
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object CellName(params object[] args)
        {
            ICell cell = this.GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (cell != null)
            {
                return cell.Name;
            }
            return null;
        }
        public virtual object CellCaption(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                string value = this.GetTextValue(2, args);
                if (cell != null)
                {
                    cell.Caption = value;
                    return Feng.Utils.Constants.OK;
                }
            }
            else
            { 
                if (cell != null)
                {
                    return cell.Caption;
                } 
            }

            return null;
        }
        public virtual object CellAction(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            string shortname = this.GetTextValue(2, args);
            if (cell != null)
            {
                if (args.Length > 3)
                {
                    string script = this.GetTextValue(3, args);
                    Actions.PropertyActionTools.ActionScript(cell, shortname, script);
                    return Feng.Utils.Constants.OK;
                }
                else
                {
                    return Actions.PropertyActionTools.ActionScript(cell, shortname);
                }
            }
            return null;
        }
        public virtual object CellTable(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }

            string tablename = this.GetTextValue(2, args);
            int tablerowindex = this.GetIntValue(3,-1, args);
            string tablecolumnname = this.GetTextValue(4, args);
            if (cell != null)
            {
                if (cell.OwnMergeCell != null)
                {
                    cell = cell.OwnMergeCell;
                }
                cell.TableName=tablename;
                cell.TableRowIndex = tablerowindex;
                cell.TableColumnName = tablecolumnname;
                return Feng.Utils.Constants.OK;
            }


            return null;
        }
        public virtual object CellTableName(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }

            string tablename = this.GetTextValue(2, args);
            if (args.Length == 3)
            {
                if (cell != null)
                {
                    if (cell.OwnMergeCell != null)
                    {
                        cell = cell.OwnMergeCell;
                    }
                    cell.TableName = tablename;
                    return Feng.Utils.Constants.OK;
                }
            }
            else
            {
                return cell.TableName;
            } 
            return null;
        }
        public virtual object CellTableRowIndex(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }

            int tablerowindex = this.GetIntValue(2, -1, args);
            if (args.Length == 3)
            {
                if (cell != null)
                {
                    if (cell.OwnMergeCell != null)
                    {
                        cell = cell.OwnMergeCell;
                    }
                    cell.TableRowIndex = tablerowindex;
                    return Feng.Utils.Constants.OK;
                }
            }
            else
            {
                return cell.TableRowIndex;
            }
            return null;
        }
        public virtual object CellTableColumnName(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }

            string tablecolumnname = this.GetTextValue(2, args);
            if (args.Length == 3)
            {
                if (cell != null)
                {
                    if (cell.OwnMergeCell != null)
                    {
                        cell = cell.OwnMergeCell;
                    }
                    cell.TableColumnName = tablecolumnname;
                    return Feng.Utils.Constants.OK;
                }
            }
            else
            {
                return cell.TableColumnName;
            }
            return null;
        }
        public virtual object CellProperty(params object[] args)
        {
            object value1 = GetArgIndex(1, args);
            string propertyname = base.GetTextValue(2, args);
            object res = ReflectionHelper.GetValue(value1, propertyname);
            return res;
        }
        public virtual object CellColumnIndex(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (cell != null)
            {
                return cell.Column.Index;
            }
            return null;
        }
        public virtual object CellColumnName(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;

            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (cell != null)
            {
                return cell.Column.Name;
            }
            return null;
        }
        public virtual object CellRowIndex(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (cell != null)
            {
                return cell.Row.Index;
            }
            return null;
        }

        public virtual object CellColumnMaxIndex(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (cell != null)
            {
                return cell.MaxColumnIndex;
            }
            return null;
        }
        public virtual object CellRowMaxIndex(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (cell != null)
            {
                return cell.MaxRowIndex;
            }
            return null;
        }
        public virtual object CellUp(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            int len = GetIntValue(2, 1, args);
            if (cell == null)
                return null;
            if (len > 1)
            {
                for (int i = 0; i < len; i++)
                {
                    cell = CellUp(args[0], cell) as ICell;
                }
                return cell;
            }
            else
            {
                len = 1;
            }
            if (cell != null)
            {
                if (cell.OwnMergeCell != null)
                {
                    cell = cell.OwnMergeCell;
                }
                ICell rescell = cell.Grid[cell.Row.Index - len, cell.Column.Index];
                if (rescell != null)
                {
                    if (rescell.OwnMergeCell != null)
                    {
                        rescell = rescell.OwnMergeCell;
                    }
                    return rescell;
                }
            }
            return null;
        }
        public virtual object CellDown(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            int len = GetIntValue(2, 1, args);
            if (cell == null)
                return null;
            if (len > 1)
            {
                for (int i = 0; i < len; i++)
                {
                    cell = CellDown(args[0], cell) as ICell;
                }
                return cell;
            }
            else
            {
                len = 1;
            }
            if (cell != null)
            {
                if (cell.OwnMergeCell != null)
                {
                    cell = cell.OwnMergeCell;
                }
                ICell rescell = cell.Grid[cell.MaxRowIndex + len, cell.Column.Index];

                if (rescell != null)
                {
                    if (rescell.OwnMergeCell != null)
                    {
                        rescell = rescell.OwnMergeCell;
                    }
                    return rescell;
                }
            }

            return null;
        }
        public virtual object CellLeft(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            int len = GetIntValue(2, -1, args);
            if (cell == null)
                return null;
            if (len > 1)
            {
                for (int i = 0; i < len; i++)
                {
                    cell = CellLeft(args[0], cell) as ICell;
                }
                return cell;
            }
            else
            {
                len = 1;
            }
            if (cell.OwnMergeCell != null)
            {
                cell = cell.OwnMergeCell;
            }
            ICell rescell = cell.Grid[cell.Row.Index, cell.Column.Index - len];
            if (rescell != null)
            {
                if (rescell.OwnMergeCell != null)
                {
                    rescell = rescell.OwnMergeCell;
                }
                return rescell;
            }

            return null;
        }
        public virtual object CellsLeft(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ICell cellme= GetArgIndex(1, args) as ICell;
            IMergeCell cell = null;
            if (cellme != null)
            {
                cell = cellme.OwnMergeCell;
            } 
            int len = GetIntValue(2, 0, args);
            if (cell == null)
                return null;
            ICell begincell = proxy.Grid[cell.Row.Index, cell.Column.Index - len];
            ICell endCell = proxy.Grid[cell.MaxRowIndex, cell.Column.Index - len];
            if (endCell == null)
            {
                endCell = begincell;
            }
            if (endCell != null)
            {
                SelectCellCollection selectcell = new SelectCellCollection();
                selectcell.BeginCell = begincell;
                selectcell.EndCell = endCell;
                return selectcell.GetAllCells();
            }
            return null;
        }
        public virtual object CellRight(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ICell cell = GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            int len = GetIntValue(2, 1, args);
            if (cell == null)
                return null;
            if (len > 1)
            {
                for (int i = 0; i < len; i++)
                {
                    cell = CellRight(args[0], cell) as ICell;
                }
                return cell;
            }
            else
            {
                len = 1;
            }
            if (cell.OwnMergeCell != null)
            {
                cell = cell.OwnMergeCell;
            }
            ICell rescell = cell.Grid[cell.Row.Index, cell.MaxColumnIndex + len];
            if (rescell != null)
            {
                if (rescell.OwnMergeCell != null)
                {
                    rescell = rescell.OwnMergeCell;
                }
                return rescell;
            }

            return null;
        }
        public virtual object CellsRight(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            IMergeCell cell = GetArgIndex(1, args) as IMergeCell;
            int len = GetIntValue(2, 0, args);
            if (cell == null)
                return null;
            ICell begincell = proxy.Grid[cell.Row.Index, cell.Column.Index + len];
            ICell endCell = proxy.Grid[cell.MaxRowIndex, cell.Column.Index + len];
            if (endCell == null)
            {
                endCell = begincell;
            }
            if (endCell != null)
            {
                SelectCellCollection selectcell = new SelectCellCollection();
                selectcell.BeginCell = begincell;
                selectcell.EndCell = endCell;
                return selectcell.GetAllCells();
            }
            return null;
        }
        public virtual object CellFocused(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;

            if (proxy != null)
            {
                ICell cell = base.GetCell(1, args);
                if (cell == null)
                {
                    cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
                }
                if (cell != null)
                {
                    cell.Grid.FocusedCell = cell;
                    return Feng.Utils.Constants.OK;
                }

                return cell.Grid.FocusedCell;
            }

            return Feng.Utils.Constants.Fail;
        }
        public virtual object CellClear(params object[] args)
        {
            ICell cell = base.GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            cell.Clear();
            return Feng.Utils.Constants.OK;
        }
        public virtual object CellTrim(params object[] args)
        {
            ICell cell = base.GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            cell.Value = cell.Text.Trim();
            return Feng.Utils.Constants.OK;
        }
        public virtual object CellHide(params object[] args)
        {
            ICell cell = base.GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            cell.Visible = false;
            return Feng.Utils.Constants.OK;
        }
        public virtual object CellShow(params object[] args)
        {
            ICell cell = base.GetArgIndex(1, args) as ICell;
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            cell.Visible = true;
            return Feng.Utils.Constants.OK;
        }
        public virtual object CellVisible(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                bool value = this.GetBooleanValue(2, args);
                if (cell != null)
                {
                    cell.Visible = value;
                    return Feng.Utils.Constants.OK;
                }
            }
            else
            {
                return cell.Visible;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object CellMerge(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                ICell cellbegin = GetParamsCell(1, args) as ICell;
                ICell cellend = GetParamsCell(2, args) as ICell;
                IMergeCell mergeCell = cellbegin.Grid.MergeCell(cellbegin, cellend);
                return mergeCell;
            }
            return null;
        }
#warning 合相同内容
        public virtual object CellUnMerge(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            if (proxy != null)
            {
                ICell cell = base.GetArgIndex(1, args) as ICell;
                if (cell == null)
                {
                    cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
                }

                IMergeCell mergeCell = null;
                if (cell is IMergeCell)
                {
                    mergeCell = cell as IMergeCell;
                }
                if (cell.OwnMergeCell != null)
                {
                    mergeCell = cell.OwnMergeCell;
                }
                cell.Grid.UnMergeCell(mergeCell);
                return Feng.Utils.Constants.OK;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object CellGetMergeCell(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                ICell cell = base.GetArgIndex(1, args) as ICell;
                if (cell != null)
                {
                    return cell.OwnMergeCell;
                } 
            }
            return null;
        }
        public virtual object CellEdit(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy != null)
            {
                ICell cell = this.GetCell(1, args);
                if (cell == null)
                {
                    cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
                }
                if (args.Length == 3)
                {
                    string shortname = base.GetTextValue(1, args);
                    ICellEditControl edit = Feng.Excel.Edits.EditControlBuilder.Build(cell.Grid, shortname);
                    cell.OwnEditControl = edit;
                }
                else
                {
                    return cell.OwnEditControl;
                }

            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object CellReadOnly(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                bool value = this.GetBooleanValue(2, args);
                if (cell != null)
                {
                    cell.ReadOnly = value;
                    cell.InhertReadOnly = false;
                    return Feng.Utils.Constants.OK;
                }
            }
            else
            {
                return cell.ReadOnly;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object CellBackImage(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                Bitmap value = this.GetArgIndex(2, args) as Bitmap;
                if (cell != null)
                {
                    cell.BackImage = value;
                    return Feng.Utils.Constants.OK;
                }
            }
            else
            {
                return cell.BackImage;
            }

            return Feng.Utils.Constants.Fail;
        }
        public virtual object CellBackColor(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                Color value = Feng.Utils.ConvertHelper.ToColor(this.GetIntValue(2, args));
                if (cell != null)
                {
                    cell.BackColor = value;
                    return Feng.Utils.Constants.OK;
                }
            }
            else
            {
                return cell.BackColor;
            }

            return Feng.Utils.Constants.Fail;
        }
        public virtual object CellBorderOutside(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length >= 3)
            {
                bool value = this.GetBooleanValue(2, args);
                cell.Grid.CellBorderClear(cell, !value);
            }
            else
            {
                cell.Grid.CellBorderClear(cell, false);
            }

            return Feng.Utils.Constants.OK;
        }
        public virtual object CellBorderTop(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length >= 3)
            {
                bool value = this.GetBooleanValue(2, args);
                cell.Grid.CreateCellBorderTop(cell, value);
            }
            else
            {
                cell.Grid.CreateCellBorderTop(cell);
            }

            return Feng.Utils.Constants.OK;
        }
        public virtual object CellBorderBottom(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length >= 3)
            {
                bool value = this.GetBooleanValue(2, args);
                cell.Grid.CreateCellBorderBottom(cell, value);
            }
            else
            {
                cell.Grid.CreateCellBorderBottom(cell);
            }

            return Feng.Utils.Constants.OK;
        }
        public virtual object CellBorderLeft(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length >= 3)
            {
                bool value = this.GetBooleanValue(2, args);
                cell.Grid.CreateCellBorderLeft(cell, value);
            }
            else
            {
                cell.Grid.CreateCellBorderLeft(cell);
            }

            return Feng.Utils.Constants.OK;
        }
        public virtual object CellBorderRight(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length >= 3)
            {
                bool value = this.GetBooleanValue(2, args);
                cell.Grid.CreateCellBorderRight(cell, value);
            }
            else
            {
                cell.Grid.CreateCellBorderRight(cell);
            }

            return Feng.Utils.Constants.OK;
        }
        public virtual object CellFormatNumber(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            string format = base.GetTextValue(2, args);
            cell.FormatType = FormatType.Numberic;
            cell.FormatString = format;

            return Feng.Utils.Constants.OK;
        }
        public virtual object CellFormatDateTime(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            string format = base.GetTextValue(2, args);
            cell.FormatType = FormatType.DateTime;
            cell.FormatString = format;

            return Feng.Utils.Constants.OK;
        }
        public virtual object CellFormatClear(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            cell.FormatType = FormatType.Null;
            cell.FormatString = string.Empty;

            return Feng.Utils.Constants.OK;
        }
        public virtual object CellAlignmentCenter(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            cell.HorizontalAlignment = StringAlignment.Center;
            cell.VerticalAlignment = StringAlignment.Center;

            return Feng.Utils.Constants.OK;
        }
        public virtual object CellAlignmentTop(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            cell.VerticalAlignment = StringAlignment.Near;

            return Feng.Utils.Constants.OK;
        }
        public virtual object CellAlignmentBottom(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            cell.VerticalAlignment = StringAlignment.Far;

            return Feng.Utils.Constants.OK;
        }
        public virtual object CellAlignmentLeft(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            cell.HorizontalAlignment = StringAlignment.Near;

            return Feng.Utils.Constants.OK;
        }
        public virtual object CellAlignmentRight(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            cell.HorizontalAlignment = StringAlignment.Far;

            return Feng.Utils.Constants.OK;
        }
        public virtual object CellAlignmentHorizontalCenter(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            cell.HorizontalAlignment = StringAlignment.Center;
            return Feng.Utils.Constants.OK;
        }
        public virtual object CellAlignmentVerticalCenter(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            cell.VerticalAlignment = StringAlignment.Center;
            return Feng.Utils.Constants.OK;
        }
        public virtual object CellAutoMultiline(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                bool value = this.GetBooleanValue(2, args);
                if (cell != null)
                {
                    cell.AutoMultiline = value;
                    return Feng.Utils.Constants.OK;
                }
            }
            else
            {
                return cell.AutoMultiline;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object CellTabStop(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                int value = this.GetIntValue(2, args);
                if (cell != null)
                {
                    cell.TabIndex = value;
                    return Feng.Utils.Constants.OK;
                }
            }
            else
            {
                return cell.TabIndex;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object CellTextDirection(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                bool value = this.GetBooleanValue(2, args);
                if (cell != null)
                {
                    cell.DirectionVertical = value;
                    return Feng.Utils.Constants.OK;
                }
            }
            else
            {
                return cell.DirectionVertical;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object CellFont(params object[] args)
        {
#warning 需要优化
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length >= 3)
            {
                string fontname = this.GetTextValue(2, args);
                float fontsize = this.GetFloatValue(3, cell.Font.Size, args);
                if (cell != null)
                {
                    Font font = new Font(fontname, fontsize);
                    cell.Font = font;
                    return Feng.Utils.Constants.OK;
                }
            }
            else
            {
                return cell.Font;
            }
            return Feng.Utils.Constants.Fail;
        }
        public virtual object CellForeColor(params object[] args)
        {
            ICell cell = this.GetCell(1, args);
            if (cell == null)
            {
                cell = Cell(GetArgIndex(0, args), GetArgIndex(1, args)) as ICell;
            }
            if (args.Length == 3)
            {
                Color value = Color.Empty;
                object objvalue = base.GetArgIndex(2,args);
                if (objvalue is Color)
                {
                    value = (Color)objvalue;
                }
                if (value == Color.Empty)
                { 
                    int artvalue = this.GetIntValue(2, -1, args);
                    if (artvalue > 0)
                    {
                        value = Feng.Utils.ConvertHelper.ToColor(artvalue);
                    }
                }
                if (value == Color.Empty)
                {
                    string artvalue = this.GetTextValue(2, -1, args);
                    if (!string.IsNullOrEmpty(artvalue))
                    {
                        value = Color.FromName(artvalue);
                    }
                }
                if (cell != null)
                {
                    if (value != Color.Empty)
                    {
                        cell.ForeColor = value;
                        return Feng.Utils.Constants.OK;
                    }
                }
            }
            else
            {
                return cell.ForeColor;
            }

            return Feng.Utils.Constants.Fail;
        }
        public virtual object CellListFill(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;

            ICell cell = this.GetCell(1, args);
            Feng.Collections.ListEx<object> values = base.GetArgIndex(2, args) as Feng.Collections.ListEx<object>;

            int start = base.GetIntValue(3, 0, args);
            int len = base.GetIntValue(4, values.Count, args);
            if (cell != null)
            {
                int row = cell.MaxRowIndex + 1;
                int column = cell.Column.Index;
                for (int i = start; i < start + len; i++)
                {
                    cell = proxy.Grid[row, column];
                    cell.Value = values[i];
                    row++;
                }
                return Feng.Utils.Constants.OK;
            }
            List<ICell> cells = base.GetArgIndex(1, args) as List<ICell>;
            if (cells != null)
            {
                for (int i = start, j = 0; (i < start + len) && j < cells.Count; i++, j++)
                {
                    cell = cells[j];
                    cell.Value = values[i];
                }
                return Feng.Utils.Constants.OK;
            }
            return null;
        }
        public virtual object CellRefernce(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            string targetfile = proxy.Grid.FileName;
#warning 表格引用
            return null;
        }

        private ICell GetParamsCell(int index, params object[] args)
        {
            ICell cell = base.GetArgIndex(index, args) as ICell;
            if (cell != null)
            {
                return cell;
            }
            cell = Cell(GetArgIndex(0, args), GetArgIndex(index, args)) as ICell;
            if (cell != null)
            {
                return cell;
            }
            return null;
        }


        public virtual object CellMoveUp(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }
            ISelectCellCollection cells = GetArgIndex(1, args) as ISelectCellCollection;
            if (cells == null)
            {
                ICell cel = GetArgIndex(1, args) as ICell;
                if (cel !=null )
                {
                    cells = new SelectCellCollection();
                    cells.BeginCell = cel;
                    cells.EndCell = cel;
                }
            }
            ICell cell = GetArgIndex(2, args) as ICell;
            int step = -1;
            if (cell ==null )
            {
                step = base.GetIntValue(2, -1, args);
            }
            if (cells != null)
            {
                if (cell != null)
                {
                    CellMoveUp(cells, cell);
                }
                else if (step>0)
                {
                    CellMoveUp(cells, step);
                }
                else
                {
                    CellMoveUp(cells);
                }
            }

            return Feng.Utils.Constants.OK;
        }
        public virtual object CellMoveDown(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }
            ISelectCellCollection cells = GetArgIndex(1, args) as ISelectCellCollection;
            if (cells == null)
            {
                ICell cel = GetArgIndex(1, args) as ICell;
                if (cel != null)
                {
                    cells = new SelectCellCollection();
                    cells.BeginCell = cel;
                    cells.EndCell = cel;
                }
            }
            ICell cell = GetArgIndex(2, args) as ICell;
            int step = -1;
            if (cell == null)
            {
                step = base.GetIntValue(2, -1, args);
            }
            if (cells != null)
            {
                if (cell != null)
                {
                    CellMoveDown(cells, cell);
                }
                else if (step > 0)
                {
                    CellMoveDown(cells, step);
                }
                else
                {
                    CellMoveDown(cells);
                }
            }

            return Feng.Utils.Constants.OK;
        }
        public virtual object CellMoveLeft(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }
            ISelectCellCollection cells = GetArgIndex(1, args) as ISelectCellCollection;
            if (cells == null)
            {
                ICell cel = GetArgIndex(1, args) as ICell;
                if (cel != null)
                {
                    cells = new SelectCellCollection();
                    cells.BeginCell = cel;
                    cells.EndCell = cel;
                }
            }
            ICell cell = GetArgIndex(2, args) as ICell;
            int step = -1;
            if (cell == null)
            {
                step = base.GetIntValue(2, -1, args);
            }
            if (cells != null)
            {
                if (cell != null)
                {
                    CellMoveLeft(cells, cell);
                }
                else if (step > 0)
                {
                    CellMoveLeft(cells, step);
                }
                else
                {
                    CellMoveLeft(cells);
                }
            }

            return Feng.Utils.Constants.OK;
        }
        public virtual object CellMoveRight(params object[] args)
        {
            ICBContext proxy = args[0] as ICBContext;
            if (proxy == null)
            {
                return null;
            }
            ISelectCellCollection cells = GetArgIndex(1, args) as ISelectCellCollection;
            if (cells == null)
            {
                ICell cel = GetArgIndex(1, args) as ICell;
                if (cel != null)
                {
                    cells = new SelectCellCollection();
                    cells.BeginCell = cel;
                    cells.EndCell = cel;
                }
            }
            ICell cell = GetArgIndex(2, args) as ICell;
            int step = -1;
            if (cell == null)
            {
                step = base.GetIntValue(2, -1, args);
            }
            if (cells != null)
            {
                if (cell != null)
                {
                    CellMoveRight(cells, cell);
                }
                else if (step > 0)
                {
                    CellMoveRight(cells, step);
                }
                else
                {
                    CellMoveRight(cells);
                }
            }

            return Feng.Utils.Constants.OK;
        }


        public virtual object CellSwap(params object[] args)
        {
            ICBContext context = args[0] as ICBContext;
            if (context == null)
                return null;
            FunctionArg proxy = context.Entity as FunctionArg;
            if (proxy == null)
                return null;
            ICell cel = GetArgIndex(1, args) as ICell;
            ICell cell = GetArgIndex(2, args) as ICell;
            proxy.Grid.Swap(cel, cell);
            return Feng.Utils.Constants.OK;
        }
    }
}
