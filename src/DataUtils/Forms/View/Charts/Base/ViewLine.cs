using Feng.Forms.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms.Views.Charts
{

    public class ViewLine : ILineColor, ILineWidth
    { 
        public Color LineColor { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public float LineWidth { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }


}
