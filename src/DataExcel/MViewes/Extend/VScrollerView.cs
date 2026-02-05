using Feng.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Excel.Extend
{ 
    public class DataExcelViewVScroll : VScrollerView
    {
        public DataExcelViewVScroll(DataExcel grid)
        {
            _Grid = grid;
            grid.FirstDisplayRowChanged += Grid_FirstDisplayRowChanged;
        }
 
        private void Grid_FirstDisplayRowChanged(object sender, int index)
        {
            try
            {
                if (index > this.Max)
                {
                    this.Max = index;
                }
                this.Value = index;
            }
            catch (Exception ex)
            {
                Feng.Utils.TraceHelper.WriteTrace("Feng.Excel.Extend", "DataExcelViewHScroll", "Grid_FirstDisplayColumnChanged", ex);
            }
        }
        private DataExcel _Grid = null;
        public DataExcel Grid
        {
            get
            {
                return _Grid;
            }
        }
        public override int Height
        {
            get
            {
                return Grid.Height - this.Width - this.Top;
            }
            set
            {
            }
        }

        public override int Left
        {
            get
            {
                return Grid.Width - this.Width;
            }
            set
            {
            }
        }

        public override Rectangle Rect { get { return new Rectangle(this.Left, this.Top, this.Width, this.Height); } }

        public override int Top
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }
        bool lck = false;
        public override void OnValueChanged(int value)
        {
            if (!this.Visible)
                return;
            try
            {
                if (lck)
                    return;
                lck = true;
                int position = value;
                if (position < this.Grid.Rows.Max)
                {
                    this.Grid.FirstDisplayedRowIndex = (position);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            finally
            {
                lck = false;
            }
            base.OnValueChanged(value);
        }

        public override bool Visible { get { return this.Grid.ShowVerticalScroller; } }

        public virtual bool OnMouseDown(object sender, MouseEventArgs e)
        {
            if (!this.Visible)
                return false;
            Point pt = this.Grid.PointControlToView(e.Location);
            if (this.Rect.Contains(pt))
            {
                return base.OnMouseDown(pt);
            }
            return false;
        }

        public virtual bool OnMouseUp(object sender, MouseEventArgs e)
        {
            if (!this.Visible)
                return false;
            return base.OnMouseUp();
        }

        public virtual bool OnMouseMove(object sender, MouseEventArgs e)
        {
            if (!this.Visible)
                return false;
            Point pt = this.Grid.PointControlToView(e.Location);
            if (this.Rect.Contains(pt) || this.MoveSelected)
            {
                Feng.Excel.DataExcel.DebugText = "DataExcelViewVScroll+OnMouseMove" + pt.ToString();
                return base.OnMouseMove(pt);
            }
            return false;
        }

        public virtual bool OnMouseClick(object sender, MouseEventArgs e)
        {
            if (!this.Visible)
                return false;
            Point pt = this.Grid.PointControlToView(e.Location);
            if (this.Rect.Contains(pt))
            {
                return base.OnMouseClick(pt);
            }
            return false;
        }
    }
}