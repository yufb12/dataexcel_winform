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
    public class xAxisItem : ViewItem
    {
        public xAxisItem()
        { 

        }

        public TextLabel TextLabel { get; set; }

        public override bool OnDraw(object sender, GraphicsObject g)
        {
            g.Graphics.DrawLine(Pens.Black, this.Left, this.Top, this.Left, this.Top + 5);
            return base.OnDraw(sender, g);
        }


    }

}
