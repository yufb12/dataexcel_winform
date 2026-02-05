using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Diagnostics;
using System.ComponentModel;

namespace Feng.Excel.Designer
{
    [Serializable]
    public class ColumnTypeConverter : TypeConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        { 
            return base.GetProperties(context, value, attributes);
        }
    }
}
