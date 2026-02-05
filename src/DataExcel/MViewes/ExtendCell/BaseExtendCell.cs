using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Feng.Print; 
using Feng.Excel.Interfaces;
using Feng.Forms.Views;

namespace Feng.Excel.Extend
{
    public abstract class BaseExtendCell : IExtendCell 
    {

        #region IEndEdit 成员

        public virtual void EndEdit()
        { 
        }

        #endregion

        #region IInitEdit 成员

        public virtual bool InitEdit(object obj)
        {
            return false;
        }

        #endregion

        #region IInEdit 成员

        public virtual bool InEdit
        {
            get;
            set;
        }

        #endregion

        #region IOwnEditControl 成员

        public virtual ICellEditControl OwnEditControl
        {
            get;
            set;
        }

        #endregion

        #region IText 成员

        public virtual string Text
        {
            get;
            set;
        }

        #endregion

        #region IForeColor 成员

        public virtual System.Drawing.Color ForeColor
        {
            get;
            set;
        }

        #endregion

        #region IGrid 成员

        public virtual DataExcel Grid
        {
            get;
            set;
        }

        #endregion

        #region IEndable 成员

        public virtual bool Enable
        {
            get;
            set;
        }

        #endregion

        #region IInhertReadOnly 成员

        public virtual bool InhertReadOnly
        {
            get;
            set;
        }


        #endregion

        #region IReadOnly 成员

        public virtual bool ReadOnly
        {
            get;
            set;
        }

        #endregion

        #region IDraw 成员

        public abstract bool OnDraw(object sender, Feng.Drawing.GraphicsObject g);
 
        #endregion

        #region ISelected 成员
        private bool _selected = false;
        public virtual bool Selected
        {
            get { return _selected; }
            set
            {
                _selected = value;
                if (value)
                {
                    this.Grid.Selecteds.Add(this);
                }
            }
        }

        #endregion

        #region ISelectColor 成员

        public virtual System.Drawing.Color SelectBackColor
        {
            get;
            set;
        }

        public virtual System.Drawing.Color SelectForceColor
        {
            get;
            set;
        }

        #endregion

        #region IValue 成员

        public virtual object Value
        {
            get;
            set;
        }

        #endregion

        #region IPrint 成员

        public virtual bool Print(PrintArgs e)
        {
            return false;   
        }

        #endregion

        #region IFont 成员

        public virtual System.Drawing.Font Font
        {
            get;
            set;
        }

        #endregion

        #region IBounds 成员

        public virtual int Left
        {
            get;
            set;
        }

        public virtual int Height
        {
            get;
            set;
        }

        public virtual int Right
        {
            get;
            set;
        }

        public virtual int Bottom
        {
            get;
            set;
        }

        public virtual int Top
        {
            get;
            set;
        }

        public virtual int Width
        {
            get;
            set;
        }

        public virtual System.Drawing.Rectangle Rect
        {
            get;
            set;
        }

        #endregion

        #region IFormat 成员

        public virtual Feng.Utils.FormatType FormatType
        {
            get;
            set;
        }

        public virtual string FormatString
        {
            get;
            set;
        }

        #endregion

        #region IDateTime 成员

        public virtual DateTime DateTime
        {
            get;
            set;
        }

        #endregion

        #region IChecked 成员

        public virtual bool Checked
        {
            get;
            set;
        }

        #endregion

        #region IMouseDownPoint 成员

        public virtual System.Drawing.Point MouseDownPoint
        {
            get;
            set;
        }

        #endregion

        #region IMouseDownSize 成员

        public virtual System.Drawing.Size MouseDownSize
        {
            get;
            set;
        }

        #endregion

        #region IExtendCell 成员

        public virtual void FreshLocation()
        {
            
        }

        public virtual void ReSetRowColumn(System.Drawing.Point pt)
        {
            
        }

        #endregion

        #region IExtendCell 成员


        public virtual Feng.Enums.SizeChangMode SizeChangMode
        {
            get;
            set;
        }

        #endregion

        #region IExtendCell 成员


        public virtual bool SizeRectContains(System.Drawing.Point pt)
        {
            return false;
        }

        #endregion

        #region IExtendCell 成员


        public virtual bool MouseDown(System.Drawing.Point pt)
        {
            return false;
        }

        #endregion

        #region IBackColor 成员

        public virtual Color BackColor
        {
            get;
            set;
        }

        #endregion

        #region IHorizontalAlignment 成员

        public virtual StringAlignment HorizontalAlignment
        {
            get;
            set;
        }

        #endregion

        #region IVerticalAlignment 成员

        public virtual StringAlignment VerticalAlignment
        {
            get;
            set;
        }

        #endregion

        #region ITextDirection 成员

        public virtual bool DirectionVertical
        {
            get;
            set;
        }

        #endregion

        #region IAutoMultiline 成员

        public virtual bool AutoMultiline
        {
            get;
            set;
        }

        #endregion

        #region ISetText 成员

        public virtual void SetText(string text)
        {
            this.Text = text;
        }

        #endregion

        #region ICellEvents 成员

