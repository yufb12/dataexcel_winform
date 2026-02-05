using System.Collections.Generic;

namespace Feng.Forms.Base
{
    public class LockVersion
    {
        public int LockIndex { get; set; }
        public string LockTime { get; set; }
        public string LockUserID { get; set; }
        public string LockUserName { get; set; }
        public byte[] GetData()
        {
            using (Feng.IO.BufferWriter bw = new IO.BufferWriter())
            {
                bw.Write(1, this.LockIndex);
                bw.Write(2, this.LockTime);
                bw.Write(3, this.LockUserID);
                bw.Write(4, this.LockUserName);
                return bw.GetData();
            }
        }
        public void ReadData(byte [] data)
        {
            using (Feng.IO.BufferReader bw = new IO.BufferReader(data))
            {
                this.LockIndex=  bw.ReadIndex(1, this.LockIndex);
                this.LockTime = bw.ReadIndex(2, this.LockTime);
                this.LockUserID = bw.ReadIndex(3, this.LockUserID);
                this.LockUserName = bw.ReadIndex(4, this.LockUserName);
             
            }
        }
    }
     
}