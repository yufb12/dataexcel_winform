using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Feng.Excel.Interfaces;
using Feng.Commands;

namespace Feng.Excel.Commands
{
    public class GridCommands : ICommand
    {
        private Excel.DataExcel Grid;
        private byte[] buff = null;

        public void SaveValue(Excel.DataExcel grid)
        {
            Grid = grid;
            buff = grid.GetFileData();
        }

        public ICommand Execute()
        {
            GridCommands command = new GridCommands();
            command.SaveValue(Grid);
            Grid.Open(buff);
            return command;
        }
    }
    public class CellCommands : ICommand
    { 
        public List<ICommand> Commands = new List<ICommand>();
        public ICommand Execute()
        {
            CellCommands cmds = new CellCommands();
            foreach (ICommand cmd in Commands)
            {
                cmds.Commands.Add(cmd.Execute());
            }
            return cmds;
        }
    }

    public class CellBoarderCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public bool LeftBottomToRightTopLineStyle { get; set; }
        public bool LeftLineStyle { get; set; }
        public bool LeftTopToRightBottomLineStyle { get; set; }
        public bool RightLineStyle { get; set; }
        public bool TopLineStyle { get; set; }
        public bool BottomLineStyle { get; set; }


        public bool isLeftBottomToRightTopLineStyle { get; set; }
        public bool isLeftLineStyle { get; set; }
        public bool isLeftTopToRightBottomLineStyle { get; set; }
        public bool isRightLineStyle { get; set; }
        public bool isTopLineStyle { get; set; }
        public bool isBottomLineStyle { get; set; }

