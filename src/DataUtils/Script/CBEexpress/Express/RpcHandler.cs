using System;
using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{
    public class RpcHandler
    {
        public static object Do(byte[] data)
        {
            string url = string.Empty;
            string function = string.Empty;
            List<object> args = new List<object>();
            using (Feng.IO.BufferReader reader = new Feng.IO.BufferReader(data))
            {
                url = reader.ReadIndex(1, url);
                function = reader.ReadIndex(2, function);
                int count = 0;
                count = reader.ReadIndex(3, count);
                for (int i = 0; i < count; i++)
                {
                    ushort index = (ushort)(i + 4);
                    object obj = reader.ReadBaseValueIndex(index, null);
                    args.Add(obj);
                }
            }
            object value = Do(url, function, args);
            return value;
        }
        public static string GetServerScript(string url)
        {
            string directory = System.IO.Path.GetDirectoryName(url);
            string filename = System.IO.Path.GetFileNameWithoutExtension(url);
            string file = System.IO.Path.Combine(directory, filename + ".cbst");
            return file;
        }
        public static object Do(string url, string function, List<object> args)
        {
            string file = GetServerScript(url);
            object value = RunEvent(file, function, args);
            return value;
        }
        public static object RunEvent(string file, string function, List<object> args)
        {
            object obj = null;
            string script = Feng.IO.FileHelper.ReadAllText(file);
            if (string.IsNullOrWhiteSpace(script))
                throw new Exception("在文件:" + file + "中未找到代码");
            NetParser netParser = new NetParser();
            netParser.File = file; 
            netParser.AddFunction(new Feng.Script.FunctionContainer.DateTimeFunctionContainer());
            netParser.AddFunction(new Feng.Script.FunctionContainer.ListFunctionContainer());
            netParser.Entity = null; //server instance
            obj = netParser.ExecEvent(script, function, args);
            return obj;
        }
    }

    public class RpcClient
    {
        public static object Do(string url, string function, List<object> args)
        {
            byte[] data = null;
            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
            {
                bw.Write(1, url);
                bw.Write(2, function);
                bw.Write(3, args.Count);
                for (ushort i = 0; i < args.Count; i++)
                {
                    ushort index = (ushort)(i + 4);
                    bw.WriteBaseValue(index, args[i]);
                }
                data = bw.GetData();
            }
            object value = RpcHandler.Do(data);
            return value;
        }
    }

}
