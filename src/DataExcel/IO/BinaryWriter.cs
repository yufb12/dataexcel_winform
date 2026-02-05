
using System;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using Feng.Data; 
using System.Collections.Generic;
using Feng.Excel.Styles;
using Feng.Forms.Interface;
using Feng.Forms.Base;

namespace Feng.Excel.IO
{
    [Serializable]
    public class BinaryWriter : Feng.IO.BufferWriter 
    {

        public BinaryWriter(Stream output)
            : base(output)
        {

        }
        public BinaryWriter()
            : base(new MemoryStream())
        {

        }
        public BinaryWriter(byte[] data)
            : base(new MemoryStream(data))
        {

        }
 
 
 
    }
}
