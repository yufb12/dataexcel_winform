//using Feng.Forms.Interface;
//using Feng.Script.CBEexpress;
//using System;

//namespace Feng.Script.ProgramSection
//{

//    public class SqlSelectStatementSection
//    {
//        public const int Success = 1;
//        public const int Fail = -1;

//       public IStateMentFactory StateMentFactory { get; set; }
//        private StatementCollection _statements = null;
//        public StatementCollection Statements
//        {
//            get { return _statements; }
//            set { _statements = value; }
//        }
//        public object Exec(IScript script, string text)
//        {
//            StatementCollection statements = Parse(text);
//            Statements = BuildStateMentsTree(statements);
//            return Exec(script);
//        }
//        public IScript Script { get; set; }
//        public object Exec(string text)
//        {
//            StatementCollection statements = Parse(text);
//            Statements = BuildStateMentsTree(statements);
//            return Exec(Script);
//        }

//        public StatementCollection Parse(string text)
//        {

//            StatementCollection statements = new StatementCollection();
//            StatementLexer lexer = new StatementLexer();
//            lexer.Parse(text);
//            if (lexer.List.Count > 0)
//            {
//                for (int i = 0; i < lexer.List.Count; i++)
//                {
//                    SymbolTable table = lexer.List[i];
//                    StatementBase statement = null;
//                    Token token = table.Peek();

//                    switch (token.Value.ToUpper())
//                    {
//                        case "IF":
//                            statement = StateMentFactory.CreateStateMentIF();
//                            break;
//                        case "ELSE":
//                            statement = StateMentFactory.CreateStateMentElse();
//                            break;
//                        case "ENDIF":
//                            statement = StateMentFactory.CreateStateMentEndIF();
//                            break;
//                        case "WHILE":
//                            statement = StateMentFactory.CreateStateWhile();
//                            break;
//                        case "ENDWHILE":
//                            statement = StateMentFactory.CreateStateEndWhile();
//                            break;
//                        default:
//                            statement = StateMentFactory.CreateStateMentExpress();
//                            break;
//                    }
//                    statement.SymbolTable = table;
//                    statement.Index = i;
//                    statements.Add(statement);

//                }
//            }
//            return statements;
//        }
//        public object Exec(IScript script)
//        {
//            if (Statements == null)
//                return Feng.Utils.Constants.Fail;
//            for (int i = 0; i < Statements.Count; i++)
//            {
//                StatementBase item = Statements[i];

//                Feng.Utils.TraceHelper.WriteTrace("", "StatementSection", "Exec", true, item.SymbolTable.ToString());

//                switch (item.Type)
//                {
//                    case StatementBase.Type_StateForeach:
//                        StatementForeach modelforeach = item as StatementForeach;
//                        DoForeachStatement(script, modelforeach);
//                        break;
//                    case StatementBase.Type_StateWhile:
//                        StateWhile modelwhile = item as StateWhile;
//                        DoWhileStatement(script, modelwhile);
//                        break;
//                    case StatementBase.Type_StateMentIF:
//                        StateMentIF modelif = item as StateMentIF;
//                        DoIfStatement(script, modelif);
//                        break;
//                    case StatementBase.Type_StateMentExpress:
//                        StateMentExpress modelexpress = item as StateMentExpress;
//                        modelexpress.Exec(script);
//                        break;
//                    default:
//                        throw new Script.SyntacticException("语句类型不正确," + item.Index + " " + item.ToString());
//                }

//            }

//            return Feng.Utils.Constants.OK;
//        }

//    }


//}
