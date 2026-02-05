namespace Feng.Script.CBEexpress
{
    public class StatementReturn : StatementBase
    {
        public override object Exec(IBreak parent, ICBEexpress script)
        {
            this.SymbolTable.ReSetPosition();
            this.SymbolTable.Pop();
            object value = null;
            if (this.SymbolTable.Peek() != Token.End)
            {
                value = script.Exec(this.SymbolTable);
            }

            script.FinishObj = value;
            script.Finished = true;
            script.Write(script, this.SymbolTable, "#Res", value);
            return value;
        }
        public override string ToString()
        {
            return "Return " + base.ToString();
        }
    }

}
