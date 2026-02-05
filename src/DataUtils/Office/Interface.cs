using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Enums
{

    public enum ThreeMode
    {
        Null,
        True,
        Flase
    }

    public enum Alignment
    {
        Far,
        Near,
        Center,
        Default
    }

    [Flags()]
    public enum enumDrawRectLineMode
    {
        CenterHorzLine = 1,
        CenterVerzLine = 2,
        TopLeftToBoomRight = 4,
        BoomLeftToTopRight = 8
    }

    public enum Purview
    {
        /// <summary>
        /// Permissions权限在此之上
        /// </summary>
        Above,
        /// <summary>
        /// Permissions权限大于等于
        /// </summary>
        AboveAndEqual,
        /// <summary>
        /// Permissions权限等于
        /// </summary>
        Equal,
        /// <summary>
        /// Permissions权限底于等于
        /// </summary>
        BelowAndEqual,
        /// <summary>
        /// Permissions权限底于
        /// </summary>
        Below,
    }

    public enum HeaderMode
    {
        RowHeader,
        ColumnHeader,
        RowFooter,
        ColumnFooter

    }

    public enum CellMouseState
    {
        Null,
        Down,
        Over
    }

    public enum CheckStates
    {
        Inhert,
        Yes,
        No
    }

    public enum DataExcelDesignMode
    {
        Design,
        Run
    }

    public enum SizeChangMode
    {
        Null,
        TopLeft,
        TopRight,
        MidLeft,
        MidRight,
        BoomLeft,
        BoomRight,
        MidTop,
        MidBoom
    }

    public enum StateMode
    {
        NULL=0,
        MOVE=1,

    }

    public enum CellType
    {
        Default,
        Label,
        Numberic,
        Text,
        Button,
        Radio,
        Combox,
        CheckBox,
        DateTime,
        PopupForm
    }

    public enum NextCellType
    {
        Left,
        Up,
        Right,
        Down
    }

    public enum CellEditType
    {

    }



    [Flags]
    public enum EditMode
    {
        NULL = 0,
        NONE = 1,
        IME = 2,
        F2 = 4,
        DoubleClick = 8,
        Default = NULL | NONE | IME | F2 | DoubleClick,
        Focused = 32,
        KeyDown = 64,
        Click = 128,
        MouseEnter = 256,
        ALL = IME | F2 | DoubleClick | Focused | KeyDown | Click 
    }

}
