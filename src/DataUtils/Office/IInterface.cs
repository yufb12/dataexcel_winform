using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

using Feng.Enums;

namespace Feng.Forms.Interface
{
    public interface IPadding
    {
        Padding Padding { get; set; }
    }

    public interface IMargins
    {
        Margins Margins { get; set; }
    }

    public interface IWndProc
    {
        void WndProc(ref Message m, ref bool Handled);
    }

    public interface ICommandID
    {
        string ID { get; set; }
    }

    public interface IVerticalAlignment
    {
        StringAlignment VerticalAlignment { get; set; }
    }

    public interface IHorizontalAlignment
    {
        StringAlignment HorizontalAlignment { get; set; }
    }

    public interface IAlignment : IVerticalAlignment, IHorizontalAlignment
    {

    }

    #region 基础接口

    public interface ITextDirection
    {
        bool DirectionVertical { get; set; }
    }

    public interface IBinding : IDataSource, IDataMember
    {

    }
    public interface IDataPosition
    {
        int Position { get; set; }
    }

    public interface IDataSource
    {
        object DataSource { get; set; }
    }

    public interface IDataMember
    {
        string DataMember { get; set; }
    }

    public interface IClose
    {
        void Close();
    }
    public interface IContensWidth
    {
        int ContensWidth { get; set; }
    }

    public interface IContensHeigth
    {
        int ContensHeigth { get; set; }
    }

    public interface IFrozen
    {
        bool Frozen { get; set; }
    }
    public interface ITabStop
    {
        bool TabStop { get; set; }
    }
    public interface ITabIndex
    {
        int TabIndex { get; set; }
    }

    public interface IText3
    {
        string Text1 { get; set; }
        string Text2 { get; set; }
        string Text3 { get; set; }
    }

    public interface ITab : ITabStop, ITabIndex
    {

    }
    public interface ITable: ITableName, ITableColumnName, ITableRowIndex
    {

    }
    public interface ITableName
    {
        string TableName { get; set; }
    }

    public interface ITableColumnName
    {
        string TableColumnName { get; set; }
    }
 
    public interface ITableRowIndex
    {
        int TableRowIndex { get; set; }
    }

    public interface IHotKeyEnable
    {
        bool HotKeyEnable { get; set; }
    }

    public interface IHotKeyData
    {
        Keys HotKeyData { get; set; }
    }


    public interface IHotKey : IHotKeyEnable, IHotKeyData
    {

    }
    public interface IDisplayMember
    {
        string DisplayMember { get; set; }
    }
    public interface IRemark
    {
        string Remark { get; set; }
    }

    public interface ICreateTime
    {
        DateTime CreateTime { get; set; }
    }
    public interface IExtend
    {
        string Extend { get; set; }
    }
    public interface IValueMember
    {
        string ValueMember { get; set; }
    }

    public interface IBindingItem
    {
        string BindingItem { get; set; }
    }

    public interface IDrawBorder
    {
        void DrawBorder(Feng.Drawing.GraphicsObject g);
    }

 
    public interface IInEdit
    {
        bool InEdit { get; }
    }

    public interface IDeafultFormat
    {
        string DeafultFormat { get; set; }
    }



    public interface IAutoMultiline
    {
        bool AutoMultiline { get; set; }
    }

    public interface ILineColor
    {
        Color LineColor { get; set; }
    }
    public interface IField
    {
        string FieldName { get; set; }
    }

    public interface ISelected
    {
        bool Selected { get; set; }
    }

    public interface IUrl
    {
        string Url { get; set; }
    }
    public interface IEditMode
    {
        EditMode EditMode { get; set; }
    }
    public interface ISelectColor
    {
        Color SelectBackColor { get; set; }
        Color SelectForceColor { get; set; }
    }




    public interface IClipRectangle
    {
        Rectangle ClipRectangle { get; }
    }

    public interface IBorderWidth
    {
        float BorderWidth { get; set; }
    }


    public interface IBorderColor
    {
        Color BorderColor { get; set; }
    }

    public interface IBackImage
    {
        Bitmap BackImage { get; set; }
        ImageLayout BackImgeSizeMode { get; set; }
    }

