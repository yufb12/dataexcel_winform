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
using Feng.Forms.Controls.GridControl.Edits;
using Feng.Forms.Controls.GridControl;

namespace Feng.Forms.Views
{
    public class MoveView : DivView
    {
        public MoveView()
        {

        }
        private StateMode _StateMode = StateMode.NULL;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public StateMode StateMode
        {
            get { return _StateMode; }
            set { _StateMode = value; }
        }
        public override bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if (this.Bounds.Contains(ve.ViewPoint))
            {
                _MouseDownPoint = System.Windows.Forms.Control.MousePosition;
                _StateMode = StateMode.MOVE;
                MouseDownLocation = new Point(this.Left, this.Top);
                return true;
            }
            return base.OnMouseDown(sender, e, ve);
        }

        public override bool OnMouseUp(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            _StateMode = StateMode.NULL;
            return base.OnMouseUp(sender, e, ve);
        }

        public override bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if (_StateMode == StateMode.MOVE)
            {
                int x = System.Windows.Forms.Control.MousePosition.X - _MouseDownPoint.X;
                int y = System.Windows.Forms.Control.MousePosition.Y - _MouseDownPoint.Y;
                this.Left = MouseDownLocation.X + x;
                this.Top = MouseDownLocation.Y + y;
                ve.Invalate = true;
                OnLocationChanged(sender, e, ve);
                return true;
            }
            return base.OnMouseMove(sender, e, ve);
        }

        public virtual void OnLocationChanged(object sender, MouseEventArgs e, EventViewArgs ve)
        {

        }

        public override DataStruct Data { get { return new DataStruct(); } }


        private Point _MouseDownPoint = Point.Empty;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point MouseDownPoint
        {
            get { return _MouseDownPoint; }
            set { _MouseDownPoint = value; }
        }


        private Point MouseDownLocation = Point.Empty;

    }

    public interface ILocationMove
    {
        void LocationMoved(object sender, int x, int y, int incrementalx, int incrementaly, bool moving, MouseEventArgs e, EventViewArgs ve);
    }
    public class MoveViewObj
    {
        public MoveViewObj()
        {

        }
        public BaseView View { get; set; }
        private StateMode _StateMode = StateMode.NULL;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public StateMode StateMode
        {
            get { return _StateMode; }
            set { _StateMode = value; }
        }
        public bool OnMouseDown(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if (View.Bounds.Contains(ve.ViewPoint))
            {
                _MouseDownPoint = System.Windows.Forms.Control.MousePosition;
                _StateMode = StateMode.MOVE;
                MouseDownLocation = new Point(View.Left, View.Top);
                return true;
            }
            return false;
        }

        public bool OnMouseUp(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if (_StateMode == StateMode.MOVE)
            {
                int x = System.Windows.Forms.Control.MousePosition.X - _MouseDownPoint.X;
                int y = System.Windows.Forms.Control.MousePosition.Y - _MouseDownPoint.Y;
                int Left = MouseDownLocation.X + x;
                int Top = MouseDownLocation.Y + y;
                ve.Invalate = true;
                OnLocationChanged(sender, Left, Top, x, y, false, e, ve);
                _StateMode = StateMode.NULL;
                return true;
            }
            _StateMode = StateMode.NULL;
            return false;
        }

        public bool OnMouseMove(object sender, MouseEventArgs e, EventViewArgs ve)
        {
            if (_StateMode == StateMode.MOVE)
            {
                int x = System.Windows.Forms.Control.MousePosition.X - _MouseDownPoint.X;
                int y = System.Windows.Forms.Control.MousePosition.Y - _MouseDownPoint.Y;
                int Left = MouseDownLocation.X + x;
                int Top = MouseDownLocation.Y + y;
                ve.Invalate = true;
                OnLocationChanged(sender, Left, Top, x, y, true, e, ve);
                return true;
            }
            return false;
        }

        public virtual void OnLocationChanged(object sender, int x, int y, int incrementalx, int incrementaly, bool moving, MouseEventArgs e, EventViewArgs ve)
        {
            ILocationMove locationMove = this.View as ILocationMove;
            if (locationMove != null)
            {
                locationMove.LocationMoved(sender, x, y, incrementalx, incrementaly, moving, e, ve);
            }
        }

        private Point _MouseDownPoint = Point.Empty;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point MouseDownPoint
        {
            get { return _MouseDownPoint; }
            set { _MouseDownPoint = value; }
        }

        private Point MouseDownLocation = Point.Empty;
    }
}

