using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Collections.Model
{
    public class Pairs
    {
        public const string OperatorToFirst = "<=";
        public const string OperatorToSecond = "=>";
        public string First { get; set; }
        public string Second { get; set; }
        public string Operator { get; set; }
        public byte[] GetData()
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(1, First);
                bw.Write(2, First);
                bw.Write(3, Operator);
                return bw.GetData();
            }
        }
    }

}
