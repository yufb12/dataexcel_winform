using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using Feng.Forms.Interface;
using Feng.Excel.Interfaces;
using Feng.Excel.Drawing;

namespace Feng.Excel.Collections
{
    [Serializable]
    public class MergeCellCollectionAddRect : IBounds
    {
        private List<IMergeCell> list = new List<IMergeCell>();

        public MergeCellCollectionAddRect(DataExcel grid)
        {
            _grid = grid;
        }

        private MergeCellCollectionRect _MergeCellCollectionRect = null;
        public MergeCellCollectionRect MergeCellCollectionRect
        {
            get { return _MergeCellCollectionRect; }
            set { this._MergeCellCollectionRect = value; }
        }

        private ICell _endcell = null;

        public bool Orientation = true;
        private void SetEndCell(ICell cell)
        {

            this._endcell = cell;
            Point pt = cell.Rect.Location;
            if (this.MergeCellCollectionRect.Left < pt.X && this.MergeCellCollectionRect.Right > pt.X)
            {
                Orientation = true;
            }
            else if (this.MergeCellCollectionRect.Top < pt.Y && this.MergeCellCollectionRect.Bottom > pt.Y)
            {
                Orientation = false;
            }
            else
            {
                if ((Math.Abs(this.MergeCellCollectionRect.Right - pt.X) > Math.Abs(this.MergeCellCollectionRect.Bottom - pt.Y)))
                {
                    Orientation = false;
                }
                else
                {
                    Orientation = true;
                }
            }
            Rectangle rectfrom = this.MergeCellCollectionRect.Rect;
            Rectangle rectto = this.EndCell.Rect;
            int x1, x2, y1, y2 = 0;
            x1 = System.Math.Min(rectfrom.Left, rectto.Left);
            x2 = System.Math.Max(rectfrom.Right, rectto.Right);
            y1 = System.Math.Min(rectfrom.Top, rectto.Top);
            y2 = System.Math.Max(rectfrom.Bottom, rectto.Bottom);
            if (Orientation)
            {

                this.Width = this.MergeCellCollectionRect.Width;
                this.Left = this.MergeCellCollectionRect.Left;
                if (this.MergeCellCollectionRect.Top < this._endcell.Top)
                {
                    this.Top = this.MergeCellCollectionRect.Top;
                    _height = y2 - y1;
                    int i = Feng.Utils.ConvertHelper.ToInt32(System.Math.Ceiling(_height / this.MergeCellCollectionRect.Height * 1m));
                    if (i > 0)
                    {
                        _height = i * this.MergeCellCollectionRect.Height;
                    }
                }
                else
                {
                    this.Top = this._endcell.Top;
                    _height = y2 - y1;
                    int i = Feng.Utils.ConvertHelper.ToInt32(System.Math.Ceiling(_height / this.MergeCellCollectionRect.Height * 1m));
                    if (i > 0)
                    {
                        _height = i * this.MergeCellCollectionRect.Height;
                    }
                }
            }
            else
            {
                this.Height = this.MergeCellCollectionRect.Height;
                this.Top = this.MergeCellCollectionRect.Top;
                if (this.MergeCellCollectionRect.Left < this._endcell.Left)
                {
                    this.Left = this.MergeCellCollectionRect.Left;


                    _width = x2 - x1;
                    int i = Feng.Utils.ConvertHelper.ToInt32(System.Math.Ceiling(_width / this.MergeCellCollectionRect.Width * 1m));
                    if (i > 0)
                    {
                        _width = i * this.MergeCellCollectionRect.Width;
                    }
                }
                else
                {
                    this.Left = this._endcell.Left;

                    _width = x2 - x1;
                    int i = Feng.Utils.ConvertHelper.ToInt32(System.Math.Ceiling(_width / this.MergeCellCollectionRect.Width * 1m));
                    if (i > 0)
                    {
                        _width = i * this.MergeCellCollectionRect.Width;
                    }
                }

            }


        }

        public ICell EndCell
        {
            get { return this._endcell; }
            set
            {
                this._endcell = value;
                SetEndCell(value);
            }
        }

        #region IList<IMergeCell> 成员

