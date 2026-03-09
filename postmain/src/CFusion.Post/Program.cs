using CFusion.Http.post;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace CFusion
{
    static class Program
    {
        [DllImport("shcore.dll", SetLastError = true)]
        internal static extern int SetProcessDpiAwareness(int value);
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SetProcessDpiAwareness(1);
            Application.Run(new frmPostMain());
        }
    }
}
