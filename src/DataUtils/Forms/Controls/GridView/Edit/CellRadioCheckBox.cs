using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel; 
using System.Drawing.Design;
using Feng.Utils;
using Feng.Print;
using Feng.Data; 

namespace Feng.Forms.Controls.GridControl.Edits
{
    [Serializable]
    public class CellRadioCheckBox : CellBaseEdit, IGridViewCellValueChanged
    {
        public CellRadioCheckBox()
        { 

        } 
 
        private string _group = string.Empty;
      
        public virtual string Group {
            get {
                return _group;
            }

            set {
                _group = value;
            }
        }

        public override bool DrawCell(GridViewCell cell, Feng.Drawing.GraphicsObject g)
        {
            DrawRadioCheckBox(cell, g, cell.Rect, cell.Value);
            return true;
        }

        public void DrawRadioCheckBox(GridViewCell cell, Feng.Drawing.GraphicsObject g, RectangleF bounds, object value)
        {
            bool check = Feng.Utils.ConvertHelper.ToBoolean(value);
            RectangleF rectt = new RectangleF(bounds.Left + 1, bounds.Top + 1, bounds.Width, bounds.Height);
            float x, y;
            x = rectt.Left + 1;
            y = rectt.Height / 2 - 6;
            System.Drawing.Drawing2D.GraphicsState gs = g.Graphics.Save();
            RectangleF rect = new RectangleF(bounds.Left + 2, bounds.Top + y, 13F, 13F);
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
            RectangleF rectstr = new RectangleF(bounds.Left + 2 + 13, bounds.Top + 0, rectt.Width - 13, rectt.Height);
            StringFormat sf = StringFormat.GenericDefault.Clone() as StringFormat;
            sf.LineAlignment = StringAlignment.Center;
            sf.Trimming = StringTrimming.EllipsisCharacter;
            if (!string.IsNullOrEmpty(cell.Text))
            {
                using (SolidBrush brush = new SolidBrush(cell.Grid.ForeColor))
                {
                    Feng.Drawing.GraphicsHelper.DrawString(g,cell.Text, cell.Grid.Font, brush, rectstr, sf);
                }
            }
            g.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            g.Graphics.Restore(gs);
        }
        public override bool DrawCellBack(GridViewCell cell, Feng.Drawing.GraphicsObject g)
        {
            return false;
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
        public void SetGroupCellChecked(GridViewCell cell)
        { 
            cell.Value = false; 
        }
        public override bool OnMouseClick(object sender, MouseEventArgs e)
        {
            GridViewCell cell = sender as GridViewCell;
            if (cell == null)
                return false;
            if (cell.Column.ReadOnly)
                return false;
            bool check = Feng.Utils.ConvertHelper.ToBoolean(cell.Value); 
            if (!check)
            {
                check = !check;
                cell.Value = check;
                SetGroupCellChecked(cell);

                CellValueChanged(cell);
            }
            
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
            Feng.Drawing.GraphicsObject gob =  e.Graphic;
            DrawRadioCheckBox(cell, gob, rect, cell.Value);
            return true;
        }

        public override bool PrintValue(GridViewCell cell, PrintArgs e, RectangleF rect, object value)
        {
            Feng.Drawing.GraphicsObject gob =   e.Graphic;
            DrawRadioCheckBox(cell, gob, rect, value);
            return true;
        }


    }

}
