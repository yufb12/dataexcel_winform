using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Data;

namespace Feng.Print
{ 

    public interface IPrint
    {
        bool Print(PrintArgs e);
    }

    public interface IPrintValue
    {
        bool PrintValue(PrintArgs e, object value);
    }


    public interface IPrintBack
    {
        bool PrintBack(PrintArgs e);
    }

    public interface IPrintBorder
    {
        void PrintBorder(PrintArgs e);
    }
}