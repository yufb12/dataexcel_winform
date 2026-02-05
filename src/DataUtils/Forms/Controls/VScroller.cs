using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;

using Feng.Drawing;

namespace Feng.Forms
{

     [ToolboxItem(false)]
    public class VScroller : System.Windows.Forms.VScrollBar
    {
        protected override void OnScroll(ScrollEventArgs se)
        {
            if (se.Type == ScrollEventType.SmallIncrement)
            {
                base.Maximum = base.Maximum + 1;
            }
            base.OnScroll(se);
        }
        public new int Maximum
        {
            get
            {
                return base.Maximum;
            }
            set
            {
                base.Maximum = value;
            }
        }
        public new int Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                if (value < 1)
                {
                    base.Value = 100;
                }
                else
                {
                    base.Value = value;
                } 
            }
        }
    }

}
