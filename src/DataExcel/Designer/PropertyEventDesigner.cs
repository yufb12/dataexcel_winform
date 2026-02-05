using System; 
using System.Drawing.Design;
using System.Windows.Forms.Design; 
using Feng.Excel.Interfaces;

namespace Feng.Excel.Designer
{
    //[Serializable]
    //public class PropertyEventDesigner : System.Drawing.Design.UITypeEditor
    //{
    //    public override System.Drawing.Design.UITypeEditorEditStyle GetEditStyle(System.ComponentModel.ITypeDescriptorContext context)
    //    {
    //        return UITypeEditorEditStyle.DropDown;
    //    }

    //    public override object EditValue(System.ComponentModel.ITypeDescriptorContext context, IServiceProvider provider, object value)
    //    {
    //        ICell cell = context.Instance as ICell;
    //        if (cell != null)
    //        {

    //            IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
    //            if (edSvc != null)
    //            {
    //                TreeViewDesignerControl edit = new TreeViewDesignerControl();
    //                edit.edSvc = edSvc;
    //                edit.Nodes.Clear();
    //                System.Windows.Forms.TreeNode node = edit.Nodes.Add("无"); 
    //                if (cell.Grid.ActionContainer != null)
    //                {
    //                    node = edit.Nodes.Add(cell.Grid.ActionContainer.Name);
    //                    foreach (string col in cell.Grid.ActionContainer.Actions)
    //                    {
    //                        System.Windows.Forms.TreeNode nodec = node.Nodes.Add(col.Name);
    //                        nodec.Tag = col;
    //                    }
    //                }
    //                foreach (BaseActionContainer action in cell.Grid.Actions)
    //                {
    //                    node = edit.Nodes.Add(action.Name);
    //                    foreach (string col in action.Actions)
    //                    {
    //                        System.Windows.Forms.TreeNode nodec = node.Nodes.Add(col.Name);
    //                        nodec.Tag = col;
    //                    }
    //                }
    //                edit.Height = 200;
    //                edSvc.DropDownControl(edit);

    //                return edit.SelectedNode.Tag;
    //            }
  
    //        }

    //        return base.EditValue(context, provider, value);
    //    }

    //}


}
