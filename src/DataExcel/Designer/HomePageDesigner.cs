using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Diagnostics;
using Feng.Excel.App;

namespace Feng.Excel.Designer
{
    [Serializable]
    public class HomePageDesigner : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Process.Start(Product.AssemblyHomePage);
            return base.EditValue(context, provider, value);
        }

    }
}
