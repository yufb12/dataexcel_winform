using Feng.Data;
using Feng.Drawing;
using Feng.Enums;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Forms.Views
{
    public class ImageView : DivView
    {
        public ImageView()
        {

        }
        public Image Image { get; set; }
        public override bool OnDraw(object sender, GraphicsObject g)
        {
            g.Graphics.DrawImage(this.Image, this.Bounds);
            return base.OnDraw(sender, g);
        }

        public override DataStruct Data { get { return new DataStruct(); } }
 

    }
}

