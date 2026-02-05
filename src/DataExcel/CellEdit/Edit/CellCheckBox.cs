using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Feng.Excel.Designer;
using System.Drawing.Design;
using Feng.Utils;
using Feng.Print;
using Feng.Drawing;
using Feng.Data;
using Feng.Excel.Interfaces;
using Feng.Excel.Commands;
using Feng.Forms.Interface;
using Feng.Forms.Views;

namespace Feng.Excel.Edits
{ 
    public class CellCheckBox : CellBaseEdit, ITextID
    {
        public CellCheckBox() 
        {  
        }

        public override string ShortName { get { return "CellCheckBox"; } set { } }
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
            Rectangle bounds = cell.Rect;
            DrawCheckBox(cell, g, bounds, cell.Value);
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
        private void DrawCheckBox(IBaseCell cell, Feng.Drawing.GraphicsObject g, Rectangle bounds, object value)
        {
            bool check = GetChecked(value);
            StringFormat sf = new StringFormat();
            sf.FormatFlags = StringFormatFlags.DisplayFormatControl;
            sf.Trimming = StringTrimming.EllipsisCharacter;
            sf.Alignment = cell.HorizontalAlignment;
            sf.LineAlignment = cell.VerticalAlignment;
 
            string txt = cell.Caption;
            GraphicsHelper.DrawCheckBox(g.Graphics, bounds, check ? 1 : 0, txt, sf, cell.ForeColor, cell.Font);

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
                cmd.Value = cell.Value;
                cmd.Text = cell.Text;
                cmd.Cell = cell;
                cell.Grid.Commands.Add(cmd);
            }
            check = !check;
            cell.Value = GetValue(check);

            UpdateIDCell(cell.Grid);
            return false;
        }

        public virtual void UpdateIDCell(DataExcel grid)
        { 
            if (grid == null)
                return;
            StringBuilder sb = new StringBuilder();
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
                        if (c.OwnEditControl is CellRadioCheckBox)
                        {
                            CellRadioCheckBox crcb = c.OwnEditControl as CellRadioCheckBox;
                            if (crcb.ID == this.ID)
                            {
                                sb.Append(Feng.Utils.ConvertHelper.ToString(c.Value));
                            }
                        }
                    }
                }
            }
            ICell cell = grid.GetCellByID(this.ID);
            if (cell != null)
            {
                cell.Value = sb.ToString();
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
                if (e.KeyCode == Keys.Space)
                {
                    if (cell.ReadOnly)
                        return false;
                    bool check = GetChecked(cell.Value);
                    if (cell.Grid.CanUndoRedo)
                    {
                        CellValueCommand cmd = new CellValueCommand();
                        cmd.Value = cell.Value;
                        cmd.Text = cell.Text;
                        cmd.Cell = cell;
                        cell.Grid.Commands.Add(cmd);
                    }
                    check = !check;
                    cell.Value = GetValue(check);
                    return true;
                }
            }
            return false;
        }

        public override bool PrintCell(IBaseCell cell, PrintArgs e, Rectangle rect)
        {
            Graphics g = e.PrintPageEventArgs.Graphics;
            Feng.Drawing.GraphicsObject gob = e.Graphic;
            DrawCheckBox(cell, gob, rect, cell.Value);
            return true;
        }

        public override bool PrintValue(IBaseCell cell, PrintArgs e, Rectangle rect, object value)
        {
            Graphics g = e.PrintPageEventArgs.Graphics;
            Feng.Drawing.GraphicsObject gob = e.Graphic;
            DrawCheckBox(cell, gob, rect, cell.Value);
            return true;
        }

        public override ICellEditControl Clone(DataExcel grid)
        {
            CellCheckBox celledit = new CellCheckBox();

            return celledit;
        }
    }

}
