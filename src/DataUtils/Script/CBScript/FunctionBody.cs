using Feng.Script.FunctionContainer;
using Feng.Script.Method;
using System;

namespace Feng.Script.CBEexpress
{

    public class FunctionBody
    {
        public FunctionBody()
        {
        }
        private IStateMentFactory statementfactory = null;
        public virtual IStateMentFactory StateMentFactory
        {
            get
            {
                if (statementfactory == null)
                {
                    statementfactory = new StateMentFactory();
                }
                return statementfactory;
            }
            set { statementfactory = value; }
        }   
        private ICBEexpress script = null;
        public virtual ICBEexpress Script
        {
            get
            {
                if (script == null)
                {
                    script = new Eexpress();
                }
                return script;
            }
            set { script = value; }
        }
         
        public IOutWatch OutWatch { get; set; }
        public virtual object Exec(string text)
        {
            try
            {
                StatementCollection statements = Parse(text);
                Statements = BuildStateMentsTree(statements);
                Script.Finished = false;
                Script.FinishObj = null;
                Script.OutWatch = OutWatch;
                DateTime dt=DateTime.Now;
                if (OutWatch != null)
                {
                    OutWatch.Write("开始执行:" + dt.ToString ("HH:mm:ss:fff"));
                }
                object value = Exec(Script);
                if (OutWatch != null)
                {
                    OutWatch.Write("结束执行:" + DateTime.Now.ToString("HH:mm:ss:fff"));
                    TimeSpan ts = DateTime.Now - dt;
                    OutWatch.Write("耗时:"+ts.TotalMilliseconds.ToString());
                }
                return value;
            }
            catch (System.Exception ex)
            {
                if (OutWatch != null)
                {
                    OutWatch.Write(ex.Message);
                }
                throw ex;
            }
        }
        private StatementCollection _statements = null;
        private StatementCollection Statements
        {
            get { return _statements; }
            set { _statements = value; }
        }
 
        private object Exec(ICBEexpress script)
        {
            if (Statements == null)
                return null;
            object value = null;
            while (Statements.Current() != null)
            {
                StatementBase item = Statements.Current();
                value = item.Exec(null, script);
                if (script.Finished)
                {
                    return script.FinishObj;
                }
                Feng.Utils.TraceHelper.WriteTrace("", "StatementSection", "Exec", true, item.SymbolTable.ToString());
                Statements.Pop();
            }
            if (script.Finished)
            {
                return script.FinishObj;
            }
            return value;
        }
        private StatementCollection Parse(string text)
        {
            StatementCollection statements = new StatementCollection();
            StatementLexer lexer = new StatementLexer();
            lexer.Parse(text);
            if (lexer.List.Count > 0)
            {
                for (int i = 0; i < lexer.List.Count; i++)
                {
                    SymbolTable table = lexer.List[i];
                    StatementBase statement = null;
                    Token token = table.Peek();

                    switch (token.Value.ToUpper())
                    {
                        case "IF":
                            statement = StateMentFactory.CreateStatementIF();
                            break;
                        case "ELSE":
                            statement = StateMentFactory.CreateStatementElse();
                            break;
                        case "ENDIF":
                            statement = StateMentFactory.CreateStatementEndIF();
                            break;
                        case "WHILE":
                            statement = StateMentFactory.CreateStatementWhile();
                            break;
                        case "ENDWHILE":
                            statement = StateMentFactory.CreateStatementEndWhile();
                            break;
                        case "FOR":
                            statement = StateMentFactory.CreateStatementFor();
                            break;
                        case "ENDFOR":
                            statement = StateMentFactory.CreateStatementEndFor();
                            break;
                        case "RETURN":
                            statement = StateMentFactory.CreateStatementReturn();
                            break;
                        case "VAR":
                            statement = StateMentFactory.CreateStatementVar();
                            break;
                        case "BREAK":
                            statement = StateMentFactory.CreateStatementBreak();
                            break;
                        case "{":
                            statement = StateMentFactory.CreateStatementLBRACE();
                            break;
                        case "}":
                            statement = StateMentFactory.CreateStatementRBRACE();
                            break;
                        case "FOREACH":
                            token = table.Pop();
                            token = table.Pop();
                            if (token.Type != TokenType.ID)
                                throw new Feng.Script.CBEexpress.CBExpressException("Foreach Error Row:" + token.Line + " Column:" + token.Column,token) { RowIndex = token.Line, ColumnIndex = token.Column };
                            string itemkey = token.Value;
                            token = table.Pop();
                            if (token.Type != TokenType.ID)
                                throw new Feng.Script.CBEexpress.CBExpressException("Foreach Error Row:" + token.Line + " Column:" + token.Column, token) { RowIndex = token.Line, ColumnIndex = token.Column };
                            string intext = token.Value.ToUpper();
                            if (intext != "IN")
                                throw new Feng.Script.CBEexpress.CBExpressException("Foreach In Error No In Key  Row:" + token.Line + " Column:" + token.Column, token) { RowIndex = token.Line, ColumnIndex = token.Column };
                            token = table.Pop();
                            if (token.Type != TokenType.ID)
                                throw new Feng.Script.CBEexpress.CBExpressException("Foreach In List Error  Row:" + token.Line + " Column:" + token.Column, token) { RowIndex = token.Line, ColumnIndex = token.Column };

                            statement = StateMentFactory.CreateStatementForeach();
                            break;
                        case "ENDFOREACH":
                            statement = StateMentFactory.CreateStatementEndForeach();
                            break;

                        case "TRY":
                            statement = StateMentFactory.CreateStatementTry();
                            break;
                        case "CATCH":
                            statement = StateMentFactory.CreateStatementCatch();
                            break;

                        case "PROC":
                            statement = StateMentFactory.CreateStatementProc();
                            break;
                        case "ENDPROC":
                            statement = StateMentFactory.CreateStatementEndProc();
                            break;
                        case "CALL":
                            statement = StateMentFactory.CreateStatementCall();
                            break;
                        default:
                            token = table.Pop();
                            statement = StateMentFactory.CreateStatementExpress();
                            token = table.Peek();
                            if (token != Token.End)
                            {
                                if (token.Value.ToString() == "=")
                                {
                                    statement = StateMentFactory.CreateStatementEquality();
                                }
                            }
                            break;
                    }
                    statement.SymbolTable = table;
                    statement.Index = i;
                    statements.Add(statement);

                }
            }
            return statements;
        }
        private StatementCollection BuildStateMentsTree(StatementCollection statements)
        {
            StatementCollection statement = new StatementCollection();
            while (statements.Current() != null)
            {
                StatementBase item = statements.Current();
                item.Build(statements);
                statement.Add(item);
            }
            return statement;
        }
 
    }

