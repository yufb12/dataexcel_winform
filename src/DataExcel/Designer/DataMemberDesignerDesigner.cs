using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms.Design;
using Feng.Forms.Controls.Designer;
namespace Feng.Excel.Designer
{
    [Serializable]
    public class DataMemberDesignerDesigner : System.Drawing.Design.UITypeEditor
    {
        public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }

        public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Feng.Excel.DataExcel chart = context.Instance as Feng.Excel.DataExcel;
            if (chart != null)
            {
                System.Data.DataSet table = chart.DataSource as System.Data.DataSet;
                if (table!=null )
                {
                    IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                    if (edSvc != null)
                    {
                        DataColumnSelect scs = new DataColumnSelect();
                        scs.edSvc = edSvc;
                        foreach (System.Data.DataTable col in table.Tables)
                        {
                            scs.Items.Add(col.TableName);
                        }
                        edSvc.DropDownControl(scs);
                        
                        return scs.SelectedItem ;
                    }
 
                }
            }

            return base.EditValue(context, provider, value);
        }

 
    }

    [ToolboxItem(false)]
    public class DataColumnSelect : System.Windows.Forms.ListBox
    {
        public IWindowsFormsEditorService edSvc = null;
        protected override void OnMouseClick(System.Windows.Forms.MouseEventArgs e)
        {
            base.OnMouseClick(e);

        }

        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            edSvc.CloseDropDown();
            base.OnMouseDown(e);
        }
    }
     
}
