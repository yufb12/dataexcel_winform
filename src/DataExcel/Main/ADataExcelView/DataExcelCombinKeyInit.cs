using Feng.Excel.Commands;
using Feng.Forms;
using Feng.Forms.Command;
using System.Windows.Forms;

namespace Feng.Excel
{
    partial class DataExcel
    {
        public virtual void InitDefaultMulitKeyActionFile()
        {
            CompositeKeys2.Add(new CommandObject(string.Empty, "File", "文件", CommandText.CommandNew, CommandText.CommandNew,
               KeysTools.GetCombinKeys(Keys.Control, Keys.N), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.N).ToString(), Keys.None.ToString(),
               "新建", CommandNew, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "File", "文件", CommandText.CommandClear, CommandText.CommandClear,
   KeysTools.GetCombinKeys(Keys.Control, Keys.Clear), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Clear).ToString(), Keys.None.ToString(),
   "清空", CommandClear, false));



            CompositeKeys2.Add(new CommandObject(string.Empty, "File", "文件", CommandText.CommandOpen, CommandText.CommandOpen,
                KeysTools.GetCombinKeys(Keys.Control, Keys.O), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.O).ToString(), Keys.None.ToString(),
                "打开", CommandOpen, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "File", "文件", CommandText.CommandSave, CommandText.CommandSave,
                KeysTools.GetCombinKeys(Keys.Control, Keys.S), KeysTools.GetCombinKeys(Keys.Control, Keys.S),
                KeysTools.ToString(Keys.Control, Keys.S), KeysTools.ToString(Keys.Control, Keys.S),
                "保存", CommandSave, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "File", "文件", CommandText.CommandSaveAs, CommandText.CommandSaveAs,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.S), Keys.None,
                KeysTools.ToString(Keys.Control, Keys.Shift, Keys.S), KeysTools.ToString(Keys.None),
                "另保存", CommandSaveAs, false));


            CompositeKeys2.Add(new CommandObject(string.Empty, "File", "文件", CommandText.CommandPrint, CommandText.CommandPrint,
                KeysTools.GetCombinKeys(Keys.Control, Keys.P), KeysTools.GetCombinKeys(Keys.Control, Keys.P),
                KeysTools.ToString(Keys.Control, Keys.P), KeysTools.ToString(Keys.Control, Keys.P),
                "打印", CommandPrint, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "File", "文件", CommandText.CommandPrintView, CommandText.CommandPrintView,
             KeysTools.GetCombinKeys(Keys.Control, Keys.P), KeysTools.GetCombinKeys(Keys.Control, Keys.V),
             KeysTools.ToString(Keys.Control, Keys.P), KeysTools.ToString(Keys.Control, Keys.V),
             "打印预览", CommandPrintView, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "File", "文件", CommandText.CommandPrintSetting, CommandText.CommandPrintSetting,
                KeysTools.GetCombinKeys(Keys.Control, Keys.P), KeysTools.GetCombinKeys(Keys.Control, Keys.S),
                KeysTools.ToString(Keys.Control, Keys.P), KeysTools.ToString(Keys.Control, Keys.S),
                "打印设置", CommandPrintSetting, false));


            CompositeKeys2.Add(new CommandObject(string.Empty, "File", "文件", CommandText.CommandPrintArea, CommandText.CommandPrintArea,
                KeysTools.GetCombinKeys(Keys.Control, Keys.P), KeysTools.GetCombinKeys(Keys.Control, Keys.A),
                KeysTools.ToString(Keys.Control, Keys.P), KeysTools.ToString(Keys.Control, Keys.A),
                "设置打印区域", CommandPrintArea, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "File", "文件", CommandText.CommandPrintAreaCancel, CommandText.CommandPrintAreaCancel,
                KeysTools.GetCombinKeys(Keys.Control, Keys.P), KeysTools.GetCombinKeys(Keys.Control, Keys.C, Keys.A),
                KeysTools.ToString(Keys.Control, Keys.P), KeysTools.ToString(Keys.Control, Keys.C, Keys.A),
                "取消打印区域", CommandPrintAreaCancel, false));
        }
        public virtual void InitDefaultMulitKeyActionEdit()
        {
            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandNe36, CommandText.CommandNe36,
    Keys.None, Keys.None, Keys.None.ToString(), Keys.None.ToString(),
   "更改列宽", CommandNew36, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandDefaultStyleCellColumn, CommandText.CommandDefaultStyleCellColumn,
    Keys.None, Keys.None, Keys.None.ToString(), Keys.None.ToString(),
   "列默认样式单元格", CommandDefaultStyleCellColumn, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandCancel, CommandText.CommandCancel,
       Keys.Escape, Keys.None, Keys.Escape.ToString(), Keys.None.ToString(),
       "取消编辑", CommandCancel, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandCopy, CommandText.CommandCopy,
KeysTools.GetCombinKeys(Keys.Control, Keys.C), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.C).ToString(), Keys.None.ToString(),
"复制", CommandCopy, false)); 

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandCut, CommandText.CommandCut,
    KeysTools.GetCombinKeys(Keys.Control, Keys.X), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.X).ToString(), Keys.None.ToString(),
    "剪切", CommandCut, true));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandPaste, CommandText.CommandPaste,
                KeysTools.GetCombinKeys(Keys.Control, Keys.V), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.V).ToString(), Keys.None.ToString(),
                "粘贴", CommandPaste, true));



            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandFind, CommandText.CommandFind,
    KeysTools.GetCombinKeys(Keys.Control, Keys.F), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.F).ToString(), Keys.None.ToString(),
    "查找", CommandFind, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandReplace, CommandText.CommandReplace,
    KeysTools.GetCombinKeys(Keys.Control, Keys.H), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.H).ToString(), Keys.None.ToString(),
    "替换", CommandReplace, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandGo, CommandText.CommandGo,
                KeysTools.GetCombinKeys(Keys.Control, Keys.G), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.G).ToString(), Keys.None.ToString(),
                "定位到", CommandGo, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandRedo, CommandText.CommandRedo,
           KeysTools.GetCombinKeys(Keys.Control, Keys.Y), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Y).ToString(), Keys.None.ToString(),
           "重做", CommandRedo, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandUndo, CommandText.CommandUndo,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Z), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Z).ToString(), Keys.None.ToString(),
                "撤消", CommandUndo, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandRepeat, CommandText.CommandRepeat,
                KeysTools.GetCombinKeys(Keys.Control, Keys.R), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.R).ToString(), Keys.None.ToString(),
                "重复命令", CommandRepeat, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandRemember, CommandText.CommandRemember,
                KeysTools.GetCombinKeys(Keys.Control, Keys.M), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.M).ToString(), Keys.None.ToString(),
                "开结/结束记忆命令", CommandRemember, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandPasteBorder, CommandText.CommandPasteBorder,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.V), KeysTools.GetCombinKeys(Keys.Control, Keys.B), KeysTools.GetCombinKeys(Keys.Shift, Keys.V).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(),
                "粘贴边框", CommandPasteBorder));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandPasteLoop, CommandText.CommandPasteLoop,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.V), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.V).ToString(), Keys.None.ToString(),
                "循环粘贴", CommandPasteLoop));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandPasteClear, CommandText.CommandPasteClear,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.C), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.C).ToString(), Keys.None.ToString(),
                "清除粘贴板", CommandPasteClear));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandColumn36, CommandText.CommandColumn36,
  KeysTools.GetCombinKeys(Keys.Shift, Keys.P,  Keys.C), KeysTools.GetCombinKeys(Keys.Shift, Keys.P, Keys.C), 
  KeysTools.ToString(Keys.Shift, Keys.P, Keys.C), KeysTools.ToString(Keys.Shift, Keys.P, Keys.C),
  "列宽", CommandColumn36));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandInsertRow, CommandText.CommandInsertRow,
              KeysTools.GetCombinKeys(Keys.Shift, Keys.S), KeysTools.GetCombinKeys(Keys.Control, Keys.Q), KeysTools.ToString(Keys.Control, Keys.S), KeysTools.ToString(Keys.Control, Keys.Q),
              "插入行", CommandInsertRow));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandInsertColumn, CommandText.CommandInsertColumn,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.S), KeysTools.GetCombinKeys(Keys.Control, Keys.W), KeysTools.ToString(Keys.Control, Keys.S), KeysTools.ToString(Keys.Control, Keys.W),
                "插入列", CommandInsertColumn));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandInsertCellMoveDown, CommandText.CommandInsertCellMoveDown,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.S), KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.S), KeysTools.ToString(Keys.Control, Keys.E),
                "插入单元格下移", CommandInsertCellMoveDown));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandInsertCellMoveRight, CommandText.CommandInsertCellMoveRight,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.S), KeysTools.GetCombinKeys(Keys.Control, Keys.R), KeysTools.ToString(Keys.Control, Keys.S), KeysTools.ToString(Keys.Control, Keys.R),
                "插入单元格右移", CommandInsertCellMoveRight));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandDeleteRow, CommandText.CommandDeleteRow,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.S), KeysTools.GetCombinKeys(Keys.Control, Keys.D), KeysTools.ToString(Keys.Control, Keys.S), KeysTools.ToString(Keys.Control, Keys.D),
                "删除行", CommandDeleteRow));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandDeleteEmptyRow, CommandText.CommandDeleteEmptyRow,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.S), KeysTools.GetCombinKeys(Keys.Control, Keys.L), KeysTools.ToString(Keys.Control, Keys.S), KeysTools.ToString(Keys.Control, Keys.L),
                "删除空行", CommandDeleteEmptyRow));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandDeleteColumn, CommandText.CommandDeleteColumn,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.S), KeysTools.GetCombinKeys(Keys.Control, Keys.F), KeysTools.ToString(Keys.Control, Keys.S), KeysTools.ToString(Keys.Control, Keys.F),
                "删除列", CommandDeleteColumn));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandDeleteCellMoveUp, CommandText.CommandDeleteCellMoveUp,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.S), KeysTools.GetCombinKeys(Keys.Control, Keys.C), KeysTools.ToString(Keys.Control, Keys.S), KeysTools.ToString(Keys.Control, Keys.C),
                "删除单元格上移", CommandDeleteCellMoveUp));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandDeleteCellMoveLeft, CommandText.CommandDeleteCellMoveLeft,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.S), KeysTools.GetCombinKeys(Keys.Control, Keys.V), KeysTools.ToString(Keys.Control, Keys.S), KeysTools.ToString(Keys.Control, Keys.V),
                "删除单元格左移", CommandDeleteCellMoveLeft));


            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandClearCell, CommandText.CommandClearCell,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.Delete), Keys.None, KeysTools.ToString(Keys.Shift, Keys.Delete), string.Empty,
                "清除单元格", CommandClearCell));


            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandHideColumn, CommandText.CommandHideColumn,
    KeysTools.GetCombinKeys(Keys.Shift, Keys.Y), 
    KeysTools.GetCombinKeys(Keys.Shift, Keys.C), 
    KeysTools.ToString(Keys.Shift, Keys.Y), 
    KeysTools.ToString(Keys.Shift, Keys.C),
    "隐藏列", CommandHideColumn));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandShowColumn, CommandText.CommandShowColumn,
