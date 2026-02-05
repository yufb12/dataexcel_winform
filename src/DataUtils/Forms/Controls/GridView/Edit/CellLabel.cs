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
    public class CellLabel : CellBaseEdit
    {
        public CellLabel() 
        {  

        }
  
 
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
            DrawCell(cell, e.Graphic, cell.Rect, cell.Text);
            return true;
        }

        public override bool PrintValue(GridViewCell cell, PrintArgs e, RectangleF rect, object value)
        {
            if (value != null)
            {
                DrawCell(cell, e.Graphic, cell.Rect, ConvertHelper.ToString(value));
            }
            return true;
        }
 
    }

}
