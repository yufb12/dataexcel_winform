using Feng.Forms.Interface;
using System;

namespace Feng.Script.CBEexpress
{
    public class SyntacticException:Exception
    {
        public SyntacticException(string msg) : base(msg)
        {

        }
        public int Row { get; set; }
        public int Column { get; set; }
        public override string Message { get { return base.Message + " Row:" + Row + " Column:" + Column; } }
    }
 
}
