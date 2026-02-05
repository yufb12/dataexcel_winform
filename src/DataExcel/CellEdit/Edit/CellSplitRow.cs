using Feng.Excel.Interfaces;
using Feng.Forms.Views;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellSplitRow : CellLabel
    {
        public CellSplitRow(DataExcel grid)
            : base(grid)
        {

        }
        private bool move = false;
        private Point downpoint = Point.Empty;
        int height = 0;
        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            ICell cell = sender as ICell;
            if (cell != null)
            {
                Point viewloaction = this.Grid.PointControlToView(e.Location);
                if (cell.Rect.Contains(viewloaction))
                {
                    move = true;
                    downpoint = System.Windows.Forms.Control.MousePosition;
                    height = cell.Row.Height;
                    return true;
                }
            }
            return base.OnMouseDown(sender, e, ve);
        }
        public override bool OnMouseUp(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            move = false;
            downpoint = Point.Empty;
            return base.OnMouseUp(sender, e, ve);
        }
        public override bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            
            ICell cell = sender as ICell;
            if (cell != null)
            {
                Point viewloaction = this.Grid.PointControlToView(e.Location);
                if (!move)
                {
                    if (cell.Rect.Contains(viewloaction))
                    {
                        cell.Grid.BeginSetCursor(Cursors.HSplit);
                        return true;
                    }
                }
                else
                {
                    if (e.Button == MouseButtons.Left)
                    {

                        int x = System.Windows.Forms.Control.MousePosition.X - downpoint.X;
                        int y = System.Windows.Forms.Control.MousePosition.Y - downpoint.Y;
                        cell.Row.Height = height + y;
                        cell.Grid.ReFreshFirstDisplayRowIndex();
                        ve.Invalate = true;
                        return true;
                    }
                } 
            }
            return base.OnMouseMove(sender, e, ve);
        }
        public override ICellEditControl Clone(DataExcel grid)
        {
            CellSplitRow celledit = new CellSplitRow(grid);
            return celledit;
        }
    }

}
