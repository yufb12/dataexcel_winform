using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feng.Forms.Controls
{
    public class EditBox : System.Windows.Forms.TextBox
    {
        public EditBox() : base()
        {
            this.AcceptsReturn = true;
            this.AcceptsTab = true;
            this.WordWrap = false;
        }
    }
}
