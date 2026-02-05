using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D; 
using Feng.Forms.Controls.Designer;

namespace Feng.Forms.Command
{
    public class CompositeKeyDescription
    { 
        public virtual string GetDescription(string command)
        { 
            return string.Empty;
        }
    }
}
