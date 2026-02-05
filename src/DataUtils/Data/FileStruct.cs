using System;
using System.Collections.Generic;
using System.Data;
using System.ComponentModel;
using Feng.Utils;

using System.Drawing;
namespace Feng.Data
{ 
    public class FileStruct
    {
        public FileStruct()
        {

        }

        public Version Version { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string DownLoadUrl { get; set; }
        public string ID { get; set; }
    }
}
