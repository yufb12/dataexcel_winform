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
using Feng.Forms.Controls;
using Feng.Data;
using Feng.Excel.Interfaces;
using Feng.Forms.Controls.Designer;
using Feng.Excel.Commands;
using Feng.Forms.Views;

namespace Feng.Excel.Edits
{ 
    public class CellCnCurrency : CellBaseEdit, IEdit,ICellEditControl
    {
        public override string ShortName { get { return "CellCnCurrency"; } set { } }
        private NumTextBox edit = null;

        public CellCnCurrency(DataExcel grid):
            base(grid)
        {
            edit = new NumTextBox();
            edit.TextChanged += Edit_TextChanged;
        }

        private void Edit_TextChanged(object sender, EventArgs e)
        {
            try
            {
                this.Grid.OnCellEditControlValueChanged(this.Cell, this.Text);
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmMain2", "statusSum_Click", ex);
            }
        }
 
        public override bool InitEdit(object obj)
        {
            try
            {
                ICell cell = obj as ICell;
                if (cell == null)
                    return false; 
                this.Cell = cell;
                if (this.Cell == null)
                    return false;
                this.Left = (int)cell.Left + 1;
                this.Top = (int)cell.Top + 1;
                this.Width = (int)cell.Width - 1;
                this.Height = (int)cell.Height - 1;
                this.edit.Visible = true;
                this.edit.Multiline = true;
                this.Grid.AddEdit(this);
                this.Grid.AddControl(this.edit);
                Point viewloaction = this.Grid .PointViewToControl(cell.Location);
                this.edit.Left = (int)viewloaction.X + 1;
                this.edit.Top = (int)viewloaction.Y + 1;
                this.edit.Width = (int)cell.Width - 1;
                this.edit.Height = (int)cell.Height - 1;
                this.edit.Visible = true;
                if (!string.IsNullOrEmpty(cell.Expression))
                {
                    this.Text = "=" + cell.Expression;
                }
                else
                {
                    this.Text = cell.Text;
                }
                this.edit.BorderStyle = System.Windows.Forms.BorderStyle.None;
                this.edit.Focus(); 
                return true;
            }
            finally
            {
            }
        }
 
        public override void EndEdit()
        {
            try
            {
                this.edit.Hide();
                if (this.Cell != null)
                {
                    if (this.Cell.Grid.CanUndoRedo)
                    {
                        CellValueCommand cmd = new CellValueCommand();
                        cmd.Text = this.Cell.Text;
                        cmd.Value = this.Cell.Value;
                        cmd.Cell = this.Cell;
                        this.Cell.Grid.Commands.Add(cmd);
                    }
                    this.Cell.Value = this.edit.Value1;
                    this.Cell.Text = this.edit.Text;
                    this.Grid.EndEditClear();
                    this.Grid.EndEdit();
                }

                this.Cell = null;
            }
            catch (Exception ex)
            {
                
            }
            finally
            {
                this._inedit = false;
            }
        }

        public virtual bool DrawCell(IBaseCell cell, PrintArgs g)
        {
            return false;
        }

        public virtual bool DrawCellBack(IBaseCell cell, PrintArgs g)
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
                this._id = bw.ReadIndex(1, 0);
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
                    data.Data = bw.GetData();
                }
                return data;
            }
        }
 
        public override bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
        {
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
 
        public string GetDecimalText(decimal value)
        {
            string text = string.Empty;
            if (value >= 1)
            {
                text = value.ToString("0.00");
                text = text.Replace(".", "");
            }
            else
            {
                text = value.ToString("#.00");
                text = text.Replace(".", "");
                text = text.TrimStart('0');
            }
            return text;
        }
        public override bool DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            string text = GetDecimalText(Feng.Utils.ConvertHelper.ToDecimal(cell.Value));
            DrawCell(cell, g, cell.Rect, text, false);
            return true;
        }
        private int _max = 9;
        public int Max
        {
            get { return this._max; }
            set { this._max = value; }
        }
        private int _min = 0;
        public int Min
        {
            get { return this._min; }
            set { this._min = value; }
        }

        private int _rowheight = 16;
        [Browsable(true), DefaultValue(16)]
        public int RowHeight
        {
            get { return this._rowheight; }
            set { this._rowheight = value; }
        }
        private string[] dics = new string[] { "分", "角", "元", "十", "百", "千", "万", "十", "百", "千", "亿", "十", "百", "千" };
        public void DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g, Rectangle bounds, string text, bool isPrint)
        {
            int count = this._max - this._min + 1;
            List<Rectangle> list = new List<Rectangle>();
            int width = bounds.Width / count;
            int left = bounds.Left;
            Point pt1 = new Point(bounds.Left, bounds.Top + RowHeight);
            Point pt2 = new Point(bounds.Right, bounds.Top + RowHeight);

            g.Graphics.DrawLine(Pens.Black, pt1, pt2);

            StringFormat sf = new StringFormat();
            sf.FormatFlags = StringFormatFlags.DisplayFormatControl;
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            for (int i = _max; i >= _min; i--)
            {
                Rectangle rect = new Rectangle();
                rect.Width = width;
                rect.Height = RowHeight > bounds.Height ? bounds.Height : RowHeight;
                rect.X = left;
                rect.Y = bounds.Top + 1;

                Feng.Drawing.GraphicsHelper.DrawString(g,this.dics[i], cell.Font, Brushes.Black, rect, sf);

                rect = new Rectangle();
                rect.Width = width;
                rect.Height = bounds.Height - RowHeight;
                rect.X = left;
                rect.Y = bounds.Top + RowHeight + 1;
                if (text.Length > i)
                {
                    string ss = text[text.Length - i - 1].ToString();
                    Feng.Drawing.GraphicsHelper.DrawString(g,ss, cell.Font, Brushes.Black, rect, sf);
                }

                pt1 = new Point(left + width, bounds.Top);
                pt2 = new Point(left + width, bounds.Top + bounds.Height);

                g.Graphics.DrawLine(Pens.Black, pt1, pt2);
                left = left + width;
            }


        }
 
        private int _id = -1;
        [Browsable(false)]
        public override int AddressID
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

        public override bool PrintCell(IBaseCell cell, PrintArgs e, Rectangle rect)
        {
            string text = GetDecimalText(Feng.Utils.ConvertHelper.ToDecimal(cell.Value));
            Feng.Drawing.GraphicsObject gob = e.Graphic;
            DrawCell(cell, gob, rect, text, false);
            return true;
        }

        public override bool PrintValue(IBaseCell cell, PrintArgs e, Rectangle rect, object value)
        {
            string text = GetDecimalText(Feng.Utils.ConvertHelper.ToDecimal(cell.Value));
            Feng.Drawing.GraphicsObject gob = e.Graphic;
            DrawCell(cell, gob, rect, text, false);
            return true;
        }
 
        public override ICellEditControl Clone(DataExcel grid)
        {
            CellCnCurrency celledit = new CellCnCurrency(grid); 
            celledit._max = this._max;
            celledit._min = this._min;
            celledit._rowheight = this._rowheight;
            return celledit;
        }
 
    }

}
