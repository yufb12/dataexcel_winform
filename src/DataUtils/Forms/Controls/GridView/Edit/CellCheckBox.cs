using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel; 
using System.Drawing.Design;
using Feng.Utils;
using Feng.Print;
using Feng.Drawing;
using Feng.Data;

namespace Feng.Forms.Controls.GridControl.Edits
{ 
    public class CellCheckBox : CellBaseEdit, Feng.Forms.Interface.IEditControl
    {
        public CellCheckBox()
        {
        }

        public override bool DrawCell(GridViewCell cell, Feng.Drawing.GraphicsObject g)
        {
            RectangleF bounds = cell.Rect;
            DrawCheckBox(cell, g, bounds, cell.Value);
            return true;
        }

        private void DrawCheckBox(GridViewCell cell, Feng.Drawing.GraphicsObject g, RectangleF bounds, object value)
        {
            bool check = Feng.Utils.ConvertHelper.ToBoolean(value);
            StringFormat sf = new StringFormat();
            sf.FormatFlags = StringFormatFlags.DisplayFormatControl;
            sf.Trimming = StringTrimming.EllipsisCharacter;
            sf.Alignment = cell.Column.HorizontalAlignment;
            sf.LineAlignment = cell.Column.VerticalAlignment;

            string txt = cell.Text;
            GraphicsHelper.DrawCheckBox(g.Graphics, bounds, check ? 1 : 0, txt, sf, cell.Grid.ForeColor, cell.Grid.Font);

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
                 
                return data;
            }
        }

        public override bool OnMouseClick(object sender, MouseEventArgs e)
        {
            GridViewCell cell = sender as GridViewCell;
            if (cell == null)
                return false;
            if (cell.Column.ReadOnly)
                return false;
            bool check = Feng.Utils.ConvertHelper.ToBoolean(cell.Value);
 
            check = !check;
            cell.Value = check;
            cell.Grid.OnCellValueChanged(cell);
            return false;
        }

        public override bool OnKeyDown(object sender, KeyEventArgs e)
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
                    cell.Grid.MoveFocusedCellToRightCell();
                }
                if (e.KeyData == Keys.Left)
                {
                    cell.Grid.MoveFocusedCellToLeftCell();
                }
            }
            return false;
        }

        public override bool PrintCell(GridViewCell cell, PrintArgs e, RectangleF rect)
        {
            Graphics g = e.PrintPageEventArgs.Graphics;
            Feng.Drawing.GraphicsObject gob = e.Graphic;
            DrawCheckBox(cell, gob, rect, cell.Value);
            return true;
        }

        public override bool PrintValue(GridViewCell cell, PrintArgs e, RectangleF rect, object value)
        {
            Graphics g = e.PrintPageEventArgs.Graphics;
            Feng.Drawing.GraphicsObject gob = e.Graphic;
            DrawCheckBox(cell, gob, rect, cell.Value);
            return true;
        }

    }

}
