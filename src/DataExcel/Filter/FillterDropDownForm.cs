using Feng.Data;
using Feng.Drawing;
using Feng.Excel.Interfaces; 
using Feng.Forms.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Fillter
{  
    public partial class FillterDropDownForm : Feng.Forms.Popup.PopupForm
    {
        public FillterDropDownForm()
        {
            InitializeComponent();
        }
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label btnSortAsc;
        private System.Windows.Forms.Label btnSortDesc;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel btnContentsSelAll;
        private System.Windows.Forms.TextBox txtContentsSearch;
        private System.Windows.Forms.ListView listViewContents;
        private System.Windows.Forms.LinkLabel btnContentsSelInva;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private Button btnOK;
        private Button btnCancel;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private FilterExcel FilterExcel = null;
        private LinkLabel btnClear;
        private TextBox txtItemCount;
        private RadioButton radText;
        private RadioButton radNum;
        private RadioButton radTime;
        private Panel panel4;
        private ColumnHeader columnHeader3;
        private ComboBox txtSumField;
        private Label label1;
        private FilterExcel.FilterColumn FilterColumn = null;
        public void InitPopup(FilterExcel filterexcel)
        {
            FilterExcel = filterexcel;
            InitCom();
        }
        private bool OKING = false;
        public override void Popup(Point pt)
        {
            OKING = false;
            base.Popup(pt);
        }
        public void InitFilterColumn(FilterExcel.FilterColumn filterColumn)
        {
            FilterColumn = filterColumn;
        }
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.ShowIcon = false;
                base.OnLoad(e);

                Feng.Forms.Controls.ListViewMulSelectTools listViewMulSelectTools = new Feng.Forms.Controls.ListViewMulSelectTools();
                listViewMulSelectTools.Init(this.listViewContents,true);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }
        public ICell cell;
        public DataExcel Grid;
        public class ModelDic
        {
            public string Key { get; set; }
            public int Count { get; set; }
            public decimal Sum { get; set; }
        }
        private bool initcom = false;
        private void InitCom()
        {
            initcom = true;
            try
            {

                this.txtSumField.Items.Clear();
                foreach (ICell item in FilterExcel.FilterCells)
                {
                    this.txtSumField.Items.Add(item.Text);
                }
            }
            catch (Exception ex)
            { 
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex); 
            }
            finally
            {
                initcom = false;
            }
        }
        Feng.Collections.DictionaryEx<string, ModelDic> dics = new Feng.Collections.DictionaryEx<string, ModelDic>();
        public void InitData()
        {
            dics.Clear();
            int columnstartindex = this.cell.Column.Index;
            int rowstartindex = this.cell.Row.Index;
            int columnendindex = this.cell.Grid.MaxHasValueColumn;
            int rowendindex = this.cell.Grid.Rows.MaxHasValueIndex;
            this.listViewContents.Items.Clear();
            for (int i = rowstartindex + 1; i <= rowendindex; i++)
            {
                IRow row = this.Grid.GetRow(i);
                if (row == null)
                    continue;
                if (!row.Visible)
                    continue;

                ICell cel = this.Grid[i, columnstartindex];
                string txt = Feng.Utils.ConvertHelper.ToString(cel.Value);
                bool hasrow = HasRow(row);
                if (hasrow)
                    continue;
                ModelDic modelDic = dics[txt];
                if (modelDic == null)
                {
                    modelDic = new ModelDic();
                    dics[txt] = modelDic;
                }
                int count = modelDic.Count + 1;
                modelDic.Count = count;
                if (this.FilterExcel.SumColumn > 0)
                {
                    ICell celsum = this.Grid[i, this.FilterExcel.SumColumn];
                    decimal decvalue = Feng.Utils.ConvertHelper.ToDecimal(celsum.Value);
                    modelDic.Sum = modelDic.Sum+ decvalue;
                }
                //string txt = Feng.Utils.ConvertHelper.ToString(cel.Value);
            }

            foreach (var item in dics)
            {
                string key = item.Key;
                string value = item.Value.Count.ToString();
                ListViewItem listViewItem = new ListViewItem(key);
                this.listViewContents.Items.Add(listViewItem);
                ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems.Add(value);
                listViewSubItem.Tag = item.Value.Count;

                if (this.FilterExcel.SumColumn > 0)
                {
                    value = item.Value.Sum.ToString();
                    listViewSubItem = listViewItem.SubItems.Add(value);
                    listViewSubItem.Tag = item.Value.Sum;
                }
            }
            this.txtItemCount.Text = this.listViewContents.Items.Count.ToString();
        }



        private bool HasRow(IRow row)
        {
            bool res = false;
            foreach (var item in this.FilterExcel.FilterColumns)
            {
                if (item == this.FilterColumn)
                {
                    continue;
                } 
                if (item.FilterRows.Contains(row))
                {
                    return true;
                }
            }
            return res;
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "111",
            "11",
            "11"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
            "222",
            "222"}, -1);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSortDesc = new System.Windows.Forms.Label();
            this.btnSortAsc = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.LinkLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtSumField = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.radTime = new System.Windows.Forms.RadioButton();
            this.radNum = new System.Windows.Forms.RadioButton();
            this.radText = new System.Windows.Forms.RadioButton();
            this.txtItemCount = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtContentsSearch = new System.Windows.Forms.TextBox();
            this.listViewContents = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnContentsSelAll = new System.Windows.Forms.LinkLabel();
            this.btnContentsSelInva = new System.Windows.Forms.LinkLabel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnSortDesc);
            this.panel1.Controls.Add(this.btnSortAsc);
            this.panel1.Controls.Add(this.btnClear);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(473, 34);
            this.panel1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(153, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "颜色";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSortDesc
            // 
            this.btnSortDesc.Image = global::Feng.Excel.Properties.Resources.image16_SortAsc;
            this.btnSortDesc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSortDesc.Location = new System.Drawing.Point(72, 8);
            this.btnSortDesc.Name = "btnSortDesc";
            this.btnSortDesc.Size = new System.Drawing.Size(57, 23);
            this.btnSortDesc.TabIndex = 0;
            this.btnSortDesc.Text = "升序";
            this.btnSortDesc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSortDesc.Click += new System.EventHandler(this.btnSortDesc_Click);
            // 
            // btnSortAsc
            // 
            this.btnSortAsc.Image = global::Feng.Excel.Properties.Resources.image16_SortDesc;
            this.btnSortAsc.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSortAsc.Location = new System.Drawing.Point(9, 8);
            this.btnSortAsc.Name = "btnSortAsc";
            this.btnSortAsc.Size = new System.Drawing.Size(57, 23);
            this.btnSortAsc.TabIndex = 0;
            this.btnSortAsc.Text = "降序";
            this.btnSortAsc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSortAsc.Click += new System.EventHandler(this.btnSortAsc_Click);
            // 
            // btnClear
            // 
            this.btnClear.AutoSize = true;
            this.btnClear.Location = new System.Drawing.Point(426, 9);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(37, 15);
            this.btnClear.TabIndex = 2;
            this.btnClear.TabStop = true;
            this.btnClear.Text = "清除";
            this.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnClear.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnClear_LinkClicked);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 34);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(473, 491);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtSumField);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.radTime);
            this.panel3.Controls.Add(this.radNum);
            this.panel3.Controls.Add(this.radText);
            this.panel3.Controls.Add(this.txtItemCount);
            this.panel3.Controls.Add(this.btnCancel);
            this.panel3.Controls.Add(this.btnOK);
            this.panel3.Controls.Add(this.txtContentsSearch);
            this.panel3.Controls.Add(this.listViewContents);
            this.panel3.Controls.Add(this.btnContentsSelAll);
            this.panel3.Controls.Add(this.btnContentsSelInva);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(473, 491);
            this.panel3.TabIndex = 3;
            // 
            // txtSumField
            // 
            this.txtSumField.FormattingEnabled = true;
            this.txtSumField.Location = new System.Drawing.Point(348, 43);
            this.txtSumField.Name = "txtSumField";
            this.txtSumField.Size = new System.Drawing.Size(114, 23);
            this.txtSumField.TabIndex = 7;
            this.txtSumField.SelectedValueChanged += new System.EventHandler(this.txtSumField_SelectedValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(300, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "合计:";
            // 
            // radTime
            // 
            this.radTime.AutoSize = true;
            this.radTime.Location = new System.Drawing.Point(162, 7);
            this.radTime.Name = "radTime";
            this.radTime.Size = new System.Drawing.Size(58, 19);
            this.radTime.TabIndex = 5;
            this.radTime.Text = "时间";
            this.radTime.UseVisualStyleBackColor = true;
            // 
            // radNum
            // 
            this.radNum.AutoSize = true;
            this.radNum.Location = new System.Drawing.Point(87, 7);
            this.radNum.Name = "radNum";
            this.radNum.Size = new System.Drawing.Size(58, 19);
            this.radNum.TabIndex = 5;
            this.radNum.Text = "数字";
            this.radNum.UseVisualStyleBackColor = true;
            // 
            // radText
            // 
            this.radText.AutoSize = true;
            this.radText.Checked = true;
            this.radText.Location = new System.Drawing.Point(13, 7);
            this.radText.Name = "radText";
            this.radText.Size = new System.Drawing.Size(58, 19);
            this.radText.TabIndex = 5;
            this.radText.TabStop = true;
            this.radText.Text = "文本";
            this.radText.UseVisualStyleBackColor = true;
            // 
            // txtItemCount
            // 
            this.txtItemCount.BackColor = System.Drawing.Color.White;
            this.txtItemCount.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtItemCount.Location = new System.Drawing.Point(8, 455);
            this.txtItemCount.Name = "txtItemCount";
            this.txtItemCount.ReadOnly = true;
            this.txtItemCount.Size = new System.Drawing.Size(100, 18);
            this.txtItemCount.TabIndex = 4;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(372, 453);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(78, 30);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Location = new System.Drawing.Point(279, 453);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(78, 30);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtContentsSearch
            // 
            this.txtContentsSearch.Location = new System.Drawing.Point(7, 40);
            this.txtContentsSearch.Name = "txtContentsSearch";
            this.txtContentsSearch.Size = new System.Drawing.Size(148, 25);
            this.txtContentsSearch.TabIndex = 1;
            // 
            // listViewContents
            // 
            this.listViewContents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewContents.CheckBoxes = true;
            this.listViewContents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.listViewContents.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            this.listViewContents.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listViewContents.Location = new System.Drawing.Point(7, 71);
            this.listViewContents.Name = "listViewContents";
            this.listViewContents.Size = new System.Drawing.Size(456, 376);
            this.listViewContents.TabIndex = 0;
            this.listViewContents.UseCompatibleStateImageBehavior = false;
            this.listViewContents.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "内容";
            this.columnHeader1.Width = 146;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "次数";
            this.columnHeader2.Width = 143;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "合计";
            this.columnHeader3.Width = 93;
            // 
            // btnContentsSelAll
            // 
            this.btnContentsSelAll.AutoSize = true;
            this.btnContentsSelAll.Location = new System.Drawing.Point(171, 43);
            this.btnContentsSelAll.Name = "btnContentsSelAll";
            this.btnContentsSelAll.Size = new System.Drawing.Size(37, 15);
            this.btnContentsSelAll.TabIndex = 2;
            this.btnContentsSelAll.TabStop = true;
            this.btnContentsSelAll.Text = "全选";
            this.btnContentsSelAll.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnContentsSelAll_LinkClicked);
            // 
            // btnContentsSelInva
            // 
            this.btnContentsSelInva.AutoSize = true;
            this.btnContentsSelInva.Location = new System.Drawing.Point(206, 43);
            this.btnContentsSelInva.Name = "btnContentsSelInva";
            this.btnContentsSelInva.Size = new System.Drawing.Size(37, 15);
            this.btnContentsSelInva.TabIndex = 2;
            this.btnContentsSelInva.TabStop = true;
            this.btnContentsSelInva.Text = "反选";
            this.btnContentsSelInva.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnContentsSelInva_LinkClicked);
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.panel2);
            this.panel4.Controls.Add(this.panel1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(475, 527);
            this.panel4.TabIndex = 3;
            // 
            // FillterDropDownForm
            // 
            this.ClientSize = new System.Drawing.Size(475, 527);
            this.Controls.Add(this.panel4);
            this.Name = "FillterDropDownForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (FilterColumn == null)
                    return;
                FilterColumn.Value.Clear();
                FilterExcel.FilterRows.Clear();
                foreach (ListViewItem item in this.listViewContents.CheckedItems)
                {
                    string value = item.Text;
                    FilterColumn.Value.Add(value);
                }
                FilterExcel.Filter();
                this.Grid.ReFreshFirstDisplayRowIndex();
                this.Hide();
            }
            catch (Exception ex)
            { 
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "FillterDropDownForm", "btnOK_Click", ex);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {

                this.Hide();
            }
            catch (Exception ex)
            { 
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);
            }
        }

        private void btnSortAsc_Click(object sender, EventArgs e)
        {
            try
            {
                Sorter sorter = new Sorter();
                sorter.Grid = this.Grid;
                sorter.BeginRowIndex = this.FilterExcel.BeginRowIndex;
                sorter.EndRowIndex = this.FilterExcel.EndRowIndex;
                if (this.radNum.Checked)
                {
                    sorter.Type = TypeEnum.Tdecimal;
                }
                if (this.radTime.Checked)
                {
                    sorter.Type = TypeEnum.TDateTime;
                }
                sorter.SortDesc(this.FilterColumn.Column);
                this.Grid.ReFreshFirstDisplayRowIndex();
                this.Hide();
            }
            catch (Exception ex)
            { 
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);
            }
        }

        private void btnSortDesc_Click(object sender, EventArgs e)
        {

            try
            {
                Sorter sorter = new Sorter();
                sorter.Grid = this.Grid;
                sorter.BeginRowIndex = this.FilterExcel.BeginRowIndex;
                sorter.EndRowIndex = this.FilterExcel.EndRowIndex;
                if (this.radNum.Checked)
                {
                    sorter.Type = TypeEnum.Tdecimal;
                }
                if (this.radTime.Checked)
                {
                    sorter.Type = TypeEnum.TDateTime;
                }
                sorter.SortAsc(this.FilterColumn.Column);
                this.Grid.ReFreshFirstDisplayRowIndex();
                this.Hide();
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);
            }
        }

        private void btnClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.FilterExcel.Clear();

                this.Grid.ReFreshFirstDisplayRowIndex();
                this.Hide();
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);
            }
        }

        private void btnContentsSelAll_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                foreach (ListViewItem item in this.listViewContents.Items)
                {
                    item.Checked = true;
                }
            }
            catch (Exception ex)
            { 
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);
            }
        }

        private void btnContentsSelInva_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                foreach (ListViewItem item in this.listViewContents.Items)
                {
                    item.Checked = !item.Checked;
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);
            }
        }

        private void txtSumField_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                if (initcom)
                    return;
                foreach (ICell item in this.FilterExcel .FilterCells)
                {
                    if (item.Text == this.txtSumField.Text)
                    {
                        this.FilterExcel.SumColumn = item.Column.Index;
                        this.FilterExcel.SumField = item.Text;
                        break;
                    }
                }
                InitData();
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmDataProjectClient", "statusSum_Click", ex);
            }

        }
    }
}
