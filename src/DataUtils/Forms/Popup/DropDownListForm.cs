using Feng.Forms.Events;
using Feng.Forms.Interface;
using System;
using System.Windows.Forms;

namespace Feng.Forms.Popup
{
    public partial class DropDownListForm : PopupForm
    {
        public DropDownListForm()
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

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (popupedit != null)
            {
                popupedit.OnOK(this.listView1.FocusedItem.Text, this.listView1.FocusedItem.Tag);
            }
        }
        public override void SelectFirst()
        {
            if (popupedit != null)
            {
                popupedit.OnOK(this.listView1.FocusedItem.Text, this.listView1.FocusedItem.Tag);
            }
            base.SelectFirst();
        }
        public override void SetFocus()
        {
            this.listView1.Focus();
            base.SetFocus();
        }
        public override void MoveToFirst()
        {
            this.listView1.Items[0].Selected = true;
            this.listView1.Focus();
            base.MoveToFirst();
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (popupedit != null)
                    {
                        popupedit.OnOK(this.listView1.FocusedItem.Text, this.listView1.FocusedItem.Tag);
                    }
                }
                if (e.KeyCode == Keys.Escape)
                {
                    if (popupedit != null)
                    {
                        popupedit.OnCancel();
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }
    }
}
