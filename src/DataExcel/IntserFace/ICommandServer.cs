using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Excel.Interfaces
{ 
    public interface IRectF
    {
        System.Drawing.Rectangle Rect { get; set; }

    }

    public interface IColor
    {
        System.Drawing.Color Color { get; set; }
    }

    public interface IImage
    {
        System.Drawing.Image Image { get; set; }
    }

}
