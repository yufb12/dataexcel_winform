using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using MindMap.UI;

namespace MindMap
{
    static class Program
    {
        [DllImport("shcore.dll", SetLastError = true)]
        internal static extern int SetProcessDpiAwareness(int value);

        /// <summary>
        /// 应用程序的主入口点（v1.7.1新增：DPI感知，支持高清屏幕）
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SetProcessDpiAwareness(1);
            Application.Run(new MainForm());
        }
    }
}
