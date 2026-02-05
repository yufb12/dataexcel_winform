using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Drawing;

namespace Feng.Forms.Skins
{
    public class ScrollerSkin
    {
        private Color _backareacolor = System.Drawing.SystemColors.Control;
        public virtual Color BackAreaColor
        {
            get { return _backareacolor; }
            set { _backareacolor = value; }
        }
        private Color _Arrowcolor = System.Drawing.SystemColors.Control;
        public virtual Color ArrowColor
        {
            get { return _Arrowcolor; }
            set { _Arrowcolor = value; }
        }
        private Color _backdirectioncolor = System.Drawing.SystemColors.ControlDarkDark;
        public virtual Color BackDirectionColor
        {
            get { return _backdirectioncolor; }
            set { _backdirectioncolor = value; }
        }
        private Color _ThumdBackcolor = System.Drawing.SystemColors.ControlDark;
        public virtual Color ThumdBackColor
        {
            get { return _ThumdBackcolor; }
            set { _ThumdBackcolor = value; }
        }
        private Color _SelectThumdBackcolor = Color.SkyBlue;
        public virtual Color SelectThumdBackcolor
        {
            get { return _SelectThumdBackcolor; }
            set { _SelectThumdBackcolor = value; }
        }

    }
 
}
