using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Drawing.Drawing2D;
using Feng.Forms.Controls.Designer;
using Feng.Drawing;
using System.Security.Permissions;
using System.Data;

using Feng.Data;
using System.Reflection;
using Feng.Enums;
using Feng.Forms.Controls.GridControl;
using Feng.Print;

namespace Feng.Forms.Controls.TreeView
{ 
    public partial class DataTreeView : DataTreeViewBase
    {
        private DataTreeViewControl _tree = null;
        public DataTreeView(DataTreeViewControl tree)
        {
            hscroll = new DataTreeViewHScroll(this);
            vscroll = new DataTreeViewVScroll(this);
            _tree = tree;
            Init();
        }
        public DataTreeViewControl Grid {
            get {
                return _tree;
            }
        }
        public override Control Control
        {
            get
            {
                return Grid;
            }
        }
        public void InitEvent()
        {

        }

        public override void Init()
        {
            base.ShowLines = false;
            base.ShowRowHeader = false;
            base.ShowColumnHeader = false;
            base.ShowFooter = false;
            this.Columns.Add(new TreeViewSingleColumn(this));

            vscroll.Visible = false;
            hscroll.Visible = false; 
            base.Init();
        }
        DataTreeViewHScroll hscroll = null;
        DataTreeViewVScroll vscroll = null;
        public override VScrollerView VScroll { get {
                return vscroll;
            } }
        public override HScrollerView HScroll
        {
            get
            {
                return hscroll;
            }
        }
        public override bool ReadOnly
        {
            get
            {
                return this.Grid.ReadOnly;
            }
            set
            {
                this.Grid.ReadOnly = value;
                base.ReadOnly = value;
            }
        } 
        [Category(CategorySetting.PropertyUI)]
        public override Font Font
        {
            get
            {
                if (this._font == null)
                {
                    return Grid.Font;
                }
                return _font;
            }
            set
            {
                _font = value;
            }
        } 
        [Category(CategorySetting.PropertyUI)]
        public override Color ForeColor
        {
            get
            {
                if (this._forecolor == Color.Empty)
                {
                    return Grid.ForeColor;
                }
                return _forecolor;
            }
            set
            {
                _forecolor = value;
            }
        } 
        [Category(CategorySetting.PropertyUI)]
        public override Color BackColor
        {
            get
            {
                if (this._backcolor == Color.Empty)
                {
                    return Grid.BackColor;
                }
                return _backcolor;
            }
            set
            {
                _backcolor = value;
            }
        }
        public override int Height
        {
            get
            {
                return (int)Grid.Height;
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
                return (int)Grid.Width;
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
                return 0;
            }
            set
            {
            }
        }
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
 
        public override void AddControl(Control ctl)
        {
            if (!this.Grid.Controls.Contains(ctl))
            {
                this.Grid.Controls.Add(ctl);
            }
            base.AddControl(ctl);
        }

        public override void RemoveControl(Control ctl)
        {
            if (this.Grid.Controls.Contains(ctl))
            {
                this.Grid.Controls.Remove(ctl);
            }
            base.RemoveControl(ctl);
        }

        public override Point PointToClient(Point pt)
        {
            Point p = (pt);
            return new Point(p.X - this.Left, p.Y - this.Top);
        }

        public override Point PointToScreen(Point pt)
        {
            return this.Grid.PointToScreen(pt);
        }
         

        public bool InEdit
        {
            get { return false; }
        }

        public void TextPress(string text)
        {

        }
 
        public bool DrawCellBack(GraphicsObject g)
        {
            return false;
        }

        public bool PrintCell(PrintArgs e, RectangleF rect)
        {
            return false;
        }

        public bool PrintValue(PrintArgs e, RectangleF rect, object value)
        {
            return false;
        }

        public bool PrintCellBack(PrintArgs e)
        {
            return false;
        }

        public string DataUrl
        {
            get
            {
                return string.Empty;
            }
            set
            {

            }
        }

        public override void Invalidate()
        {
            try
            {
                this.Grid.Invalidate();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        public override void Invalidate(Rectangle rc)
        {
            try
            {
                this.Grid.Invalidate();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }
 
        public override void BeginSetCursor(Cursor begincursor)
        {
            this.Grid.BeginSetCursor(begincursor);
        }

 
    }


}
