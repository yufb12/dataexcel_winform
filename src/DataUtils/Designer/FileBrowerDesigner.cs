using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using Feng.IO;

namespace Feng.Forms.Controls.Designer
{
    public class FileBrowerDesigner : System.Drawing.Design.UITypeEditor
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
                using (System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog())
                {
                    string text = Feng.Utils.ConvertHelper.ToString(value);
                    dlg.FileName = text;
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        return dlg.FileName;
                    }
                }
            }

            return base.EditValue(context, provider, value);
        }
    }
}