using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Feng.Utils
{
    public static class SendKey
    {
        public static void Send(string key)
        {
            System.Windows.Forms.SendKeys.SendWait(key);
        }


    }
}
