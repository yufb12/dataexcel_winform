
using System;
using System.IO;


namespace Feng.IO
{
    [Serializable]
    public class BaseWriter : System.IO.BinaryWriter
    {
        public const int NullValue = -1;
        public const byte[] NullDATA = null;
        public BaseWriter(Stream output)
            : base(output)
        {

        }
        public BaseWriter()
            : base(new MemoryStream())
        {

        }
        public BaseWriter(byte[] data)
            : base(new MemoryStream(data))
        {

        }

        public byte[] GetData()
        {
            MemoryStream ms = this.BaseStream as MemoryStream;
            return ms.ToArray();
        }

        public override void Write(char ch)
        {
            base.Write((int)ch);
        }

        public virtual void WriteBytes(byte[] value)
        {
            base.Write(value);
        }

        public virtual void WriteULong(UInt64 value)
        {
            this.Write(value);
        }
        public virtual void WriteEmpty()
        {
            byte[] value = new byte[] { };
            base.Write(value.Length);
            base.Write(value);
        }
        public virtual void WriteLong(Int64 value)
        {
            this.Write(value);
        }

        public virtual void WriteShort(Int16 value)
        {
            this.Write(value);
        }

        public virtual void WriteUShort(UInt16 value)
        {
            this.Write(value);
        }

        public virtual void WriteByte(byte value)
        {
            this.Write(value);
        }

        public virtual void WriteUInt(UInt32 value)
        {
            this.Write(value);
        }

        public virtual void WriteInt(Int32 value)
        {
            this.Write(value);
        }
 

    }
}
