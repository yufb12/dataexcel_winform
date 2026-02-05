using Feng.Data;
using Feng.Print;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Feng.Forms.Controls.GridControl.Edits
{
    [Serializable]
    [ToolboxItem(false)]
    public class CellPasswordBox : TextBox, Feng.Forms.Interface.IEditControl, IGridViewCellValueChanged
    {
        public CellPasswordBox()
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
                Init(_Cell);
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
                        this.Parent.Grid.MoveFocusedCellToBottomCell();
                    }
                    if (e.KeyCode == Keys.Tab)
                    {
                        this.Parent.Grid.MoveFocusedCellToRightCell();
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
                this.PasswordChar = '*';
                this._inedit = true;
                this.Left = (int)cell.Left;
                this.Top = (int)cell.Top;
                this.Width = (int)cell.Width;
                this.Height = (int)cell.Height;
                this.Visible = true;
                cell.Grid.AddControl(this);

                this.Visible = true;
                this.Text = cell.Text;

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

        protected override void OnTextChanged(EventArgs e)
        {
            if (locktext)
                return;
            if (this.Parent == null)
                return;
            this.Parent.Value = this.Text;
            CellValueChanged(this.Parent);
            base.OnTextChanged(e);
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
            GridViewCell cell = sender as GridViewCell;
            if (cell == null)
                return false;
            if (e.Alt || e.Control || e.Shift)
            {

            }
            else
            {
                if (e.KeyData == Keys.Right)
                {
                    this.Parent.Grid.MoveFocusedCellToRightCell();
                }
                if (e.KeyData == Keys.Left)
                {
                    this.Parent.Grid.MoveFocusedCellToLeftCell();
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
        public void DrawCell(GridViewCell cell, Feng.Drawing.GraphicsObject g, RectangleF rect, object value)
        {
            GridView.DrawCellText(cell, g, rect, "******");
        }
        public virtual bool DrawCell(GridViewCell cell, Feng.Drawing.GraphicsObject g)
        {
            DrawCell(cell, g, cell.Rect, cell.Text);
            return true;
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
            Feng.Drawing.GraphicsObject gob = e.Graphic;
            DrawCell(cell, gob, rect, string.Empty);
            return true;
        }

        public virtual bool PrintValue(GridViewCell cell, PrintArgs e, RectangleF rect, object value)
        {
            Feng.Drawing.GraphicsObject gob = e.Graphic;
            DrawCell(cell, gob, rect, value);
            return false;
        }

        public virtual void TextPress(string text)
        {

        }
        public void CellValueChanged(GridViewCell cell)
        {

        }


        public void ReadDataStruct(DataStruct data)
        {
        }
    }

}
