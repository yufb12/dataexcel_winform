using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Feng.IO
{

    public static class CompressHelper
    { 
 
        public static byte[] DeflateStreamDecompress(byte[] encrypted)
        {
            byte[] data = new byte[0];
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(encrypted))
            {
                using (System.IO.MemoryStream cache = new System.IO.MemoryStream())
                {
                    using (System.IO.Compression.DeflateStream ds = 
                        new System.IO.Compression.DeflateStream(ms, 
                            System.IO.Compression.CompressionMode.Decompress, true))
                    {
                        byte[] buffer = new byte[encrypted.Length];
                        int len = ds.Read(buffer, 0, buffer.Length);
                        while (len > 0)
                        {
                            cache.Write(buffer, 0, len);
                            buffer = new byte[encrypted.Length];
                            len = ds.Read(buffer, 0, buffer.Length);
                        }
                        ds.Flush();
                        ds.Close();
                    }
                    ms.Flush();
                    ms.Close();
                    cache.Flush();
                    data = cache.ToArray();
                    cache.Close();
                }
            }
            return data;
        }

        public static byte[] DeflateStreamCompress(byte[] original)
        {
            byte[] data = new byte[0];
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (System.IO.Compression.DeflateStream ds =
                    new System.IO.Compression.DeflateStream(ms,
                        System.IO.Compression.CompressionMode.Compress, true))
                {
                    ds.Write(original, 0, original.Length);
                    ds.Flush();
                    ds.Close();
                }
                ms.Flush();
                data = ms.ToArray();
                ms.Close();
            }
            return data;
        }

        public static byte[] GZipStreamDecompress(byte[] encrypted)
        {
            byte[] data = new byte[0];
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(encrypted))
            {
                using (System.IO.MemoryStream cache = new System.IO.MemoryStream())
                {
                    using (System.IO.Compression.GZipStream ds = 
                        new System.IO.Compression.GZipStream(ms, 
                            System.IO.Compression.CompressionMode.Decompress, true))
                    {
                        byte[] buffer = new byte[encrypted.Length];
                        int len = ds.Read(buffer, 0, buffer.Length);
                        while (len > 0)
                        {
                            cache.Write(buffer, 0, len);
                            buffer = new byte[encrypted.Length];
                            len = ds.Read(buffer, 0, buffer.Length);
                        }
                        ds.Flush();
                        ds.Close();
                    }
                    cache.Flush();
                    data= cache.ToArray();
                }
                ms.Flush();
                ms.Close();
            }
            return data;
        }

        public static byte[] GZipStreamCompress(byte[] original)
        {
            byte[] data = new byte[0];
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (System.IO.Compression.GZipStream ds =
                    new System.IO.Compression.GZipStream(ms,
                        System.IO.Compression.CompressionMode.Compress, true))
                {
                    ds.Write(original, 0, original.Length);
                    ds.Flush();
                    ds.Close();
                }
                ms.Flush();
                data = ms.ToArray();
                ms.Close();
                
            }
            return data;
        } 

        //public static byte[] ZipStreamCompress(byte[] original)
        //{
        //    return AcedDeflator.Instance.Compress(original);
        //}

        //public static byte[] ZipStreamDecompress(byte[] encrypted)
        //{
        //    return AcedInflator.Instance.Decompress(encrypted);
        //}
 
        public static bool CompareFile(string file1, string file2)
        {
            return CompareData(System.IO.File.ReadAllBytes(file1), System.IO.File.ReadAllBytes(file2));
        }

        public static bool CompareData(byte[] buf1, byte[] buf2)
        {
            List<byte> list = new List<byte>();
            
            if (buf1.Length != buf2.Length)
            {
                return false;
            }
            int len1 = buf1.Length;
            for (int i = 0; i < len1; i++)
            {
                if (buf1[i] != buf2[i])
                {
                    list.Add(buf1[i]);
#if DEBUG
                    continue;
#else
                    return false;
#endif
                }
            }
            return true;
        }

        public static byte[] GZipDecompress(byte[] encrypted)
        {
            if (encrypted.Length < 1)
                return encrypted;
            using (MemoryStream cmpStream = new MemoryStream(encrypted))
            {
                using (MemoryStream orgStream = new MemoryStream())
                {
                    DeCompress(cmpStream, orgStream);
                    return orgStream.ToArray();
                }
            }
        }

        public static byte[] GZipCompress(byte[] original)
        {
            if (original == null)
                return new byte[] { };
            using (MemoryStream orgStream = new MemoryStream(original))
            {
                using (MemoryStream cmpStream = new MemoryStream())
                {
                    Compress(orgStream, cmpStream);
                    return cmpStream.ToArray();
                }

            }
        } 

        public static void Compress(System.IO.Stream orgStream, System.IO.Stream cmpStream)
        {
            System.IO.Compression.GZipStream zipStream = 
                new System.IO.Compression.GZipStream(cmpStream, System.IO.Compression.CompressionMode.Compress);
            BinaryWriter writer = new BinaryWriter(zipStream);
            BinaryReader reader = new BinaryReader(orgStream);
            while (true)
            {
                byte[] buffer = reader.ReadBytes(1024);
                if (buffer == null || buffer.Length < 1)
                    break;
                writer.Write(buffer);
            }
            writer.Close();
        }

        public static void DeCompress(System.IO.Stream cmpStream, System.IO.Stream orgStream)
        {
            System.IO.Compression.GZipStream zipStream = 
                new System.IO.Compression.GZipStream(cmpStream, System.IO.Compression.CompressionMode.Decompress);
            BinaryReader reader = new BinaryReader(zipStream);
            BinaryWriter writer = new BinaryWriter(orgStream);
            while (true)
            {
                byte[] buffer = reader.ReadBytes(1024);
                if (buffer == null || buffer.Length < 1)
                    break;
                writer.Write(buffer);
            }
            writer.Close();
        }
    }
}