        public virtual bool OnMouseUp( object sender,MouseEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnMouseLeave(object sender, EventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnMouseHover(object sender, EventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnMouseEnter(object sender, EventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnMouseClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnMouseCaptureChanged(object sender, EventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnClick(object sender, EventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnKeyPress(object sender, KeyPressEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnKeyUp(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnDoubleClick(object sender, EventArgs e, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnPreProcessMessage(object sender, ref Message msg, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnProcessCmdKey(object sender, ref Message msg, Keys keyData, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnProcessDialogChar(object sender, char charCode, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnProcessDialogKey(object sender, Keys keyData, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnProcessKeyEventArgs(object sender, ref Message m, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnProcessKeyMessage(object sender, ref Message m, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnProcessKeyPreview(object sender, ref Message m, EventViewArgs ve)
        {
            return false;
        }

        public virtual bool OnWndProc(object sender, ref Message m, EventViewArgs ve)
        {
            return false;
        }


        public virtual bool OnMouseWheel(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return false;
        }

        #endregion

        #region PropertyEvent 成员
        private string _PropertyOnMouseUp = null;
        public virtual string PropertyOnMouseUp
        {
            get
            {
                return _PropertyOnMouseUp;
            }
            set
            {
                _PropertyOnMouseUp = value;
            }
        }

        private string _PropertyOnMouseMove = null;
        public virtual string PropertyOnMouseMove
        {
            get
            {
                return _PropertyOnMouseMove;
            }
            set
            {
                _PropertyOnMouseMove = value;
            }
        }

        private string _PropertyOnMouseLeave = null;
        public virtual string PropertyOnMouseLeave
        {
            get
            {
                return _PropertyOnMouseLeave;
            }
            set
            {
                _PropertyOnMouseLeave = value;
            }
        }

        private string _PropertyOnMouseHover = null;
        public virtual string PropertyOnMouseHover
        {
            get
            {
                return _PropertyOnMouseHover;
            }
            set
            {
                _PropertyOnMouseHover = value;
            }
        }
        private string _PropertyOnMouseEnter = null;
        public virtual string PropertyOnMouseEnter
        {
            get
            {
                return _PropertyOnMouseEnter;
            }
            set
            {
                _PropertyOnMouseEnter = value;
            }
        }

        private string _PropertyOnMouseDown = null;
        public virtual string PropertyOnMouseDown
        {
            get
            {
                return _PropertyOnMouseDown;
            }
            set
            {
                _PropertyOnMouseDown = value;
            }
        }

        private string _PropertyOnMouseDoubleClick = null;
        public virtual string PropertyOnMouseDoubleClick
        {
            get
            {
                return _PropertyOnMouseDoubleClick;
            }
            set
            {
                _PropertyOnMouseDoubleClick = value;
            }
        }

        private string _PropertyOnMouseClick = null;
        public virtual string PropertyOnMouseClick
        {
            get
            {
                return _PropertyOnMouseClick;
            }
            set
            {
                _PropertyOnMouseClick = value;
            }
        }

        private string _PropertyOnMouseCaptureChanged = null;
        public virtual string PropertyOnMouseCaptureChanged
        {
            get
            {
                return _PropertyOnMouseCaptureChanged;
            }
            set
            {
                _PropertyOnMouseCaptureChanged = value;
            }
        }

        private string _PropertyOnMouseWheel = null;
        public virtual string PropertyOnMouseWheel
        {
            get
            {
                return _PropertyOnMouseWheel;
            }
            set
            {
                _PropertyOnMouseWheel = value;
            }
        }

        private string _PropertyOnClick = null;
        public virtual string PropertyOnClick
        {
            get
            {
                return _PropertyOnClick;
            }
            set
            {
                _PropertyOnClick = value;
            }
        }

        private string _PropertyOnKeyDown = null;
        public virtual string PropertyOnKeyDown
        {
            get
            {
                return _PropertyOnKeyDown;
            }
            set
            {
                _PropertyOnKeyDown = value;
            }
        }

        private string _PropertyOnKeyPress = null;
        public virtual string PropertyOnKeyPress
        {
            get
            {
                return _PropertyOnKeyPress;
            }
            set
            {
                _PropertyOnKeyPress = value;
            }
        }

        private string _PropertyOnKeyUp = null;
        public virtual string PropertyOnKeyUp
        {
            get
            {
                return _PropertyOnKeyUp;
            }
            set
            {
                _PropertyOnKeyUp = value;
            }
        }

        private string _PropertyOnPreviewKeyDown = null;
        public virtual string PropertyOnPreviewKeyDown
        {
            get
            {
                return _PropertyOnPreviewKeyDown;
            }
            set
            {
                _PropertyOnPreviewKeyDown = value;
            }
        }

        private string _PropertyOnDoubleClick = null;
        public virtual string PropertyOnDoubleClick
        {
            get
            {
                return _PropertyOnDoubleClick;
            }
            set
            {
                _PropertyOnDoubleClick = value;
            }
        }

        private string _PropertyOnCellInitEdit = null;
        public string PropertyOnCellInitEdit
        {
            get
            {
                return _PropertyOnCellInitEdit;
            }
            set
            {
                _PropertyOnCellInitEdit = value;
            }
        }

        private string _PropertyOnCellEndEdit = null;
        public string PropertyOnCellEndEdit
        {
            get
            {
                return _PropertyOnCellEndEdit;
            }
            set
            {
                _PropertyOnCellEndEdit = value;
            }
        }

        private string _PropertyOnCellValueChanged = null;
        public string PropertyOnCellValueChanged
        {
            get
            {
                return _PropertyOnCellValueChanged;
            }
            set
            {
                _PropertyOnCellValueChanged = value;
            }
        }
        #endregion


        private string _PropertyOnDrawBack = null;
        public string PropertyOnDrawBack
        {
            get
            {
                return _PropertyOnDrawBack;
            }
            set
            {
                _PropertyOnDrawBack = value;
            }
        }

        private string _PropertyOnDrawCell = null;
        public string PropertyOnDrawCell
        {
            get
            {
                return _PropertyOnDrawCell;
            }
            set
            {
                _PropertyOnDrawCell = value;
            }
        }

        public bool OnDrawBack(object sender, Feng.Drawing.GraphicsObject g)
        {
            throw new NotImplementedException();
        }
    }
}
