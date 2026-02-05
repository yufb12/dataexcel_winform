
using System;


namespace Feng.IO
{
    public static class BitConver
    {
        public static string GetString(byte[] value)
        {
            return System.Text.Encoding.Unicode.GetString(value);
        }
        public static string GetString(byte[] value, int index, int count)
        {
            return System.Text.Encoding.Unicode.GetString(value, index, count);
        }
        public static byte[] GetBytes(string value)
        {
            return System.Text.Encoding.Unicode.GetBytes(value);
        }
        public static byte[] ToBytes(string value)
        {
            return System.Text.Encoding.Unicode.GetBytes(value);
        }

        public static byte[] ToBytes(int value)
        {
            return BitConverter.GetBytes(value);
        }
    }
}
