using System;

using System.Text;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Design;
using System.Collections;
using Feng.Excel.App;


namespace Feng.Excel.Designer
{

    internal class DataExcelDesignerActionList :
          System.ComponentModel.Design.DesignerActionList
    {
        private IDesignerHost host = null;

        private DataExcelControl relatedControl = null;

        private DataExcelDesigner relatedDesigner = null;

        public DataExcelDesignerActionList(IComponent component)
            : base(component)
        {
            this.relatedControl = component as DataExcelControl;

            this.host =
                this.Component.Site.GetService(typeof(IDesignerHost))
                as IDesignerHost;

            IDesigner dcd = host.GetDesigner(this.Component);
            this.relatedDesigner = dcd as DataExcelDesigner;

        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items =
                new DesignerActionItemCollection();

            try
            {


                items.Add(new DesignerActionMethodItem(
    this,
    "DockNone",
    "Dock None",
    true));

                items.Add(new DesignerActionMethodItem(
                    this,
                    "DockInParent",
                    "Dock Fill",
                    true));

                items.Add(new DesignerActionMethodItem(
    this,
    "Designer",
    "Designer",
    true));
                items.Add(new DesignerActionMethodItem(
this,
"Clear",
"Clear",
true));
                items.Add(new DesignerActionMethodItem(
this,
"Open",
"Open",
true));
                items.Add(new DesignerActionMethodItem(
this,
"About",
"About",
true));
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

            return items;

        }
        private void Designer()
        {
            try
            {
                DataExcelDesignerForm frm = new DataExcelDesignerForm();
                frm.Init(relatedControl);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    DesignerChche model = new DesignerChche();
                    model.FileName = frm.File;
                    model.DesignerData = frm.Data;
                    object oldvalue = model.DesignerData;
                    object newvalue = model;
                    this.relatedControl.DesignerData = model;
                    this.relatedDesigner.DesignDataChanged(oldvalue, newvalue);
                }

            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }
        private void Clear()
        {
            try
            {
                this.relatedControl.EditView.Clear();
                this.relatedControl.EditView.Init();
                this.relatedControl.EditView.ReFreshFirstDisplayRowIndex();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }
        private void Open()
        {
            try
            {
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    dlg.Filter = Feng.App.FileExtension_DataExcel.SelDataExcelFileAndAll;
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        DesignerChche model = new DesignerChche();
                        model.FileName = dlg.FileName;
                        model.DesignerData = System.IO.File.ReadAllBytes(dlg.FileName);
                        object oldvalue = model.DesignerData;
                        object newvalue = model;
                        this.relatedControl.DesignerData = model;
                        this.relatedDesigner.DesignDataChanged(oldvalue, newvalue);
                    }
                }
          
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }
        private void DockInParent()
        {

            try
            {

                if (this.relatedControl == null)
                    return;
                //////this.relatedControl.Dock = DockStyle.Fill;
                //Form frm = new Form();
                //frm.Controls.Add(this.relatedControl);
                //frm.ShowDialog();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void DockNone()
        {

            try
            {
                if (this.relatedControl == null)
                    return;
                //this.relatedControl.Dock = DockStyle.None;
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }
        private void About()
        {

            try
            {
                System.Diagnostics.Process.Start(Product.AssemblyHomePage);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

    }
}
