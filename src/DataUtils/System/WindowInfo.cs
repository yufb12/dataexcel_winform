using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

using System.Text;
using Feng.Utils;
using Feng.DataUtlis;
namespace Feng.Forms
{
    public class WindowInfo
    {
        public List<WindowInfo> WindowsList = new List<WindowInfo>();
        public void SetWindowCaption(int hwnd, string lpString)
        {
            UnsafeNativeMethods.SetWindowText(hwnd, lpString);
        }
        public WindowInfo()
        { 
        } 
        public string Title { get; set; }
        public string ProcessName { get; set; }
        public int ProcessID { get; set; }
        public string ProcessFile { get; set; }
        public IntPtr MainHandle { get; set; }
        public bool ProcessResponding { get; set; }
        public bool Visible { get;set; }
        public int Handler { get; set; }
        public string ClassName { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Rectangle Rect
        {
            get
            {
                return new Rectangle(this.Left, this.Top, this.Width, this.Height);
        } }

        public bool IsWindows()
        {
            return UnsafeNativeMethods.IsWindow(this.Handler);
        }
 
        public List<WindowInfo> GetProcessMainWindowInfo()
        {
            System.Diagnostics.Process pro = null;
            List<WindowInfo> list = new List<WindowInfo>();
            foreach (Process pro_loopVariable in System.Diagnostics.Process.GetProcesses())
            {
                pro = pro_loopVariable;
                if (pro.ProcessName.ToLower() == "system" | pro.Id == 0)
                {
                    break; // TODO: might not be correct. Was : Exit For
                }
                if (!string.IsNullOrEmpty(pro.MainWindowTitle.Trim()))
                {
                    WindowInfo li = new WindowInfo();
                    li.Title = (pro.MainWindowTitle);
                    li.ProcessID = pro.Id;
                    li.ProcessResponding = pro.Responding;

                    li.MainHandle = (pro.MainWindowHandle);
                    li.ProcessName = (pro.ProcessName);
                    li.ProcessFile = (pro.MainModule.FileName);
                    list.Add(li); 
                }
            }
            return list;
        }
        public static List<WindowInfo> GetChildWindows(int handle)
        { 
            int res = Feng.Utils.UnsafeNativeMethods.EnumChildWindows(handle, CallBackEnumWindows, 0);
            return listchild;
        }
        public void CloseWindow()
        {
            Feng.Utils.UnsafeNativeMethods.PostMessage(this.MainHandle, UnsafeNativeMethods.WM_QUIT, 0, 0);
        }

        public static List<WindowInfo> GetWindowInfo(int handle,
            string classname, 
            int minwidth, 
            int minheight,
            string appname)
        {
            listchild.Clear();
            _classname = classname;
            _minwidth = minwidth;
            _minheight = minheight;
            _appname = appname;
            UnsafeNativeMethods.GetDesktopWindow();
            IntPtr _WindowsStation = UnsafeNativeMethods.GetProcessWindowStation();
            IntPtr _ThreadID = new IntPtr(UnsafeNativeMethods.GetCurrentThreadId());
            IntPtr _DeskTop = UnsafeNativeMethods.GetThreadDesktop(_ThreadID);
            IntPtr _HwinstaUser = UnsafeNativeMethods.OpenWindowStation("WinSta0", false, 33554432);
            if (_HwinstaUser == IntPtr.Zero)
            {
                UnsafeNativeMethods.RpcRevertToSelf();
               
            }
            UnsafeNativeMethods.SetProcessWindowStation(_HwinstaUser);
            IntPtr _HdeskUser = UnsafeNativeMethods.OpenDesktop("Default", 0, false, 33554432);
            UnsafeNativeMethods.RpcRevertToSelf();
            if (_HdeskUser == IntPtr.Zero)
            {
                UnsafeNativeMethods.SetProcessWindowStation(_WindowsStation);
                UnsafeNativeMethods.CloseWindowStation(_HwinstaUser);
                
            }
            UnsafeNativeMethods.SetThreadDesktop(_HdeskUser);
            int res = Feng.Utils.UnsafeNativeMethods.EnumChildWindows(handle, EnumWindowInfoCallBackByClassName, 0);
            IntPtr _GuiThreadId = _ThreadID;
            _GuiThreadId = IntPtr.Zero;
            UnsafeNativeMethods.SetThreadDesktop(_DeskTop);
            UnsafeNativeMethods.SetProcessWindowStation(_WindowsStation);
            UnsafeNativeMethods.CloseDesktop(_HdeskUser);
            UnsafeNativeMethods.CloseWindowStation(_HwinstaUser);
            return listchild;
        }

        public static List<WindowInfo> GetWindowInfo(int handle,
          string classname,
            string title,
          int minwidth,
          int minheight,
          string appname)
        {
            listchild.Clear();
            _classname = classname;
            _minwidth = minwidth;
            _minheight = minheight;
            _appname = appname;
            _title = title;
            UnsafeNativeMethods.GetDesktopWindow();
            IntPtr _WindowsStation = UnsafeNativeMethods.GetProcessWindowStation();
            IntPtr _ThreadID = new IntPtr(UnsafeNativeMethods.GetCurrentThreadId());
            IntPtr _DeskTop = UnsafeNativeMethods.GetThreadDesktop(_ThreadID);
            IntPtr _HwinstaUser = UnsafeNativeMethods.OpenWindowStation("WinSta0", false, 33554432);
            if (_HwinstaUser == IntPtr.Zero)
            {
                UnsafeNativeMethods.RpcRevertToSelf();

            }
            UnsafeNativeMethods.SetProcessWindowStation(_HwinstaUser);
            IntPtr _HdeskUser = UnsafeNativeMethods.OpenDesktop("Default", 0, false, 33554432);
            UnsafeNativeMethods.RpcRevertToSelf();
            if (_HdeskUser == IntPtr.Zero)
            {
                UnsafeNativeMethods.SetProcessWindowStation(_WindowsStation);
                UnsafeNativeMethods.CloseWindowStation(_HwinstaUser);

            }
            UnsafeNativeMethods.SetThreadDesktop(_HdeskUser);
            int res = Feng.Utils.UnsafeNativeMethods.EnumChildWindows(handle, EnumWindowInfoCallBackByClassNameAndTitle, 0);
            IntPtr _GuiThreadId = _ThreadID;
            _GuiThreadId = IntPtr.Zero;
            UnsafeNativeMethods.SetThreadDesktop(_DeskTop);
            UnsafeNativeMethods.SetProcessWindowStation(_WindowsStation);
            UnsafeNativeMethods.CloseDesktop(_HdeskUser);
            UnsafeNativeMethods.CloseWindowStation(_HwinstaUser);
            return listchild;
        }
        private static string _classname = string.Empty;
        private static int _minwidth; 
        private static int _minheight;
        private static string _appname = string.Empty;
        private static string _title = string.Empty;
        public static WindowInfo CreateWindowInfo(string titletemp, System.Diagnostics.Process pro, int hwnd, string classname, Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT wp)
        {
            WindowInfo li = new WindowInfo();
            li.Title = titletemp;
            li.ProcessID = pro.Id;
            li.ProcessResponding = pro.Responding;
            li.Handler = hwnd;
            li.MainHandle = (pro.MainWindowHandle);
            li.ProcessName = (pro.ProcessName);
            li.ClassName = classname;
            li.Width = wp.rcNormalPosition.Right - wp.rcNormalPosition.Left;
            li.Height = wp.rcNormalPosition.Bottom - wp.rcNormalPosition.Top;
            li.Left = wp.rcNormalPosition.Left;
            li.Top = wp.rcNormalPosition.Top;
            try
            {
                li.ProcessFile = (pro.MainModule.FileName);
            }
            catch (Exception)
            {

            }
            return li;
        }
        public string GetTitle()
        {
            StringBuilder title = new StringBuilder(255);
            Feng.Utils.UnsafeNativeMethods.GetWindowText(this.Handler, title, 255);
            return title.ToString();
        }
        private static bool EnumWindowInfoCallBackByClassName(int hwnd, int lParam)
        {
            StringBuilder title = new StringBuilder(255);
            int ThreadIDR = 0;
            int ProcessIDR = 0;
            int titleall = 0;
            string titletemp = null;
            Feng.Utils.UnsafeNativeMethods.ENUMSHOW lstyle = 0;

            titleall = Feng.Utils.UnsafeNativeMethods.GetWindowText(hwnd, title, 255);
            string classname = string.Empty;
            StringBuilder sbclassname = new StringBuilder(256);
            int res = Feng.Utils.UnsafeNativeMethods.GetClassName(hwnd, sbclassname, 128);
            classname = sbclassname.ToString().Trim('\0');
            if (classname == _classname)
            {
                titletemp = title.ToString();
                Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT wp = default(Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT);

                if (Feng.Utils.UnsafeNativeMethods.GetWindowPlacement(hwnd, ref wp))
                {
                    int width=wp.rcNormalPosition.Right - wp.rcNormalPosition.Left;
                    int height=wp.rcNormalPosition .Bottom -wp.rcNormalPosition.Top;
                    if ((width > _minwidth)
                        && (height > _minheight))
                    {
                        ThreadIDR = Feng.Utils.UnsafeNativeMethods.GetWindowThreadProcessId(hwnd, ref ProcessIDR);
                        lstyle = (Feng.Utils.UnsafeNativeMethods.ENUMSHOW)Feng.Utils.UnsafeNativeMethods.GetWindowLong(hwnd, Feng.Utils.UnsafeNativeMethods.ENUMSHOW.GWL_STYLE);
                        Process pro = Process.GetProcessById(ProcessIDR);
                        if (pro.ProcessName == _appname || string.IsNullOrEmpty(_appname))
                        {
                            WindowInfo li = CreateWindowInfo(titletemp, pro, hwnd, classname, wp);
                            listchild.Add(li);
                        }
                    }
                }
            }
            return true;
        }
        private static bool EnumWindowInfoCallBackByClassNameAndTitle(int hwnd, int lParam)
        {
            StringBuilder title = new StringBuilder(255);
            int ThreadIDR = 0;
            int ProcessIDR = 0;
            int titleall = 0;
            string titletemp = null;
            Feng.Utils.UnsafeNativeMethods.ENUMSHOW lstyle = 0;

            titleall = Feng.Utils.UnsafeNativeMethods.GetWindowText(hwnd, title, 255);
            string classname = string.Empty;
            StringBuilder sbclassname = new StringBuilder(256);
            int res = Feng.Utils.UnsafeNativeMethods.GetClassName(hwnd, sbclassname, 128);
            classname = sbclassname.ToString().Trim('\0');
            if (classname == _classname && title.ToString() == _title)
            {
                titletemp = title.ToString();
                Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT wp = default(Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT);

                if (Feng.Utils.UnsafeNativeMethods.GetWindowPlacement(hwnd, ref wp))
                {
                    int width = wp.rcNormalPosition.Right - wp.rcNormalPosition.Left;
                    int height = wp.rcNormalPosition.Bottom - wp.rcNormalPosition.Top;
                    if ((width > _minwidth)
                        && (height > _minheight))
                    {
                        ThreadIDR = Feng.Utils.UnsafeNativeMethods.GetWindowThreadProcessId(hwnd, ref ProcessIDR);
                        lstyle = (Feng.Utils.UnsafeNativeMethods.ENUMSHOW)Feng.Utils.UnsafeNativeMethods.GetWindowLong(hwnd, Feng.Utils.UnsafeNativeMethods.ENUMSHOW.GWL_STYLE);
                        Process pro = Process.GetProcessById(ProcessIDR);
                        if (pro.ProcessName == _appname || string.IsNullOrEmpty(_appname))
                        {
                            WindowInfo li = CreateWindowInfo(titletemp, pro, hwnd, classname, wp);
                            listchild.Add(li);
                        }
                    }
                }
            }
            return true;
        }

        public static Feng.Utils.UnsafeNativeMethods.CallBack CallBackEnumWindows = new UnsafeNativeMethods.CallBack(EnumWindowInfoCallBack);
        private static List<WindowInfo> listchild = new List<WindowInfo>();

        public static bool EnumWindowInfoCallBack(int hwnd, int lParam)
        {
            StringBuilder title = new StringBuilder(255);
            int ThreadIDR = 0;
            int ProcessIDR = 0; 
            string titletemp = null;
            Feng.Utils.UnsafeNativeMethods.ENUMSHOW lstyle = 0;
            Feng.Utils.UnsafeNativeMethods.GetWindowText(hwnd, title, 255);
 
            string classname = string.Empty;
            StringBuilder sbclassname = new StringBuilder(256);
            int res = Feng.Utils.UnsafeNativeMethods.GetClassName(hwnd, sbclassname, 128);
            classname = sbclassname.ToString();
            titletemp = title.ToString();
            Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT wp = default(Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT);

            if (Feng.Utils.UnsafeNativeMethods.GetWindowPlacement(hwnd, ref wp) == true)
            {
                if ((wp.rcNormalPosition.Right - wp.rcNormalPosition.Left > 4))
                {
                    ThreadIDR = Feng.Utils.UnsafeNativeMethods.GetWindowThreadProcessId(hwnd, ref ProcessIDR);
                    lstyle = (Feng.Utils.UnsafeNativeMethods.ENUMSHOW)Feng.Utils.UnsafeNativeMethods.GetWindowLong(hwnd, Feng.Utils.UnsafeNativeMethods.ENUMSHOW.GWL_STYLE);
                    Process pro = Process.GetProcessById(ProcessIDR);
                    WindowInfo li = CreateWindowInfo(titletemp, pro, hwnd, classname, wp);
                    listchild.Add(li);
                }
            }
            return true;
        }
        /// <summary>
        /// 开启窗体 使用后用 System.Windows.Forms.Application.Run(_Form);来启动窗体
        /// </summary>
        /// <param name="s">日志</param>
        public static void ServiceForm()
        {
            try
            {
                UnsafeNativeMethods.GetDesktopWindow();
                IntPtr _WindowsStation = UnsafeNativeMethods.GetProcessWindowStation();
                IntPtr _ThreadID = new IntPtr(UnsafeNativeMethods.GetCurrentThreadId());
                IntPtr _DeskTop = UnsafeNativeMethods.GetThreadDesktop(_ThreadID);
                IntPtr _HwinstaUser = UnsafeNativeMethods.OpenWindowStation("WinSta0", false, 33554432);
                if (_HwinstaUser == IntPtr.Zero)
                {
                    UnsafeNativeMethods.RpcRevertToSelf();
                    return;
                }
                UnsafeNativeMethods.SetProcessWindowStation(_HwinstaUser);
                IntPtr _HdeskUser = UnsafeNativeMethods.OpenDesktop("Default", 0, false, 33554432);
                UnsafeNativeMethods.RpcRevertToSelf();
                if (_HdeskUser == IntPtr.Zero)
                {
                    UnsafeNativeMethods.SetProcessWindowStation(_WindowsStation);
                    UnsafeNativeMethods.CloseWindowStation(_HwinstaUser);
                    return;
                }
                UnsafeNativeMethods.SetThreadDesktop(_HdeskUser);
                IntPtr _GuiThreadId = _ThreadID;
                _GuiThreadId = IntPtr.Zero;
                UnsafeNativeMethods.SetThreadDesktop(_DeskTop);
                UnsafeNativeMethods.SetProcessWindowStation(_WindowsStation);
                UnsafeNativeMethods.CloseDesktop(_HdeskUser);
                UnsafeNativeMethods.CloseWindowStation(_HwinstaUser);
            }
            catch (Exception  )
            { 
            }
        }

        public static List<WindowInfo> GetActiveAllWindows(int desktopHandle)
        {
             
            int lnghand = 0;
            StringBuilder title = new StringBuilder(255);
            int ThreadIDR = 0;
            int ProcessIDR = 0;
            int titleall = 0;
            string titletemp = null;
            Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT wp = default(Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT);
 
            Feng.Utils.UnsafeNativeMethods.ENUMSHOW lstyle = 0;
            List<WindowInfo> list = new List<WindowInfo>();
            desktopHandle = Feng.Utils.UnsafeNativeMethods.GetDesktopWindow();
            lnghand = Feng.Utils.UnsafeNativeMethods.GetWindow(desktopHandle, UnsafeNativeMethods.GW_CHILD);
            wp.Length = Marshal.SizeOf(wp);
            List<int> listptr = new List<int>();
            while (lnghand != 0)
            {
                lnghand = Feng.Utils.UnsafeNativeMethods.GetWindow(lnghand, UnsafeNativeMethods.GW_HWNDNEXT);
                titleall = Feng.Utils.UnsafeNativeMethods.GetWindowText(lnghand, title, 255);
                string classname = string.Empty;
                StringBuilder sbclassname = new StringBuilder(256);
                int res = Feng.Utils.UnsafeNativeMethods.GetClassName(lnghand, sbclassname, 128);
                classname = sbclassname.ToString();
                titletemp = title.ToString();
                if (listptr.Contains(lnghand))
                {
                    continue;
                }
                listptr.Add(lnghand);
                if (Feng.Utils.UnsafeNativeMethods.GetWindowPlacement(lnghand, ref wp) == true)
                {
                    if ((wp.rcNormalPosition.Right - wp.rcNormalPosition.Left > 4))
                    {
                        ThreadIDR = Feng.Utils.UnsafeNativeMethods.GetWindowThreadProcessId(lnghand, ref ProcessIDR);
                        lstyle = (Feng.Utils.UnsafeNativeMethods.ENUMSHOW)Feng.Utils.UnsafeNativeMethods.GetWindowLong(lnghand, Feng.Utils.UnsafeNativeMethods.ENUMSHOW.GWL_STYLE);
                        Process pro = Process.GetProcessById(ProcessIDR);
                        WindowInfo li = CreateWindowInfo(titletemp, pro, lnghand, classname, wp);
                        list.Add(li); 
                    } 
                }
            }
            return list;
        }
        public static string ClearNullstr(string str)
        {
            str = str.Trim('\0');
            return str;
        }
        public bool SetWindowPlacementmenu(int lhandle, Feng.Utils.UnsafeNativeMethods.ENUMPLACEMENT nCmdShow)
        {
            Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT windowplacement  = default(Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT);
            windowplacement.Length = Marshal.SizeOf(windowplacement);
            if (Feng.Utils.UnsafeNativeMethods.GetWindowPlacement(lhandle, ref windowplacement) == false)
            {
                return false;
            }
            else
            {
                var _with6 = windowplacement;
                _with6.showCmd = (int)nCmdShow;
            }
            if (Feng.Utils.UnsafeNativeMethods.SetWindowPlacement(lhandle, ref windowplacement) == false)
            {
                return false;
            }
            else
            {
                return true;
            } 
        }
        public bool SetWindowPosmenu(int lhandle, Feng.Utils.UnsafeNativeMethods.ENUMWINDPOSE PostSet)
        {
            Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT WINDOWPLACEMENT = default(Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT);
            WINDOWPLACEMENT.Length = Marshal.SizeOf(WINDOWPLACEMENT);
            if (Feng.Utils.UnsafeNativeMethods.GetWindowPlacement(lhandle, ref WINDOWPLACEMENT) == false)
            {
                return false;
            }
            else
            {
            }
            if (Feng.Utils.UnsafeNativeMethods.SetWindowPos(lhandle, (int)PostSet, WINDOWPLACEMENT.rcNormalPosition.Left, WINDOWPLACEMENT.rcNormalPosition.Top, WINDOWPLACEMENT.rcNormalPosition.Right - WINDOWPLACEMENT.rcNormalPosition.Left, WINDOWPLACEMENT.rcNormalPosition.Bottom - WINDOWPLACEMENT.rcNormalPosition.Top, WINDOWPLACEMENT.flags) == false)
            {
                return false;
            }
            else
            {
                return true;
            } 

        }
        public bool SendMessageToCloseWindow(int ProcID)
        {
            Process pro = null;
            pro = System.Diagnostics.Process.GetProcessById(ProcID);
            if (pro.CloseMainWindow() == true)
            {
                return true;
            }
            else
            {
                return false;
            } 
        }
        public bool KillProcess(int ProcID)
        {
            Process pro = null;
            Process.EnterDebugMode();
            pro = System.Diagnostics.Process.GetProcessById(ProcID);
            pro.Kill();
            Process.LeaveDebugMode();
            return false;
        }
        public override string ToString()
        {
            return string.Format("Title={0},ProcessName={1},ProcessID={2},Handler={3},ClassName={4},Rect={5},ProcessFile={6}",
                this.Title, this.ProcessName, this.ProcessID, this.Handler, this.ClassName, this.Rect, this.ProcessFile);
        }
        public bool SetPositionAndTopmostShow(Point pt)
        {
            if (SetWindowPosmenu(this.Handler, UnsafeNativeMethods.ENUMWINDPOSE.HWND_TOP))
            {
                if (SetPosition(pt))
                {
                    return true;
                }
            }
            return false;
        }
        public bool SetPosition(Point pt)
        {
            Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT windowplacement = default(Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT);
            windowplacement.Length = Marshal.SizeOf(windowplacement);

            if (!Feng.Utils.UnsafeNativeMethods.GetWindowPlacement(this.Handler, ref windowplacement))
            {
                return false;
            }
            windowplacement.rcNormalPosition.Left = pt.X;
            windowplacement.rcNormalPosition.Top = pt.Y;
            if (Feng.Utils.UnsafeNativeMethods.SetWindowPlacement(this.Handler, ref windowplacement))
            {
                return true;
            } 
            return false;
        }
        public bool Focus()
        {
            return UnsafeNativeMethods.SetForegroundWindow(this.Handler) > 0;
        }

        public static bool GenuineDataProjectValidation()
        {
            string keyname = @"Software\Booxin\DataProject\" + Product.AssemblyDescription;
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(keyname);
            if (key != null)
            {
                object CopyRightName = null;
                CopyRightName = key.GetValue("CName", CopyRightName);
                if (CopyRightName != null)
                {
                    string strCopyRightName = CopyRightName.ToString();
                    object CopyRightKey = null;
                    CopyRightKey = key.GetValue("CKey", CopyRightKey);//CopyRightKey
                    if (CopyRightKey != null)
                    {
                        string strCopyRightKey = CopyRightKey.ToString();
                        string keyvalue = Feng.IO.DEncrypt.GetMd5Hash(CopyRightName + "DataProject");
                        if (strCopyRightKey == keyvalue)
                        {
                            return true;
                        }
                    }
                }

            }
            return false;
        }
    }
}