    public interface IShowSelectBorder
    {
        bool ShowFocusedSelectBorder { get; set; }
        Color SelectBorderColor { get; set; }
    }
    public interface IAddrangle<T>
    {
        void AddRange(params T[] ts);
    }

    public interface ISelectIndex
    {
        int SelectIndex { get; set; }
    }
 

    public interface IIsHeader
    {
        bool IsHeader { get; set; }
    }

    public interface ITrueString
    {
        string TrueString { get; set; }
    }
    public interface IIsFooter
    {
        bool IsFooter { get; set; }
    }
    public interface IPosition
    {
        int Position { get; set; }
    }

    public interface IDiaplayIndex
    {
        int DisplayIndex { get; }
    }

    public interface ICheckOnClick
    {
        bool CheckOnClick { get; set; }
    }

    public interface IPopupEdit
    {
        void ShowPopup();
        void HidePopup();
        void OnOK(object value, object model);
        void OnCancel();
    }
    public interface IShowDialog
    {
        DialogResult ShowDialog();
    }
    public interface IOpenData
    {
        void Open(byte[] data);

    }
    public interface IDesignData
    {
        byte[] GetData();
    }
    public interface IInitDesgin
    {
        void InitDesgin();
    }
    public interface IDesignForm: IShowDialog, IOpenData, IDesignData, IInitDesgin
    {
        IDesignForm New();
    }
    public interface IVisibleCount
    {
        int VisibleCount { get; set; }
    }

    public interface INext
    {
        void ProvPage();
        void Prov();
        void NextPage();
        void Next();
        void Home();
        void End();
    }
    public interface IChange : ISmallChange, ILargeChange
    {

    }

    public interface ISmallChange
    {
        int SmallChange { get; set; }
    }
    public interface ILargeChange
    {
        int LargeChange { get; set; }
    }





    public interface IIndex
    {
        int Index { get; set; }
    }

    public interface IRect
    {
        int Left { get; set; }
        int Height { get; set; }
        int Right { get; }
        int Bottom { get; }
        int Top { get; set; }
        int Width { get; set; }
        Rectangle Rect { get; }
    }
    public interface IPointToClient
    {
        Point PointToClient(Point pt);
    }
    public interface IPointToScreen
    {
        Point PointToScreen(Point pt);
    }
    public interface IPointToControl
    {
        Point PointToControl(Point pt);
    }
    public interface IBounds : IRect
    {
        //int Left { get; set; }
        //int Height { get; set; }
        //int Right { get; }
        //int Bottom { get; }
        //int Top { get; set; }
        //int Width { get; set; }
        //Rectangle Rect { get; }
    }
    public interface ITitle
    {
        string Title { get; set; }
    }
    public interface IDraw
    {
        bool OnDraw(object sender, Feng.Drawing.GraphicsObject g);
    }

    public interface IDrawBack
    {
        bool OnDrawBack(object sender, Feng.Drawing.GraphicsObject g);
    }
    public interface IOnDrawBack
    {
        bool OnDrawBack(object sender, Graphics g);
    }

    public interface IClear
    {
        void Clear();
    }

    public interface IFont
    {
        Font Font { get; set; }
    }

    public interface IMouseDown
    {
        bool MouseDown(object sender, MouseEventArgs e);
    }
    public interface IMouseUp
    {
        void MouseUp(object sender, MouseEventArgs e);
    }
    public interface ICount
    {
        int Count { get; }
    }

    public interface IBackColor
    {
        Color BackColor { get; set; }
    }

 
    public interface ILineWidth
    {
        float LineWidth { get; set; }
    }
    public interface IForeColor
    {
        Color ForeColor { get; set; }
    }

    public interface IControlColor : IBackColor, IForeColor
    {


    }

    public interface IVersion
    {
        string Version { get; } 
    }

    public interface IAssembly
    {
        string DllName { get; }
    }

    public interface IDownLoadUrl
    {
        string DownLoadUrl { get; }
    }

    public interface IPlusAssembly : IVersion, IAssembly, IDownLoadUrl, IBase
    {

    }

