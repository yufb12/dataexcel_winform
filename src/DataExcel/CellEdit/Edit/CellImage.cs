using Feng.Data;
using Feng.Excel.Commands;
using Feng.Excel.Interfaces;
using Feng.Forms.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellImage : CellBaseEdit
    {
        public CellImage(DataExcel grid)
            : base(grid)
        {
        }

        public override string ShortName { get { return "CellImage"; } set { } }

        private ImageLayout _SizeMode = ImageLayout.Center;
        public virtual ImageLayout SizeMode
        {
            get
            {
                return this._SizeMode;
            }
            set
            {
                this._SizeMode = value;
            }
        }

        private bool _ClickShow = false;
        public virtual bool ClickShow 
        { 
            get { return _ClickShow; } 
            set { _ClickShow = value; }
        }

        private void DrawImage(Feng.Drawing.GraphicsObject g, Rectangle bounds, Image image)
        {
            Rectangle rect = Feng.Drawing.ImageHelper.ImageRectangleFromSizeMode(SizeMode, image, bounds);
            g.Graphics.DrawImage(image, rect);
        }

        public override bool DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            Image image = cell.Value as Image;
            if (image != null)
            {
                DrawImage(g, cell.Rect, image);
                return true;
            }
            return false;
        }

        public override bool DrawCellBack(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            return false;
        }

        public override void Read(DataExcel grid, int version, DataStruct data)
        {
            ReadDataStruct(data);
        }

        public override void ReadDataStruct(DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader bw = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                this.AddressID = bw.ReadIndex(1, 0);
                this.SizeMode = (ImageLayout)bw.ReadIndex(2, 0);
                this._ClickShow =  bw.ReadIndex(3, this._ClickShow);
            }
        }
        [Browsable(false)]
        public override DataStruct Data
        {
            get
            {
                Type t = this.GetType();
                DataStruct data = new DataStruct()
                {
                    DllName = this.DllName,
                    Version = this.Version,
                    AessemlyDownLoadUrl = this.DownLoadUrl,
                    FullName = t.FullName,
                    Name = t.Name,
                };

                using (Feng.Excel.IO.BinaryWriter bw = new Feng.Excel.IO.BinaryWriter())
                {
                    bw.Write(1, this.AddressID);
                    bw.Write(2, (int)this.SizeMode);
                    bw.Write(3, this._ClickShow);
                    data.Data = bw.GetData();
                }
                return data;
            }
        }

        public override bool OnMouseClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            IBaseCell cell = sender as IBaseCell;
            if (cell == null)
                return false;
            if (cell.Visible)
            {
                if (this.ClickShow)
                {
                    Feng.Excel.Edits.EditForm.CellImageShow frm = new Feng.Excel.Edits.EditForm.CellImageShow();
                    frm.pictureBox1.Image = cell.BackImage;
                    frm.Show();
                }
                else if (System.Windows.Forms.Control.ModifierKeys== Keys.Control)
                {
                    Feng.Excel.Edits.EditForm.CellImageShow frm = new Feng.Excel.Edits.EditForm.CellImageShow();
                    frm.pictureBox1.Image = cell.BackImage;
                    frm.Show();
                }
            }
            return base.OnMouseClick(sender, e, ve);
        }

        public override bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            IBaseCell cell = sender as IBaseCell;
            if (cell == null)
                return false;
            if (!cell.ReadOnly)
            {
                using (System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog())
                {
                    dlg.Filter = "(bmp,jpg,jpeg,png)|*.bmp;*.jpg;*.jpeg;*.png|*.bmp|*.bmp|*.jpg|*.jpg|*.jpeg|*.jpeg|*.png|*.png";
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string file = dlg.FileName;
                        Image img = Image.FromFile(file);
                        if (cell.Grid.CanUndoRedo)
                        {
                            CellValueCommand cmd = new CellValueCommand();
                            cmd.Text = cell.Text;
                            cmd.Value = cell.Value;
                            cmd.Cell = cell;
                            cell.Grid.Commands.Add(cmd);
                        }
                        cell.Value = img;
                        Size sf = img.Size;
                        int height = sf.Height; ;
                        if (height > this.Grid.Height / 3)
                        {
                            height = this.Grid.Height / 3;
                        }
                        int width = sf.Width;
                        if (width > this.Grid.Width / 3)
                        {
                            width = this.Grid.Width / 3;
                        }
                        cell.ContensHeigth = height;
                        cell.ContensWidth = width;
                        if (!cell.IsMergeCell)
                        {
                            if (cell.ContensWidth > cell.Grid.DefaultColumnWidth)
                            {
                                cell.Grid.RefreshColumnWidth(cell.Column);
                            }
                        }
                        this.Grid.EndEditClear();
                    }
                }
                return true;
            }
            return false;
        }

        public override bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            IBaseCell cell = sender as IBaseCell;
            if (cell == null)
                return false;
            if (e.Alt || e.Control || e.Shift)
            {

            }
            else
            {
                if (e.KeyData == Keys.Right)
                {
                    cell.Grid.MoveFocusedCellToRightCell();
                }
                if (e.KeyData == Keys.Left)
                {
                    cell.Grid.MoveFocusedCellToLeftCell();
                }
                if (e.KeyData == Keys.Delete)
                {
                    if (!cell.ReadOnly)
                    {

                        cell.BackImage = null;
                    }
                }
                if (e.KeyData == Keys.Enter)
                {
                    Feng.Excel.Edits.EditForm.CellImageShow frm = new Feng.Excel.Edits.EditForm.CellImageShow();
                    frm.pictureBox1.Image = cell.BackImage;
                    frm.Show();
                    return true;
                }
            }
            return false;
        }

        public override ICellEditControl Clone(DataExcel grid)
        {
            CellImage celledit = new CellImage(grid);
            celledit._SizeMode = this._SizeMode;
            celledit._ClickShow = this._ClickShow;
            return celledit;
        }
    }

}
