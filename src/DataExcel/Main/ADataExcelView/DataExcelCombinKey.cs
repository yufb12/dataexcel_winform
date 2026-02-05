using Feng.Excel.App;
using Feng.Excel.Args;
using Feng.Excel.Base;
using Feng.Excel.Collections;
using Feng.Excel.Commands;
using Feng.Excel.Extend;
using Feng.Excel.Interfaces;
using Feng.Excel.Table;
using Feng.Forms;
using Feng.Forms.Command;
using Feng.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Excel
{
    partial class DataExcel
    { 
        public virtual void CommandExcute(string commandtext)
        {
            CommandExcute(commandtext, string.Empty);
        }
        public virtual void CommandExcute(string commandtext, string arg)
        {
            Feng.Forms.Command.CommandObject command = CompositeKeys2.GetCommand(commandtext);

            if (command == null)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcelCombinKey", "CommandExcute",true, commandtext + "  " + arg);
                return;
            }
            command.CommandArgs = arg;
            CommandExcute(command);
 
        }
        public virtual void CommandExcute(Feng.Forms.Command.CommandObject command)
        { 
            CacheStack<BeforeCommandExcuteArgs> cacheStackbeforecommd = CacheStack<BeforeCommandExcuteArgs>.GetCacheStack("BeforeCommandExcuteArgs");
            BeforeCommandExcuteArgs args = cacheStackbeforecommd.Pop();
            if (args == null)
            {
                args = new BeforeCommandExcuteArgs();
            }
            args.CommandText = command.CommandText;
            args.Cancel = false;
            args.CommandText = command.CommandText;
            args.Arg = command.CommandArgs;
            OnBeforeCommandExcute(args);
            cacheStackbeforecommd.Push(args);
            if (args.Cancel)
            {
                return;
            }
            CommandBegin();
            if (this.CanUndoRedo)
            {
                if (command.UndoRendo)
                {
                    CommandChangedCommand cmd = new CommandChangedCommand();
                    cmd.Value = this.GetFileData();
                    cmd.Grid = this;
                    this.Commands.Add(cmd);
                }
            }
            this.CompositeKeys2.Exec(command, string.Empty);
            CommandEnd();
            CacheStack<CommandExcutedArgs> cacheCommandExcutedArgs = CacheStack<CommandExcutedArgs>.GetCacheStack("CommandExcutedArgs");
            CommandExcutedArgs e = cacheCommandExcutedArgs.Pop();
            if (e == null)
            {
                e = new CommandExcutedArgs();
            }
            e.Command = command;
            e.Arg = command.CommandArgs;
            e.CommandText = command.CommandText;
            OnCommandExcuted(e);
            cacheCommandExcutedArgs.Push(e);
        }

        public virtual void CommandBegin()
        {
            this.BeginReFresh();
        }
        public virtual void CommandEnd()
        {
            this.EndReFresh();
        }

        public virtual void ShowGridScroller(string arg)
        {
            this.ShowHorizontalScroller = !this.ShowVerticalScroller;
            this.ShowVerticalScroller = !this.ShowVerticalScroller;
        }
        public virtual void ShowGridReticle(string arg)
        {
            if (!ReticleDrawExtend.Contains(this))
            {
                ReticleDrawExtend.Reticled(this);
            }
            else
            {
                ReticleDrawExtend.UnReticled(this);
            }
        }

        public virtual void CommandShowRuler(string arg)
        {
            this.ShowVerticalRuler = !this.ShowVerticalRuler;
            this.ShowHorizontalRuler = !this.ShowHorizontalRuler;
        }
        public virtual void CommandSave(string arg)
        {
            this.Save();
        }
        public virtual void CommandSaveAs(string arg)
        {
            this.SaveAs();
        }

        public virtual void CommandNew(string arg)
        {
            this.Clear();
            this.Init();
            this.ReFreshFirstDisplayRowIndex();
            this.RefreshRowHeaderWidth();
        }       

        public virtual void CommandClear(string arg)
        {
            string file = this._filename;
            this.Clear();
            this.Init();
            this.ReFreshFirstDisplayRowIndex();
            this.RefreshRowHeaderWidth();
            this._filename = file;
        }       
        public virtual void CommandNew36(string arg)
        {
            int width = this.DefaultColumnWidth;
            Forms.frmColumnWidth dlg = new Forms.frmColumnWidth();
            dlg.txtInput.Text = "30";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                int inputwidth = Feng.Utils.ConvertHelper.ToInt32(dlg.txtInput.Text, 30);
                if (dlg.chkfullColumn.Checked)
                {
                    this.DefaultColumnWidth = inputwidth;
                    int maxcolumn = this.MaxColumn;
                    for (int i = 1; i <= maxcolumn; i++)
                    {
                        IColumn item = this.Columns[i];
                        if (item == null)
                            continue;
                        if (width == item.Width)
                        {
                            item.Width = this.DefaultColumnWidth;
                        }
                    }
                }
                else
                {
                    List<ICell> list = this.GetSelectCells();
                    foreach (ICell item in list)
                    {
                        if (item != null)
                        {
                            item.Column.Width = inputwidth;
                        }
                    }
                }
            }
            this.ReFreshFirstDisplayColumnIndex();
        }

        public virtual void CommandOpen(string arg)
        {
            this.Open();
        }
        public virtual void CommandPrint(string arg)
        {
            this.Print();
        }
        public virtual void CommandPrintView(string arg)
        {
            this.PrintView();
        }
        public virtual void CommandPrintSetting(string arg)
        {
            this.PrintSetting();
        }
        public virtual void CommandPrintArea(string arg)
        {
            SelectCellCollection cells = GetArgCells(arg);
            if (cells .BeginCell==cells.EndCell)
            {
                SelectCellCollection sel = this.SelectCells as SelectCellCollection;
                if (sel != null)
                {
                    this.SetPrintArea(sel);
                }
            }
            else
            {
                this.SetPrintArea(cells);
            }
        }
        public virtual void CommandPrintAreaCancel(string arg)
        { 
                this.SetPrintArea(null); 
        }
        #region Test
        public virtual void TestA(string arg)
        {
            //this.VScroller.Location = new Point(0, 0);
            //this.VScroller.Show();
            //this.VScroller.Width = 100;
            //this.VScroller.Height = 30;
            //this.VScroller.Parent = this; 
        }

        #endregion

        public virtual void CommandCellTable(string arg)
        {
            if (this.SelectCells != null)
            {
                CellTableTools.SetSelectCellsDataTable(this, this.SelectCells);
            }
        }
        public virtual void CommandTable(string arg)
        {
            if (this.SelectCells != null)
            {
                int minrow = this.SelectCells.MinRow();
                int mincolumn = this.SelectCells.MinColumn();
                int maxrow = this.SelectCells.MaxRow();
                int maxcolumn = this.SelectCells.MaxRow();
                string tablename = "TableName_" + minrow + "_" + mincolumn;
                int row = 0;
                int column = 1;
                for (int irow = 0; irow <= maxrow; irow++)
                {
                    for (int icolumn = 0; icolumn < maxcolumn; icolumn++)
                    {
                        ICell cell = this[irow, icolumn];
                        if (cell.OwnMergeCell != null)
                        {
                            cell = cell.OwnMergeCell;
                            icolumn = icolumn + cell.MaxColumnIndex - cell.Column.Index;
                        }
                        cell.TableName = tablename;
                        cell.TableRowIndex = row;
                        cell.TableColumnName = "Column_" + column;
                        column = column + 1;
                    }
                    row = row + 1;
                }
            }
        }
        public virtual void CommandGridReadOnly(string arg)
        {
            this.ReadOnly = !this.ReadOnly;
        }
        public virtual void CommandGridShowColumnHeader(string arg)
        {
            this.ShowColumnHeader = !this.ShowColumnHeader;
        }
        public virtual void CommandGridShowRowHeader(string arg)
        {
            this.ShowRowHeader = !this.ShowRowHeader;
        }
        public virtual void CommandGridShowHeader(string arg)
        {
            this.ShowRowHeader = !this.ShowColumnHeader;
            this.ShowColumnHeader = !this.ShowColumnHeader;
        }

        public virtual void CommandGridShowGridColumnLine(string arg)
        {
            this.ShowGridColumnLine = !this.ShowGridColumnLine;
        }
        public virtual void CommandGridShowGridRowLine(string arg)
        {
            this.ShowGridRowLine = !this.ShowGridRowLine;
        }
        public virtual void CommandShowGridLine(string arg)
        {
            this.ShowGridRowLine = !this.ShowGridColumnLine;
            this.ShowGridColumnLine = !this.ShowGridColumnLine;
        }
        public virtual void CommandUndefined(string arg)
        {
#warning CommandUndefined
        }
        public virtual void CommandCancel(string arg)
        {
            this.Cancel();
        }
        public virtual void CommandMoveFocusedCellToUp(string arg)
        {
            this.MoveFocusedCellToUpCell();
        }
        public virtual void CommandMoveFocusedCellToDown(string arg)
        {
            this.MoveFocusedCellToDownCell();
        }
        public virtual void CommandMoveFocusedCellToLeft(string arg)
        {
            this.MoveFocusedCellToLeftCell();
        }
        public virtual void CommandMoveFocusedCellToRight(string arg)
        {
            this.MoveFocusedCellToRightCell();
        }
        public virtual void CommandMoveFocusedCellToTab(string arg)
        {
            this.MoveFocusedCellToNextTabCell();
        }
        public virtual void CommandSelectAll(string arg)
        {
            this.SelectAll(arg);
        }

        private void SelectAll(string arg)
        {
            //int minrow = 0;
            //int maxrow = this.MaxHasValueRow;
            //int mincol = 0;
            //int maxcol = this.MaxHasValueColumn;
            //this.SelectCell(minrow, mincol, maxrow, maxcol);
        }
        public virtual void CommandCopy(string arg)
        {
            this.Copy();
        }        

        public virtual void CommandCopyFormat(string arg)
        {
            this.CopyFormat();
        }
        public virtual void CommandPasteText(string arg)
        {
            this.PasteText();
        }
        public virtual void CommandPasteFormatBorder(string arg)
        {
            this.PasteCellBorder();
        }
        public virtual void CommandPasteFormatColor(string arg)
        {
            this.PasteColorAndImage();
        }
        public virtual void CommandCellReadOnly(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                bool isreadonly = true;
                if (this.FocusedCell != null)
                {
                    isreadonly =! this.FocusedCell.ReadOnly;
                }
                SetSelectCellReadOnly(isreadonly);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                bool isreadonly = true;
                if (cells.BeginCell != null)
                {
                    isreadonly = !cells.BeginCell.ReadOnly;
                }
                SetSelectCellReadOnly(cells, isreadonly);
            }
        }
        public virtual void CommandPasteFormat(string arg)
        {
            this.PasteFormat();
        }
        public virtual void CommandCopyID(string arg)
        {
            this.CopyID();
        }
        public virtual void CommandCut(string arg)
        {
            this.Cut();
        }

        public virtual void CommandPaste(string arg)
        {
            this.Paste();
        }

        public virtual void CommandPasteBorder(string arg)
        {
            this.PasteBorder();
        }
        public virtual void CommandFind(string arg)
        {
            this.GoToFind();
        }
        public virtual void CommandReplace(string arg)
        {
            this.GoToReplace();
        }
        public virtual void CommandGo(string arg)
        {
            this.GoToLine();
        }
        public virtual void CommandUndo(string arg)
        {
            this.Undo();
        }
        public virtual void CommandRedo(string arg)
        {
            this.Redo();
        }
        public virtual void CommandFirstCell(string arg)
        {
            this.GoToFirstCell();
        }
        public virtual void CommandRemember(string arg)
        {
            if (this.CompositeKeys2.RememberCommands)
            {
                this.CompositeKeys2.BeginRememberCommands();
            }
            else
            {
                this.CompositeKeys2.EndRememberCommands();
            }
        }

        public virtual void CommandRepeat(string arg)
        {
            foreach (CommandObject command in this.CompositeKeys2.RememberCommandItems)
            {
                
            }
        }

        public virtual void CommandColumn36(string arg)
        {
            this.DefaultColumnWidth = 36;
            foreach (IColumn item in this.Columns)
            {
                item.Width = 36;
            }
            this.ReFreshFirstDisplayColumnIndex();
        }

        public virtual void CommandInsertRow(string arg)
        {
            if (this.ReadOnly)
                return;
            if (string.IsNullOrWhiteSpace(arg))
            {
                InsertRow();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                InsertRow(cells);
            }
        }
        public virtual void CommandInsertColumn(string arg)
        {
            if (this.ReadOnly)
                return;
            if (string.IsNullOrWhiteSpace(arg))
            {
                InsertColumn();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                InsertColumn(cells);
            }
        }

        public virtual void CommandInsertCellMoveDown(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                InsertCellMoveDown();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                InsertCellMoveDown(cells);
            }
        }
        public virtual void CommandInsertCellMoveRight(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                InsertCellMoveRight();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                InsertCellMoveRight(cells);
            }
        }

        public virtual void CommandDeleteRow(string arg)
        { 
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.DeleteRow();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                DeleteRow(cells.GetAllCells());
            }
            this.ReFreshFirstDisplayRowIndex();
        }

        public virtual void CommandDeleteEmptyRow(string arg)
        {
            this.DeleteEmptyRow();
            this.ReFreshFirstDisplayRowIndex();
        }

        public virtual void CommandDeleteColumn(string arg)
        {

            if (string.IsNullOrWhiteSpace(arg))
            {
                this.DeleteColumn();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                DeleteColumn(cells.GetAllCells());
            }
            this.ReFreshFirstDisplayColumnIndex();
        }
        public virtual void CommandHideColumn(string arg)
        {

            if (string.IsNullOrWhiteSpace(arg))
            {
                this.HideColumn();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                HideColumn(cells.GetAllCells());
            }
            this.ReFreshFirstDisplayColumnIndex();
        }
        public virtual void CommandShowColumn(string arg)
        {

            if (string.IsNullOrWhiteSpace(arg))
            {
                this.ShowColumn();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                ShowColumn(cells.GetAllCells());
            }
            this.ReFreshFirstDisplayColumnIndex();
        }
        public virtual void CommandShowRow(string arg)
        {

            if (string.IsNullOrWhiteSpace(arg))
            {
                this.ShowRow();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                ShowRow(cells.GetAllCells());
            }
            this.ReFreshFirstDisplayRowIndex();
        }
        public virtual void CommandHideRow(string arg)
        {

            if (string.IsNullOrWhiteSpace(arg))
            {
                this.HideRow();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                HideRow(cells.GetAllCells());
            }
            this.ReFreshFirstDisplayRowIndex();
        }
        public virtual void CommandClearCell(string arg)
        {

            if (string.IsNullOrWhiteSpace(arg))
            {
                this.ClearCell();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                ClearCell(cells.GetAllCells());
            } 
        }
        
        public virtual void CommandDeleteCellMoveUp(string arg)
        {  
            if (string.IsNullOrWhiteSpace(arg))
            {
                DeleteCellMoveUp();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                DeleteCellMoveUp(cells);
            }
        }
        public virtual void CommandDeleteCellMoveLeft(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                DeleteCellMoveLeft();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                DeleteCellMoveLeft(cells);
            }
        }
        public virtual void CommandShowShortcut(string arg)
        {
            this.ShowShortcut(arg);
        }
        public void ShowShortcut(string arg)
        {
            Feng.Forms.TextDialog.ShowInputTextDialog("快捷键", CompositeKeys2.GetInfo());
        }
        public virtual void CommandShowHistory(string arg)
        {
            StringBuilder sb = new StringBuilder();
            foreach (CommandObject text in CompositeKeys2.CommandHistory)
            {
                sb.AppendLine(text.CommandText +" "+text .CommandArgs);
            }
            Feng.Forms.TextDialog.ShowInputTextDialog("历史命令", sb.ToString());
        }

        public virtual void CommandShowRemember(string arg)
        {
            StringBuilder sb = new StringBuilder();
            foreach (CommandObject text in CompositeKeys2.RememberCommandItems)
            {
                sb.AppendLine(text.CommandText + " " + text.CommandArgs);
            }
            Feng.Forms.TextDialog.ShowInputTextDialog("记录命令", sb.ToString());
        }
        public virtual void CommandFocusedCellPrev(string arg)
        {
            this.TurnFocusdToPrevFocsedCellMark();
        }
        public virtual void CommandFocusedCellNext(string arg)
        {
            this.TurnFocusdToNextFocsedCellMark();
        }
        public virtual void CommandColumnAutoSize(string arg)
        {
            this.ReFreshColumnHeaderWidth(true);
        }
        public virtual void CommandBookmarkFirst(string arg)
        {
            this.TurnFocusdToFirstFocsedCellMark();
        }
        public virtual void CommandBookmarkEnd(string arg)
        {
            this.TurnFocusdToFooterFocsedCellMark();
        }
        public virtual void CommandBookmarkNext(string arg)
        {
            this.TurnFocusdToNextBookMark();
        }
        public virtual void CommandBookmarkPrev(string arg)
        {
            this.TurnFocusdToPrevBookMark();
        }
        public virtual void CommandBookmarkHeader(string arg)
        {
            this.TurnFocusdToFirstBookMark();
        }
        public virtual void CommandBookmarkFooter(string arg)
        {
            this.TurnFocusdToFooterBookMark();
        }
        public virtual void CommandBookmarkAdd(string arg)
        {
            this.AddBookMark();
        }
        public virtual void CommandBookmarkDelete(string arg)
        {
            this.RemoveBookMark();
        }
        public virtual void CommandFontCancel(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellFontStyleBold(false);

                this.SetSelectCellFontStyleItalic(false);

                this.SetSelectCellFontStyleUnderline(false);

                this.SetSelectCellFontStyleStrikeout(false);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellFontStyleBold(cells, false);

                this.SetSelectCellFontStyleItalic(cells, false);

                this.SetSelectCellFontStyleUnderline(cells, false);

                this.SetSelectCellFontStyleStrikeout(cells, false);
            }


        }
        public virtual void CommandFont(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellFont(this.GetSelectCells());
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellFont(cells.GetAllCells());
            }
        }
        public virtual void CommandTextToLower(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellTextToLower();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellTextToLower(cells);
            }

        }
        public virtual void CommandTextToUpper(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellTextToUpper();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellTextToUpper(cells);
            }

        }
        public virtual void CommandFontBold(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellFontStyleBold(true);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellFontStyleBold(cells, true);
            }


        }
        public virtual void CommandFontBoldCancel(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellFontStyleBold(false);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellFontStyleBold(cells, false);
            }


        }
        public virtual void CommandFontItalic(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellFontStyleItalic(true);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellFontStyleItalic(cells, true);
            }

        }
        public virtual void CommandFontItalicCancel(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellFontStyleItalic(false);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellFontStyleItalic(cells, false);
            }

        }
        public virtual void CommandFontUnderline(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellFontStyleUnderline(true);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellFontStyleUnderline(cells, true);
            }

        }
        public virtual void CommandFontUnderlineCancel(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellFontStyleUnderline(false);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellFontStyleUnderline(cells, false);
            }

        }
        public virtual void CommandFontStrikeout(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellFontStyleStrikeout(true);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellFontStyleStrikeout(cells, true);
            }

        }
        public virtual void CommandFontStrikeoutCancel(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellFontStyleStrikeout(false);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellFontStyleStrikeout(cells, false);
            }

        }
        public virtual void CommandFontSizeUp(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellFontStyleFontSize(true);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellFontStyleFontSize(cells, true);
            }

        }
        public virtual void CommandFontSizeDown(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellFontStyleFontSize(false);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellFontStyleFontSize(cells, false);
            }

        }
        public virtual void CommandTextAutoMultiline(string arg)
        {
            
            if (string.IsNullOrWhiteSpace(arg))
            {
                bool automulline = false;
                if (this.FocusedCell != null)
                {
                    automulline = !this.FocusedCell.AutoMultiline;
                }
                this.SetSelectCellAllTextAutoMultLine(automulline);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellAllTextAutoMultLine(cells, true);
            }
        }
        public virtual void CommandTextAutoMultilineCancel(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellAllTextAutoMultLine(false);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellAllTextAutoMultLine(cells, false);
            }
        }
        public virtual void CommandMergeClear(string arg)
        {
            List<ICell> cells = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                cells = this.SelectCells.GetAllCells();
            }
            else
            {
                SelectCellCollection cellrang = GetArgCells(arg);
                cells = cellrang.GetAllCells();
            }
            foreach (ICell cell in cells)
            {
                this.UnMergeCell(cell.OwnMergeCell as IMergeCell);
                this.FocusedCell = cell;
            }
        }
        public virtual void CommandMergeCell(string arg)
        {
            ISelectCellCollection cells = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                cells = this.SelectCells;
            }
            else
            {
                SelectCellCollection cellrang = GetArgCells(arg);
                cells = cellrang;
            }

            if (cells != null)
            {
                if (!IsSingleSelect)
                {
                    bool hasmerge = false;
                    List<ICell> list = cells.GetAllCells();
                    foreach (ICell item in list)
                    {
                        if (item is IMergeCell)
                        {
                            this.UnMergeCell(item as IMergeCell);
                            hasmerge = true;
                        }
                        if (item.OwnMergeCell!=null)
                        {
                            this.UnMergeCell(item.OwnMergeCell);
                            hasmerge = true;
                        }
                    }
                    if (!hasmerge)
                    {
                        IMergeCell mcell = this.MergeCell(cells);
                        this.FocusedCell = mcell;
                    }
                }
                else
                {
                    IMergeCell mcell = this.SelectCells.BeginCell as IMergeCell;
                    if (mcell != null)
                    {
                        ICell cellbegin = mcell.BeginCell;
                        ICell cellend = mcell.EndCell;
                        this.UnMergeCell(mcell);
                        this.SelectCells.BeginCell = cellbegin;
                        this.SelectCells.EndCell = cellend;
                    }
                }
            }
            this.ReFreshFirstDisplayColumnIndex();
            this.ReFreshFirstDisplayRowIndex();
        }
