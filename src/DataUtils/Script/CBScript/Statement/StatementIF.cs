using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{

    public class StatementIF : StatementBase
    {
        public StatementIF()
        {
        }
        public override object Exec(IBreak parent, ICBEexpress script)
        {
            this.SymbolTable.ReSetPosition();
            this.SymbolTable.Pop();
            object value = script.Exec(this.SymbolTable);
            bool res = Feng.Utils.ConvertHelper.ToBoolean(value);
            script.Write(script, this.SymbolTable, "#Res", value);
            if (res)
            {
                script.Write(script, this.SymbolTable, "Begin", string.Empty);
                foreach (StatementBase statement in Statementslist)
                {
                    if (statement is StatementIFElse)
                    {
                        break;
                    }
                    value = statement.Exec(parent, script);
                    if (script.Finished)
                    {
                        return script.FinishObj;
                    }
                }
                script.Write(script, this.SymbolTable, "End", value);
            }
            else
            {
                foreach (StatementIFElse statement in ElseList)
                {
                    StatementIFElse statementels = statement as StatementIFElse;
                    if (!statementels.CanExec(script))
                    {
                        continue;
                    }
                    value = statement.Exec(parent, script);
                    if (script.Finished)
                    {
                        return script.FinishObj;
                    }
                    return value;
                }
            }
            return value;
        }
        public override string ToString()
        {
            return "IF " + base.ToString();
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

        private List<StatementIFElse> elselist = null;
        public virtual List<StatementIFElse> ElseList
        {
            get
            {
                if (elselist == null)
                {
                    elselist = new List<StatementIFElse>();
                }
                return elselist;
            }
        }

        public virtual StatementEndIF End { get; set; }
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
                    End = statement as StatementEndIF;
                    statement.Build(statements);
                    return;
                }
                if (statement is StatementIFElse)
                {
                    StatementIFElse stateMentElse = statement as StatementIFElse;
                    stateMentElse.Build(statements);
                    ElseList.Add(stateMentElse);
                    continue;
                }
                statement.Build(statements);
                Statementslist.Add(statement);
            }

        }
        public override void Build(List<SymbolTable> List)
        {

        }
    }


    public class StatementEndIF : StatementBase
    {
        public override string ToString()
        {
            return "EndIF " + base.ToString();
        }
        public override object Exec(IBreak parent, ICBEexpress script)
        {
            throw new Feng.Script.CBEexpress.CBExpressException("EndIF Exec", null)
            { RowIndex = this.Index };
        }
    }
}
