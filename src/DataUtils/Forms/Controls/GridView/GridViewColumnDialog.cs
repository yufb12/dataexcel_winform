using Feng.Forms.Controls.GridControl;
using System;
using System.Windows.Forms;

namespace Feng.Forms
{

    public partial class GridViewColumnDialog : Form
    {
        public GridViewColumnDialog()
        {
            InitializeComponent();
        }

        private ColumnCollection _Columns = null;
        public ColumnCollection Columns
        {
            get
            {
                return _Columns;
            }
        }
        private GridView _gridview = null;
        public GridView Grid
        {
            get
            {
                return _gridview;
            }
        }
        public void Init(GridView gridview, ColumnCollection Columns)
        {
            _Columns = Columns;
            _gridview = gridview;
            foreach (GridViewColumn Column in Columns)
            {
                ListViewItem item = listView.Items.Add(Column.Caption);
                item.Tag = Column;
            }
        }
        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (this.listView.SelectedItems.Count == 1)
                {
                    this.propertyGrid.SelectedObject = this.listView.SelectedItems[0].Tag;
                    return;
                }
                int count = this.listView.SelectedItems.Count;
                object[] values = new object[count];
                for (int i = 0; i < count; i++)
                {
                    values[i] = this.listView.SelectedItems[i].Tag;
                }
                this.propertyGrid.SelectedObjects = values;
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                GridViewColumn column = new GridViewColumn(this.Grid);
                Columns.Add(column);
                column.Caption = "Column" + Columns.IndexOf(column);
                ListViewItem item = listView.Items.Add(column.Caption);
                item.Tag = column;
                this.listView.FocusedItem = item;
                this.propertyGrid.SelectedObject = column;
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

            try
            {
                if (this.listView.FocusedItem != null)
                {
                    Columns.Remove(this.listView.FocusedItem.Tag as GridViewColumn);
                    this.listView.Items.Remove(this.listView.FocusedItem);
                    this.propertyGrid.SelectedObject = null;
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void btnOk_Click(object sender, EventArgs e)
        {

            try
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

            try
            {
                this.Close();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {

            try
            {
                foreach (ListViewItem item in listView.Items)
                {
                    item.Text = (item.Tag as GridViewColumn).Caption;
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {

            try
            {
                if (this.listView.FocusedItem != null)
                {
                    GridViewColumn col = this.listView.FocusedItem.Tag as GridViewColumn;
                    int index = this.Columns.IndexOf(col);
                    if (index > 0)
                    {
                        this.Columns.Remove(col);
                        this.Columns.Insert(index - 1, col);
                        this.listView.FocusedItem.Selected = false;
                        this.listView.Items[this.listView.FocusedItem.Index - 1].Selected = true;
                        this.listView.Items[this.listView.FocusedItem.Index - 1].Focused = true;
                        for (int i = 0; i < this.Columns.Count; i++)
                        {
                            this.listView.Items[i].Text = this.Columns[i].Caption;
                            this.listView.Items[i].Tag = this.Columns[i];
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listView.FocusedItem != null)
                {
                    GridViewColumn col = this.listView.FocusedItem.Tag as GridViewColumn;
                    int index = this.Columns.IndexOf(col);
                    if (index < this.Columns.Count - 1)
                    {
                        this.Columns.Remove(col);
                        this.Columns.Insert(index + 1, col);
                        this.listView.FocusedItem.Selected = false;
                        this.listView.Items[this.listView.FocusedItem.Index + 1].Selected = true;
                        this.listView.Items[this.listView.FocusedItem.Index + 1].Focused = true;
                        for (int i = 0; i < this.Columns.Count; i++)
                        {
                            this.listView.Items[i].Text = this.Columns[i].Caption;
                            this.listView.Items[i].Tag = this.Columns[i];
                        }
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
