
using System;
using System.Drawing;
using System.IO;


namespace Feng.IO
{
    [Serializable]
    public class BaseReader : System.IO.BinaryReader
    {

        public BaseReader(Stream output)
            : base(output)
        {

        }

        public BaseReader(byte[] data)
            : base(new MemoryStream(data))
        {

        }

        public static byte[] GetData(string filename)
        {
            if (System.IO.File.Exists(filename))
            {
                return System.IO.File.ReadAllBytes(filename);
            }
            return null;
        }

        public static Image GetImage(string filename)
        {
            byte[] data = GetData(filename);
            if (data != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Image img = Image.FromStream(ms);
                    return img;
                }
            }
            return null;
        }

        public virtual int ReadInt()
        {
            return this.ReadInt32();
        }
        public virtual int ReadShort()
        {
            return this.ReadInt16();
        }
        public virtual string ReadASCII(int len)
        {
            byte[] data = this.ReadBytes(len);
            return System.Text.Encoding.ASCII.GetString(data);
        }
        public virtual string ReadASCII4()
        {
            return ReadASCII(4);
        }
        public virtual string ReadASCII2()
        {
            return ReadASCII(2);
        }
        public virtual string ReadASCII8()
        {
            return ReadASCII(8);
        }
    }
}
