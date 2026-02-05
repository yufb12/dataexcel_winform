using Feng.Excel.Interfaces;
using Feng.Forms.Interface;
using System;
using System.Drawing;

namespace Feng.Excel.Edits.EditForms
{
    public partial class CellDropDownDataExcelForm : Feng.Forms.Popup.PopupForm, IDropDownGrid
    {
        public CellDropDownDataExcelForm()
        {
            dataExcel1 = new DataExcelControl();
        }

        public IPopupEdit popupedit = null;
        public void InitPopup(IPopupEdit pedit)
        {
            popupedit = pedit;
        }
        private bool OKING = false;
        public override void Popup(Point pt)
        {
            OKING = false;
            base.Popup(pt);
        }
        private DataExcelControl dataExcel1 = null;
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                this.Controls.Add(this.dataExcel1);
                this.dataExcel1.EditView.CellClick += DataExcel1_CellDoubleClick;
                this.dataExcel1.Dock = System.Windows.Forms.DockStyle.Fill;
                base.OnLoad(e);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }
 
        private void DataExcel1_CellDoubleClick(object sender, ICell cell)
        {
            if (OKING)
            {
                return;
            }
            try
            {
                OKING = true;
                popupedit.OnOK(this, this.dataExcel1.EditView.FocusedCell);
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except", ex);
            }
        }

        public void InitData(Feng.Excel.DataExcel grid)
        {
            this.dataExcel1.InitEditView(grid);
            this.dataExcel1.EditView.ReadOnly = true;
        }
        public virtual DataExcel GetDropDownGrid()
        {
            return this.dataExcel1.EditView;
        }
    }
}
