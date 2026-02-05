using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;

namespace Feng.Print
{
    public abstract class PrintPageBase : IPrint
    {
        private bool _Handled = false;
        public virtual bool Handled
        {
            get
            {
                return _Handled;
            }
            set
            {
                _Handled = value;
            }
        }
        private Rectangle _rect = Rectangle.Empty;
        public virtual Rectangle Rect
        {
            get { return _rect; }
            set
            {
                _rect = value;
            }
        }

        public abstract bool Print(PrintArgs e);
    }
}
