using Feng.Drawing;
using Feng.Enums;
using Feng.Forms.Interface;
using Feng.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public class ViewItem : TextView, IValue
    {
        public ViewItem()
        {

        }

        public virtual object Value { get; set; } 
    }
 
}
