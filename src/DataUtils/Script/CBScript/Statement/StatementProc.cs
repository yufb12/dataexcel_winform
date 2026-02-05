using Feng.Forms.Interface;
using Feng.Script.CBEexpress;
using System;
using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{
    public class StatementProc : StatementBase, IBreak
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
            this.Break = false;
            script.Write(script, this.SymbolTable, "Proc", string.Empty);
            if (isstatic)
            {
                script.ExecProxy.STATICVALUES[name.ToUpper()] = this.Statementslist;
            }
            else
            {
                script.ExecProxy.SetKeyValue(name, this.Statementslist);
            }
            script.Write(script, this.SymbolTable, "EndProc", name);
            return name;
        }
        public override string ToString()
        {
            return "Foreach " + base.ToString();
        }

        public virtual StatementEndProc End { get; set; }
        private List<StatementBase> statementslist = null;
        public virtual List<StatementBase> Statementslist
        {
            get
            {
                if (statementslist == null)
                {
                    statementslist = new List<StatementBase>();
                }
                return statementslist;
            }
        }
        public override void Build(StatementCollection statements)
        {
            StatementBase statement = statements.Current();
            statements.Pop();
            while (true)
            {
                statement = statements.Current();
                if (statement == null)
                {
                    return;
                }
                if (statement is StatementEndProc)
                {
                    End = statement as StatementEndProc;
                    statement.Build(statements);
                    return;
                }
                statement.Build(statements);
                Statementslist.Add(statement);
            }
        }
    }

    public class StatementEndProc : StatementBase
    {
        public override string ToString()
        {
            return "EndProc " + base.ToString();
        }
        public override object Exec(IBreak parent, ICBEexpress script)
        {
            throw new Feng.Script.CBEexpress.CBExpressException("EndProc", null) { RowIndex = this.Index };
        }
    }


}
