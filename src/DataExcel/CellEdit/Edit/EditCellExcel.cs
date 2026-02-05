using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using Feng.Excel.Designer;
using System.Drawing.Design;
using Feng.Utils;
using Feng.Print;
using Feng.Forms.Controls;
using Feng.Data;
using Feng.Excel.Interfaces;
using Feng.Forms.Controls.Designer;
using Feng.Excel.Commands;
using Feng.Forms.Interface;
using Feng.Forms.Views;
using Feng.Drawing;
using System.Drawing.Drawing2D;

namespace Feng.Excel.Edits
{
    public class CellExcel : CellBaseEdit, IEdit, ICellEditControl
    {
        public override string ShortName { get { return "CellExcel"; } set { } }
        private CellEditDataExcel edit = null;
        private OperatorView operatorView = null;
        public CellEditDataExcel Edit{ get { return edit; } }
        public CellExcel(DataExcel grid) :
            base(grid)
        {
            operatorView = new OperatorView(this);
            operatorView.Width = 16;
            operatorView.Height = 16;
            operatorView.Top = 0;
            operatorView.Left = 0;
            edit = new CellEditDataExcel(this,grid);
            //edit.DebugName = "CellEditDataExcel";
            edit.Init();
            edit.Width = this.Width;
            edit.Height = this.Height;
        }

        public string Path { get; set; }
        public enum VersionUpdateMode
        {
            最新,
            脱机,
            
        }
        public int FileVersionUpdateMode { get; set; }
        public override void EndEdit()
        {
        }

        public override void Read(DataExcel grid, int version, DataStruct data)
        {
            ReadDataStruct(data);
        }

        public override void ReadDataStruct(DataStruct data)
        {
            using (Feng.Excel.IO.BinaryReader bw = new Feng.Excel.IO.BinaryReader(data.Data))
            {
                this.AddressID = bw.ReadIndex(1, 0);
                this.FileVersionUpdateMode = bw.ReadIndex(2, 0);
                this.Path = bw.ReadIndex(3, string.Empty); 
                byte[] buffer = bw.ReadIndex(4, Feng.Utils.Constants.EmptyData);
 
                edit.Open(buffer);
                int focusedrowindex = Feng.Utils.ConvertHelper.ToInt32(edit.readDics.Get("focusedcellrowindex"), -1);
                int focusedcolumnindex = Feng.Utils.ConvertHelper.ToInt32(edit.readDics.Get("focusedcellcolumnindex"), -1);
                if (focusedrowindex > 0 && focusedcolumnindex > 0)
                {
                    ICell cell = edit.GetCell(focusedrowindex, focusedcolumnindex);
                    edit.FocusedCell = cell;
                }
                edit.ReFreshFirstDisplayColumnIndex();
                edit.ReFreshFirstDisplayRowIndex();
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
                    bw.Write(1, this.AddressID);
                    bw.Write(2, this.FileVersionUpdateMode);
                    bw.Write(3, this.Path);
                    bw.Write(4, this.edit.GetFileData());
                    data.Data = bw.GetData();
                }
                return data;
            }
        }

        public override bool DrawCell(IBaseCell cell, Feng.Drawing.GraphicsObject g)
        {
            GraphicsState gs = g.Graphics.Save();
            g.Graphics.SetClip(cell.Rect);
            g.ClipRectangle = cell.Rect;
            this.edit.OnDraw(this, g);
            if (this.edit.InDesign)
            {
                g.Graphics.TranslateTransform(this.Left, this.Top);
                operatorView.OnDraw(this, g);
            }
            g.Graphics.Restore(gs);
            //g.Graphics.DrawString(Rect.ToString(), cell.Font, Brushes.Red, Rect);
            return true;
        }

