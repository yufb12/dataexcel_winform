using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Feng.Office
{
    public static class SendKey
    {
        public static void Send(string key)
        {
#if DEBUG
            Feng.Office.TraceHelper.WriteTrace("SendKey", key);
#endif
            System.Windows.Forms.SendKeys.Send(key);
        }
        [DllImport("mscoree.dll", CharSet = CharSet.Unicode)]
        static extern bool StrongNameSignatureVerificationEx(string wszFilePath, bool fForceVerification, ref bool pfWasVerified);

        //bool notForced = false;
        //bool verified = StrongNameSignatureVerificationEx(assembly, false, ref notForced);
        //Console.WriteLine("Verified: {0}\nForced: {1}", verified, !notForced);
    }
}
