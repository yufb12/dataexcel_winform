namespace Feng.Script.CBEexpress
{
    public class StatementVar : StatementBase
    {
        public StatementVar()
        {
        }
        public override object Exec(IBreak parent, ICBEexpress script)
        {
            this.SymbolTable.ReSetPosition();
            this.SymbolTable.Pop();
            Token token = this.SymbolTable.Pop();
            if (token.Type != TokenType.ID)
            {
                throw new SyntacticException("Var Error" + this.SymbolTable.ToString()) { Column = token.Column, Row = token.Line };
            }
            string var = token.Value.ToString();
            token = this.SymbolTable.Pop();
            object value = null;
            if (token != Token.End)
            {
                if (token.Type == TokenType.equality)
                {
                    value = script.Exec(this.SymbolTable);
                }
            }
            script.ExecProxy.SetKeyValue(var, value);
            script.Write(script, this.SymbolTable, var, value);
            return value;
        }
        public override string ToString()
        {
            return "Var " + base.ToString();
        }

    }

}
