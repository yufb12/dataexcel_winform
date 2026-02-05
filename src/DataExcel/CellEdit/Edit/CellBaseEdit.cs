using Feng.Data;
using Feng.Excel.Interfaces;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Views;
using Feng.Print;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [Serializable]
    public abstract class CellBaseEdit : DivView,ICellEditControl
    {
        public CellBaseEdit()
        {
        }
            public CellBaseEdit(DataExcel grid)
        { 
            this.grid = grid;
        }

        public abstract string ShortName { get; set; }
        private DataExcel grid = null;
        public DataExcel Grid
        {
            get
            {
                return grid;
            }
        }
        private bool _hasChildEdit = false;
        [Category(CategorySetting.PropertyDesign)]
        public virtual bool HasChildEdit
        {
            get
            {
                return _hasChildEdit;
            }
            set
            {
                _hasChildEdit = value;
            }
        }
 
        public virtual bool InitEdit(object obj)
        {
            this._inedit = true;
            ICell cell = obj as ICell;
            if (cell != null)
            {
                this.Cell = cell;
            }
            return true;
        }
 
        public virtual void EndEdit()
        {
            this._inedit = false;
            if (this.Grid == null)
                return;
            this.Grid.EndEdit();
        }

        public virtual bool DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        { 
            return false;
        }

        public virtual bool DrawCellBack(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            return false;
        }
 
        public abstract void Read(DataExcel grid, int version, DataStruct data);

        [Browsable(false)]
        public virtual string Version
        {
            get { return string.Empty; }
        }

        [Browsable(false)]
        public virtual int VersionIndex
        {
            get { return 0; }
        }

        [Browsable(false)]
        public virtual string DllName
        {
            get { return string.Empty; }
        }

        [Browsable(false)]
        public virtual string DownLoadUrl
        {
            get { return string.Empty; }
        }
 
        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            IBaseCell cell = sender as IBaseCell;
            if (cell != null)
            {
                DataExcel grid = cell.Grid;
                Point viewloaction = grid.PointControlToView(e.Location);
                if (cell.Rect.Contains(viewloaction))
                {
                    return true;
                }
            }
            return false;
        }
 
        protected bool _inedit = false;
        [Browsable(false)]
        public virtual bool InEdit
        {
            get { return _inedit; }
        }
 
        public virtual bool PrintCellBack(IBaseCell cell, PrintArgs e)
        {
            return false;
        }

        private int _id = -1;
        [Browsable(false)]
        public virtual int AddressID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
         
        public virtual bool PrintCell(IBaseCell cell, PrintArgs e, Rectangle rect)
        {
            return false;
        }

        public virtual bool PrintValue(IBaseCell cell, PrintArgs e, Rectangle rect, object value)
        { 
            return false;
        }
          
        public virtual void TextPress(string text)
        {

        }

  
        private ICell _Cell = null;
        public virtual ICell Cell
        {
            get
            {
                return _Cell;
            }
            set
            {
                _Cell = value;
            }
        }

        public abstract ICellEditControl Clone(DataExcel grid);

        public abstract void ReadDataStruct(DataStruct data);
    }

}
