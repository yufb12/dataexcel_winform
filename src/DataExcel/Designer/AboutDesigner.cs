using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using Feng.App;
using Feng.Excel.App;

namespace Feng.Excel.Designer
{
    [Serializable]
    public class AboutDesigner : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            AboutBox dlg = new AboutBox();
            dlg.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            dlg.ShowDialog();
            return base.EditValue(context, provider, value);
        }

    }

    [Serializable]
    public class UpdateInfoDesigner : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.Modal;
        }
 

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null
                && context.Instance != null
                && provider != null)
            {

                IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                DataExcel box = context.Instance as DataExcel;
                Type ty = context.Instance.GetType();
                if (edSvc != null)
                {
                    if (box != null)
                    {
                        using (UpdateInfoForm frm = new UpdateInfoForm())
                        {
                            if (box != null)
                            {
                                UpdateInfo obj = box.UpdateInfo;
                                frm.SetText(obj); 
                            }
                            frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                return frm.GetText();
                            }
                        }
                    }
                    else
                    {
                        UpdateInfo obj = context.Instance as UpdateInfo;
                        if (obj != null)
                        {
                            using (UpdateInfoForm frm = new UpdateInfoForm())
                            {
                                frm.SetText(obj);
                                frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                {
                                    return frm.GetText();
                                }
                            }
                        }
                    }

                }
            }

            return value;
        }
 

    }
}
