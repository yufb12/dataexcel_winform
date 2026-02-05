using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{ 
    public class StatementEquality : StatementBase
    { 
        public override object Exec(IBreak parent, ICBEexpress script)
        {
            this.SymbolTable.ReSetPosition(); 
            Token token = this.SymbolTable.Pop();
            if (token.Type != TokenType.ID)
            {
                throw new SyntacticException("Var Error") { Column = token.Column, Row = token.Line };
            }
            string var = token.Value.ToString();
            token = this.SymbolTable.Pop();
            object value = null;
            if (token == Token.End)
            {
                throw new SyntacticException("Var TokenEnd Error") { Column = token.Column, Row = token.Line };
            } 
            if (token.Type != TokenType.equality)
            {
                throw new SyntacticException("Var Equality Error") { Column = token.Column, Row = token.Line };
            }
            value = script.Exec(this.SymbolTable);
            script.ExecProxy.SetKeyValue(var, value);
            script.Write(script, this.SymbolTable, var,value);
            return value;
        }
        public override string ToString()
        {
            return "Equality " + base.ToString();
        } 
    } 

}
