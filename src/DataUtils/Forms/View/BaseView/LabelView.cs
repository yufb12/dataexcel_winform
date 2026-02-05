using Feng.Data;
using Feng.Drawing;

namespace Feng.Forms.Views
{
    public class LabelView : DivView
    {
        public LabelView()
        {

        }

        public override bool OnDraw(object sender, GraphicsObject g)
        {
            return base.OnDraw(sender, g);
        }

        public override DataStruct Data { get { return new DataStruct(); } }


    }
}

