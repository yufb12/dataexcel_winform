using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Diagnostics;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Designer
{
    [Serializable]
    
    public class ColumnDesigner : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
    
        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IColumn col = context as IColumn;
            if (col != null)
            {
                string strindex = string.Format("{0}", col.Index);
                return strindex;
            }
            return base.EditValue(context, provider, value);
        } 
    }
}
