using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing.Design;

namespace Feng.Excel.Designer
{
    public class DataExcelDesigner : ControlDesigner
    {
        private DesignerActionListCollection actionLists = null;
        public override System.ComponentModel.Design.DesignerActionListCollection ActionLists
        {
            get
            {
                try
                {
                    if (null == actionLists)
                    {
                        actionLists = new DesignerActionListCollection();
                        actionLists.Add(
                           new DataExcelDesignerActionList(this.Component));
                    } 
                }
                catch (Exception ex)
                {
                    Feng.Utils.ExceptionHelper.ShowError(ex);
                }
                return actionLists;
            }
        }
        public void DesignDataChanged(object oldvalue,object newvalue)
        {

            //try
            //{
                this.RaiseComponentChanged(TypeDescriptor.GetProperties(this.Component)["DesignerData"], oldvalue, newvalue);
            //}
            //catch (Exception ex)
            //{
            //    Feng.Utils.ExceptionHelper.ShowError(ex);
            //}
  
        } 
    }

    [Serializable()]
    [TypeConverterAttribute(typeof(DesignerChcheTypeConverter))] 
    public class DesignerChche
    {
        public string FileName { get; set; }
        public byte[] DesignerData { get; set; }
    }

    public class DesignerChcheTypeConverter :ExpandableObjectConverter// TypeConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            try
            {
                DesignerChche model = value as DesignerChche;
                if (model != null)
                {
                    return model.FileName;
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

    }


    public class DesignerChcheTypeEditor : UITypeEditor
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
                if (edSvc != null)
                {
                    using (System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog())
                    {  
                        DesignerChche model = value as DesignerChche;     
                        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        { 
                            model = new DesignerChche();
                            model.FileName = System.IO.Path.GetFileName(dlg.FileName);
                            model.DesignerData = System.IO.File.ReadAllBytes(dlg.FileName);
                        }
                        DataExcelControl grid = context.Instance as DataExcelControl;
                        if (grid != null)
                        {
                            grid.DesignerData = model; 
                        }
                        value = model;
                        context.OnComponentChanged();
                    }
                }
            }

            return value;
        }
    }
}
