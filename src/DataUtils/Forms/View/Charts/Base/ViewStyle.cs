using Feng.Forms.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms.Views.Charts
{

    public class ViewStyle : IFont, IForeColor, IBackColor, IFocusForeColor, IFocusBackColor, IFocusImage
    {
        public Font Font { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color ForeColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color BackColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color FocusForeColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Color FocusBackColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Bitmap FocusImage { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public ImageLayout FocusImageSizeMode { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }


}
