using System.Collections.Generic;


namespace Feng.Script.CBEexpress
{
    public class Parse
    {

        public object Exec(OperatorExecBase operatorexec, SymbolTable table)
        {

            try
            {
                object obj = this.exp(operatorexec, table);
                Token token = table.Peek();
                if (token != Token.End)
                {
                    throw new CBExpressException("" + GetErrorText() + "语法不正确，未正确结束" + " Row:" + token.Line + " Column:" + token.Column, token);
                }
                return obj;
            }
            catch (System.Exception ex)
            {
                CBExpressException exc = new CBExpressException("" + GetErrorText() + " 程序遇到异常" + ex.Message,
                    table.Peek(), ex);
                throw exc;
            }    
        }
        public Token Token1 { get; set; }
        public Token Token2 { get; set; }
        public Token Token3 { get; set; }
        public Token Token4 { get; set; }
        public Token Token5 { get; set; }
        public Token Token6 { get; set; }
        public Token Token7 { get; set; }
        public Token Token8 { get; set; }
        public Token Token9 { get; set; }
        public Token Token10 { get; set; }
        public Token PopupToken(SymbolTable table)
        {
            Token token = table.Pop();

            Token10 = Token9;
            Token9 = Token8;
            Token8 = Token7;
            Token7 = Token6;
            Token6 = Token5;
            Token5 = Token4;
            Token4 = Token3;
            Token3 = Token2;
            Token2 = Token1;
            Token1 = token;
            return token;
        }
        private string GetErrorText()
        {
            string text = TokenValue(Token10)
                + " "
                + TokenValue(Token9)
                + " "
                + TokenValue(Token8)
                + " "
                + TokenValue(Token7)
                + " "
                + TokenValue(Token6)
                + " "
                + TokenValue(Token5)
                + " "
                + TokenValue(Token4)
                + " "
                + TokenValue(Token3)
                + " "
                + TokenValue(Token2)
                + " "
                + TokenValue(Token1);
            return " 语法错误:" + text.Trim();
        }
        private string TokenValue(Token token)
        {
            if (token == null)
                return string.Empty;
            return token.Value;
        }
        private object exp(OperatorExecBase operatorexec, SymbolTable table)
        {
            object obj = this.logical_or_exp(operatorexec, table);
            return obj;
        }
        /// <summary>
        /// ||
        /// </summary> 
        private object logical_or_exp(OperatorExecBase operatorexec, SymbolTable table)//||
        {
            object value1 = logical_and_exp(operatorexec, table);
            Token token = table.Peek();
            while (token.Type == TokenType.logical_or_exp)
            {
                token = PopupToken(table);
                object value2 = logical_and_exp(operatorexec, table);
                value1 = OperatorLogical_or.Instance.Exec(this, operatorexec, value1, value2, token);
                token = table.Peek();
            }
            return value1;
        }

        private object logical_and_exp(OperatorExecBase operatorexec, SymbolTable table)// &&
        {

            object value1 = inclusive_or_exp(operatorexec, table);
            Token token = table.Peek();
            while (token.Type == TokenType.logical_and_exp)
            {
                token = PopupToken(table);
                object value2 = inclusive_or_exp(operatorexec, table);
                value1 = OperatorLogical_and.Instance.Exec(this, operatorexec, value1, value2, token);
                token = table.Peek();
            }
            return value1;
        }

        private object inclusive_or_exp(OperatorExecBase operatorexec, SymbolTable table) // |
        {

            object value1 = exclusive_or_exp(operatorexec, table);
            Token token = table.Peek();
            while (token.Type == TokenType.inclusive_or_exp)
            {
                token = PopupToken(table);
                object value2 = exclusive_or_exp(operatorexec, table);
                value1 = OperatorInclusive_or.Instance.Exec(this, operatorexec, value1, value2, token);
                token = table.Peek();
            }
            return value1;
        }

        private object exclusive_or_exp(OperatorExecBase operatorexec, SymbolTable table) // ^
        {

            object value1 = and_exp(operatorexec, table);
            Token token = table.Peek();
            while (token.Type == TokenType.exclusive_or_exp)
            {
                token = PopupToken(table);
                object value2 = and_exp(operatorexec, table);
                value1 = OperatorExclusive_or.Instance.Exec(this, operatorexec, value1, value2, token);
                token = table.Peek();
            }
            return value1;
        }

