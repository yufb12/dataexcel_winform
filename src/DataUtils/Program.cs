//using System;
//using System.Collections.Generic;
//using System.Windows.Forms;
 
//namespace Feng.Utils
//{
//    static class Program
//    {
//        /// <summary>
//        /// 应用程序的主入口点。
//        /// </summary>
//        [STAThread]
//        static void Main()
//        {
//            Application.EnableVisualStyles();
//            Application.SetCompatibleTextRenderingDefault(false);
//            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
//            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
//            Application.Run(new Feng.Assistant.frmSysEventMainFormTree());
//            //Application.Run(new Feng.Utils.SysHelper.frmSysEventMainForm());
//        }

//        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
//        {
//            try
//            {
//                Application.Run(new Feng.Assistant.frmSysEventMainForm());
//            }
//            catch (Exception ex)
//            {
//                string msg = "遇到重大问题，请联系客服【1250037751】修改\r\n"
//                    + "\r\n 或打开网站下载最新版" + "\r\n" +
//                    ex.Message + "\r\n" + ex.StackTrace;
//                MessageBox.Show(msg);
//                System.Diagnostics.Process.Start("http://www.booxin.com/SysEvents/syseventdownload.htm");

//            }
        
//        }

//        static void Application_ApplicationExit(object sender, EventArgs e)
//        {

//            try
//            {
                
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }
        
//        }

//    }
//}
