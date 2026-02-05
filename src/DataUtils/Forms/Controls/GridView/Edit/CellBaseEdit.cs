using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel; 
using System.Drawing.Design;
using Feng.Utils; 
using Feng.Data;
using Feng.Print;
using Feng.Forms.Controls.GridControl;

namespace Feng.Forms.Controls.GridControl.Edits
{
    public interface IGridViewCellValueChanged
    {
        void CellValueChanged(GridViewCell cell);
    }
    [Serializable]
    public abstract class CellBaseEdit : Feng.Forms.Interface.IEditControl, IGridViewCellValueChanged
    {
        public CellBaseEdit() 
        {  
        }

        public virtual void Init(GridViewCell cell)
        {

        }

        public virtual void EndEdit()
        {  

        }

        public virtual bool DrawCell(GridViewCell cell, Feng.Drawing.GraphicsObject g)
        { 
            return false;
        }

        public virtual bool DrawCellBack(GridViewCell cell, Feng.Drawing.GraphicsObject g)
        {
            return false;
        }
 
        public virtual void DrawCell(GridViewCell cell, Feng.Drawing.GraphicsObject g, Rectangle  rect, string text)
        {
            GridView.DrawCellText(cell, g, rect, text);
        }

        [Browsable(false)]
        public virtual string Version
        {
            get { return string.Empty; }
        }

        [Browsable(false)]
        public virtual int VersionIndex
        {
            get { return 0; }
        }

        [Browsable(false)]
        public virtual string DllName
        {
            get { return string.Empty; }
        }

        [Browsable(false)]
        public virtual string DownLoadUrl
        {
            get { return string.Empty; }
        }

        [Browsable(false)]
        public abstract DataStruct Data
        {
            get;
        }

        public virtual bool OnMouseUp(object sender, MouseEventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseMove(object sender, MouseEventArgs e)
        { 
            return false;
        }

        public virtual bool OnMouseLeave(object sender, EventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseHover(object sender, EventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseEnter(object sender, EventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseDown(object sender, MouseEventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseDoubleClick(object sender, MouseEventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseClick(object sender, MouseEventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseCaptureChanged(object sender, EventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseWheel(object sender, MouseEventArgs e)
        {
            return false;
        }

        public virtual bool OnClick(object sender, EventArgs e)
        {
            return false;
        }

        public virtual bool OnKeyDown(object sender, KeyEventArgs e)
        {
        
            return false;
        }

        public virtual bool OnKeyPress(object sender, KeyPressEventArgs e)
        {
            return false;
        }

        public virtual bool OnKeyUp(object sender, KeyEventArgs e)
        {
            return false;
        }

        public virtual bool OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            return false;
        }

        public virtual bool OnDoubleClick(object sender, EventArgs e)
        {
            return false;
        }

        public virtual bool OnPreProcessMessage(object sender, ref Message msg)
        {
            return false;
        }

        public virtual bool OnProcessCmdKey(object sender, ref Message msg, Keys keyData)
        {
            return false;
        }

        public virtual bool OnProcessDialogChar(object sender, char charCode)
        {
            return false;
        }

        public virtual bool OnProcessDialogKey(object sender, Keys keyData)
        {
            return false;
        }

        public virtual bool OnProcessKeyEventArgs(object sender, ref Message m)
        {
            return false;
        }

        public virtual bool OnProcessKeyMessage(object sender, ref Message m)
        {
            return false;
        }

        public virtual bool OnProcessKeyPreview(object sender, ref Message m)
        {
            return false;
        }

        public virtual bool OnWndProc(object sender, ref Message m)
        {
            return false;
        }

        private bool _inedit = false;
        [Browsable(false)]
        public virtual bool InEdit
        {
            get { return _inedit; }
        }

        [Browsable(false)]
        public virtual string DataUrl
        {
            get
            {
                return string.Empty;
            }
            set
            {
            }
        }

        public virtual bool PrintCellBack(GridViewCell cell, PrintArgs e)
        {
            return false;
        }

        private int _id = -1;
        [Browsable(false)]
        public virtual int AddressID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        public virtual bool PrintCell(GridViewCell cell, PrintArgs e, RectangleF rect)
        {
            return false;
        }

        public virtual bool PrintValue(GridViewCell cell, PrintArgs e, RectangleF rect, object value)
        { 
            return false;
        }
          
        public virtual void TextPress(string text)
        {

        }

        public virtual void CellValueChanged(GridViewCell cell)
        {
            cell.Grid.OnCellValueChanged(cell);
        }


        public virtual void ReadDataStruct(DataStruct data)
        { 
        }
        private GridViewCell _Cell = null;
        public virtual GridViewCell Parent
        {
            get
            {
                return _Cell;
            }
            set
            {
                _Cell = value;
            }
        }
    }

}
