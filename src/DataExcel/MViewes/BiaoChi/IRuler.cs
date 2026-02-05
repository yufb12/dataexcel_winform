using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Printing;
using Feng.Forms.Interface;
using Feng.Data;
using Feng.Args;
using Feng.Excel.Interfaces;
using Feng.Excel.App;

namespace Feng.Excel.Interfaces
{
    public interface IRuler : IGrid, IFont, IReadOnlyMax, IReadOnlyMin, IVisible, IDraw,
        IDataStruct, IPlusAssembly, IBounds, IBorderColor, IForeColor 
    {


    }
 

}
