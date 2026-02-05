using System;
using System.Collections.Generic;

namespace Feng.Forms.Base
{
    public class CopyRight
    {
        public CopyRight()
        {

        }
        public int Index { get; set; }
        public DateTime Time { get; set; }
        public DateTime FinishTime { get; set; }
        public string UserName { get; set; }
        public string Url { get; set; }
        public string Key { get; set; }//修改
        public byte[] GetData()
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(1, this.Index);
                bw.Write(2, this.Time);
                bw.Write(3, this.FinishTime);
                bw.Write(4, this.UserName);
                bw.Write(5, this.Url);
                bw.Write(6, this.Key);
                bw.Write(7, this.UserName);
                return bw.GetData();
            }
        }
        public void ReadData(byte [] data)
        {
            using (Feng.IO.BufferReader bw = new IO.BufferReader(data))
            {
                this.Index =  bw.ReadIndex(1, this.Index);
                this.Time = bw.ReadIndex(2, this.Time);
                this.FinishTime = bw.ReadIndex(3, this.FinishTime);
                this.UserName = bw.ReadIndex(4, this.UserName);
                this.Url = bw.ReadIndex(5, this.Url);
                this.Key = bw.ReadIndex(6, this.Key);
                this.UserName = bw.ReadIndex(7, this.UserName);

            }
        }
    }
     
}