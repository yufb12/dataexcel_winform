using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Reflection;


namespace Feng.Utils
{
    public class KeyHook
    { 
        public event KeyEventHandler KeyEvent;

        static int hKeyboardHook = 0;
        Feng.Utils.UnsafeNativeMethods.HookProc proc;

        private int KeyboardHookProc(int Msg, Int32 wParam, IntPtr lParam)
        {
            int x = (int)wParam;
            KeyEventArgs e = new KeyEventArgs(((Keys)((int)((long)wParam))));
            if (wParam == (int)UnsafeNativeMethods.WM_KEYDOWN)
            {
                if (e.KeyCode == Keys.Up)
                {
                    if (Msg == UnsafeNativeMethods.WM_KEYDOWN)
                    {
                        if (KeyEvent != null)
                        {
                            KeyEvent(this, e);
                        }
                    }
                }
            }
            return UnsafeNativeMethods.CallNextHookEx(hKeyboardHook, Msg, wParam, lParam);
        }

        public void Start()
        {
            if (hKeyboardHook == 0)
            {
                proc = new Feng.Utils.UnsafeNativeMethods.HookProc(KeyboardHookProc);
                hKeyboardHook = UnsafeNativeMethods.SetWindowsHookEx(2, proc, 
                    IntPtr.Zero, UnsafeNativeMethods.GetCurrentThreadId());
            }
        }

        public void End()
        {
            UnsafeNativeMethods.UnhookWindowsHookEx(hKeyboardHook); 
        } 
    } 
}