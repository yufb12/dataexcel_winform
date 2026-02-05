#define NoTestReSetSize
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using Feng.Excel.Interfaces;
using Feng.Forms.Views;

namespace Feng.Excel.Extend
{
    [Serializable]
    public class ImageCell : ExtendCell, IImageCell  
    {
        public ImageCell(DataExcel grid)
            : base(grid)
        { 

        }
 
        #region IImage 成员
        private Image _image = null;
        public virtual Image Image
        {
            get
            {
                return _image;
            }
            set
            {

                _image = value;
                if (_image != null)
                {
                    this.Width = this._image.Width;
                    this.Height = this._image.Height;
                }
            }
        }

        #endregion

        public override bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            if (this._image != null)
            {
                g.Graphics.DrawImage(this._image, this.Rect);
            }
            return base.OnDraw(this, g);
        }
        public void OpenImage()
        {
            using (System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog())
            {
                dlg.Filter = "(bmp,jpg,jpeg,png)|*.bmp;*.jpg;*.jpeg;*.png|*.bmp|*.bmp|*.jpg|*.jpg|*.jpeg|*.jpeg|*.png|*.png";
                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    string file = dlg.FileName;
                    Image img = Image.FromFile(file);
                    this.Image = img;
                }
            }
        }
        public override bool OnMouseDoubleClick(object sender, System.Windows.Forms.MouseEventArgs e, EventViewArgs ve)
        {
            if (!this.ReadOnly)
            {
                using (System.Windows.Forms.OpenFileDialog dlg = new System.Windows.Forms.OpenFileDialog())
                {
                    dlg.Filter = "(bmp,jpg,jpeg,png)|*.bmp;*.jpg;*.jpeg;*.png|*.bmp|*.bmp|*.jpg|*.jpg|*.jpeg|*.jpeg|*.png|*.png";
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        string file = dlg.FileName;
                        Image img = Image.FromFile(file);
                        this.Image = img;
                    }
                }
            }
            return base.OnMouseDoubleClick(this,e, ve);
        }

        #region IImageCell 成员

        private IImageCellCollection _imagecells = null;
        public IImageCellCollection ImageCellCollection
        {
            get
            {
                if (_imagecells == null)
                {
                    _imagecells = new ImageCellCollection(this.Grid);
                }
                return _imagecells;
            }
            set
            {
                _imagecells = value;
            }
        }

        #endregion
    }
}