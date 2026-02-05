using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Script.CBEexpress
{


    public abstract class NetStatementBase
    {
        public int Type { get; set; }
        public Token BeginToken { get; set; }
        public Token EndToken { get; set; }
        public List<NetStatementBase> NetStatements { get; set; }
        public abstract void Begin(TokenPool lexer);

        public abstract void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext methodProxy);

        public virtual void CheckDebug(ICBContext methodProxy, NetStatementBase statement)
        {
            if (methodProxy.Debug != null)
            {
                methodProxy.Debug.CheckBreakpoint(statement);
            }
        }
        public virtual void DebugLog(ICBContext methodProxy, NetStatementBase statement, object value)
        {
            if (methodProxy.Debug != null)
            {
                methodProxy.Debug.OnDebugEvent(DebugEventType.LogValue, statement, value);
            }
        }
        public virtual void DebugLog(ICBContext methodProxy, NetStatementBase statement, string varname, object value)
        {
            if (methodProxy.Debug != null)
            {
                methodProxy.Debug.OnDebugEvent(DebugEventType.LogValue, statement, varname, value);
            }
        }
    }

    public class NetStatementExpress : NetStatementBase
    {

        private NetExpressBase netExpress;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Peek();
            this.BeginToken = token;
            netExpress = NetExpressBuilder.Build(lexer, TokenType.SignSemicolon);
            token = lexer.Peek();
            this.EndToken = token;
        }
        public virtual void Begin(TokenPool lexer, int endtype)
        {
            Token token = lexer.Peek();
            this.BeginToken = token;
            netExpress = NetExpressBuilder.Build(lexer, endtype);
            token = lexer.Peek();
            this.EndToken = token;
        }
        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            netExpress.Exec(varStack, cbcontext);
        }
        public virtual object Exec(NetVarCollection varStack, ICBContext methodProxy)
        {
            try
            { 
                return netExpress.Exec(varStack, methodProxy);
            }
            catch (Exception ex)
            {
                throw new CBException(ex.Message + "\r\n" + netExpress.ToString(), ex);
            }

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(netExpress.ToString() + ";");
            return sb.ToString();
        }
    }
    public class NetStatementFor : NetStatementBase
    {
        private NetStatementBase initstatement;
        private NetStatementExpress ifstatement;
        private NetStatementBase addstatement;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_FOR)
                throw new SyntacticException(token, "语法不正确缺少关键字for", CBEexpressExCode.ERRORCODE_11115);
            this.BeginToken = token;
            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftParenthesis)
                throw new SyntacticException(token, "语法不正确缺少左括号", CBEexpressExCode.ERRORCODE_11116);

            token = lexer.Peek();
            if (token.Type == TokenType.Key_VAR)
            {
                initstatement = new NetStatementVar();
                initstatement.Begin(lexer);
            }
            else if (token.Type == TokenType.Key_Object)
            {
                initstatement = new NetStatementObject();
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
            ifstatement = new NetStatementExpress();
            ifstatement.Begin(lexer);
            //NetExpressBuilder.Build(lexer, TokenType.SignSemicolon);
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少分号 ;", CBEexpressExCode.ERRORCODE_11118);

            token = lexer.Peek();
            if (token.Type == TokenType.ID)
            {
                token = lexer.Pop();
                token = lexer.Peek();
                token = lexer.Back();
                if (token.Type == TokenType.SignEuality)
                {
                    NetStatementID addstatementNetStatementID = new NetStatementID();
                    addstatementNetStatementID.Begin(lexer,TokenType.SignRightParenthesis);
                    addstatement = addstatementNetStatementID;
                }
                else
                {
                    NetStatementExpress addstatementnetStatementExpress = new NetStatementExpress();
                    addstatementnetStatementExpress.Begin(lexer, TokenType.SignRightParenthesis);
                    addstatement = addstatementnetStatementExpress;
                }
            }
            else
            {
                NetStatementExpress addstatementnetStatementExpress = new NetStatementExpress(); 
                addstatementnetStatementExpress.Begin(lexer, TokenType.SignRightParenthesis);
                addstatement = addstatementnetStatementExpress;
            }

            token = lexer.Pop();
            if (token.Type != TokenType.SignRightParenthesis)
                throw new SyntacticException(token, "语法不正确缺少小括号)", CBEexpressExCode.ERRORCODE_11119);

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
            this.EndToken = token;
        }

        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            NetVarList netVarList = new NetVarList("FOR");
            try
            {
                netInterrupt.CanBreak = true;
                varStack.Add(netVarList);
                CheckDebug(cbcontext, initstatement);
                initstatement.Exec(varStack, netInterrupt, cbcontext);
                CheckDebug(cbcontext, ifstatement);
                object value = ifstatement.Exec(varStack, cbcontext);
                DebugLog(cbcontext, ifstatement, value);
                bool res = Feng.Utils.ConvertHelper.ToBoolean(value);
                while (res)
                {
                    foreach (NetStatementBase item in NetStatements)
                    { 
                        try
                        {
                            CheckDebug(cbcontext, item);
                            item.Exec(varStack, netInterrupt, cbcontext);
                            if (netInterrupt.Break || netInterrupt.Continue || netInterrupt.Return)
                                break;
                        }
                        catch (SyntacticException ex)
                        {
                            throw new CBException(ex.Message + "\r\n" + item.ToString(), ex.ErrorCode, ex);
                        }
                    }

                    netInterrupt.Continue = false;
                    if (netInterrupt.Break || netInterrupt.Return)
                        break;
                    CheckDebug(cbcontext, addstatement);
                    DebugLog(cbcontext, addstatement, string.Empty);
                    addstatement.Exec(varStack, netInterrupt, cbcontext); 
                    CheckDebug(cbcontext, ifstatement);
                    value = ifstatement.Exec(varStack, cbcontext);
                    DebugLog(cbcontext, ifstatement, value);
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            foreach (NetStatementBase item in this.NetStatements)
            {
                sb1.AppendLine(item.ToString());
            }
            sb.AppendLine("for(" + initstatement.ToString() + "; " + ifstatement.ToString() + " ; " + addstatement.ToString() + ")");
            sb.AppendLine("{");
            sb.AppendLine(sb1.ToString());
            sb.AppendLine("}");
            return sb.ToString();
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
        public virtual string GetFunctionName()
        {
            return this.functioname.Value;
        }
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_FUNCTION)
                throw new SyntacticException(token, "语法不正确缺少关键了function", CBEexpressExCode.ERRORCODE_11124);
            this.BeginToken = token;
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
                if (token.Type == TokenType.SignRightBRACE)
                    break;
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
            this.EndToken = token;
        }
        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            //cbcontext.AddFunction(functioname.Value, this);
        }
        public virtual object Run(NetVarCollection varStack, ICBContext cbcontext, List<object> paramteres)
        {
            NetSkip netInterrupt = new NetSkip();
            NetVarList netVarList = new NetVarList("FUNCTION");
            try
            {
                if (cbcontext.Debug != null)
                {
                    cbcontext.Debug.AddLevel();
                }
                cbcontext.FunctionName = functioname.Value;
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
                        netVarList.SetValue(token.Value, null);
                    }
                }
                foreach (NetStatementBase item in NetStatements)
                {
                    try
                    {
                        CheckDebug(cbcontext, item);
                        cbcontext.FunctionName = functioname.Value;
                        item.Exec(varStack, netInterrupt, cbcontext);
                        if (netInterrupt.Return)
                            break;
                    }
                    catch (SyntacticException ex)
                    {
                        throw new CBException(ex.Message + "\r\n" + item.ToString(), ex.ErrorCode, ex);
                    }
                }
            }
            finally
            {
                if (cbcontext.Debug != null)
                {
                    cbcontext.Debug.SubLevel();
                }
                cbcontext.FunctionName = string.Empty;
                netInterrupt.CanReturn = false;
                netInterrupt.Return = false;
                varStack.Remove(netVarList);

            }
            return netInterrupt.ReturnValue;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            foreach (Token item in arglist)
            {
                sb2.Append(item.Value.ToString() + ",");
            }
            if (sb2.Length > 0)
            {
                sb2.Length = sb2.Length - 1;
            }

            foreach (NetStatementBase item in this.NetStatements)
            {
                sb1.AppendLine(item.ToString());
            }
            sb.AppendLine("function " + functioname.ToString() + " (" + sb2.ToString() + ")");
            sb.AppendLine("{");
            sb.AppendLine(sb1.ToString());
            sb.AppendLine("}");
            return sb.ToString();
        }

        public virtual object Preprocessing(ICBContext cbcontext, string paretpath)
        {
            cbcontext.AddFunction(functioname.Value, this);
            return true;
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
        public override string GetFunctionName()
        {
            return this.functioname.Value;
        }
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_Object)
                throw new SyntacticException(token, "语法不正确缺少关键了object", CBEexpressExCode.ERRORCODE_11237);
            this.BeginToken = token;
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
                    throw new SyntacticException(token, "语法不正确缺少逗号,或者右括号),", CBEexpressExCode.ERRORCODE_11253);
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
            this.EndToken = token;
        }
        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            //cbcontext.AddFunction(functioname.Value, this);
        }
        public override object Run(NetVarCollection varStack, ICBContext cbcontext, List<object> paramteres)
        {
            NetSkip netInterrupt = new NetSkip();
            NetVarList netVarList = new NetVarList("FUNCTION");
            try
            {
                if (cbcontext.Debug != null)
                {
                    cbcontext.Debug.AddLevel();
                }
                cbcontext.FunctionName = functioname.Value;
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
                        netVarList.SetValue(token.Value, null);
                    }
                }
                foreach (NetStatementBase item in NetStatements)
                { 
                    try
                    {
                        CheckDebug(cbcontext, item);
                        cbcontext.FunctionName = functioname.Value;
                        item.Exec(varStack, netInterrupt, cbcontext);
                        if (netInterrupt.Return)
                            break;
                    }
                    catch (SyntacticException ex)
                    {
                        throw new CBException(ex.Message + "\r\n" + item.ToString(), ex.ErrorCode, ex);
                    }
                }
            }
            finally
            {
                if (cbcontext.Debug != null)
                {
                    cbcontext.Debug.SubLevel();
                }
                cbcontext.FunctionName = string.Empty;
                netInterrupt.CanReturn = false;
                netInterrupt.Return = false;
                varStack.Remove(netVarList);
            }
            return netInterrupt.ReturnValue;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            foreach (Token item in arglist)
            {
                sb2.Append(item.Value.ToString() + ",");
            }
            if (sb2.Length > 0)
            {
                sb2.Length = sb2.Length - 1;
            }

            foreach (NetStatementBase item in this.NetStatements)
            {
                sb1.AppendLine(item.ToString());
            }
            sb.AppendLine("object " + functioname.ToString() + " (" + sb2.ToString() + ")");
            sb.AppendLine("{");
            sb.AppendLine(sb1.ToString());
            sb.AppendLine("}");
            return sb.ToString();
        }
        public override object Preprocessing(ICBContext cbcontext, string paretpath)
        {
            cbcontext.AddFunction(functioname.Value, this);
            return true;
        }
    }

    public class NetStatementForeach : NetStatementBase
    {
        private Token foreachitem;
        private Token varitem;
        private Token foreachlist;
        private NetStatementForeachVarIn statementForeachVarIn = null;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_FOREACH)
                throw new SyntacticException(token, "语法不正确缺少关键字foreach", CBEexpressExCode.ERRORCODE_11136);
            this.BeginToken = token;
            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftParenthesis)
                throw new SyntacticException(token, "语法不正确缺少左括号(", CBEexpressExCode.ERRORCODE_11137);

            token = lexer.Pop();
            if (!(token.Type == TokenType.Key_VAR || token.Type == TokenType.Key_Object))
                throw new SyntacticException(token, "语法不正确缺少关键字var或者object", CBEexpressExCode.ERRORCODE_11138);
            varitem = token;


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
            statementForeachVarIn = new NetStatementForeachVarIn();
            statementForeachVarIn.Init(foreachitem, varitem, foreachlist);
            while (true)
            {
                token = lexer.Peek();
                if (token.Type == TokenType.SignRightBRACE)
                    break;
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
            this.EndToken = token;
        }

        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            NetVarList netVarList = new NetVarList("FOREACH");
            try
            {
                netInterrupt.CanBreak = true;
                varStack.Add(netVarList);
                System.Collections.IEnumerable collection = varStack.GetVarValue(foreachlist.Value, foreachlist) as System.Collections.IEnumerable;
                foreach (var itemvalue in collection)
                {
                    CheckDebug(cbcontext, statementForeachVarIn);
                    netVarList.SetValue(foreachitem.Value, itemvalue);

                    DebugLog(cbcontext, statementForeachVarIn, itemvalue);
                    foreach (NetStatementBase item in NetStatements)
                    {
                        try
                        {
                            CheckDebug(cbcontext, item);
                            item.Exec(varStack, netInterrupt, cbcontext);
                            if (netInterrupt.Break || netInterrupt.Continue || netInterrupt.Return)
                                break; 
                        }
                        catch (SyntacticException ex)
                        {
                            throw new CBException(ex.Message + "\r\n" + item.ToString(), ex.ErrorCode, ex);
                        }
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            foreach (NetStatementBase item in this.NetStatements)
            {
                sb1.AppendLine(item.ToString());
            }
            sb.AppendLine("foreach(" + varitem.ToString() + " " + foreachitem.ToString() + " in " + foreachlist.ToString() + ")");
            sb.AppendLine("{");
            sb.AppendLine(sb1.ToString());
            sb.AppendLine("}");
            return sb.ToString();
        }
    }

    public class NetStatementForeachVarIn : NetStatementBase
    {
        private Token foreachitem;
        private Token varitem;
        private Token foreachlist;
        public virtual void Init(Token fi, Token vm, Token ft)
        {
            this.BeginToken = fi;
            this.EndToken = ft;
            foreachitem = fi;
            varitem = vm;
            foreachlist = ft;

        }
        public override void Begin(TokenPool lexer)
        {
        }

        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
        }

        public override string ToString()
        {
            return varitem.ToString() + " " + foreachitem.ToString() + " in " + foreachlist.ToString();
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
            this.BeginToken = token;
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

            token = lexer.Back();
            token = lexer.Peek();
            this.EndToken = token;
            token = lexer.Pop();
            this.NetStatements = new List<NetStatementBase>();
            this.NetStatements.Add(netStatementIF);
            foreach (NetStatementElseIF item in netStatementElseIFes)
            {
                this.NetStatements.Add(item);
            }
            this.NetStatements.Add(netStatementElse);
        }

        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            NetVarList netVarList = new NetVarList("IF");
            try
            {
                varStack.Add(netVarList);
                netInterrupt.IF = false;
                netStatementIF.Exec(varStack, netInterrupt, cbcontext);
                if (netInterrupt.IF)
                    return;
                foreach (NetStatementElseIF item in netStatementElseIFes)
                {
                    item.Exec(varStack, netInterrupt, cbcontext);
                    if (netInterrupt.IF)
                        return;
                    if (netInterrupt.Break || netInterrupt.Continue || netInterrupt.Return)
                        return;
                }
                if (netStatementElse != null)
                {
                    netStatementElse.Exec(varStack, netInterrupt, cbcontext);
                }
            }
            finally
            {
                varStack.Remove(netVarList);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(netStatementIF.ToString());
            if (netStatementElseIFes != null)
            {
                foreach (NetStatementElseIF item in netStatementElseIFes)
                {
                    sb.AppendLine(item.ToString());
                }
            }
            if (netStatementElse != null)
            {
                sb.AppendLine(netStatementElse.ToString());
            }
            return sb.ToString();
        }
    }
    public class NetStatementIFChild : NetStatementBase
    {
        private NetStatementExpress ifstatement;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_IF)
                throw new SyntacticException(token, "语法不正确缺少关键字if", CBEexpressExCode.ERRORCODE_11171);
            this.BeginToken = token;
            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftParenthesis)
                throw new SyntacticException(token, "语法不正确缺少左括号(", CBEexpressExCode.ERRORCODE_11172);

            ifstatement = new NetStatementExpress();
            ifstatement.Begin(lexer, TokenType.SignRightParenthesis);

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
                throw new SyntacticException(token, "语法不正确缺少右大括号}", CBEexpressExCode.ERRORCODE_11168);
            this.EndToken = token;
        }
        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            NetVarList netVarList = new NetVarList("IF");
            try
            {
                varStack.Add(netVarList);

                CheckDebug(cbcontext, ifstatement);
                object value = ifstatement.Exec(varStack, cbcontext);
                DebugLog(cbcontext, ifstatement, value);
                bool res = Feng.Utils.ConvertHelper.ToBoolean(value);
                if (res)
                {
                    foreach (NetStatementBase item in NetStatements)
                    { 
                        try
                        {
                            CheckDebug(cbcontext, item);
                            item.Exec(varStack, netInterrupt, cbcontext);
                            if (netInterrupt.Break || netInterrupt.Continue || netInterrupt.Return)
                                return;
                        }
                        catch (SyntacticException ex)
                        {
                            throw new CBException(ex.Message + "\r\n" + item.ToString(), ex.ErrorCode, ex);
                        }
                    }
                    netInterrupt.IF = true;
                }
            }
            finally
            {
                varStack.Remove(netVarList);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            foreach (NetStatementBase item in this.NetStatements)
            {
                sb1.AppendLine(item.ToString());
            }
            sb.AppendLine("if(" + ifstatement.ToString() + ")");
            sb.AppendLine("{");
            sb.AppendLine(sb1.ToString());
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
    public class NetStatementElseIF : NetStatementBase
    {
        private NetStatementExpress ifstatement;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_IF)
                throw new SyntacticException(token, "语法不正确缺少关键字 else if", CBEexpressExCode.ERRORCODE_11157);
            this.BeginToken = token;
            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftParenthesis)
                throw new SyntacticException(token, "语法不正确缺少左括号(", CBEexpressExCode.ERRORCODE_11158);

            ifstatement = new NetStatementExpress();
            ifstatement.Begin(lexer, TokenType.SignRightParenthesis);

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
                throw new SyntacticException(token, "语法不正确缺少右大括号}", CBEexpressExCode.ERRORCODE_11163);
            this.EndToken = token;
        }

        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            NetVarList netVarList = new NetVarList("ELSE IF");
            try
            {
                varStack.Add(netVarList);
                CheckDebug(cbcontext, ifstatement);
                object value = ifstatement.Exec(varStack, cbcontext);
                DebugLog(cbcontext, ifstatement, value);
                bool res = Feng.Utils.ConvertHelper.ToBoolean(value);
                if (res)
                {
                    foreach (NetStatementBase item in NetStatements)
                    {
                        try
                        {
                            CheckDebug(cbcontext, item);
                            item.Exec(varStack, netInterrupt, cbcontext);
                            if (netInterrupt.Break || netInterrupt.Continue || netInterrupt.Return)
                                return; 
                        }
                        catch (SyntacticException ex)
                        {
                            throw new CBException(ex.Message + "\r\n" + item.ToString(), ex.ErrorCode, ex);
                        }
                    }
                    netInterrupt.IF = true;
                }
            }
            finally
            {
                varStack.Remove(netVarList);
            }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            foreach (NetStatementBase item in this.NetStatements)
            {
                sb1.AppendLine(item.ToString());
            }
            sb.AppendLine("else if(" + ifstatement.ToString() + ")");
            sb.AppendLine("{");
            sb.AppendLine(sb1.ToString());
            sb.AppendLine("}");
            return sb.ToString();
        }
    }
    public class NetStatementElse : NetStatementBase
    {
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_ELSE)
                throw new SyntacticException(token, "语法不正确缺少关键字else", CBEexpressExCode.ERRORCODE_11164);
            this.BeginToken = token;
            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftBRACE)
                throw new SyntacticException(token, "语法不正确缺少左大括号{", CBEexpressExCode.ERRORCODE_11165);
            NetStatements = new List<NetStatementBase>();
            while (true)
            {
                token = lexer.Peek();
                if (token.Type == TokenType.Key_FUNCTION)
                    throw new SyntacticException(token, "语法不正确不能内嵌函数", CBEexpressExCode.ERRORCODE_11166);
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
                throw new SyntacticException(token, "语法不正确缺少右大括号}", CBEexpressExCode.ERRORCODE_11167);
            this.EndToken = token;
        }

        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            NetVarList netVarList = new NetVarList("ELSE");
            try
            {
                varStack.Add(netVarList);
                foreach (NetStatementBase item in NetStatements)
                {
                    try
                    {
                        CheckDebug(cbcontext, item);
                        item.Exec(varStack, netInterrupt, cbcontext);
                        if (netInterrupt.Break || netInterrupt.Continue || netInterrupt.Return)
                            return; 
                    }
                    catch (SyntacticException ex)
                    {
                        throw new CBException(ex.Message + "\r\n" + item.ToString(), ex.ErrorCode, ex);
                    }
                }

            }
            finally
            {
                varStack.Remove(netVarList);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            foreach (NetStatementBase item in this.NetStatements)
            {
                sb1.AppendLine(item.ToString());
            }
            sb.AppendLine("else");
            sb.AppendLine("{");
            sb.AppendLine(sb1.ToString());
            sb.AppendLine("}");
            return sb.ToString();
        }
    }

    public class NetStatementWhile : NetStatementBase
    {
        private NetStatementExpress ifstatement;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_WHILE)
                throw new SyntacticException(token, "语法不正确缺少关键字while", CBEexpressExCode.ERRORCODE_11175);
            this.BeginToken = token;
            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftParenthesis)
                throw new SyntacticException(token, "语法不正确缺少左括号(", CBEexpressExCode.ERRORCODE_11176);

            ifstatement = new NetStatementExpress();
            ifstatement.Begin(lexer, TokenType.SignRightParenthesis);

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
            this.EndToken = token;
        }

        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            NetVarList netVarList = new NetVarList("WHILE");
            try
            {
                netInterrupt.CanBreak = true;
                varStack.Add(netVarList);
                CheckDebug(cbcontext, ifstatement);
                object value = ifstatement.Exec(varStack, cbcontext);
                DebugLog(cbcontext, ifstatement, value);
                bool res = Feng.Utils.ConvertHelper.ToBoolean(value);
                while (res)
                {
                    foreach (NetStatementBase item in NetStatements)
                    {
                        try
                        {
                            CheckDebug(cbcontext, item);
                            item.Exec(varStack, netInterrupt, cbcontext);
                            if (netInterrupt.Break || netInterrupt.Continue || netInterrupt.Return)
                                break; 
                        }
                        catch (SyntacticException ex)
                        {
                            throw new CBException(ex.Message + "\r\n" + item.ToString(), ex.ErrorCode, ex);
                        }
                    }

                    netInterrupt.Continue = false;
                    if (netInterrupt.Break || netInterrupt.Return)
                        break;
                    value = ifstatement.Exec(varStack, cbcontext);
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            foreach (NetStatementBase item in this.NetStatements)
            {
                sb1.AppendLine(item.ToString());
            }
            sb.AppendLine("while(" + ifstatement.ToString() + ")");
            sb.AppendLine("{");
            sb.AppendLine(sb1.ToString());
            sb.AppendLine("}");
            return sb.ToString();
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
            this.BeginToken = token;
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
            this.EndToken = token;
            this.NetStatements = new List<NetStatementBase>();
            foreach (NetStatementBase item in NetStatementsTry)
            {
                this.NetStatements.Add(item);

            }
            foreach (NetStatementBase item in NetStatementsCatch)
            {
                this.NetStatements.Add(item);
            }
        }

        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            NetVarList netVarList = new NetVarList("Try");
            varStack.Add(netVarList);
            try
            {
                foreach (NetStatementBase item in NetStatementsTry)
                { 
                    try
                    {
                        CheckDebug(cbcontext, item);
                        item.Exec(varStack, netInterrupt, cbcontext);
                        if (netInterrupt.Return)
                            break;
                    }
                    catch (SyntacticException ex)
                    {
                        throw new CBException(ex.Message + "\r\n" + item.ToString(), ex.ErrorCode, ex);
                    }
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
                        try
                        {
                            CheckDebug(cbcontext, item);
                            item.Exec(varStack, netInterrupt, cbcontext);
                            if (netInterrupt.Return)
                                break;
                        }
                        catch (SyntacticException exx)
                        {
                            throw new CBException(exx.Message + "\r\n" + item.ToString(), exx.ErrorCode, exx);
                        }
                    }
                }
                finally
                {
                    varStack.Remove(netVarList);
                }
            }
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            foreach (NetStatementBase item in NetStatementsTry)
            {
                sb1.AppendLine(item.ToString());
            }
            foreach (NetStatementBase item in NetStatementsCatch)
            {
                sb2.AppendLine(item.ToString());
            }
            sb.AppendLine("try");
            sb.AppendLine("{");
            sb.AppendLine(sb1.ToString());
            sb.AppendLine("}");
            sb.AppendLine("catch(ex)");
            sb.AppendLine("{");
            sb.AppendLine(sb2.ToString());
            sb.AppendLine("}");
            return sb.ToString();
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
            this.BeginToken = token;
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
            this.EndToken = token;
        }

        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            object value = null;
            if (netExpress!=null)
            {
                value = netExpress.Exec(varStack, cbcontext);
            }
            varStack.SetVarValue(varname.Value, value);
            DebugLog(cbcontext, this, varname.Value, value);
        }

        public override string ToString()
        {
            string exptxt = string.Empty;
            if (netExpress!=null)
            {
                exptxt = "=" + netExpress.ToString();
            }
            return "var " + varname.Value +  exptxt + ";";
        }

        internal string GetVarName()
        {
            return varname.Value;
        }
    }

    public class NetStatementObject : NetStatementBase
    {
        public NetStatementObject()
        {

        }
        private NetExpressBase netExpress = null;
        private Token varname = null;
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_Object)
                throw new SyntacticException(token, "语法不正确缺少关键字 object", CBEexpressExCode.ERRORCODE_11282);
            this.BeginToken = token;
            token = lexer.Pop();
            if (token.Type != TokenType.ID)
                throw new SyntacticException(token, "语法不正确缺少变量名", CBEexpressExCode.ERRORCODE_11283);
            varname = token;

            token = lexer.Pop();
            if (token.Type != TokenType.SignEuality)
            {
                if (token.Type == TokenType.SignSemicolon)
                {
                    return;
                }
                throw new SyntacticException(token, "语法不正确缺少等号=", CBEexpressExCode.ERRORCODE_11285);
            }
            netExpress = NetExpressBuilder.Build(lexer, TokenType.SignSemicolon);
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号;", CBEexpressExCode.ERRORCODE_11286);
            this.EndToken = token;
        }

        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            object value = netExpress.Exec(varStack, cbcontext);
            varStack.SetVarValue(varname.Value, value);
            DebugLog(cbcontext, this, varname.Value, value);
        }

        public override string ToString()
        {
            return "object " + varname.Value + "=" + netExpress.ToString() + ";";
        }

        internal string GetVarName()
        {
            return varname.Value;
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
            this.BeginToken = token;
            name = token;

            token = lexer.Pop();
            if (token.Type != TokenType.SignEuality)
                throw new SyntacticException(token, "语法不正确缺少等号", CBEexpressExCode.ERRORCODE_11188);
            netExpress = NetExpressBuilder.Build(lexer, TokenType.SignSemicolon);
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号;", CBEexpressExCode.ERRORCODE_11189);
            this.EndToken = token;
        }
        public virtual void Begin(TokenPool lexer,int endtype)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.ID)
                throw new SyntacticException(token, "语法不正确缺少变量名", CBEexpressExCode.ERRORCODE_11187);
            this.BeginToken = token;
            name = token;

            token = lexer.Pop();
            if (token.Type != TokenType.SignEuality)
                throw new SyntacticException(token, "语法不正确缺少等号", CBEexpressExCode.ERRORCODE_11188);
            netExpress = NetExpressBuilder.Build(lexer, endtype);
            token = lexer.Peek();
            if (token.Type != endtype)
                throw new SyntacticException(token, "语法不正确缺少结束符号;", CBEexpressExCode.ERRORCODE_11189);
            this.EndToken = token;
        }
        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            object value = netExpress.Exec(varStack, cbcontext);
            varStack.SetVarValue(name.Value, value);
            DebugLog(cbcontext, this, name.Value, value);
        }

        public override string ToString()
        {
            return "" + name.Value + "=" + netExpress.ToString() + ";";
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
            this.BeginToken = token;
            name = token;

            token = lexer.Pop();
            if (token.Type != TokenType.SignIncrement)
                throw new SyntacticException(token, "语法不正确缺少自增符号++", CBEexpressExCode.ERRORCODE_11192);
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号;", CBEexpressExCode.ERRORCODE_11193);
            this.EndToken = token;
        }
        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            object value = varStack.GetVarValue(name.Value, name);
            value = cbcontext.netOperatorProxy.Increment(value);
            varStack.SetVarValue(name.Value, value);
            DebugLog(cbcontext, this, name.Value, value);
        }
        public override string ToString()
        {
            return "" + name.Value + "++" + ";";
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
            this.BeginToken = token;
            name = token;

            token = lexer.Pop();
            if (token.Type != TokenType.SignDecrement)
                throw new SyntacticException(token, "语法不正确缺少自减符号--", CBEexpressExCode.ERRORCODE_11196);
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号", CBEexpressExCode.ERRORCODE_11197);
            this.EndToken = token;
        }
        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            object value = varStack.GetVarValue(name.Value, name);
            value = cbcontext.netOperatorProxy.Decrement(value);
            varStack.SetVarValue(name.Value, value);
            DebugLog(cbcontext, this, name.Value, value);
        }

        public override string ToString()
        {
            return "" + name.Value + "--" + ";";
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
            this.BeginToken = token;
            token = lexer.Peek();
            if (token.Type == TokenType.SignSemicolon)
            {
                token = lexer.Pop();
                return;
            }
            netExpress = NetExpressBuilder.Build(lexer, TokenType.SignSemicolon);
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号;", CBEexpressExCode.ERRORCODE_11199);
            this.EndToken = token;
        }

        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            if (!netInterrupt.CanReturn)
                throw new SyntacticException(null, "未找到函数,异常的return关键字", CBEexpressExCode.ERRORCODE_11227);
            CheckDebug(cbcontext, this);
            object value = null;
            if (netExpress != null)
            {
                value = netExpress.Exec(varStack, cbcontext);
            }
            netInterrupt.Return = true;
            netInterrupt.ReturnValue = value;
            DebugLog(cbcontext, this, value);
        }

        public override string ToString()
        {
            return "return " + netExpress.ToString() + ";";
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
            this.BeginToken = token;
            token = lexer.Pop();
            if (token.Type != TokenType.ID)
                throw new SyntacticException(token, "语法不正确缺少变量", CBEexpressExCode.ERRORCODE_11279);
            tokenID = token;

            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号;", CBEexpressExCode.ERRORCODE_11281);
            this.EndToken = token;
        }

        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            DebugLog(cbcontext, this, string.Empty);
            object value = varStack.GetVarValue(this.tokenID.Value, this.tokenID);
            throw new CBException(value);
        }

        public override string ToString()
        {
            return "throw " + tokenID.Value.ToString() + ";";
        }
    }
    public class NetStatementIDFunction : NetStatementBase
    {
        public NetStatementIDFunction()
        {
            vars = new List<NetExpressBase>();
        }
        List<NetExpressBase> vars = null;
        public Token TokenFunctionName { get; set; }
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.ID)
                throw new SyntacticException(token, "语法不正确缺少函数名称", CBEexpressExCode.ERRORCODE_11211);
            this.BeginToken = token;
            TokenFunctionName = token;

            token = lexer.Pop();
            if (token.Type != TokenType.SignLeftParenthesis)
                throw new SyntacticException(token, "语法不正确缺少左括号(", CBEexpressExCode.ERRORCODE_11212);

            token = lexer.Peek();
            if (token.Type != TokenType.SignRightParenthesis)
            {
                while (true)
                {
                    NetExpressBase netExpressBase = NetExpressBuilder.Build(lexer, TokenType.SignComma, TokenType.SignRightParenthesis);
                    vars.Add(netExpressBase);
                    token = lexer.Peek();
                    if (token.Type == TokenType.SignRightParenthesis)
                        break;
                    lexer.Pop();
                }
            }

            token = lexer.Pop();
            if (token.Type != TokenType.SignRightParenthesis)
                throw new SyntacticException(token, "语法不正确缺少右括号)", CBEexpressExCode.ERRORCODE_11213);
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号;", CBEexpressExCode.ERRORCODE_11215);
            this.EndToken = token;
        }

        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            List<object> args = new List<object>();
            foreach (NetExpressBase item in vars)
            {
                object arg = item.Exec(varStack,cbcontext);
                args.Add(arg);
            }
            try
            {
                object value = cbcontext.RunFunction(TokenFunctionName.Value, args, varStack, cbcontext);
                DebugLog(cbcontext, this, TokenFunctionName.Value, value);
            }
            catch (Exception ex)
            {
                throw new SyntacticException(TokenFunctionName, "函数(" + TokenFunctionName.Value + ") 执行失败:" + ex.Message, CBEexpressExCode.ERRORCODE_11233);
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (NetExpressBase item in vars)
            {
                sb.Append(item.ToString() + ",");
            }
            if (sb.Length > 0)
            {
                sb.Length = sb.Length - 1;
            }
            return TokenFunctionName.Value + " (" + sb.ToString() + ");";
        }
    }

    public class NetStatementBreak : NetStatementBase
    {
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_BREAK)
                throw new SyntacticException(token, "语法不正确缺少关键字break", CBEexpressExCode.ERRORCODE_11216);
            this.BeginToken = token;
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号;", CBEexpressExCode.ERRORCODE_11217);
            this.EndToken = token;
        }

        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            DebugLog(cbcontext, this, string.Empty);
            if (!netInterrupt.CanBreak)
                throw new SyntacticException(null, "未找到循环,异常的break关键字", CBEexpressExCode.ERRORCODE_11228);
            netInterrupt.Break = true;
        }

        public override string ToString()
        {
            return "break" + ";";
        }
    }
    public class NetStatementContinue : NetStatementBase
    {
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_CONTINUE)
                throw new SyntacticException(token, "语法不正确缺少关键字continue", CBEexpressExCode.ERRORCODE_11218);
            this.BeginToken = token;
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "语法不正确缺少结束符号;", CBEexpressExCode.ERRORCODE_11219);
            this.EndToken = token;
        }

        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
            DebugLog(cbcontext, this,string.Empty);
            if (!netInterrupt.CanBreak)
                throw new SyntacticException(null, "未找到循环,异常的Continue关键字", CBEexpressExCode.ERRORCODE_11229);
            netInterrupt.Continue = true;
        }


        public override string ToString()
        {
            return "Continue" + ";";
        }
    }
    public class NetStatementEmpty : NetStatementBase
    {
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "空语句错语法错误", CBEexpressExCode.ERRORCODE_11236);
            this.BeginToken = token;
            this.EndToken = token;
        }

        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {
        }

        public override string ToString()
        {
            return "" + ";";
        }
    }

    public class NetStatementInclude : NetStatementBase
    {
        public Token Path { get; private set; }
        public override void Begin(TokenPool lexer)
        {
            Token token = lexer.Pop();
            if (token.Type != TokenType.Key_Include)
                throw new SyntacticException(token, "Include语句语法错误", CBEexpressExCode.ERRORCODE_11265);
            this.BeginToken = token;
            token = lexer.Pop();
            if (token.Type != TokenType.CONST_STRING)
                throw new SyntacticException(token, "Include语句,必须为常量字符串", CBEexpressExCode.ERRORCODE_11266);
            Path = token;
            token = lexer.Pop();
            if (token.Type != TokenType.SignSemicolon)
                throw new SyntacticException(token, "Include语句,缺少;结束符", CBEexpressExCode.ERRORCODE_11267);
            this.EndToken = token;
        }

        public override void Exec(NetVarCollection varStack, NetSkip netInterrupt, ICBContext cbcontext)
        {

        }

        public override string ToString()
        {
            return "Include \"" + Path.Value + "\";";
        }
    }


}
