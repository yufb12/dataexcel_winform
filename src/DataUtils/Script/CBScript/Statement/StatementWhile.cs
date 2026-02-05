using Feng.Forms;
using System;
using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{
    public class StatementWhile : StatementBase, IBreak
    {

        public virtual bool Break { get; set; }
        public override object Exec(IBreak parent, ICBEexpress script)
        {
            this.SymbolTable.ReSetPosition();
            this.SymbolTable.Pop();
            object value = script.Exec(this.SymbolTable);

            script.Write(script, this.SymbolTable, "#Res", value);
            bool res = Feng.Utils.ConvertHelper.ToBoolean(value);
            object result = null;
            this.Break = false;

            DateTime starttime = DateTime.Now;
            int whiletimes = 1;
            while (res)
            {
                TimeSpan ts = DateTime.Now - starttime;
                whiletimes = whiletimes + 1;
                if (ts.TotalSeconds > 1 && whiletimes > 2000)
                {
                    string tooltip = string.Format ("While循环已经执行超过3分钟，且已经循环执行超过200次,是否取消执行?");
                    System.Windows.Forms.DialogResult dialogResult = WaitingTimeDialog.ShowInputTextDialog("继续执行提示", tooltip,10, System.Windows.Forms.DialogResult.Cancel);
                    if (dialogResult == System.Windows.Forms.DialogResult.OK)
                    {
                        throw new Exception("用户终止执行");
                    }
                    starttime = DateTime.Now;
                    whiletimes = 1;
                }
                script.Write(script, this.SymbolTable, "Begin", value);
                if (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control)
                { 
                }
                System.Threading.Thread.Sleep(10);
                foreach (StatementBase item in Statementslist)
                {
                    if (script.Finished)
                    {
                        return script.FinishObj;
                    }
                    if (this.Break)
                    {
                        return result;
                    }
                    result = item.Exec(this, script);
                }
                this.SymbolTable.ReSetPosition();
                this.SymbolTable.Pop();
                value = script.Exec(this.SymbolTable);
                res = Feng.Utils.ConvertHelper.ToBoolean(value);
                script.Write(script, this.SymbolTable, "End", value);
            }
            return result;
        }
        public override string ToString()
        {
            return "While " + base.ToString();
        }

        public virtual StatementEndWhile End { get; set; }
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
                    throw new CBExpressException("While Error Row:" + this.Index, null) { RowIndex = this.Index };
                }
                if (this.Break)
                {
                    return;
                }
                if (statement is StatementEndWhile)
                {
                    End = statement as StatementEndWhile;
                    statement.Build(statements);
                    return;
                }
                statement.Build(statements);
                Statementslist.Add(statement);
            }
        }
    }

    public class StatementEndWhile : StatementBase
    {
        public override string ToString()
        {
            return "EndWhile " + base.ToString();
        }
        public override object Exec(IBreak parent, ICBEexpress script)
        {
            throw new Feng.Script.CBEexpress.CBExpressException("EndWhile Exec", null) { RowIndex = this.Index };
        }
    }


}
