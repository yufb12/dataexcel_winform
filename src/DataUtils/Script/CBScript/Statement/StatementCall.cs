using Feng.Forms.Interface;
using Feng.Script.CBEexpress;
using System;
using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{
    public class StatementCall : StatementBase, IBreak
    { 
        public virtual bool Break { get; set; }
        public override object Exec(IBreak parent, ICBEexpress script)
        {
            this.SymbolTable.ReSetPosition();
            this.SymbolTable.Pop();
            string name = this.SymbolTable.Pop().Value;
            bool isstatic = false;
            if (name.ToUpper() == "STATIC")
            {
                isstatic = true;
                name = this.SymbolTable.Pop().Value;
            }
            object value = null;
            this.Break = false;
            object obj = null;
            if (isstatic)
            {
                obj = script.ExecProxy.STATICVALUES[name.ToUpper()];
            }
            else
            {
                obj = script.ExecProxy.GetKeyValue(name);
            }
            List<StatementBase> Statementslist = obj as List<StatementBase>;
            script.Write(script, this.SymbolTable, "Call", string.Empty);

            foreach (StatementBase statement in Statementslist)
            {
                if (this.Break)
                {
                    return value;
                }
                value = statement.Exec(this, script);
                if (script.Finished)
                {
                    return script.FinishObj;
                }
            }

            script.Write(script, this.SymbolTable, "Call", value);
            return value;
        }
        public override string ToString()
        {
            return "Call " + base.ToString();
        }

 
 
    }
 


}
