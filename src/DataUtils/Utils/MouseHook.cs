using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace Feng.Utils
{

    public class MouseHook
    {
        private static MouseHook instance = null;
        public static MouseHook Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MouseHook();
                }
                return instance;
            }
        }
        private MouseHook()
        {

        }

        int handle = 0;

        private Feng.Utils.UnsafeNativeMethods.HookProc MouseHookProcedure;
        private bool _started = false;

        public bool Started
        {
            get
            {
                return _started;
            }
        }

        public void Start()
        {
            if (Started)
                return;
            Feng.Utils.TraceHelper.WriteTrace("MouseHook", "Start", "Start", true, "Begin");
            MouseHookProcedure = CallNextProc;
            handle = Utils.UnsafeNativeMethods.SetWindowsHookEx(Utils.UnsafeNativeMethods.WH_MOUSE_LL,
    MouseHookProcedure, 0, 0);
            if (handle != 0)
            {
                _started = true;
            }
            else
            {
                int errorCode = Marshal.GetLastWin32Error();
                Exception ex = new Exception(errorCode.ToString());
                Feng.Utils.BugReport.Log(ex); 
            }

            Feng.Utils.TraceHelper.WriteTrace("MouseHook", "Start", "Start", true, "End");
        }

        public void Stop()
        {
            return;
            Feng.Utils.TraceHelper.WriteTrace("MouseHook", "Stop", "Stop", true, "Begin");
            if (_started)
            {
                int ret = Utils.UnsafeNativeMethods.UnhookWindowsHookEx(handle);
                if (ret == 0)
                {
                    int errorCode = Marshal.GetLastWin32Error();
                    Exception ex = new Exception(errorCode.ToString());
                    Feng.Utils.BugReport.Log(ex);
                }
            }
            _started = false;
            Feng.Utils.TraceHelper.WriteTrace("MouseHook", "Stop", "Stop", true, "End");
        }

        public delegate bool MouseDownHookHandler(object sender, MouseEventArgs e);
        public event MouseDownHookHandler MouseDown;
        public bool Handler = true;
        public int CallNextProc(int nCode, int wParam, IntPtr lParam)
        { 
            try
            {
                if (Handler)
                {
                    if (wParam == Utils.UnsafeNativeMethods.WM_LBUTTONDOWN)
                    {
                        if (MouseDown != null)
                        {
                            Feng.Utils.UnsafeNativeMethods.MouseLLHookStruct mouseHookStruct
                                = (Feng.Utils.UnsafeNativeMethods.MouseLLHookStruct)Marshal.PtrToStructure(lParam, typeof(Feng.Utils.UnsafeNativeMethods.MouseLLHookStruct));
                            MouseButtons button = MouseButtons.None;
                            int clickCount = 0;
                            short mouseDelta = 0;
                            mouseDelta = (short)((mouseHookStruct.mouseData >> 16) & 0xffff);
                            if (mouseHookStruct.dwExtraInfo < 0)
                            {
                                mouseDelta = (short)(mouseDelta * -1);
                            }
                            MouseEventArgs e = new MouseEventArgs(
                                        button,
                                        clickCount,
                                        mouseHookStruct.pt.x,
                                        mouseHookStruct.pt.y,
                                        mouseDelta);

                            try
                            {
                                if (MouseDown(this, e))
                                {

                                }
                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                            finally
                            {
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            { 
                Feng.Utils.BugReport.Log(ex);
            }
           
            return Utils.UnsafeNativeMethods.CallNextHookEx(handle, nCode, wParam, lParam);

        }
        public virtual void OnMouseDown()
        {
            MouseEventArgs e = new MouseEventArgs(
              MouseButtons.Left,
              1,
              System.Windows.Forms.Control.MousePosition.X,
             System.Windows.Forms.Control.MousePosition.Y,
             1
             );

            try
            {
                if (MouseDown != null)
                {
                    if (MouseDown(this, e))
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
                throw ex;
            }
            finally
            {
            }
        }
        public static void MouseClick()
        {
            try
            {
                UnsafeNativeMethods.mouse_event(UnsafeNativeMethods.MOUSEEVENTF_RIGHTDOWN | UnsafeNativeMethods.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);

            }
            catch (Exception ex)
            {
                 Feng.Utils.BugReport.Log(ex);
            }
        }
        public static void MouseMove(int x, int y)
        {
            try
            {
                UnsafeNativeMethods.mouse_event(UnsafeNativeMethods.MOUSEEVENTF_ABSOLUTE | UnsafeNativeMethods.MOUSEEVENTF_MOVE, x, y, 0, 0);
            }
            catch (Exception ex)
            {
                Feng.Utils.BugReport.Log(ex);
            }
        }
    }
}