        private object and_exp(OperatorExecBase operatorexec, SymbolTable table) // &
        {

            object value1 = equality_exp(operatorexec, table);
            Token token = table.Peek();
            while (token.Type == TokenType.and_exp)
            {
                token = PopupToken(table);
                object value2 = equality_exp(operatorexec, table);
                value1 = OperatorAnd.Instance.Exec(this, operatorexec, value1, value2, token);
                token = table.Peek();
            }
            return value1;
        }

        private object equality_exp(OperatorExecBase operatorexec, SymbolTable table)// !=
        {

            object value1 = relational_exp(operatorexec, table);
            Token token = table.Peek();
            while (token.Type == TokenType.equality_exp)
            {
                token = PopupToken(table);
                object value2 = relational_exp(operatorexec, table);
                value1 = OperatorEquality.Instance.Exec(this, operatorexec, value1, value2, token);
                token = table.Peek();
            }
            return value1;
        }

        private object relational_exp(OperatorExecBase operatorexec, SymbolTable table)  // > < >= <=
        {
            object value1 = shift_expression(operatorexec, table);
            Token token = table.Peek();
            while (token.Type == TokenType.relational_exp)
            {
                token = PopupToken(table);
                object value2 = shift_expression(operatorexec, table);
                value1 = OperatorRelational.Instance.Exec(this, operatorexec, value1, value2, token);
                token = table.Peek();
            }
            return value1;
        }

        private object shift_expression(OperatorExecBase operatorexec, SymbolTable table) // << >>
        {
            object value1 = additive_exp(operatorexec, table);
            Token token = table.Peek();
            while (token.Type == TokenType.shift_expression)
            {
                token = PopupToken(table);
                object value2 = additive_exp(operatorexec, table);
                value1 = OperatorShift.Instance.Exec(this, operatorexec, value1, value2, token);
                token = table.Peek();
            }
            return value1;
        }

        private object additive_exp(OperatorExecBase operatorexec, SymbolTable table)// + -
        {

            object value1 = mult_exp(operatorexec, table);
            Token token = table.Peek();
            while (token.Type == TokenType.additive_exp)
            {
                token = PopupToken(table);
                object value2 = mult_exp(operatorexec, table);
                value1 = OperatorAdditive.Instance.Exec(this, operatorexec, value1, value2, token);
                token = table.Peek();
            }
            return value1;
        }

        private object mult_exp(OperatorExecBase operatorexec, SymbolTable table)// * /
        {
            object value1 = unary_exp(operatorexec, table);
            Token token = table.Peek();
            while (token.Type == TokenType.mult_exp)
            {
                token = PopupToken(table);
                object value2 = unary_exp(operatorexec, table);
                value1 = OperatorMult.Instance.Exec(this, operatorexec, value1, value2, token);
                token = table.Peek();
            }
            return value1;
        }

        private object unary_exp(OperatorExecBase operatorexec, SymbolTable table) //- ++ -- ! ~
        {
            object value1 = null;
            Token token = table.Peek();
            if (token.Type == TokenType.unary_exp || token.Value == "-")
            {
                token = PopupToken(table);
                value1 = unary_exp(operatorexec, table);
                value1 = OperatorUnary.Instance.Exec(this, operatorexec, value1, null, token);
                return value1;
            }
            value1 = postfix_exp(operatorexec, table);
            return value1;
        }

        private object postfix_exp(OperatorExecBase operatorexec, SymbolTable table) //function
        {
            Token token = table.Peek();
            object value1 = null;
            switch (token.Type)
            {
                case TokenType.ID:
                    value1 = id_exp(operatorexec, table);
                    break;
                default:
                    value1 = primary_exp(operatorexec, table);
                    break;
            }
            return OperatorPostfix.Instance.Exec(this, operatorexec, value1, null, token);
        }

        private object id_exp(OperatorExecBase operatorexec, SymbolTable table)
        {
            Token token = PopupToken(table);
            string id = token.Value;
            Token tokennext = table.Peek();
            if (tokennext.Type == TokenType.function_exp_begin)
            {
                return function_exp(operatorexec, table, id);
            }
            object value1 = OperatorID.Instance.Exec(this, operatorexec, id, null, token);

