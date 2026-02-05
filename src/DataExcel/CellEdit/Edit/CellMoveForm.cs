using Feng.Excel.Interfaces;
using Feng.Forms.Views;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellMoveForm : CellLabel
    {
        public CellMoveForm(DataExcel grid)
            : base(grid)
        {

        }
        private bool move = false;
        private Point downpoint = Point.Empty;
        private Point formpoint = Point.Empty;
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
                    formpoint = this.Grid.FindForm().Location;
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
            //ICell cell = sender as ICell;
            //if (cell != null)
            //{
            //    if (cell.Rect.Contains(e.Location))
            //    {
            if (e.Button == MouseButtons.Left)
            {
                if (move)
                {
                    int x = System.Windows.Forms.Control.MousePosition.X - downpoint.X;
                    int y = System.Windows.Forms.Control.MousePosition.Y - downpoint.Y;
                    this.Grid.FindForm().Location = new Point(formpoint.X + x, formpoint.Y + y);
                    return true;
                }
            }
            //    }
            //}
            return base.OnMouseMove(sender, e, ve);
        }
        public override ICellEditControl Clone(DataExcel grid)
        {
            CellMoveForm celledit = new CellMoveForm(grid);
            return celledit;
        }
    }

}
