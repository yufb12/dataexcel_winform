using Feng.Data;
using Feng.Drawing;

namespace Feng.Forms.Views
{
    public class RectangleView : DivView
    {
        public RectangleView()
        {

        }

        public override bool OnDraw(object sender, GraphicsObject g)
        {
            System.Drawing.Drawing2D.GraphicsState gs = g.Graphics.Save();
            g.Graphics.TranslateTransform(this.Left, this.Top);
            g.Graphics.SetClip(this.Rect);
            bool res = base.OnDraw(sender, g);
            g.Graphics.Restore(gs);
            return res;
        }

        public override DataStruct Data { get { return new DataStruct(); } }


    }
}

