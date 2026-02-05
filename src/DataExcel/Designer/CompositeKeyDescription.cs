using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing.Design;

namespace Feng.Excel.Description
{
    public class ExcelCompositeKeyDescription : Feng.Forms.Command.CompositeKeyDescription
    {
        public ExcelCompositeKeyDescription()
        {
            Add("CommandCancel", "取消");
            Add("CommandMoveFocusedCellToUp", "向上移动焦点单元格");
            Add("CommandMoveFocusedCellToDown", "向下移动焦点单元格");
            Add("CommandMoveFocusedCellToLeft", "向左移动焦点单元格");
            Add("CommandMoveFocusedCellToRight", "向右移动焦点单元格");
            Add("CommandMoveFocusedCellToTab", "Tab移动焦点单元格");
            Add("CommandSelectAll", "选中所有");
            Add("CommandCopy", "复制");
            Add("CommandCut", "剪切");
            Add("CommandPaste", "粘贴");
            Add("CommandFind", "查找");
            Add("CommandGo", "定位单元格");
            Add("CommandSum", "合计");
            Add("CommandSumSelectCells", "合计选中");
            Add("CommandRedo", "撤消");
            Add("CommandUndo", "重做");
            Add("CommandSave", "保存");
            Add("CommandNew", "新建");
            Add("CommandOpen", "打开");
            Add("CommandFirstCell", "重定向第一个单元格");
            Add("CommandFocusedCellPrev", "重定向上一个单元格");
            Add("CommandFocusedCellNext", "重定向下一个单元格");
            Add("CommandCopyFormatMerge", "复制合计格式");
            Add("CommandBookmarkPrev", "重定向上个标签");

            Add("CommandSelectUp", "向上选中");
            Add("CommandSelectDown", "向下选中");
            Add("CommandSelectLeft", "向左选中");
            Add("CommandSelectRight", "向右选中");
            Add("CommandSelectUpMove", "选中上移");
            Add("CommandSelectDownMove", "选中下移");
            Add("CommandSelectLeftMove", "选中左移");
            Add("CommandSelectRightMove", "选中右移");
            Add("CommandShowShortcut", "显示快建提示");
            Add("CommandSelectUpText", "选中上文本");
            Add("CommandSelectDownText", "选中下文本");
            Add("CommandSelectLeftText", "选中左文本");
            Add("CommandSelectRightText", "选中右文本");
            Add("CommandMergeCell", "合并单元格");
            Add("CommandPasteLoop", "循环粘贴");
            Add("CommandPasteClear", "清除循环粘贴");
            Add("CommandSaveAs", "另存为");

            Add("CommandPasteFormat", "格式粘贴");
            Add("CommandTurnFocusdToFirstFocsedCellMark", "转至第一个焦点");
            Add("CommandTurnFocusdToFooterFocsedCellMark", "转至最后一个焦点");
            Add("CommandBorderFull", "设置全边框");
            Add("CommandShowCellInfo", "显示单元格信息");
            Add("CommandHideCellInfo", "隐藏单元格信息");

            Add("CommandBookmarkPrev", "重定向上个标签");
            Add("CommandBookmarkAdd", "添加标签");
            Add("CommandBookmarkDelete", "删除标签");
            Add("CommandBookmarkHeader", "重定向第一个标签");
            Add("CommandBookmarkFooter", "重定向最后一个标签");
            Add("CommandBorderFull", "设置全边框");
            Add("CommandBorderNull", "取消全边框");
            Add("CommandCopyID", "复制ID");
            Add("CommandBorderTop", "设置上边框");
            Add("CommandTextAlignTop", "设置文字上对齐");
            Add("CommandBorderBottom", "设置下边框");
            Add("CommandTextAlignBottom", "设置文字下对齐");
            Add("CommandBorderLeft", "设置左边框");
            Add("CommandTextAlignLeft", "设置文字左对齐");
            Add("CommandBorderRight", "设置右边框");
            Add("CommandTextAlignRight", "设置文字右对齐");
            Add("CommandBorderLeftTopToRightBottom", "设置右斜边框");
            Add("CommandBorderLeftBoomToRightTop", "设置左斜边框");
            Add("CommandTextAlignVerticalCenter", "设置文字垂直居中");
            Add("CommandBorderTopClear", "清除上边框");
            Add("CommandBorderBottomClear", "清除底边框");
            Add("CommandBorderLeftClear", "清除左边框");
            Add("CommandBorderRightClear", "清除右边框");
            Add("CommandBorderLeftTopToRightBottomClear", "清除右斜边框");
            Add("CommandBorderLeftBottomToRightTopClear", "清除左斜边框");
            Add("CommandBorderTopClear", "清除上边框");
            Add("CommandInsertCellMoveDown", "添加单元格");
            Add("CommandFontSizeUp", "放大字体");
            Add("CommandBorderBottomClear", "清除下边框");
            Add("CommandCopyFormatMerge", "复制合并格式");
            Add("CommandFontSizeDown", "缩小字体");
            Add("CommandBorderLeftClear", "清除左边框");
            Add("CommandInsertColumn", "添加列");
            Add("CommandCopyFormatMerge", "复制合并格式");
            Add("CommandFontItalic", "斜体字");
            Add("CommandBorderRightClear", "清除右边框");
            Add("CommandInsertCellMoveRight", "插入单元格右移");
            Add("CommandFontUnderline", "下划线");
            Add("CommandBorderLeftTopToRightBottomClear", "清除右余边框");
            Add("CommandPasteText", "粘贴文本");
            Add("CommandFontStrikeout", "删除线");
            Add("CommandBorderLeftBottomToRightTopClear", "清除左余边框");
            Add("CommandFontStrikeoutCancel", "取消删除线");
            Add("CommandInsertRow", "添加行");
            Add("CommandFontBold", "粗体字");
            Add("CommandCopyFormatMerge", "复制合并格式");
            Add("CommandFontUnderlineCancel", "取消下划线");
            Add("CommandCopyFormatMerge", "复制合并格式");
            Add("CommandPasteFormatColor", "粘贴颜色");
            Add("CommandFontCancel", "取消字体设置");
            Add("CommandCopyFormatMerge", "复制合并格式");
            Add("CommandCopyFormat", "复制格式");
            Add("CommandFontBoldCancel", "取消粗体");
            Add("CommandPasteFormatBorder", "粘贴边框");
            Add("CommandFontItalicCancel", "取消斜体");
            Add("CommandTextTrimSpace", "去除空格");
            Add("CommandTextTrimStartSpace", "去除开始处空格");
            Add("CommandTextTrimEndSpace", "去除结尾处空格");
            Add("CommandTextRemoveFooterSymbol", "去除结尾字符");
            Add("CommandTextRemoveFooterSymbol", "去除结尾字符");
            Add("CommandTextRemoveHeaderSymbol", "去除开始字符");
            Add("CommandTextAlignHorizontalCenter", "水平对齐");
            Add("CommandTextAlignCenter", "居中对齐");
            Add("CommandCellEditNull", "清除编辑格式");
            Add("CommandCellEditNumber", "数字编辑格式");
            Add("CommandCellEditLabel", "标签编辑格式");
            Add("CommandCellEditCheckBox", "复选框编辑格式");
            Add("CommandCellEditCnNumber", "中文数字编辑格式");
            Add("CommandCellEditComboBox", "选择框编辑格式");
            Add("CommandCellEditDateTime", "日期编辑格式");
            Add("CommandCellEditGridView", "表格编辑格式");
            Add("CommandCellEditImage", "图片编辑格式");
            Add("CommandCellEditLinkLabel", "链接标签编辑格式");
            Add("CommandCellEditPassword", "密码框编辑格式");
            Add("CommandCellEditRadioBox", "单选框编辑格式");
            Add("CommandCellEditTreeView", "树编辑格式");
            Add("CommandCellEditTime", "时间编辑格式");
        }
        public void Add(string key, string value)
        {
            if (dics.ContainsKey(key))
            {
                return;
            }
            dics.Add(key, value);
        }
        private Dictionary<string, string> dics = new Dictionary<string, string>();
        public override string GetDescription(string command)
        {
            if (dics.ContainsKey(command))
            {
                return dics[command];
            }
            return string.Empty;
        }

    }


}

 