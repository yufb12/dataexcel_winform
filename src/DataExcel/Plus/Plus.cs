using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Feng.Excel.Interfaces;

namespace Feng.Excel.Generic
{
    [Serializable]
    public abstract class PlusBase : IPlus
    {

        public virtual void Load(DataExcel grid)
        {
            _grid = grid;
        }
        public abstract string Company { get; }
        public abstract string Name { get; }
        public abstract string Title { get; }
        public abstract string Description { get; }
        public abstract string Copyright { get; }
        public abstract string Culture { get; }
        public abstract string Product { get; }
        //public abstract string State { get; set; }
        public abstract Guid Guid { get; }


        #region IDataExcelGrid 成员
        [NonSerialized]
        private DataExcel _grid;

        public virtual DataExcel Grid
        {
            get { return _grid; }
        }

        #endregion

        #region IPlus 成员


        public virtual void Load(System.Windows.Forms.Form frm)
        { 
        }

        #endregion
    }
}
