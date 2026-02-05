using Feng.Data;
using Feng.Drawing;
using Feng.Forms.Controls.Designer;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Feng.Forms.Views
{
    public class ControlView : DivView
    {
        public ControlView()
        {

        }
        private System.Windows.Forms.Control bingdingcontrol = null;
        public System.Windows.Forms.Control BingDingControl { get { return bingdingcontrol; } }
        public virtual void BingDing(System.Windows.Forms.Control control)
        {
            if (control == null)
                return;
            bingdingcontrol = control;
            control.Tag = this;
            control.Layout += Control_Layout;
            control.KeyUp += Control_KeyUp;
            control.KeyPress += Control_KeyPress;
            control.Enter += Control_Enter;
            control.HandleDestroyed += Control_HandleDestroyed;
            control.HelpRequested += Control_HelpRequested;
            control.QueryAccessibilityHelp += Control_QueryAccessibilityHelp;
            control.QueryContinueDrag += Control_QueryContinueDrag;
            control.Paint += Control_Paint;
            control.Leave += Control_Leave;
            control.PaddingChanged += Control_PaddingChanged;
            control.DoubleClick += Control_DoubleClick;
            control.Invalidated += Control_Invalidated;
            control.MouseMove += Control_MouseMove;
            control.MouseClick += Control_MouseClick;
            control.MouseDoubleClick += Control_MouseDoubleClick;
            control.MouseCaptureChanged += Control_MouseCaptureChanged;
            control.MouseDown += Control_MouseDown;
            control.MouseEnter += Control_MouseEnter;
            control.MouseLeave += Control_MouseLeave;
            control.MouseHover += Control_MouseHover;
            control.HandleCreated += Control_HandleCreated;
            control.MouseUp += Control_MouseUp;
            control.MouseWheel += Control_MouseWheel;
            control.Move += Control_Move;
            control.PreviewKeyDown += Control_PreviewKeyDown;
            control.Resize += Control_Resize;
            control.ChangeUICues += Control_ChangeUICues;
            control.LostFocus += Control_LostFocus;
            control.GiveFeedback += Control_GiveFeedback;
            control.LocationChanged += Control_LocationChanged;
            control.DragOver += Control_DragOver;
            control.StyleChanged += Control_StyleChanged;
            control.AutoSizeChanged += Control_AutoSizeChanged;
            control.BackColorChanged += Control_BackColorChanged;
            control.BackgroundImageChanged += Control_BackgroundImageChanged;
            control.BackgroundImageLayoutChanged += Control_BackgroundImageLayoutChanged;
            control.BindingContextChanged += Control_BindingContextChanged;
            control.CausesValidationChanged += Control_CausesValidationChanged;
            control.ClientSizeChanged += Control_ClientSizeChanged;
            control.ContextMenuChanged += Control_ContextMenuChanged;
            control.ContextMenuStripChanged += Control_ContextMenuStripChanged;
            control.CursorChanged += Control_CursorChanged;
            control.DockChanged += Control_DockChanged;
            control.EnabledChanged += Control_EnabledChanged;
            control.DragLeave += Control_DragLeave;
            control.FontChanged += Control_FontChanged;
            control.MarginChanged += Control_MarginChanged;
            control.RegionChanged += Control_RegionChanged;
            control.RightToLeftChanged += Control_RightToLeftChanged;
            control.SizeChanged += Control_SizeChanged;
            control.TabIndexChanged += Control_TabIndexChanged;
            control.TabStopChanged += Control_TabStopChanged;
            control.TextChanged += Control_TextChanged;
            control.VisibleChanged += Control_VisibleChanged;
            control.Click += Control_Click;
            control.ControlAdded += Control_ControlAdded;
            control.ControlRemoved += Control_ControlRemoved;
            control.DragDrop += Control_DragDrop;
            control.DragEnter += Control_DragEnter;
            control.ForeColorChanged += Control_ForeColorChanged;
            control.GotFocus += Control_GotFocus;
            control.SystemColorsChanged += Control_SystemColorsChanged;
            control.KeyDown += Control_KeyDown;
            control.ImeModeChanged += Control_ImeModeChanged;
            control.Validated += Control_Validated;
            control.ParentChanged += Control_ParentChanged;
            control.Validating += Control_Validating;
        }

        public override void Invalidate()
        {
            bingdingcontrol.Invalidate();
            base.Invalidate();
        }

        public void Control_Layout(object sender, LayoutEventArgs e)
        {

        }
 
        private void Control_Validating(object sender, CancelEventArgs e)
        {

        }

        private void Control_ParentChanged(object sender, EventArgs e)
        {

        }

        private void Control_Validated(object sender, EventArgs e)
        {

        }

        private void Control_ImeModeChanged(object sender, EventArgs e)
        {

        }

        private void Control_KeyDown(object sender, KeyEventArgs e)
        {
            this.OnKeyDown(sender, e, Views.EventViewArgs.GetEventViewArgs(bingdingcontrol,e));
        }

        private void Control_SystemColorsChanged(object sender, EventArgs e)
        {

        }

        private void Control_GotFocus(object sender, EventArgs e)
        {

        }

        private void Control_ForeColorChanged(object sender, EventArgs e)
        {

        }

        private void Control_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void Control_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void Control_ControlRemoved(object sender, ControlEventArgs e)
        {

        }

        private void Control_ControlAdded(object sender, ControlEventArgs e)
        {

        }

        private void Control_Click(object sender, EventArgs e)
        {

        }

        private void Control_VisibleChanged(object sender, EventArgs e)
        {

        }

        private void Control_TextChanged(object sender, EventArgs e)
        {

        }

        private void Control_TabStopChanged(object sender, EventArgs e)
        {

        }

        private void Control_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void Control_SizeChanged(object sender, EventArgs e)
        {

        }

        private void Control_RightToLeftChanged(object sender, EventArgs e)
        {

        }

        private void Control_RegionChanged(object sender, EventArgs e)
        {

        }

        private void Control_MarginChanged(object sender, EventArgs e)
        {

        }

        private void Control_FontChanged(object sender, EventArgs e)
        {

        }

        private void Control_DragLeave(object sender, EventArgs e)
        {

        }

        private void Control_EnabledChanged(object sender, EventArgs e)
        {

        }

        private void Control_DockChanged(object sender, EventArgs e)
        {

        }

        private void Control_CursorChanged(object sender, EventArgs e)
        {

        }

        private void Control_ContextMenuStripChanged(object sender, EventArgs e)
        {

        }

        private void Control_ContextMenuChanged(object sender, EventArgs e)
        {

        }

        private void Control_ClientSizeChanged(object sender, EventArgs e)
        {

        }

        private void Control_CausesValidationChanged(object sender, EventArgs e)
        {

        }

        private void Control_BindingContextChanged(object sender, EventArgs e)
        {

        }

        private void Control_BackgroundImageLayoutChanged(object sender, EventArgs e)
        {

        }

        private void Control_BackgroundImageChanged(object sender, EventArgs e)
        {

        }

        private void Control_BackColorChanged(object sender, EventArgs e)
        {

        }

        private void Control_AutoSizeChanged(object sender, EventArgs e)
        {

        }

        private void Control_StyleChanged(object sender, EventArgs e)
        {

        }

        private void Control_DragOver(object sender, DragEventArgs e)
        {

        }

        private void Control_LocationChanged(object sender, EventArgs e)
        {

        }

        private void Control_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }

        private void Control_LostFocus(object sender, EventArgs e)
        {

        }

        private void Control_ChangeUICues(object sender, UICuesEventArgs e)
        {

        }

        private void Control_Resize(object sender, EventArgs e)
        {

        }

        private void Control_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {

        }

        private void Control_Move(object sender, EventArgs e)
        {

        }

        private void Control_MouseWheel(object sender, MouseEventArgs e)
        {

        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void Control_HandleCreated(object sender, EventArgs e)
        {

        }

        private void Control_MouseHover(object sender, EventArgs e)
        {

        }

        private void Control_MouseLeave(object sender, EventArgs e)
        {

        }

        private void Control_MouseEnter(object sender, EventArgs e)
        {

        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Control_MouseCaptureChanged(object sender, EventArgs e)
        {

        }

        private void Control_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void Control_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void Control_Invalidated(object sender, InvalidateEventArgs e)
        {

        }

        private void Control_DoubleClick(object sender, EventArgs e)
        {

        }

        private void Control_PaddingChanged(object sender, EventArgs e)
        {

        }

        private void Control_Leave(object sender, EventArgs e)
        {

        }
        private Feng.Drawing.GraphicsObject currentGraphicsObject = null;
        private void Control_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                if (currentGraphicsObject == null)
                {
                    currentGraphicsObject = new Feng.Drawing.GraphicsObject();
                }
                currentGraphicsObject.Graphics = e.Graphics;
                currentGraphicsObject.MousePoint = System.Windows.Forms.Control.MousePosition;
                currentGraphicsObject.ModifierKeys = System.Windows.Forms.Control.ModifierKeys;
                currentGraphicsObject.MouseButtons = System.Windows.Forms.Control.MouseButtons;
                currentGraphicsObject.Control = this.bingdingcontrol;
                currentGraphicsObject.ClientPoint = currentGraphicsObject.MousePoint;
                currentGraphicsObject.ClipRectangle = this.Bounds;
                Graphics g = e.Graphics;
                GraphicsState gs = g.Save();
                g.PageUnit = GraphicsUnit.Pixel;
                g.ResetTransform();
                if (currentGraphicsObject.Items != null)
                {
                    currentGraphicsObject.Items.Clear();
                }
 
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnDrawBack(this, currentGraphicsObject);
                    if (res)
                    {
                        g.Restore(gs);
                        return;
                    }
                }
                foreach (BaseView item in Viewes)
                {
                    bool res = item.OnDraw(this, currentGraphicsObject);
                    if (res)
                    {
                        g.Restore(gs);
                        return;
                    }
                }
                g.Restore(gs);
                //currentGraphicsObject.Graphics.ResetClip();
                //currentGraphicsObject.Graphics.ResetTransform();
                //currentGraphicsObject.Graphics.DrawString(currentGraphicsObject.DebugText, 
                //    currentGraphicsObject.DefaultFont, Brushes.Red, currentGraphicsObject.WorkArea);
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                Feng.Utils.TraceHelper.WriteTrace("Views", "ViewControl", "OnMouseWheel", ex);
            }
        }

        private void Control_QueryContinueDrag(object sender, QueryContinueDragEventArgs e)
        {

        }

        private void Control_QueryAccessibilityHelp(object sender, QueryAccessibilityHelpEventArgs e)
        {

        }

        private void Control_HelpRequested(object sender, HelpEventArgs hlpevent)
        {

        }

        private void Control_HandleDestroyed(object sender, EventArgs e)
        {

        }

        private void Control_Enter(object sender, EventArgs e)
        {

        }

        private void Control_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void Control_KeyUp(object sender, KeyEventArgs e)
        {

        }
    }
}

