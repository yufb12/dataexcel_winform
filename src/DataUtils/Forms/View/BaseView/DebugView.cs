using Feng.Data;
using Feng.Drawing;
using System.Windows.Forms;

namespace Feng.Forms.Views
{
    public class DebugView : DivView
    {
        public DebugView()
        {

        }

        public override bool OnDraw(object sender, GraphicsObject g)
        {
            g.Graphics.DrawString(sb.ToString(), g.DefaultFont, System.Drawing.Brushes.Black, this.Left, this.Top);
            return base.OnDraw(sender, g);
        }
        public void AppendText(string txt)
        {
            this.sb.AppendLine(txt);
        }
        public override DataStruct Data { get { return new DataStruct(); } }
        private System.Text.StringBuilder sb = new System.Text.StringBuilder();
        public override bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            sb.Length = 0;
            sb.AppendFormat("ViewPoint={0},ControlPoint={1},x={2},y={3} \r\n", ve.ViewPoint, ve.ControlPoint,ve.X,ve.Y);
            return base.OnMouseMove(sender, e, ve);
        }
    }
}

