using Feng.Drawing;
using Feng.Enums;
using Feng.Forms.Interface;
using Feng.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public class yAxisItem : ViewItem
    {
        public yAxisItem()
        { 

        }

        public TextLabel TextLabel { get; set; }

        public override bool OnDraw(object sender, GraphicsObject g)
        {
            g.Graphics.DrawLine(Pens.Black, this.Right - 5, this.Bottom, this.Right, this.Bottom);
            return base.OnDraw(sender, g);
        }


    }

}
