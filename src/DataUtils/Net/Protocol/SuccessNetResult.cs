using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading; 
using System.Runtime.InteropServices;
using Feng.Net.Packets;

namespace Feng.Net.Packets
{  
    public class SuccessNetResult : NetResult
    {
        public SuccessNetResult(bool success)
            : base(success)
        { 

        }

        public override int Code
        {
            get
            {
                return base.Code;
            }
            set
            {
                throw new Exception("Read Only");
            }
        }
        public override string Message
        {
            get
            {
                return base.Message;
            }
            set
            {
                throw new Exception("Read Only");
            }
        }
        public override byte[] OrgValue
        {
            get
            {
                return base.OrgValue;
            }
            set
            {
                throw new Exception("Read Only");
            }
        }
        public override bool Success
        {
            get
            {
                return base.Success;
            }
            set
            {
                throw new Exception("Read Only");
            }
        }
        public override object Value
        {
            get
            {
                return base.Value;
            }
            set
            {
                throw new Exception("Read Only");
            }
        }
    }
  
}
