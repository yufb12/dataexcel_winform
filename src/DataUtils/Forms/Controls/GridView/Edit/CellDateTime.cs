using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Drawing;
using System.Security.Permissions;
using System.Data;

using Feng.Forms.Interface;
using Feng.Data;

namespace Feng.Forms.Controls.GridControl.Edits
{

    [Serializable]
    [ToolboxItem(false)]
    public class CellDateTime : DateTimePicker, IEditView,Feng.Forms.Interface.IEditControl, IGridViewCellValueChanged
    {
        public CellDateTime()
        {
        }

        private GridViewCell _Cell = null;
        [Browsable(false)]
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

        bool locktext = false;

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override bool ProcessKeyEventArgs(ref Message m)
        {
            if ((m.Msg == 260) || (m.Msg == 0x100))
            { 
                KeyEventArgs e = new KeyEventArgs(((Keys)((int)((long)m.WParam))) | Control.ModifierKeys);
                if ((e.KeyCode != Keys.ProcessKey) || (((int)m.LParam) != 1))
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        if (this.Parent != null)
                        {
                            if (this.Parent.Value == null)
                            {
                                this.Parent.Value = this.Value;
                            }

                        }
                        this.Parent.Grid.MoveFocusedCellToBottomCell();
                    }
                    if (e.KeyCode == Keys.Tab)
                    {
                        this.Parent.Grid.MoveFocusedCellToRightCell();
                    }
                    //if (e.KeyData == Keys.Right)
                    //{
                    //    this.Cell.Grid.MoveFocusedCellToRightCell();
                    //    return true;
                    //}
                    //if (e.KeyData == Keys.Left)
                    //{
                    //    this.Cell.Grid.MoveFocusedCellToLeftCell();
                    //    return true;
                    //}
                    if (e.KeyData == Keys.Up)
                    {
                        this.Parent.Grid.MoveFocusedCellToTopCell();
                        return true;
                    }
                    if (e.KeyData == Keys.Down)
                    {
                        this.Parent.Grid.MoveFocusedCellToBottomCell();
                        return true;
                    }
                }
            }

            return base.ProcessKeyEventArgs(ref m);
        }
        public void Init(GridViewCell cell)
        {
            locktext = true;
            try
            {
                _Cell = cell;
                if (this.Parent == null)
                    return;
                this.Grid.Edit = this;
                this._inedit = true;
                DateTime dt = Feng.Utils.ConvertHelper.ToDateTime(cell.Value); 
                if (dt == DateTime.MinValue)
                {
                    this.Text = string.Empty;
                }
                else
                {
                    this.Value = dt;
                }
                this.Left = (int)cell.Left + cell.Grid.Left + 1;
                this.Top = (int)cell.Top + cell.Grid.Top + 1;
                this.Width = (int)cell.Width - 1;
                this.Height = (int)cell.Height - 1;
                this.Visible = true;
                cell.Grid.AddControl(this);
                this.Visible = true;

                this.Focus();

            }
            finally
            {
                locktext = false;
            }
        }

        public void EndEdit()
        {
            this._inedit = false;
            this.Visible = false;
            this._Cell = null;
        }
        protected override void OnValueChanged(EventArgs eventargs)
        {
            if (locktext)
                return;
            if (this.Parent == null)
                return;
            this.Parent.Value = this.Value;
            CellValueChanged(this.Parent);
            base.OnValueChanged(eventargs);
        }

        public virtual bool IDraw(GridViewCell cell)
        {
            return false;
        }
        [Browsable(false)]
        public virtual GridView Grid
        {
            get
            {
                if (this.Parent == null)
                    return null;
                return this.Parent.Grid;
            }
        }
 
        [Browsable(false)]
        public string Version
        {
            get { return string.Empty; }
        }

        [Browsable(false)]
        public int VersionIndex
        {
            get { return 0; }
        }

        [Browsable(false)]
        public string DllName
        {
            get { return string.Empty; }
        }

        [Browsable(false)]
        public string DownLoadUrl
        {
            get { return string.Empty; }
        }

        [Browsable(false)]
        public DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = this.DllName,
                    Version = this.Version,
                    AessemlyDownLoadUrl = this.DownLoadUrl,
                    FullName = t.FullName,
                    Name = t.Name,
                };


                return data;
            }
        }

        public virtual void ReadDataStruct(DataStruct data)
        {

        }
        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
        }
        protected override void OnClick(EventArgs e)
        {

            try
            {

                if (this.Parent != null)
                {
                    if (!this.Parent.Value.Equals(this.Value))
                    {
                        this.Parent.Value = this.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

            base.OnClick(e);
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
            return true;
        }

        public virtual bool OnClick(object sender, EventArgs e)
        {
            return false;
        }

        public virtual bool OnKeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Alt || e.Control || e.Shift)
            //{

            //}
            //else
            //{
            //    if (e.KeyData == Keys.Right)
            //    {
            //        this.Cell.Grid.MoveFocusedCellToRightCell();
            //    }
            //    if (e.KeyData == Keys.Left)
            //    {
            //        this.Cell.Grid.MoveFocusedCellToLeftCell();
            //    }
            //}
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

        public virtual bool DrawCell(GridViewCell cell, Feng.Drawing.GraphicsObject g)
        {
            return false;
 
        }

        public virtual bool DrawCellBack(GridViewCell cell, Feng.Drawing.GraphicsObject g)
        {
            return false;
        }

        [Browsable(false)]
        public string DataUrl
        {
            get
            {
                return string.Empty;
            }
            set
            {
            }
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
 
        public virtual void TextPress(string text)
        {

        }
        public void CellValueChanged(GridViewCell cell)
        { 
            cell.Grid.OnCellValueChanged(cell);
        }

        public bool InitEdit(object obj)
        {
            throw new NotImplementedException();
        }

        public bool DrawCell(object sender, GraphicsObject g, Rectangle rect, object value)
        {
            throw new NotImplementedException();
        }

        public bool DrawBackCell(object sender, GraphicsObject g, Rectangle rect, object value)
        {
            throw new NotImplementedException();
        }

        public bool PrintCell(object sender, GraphicsObject g, Rectangle rect, object value)
        {
            throw new NotImplementedException();
        }

        public bool PrintBackCell(object sender, GraphicsObject g, Rectangle rect, object value)
        {
            throw new NotImplementedException();
        }
    }
} 
