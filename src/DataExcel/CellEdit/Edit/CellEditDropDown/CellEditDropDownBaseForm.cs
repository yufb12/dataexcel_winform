using Feng.Forms.Events;
using Feng.Forms.Interface;
using Feng.Forms.Popup;
using System;
using System.Windows.Forms;

namespace Feng.Excel.Edits.EditForms
{
    public partial class CellEditDropDownBaseForm : PopupForm
    {
        public CellEditDropDownBaseForm()
        {
            InitializeComponent();
        }
        public IPopupEdit popupedit = null;
        public void InitPopup(IPopupEdit pedit)
        {
            popupedit = pedit;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        public override void KeyChanged(object sender, DropDownBoxTextChangedEventArgs e)
        {
            base.KeyChanged(sender, e);
        }
 
    }
}
