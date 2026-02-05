using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Utils;
using Feng.Drawing;
using Feng.Forms.Views;
using Feng.Excel;
using Feng.Excel.Designer;
using Feng.Excel.ExcelLicense;
using System.Runtime.InteropServices;
using System.Drawing.Design;

namespace Feng.Excel
{
    [ToolboxItem(true)]
    [Designer(typeof(DataExcelDesigner))]
    [DefaultProperty("EditView"), DefaultEvent("Click")]
    [ToolboxBitmap(typeof(DataExcel), "Bitmap256.DataExcelBmp.bmp")]
    [Guid(Feng.Excel.App.Product.AssemblyControlGuid)]
    [LicenseProvider(typeof(DataExcelLicenseProvider))]
    [Docking(DockingBehavior.Ask)]
    public class DataExcelControl : ViewControl
    {
        public DataExcelControl()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.ContainerControl, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.StandardClick, true);
            this.SetStyle(ControlStyles.Selectable, true);
            this.SetStyle(ControlStyles.StandardDoubleClick, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.UpdateStyles();
            this.BackColor = Color.White;
        }
 
        public override string Text
        {
            get
            {
                return this.EditView.Text;
            }
            set
            {
                this.EditView.Text = value;
                this.Invalidate();
            }
        }

        public void InitEditView(DataExcel dataexcel)
        {
            this.Viewes.Remove(this.EditView);
            editView = dataexcel;
            if (editView != null)
            {
                editView.BindingControl(this);
            }
        }
        private DataExcel editView = null;
        [Browsable(true)]
        public DataExcel EditView
        {
            get
            {
                if (editView == null)
                {
                    editView = new DataExcel();
                    editView.BindingControl(this);
                    editView.Init();
                }
                return editView;
            }
        }

        private DesignerChche designerdata = null;
        [Localizable(true)]
        [Browsable(true)]
        [Editor(typeof(DesignerChcheTypeEditor), typeof(UITypeEditor))]
        [DefaultValue(null)]
        public DesignerChche DesignerData
        {
            get
            {
                return designerdata;
            }
            set
            {
                designerdata = value;
                if (this.designerdata != null)
                {

                    try
                    {
                        this.EditView.Open(designerdata.DesignerData);
                    }
                    catch (Exception)
                    {
                    }

                }
            }
        }

        protected override void OnSizeChanged(EventArgs e)
        {  
            try
            {
                this.EditView.Left = 0;
                this.EditView.Top = 0;
                this.EditView.Width = this.Width;
                this.EditView.Height = this.Height;
                //this.EditView.SaveAs();
                int padding = 0;
                this.EditView.Left = padding;
                this.EditView.Top = padding;
                this.EditView.Width = this.Width - padding;
                this.EditView.Height = this.Height - padding;
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmMain2", "statusSum_Click", ex);
            }
            base.OnSizeChanged(e);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            try
            {
                this.EditView.Handle = this.Handle;
                //this.EditView.Left = 0;
                //this.EditView.Top = 0;
                //this.EditView.Width = this.Width;
                //this.EditView.Height = this.Height;
                editView.BindingControl(this);
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmMain2", "statusSum_Click", ex);
            }
            base.OnHandleCreated(e);
        }

        protected override void OnGotFocus(EventArgs e)
        {
            try
            {
                this.EditView.Handle = this.Handle;
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmMain2", "statusSum_Click", ex);
            }
            base.OnGotFocus(e);
        }
    }
}
