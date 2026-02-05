using Feng.Data;
using Feng.Drawing;
using Feng.Forms.Interface;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Forms.Views
{
    public class DivView : BaseView
    {
        public DivView()
        {
            Viewes = new ViewCollection();
            DecorateViewes = new ViewCollection();
        }

        public override void BindingControl(ViewControl ctl)
        {
            control = ctl;
            ctl.AddView(this);
        }
        private ViewControl control = null;
        public override ViewControl Control
        {
            get
            {
                if (this.control != null)
                    return control;
                if (this.ParentView == null)
                    return null;
                return this.ParentView.Control;
            }
        }


        /// <summary>
        /// 1、本级大小改变时，子级排列方式
        /// </summary>
        public virtual DivLaoutChildMode SL_LaoutChildMode { get; set; }
 
        /// <summary>
        /// 2、尺寸计算时宽度与高度计算顺序，默认TRUE水平模式，先算宽度再计算高度
        /// </summary>
        public virtual bool SL_SizePriorityHeight { get; set; }

        /// <summary>
        /// 3、父级大小改变时，本级宽度计算方式
        /// </summary>
        public virtual DivSizeWidthMode SL_SizeWidthMode { get; set; }
        /// <summary>
        /// 3、父级大小改变时，本级高度计算方式
        /// </summary>
        public virtual DivSizeHeightMode SL_SizeHeightMode { get; set; }

        /// <summary>
        /// 4、父级大小改变时，垂直排列方式
        /// </summary>
        public virtual DivLaoutVerticalMode SL_LaoutVerticalMode { get; set; }

        /// <summary>
        /// 4、父级大小改变时，水平排列方式
        /// </summary>
        public virtual DivLaoutHorizontalMode SL_LaoutHorizontalMode { get; set; }

        public virtual int SL_SizeMarginWidth { get; set; }
        public virtual int SL_SizeMarginHeight { get; set; }
        public virtual int SL_SizePadddingWidth { get; set; }
        public virtual int SL_SizePadddingHeight { get; set; }


        public virtual int SL_LayoutPadddingLeft { get; set; }
        public virtual int SL_LayoutPadddingRight { get; set; }
        public virtual int SL_LayoutPadddingTop { get; set; }
        public virtual int SL_LayoutPadddingBottom { get; set; }
        public virtual DivView PrevView { get; set; }


        public override void ReadDataStruct(DataStruct data)
        {
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data.Data))
            {
                DataStruct ds = reader.ReadIndex(1, (DataStruct)null);
                base.ReadDataStruct(ds);
                this.SL_LaoutChildMode = (DivLaoutChildMode)reader.ReadIndex(63, (int)SL_LaoutChildMode);
                this.SL_SizePriorityHeight = reader.ReadIndex(64, SL_SizePriorityHeight);
                this.SL_SizeWidthMode = (DivSizeWidthMode)reader.ReadIndex(65, (int)SL_SizeWidthMode);
                this.SL_SizeHeightMode = (DivSizeHeightMode)reader.ReadIndex(66, (int)SL_SizeHeightMode);
                this.SL_LaoutVerticalMode = (DivLaoutVerticalMode)reader.ReadIndex(67, (int)SL_LaoutVerticalMode);
                this.SL_LaoutHorizontalMode = (DivLaoutHorizontalMode)reader.ReadIndex(68, (int)SL_LaoutHorizontalMode);
                this.SL_SizeMarginWidth = reader.ReadIndex(69, SL_SizeMarginWidth);
                this.SL_SizePadddingWidth = reader.ReadIndex(70, SL_SizePadddingWidth);
                this.SL_SizeMarginHeight = reader.ReadIndex(71, SL_SizeMarginHeight);
                this.SL_SizePadddingHeight = reader.ReadIndex(72, SL_SizePadddingHeight);
                this.SL_LayoutPadddingLeft = reader.ReadIndex(73, SL_LayoutPadddingLeft);
                this.SL_LayoutPadddingRight = reader.ReadIndex(74, SL_LayoutPadddingRight);
                this.SL_LayoutPadddingTop = reader.ReadIndex(75, SL_LayoutPadddingTop);
                this.SL_LayoutPadddingBottom = reader.ReadIndex(76, SL_LayoutPadddingBottom);
            }
        }

        [Browsable(false)]
        public override DataStruct Data
        {
            get
            {
                DataStruct ds = new DataStruct();
                using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
                {
                    DataStruct dsbase = base.Data;
                    bw.Write(1, dsbase);
                    bw.Write(63, (int)SL_LaoutChildMode);
                    bw.Write(64, SL_SizePriorityHeight);
                    bw.Write(65, (int)SL_SizeWidthMode);
                    bw.Write(66, (int)SL_SizeHeightMode);
                    bw.Write(67, (int)SL_LaoutVerticalMode);
                    bw.Write(68, (int)SL_LaoutHorizontalMode);
                    bw.Write(69, SL_SizeMarginWidth);
                    bw.Write(70, SL_SizePadddingWidth);
                    bw.Write(71, SL_SizeMarginHeight);
                    bw.Write(72, SL_SizePadddingHeight);
                    bw.Write(73, SL_LayoutPadddingLeft);
                    bw.Write(74, SL_LayoutPadddingRight);
                    bw.Write(75, SL_LayoutPadddingTop);
                    bw.Write(76, SL_LayoutPadddingBottom);
                    ds.Data = bw.GetData();
                }
                return ds;
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ViewCollection Viewes { get; set; }

        /// <summary>
        /// 装饰类
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual ViewCollection DecorateViewes { get; set; }

        public virtual void AddDecorateView(DivView view)
        {
            if (!this.DecorateViewes.Contains(view))
            {
                this.DecorateViewes.Add(view);
            }
        }

        public virtual void AddView(DivView view)
        {
            if (!this.Viewes.Contains(view))
            {
                this.Viewes.Add(view);
            }
            view.ParentView = this;
            view.RootView = this.RootView;
        }
        public virtual void RemoveView(DivView view)
        {
            this.Viewes.Remove(view);
        }
        public override bool OnWndProc(object sender, ref Message m, EventViewArgs ve)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnWndProc(sender, ref m, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnWndProc(sender, ref m, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "WndProc", ex);
            }

            return base.OnWndProc(sender, ref m, ve);
        }
        public override bool OnMouseCaptureChanged(object sender, EventArgs e, EventViewArgs ve)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnMouseCaptureChanged(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnMouseCaptureChanged(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseCaptureChanged", ex);
            }
            return base.OnMouseCaptureChanged(sender, e, ve);
        }
        public override bool OnMouseClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                int x = ve.X;
                int y = ve.Y;
                foreach (DivView item in this.DecorateViewes)
                {
                    ve.X = (x - item.Left) * (100 + item.Zoom) / 100;
                    ve.Y = (y - item.Top) * (100 + item.Zoom) / 100;
                    bool res = item.OnMouseClick(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in this.Viewes)
                {
                    ve.X = (x - item.Left) * (100 + item.Zoom) / 100;
                    ve.Y = (y - item.Top) * (100 + item.Zoom) / 100;
                    bool res = item.OnMouseClick(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseClick", ex);
            }
            return base.OnMouseClick(sender, e, ve);
        }
        public override bool OnMouseDoubleClick(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                int x = ve.X;
                int y = ve.Y;
                foreach (DivView item in this.DecorateViewes)
                {
                    ve.X = (x - item.Left) * (100 + item.Zoom) / 100;
                    ve.Y = (y - item.Top) * (100 + item.Zoom) / 100;
                    bool res = item.OnMouseDoubleClick(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in this.Viewes)
                {
                    ve.X = (x - item.Left) * (100 + item.Zoom) / 100;
                    ve.Y = (y - item.Top) * (100 + item.Zoom) / 100;
                    bool res = item.OnMouseDoubleClick(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseDoubleClick", ex);
            }
            return base.OnMouseDoubleClick(sender, e, ve);
        }
        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                int x = ve.X;
                int y = ve.Y;
                foreach (DivView item in this.DecorateViewes)
                {
                    ve.X = (x - item.Left) * (100 + item.Zoom) / 100;
                    ve.Y = (y - item.Top) * (100 + item.Zoom) / 100;
                    bool res = item.OnMouseDown(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in this.Viewes)
                {
                    ve.X = (x - item.Left) * (100 + item.Zoom) / 100;
                    ve.Y = (y - item.Top) * (100 + item.Zoom) / 100;
                    bool res = item.OnMouseDown(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseDown", ex);
            }
            return base.OnMouseDown(sender, e, ve);
        }
        public override bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                int x = ve.X;
                int y = ve.Y;
                foreach (DivView item in DecorateViewes)
                {
                    ve.X = (x - item.Left) * (100 + item.Zoom) / 100;
                    ve.Y = (y - item.Top) * (100 + item.Zoom) / 100;
                    bool res = item.OnMouseMove(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    ve.X = (x - item.Left) * (100 + item.Zoom) / 100;
                    ve.Y = (y - item.Top) * (100 + item.Zoom) / 100;
                    bool res = item.OnMouseMove(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }

            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseMove", ex);
            }
            return base.OnMouseMove(sender, e, ve);
        }
        public override bool OnMouseUp(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                int x = ve.X;
                int y = ve.Y;
                foreach (DivView item in DecorateViewes)
                {
                    ve.X = (x - item.Left) * (100 + item.Zoom) / 100;
                    ve.Y = (y - item.Top) * (100 + item.Zoom) / 100;
                    bool res = item.OnMouseUp(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    ve.X = (x - item.Left) * (100 + item.Zoom) / 100;
                    ve.Y = (y - item.Top) * (100 + item.Zoom) / 100;
                    bool res = item.OnMouseUp(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseUp", ex);
            }
            return base.OnMouseUp(sender, e, ve);
        }
        public override bool OnMouseEnter(object sender, EventArgs e, EventViewArgs ve)
        {
            try
            {
                int x = ve.X;
                int y = ve.Y;
                foreach (DivView item in DecorateViewes)
                {
                    ve.X = (x - item.Left) * (100 + item.Zoom) / 100;
                    ve.Y = (y - item.Top) * (100 + item.Zoom) / 100;
                    bool res = item.OnMouseEnter(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    ve.X = (x - item.Left) * (100 + item.Zoom) / 100;
                    ve.Y = (y - item.Top) * (100 + item.Zoom) / 100;
                    bool res = item.OnMouseEnter(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseEnter", ex);
            }
            return base.OnMouseEnter(sender, e, ve);
        }
        public override bool OnMouseHover(object sender, EventArgs e, EventViewArgs ve)
        {
            try
            {
                int x = ve.X;
                int y = ve.Y;
                foreach (DivView item in DecorateViewes)
                {
                    ve.X = (x - item.Left) * (100 + item.Zoom) / 100;
                    ve.Y = (y - item.Top) * (100 + item.Zoom) / 100;
                    bool res = item.OnMouseHover(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    ve.X = (x - item.Left) * (100 + item.Zoom) / 100;
                    ve.Y = (y - item.Top) * (100 + item.Zoom) / 100;
                    bool res = item.OnMouseHover(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseHover", ex);
            }
            return base.OnMouseHover(sender, e, ve);
        }
        public override bool OnMouseLeave(object sender, EventArgs e, EventViewArgs ve)
        {
            try
            {
                int x = ve.X;
                int y = ve.Y;
                foreach (DivView item in DecorateViewes)
                {
                    ve.X = (x - item.Left) * (100 + item.Zoom) / 100;
                    ve.Y = (y - item.Top) * (100 + item.Zoom) / 100;
                    bool res = item.OnMouseLeave(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    ve.X = (x - item.Left) * (100 + item.Zoom) / 100;
                    ve.Y = (y - item.Top) * (100 + item.Zoom) / 100;
                    bool res = item.OnMouseLeave(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseLeave", ex);
            }
            return base.OnMouseLeave(sender, e, ve);
        }
        public override bool OnMouseWheel(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnMouseWheel(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnMouseWheel(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnMouseWheel(sender, e, ve);
        }
        public override bool OnKeyDown(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnKeyDown(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnKeyDown(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnKeyDown(sender, e, ve);
        }
        public override bool OnKeyPress(object sender, KeyPressEventArgs e, EventViewArgs ve)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnKeyPress(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnKeyPress(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnKeyPress(sender, e, ve);
        }
        public override bool OnKeyUp(object sender, KeyEventArgs e, EventViewArgs ve)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnKeyUp(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnKeyUp(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnKeyUp(sender, e, ve);
        }
        public override bool OnClick(object sender, EventArgs e, EventViewArgs ve)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnClick(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnClick(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnClick(sender, e, ve);
        }
        public override bool OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e, EventViewArgs ve)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnPreviewKeyDown(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {

                    bool res = item.OnPreviewKeyDown(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnPreviewKeyDown(sender, e, ve);
        }
        public override bool OnDoubleClick(object sender, EventArgs e, EventViewArgs ve)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnDoubleClick(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {

                    bool res = item.OnDoubleClick(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnDoubleClick(sender, e, ve);
        }
        public override bool OnPreProcessMessage(object sender, ref Message msg, EventViewArgs ve)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnPreProcessMessage(sender, ref msg, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {

                    bool res = item.OnPreProcessMessage(sender, ref msg, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnPreProcessMessage(sender, ref msg, ve);
        }
        public override bool OnProcessCmdKey(object sender, ref Message msg, Keys e, EventViewArgs ve)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnProcessCmdKey(sender, ref msg, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {

                    bool res = item.OnProcessCmdKey(sender, ref msg, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnProcessCmdKey(sender, ref msg, e, ve);
        }
        public override bool OnProcessDialogChar(object sender, char e, EventViewArgs ve)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnProcessDialogChar(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {

                    bool res = item.OnProcessDialogChar(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnProcessDialogChar(sender, e, ve);
        }
        public override bool OnProcessDialogKey(object sender, Keys e, EventViewArgs ve)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnProcessDialogKey(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {

                    bool res = item.OnProcessDialogKey(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnProcessDialogKey(sender, e, ve);
        }
        public override bool OnProcessKeyEventArgs(object sender, ref Message msg, EventViewArgs ve)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnProcessKeyEventArgs(sender, ref msg, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {

                    bool res = item.OnProcessKeyEventArgs(sender, ref msg, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnProcessKeyEventArgs(sender, ref msg, ve);
        }
        public override bool OnProcessKeyMessage(object sender, ref Message msg, EventViewArgs ve)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnProcessKeyMessage(sender, ref msg, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {

                    bool res = item.OnProcessKeyMessage(sender, ref msg, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnProcessKeyMessage(sender, ref msg, ve);
        }
        public override bool OnProcessKeyPreview(object sender, ref Message msg, EventViewArgs ve)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnProcessKeyPreview(sender, ref msg, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {

                    bool res = item.OnProcessKeyPreview(sender, ref msg, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnProcessKeyPreview(sender, ref msg, ve);
        }
        public override bool OnDragEnter(object sender, DragEventArgs e, EventViewArgs ve)
        {
            try
            {

                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnDragEnter(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnDragEnter(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnDragEnter(sender, e, ve);
        }
        public override bool OnDragDrop(object sender, DragEventArgs e, EventViewArgs ve)
        {
            try
            {

                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnSizeChanged(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnSizeChanged(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnDragDrop(sender, e, ve);
        }
        public override bool OnDragLeave(object sender, EventArgs e, EventViewArgs ve)
        {
            try
            {

                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnSizeChanged(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnSizeChanged(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnDragLeave(sender, e, ve);
        }
        public override bool OnHandleCreated(object sender, EventArgs e, EventViewArgs ve)
        {
            try
            {

                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnSizeChanged(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnSizeChanged(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnHandleCreated(sender, e, ve);
        }

        public override bool OnSizeChanged(object sender, EventArgs e, EventViewArgs ve)
        {
            ReSize(sender, e, null, this, ve);
            ReLayout(sender, e, null, this, ve);
            return false;
        }
        public virtual void ReSetSize()
        {
            EventViewArgs ve = EventViewArgs.GetEventViewArgs(this.Control);
            EventArgs e = new EventArgs();
            ReSize(this, e, null, this, ve);
            ReLayout(this, e, null, this, ve);
        }
        public override bool OnDraw(object sender, Feng.Drawing.GraphicsObject g)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnDraw(sender, g);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnDraw(sender, g);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnDraw(sender, g);
        }
        public override bool OnDrawBack(object sender, Feng.Drawing.GraphicsObject g)
        {
            try
            {
                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnDrawBack(sender, g);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnDrawBack(sender, g);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnDraw(sender, g);
        }
        public override bool OnRefresh(object sender, EventArgs e, EventViewArgs ve)
        {
            try
            {

                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnRefresh(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnRefresh(sender, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnRefresh(sender, e, ve);
        }
        public override bool OnSetTextView(object sender, bool isfont, Font font, bool isforecolor, Color color, bool isbackcolor, Color backcolor, EventArgs e, EventViewArgs ve)
        {
            try
            {

                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnSetTextView(sender, isfont, font, isforecolor, color, isbackcolor, backcolor, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {

                    bool res = item.OnSetTextView(sender, isfont, font, isforecolor, color, isbackcolor, backcolor, e, ve);
                    if (res)
                    {
                        return true;
                    }

                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
            return base.OnSetTextView(sender, isfont, font, isforecolor, color, isbackcolor, backcolor, e, ve);
        }
        public override bool OnSetObj(object sender, object obj, EventArgs e, EventViewArgs ve)
        {
            try
            {

                foreach (DivView item in DecorateViewes)
                {
                    bool res = item.OnSetObj(sender, obj, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnSetObj(sender, obj, e, ve);
                    if (res)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnSetObj", ex);
            }
            return base.OnSetObj(sender, obj, e, ve);
        }
        public virtual void Sort()
        {
            this.Viewes.Sort();
        }
        public override bool SendMessage(BaseView view, object sender, ViewMessage message)
        {
            for (int i = this.Viewes.Count - 1; i >= 0; i--)
            {
                try
                {
                    BaseView viewc = this.Viewes[i];
                    bool res = viewc.SendMessage(viewc, sender, message);
                    if (res)
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Feng.Utils.BugReport.Log(ex);
                }
            }
            return false;
        }

        public virtual bool ReLayout(object sender, EventArgs e, BaseView pView, BaseView parentView, EventViewArgs ve)
        {
            DivView prevView = null;
            foreach (BaseView item in this.Viewes)
            {
                DivView view = item as DivView;
                if (view != null)
                {
                    if (!view.Visable)
                        continue;
                    LayoutCurrentChildView(view, prevView, this);
                    view.ReLayout(sender, e, prevView, this, ve);
                    prevView = view;
                }
            }
            return false;
        }

        public virtual void LayoutCurrentChildView(DivView view, DivView prevView, DivView parentView)
        {
            if (this.SL_LaoutChildMode == DivLaoutChildMode.VerticalTopBoBottom)
            {
                this.LayoutChildViewVertical(view, prevView, parentView);
            }
            else
            {
                this.LayoutChildViewHorizontal(view, prevView, parentView);
            }
        }

        public virtual void LayoutChildViewHorizontal(DivView view, DivView prevView, DivView parentView)
        {
            int top = 0;
            switch (view.SL_LaoutVerticalMode)
            {
                case DivLaoutVerticalMode.Fix:
                    top = view.Top;
                    break;
                case DivLaoutVerticalMode.PadddingTop:
                    top = top + view.SL_LayoutPadddingTop;
                    break;
                case DivLaoutVerticalMode.PadddingBottom:
                    top = this.Height - view.SL_LayoutPadddingBottom - view.Height;
                    break;
                case DivLaoutVerticalMode.Center:
                    top = top + view.SL_LayoutPadddingTop + (this.Height - view.Height) / 2 - view.SL_LayoutPadddingBottom;
                    break;
                case DivLaoutVerticalMode.PadddingPrev:
                    if (prevView != null)
                    {
                        top = top + prevView.Bottom + view.SL_LayoutPadddingTop;
                    }
                    else
                    {
                        top = top + view.SL_LayoutPadddingTop;
                    }
                    break;
                default:
                    break;
            }
            view.Top = top;

            int left = 0;
            switch (view.SL_LaoutHorizontalMode)
            {
                case DivLaoutHorizontalMode.Fix:
                    left = view.Left;
                    break;
                case DivLaoutHorizontalMode.PadddingLeft:
                    left = left + view.SL_LayoutPadddingLeft;
                    break;
                case DivLaoutHorizontalMode.PadddingRight:
                    left = this.Width - view.SL_LayoutPadddingRight - view.Width;
                    break;
                case DivLaoutHorizontalMode.Center:
                    left = left + view.SL_LayoutPadddingLeft + (this.Width - view.Width) / 2;
                    break;
                case DivLaoutHorizontalMode.PadddingPrev:
                    if (prevView != null)
                    {
                        left = left + prevView.Right + view.SL_LayoutPadddingLeft;
                    }
                    else
                    {
                        left = left + view.SL_LayoutPadddingLeft;
                    }
                    break;
                default:
                    break;
            }
            view.Left = left;
        }

        public virtual void LayoutChildViewVertical(DivView view, DivView prevView, DivView parentView)
        {
            int top = 0;
            switch (view.SL_LaoutVerticalMode)
            {
                case DivLaoutVerticalMode.Fix:
                    top = view.Top;
                    break;
                case DivLaoutVerticalMode.PadddingTop:
                    top = top + view.SL_LayoutPadddingTop;
                    break;
                case DivLaoutVerticalMode.PadddingBottom:
                    top = this.Height - view.SL_LayoutPadddingBottom - view.Height;
                    break;
                case DivLaoutVerticalMode.Center:
                    top = top + view.SL_LayoutPadddingTop + (this.Height - view.Height) / 2 - view.SL_LayoutPadddingBottom;
                    break;
                case DivLaoutVerticalMode.PadddingPrev:
                    if (prevView != null)
                    {
                        top = top + view.SL_LayoutPadddingTop + prevView.Bottom;
                    }
                    break;
                default:
                    break;
            }
            view.Top = top;

            int left = 0;

            switch (view.SL_LaoutHorizontalMode)
            {
                case DivLaoutHorizontalMode.Fix:
                    left = view.Left;
                    break;
                case DivLaoutHorizontalMode.PadddingLeft:
                    left = left + view.SL_LayoutPadddingLeft;
                    break;
                case DivLaoutHorizontalMode.PadddingRight:
                    left = this.Width - view.SL_LayoutPadddingRight - view.Width;
                    break;
                case DivLaoutHorizontalMode.PadddingPrev:
                    if (prevView != null)
                    {
                        left = left + view.SL_LayoutPadddingLeft + prevView.Right;
                    }
                    break;
                default:
                    break;
            }
            view.Left = left;
        }


        public virtual void OnSizeChanging(object sender, EventArgs e, EventViewArgs ve)
        {

        }

        public virtual bool ReSize(object sender, EventArgs e, DivView pView, DivView parentView, EventViewArgs ve)
        { 
            DivView prevView = null;
            if (this.SL_SizePriorityHeight)
            {
                this.SizeViewHeight(sender, e, prevView, parentView, ve);
                this.SizeViewWidth(sender, e, prevView, parentView, ve);
            }
            else
            {
                SizeViewWidth(sender, e, prevView, parentView, ve);
                SizeViewHeight(sender, e, prevView, parentView, ve);
            }
            foreach (DivView item in this.Viewes)
            {
                if (item == null)
                    continue;
                item.ReSize(sender, e, prevView, this, ve);
                prevView = item;
            }
            return false;
        }


        public virtual void SizeViewWidth(object sender, EventArgs e, DivView prevView, DivView parentView, EventViewArgs ve)
        {
            switch (this.SL_SizeWidthMode)
            {
                case DivSizeWidthMode.Fix:
                    break;
                case DivSizeWidthMode.Customize:
                    this.AutoSizeWidth();
                    break;
                case DivSizeWidthMode.Margin:
                    this.Width = parentView.Width + this.SL_SizeMarginWidth - this.SL_SizePadddingWidth;
                    break;
                case DivSizeWidthMode.EqualHeight:
                    this.Width = this.Height + this.SL_SizeMarginWidth;
                    break;
                case DivSizeWidthMode.PrevWidth:
                    if (prevView != null)
                    {
                        this.Width = prevView.Width + this.SL_SizeMarginWidth;
                    }
                    break;

                case DivSizeWidthMode.Fill:
                    if (parentView != null)
                    {
                        int widtho = 0;
                        foreach (DivView item in parentView.Viewes)
                        {
                            if (item == this)
                                continue;
                            widtho = widtho + item.Width;
                        }
                        this.Width = parentView.Width - widtho;
                    }
                    break;
                case DivSizeWidthMode.ChildWidth:
                    int width = 0;
                    DivView pprevView = null;
                    foreach (DivView item in this.Viewes)
                    {
                        item.ReSize(sender, e, pprevView, this, ve);
                        pprevView = item;
                    }
                    foreach (DivView item in this.Viewes)
                    {
                        width = width + item.Width;
                    }
                    this.Width = width + this.SL_SizeMarginWidth;
                    break;
                default:
                    break;
            }

        }
        private bool lockSizeViewHeigth = false;
        public virtual void SizeViewHeight(object sender, EventArgs e, DivView prevView, DivView parentView, EventViewArgs ve)
        {
            if (lockSizeViewHeigth)
                return;
            lockSizeViewHeigth = true;
            try
            {
                switch (this.SL_SizeHeightMode)
                {
                    case DivSizeHeightMode.Fix:
                        break;
                    case DivSizeHeightMode.Customize:
                        this.AutoSizeHeight();
                        break;
                    case DivSizeHeightMode.Margin:
                        this.Height = parentView.Height + this.SL_SizeMarginHeight - this.SL_SizePadddingHeight;
                        break;
                    case DivSizeHeightMode.EqualWidth:
                        this.Height = this.Width + this.SL_SizeMarginHeight;
                        break;
                    case DivSizeHeightMode.PrevHeight:
                        if (prevView != null)
                        {
                            this.Height = prevView.Height + this.SL_SizeMarginHeight;
                        }
                        break;
                    case DivSizeHeightMode.Fill:
                        if (parentView != null)
                        {
                            int heighto = 0;
                            foreach (DivView item in parentView.Viewes)
                            {
                                if (item == this)
                                    continue;
                                heighto = heighto + item.Height;
                            }
                            this.Height = parentView.Height - heighto;
                        }
                        break;
                    case DivSizeHeightMode.ChildHeight:
                        int height = 0; 
                        DivView pprevView = null;
                        foreach (DivView item in this.Viewes)
                        {
                            item.ReSize(sender, e, pprevView, this, ve);
                            pprevView = item;
                        }
                        foreach (DivView item in this.Viewes)
                        {
                            height = height + item.Height;
                        }
                        this.Height = height + this.SL_SizeMarginHeight;
                        break;
                    default:
                        break;
                }
            }
            finally
            {
                lockSizeViewHeigth = false;
            }
        }

        public virtual void SizeViewStaticMode(BaseView view)
        {
 
        }

        public virtual bool AutoSizeHeight()
        {
            return false;
        }

        public virtual bool AutoSizeWidth()
        {
            return false;
        }

        public event Feng.Forms.EventHandlers.ObjectEventHandler LocationChanged;
        public event Feng.Forms.EventHandlers.ObjectEventHandler SizeChanged;
        public virtual void OnLocationChanged()
        {
            if (LocationChanged != null)
            {
                LocationChanged(this);
            }
        }
        public virtual void OnSizeChanged()
        {
            if (SizeChanged != null)
            {
                SizeChanged(this);
            }
        }
        public virtual DivView Clone()
        {
            return new DivView();
        }
    }
}

