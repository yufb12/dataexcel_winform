using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Feng.Forms.Views.Charts
{
    public class ChartControl : ViewControl
    {
        public ChartControl() : base()
        {

        }

        public void InitEditView(ChartView view)
        {
            this.Viewes.Remove(this.EditView);
            editView = view;
            editView.BindingControl(this);

            int padding = 0;
            this.EditView.Left = padding;
            this.EditView.Top = padding;
            this.EditView.Width = this.Width - padding;
            this.EditView.Height = this.Height - padding;
        }
        private ChartView editView = null;
        [Browsable(true)]
        public ChartView EditView
        {
            get
            {
                if (editView == null)
                {
                    editView = new ChartView();
                    editView.BindingControl(this);
                    editView.Init();
                }
                return editView;
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
                editView.BindingControl(this);
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmMain2", "statusSum_Click", ex);
            }
            base.OnHandleCreated(e);
        }
    }
}
