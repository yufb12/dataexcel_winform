using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using Feng.Utils;

using System.Drawing;
namespace Feng.Data
{
    public struct AessemlyStruct
    {
        public int Header { get; set; }
        public long Position { get; set; }
        public long Index { get; set; }
        public int NameLength { get; set; }
        public string Name { get; set; }
        public int FullNameLength { get; set; }
        public string FullName { get; set; }
        public int AessemlyDownLoadUrlLength { get; set; }
        public string AessemlyDownLoadUrl { get; set; }
        public long DataLength { get; set; }
        public byte[] Data { get; set; }
        public int End { get; set; }
    }
}
