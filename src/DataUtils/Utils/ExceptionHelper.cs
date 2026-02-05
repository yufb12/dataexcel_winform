using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Feng.Net.Base;
using Feng.Forms.Dialogs;

namespace Feng.Utils
{
    public class AppException : Exception
    {
        public AppException(string msg)
            : base(msg)
        {

        }
        public AppException(string msg, Exception ex)
            : base(msg)
        {
            instance = ex;
        }
        public AppException(string msg, string code)
            : base(msg)
        {
            Code = code;
        }
        public AppException(string msg, int code)
           : base(msg)
        {
            Code = code.ToString();
        }
        public AppException(int code)
            : base("error:" + code)
        {

        }
        public string Code { get; set; }
        private Exception instance = null;
        public Exception Instance
        {
            get
            {
                return instance;
            }
            set
            {
                instance = value;
            }
        }
    }
 
    public class ExceptionHelper
    {
        public static void ThrowException(string code, string msg)
        {
            throw new AppException(msg, code);
        }
        public static void ThrowException(string code)
        {
            throw new AppException(ErrorCode.GetErrorMsg(code));
        }
        public static void ThrowNullException(object obj, string msg)
        {
            if (obj == null)
            {
                throw new AppException(msg);
            }
        }
        public static void ThrowNullException(object obj, string msg, string code)
        {
            if (obj == null)
            {
                throw new AppException(msg, code);
            }
        }
        static ExceptionHelper()
        {
     
        }
        public static bool ShowAppExceptInfo = false;
        private static int errorcount = 0;
        private static DateTime starttime = DateTime.Now;
        public static void ShowError(string mes)
        {
            MessageBox.Show(mes, "系统错误！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public static void ShowError(Exception e)
        {
            try
            {
                if (e == null)
                    return;
                Feng.IO.LogHelper.Log(e);
                AppException appex = e as AppException;
                if (appex != null)
                {
                    if (ShowAppExceptInfo)
                    {
                        string msg = e.Message;
                        if (appex.Instance != null)
                        {
                            msg = appex.Instance.Source + "\r\n" + appex.Instance.Message + "\r\n" + appex.Instance.StackTrace;
                            Feng.IO.LogHelper.Log(appex.Instance);
                        }
                        MessageBox.Show(msg, "系统错误！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(appex.Code))
                        {
                            MessageBox.Show(appex.Message, "系统错误！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            MessageBox.Show("Code=" + appex.Code + "\r\n" + appex.Message, "系统错误！", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    return;
                }
                NetException netex = e as NetException;
                if (netex != null)
                {
                    Feng.Utils.MsgBox.ShowInfomation(netex.Message);
                    return;
                }
                if ((DateTime.Now - starttime).TotalSeconds < 5)
                {
                    errorcount++;
                    if (errorcount > 5)
                    {
                        return;
                    }
                }
                else
                {
                    starttime = DateTime.Now;
                    errorcount = 0;
                }
                ErrorDialog.ShowError(e);
            }
            catch (Exception)
            {

            }
        }
    }
}
