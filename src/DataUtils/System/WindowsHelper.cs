using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing; 
using System.Windows.Forms;

namespace Feng.Utils
{
    public class WindowsInfo
    {
        public int ProcessID { get; set; }
        public string ProcessName { get; set; }
        public int ThreadID { get; set; }
        public int WindowsID { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public int Style { get; set; }

    }
    public class WindowsHelper
    {
        public static List<WindowsInfo> GetAllWindows()
        {
            List<WindowsInfo> list = new List<WindowsInfo>();
            StringBuilder lpString = new StringBuilder(0xff);

            Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT windowplacement = new Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT();

            int window = UnsafeNativeMethods.GetWindow(UnsafeNativeMethods.GetDesktopWindow(), 5);
            windowplacement.Length = Marshal.SizeOf(windowplacement);
       
            while (window != 0)
            {
                window = UnsafeNativeMethods.GetWindow(window, 2);
                int num = UnsafeNativeMethods.GetWindowText(window, lpString, 0xff);
                string text = lpString.ToString().TrimEnd('\0');
                //if (((((num != 0) & (text.ToLower() != "m".ToLower())) & (text.ToLower() != "default ime".ToLower())) && GetWindowPlacement(window, ref windowplacement)) && ((windowplacement.rcNormalPosition.Right - windowplacement.rcNormalPosition.Left) > 4))
                //{
                try
                {

                    WindowsInfo item = new WindowsInfo();
                    Process processById;
                    int num5 = 0;
                    int windowThreadProcessId = UnsafeNativeMethods.GetWindowThreadProcessId(window, ref num5);
                    int windowLong = UnsafeNativeMethods.GetWindowLong(window, -16);

                    processById = Process.GetProcessById(num5);
                    item.Title = (text);
                    item.ProcessID = processById.Id;
                    item.WindowsID = window;
                    item.ProcessName = (processById.ProcessName);
                    try
                    { 
                        item.FileName = processById.MainModule.FileName;
                    }
                    catch (Exception)
                    {
                        item.FileName = processById.StartInfo.FileName;
                    }
                    item.Style = windowLong;
                    list.Add(item);
                }
                catch (Exception ex)
                {
                    Feng.Utils.BugReport.Log(ex);
                    Utils.TraceHelper.WriteTrace("DataUtils", "GridViewCell", "OnDraw", ex);
                }
                //}
            }
            return list;
        }

        public static bool SetWindowPlacementmenu(int lhandle, Feng.Utils.UnsafeNativeMethods.ENUMPLACEMENT nCmdShow)
        {
            bool flag = false;
            Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT windowplacement = new Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT();
            windowplacement.Length = Marshal.SizeOf(windowplacement);
            if (!UnsafeNativeMethods.GetWindowPlacement(lhandle, ref windowplacement))
            {
                return false;
            }
            windowplacement.showCmd = (int)nCmdShow;
            if (!UnsafeNativeMethods.SetWindowPlacement(lhandle, ref windowplacement))
            {
                return false;
            }
            flag = true;
            return flag;
        }

        public static bool SetWindowPosmenu(int lhandle, Feng.Utils.UnsafeNativeMethods.ENUMWINDPOSE PostSet)
        {
            bool flag = false;
            Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT windowplacement = new Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT();
            windowplacement.Length = Marshal.SizeOf(windowplacement);
            if (!UnsafeNativeMethods.GetWindowPlacement(lhandle, ref windowplacement))
            {
                return false;
            }
            if (!UnsafeNativeMethods.SetWindowPos(lhandle, (int)PostSet, windowplacement.rcNormalPosition.Left, windowplacement.rcNormalPosition.Top, windowplacement.rcNormalPosition.Right - windowplacement.rcNormalPosition.Left, windowplacement.rcNormalPosition.Bottom - windowplacement.rcNormalPosition.Top, windowplacement.flags))
            {
                return false;
            }
            flag = true;
            return flag;
        }
 
        public static void ShowWinodw(int handle)
        {
            SetWindowPlacementmenu(handle, UnsafeNativeMethods.ENUMPLACEMENT.SW_SHOW);
        }

        public static void HideWinodw(int handle)
        {
            SetWindowPlacementmenu(handle, UnsafeNativeMethods.ENUMPLACEMENT.SW_HIDE);
        }

        //public static bool SetWindowPosmenu(int lhandle, Feng.Utils.UnsafeNativeMethods.ENUMWINDPOSE PostSet)
        //{
        //    Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT WINDOWPLACEMENT = default(Feng.Utils.UnsafeNativeMethods.WINDOWPLACEMENT);
        //    WINDOWPLACEMENT.Length = Marshal.SizeOf(WINDOWPLACEMENT);
        //    if (Feng.Utils.UnsafeNativeMethods.GetWindowPlacement(lhandle, ref WINDOWPLACEMENT) == false)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //    }
        //    if (Feng.Utils.UnsafeNativeMethods.SetWindowPos(lhandle, (int)PostSet, WINDOWPLACEMENT.rcNormalPosition.Left, WINDOWPLACEMENT.rcNormalPosition.Top, WINDOWPLACEMENT.rcNormalPosition.Right - WINDOWPLACEMENT.rcNormalPosition.Left, WINDOWPLACEMENT.rcNormalPosition.Bottom - WINDOWPLACEMENT.rcNormalPosition.Top, WINDOWPLACEMENT.flags) == false)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }

        //}
        public static void TopMostWinodw(int handle)
        {
            SetWindowPosmenu(handle, UnsafeNativeMethods.ENUMWINDPOSE.HWND_TOPMOST);
        }
    }
}