#warning DARKCOLOR
        //TODO

        public virtual void CommandCellBackGround1(string arg)
        {   //赤橙黄绿青蓝紫
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellColorBackColor(Color.Red);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellColorBackColor(cells.GetAllCells(),Color.Red);
            }
        }

        public virtual void CommandCellVisible(string arg)
        {   
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellVisable(this.GetSelectCells());
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellVisable(cells.GetAllCells());
            }
        }

        public virtual void CommandCellHide(string arg)
        {   
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellHide(this.GetSelectCells());
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellHide(cells.GetAllCells());
            }
        }
        public virtual void CommandCellBackGround(string arg)
        {   //赤橙黄绿青蓝紫
            using (ColorDialog dlg = new ColorDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    if (string.IsNullOrWhiteSpace(arg))
                    {
                        this.SetSelectCellColorBackColor(dlg.Color);
                    }
                    else
                    {
                        SelectCellCollection cells = GetArgCells(arg);
                        this.SetSelectCellColorBackColor(dlg.Color);
                    } 
                }
            }
        }
        public virtual void CommandCellForeColor(string arg)
        {   //赤橙黄绿青蓝紫
            using (ColorDialog dlg = new ColorDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {

                    if (string.IsNullOrWhiteSpace(arg))
                    {
                        this.SetSelectCellColorForeColor(dlg.Color);
                    }
                    else
                    {
                        SelectCellCollection cells = GetArgCells(arg);
                        this.SetSelectCellColorForeColor(dlg.Color);
                    } 
                }
            }
        }
        public virtual void CommandCellBackGround2(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellColorBackColor(Color.Orange);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellColorBackColor(cells.GetAllCells(), Color.Orange);
            }
        }
        public virtual void CommandCellBackGround3(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellColorBackColor(Color.Yellow);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellColorBackColor(cells.GetAllCells(), Color.Yellow);
            }
        }
        public virtual void CommandCellBackGround4(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellColorBackColor(Color.Green);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellColorBackColor(cells.GetAllCells(), Color.Green);
            }
        }
        public virtual void CommandCellBackGround5(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellColorBackColor(Color.Cyan);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellColorBackColor(cells.GetAllCells(), Color.Cyan);
            }
        }
        public virtual void CommandCellBackGround6(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellColorBackColor(Color.Blue);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellColorBackColor(cells.GetAllCells(), Color.Blue);
            }
        }
        public virtual void CommandCellBackGround7(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellColorBackColor(Color.Purple);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellColorBackColor(cells.GetAllCells(), Color.Purple);
            }
        }
        public virtual void CommandCellBackGroundLight(string arg)
        {
            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            this.BeginReFresh();

            foreach (ICell cell in list)
            {
                Color color = System.Windows.Forms.ControlPaint.Light(cell.BackColor);
                cell.BackColor = color;
            }
            this.EndReFresh();

        }
        public virtual void CommandCellBackGroundDark(string arg)
        {
            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            this.BeginReFresh();
            foreach (ICell cell in list)
            {
                Color color = System.Windows.Forms.ControlPaint.Dark(cell.BackColor);
                cell.BackColor = color;
            }
            this.EndReFresh();

        }
        public virtual void CommandBorderFull(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellBorderClear(false);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellBorderClear(cells.GetAllCells(), false);
            }
        }
        public virtual void CommandBorderClear(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellBorderClear(true);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellBorderClear(cells.GetAllCells(), true);
            }
        }
        public virtual void CommandBorderTop(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellBorderTop(false);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellBorderTop(cells.GetAllCells(), false);
            }
        }
        public virtual void CommandBorderBottom(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellBorderBottom(false);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellBorderBottom(cells.GetAllCells(), false);
            }
        }
        public virtual void CommandBorderLeft(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellBorderLeft(false);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellBorderLeft(cells.GetAllCells(), false);
            }
        }
        public virtual void CommandBorderRight(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellBorderRight(false);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellBorderRight(cells.GetAllCells(), false);
            }
        }
        public virtual void CommandBorderLeftTopToRightBottom(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellBorderLeftTopToRightBottom(false);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellBorderLeftTopToRightBottom(cells.GetAllCells(), false);
            }
        }
        public virtual void CommandBorderBorderOutside(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellBorderOutsideClear(false);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellBorderOutsideClear(cells.GetAllCells(), false);
            }
        }
        public virtual void CommandBorderLeftBoomToRightTop(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellBorderLeftBottomToRightTop(false);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellBorderLeftBottomToRightTop(cells.GetAllCells(), false);
            }
        }
        public virtual void CommandBorderTopClear(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellBorderTop(true);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellBorderTop(cells.GetAllCells(), true);
            }
        }
        public virtual void CommandBorderBottomClear(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellBorderBottom(true);

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellBorderBottom(cells.GetAllCells(), true);
            }
        }
        public virtual void CommandBorderLeftClear(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellBorderLeft(true);

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellBorderLeft(cells.GetAllCells(), true);
            }
        }
        public virtual void CommandBorderRightClear(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellBorderRight(true);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellBorderRight(cells.GetAllCells(), true);
            }
        }
        public virtual void CommandBorderLeftTopToRightBottomClear(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellBorderLeftTopToRightBottom(true);

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellBorderLeftTopToRightBottom(cells.GetAllCells(), true);
            }
        }
        public virtual void CommandBorderLeftBottomToRightTopClear(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellBorderLeftBottomToRightTop(true);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellBorderLeftBottomToRightTop(cells.GetAllCells(), true);
            }
        }
        public virtual void CommandBorderOutsideClear(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellBorderOutsideClear(false);

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellBorderOutsideClear(cells.GetAllCells(), true);
            }
        }
        public virtual void CommandPasteLoop(string arg)
        {
            if (CopyText.Count < 1)
            {
                if (this.FocusedCell != null)
                {
                    if (!this.FocusedCell.ReadOnly)
                    {
                        string valuestr = Feng.Forms.ClipboardHelper.GetText();
                        if (this.CanUndoRedo)
                        {
                            CommandChangedCommand cmd = new CommandChangedCommand();
                            cmd.Value = this.GetFileData();
                            cmd.Grid = this;
                            this.Commands.Add(cmd);
                        }
                        this.FocusedCell.Value = valuestr;
                    }
                }
                return;
            }
            //int index = ConvertHelper.ToInt32(CompositeKeys.LastCompsite.Value, 0);
            //if (CompositeKeys.LastCompsite.Command == CommandPasteLoop)
            //{
            //    index++;
            //    if (index >= CopyText.Count)
            //    {
            //        index = 0;
            //    }
            //}
            //else
            //{
            //    index = 0;
            //}
            string text = string.Empty;
            //text = CopyText.GetOrderDesc(index);
#warning CopyText.GetOrderDesc(index);

            Paste(text);
            Feng.Forms.ClipboardHelper.SetText(text);
            //CompositeKeys.LastCompsite.Command = CommandPasteLoop;
            //CompositeKeys.LastCompsite.Value = index;
        }
        public virtual void CommandPasteClear(string arg)
        {
            CopyText.Clear();
        }

        public virtual void CommandSelectUp(string arg)
        {

            if (this.SelectCells != null)
            {
                int minrow = 0;
                int maxrow = 0;
                int mincolumn = 0;
                int maxcolumn = 0;
                ICell cellbegin = null;
                ICell cellend = null;

                int fminrow = this.FocusedCell.Row.Index;
                int fmaxrow = this.FocusedCell.MaxRowIndex;
                int sminrow = this.SelectCells.MinRow();
                int smaxrow = this.SelectCells.MaxRow();
                int index = fminrow;
                if (sminrow == fminrow && smaxrow == fmaxrow)
                {
                    minrow = fminrow - 1;
                    maxrow = smaxrow;
                    if (minrow > 0)
                    {
                        mincolumn = this.SelectCells.MinColumn();
                        maxcolumn = this.SelectCells.MaxColumn();
                        cellbegin = this[minrow, mincolumn];
                        cellend = this[maxrow, maxcolumn];
                        this.SelectCells.BeginCell = cellbegin;
                        this.SelectCells.EndCell = cellend;
                    }
                }
                if (sminrow < fminrow)
                {
                    minrow = sminrow - 1;
                    maxrow = smaxrow;
                    if (minrow > 0)
                    {
                        mincolumn = this.SelectCells.MinColumn();
                        maxcolumn = this.SelectCells.MaxColumn();
                        cellbegin = this[minrow, mincolumn];
                        cellend = this[maxrow, maxcolumn];
                        this.SelectCells.BeginCell = cellbegin;
                        this.SelectCells.EndCell = cellend;
                    }
                }
                if (smaxrow > fmaxrow)
                {
                    minrow = fminrow;
                    maxrow = smaxrow - 1;
                    if (minrow > 0)
                    {
                        mincolumn = this.SelectCells.MinColumn();
                        maxcolumn = this.SelectCells.MaxColumn();
                        cellbegin = this[minrow, mincolumn];
                        cellend = this[maxrow, maxcolumn];
                        this.SelectCells.BeginCell = cellbegin;
                        this.SelectCells.EndCell = cellend;
                    }
                }

            }
        }
        public virtual void CommandSelectDown(string arg)
        {
            if (this.SelectCells != null)
            {
                int minrow = 0;
                int maxrow = 0;
                int mincolumn = 0;
                int maxcolumn = 0;
                ICell cellbegin = null;
                ICell cellend = null;

                int fminrow = this.FocusedCell.Row.Index;
                int fmaxrow = this.FocusedCell.MaxRowIndex;
                int sminrow = this.SelectCells.MinRow();
                int smaxrow = this.SelectCells.MaxRow();
                int index = fminrow;
                if (sminrow == fminrow && smaxrow == fmaxrow)
                {
                    minrow = fminrow;
                    maxrow = smaxrow + 1;
                    mincolumn = this.SelectCells.MinColumn();
                    maxcolumn = this.SelectCells.MaxColumn();
                    cellbegin = this[minrow, mincolumn];
                    cellend = this[maxrow, maxcolumn];
                    this.SelectCells.BeginCell = cellbegin;
                    this.SelectCells.EndCell = cellend;

                }
                if (sminrow < fminrow)
                {
                    minrow = sminrow + 1;
                    maxrow = smaxrow;
                    mincolumn = this.SelectCells.MinColumn();
                    maxcolumn = this.SelectCells.MaxColumn();
                    cellbegin = this[minrow, mincolumn];
                    cellend = this[maxrow, maxcolumn];
                    this.SelectCells.BeginCell = cellbegin;
                    this.SelectCells.EndCell = cellend;

                }
                if (smaxrow > fmaxrow)
                {
                    minrow = fminrow;
                    maxrow = smaxrow + 1;
                    mincolumn = this.SelectCells.MinColumn();
                    maxcolumn = this.SelectCells.MaxColumn();
                    cellbegin = this[minrow, mincolumn];
                    cellend = this[maxrow, maxcolumn];
                    this.SelectCells.BeginCell = cellbegin;
                    this.SelectCells.EndCell = cellend;

                }
            }
        }
        public virtual void CommandSelectLeft(string arg)
        {
            if (this.SelectCells != null)
            {
                int minrow = 0;
                int maxrow = 0;
                int mincolumn = 0;
                int maxcolumn = 0;
                ICell cellbegin = null;
                ICell cellend = null;

                int fmin = this.FocusedCell.Column.Index;
                int fmax = this.FocusedCell.MaxColumnIndex;
                int smin = this.SelectCells.MinColumn();
                int smax = this.SelectCells.MaxColumn();

                if (smin == fmin && smax == fmax)
                {
                    mincolumn = fmin - 1;
                    maxcolumn = smax;
                    if (mincolumn > 0)
                    {
                        minrow = this.SelectCells.MinRow();
                        maxrow = this.SelectCells.MaxRow();
                        cellbegin = this[minrow, mincolumn];
                        cellend = this[maxrow, maxcolumn];
                        this.SelectCells.BeginCell = cellbegin;
                        this.SelectCells.EndCell = cellend;
                    }
                }
                if (smin < fmin)
                {
                    mincolumn = smin - 1;
                    maxcolumn = smax;
                    if (mincolumn > 0)
                    {
                        minrow = this.SelectCells.MinRow();
                        maxrow = this.SelectCells.MaxRow();
                        cellbegin = this[minrow, mincolumn];
                        cellend = this[maxrow, maxcolumn];
                        this.SelectCells.BeginCell = cellbegin;
                        this.SelectCells.EndCell = cellend;
                    }
                }
                if (smax > fmax)
                {
                    mincolumn = fmin;
                    maxcolumn = smax - 1;
                    if (mincolumn > 0)
                    {
                        minrow = this.SelectCells.MinRow();
                        maxrow = this.SelectCells.MaxRow();
                        cellbegin = this[minrow, mincolumn];
                        cellend = this[maxrow, maxcolumn];
                        this.SelectCells.BeginCell = cellbegin;
                        this.SelectCells.EndCell = cellend;
                    }
                }
            }
        }
        public virtual void CommandSelectRight(string arg)
        {
            if (this.SelectCells != null)
            {
                int minrow = 0;
                int maxrow = 0;
                int mincolumn = 0;
                int maxcolumn = 0;
                ICell cellbegin = null;
                ICell cellend = null;

                int fmin = this.FocusedCell.Column.Index;
                int fmax = this.FocusedCell.MaxColumnIndex;
                int smin = this.SelectCells.MinColumn();
                int smax = this.SelectCells.MaxColumn();

                if (smin == fmin && smax == fmax)
                {
                    mincolumn = fmin;
                    maxcolumn = smax + 1;
                    minrow = this.SelectCells.MinRow();
                    maxrow = this.SelectCells.MaxRow();
                    cellbegin = this[minrow, mincolumn];
                    cellend = this[maxrow, maxcolumn];
                    this.SelectCells.BeginCell = cellbegin;
                    this.SelectCells.EndCell = cellend;

                }
                if (smin < fmin)
                {
                    mincolumn = smin + 1;
                    maxcolumn = smax;
                    minrow = this.SelectCells.MinRow();
                    maxrow = this.SelectCells.MaxRow();
                    cellbegin = this[minrow, mincolumn];
                    cellend = this[maxrow, maxcolumn];
                    this.SelectCells.BeginCell = cellbegin;
                    this.SelectCells.EndCell = cellend;

                }
                if (smax > fmax)
                {
                    mincolumn = fmin;
                    maxcolumn = smax + 1;
                    minrow = this.SelectCells.MinRow();
                    maxrow = this.SelectCells.MaxRow();
                    cellbegin = this[minrow, mincolumn];
                    cellend = this[maxrow, maxcolumn];
                    this.SelectCells.BeginCell = cellbegin;
                    this.SelectCells.EndCell = cellend;

                }
            }
        }

        public virtual void CommandSelectUpMove(string arg)
        {
#warning  Cells
            ISelectCellCollection cells = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                cells = this.SelectCells;
            }
            else
            {
                cells = GetArgCells(arg);
            }
            CellMoveUp(cells);
        }
        public virtual void CommandSelectDownMove(string arg)
        {
#warning  Cells
            ISelectCellCollection cells = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                cells = this.SelectCells;
            }
            else
            {
                cells = GetArgCells(arg);
            }
            CellMoveDown(cells);
        }
        public virtual void CommandSelectLeftMove(string arg)
        {
#warning  Cells
            ISelectCellCollection cells = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                cells = this.SelectCells;
            }
            else
            {
                cells = GetArgCells(arg);
            }
            CellMoveLeft(cells);
        }
        public virtual void CommandSelectRightMove(string arg)
        {
#warning  Cells
            ISelectCellCollection cells = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                cells = this.SelectCells;
            }
            else
            {
                cells = GetArgCells(arg);
            }
            CellMoveRight(cells);
        }
        public virtual void CommandShowCellInfo(string arg)
        {
            this.DrawCell += DataExcel_DrawCell;
        }
        public virtual void CommandHideCellInfo(string arg)
        {
            this.DrawCell -= DataExcel_DrawCell;
        }
        void DataExcel_DrawCell(object sender, DrawCellArgs e)
        {

            try
            {
                e.Graphics.Graphics.DrawString(string.Format("Cell:{0}", e.Cell.Text), this.Font, Brushes.Red, e.Cell.Rect);
                //e.Graphics.Graphics.DrawString(string.Format("Cell:{0},{1},{2},{3}", e.Cell.Row.Index, e.Cell.Column.Index, e.Cell.MaxRowIndex, e.Cell.MaxColumnIndex), this.Font, Brushes.Red, e.Cell.Rect); 
            }
            catch (Exception ex)
            {
            }

        }
        public virtual void CommandSelectUpText(string arg)
        {
#warning test
            if (this.SelectCells != null)
            {
                int minrow = 0;
                int maxrow = 0;
                int mincolumn = 0;
                int maxcolumn = 0;
                ICell cellbegin = null;
                ICell cellend = null;

                int fminrow = this.FocusedCell.Row.Index;
                int fmaxrow = this.FocusedCell.MaxRowIndex;
                int sminrow = this.SelectCells.MinRow();
                int smaxrow = this.SelectCells.MaxRow();

                int frowindex = this.FocusedCell.Column.Index;
                int index = 0;
                for (int i = fminrow; i > 0; i--)
                {
                    ICell cell = this[i, frowindex];
                    string celltext = cell.Text;
                    if (!string.IsNullOrWhiteSpace(celltext))
                    {
                        index = i;
                    }
                    else
                    {
                        break;
                    }
                }

                if (sminrow == fminrow && smaxrow == fmaxrow)
                {
                    minrow = index;
                    maxrow = smaxrow;
                    if (minrow > 0)
                    {
                        mincolumn = this.SelectCells.MinColumn();
                        maxcolumn = this.SelectCells.MaxColumn();
                        cellbegin = this[minrow, mincolumn];
                        cellend = this[maxrow, maxcolumn];
                        this.SelectCells.BeginCell = cellbegin;
                        this.SelectCells.EndCell = cellend;
                    }
                }
                if (sminrow < fminrow)
                {
                    minrow = index;
                    maxrow = smaxrow;
                    if (minrow > 0)
                    {
                        mincolumn = this.SelectCells.MinColumn();
                        maxcolumn = this.SelectCells.MaxColumn();
                        cellbegin = this[minrow, mincolumn];
                        cellend = this[maxrow, maxcolumn];
                        this.SelectCells.BeginCell = cellbegin;
                        this.SelectCells.EndCell = cellend;
                    }
                }
                if (smaxrow > fmaxrow)
                {
                    minrow = fminrow;
                    maxrow = index;
                    if (minrow > 0)
                    {
                        mincolumn = this.SelectCells.MinColumn();
                        maxcolumn = this.SelectCells.MaxColumn();
                        cellbegin = this[minrow, mincolumn];
                        cellend = this[maxrow, maxcolumn];
                        this.SelectCells.BeginCell = cellbegin;
                        this.SelectCells.EndCell = cellend;
                    }
                }

            }
        }
        public virtual void CommandSelectDownText(string arg)
        {
#warning test
            if (this.SelectCells != null)
            {
                int minrow = 0;
                int maxrow = 0;
                int mincolumn = 0;
                int maxcolumn = 0;
                ICell cellbegin = null;
                ICell cellend = null;

                int fminrow = this.FocusedCell.Row.Index;
                int fmaxrow = this.FocusedCell.MaxRowIndex;
                int sminrow = this.SelectCells.MinRow();
                int smaxrow = this.SelectCells.MaxRow();

                int frowindex = this.FocusedCell.Column.Index;
                int index = 0;
                for (int i = smaxrow; i < (smaxrow + 100); i++)
                {
                    ICell cell = this[i, frowindex];
                    string celltext = cell.Text;
                    if (!string.IsNullOrWhiteSpace(celltext))
                    {
                        index = i;
                    }
                    else
                    {
                        break;
                    }
                }

                minrow = fminrow;
                maxrow = index;
                mincolumn = this.SelectCells.MinColumn();
                maxcolumn = this.SelectCells.MaxColumn();
                cellbegin = this[minrow, mincolumn];
                cellend = this[maxrow, maxcolumn];
                this.SelectCells.BeginCell = cellbegin;
                this.SelectCells.EndCell = cellend;

            }
        }
        public virtual void CommandSelectLeftText(string arg)
        {
#warning test
            if (this.SelectCells != null)
            {
                int minrow = 0;
                int maxrow = 0;
                int mincolumn = 0;
                int maxcolumn = 0;
                ICell cellbegin = null;
                ICell cellend = null;

                int fmin = this.FocusedCell.Column.Index;
                int fmax = this.FocusedCell.MaxColumnIndex;
                int smin = this.SelectCells.MinColumn();
                int smax = this.SelectCells.MaxColumn();

                int frowindex = this.FocusedCell.Row.Index;
                int index = 0;
                for (int i = fmin; i > 0; i--)
                {
                    ICell cell = this[frowindex, i];
                    string celltext = cell.Text;
                    if (!string.IsNullOrWhiteSpace(celltext))
                    {
                        index = i;
                    }
                    else
                    {
                        break;
                    }
                }


                if (smin == fmin && smax == fmax)
                {
                    mincolumn = index;
                    maxcolumn = smax;
                    if (mincolumn > 0)
                    {
                        minrow = this.SelectCells.MinRow();
                        maxrow = this.SelectCells.MaxRow();
                        cellbegin = this[minrow, mincolumn];
                        cellend = this[maxrow, maxcolumn];
                        this.SelectCells.BeginCell = cellbegin;
                        this.SelectCells.EndCell = cellend;
                    }
                }
                if (smin < fmin)
                {
                    mincolumn = index;
                    maxcolumn = smax;
                    if (mincolumn > 0)
                    {
                        minrow = this.SelectCells.MinRow();
                        maxrow = this.SelectCells.MaxRow();
                        cellbegin = this[minrow, mincolumn];
                        cellend = this[maxrow, maxcolumn];
                        this.SelectCells.BeginCell = cellbegin;
                        this.SelectCells.EndCell = cellend;
                    }
                }
                if (smax > fmax)
                {
                    mincolumn = fmin;
                    maxcolumn = index;
                    if (mincolumn > 0)
                    {
                        minrow = this.SelectCells.MinRow();
                        maxrow = this.SelectCells.MaxRow();
                        cellbegin = this[minrow, mincolumn];
                        cellend = this[maxrow, maxcolumn];
                        this.SelectCells.BeginCell = cellbegin;
                        this.SelectCells.EndCell = cellend;
                    }
                }
            }
        }
        public virtual void CommandSelectRightText(string arg)
        {
#warning test
            if (this.SelectCells != null)
            {
                int minrow = 0;
                int maxrow = 0;
                int mincolumn = 0;
                int maxcolumn = 0;
                ICell cellbegin = null;
                ICell cellend = null;

                int fmin = this.FocusedCell.Column.Index;
                int fmax = this.FocusedCell.MaxColumnIndex;
                int smin = this.SelectCells.MinColumn();
                int smax = this.SelectCells.MaxColumn();

                int frowindex = this.FocusedCell.Row.Index;
                int index = 0;
                for (int i = smax; i < (smax + 100); i++)
                {
                    ICell cell = this[frowindex, i];
                    string celltext = cell.Text;
                    if (!string.IsNullOrWhiteSpace(celltext))
                    {
                        index = i;
                    }
                    else
                    {
                        break;
                    }
                }

                mincolumn = fmin;
                maxcolumn = index;// smax + 1;
                minrow = this.SelectCells.MinRow();
                maxrow = this.SelectCells.MaxRow();
                cellbegin = this[minrow, mincolumn];
                cellend = this[maxrow, maxcolumn];
                this.SelectCells.BeginCell = cellbegin;
                this.SelectCells.EndCell = cellend;
            }
        }

        public virtual void CommandTextAlignCenter(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {

                this.SetSelectCellTextAlign(true, StringAlignment.Center
                    , true, StringAlignment.Center);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);

                this.SetSelectCellTextAlign(cells.GetAllCells(), true, StringAlignment.Center
                    , true, StringAlignment.Center);
            }
        }
        public virtual void CommandTextAlignVerticalCenter(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellTextAlign(false, StringAlignment.Center
 , true, StringAlignment.Center);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellTextAlign(cells.GetAllCells(), false, StringAlignment.Center
 , true, StringAlignment.Center);
            }

        }
        public virtual void CommandTextAlignHorizontalCenter(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellTextAlign(true, StringAlignment.Center, false, StringAlignment.Center);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellTextAlign(cells.GetAllCells(), true, StringAlignment.Center, false, StringAlignment.Center);
            }
        }
        public virtual void CommandTextAlignTop(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {

                this.SetSelectCellTextAlign(false, StringAlignment.Center, true, StringAlignment.Near);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellTextAlign(cells.GetAllCells(), false, StringAlignment.Center, true, StringAlignment.Near);
            }
        }
        public virtual void CommandTextAlignBottom(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellTextAlign(false, StringAlignment.Center, true, StringAlignment.Far);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellTextAlign(cells.GetAllCells(), false, StringAlignment.Center, true, StringAlignment.Far);
            }
        }
        public virtual void CommandTextAlignLeft(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellTextAlign(true, StringAlignment.Near, false, StringAlignment.Far);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellTextAlign(cells.GetAllCells(), true, StringAlignment.Near, false, StringAlignment.Far);

            }
        }
        public virtual void CommandTextAlignRight(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellTextAlign(true, StringAlignment.Far, false, StringAlignment.Far);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellTextAlign(cells.GetAllCells(), true, StringAlignment.Far, false, StringAlignment.Far);
            }
        }
        public virtual void CommandTextOrientationRotateDown(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellTextOrientationRotateDown(true);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellTextOrientationRotateDown(cells.GetAllCells(), true);
            }
        }

        public virtual void CommandMulCellBackImage(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                using (System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog())
                {
                    dlg.Filter = "(bmp,jpg,jpeg,png)|*.bmp;*.jpg;*.jpeg;*.png|*.bmp|*.bmp|*.jpg|*.jpg|*.jpeg|*.jpeg|*.png|*.png";
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    { 
                        IBackCell cell = this.SetBackCells();
                        cell.BackImage = (Bitmap)Bitmap.FromFile(dlg.FileName);
                        this.ReFresh();
                    }
                }
            }
            else
            {
                using (System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog())
                {
                    dlg.Filter = "(bmp,jpg,jpeg,png)|*.bmp;*.jpg;*.jpeg;*.png|*.bmp|*.bmp|*.jpg|*.jpg|*.jpeg|*.jpeg|*.png|*.png";
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        SelectCellCollection cells = GetArgCells(arg);
                        IBackCell cell = this.SetBackCells(cells);
                        cell.BackImage = (Bitmap)Bitmap.FromFile(dlg.FileName);
                        this.ReFresh();
                    }
                }
            }
        }
        public virtual void CommandMulCellBackImageCancel(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.DeleteBackCell(this.FocusedCell.OwnBackCell);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                List<ICell> list = cells.GetAllCells();
                foreach (ICell cell in list)
                {
                    this.DeleteBackCell(cell.OwnBackCell);
                }
            }
        }
        public virtual void SetSelectCellTextTrim(bool space, bool header, bool footer, bool symbol, string symboltext)
        {
            SetSelectCellTextTrim(this.GetSelectCells(), space, header, footer, symbol, symboltext);
        }
        public virtual void SetSelectCellTextTrim(List<ICell> list, bool space, bool header, bool footer, bool symbol, string symboltext)
        {
            foreach (ICell cell in list)
            {
                string text = Feng.Utils.ConvertHelper.ToString(cell.Value);
                if (space)
                {
                    if (header)
                    {
                        text = text.TrimStart();
                    }
                    else if (footer)
                    {
                        text = text.TrimEnd();
                    }
                    else
                    {
                        text = text.Trim();
                    }
                }
                if (symbol)
                {

                    if (header)
                    {
                        text = Feng.Utils.ConvertHelper.TrimStartInChars(text, symboltext);
                    }
                    else if (footer)
                    {
                        text = Feng.Utils.ConvertHelper.TrimEndInChars(text, symboltext);
                    }
                    else
                    {
                        text = Feng.Utils.ConvertHelper.TrimInChars(text, symboltext);
                    }
                }
                cell.Value = text;
            }
        }

        public virtual void CommandTextTrimSpace(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellTextTrim(true, false, false, false, AppConfig.TrimSymbol);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellTextTrim(cells.GetAllCells(), true, false, false, false, AppConfig.TrimSymbol);
            }
        }
        public virtual void CommandTextTrimStartSpace(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellTextTrim(true, true, false, false, AppConfig.TrimSymbol);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellTextTrim(cells.GetAllCells(), true, true, false, false, AppConfig.TrimSymbol);
            }
        }
        public virtual void CommandTextTrimEndSpace(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellTextTrim(true, false, true, false, AppConfig.TrimSymbol);

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellTextTrim(cells.GetAllCells(), true, false, true, false, AppConfig.TrimSymbol);
            }
        }

        public virtual void CommandTextTrimSymbol(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellTextTrim(false, false, false, true, AppConfig.TrimSymbol);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellTextTrim(cells.GetAllCells(), false, false, false, true, AppConfig.TrimSymbol);
            }
        }
        public virtual void CommandTextTrimStartSymbol(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellTextTrim(false, true, false, true, AppConfig.TrimSymbol);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellTextTrim(cells.GetAllCells(), false, true, false, true, AppConfig.TrimSymbol);
            }
        }
        public virtual void CommandTextTrimEndSymbol(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellTextTrim(false, false, true, true, AppConfig.TrimSymbol);
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellTextTrim(cells.GetAllCells(), false, false, true, true, AppConfig.TrimSymbol);
            }
        }
        public virtual void CommandSum(string arg)
        {
            if (this.FocusedCell != null)
            {
                if (this.FocusedCell.ReadOnly)
                {
                    return;
                }
                int rowindex = this.FocusedCell.Row.Index;
                int column = this.FocusedCell.Column.Index;
                int row = rowindex;
                if (rowindex > 1)
                {
                    for (int i = rowindex - 1; i >= 1; i--)
                    {
                        row = i;
                        ICell cell = this[i, column];
                        if (cell.OwnMergeCell != null)
                        {
                            cell = cell.OwnMergeCell;
                        }
                        string text = Feng.Utils.ConvertHelper.ToString(cell.Value);
                        bool isnum = Feng.Utils.ConvertHelper.IsNumber(text);
                        if (!isnum)
                        {
                            break;
                        }
                    }
                    if (row < rowindex)
                    {
                        string name1 = DataExcel.GetCellName(rowindex - 1, column);
                        string name2 = DataExcel.GetCellName(row, column);
                        string cellrange = name1 + ":" + name2;
                        string expression = "SUM(Cells(" + cellrange + "))";
                        this.FocusedCell.Expression = expression;
                    }
                }
            }
        }

        public virtual void CommandSumSelectCells(string arg)
        {
            if (this.SelectCells != null)
            {
                string name1 = this.SelectCells.MinCell.Name;
                string name2 = this.SelectCells.MaxCell.Name;
                string cellrange = name1 + ":" + name2;
                string expression = "SUM(cellvalues(\"" + cellrange + "\"))";
                Feng.Forms.ClipboardHelper.SetText(expression);
                int rowmax = this.SelectCells.MaxRow();
                int columnmin = this.SelectCells.MinColumn();
                int rowindex = rowmax + 1;

                ICell cell = this[rowindex, columnmin];
                if (!cell.ReadOnly)
                {
                    cell.Expression = expression;
                }

            }
        }


        public virtual void CommandClock(string arg)
        {
            this.AddView(new Feng.Forms.Views.ClockView() { });
        }

        public virtual void CommandCellEditNull(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellEditNull();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellEditNull(cells.GetAllCells());
            }
        }
        public virtual void CommandCellEditNumber(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellEditNumberCell();

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellEditNumberCell(cells.GetAllCells());
            }
        }
        public virtual void CommandCellEditLabel(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellEditLabelCell();

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellEditLabelCell(cells.GetAllCells());
            }
        }
        public virtual void CommandCellEditCheckBox(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellEditCheckBoxCell();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellEditCheckBoxCell(cells.GetAllCells());
            }
        }
        public virtual void CommandCellEditCnNumber(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellEditCnNumberCell();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellEditCnNumberCell(cells.GetAllCells());
            }
        }
        public virtual void CommandCellEditComboBox(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellEditComboBoxCell();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellEditComboBoxCell(cells.GetAllCells());
            }
        }
        //public virtual void CommandCellEditDateTime(string arg)
        //{
        //    if (string.IsNullOrWhiteSpace(arg))
        //    {
        //        this.SetSelectCellEditDateTimeCell();
        //    }
        //    else
        //    {
        //        SelectCellCollection cells = GetArgCells(arg);
        //        this.SetSelectCellEditDateTimeCell(cells.GetAllCells());
        //    }
        //}
        public virtual void CommandCellEditGridView(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellEditGridViewCell(this.GetSelectCells());
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellEditGridViewCell(cells.GetAllCells());
            }
        }
        public virtual void CommandCellEditImage(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellEditImageCell();

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellEditImageCell(cells.GetAllCells());
            }
        }       
        public virtual void CommandCellEditCellTimer(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellEditCellTimer();

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellEditCellTimer(cells.GetAllCells());
            }
        }
        public virtual void CommandCellEditLinkLabel(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellEditLinkLabelCell();

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellEditLinkLabelCell(cells.GetAllCells());
            }
        }
        public virtual void CommandCellEditPassword(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellEditPasswordCell();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellEditPasswordCell(cells.GetAllCells());
            }
        }
        public virtual void CommandCellEditRadioBox(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellEditRadioBoxCell();

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellEditRadioBoxCell(cells.GetAllCells());
            }
        }
        public virtual void CommandCellDropDownDateTime(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellDropDownDateTime();

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellDropDownDateTime(cells.GetAllCells());
            }
        }
        public virtual void CommandCellDropDownFillter(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellDropDownFillter();

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellDropDownFillter(cells.GetAllCells());
            }
        }
        //public virtual void CommandCellDropDownDate(string arg)
        //{
        //    if (string.IsNullOrWhiteSpace(arg))
        //    {
        //        this.SetSelectCellDropDownDate();

        //    }
        //    else
        //    {
        //        SelectCellCollection cells = GetArgCells(arg);
        //        this.SetSelectCellDropDownDate(cells.GetAllCells());
        //    }
        //}
        public virtual void CommandEditCellSplitRow(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellSplitRow();

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellSplitRow(cells.GetAllCells());
            }
        }
        public virtual void CommandEditCellSplitColumn(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellSplitColumn();

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellSplitColumn(cells.GetAllCells());
            }
        }
        public virtual void CommandCellEditTreeView(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellEditTreeViewCell(this.GetSelectCells ());

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellEditTreeViewCell(cells.GetAllCells());
            }
        }
        public virtual void CommandCellExcel(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                this.SetSelectCellExcel(this.GetSelectCells());

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                this.SetSelectCellExcel(cells.GetAllCells());
            }
        }
        public virtual void CommandCellEditColor(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellEditColorCell();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellEditColorCell(cells.GetAllCells ());
            }
        }
        public virtual void CommandCellProcess(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellProcessCell();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellProcessCell(cells.GetAllCells());
            }
        }

        public virtual void CommandCellCnNumber(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellCnNumber();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellCnNumber(cells.GetAllCells ());
            }
        }

        public virtual void CommandCellFileSelectEdit(string arg)
        {
            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            SetSelectCellFileSelectEdit(list);
        }
 
        public virtual void CommandCellDropDownDataExcel(string arg)
        {
            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            SetSelectCellDropDownDataExcel(list);
        }
 
        public virtual void CommandCellFolderBrowserEdit(string arg)
        {

            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            SetSelectCellFolderBrowserEdit(list);
        }
 
        public virtual void CommandCellMoveForm(string arg)
        {

            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            SetSelectCellMoveForm(list);
        }
 
        public virtual void CommandCellButton(string arg)
        {

            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            SetSelectCellButton(list);
        }
 
        public virtual void CommandCellSpText(string arg)
        {

            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            SetSelectCellSpText(list);
        }

        public virtual void CommandCellSwitch(string arg)
        {

            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            SetSelectCellSwitch(list);
        }
         
        public virtual void CommandCellTextBoxEdit(string arg)
        {

            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            SetSelectCellTextBoxEdit(list);
        }

        public virtual void SetSelectCellExcel(string arg)
        {

            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            SetSelectCellExcel(list);
        }

        public virtual void CommandCellToolBar(string arg)
        {

            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            SetSelectCellToolBar(list);
        }

        public virtual void CommandCellTreeView(string arg)
        {

            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            SetSelectCellTreeView(list);
        }

        public virtual void CommandCellGridView(string arg)
        {

            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            SetSelectCellGridView(list);
        }

        public virtual void CommandCellVector(string arg)
        {

            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            SetSelectCellVector(list);
        }
        public virtual void CommandSetValue(string arg)
        {

            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            CommandSetValue(list,string.Empty);
        }
        public virtual void CommandSetExpress(string arg,string function)
        {

            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            CommandSetExpress(list, function);
        }
        public virtual void CommandSetExpress(string arg)
        {

            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            CommandSetValue(list, string.Empty);
        }
        private SelectCellCollection GetArgCells(string arg)
        {
            SelectCellCollection cellRange = new SelectCellCollection();
            if (arg.Contains(":"))
            {
                string txtcell1 = arg.Split(':')[0];
                ICell cell1 = this.GetCellByID(txtcell1);
                txtcell1 = arg.Split(':')[1];
                ICell cell2 = this.GetCellByID(txtcell1);
                cellRange.BeginCell = cell1;
                cellRange.EndCell = cell2;
            }
            else
            {
                ICell cell1 = this.GetCellByID(arg);
                if (cell1 != null)
                {
                    cellRange.BeginCell = cell1;
                    cellRange.EndCell = cell1;
                }
            }
            return cellRange;
        }
        public virtual void SetSelectCellBorderLine(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellLineBorder(this.ClassFactory.CreateLineStyle());
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellLineBorder(cells, this.ClassFactory.CreateLineStyle());
            }
        }
        public virtual void CommandTextFormatMoney(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellFormatNumberMoney();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellFormatNumberMoney(cells);
            }
        }
        public virtual void CommandTextFormatPercent(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellFormatNumberPercent();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellFormatNumberPercent(cells);
            }
        }
        public virtual void CommandTextFormatThousandths(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellFormatNumberThousandths();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellFormatNumberThousandths(cells);
            }
        }
        public virtual void CommandTextFormatDecimalPlaces1(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellFormatNumberDecimalPlaces1();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellFormatNumberDecimalPlaces1(cells);
            }
        }
        public virtual void CommandTextFormatDecimalPlaces2(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellFormatNumberDecimalPlaces2();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellFormatNumberDecimalPlaces2(cells);
            }
        }
        public virtual void CommandTextFormatDateTimeDay(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellFormatDateTimeDay();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellFormatDateTimeDay(cells);
            }
        }
        public virtual void CommandTextFormatDateTimeTime(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellFormatDateTimeg();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellFormatDateTimeg(cells);
            }
        }
        public virtual void CommandTextFormatText(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellFormatText();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellFormatText(cells);
            }
        }
        public virtual void CommandCellBackImage(string arg)
        {

            using (System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog())
            {
                dlg.Filter = "(bmp,jpg,jpeg,png)|*.bmp;*.jpg;*.jpeg;*.png|*.bmp|*.bmp|*.jpg|*.jpg|*.jpeg|*.jpeg|*.png|*.png";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Bitmap bmp = (Bitmap)Bitmap.FromFile(dlg.FileName);
                    if (string.IsNullOrWhiteSpace(arg))
                    {
                        SetSelectCellImageBackImage(bmp);

                    }
                    else
                    {
                        SelectCellCollection cells = GetArgCells(arg);
                        SetSelectCellImageBackImage(cells.GetAllCells(),bmp);
                    }
                }
            }
        }
        public virtual void CommandCellBackImageClear(string arg)
        {
            Bitmap bmp = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                SetSelectCellImageBackImage(bmp);

            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                SetSelectCellImageBackImage(cells.GetAllCells(), bmp);
            }
        }

        public virtual void CommandUpdataSelectAdd(string arg)
        {
 
        }   
        public virtual void CommandFrozenColumn(string arg)
        { 
            if (string.IsNullOrWhiteSpace(arg))
            {
                if (this.FrozenColumn == this.FocusedCell.Column.Index)
                {
                    this.FrozenColumn = -1;
                }
                else
                {
                    this.FrozenColumn = this.FocusedCell.Column.Index;
                }
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                if (cells.BeginCell != null)
                {
                    if (this.FrozenColumn == cells.BeginCell.Column.Index)
                    {
                        this.FrozenColumn = -1;
                    }
                    else
                    {
                        this.FrozenColumn = cells.BeginCell.Column.Index;
                    }
                    
                }
            }
        }
        public virtual void CommandFrozenRow(string arg)
        {
            if (string.IsNullOrWhiteSpace(arg))
            {
                if (this.FrozenRow == this.FocusedCell.Row.Index)
                {
                    this.FrozenRow = -1;
                }
                else
                {
                    this.FrozenRow = this.FocusedCell.Row.Index;
                }
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                if (cells.BeginCell != null)
                {
                    if (this.FrozenRow == cells.BeginCell.Row.Index)
                    {
                        this.FrozenRow = -1;
                    }
                    else
                    {
                        this.FrozenRow = cells.BeginCell.Row.Index;
                    }

                }
            }
        }
        public virtual void CommandFrozen(string arg)
        {
            CommandFrozenColumn(arg);
            CommandFrozenRow(arg);
        }
        public virtual void CommandFrozenCancel(string arg)
        {
            this.FrozenRow = -1;
            this.FrozenColumn = -1;
        }
        public virtual void CommandDefaultStyleCellColumn(string arg)
        {
            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            List<IColumn> columns = new List<IColumn>();
            foreach (ICell cell in list)
            {
                if (!columns.Contains(cell.Column))
                {
                    columns.Add(cell.Column);
                    cell.Column.DefaultStyleCell = cell;
                }
            }
 
        }
        public virtual void CommandDefaultStyleCellRow(string arg)
        {
            List<ICell> list = null;
            if (string.IsNullOrWhiteSpace(arg))
            {
                list = GetSelectCells();
            }
            else
            {
                SelectCellCollection cells = GetArgCells(arg);
                list = cells.GetAllCells();
            }
            List<IRow> columns = new List<IRow>();
            foreach (ICell cell in list)
            {
                if (!columns.Contains(cell.Row))
                {
                    columns.Add(cell.Row);
                    cell.Row.DefaultStyleCell = cell;
                }
            }
        }
    }
}
