using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Feng.Utils
{
    public static class RandomCache
    {
        private static Random rnd = null;
        static RandomCache()
        {
            rnd = new Random(DateTime.Now.Millisecond);
        }

        public static int Next()
        {
            return rnd.Next();
        }

        public static int Next(int min, int max)
        {
            return rnd.Next(min, max);
        } 
    }
}
