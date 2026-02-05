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
    public class CellNumber : IEditView, IGridViewCellValueChanged
    {
        private NumericUpDown edit = new NumericUpDown();
        public CellNumber()
        {
            edit.PreviewKeyDown += Edit_PreviewKeyDown;
            edit.ValueChanged += Edit_ValueChanged;
        }

        private void Edit_ValueChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("GridControl.Edits", "CellNumber", "Edit_ValueChanged", ex);
            }
        }

        private void Edit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("GridControl.Edits", "CellNumber", "Edit_PreviewKeyDown", ex);
            }
        }

        private object parent = null;
        [Browsable(false)]
        public virtual object Parent
        {
            get
            {
                return parent;
            }
        }

        bool locktext = false;

        //[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        //protected override bool ProcessKeyEventArgs(ref Message m)
        //{
        //    if ((m.Msg == 260) || (m.Msg == 0x100))
        //    { 
        //        KeyEventArgs e = new KeyEventArgs(((Keys)((int)((long)m.WParam))) | Control.ModifierKeys);
        //        if ((e.KeyCode != Keys.ProcessKey) || (((int)m.LParam) != 1))
        //        {
        //            if (e.KeyCode == Keys.Enter)
        //            {
        //                this.Cell.Grid.MoveFocusedCellToBottomCell();
        //            }
        //            if (e.KeyCode == Keys.Tab)
        //            {
        //                this.Cell.Grid.MoveFocusedCellToRightCell();
        //            }
        //        }
        //    }

        //    return base.ProcessKeyEventArgs(ref m);
        //}


        public void Init(object cell)
        {
            locktext = true;
            try
            {
                parent = cell;
                if (this.Parent == null)
                    return;
                this._inedit = true;
                this.edit.DecimalPlaces = 2;
                this.edit.Maximum = int.MaxValue;
                this.edit.Minimum = int.MinValue;
                IValue cvalue = cell as IValue;
                if (cvalue != null)
                {
                    this.edit.Value = Feng.Utils.ConvertHelper.ToDecimal(cvalue.Value);
                }
                IBounds bounds = cell as IBounds;
                if (bounds != null)
                {
                    this.edit.Left = (int)bounds.Left + 1;
                    this.edit.Top = (int)bounds.Top + 1;
                    this.edit.Width = (int)bounds.Width - 1;
                    this.edit.Height = (int)bounds.Height - 1;
                }
                IFindControl findControl = this.Parent as IFindControl;
                if (findControl != null)
                {
                    Control control = findControl.FindControl();
                    if (control != null)
                    {
                        control.Controls.Add(this.edit);
                    }
                }
                this.edit.Visible = true;
                this.edit.Focus();
            }
            finally
            {
                locktext = false;
            }
        }

        public void EndEdit()
        {
            IFindControl findControl = this.Parent as IFindControl;
            if (findControl != null)
            {
                Control control = findControl.FindControl();
                if (control != null)
                {
                    control.Controls.Remove(this.edit);
                }
            }
            this._inedit = false; 
            this.parent = null;
            IEndEdit endEdit = this.Parent as IEndEdit;
            if (endEdit != null)
            {
                endEdit.EndEdit();
            }
        }

        //protected override void OnValueChanged(EventArgs eventargs)
        //{
        //    if (locktext)
        //        return;
        //    if (this.Parent == null)
        //        return;
        //    this.Parent.Value = this.Value;
        //    CellValueChanged(this.Parent);
        //    base.OnValueChanged(eventargs);
        //}

        public virtual bool IDraw(GridViewCell cell)
        {
            return false;
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
 
        //public virtual bool OnCellKeyDown(object sender, KeyEventArgs e)
        //{
        //    GridViewCell cell = sender as GridViewCell;
        //    if (cell == null)
        //        return false;
        //    if (e.Alt || e.Control || e.Shift)
        //    {

        //    }
        //    else
        //    {
        //        if (e.KeyData == Keys.Right)
        //        {
        //            this.Parent.Grid.MoveFocusedCellToRightCell();
        //        }
        //        if (e.KeyData == Keys.Left)
        //        {
        //            this.Parent.Grid.MoveFocusedCellToLeftCell();
        //        }
        //    }
        //    return false;
        //}
 
 
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
