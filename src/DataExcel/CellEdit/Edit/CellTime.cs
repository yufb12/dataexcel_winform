using Feng.Data;
using Feng.Excel.Commands;
using Feng.Excel.Interfaces;
using Feng.Forms.Controls.Designer;
using Feng.Print;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [Serializable]
    [ToolboxItem(false)]
    public class CellTime : DateTimePicker, ICellEditControl
    {
        public CellTime(DataExcel grid)
        {
            this.grid = grid;
        }

        public virtual string ShortName { get { return "CellTime"; } set { } }
        private DataExcel grid = null;
        public DataExcel Grid
        {
            get
            {
                return grid;
            }
        }
        private ICell _Cell = null;
        [Browsable(false)]
        public virtual ICell Cell
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
                        this.Cell.Grid.MoveFocusedCellToDownCell();
                    }
                    if (e.KeyCode == Keys.Tab)
                    {
                        this.Cell.Grid.MoveFocusedCellToRightCell();
                    }
                }
            }

            return base.ProcessKeyEventArgs(ref m);
        }
        public virtual bool InitEdit(object obj)
        {
            locktext = true;
            try
            {
                ICell cell = obj as ICell;
                if (cell == null)
                    return false;
                
                _Cell = cell;
                if (this.Cell == null)
                    return false;
                this._inedit = true;
                this.Left = (int)cell.Left;
                this.Top = (int)cell.Top;
                this.Width = (int)cell.Width;
                this.Height = (int)cell.Height;
                this.Visible = true;
                //cell.Grid.AddEdit(this);
                this.Visible = true;
                this.Value = Feng.Utils.ConvertHelper.ToDateTime(cell.Value);
                this.Format = DateTimePickerFormat.Time;
                this.ShowUpDown = true;
                this.Focus();
            }
            finally
            {
                locktext = false;
            }
            return true;
        }

        public void EndEdit()
        {
            this._inedit = false;
            this.Visible = false;
            this.Grid.EndEdit();
            this._Cell = null;
        }

        protected override void OnValueChanged(EventArgs eventargs)
        {
            if (locktext)
                return;
            if (this.Cell == null)
                return;
            if (this.Grid.CanUndoRedo)
            {
                CellValueCommand cmd = new CellValueCommand();
                cmd.Value = this.Cell.Value;
                cmd.Text = this.Cell.Text;
                cmd.Cell = this.Cell;
                this.Grid.Commands.Add(cmd);
            }
            this.Cell.Value = this.Value;
            this.Cell.Text = this.Text;
            this.Cell.FreshContens();
            if (!this.Cell.IsMergeCell)
            {
                if (this.Cell.ContensWidth > this.Grid.DefaultColumnWidth)
                {
                    this.Grid.RefreshColumnWidth(this.Cell.Column);
                }
            }
            this.Grid.EndEditClear();
            base.OnValueChanged(eventargs);
        }

        public virtual bool IDraw(IBaseCell cell)
        {
            return false;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (this.Cell != null)
            {
                this.grid.OnCellEditControlValueChanged(this.Cell, this.Text);
            }
            base.OnTextChanged(e);
        }

        public void Save(IO.BinaryWriter stream)
        {
        }

        private bool _hasChildEdit = false;
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool HasChildEdit
        {
            get
            {
                return _hasChildEdit;
            }
            set
            {
                _hasChildEdit = value;
            }
        }

        public void Read(DataExcel grid, int version, DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader bw = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                this.AddressID = bw.ReadIndex(1, 0);
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

                using (Feng.Excel.IO.BinaryWriter bw = new Feng.Excel.IO.BinaryWriter())
                {
                    bw.Write(1, this.AddressID);
                    data.Data = bw.GetData();
                }
                return data;
            }
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
            IBaseCell cell = sender as IBaseCell;
            if (cell == null)
                return false;
            if (e.Alt || e.Control || e.Shift)
            {

            }
            else
            {
                if (e.KeyData == Keys.Right)
                {
                    this.Cell.Grid.MoveFocusedCellToRightCell();
                }
                if (e.KeyData == Keys.Left)
                {
                    this.Cell.Grid.MoveFocusedCellToLeftCell();
                }
            }
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

        public virtual bool DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            return false;
        }

        public virtual bool DrawCellBack(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            return false;
        }

        public virtual bool PrintCellBack(IBaseCell cell, PrintArgs e)
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

        public virtual bool PrintCell(IBaseCell cell, PrintArgs e, Rectangle rect)
        {
            return false;
        }

        public virtual bool PrintValue(IBaseCell cell, PrintArgs e, Rectangle rect, object value)
        {
            return false;
        }

        public virtual void TextPress(string text)
        {

        }

        public void CellValueChanged(IBaseCell cell)
        {

        }

        public void ReadDataStruct(DataStruct data)
        {
        }

        public virtual ICellEditControl Clone(DataExcel grid)
        {
            CellTime celledit = new CellTime(grid);
            celledit._hasChildEdit = this._hasChildEdit;
            return celledit;
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {

            try
            {
                if (e.KeyData == Keys.Escape)
                {
                    if (this.InEdit)
                    {
                        if (this.Cell != null)
                        {
                            this.Cell.EndEdit();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }
    }

}
