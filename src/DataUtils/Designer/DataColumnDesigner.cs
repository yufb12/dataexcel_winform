using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;

namespace Feng.Forms.Controls.Designer
{ 

    [Serializable]
    public class TreeViewNodesDesigner : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
            if (edSvc != null)
            {
                using (Feng.Forms.TreeViewDialog scs = new Forms.TreeViewDialog())
                {
                    string text = Feng.Utils.ConvertHelper.ToString(value); 
                    if (scs.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        return value;
                    }
                }
            }
  
            return base.EditValue(context, provider, value);
        }


    }
}
