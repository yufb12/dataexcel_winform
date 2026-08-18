using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace TianZiGeGenerator
{
    #region 枚举定义

    /// <summary>田格类型枚举</summary>
    public enum GridType
    {
        /// <summary>田字格</summary>
        TianZi,
        /// <summary>米字格</summary>
        MiZi,
        /// <summary>回宫格</summary>
        HuiGong,
        /// <summary>方格</summary>
        FangGe
    }

    /// <summary>汉字排列模式</summary>
    public enum CharMode
    {
        /// <summary>全部描红</summary>
        AllTrace,
        /// <summary>描红加空白行交替</summary>
        TraceWithBlank
    }

    /// <summary>页面方向</summary>
    public enum PageOrientation
    {
        /// <summary>竖版</summary>
        Portrait,
        /// <summary>横版</summary>
        Landscape
    }

    /// <summary>纸张类型</summary>
    public enum PaperSizeType
    {
        /// <summary>A4</summary>
        A4,
        /// <summary>Letter</summary>
        Letter
    }

    #endregion

 
}
