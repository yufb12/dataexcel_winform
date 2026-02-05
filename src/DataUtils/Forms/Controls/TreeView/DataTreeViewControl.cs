using Feng.Forms.Views;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;

namespace Feng.Forms.Controls.TreeView
{
    [Docking(DockingBehavior.Ask)]
    public partial class DataTreeViewControl : ViewControl
    {
        public DataTreeViewControl()
            : base()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.ContainerControl, true);
            this.SetStyle(ControlStyles.StandardClick, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.UpdateStyles();
            this.BackColor = Color.White;
        }

        private DataTreeView _gridview = null;
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DataTreeView TreeView
        {
            get
            {
                try
                {

                    if (_gridview == null)
                    {
                        _gridview = new DataTreeView(this);
                        this.AddView(_gridview);
                    }
                }
                catch (Exception ex)
                {
                    Feng.Utils.ExceptionHelper.ShowError(ex);
                }
                return _gridview;
            }

        }

        private bool _readonly = false;
        public virtual bool ReadOnly
        {
            get
            {
                return this._readonly;
            }
            set
            {
                this._readonly = value;
            }
        }
 
        protected override void OnSizeChanged(EventArgs e)
        {

            try
            {
                if (this.TreeView != null)
                {
                    this.TreeView.RefreshAll();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

            base.OnSizeChanged(e);
        }
 
        protected override void OnMouseDown(MouseEventArgs e)
        {

            try
            { 
                this.Focus();
                //Views.EventViewArgs ve = GetViewsEventViewArgs();
                //TreeView.OnMouseDown(this, e, ve);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            base.OnMouseDown(e);
        }
 
 
    }


}
