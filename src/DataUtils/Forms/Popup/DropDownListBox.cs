using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Popup;

namespace Feng.Forms.Controls
{
    [ToolboxItem(true)]
    public class DropDownListBox:DropDownBox 
    { 

        public DropDownListBox()
        {
            
        }

        private DropDownListForm _popupform = null;
        public override PopupForm PopupForm
        {
            get
            {
                if (_popupform == null)
                {
                    _popupform = new DropDownListForm();
                    base.InitForm(_popupform);
                }
                return _popupform;
            } 
        }
    }
 
}
