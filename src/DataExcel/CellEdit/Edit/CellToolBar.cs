using Feng.Data;
using Feng.Drawing;
using Feng.Excel.Delegates;
using Feng.Excel.Interfaces;
using Feng.Forms.Controls.Designer;
using Feng.Forms.Views;
using Feng.Print;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Excel.Edits
{
    [Serializable]
    public class CellToolBar : Feng.Forms.Views.ToolBarView, ICellEditControl
    {
        public CellToolBar(DataExcel grid) 
        {
            _grid = grid;
            Init();
        }
        public CellToolBar(ICell cell) 
        {
            Cell = cell;
            Init();
        }

        public virtual string ShortName { get { return "CellToolBar"; } set { } }
        private DataExcel _grid = null;
        public DataExcel Grid
        {
            get
            {
                if (this.Cell != null)
                {
                    return this.Cell.Grid;
                }
                return _grid;
            }
        }

 
        public void InitEvent()
        {
            if (Cell != null)
            {

            }
        }

        public override void Init()
        {
            base.Init();
        }

        private ICell _cell = null;
        [Browsable(false)]
        public ICell Cell
        {
            get
            {
                if (_cell != null)
                {
                    if (_cell.OwnMergeCell != null)
                    {
                        return _cell.OwnMergeCell;
                    }
                }
                return _cell;
            }
            set
            {
                _cell = value;
                InitEvent();
            }
        }

        private Font _font = null;
        [Category(CategorySetting.PropertyUI)]
        public override Font Font
        {
            get
            {
                if (this._font == null)
                {
                    return Cell.Font;
                }
                return _font;
            }
            set
            {
                _font = value;
            }
        }
        private Color _forecolor = Color.Empty;
        [Category(CategorySetting.PropertyUI)]
        public override Color ForeColor
        {
            get
            {
                if (this._forecolor == Color.Empty)
                {
                    return Cell.ForeColor;
                }
                return _forecolor;
            }
            set
            {
                _forecolor = value;
            }
        }
        private Color _backcolor = Color.Empty;
        [Category(CategorySetting.PropertyUI)]
        public override Color BackColor
        {
            get
            {
                if (this._backcolor == Color.Empty)
                {
                    return Cell.BackColor;
                }
                return _backcolor;
            }
            set
            {
                _backcolor = value;
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
        public override int Height
        {
            get
            {
                return (int)Cell.Height;
            }
            set
            {
                base.Height = value;
            }
        }
        public override int Width
        {
            get
            {
                return (int)Cell.Width;
            }
            set
            {
                base.Width = value;
            }
        }
        public override int Left
        {
            get
            {
                return (int)Cell.Left;
            }
            set
            {
                base.Left = value;
            }
        }
        public override int Top
        {
            get
            {
                return (int)Cell.Top;
            }
            set
            {
                base.Top = value;
            }
        }
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

        public void Read(DataExcel grid, int version, DataStruct data)
        {
            _grid = grid;
            using (Feng.Excel.IO.BinaryReader stream = new IO.BinaryReader(data.Data))
            {
                DataStruct ds = stream.ReadIndex(0, DataStruct.DataStructNull);
                if (ds != null)
                {
                    base.ReadDataStruct(ds);
                }
                this._backcolor = stream.ReadIndex(1, this._backcolor);
                this._font = stream.ReadIndex(2, this._font);
                this._forecolor = stream.ReadIndex(3, this._forecolor);
                this._id = stream.ReadIndex(4, this._id);
                rowindex = stream.ReadIndex(5, 0);
                columnindex = stream.ReadIndex(6, 0);

            }
            if (this.Grid != null)
            {
                this.Grid.LoadCompleted += new SimpleEventHandler(Grid_LoadCompleted);
            }
        }

        protected int rowindex = 0;
        protected int columnindex = 0;
        void Grid_LoadCompleted(object sender)
        {

            try
            {
                this.Grid.LoadCompleted -= new SimpleEventHandler(Grid_LoadCompleted);
                this._cell = this.Grid[rowindex, columnindex];

                InitEvent();
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
#if DEBUG
                Feng.Utils.TraceHelper.WriteTrace("DataExcel", "DataExcel", "Except", ex);
#endif
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
                    bw.Write(0, base.Data);
                    bw.Write(1, this._backcolor);
                    bw.Write(2, this._font);
                    bw.Write(3, this._forecolor);
                    bw.Write(4, this._id);
                    bw.Write(5, rowindex);
                    bw.Write(6, columnindex);
                    data.Data = bw.GetData();
                }
                return data;
            }
        }
 

        public override void BeginSetCursor(Cursor begincursor)
        {
            Cell.Grid.BeginSetCursor(begincursor);
        }

    
        public override bool OnDraw(object sender, GraphicsObject g)
        {
            return base.OnDraw(this, g);
        }
        public virtual bool InitEdit(object obj)
        {
            
            return false;
        }

        public virtual void EndEdit()
        {
            this.Grid.EndEdit();
        }

        public bool InEdit
        {
            get { return false; }
        }

        public void TextPress(string text)
        {

        }

        public bool DrawCell(IBaseCell cell, GraphicsObject g)
        {
            GraphicsState gs = g.Graphics.Save();
            try
            {
                Rectangle rect = cell.Rect;
                g.Graphics.SetClip(rect);
                g.Graphics.TranslateTransform(rect.Left, rect.Top);
                g.Graphics.DrawRectangle(new Pen(Color.FromArgb(192, 192, 192)), 0, 0, rect.Width, rect.Height);
                base.OnDraw(this, g);
            }
            catch (Exception ex)
            {
                Feng.IO.LogHelper.Log(ex);
            }
            finally
            {
                g.Graphics.Restore(gs);
            }
            return true;
        }

        public bool DrawCellBack(IBaseCell cell, GraphicsObject g)
        {
            return false;
        }

        public bool PrintCell(IBaseCell cell, PrintArgs e, Rectangle rect)
        {
            return false;
        }

        public bool PrintValue(IBaseCell cell, PrintArgs e, Rectangle rect, object value)
        {
            return false;
        }

        public bool PrintCellBack(IBaseCell cell, PrintArgs e)
        {
            return false;
        }



        public void CellValueChanged(IBaseCell cell)
        {

        }

        public override void Invalidate()
        {
            this.Cell.Grid.Invalidate();

        }
        public override void Invalidate(Rectangle rc)
        {
            this.Cell.Grid.Invalidate();
        }

        public virtual ICellEditControl Clone(DataExcel grid)
        {
            CellToolBar celledit = new CellToolBar(grid);
            celledit._backcolor = this._backcolor;
            celledit._font = this._font;
            celledit._forecolor = this._forecolor;
            celledit._hasChildEdit = this._hasChildEdit;
            return celledit;
        }
    }

}
