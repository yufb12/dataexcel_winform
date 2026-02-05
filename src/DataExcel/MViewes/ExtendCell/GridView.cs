#define NoTestReSetSize
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Extend
{
    [Serializable]
    public class GridView : ExtendCell  
    {
        public GridView(DataExcel grid)
            : base(grid)
        { 

        }

        public override bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        { 
            return base.OnDraw(this, g);
        }
 
        #region IImageCell 成员

        private IGridViewCollection _list = null;
        public IGridViewCollection List
        {
            get
            {
                if (_list == null)
                {
                    _list = new GridViewCollection(this.Grid);
                }
                return _list;
            }
            set
            {
                _list = value;
            }
        }

        #endregion
    }
}