    public class SimpleFunctionBody: FunctionBody
    {
        public SimpleFunctionBody()
        { 
            ExcuteProxy excuteProxy = new ExcuteProxy();
            CBMethod cbmethod = new CBMethod();  
            excuteProxy.RunMethod = cbmethod;
            this.Script.ExecProxy = excuteProxy;
            CBMethod = cbmethod;
            this.CBMethod.Methods.Add(methodContainer);
        }
        public CBMethod CBMethod { get; set; }
        public void Add(CBMethodContainer methodContainer)
        {
            this.CBMethod.Methods.Add(methodContainer);
        }
        public void Add(BaseMethod method)
        {
            methodContainer.MethodList.Add(method);
        }
        CBMethodContainer methodContainer = new CBMethodContainer();
        public void InitDefaultFunction()
        { 
            this.CBMethod.Methods.Add(new StringFunctionContainer()); 
            this.CBMethod.Methods.Add(new DateTimeFunctionContainer());
            this.CBMethod.Methods.Add(new FileFunctionContainer());
            this.CBMethod.Methods.Add(new ConvertFunctionContainer());
            this.CBMethod.Methods.Add(new MathematicsFunctionContainer());
            this.CBMethod.Methods.Add(new TrigonometricFunctionContainer()); 
            this.CBMethod.Methods.Add(new ListFunctionContainer());
            this.CBMethod.Methods.Add(new DataTableFunctionContainer()); 
            this.CBMethod.Methods.Add(new SqlServerFunctionContainer());
            this.CBMethod.Methods.Add(new WebServiceFunctionContainer());
            this.CBMethod.Methods.Add(new FormFunctionContainer());
            //this.CBMethod.Methods.Add(new ConsoleFunctionContainer()); 
            this.CBMethod.Methods.Add(new StyleFunctionContainer());
            this.CBMethod.Methods.Add(new XMLFunctionContainer());
            this.CBMethod.Methods.Add(new JsonFunctionContainer());
            this.CBMethod.Methods.Add(new ReflectionContainer());
            //this.CBMethod.Methods.Add(new DebugFunctionContainer());
 
        }

        private static SimpleFunctionBody simplefunction;
        public static SimpleFunctionBody SimpleFunction {
            get {
                if (simplefunction == null)
                {
                    simplefunction = new SimpleFunctionBody();
                }
                return simplefunction;
            }
        }
    }


}
