using Feng.Forms.Interface;
using Feng.Script.Method;
using System;
using System.Collections.Generic;
using System.IO;

namespace Feng.Script.CBEexpress
{
    public interface IEntity
    {
        object Entity { get; set; }
    }
    public interface ILoadFunction
    {
        bool LoadFunction(string paretpath, string path);
    }
    public interface IFunctionName
    {
        string FunctionName { get; set; }
    }
    public interface ICBContext : IEntity, IFunctionName, IClear
    {
        string File { get; set; }
        NetStatementFunction GetFunction(string value);
        void AddFunction(string value, NetStatementFunction netStatementFunction);
        void AddFunction(NetStatementFunction netStatementFunction);
        void AddFunction(IMethod methodContainer);
        List<IMethod> GetCBMethods();
        NetOperatorProxyBase netOperatorProxy { get; }
        NetDebug Debug { get; set; }
        bool HasFunction(string value);
        object RunFunction(string value, List<object> args, NetVarCollection varStack, ICBContext methodProxy);
        object RunRpcFunction(string url, string function, List<object> args);
    }

    public class CBContext : ICBContext, INetParserInit
    {
        public CBContext()
        {
            netOperatorProxy = new NetExcuteProxy();
            dics = new Feng.Collections.DictionaryEx<string, NetStatementFunction>();
        }
        public virtual NetOperatorProxyBase netOperatorProxy { get; private set; }
        public virtual object Entity { get; set; }
        public virtual string File { get; set; }
        public virtual NetDebug Debug { get; set; }
        public virtual string FunctionName { get; set; }

        Feng.Collections.DictionaryEx<string, NetStatementFunction> dics = null;
        public virtual void AddFunction(string functionname, NetStatementFunction netStatementFunction)
        {
            dics.Add(functionname, netStatementFunction);
        }
        public virtual void AddFunction(NetStatementFunction netStatementFunction)
        {
            dics.Add(netStatementFunction.GetFunctionName(), netStatementFunction);
        }
        private List<IMethod> CBMethods = new List<IMethod>();
        public virtual void AddFunction(IMethod methodContainer)
        {
            CBMethods.Add(methodContainer);
        }
        public List<IMethod> GetCBMethods()
        {
            return CBMethods;
        }
        public virtual NetStatementFunction GetFunction(string functionname)
        {
            return dics[functionname];
        }

        public virtual bool HasFunction(string functionname)
        {
            bool res = dics.ContainsKey(functionname);
            if (res)
                return true;
            foreach (IMethod methodContainer in CBMethods)
            {
                if (methodContainer.Contains(functionname))
                {
                    return true;
                }
            }
            return res;
        }

