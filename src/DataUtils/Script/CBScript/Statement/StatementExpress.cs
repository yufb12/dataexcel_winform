namespace Feng.Script.CBEexpress
{

    public class StatementExpress : StatementBase
    { 

        public override object Exec(IBreak parent, ICBEexpress script)
        {
            this.SymbolTable.ReSetPosition();
            object value = script.Exec(this.SymbolTable);
            script.Write(script, this.SymbolTable, "#Res", value);
            return value;
        } 
    }

}
