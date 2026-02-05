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
using System.Security.Permissions;

using Feng.Utils;
using Feng.Data;

namespace Feng.Forms.ComponentModel
{

    public class ImageTextValue  
    {
        public virtual string Text { get; set; }

        public virtual Bitmap Image { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

  
}