        public override int Top
        {
            get
            {
                if (this.Cell == null)
                    return 0;
                return this.Cell.Top;
            }
            set { }
        }
        public override int Left
        {
            get
            {
                if (this.Cell == null)
                    return 0;
                return this.Cell.Left;
            }
            set { }
        }
        public override int Width
        {
            get
            {
                if (this.Cell == null)
                    return 0;
                return this.Cell.Width;
            }
            set { }
        }
        public override int Height
        {
            get
            {
                if (this.Cell == null)
                    return 0;
                return this.Cell.Height;
            }
            set { }
        }
        public override ICell Cell
        {
            get
            {
                return base.Cell;
            }
            set
            {
                base.Cell = value;
                if (value == null)
                {
                    return;
                }
                if (value.OwnMergeCell == null)
                {
                    edit.Cell = value;
                }
                else
                {
                    edit.Cell = value.OwnMergeCell;
                }
                edit.ReFreshFirstDisplayColumnIndex();
                edit.ReFreshFirstDisplayRowIndex();
            }
        }

        public override bool PrintCell(IBaseCell cell, PrintArgs e, Rectangle rect)
        {
            this.edit.Print(e);
            return true;
        }

        public override bool PrintValue(IBaseCell cell, PrintArgs e, Rectangle rect, object value)
        {
            this.edit.Print(e);
            return true;
        }

