using System;
using System.Text;

namespace Feng.Net.Protocol
{
    public class ExtendLockInfo
    {
        public string lockuserid { get; set; }
        public DateTime lockcreattime { get; set; }
        public string lockurl { get; set; }
        public DateTime fileLastWriteTime { get; set; }
        public DateTime fileCreationTime { get; set; }
        public DateTime fileLastAccessTime { get; set; }


        public string requestuserid { get; set; }
        public DateTime requescreattime { get; set; }
        public string requesurl { get; set; }
    }
}