        public int IndexOf(IMergeCell item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, IMergeCell item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public IMergeCell this[int index]
        {
            get
            {
                return this.list[index];
            }
            set
            {
                list[index] = value;
            }
        }


        #endregion

        #region ICollection<IMergeCell> 成员

        public void Add(IMergeCell item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public virtual bool Contains(IMergeCell item)
        {
            return list.Contains(item);
        }

        public void CopyTo(IMergeCell[] array, int arrayIndex)
        {
            this.list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.list.Count; }
        }

        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        public virtual bool Remove(IMergeCell item)
        {
            return this.list.Remove(item);
        }

        #endregion

        #region IEnumerable<IMergeCell> 成员

        public IEnumerator<IMergeCell> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        #endregion

        #region ICol<IMergeCell> 成员
        public void AddRange(params IMergeCell[] ts)
        {
            foreach (IMergeCell c in ts)
            {
                this.Add(c);
            }
        }

        #endregion

        #region IDataExcelGrid 成员
        [NonSerialized]
        private DataExcel _grid = null;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataExcel Grid
        {
            get { return _grid; }
        }

        #endregion

        #region IDraw 成员

        private int distance = 0;
        public void OnDraw(Feng.Drawing.GraphicsObject g)
        {
#warning 此处可以优化
            if (this.EndCell == null)
            {
                return;
            }
            Point pt = new Point();
            if (this.Bottom > this.MergeCellCollectionRect.Bottom)
            {
                pt.Y = this.Bottom - this.distance;
                pt.X = this.Right;
            }
            else if (this.MergeCellCollectionRect.Top > this.Top)
            {
                pt.Y = this.Top - this.distance;
                pt.X = this.Right;
            }

            if (this.Right > this.MergeCellCollectionRect.Right)
            {
                pt.X = this.Right;
                pt.Y = this.Bottom;
            }
            else if (this.MergeCellCollectionRect.Left > this.Left)
            {
                pt.X = this.Left;
                pt.Y = this.Bottom;
            }

            if (pt != Point.Empty)
            {

                string strvalue = string.Empty;
                if (Orientation)
                {
                    if (this.EndCell.Row.Index > this.MergeCellCollectionRect.EndCell.Row.Index)
                    {
                        double d = 0;
                        if (double.TryParse(this.MergeCellCollectionRect.FirstMergeCell.BeginCell.Text, out d))
                        {
                            strvalue = string.Format("{0}", d + this.EndCell.Row.Index - this.MergeCellCollectionRect.EndCell.Row.Index);
                        }
                        else
                        {
                            strvalue = this.MergeCellCollectionRect.FirstMergeCell.BeginCell.Text;
                        }
                    }
                    else if (this.EndCell.Row.Index < this.MergeCellCollectionRect.FirstMergeCell.BeginCell.Row.Index)
                    {
                        double d = 0;
                        if (double.TryParse(this.MergeCellCollectionRect.FirstMergeCell.BeginCell.Text, out d))
                        {
                            strvalue = string.Format("{0}", d + this.EndCell.Row.Index - this.MergeCellCollectionRect.EndCell.Row.Index);
                        }
                        else
                        {
                            strvalue = this.MergeCellCollectionRect.FirstMergeCell.BeginCell.Text;
                        }
                    }
                }
                else
                {
                    if (this.MergeCellCollectionRect.EndCell == null)
                    {
                        strvalue = this.MergeCellCollectionRect.FirstMergeCell.BeginCell.Text;
                    }
                    else
                    {
                        strvalue = this.MergeCellCollectionRect.EndCell.Text;
                    }
                }

                Feng.Drawing.GraphicsHelper.DrawString(g, strvalue, this.Grid.Font, Brushes.Red, pt);
            }


            DrawHelper.DrawRect(g.Graphics, this.Rect, Color.CadetBlue, 3f, System.Drawing.Drawing2D.DashStyle.Dash);


        }

        #endregion

        #region IBounds 成员

        private int _left = 0;
        public int Left
        {
            get
            {
                return _left;
            }
            set
            {
                _left = value;
            }
        }

        private int _height = 0;
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        public int Right
        {
            get
            {

                return _width + _left;
            }

        }
        public int Bottom
        {
            get
            {

                return _top + _height;
            }

        }
        private int _top = 0;
        public int Top
        {
            get
            {
                return _top;
            }
            set
            {
                _top = value;
            }
        }

        private int _width = 0;
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        public Rectangle Rect
        {
            get
            {
                return new Rectangle(this._left, this._top, this._width, this._height);
            }
        }
        #endregion


    }
}
