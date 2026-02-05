using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using Feng.Utils;
using System;
using Feng.Forms;
using Feng.Forms.Interface;

namespace Feng.Forms.Popup
{
    public class DatePopupForm : PopupForm
    { 
        public DatePopupForm()
        {
            InitializeComponent(); 
        }

        private MonthCalendar monthCalendar1;
        public IPopupEdit popupedit = null;
        public void InitPopup(IPopupEdit pedit)
        {
            popupedit = pedit;
        }
 
        private void InitializeComponent()
        {
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.monthCalendar1.Location = new System.Drawing.Point(0, 0);
            this.monthCalendar1.MaxSelectionCount = 1;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.ShowWeekNumbers = true;
            this.monthCalendar1.TabIndex = 0;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // DatePopupForm
            // 
            this.ClientSize = new System.Drawing.Size(289, 206);
            this.Controls.Add(this.monthCalendar1);
            this.Name = "DatePopupForm";
            this.ShowIcon = false;
            this.ResumeLayout(false);

        }
        public override void OK(object value, object model)
        {
            if (popupedit != null)
            {
                popupedit.OnOK(value, model);
            }
            base.OK(value, model);
        }
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
          
        }

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            this.OK(this.Value, this.Value);
        }

        public DateTime Value { get {
                return this.monthCalendar1.SelectionEnd;
            }
            set { 
                monthCalendar1.SelectionStart = value;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.OK(this.Value, this.Value);
        }
    }


}