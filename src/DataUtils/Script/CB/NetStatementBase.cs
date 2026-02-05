
using Feng.Script.Method;
using System;
using System.Collections.Generic;

namespace Feng.Script.CB
{
     

    public class NetStatementBase
    {
        public int Type { get; set; }
        public virtual void Begin(TokenPool lexer)
        {
        }

        public virtual void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {

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
        public void SetValue(string key, object value)
        {
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

    public class NetContext
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

        public bool HasVarValue(string varname)
        {
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
        public object GetVarValue(string varname, Token token)
        {
            for (int i = stackList.Count - 1; i >= 0; i--)
            {
                NetVarList varlist = stackList[i];
                if (varlist.Has(varname))
                {
                    return varlist.GetValue(varname);
                }
            }
            throw new SyntacticException(token, "不存在此变量:" + varname + " ", CBEexpressExCode.ERRORCODE_11113);
        }

        public void SetVarValue(string varname, object value)
        {
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

    public class NetStatementFor : NetStatementBase
    {
        private NetStatementBase initstatement;
        private NetExpressBase ifstatement;
        private NetExpressBase addstatement;
        private List<NetStatementBase> NetStatements;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_FOR)
                throw new SyntacticException(token, "语法不正确缺少关键字for", CBEexpressExCode.ERRORCODE_11115);

            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftParenthesis)
                throw new SyntacticException(token, "语法不正确缺少左括号", CBEexpressExCode.ERRORCODE_11116);

            token = lexer.Peek();
            if (token.Type == TokenType.Key_VAR)
            {
                initstatement = new NetStatementVar();
                initstatement.Begin(lexer);
            }
            else if (token.Type == TokenType.ID)
            {
                initstatement = new NetStatementID();
                initstatement.Begin(lexer);
            }
            else if (token.Type == TokenType.SignSemicolon)
            {
                initstatement = new NetStatementID();
            }
            else
            {
                throw new SyntacticException(token, "语法不正确缺少初始化表达", CBEexpressExCode.ERRORCODE_11117);
            }
            ifstatement = NetExpressBuilder.Build(lexer, TokenType.SignSemicolon);
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少分号 ;", CBEexpressExCode.ERRORCODE_11118);

            addstatement = NetExpressBuilder.Build(lexer, TokenType.SignRightParenthesis);

            token = lexer.Pop();
            if (token.Type != TokenType.SignRightParenthesis)
                throw new SyntacticException(token, "语法不正确缺少分号)", CBEexpressExCode.ERRORCODE_11119);
            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftBRACE)
                throw new SyntacticException(token, "语法不正确缺少左大括号{", CBEexpressExCode.ERRORCODE_11121);
            NetStatements = new List<NetStatementBase>();
            while (true)
            {
                token = lexer.Peek();
                if (token.Type == TokenType.Key_FUNCTION)
                    throw new SyntacticException(token, "语法不正确不能嵌套函数", CBEexpressExCode.ERRORCODE_11122);

                if (token.Type == TokenType.SignRightBRACE)
                    break;
                NetStatementBase netStatement = NetStatementBuilder.Build(lexer);
                NetStatements.Add(netStatement);
                token = lexer.Peek();
                if (token.Type == TokenType.SignRightBRACE)
                    break;
            }
            token = lexer.Pop();
            if (token.Type != TokenType.SignRightBRACE)
                throw new SyntacticException(token, "语法不正确缺少右大括号}", CBEexpressExCode.ERRORCODE_11123);
        }

        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            NetVarList netVarList = new NetVarList("FOR");
            try
            {
                netInterrupt.CanBreak = true;
                varStack.Add(netVarList);
                initstatement.Exec(varStack, netInterrupt, methodProxy);
                object value = ifstatement.Exec(varStack, methodProxy);
                bool res = Feng.Utils.ConvertHelper.ToBoolean(value);
                while (res)
                {
                    foreach (NetStatementBase item in NetStatements)
                    {
                        item.Exec(varStack, netInterrupt, methodProxy);
                        if (netInterrupt.Break || netInterrupt.Continue || netInterrupt.Return)
                            break;
                    }

                    netInterrupt.Continue = false;
                    if (netInterrupt.Break || netInterrupt.Return)
                        break;
                    addstatement.Exec(varStack, methodProxy);
                    value = ifstatement.Exec(varStack, methodProxy);
                    res = Feng.Utils.ConvertHelper.ToBoolean(value);
                }
            }
            finally
            {
                netInterrupt.CanBreak = false;
                netInterrupt.Break = false;
                netInterrupt.Continue = false;
                varStack.Remove(netVarList);
            }
        }
    }
    public class NetStatementFunction : NetStatementBase
    {
        public NetStatementFunction()
        {
            arglist = new List<Token>();
        }
        private Token functioname;
        private List<Token> arglist;
        private List<NetStatementBase> NetStatements;
        public virtual string GetFunctionName()
        {
            return this.functioname.Value;
        }
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_FUNCTION)
                throw new SyntacticException(token, "语法不正确缺少关键了function", CBEexpressExCode.ERRORCODE_11124);

