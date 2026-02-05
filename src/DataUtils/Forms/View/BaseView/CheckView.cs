using Feng.Data;
using Feng.Drawing;
using Feng.Enums;
using Feng.Forms.Interface;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Forms.Views
{
    public class CheckView2 : DivView,IChecked
    {
        public CheckView2()
        {
            
        }
        public bool Checked { get; set; }
        public override bool OnDraw(object sender, GraphicsObject g)
        {
            GraphicsHelper.DrawCheckBox(g.Graphics, this.Rect, Checked ? 1 : 0);
            return base.OnDraw(sender, g);
        }

        public override DataStruct Data { get { return new DataStruct(); } }
 

    }
}

