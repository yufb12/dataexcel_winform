using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D; 
using Feng.Forms.Controls.Designer;

namespace Feng.Forms
{ 
    public class ImageCache // , IList<ToolBarItem>
    {
        static ImageCache()
        {
            mulimage = Feng.Utils.Properties.Resources.celleditmul;
        }
        private static Image mulimage = null;
        public static Image MulImage { get { return mulimage; } }

    }

}
