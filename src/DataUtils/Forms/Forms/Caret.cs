using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Feng.Forms
{
    public class Caret
    {
        public Caret()
        {

        }
        private IntPtr handle = IntPtr.Zero;
        private bool _Visible = false;
        private bool _init = false;
        private int x = 0;
        private int y = 0;

        public IntPtr Handle
        {
            get
            {
                return handle;
            }
            set
            {
                handle = value;
            }
        }
        public bool Visible
        {
            get { return _Visible; }
            set { _Visible = value; }
        }
 
        private int _height = 16;
        public int Height
        {
            get { return this._height; }
        }
        public bool Init
        {
            get
            {
                return _init;
            }
        }

        public int Y { get { return y; } set { y = value; } }
        public int X { get { return x; } set { x = value; } }

        public bool Create(IntPtr vhandle, int width, int height)
        {
            handle = vhandle;
            if (Feng.Utils.UnsafeNativeMethods.CreateCaret(handle, 0, width, height))
            {
                _init = true;
                this._Visible = false;
            }
            return _init;
        }

        public void Hide()
        {
            if (Feng.Utils.UnsafeNativeMethods.HideCaret(handle))
            {
                _Visible = false;
            }
        }

        private bool Show()
        {
            if (Feng.Utils.UnsafeNativeMethods.ShowCaret(handle))
            {
                _Visible = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Show(Point location)
        {
            Show(this.Handle,1, 12, location.X, location.Y);
        }

        public bool SetPosition(Point pt)
        {
            x = pt.X;
            y = pt.Y;
            return Feng.Utils.UnsafeNativeMethods.SetCaretPos(pt.X, pt.Y);
        }

        public bool SetPosition(int x1, int y1)
        {
            x = x1;
            y = y1;
            return Feng.Utils.UnsafeNativeMethods.SetCaretPos(x, y);
        }

        public bool SetPosition(Point pt, int height)
        {
            if (Feng.Utils.UnsafeNativeMethods.CreateCaret(handle, 0, 2, height))
            {
                return Feng.Utils.UnsafeNativeMethods.SetCaretPos(pt.X, pt.Y);
            }
            return false;
        }

        public bool SetPosition(int x, int y, int height)
        {
            if (Feng.Utils.UnsafeNativeMethods.CreateCaret(handle, 0, 2, height))
            {
                return Feng.Utils.UnsafeNativeMethods.SetCaretPos(x, y);
            }
            return false;
        }

        public void Destroy()
        {
            Feng.Utils.UnsafeNativeMethods.DestroyCaret();
        }

        public void Show(IntPtr handle, int height, int left, int top)
        {
            Show(handle, 2, height, left, top);
        }

        public void Show(IntPtr handle, float height, float left, float top)
        {
            Show(handle, 2, (int)height, (int)left, (int)top);
        }

        public void Show(IntPtr handle,int thickness, int height, int left, int top)
        {
            if (this.Handle != handle)
            {
                Create(this.Handle, thickness, height);
                this.Handle = handle;
            } 
            if (_height != height)
            {
                this._height = height;
                Destroy();
                Create(this.handle, thickness, height);
            }
            if (!this.Visible)
            {
                Create(this.handle, thickness, height);
            }
            if (!Show())
            {
                Create(this.handle, thickness, height);
            }
            SetPosition(left, top);
        } 
    }
}
