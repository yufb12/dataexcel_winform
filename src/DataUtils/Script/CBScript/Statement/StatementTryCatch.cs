using Feng.Forms.Interface;
using Feng.Script.CBEexpress;
using System;
using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{
    public class StatementTry : StatementBase, IBreak
    { 
        public StatementTry()
        {

        }
        public virtual bool Break { get; set; }
        public override object Exec(IBreak parent, ICBEexpress script)
        {
            this.SymbolTable.ReSetPosition();
            this.SymbolTable.Pop();
            object value = null;
            this.Break = false;
            script.Write(script, this.SymbolTable, "Try", string.Empty);
            try
            { 
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
            }
            catch (Exception ex)
            {
                script.ExecProxy.SetKeyValue("LastError", ex.Message);
            }

            script.Write(script, this.SymbolTable, "Catch", value);
            return value;
        }
        public override string ToString()
        {
            return "Foreach " + base.ToString();
        }

        public virtual StatementCatch End { get; set; }
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
                if (statement is StatementCatch)
                {
                    End = statement as StatementCatch;
                    statement.Build(statements);
                    return;
                }
                statement.Build(statements);
                Statementslist.Add(statement);
            }
        }
    }

    public class StatementCatch : StatementBase
    {
        public override string ToString()
        {
            return "EndForeach " + base.ToString();
        }
        public override object Exec(IBreak parent, ICBEexpress script)
        {
            throw new Feng.Script.CBEexpress.CBExpressException("EndForeach Exec", null) { RowIndex = this.Index };
        }
    }


}
