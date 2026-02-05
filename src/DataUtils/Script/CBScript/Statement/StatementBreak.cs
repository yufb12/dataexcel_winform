namespace Feng.Script.CBEexpress
{
    public class StatementBreak : StatementBase
    { 
        public override object Exec(IBreak parent, ICBEexpress script)
        { 
            if (parent == null)
            { 
                throw new CBExpressException("Break Error Row:" + this.Index,null) { RowIndex =this.Index };
            } 
            parent.Break = true;
            object value = null;
            script.Write(script,this.SymbolTable, "#Res", value); 
            return true;
        }
        public override string ToString()
        {
            return "Break " + base.ToString();
        }
    }

}