    public interface IBase
    {

    }
    //public interface IScroller : IScrollerBase, IGrid
    //{
    //}

    public interface IInt32Value
    {
        int Position { get; set; }
    }

    public interface IReadOnlyMin
    {
        int Min { get; }
    }
    public interface IMin
    {
        int Min { get; set; }
    }
    public interface IMaxMin : IMax, IMin
    {

    }
    public interface IReadOnlyMax
    {
        int Max { get; }
    }
    public interface IMax
    {
        int Max { get; set; }

    }
    public interface IMouseOverImage
    {
        Bitmap MouseOverImage { get; set; }
        ImageLayout MouseOverImageSizeMode { get; set; }
    }

    public interface IMouseDownImage
    {
        Bitmap MouseDownImage { get; set; }
        ImageLayout MouseDownImageSizeMode { get; set; }
    }

    public interface IDisableImage
    {
        Bitmap DisableImage { get; set; }
        ImageLayout DisableImageSizeMode { get; set; }
    }

    public interface IReadOnlyImage
    {
        Bitmap ReadOnlyImage { get; set; }
        ImageLayout ReadOnlyImageSizeMode { get; set; }
    }

    public interface IFocusImage
    {
        Bitmap FocusImage { get; set; }
        ImageLayout FocusImageSizeMode { get; set; }
    }

    public interface IMouseOverBackColor
    {
        Color MouseOverBackColor { get; set; }
    }

    public interface IMouseDownBackColor
    {
        Color MouseDownBackColor { get; set; }
    }



    public interface IFocusBackColor
    {
        Color FocusBackColor { get; set; }
    }

    public interface IMouseOverForeColor
    {
        Color MouseOverForeColor { get; set; }
    }

    public interface IMouseDownForeColor
    {
        Color MouseDownForeColor { get; set; }
    }


    public interface IFocusForeColor
    {
        Color FocusForeColor { get; set; }
    }
    public interface IMouseUpImage
    {
        Bitmap MouseUpImage { get; set; }
        ImageLayout MouseUpImageSizeMode { get; set; }
    }
    public interface IMouseUpBackColor
    {
        Color MouseUpBackColor { get; set; }
    }
    public interface IMouseUpForeColor
    {
        Color MouseUpForeColor { get; set; }
    }
    public interface IEventImageColorSet :
            IMouseOverImage,     IMouseDownImage,     IFocusImage     , IMouseUpImage
        ,   IMouseOverBackColor, IMouseDownBackColor, IFocusBackColor , IMouseUpBackColor
        ,   IMouseOverForeColor, IMouseDownForeColor, IFocusForeColor , IMouseUpForeColor
        ,   IDisableImage, IReadOnlyImage
    {

    }

    public interface IAutoRunExceute
    {
        void Exceute();
    }

    public interface IID
    {
        int ID { get; set; }

    }
    public interface ITextID
    {
        string ID { get; set; }

    }
    public interface IBoolValue: ITrueValue, IFalseValue
    {

    }
    public interface ITrueValue
    {
        string TrueValue { get; set; }

    }
    public interface IFalseValue
    {
        string FalseValue { get; set; }

    }

    public interface IAllowAdd
    {
        bool AllowAdd { get; set; }
    }

    public interface IAllowDelete
    {
        bool AllowDelete { get; set; }
    }

    public interface IAllowEdit
    {
        bool AllowEdit { get; set; }
    }


    public interface IScrollerBase : IDraw, IFont, IBounds, IVisible,
IInt32Value, IMaxMin, IClear, IChange, INext, IVisibleCount
    {
        event Feng.EventHelper.BeforePositionChangedEventHandler BeforeValueChanged;
        event Feng.EventHelper.ValueChangedEventHandler ValueChanged;
        event Feng.EventHelper.ClickEventHandler Click;
        event Feng.EventHelper.ClickEventHandler ThumdAreaClick;
        event Feng.EventHelper.ClickEventHandler DownArrowAreaClick;
        event Feng.EventHelper.ClickEventHandler UpArrowAreaClick;
    }
    #endregion
}
