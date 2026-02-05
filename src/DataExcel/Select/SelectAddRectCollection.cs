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
    public class SelectAddRectCollection : IBounds, IList<ICell>, ICollection<ICell>, IEnumerable<ICell>,
        IAddrangle<ICell>, IDraw, IBackColor
    { 
        public SelectAddRectCollection(DataExcel grid)
        {
            this._grid = grid;

        }

        public List<ICell> list = new List<ICell>();

        #region 用户属性

        #endregion


        [NonSerialized]
        private DataExcel _grid = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DataExcel Grid
        {
            get { return this._grid; }

            set { this._grid = value; }
        }

        private int distance = 0;

        public virtual bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
#warning 此处可以优化
            if (this.EndCell == null)
            {
                return false;
            }
            Point pt = new Point();
            if (this.Bottom > this.SelectCellCollection.Bottom)
            {
                pt.Y = this.Bottom - this.distance;
                pt.X = this.Right;
            }
            else if (this.SelectCellCollection.Top > this.Top)
            {
                pt.Y = this.Top - this.distance;
                pt.X = this.Right;
            }

            if (this.Right > this.SelectCellCollection.Right)
            {
                pt.X = this.Right;
                pt.Y = this.Bottom;
            }
            else if (this.SelectCellCollection.Left > this.Left)
            {
                pt.X = this.Left;
                pt.Y = this.Bottom;
            }

            if (pt != Point.Empty)
            {

                string strvalue = string.Empty;
                if (Orientation)
                {
                    if (this.EndCell.Row.Index > this.SelectCellCollection.EndCell.Row.Index)
                    {
                        double d = 0;
                        if (double.TryParse(this.SelectCellCollection.BeginCell.Text, out d))
                        {
                            strvalue = string.Format("{0}", d + this.EndCell.Row.Index - this.SelectCellCollection.EndCell.Row.Index);
                        }
                        else
                        {
                            strvalue = this.SelectCellCollection.BeginCell.Text;
                        }
                    }
                    else if (this.EndCell.Row.Index < this.SelectCellCollection.BeginCell.Row.Index)
                    {
                        double d = 0;
                        if (double.TryParse(this.SelectCellCollection.BeginCell.Text, out d))
                        {
                            strvalue = string.Format("{0}", d + this.EndCell.Row.Index - this.SelectCellCollection.EndCell.Row.Index);
                        }
                        else
                        {
                            strvalue = this.SelectCellCollection.BeginCell.Text;
                        }
                    }
                }
                else
                {
                    if (this.SelectCellCollection.EndCell == null)
                    {
                        strvalue = this.SelectCellCollection.BeginCell.Text;
                    }
                    else
                    {
                        strvalue = this.SelectCellCollection.EndCell.Text;
                    }
                }

                Feng.Drawing.GraphicsHelper.DrawString(g, strvalue, this.Grid.Font, Brushes.Red, pt);
            }


            DrawHelper.DrawRect(g.Graphics, this.Rect, this.BackColor, 3f, System.Drawing.Drawing2D.DashStyle.Dash);
            return false;
        }



        public override string ToString()
        {
            string str = string.Format("Left:{0},Top{1},Width{2},Height{3}", this.Left, this.Top, this.Width, this.Height);
            return str;
        }

        private ICell _endcell = null;

        public bool Orientation = true;

        private void SetEndCell2(ICell cell)
        {

            this._endcell = cell;
            Point pt = cell.Rect.Location;
            if (this.SelectCellCollection.Left < pt.X && this.SelectCellCollection.Right > pt.X)
            {
                Orientation = true;
            }
            else if (this.SelectCellCollection.Top < pt.Y && this.SelectCellCollection.Bottom > pt.Y)
            {
                Orientation = false;
            }
            else
            {
                if ((Math.Abs(this.SelectCellCollection.Right - pt.X) >
                    Math.Abs(this.SelectCellCollection.Bottom - pt.Y)))
                {
                    Orientation = false;
                }
                else
                {
                    Orientation = true;
                }
            }
        }

        private void SetEndCell(ICell value)
        {

#if DEBUG
            try
            {

#endif


#if DEBUG
                if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control)
                {

                }
#endif


                this.Grid.BeginReFresh();
                SetEndCell2(value);
                ICell cell = value;
                this._endcell = cell;
                SetSize();
                this.Grid.EndReFresh();


#if DEBUG
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
#endif
        }

        public ICell EndCell
        {
            get
            {
                if (this._endcell == null)
                {
                    return this._SelectCellCollection.EndCell;
                }
                return this._endcell;
            }
            set
            {
                this._endcell = value;
                SetEndCell(value);
            }
        }
        public void SetSize()
        {
            Rectangle bounds = Rectangle.Empty;
            int maxrowindex = 0;
            //Math.Max(this.SelectCellCollection.MaxCell.Row.Index, this.EndCell.Row.Index);
            int maxcolumnindex = 0;
            //Math.Max(this.SelectCellCollection.MaxCell.Column.Index, this.EndCell.Column.Index);

            int minrowindex = 0; 
            //Math.Min(this.SelectCellCollection.MinCell.Row.Index, this.EndCell.Row.Index);
            int mincolumnindex = 0; 
            //Math.Min(this.SelectCellCollection.MinCell.Column.Index, this.EndCell.Column.Index);

            if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control)
            {

            }
            if (Orientation)
            {
                if (this.EndCell.Row.Index > this.SelectCellCollection.MaxCell.Row.Index)
                {
                    mincolumnindex = this.SelectCellCollection.MinColumn();
                    maxcolumnindex = this.SelectCellCollection.MaxColumn();
                    minrowindex = this.SelectCellCollection.MaxRow()+1;
                    maxrowindex = this.EndCell.MaxRowIndex;
                }
                else if (this.EndCell.Row.Index < this.SelectCellCollection.MinCell.Row.Index)
                {
                    mincolumnindex = this.SelectCellCollection.MinColumn();
                    maxcolumnindex = this.SelectCellCollection.MaxColumn();
                    minrowindex = this.EndCell.Row.Index;
                    maxrowindex = this.SelectCellCollection.MinRow()-1;
                }
            }
            else
            {
                if (this.EndCell.Column.Index > this.SelectCellCollection.MaxCell.Column.Index)
                {
                    minrowindex = this.SelectCellCollection.MinRow();
                    maxrowindex = this.SelectCellCollection.MaxRow();
                    mincolumnindex = this.SelectCellCollection.MaxColumn()+1;
                    maxcolumnindex = this.EndCell.MaxColumnIndex;
                }
                else if (this.EndCell.Column.Index < this.SelectCellCollection.MinCell.Column.Index)
                {
                    minrowindex = this.SelectCellCollection.MinRow();
                    maxrowindex = this.SelectCellCollection.MaxRow();
                    mincolumnindex = this.EndCell.Column.Index;
                    maxcolumnindex = this.SelectCellCollection.MinColumn()-1;
                }
            }
            //ICell maxcell = this.Grid[maxrowindex, maxcolumnindex];
            //ICell mincell = this.Grid[minrowindex, mincolumnindex];


            int maxrow = maxrowindex;
            int maxcolumn = maxcolumnindex;
            int minrow = minrowindex;
            int mincolumn = mincolumnindex;
            List<IRow> rows = this.Grid.VisibleRows;
            List<IColumn> columns = this.Grid.VisibleColumns;
            int top = this.Grid.Height + 2000;
            int left = this.Grid.Width + 2000;
            int width = 0;
            int height = 0; 
            foreach (IRow item in rows)
            { 
                if (item.Index >= minrow && item.Index <= maxrow)
                {
                    height = height + item.Height;
                    if (item.Top < top)
                    {
                        top = item.Top;
                    }
                }
            }

            foreach (IColumn item in columns)
            {
                if (item.Index >= mincolumn && item.Index <= maxcolumn)
                {
                    width = width + item.Width;
                    if (item.Left < left)
                    {
                        left = item.Left;
                    }
                }
            }
            this.Top = top;
            this.Left = left;
            this.Width = width;
            this.Height = height;

            //DataExcel.GetRect(mincell, maxcell, ref bounds);

            //this.Top = bounds.Top;
            //this.Left = bounds.Left;
            //this.Width = bounds.Width;
            //this.Height = bounds.Height;


        }
        private ISelectCellCollection _SelectCellCollection = null;
        public ISelectCellCollection SelectCellCollection
        {
            get { return this._SelectCellCollection; }

            set
            {
                this._SelectCellCollection = value;
                this._endcell = null;
                this.Left = this._SelectCellCollection.Left;
                this.Top = this._SelectCellCollection.Top;
                this.Width = this._SelectCellCollection.Width;
                this.Height = this._SelectCellCollection.Height;
            }
        }

        #region IList<clsxieyi> 成员

        public int IndexOf(ICell item)
        {
            return list.IndexOf(item);
        }

        public void Insert(int index, ICell item)
        {
            list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            list.RemoveAt(index);
        }

        public ICell this[int index]
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

        #region ICollection<clsxieyi> 成员

        public void Add(ICell item)
        {
            list.Add(item);
        }

        public void Clear()
        {
            list.Clear();
        }

        public virtual bool Contains(ICell item)
        {
            return list.Contains(item);
        }

        public void CopyTo(ICell[] array, int arrayIndex)
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

        public virtual bool Remove(ICell item)
        {
            return this.list.Remove(item);
        }

        #endregion

        #region IEnumerable<clsxieyi> 成员

        public IEnumerator<ICell> GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.list.GetEnumerator();
        }

        #endregion

        #region ICol<clsxieyi> 成员
        public void AddRange(params ICell[] ts)
        {
            foreach (ICell c in ts)
            {
                this.Add(c);
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

        #region IControlColor 成员
        private Color _backcolor = Color.SlateBlue;
        public Color BackColor
        {
            get
            {
                return _backcolor;
            }
            set
            {
                _backcolor = value;
            }
        }
        #endregion
    }


}
