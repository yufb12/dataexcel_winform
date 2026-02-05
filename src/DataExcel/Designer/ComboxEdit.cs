using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using Feng.Excel.Edits;

namespace Feng.Excel.Designer
{
    public class ComboxEdit : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
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
                object[] box = context.Instance as object[];
                Type ty = context.Instance.GetType();
                if (edSvc != null)
                {
                    if (box != null)
                    {
                        using (ComboxEditForm frm = new ComboxEditForm())
                        {
                            if (box != null)
                            {
                                if (box.Length > 0)
                                {
                                    CellComboBox obj = box[0] as CellComboBox;
                                    List<string> items = new List<string>();
                                    foreach (object m in obj.Items)
                                    {
                                        if (m != null)
                                        {
                                            items.Add(m.ToString());
                                        }
                                    }
                                    frm.SetText(items);
                                }
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
                        CellComboBox obj = context.Instance as CellComboBox;
                        if (obj != null)
                        {
                            using (ComboxEditForm frm = new ComboxEditForm())
                            {
                                List<string> items = new List<string>();
                                foreach (object m in obj.Items)
                                {
                                    if (m != null)
                                    {
                                        items.Add(m.ToString());
                                    }
                                }
                                frm.SetText(items);
                                frm.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
                                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                {
                                    obj.Items.Clear();

                                    List<string> list = frm.GetText();
                                    foreach (string item in list)
                                    {
                                        obj.Items.Add(item);
                                    }
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
