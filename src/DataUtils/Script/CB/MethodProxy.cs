
using Feng.Forms.Interface;
using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;
using System.Collections.Generic;
using System.IO;

namespace Feng.Script.CB
{


    public interface IEntity
    {
        object Entity { get; set; }
    }
    public interface ILoadFunction
    {
        bool LoadFunction(string paretpath, string path);
    }
    public interface IPreprocessing
    {
        object Preprocessing(IMethodProxy methodProxy, string path);
    }
    public interface IMethodProxy : IEntity, ILoadFunction, IClear
    {
        string File { get; set; }
        NetStatementFunction GetFunction(string value);
        void AddFunction(string value, NetStatementFunction netStatementFunction);
        void AddFunction(NetStatementFunction netStatementFunction);
        void AddFunction(CBMethodContainer methodContainer);
        NetOperatorProxyBase netOperatorProxy { get; }
        bool HasFunction(string value);
        object RunFunction(string value, List<object> args);
        object RunRpcFunction(string value, List<object> args);
    }

    public class MethodProxy : IMethodProxy, INetParserInit
    {
        public MethodProxy()
        {
            netOperatorProxy = new NetExcuteProxy();
            dics = new Feng.Collections.DictionaryEx<string, NetStatementFunction>();
        }
        public NetOperatorProxyBase netOperatorProxy { get; private set; }
        public object Entity { get; set; }
        public string File { get; set; }

        Feng.Collections.DictionaryEx<string, NetStatementFunction> dics = null;
        public void AddFunction(string functionname, NetStatementFunction netStatementFunction)
        {
            dics.Add(functionname, netStatementFunction);
        }
        public void AddFunction(NetStatementFunction netStatementFunction)
        {
            dics.Add(netStatementFunction.GetFunctionName(), netStatementFunction);
        }
        private List<CBMethodContainer> CBMethods = new List<CBMethodContainer>();
        public void AddFunction(CBMethodContainer methodContainer)
        {
            CBMethods.Add(methodContainer);
        }

        public NetStatementFunction GetFunction(string functionname)
        {
            return dics[functionname];
        }

        public bool HasFunction(string functionname)
        {
            bool res = dics.ContainsKey(functionname);
            if (res)
                return true;
            foreach (CBMethodContainer methodContainer in CBMethods)
            {
                if (methodContainer.Contains(functionname))
                {
                    return true;
                }
            }
            return res;
        }

        public object RunFunction(string functionname, List<object> args)
        {
            foreach (CBMethodContainer methodContainer in CBMethods)
            {
                foreach (IMethodInfo item in methodContainer.MethodList)
                {
                    if (item.Name.ToUpper() == functionname.ToUpper())
                    {
                        object value = item.Exec(args.ToArray());
                        return value;
                    }
                }
            }
            throw new Exception("不存在此函数:" + functionname);
        }

        public void Init(NetVarCollection varStack, IMethodProxy methodProxy)
        {
            foreach (CBMethodContainer item in CBMethods)
            {
                INetParserInit netParserInit = item as INetParserInit;
                if (netParserInit != null)
                {
                    netParserInit.Init(varStack, methodProxy);
                }
            }
        }

        public void Clear()
        {
            dics.Clear();
        }

        public object RunRpcFunction(string function, List<object> args)
        {
            using (Feng.IO.BufferWriter bw = new Feng.IO.BufferWriter())
            {
                bw.Write(1, this.File);
                bw.Write(2, function);
                for (ushort i = 0; i < args.Count; i++)
                {
                    ushort index = (ushort)(i + 2);
                    bw.WriteBaseValue(index, args[i]);
                }
            }
            return null;
        }
        public TokenPool GetTokenPool(string txt)
        {
            Lexer lexer = new Lexer(txt);
            lexer.Parse();
            return lexer.TokenPool;
        }

        public object Preprocessing(List<NetStatementBase> list, string path)
        {
            foreach (NetStatementBase item in list)
            {
                IPreprocessing preprocessing = item as IPreprocessing;
                if (preprocessing == null)
                    continue;
                preprocessing.Preprocessing(this, path);
            }
            return true;
        }
        public static string GetAbsolutePath(string currentFilePath, string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath))
            {
                throw new ArgumentException("相对路径不能为空", nameof(relativePath));
            }

            if (string.IsNullOrEmpty(currentFilePath))
            {
                throw new ArgumentException("当前文件路径不能为空", nameof(currentFilePath));
            }

            // 若相对路径已经是绝对路径，直接返回
            if (Path.IsPathRooted(relativePath))
            {
                return relativePath;
            }

            // 获取当前文件所在的目录
            string currentDirectory = Path.GetDirectoryName(currentFilePath);

            // 若当前目录为空，抛出异常
            if (string.IsNullOrEmpty(currentDirectory))
            {
                throw new InvalidOperationException("无法获取当前文件的目录");
            }

            // 组合路径
            string absolutePath = Path.Combine(currentDirectory, relativePath);

            // 解析路径中的"."和".."
            return Path.GetFullPath(absolutePath);
        }
 
        public bool LoadFunction(string paretpath, string path)
        {
            string file = GetAbsolutePath(paretpath, path);
            if (!System.IO.File.Exists(file))
                return false;
            string script = System.IO.File.ReadAllText(file, System.Text.Encoding.UTF8);
            TokenPool tokenPool = GetTokenPool(script);
            List<NetStatementBase> list = NetStatementBuilder.GetNetStatements(tokenPool);
            foreach (NetStatementBase item in list)
            {
                NetStatementFunction fun = item as NetStatementFunction;
                if (fun == null)
                    continue;
                this.AddFunction(fun);
            }
            string ppath = System.IO.Path.GetDirectoryName(file);
            Preprocessing(list, file);
            return true;
        }
    }
     
}
