using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using Feng.Forms.Dialogs;
using Feng.Excel.Extend;

namespace Feng.Excel.Designer
{
    [Serializable]
    public class DataColumnDesigner : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            DataExcelChart chart = context.Instance as DataExcelChart;
            if (chart != null)
            {
               System.Data.DataTable table= chart.DataSource as System.Data.DataTable;
                if (table!=null )
                {
                    IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                    if (edSvc != null)
                    {
                        DataColumnSelect scs = new DataColumnSelect();
                        scs.edSvc = edSvc;
                        foreach (System.Data.DataColumn col in table.Columns)
                        {
                            scs.Items.Add(col.ColumnName);
                        }
                        edSvc.DropDownControl(scs);
                        
                        return scs.SelectedItem ;
                    }
 
                }
            }

            return base.EditValue(context, provider, value);
        }


    }


    [Serializable]
    public class MemonDesigner : System.Drawing.Design.UITypeEditor
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
                using (InputMultilineDialog scs = new InputMultilineDialog())
                {
                    string text = Feng.Utils.ConvertHelper.ToString(value);
                    scs.txtInput.Text = text;
                    if (scs.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        return scs.txtInput.Text;
                    }
                }
            }
  
            return base.EditValue(context, provider, value);
        }


    }
}
