using Feng.Data;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Interface;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Forms.Views
{


    public enum DivSizeWidthMode
    {
        /// <summary>
        /// View.SizeWidthMode属性值，固定宽度
        /// </summary>
        Fix = 0,
        /// <summary>
        /// View.SizeWidthMode属性值，子类继承，自动计算宽度
        /// </summary>
        Customize = 1,
        /// <summary>
        /// View.SizeWidthMode属性值，this.Width - view.MarginSizeRight - view.MarginSizeLeft
        /// 父级的宽度减去设置的MarginSizeRight值和MarginSizeLeft的值
        /// </summary>
        Fill = 5,
        Margin = 6,
        EqualHeight = 7,
        PrevWidth = 8,
        ChildWidth = 9,
    }
    public enum DivSizeHeightMode
    {
        /// <summary>
        /// View.SizeWidthMode属性值，固定宽度
        /// </summary>
        Fix = 0,
        /// <summary>
        /// View.SizeWidthMode属性值，子类继承，自动计算宽度
        /// </summary>
        Customize = 1,
        /// <summary>
        /// View.SizeWidthMode属性值，this.Width - view.MarginSizeRight - view.MarginSizeLeft
        /// 父级的宽度减去设置的MarginSizeRight值和MarginSizeLeft的值
        /// </summary>
        Fill = 5,
        Margin = 6,
        EqualWidth = 7,
        PrevHeight = 8,
        ChildHeight = 9,
    }
    public enum DivLaoutChildMode
    { 
        VerticalTopBoBottom = 2,
        HorizontalLeftToRight = 3,
    }
 
    public enum DivLaoutVerticalMode
    {
        Fix = 0,
        PadddingTop = 2,
        PadddingBottom = 3,
        PadddingPrev = 5,
        Center = 7,
    }
    public enum DivLaoutHorizontalMode
    {
        Fix = 0,
        PadddingLeft = 2,
        PadddingRight = 3,
        PadddingPrev = 5,
        Center = 7,
    }

}