        public ICommand Execute()
        {
            CellBackColorCommand cmd = new CellBackColorCommand();
            cmd.Value = Cell.BackColor;
            cmd.Cell = Cell;
            if (Cell.BorderStyle != null)
            {
                if (isLeftBottomToRightTopLineStyle)
                {
                    Cell.BorderStyle.LeftBottomToRightTopLineStyle.Visible = LeftBottomToRightTopLineStyle;
                }
                if (isLeftLineStyle)
                {
                    Cell.BorderStyle.LeftLineStyle.Visible = LeftLineStyle;
                }
                if (isLeftTopToRightBottomLineStyle)
                {
                    Cell.BorderStyle.LeftTopToRightBottomLineStyle.Visible = LeftTopToRightBottomLineStyle;
                }
                if (isRightLineStyle)
                {
                    Cell.BorderStyle.RightLineStyle.Visible = RightLineStyle;
                }
                if (isTopLineStyle)
                {
                    Cell.BorderStyle.TopLineStyle.Visible = TopLineStyle;
                }
                if (isBottomLineStyle)
                {
                    Cell.BorderStyle.BottomLineStyle.Visible = BottomLineStyle;
                }
            }
            return cmd;
        }
    }

    public class CellBackColorCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public Color Value { get; set; }

        public ICommand Execute()
        {
            CellBackColorCommand cmd=new CellBackColorCommand ();
            cmd.Value = Cell.BackColor;
            cmd.Cell = Cell;
            Cell.BackColor = Value;
            return cmd;
        }
    }
    public class CellMergeCommand : ICommand
    {
        public ICell CellBegin { get; set; }
        public ICell CellEnd { get; set; }
   
        public ICommand Execute()
        {
            if (CellBegin.OwnMergeCell != null)
            {
                CellMergeClearCommand cmd = new CellMergeClearCommand();
                CellBegin.Grid.UnMergeCell(CellBegin.OwnMergeCell);
                cmd.CellBegin = CellBegin;
                cmd.CellEnd = CellEnd;
                return cmd;
            }
            return null;
        }
    }
    public class CellMergeClearCommand : ICommand
    {
        public ICell CellBegin { get; set; }
        public ICell CellEnd { get; set; }

        public ICommand Execute()
        {
            if (CellBegin.OwnMergeCell != null)
            {
                CellMergeCommand cmd = new CellMergeCommand();
                CellBegin.Grid.MergeCell(CellBegin, CellEnd);
                cmd.CellBegin = CellBegin;
                cmd.CellEnd = CellEnd;
                return cmd;
            }
            return null;
        }
    }
    public class CellReadOnlyCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public virtual bool Value { get; set; }
        public virtual bool InheatReadOnly { get; set; }
        public ICommand Execute()
        {
            CellReadOnlyCommand cmd = new CellReadOnlyCommand();
            cmd.Value = Cell.ReadOnly;
            cmd.Cell = Cell;
            cmd.InheatReadOnly = Cell.InhertReadOnly;
            Cell.InhertReadOnly = InheatReadOnly;
            Cell.ReadOnly = Value;
            return cmd;
        }
    }

    public class CellFormatStringCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public string Value { get; set; }

        public ICommand Execute()
        {
            CellFormatStringCommand cmd = new CellFormatStringCommand();
            cmd.Value = Cell.FormatString;
            cmd.Cell = Cell;
            Cell.FormatString = Value;
            return cmd;
        }
    }

    public class CellFormatTypeCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public Feng.Utils.FormatType Value { get; set; }

        public ICommand Execute()
        {
            CellFormatTypeCommand cmd = new CellFormatTypeCommand();
            cmd.Value = Cell.FormatType;
            cmd.Cell = Cell;
            Cell.FormatType = Value;
            return cmd;
        }
    }
    public class CellInhertReadOnlyCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public virtual bool Value { get; set; }

        public ICommand Execute()
        {
            CellInhertReadOnlyCommand cmd = new CellInhertReadOnlyCommand();
            cmd.Value = Cell.InhertReadOnly;
            cmd.Cell = Cell;
            Cell.InhertReadOnly = Value;
            return cmd;
        }
    }

    public class CellFocusBackColorCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public Color Value { get; set; }

        public ICommand Execute()
        {
            CellFocusBackColorCommand cmd = new CellFocusBackColorCommand();
            cmd.Value = Cell.FocusBackColor;
            cmd.Cell = Cell;
            Cell.FocusBackColor = Value;
            return cmd;
        }
    }

    public class CellFocusForeColorCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public Color Value { get; set; }

        public ICommand Execute()
        {
            CellFocusForeColorCommand cmd = new CellFocusForeColorCommand();
            cmd.Value = Cell.FocusForeColor;
            cmd.Cell = Cell;
            Cell.FocusForeColor = Value;
            return cmd;
        }
    }


    public class CellMouseOverImageSizeModeCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public ImageLayout Value { get; set; }

        public ICommand Execute()
        {
            CellMouseOverImageSizeModeCommand cmd = new CellMouseOverImageSizeModeCommand();
            cmd.Value = Cell.MouseOverImageSizeMode;
            cmd.Cell = Cell;
            Cell.MouseOverImageSizeMode = Value;
            return cmd;
        }
    }


    public class CellMouseDownImageSizeModeCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public ImageLayout Value { get; set; }

        public ICommand Execute()
        {
            CellMouseDownImageSizeModeCommand cmd = new CellMouseDownImageSizeModeCommand();
            cmd.Value = Cell.MouseDownImageSizeMode;
            cmd.Cell = Cell;
            Cell.MouseDownImageSizeMode = Value;
            return cmd;
        }
    }


    public class CellReadOnlyImageSizeModeCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public ImageLayout Value { get; set; }

        public ICommand Execute()
        {
            CellReadOnlyImageSizeModeCommand cmd = new CellReadOnlyImageSizeModeCommand();
            cmd.Value = Cell.ReadOnlyImageSizeMode;
            cmd.Cell = Cell;
            Cell.ReadOnlyImageSizeMode = Value;
            return cmd;
        }
    }

    public class CellFocusImageSizeModeCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public ImageLayout Value { get; set; }

        public ICommand Execute()
        {
            CellFocusImageSizeModeCommand cmd = new CellFocusImageSizeModeCommand();
            cmd.Value = Cell.FocusImageSizeMode;
            cmd.Cell = Cell;
            Cell.FocusImageSizeMode = Value;
            return cmd;
        }
    }


    public class CellTabStopCommand : ICommand
    {
        public ICell Cell { get; set; }
        public virtual bool Value { get; set; }

        public ICommand Execute()
        {
            CellTabStopCommand cmd = new CellTabStopCommand();
            cmd.Value = Cell.TabStop;
            cmd.Cell = Cell;
            Cell.TabStop = Value;
            return cmd;
        }
    }


    public class CellTabIndexCommand : ICommand
    {
        public ICell Cell { get; set; }
        public int Value { get; set; }

        public ICommand Execute()
        {
            CellTabIndexCommand cmd = new CellTabIndexCommand();
            cmd.Value = Cell.TabIndex;
            cmd.Cell = Cell;
            Cell.TabIndex = Value;
            return cmd;
        }
    }


    public class CellHotKeyEnableCommand : ICommand
    {
        public ICell Cell { get; set; }
        public virtual bool Value { get; set; }

        public ICommand Execute()
        {
            CellHotKeyEnableCommand cmd = new CellHotKeyEnableCommand();
            cmd.Value = Cell.HotKeyEnable;
            cmd.Cell = Cell;
            Cell.HotKeyEnable = Value;
            return cmd;
        }
    }


    public class CellHotKeyDataCommand : ICommand
    {
        public ICell Cell { get; set; }
        public Keys Value { get; set; }

        public ICommand Execute()
        {
            CellHotKeyDataCommand cmd = new CellHotKeyDataCommand();
            cmd.Value = Cell.HotKeyData;
            cmd.Cell = Cell;
            Cell.HotKeyData = Value;
            return cmd;
        }
    }


    public class CellCommandIDCommand : ICommand
    {
        public ICell Cell { get; set; }
        public string Value { get; set; }

        public ICommand Execute()
        {
            CellCommandIDCommand cmd = new CellCommandIDCommand();
            cmd.Value = Cell.ID;
            cmd.Cell = Cell;
            Cell.ID = Value;
            return cmd;
        }
    }

    public class CellSelectBackColorCommand : ICommand
    {
        public ICell Cell { get; set; }
        public Color Value { get; set; }

        public ICommand Execute()
        {
            CellSelectBackColorCommand cmd = new CellSelectBackColorCommand();
            cmd.Value = Cell.SelectBackColor;
            cmd.Cell = Cell;
            Cell.SelectBackColor = Value;
            return cmd;
        }
    }


    //public class CellBorderAnchorCommand : ICommand
    //{
    //    public CellBorderStyle Instance { get; set; }
    //    public BorderAnchor Value { get; set; }

    //    public ICommand Execute()
    //    {
    //        CellBorderAnchorCommand cmd = new CellBorderAnchorCommand();
    //        cmd.Value = Instance.BorderAnchor;
    //        cmd.Instance = Instance;
    //        Instance.BorderAnchor = Value;
    //        return cmd;
    //    }
    //}

    public class CellSelectedCommand : ICommand
    {
        public ICell Cell { get; set; }
        public virtual bool Value { get; set; }

        public ICommand Execute()
        {
            CellSelectedCommand cmd = new CellSelectedCommand();
            cmd.Value = Cell.Selected;
            cmd.Cell = Cell;
            Cell.Selected = Value;
            return cmd;
        }
    }


    public class CellSelectForceColorCommand : ICommand
    {
        public ICell Cell { get; set; }
        public Color Value { get; set; }

        public ICommand Execute()
        {
            CellSelectForceColorCommand cmd = new CellSelectForceColorCommand();
            cmd.Value = Cell.SelectForceColor;
            cmd.Cell = Cell;
            Cell.SelectForceColor = Value;
            return cmd;
        }
    }


    public class CellSelectBorderColorCommand : ICommand
    {
        public ICell Cell { get; set; }
        public Color Value { get; set; }

        public ICommand Execute()
        {
            CellSelectBorderColorCommand cmd = new CellSelectBorderColorCommand();
            cmd.Value = Cell.SelectBorderColor;
            cmd.Cell = Cell;
            Cell.SelectBorderColor = Value;
            return cmd;
        }
    }


    public class CellHorizontalAlignmentCommand : ICommand
    {
        public ICell Cell { get; set; }
        public StringAlignment Value { get; set; }

        public ICommand Execute()
        {
            CellHorizontalAlignmentCommand cmd = new CellHorizontalAlignmentCommand();
            cmd.Value = Cell.HorizontalAlignment;
            cmd.Cell = Cell;
            Cell.HorizontalAlignment = Value;
            return cmd;
        }
    }

    public class CellVerticalAlignmentCommand : ICommand
    {
        public ICell Cell { get; set; }
        public StringAlignment Value { get; set; }

        public ICommand Execute()
        {
            CellVerticalAlignmentCommand cmd = new CellVerticalAlignmentCommand();
            cmd.Value = Cell.VerticalAlignment;
            cmd.Cell = Cell;
            Cell.VerticalAlignment = Value;
            return cmd;
        }
    }

    public class CellDisplayMemberCommand : ICommand
    {
        public ICell Cell { get; set; }
        public string Value { get; set; }

        public ICommand Execute()
        {
            CellDisplayMemberCommand cmd = new CellDisplayMemberCommand();
            cmd.Value = Cell.DisplayMember;
            cmd.Cell = Cell;
            Cell.DisplayMember = Value;
            return cmd;
        }
    }

    public class CellValueMemberCommand : ICommand
    {
        public ICell Cell { get; set; }
        public string Value { get; set; }

        public ICommand Execute()
        {
            CellValueMemberCommand cmd = new CellValueMemberCommand();
            cmd.Value = Cell.ValueMember;
            cmd.Cell = Cell;
            Cell.ValueMember = Value;
            return cmd;
        }
    }
 

    public class CellMouseOverImageCommand : ICommand
    {
        public ICell Cell { get; set; }
        public Bitmap Value { get; set; }

        public ICommand Execute()
        {
            CellMouseOverImageCommand cmd = new CellMouseOverImageCommand();
            cmd.Value = Cell.MouseOverImage;
            cmd.Cell = Cell;
            Cell.MouseOverImage = Value;
            return cmd;
        }
    }

    public class CellDirectionVerticalCommand : ICommand
    {
        public ICell Cell { get; set; }
        public virtual bool Value { get; set; }

        public ICommand Execute()
        {
            CellDirectionVerticalCommand cmd = new CellDirectionVerticalCommand();
            cmd.Value = Cell.DirectionVertical;
            cmd.Cell = Cell;
            Cell.DirectionVertical = Value;
            return cmd;
        }
    }

    public class CellBackImgeSizeModeCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public ImageLayout Value { get; set; }

        public ICommand Execute()
        {
            CellBackImgeSizeModeCommand cmd = new CellBackImgeSizeModeCommand();
            cmd.Value = Cell.BackImgeSizeMode;
            cmd.Cell = Cell;
            Cell.BackImgeSizeMode = Value;
            return cmd;
        }
    }

    public class CellDisableImageSizeModeCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public ImageLayout Value { get; set; }

        public ICommand Execute()
        {
            CellDisableImageSizeModeCommand cmd = new CellDisableImageSizeModeCommand();
            cmd.Value = Cell.DisableImageSizeMode;
            cmd.Cell = Cell;
            Cell.DisableImageSizeMode = Value;
            return cmd;
        }
    }

    public class CellBackImageCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public Bitmap Value { get; set; }
        public ICommand Execute()
        {
            CellBackImageCommand cmd = new CellBackImageCommand();
            cmd.Value = Cell.BackImage;
            cmd.Cell = Cell;
            Cell.BackImage = Value;
            return cmd;
        }
    }

    public class CellDisableForeColorCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public Color Value { get; set; }
        public ICommand Execute()
        {
            CellDisableForeColorCommand cmd = new CellDisableForeColorCommand();
            cmd.Value = Cell.ForeColor;
            cmd.Cell = Cell;
            Cell.ForeColor = Value;
            return cmd;
        }

 
    }

    public class CellDisableBackColorCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public Color Value { get; set; }
        public ICommand Execute()
        {
            CellDisableBackColorCommand cmd = new CellDisableBackColorCommand();
            cmd.Value = Cell.BackColor;
            cmd.Cell = Cell;
            Cell.BackColor = Value;
            return cmd;
        }
 
    }

    public class CellReadOnlyForeColorCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public Color Value { get; set; }
        public ICommand Execute()
        {
            CellReadOnlyForeColorCommand cmd = new CellReadOnlyForeColorCommand();
            cmd.Value = Cell.ForeColor;
            cmd.Cell = Cell;
            Cell.ForeColor = Value;
            return cmd;
        }
 
    }

    public class CellReadOnlyBackColorCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public Color Value { get; set; }
        public ICommand Execute()
        {
            CellReadOnlyBackColorCommand cmd = new CellReadOnlyBackColorCommand();
            cmd.Value = Cell.BackColor;
            cmd.Cell = Cell;
            Cell.BackColor = Value;
            return cmd;
        }
 
    }

    public class CellMouseOverForeColorCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public Color Value { get; set; }
        public ICommand Execute()
        {
            CellMouseOverForeColorCommand cmd = new CellMouseOverForeColorCommand();
            cmd.Value = Cell.MouseOverForeColor;
            cmd.Cell = Cell;
            Cell.MouseOverForeColor = Value;
            return cmd;
        }
 
    }

    public class CellMouseOverBackColorCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public Color Value { get; set; }
        public ICommand Execute()
        {
            CellMouseOverBackColorCommand cmd = new CellMouseOverBackColorCommand();
            cmd.Value = Cell.MouseOverBackColor;
            cmd.Cell = Cell;
            Cell.MouseOverBackColor = Value;
            return cmd;
        }
 
    }

    public class CellMouseDownForeColorCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public Color Value { get; set; }
        public ICommand Execute()
        {
            CellMouseDownForeColorCommand cmd = new CellMouseDownForeColorCommand();
            cmd.Value = Cell.MouseDownForeColor;
            cmd.Cell = Cell;
            Cell.MouseDownForeColor = Value;
            return cmd;
        }
 
    }

    public class CellMouseDownBackColorCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public Color Value { get; set; }
        public ICommand Execute()
        {
            CellMouseDownBackColorCommand cmd = new CellMouseDownBackColorCommand();
            cmd.Value = Cell.MouseDownBackColor;
            cmd.Cell = Cell;
            Cell.MouseDownBackColor = Value;
            return cmd;
        }
 
    }

    public class CellForeColorCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public Color Value { get; set; }
        public ICommand Execute()
        {
            CellForeColorCommand cmd = new CellForeColorCommand();
            cmd.Value = Cell.ForeColor;
            cmd.Cell = Cell;
            Cell.ForeColor = Value;
            return cmd;
        }
 
    }

    public class CellValueCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public object Value { get; set; }
        public string Text { get; set; }
        public ICommand Execute()
        {
            CellValueCommand cmd = new CellValueCommand();
            cmd.Value = Cell.Value;
            cmd.Text = Cell.Text;
            cmd.Cell = Cell;
            Cell.Value = Value;
            Cell.Text = Text;
            return cmd;
        }
 
    }


    public class CellFontCommand : ICommand
    {
        public IBaseCell Cell { get; set; }
        public Font Value { get; set; } 
        public ICommand Execute()
        {
            CellValueCommand cmd = new CellValueCommand();
            cmd.Value = Cell.Font;
            cmd.Text = this.Cell.Text;
            cmd.Cell = Cell;
            Cell.Font = Value; 
            return cmd;
        }

    }

    public class RowHeightCommand : ICommand
    {
        public IRow Row { get; set; }
        public int Value { get; set; }

        public ICommand Execute()
        {
            RowHeightCommand cmd = new RowHeightCommand();
            cmd.Value = Row.Height;
            cmd.Row = Row;
            Row.Height = Value;
            return cmd;
        }
 
    }

    public class ColumnWidthCommand : ICommand
    {
        public IColumn Column { get; set; }
        public int Value { get; set; }

        public ICommand Execute()
        {
            ColumnWidthCommand cmd = new ColumnWidthCommand();
            cmd.Value = Column.Width;
            cmd.Column = Column;
            Column.Width = Value;
            return cmd;
        }
 
    }

    public class CommandChangedCommand : ICommand
    {
        private static int index = 0;
        public CommandChangedCommand()
        {
            index++;
            Index = index;
            Feng.Utils.TraceHelper.WriteTrace("Command", "", "保存文件", true, Index.ToString());
        }
        public byte[] Value { get; set; }
        public DataExcel Grid { get; set; }
        public int Index { get; set; }
        public ICommand Execute()
        {
            CommandChangedCommand cmd = new CommandChangedCommand();
            cmd.Value = Grid.GetFileData();
            cmd.Grid = Grid;
            cmd.Index = Index;
            Grid.Open(Value);
            Feng.Utils.TraceHelper.WriteTrace("Command", "CommandChangedCommand", "ReDo", true, Index.ToString());

            return cmd;
        }

    }
}
