using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Feng.Net.Base
{ 
    public class NetException : Exception
    {
        public NetException(string error)
            : base(error)
        {

        }
    }
}
