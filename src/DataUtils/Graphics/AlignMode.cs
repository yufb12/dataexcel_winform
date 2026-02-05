using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace Feng.Drawing
{
    public enum AlignMode
    {
        Center,
        Near,
        Far
    }
    public class Direction
    {
        public const int Top = 1;
        public const int Bottom = 2;
        public const int Left = 4;
        public const int Right = 8;
        
    }
}
