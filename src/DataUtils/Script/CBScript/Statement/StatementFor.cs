using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{
    public class StatementFor : StatementBase, IBreak
    {

        public virtual bool Break { get; set; }
        public object ExecFor(SymbolTable symbolTable, ICBEexpress script)
        {
            symbolTable.ReSetPosition();
            Token token = symbolTable.Pop();
            token = symbolTable.Pop();
            if (token.Type != TokenType.ID)
            {
                throw new SyntacticException("For Error") { Column = token.Column, Row = token.Line };
            }
            if (token.Value.ToLower() == "var")
            {
                token = symbolTable.Pop();
                if (token.Type != TokenType.ID)
                {
                    throw new SyntacticException("For Error") { Column = token.Column, Row = token.Line };
                }
            }
            string var = token.Value.ToString();
            token = symbolTable.Pop();
            object value = null;
            if (token == Token.End)
            {
                throw new SyntacticException("For TokenEnd Error") { Column = token.Column, Row = token.Line };
            }
            if (token.Type != TokenType.equality)
            {
                throw new SyntacticException("For Equality Error") { Column = token.Column, Row = token.Line };
            }
            value = script.Exec(symbolTable);
            script.ExecProxy.SetKeyValue(var, value);
            script.Write(script, symbolTable, var, value);
            return value;
        }
        public override object Exec(IBreak parent, ICBEexpress script)
        {
            object value = ExecFor(this.SymbolTable, script);

            script.Write(script, this.SymbolTable, "For Begin #Res", value);
            bool res = true;
            object result = null;
            this.Break = false;
            while (res)
            {  
                StatementBase itemif = Statementslist[0];
                itemif.SymbolTable.ReSetPosition(); 
                value = script.Exec(itemif.SymbolTable);
                res = Feng.Utils.ConvertHelper.ToBoolean(value);
                script.Write(script, itemif.SymbolTable, "#Res", value);
                if (res)
                {
                    for (int i = 2; i < Statementslist.Count; i++)
                    {
                        StatementBase item = Statementslist[i];
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

                    StatementBase itemautoadd = Statementslist[1];
                    itemautoadd.SymbolTable.ReSetPosition();
                    value = itemautoadd.Exec(parent, script); 
                    script.Write(script, itemautoadd.SymbolTable, "For Next #Res", value);
                }
            }
            script.Write(script, this.SymbolTable, "For End", value);
            return result;
        }
        public override string ToString()
        {
            return "While " + base.ToString();
        }

        public virtual StatementEndFor End { get; set; }
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
 
                if (statement is StatementEndFor)
                {
                    End = statement as StatementEndFor;
                    statement.Build(statements);
                    return;
                }
                statement.Build(statements);
                Statementslist.Add(statement);
            }
        }
    }

    public class StatementEndFor : StatementBase
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