        public override ICellEditControl Clone(DataExcel grid)
        {
            CellExcel celledit = new CellExcel(grid);
            celledit.Read(grid, 1, this.Data);
            return celledit;
        }


        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return edit.OnMouseDown(sender, e, ve);
        }

        public override bool OnMouseUp(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return edit.OnMouseUp(sender, e, ve);
        }

        public override bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return edit.OnMouseMove(sender, e, ve);
        }

        public override bool OnMouseLeave(object sender, EventArgs e, EventViewArgs ve)
        {
            return edit.OnMouseLeave(sender, e, ve);
        }

        public override bool OnMouseHover(object sender, EventArgs e, EventViewArgs ve)
        {
            return edit.OnMouseHover(sender, e, ve);
        }

        public override bool OnMouseEnter(object sender, EventArgs e, EventViewArgs ve)
        {
            return edit.OnMouseEnter(sender, e, ve);
        }

        public override bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {

            return edit.OnMouseDoubleClick(sender, e, ve);
        }

        public override bool OnMouseClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            Point pt = ve.ControlPoint; 
            pt.Offset(this.Left * -1, this.Top * -1);
            if (this.operatorView.Rect.Contains (pt))
            {
                if (!this.ReadOnly)
                {
                    using (OpenFileDialog dlg = new OpenFileDialog())
                    {
                        dlg.Filter = Feng.App.FileExtension_DataExcel.SelDataExcelFile;
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            edit.Open(dlg.FileName);
                        }
                    }
                }
                return true;
            }
            return edit.OnMouseClick(sender, e, ve);
        }

        public override bool OnMouseCaptureChanged(object sender, EventArgs e, EventViewArgs ve)
        {
            return edit.OnMouseCaptureChanged(sender, e, ve);
        }

        public override bool OnMouseWheel(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return edit.OnMouseWheel(sender, e, ve);
        }

        public override bool OnClick(object sender, EventArgs e, EventViewArgs ve)
        {
            return edit.OnClick(sender, e, ve);
        }

        public override bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            return edit.OnKeyDown(sender, e, ve);
        }

        public override bool OnKeyPress(object sender, KeyPressEventArgs e, EventViewArgs ve)
        {
            return edit.OnKeyPress(sender, e, ve);
        }

        public override bool OnKeyUp(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            return edit.OnKeyUp(sender, e, ve);
        }

        public override bool OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e, EventViewArgs ve)
        {
            return edit.OnPreviewKeyDown(sender, e, ve);
        }

        public override bool OnDoubleClick(object sender, EventArgs e, EventViewArgs ve)
        {
            return edit.OnDoubleClick(sender, e, ve);
        }

        public override bool OnPreProcessMessage(object sender, ref Message msg, EventViewArgs ve)
        {
            return edit.OnPreProcessMessage(sender, ref msg, ve);
        }

        public override bool OnProcessCmdKey(object sender, ref Message msg, Keys keyData, EventViewArgs ve)
        {
            return edit.OnProcessCmdKey(sender, ref msg, keyData, ve);
        }

        public override bool OnProcessDialogChar(object sender, char e, EventViewArgs ve)
        {
            return edit.OnProcessDialogChar(sender, e, ve);
        }

        public override bool OnProcessDialogKey(object sender, Keys e, EventViewArgs ve)
        {
            return edit.OnProcessDialogKey(sender, e, ve);
        }

        public override bool OnProcessKeyEventArgs(object sender, ref Message m, EventViewArgs ve)
        {
            return edit.OnProcessKeyEventArgs(sender, ref m, ve);
        }

        public override bool OnProcessKeyMessage(object sender, ref Message m, EventViewArgs ve)
        {
            return edit.OnProcessKeyMessage(sender, ref m, ve);
        }

        public override bool OnProcessKeyPreview(object sender, ref Message m, EventViewArgs ve)
        {
            return edit.OnProcessKeyPreview(sender, ref m, ve);
        }

        public override bool OnWndProc(object sender, ref Message m, EventViewArgs ve)
        {
            return edit.OnWndProc(sender, ref m, ve);
        }

        public override bool OnDragEnter(object sender, DragEventArgs e, EventViewArgs ve)
        {
            return edit.OnDragEnter(sender, e, ve);
        }

        public override bool OnDragDrop(object sender, DragEventArgs e, EventViewArgs ve)
        {
            return edit.OnDragDrop(sender, e, ve);
        }

        public override bool OnDragLeave(object sender, EventArgs e, EventViewArgs ve)
        {
            return edit.OnDragLeave(sender, e, ve);
        }

        public override bool OnHandleCreated(object sender, EventArgs e, EventViewArgs ve)
        {
            return edit.OnHandleCreated(sender, e, ve);
        }

        public override bool OnSizeChanged(object sender, EventArgs e, EventViewArgs ve)
        {
            return edit.OnSizeChanged(sender, e, ve);
        }

        public override bool OnClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return edit.OnClick(sender, e, ve);
        }

        public override bool OnKeyPress(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            return edit.OnKeyPress(sender, e, ve);
        }

        public override bool OnDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            return edit.OnDoubleClick(sender, e, ve);
        }

    }
    public class CellEditDataExcel : DataExcel
    {
        private IRect rect = null;
        private DataExcel viewControl = null;
        public IRect Cell { get { return rect; }set { rect = value; } }
        public CellEditDataExcel(IRect rct, DataExcel viewcontrol) : base()
        {
            rect = rct;
            viewControl = viewcontrol;
            this.Init();
        }
        public override int Top { get { return rect.Top; } set { } }
        public override int Left { get { return rect.Left; } set { } }
        public override int Width { get { return rect.Width; } set { } }
        public override int Height { get { return rect.Height; } set { } }
        public override ViewControl Control { get { return viewControl.Control; } }
        public override Point PointViewToControl(Point pt)
        { 
            return base.PointViewToControl(pt);
        }
        public override Point PointToScreen(Point pt)
        {
            pt.Offset(this.Left, this.Top);
            return base.PointToScreen(pt);
        }
        public override Point PointToClient(Point pt)
        {
            pt.Offset(this.Left*-1, this.Top * -1);
            return base.PointToClient(pt);
        }
#if DEBUG
        public override bool OnDraw(object sender, GraphicsObject currentGraphicsObject)
        {
            //if (Feng.Script.FunctionContainer.DebugFunctionContainer.DebugSate)
            //{

            //}
            return base.OnDraw(sender, currentGraphicsObject);
        }
#endif
    }

    public class OperatorView : DivView
    {
        public BaseView parentView = null;
        private ImageView ImageView = null;
        public OperatorView(BaseView baseView)
        {
            ImageView = new ImageView();
            ImageView.Width = 16;
            ImageView.Height = 16;
            ImageView.Left = 0;
            ImageView.Top = 0;
            ImageView.Image = Feng.Forms.ImageCache.MulImage;
            ImageView.SizeMode = ImageLayout.Stretch;

            parentView = baseView;
            this.AddView(ImageView);
        }
    }

    public class ImageView:DivView
    {
        public Image Image { get; set; }
        public ImageLayout SizeMode { get; set; }
        public override bool OnDraw(object sender, GraphicsObject g)
        {
            Feng.Drawing.GraphicsHelper.DrawImage(g.Graphics, this.Image, this.Rect, SizeMode);
            return base.OnDraw(sender ,g);
        }
    }

}
