using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{
    public class StatementLBRACE : StatementBase, IBreak
    {

        public virtual bool Break { get; set; }
 
        public override object Exec(IBreak parent, ICBEexpress script)
        {
            object value = null;
            foreach (StatementBase item in this.Statementslist)
            {
                item.SymbolTable.ReSetPosition();
                value = item.Exec(parent, script);
            }
             
            return value;
        }
        public override string ToString()
        {
            return "While " + base.ToString();
        }

        public virtual StatementRBRACE End { get; set; }
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
                    throw new CBExpressException("For Error Row:" + this.Index, null) { RowIndex = this.Index };
                }
                if (this.Break)
                {
                    return;
                }
 
                if (statement is StatementRBRACE)
                {
                    End = statement as StatementRBRACE;
                    statement.Build(statements);
                    return;
                }
                statement.Build(statements);
                Statementslist.Add(statement);
            }
        }
    }

    public class StatementRBRACE : StatementBase
    { 
        public override string ToString()
        {
            return "EndFor " + base.ToString();
        }
        public override object Exec(IBreak parent, ICBEexpress script)
        {
            throw new Feng.Script.CBEexpress.CBExpressException("EndFor Exec", null) { RowIndex = this.Index };
        }
    }


}
