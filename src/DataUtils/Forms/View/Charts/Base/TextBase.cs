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
    public class TextView : DivView, ITextRect
    {

        public virtual StringAlignment HorizontalAlignment { get; set; }
        public virtual StringAlignment VerticalAlignment { get; set; }
        public virtual bool DirectionVertical { get; set; }
        public override bool OnSetTextView(object sender, bool isfont, Font font, bool isforecolor, Color color, bool isbackcolor, Color backcolor, EventArgs e, EventViewArgs ve)
        {
            return base.OnSetTextView(sender, isfont, font, isforecolor, color, isbackcolor, backcolor, e, ve);
        }
        public override bool OnDraw(object sender, GraphicsObject g)
        {
            Feng.Drawing.GraphicsHelper.DrawText(g.Graphics, this.Font, this.Text, this.ForeColor, this.Rect, true, VerticalAlignment, HorizontalAlignment);
            return base.OnDraw(sender, g);
        }
    }

}
