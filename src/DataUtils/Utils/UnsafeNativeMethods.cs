using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Feng.Utils
{
    public static class UnsafeNativeMethods
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("USER32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("USER32.dll")]
        public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        [DllImport("Imm32.dll")]
        public static extern bool ImmAssociateContextEx(IntPtr hWnd, IntPtr zero, int v);

        [DllImport("Imm32.dll")]
        public static extern bool ImmSetOpenStatus(IntPtr hIMC, bool v);

        [DllImport("Imm32.dll")]
        public static extern bool ImmReleaseContext(IntPtr hWnd, IntPtr hIMC);

        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr HWnd, uint Msg, int WParam, int LParam);


        [DllImport("user32.dll")]
        public static extern bool SetProcessDPIAware();

        [DllImport("kernel32.dll")]
        public static extern ulong GetTickCount64();

        //[DllImport("Shell32.dll")]
        //public static extern int SHGetFileInfo(string pszPath, uint dwFileAttributes, ref   SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        [DllImport("mscoree.dll", CharSet = CharSet.Unicode)]
        public static extern bool StrongNameSignatureVerificationEx(string wszFilePath, bool fForceVerification, ref bool pfWasVerified);

        [DllImport("User32.dll", EntryPoint = "DestroyIcon")]
        private static extern int DestroyIcon(IntPtr hIcon);
        //[DllImport("user32.dll")]
        //public static extern int SendMessage(IntPtr hWnd, int Msg, uint wParam, uint lParam);
        //[DllImport("USER32.dll")]
        //public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        //[DllImport("USER32.dll")]
        //public static extern bool ShowWindow(IntPtr hWnd, int cmdShow);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, IntPtr threadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(IntPtr handler);

        //[DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        //public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);

        //[DllImport("kernel32.dll")]
        //public static extern int GetCurrentThreadId();

        [DllImport("user32")]
        public static extern int ToAscii(int uVirtKey,
                                         int uScanCode,
                                         byte[] lpbKeyState,
                                         byte[] lpwTransKey,
                                         int fuState);



        [DllImport("user32")]
        public static extern int GetKeyboardState(byte[] pbKeyState);


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern short GetKeyState(int vKey);

        [DllImport("Imm32.dll")]
        public static extern IntPtr ImmGetContext(IntPtr hWnd);
        [DllImport("Imm32.dll")]
        public static extern IntPtr ImmAssociateContext(IntPtr hWnd, IntPtr hIMC);

        [DllImport("imm32.dll")]
        public static extern int ImmGetCompositionString(IntPtr hIMC, int dwIndex, StringBuilder lPBuf, int dwBufLen);

        [DllImport("imm32.dll")]
        public static extern bool ImmSetCompositionWindow(IntPtr hIMC, ref COMPOSITIONFORM lpCompForm);

        [DllImport("User32.dll")]
        public static extern bool CreateCaret(IntPtr hWnd, int hBitmap, int nWidth, int nHeight);

        [DllImport("User32.dll")]
        public static extern bool SetCaretPos(int x, int y);

        [DllImport("User32.dll")]
        public static extern bool DestroyCaret();

        [DllImport("User32.dll")]
        public static extern bool ShowCaret(IntPtr hWnd);

        [DllImport("User32.dll")]
        public static extern bool HideCaret(IntPtr hWnd);

        [DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")]
        public static extern int SetLayeredWindowAttributes(
        IntPtr hwnd,
        int crKey,
        int bAlpha,
        int dwFlags
        );

        [DllImport("kernel32.dll")]
        public static extern bool FreeConsole();

        [DllImport("kernel32.dll")]
        public static extern bool AllocConsole();

        [DllImport("User32.DLL")]
        private static extern int SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("User32.DLL")]
        private static extern bool ReleaseCapture();
        private const uint WM_SYSCOMMAND = 0x0112;
        private const int WM_SETREDRAW = 11;
        private const int SC_MOVE = 61456;
        private const int HTCAPTION = 2;
        public static void MoveWindow(IntPtr handle)
        {
            ReleaseCapture();
            SendMessage(handle, WM_SYSCOMMAND, SC_MOVE | HTCAPTION, 5);
        }

        public static Bitmap GetBitMapFromFileExten(string exten)
        {
            string dic = Feng.IO.FileHelper.GetStartUpFileUSER("Temp", "\\IcoTemp");
            if (!System.IO.Directory.Exists(dic))
            {
                System.IO.Directory.CreateDirectory(dic);
            }
            string path = dic + "\\TEMP_FILEEXTEN" + exten;
            IO.FileHelper.WriteAllBytes(path, new byte[] { 0x01, 0x02 });
            Icon icon = GetLargeFileIcon(path);
            if (icon == null)
            {
                return new Bitmap(16, 16);
            }
            Bitmap bmp = icon.ToBitmap();
            return bmp;
        }

        public static Icon GetIcon(string fileName)
        {
            return GetLargeFileIcon(fileName);
        }

        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int x;

            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        public class MouseLLHookStruct
        {
            public POINT pt;
            public int mouseData;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        /// <summary>
        /// 返回系统设置的图标
        /// </summary>
        /// <param name="pszPath">文件路径 如果为""  返回文件夹的</param>
        /// <param name="dwFileAttributes">0</param>
        /// <param name="psfi">结构体</param>
        /// <param name="cbSizeFileInfo">结构体大小</param>
        /// <param name="uFlags">枚举类型</param>
        /// <returns>-1失败</returns>
        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref   SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        public static Icon GetLargeFileIcon(string file)
        {
            SHFILEINFO _SHFILEINFO = new SHFILEINFO();
            IntPtr _IconIntPtr = SHGetFileInfo(file, 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI_ICON | SHGFI_LARGEICON | SHGFI_USEFILEATTRIBUTES));
            if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
            Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
            return _Icon;
        }
        public static Icon GetSmallFileIcon(string file)
        {
            SHFILEINFO _SHFILEINFO = new SHFILEINFO();
            IntPtr _IconIntPtr = SHGetFileInfo(file, 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI_ICON | SHGFI_SMALLICON | SHGFI_USEFILEATTRIBUTES));
            if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
            Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
            return _Icon;
        }
        /// <summary>
        /// 获取文件夹图标  
        /// </summary>
        /// <returns>图标</returns>
        public static Icon GetDirectoryIcon()
        {
            SHFILEINFO _SHFILEINFO = new SHFILEINFO();
            IntPtr _IconIntPtr = SHGetFileInfo(@"", 0, ref _SHFILEINFO, (uint)Marshal.SizeOf(_SHFILEINFO), (uint)(SHGFI_ICON | SHGFI_LARGEICON));
            if (_IconIntPtr.Equals(IntPtr.Zero)) return null;
            Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
            return _Icon;
        }


        private const uint SHGFI_ICON = 0x100;
        private const uint SHGFI_LARGEICON = 0x0; //大图标 32×32
        private const uint SHGFI_SMALLICON = 0x1; //小图标 16×16
        private const uint SHGFI_USEFILEATTRIBUTES = 0x10;

        public const int WM_IME_SETCONTEXT = 0x0281;
        public const int WM_IME_CHAR = 0x0286;
        public const int WM_CHAR = 0x0102;
        public const int WM_IME_COMPOSITION = 0x010F;
        public const int GCS_COMPSTR = 0x0008;
        public const int WM_KEYUP = 0x0101;

        public const int WM_IME_STARTCOMPOSITION = 0x010D;
        public const int WM_IME_ENDCOMPOSITION = 0x010E; 

        public const int WM_KEYDOWN = 0x0100;
        public const int WM_SYSKEYDOWN = 0x104;
        public const int WM_SYSKEYUP = 0x105;
        public const int WM_PASTE = 0x0302;
        public const int WM_ACTIVATEAPP = 0x1c;
        public const int WM_KILLFOCUS = 0x8;
        public const int WM_SETCURSOR = 0x20;
        public const int WM_CTLCOLOREDIT = 0x133;
        public const int WM_GETTEXT = 0xd;
        public const int WM_SIZE = 0xe;
        public const int WM_SHOWWINDOW = 0x18;
        public const int WM_STYLECHANGED = 0x7d;
        public const int WM_STYLECHANGING = 0x7c;
        public const int WM_SETICON = 0x80;
        public const uint WM_CLOSE = 0x10;
        public const uint WM_DESTROY = 0x02;
        public const uint WM_QUIT = 0x12;
        public const int WH_MOUSE = 7;
        public const int WH_MOUSE_LL = 14;

        public const int WH_KEYBOARD_LL = 13;
        public const int WM_LBUTTONDOWN = 0x201;
        public const int WM_LBUTTONUP = 0x202;
        public const int WM_CUT = 0x0300;
        public const int WM_COPY = 0x0301;
        public const int WM_VALITATE = 0x7852;

        public const int HC_ACTION = 0;
        public const int PM_REMOVE = 0x0001;
        public const int GCS_RESULTSTR = 0x0800;
        public const int GW_HWNDFIRST = 0;
        public const int GW_HWNDLAST = 1;
        public const int GW_HWNDNEXT = 2;
        public const int GW_HWNDPREV = 3;
        public const int GW_OWNER = 4;
        public const int GW_CHILD = 5;
        public const int GW_ENABLEDPOPUP = 6;
        public const int GW_MAX = 6;

        public const int WS_EX_WINDOWEDGE = 0x100; //窗口具有凸起的3D边框 
        public const int WS_EX_CLIENTEDGE = 0x200; //窗口具有阴影边界 
        public const int WS_EX_TOOLWINDOW = 0x80; //小标题工具窗口 
        public const int WS_EX_TOPMOST = 0x8; //窗口总在顶层 
        public const int WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE); //WS_EX-CLIENTEDGE和WS_EX_WINDOWEDGE的组合 
        public const int WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST); //WS_EX_WINDOWEDGE和WS_EX_TOOLWINDOW和WS_EX_TOPMOST的组合 
        public const int WS_EX_DLGMODALFRAME = 0x1; //带双边的窗口 
        public const int WS_EX_NOPARENTNOTIFY = 0x4; //窗口在创建和销毁时不向父窗口发送WM_PARENTNOTIFY消息 
        public const int WS_EX_TRANSPARENT = 0x20; //窗口透眀 
        public const int WS_EX_MDICHILD = 0x40; //MDI子窗口 
        public const int WS_EX_CONTEXTHELP = 0x400; //标题栏包含问号联机帮助按钮 
        public const int WS_EX_RIGHT = 0x1000; //窗口具有右对齐属性 
        public const int WS_EX_RTLREADING = 0x2000; //窗口文本自右向左显示 
        public const int WS_EX_LEFTSCROLLBAR = 0x4000; //标题栏在客户区的左边 
        public const int WS_EX_CONTROLPARENT = 0x10000; //允许用户使用Tab键在窗口的子窗口间搜索 
        public const int WS_EX_STATICEDGE = 0x20000; //为不接受用户输入的项创建一个三维边界风格 
        public const int WS_EX_APPWINDOW = 0x40000; //在任务栏上显示顶层窗口的标题按钮 
        public const int WS_EX_LAYERED = 0x80000; //窗口具有透眀属性(Win2000)以上 
        public const int WS_EX_NOINHERITLAYOUT = 0x100000; //窗口布局不传递给子窗口(Win2000)以上 
        public const int WS_EX_LAYOUTRTL = 0x400000; //水平起点在右边的窗口 
        public const int WS_EX_NOACTIVATE = 0x8000000; //窗口不会变成前台窗口(Win2000)以上 
        public const int WS_EX_LEFT = 0x0; //窗口具有左对齐属性 
        public const int WS_EX_LTRREADING = 0x0; //窗口文本自左向右显示 
        public const int WS_EX_RIGHTSCROLLBAR = 0x0; //垂直滚动条在窗口的右边界 
        public const int WS_EX_ACCEPTFILES = 0x10; //接受文件拖曳 
        public const int WS_EX_COMPOSITED = 0x2000000; //窗体所有子窗口使用双缓冲从低到高绘制(XP) 
        public const int GWL_STYLE = (-16);
        public const int GWL_EXSTYLE = (-20);
        public const int LWA_ALPHA = 0x2;
        public const int LWA_COLORKEY = 0x1;
        public const int WM_NCACTIVATE = 0x86;

        public static void EmulateFormFocus(IntPtr formHandle)
        {
            SendMessage(formHandle, 0x86, 1, 0);
        }

        ////////SWP_DRAWFRAME  围绕窗口画一个框 
        ////////SWP_HIDEWINDOW  隐藏窗口 
        ////////SWP_NOACTIVATE  不激活窗口 
        ////////SWP_NOMOVE  保持当前位置 (x和y设定将被忽略) &H2 
        ////////SWP_NOREDRAW  窗口不自动重画 
        ////////SWP_NOSIZE  保持当前大小 (cx和cy会被忽略) &H1 
        ////////SWP_NOZORDER  保持窗口在列表的当前位置 (hWndInsertAfter将被忽略) 
        ////////SWP_SHOWWINDOW 显示窗口 &H40 
        ////////SWP_FRAMECHANGED 强迫一条WM_NCCALCSIZE消息进入窗口，即使窗口的大小没有改变 

        [DllImport("kernel32.dll")]
        public static extern int GetCurrentThreadId();

        [DllImport("user32.dll", CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, int dwThreadId);


        [DllImport("user32.dll", CharSet = CharSet.Auto,
           CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, int hMod, int dwThreadId);
        //public delegate int HookProc(int nCode, int wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern int UnhookWindowsHookEx(int idHook);


        [DllImport("user32.dll", CharSet = CharSet.Auto,
             CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(
            int idHook,
            int nCode,
            int wParam,
            IntPtr lParam);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetModuleHandle(string name);

        public delegate int HookProc(int Msg, Int32 wParam, IntPtr lParam);


        [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]
        public static extern IntPtr WindowFromPoint(LPPOINT Point);

        public struct LPPOINT
        {
            public int X;
            public int Y;
        }
        //public struct RECT
        //{
        //    public int Left;
        //    public int Top;
        //    public int Right;
        //    public int Bottom;
        //}
        /// <summary>
        /// 结构体数据成员的意义
        //编辑

        //WINDOWPLACEMENT 结构中包含了有关窗口在屏幕上位置的信息。
        //成员：
        //length

        //length指定了结构的长度，以字节为单位。
        //flags

        //flags指定了控制最小化窗口的位置的标志以及复原窗口的方法。这个成员可以是下面列出的标志之一，或都是： · WPF_SETMINPOSITION 表明可以指定最小化窗口的x和y坐标。如果是在ptMinPosition成员中设置坐标，则必须指定这个标志。
        //·
        //showCmd

        //WPF_RESTORETOMAXIMIZED表明复原后的窗口将会被最大化，而不管它在最小化之前是否是最大化的。这个设置仅在下一次复原窗口时有效。它不改变缺省的复原操作。这个标志仅当showCmd成员中指定了SW_SHOWMINIMIZED时才有效。
        //showCmd 指定了窗口的当前显示状态。这个成员可以是下列值之一： ·
        //SW_HIDE 隐藏窗口，使其它窗口变为激活的。
        //· SW_MINIMIZE 最小化指定的窗口，并激活系统列表中的顶层窗口。
        //· SW_RESTORE 激活并显示窗口。如果窗口是最小化或最大化的，Windows将把它恢复到原来的大小和位置（与SW_SHOWNORMAL相同）。
        //· SW_SHOW 激活窗口并按照当前的位置和大小显示窗口。
        //· SW_SHOWMAXIMIZED 激活窗口并将其显示为最大化的。
        //· SW_SHOWMINIMIZED 激活窗口并将其显示为图标。
        //· SW_SHOWMINNOACTIVE 将窗口显示为图标。当前激活的窗口仍保持激活状态。
        //· SW_SHOWNA 按当前状态显示窗口。当前激活的窗口仍保持激活状态。
        //· SW_SHOWNOACTIVATE 按最近的位置和大小显示窗口。当前激活的窗口仍保持激活状态。
        //· SW_SHOWNORMAL 激活并显示窗口。如果窗口是最小化或最大化的，Windows将它恢复到原来的大小和位置（与SW_RESTORE相同）。
        //ptMinPosition

        //ptMinPosition 指定了窗口被最小化时左上角的位置。
        //ptMaxPosition

        //ptMaxPosition 指定了窗口被最大化时左上角的位置。
        //rcNormalPosition

        //rcNormalPosition 指定了窗口处于正常状态（复原）时的坐标。
        /// </summary>
        //public struct WINDOWPLACEMENT
        //{
        //    public int Length;
        //    public int flags;
        //    public int showCmd;
        //    public LPPOINT ptMinPosition;
        //    public LPPOINT ptMaxPosition;
        //    public RECT rcNormalPosition;
        //}
        //public enum enumshow
        //{
        //    show = 3,
        //    normal = 2,
        //    WS_VISIBLE = 0x10000000,
        //    WS_DISABLED = 0x8000000,
        //    GWL_STYLE = -16,
        //    hide = 1

        //}
        //public enum enumwindpose
        //{
        //    HWND_TOP = 0,
        //    HWND_BOTTOM = 1,
        //    HWND_TOPMOST = -1,
        //    HWND_NOTOPMOST = -2
        //}
        //public enum enumplacement
        //{
        //    SW_SHOW = 5,
        //    SW_ERASE = 0x4,
        //    SW_HIDE = 0,
        //    SW_INVALIDATE = 0x2,
        //    SW_MAX = 10,
        //    SW_MAXIMIZE = 3,
        //    SW_MINIMIZE = 6,
        //    SW_NORMAL = 1,
        //    SW_OTHERUNZOOM = 4,
        //    SW_OTHERZOOM = 2,
        //    SW_PARENTCLOSING = 1,
        //    SW_PARENTOPENING = 3,
        //    SW_RESTORE = 9,
        //    SW_SCROLLCHILDREN = 0x1,
        //    SW_SHOWDEFAULT = 10,
        //    SW_SHOWMAXIMIZED = 3,
        //    SW_SHOWMINIMIZED = 2,
        //    SW_SHOWMINNOACTIVE = 7,
        //    SW_SHOWNA = 8,
        //    SW_SHOWNOACTIVATE = 4,
        //    SW_SHOWNORMAL = 1,
        //    SW_FORCEMINIMIZE = 11
        //}

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int GetDesktopWindow();
        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int GetWindow(int hwnd, int uCmd);
        [DllImport("user32", EntryPoint = "GetWindowLongA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int GetWindowLong(int hwnd, int nIndex);
        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool GetWindowPlacement(int hwnd, ref Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT lpwndpl);
        [DllImport("user32.dll", EntryPoint = "GetWindowTextA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int GetWindowText(int hwnd, [MarshalAs(UnmanagedType.VBByRefStr)] ref string lpString, int cch);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int GetWindowThreadProcessId(int hwnd, ref int lpdwProcessId);

        [DllImport("user32.dll", CharSet = CharSet.Ansi,
       CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        public static extern bool IsWindowVisible(int handle);

        //[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        //public static extern int GetWindow(int hwnd, int uCmd);
        //[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        //public static extern int GetWindowThreadProcessId(int hwnd, ref int lpdwProcessId);


        [DllImport("user32.dll", EntryPoint = "GetWindowTextA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int GetWindowText(int hwnd, string lpString, int cch);

        [DllImport("user32.dll", EntryPoint = "GetWindowTextA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int GetWindowText(int hwnd, StringBuilder lpString, int cch);

        //[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        //public static extern bool GetWindowPlacement(int hwnd, ref WINDOWPLACEMENT lpwndpl);
        [DllImport("user32", EntryPoint = "GetWindowLongA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int GetWindowLong(int hwnd, ENUMSHOW nIndex);

        //[DllImport("user32", EntryPoint = "GetWindowLongA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        //public static extern int GetWindowLong(int hwnd, int nIndex);
        [DllImport("user32", EntryPoint = "GetWindowLongA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern int GetWindowLong(IntPtr hwnd, int nIndex);


        [DllImport("user32", EntryPoint = "SetWindowLong")]
        public static extern uint SetWindowLong(
        IntPtr hwnd,
        int nIndex,
        uint dwNewLong
        );

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool SetWindowPlacement(int hwnd, ref WINDOWPLACEMENT lpwndpl);
        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool SetWindowPos(int hwnd, int hWndInsertAfter,
            int x, int y, int cx, int cy, int uFlags);

        [DllImport("user32.dll", EntryPoint = "SetWindowTextA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern bool SetWindowText(int hwnd, string lpString);
 
        /// <summary>
        /// 该函数获得指定窗口所属的类的类名
        /// </summary>
        /// <param name="hWnd">窗口的句柄及间接给出的窗口所属的类</param>
        /// <param name="nIndex">指向接收窗口类名字符串的缓冲区的指针</param>
        /// <param name="dwNewLong">指定由参数lpClassName指示的缓冲区的字节数。如果类名字符串大于缓冲区的长度，则多出的部分被截断</param>
        /// <remarks>
        /// 速查：Windows NT：3.1以上版本：Windows：95以上版本；Windows CE1.0以上版本；
        /// 头文件：winuser.h
        /// 库文件：user32.lib;
        /// Unicode：在 Windows NT上实现为 Unicode和 ANSI两种版本。
        /// </remarks>
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder ClassName, int nMaxCount);

        /// <summary>
        /// 该函数获得指定窗口所属的类的类名
        /// </summary>
        /// <param name="hWnd">窗口的句柄及间接给出的窗口所属的类</param>
        /// <param name="nIndex">指向接收窗口类名字符串的缓冲区的指针</param>
        /// <param name="dwNewLong">指定由参数lpClassName指示的缓冲区的字节数。如果类名字符串大于缓冲区的长度，则多出的部分被截断</param>
        /// <remarks>
        /// 速查：Windows NT：3.1以上版本：Windows：95以上版本；Windows CE1.0以上版本；
        /// 头文件：winuser.h
        /// 库文件：user32.lib;
        /// Unicode：在 Windows NT上实现为 Unicode和 ANSI两种版本。
        /// </remarks>
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassName(int hWnd, StringBuilder ClassName, int nMaxCount);

        /// <summary>
        /// 函数功能：枚举一个父窗口的所有子窗口。
        //函数原型：
        //BOOL EnumChildWindows(HWND hWndParent,WNDENUMPROC lpEnumFunc, LPARAM lParam);
        //各个参数如下：
        //HWND hWndParent 父窗口句柄
        //WNDENUMPROC lpEnumFunc 回调函数的地址
        //LPARAM lParam 自定义的参数
        //其中CallBack是这样的一个委托：　　public delegate bool CallBack(int hwnd, int lParam);
        //注意：回调函数的返回值将会影响到这个API函数的行为。如果回调函数返回true，则枚举继续直到枚举完成；如果返回false，则将会中止枚举。
        /// </summary> 
        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(int hWndParent, CallBack lpfn, int lParam);
        public delegate bool CallBack(int hwnd, int lParam);

        /// <summary>
        /// 参数 意义 
        ///dwFlags Long，下表中标志之一或它们的组合 
        ///dx，dy Long，根据MOUSEEVENTF_ABSOLUTE标志，指定x，y方向的绝对位置或相对位置 
        ///cButtons Long，没有使用 
        ///dwExtraInfo Long，没有使用 
        ///dwFlags常数 意义 
        /// </summary> 
        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern int mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);
        public const int MOUSEEVENTF_MOVE = 0x0001;     // 移动鼠标 
        public const int MOUSEEVENTF_LEFTDOWN = 0x0002; //模拟鼠标左键按下 
        public const int MOUSEEVENTF_LEFTUP = 0x0004; //模拟鼠标左键抬起 
        public const int MOUSEEVENTF_RIGHTDOWN = 0x0008; //模拟鼠标右键按下 
        public const int MOUSEEVENTF_RIGHTUP = 0x0010; //模拟鼠标右键抬起 
        public const int MOUSEEVENTF_MIDDLEDOWN = 0x0020; //模拟鼠标中键按下 
        public const int MOUSEEVENTF_MIDDLEUP = 0x0040; //模拟鼠标中键抬起 
        public const int MOUSEEVENTF_ABSOLUTE = 0x8000; //标示是否采用绝对坐标 
        //public const int WM_MOUSEWHEEL = 0x020A;//标示模拟鼠标滚动 
        public const int WM_MOUSEWHEEL = 0x800;//标示模拟鼠标滚动 
        public const int WHEEL_DELTA = 120;

        public static void MouseClick(Point Point)
        {
            UnsafeNativeMethods.MouseMove(Point.X, Point.Y);
            System.Threading.Thread.Sleep(30);
            UnsafeNativeMethods.MouseClick(Point.X, Point.Y);
        }

        public static void MouseClick(int x, int y)
        {
            UnsafeNativeMethods.mouse_event(UnsafeNativeMethods.MOUSEEVENTF_LEFTDOWN | UnsafeNativeMethods.MOUSEEVENTF_LEFTUP, x, y, 0, 0);
        }
        public static void MouseMove(int x, int y)
        {
            UnsafeNativeMethods.SetCursorPos(x, y);
        }

        public static void MouseWheelUP(int x, int y)
        {
            UnsafeNativeMethods.mouse_event(UnsafeNativeMethods.WM_MOUSEWHEEL, x, y, UnsafeNativeMethods.WHEEL_DELTA, 0);
        }

        public static void MouseWheelDOWN(int x, int y)
        {
            UnsafeNativeMethods.mouse_event(UnsafeNativeMethods.WM_MOUSEWHEEL, x, y, UnsafeNativeMethods.WHEEL_DELTA * -1, 0);
        }

        [DllImport("user32")]
        public static extern int SetForegroundWindow(IntPtr hwnd);
        [DllImport("user32")]
        public static extern int SetForegroundWindow(int hwnd);

        [DllImport("user32.dll", EntryPoint = "IsWindow")]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "IsWindow")]
        public static extern bool IsWindow(int hWnd);
        #region User32.DLL
        //[DllImport("user32.dll")]
        //public static extern int GetDesktopWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetProcessWindowStation();
        //[DllImport("kernel32.dll")]
        //public static extern IntPtr GetCurrentThreadId();
        [DllImport("user32.dll")]
        public static extern IntPtr GetThreadDesktop(IntPtr dwThread);
        [DllImport("user32.dll")]
        public static extern IntPtr OpenWindowStation(string lpszWinSta, bool fInherit, int dwDesiredAccess);
        [DllImport("User32.dll")]
        public static extern IntPtr OpenDesktop(string lpsxDesktop, uint dwFlags, bool fInherit, uint dwDesiredAccess);
        [DllImport("user32.dll")]
        public static extern IntPtr CloseDesktop(IntPtr hDesktop);
        [DllImport("user32.dll")]
        public static extern IntPtr SetThreadDesktop(IntPtr hDesktop);
        [DllImport("user32.dll")]
        public static extern IntPtr SetProcessWindowStation(IntPtr hWinSta);
        [DllImport("user32.dll")]
        public static extern IntPtr CloseWindowStation(IntPtr hWinSta);
        #endregion
        #region Rpcrt4.dll
        [DllImport("rpcrt4.dll", SetLastError = true)]
        public static extern IntPtr RpcImpersonatClient(int rpc);
        [DllImport("rpcrt4.dll", SetLastError = true)]
        public static extern IntPtr RpcRevertToSelf();
        #endregion


        public const int FO_DELETE = 0x3;
        public const ushort FOF_NOCONFIRMATION = 0x10;
        public const ushort FOF_ALLOWUNDO = 0x40;

        [DllImport("shell32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int SHFileOperation([In, Out] _SHFILEOPSTRUCT str);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public class _SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            public UInt32 wFunc;
            public string pFrom;
            public string pTo;
            public UInt16 fFlags;
            public Int32 fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }


        public enum ENUMPLACEMENT
        {
            SW_ERASE = 4,
            SW_FORCEMINIMIZE = 11,
            SW_HIDE = 0,
            SW_INVALIDATE = 2,
            SW_MAX = 10,
            SW_MAXIMIZE = 3,
            SW_MINIMIZE = 6,
            SW_NORMAL = 1,
            SW_OTHERUNZOOM = 4,
            SW_OTHERZOOM = 2,
            SW_PARENTCLOSING = 1,
            SW_PARENTOPENING = 3,
            SW_RESTORE = 9,
            SW_SCROLLCHILDREN = 1,
            SW_SHOW = 5,
            SW_SHOWDEFAULT = 10,
            SW_SHOWMAXIMIZED = 3,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOWNORMAL = 1
        }

        public enum ENUMSHOW
        {
            GWL_STYLE = -16,
            hide = 1,
            normal = 2,
            show = 3,
            WS_DISABLED = 0x8000000,
            WS_VISIBLE = 0x10000000
        }

        public enum ENUMWINDPOSE
        {
            HWND_BOTTOM = 1,
            HWND_NOTOPMOST = -2,
            HWND_TOP = 0,
            HWND_TOPMOST = -1
        }

        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WINDOWPLACEMENT
        {
            public int Length;
            public int flags;
            public int showCmd;
            public POINTAPI ptMinPosition;
            public POINTAPI ptMaxPosition;
            public RECT rcNormalPosition;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINTAPI
        {
            public int X;
            public int Y;
        }


        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
 
        [StructLayout(LayoutKind.Sequential)]
        public struct COMPOSITIONFORM
        {
            public uint dwStyle;
            public Point ptCurrentPos;
            public RECT rcArea;
        }
        public const int CFS_DEFAULT = 0x0;
        public const int CFS_RECT = 0x1;
        public const int CFS_POINT = 0x2;
        public const int CFS_SCREEN = 0x4;
        public const int CFS_FORCE_POSITION = 0x20;
        public const int CFS_CANDIDATEPOS = 0x40;
        public const int CFS_EXCLUDE = 0x80;
        [Flags]
        internal enum FlagsSetWindowPos : uint
        {
            SWP_NOSIZE = 0x0001,
            SWP_NOMOVE = 0x0002,
            SWP_NOZORDER = 0x0004,
            SWP_NOREDRAW = 0x0008,
            SWP_NOACTIVATE = 0x0010,
            SWP_FRAMECHANGED = 0x0020,
            SWP_SHOWWINDOW = 0x0040,
            SWP_HIDEWINDOW = 0x0080,
            SWP_NOCOPYBITS = 0x0100,
            SWP_NOOWNERZORDER = 0x0200,
            SWP_NOSENDCHANGING = 0x0400,
            SWP_DRAWFRAME = 0x0020,
            SWP_NOREPOSITION = 0x0200,
            SWP_DEFERERASE = 0x2000,
            SWP_ASYNCWINDOWPOS = 0x4000
        }

        internal enum ShowWindowStyles : short
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }

        internal enum WindowStyles : uint
        {
            WS_OVERLAPPED = 0x00000000,
            WS_POPUP = 0x80000000,
            WS_CHILD = 0x40000000,
            WS_MINIMIZE = 0x20000000,
            WS_VISIBLE = 0x10000000,
            WS_DISABLED = 0x08000000,
            WS_CLIPSIBLINGS = 0x04000000,
            WS_CLIPCHILDREN = 0x02000000,
            WS_MAXIMIZE = 0x01000000,
            WS_CAPTION = 0x00C00000,
            WS_BORDER = 0x00800000,
            WS_DLGFRAME = 0x00400000,
            WS_VSCROLL = 0x00200000,
            WS_HSCROLL = 0x00100000,
            WS_SYSMENU = 0x00080000,
            WS_THICKFRAME = 0x00040000,
            WS_GROUP = 0x00020000,
            WS_TABSTOP = 0x00010000,
            WS_MINIMIZEBOX = 0x00020000,
            WS_MAXIMIZEBOX = 0x00010000,
            WS_TILED = 0x00000000,
            WS_ICONIC = 0x20000000,
            WS_SIZEBOX = 0x00040000,
            WS_POPUPWINDOW = 0x80880000,
            WS_OVERLAPPEDWINDOW = 0x00CF0000,
            WS_TILEDWINDOW = 0x00CF0000,
            WS_CHILDWINDOW = 0x40000000
        }

        internal enum WindowExStyles
        {
            WS_EX_DLGMODALFRAME = 0x00000001,
            WS_EX_NOPARENTNOTIFY = 0x00000004,
            WS_EX_TOPMOST = 0x00000008,
            WS_EX_ACCEPTFILES = 0x00000010,
            WS_EX_TRANSPARENT = 0x00000020,
            WS_EX_MDICHILD = 0x00000040,
            WS_EX_TOOLWINDOW = 0x00000080,
            WS_EX_WINDOWEDGE = 0x00000100,
            WS_EX_CLIENTEDGE = 0x00000200,
            WS_EX_CONTEXTHELP = 0x00000400,
            WS_EX_RIGHT = 0x00001000,
            WS_EX_LEFT = 0x00000000,
            WS_EX_RTLREADING = 0x00002000,
            WS_EX_LTRREADING = 0x00000000,
            WS_EX_LEFTSCROLLBAR = 0x00004000,
            WS_EX_RIGHTSCROLLBAR = 0x00000000,
            WS_EX_CONTROLPARENT = 0x00010000,
            WS_EX_STATICEDGE = 0x00020000,
            WS_EX_APPWINDOW = 0x00040000,
            WS_EX_OVERLAPPEDWINDOW = 0x00000300,
            WS_EX_PALETTEWINDOW = 0x00000188,
            WS_EX_LAYERED = 0x00080000
        }

        internal enum Msgs
        {
            WM_NULL = 0x0000,
            WM_CREATE = 0x0001,
            WM_DESTROY = 0x0002,
            WM_MOVE = 0x0003,
            WM_SIZE = 0x0005,
            WM_ACTIVATE = 0x0006,
            WM_SETFOCUS = 0x0007,
            WM_KILLFOCUS = 0x0008,
            WM_ENABLE = 0x000A,
            WM_SETREDRAW = 0x000B,
            WM_SETTEXT = 0x000C,
            WM_GETTEXT = 0x000D,
            WM_GETTEXTLENGTH = 0x000E,
            WM_PAINT = 0x000F,
            WM_CLOSE = 0x0010,
            WM_QUERYENDSESSION = 0x0011,
            WM_QUIT = 0x0012,
            WM_QUERYOPEN = 0x0013,
            WM_ERASEBKGND = 0x0014,
            WM_SYSCOLORCHANGE = 0x0015,
            WM_ENDSESSION = 0x0016,
            WM_SHOWWINDOW = 0x0018,
            WM_WININICHANGE = 0x001A,
            WM_SETTINGCHANGE = 0x001A,
            WM_DEVMODECHANGE = 0x001B,
            WM_ACTIVATEAPP = 0x001C,
            WM_FONTCHANGE = 0x001D,
            WM_TIMECHANGE = 0x001E,
            WM_CANCELMODE = 0x001F,
            WM_SETCURSOR = 0x0020,
            WM_MOUSEACTIVATE = 0x0021,
            WM_CHILDACTIVATE = 0x0022,
            WM_QUEUESYNC = 0x0023,
            WM_GETMINMAXINFO = 0x0024,
            WM_PAINTICON = 0x0026,
            WM_ICONERASEBKGND = 0x0027,
            WM_NEXTDLGCTL = 0x0028,
            WM_SPOOLERSTATUS = 0x002A,
            WM_DRAWITEM = 0x002B,
            WM_MEASUREITEM = 0x002C,
            WM_DELETEITEM = 0x002D,
            WM_VKEYTOITEM = 0x002E,
            WM_CHARTOITEM = 0x002F,
            WM_SETFONT = 0x0030,
            WM_GETFONT = 0x0031,
            WM_SETHOTKEY = 0x0032,
            WM_GETHOTKEY = 0x0033,
            WM_QUERYDRAGICON = 0x0037,
            WM_COMPAREITEM = 0x0039,
            WM_GETOBJECT = 0x003D,
            WM_COMPACTING = 0x0041,
            WM_COMMNOTIFY = 0x0044,
            WM_WINDOWPOSCHANGING = 0x0046,
            WM_WINDOWPOSCHANGED = 0x0047,
            WM_POWER = 0x0048,
            WM_COPYDATA = 0x004A,
            WM_CANCELJOURNAL = 0x004B,
            WM_NOTIFY = 0x004E,
            WM_INPUTLANGCHANGEREQUEST = 0x0050,
            WM_INPUTLANGCHANGE = 0x0051,
            WM_TCARD = 0x0052,
            WM_HELP = 0x0053,
            WM_USERCHANGED = 0x0054,
            WM_NOTIFYFORMAT = 0x0055,
            WM_CONTEXTMENU = 0x007B,
            WM_STYLECHANGING = 0x007C,
            WM_STYLECHANGED = 0x007D,
            WM_DISPLAYCHANGE = 0x007E,
            WM_GETICON = 0x007F,
            WM_SETICON = 0x0080,
            WM_NCCREATE = 0x0081,
            WM_NCDESTROY = 0x0082,
            WM_NCCALCSIZE = 0x0083,
            WM_NCHITTEST = 0x0084,
            WM_NCPAINT = 0x0085,
            WM_NCACTIVATE = 0x0086,
            WM_GETDLGCODE = 0x0087,
            WM_SYNCPAINT = 0x0088,
            WM_NCMOUSEMOVE = 0x00A0,
            WM_NCLBUTTONDOWN = 0x00A1,
            WM_NCLBUTTONUP = 0x00A2,
            WM_NCLBUTTONDBLCLK = 0x00A3,
            WM_NCRBUTTONDOWN = 0x00A4,
            WM_NCRBUTTONUP = 0x00A5,
            WM_NCRBUTTONDBLCLK = 0x00A6,
            WM_NCMBUTTONDOWN = 0x00A7,
            WM_NCMBUTTONUP = 0x00A8,
            WM_NCMBUTTONDBLCLK = 0x00A9,
            WM_KEYDOWN = 0x0100,
            WM_KEYUP = 0x0101,
            WM_CHAR = 0x0102,
            WM_DEADCHAR = 0x0103,
            WM_SYSKEYDOWN = 0x0104,
            WM_SYSKEYUP = 0x0105,
            WM_SYSCHAR = 0x0106,
            WM_SYSDEADCHAR = 0x0107,
            WM_KEYLAST = 0x0108,
            WM_IME_STARTCOMPOSITION = 0x010D,
            WM_IME_ENDCOMPOSITION = 0x010E,
            WM_IME_COMPOSITION = 0x010F,
            WM_IME_KEYLAST = 0x010F,
            WM_INITDIALOG = 0x0110,
            WM_COMMAND = 0x0111,
            WM_SYSCOMMAND = 0x0112,
            WM_TIMER = 0x0113,
            WM_HSCROLL = 0x0114,
            WM_VSCROLL = 0x0115,
            WM_INITMENU = 0x0116,
            WM_INITMENUPOPUP = 0x0117,
            WM_MENUSELECT = 0x011F,
            WM_MENUCHAR = 0x0120,
            WM_ENTERIDLE = 0x0121,
            WM_MENURBUTTONUP = 0x0122,
            WM_MENUDRAG = 0x0123,
            WM_MENUGETOBJECT = 0x0124,
            WM_UNINITMENUPOPUP = 0x0125,
            WM_MENUCOMMAND = 0x0126,
            WM_CTLCOLORMSGBOX = 0x0132,
            WM_CTLCOLOREDIT = 0x0133,
            WM_CTLCOLORLISTBOX = 0x0134,
            WM_CTLCOLORBTN = 0x0135,
            WM_CTLCOLORDLG = 0x0136,
            WM_CTLCOLORSCROLLBAR = 0x0137,
            WM_CTLCOLORSTATIC = 0x0138,
            WM_MOUSEMOVE = 0x0200,
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_LBUTTONDBLCLK = 0x0203,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205,
            WM_RBUTTONDBLCLK = 0x0206,
            WM_MBUTTONDOWN = 0x0207,
            WM_MBUTTONUP = 0x0208,
            WM_MBUTTONDBLCLK = 0x0209,
            WM_MOUSEWHEEL = 0x020A,
            WM_PARENTNOTIFY = 0x0210,
            WM_ENTERMENULOOP = 0x0211,
            WM_EXITMENULOOP = 0x0212,
            WM_NEXTMENU = 0x0213,
            WM_SIZING = 0x0214,
            WM_CAPTURECHANGED = 0x0215,
            WM_MOVING = 0x0216,
            WM_DEVICECHANGE = 0x0219,
            WM_MDICREATE = 0x0220,
            WM_MDIDESTROY = 0x0221,
            WM_MDIACTIVATE = 0x0222,
            WM_MDIRESTORE = 0x0223,
            WM_MDINEXT = 0x0224,
            WM_MDIMAXIMIZE = 0x0225,
            WM_MDITILE = 0x0226,
            WM_MDICASCADE = 0x0227,
            WM_MDIICONARRANGE = 0x0228,
            WM_MDIGETACTIVE = 0x0229,
            WM_MDISETMENU = 0x0230,
            WM_ENTERSIZEMOVE = 0x0231,
            WM_EXITSIZEMOVE = 0x0232,
            WM_DROPFILES = 0x0233,
            WM_MDIREFRESHMENU = 0x0234,
            WM_IME_SETCONTEXT = 0x0281,
            WM_IME_NOTIFY = 0x0282,
            WM_IME_CONTROL = 0x0283,
            WM_IME_COMPOSITIONFULL = 0x0284,
            WM_IME_SELECT = 0x0285,
            WM_IME_CHAR = 0x0286,
            WM_IME_REQUEST = 0x0288,
            WM_IME_KEYDOWN = 0x0290,
            WM_IME_KEYUP = 0x0291,
            WM_MOUSEHOVER = 0x02A1,
            WM_MOUSELEAVE = 0x02A3,
            WM_CUT = 0x0300,
            WM_COPY = 0x0301,
            WM_PASTE = 0x0302,
            WM_CLEAR = 0x0303,
            WM_UNDO = 0x0304,
            WM_RENDERFORMAT = 0x0305,
            WM_RENDERALLFORMATS = 0x0306,
            WM_DESTROYCLIPBOARD = 0x0307,
            WM_DRAWCLIPBOARD = 0x0308,
            WM_PAINTCLIPBOARD = 0x0309,
            WM_VSCROLLCLIPBOARD = 0x030A,
            WM_SIZECLIPBOARD = 0x030B,
            WM_ASKCBFORMATNAME = 0x030C,
            WM_CHANGECBCHAIN = 0x030D,
            WM_HSCROLLCLIPBOARD = 0x030E,
            WM_QUERYNEWPALETTE = 0x030F,
            WM_PALETTEISCHANGING = 0x0310,
            WM_PALETTECHANGED = 0x0311,
            WM_HOTKEY = 0x0312,
            WM_PRINT = 0x0317,
            WM_PRINTCLIENT = 0x0318,
            WM_HANDHELDFIRST = 0x0358,
            WM_HANDHELDLAST = 0x035F,
            WM_AFXFIRST = 0x0360,
            WM_AFXLAST = 0x037F,
            WM_PENWINFIRST = 0x0380,
            WM_PENWINLAST = 0x038F,
            WM_APP = 0x8000,
            WM_USER = 0x0400
        }

        internal enum HitTest
        {
            HTERROR = -2,
            HTTRANSPARENT = -1,
            HTNOWHERE = 0,
            HTCLIENT = 1,
            HTCAPTION = 2,
            HTSYSMENU = 3,
            HTGROWBOX = 4,
            HTSIZE = 4,
            HTMENU = 5,
            HTHSCROLL = 6,
            HTVSCROLL = 7,
            HTMINBUTTON = 8,
            HTMAXBUTTON = 9,
            HTLEFT = 10,
            HTRIGHT = 11,
            HTTOP = 12,
            HTTOPLEFT = 13,
            HTTOPRIGHT = 14,
            HTBOTTOM = 15,
            HTBOTTOMLEFT = 16,
            HTBOTTOMRIGHT = 17,
            HTBORDER = 18,
            HTREDUCE = 8,
            HTZOOM = 9,
            HTSIZEFIRST = 10,
            HTSIZELAST = 17,
            HTOBJECT = 19,
            HTCLOSE = 20,
            HTHELP = 21
        }

        internal enum ScrollBars : uint
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        internal enum GetWindowLongIndex : int
        {
            GWL_STYLE = -16,
            GWL_EXSTYLE = -20
        }

        // Hook Types  
        internal enum HookType : int
        {
            WH_JOURNALRECORD = 0,
            WH_JOURNALPLAYBACK = 1,
            WH_KEYBOARD = 2,
            WH_GETMESSAGE = 3,
            WH_CALLWNDPROC = 4,
            WH_CBT = 5,
            WH_SYSMSGFILTER = 6,
            WH_MOUSE = 7,
            WH_HARDWARE = 8,
            WH_DEBUG = 9,
            WH_SHELL = 10,
            WH_FOREGROUNDIDLE = 11,
            WH_CALLWNDPROCRET = 12,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14
        }
    }
}
