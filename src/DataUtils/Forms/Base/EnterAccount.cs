using System;
using System.Collections.Generic;

namespace Feng.Forms.Base
{
    public class EnterAccount
    {
        public int Index { get; set; }
        public DateTime Time { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Remark { get; set; }
        public byte[] GetData()
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(1, this.Index);
                bw.Write(2, this.Time);
                bw.Write(3, this.UserID);
                bw.Write(4, this.UserName);
                bw.Write(5, this.UserName);
                return bw.GetData();
            }
        }
        public void ReadData(byte [] data)
        {
            using (Feng.IO.BufferReader bw = new IO.BufferReader(data))
            {
                this.Index=  bw.ReadIndex(1, this.Index);
                this.Time = bw.ReadIndex(2, this.Time);
                this.UserID = bw.ReadIndex(3, this.UserID);
                this.UserName = bw.ReadIndex(4, this.UserName);
                this.Remark = bw.ReadIndex(5, this.Remark);

            }
        }
    }
     
}