        public virtual object RunFunction(string functionname, List<object> args, NetVarCollection varStack, ICBContext methodProxy)
        {
            NetStatementFunction netStatementFunction = this.GetFunction(functionname);
            if (netStatementFunction != null)
            {
                object value = netStatementFunction.Run(varStack, this, args);
                return value;
            }

            args.Insert(0, methodProxy);
            foreach (IMethod methodContainer in CBMethods)
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

        public virtual void Init(NetVarCollection varStack, ICBContext methodProxy)
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

        public virtual void Clear()
        {
            dics.Clear();
        }

        public virtual object RunRpcFunction(string url, string function, List<object> args)
        {
            object value = RpcClient.Do(url, function, args);
            return value;
        }
        public virtual TokenPool GetTokenPool(string txt)
        {
            Lexer lexer = new Lexer(txt);
            lexer.Parse();
            return lexer.TokenPool;
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


    }



    public class NetVarList
    {
        public NetVarList()
        {
            dics = new Feng.Collections.DictionaryEx<string, object>();
        }
#if DEBUG
        public NetVarList(string name)
        {
            Name = name;
            dics = new Feng.Collections.DictionaryEx<string, object>();
        }
#endif
        public string Name { get; set; }
        private Feng.Collections.DictionaryEx<string, object> dics = null;
        public void SetValue(string name, object value)
        {
            string key = name.ToUpper();
            dics.Add(key, value);
        }
        public object GetValue(string key)
        {
            return dics.Get(key);
        }
        public void Remove(string key)
        {
            dics.Remove(key);
        }

        public bool Has(string key)
        {
            return dics.ContainsKey(key);
        }
    }


    public class NetSkip
    {
        public bool CanReturn { get; set; }
        public bool CanBreak { get; set; }
        public bool Return { get; set; }
        public object ReturnValue { get; set; }
        public bool Break { get; set; }
        public bool Continue { get; set; }
        public bool IF { get; set; }
    }

    public class NetVarCollection
    {
        public NetVarCollection()
        {
            stackList = new Feng.Collections.ListEx<NetVarList>();
        }

        private Feng.Collections.ListEx<NetVarList> stackList = null;

        public bool HasVarValue(string varnamel)
        {
            string varname = varnamel.ToUpper();
            for (int i = stackList.Count - 1; i >= 0; i--)
            {
                NetVarList varlist = stackList[i];
                if (varlist.Has(varname))
                {
                    return true;
                }
            }
            return false;
        }
        public object GetVarValue(string varnamel, Token token)
        {
            string varname = varnamel.ToUpper();
            for (int i = stackList.Count - 1; i >= 0; i--)
            {
                NetVarList varlist = stackList[i];
                if (varlist.Has(varname))
                {
                    return varlist.GetValue(varname);
                }
            }
            throw new SyntacticException(token, "不存在此变量:[" + varname + "]", CBEexpressExCode.ERRORCODE_11113);
        }

        public void SetVarValue(string varnamel, object value)
        {
            string varname = varnamel.ToUpper();
            bool has = false;
            for (int i = stackList.Count - 1; i >= 0; i--)
            {
                NetVarList varlist = stackList[i];
                if (varlist.Has(varname))
                {
                    varlist.SetValue(varname, value);
                    has = true;
                    break;
                }
            }
            if (!has)
            {
                for (int i = stackList.Count - 1; i >= 0; i--)
                {
                    NetVarList varlist = stackList[i];
                    varlist.SetValue(varname, value);
                    has = true;
                    break;
                }
            }
        }

        public object GetTokenValue(Token token)
        {
            object currentvalue = null;
            switch (token.Type)
            {
                case TokenType.CONST_FALSE:
                    currentvalue = false;
                    break;
                case TokenType.CONST_NULL:
                    currentvalue = null;
                    break;
                case TokenType.CONST_NUMBER:
                    currentvalue = decimal.Parse(token.Value);
                    break;
                case TokenType.CONST_STRING:
                    currentvalue = token.Value;
                    break;
                case TokenType.CONST_TRUE:
                    currentvalue = true;
                    break;
                case TokenType.ID:
                    currentvalue = GetVarValue(token.Value, token);
                    break;
                default:
                    throw new SyntacticException(token, "类型不正确", CBEexpressExCode.ERRORCODE_11114);
            }
            return currentvalue;
        }

        public void Add(NetVarList netVarList)
        {
            stackList.Add(netVarList);
        }

        public void Remove(NetVarList netVarList)
        {
            stackList.Remove(netVarList);
        }
    }

    public class NetStatementBuilder
    {
        public static List<NetStatementBase> GetNetStatements(TokenPool tokenPool)
        {
            List<NetStatementBase> netStatements = new List<NetStatementBase>();
            while (tokenPool.HasNext())
            {
                NetStatementBase netStatement = Build(tokenPool);
                netStatements.Add(netStatement);
            }
            return netStatements;
        }

        public static NetStatementBase Build(TokenPool lexer)
        {
            NetStatementBase netStatement = null;
            Token token = lexer.Peek();
            switch (token.Type)
            {
                case TokenType.Key_FOREACH:
                    netStatement = new NetStatementForeach();
                    netStatement.Begin(lexer);
                    break;
                case TokenType.Key_FOR:
                    netStatement = new NetStatementFor();
                    netStatement.Begin(lexer);
                    break;
                case TokenType.Key_WHILE:
                    netStatement = new NetStatementWhile();
                    netStatement.Begin(lexer);
                    break;
                case TokenType.Key_Try:
                    netStatement = new NetStatementTry();
                    netStatement.Begin(lexer);
                    break;
                case TokenType.Key_VAR:
                    netStatement = new NetStatementVar();
                    netStatement.Begin(lexer);
                    break;
                case TokenType.ID:
                    token = lexer.Pop();
                    token = lexer.Peek();
                    lexer.Back();
                    if (token.Type == TokenType.SignLeftParenthesis)
                    {
                        netStatement = new NetStatementIDFunction();
                        netStatement.Begin(lexer);
                    }
                    else if (token.Type == TokenType.SignIncrement)
                    {
                        netStatement = new NetStatementIncrement();
                        netStatement.Begin(lexer);
                    }
                    else if (token.Type == TokenType.SignDecrement)
                    {
                        netStatement = new NetStatementDecrement();
                        netStatement.Begin(lexer);
                    }
                    else
                    {
                        netStatement = new NetStatementID();
                        netStatement.Begin(lexer);
                    }
                    break;
                case TokenType.Key_RETURN:
                    netStatement = new NetStatementReturn();
                    netStatement.Begin(lexer);
                    break;
                case TokenType.Key_Throw:
                    netStatement = new NetStatementThrow();
                    netStatement.Begin(lexer);
                    break;
                case TokenType.Key_IF:
                    netStatement = new NetStatementIF();
                    netStatement.Begin(lexer);
                    break;
                case TokenType.Key_FUNCTION:
                    netStatement = new NetStatementFunction();
                    netStatement.Begin(lexer);
                    break;
                case TokenType.Key_Object:
                    token = lexer.Pop();
                    token = lexer.Peek();
                    if (token.Type != TokenType.ID)
                        throw new SyntacticException(token, "不支持的关键字", CBEexpressExCode.ERRORCODE_11287);
                    token = lexer.Pop();
                    token = lexer.Peek();
                    if (token.Type == TokenType.SignLeftParenthesis)
                    {
                        lexer.Back();
                        lexer.Back();
                        netStatement = new NetStatementObjectFunction();
                        netStatement.Begin(lexer);
                        break;
                    }
                    else if (token.Type == TokenType.SignEuality)
                    {
                        lexer.Back();
                        lexer.Back();
                        netStatement = new NetStatementObject();
                        netStatement.Begin(lexer);
                        break;
                    }
                    throw new SyntacticException(token, "不支持的关键字", CBEexpressExCode.ERRORCODE_11288);
                case TokenType.Key_BREAK:
                    netStatement = new NetStatementBreak();
                    netStatement.Begin(lexer);
                    break;
                case TokenType.Key_CONTINUE:
                    netStatement = new NetStatementContinue();
                    netStatement.Begin(lexer);
                    break;
                case TokenType.SignSemicolon:
                    netStatement = new NetStatementEmpty();
                    netStatement.Begin(lexer);
                    break;
                case TokenType.Key_Include:
                    netStatement = new NetStatementInclude();
                    netStatement.Begin(lexer);
                    break;
                default:
                    throw new SyntacticException(token, "不支持的关键字", CBEexpressExCode.ERRORCODE_11235);
            }

            return netStatement;
        }


        public static NetStatementBase Build(TokenPool lexer,int endtype)
        {
            NetStatementBase netStatement = null;
            Token token = lexer.Peek();
            switch (token.Type)
            {    
                case TokenType.Key_VAR:
                    netStatement = new NetStatementVar();
                    netStatement.Begin(lexer);
                    break;
                case TokenType.ID:
                    token = lexer.Pop();
                    token = lexer.Peek();
                    lexer.Back(); if (token.Type == TokenType.SignIncrement)
                    {
                        netStatement = new NetStatementIncrement();
                        netStatement.Begin(lexer);
                        break;
                    }
                    else if (token.Type == TokenType.SignDecrement)
                    {
                        netStatement = new NetStatementDecrement();
                        netStatement.Begin(lexer);
                        break;
                    }
                    else
                    {
                        netStatement = new NetStatementID();
                        netStatement.Begin(lexer);
                        break;
                    }
                    throw new SyntacticException(token, "不支持的关键字", CBEexpressExCode.ERRORCODE_11288);
                case TokenType.Key_Object:
                    token = lexer.Pop();
                    token = lexer.Peek();
                    if (token.Type != TokenType.ID)
                        throw new SyntacticException(token, "不支持的关键字", CBEexpressExCode.ERRORCODE_11287);
                    token = lexer.Pop();
                    token = lexer.Peek();
                    if (token.Type == TokenType.SignLeftParenthesis)
                    {
                        lexer.Back();
                        lexer.Back();
                        netStatement = new NetStatementObjectFunction();
                        netStatement.Begin(lexer);
                        break;
                    }
                    else if (token.Type == TokenType.SignEuality)
                    {
                        lexer.Back();
                        lexer.Back();
                        netStatement = new NetStatementObject();
                        netStatement.Begin(lexer);
                        break;
                    }
                    throw new SyntacticException(token, "不支持的关键字", CBEexpressExCode.ERRORCODE_11288);
                 case TokenType.SignSemicolon:
                    netStatement = new NetStatementEmpty();
                    netStatement.Begin(lexer);
                    break; 
                default:
                    throw new SyntacticException(token, "不支持的关键字", CBEexpressExCode.ERRORCODE_11235);
            }

            return netStatement;
        }
    }

    public class NetParser
    {
        public NetParser()
        {
            varStack.Add(rootvarlist);
        }
        public string File { get; set; }
        public object Entity { get; set; }
        public NetDebug debug { get; set; }
        NetVarCollection varStack = new NetVarCollection();
        NetSkip netInterrupt = new NetSkip();
        ICBContext cbcontext = new CBContext();
        NetVarList rootvarlist = new NetVarList("ROOT");
        public object Check(string txt)
        {
            TokenPool tokenPool = GetTokenPool(txt);
            object result = null;
            try
            {
                result = Check(tokenPool);
            }
            catch (SyntacticException ex)
            {
                string msg = ex.Message + "\r\n" + GetLine(txt, ex.Token.EndIndex);
                ex.SetMsg(msg);
                throw ex;
            }
            return result;
        }
        public void Debug(string txt)
        {
            debug = new NetDebug();
            debug.SetCommand(DebugCommand.Pause);
            System.Threading.Thread thread = new System.Threading.Thread(Run);
            thread.IsBackground = true;
            thread.Start(txt);
        }
        private void Run(object obj)
        {
            try
            {
                object value = Exec(obj.ToString());
            }
            catch (Exception ex)
            {
                debug.OnDebugExceptionEvent(ex);
            }
        }
        public object Exec(string txt)
        {
            string express = txt;
            object result = null;
            try
            {
                TokenPool tokenPool = GetTokenPool(express);
                result = Exec(tokenPool);
            }
            catch (SyntacticException ex)
            {
                string linetxt = string.Empty;
                if (ex.Token!=null)
                {
                    linetxt= GetLine(express, ex.Token.EndIndex);
                }
                string msg = ex.Message + "\r\n" + linetxt;
                ex.SetMsg(msg);
                throw ex;
            }
            return result;
        }
        public object ExecExpress(string txt)
        {
            object result = null;
            string express = string.Empty;
            try
            {
                express = "var netresult=" + txt + ";";
                TokenPool tokenPool = GetTokenPool(express);
                NetStatementVar statementVar = NetStatementBuilder.Build(tokenPool) as NetStatementVar;
                if (statementVar == null)
                    throw new SyntacticException(null, "语句不正确", CBEexpressExCode.ERRORCODE_11231);
                cbcontext.Clear();
                cbcontext.Entity = this.Entity;
                cbcontext.File = this.File;
                cbcontext.Debug = this.debug;
                statementVar.Exec(varStack, netInterrupt, cbcontext);
                result = varStack.GetVarValue("netresult", null);
            }
            catch (SyntacticException ex)
            {
                string msg = ex.Message + "\r\n" + GetLine(express, ex.Token.EndIndex);
                ex.SetMsg(msg);
                throw ex;
            }
            return result;
        }


        private object Exec(TokenPool tokenPool)
        {
            List<NetStatementBase> list = NetStatementBuilder.GetNetStatements(tokenPool);

            cbcontext.Clear();
            cbcontext.Entity = this.Entity;
            cbcontext.File = this.File;
            cbcontext.Debug = this.debug;
            foreach (NetStatementBase item in list)
            {
                NetStatementFunction fun = item as NetStatementFunction;
                if (fun == null)
                    continue;
                cbcontext.AddFunction(fun);
            }
            netInterrupt.CanReturn = true;
            for (int i = 0; i < list.Count; i++)
            {
                NetStatementBase netStatement = list[i];
                try
                {
                    netStatement.Exec(varStack, netInterrupt, cbcontext);
                    if (netInterrupt.Return)
                    {
                        return netInterrupt.ReturnValue;
                    }
                }
                catch (SyntacticException ex)
                {
                    throw new CBException(ex.Message+"\r\n"+ netStatement.ToString(),ex.ErrorCode, ex);
                }
            }

            object result = null;
            if (varStack.HasVarValue("netresult"))
            {
                result = varStack.GetVarValue("netresult", null);
            }
            return result;
        }
        public object ExecEvent(string script, string eventname, List<object> args)
        {
            object result = null;
            try
            {
                TokenPool tokenPool = GetTokenPool(script);
                List<NetStatementBase> list = NetStatementBuilder.GetNetStatements(tokenPool);
 
                cbcontext.Clear();
                cbcontext.Entity = this.Entity;
                cbcontext.File = this.File;

                foreach (NetStatementBase item in list)
                {
                    NetStatementFunction fun = item as NetStatementFunction;
                    if (fun == null)
                        continue;
                    cbcontext.AddFunction(fun);
                }
                bool findfunction = false;
                for (int i = 0; i < list.Count; i++)
                {
                    NetStatementBase netStatement = list[i];
                    NetStatementFunction netStatementFunction = netStatement as NetStatementFunction;
                    if (netStatementFunction == null)
                        continue;
                    string functionname = netStatementFunction.GetFunctionName();
                    if (functionname != eventname)
                        continue;
                    findfunction = true;
                    result = netStatementFunction.Run(varStack, cbcontext, args);
                    break;
                }
                if (!findfunction)
                {
                    throw new Exception("未找到函数:" + eventname);
                }
                if (varStack.HasVarValue("netresult"))
                {
                    result = varStack.GetVarValue("netresult", null);
                }
            }
            catch (SyntacticException ex)
            {
                string msg = ex.Message + "\r\n" + GetLine(script, ex.Token.EndIndex);
                ex.SetMsg(msg);
                throw ex;
            }
            return result;
        }
        public TokenPool GetTokenPool(string txt)
        {
            Lexer lexer = new Lexer(txt);
            lexer.Parse();
            return lexer.TokenPool;
        }


        private object Check(TokenPool tokenPool)
        {
            object value = null;
            List<NetStatementBase> list = NetStatementBuilder.GetNetStatements(tokenPool);
            return value;
        }

        public void AddFunction(IMethod methodContainer)
        {
            cbcontext.AddFunction(methodContainer);
        }

        public void AddVar(string name,object value)
        {
            this.rootvarlist.SetValue(name,value);
        }
        public static string GetLine(string txt, int index)
        {
            try
            {
                int begin = 0;
                for (int i = index-1; i >= 0; i--)
                {
                    if (txt[i] == '\n')
                    {
                        begin = i;
                        break;
                    }
                }
                int end = index;
                for (int i = index; i < txt.Length; i++)
                {
                    if (txt[i] == '\n')
                    {
                        end = i;
                        break;
                    }
                }
                string msg = txt.Substring(begin, end - begin).Trim();
                return msg;
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }
        public void Inited()
        {
            INetParserInit netParserInit = cbcontext as INetParserInit;
            if (netParserInit != null)
            {
                netParserInit.Init(varStack, cbcontext);
            }
        }
    }

    public interface INetParserInit
    {
        void Init(NetVarCollection varStack, ICBContext methodProxy);
    }
}
