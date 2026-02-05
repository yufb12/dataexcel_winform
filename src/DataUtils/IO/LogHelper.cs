using System;
using System.Text;

namespace Feng.IO
{
    public class LogHelper
    {
        public static bool logFile = false;
        static LogHelper()
        {

            System.Threading.Thread th = new System.Threading.Thread(ClearLog);
            th.IsBackground = true;
            th.Start();
        }
        public static string LogRootPath
        {
            get
            {
                if (System.IO.Directory.Exists(@"F:\file\testdir\TEST\Log"))
                {
                    return @"F:\file\testdir\TEST\Log";
                }
                return Feng.IO.FileHelper.StartupPathUserData;
            }
        }

        public static string Log(Exception ex)
        {
            try
            {
                if (!logFile)
                    return string.Empty;
                string date = "__log__" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                string path = LogRootPath + "\\Bug\\" + DateTime.Now.ToString("yyyyMM");
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                path = path + "\\" + date;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("###################################");
                sb.AppendLine(DateTime.Now.ToString());
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.Source);
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine(ex.TargetSite == null ? string.Empty : ex.TargetSite.ToString());
                Exception iex = ex.InnerException;
                if (iex != null)
                {
                    sb.AppendLine(iex.Message);
                    sb.AppendLine(iex.Source);
                    sb.AppendLine(iex.StackTrace);
                    sb.AppendLine(iex.TargetSite == null ? string.Empty : ex.TargetSite.ToString());
                }
                sb.AppendLine("###################################");
                string text = sb.ToString();
                System.IO.File.AppendAllText(path, text, Encoding.Unicode);
                return sb.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        public static string Log(string type, Exception ex)
        {
            try
            { 
                if (!logFile)
                    return string.Empty;
                string date = "__log__" + DateTime.Now.ToString("yyyyMMdd") + ".log";
                string path = LogRootPath + "\\Bug\\" + type + "\\" + DateTime.Now.ToString("yyyyMM"); 
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                path = path + "\\" + date;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("###################################");
                sb.AppendLine(DateTime.Now.ToString());
                sb.AppendLine(ex.Message);
                sb.AppendLine(ex.Source);
                sb.AppendLine(ex.StackTrace);
                sb.AppendLine(ex.TargetSite == null ? string.Empty : ex.TargetSite.ToString());
                Exception iex = ex.InnerException;
                if (iex != null)
                {
                    sb.AppendLine(iex.Message);
                    sb.AppendLine(iex.Source);
                    sb.AppendLine(iex.StackTrace);
                    sb.AppendLine(iex.TargetSite == null ? string.Empty : ex.TargetSite.ToString());
                }
                sb.AppendLine("###################################");
                string text = sb.ToString(); 
                System.IO.File.AppendAllText(path, text, Encoding.Unicode);
                return sb.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        public static string Log(string txt)
        {
            try
            {
                if (!logFile)
                    return string.Empty;
                string date = "__log__" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                string path = LogRootPath + "\\log\\" + DateTime.Now.ToString("yyyyMM");
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                path = path + "\\" + date;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("###################################");
                sb.AppendLine(DateTime.Now.ToString());
                sb.AppendLine(txt);

                sb.AppendLine("###################################");
                System.IO.File.AppendAllText(path, sb.ToString(), Encoding.Unicode);
                return sb.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        public static string Log(string type, string txt)
        {
            try
            {
                if (!logFile)
                    return string.Empty;
                string date = "__log__" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                string path = LogRootPath + "\\log\\" + type + "\\" + DateTime.Now.ToString("yyyyMM");
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                path = path + "\\" + date;
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("###################################");
                sb.AppendLine(DateTime.Now.ToString());
                sb.AppendLine("Type:" + type);
                sb.AppendLine("Value:" + txt);

                sb.AppendLine("###################################");
                System.IO.File.AppendAllText(path, sb.ToString(), Encoding.Unicode);
                return sb.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }

        public static void LogValue(string type, string txt)
        {
            try
            {
                if (!logFile)
                    return;
                string date = "__log__" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
                string path = LogRootPath + "\\log\\" + type + "\\" + DateTime.Now.ToString("yyyyMM");
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                path = path + "\\" + date; 
                System.IO.File.AppendAllText(path, txt, Encoding.Unicode); 
            }
            catch (Exception)
            { 
            }

        }

        public static void LogMonthValue(string type, string txt)
        {
            try
            {
                if (!logFile)
                    return;
                string date = "__log__" + DateTime.Now.ToString("yyyyMM") + ".txt";
                string path = LogRootPath + "\\log\\" + type + "\\" + DateTime.Now.ToString("yyyyMM");
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                path = path + "\\" + date; 
                System.IO.File.AppendAllText(path,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") +"\t"+ txt+ "\t\r\n", Encoding.Unicode); 
            }
            catch (Exception)
            { 
            }

        }

        public static void ClearLog()
        {
            while (true)
            {
                try
                {
                    string path = LogRootPath;
                    string[] files = System.IO.Directory.GetFiles(path);
                    foreach (string file in files)
                    {
                        string filename = System.IO.Path.GetFileNameWithoutExtension(file);
                        if (filename.StartsWith("__log__"))
                        {
                            if (filename.Length > 6)
                            {
                                int filedate = int.Parse(filename);
                                int filenow = int.Parse(DateTime.Now.AddDays(-7).ToString("yyyyMMdd"));
                                if (filedate < filenow)
                                {
                                    System.IO.File.Delete(file);
                                }
                            }
                            else
                            {
                                int filedate = int.Parse(filename);
                                int filenow = int.Parse(DateTime.Now.AddMonths(-2).ToString("yyyyMM"));
                                if (filedate < filenow)
                                {
                                    System.IO.File.Delete(file);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Feng.Utils.BugReport.Log(ex);
                    Feng.Utils.TraceHelper.WriteTrace("DataDesign", "frmMain2", "statusSum_Click", ex);
                }
                System.Threading.Thread.Sleep(1000 * 60 * 60);
                if (!logFile)
                    return;
            }
        }

    }
}
