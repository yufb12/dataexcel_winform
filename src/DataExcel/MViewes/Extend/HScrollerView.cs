using Feng.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Feng.Excel.Extend
{

    public class DataExcelViewHScroll : HScrollerView
    {
        public DataExcelViewHScroll(DataExcel grid)
        {
            _Grid = grid;
            grid.FirstDisplayColumnChanged += Grid_FirstDisplayColumnChanged;
        }

        private void Grid_FirstDisplayColumnChanged(object sender, int index)
        {
            try
            {
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

        public override int Top
        {
            get
            {
                return Grid.Height - this.Height;
            }
            set
            {
            }
        }

        public override int Left
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }

        public override int Width
        {
            get
            {
                return Grid.Width - this.Height;
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
                if (position < this.Grid.Columns.Max)
                {
                    this.Grid.FirstDisplayedColumnIndex = (position);
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

        public override bool Visible { get { return this.Grid.ShowHorizontalScroller; } }

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
            if (this.Rect.Contains(pt)|| this.MoveSelected)
            {
                Feng.Excel.DataExcel.DebugText = "DataExcelViewHScroll+OnMouseMove" + pt.ToString();
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