using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using Feng.Utils;
using System;
using Feng.Forms;
using Feng.Forms.Interface;

namespace Feng.Forms.Popup
{
    public class DateTimePopupForm : PopupForm
    { 
        public DateTimePopupForm()
        {
            InitializeComponent(); 
        }

        private NumericUpDown txtYear;
        private NumericUpDown txtMonth;
        private NumericUpDown txtDay;
        private NumericUpDown txtHour;
        private NumericUpDown txtMin;
        private NumericUpDown txtSecond;
        public IPopupEdit popupedit = null;
        public void InitPopup(IPopupEdit pedit)
        {
            popupedit = pedit;
        }
        private MonthCalendar monthCalendar1;
        private Button btnOK;
        private Button btnCancel;
 
        private void InitializeComponent()
        {
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtYear = new System.Windows.Forms.NumericUpDown();
            this.txtMonth = new System.Windows.Forms.NumericUpDown();
            this.txtDay = new System.Windows.Forms.NumericUpDown();
            this.txtHour = new System.Windows.Forms.NumericUpDown();
            this.txtMin = new System.Windows.Forms.NumericUpDown();
            this.txtSecond = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecond)).BeginInit();
            this.SuspendLayout();
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(0, 0);
            this.monthCalendar1.MaxSelectionCount = 1;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.ShowWeekNumbers = true;
            this.monthCalendar1.TabIndex = 0;
            this.monthCalendar1.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.monthCalendar1_DateSelected);
            // 
            // btnOK
            // 
            this.btnOK.AutoEllipsis = true;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Location = new System.Drawing.Point(211, 240);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 26);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AutoEllipsis = true;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancel.Location = new System.Drawing.Point(133, 240);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 26);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtYear
            // 
            this.txtYear.Location = new System.Drawing.Point(3, 207);
            this.txtYear.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.txtYear.Minimum = new decimal(new int[] {
            9000,
            0,
            0,
            -2147483648});
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(61, 25);
            this.txtYear.TabIndex = 3;
            this.txtYear.Value = new decimal(new int[] {
            2023,
            0,
            0,
            0});
            // 
            // txtMonth
            // 
            this.txtMonth.Location = new System.Drawing.Point(65, 207);
            this.txtMonth.Maximum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.txtMonth.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMonth.Name = "txtMonth";
            this.txtMonth.Size = new System.Drawing.Size(43, 25);
            this.txtMonth.TabIndex = 4;
            this.txtMonth.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            // 
            // txtDay
            // 
            this.txtDay.Location = new System.Drawing.Point(108, 207);
            this.txtDay.Maximum = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.txtDay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtDay.Name = "txtDay";
            this.txtDay.Size = new System.Drawing.Size(40, 25);
            this.txtDay.TabIndex = 4;
            this.txtDay.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            this.txtDay.ValueChanged += new System.EventHandler(this.txtDay_ValueChanged);
            // 
            // txtHour
            // 
            this.txtHour.Location = new System.Drawing.Point(159, 207);
            this.txtHour.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.txtHour.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtHour.Name = "txtHour";
            this.txtHour.Size = new System.Drawing.Size(40, 25);
            this.txtHour.TabIndex = 4;
            this.txtHour.Value = new decimal(new int[] {
            24,
            0,
            0,
            0});
            // 
            // txtMin
            // 
            this.txtMin.Location = new System.Drawing.Point(203, 207);
            this.txtMin.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.txtMin.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtMin.Name = "txtMin";
            this.txtMin.Size = new System.Drawing.Size(40, 25);
            this.txtMin.TabIndex = 4;
            this.txtMin.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            // 
            // txtSecond
            // 
            this.txtSecond.Location = new System.Drawing.Point(247, 207);
            this.txtSecond.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.txtSecond.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtSecond.Name = "txtSecond";
            this.txtSecond.Size = new System.Drawing.Size(40, 25);
            this.txtSecond.TabIndex = 4;
            this.txtSecond.Value = new decimal(new int[] {
            31,
            0,
            0,
            0});
            // 
            // DateTimePopupForm
            // 
            this.ClientSize = new System.Drawing.Size(290, 273);
            this.Controls.Add(this.txtSecond);
            this.Controls.Add(this.txtMin);
            this.Controls.Add(this.txtHour);
            this.Controls.Add(this.txtDay);
            this.Controls.Add(this.txtMonth);
            this.Controls.Add(this.txtYear);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "DateTimePopupForm";
            this.ShowIcon = false;
            ((System.ComponentModel.ISupportInitialize)(this.txtYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMonth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSecond)).EndInit();
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

        private void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {          
            try
            {
                this.txtYear.Value = monthCalendar1.SelectionStart.Year;
                this.txtMonth.Value = monthCalendar1.SelectionStart.Month;
                this.txtDay.Value = monthCalendar1.SelectionStart.Day;
                this.txtHour.Value = monthCalendar1.SelectionStart.Hour;
                this.txtMin.Value = monthCalendar1.SelectionStart.Minute;
                this.txtSecond.Value = monthCalendar1.SelectionStart.Second;
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);
            }

        }

        public DateTime Value
        {
            get
            {
                return new DateTime((ushort)this.txtYear.Value,
                    (ushort)this.txtMonth.Value,
                    (ushort)this.txtDay.Value,
                    (ushort)this.txtHour.Value,
                    (ushort)this.txtMin.Value,
                   (ushort)this.txtSecond.Value);
            }
            set
            {
                this.txtYear.Value = value.Year;
                this.txtMonth.Value = value.Month;
                this.txtDay.Value = value.Day;
                this.txtHour.Value = value.Hour;
                this.txtMin.Value = value.Minute;
                this.txtSecond.Value = value.Second;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {          
            try
            {
                DateTime dt = this.Value;
                this.OK(this.Value, this.Value);
            }
            catch (Exception ex)
            {
                this.txtDay.Focus();
                this.txtDay.Value = 28;
            }
            
        }

        private void txtDay_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }


}