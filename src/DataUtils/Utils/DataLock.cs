using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Feng.Utils
{ 
    public class DataLock
    {
        public static void DeFile(string file)
        {
            using (FileStream fs = System.IO.File.Open(file, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                if (fs.Length < 100)
                {
                    return;
                }
                byte[] data = new byte[16];
                byte[] buffer = null;
                System.IO.BinaryReader reader = new BinaryReader(fs);
                {
                    fs.Seek(0, SeekOrigin.Begin);
                    buffer = reader.ReadBytes(16);
                    for (int i = 0; i < 16; i++)
                    {
                        byte position = buffer[i];
                        int index = i + 18;
                        fs.Seek(i, SeekOrigin.Begin);
                        byte bydata = reader.ReadByte(); 
                        data[i] = bydata;
                    }
                } 
                System.IO.BinaryWriter bw = new BinaryWriter(fs);
                {
                    for (int i = 0; i < 16; i++)
                    {
                        byte position = buffer[i];
                        int index = i + 18;
                        fs.Seek(i, SeekOrigin.Begin);
                        byte bydata = data[15-i];
                        bw.Write(bydata); 
                    }
                }
        
                bw.Flush();
                bw.Close();
                reader.Close();
                fs.Close();
            }
        }
         
    }
}
