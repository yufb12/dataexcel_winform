using Feng.Forms.Controls;
using Feng.Forms.Events;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms.Popup
{
    public partial class ToolBarItemDropDownForm : PopupForm
    {
        public ToolBarItemDropDownForm(Feng.Forms.Controls.ToolBar toolbar)
            : base()
        {
            
            base.SetStyle(ControlStyles.DoubleBuffer
                | ControlStyles.ResizeRedraw
                | ControlStyles.SupportsTransparentBackColor
                | ControlStyles.AllPaintingInWmPaint, true);

            base.SetStyle(ControlStyles.ContainerControl, true);
            base.SetStyle(ControlStyles.StandardClick, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            base.UpdateStyles();
            _toolbar = toolbar;
            //InitializeComponent();
        }
        private Feng.Forms.Controls.ToolBar _toolbar = null;
        public Feng.Forms.Controls.ToolBar ToolBar
        {
            get
            {
                return _toolbar;
            } 
        }
        private List<ToolBarItem> listhidetemp = null;
        public void Init(List<ToolBarItem> listhide)
        {
            listhidetemp = listhide;
            InitItem();
        }
        public void Init(ToolBarItemCollection list)
        {
            listhidetemp = new List<ToolBarItem>();
            listhidetemp.AddRange(list.ToArray());
            InitItem();
        }
        int itemheight = 0;
        int itemwidth = 0;
        public void InitItem()
        { 
            int top = 0;
            List = new List<ToolBarItem>();
            itemheight = 0;
            itemwidth = 0;
            foreach (ToolBarItem item in listhidetemp)
            {
                ToolBarItem baritem = item;
                if (baritem is ToolBarItemVSplit)
                {
                    baritem = new ToolBarItemHSplit();
                    baritem.ToolBar = item.ToolBar;
                } 
                baritem.Top = top;
                baritem.Left = 0;
                top = top + baritem.Height; 
                List.Add(baritem);
                itemheight = itemheight + baritem.Height;
                if (baritem.Width > itemwidth)
                {
                    itemwidth = baritem.Width;
                }
            }
            if (itemwidth < 136)
            {
                itemwidth = 136;
            }
            this.ClientSize = new System.Drawing.Size(itemwidth, itemheight); 
            foreach (ToolBarItem item in List)
            {
                item.Width = this.Width;
            }
            BarItemHeader.Width = this.Width;
            BarItemFooter.Width = this.Width;

        }
        public ToolBarItem GetItem(Point pt)
        {
            foreach (ToolBarItem item in this.ShowTimes)
            {
                if (item.Rect.Contains(pt))
                {
                    return item;
                }
            }
            return null;
        }
        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.Size = new System.Drawing.Size(itemwidth, itemheight);
                base.OnLoad(e);
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {

            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    ToolBarItem item = GetItem(e.Location);
                    if (item == BarItemHeader)
                    {
                        this.firsttoobaritemindex =  this.firsttoobaritemindex - 1;
                        this.firsttoobaritemindex = Math.Max(0, this.firsttoobaritemindex);
                        this.firsttoobaritemindex = Math.Min(this.firsttoobaritemindex, ShowTimes.Count);
                        if (workheight > this.Height)
                        {
                            refreshitme1();
                        }
                        else
                        {
                            refreshitme2();
                        }
                        this.Invalidate();
                        return;
                    }
                    if (item == BarItemFooter)
                    {
                        this.firsttoobaritemindex = this.firsttoobaritemindex + 1;
                        this.firsttoobaritemindex = Math.Max(0, this.firsttoobaritemindex);
                        this.firsttoobaritemindex = Math.Min(this.firsttoobaritemindex, ShowTimes.Count);
                        if (workheight > this.Height)
                        {
                            refreshitme1();
                        }
                        else
                        {
                            refreshitme2();
                        }
                        this.Invalidate();
                        return;
                    }
                    this.ToolBar.OnItemClick(item);
                    this.ClosePopup();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
  
            base.OnMouseDown(e);
        }
 
        private List<ToolBarItem> List = null;
        Rectangle rect = Rectangle.Empty;
        protected override void OnPaint(PaintEventArgs e)
        {

            try
            {

                Feng.Drawing.GraphicsObject graphicsobject = Feng.Drawing.GraphicsObjectCache.Get(this.GetType().FullName);
                graphicsobject.Control = this;
                graphicsobject.ModifierKeys = System.Windows.Forms.Control.ModifierKeys;
                graphicsobject.MouseButtons = System.Windows.Forms.Control.MouseButtons;
                graphicsobject.MousePoint = System.Windows.Forms.Control.MousePosition;
                graphicsobject.WorkArea = new Rectangle(0, 0, this.Width, this.Height);
                graphicsobject.ClipRectangle = e.ClipRectangle;
                graphicsobject.ClientPoint = this.PointToClient(System.Windows.Forms.Control.MousePosition);
                graphicsobject.Graphics = e.Graphics;
                OnDraw(graphicsobject);

                graphicsobject.Dispose();


            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
            }
  
        }
        public void OnDraw(Feng.Drawing.GraphicsObject g)
        { 
            rect = new Rectangle(0, 0, this.Width, this.Height);
            Feng.Forms.Skins.ToolBarItemSkin.Default.DrawBarBack(g.Graphics, rect);
            if (List == null)
                return;
            if (BarItemHeader.Visable)
            {
                BarItemHeader.OnDraw(g);
            }
            foreach (ToolBarItem item in ShowTimes)
            {
                item.OnDraw(g);
            }
            if (BarItemFooter.Visable)
            {
                BarItemFooter.OnDraw(g);
            }
        }
        private ToolBarItemUp _baritemheader = null;
        public virtual ToolBarItemUp BarItemHeader
        {
            get
            {
                if (_baritemheader == null)
                {
                    _baritemheader = new ToolBarItemUp();
                    _baritemheader.ToolBar = this.ToolBar;
                }
                return _baritemheader;
            }
        }

        private ToolBarItemDown _baritemfooter = null;
        public virtual ToolBarItemDown BarItemFooter
        {
            get
            {

                if (_baritemfooter == null)
                {
                    _baritemfooter = new ToolBarItemDown();
                    _baritemfooter.ToolBar = this.ToolBar;
                }
                return _baritemfooter;
            }
        }
        public override void KeyChanged(object sender, DropDownBoxTextChangedEventArgs e)
        {
            base.KeyChanged(sender, e);
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                this.Invalidate();
                ToolBarItem item = GetItem(e.Location);
                if (item != null)
                {
                    if (item.Items.Count > 0)
                    {
                        ShowLeftItems(item);
                    }
                }
                base.OnMouseMove(e);
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmMain2", "statusSum_Click", ex);
            }
        }
        public override void SelectFirst()
        {
            base.SelectFirst();
        }
        public override void SetFocus()
        {
            base.SetFocus();
        }
        public override void MoveToFirst()
        {
            base.MoveToFirst();
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {
            try
            {
                int numberOfTextLinesToMove = -1 * e.Delta / 120;
                this.firsttoobaritemindex = (this.firsttoobaritemindex + numberOfTextLinesToMove);
                this.firsttoobaritemindex = Math.Max(0, this.firsttoobaritemindex);
                this.firsttoobaritemindex = Math.Min( this.firsttoobaritemindex,ShowTimes.Count);
                if (workheight > this.Height)
                {
                    refreshitme1();
                }
                else
                {
                    refreshitme2();
                }
                this.Invalidate();
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("", "", "OnMouseWheel", ex);
            }
            base.OnMouseWheel(e);
        }
        ToolBarItemDropDownForm leftform = null;
        public void ShowLeftItems(ToolBarItem item)
        {
            if (leftform == null)
            {
                leftform = new ToolBarItemDropDownForm(ToolBar);
                leftform.VisibleChanged += Leftform_VisibleChanged;
            }
            if (leftform.IsDisposed)
            {
                leftform = new ToolBarItemDropDownForm(ToolBar);
                leftform.VisibleChanged += Leftform_VisibleChanged;
            }
            leftform.Init(item.Items);
            leftform.ParentEditForm = this.FindForm();
            Point pt = this.PointToScreen(new Point(item.Rect.Right, item.Rect.Top));
            leftform.Popup(pt); 
        }

        private void Leftform_VisibleChanged(object sender, EventArgs e)
        {
            if (!leftform.Visible)
            { 
            }
        }
        private int firsttoobaritemindex = 0;
        int workheight = 0;
        public override void Popup(Point pt)
        {
            this.firsttoobaritemindex=0;

            Rectangle rect = Screen.PrimaryScreen.WorkingArea;

            if (this.Height + pt.Y > rect.Height)
            {
                int top = rect.Height - this.Height-20;
                if (top < 0)
                {
                    top = 0;
                }
                pt.Y = top;
            }
            base.Popup(pt);
            workheight = rect.Height- pt.Y;
            this.Height = Math.Min(workheight, this.Height);
            if (workheight > this.Height)
            {
                refreshitme1();
            }
            else
            {
                refreshitme2();
            }
        }
        private List<ToolBarItem> ShowTimes = new List<ToolBarItem> ();

        public void refreshitme1()
        {
            ShowTimes.Clear();
            int top = 0; 
            itemheight = 0;
            itemwidth = 0;
            BarItemHeader.Visable = false;
            BarItemFooter.Visable = false;
            for (int i = 0; i < List.Count; i++)
            {
                ToolBarItem item = List[i];
                item.Top = top;
                ShowTimes.Add(item);
                top = top + item.Height;
            }
        }
        public void refreshitme2()
        {
            ShowTimes.Clear();
            int top = 0;
            itemheight = 0;
            itemwidth = 0;
            BarItemHeader.Visable = false;
            BarItemFooter.Visable = false;
            if (firsttoobaritemindex > 0)
            {
                BarItemHeader.Visable = true;
                BarItemHeader.Top = top;
                ShowTimes.Add(BarItemHeader);
                top = top + BarItemHeader.Height;
            }
            for (int i = firsttoobaritemindex; i < List.Count; i++)
            {
                ToolBarItem item = List[i];
                if (top + item.Height + BarItemFooter.Height > this.Height)
                {
                    BarItemFooter.Top = top;
                    ShowTimes.Add(BarItemFooter);
                    top = top + BarItemFooter.Height;
                    return;
                }
                item.Top = top;
                ShowTimes.Add(item);
                top = top + item.Height;

            }
        }
    }
}
