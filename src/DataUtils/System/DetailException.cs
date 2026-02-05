using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Feng.Utils
{
    public class DetailException : Exception
    {
        //
        // 摘要:
        //     使用指定的错误消息初始化 System.Exception 类的新实例。
        //
        // 参数:
        //   message:
        //     描述错误的消息。
        public DetailException(string message) : base
            (message)
        {

        }
        //
        // 摘要:
        //     使用指定错误消息和对作为此异常原因的内部异常的引用来初始化 System.Exception 类的新实例。
        //
        // 参数:
        //   message:
        //     解释异常原因的错误消息。
        //
        //   innerException:
        //     导致当前异常的异常；如果未指定内部异常，则是一个 null 引用（在 Visual Basic 中为 Nothing）。
        public DetailException(string message, Exception innerException) : base
            (message, innerException)
        {

        }

        public string DetailMessage()
        {
            string msg = string.Empty;
            if (this.InnerException != null)
            {
                msg = string.Format("{0},\r\n{1},\r\n{2}", this.Message, this.Source, this.StackTrace);
            }
            else
            {
                msg = string.Format("{0},\r\n{1},\r\n{2}", this.Message, this.Source, this.StackTrace);
            }
            return msg;
        }
    }
}
