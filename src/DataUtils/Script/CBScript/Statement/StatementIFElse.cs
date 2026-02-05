using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{

    public class StatementIFElse : StatementBase
    {
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
                if (statement is StatementEndIF)
                {
                    return;
                }
                if (statement is StatementIFElse)
                {
                    return;
                }
                statement.Build(statements);
                Statementslist.Add(statement);
            }
        }
        public virtual bool CanExec(ICBEexpress script)
        {
            if (this.SymbolTable.Count < 2)
            {
                return true;
            }
            this.SymbolTable.ReSetPosition();
            this.SymbolTable.Pop();
            object value = script.Exec(this.SymbolTable);
            bool res = Feng.Utils.ConvertHelper.ToBoolean(value);
            script.Write(script, this.SymbolTable, "#Res", value);
            return res;
        }
        public override object Exec(IBreak parent, ICBEexpress script)
        {
            this.SymbolTable.ReSetPosition();
            this.SymbolTable.Pop();
            object value = null;

            script.Write(script, this.SymbolTable, "Begin", string.Empty);
            foreach (StatementBase statement in Statementslist)
            {
                if (statement is StatementIFElse)
                {
                    break;
                }
                value = statement.Exec(parent, script);
            }
            script.Write(script, this.SymbolTable, "End", value);
            return value;
        }
        public override string ToString()
        {
            return "Else " + base.ToString();
        }

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

    }

}
