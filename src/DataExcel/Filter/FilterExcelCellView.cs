using Feng.Drawing;
using Feng.Forms.Views;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Fillter
{

    public class FilterExcelCellView : Feng.Forms.Views.DivView
    {
        public override bool OnDraw(object sender, GraphicsObject g)
        {
            Rectangle rect = new Rectangle(this.Left, this.Top, this.Width, this.Height);
            g.Graphics.DrawImage(Feng.Drawing.Images.celleditdropdown, rect);
            return false;
        }

        public override bool OnMouseClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if (this.Rect.Contains(ve.ViewPoint))
            {
                return true;
            }
            return false;
        }
    }

}
