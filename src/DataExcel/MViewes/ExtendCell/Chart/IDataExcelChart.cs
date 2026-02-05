using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Feng.Forms.Interface;
using Feng.Enums;
using Feng.Excel.Interfaces;
using Feng.Excel.Styles;

namespace Feng.Excel.Chart
{
    public interface IChart
    {
        IDataExcelChart Chart { get; }
    }

    public interface IISeriesValues
    {
        ISeriesValueCollection SeriesValues { get; set; }
    }

    public interface ISeries : IFont, IForeColor, IBackColor, IVisible, IGrid, IDraw, IChart
        , ICaption, IISeriesValues
    {
        int MinorCount { get; set; }
        int GridSpacing { get; set; }
    }
    public interface IBorder
    {
        LineStyle Border { get; set; }
    }
    public interface IDock
    {
        System.Windows.Forms.DockStyle Dock { get; set; }
    }
    public interface ITtitle
    {
        string Text { get; set; }
    }
    public interface IChartTitle : ITtitle, IFont, IForeColor, IVisible, IGrid, IDraw, IChart, IDock
    {
        Alignment Alignment { get; set; }
        bool Antialiasing { get; set; }
    }

    public interface ILegend : IDraw, IGrid, IForeColor, IBorderColor, IBackColor, IBackImage,
         IChart, IVisible, IBorder, IBounds, IMargin
    {
        
    }

    public interface IMargin
    {
        System.Windows.Forms.Padding Margin { get; set; }
    }

    public interface IAxis : IVisible, IColor, IDraw, IChart, IFont, IForeColor, IBackColor, IBounds 
    {
        int SmallLine { get; set; }
        int LargeLine { get; set; }
        LineStyle Border1 { get; set; }
        LineStyle Border2 { get; set; }
        LineStyle Border3 { get; set; }
        int MinorCount { get; set; }
        int GridSpacing { get; set; }
        bool GridSpacingAuto { get; set; }
        bool Reverse { get; set; }
        bool Interlaced { get; set; }
        Color InterlacedColor { get; set; }
        Alignment Alignment { get; set; }
        
    }
 
    public interface IAxisX : IAxis
    {
        object MaxValue { get; set; }
        object MinValue { get; set; }
        int GetHeight(object value);
    }

    public interface IAxisY : IAxis
    {
        AxisYValueType ValueType { get; set; }

        List<ISeriesValue> SeriesValues { get; set; }

    }

    public enum AxisYValueType
    {
        Number,
        DataTime,
    }

    public interface IDataExcelChart : IGrid, IDraw, IBinding, IDisplayMember, IValueMember, IBackImage, IBackColor,
        IFont, IExtendCell, IPosition 
    {
        ILegend Legend { get; set; }
        ITitleCollection LeftTitles { get; set; }
        ITitleCollection RightTitles { get; set; }
        ITitleCollection TopTitles { get; set; }
        ITitleCollection BottomTitles { get; set; }

        string PaletteName { get; set; }
        int PaletteBaseColorNumber { get; set; }
        IAxisY AxisY { get; set; }
        IAxisX AxisX { get; set; }
        string SeriesName { get; set; }
        ISeriesCollection Series { get; set; }
    }
  
}