            return value1;
        }

        private object primary_exp(OperatorExecBase operatorexec, SymbolTable table)
        {
            Token token = table.Peek();
            switch (token.Type)
            {
                case TokenType.function_exp_begin:
                    token = PopupToken(table);
                    object value = exp(operatorexec, table);
                    token = table.Peek();
                    if (token.Type == TokenType.function_exp_end)
                    {
                        token = PopupToken(table);
                        return value;
                    }
                    break;
                case TokenType.NULL:
                    token = PopupToken(table);
                    return OperatorConst_Null.Instance.Exec(this, operatorexec, null, null, token);
                case TokenType.CONST_NUMBER:
                    token = PopupToken(table);
                    return OperatorConst_Number.Instance.Exec(this, operatorexec, null, null, token);
                case TokenType.CONST_STRING:
                    token = PopupToken(table);
                    return OperatorConst_String.Instance.Exec(this, operatorexec, null, null, token);
                case TokenType.THIS:
                    token = PopupToken(table);
                    return OperatorConst_This.Instance.Exec(this, operatorexec, null, null, token);
                case TokenType.TRUE:
                    token = PopupToken(table);
                    return OperatorConst_True.Instance.Exec(this, operatorexec, null, null, token);
                case TokenType.FALSE:
                    token = PopupToken(table);
                    return OperatorConst_False.Instance.Exec(this, operatorexec, null, null, token);
                default:
                    break;
            }
            throw new CBExpressException("" + GetErrorText() + "语法不正确未结束" + " Row:" + token.Line + " Column:" + token.Column, token)
            { RowIndex = token.Line, ColumnIndex = token.Column };
        }

        private object function_exp(OperatorExecBase operatorexec, SymbolTable table, string functionname)
        {
            Token token = table.Peek();
            switch (token.Type)
            {
                case TokenType.function_exp_begin:
                    token = PopupToken(table);
                    token = table.Peek();
                    if (token.Type == TokenType.function_exp_end)
                    {
                        token = PopupToken(table);
                        return OperatorFunction.Instance.Exec(this, operatorexec, functionname, null, token);
                    }
                    object value1 = argument_exp_list(operatorexec, table);
                    token = table.Peek();
                    if (token.Type == TokenType.function_exp_end)
                    {
                        token = PopupToken(table);
                        return OperatorFunction.Instance.Exec(this, operatorexec, functionname, value1, token);
                    }
                    break;
                default:
                    break;
            }
            throw new CBExpressException("" + GetErrorText() + "函数" + functionname + "不正确" + " Row:" + token.Line + " Column:" + token.Column, token)
            { RowIndex = token.Line, ColumnIndex = token.Column };
        }

        private object argument_exp_list(OperatorExecBase operatorexec, SymbolTable table)
        {
            List<object> list = new List<object>();

            Token token = table.Peek();
            object value = null;
            value = exp(operatorexec, table);
            list.Add(value);
            token = table.Peek();
            switch (token.Type)
            {
                case TokenType.argument_exp_list:
                    token = PopupToken(table);
                    argument_exp_list(operatorexec, table, list);
                    break;
                case TokenType.function_exp_end:
                    break;
                default:
                    throw new CBExpressException("" + GetErrorText() + "函数参数不正确，是否少右括号" + " Row:" + token.Line + " Column:" + token.Column, token)
                    { RowIndex = token.Line, ColumnIndex = token.Column };
            }
            return OperatorArgument_list.Instance.Exec(this, operatorexec, list, null, token);
        }

        private void argument_exp_list(OperatorExecBase operatorexec, SymbolTable table, List<object> list)
        {
            Token token = table.Peek();
            object value = null;
            value = exp(operatorexec, table);
            list.Add(value);
            token = table.Peek();
            switch (token.Type)
            {
                case TokenType.argument_exp_list:
                    token = PopupToken(table);
                    argument_exp_list(operatorexec, table, list);
                    break;
                case TokenType.function_exp_end:
                    break;
                default:
                    throw new CBExpressException("" + GetErrorText() + "函数未正确结束" + " Row:" + token.Line + " Column:" + token.Column, token)
                    { RowIndex = token.Line, ColumnIndex = token.Column };
            }
        }

    }
}