KeysTools.GetCombinKeys(Keys.Shift, Keys.Y),
KeysTools.GetCombinKeys(Keys.Shift, Keys.P),
KeysTools.ToString(Keys.Shift, Keys.Y),
KeysTools.ToString(Keys.Shift, Keys.P),
"显示列", CommandShowColumn));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandHideRow, CommandText.CommandHideRow,
KeysTools.GetCombinKeys(Keys.Shift, Keys.Y),
KeysTools.GetCombinKeys(Keys.Shift, Keys.R),
KeysTools.ToString(Keys.Shift, Keys.Y),
KeysTools.ToString(Keys.Shift, Keys.R),
"隐藏行", CommandHideRow));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandShowRow, CommandText.CommandShowRow,
KeysTools.GetCombinKeys(Keys.Shift, Keys.Y),
KeysTools.GetCombinKeys(Keys.Shift, Keys.O),
KeysTools.ToString(Keys.Shift, Keys.Y),
KeysTools.ToString(Keys.Shift, Keys.O),
"显示行", CommandShowRow));



            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandPasteFormat, CommandText.CommandPasteFormat,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.V), Keys.None, KeysTools.ToString(Keys.Shift, Keys.V), KeysTools.ToString(Keys.None),
                "粘贴样式", CommandPasteFormat));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandCopyFormat, CommandText.CommandCopyFormat,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.C), KeysTools.GetCombinKeys(Keys.Shift, Keys.A), KeysTools.ToString(Keys.Shift, Keys.C), KeysTools.ToString(Keys.Shift, Keys.A),
                "复制样式(格式刷)", CommandCopyFormat, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandPasteFormatBorder, CommandText.CommandPasteFormatBorder,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.C), KeysTools.GetCombinKeys(Keys.Shift, Keys.B), KeysTools.ToString(Keys.Shift, Keys.C), KeysTools.ToString(Keys.Shift, Keys.B),
                "粘贴样式边框", CommandPasteFormatBorder));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandPasteText, CommandText.CommandPasteText,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.C), KeysTools.GetCombinKeys(Keys.Shift, Keys.T), KeysTools.ToString(Keys.Shift, Keys.C), KeysTools.ToString(Keys.Shift, Keys.T),
                "粘贴文本", CommandPasteText));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandPasteFormatColor, CommandText.CommandPasteFormatColor,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.C), KeysTools.GetCombinKeys(Keys.Shift, Keys.C), KeysTools.ToString(Keys.Shift, Keys.C), KeysTools.ToString(Keys.Shift, Keys.C),
                "粘贴颜色", CommandPasteFormatColor));


            CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandCopyID, CommandText.CommandCopyID,
                KeysTools.GetCombinKeys(Keys.Control, Keys.D), KeysTools.GetCombinKeys(Keys.Control, Keys.C), KeysTools.ToString(Keys.Control, Keys.D), KeysTools.ToString(Keys.Control, Keys.C),
                "复制ID", CommandCopyID, false));

            //CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandSetValue, CommandText.CommandSetValue,
            //    Keys.Escape, Keys.None, Keys.Escape.ToString(), Keys.None.ToString(),
            //    "清除文本", CommandSetValue));

            //CompositeKeys2.Add(new CommandObject(string.Empty, "Edit", "编辑", CommandText.CommandSetExpress, CommandText.CommandSetExpress,
            //    Keys.Escape, Keys.None, Keys.Escape.ToString(), Keys.None.ToString(),
            //    "清除公式", CommandSetExpress));
        }
        public virtual void InitDefaultMulitKeyActionSelect()
        {
            CompositeKeys2.Add(new CommandObject("Edit", "Select", "选择", CommandText.CommandMoveFocusedCellToUp, CommandText.CommandMoveFocusedCellToUp,
                Keys.Up, Keys.None, Keys.Up.ToString(), Keys.None.ToString(),
                "将当前焦点移动到上一个单元格", CommandMoveFocusedCellToUp, false));

            CompositeKeys2.Add(new CommandObject("Edit", "Select", "选择", CommandText.CommandMoveFocusedCellToDown, CommandText.CommandMoveFocusedCellToDown,
                Keys.Down, Keys.None, Keys.Down.ToString(), Keys.None.ToString(),
                "将当前焦点移动到下一个单元格", CommandMoveFocusedCellToDown, false));

            CompositeKeys2.Add(new CommandObject("Edit", "Select", "选择", CommandText.CommandMoveFocusedCellToLeft, CommandText.CommandMoveFocusedCellToLeft,
                Keys.Left, Keys.None, Keys.Left.ToString(), Keys.None.ToString(),
                "将当前焦点移动到左边单元格", CommandMoveFocusedCellToLeft, false));

            CompositeKeys2.Add(new CommandObject("Edit", "Select", "选择", CommandText.CommandMoveFocusedCellToRight, CommandText.CommandMoveFocusedCellToRight,
                Keys.Right, Keys.None, Keys.Right.ToString(), Keys.None.ToString(),
                "将当前焦点移动到右边单元格", CommandMoveFocusedCellToRight, false));

            CompositeKeys2.Add(new CommandObject("Edit", "Select", "选择", CommandText.CommandMoveFocusedCellToTab, CommandText.CommandMoveFocusedCellToTab,
                Keys.Tab, Keys.None, Keys.Tab.ToString(), Keys.None.ToString(),
                "将当前焦点移动到下个Tab", CommandMoveFocusedCellToTab, false));



            CompositeKeys2.Add(new CommandObject("Edit", "Select", "选择", CommandText.CommandSelectAll, CommandText.CommandSelectAll,
                KeysTools.GetCombinKeys(Keys.Control, Keys.A), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.A).ToString(), Keys.None.ToString(),
                "选择所有", CommandSelectAll, false));



            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandFirstCell, CommandText.CommandFirstCell,
                KeysTools.GetCombinKeys(Keys.Control, Keys.D1), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.D1).ToString(), Keys.None.ToString(),
                "移动到第一个单元格", CommandFirstCell, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandFocusedCellPrev, CommandText.CommandFocusedCellPrev,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Q), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Q).ToString(), Keys.None.ToString(),
                "上一个选中单元格", CommandFocusedCellPrev, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandFocusedCellNext, CommandText.CommandFocusedCellNext,
                KeysTools.GetCombinKeys(Keys.Control, Keys.W), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.W).ToString(), Keys.None.ToString(),
                "下一个选中单元格", CommandFocusedCellNext, false));



            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandBookmarkFirst, CommandText.CommandBookmarkFirst,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Q), KeysTools.GetCombinKeys(Keys.Control, Keys.A), KeysTools.GetCombinKeys(Keys.Control, Keys.Q).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.A).ToString(),
                "跳转到第一个书签", CommandBookmarkFirst, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandBookmarkEnd, CommandText.CommandBookmarkEnd,
                KeysTools.GetCombinKeys(Keys.Control, Keys.W), KeysTools.GetCombinKeys(Keys.Control, Keys.A), KeysTools.GetCombinKeys(Keys.Control, Keys.W).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.A).ToString(),
                "跳转到最后一个书签", CommandBookmarkEnd, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandBookmarkPrev, CommandText.CommandBookmarkPrev,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Subtract), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Subtract).ToString(), Keys.None.ToString(),
                "跳转到上一个书签", CommandBookmarkPrev, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandBookmarkNext, CommandText.CommandBookmarkNext,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Add), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Add).ToString(), Keys.None.ToString(),
                "跳转到下一个书签", CommandBookmarkNext, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandSelectUp, CommandText.CommandSelectUp,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Up), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Up).ToString(), Keys.None.ToString(),
                "向上选择", CommandSelectUp, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandSelectDown, CommandText.CommandSelectDown,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Down), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Down).ToString(), Keys.None.ToString(),
                "向下选择", CommandSelectDown, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandSelectLeft, CommandText.CommandSelectLeft,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Left), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Left).ToString(), Keys.None.ToString(),
                "向左选择", CommandSelectLeft, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandSelectRight, CommandText.CommandSelectRight,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Right), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Right).ToString(), Keys.None.ToString(),
                "向右选择", CommandSelectRight, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandSelectUpMove, CommandText.CommandSelectUpMove,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.Up), Keys.None, KeysTools.GetCombinKeys(Keys.Shift, Keys.Up).ToString(), Keys.None.ToString(),
                "选中向上移动", CommandSelectUpMove, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandSelectDownMove, CommandText.CommandSelectDownMove,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.Down), Keys.None, KeysTools.GetCombinKeys(Keys.Shift, Keys.Down).ToString(), Keys.None.ToString(),
                "选中向下移动", CommandSelectDownMove, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandSelectLeftMove, CommandText.CommandSelectLeftMove,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.Left), Keys.None, KeysTools.GetCombinKeys(Keys.Shift, Keys.Left).ToString(), Keys.None.ToString(),
                "选中向左移动", CommandSelectLeftMove, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandSelectRightMove, CommandText.CommandSelectRightMove,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.Right), Keys.None, KeysTools.GetCombinKeys(Keys.Shift, Keys.Right).ToString(), Keys.None.ToString(),
                "选中向右移动", CommandSelectRightMove, false));


            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandSelectUpText, CommandText.CommandSelectUpText,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.Up), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.Up).ToString(), Keys.None.ToString(),
                "向上移动文本", CommandSelectUpText, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandSelectDownText, CommandText.CommandSelectDownText,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.Down), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.Down).ToString(), Keys.None.ToString(),
                "向下移动文本", CommandSelectDownText, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandSelectLeftText, CommandText.CommandSelectLeftText,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.Left), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.Left).ToString(), Keys.None.ToString(),
                "向左移动文本", CommandSelectLeftText, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandSelectRightText, CommandText.CommandSelectRightText,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.Right), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.Right).ToString(), Keys.None.ToString(),
                "向右移动文本", CommandSelectRightText, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandBookmarkNext, CommandText.CommandBookmarkNext,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.F, KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), Keys.F.ToString(),
                "跳转到下个书签", CommandBookmarkNext, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandBookmarkNext, CommandText.CommandBookmarkNext,
                Keys.F3, Keys.None, Keys.F3.ToString(), Keys.None.ToString(),
                "跳转到下个书签", CommandBookmarkNext, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandBookmarkPrev, CommandText.CommandBookmarkPrev,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.S, KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), Keys.S.ToString(),
                "跳转到上个书签", CommandBookmarkPrev, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandBookmarkAdd, CommandText.CommandBookmarkAdd,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.A, KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), Keys.A.ToString(),
                "添加书签", CommandBookmarkAdd, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandBookmarkDelete, CommandText.CommandBookmarkDelete,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.C, KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), Keys.C.ToString(),
                "删除当前书签", CommandBookmarkDelete, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandBookmarkHeader, CommandText.CommandBookmarkHeader,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.E, KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), Keys.E.ToString(),
                "跳转到首个书签", CommandBookmarkHeader, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Select", "选择", CommandText.CommandBookmarkFooter, CommandText.CommandBookmarkFooter,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.D, KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), Keys.D.ToString(),
                "跳转到最后一个书签", CommandBookmarkFooter, false));
        }
        public virtual void InitDefaultMulitKeyActionCalc()
        {


            CompositeKeys2.Add(new CommandObject(string.Empty, "Tool", "工具", CommandText.CommandSum, CommandText.CommandSum,
                KeysTools.GetCombinKeys(Keys.Control, Keys.J), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.J).ToString(), Keys.None.ToString(),
                "合计", CommandSum));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Tool", "工具", CommandText.CommandSumSelectCells, CommandText.CommandSumSelectCells,
                KeysTools.GetCombinKeys(Keys.Control, Keys.K), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.K).ToString(), Keys.None.ToString(),
                "合计选中", CommandSumSelectCells));


            CompositeKeys2.Add(new CommandObject(string.Empty, "Tool", "工具", CommandText.CommandClock, CommandText.CommandClock,
                KeysTools.GetCombinKeys(Keys.Control, Keys.K), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.K).ToString(), Keys.None.ToString(),
                "闹钟", CommandClock));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Tool", "工具", CommandText.CommandCellTable, CommandText.CommandCellTable,
                KeysTools.GetCombinKeys(Keys.Control, Keys.K), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.K).ToString(), Keys.None.ToString(),
                "设置为表格", CommandCellTable));

        }
        public virtual void InitDefaultMulitKeyActionGrid()
        {

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandColumnAutoSize, CommandText.CommandColumnAutoSize,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.E).ToString(), Keys.None.ToString(),
                "重置列宽", CommandColumnAutoSize));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandMergeCell, CommandText.CommandMergeCell,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.Q), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.Q).ToString(), Keys.None.ToString(),
                "合并/取消合并单元格", CommandMergeCell));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandMergeClear, CommandText.CommandMergeClear,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.T), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.T).ToString(), Keys.None.ToString(),
                "清除合并单元格", CommandMergeClear));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandBorderFull, CommandText.CommandBorderFull,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.Z, KeysTools.GetCombinKeys(Keys.Control, Keys.A).ToString(), Keys.Z.ToString(),
                "边框 全部", CommandBorderFull));



            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandCellHide, CommandText.CommandCellHide,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.NumPad1, KeysTools.GetCombinKeys(Keys.Control, Keys.H).ToString(), Keys.NumPad1.ToString(),
                "隐藏内容", CommandCellHide));



            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandCellVisible, CommandText.CommandCellVisible,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.NumPad1, KeysTools.GetCombinKeys(Keys.Control, Keys.S).ToString(), Keys.NumPad1.ToString(),
                "显示内容", CommandCellVisible));


            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandCellBackImage, CommandText.CommandCellBackImage,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.NumPad1, KeysTools.GetCombinKeys(Keys.Control, Keys.I).ToString(), Keys.NumPad1.ToString(),
                "背景图片", CommandCellBackImage));


            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandCellBackImageClear, CommandText.CommandCellBackImageClear,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.NumPad1, KeysTools.GetCombinKeys(Keys.Control, Keys.K).ToString(), Keys.NumPad1.ToString(),
                "背景图片取消", CommandCellBackImageClear));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandCellBackGround1, CommandText.CommandCellBackGround1,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.NumPad1, KeysTools.GetCombinKeys(Keys.Control, Keys.D1).ToString(), Keys.NumPad1.ToString(),
                "背景颜色1", CommandCellBackGround1));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandCellBackGround2, CommandText.CommandCellBackGround2,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.NumPad2, KeysTools.GetCombinKeys(Keys.Control, Keys.D2).ToString(), Keys.NumPad2.ToString(),
                "背景颜色2", CommandCellBackGround2));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandCellBackGround3, CommandText.CommandCellBackGround3,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.NumPad3, KeysTools.GetCombinKeys(Keys.Control, Keys.D3).ToString(), Keys.NumPad3.ToString(),
                "背景颜色3", CommandCellBackGround3));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandCellBackGround4, CommandText.CommandCellBackGround4,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.NumPad4, KeysTools.GetCombinKeys(Keys.Control, Keys.D4).ToString(), Keys.NumPad4.ToString(),
                "背景颜色4", CommandCellBackGround4));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandCellBackGround5, CommandText.CommandCellBackGround5,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.NumPad5, KeysTools.GetCombinKeys(Keys.Control, Keys.D5).ToString(), Keys.NumPad5.ToString(),
                "背景颜色5", CommandCellBackGround5));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandCellBackGround6, CommandText.CommandCellBackGround6,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.NumPad6, KeysTools.GetCombinKeys(Keys.Control, Keys.D6).ToString(), Keys.NumPad6.ToString(),
                "背景颜色6", CommandCellBackGround6));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandCellBackGround7, CommandText.CommandCellBackGround7,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.NumPad7, KeysTools.GetCombinKeys(Keys.Control, Keys.D7).ToString(), Keys.NumPad7.ToString(),
                "背景颜色7", CommandCellBackGround7));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandCellBackGroundLight, CommandText.CommandCellBackGroundLight,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.OemMinus, KeysTools.GetCombinKeys(Keys.Control, Keys.E).ToString(), Keys.OemMinus.ToString(),
                "背景颜色变亮", CommandCellBackGroundLight));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandCellBackGroundDark, CommandText.CommandCellBackGroundDark,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), Keys.Oemplus, KeysTools.GetCombinKeys(Keys.Control, Keys.D).ToString(), Keys.Oemplus.ToString(),
                "背景颜色变深", CommandCellBackGroundDark));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandBorderFull, CommandText.CommandBorderFull,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), KeysTools.GetCombinKeys(Keys.Control, Keys.A), KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.A).ToString(),
                "边框 全部", CommandBorderFull));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandBorderBorderOutside, CommandText.CommandBorderBorderOutside,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), KeysTools.GetCombinKeys(Keys.Control, Keys.O), KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.O).ToString(),
                "边框 外边框", CommandBorderBorderOutside));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandBorderClear, CommandText.CommandBorderClear,
             KeysTools.GetCombinKeys(Keys.Control, Keys.B), KeysTools.GetCombinKeys(Keys.Control, Keys.C), KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.C).ToString(),
             "边框 清除", CommandBorderClear));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandBorderTop, CommandText.CommandBorderTop,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.E).ToString(),
                "边框 上边框", CommandBorderTop));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandBorderBottom, CommandText.CommandBorderBottom,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), KeysTools.GetCombinKeys(Keys.Control, Keys.D), KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.D).ToString(),
                "边框 下边框", CommandBorderBottom));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandBorderLeft, CommandText.CommandBorderLeft,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), KeysTools.GetCombinKeys(Keys.Control, Keys.W), KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.W).ToString(),
                "边框 左边框", CommandBorderLeft));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandBorderRight, CommandText.CommandBorderRight,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), KeysTools.GetCombinKeys(Keys.Control, Keys.R), KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.R).ToString(),
                "边框 右边框", CommandBorderRight));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandBorderLeftTopToRightBottom, CommandText.CommandBorderLeftTopToRightBottom,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), KeysTools.GetCombinKeys(Keys.Control, Keys.T), KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.T).ToString(),
                "边框 左上至右下边框", CommandBorderLeftTopToRightBottom));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandBorderLeftBoomToRightTop, CommandText.CommandBorderLeftBoomToRightTop,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), KeysTools.GetCombinKeys(Keys.Control, Keys.G), KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.G).ToString(),
                "边框 左下至右上边框", CommandBorderLeftBoomToRightTop));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandBorderBorderOutsideClear, CommandText.CommandBorderBorderOutsideClear,
    KeysTools.GetCombinKeys(Keys.Control, Keys.B), KeysTools.GetCombinKeys(Keys.Control, Keys.O), KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.O).ToString(),
    "边框 外边框清除", CommandBorderOutsideClear));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandBorderTopClear, CommandText.CommandBorderTopClear,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), KeysTools.GetCombinKeys(Keys.Control, Keys.I), KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.I).ToString(),
                "边框 取消上边框", CommandBorderTopClear));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandBorderBottomClear, CommandText.CommandBorderBottomClear,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), KeysTools.GetCombinKeys(Keys.Control, Keys.K), KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.K).ToString(),
                "边框 取消下边框", CommandBorderBottomClear));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandBorderLeftClear, CommandText.CommandBorderLeftClear,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), KeysTools.GetCombinKeys(Keys.Control, Keys.J), KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.J).ToString(),
                "边框 取消左边框", CommandBorderLeftClear));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandBorderRightClear, CommandText.CommandBorderRightClear,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), KeysTools.GetCombinKeys(Keys.Control, Keys.L), KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.L).ToString(),
                "边框 取消右边框", CommandBorderRightClear));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandBorderLeftTopToRightBottomClear, CommandText.CommandBorderLeftTopToRightBottomClear,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), KeysTools.GetCombinKeys(Keys.Control, Keys.N), KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.N).ToString(),
                "边框 取消左上至右下边框", CommandBorderLeftTopToRightBottomClear));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandBorderLeftBottomToRightTopClear, CommandText.CommandBorderLeftBottomToRightTopClear,
                KeysTools.GetCombinKeys(Keys.Control, Keys.B), KeysTools.GetCombinKeys(Keys.Control, Keys.M), KeysTools.GetCombinKeys(Keys.Control, Keys.B).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.M).ToString(),
                "边框 取消左下至右上边框", CommandBorderLeftBottomToRightTopClear));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandCellReadOnly, CommandText.CommandCellReadOnly,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.C), KeysTools.GetCombinKeys(Keys.Shift, Keys.R), KeysTools.ToString(Keys.Shift, Keys.C), KeysTools.ToString(Keys.Shift, Keys.R),
                "表格只读", CommandGridReadOnly));


            CompositeKeys2.Add(new CommandObject(string.Empty, "Grid", "表格", CommandText.CommandGridReadOnly, CommandText.CommandGridReadOnly,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.G), KeysTools.GetCombinKeys(Keys.Shift, Keys.R), KeysTools.ToString(Keys.Control, Keys.G), KeysTools.ToString(Keys.Control, Keys.R),
                "单元格只读", CommandCellReadOnly ));

        }
        public virtual void InitDefaultMulitKeyActionView()
        {

            CompositeKeys2.Add(new CommandObject(string.Empty, "View", "视图", CommandText.CommandShowShortcut, CommandText.CommandShowShortcut,
                KeysTools.GetCombinKeys(Keys.Control, Keys.F1), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.F1).ToString(), Keys.None.ToString(),
                "显示快捷键", CommandShowShortcut, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "View", "视图", CommandText.CommandShowHistory, CommandText.CommandShowHistory,
                KeysTools.GetCombinKeys(Keys.Control, Keys.F2), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.F2).ToString(), Keys.None.ToString(),
                "显示命令历史记录", CommandShowHistory, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "View", "视图", CommandText.CommandShowRemember, CommandText.CommandShowRemember,
                KeysTools.GetCombinKeys(Keys.Control, Keys.F3), Keys.None, KeysTools.GetCombinKeys(Keys.Control, Keys.F3).ToString(), Keys.None.ToString(),
                "显示记忆命令", CommandShowRemember, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "View", "视图", CommandText.CommandShowCellInfo, CommandText.CommandShowCellInfo,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.M), KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.M), KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.M).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.M).ToString(),
                "显示单元格信息", CommandShowCellInfo, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "View", "视图", CommandText.CommandHideCellInfo, CommandText.CommandHideCellInfo,
                KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.M), KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.N), KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.M).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.Shift, Keys.N).ToString(),
                "隐藏单元格信息", CommandHideCellInfo, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "View", "视图", CommandText.CommandGridShowColumnHeader, CommandText.CommandGridShowColumnHeader,
          KeysTools.GetCombinKeys(Keys.Shift, Keys.G), KeysTools.GetCombinKeys(Keys.Shift, Keys.Y), KeysTools.ToString(Keys.Control, Keys.G), KeysTools.ToString(Keys.Control, Keys.Y),
          "显示列头", CommandGridShowColumnHeader, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "View", "视图", CommandText.CommandGridShowRowHeader, CommandText.CommandGridShowRowHeader,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.G), KeysTools.GetCombinKeys(Keys.Shift, Keys.N), KeysTools.ToString(Keys.Control, Keys.G), KeysTools.ToString(Keys.Control, Keys.N),
                "显示行头", CommandGridShowRowHeader, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "View", "视图", CommandText.CommandGridShowHeader, CommandText.CommandGridShowHeader,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.G), KeysTools.GetCombinKeys(Keys.Shift, Keys.H), KeysTools.ToString(Keys.Control, Keys.G), KeysTools.ToString(Keys.Control, Keys.H),
                "显示行列头", CommandGridShowHeader, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "View", "视图", CommandText.CommandShowGridLine, CommandText.CommandShowGridLine,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.G), KeysTools.GetCombinKeys(Keys.Shift, Keys.P), KeysTools.ToString(Keys.Control, Keys.G), KeysTools.ToString(Keys.Control, Keys.O),
                "显示网格线", CommandShowGridLine, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "View", "视图", CommandText.CommandGridShowGridColumnLine, CommandText.CommandGridShowGridColumnLine,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.G), KeysTools.GetCombinKeys(Keys.Shift, Keys.O), KeysTools.ToString(Keys.Control, Keys.G), KeysTools.ToString(Keys.Control, Keys.O),
                "显示列表格线", CommandGridShowGridColumnLine, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "View", "视图", CommandText.CommandGridShowGridRowLine, CommandText.CommandGridShowGridRowLine,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.G), KeysTools.GetCombinKeys(Keys.Shift, Keys.L), KeysTools.ToString(Keys.Control, Keys.G), KeysTools.ToString(Keys.Control, Keys.L),
                "显示行表格线", CommandGridShowGridRowLine, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "View", "视图", CommandText.CommandShowGridScroller, CommandText.CommandShowGridScroller,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.G), KeysTools.GetCombinKeys(Keys.Shift, Keys.S), KeysTools.ToString(Keys.Control, Keys.G), KeysTools.ToString(Keys.Control, Keys.S),
                "显示滚动条", ShowGridScroller, false));
            CompositeKeys2.Add(new CommandObject(string.Empty, "View", "视图", CommandText.CommandShowRuler, CommandText.CommandShowRuler,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.G), KeysTools.GetCombinKeys(Keys.Shift, Keys.K), KeysTools.ToString(Keys.Control, Keys.G), KeysTools.ToString(Keys.Control, Keys.K),
                "显示标尺", CommandShowRuler, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "View", "视图", CommandText.CommandFrozen, CommandText.CommandFrozen,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.G), KeysTools.GetCombinKeys(Keys.Shift, Keys.H), KeysTools.ToString(Keys.Control, Keys.G), KeysTools.ToString(Keys.Control, Keys.H),
                "冻结到单元格", CommandFrozen, false));


            CompositeKeys2.Add(new CommandObject(string.Empty, "View", "视图", CommandText.CommandFrozenColumn, CommandText.CommandFrozenColumn,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.G), KeysTools.GetCombinKeys(Keys.Shift, Keys.J), 
                KeysTools.ToString(Keys.Control, Keys.G), KeysTools.ToString(Keys.Control, Keys.J),
                "冻结到列", CommandFrozenColumn, false));

            CompositeKeys2.Add(new CommandObject(string.Empty, "View", "视图", CommandText.CommandFrozenRow, CommandText.CommandFrozenRow,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.G), KeysTools.GetCombinKeys(Keys.Shift, Keys.R),
                KeysTools.ToString(Keys.Control, Keys.G), KeysTools.ToString(Keys.Control, Keys.R),
                "冻结到行", CommandFrozenRow, false));
        }
        public virtual void InitDefaultMulitKeyActionEditControl()
        {


            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellEditNull, CommandText.CommandCellEditNull,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.Q), KeysTools.GetCombinKeys(Keys.Control, Keys.E).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.Q).ToString(),
                "文本编辑控件(默认)", CommandCellEditNull));

            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellEditNumber, CommandText.CommandCellEditNumber,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.N), KeysTools.GetCombinKeys(Keys.Control, Keys.E).ToString(), KeysTools.GetCombinKeys(Keys.Control, Keys.N).ToString(),
                "数字控件", CommandCellEditNumber));
            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellEditLabel, CommandText.CommandCellEditLabel,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.L), KeysTools.ToString(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.L),
                "标签编辑控件", CommandCellEditLabel));
            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellEditCheckBox, CommandText.CommandCellEditCheckBox,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.C), KeysTools.ToString(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.C),
                "复选框编辑控件", CommandCellEditCheckBox));
            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellEditCnNumber, CommandText.CommandCellEditCnNumber,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.Z), KeysTools.ToString(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.Z),
                "中文大写数字金额", CommandCellEditCnNumber));
            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellEditComboBox, CommandText.CommandCellEditComboBox,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.X), KeysTools.ToString(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.X),
                "下拉编辑控件", CommandCellEditComboBox));

    //        CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellEditDateTime, CommandText.CommandCellEditDateTime,
    //            KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.T), KeysTools.ToString(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.T),
    //            "日期编辑控件", CommandCellEditDateTime));

    //        CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellDropDownDate, CommandText.CommandCellDropDownDate,
    //KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.T), KeysTools.ToString(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.T),
    //"下拉日期编辑控件", CommandCellDropDownDate));

            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellDropDownDateTime, CommandText.CommandCellDropDownDateTime,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.Y), KeysTools.ToString(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.Y),
                "日期时间编辑控件", CommandCellDropDownDateTime));

            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellEditGridView, CommandText.CommandCellEditGridView,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.G), KeysTools.ToString(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.G),
                "内嵌表格编辑控件", CommandCellEditGridView));
            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellEditImage, CommandText.CommandCellEditImage,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.I), KeysTools.ToString(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.I),
                "图像编辑控件", CommandCellEditImage));

            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellEditCellTimer, CommandText.CommandCellEditCellTimer,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.I), KeysTools.ToString(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.I),
                "图像编辑控件", CommandCellEditCellTimer));

            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellEditLinkLabel, CommandText.CommandCellEditLinkLabel,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.K), KeysTools.ToString(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.K),
                "链接编辑控件", CommandCellEditLinkLabel));
            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellEditPassword, CommandText.CommandCellEditPassword,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.P), KeysTools.ToString(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.P),
                "密码框编辑控件", CommandCellEditPassword));
            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellEditRadioBox, CommandText.CommandCellEditRadioBox,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.R), KeysTools.ToString(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.R),
                "单选框编辑控件", CommandCellEditRadioBox));
            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellEditTreeView, CommandText.CommandCellEditTreeView,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.V), KeysTools.ToString(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.V),
                "树编辑控件", CommandCellEditTreeView));
            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellExcel, CommandText.CommandCellExcel,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.M), KeysTools.ToString(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.M),
                "内嵌DATAEXCEL控件", CommandCellExcel));
            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellEditColor, CommandText.CommandCellEditColor,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.A), KeysTools.ToString(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.A),
                "颜色编辑控件", CommandCellEditColor));


            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellProcess, CommandText.CommandCellProcess,
                KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.GetCombinKeys(Keys.Control, Keys.P), KeysTools.ToString(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.P),
                "进度控件", CommandCellProcess));

            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellDropDownDataExcel, CommandText.CommandCellDropDownDataExcel,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "下拉表格控件", CommandCellDropDownDataExcel));

            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellDropDownList, CommandText.CommandCellDropDownList,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "选择文件", CommandCellFileSelectEdit));
            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellFolderBrowserEdit, CommandText.CommandCellFolderBrowserEdit,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "文件夹控件", CommandCellFolderBrowserEdit));

            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellMoveForm, CommandText.CommandCellMoveForm,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "移动窗口控件", CommandCellMoveForm));

            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandEditCellSplitRow, CommandText.CommandEditCellSplitRow,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "移动行高", CommandEditCellSplitRow));

            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandEditCellSplitColumn, CommandText.CommandEditCellSplitColumn,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "移动列宽", CommandEditCellSplitColumn));

            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellImageButton, CommandText.CommandCellImageButton,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "按钮控件", CommandCellButton));

            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellSpText, CommandText.CommandCellSpText,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "行列分隔控件", CommandCellSpText));

            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellSwitch, CommandText.CommandCellSwitch,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "开关按钮控件", CommandCellSwitch));

            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellTextBoxEdit, CommandText.CommandCellTextBoxEdit,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "文本框控件", CommandCellTextBoxEdit));

            //CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellTime, CommandText.CommandCellTime,
            //    Keys.None, Keys.None, string.Empty, string.Empty,
            //    "时间控件", CommandCellTime));

            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellToolBar, CommandText.CommandCellToolBar,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "工具条控件", CommandCellToolBar));


            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellTreeView, CommandText.CommandCellTreeView,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "树控件", CommandCellTreeView));

            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellGridView, CommandText.CommandCellGridView,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "表格控件", CommandCellGridView));

            CompositeKeys2.Add(new CommandObject(string.Empty, "EditControl", "编辑控件", CommandText.CommandCellVector, CommandText.CommandCellVector,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "矢量图元控件", CommandCellVector));
        }
        public virtual void InitDefaultMulitKeyActionStyle()
        {
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandCellBackGround, CommandText.CommandCellBackGround,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.C), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.C),
                "背景色", CommandCellBackGround));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandCellForeColor, CommandText.CommandCellForeColor,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.C), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.C),
                "前景色", CommandCellForeColor));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandFont, CommandText.CommandFont,
    KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.C), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.C),
    "字体", CommandFont));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandFontCancel, CommandText.CommandFontCancel,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.C), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.C),
                "重置默认字体", CommandFontCancel));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandFontBold, CommandText.CommandFontBold,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.Q), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.Q),
                "加粗字体", CommandFontBold));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandFontBoldCancel, CommandText.CommandFontBoldCancel,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.A), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.A),
                "取消加粗字体", CommandFontBoldCancel));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandFontItalic, CommandText.CommandFontItalic,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.W), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.W),
                "斜体字体", CommandFontItalic));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandFontItalicCancel, CommandText.CommandFontItalicCancel,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.S), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.S),
                "取消斜体字体", CommandFontItalicCancel));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandFontUnderline, CommandText.CommandFontUnderline,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.R), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.R),
                "下划线字体", CommandFontUnderline));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandFontUnderlineCancel, CommandText.CommandFontUnderlineCancel,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.F),
                "取消下划线字体", CommandFontUnderlineCancel));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandFontStrikeout, CommandText.CommandFontStrikeout,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.T), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.T),
                "删除线", CommandFontStrikeout));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandFontStrikeoutCancel, CommandText.CommandFontStrikeoutCancel,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.G), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.G),
                "取消删除线", CommandFontStrikeoutCancel));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandFontSizeUp, CommandText.CommandFontSizeUp,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.E), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.E),
                "字体大小加大", CommandFontSizeUp));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandFontSizeDown, CommandText.CommandFontSizeDown,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.D), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.D),
                "字体大小减小", CommandFontSizeDown));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextAutoMultiline, CommandText.CommandTextAutoMultiline,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.B), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.B),
                "自动换行", CommandTextAutoMultiline));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextAutoMultilineCancel, CommandText.CommandTextAutoMultilineCancel,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.N), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.N),
                "取消自动换行", CommandTextAutoMultilineCancel));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextToLower, CommandText.CommandTextToLower,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.Shift, Keys.U), Keys.None, KeysTools.ToString(Keys.Shift, Keys.Shift, Keys.U), KeysTools.ToString(Keys.None),
                "转换为小写字母", CommandTextToLower));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextToUpper, CommandText.CommandTextToUpper,
             KeysTools.GetCombinKeys(Keys.Shift, Keys.Shift, Keys.L), Keys.None, KeysTools.ToString(Keys.Shift, Keys.Shift, Keys.L), KeysTools.ToString(Keys.None),
                "转换为大写字母", CommandTextToUpper));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextTrimSpace, CommandText.CommandTextTrimSpace,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.Y), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.Y),
                "去除文本头尾空格", CommandTextTrimSpace));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextTrimStartSpace, CommandText.CommandTextTrimStartSpace,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.U), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.U),
                "去除文本头部空格", CommandTextTrimStartSpace));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextTrimEndSpace, CommandText.CommandTextTrimEndSpace,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.I), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.I),
                "去除文本尾部空格", CommandTextTrimEndSpace));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextTrimSymbol, CommandText.CommandTextTrimSymbol,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.H), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.H),
                "去除文本头尾符号", CommandTextTrimSymbol));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextTrimStartSymbol, CommandText.CommandTextTrimStartSymbol,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.K), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.K),
                "去除文本头部符号", CommandTextTrimStartSymbol));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextTrimEndSymbol, CommandText.CommandTextTrimEndSymbol,
                KeysTools.GetCombinKeys(Keys.Shift, Keys.F), KeysTools.GetCombinKeys(Keys.Shift, Keys.J), KeysTools.ToString(Keys.Shift, Keys.F), KeysTools.ToString(Keys.Shift, Keys.J),
                "去除文本尾部符号", CommandTextTrimEndSymbol));


            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextAlignTop, CommandText.CommandTextAlignTop,
                KeysTools.GetCombinKeys(Keys.Control, Keys.D), KeysTools.GetCombinKeys(Keys.Control, Keys.E), KeysTools.ToString(Keys.Control, Keys.D), KeysTools.ToString(Keys.Control, Keys.E),
                "文本上对齐", CommandTextAlignTop));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextAlignBottom, CommandText.CommandTextAlignBottom,
                KeysTools.GetCombinKeys(Keys.Control, Keys.D), KeysTools.GetCombinKeys(Keys.Control, Keys.D), KeysTools.ToString(Keys.Control, Keys.D), KeysTools.ToString(Keys.Control, Keys.D),
                "文本下对齐", CommandTextAlignBottom));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextAlignLeft, CommandText.CommandTextAlignLeft,
                KeysTools.GetCombinKeys(Keys.Control, Keys.D), KeysTools.GetCombinKeys(Keys.Control, Keys.W), KeysTools.ToString(Keys.Control, Keys.D), KeysTools.ToString(Keys.Control, Keys.W),
                "文本左对齐", CommandTextAlignLeft));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextAlignRight, CommandText.CommandTextAlignRight,
                KeysTools.GetCombinKeys(Keys.Control, Keys.D), KeysTools.GetCombinKeys(Keys.Control, Keys.R), KeysTools.ToString(Keys.Control, Keys.D), KeysTools.ToString(Keys.Control, Keys.R),
                "文本右对齐", CommandTextAlignRight));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextAlignHorizontalCenter, CommandText.CommandTextAlignHorizontalCenter,
                KeysTools.GetCombinKeys(Keys.Control, Keys.D), KeysTools.GetCombinKeys(Keys.Control, Keys.H), KeysTools.ToString(Keys.Control, Keys.D), KeysTools.ToString(Keys.Control, Keys.H),
                "文本水平居中", CommandTextAlignHorizontalCenter));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextAlignVerticalCenter, CommandText.CommandTextAlignVerticalCenter,
                KeysTools.GetCombinKeys(Keys.Control, Keys.D), KeysTools.GetCombinKeys(Keys.Control, Keys.G), KeysTools.ToString(Keys.Control, Keys.D), KeysTools.ToString(Keys.Control, Keys.G),
                "文本垂直居中", CommandTextAlignVerticalCenter));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextAlignCenter, CommandText.CommandTextAlignCenter,
                KeysTools.GetCombinKeys(Keys.Control, Keys.D), KeysTools.GetCombinKeys(Keys.Control, Keys.Q), KeysTools.ToString(Keys.Control, Keys.D), KeysTools.ToString(Keys.Control, Keys.Q),
                "文本居中", CommandTextAlignCenter));
            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandTextOrientationRotateDown, CommandText.CommandTextOrientationRotateDown,
                KeysTools.GetCombinKeys(Keys.Control, Keys.D), KeysTools.GetCombinKeys(Keys.Control, Keys.L), KeysTools.ToString(Keys.Control, Keys.D), KeysTools.ToString(Keys.Control, Keys.L),
                "垂直文字", CommandTextOrientationRotateDown));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandMulCellBackImage, CommandText.CommandMulCellBackImage,
                KeysTools.GetCombinKeys(Keys.Control, Keys.D), KeysTools.GetCombinKeys(Keys.Control, Keys.I), KeysTools.ToString(Keys.Control, Keys.D), KeysTools.ToString(Keys.Control, Keys.I),
                "多单元格背景", CommandMulCellBackImage));

            CompositeKeys2.Add(new CommandObject(string.Empty, "Style", "样式", CommandText.CommandMulCellBackImageCancel, CommandText.CommandMulCellBackImageCancel,
                KeysTools.GetCombinKeys(Keys.Control, Keys.D), KeysTools.GetCombinKeys(Keys.Control, Keys.I), KeysTools.ToString(Keys.Control, Keys.D), KeysTools.ToString(Keys.Control, Keys.I),
                "取消多单元格背景", CommandMulCellBackImageCancel));
        }
        public virtual void InitDefaultMulitKeyActionTextFormat()
        {

            CompositeKeys2.Add(new CommandObject(string.Empty, "TextFormat", "文本格式", CommandText.CommandTextFormatText, CommandText.CommandTextFormatText,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "文本样式", CommandTextFormatText));
            CompositeKeys2.Add(new CommandObject(string.Empty, "TextFormat", "文本格式", CommandText.CommandTextFormatMoney, CommandText.CommandTextFormatMoney,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "金额", CommandTextFormatMoney));
            CompositeKeys2.Add(new CommandObject(string.Empty, "TextFormat", "文本格式", CommandText.CommandTextFormatPercent, CommandText.CommandTextFormatPercent,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "百分比", CommandTextFormatPercent));
            CompositeKeys2.Add(new CommandObject(string.Empty, "TextFormat", "文本格式", CommandText.CommandTextFormatThousandths, CommandText.CommandTextFormatThousandths,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "千分位", CommandTextFormatThousandths));
            CompositeKeys2.Add(new CommandObject(string.Empty, "TextFormat", "文本格式", CommandText.CommandTextFormatDecimalPlaces1, CommandText.CommandTextFormatDecimalPlaces1,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "1位小数", CommandTextFormatDecimalPlaces1));
            CompositeKeys2.Add(new CommandObject(string.Empty, "TextFormat", "文本格式", CommandText.CommandTextFormatDecimalPlaces2, CommandText.CommandTextFormatDecimalPlaces2,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "两位小数", CommandTextFormatDecimalPlaces2));
            CompositeKeys2.Add(new CommandObject(string.Empty, "TextFormat", "文本格式", CommandText.CommandTextFormatDateTimeDay, CommandText.CommandTextFormatDateTimeDay,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "日期年月日格式", CommandTextFormatDateTimeDay));
            CompositeKeys2.Add(new CommandObject(string.Empty, "TextFormat", "文本格式", CommandText.CommandTextFormatDateTimeTime, CommandText.CommandTextFormatDateTimeTime,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "日期时间格式", CommandTextFormatDateTimeTime));
        }
        public virtual void InitDefaultDesignHide()
        {
            CompositeKeys2.Add(new CommandObject("Edit", "Select", "选择", CommandText.CommandUpdataSelectAdd, CommandText.CommandUpdataSelectAdd,
                Keys.None, Keys.None, string.Empty, string.Empty,
                "下拉复制", CommandUpdataSelectAdd));

        }
        public virtual void InitDefaultMulitKeyAction()
        {
            if (CompositeKeys2.Commands.Count > 0)
                return;
            InitDefaultMulitKeyActionFile();
            InitDefaultMulitKeyActionEdit();
            InitDefaultMulitKeyActionSelect();
            InitDefaultMulitKeyActionCalc();
            InitDefaultMulitKeyActionGrid();
            InitDefaultMulitKeyActionView();
            InitDefaultMulitKeyActionEditControl();
            InitDefaultMulitKeyActionStyle();
            InitDefaultMulitKeyActionTextFormat();
            InitDefaultDesignHide();
            CompositeKeys2.CommandCheck();
#warning 背景色 加 变蓝，变黄，变红 前景色 变蓝 变黄 变红


#warning 补全控件


        }
    }
}