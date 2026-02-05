using Feng.Data;
using Feng.Drawing;

namespace Feng.Forms.Views
{
    public class ImageTextView : DivView
    {
        public ImageTextView()
        {

        }
        private ImageView imageview = null;
        private LabelView labelview = null;

        public override bool OnDraw(object sender, GraphicsObject g)
        {
            return base.OnDraw(sender, g);
        }

        public override DataStruct Data { get { return new DataStruct(); } }


    }
}

