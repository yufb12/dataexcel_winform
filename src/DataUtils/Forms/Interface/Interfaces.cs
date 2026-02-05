using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D; 
using Feng.Forms.Controls.Designer;
using Feng.Data;
using Feng.Forms.Base;
using Feng.Forms.Views;

namespace Feng.Forms.Interface
{

    public interface IShow
    {
        void Show();
    }
    public interface IReadData
    {
        void Read(DataStruct data);
    }

    public interface IDataStruct
    {
        DataStruct Data { get; } 
    }
    public interface IGetDataStruct
    {
        DataStruct GetDataStruct();
    }
    public interface IDataBoundItem
    {
        object DataBoundItem { get; set; }
    }
    public interface IChecked
    {
        bool Checked { get; set; }
    }
    public interface IShowCheck
    {
        bool ShowCheckBox { get; set; }
    }
    public interface IInitEdit
    {
        bool InitEdit(object obj);
    }

    public interface IEndEdit
    {
        void EndEdit();
    }
    public interface ILocation
    {
        Point Location { get; }
    }
    public interface ISortIndex
    {
        int SortIndex { get; set; }
    }
    public interface IIOFileData: IGetFileData, IOpenFileData
    {

    }
    public interface IGetFileData
    {
        byte[] GetFileData();
    }
    public interface IOpenFileData
    {
        void OpenFileData(byte[] data);
    }


    public interface IViewID
    {
        string ViewID { get; set; }
    }

    public interface IInit
    {
        void Init();
    }
    public interface IFieldName
    {
        string FieldName { get; set; }
    }
    public interface ICaption
    {
        string Caption { get; set; }
    }
    public interface IName
    {
        string Name { get; set; }
    }
    public interface IParamsText
    {
        string[] Args { get; set; }
    }
    public interface IParams
    {
        object[] Args { get; set; }
    }
    public interface IValue
    {
        object Value { get; set; }
    }
    public interface IText
    {
        string Text { get; set; }
    }
    public interface IVisible
    {
        bool Visible { get; set; }
    }

    public interface IInhertReadOnly
    {
        bool InhertReadOnly { get; set; }
    }

    public interface IReadOnly
    {
        bool ReadOnly { get; set; }
    }
    public interface ITag
    {
        object Tag { get; set; }
    }

    public interface ILocked
    {
        bool Locked { get; set; }
    }
    public interface IAllowInputExpress
    {
        YesNoInhert AllowInputExpress { get; set; }
    }
    public interface IToolTip
    {
        string ToolTip { get; set; }
    }
    public interface IAutoWidth
    {
        bool AutoWidth { get; set; }
    }

    public interface IAutoHeight
    {
        bool AutoHeight { get; set; }
    }
    public interface IAutoSize : IAutoWidth, IAutoHeight
    {

    }

    public interface IFormat
    {
        Feng.Utils.FormatType FormatType { get; set; }
        string FormatString { get; set; }
    }
    public interface IInitGridCellEdit
    {
        void Init(Feng.Forms.Controls.GridControl.GridViewCell cell);
    }
    public interface IEditCell
    {
        Feng.Forms.Controls.GridControl.GridViewCell Parent { get; }
    }

    public interface IDrawCell
    {
        bool DrawCell(object sender, Feng.Drawing.GraphicsObject g,Rectangle rect,object value);
    }

    public interface IDrawBackCell
    {
        bool DrawBackCell(object sender, Feng.Drawing.GraphicsObject g, Rectangle rect, object value);
    }
    public interface IDrawBooder
    {
        bool DrawBackCell(object sender, Feng.Drawing.GraphicsObject g, Rectangle rect, object value);
    }


    public interface IPrintCell
    {
        bool PrintCell(object sender, Feng.Drawing.GraphicsObject g, Rectangle rect, object value);
    }

    public interface IPrintBackCell
    {
        bool PrintBackCell(object sender, Feng.Drawing.GraphicsObject g, Rectangle rect, object value);
    }

