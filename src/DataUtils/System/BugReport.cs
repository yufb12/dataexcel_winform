using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Feng.Utils
{
    public class BugReport
    {
        public static void Log(Exception ex)
        {
            Feng.Utils.TraceHelper.WriteTrace("BUG", "BUG", "BUG", ex);
        }
    }
}
