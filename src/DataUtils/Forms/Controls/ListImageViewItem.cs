using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D; 
using Feng.Forms.Controls.Designer;

namespace Feng.Forms.Controls
{
    [ToolboxItem(false)]
    public class ListImageViewItem  // , IList<ToolBarItem>
    {
        public ListImageViewItem()
        {

        }
        public void OnDraw(Graphics g)
        {
        }
        private ListImageView _parent = null;
        public ListImageView Parnt
        {
            get
            {
                return this._parent;
            }
            set
            {
                this._parent = value;
            }
        }
    }

}