    public interface IOnMouseDown
    {
        bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve);
    }

    public interface IOnMouseUp
    {
        bool OnMouseUp(object sender, MouseEventArgs e, EventViewArgs ve);
    }
    public interface IOnMouseMove
    {
        bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve);
    }
    public interface IOnMouseLeave
    {
        bool OnMouseLeave(object sender, EventArgs e, EventViewArgs ve);
    }

    public interface IOnMouseHover
    {
        bool OnMouseHover(object sender, EventArgs e, EventViewArgs ve);
    }
    public interface IOnMouseEnter
    {
        bool OnMouseEnter(object sender, EventArgs e, EventViewArgs ve);
    }
    public interface IOnMouseDoubleClick
    {
        bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve);
    }
    public interface IOnMouseClick
    {
        bool OnMouseClick(object sender, MouseEventArgs e, EventViewArgs ve);
    }

    public interface IOnMouseCaptureChanged
    {
        bool OnMouseCaptureChanged(object sender, EventArgs e, EventViewArgs ve);
    }
    public interface IOnMouseWheel
    {
        bool OnMouseWheel(object sender, MouseEventArgs e, EventViewArgs ve);
    }
    public interface IOnClick
    {
        bool OnClick(object sender, EventArgs e, EventViewArgs ve);
    }
    public interface IOnKeyDown
    {
        bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve);
    }
    public interface IOnKeyPress
    {
        bool OnKeyPress(object sender, KeyPressEventArgs e, EventViewArgs ve);
    }
    public interface IOnKeyUp
    {
        bool OnKeyUp(object sender, KeyEventArgs e, EventViewArgs ve);
    }
    public interface IOnPreviewKeyDown
    {
        bool OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e, EventViewArgs ve);
    }
    public interface IOnDoubleClick
    {
        bool OnDoubleClick(object sender, EventArgs e, EventViewArgs ve);
    }
    public interface IOnPreProcessMessage
    {
        bool OnPreProcessMessage(object sender, ref Message msg, EventViewArgs ve);
    }
    public interface IOnProcessCmdKey
    {
        bool OnProcessCmdKey(object sender, ref Message msg, Keys keyData, EventViewArgs ve);
    }
    public interface IOnProcessDialogChar
    {
        bool OnProcessDialogChar(object sender, char charCode, EventViewArgs ve);
    }
    public interface IOnProcessDialogKey
    {
        bool OnProcessDialogKey(object sender, Keys keyData, EventViewArgs ve);
    }
    public interface IOnProcessKeyEventArgs
    {
        bool OnProcessKeyEventArgs(object sender, ref Message m, EventViewArgs ve);
    }
    public interface IOnProcessKeyMessage
    {
        bool OnProcessKeyMessage(object sender, ref Message m, EventViewArgs ve);
    }

    public interface IOnProcessKeyPreview
    {
        bool OnProcessKeyPreview(object sender, ref Message m, EventViewArgs ve);
    }

    public interface IOnWndProc
    {
        bool OnWndProc(object sender, ref Message m, EventViewArgs ve);
    }

    public interface IViewEvent: IOnMouseDown, IOnMouseUp, IOnMouseMove, IOnMouseLeave, IOnMouseHover
        , IOnMouseEnter, IOnMouseDoubleClick, IOnMouseClick, IOnMouseCaptureChanged, IOnMouseWheel
        , IOnClick, IOnKeyDown, IOnKeyPress, IOnKeyUp, IOnPreviewKeyDown, IOnDoubleClick
        , IOnPreProcessMessage, IOnProcessCmdKey, IOnProcessDialogChar, IOnProcessDialogKey
        , IOnProcessKeyEventArgs, IOnProcessKeyMessage, IOnProcessKeyPreview, IOnWndProc

    {           
          
         
    }


    public interface IFindControl
    {
        System.Windows.Forms.Control FindControl();
    }
    public interface IEditView : IInitEdit, IEndEdit, IDataStruct
    , IDrawCell, IDrawBackCell, IPrintCell, IPrintBackCell
    {

    }



    public interface IEditControl : IInitGridCellEdit, IEndEdit, IEditCell, IDataStruct
        
    { 

    
    }



    public interface IShortName
    {
        string ShortName { get; set; }
    }

    public interface IDescript
    {
        string Descript { get; set; }
    }

    public interface IPropertyAction : IDescript, IShortName
    {
        string ActionName { get; set; }
        string Script { get; set; }
    }
    public interface ITitleImage
    {
        Image Image { get; set; }
    }

    public interface ITextRect : IText, IForeColor, IBackColor,
   IHorizontalAlignment, IVerticalAlignment, ITextDirection, IFont, IBounds
    {

    }

}
