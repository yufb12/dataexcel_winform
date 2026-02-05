using Feng.Data;
using Feng.Excel.Commands;
using Feng.Excel.Interfaces;
using Feng.Forms.Interface;
using Feng.Forms.Views;
using Feng.Print;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellRadioCheckBox : CellBaseEdit, ITextID, IBoolValue
    {
        public CellRadioCheckBox(DataExcel grid)
            : base(grid)
        {

        }
        public override string ShortName { get { return "CellRadioCheckBox"; } set { } }

        private string _id = string.Empty;

        public virtual string ID
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

        private string _truevalue = string.Empty;
        public string _falsevalue = string.Empty;

        public virtual string TrueValue
        {
            get { return _truevalue; }
            set { _truevalue = value; }
        }

        public virtual string FalseValue
        {
            get { return _falsevalue; }
            set { _falsevalue = value; }
        }

        public override bool DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            DrawRadioCheckBox(cell, g, cell.Rect, cell.Value);
            return true;
        }

        public bool GetChecked(object value)
        {
            if (!(string.IsNullOrEmpty(TrueValue) && string.IsNullOrEmpty(FalseValue)))
            {
                if (value.Equals(TrueValue))
                {
                    return true;
                }
                return false;
            }
            return Feng.Utils.ConvertHelper.ToBoolean(value);
        }

        public void DrawRadioCheckBox(IBaseCell cell, Feng.Drawing.GraphicsObject g, Rectangle bounds, object value)
        {
            bool check = GetChecked(value);
            Rectangle rectt = new Rectangle(bounds.Left + 1, bounds.Top + 1, bounds.Width, bounds.Height);
            int x, y;
            x = rectt.Left + 1;
            y = rectt.Height / 2 - 6;
            System.Drawing.Drawing2D.GraphicsState gs = g.Graphics.Save();
            Rectangle rect = new Rectangle(bounds.Left + 2, bounds.Top + y, 13, 13);
            g.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            g.Graphics.DrawEllipse(Pens.RoyalBlue, rect);
            rect.Inflate(-1, -1);
            g.Graphics.FillEllipse(Brushes.White, rect);
            if (check)
            {
                rect.Inflate(-8, -8);
                g.Graphics.FillEllipse(Brushes.Green, rect);
            }
            g.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            Rectangle rectstr = new Rectangle(bounds.Left + 2 + 13, bounds.Top + 0, rectt.Width - 13, rectt.Height);
            StringFormat sf = StringFormat.GenericDefault.Clone() as StringFormat;
            sf.LineAlignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.EllipsisCharacter;
            if (!string.IsNullOrEmpty(cell.Caption))
            {
                using (SolidBrush brush = new SolidBrush(cell.ForeColor))
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g, cell.Caption, cell.Font, brush, rectstr, sf);
                }
            }
            g.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            g.Graphics.Restore(gs);
        }

        public override bool DrawCellBack(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            return false;
        }

        public override void Read(DataExcel grid, int version, DataStruct data)
        {
            ReadDataStruct(data);
        }

        public override void ReadDataStruct(DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader bw = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                this.AddressID = bw.ReadIndex(1, 0);
                this.ID = bw.ReadIndex(2, string.Empty);
                this.TrueValue = bw.ReadIndex(3, string.Empty);
                this.FalseValue = bw.ReadIndex(4, string.Empty);
            }
        }
        [Browsable(false)]
        public override DataStruct Data
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
                    bw.Write(2, this.ID);
                    bw.Write(3, this.TrueValue);
                    bw.Write(4, this.FalseValue);
                    data.Data = bw.GetData();
                }
                return data;
            }
        }

        public void SetGroupCellChecked(IBaseCell cell)
        {

            DataExcel grid = cell.Grid;
            grid.BeginReFresh();
            foreach (IRow row in grid.Rows)
            {
                foreach (IColumn col in grid.Columns)
                {
                    ICell c = row[col];
                    if (c != null)
                    {
                        if (c.OwnMergeCell != null)
                        {
                            c = c.OwnMergeCell;
                        }
                        if (c == cell)
                            continue;

                        if (c.OwnEditControl is CellRadioCheckBox)
                        {
                            CellRadioCheckBox crcb = c.OwnEditControl as CellRadioCheckBox;
                            if (crcb.ID == this.ID)
                            {
                                c.Value = GetValue(false);
                            }
                        }
                    }
                }
            }
            grid.EndReFresh();
        }

        public override bool OnMouseClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            IBaseCell cell = sender as IBaseCell;
            if (cell == null)
                return false;
            if (cell.ReadOnly)
                return false;
            bool check = GetChecked(cell.Value);
            if (cell.Grid.CanUndoRedo)
            {
                CellValueCommand cmd = new CellValueCommand();
                cmd.Text = cell.Text;
                cmd.Value = cell.Value;
                cmd.Cell = cell;
                cell.Grid.Commands.Add(cmd);
            }
            if (!check)
            {
                check = !check;
                cell.Value = GetValue(check);
                SetGroupCellChecked(cell);
                UpdateIDCell(cell.Grid, ID, cell.Value);
            }

            return false;
        }

        public object GetValue(bool res)
        {
            if (string.IsNullOrEmpty(TrueValue) && string.IsNullOrEmpty(FalseValue))
            {
                return res;
            }
            else
            {
                if (res)
                {
                    return TrueValue;
                }
                return FalseValue;
            }
        }

        public override bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
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
                    cell.Grid.MoveFocusedCellToRightCell();
                }
                if (e.KeyData == Keys.Left)
                {
                    cell.Grid.MoveFocusedCellToLeftCell();
                }
            }
            return false;
        }

        public override bool PrintCell(IBaseCell cell, PrintArgs e, Rectangle rect)
        {
            Feng.Drawing.GraphicsObject gob = e.Graphic;
            DrawRadioCheckBox(cell, gob, rect, cell.Value);
            return true;
        }

        public override bool PrintValue(IBaseCell cell, PrintArgs e, Rectangle rect, object value)
        {
            Feng.Drawing.GraphicsObject gob = e.Graphic;
            DrawRadioCheckBox(cell, gob, rect, value);
            return true;
        }

        public override ICellEditControl Clone(DataExcel grid)
        {
            CellRadioCheckBox celledit = new CellRadioCheckBox(grid);
            celledit._id = this._id;
            return celledit;
        }

        public virtual void UpdateIDCell(DataExcel grid, string id, object value)
        {
            if (grid != null)
            {
                ICell cell = grid.GetCellByID(id);
                if (cell != null)
                {
                    cell.Value = value;
                }
            }
        }
    }

}
