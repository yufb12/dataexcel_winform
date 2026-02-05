using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Reflection;

namespace Feng.Utils
{ 
    public class KeyboardHook
    {
        public event KeyEventHandler KeyDown;
        public event KeyPressEventHandler KeyPress;
        public event KeyEventHandler KeyUp;
         
        static int handle = 0;
        private bool _started = false;
        public bool Started
        {
            get
            {
                return _started;
            }
        }
        Feng.Utils.UnsafeNativeMethods.HookProc KeyboardHookProcedure; 
 
        public bool Start()
        { 
            if (handle == 0)
            {
                KeyboardHookProcedure = new Feng.Utils.UnsafeNativeMethods.HookProc(KeyboardHookProc);
                handle = Feng.Utils.UnsafeNativeMethods.SetWindowsHookEx(
                    Feng.Utils.UnsafeNativeMethods.WH_KEYBOARD_LL, KeyboardHookProcedure,
                    Feng.Utils.UnsafeNativeMethods.GetModuleHandle(System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName), 0);
 
                if (handle != 0)
                {
                    _started = true;
                    return true;
                } 
            }
            return false;
        }

        public void Stop()
        { 
            if (_started)
            { 
                int ret = Utils.UnsafeNativeMethods.UnhookWindowsHookEx(handle); 
            }
            _started = false; 
        } 
 
        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        { 
            if ((nCode >= 0) && (KeyDown != null || KeyUp != null || KeyPress != null))
            {
                Utils.UnsafeNativeMethods.KeyboardHookStruct MyKeyboardHookStruct = (Utils.UnsafeNativeMethods.KeyboardHookStruct)Marshal.PtrToStructure(lParam, typeof(Utils.UnsafeNativeMethods.KeyboardHookStruct));

                if (KeyDown != null && (wParam == Utils.UnsafeNativeMethods.WM_KEYDOWN || wParam == Utils.UnsafeNativeMethods.WM_SYSKEYDOWN))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    KeyDown(this, e);
                }

                if (KeyPress != null && wParam == Utils.UnsafeNativeMethods.WM_KEYDOWN)
                {
                    byte[] keyState = new byte[256];
                    Utils.UnsafeNativeMethods.GetKeyboardState(keyState);

                    byte[] inBuffer = new byte[2];
                    if (Utils.UnsafeNativeMethods.ToAscii(MyKeyboardHookStruct.vkCode, MyKeyboardHookStruct.scanCode, keyState, inBuffer, MyKeyboardHookStruct.flags) == 1)
                    {
                        KeyPressEventArgs e = new KeyPressEventArgs((char)inBuffer[0]);
                        KeyPress(this, e);
                    }
                }
                if (KeyUp != null && (wParam == Utils.UnsafeNativeMethods.WM_KEYUP || wParam == Utils.UnsafeNativeMethods.WM_SYSKEYUP))
                {
                    Keys keyData = (Keys)MyKeyboardHookStruct.vkCode;
                    KeyEventArgs e = new KeyEventArgs(keyData);
                    KeyUp(this, e);
                }
            }
            return Utils.UnsafeNativeMethods.CallNextHookEx(handle, nCode, wParam, lParam);
        } 
    }
}