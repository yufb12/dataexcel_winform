using Feng.Forms.Interface;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms.Views.Charts
{

    public class ViewRect:IRect,IPadding,IMargins
    {
        public ViewPosition Position { get;set; }
        public int Left { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Height { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Right => throw new NotImplementedException();
        public int Bottom => throw new NotImplementedException();
        public int Top { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Width { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Rectangle Rect => throw new NotImplementedException();
        public Padding Padding { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Margins Margins { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }

    public enum ViewPosition
    {
        PositionStatic,
        PositionRelative,
        PositionFixed,
        PositionAbsolute,
        PositionSticky,
    }
}
