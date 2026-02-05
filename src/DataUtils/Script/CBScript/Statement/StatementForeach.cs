using Feng.Forms.Interface;
using Feng.Script.CBEexpress;
using System;
using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{
    public class StatementForeach : StatementBase, IBreak
    {
        public virtual string ItemKey { get; set; }
        public virtual string ListKey { get; set; }
        public virtual bool Break { get; set; }
        public override object Exec(IBreak parent, ICBEexpress script)
        {
            this.SymbolTable.ReSetPosition();
            this.SymbolTable.Pop();
            string itemkey = this.SymbolTable.Pop().Value;
            string intext = this.SymbolTable.Pop().Value;
            string listkey = this.SymbolTable.Pop().Value;
            ItemKey = itemkey;
            ListKey = listkey;
            object value = null;
            object objlist = script.ExecProxy.GetKeyValue(listkey);
            System.Collections.IEnumerable list = objlist as System.Collections.IEnumerable;
            System.Collections.IEnumerator enumerator = list.GetEnumerator();
            this.Break = false;
            script.Write(script, this.SymbolTable, "Begin", string.Empty);
            foreach (var item in list)
            {
                script.Write(script, this.SymbolTable, itemkey, item);
                script.ExecProxy.SetKeyValue(itemkey, item);
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

            script.Write(script, this.SymbolTable, "End", value);
            return value;
        }
        public override string ToString()
        {
            return "Foreach " + base.ToString();
        }

        public virtual StatementEndForeach End { get; set; }
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
                if (statement is StatementEndForeach)
                {
                    End = statement as StatementEndForeach;
                    statement.Build(statements);
                    return;
                }
                statement.Build(statements);
                Statementslist.Add(statement);
            }
        }
    }

    public class StatementEndForeach : StatementBase
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