            token = lexer.Pop();
            if (token.Type != TokenType.ID)
                throw new SyntacticException(token, "语法不正确缺少函数名称", CBEexpressExCode.ERRORCODE_11125);
            functioname = token;

            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftParenthesis)
                throw new SyntacticException(token, "语法不正确缺少左括号", CBEexpressExCode.ERRORCODE_11126);


            while (true)
            {
                token = lexer.Peek();
                if (token.Type == TokenType.SignRightParenthesis)
                    break;
                if (!(token.Type == TokenType.Key_VAR || token.Type == TokenType.Key_Object))
                    throw new SyntacticException(token, "语法不正确缺少关键字var或者object", CBEexpressExCode.ERRORCODE_11127);
                token = lexer.Pop();
                token = lexer.Peek();
                if (token.Type != TokenType.ID)
                    throw new SyntacticException(token, "语法不正确缺少参数名称", CBEexpressExCode.ERRORCODE_11128);
                token = lexer.Pop();
                arglist.Add(token);
                token = lexer.Peek();
                if (token.Type == TokenType.SignRightParenthesis)
                    break;
                if (token.Type != TokenType.SignComma)
                    throw new SyntacticException(token, "语法不正确缺少逗号,", CBEexpressExCode.ERRORCODE_11129);
                token = lexer.Pop();
            }
            token = lexer.Pop();
            if (token.Type != TokenType.SignRightParenthesis)
                throw new SyntacticException(token, "语法不正确缺右括号)", CBEexpressExCode.ERRORCODE_11131);

            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftBRACE)
                throw new SyntacticException(token, "语法不正确缺左大括号{", CBEexpressExCode.ERRORCODE_11132);
            NetStatements = new List<NetStatementBase>();
            while (true)
            {
                token = lexer.Peek();
                if (token.Type == TokenType.Key_FUNCTION)
                    throw new SyntacticException(token, "语法不正确不能内嵌函数", CBEexpressExCode.ERRORCODE_11133);
                NetStatementBase netStatement = NetStatementBuilder.Build(lexer);

                NetStatements.Add(netStatement);
                token = lexer.Peek();
                if (token.Type == TokenType.SignRightBRACE)
                    break;
            }
            token = lexer.Pop();
            if (token.Type != TokenType.SignRightBRACE)
                throw new SyntacticException(token, "语法不正确缺右大括号}", CBEexpressExCode.ERRORCODE_11134);
        }
        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            methodProxy.AddFunction(functioname.Value, this);
        }
        public virtual object Run(NetVarCollection varStack, IMethodProxy methodProxy, List<object> paramteres)
        {
            NetContext netInterrupt = new NetContext();
            NetVarList netVarList = new NetVarList("FUNCTION");
            try
            {
                netInterrupt.CanReturn = true;
                varStack.Add(netVarList);
                for (int i = 0; i < arglist.Count; i++)
                {
                    Token token = arglist[i];
                    if (i < paramteres.Count)
                    {
                        netVarList.SetValue(token.Value, paramteres[i]);
                    }
                    else
                    {
                        netVarList.SetValue(token.Value, ArgsNull.DBNULL);
                    }
                }
                foreach (NetStatementBase item in NetStatements)
                {
                    item.Exec(varStack, netInterrupt, methodProxy);
                    if (netInterrupt.Return)
                        break;
                }
            }
            finally
            {
                netInterrupt.CanReturn = false;
                netInterrupt.Return = false;
                varStack.Remove(netVarList);
            }
            return netInterrupt.ReturnValue;
        }
    }
    public class NetStatementObjectFunction : NetStatementFunction
    {
        public NetStatementObjectFunction()
        {
            arglist = new List<Token>();
        }
        private Token functioname;
        private List<Token> arglist;
        private List<NetStatementBase> NetStatements;
        public override string GetFunctionName()
        {
            return this.functioname.Value;
        }
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_Object)
                throw new SyntacticException(token, "语法不正确缺少关键了object", CBEexpressExCode.ERRORCODE_11237);

            token = lexer.Pop();
            if (token.Type != TokenType.ID)
                throw new SyntacticException(token, "语法不正确缺少函数名称", CBEexpressExCode.ERRORCODE_11238);
            functioname = token;

            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftParenthesis)
                throw new SyntacticException(token, "语法不正确缺少左括号", CBEexpressExCode.ERRORCODE_11239);


            while (true)
            {
                token = lexer.Peek();
                if (token.Type == TokenType.SignRightParenthesis)
                    break;
                if (!(token.Type == TokenType.Key_VAR || token.Type == TokenType.Key_Object))
                    throw new SyntacticException(token, "语法不正确缺少关键字var或者object", CBEexpressExCode.ERRORCODE_11251);
                token = lexer.Pop();
                token = lexer.Peek();
                if (token.Type != TokenType.ID)
                    throw new SyntacticException(token, "语法不正确缺少参数名称", CBEexpressExCode.ERRORCODE_11252);
                token = lexer.Pop();
                arglist.Add(token);
                token = lexer.Peek();
                if (token.Type == TokenType.SignRightParenthesis)
                    break;
                if (token.Type != TokenType.SignComma)
                    throw new SyntacticException(token, "语法不正确缺少逗号,", CBEexpressExCode.ERRORCODE_11253);
                token = lexer.Pop();
            }
            token = lexer.Pop();
            if (token.Type != TokenType.SignRightParenthesis)
                throw new SyntacticException(token, "语法不正确缺右括号)", CBEexpressExCode.ERRORCODE_11254);

            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftBRACE)
                throw new SyntacticException(token, "语法不正确缺左大括号{", CBEexpressExCode.ERRORCODE_11255);
            NetStatements = new List<NetStatementBase>();
            while (true)
            {
                token = lexer.Peek();
                if (token.Type == TokenType.Key_FUNCTION)
                    throw new SyntacticException(token, "语法不正确不能内嵌函数", CBEexpressExCode.ERRORCODE_11256);
                NetStatementBase netStatement = NetStatementBuilder.Build(lexer);

                NetStatements.Add(netStatement);
                token = lexer.Peek();
                if (token.Type == TokenType.SignRightBRACE)
                    break;
            }
            token = lexer.Pop();
            if (token.Type != TokenType.SignRightBRACE)
                throw new SyntacticException(token, "语法不正确缺右大括号}", CBEexpressExCode.ERRORCODE_11257);
        }
        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            methodProxy.AddFunction(functioname.Value, this);
        }
        public override object Run(NetVarCollection varStack, IMethodProxy methodProxy, List<object> paramteres)
        {
            NetContext netInterrupt = new NetContext();
            NetVarList netVarList = new NetVarList("FUNCTION");
            try
            {
                netInterrupt.CanReturn = true;
                varStack.Add(netVarList);
                for (int i = 0; i < arglist.Count; i++)
                {
                    Token token = arglist[i];
                    if (i < paramteres.Count)
                    {
                        netVarList.SetValue(token.Value, paramteres[i]);
                    }
                    else
                    {
                        netVarList.SetValue(token.Value, ArgsNull.DBNULL);
                    }
                }
                foreach (NetStatementBase item in NetStatements)
                {
                    item.Exec(varStack, netInterrupt, methodProxy);
                    if (netInterrupt.Return)
                        break;
                }
            }
            finally
            {
                netInterrupt.CanReturn = false;
                netInterrupt.Return = false;
                varStack.Remove(netVarList);
            }
            return netInterrupt.ReturnValue;
        }
    }

    public class NetStatementForeach : NetStatementBase
    {
        private Token foreachitem;
        private Token foreachlist;
        private List<NetStatementBase> NetStatements;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_FOREACH)
                throw new SyntacticException(token, "语法不正确缺少关键字foreach", CBEexpressExCode.ERRORCODE_11136);

            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftParenthesis)
                throw new SyntacticException(token, "语法不正确缺少左括号(", CBEexpressExCode.ERRORCODE_11137);

            token = lexer.Peek();
            if (token.Type != TokenType.Key_VAR)
                throw new SyntacticException(token, "语法不正确缺少关键字var", CBEexpressExCode.ERRORCODE_11138);

            token = lexer.Pop();
            if (token.Type != TokenType.ID)
                throw new SyntacticException(token, "语法不正确缺少变量", CBEexpressExCode.ERRORCODE_11139);

            foreachitem = token;
            token = lexer.Pop();
            if (token.Type != TokenType.Key_IN)
                throw new SyntacticException(token, "语法不正确缺少关键字in", CBEexpressExCode.ERRORCODE_11151);

            token = lexer.Pop();
            if (token.Type != TokenType.ID)
                throw new SyntacticException(token, "语法不正确缺少集合变量", CBEexpressExCode.ERRORCODE_11152);
            foreachlist = token;
            token = lexer.Pop();
            if (token.Type != TokenType.SignRightParenthesis)
                throw new SyntacticException(token, "语法不正确缺少右括号)", CBEexpressExCode.ERRORCODE_11153);
            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftBRACE)
                throw new SyntacticException(token, "语法不正确缺少左大括号{", CBEexpressExCode.ERRORCODE_11154);
            NetStatements = new List<NetStatementBase>();
            while (true)
            {
                token = lexer.Peek();
                if (token.Type == TokenType.Key_FUNCTION)
                    throw new SyntacticException(token, "语法不正确不能嵌套函数", CBEexpressExCode.ERRORCODE_11155);
                NetStatementBase netStatement = NetStatementBuilder.Build(lexer);

                NetStatements.Add(netStatement);
                token = lexer.Peek();
                if (token.Type == TokenType.SignRightBRACE)
                    break;
            }
            token = lexer.Pop();
            if (token.Type != TokenType.SignRightBRACE)
                throw new SyntacticException(token, "语法不正确缺少右大括号}", CBEexpressExCode.ERRORCODE_11156);
        }

        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            NetVarList netVarList = new NetVarList("FOREACH");
            try
            {
                netInterrupt.CanBreak = true;
                varStack.Add(netVarList);
                System.Collections.IEnumerable collection = varStack.GetVarValue(foreachlist.Value, foreachlist) as System.Collections.IEnumerable;
                foreach (var itemvalue in collection)
                {
                    netVarList.SetValue(foreachitem.Value, itemvalue);
                    foreach (NetStatementBase item in NetStatements)
                    {
                        item.Exec(varStack, netInterrupt, methodProxy);
                        if (netInterrupt.Break || netInterrupt.Continue || netInterrupt.Return)
                            break;
                    }

                    netInterrupt.Continue = false;
                    if (netInterrupt.Break || netInterrupt.Return)
                        break;
                }
            }
            finally
            {
                netInterrupt.CanBreak = false;
                netInterrupt.Break = false;
                netInterrupt.Continue = false;
                varStack.Remove(netVarList);
            }
        }
    }

    public class NetStatementIF : NetStatementBase
    {
        public NetStatementIF()
        {
            netStatementElseIFes = new List<NetStatementElseIF>();
        }
        private NetStatementIFChild netStatementIF;
        private List<NetStatementElseIF> netStatementElseIFes;
        private NetStatementElse netStatementElse;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Peek();
            if (token.Type != TokenType.Key_IF)
                throw new SyntacticException(token, "语法不正确缺少关键字if", CBEexpressExCode.ERRORCODE_11135);
            netStatementIF = new NetStatementIFChild();
            netStatementIF.Begin(lexer);
            while (true)
            {
                token = lexer.Peek();
                if (token.Type == TokenType.Key_ELSE)
                {
                    token = lexer.Pop();
                    token = lexer.Peek();
                    if (token.Type == TokenType.Key_IF)
                    {
                        NetStatementElseIF netStatementElseIF = new NetStatementElseIF();
                        netStatementElseIF.Begin(lexer);
                        netStatementElseIFes.Add(netStatementElseIF);
                        continue;
                    }
                    else
                    {
                        lexer.Back();
                        netStatementElse = new NetStatementElse();
                        netStatementElse.Begin(lexer);
                        break;
                    }
                }
                break;
            }
        }

        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            NetVarList netVarList = new NetVarList("IF");
            try
            {
                varStack.Add(netVarList);
                netInterrupt.IF = false;
                netStatementIF.Exec(varStack, netInterrupt, methodProxy);
                if (netInterrupt.IF)
                    return;
                foreach (NetStatementElseIF item in netStatementElseIFes)
                {
                    item.Exec(varStack, netInterrupt, methodProxy);
                    if (netInterrupt.IF)
                        return;
                    if (netInterrupt.Break || netInterrupt.Continue || netInterrupt.Return)
                        return;
                }
                if (netStatementElse != null)
                {
                    netStatementElse.Exec(varStack, netInterrupt, methodProxy);
                }
            }
            finally
            {
                varStack.Remove(netVarList);
            }
        }
    }
    public class NetStatementIFChild : NetStatementBase
    {
        private NetExpressBase ifstatement;
        private List<NetStatementBase> NetStatements;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_IF)
                throw new SyntacticException(token, "语法不正确缺少关键字if", CBEexpressExCode.ERRORCODE_11171);

            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftParenthesis)
                throw new SyntacticException(token, "语法不正确缺少左括号(", CBEexpressExCode.ERRORCODE_11172);

            ifstatement = NetExpressBuilder.Build(lexer, TokenType.SignRightParenthesis);

            token = lexer.Pop();
            if (token.Type != TokenType.SignRightParenthesis)
                throw new SyntacticException(token, "语法不正确缺少右括号)", CBEexpressExCode.ERRORCODE_11173);
            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftBRACE)
                throw new SyntacticException(token, "语法不正确缺少左大括号{", CBEexpressExCode.ERRORCODE_11174);
            NetStatements = new List<NetStatementBase>();
            while (true)
            {
                token = lexer.Peek();
                if (token.Type == TokenType.Key_FUNCTION)
                    throw new SyntacticException(token, "语法不正确不能内嵌函数", CBEexpressExCode.ERRORCODE_11169);
                NetStatementBase netStatement = NetStatementBuilder.Build(lexer);

                NetStatements.Add(netStatement);
                token = lexer.Peek();
                if (token.Type == TokenType.SignRightBRACE)
                    break;
            }
            token = lexer.Pop();
            if (token.Type != TokenType.SignRightBRACE)
                throw new SyntacticException(token, "语法不正确缺少右大括号}", CBEexpressExCode.ERRORCODE_11168);
        }
        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            NetVarList netVarList = new NetVarList("IF");
            try
            {
                varStack.Add(netVarList);
                object value = ifstatement.Exec(varStack, methodProxy);
                bool res = Feng.Utils.ConvertHelper.ToBoolean(value);
                if (res)
                {
                    foreach (NetStatementBase item in NetStatements)
                    {
                        item.Exec(varStack, netInterrupt, methodProxy);
                        if (netInterrupt.Break || netInterrupt.Continue || netInterrupt.Return)
                            return;
                    }
                    netInterrupt.IF = true;
                }
            }
            finally
            {
                varStack.Remove(netVarList);
            }
        }
    }
    public class NetStatementElseIF : NetStatementBase
    {
        private NetExpressBase ifstatement;
        private List<NetStatementBase> NetStatements;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_IF)
                throw new SyntacticException(token, "语法不正确缺少关键字 else if", CBEexpressExCode.ERRORCODE_11157);

            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftParenthesis)
                throw new SyntacticException(token, "语法不正确缺少左括号(", CBEexpressExCode.ERRORCODE_11158);

            ifstatement = NetExpressBuilder.Build(lexer, TokenType.SignRightParenthesis);

            token = lexer.Pop();
            if (token.Type != TokenType.SignRightParenthesis)
                throw new SyntacticException(token, "语法不正确缺少右括号)", CBEexpressExCode.ERRORCODE_11159);
            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftBRACE)
                throw new SyntacticException(token, "语法不正确缺少左大括号{", CBEexpressExCode.ERRORCODE_11161);
            NetStatements = new List<NetStatementBase>();
            while (true)
            {
                token = lexer.Peek();
                if (token.Type == TokenType.Key_FUNCTION)
                    throw new SyntacticException(token, "语法不正确不能内嵌函数", CBEexpressExCode.ERRORCODE_11162);
                NetStatementBase netStatement = NetStatementBuilder.Build(lexer);

                NetStatements.Add(netStatement);
                token = lexer.Peek();
                if (token.Type == TokenType.SignRightBRACE)
                    break;
            }
            token = lexer.Pop();
            if (token.Type != TokenType.SignRightBRACE)
                throw new SyntacticException(token, "语法不正确缺少右大括号}", CBEexpressExCode.ERRORCODE_11163);
        }

        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            NetVarList netVarList = new NetVarList("ELSE IF");
            try
            {
                varStack.Add(netVarList);
                object value = ifstatement.Exec(varStack, methodProxy);
                bool res = Feng.Utils.ConvertHelper.ToBoolean(value);
                if (res)
                {
                    foreach (NetStatementBase item in NetStatements)
                    {
                        item.Exec(varStack, netInterrupt, methodProxy);
                        if (netInterrupt.Break || netInterrupt.Continue || netInterrupt.Return)
                            return;
                    }
                    netInterrupt.IF = true;
                }
            }
            finally
            {
                varStack.Remove(netVarList);
            }
        }
    }
    public class NetStatementElse : NetStatementBase
    {
        private List<NetStatementBase> NetStatements;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_ELSE)
                throw new SyntacticException(token, "语法不正确缺少关键字else", CBEexpressExCode.ERRORCODE_11164);

            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftBRACE)
                throw new SyntacticException(token, "语法不正确缺少左大括号{", CBEexpressExCode.ERRORCODE_11165);
            NetStatements = new List<NetStatementBase>();
            while (true)
            {
                token = lexer.Peek();
                if (token.Type == TokenType.Key_FUNCTION)
                    throw new SyntacticException(token, "语法不正确不能内嵌函数", CBEexpressExCode.ERRORCODE_11166);
                NetStatementBase netStatement = NetStatementBuilder.Build(lexer);

                NetStatements.Add(netStatement);
                token = lexer.Peek();
                if (token.Type == TokenType.SignRightBRACE)
                    break;
            }
            token = lexer.Pop();
            if (token.Type != TokenType.SignRightBRACE)
                throw new SyntacticException(token, "语法不正确缺少右大括号}", CBEexpressExCode.ERRORCODE_11167);
        }

        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            NetVarList netVarList = new NetVarList("ELSE");
            try
            {
                varStack.Add(netVarList);
                foreach (NetStatementBase item in NetStatements)
                {
                    item.Exec(varStack, netInterrupt, methodProxy);
                    if (netInterrupt.Break || netInterrupt.Continue || netInterrupt.Return)
                        return;
                }

            }
            finally
            {
                varStack.Remove(netVarList);
            }
        }
    }

    public class NetStatementWhile : NetStatementBase
    {
        private NetExpressBase ifstatement;
        private List<NetStatementBase> NetStatements;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_WHILE)
                throw new SyntacticException(token, "语法不正确缺少关键字while", CBEexpressExCode.ERRORCODE_11175);

            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftParenthesis)
                throw new SyntacticException(token, "语法不正确缺少左括号(", CBEexpressExCode.ERRORCODE_11176);

            ifstatement = NetExpressBuilder.Build(lexer, TokenType.SignRightParenthesis);

            token = lexer.Pop();
            if (token.Type != TokenType.SignRightParenthesis)
                throw new SyntacticException(token, "语法不正确缺少右括号)", CBEexpressExCode.ERRORCODE_11177);
            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftBRACE)
                throw new SyntacticException(token, "语法不正确缺少左大括号{", CBEexpressExCode.ERRORCODE_11178);
            NetStatements = new List<NetStatementBase>();
            while (true)
            {
                token = lexer.Peek();
                if (token.Type == TokenType.Key_FUNCTION)
                    throw new SyntacticException(token, "不能内嵌函数", CBEexpressExCode.ERRORCODE_11179);


                NetStatementBase netStatement = NetStatementBuilder.Build(lexer);
                NetStatements.Add(netStatement);
                token = lexer.Peek();
                if (token.Type == TokenType.SignRightBRACE)
                    break;
            }
            token = lexer.Pop();
            if (token.Type != TokenType.SignRightBRACE)
                throw new SyntacticException(token, "语法不正确缺少右大括号}", CBEexpressExCode.ERRORCODE_11181);
        }

        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            NetVarList netVarList = new NetVarList("WHILE");
            try
            {
                netInterrupt.CanBreak = true;
                varStack.Add(netVarList);
                object value = ifstatement.Exec(varStack, methodProxy);
                bool res = Feng.Utils.ConvertHelper.ToBoolean(value);
                while (res)
                {
                    foreach (NetStatementBase item in NetStatements)
                    {
                        item.Exec(varStack, netInterrupt, methodProxy);
                        if (netInterrupt.Break || netInterrupt.Continue || netInterrupt.Return)
                            break;
                    }

                    netInterrupt.Continue = false;
                    if (netInterrupt.Break || netInterrupt.Return)
                        break;
                    value = ifstatement.Exec(varStack, methodProxy);
                }
            }
            finally
            {
                netInterrupt.CanBreak = false;
                netInterrupt.Break = false;
                netInterrupt.Continue = false;
                varStack.Remove(netVarList);
            }
        }
    }

    public class NetStatementTry : NetStatementBase
    {
        private Token TokenEx = null;
        private List<NetStatementBase> NetStatementsTry;
        private List<NetStatementBase> NetStatementsCatch;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_Try)
                throw new SyntacticException(token, "语法不正确缺少关键字try", CBEexpressExCode.ERRORCODE_11266);

            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftBRACE)
                throw new SyntacticException(token, "语法不正确缺少左大括号{", CBEexpressExCode.ERRORCODE_11267);
            NetStatementsTry = new List<NetStatementBase>();
            while (true)
            {
                token = lexer.Peek();
                if (token.Type == TokenType.Key_FUNCTION)
                    throw new SyntacticException(token, "不能内嵌函数", CBEexpressExCode.ERRORCODE_11268);


                NetStatementBase netStatement = NetStatementBuilder.Build(lexer);
                NetStatementsTry.Add(netStatement);
                token = lexer.Peek();
                if (token.Type == TokenType.SignRightBRACE)
                    break;
            }
            token = lexer.Pop();
            if (token.Type != TokenType.SignRightBRACE)
                throw new SyntacticException(token, "语法不正确缺少右大括号}", CBEexpressExCode.ERRORCODE_11269);

            token = lexer.Pop();
            if (token.Type != TokenType.Key_Catch)
                throw new SyntacticException(token, "语法不正确缺少关键字catch", CBEexpressExCode.ERRORCODE_11271);


            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftParenthesis)
                throw new SyntacticException(token, "语法不正确缺少左括号(", CBEexpressExCode.ERRORCODE_11272);


            token = lexer.Pop();
            if (token.Type != TokenType.ID)
                throw new SyntacticException(token, "语法不正确缺少异常变量ex", CBEexpressExCode.ERRORCODE_11273);
            TokenEx = token;
            token = lexer.Pop();
            if (token.Type != TokenType.SignRightParenthesis)
                throw new SyntacticException(token, "语法不正确缺少右括号)", CBEexpressExCode.ERRORCODE_11274);

            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftBRACE)
                throw new SyntacticException(token, "语法不正确缺少左大括号{", CBEexpressExCode.ERRORCODE_11275);
            NetStatementsCatch = new List<NetStatementBase>();
            while (true)
            {
                token = lexer.Peek();
                if (token.Type == TokenType.Key_FUNCTION)
                    throw new SyntacticException(token, "不能内嵌函数", CBEexpressExCode.ERRORCODE_11276);
                if (token.Type == TokenType.SignRightBRACE)
                    break;

                NetStatementBase netStatement = NetStatementBuilder.Build(lexer);
                NetStatementsCatch.Add(netStatement);
                token = lexer.Peek();
                if (token.Type == TokenType.SignRightBRACE)
                    break;
            }
            token = lexer.Pop();
            if (token.Type != TokenType.SignRightBRACE)
                throw new SyntacticException(token, "语法不正确缺少右大括号}", CBEexpressExCode.ERRORCODE_11277);

        }

        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            NetVarList netVarList = new NetVarList("Try");
            varStack.Add(netVarList);
            try
            {
                foreach (NetStatementBase item in NetStatementsTry)
                {
                    item.Exec(varStack, netInterrupt, methodProxy);
                    if (netInterrupt.Return)
                        break;
                }

                netInterrupt.Continue = false;
            }
            catch (Exception ex)
            {
                varStack.Remove(netVarList);
                netVarList = new NetVarList("Catch");
                netVarList.SetValue(TokenEx.Value, ex);
                varStack.Add(netVarList);
                try
                {
                    foreach (NetStatementBase item in NetStatementsCatch)
                    {
                        item.Exec(varStack, netInterrupt, methodProxy);
                        if (netInterrupt.Return)
                            break;
                    }
                }
                finally
                {
                    varStack.Remove(netVarList);
                }
            }
        }
    }
    public class NetStatementVar : NetStatementBase
    {
        private NetExpressBase netExpress = null;
        private Token varname = null;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_VAR)
                throw new SyntacticException(token, "语法不正确缺少关键字var", CBEexpressExCode.ERRORCODE_11182);

            token = lexer.Pop();
            if (token.Type != TokenType.ID)
                throw new SyntacticException(token, "语法不正确缺少变量名", CBEexpressExCode.ERRORCODE_11183);
            varname = token;

            token = lexer.Pop();
            if (token.Type != TokenType.SignEuality)
            {
                if (token.Type == TokenType.SignSemicolon)
                {
                    return;
                }
                throw new SyntacticException(token, "语法不正确缺少等号=", CBEexpressExCode.ERRORCODE_11185);
            }
            netExpress = NetExpressBuilder.Build(lexer, TokenType.SignSemicolon);
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号;", CBEexpressExCode.ERRORCODE_11186);
        }

        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            object value = netExpress.Exec(varStack, methodProxy);
            varStack.SetVarValue(varname.Value, value);
        }

        public override string ToString()
        {
            return "var " + varname.Value + "=" + netExpress.ToString();
        }
    }

    public class NetStatementID : NetStatementBase
    {
        private NetExpressBase netExpress = null;
        private Token name = null;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.ID)
                throw new SyntacticException(token, "语法不正确缺少变量名", CBEexpressExCode.ERRORCODE_11187);
            name = token;

            token = lexer.Pop();
            if (token.Type != TokenType.SignEuality)
                throw new SyntacticException(token, "语法不正确缺少等号", CBEexpressExCode.ERRORCODE_11188);
            netExpress = NetExpressBuilder.Build(lexer, TokenType.SignSemicolon);
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号;", CBEexpressExCode.ERRORCODE_11189);
        }
        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            object value = netExpress.Exec(varStack, methodProxy);
            varStack.SetVarValue(name.Value, value);
        }
    }

    public class NetStatementIncrement : NetStatementBase
    {
        private Token name = null;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.ID)
                throw new SyntacticException(token, "语法不正确缺少变量名", CBEexpressExCode.ERRORCODE_11191);
            name = token;

            token = lexer.Pop();
            if (token.Type != TokenType.SignIncrement)
                throw new SyntacticException(token, "语法不正确缺少自增符号++", CBEexpressExCode.ERRORCODE_11192);
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号;", CBEexpressExCode.ERRORCODE_11193);
        }
        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            object value = varStack.GetVarValue(name.Value, name);
            value = methodProxy.netOperatorProxy.Increment(value);
            varStack.SetVarValue(name.Value, value);
        }
    }
    public class NetStatementDecrement : NetStatementBase
    {
        private Token name = null;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.ID)
                throw new SyntacticException(token, "语法不正确缺少变量名", CBEexpressExCode.ERRORCODE_11195);
            name = token;

            token = lexer.Pop();
            if (token.Type != TokenType.SignDecrement)
                throw new SyntacticException(token, "语法不正确缺少自减符号--", CBEexpressExCode.ERRORCODE_11196);
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号", CBEexpressExCode.ERRORCODE_11197);
        }
        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            object value = varStack.GetVarValue(name.Value, name);
            value = methodProxy.netOperatorProxy.Decrement(value);
            varStack.SetVarValue(name.Value, value);
        }
    }
    public class NetStatementReturn : NetStatementBase
    {
        private NetExpressBase netExpress = null;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_RETURN)
                throw new SyntacticException(token, "语法不正确缺少关键字return", CBEexpressExCode.ERRORCODE_11198);
            netExpress = NetExpressBuilder.Build(lexer, TokenType.SignSemicolon);
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号;", CBEexpressExCode.ERRORCODE_11199);
        }

        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            if (!netInterrupt.CanReturn)
                throw new SyntacticException(null, "未找到函数,异常的return关键字", CBEexpressExCode.ERRORCODE_11227);
            object value = netExpress.Exec(varStack, methodProxy);
            netInterrupt.Return = true;
            netInterrupt.ReturnValue = value;
        }
    }
    public class NetStatementThrow : NetStatementBase
    {
        private Token tokenID = null;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_Throw)
                throw new SyntacticException(token, "语法不正确缺少关键字throw", CBEexpressExCode.ERRORCODE_11278);

            token = lexer.Pop();
            if (token.Type != TokenType.ID)
                throw new SyntacticException(token, "语法不正确缺少变量", CBEexpressExCode.ERRORCODE_11279);
            tokenID = token;

            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号;", CBEexpressExCode.ERRORCODE_11281);
        }

        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            object value = varStack.GetVarValue(this.tokenID.Value, this.tokenID);
            throw new CBException(value);
        }
    }
    public class NetStatementIDFunction : NetStatementBase
    {
        List<Token> vars = new List<Token>();
        public Token TokenFunctionName { get; set; }
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.ID)
                throw new SyntacticException(token, "语法不正确缺少函数名称", CBEexpressExCode.ERRORCODE_11211);
            TokenFunctionName = token;

            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftParenthesis)
                throw new SyntacticException(token, "语法不正确缺少左括号(", CBEexpressExCode.ERRORCODE_11212);

            NetExpressFunction.Read(lexer, vars);

            token = lexer.Pop();
            if (token.Type != TokenType.SignRightParenthesis)
                throw new SyntacticException(token, "语法不正确缺少右括号)", CBEexpressExCode.ERRORCODE_11213);
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号;", CBEexpressExCode.ERRORCODE_11215);
        }

        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {

            NetStatementFunction netStatementFunction = methodProxy.GetFunction(TokenFunctionName.Value);
            List<object> args = new List<object>();
            foreach (Token item in vars)
            {
                object arg = varStack.GetTokenValue(item);
                args.Add(arg);
            }
            if (netStatementFunction != null)
            {
                netStatementFunction.Run(varStack, methodProxy, args);
            }
            else
            {
                try
                {
                    args.Insert(0, methodProxy);
                    object value = methodProxy.RunFunction(TokenFunctionName.Value, args);
                }
                catch (Exception ex)
                {
                    throw new SyntacticException(TokenFunctionName, "函数执行失败:" + ex.Message, CBEexpressExCode.ERRORCODE_11233);
                }
            }
        }
    }

    public class NetStatementBreak : NetStatementBase
    {
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_BREAK)
                throw new SyntacticException(token, "语法不正确缺少关键字break", CBEexpressExCode.ERRORCODE_11216);
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号;", CBEexpressExCode.ERRORCODE_11217);
        }


        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            if (!netInterrupt.CanBreak)
                throw new SyntacticException(null, "未找到循环,异常的break关键字", CBEexpressExCode.ERRORCODE_11228);
            netInterrupt.Break = true;
        }
    }
    public class NetStatementContinue : NetStatementBase
    {
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_CONTINUE)
                throw new SyntacticException(token, "语法不正确缺少关键字continue", CBEexpressExCode.ERRORCODE_11218);
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号;", CBEexpressExCode.ERRORCODE_11219);
        }

        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
            if (!netInterrupt.CanBreak)
                throw new SyntacticException(null, "未找到循环,异常的break关键字", CBEexpressExCode.ERRORCODE_11229);
            netInterrupt.Continue = true;
        }
    }
    public class NetStatementEmpty : NetStatementBase
    {
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "空语句错语法错误", CBEexpressExCode.ERRORCODE_11236);
        }

        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {
        }
    }
    public class NetStatementInclude : NetStatementBase, IPreprocessing
    {
        public Token Path { get; private set; }
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_Include)
                throw new SyntacticException(token, "Include语句语法错误", CBEexpressExCode.ERRORCODE_11265);
            token = lexer.Pop();
            if (token.Type != TokenType.CONST_STRING)
                throw new SyntacticException(token, "Include语句,必须为常量字符串", CBEexpressExCode.ERRORCODE_11266);
            Path = token;
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "Include语句,缺少;结束符", CBEexpressExCode.ERRORCODE_11267);
        }

        public override void Exec(NetVarCollection varStack, NetContext netInterrupt, IMethodProxy methodProxy)
        {

        }
        private bool lockloadfunction = false;
        public object Preprocessing(IMethodProxy methodProxy, string paretpath)
        {

            string path = this.Path.Value;
            if (lockloadfunction)
                throw new Exception(path + "对文件:" + paretpath + "循环引用");
            lockloadfunction = true;
            try
            {
                methodProxy.LoadFunction(paretpath, path);
            }
            finally
            {
                lockloadfunction = false;
            }
            return true;
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
                    netStatement = new NetStatementObjectFunction();
                    netStatement.Begin(lexer);
                    break;
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
    }

    public class NetParser
    {
        public NetParser()
        {

        }
        public static NetParser Instance
        {
            get
            {
                return new NetParser();
            }
        }
        public string File { get; set; }
        public object Entity { get; set; }

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
                string msg = ex.Message + "\r\n" + GetLine(ex.Token.ScriptFile.Contents, ex.Token.EndIndex);
                ex.SetMsg(msg);
                throw ex;
            }
            return result;
        }
        public object ExecExpress(string txt)
        {
            object result = null;
            try
            {
                string express = "var netresult=" + txt + ";";
                TokenPool tokenPool = GetTokenPool(express);
                NetStatementVar statementVar = NetStatementBuilder.Build(tokenPool) as NetStatementVar;
                if (statementVar == null)
                    throw new SyntacticException(null, "语句不正确", CBEexpressExCode.ERRORCODE_11231);

                NetVarList varlist = new NetVarList("ROOT");
                varStack.Add(varlist);
                statementVar.Exec(varStack, netInterrupt, methodProxy);
                result = varStack.GetVarValue("netresult", null);
            }
            catch (SyntacticException ex)
            {
                string msg = ex.Message + "\r\n" + GetLine(ex.Token.ScriptFile.Contents, ex.Token.EndIndex);
                ex.SetMsg(msg);
                throw ex;
            }
            return result;
        }

        private object Preprocessing(List<NetStatementBase> list)
        {
            foreach (NetStatementBase item in list)
            {
                IPreprocessing preprocessing = item as IPreprocessing;
                if (preprocessing == null)
                    continue;
                preprocessing.Preprocessing(this.methodProxy, File);
            }
            return true;
        }
        private object Exec(TokenPool tokenPool)
        {
            List<NetStatementBase> list = NetStatementBuilder.GetNetStatements(tokenPool);
            NetVarList varlist = new NetVarList("ROOT");
            varStack.Add(varlist);
            methodProxy.Clear();
            methodProxy.Entity = this.Entity;
            methodProxy.File = this.File;
            Preprocessing(list);
            for (int i = 0; i < list.Count; i++)
            {
                NetStatementBase netStatement = list[i];
                netStatement.Exec(varStack, netInterrupt, methodProxy);
            }

            object result = null;
            if (varStack.HasVarValue("netresult"))
            {
                result = varStack.GetVarValue("netresult", null);
            }
            return result;
        }
        public object ExecEvent(string script, string eventname)
        {
            object result = null;
            try
            {
                TokenPool tokenPool = GetTokenPool(script);
                List<NetStatementBase> list = NetStatementBuilder.GetNetStatements(tokenPool);
                NetVarList varlist = new NetVarList("ROOT");
                varStack.Add(varlist);
                methodProxy.Clear();
                methodProxy.Entity = this.Entity;
                methodProxy.File = this.File;
                Preprocessing(list);
                for (int i = 0; i < list.Count; i++)
                {
                    NetStatementBase netStatement = list[i];
                    NetStatementFunction netStatementFunction = netStatement as NetStatementFunction;
                    if (netStatementFunction == null)
                        continue;
                    if (netStatementFunction.GetFunctionName() != eventname)
                        continue;
                    netStatementFunction.Run(varStack, methodProxy, new List<object>());
                    break;
                }

                if (varStack.HasVarValue("netresult"))
                {
                    result = varStack.GetVarValue("netresult", null);
                }
            }
            catch (SyntacticException ex)
            {
                string msg = ex.Message + "\r\n" + GetLine(ex.Token.ScriptFile.Contents, ex.Token.EndIndex);
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
        NetVarCollection varStack = new NetVarCollection();
        NetContext netInterrupt = new NetContext();
        IMethodProxy methodProxy = new MethodProxy();

        private object Check(TokenPool tokenPool)
        {
            object value = null;
            List<NetStatementBase> list = NetStatementBuilder.GetNetStatements(tokenPool);
            Preprocessing(list);
            return value;
        }
        public void LoadFunction(string script)
        {
            TokenPool tokenPool = GetTokenPool(script);
            List<NetStatementBase> list = NetStatementBuilder.GetNetStatements(tokenPool);
            foreach (NetStatementBase item in list)
            {
                NetStatementFunction fun = item as NetStatementFunction;
                if (fun == null)
                    continue;
                methodProxy.AddFunction(fun);
            }
        }
        public void AddFunction(CBMethodContainer methodContainer)
        {
            methodProxy.AddFunction(methodContainer);
        }
        public object Compile(List<NetStatementBase> list)
        {
            return null;
        }
        public static string GetLine(string txt, int index)
        {
            try
            {
                int begin = 0;
                for (int i = index; i >= 0; i--)
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
                return txt.Substring(begin, end - begin);
            }
            catch (Exception)
            {
                return string.Empty;
            }

        }
        public void Inited()
        {
            INetParserInit netParserInit = methodProxy as INetParserInit;
            if (netParserInit != null)
            {
                netParserInit.Init(varStack, methodProxy);
            }
        }
    }

    public interface INetParserInit
    {
        void Init(NetVarCollection varStack, IMethodProxy methodProxy);
    }
}
