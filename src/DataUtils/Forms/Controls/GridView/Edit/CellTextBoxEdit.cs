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
    public class CellTextBoxEdit : IEditView, IGridViewCellValueChanged
    {
        private TextBox edit = new TextBox();
        public CellTextBoxEdit()
        {
            edit.PreviewKeyDown += Edit_PreviewKeyDown;
            edit.TextChanged += Edit_TextChanged;
        }

        private bool lckPreviewKeyDown = false;
        private void Edit_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Tab)
                {
                    if (lckPreviewKeyDown)
                        return;
                    lckPreviewKeyDown = true;
                    IOnKeyDown onKeyDown = this.Parent as IOnKeyDown;
                    IEndEdit endEdit = this.parent as IEndEdit;
                    if (endEdit != null)
                    {
                        endEdit.EndEdit();
                    }
                    if (onKeyDown != null)
                    {
                        KeyEventArgs keyEventArgs = new KeyEventArgs(e.KeyData);
                        onKeyDown.OnKeyDown(this, keyEventArgs, Views.EventViewArgs.GetEventViewArgs(edit));
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmMain2", "statusSum_Click", ex);
            }
            finally
            {
                lckPreviewKeyDown = false;
            }
        }

        private void Edit_TextChanged(object sender, EventArgs e)
        {
            IValue pvalue = this.Parent as IValue;
            if (pvalue != null)
            {
                pvalue.Value = this.edit.Text;
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

        //[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        //protected override bool ProcessKeyEventArgs(ref Message m)
        //{
        //    if ((m.Msg == 260) || (m.Msg == 0x100))
        //    {
        //        if (this.Parent != null)
        //        { 
        //            KeyEventArgs e = new KeyEventArgs(((Keys)((int)((long)m.WParam))) | Control.ModifierKeys);
        //            if ((e.KeyCode != Keys.ProcessKey) || (((int)m.LParam) != 1))
        //            {
        //                if (!this.Multiline)
        //                {
        //                    if (e.KeyCode == Keys.Enter)
        //                    {
        //                        this.Parent.Grid.MoveFocusedCellToRightCell();
        //                    }
        //                }
        //                if (e.KeyCode == Keys.Tab)
        //                {
        //                    this.Parent.Grid.MoveFocusedCellToRightCell();
        //                }
        //                if (e.KeyData == Keys.Right)
        //                {
        //                    if (this.SelectionStart >= this.Text.Length)
        //                    {
        //                        this.Parent.Grid.MoveFocusedCellToRightCell();
        //                        return true;
        //                    }
        //                }
        //                if (e.KeyData == Keys.Left)
        //                {
        //                    if (this.SelectionStart < 1)
        //                    {
        //                        this.Parent.Grid.MoveFocusedCellToLeftCell();
        //                        return true;
        //                    }
        //                }
        //                if (e.KeyData == Keys.Up)
        //                {
        //                    if (this.Lines.Length < 2)
        //                    {
        //                        this.Parent.Grid.MoveFocusedCellToTopCell();
        //                        return true;
        //                    }
        //                    int start = this.SelectionStart;
        //                    int lastindex = this.Lines[0].Length;

        //                    if (start <= lastindex)
        //                    {

        //                        this.Parent.Grid.MoveFocusedCellToTopCell();
        //                        return true;
        //                    }
        //                }
        //                if (e.KeyData == Keys.Down)
        //                {
        //                    if (this.Lines.Length < 2)
        //                    {
        //                        this.Parent.Grid.MoveFocusedCellToBottomCell();
        //                        return true;
        //                    }
        //                    int start = this.SelectionStart;
        //                    int lastindex = this.Text.Length - this.Lines[0].Length;

        //                    if (start >= lastindex)
        //                    {
        //                        this.Parent.Grid.MoveFocusedCellToBottomCell();
        //                        return true;
        //                    }

        //                }
        //            }
        //        }
        //    }

        //    return base.ProcessKeyEventArgs(ref m);
        //}

 

        //protected override void OnKeyDown(KeyEventArgs e)
        //{
        //    if (this.Parent == null)
        //        return;
        //    try
        //    {
        //        if (e.Alt || e.Control || e.Shift)
        //        {

        //        }
        //        else
        //        {
        //            //if (e.KeyData == Keys.Enter)
        //            //{
        //            //    this.Cell.Grid.MoveFocusedCellToRightCell();
        //            //}
        //            //if (e.KeyData == Keys.Left)
        //            //{
        //            //    this.Cell.Grid.MoveFocusedCellToLeftCell();
        //            //}
        //            //if (e.KeyData == Keys.Up)
        //            //{
        //            //    this.Cell.Grid.MoveFocusedCellToTopCell();
        //            //}
        //            //if (e.KeyData == Keys.Down)
        //            //{
        //            //    this.Cell.Grid.MoveFocusedCellToBottomCell();
        //            //}
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Feng.Utils.BugReport.Log(ex);
        //        Feng.Utils.TraceHelper.WriteTrace("DataExcel", "CellTextBoxEdit", "OnKeyDown", ex);
        //    }


        //    base.OnKeyDown(e);
        //}
         
        public virtual bool InitEdit(object cell)
        { 
            try
            {
                parent = cell;
                if (this.Parent == null)
                    return false;
                this._inedit = true;
                this.edit.Multiline = true;
                IValue cvalue = cell as IValue;
                if (cvalue != null)
                {
                    this.edit.Text = Feng.Utils.ConvertHelper.ToString(cvalue.Value);
                }
                IBounds bounds = cell as IBounds;

                if (bounds != null)
                {
                    this.edit.Left = (int)bounds.Left + 1;
                    this.edit.Top = (int)bounds.Top + 1;
                    this.edit.Width = (int)bounds.Width - 1;
                    this.edit.Height = (int)bounds.Height - 1;
                }
                IPointToControl pointToClient = cell as IPointToControl;
                if (pointToClient != null)
                { 
                    Point pt = pointToClient.PointToControl(bounds.Rect.Location);
                    this.edit.Left = (int)pt.X + 1;
                    this.edit.Top = (int)pt.Y+ 1;
                }
                this.edit.Visible = true;
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
                this.edit.BorderStyle = System.Windows.Forms.BorderStyle.None;
                this.edit.SelectAll();
                this.edit.Focus(); 
                
            }
            finally
            { 
            }
            return true;
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
            //this._inedit = false;
            //this.Hide();
            //if (this.Parent != null)
            //{
            //    string text = this.Text;
            //    this.Parent.Value = text; 
            //    CellValueChanged(this.Parent);
            //}

            //this.parent = null;
        }

        public virtual bool IDraw(GridViewCell cell)
        {
            if (this.InEdit)
            {
                return true;
            }
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
 

        public virtual bool OnCellKeyDown(object sender, KeyEventArgs e)
        {
            GridViewCell cell = sender as GridViewCell;
            if (cell == null)
                return false;
            if (e.Alt || e.Control || e.Shift)
            {

            }
            else
            {
                //if (e.KeyData == Keys.Right)
                //{
                //    this.Parent.Grid.MoveFocusedCellToRightCell();
                //}
                //if (e.KeyData == Keys.Left)
                //{
                //    this.Parent.Grid.MoveFocusedCellToLeftCell();
                //}
            }
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

        [Browsable(false)]
        public virtual int AddressID
        {
            get
            {
                return -1;
            }
            set
            {
            }
        }

        public virtual void TextPress(string text)
        {
            this.edit.Text = text;
            this.edit.SelectionStart = text.Length;
        }


        public void CellValueChanged(GridViewCell cell)
        {
            if (cell == null)
                return;
            cell.Grid.OnCellValueChanged(cell);
        }

        [Browsable(false)]
        public virtual DataStruct Data
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

 
        public bool DrawCell(object sender, GraphicsObject g, Rectangle rect, object value)
        {
            return false;
        }

        public bool DrawBackCell(object sender, GraphicsObject g, Rectangle rect, object value)
        {
            return false;
        }

        public bool PrintCell(object sender, GraphicsObject g, Rectangle rect, object value)
        {
            return false;
        }

        public bool PrintBackCell(object sender, GraphicsObject g, Rectangle rect, object value)
        {
            return false;
        }
    }

} 
