using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Script.CBEexpress
{

    public abstract class NetExpressBase
    {
        public virtual NetOperatorBase Operator { get; set; }
        public virtual Token Token { get; set; }
        public virtual object Exec(NetVarCollection varStack, ICBContext methodProxy)
        {
            throw new NotImplementedException();
        }
    }

    public class NetExpressBuilder
    {
        public static NetExpressBase Build(TokenPool lexer, int endtype1,int endtype2)
        {
            NetExpressPool netExpress = new NetExpressPool();
            ExpressPool expressBases = new ExpressPool();
            while (true)
            {
                NetExpressBase nextExpress = GetNetExpress(lexer, endtype1, endtype2);
                expressBases.Push(nextExpress);
                Token token = lexer.Peek();
                if (token.Type == endtype1)
                {
                    nextExpress.Operator = OperatorNull.Instance;
                    break;
                }
                if (token.Type == endtype2)
                {
                    nextExpress.Operator = OperatorNull.Instance;
                    break;
                }
                NetOperatorBase netOperator = GetNetOperator(lexer);
                nextExpress.Operator = netOperator;
                token = lexer.Peek();
                if (token.Type == endtype1)
                {
                    break;
                }
                if (token.Type == endtype2)
                {
                    break;
                }
            }
            netExpress.ExpressPool = expressBases;
            return netExpress;
        }
        public static NetExpressBase Build(TokenPool lexer, int endtype)
        {
            return Build(lexer, endtype, TokenType.Error);
        }

        private static NetExpressBase GetNetExpress(TokenPool lexer, int endtype1, int endtype2)
        {
            NetExpressBase netExpress = null;
            Token token = lexer.Pop();
            switch (token.Type)
            {
                case TokenType.SignLeftParenthesis:
                    NetExpressKuoHao netExpress1 = new NetExpressKuoHao();
                    netExpress1.Token = token;
                    netExpress1.NetExpress = Build(lexer, TokenType.SignRightParenthesis);
                    netExpress = netExpress1;
                    token = lexer.Pop();
                    if (token.Type != TokenType.SignRightParenthesis)
                        throw new SyntacticException(token, "缺少右括号 )", CBEexpressExCode.ERRORCODE_11101);
                    break;
                case TokenType.SignMinus:
                case TokenType.SignNegative:
                    NetExpressNegative netExpressNegative = new NetExpressNegative();
                    netExpressNegative.Token = token;
                    netExpressNegative.NetExpress = GetNetExpress(lexer, endtype1, endtype2);
                    netExpress = netExpressNegative;
                    token.Type = TokenType.SignNegative;
                    break;
                case TokenType.SignlogicalNOT:
                    NetExpressSignlogicalNOT netExpressSignlogicalNOT = new NetExpressSignlogicalNOT();
                    netExpressSignlogicalNOT.Token = token;
                    netExpressSignlogicalNOT.NetExpress = GetNetExpress(lexer, endtype1, endtype2);
                    netExpress = netExpressSignlogicalNOT;
                    break;
                case TokenType.CONST_NUMBER:
                case TokenType.CONST_FALSE:
                case TokenType.CONST_NULL:
                case TokenType.CONST_TRUE:
                case TokenType.CONST_STRING:
                    NetExpressSimple netExpressSimple = new NetExpressSimple();
                    netExpressSimple.Token = token;
                    netExpress = netExpressSimple;
                    break;
                case TokenType.Key_RPC:
                    NetExpressRpcFunction netExpressRpcFunction = new NetExpressRpcFunction();
                    netExpressRpcFunction.Token = token;
                    token = lexer.Peek();
                    if (token.Type != TokenType.ID)
                        throw new SyntacticException(token, "Rpc 函数名称不正确", CBEexpressExCode.ERRORCODE_11258);
                    token = lexer.Pop();
                    netExpressRpcFunction.Token = token;
                    token = lexer.Peek();
                    if (token.Type != TokenType.SignLeftParenthesis)
                        throw new SyntacticException(token, "Rpc 函数少左括号", CBEexpressExCode.ERRORCODE_11259);
                    token = lexer.Pop();

                    List<Token> varsrpc = Read(lexer);
                    netExpressRpcFunction.InitTokens(varsrpc);
                    netExpress = netExpressRpcFunction;
                    token = lexer.Pop();
                    if (token.Type != TokenType.SignRightParenthesis)
                        throw new SyntacticException(token, "缺少右括号 )", CBEexpressExCode.ERRORCODE_11263);
                    break;
                case TokenType.ID:
                    Token tokennext = lexer.Peek();
                    switch (tokennext.Type)
                    {
                        case TokenType.SignLeftParenthesis:
                            lexer.Pop();
                            NetExpressFunction netExpressFunction = new NetExpressFunction();
                            netExpressFunction.Token = token;
                            //List<Token> vars = Read(lexer);
                            //netExpressFunction.InitTokens(vars);
                            netExpressFunction.Begin(lexer);
                            netExpress = netExpressFunction;
                            token = lexer.Pop();
                            if (token.Type != TokenType.SignRightParenthesis)
                                throw new SyntacticException(token, "缺少右括号 )", CBEexpressExCode.ERRORCODE_11102);
                            break;

                        case TokenType.SignLeftSquareBrackets:
                            token = lexer.Pop();
                            NetExpressProperty netExpressProperty = new NetExpressProperty();
                            netExpressProperty.Token = token;
                            netExpressProperty.NetExpress = Build(lexer, TokenType.SignRightSquareBrackets);
                            netExpress = netExpressProperty;
                            token = lexer.Pop();
                            if (token.Type != TokenType.SignRightSquareBrackets)
                                throw new SyntacticException(token, "缺少右中括号 )", CBEexpressExCode.ERRORCODE_11103);
                            break;
                        case TokenType.SignEuality:
                            token = lexer.Pop();
                            NetExpressAssignment netExpressAssignment = new NetExpressAssignment();
                            netExpressAssignment.Token = token;
                            netExpressAssignment.NetExpress = Build(lexer, endtype1, endtype2);
                            netExpress = netExpressAssignment;
                            break;
                        case TokenType.SignIncrement:
                            lexer.Pop();
                            NetExpressIDIncrement netExpressIDIncrement = new NetExpressIDIncrement();
                            netExpressIDIncrement.Token = token;
                            netExpress = netExpressIDIncrement;
                            break;
                        case TokenType.SignDecrement:
                            lexer.Pop();
                            NetExpressIDDecrement netExpressIDDecrement = new NetExpressIDDecrement();
                            netExpressIDDecrement.Token = token;
                            netExpress = netExpressIDDecrement;
                            break;
                        default:
                            NetExpressID netExpressID = new NetExpressID();
                            netExpressID.Token = token;
                            netExpress = netExpressID;
                            break;
                    }
                    break;
                default:
                    throw new SyntacticException(token, "未定义的字符", CBEexpressExCode.ERRORCODE_11104);
            }
            return netExpress;
        }

        private static NetOperatorBase GetNetOperator(TokenPool lexer)
        {
            Token token = lexer.Peek();
            NetOperatorBase netOperator = null;
            switch (token.Type)
            {
                case TokenType.SignlogicalAND:
                    token = lexer.Pop();
                    netOperator = OperatorSignlogicalAND.Instance;
                    break;
                case TokenType.SignlogicalOR:
                    token = lexer.Pop();
                    netOperator = OperatorSignlogicalOR.Instance;
                    break;
                //case TokenType.SignbitwiseOR:
                //    token = lexer.Pop();
                //    netOperator = OperatorSignbitwiseOR.Instance;
                //    break;
                //case TokenType.SingXOR:
                //    token = lexer.Pop();
                //    netOperator = OperatorSingXOR.Instance;
                //    break;
                //case TokenType.SignbitwiseAND:
                //    token = lexer.Pop();
                //    netOperator = OperatorSignbitwiseAND.Instance;
                //    break;
                case TokenType.SignEqualTo:
                    token = lexer.Pop();
                    netOperator = OperatorSignEqualTo.Instance;
                    break;
                case TokenType.SignNotEqualTo:
                    token = lexer.Pop();
                    netOperator = OperatorSignNotEqualTo.Instance;
                    break;
                case TokenType.SignRelationalGreaterThan:
                    token = lexer.Pop();
                    netOperator = OperatorSignRelationalGreaterThan.Instance;
                    break;
                case TokenType.SignRelationalGreaterThanOrEqual:
                    token = lexer.Pop();
                    netOperator = OperatorSignRelationalGreaterThanOrEqual.Instance;
                    break;
                case TokenType.SignRelationalLessThan:
                    token = lexer.Pop();
                    netOperator = OperatorSignRelationalLessThan.Instance;
                    break;
                case TokenType.SignRelationalLessThanOrEqual:
                    token = lexer.Pop();
                    netOperator = OperatorSignRelationalLessThanOrEqual.Instance;
                    break;
                //case TokenType.SignShiftRight:
                //    token = lexer.Pop();
                //    netOperator = OperatorSignShiftRight.Instance;
                //    break;
                //case TokenType.SignShiftLeft:
                //    token = lexer.Pop();
                //    netOperator = OperatorSignShiftLeft.Instance;
                //    break;
                case TokenType.SignPlus:
                    token = lexer.Pop();
                    netOperator = OperatorSignPlus.Instance;
                    break;
                case TokenType.SignMinus:
                    token = lexer.Pop();
                    netOperator = OperatorSignMinus.Instance;
                    break;
                case TokenType.SignNegative:
                    token = lexer.Pop();
                    netOperator = OperatorSignNegative.Instance;
                    break;
                case TokenType.SignMultiplied:
                    token = lexer.Pop();
                    netOperator = OperatorSignMultiplied.Instance;
                    break;
                case TokenType.SignDivided:
                    token = lexer.Pop();
                    netOperator = OperatorSignDivided.Instance;
                    break;
                case TokenType.SignModulus:
                    token = lexer.Pop();
                    netOperator = OperatorSignModulus.Instance;
                    break;
                case TokenType.SignIncrement:
                    token = lexer.Pop();
                    netOperator = OperatorSignIncrement.Instance;
                    break;
                case TokenType.SignDecrement:
                    token = lexer.Pop();
                    netOperator = OperatorSignDecrement.Instance;
                    break;
                case TokenType.SignlogicalNOT:
                    token = lexer.Pop();
                    netOperator = OperatorSignlogicalNOT.Instance;
                    break;
                //case TokenType.SignbitwiseNOT:
                //    token = lexer.Pop();
                //    netOperator = OperatorSignbitwiseNOT.Instance;
                //    break;
                default:
                    throw new SyntacticException(token, "未定义的操作符", CBEexpressExCode.ERRORCODE_11105);
            }
            return netOperator;
        }


        public static List<Token> Read(TokenPool lexer)
        {
            List<Token> vars = new List<Token>();
            Token token = lexer.Peek();
            while (true)
            {
                if (token.Type == TokenType.SignRightParenthesis)
                    break;
                if (token.Type == TokenType.ID || token.Type == TokenType.CONST_NUMBER
                    || token.Type == TokenType.CONST_STRING
                    || token.Type == TokenType.CONST_NULL
                    || token.Type == TokenType.CONST_TRUE
                    || token.Type == TokenType.CONST_FALSE
                    || token.Type == TokenType.Key_THIS
                    || token.Type == TokenType.Key_ME
                    )
                {
                    token = lexer.Pop();
                    vars.Add(token);
                    token = lexer.Peek();
                    if (token.Type == TokenType.SignRightParenthesis)
                        break;
                    if (token.Type != TokenType.SignComma)
                        throw new SyntacticException(token, "意外的符号", CBEexpressExCode.ERRORCODE_11106);
                    token = lexer.Pop();
                    token = lexer.Peek();

                }
                else
                {
                    throw new SyntacticException(token, "不正确符号", CBEexpressExCode.ERRORCODE_11107);
                }
            }
            return vars;
        }
    }
    public class NetExpressPool : NetExpressBase
    {
        public NetExpressPool()
        {
        }
        public ExpressPool ExpressPool { get; set; }
        public override object Exec(NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = null;
            value = ExpressOperator.Run(ExpressPool, varStack, methodProxy);
            return value;
        }

        public override string ToString()
        {
            if (ExpressPool == null)
                return string.Empty;
            return ExpressPool.ToString();
        }
    }

    public class NetExpressSimple : NetExpressBase
    {
        public NetExpressSimple()
        {
        }

        public override object Exec(NetVarCollection varStack, ICBContext methodProxy)
        {
            object currentvalue = null;
            switch (Token.Type)
            {
                case TokenType.CONST_FALSE:
                    currentvalue = false;
                    break;
                case TokenType.CONST_NULL:
                    currentvalue = null;
                    break;
                case TokenType.CONST_NUMBER:
                    currentvalue = decimal.Parse(Token.Value);
                    break;
                case TokenType.CONST_STRING:
                    currentvalue = Token.Value;
                    break;
                case TokenType.CONST_TRUE:
                    currentvalue = true;
                    break;
                default:
                    throw new SyntacticException(Token, "未识别的常量", CBEexpressExCode.ERRORCODE_11221);
            }
            return currentvalue;
        }

        public override string ToString()
        {
            if (this.Operator == null)
                return this.Token.Value;
            return this.Token.Value + this.Operator.ToString();
        }

    }
    public class NetExpressID : NetExpressBase
    {
        public NetExpressID()
        {
        }

        public override object Exec(NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = varStack.GetVarValue(Token.Value, Token);
            return value;
        }

        public override string ToString()
        {
            if (this.Operator == null)
                return this.Token.Value;
            return this.Token.Value + this.Operator.ToString();
        }

    }
    public class NetExpressIDIncrement : NetExpressBase
    {
        public NetExpressIDIncrement()
        {
        }

        public override object Exec(NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = varStack.GetVarValue(Token.Value, Token);
            int ivalue = Feng.Utils.ConvertHelper.ToInt32(value, 0, true);
            ivalue++;
            varStack.SetVarValue(Token.Value, ivalue);
            return ivalue;
        }

        public override string ToString()
        {
            if (this.Operator == null)
                return this.Token.Value;
            return this.Token.Value + "++" + this.Operator.ToString();
        }
    }
    public class NetExpressIDDecrement : NetExpressBase
    {
        public NetExpressIDDecrement()
        {
        }

        public override object Exec(NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = varStack.GetVarValue(Token.Value, Token);
            int ivalue = Feng.Utils.ConvertHelper.ToInt32(value, 0, true);
            ivalue--;
            varStack.SetVarValue(Token.Value, ivalue);
            return ivalue;

        }

        public override string ToString()
        {
            if (this.Operator == null)
                return this.Token.Value;
            return this.Token.Value + "--" + this.Operator.ToString();
        }
    }
    public class NetExpressAssignment : NetExpressBase
    {
        public NetExpressAssignment()
        {
        }

        public NetExpressBase NetExpress { get; set; }
        public override object Exec(NetVarCollection varStack, ICBContext methodProxy)
        {
            object currentvalue = varStack.GetVarValue(Token.Value, Token);
            if (this.Operator == null)
                return currentvalue;
            object nextvalue = this.NetExpress.Exec(varStack, methodProxy);
            object value = this.Operator.Exec(currentvalue, nextvalue, varStack, methodProxy);
            return value;
        }
        public override string ToString()
        {
            if (this.Operator == null)
                return this.Token.Value;
            return this.Token.Value + NetExpress.ToString() + this.Operator.ToString();
        }
    }
    public class NetExpressNegative : NetExpressBase
    {
        public NetExpressNegative()
        {
        }

        public NetExpressBase NetExpress { get; set; }
        public override object Exec(NetVarCollection varStack, ICBContext methodProxy)
        {
            decimal value = Feng.Utils.ConvertHelper.ToDecimal(NetExpress.Exec(varStack, methodProxy), true) * -1;
            return value;
        }

        public override string ToString()
        {
            return "-" + NetExpress.ToString();
        }
    }
    //public class NetExpressSignbitwiseAND : NetExpressBase
    //{
    //    public NetExpressSignbitwiseAND()
    //    {
    //    }

    //    public NetExpressBase NetExpress { get; set; }
    //    public override object Exec(NetVarCollection varStack, ICBContext methodProxy)
    //    {
    //        object currentvalue = NetExpress.Exec(varStack, methodProxy);
    //        currentvalue = Feng.Utils.ConvertHelper.ToDecimal(currentvalue) * -1;
    //        if (this.Operator == null)
    //            return currentvalue;

    //        object nextvalue = this.NetExpress.Exec(varStack, methodProxy);
    //        object value = this.Operator.Exec(currentvalue, nextvalue, varStack, methodProxy);
    //        return value;
    //    }
    //}
    public class NetExpressSignlogicalNOT : NetExpressBase
    {
        public NetExpressSignlogicalNOT()
        {
        }

        public NetExpressBase NetExpress { get; set; }
        public override object Exec(NetVarCollection varStack, ICBContext methodProxy)
        {
            object currentvalue = NetExpress.Exec(varStack, methodProxy);
            bool result = Feng.Utils.ConvertHelper.ToBoolean(currentvalue);
            return !result;
        }

        public override string ToString()
        {
            return "!" + NetExpress.ToString();
        }
    }
    //public class NetExpressSignbitwiseNOT : NetExpressBase
    //{
    //    public NetExpressSignbitwiseNOT()
    //    {
    //    }

    //    public NetExpressBase NetExpress { get; set; }
    //    public override object Exec(NetVarCollection varStack, ICBContext methodProxy)
    //    {
    //        object currentvalue = NetExpress.Exec(varStack, methodProxy);
    //        currentvalue = Feng.Utils.ConvertHelper.ToDecimal(currentvalue) * -1;
    //        if (this.Operator == null)
    //            return currentvalue;

    //        object nextvalue = this.NetExpress.Exec(varStack, methodProxy);
    //        object value = this.Operator.Exec(currentvalue, nextvalue, varStack, methodProxy);
    //        return value;
    //    }
    //}
    public class NetExpressKuoHao : NetExpressBase
    {
        public NetExpressKuoHao()
        {
        }

        public NetExpressBase NetExpress { get; set; }
        public override object Exec(NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = NetExpress.Exec(varStack, methodProxy);
            return value;
        }
        public override string ToString()
        {
            return "(" + NetExpress.ToString() + ")";
        }
    }
    public class NetExpressFunction : NetExpressBase
    {
        public NetExpressFunction()
        {
        }

        List<NetExpressBase> Vars = new List<NetExpressBase>();

        public virtual void Begin(TokenPool lexer)
        {
            Token token = lexer.Peek();
            if (token.Type == TokenType.SignRightParenthesis)
                return;
            while (true)
            {
                NetExpressBase netExpressBase = NetExpressBuilder.Build(lexer, TokenType.SignComma, TokenType.SignRightParenthesis);
                Vars.Add(netExpressBase);
                token = lexer.Peek();
                if (token.Type == TokenType.SignRightParenthesis)
                    break;
                lexer.Pop();
            }
        }

        public override object Exec(NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = null;
            if (!methodProxy.HasFunction(Token.Value))
            {
                throw new SyntacticException(Token, "不存在此函数:" + Token.Value, CBEexpressExCode.ERRORCODE_11108);
            }
            List<object> args = new List<object>();
            foreach (NetExpressBase item in Vars)
            {
                object argvalue = item.Exec(varStack, methodProxy);
                args.Add(argvalue);
            }
            try
            {
                value = methodProxy.RunFunction(Token.Value, args, varStack, methodProxy);
            }
            catch (Exception ex)
            {
                throw new SyntacticException(Token, "函数执行失败:" + ex.Message, CBEexpressExCode.ERRORCODE_11232);
            }

            return value;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            foreach (NetExpressBase item in Vars)
            {
                sb2.Append(item.ToString() + ",");
            }
            if (sb2.Length > 0)
            {
                sb2.Length = sb2.Length - 1;
            }

            sb.Append(Token.Value + "(" + sb2.ToString() + ")");
            return sb.ToString();
        }
    }

    public class NetExpressRpcFunction : NetExpressBase
    {
        public NetExpressRpcFunction()
        {
        }

        List<Token> Vars = new List<Token>();

        public virtual void InitTokens(List<Token> vars)
        {
            Vars = vars;
        }

        public override object Exec(NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = null;
            List<object> args = new List<object>();
            foreach (Token item in Vars)
            {
                object obj = varStack.GetTokenValue(item);
                args.Add(obj);
            }
            try
            {
                value = methodProxy.RunRpcFunction(methodProxy.File, Token.Value, args);
            }
            catch (Exception ex)
            {
                throw new SyntacticException(Token, "远程函数执行失败:" + ex.Message, CBEexpressExCode.ERRORCODE_11234);
            }

            return value;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();

            foreach (Token item in Vars)
            {
                sb2.Append(item.Value.ToString() + ",");
            }
            if (sb2.Length > 0)
            {
                sb2.Length = sb2.Length - 1;
            }

            sb.Append("RPC " + Token.Value + "(" + sb2.ToString() + ")");
            return sb.ToString();
        }
    }

    public class NetExpressProperty : NetExpressBase
    {
        public NetExpressProperty()
        {
        }

        public NetExpressBase NetExpress { get; set; }

        public override object Exec(NetVarCollection varStack, ICBContext methodProxy)
        {
            object value = NetExpress.Exec(varStack, methodProxy);
            object obj = varStack.GetTokenValue(this.Token);
            value = methodProxy.netOperatorProxy.Property(obj, value);
            return value;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Token.Value + "[" + NetExpress.ToString() + "]");
            return sb.ToString();
        }
    }
}
