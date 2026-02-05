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
    public class MergeCellCollectionRect : IBounds
    {
        public MergeCellCollectionRect()
        {

        }
        private List<IMergeCell> list = new List<IMergeCell>();

        public MergeCellCollectionRect(DataExcel grid)
        {
            _grid = grid;
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
        [Browsable(false)]
        public DataExcel Grid
        {
            get { return _grid; }
        }

        #endregion

        #region IDraw 成员

        public void OnDraw(Feng.Drawing.GraphicsObject g)
        {
            using (Pen pen = new Pen(this.Grid.SelectBorderColor, this.Grid.SelectBorderWidth))
            {

                Rectangle rectf = this.Rect;
                g.Graphics.DrawRectangle(pen, rectf.Left, rectf.Top, rectf.Width, rectf.Height);
                DrawHelper.DrawCorssSelectRect(g.Graphics, this.Rect);
            }
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

        public virtual bool AddRectContains(Point pt)
        {
#warning 此处可以优化
            Rectangle rect = new Rectangle();
            rect.Width = 6;
            rect.Height = 6;
            rect.Location = new Point(this.Right - 3, this.Bottom - 3);
            rect.Inflate(3, 3);
            return rect.Contains(pt);
        }
        private IMergeCell _FirstMergeCell = null;
        public IMergeCell FirstMergeCell
        {
            get { return _FirstMergeCell; }
            set
            {
                _FirstMergeCell = value;
                this._top = this._FirstMergeCell.Top;
                this._left = this._FirstMergeCell.Left;
                this._width = this._FirstMergeCell.Width;
                this._height = this._FirstMergeCell.Height;
            }
        }
        public ICell _endcell = null;
        public ICell EndCell
        {
            get
            {
                if (_endcell == null)
                {
                    return _FirstMergeCell.EndCell;
                }
                return _endcell;
            }

            set
            {
                if (value == null)
                {
                    return;
                }
                _endcell = value;

                SetSize();
            }
        }

        private ICell _firstcell
        {
            get { return _FirstMergeCell.BeginCell; }
        }

        public void SetSize()
        {

            Rectangle rectfrom = this.FirstMergeCell.Rect, rectto = this.EndCell.Rect;

            int x1, x2, y1, y2 = 0;
            x1 = System.Math.Min(rectfrom.Left, rectto.Left);
            x2 = System.Math.Max(rectfrom.Right, rectto.Right);
            y1 = System.Math.Min(rectfrom.Top, rectto.Top);
            y2 = System.Math.Max(rectfrom.Bottom, rectto.Bottom);
            _width = x2 - x1;

            _height = y2 - y1;

            _left = x1;
            _top = y1;
            return;
        }

    }
}
