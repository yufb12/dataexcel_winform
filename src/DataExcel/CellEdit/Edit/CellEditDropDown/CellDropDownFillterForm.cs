using Feng.Excel.Interfaces;
using Feng.Forms.Interface;
using System;
using System.Drawing;

namespace Feng.Excel.Edits.EditForms
{
    public partial class CellDropDownFillterForm : Feng.Forms.Popup.PopupForm
    {
        public CellDropDownFillterForm()
        {
            InitializeComponent();
        }

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.LinkLabel btnContentsSelAll;
        private System.Windows.Forms.TextBox txtContentsSearch;
        private System.Windows.Forms.ListView listViewContents;
        private System.Windows.Forms.LinkLabel btnContentsSelInva;
        private System.Windows.Forms.LinkLabel btnContentsSelRe;
        private System.Windows.Forms.LinkLabel btnContentsSelOnly;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        public IPopupEdit popupedit = null;
        public void InitPopup(IPopupEdit pedit)
        {
            InitData();
            popupedit = pedit;
        }
        private bool OKING = false;
        public override void Popup(Point pt)
        {
            OKING = false;
            base.Popup(pt);
        } 
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.ShowIcon = false; 
                base.OnLoad(e);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }
        public ICell cell;
        public DataExcel Grid;
        public void InitData()
        {
            Feng.Collections.DictionaryEx<string, int> dics = new Feng.Collections.DictionaryEx<string, int>();
            int columnstartindex = this.cell.Column.Index;
            int rowstartindex = this.cell.Row.Index;
            int columnendindex = this.cell.Grid.MaxHasValueColumn;
            int rowendindex = this.cell.Grid.Rows.MaxHasValueIndex;
            for (int i = rowstartindex+1; i <= rowendindex; i++)
            {
                ICell cel = this.Grid[i, columnstartindex];
                string txt = Feng.Utils.ConvertHelper.ToString(cel.Value);
                int count = dics[txt]+1;
                dics[txt] = count;
            }

            this.listViewContents.Items.Clear();
            foreach (var item in dics)
            {
                this.listViewContents.Items.Add(item.Key).SubItems.Add(item.Value.ToString());
            }
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtContentsSearch = new System.Windows.Forms.TextBox();
            this.btnContentsSelOnly = new System.Windows.Forms.LinkLabel();
            this.listViewContents = new System.Windows.Forms.ListView();
            this.btnContentsSelRe = new System.Windows.Forms.LinkLabel();
            this.btnContentsSelAll = new System.Windows.Forms.LinkLabel();
            this.btnContentsSelInva = new System.Windows.Forms.LinkLabel();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(475, 493);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.checkBox1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(475, 34);
            this.panel1.TabIndex = 1;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(229, 9);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(59, 19);
            this.checkBox1.TabIndex = 1;
            this.checkBox1.Text = "增加";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.Image = global::Feng.Excel.Properties.Resources.AdvancedFilterDialog1;
            this.label3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label3.Location = new System.Drawing.Point(139, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 23);
            this.label3.TabIndex = 0;
            this.label3.Text = "颜色";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            this.label2.Image = global::Feng.Excel.Properties.Resources.AdvancedFilterDialog1;
            this.label2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label2.Location = new System.Drawing.Point(72, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 23);
            this.label2.TabIndex = 0;
            this.label2.Text = "升序";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Image = global::Feng.Excel.Properties.Resources.AdvancedFilterDialog1;
            this.label1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label1.Location = new System.Drawing.Point(9, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "降序";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            this.label4.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label4.Location = new System.Drawing.Point(12, 42);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 23);
            this.label4.TabIndex = 0;
            this.label4.Text = "内容筛选";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Image = global::Feng.Excel.Properties.Resources.AdvancedFilterDialog1;
            this.label5.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label5.Location = new System.Drawing.Point(126, 42);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 23);
            this.label5.TabIndex = 0;
            this.label5.Text = "颜色";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Image = global::Feng.Excel.Properties.Resources.AdvancedFilterDialog1;
            this.label6.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.label6.Location = new System.Drawing.Point(226, 41);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 23);
            this.label6.TabIndex = 0;
            this.label6.Text = "文本";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Location = new System.Drawing.Point(0, 75);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(475, 452);
            this.panel2.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.txtContentsSearch);
            this.panel3.Controls.Add(this.btnContentsSelOnly);
            this.panel3.Controls.Add(this.listViewContents);
            this.panel3.Controls.Add(this.btnContentsSelRe);
            this.panel3.Controls.Add(this.btnContentsSelAll);
            this.panel3.Controls.Add(this.btnContentsSelInva);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(475, 452);
            this.panel3.TabIndex = 3;
            // 
            // txtContentsSearch
            // 
            this.txtContentsSearch.Location = new System.Drawing.Point(7, 6);
            this.txtContentsSearch.Name = "txtContentsSearch";
            this.txtContentsSearch.Size = new System.Drawing.Size(148, 25);
            this.txtContentsSearch.TabIndex = 1;
            // 
            // btnContentsSelOnly
            // 
            this.btnContentsSelOnly.AutoSize = true;
            this.btnContentsSelOnly.Location = new System.Drawing.Point(269, 11);
            this.btnContentsSelOnly.Name = "btnContentsSelOnly";
            this.btnContentsSelOnly.Size = new System.Drawing.Size(52, 15);
            this.btnContentsSelOnly.TabIndex = 2;
            this.btnContentsSelOnly.TabStop = true;
            this.btnContentsSelOnly.Text = "唯一项";
            // 
            // listViewContents
            // 
            this.listViewContents.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewContents.CheckBoxes = true;
            this.listViewContents.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.listViewContents.HideSelection = false;
            listViewItem1.StateImageIndex = 0;
            listViewItem2.StateImageIndex = 0;
            this.listViewContents.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listViewContents.Location = new System.Drawing.Point(7, 37);
            this.listViewContents.Name = "listViewContents";
            this.listViewContents.Size = new System.Drawing.Size(458, 405);
            this.listViewContents.TabIndex = 0;
            this.listViewContents.UseCompatibleStateImageBehavior = false;
            this.listViewContents.View = System.Windows.Forms.View.Details;
            // 
            // btnContentsSelRe
            // 
            this.btnContentsSelRe.AutoSize = true;
            this.btnContentsSelRe.Location = new System.Drawing.Point(231, 11);
            this.btnContentsSelRe.Name = "btnContentsSelRe";
            this.btnContentsSelRe.Size = new System.Drawing.Size(37, 15);
            this.btnContentsSelRe.TabIndex = 2;
            this.btnContentsSelRe.TabStop = true;
            this.btnContentsSelRe.Text = "重复";
            // 
            // btnContentsSelAll
            // 
            this.btnContentsSelAll.AutoSize = true;
            this.btnContentsSelAll.Location = new System.Drawing.Point(161, 11);
            this.btnContentsSelAll.Name = "btnContentsSelAll";
            this.btnContentsSelAll.Size = new System.Drawing.Size(37, 15);
            this.btnContentsSelAll.TabIndex = 2;
            this.btnContentsSelAll.TabStop = true;
            this.btnContentsSelAll.Text = "全选";
            // 
            // btnContentsSelInva
            // 
            this.btnContentsSelInva.AutoSize = true;
            this.btnContentsSelInva.Location = new System.Drawing.Point(196, 11);
            this.btnContentsSelInva.Name = "btnContentsSelInva";
            this.btnContentsSelInva.Size = new System.Drawing.Size(37, 15);
            this.btnContentsSelInva.TabIndex = 2;
            this.btnContentsSelInva.TabStop = true;
            this.btnContentsSelInva.Text = "反选";
            // 
            // columnHeader1
            // 
            this.columnHeader1.Width = 146;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Width = 143;
            // 
            // CellDropDownFillterForm
            // 
            this.ClientSize = new System.Drawing.Size(475, 527);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Name = "CellDropDownFillterForm";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }
    }
}
