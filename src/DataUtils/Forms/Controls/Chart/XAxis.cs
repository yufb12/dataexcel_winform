using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Interface;
 

namespace Feng.Forms.Controls
{
    [ToolboxItem(false)]
    public class XAxis : Axis
    {
        public XAxis(Pane pane)
        {
            _pane = pane;
        }

        public override bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            return false;
        }
        private Pane _pane = null;
        public Pane Pane
        {
            get {
                return _pane;
            }
        }
    }

}
