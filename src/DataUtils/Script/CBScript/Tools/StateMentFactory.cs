namespace Feng.Script.CBEexpress
{
    public interface IStateMentFactory
    {
        StatementExpress CreateStatementExpress();
        StatementWhile CreateStatementWhile();
        StatementEndWhile CreateStatementEndWhile();
        StatementIF CreateStatementIF();
        StatementIFElse CreateStatementElse();
        StatementEndIF CreateStatementEndIF();
        StatementReturn CreateStatementReturn();
        StatementVar CreateStatementVar();
        StatementEquality CreateStatementEquality();
        StatementForeach CreateStatementForeach();
        StatementEndForeach CreateStatementEndForeach();
        StatementBreak CreateStatementBreak();
        StatementFor CreateStatementFor();
        StatementEndFor CreateStatementEndFor();
        StatementTry CreateStatementTry();
        StatementCatch CreateStatementCatch();
        StatementProc CreateStatementProc();
        StatementEndProc CreateStatementEndProc();
        StatementCall CreateStatementCall();
        StatementLBRACE CreateStatementLBRACE();
        StatementRBRACE CreateStatementRBRACE();
    }
    public class StateMentFactory : IStateMentFactory
    {
        public StateMentFactory()
        {
        }
        public StatementWhile CreateStatementWhile()
        {
            return new StatementWhile();
        }
        public StatementEndWhile CreateStatementEndWhile()
        {
            return new StatementEndWhile();
        }
        public StatementIFElse CreateStatementElse()
        {
            return new StatementIFElse();
        }
        public StatementEndIF CreateStatementEndIF()
        {
            return new StatementEndIF();
        }
        public StatementExpress CreateStatementExpress()
        {
            return new StatementExpress();
        }
        public StatementIF CreateStatementIF()
        {
            return new StatementIF();
        }
        public StatementReturn CreateStatementReturn()
        {
            return new StatementReturn();
        }
        public StatementVar CreateStatementVar()
        {
            return new StatementVar();
        }
        public StatementEquality CreateStatementEquality()
        {
            return new StatementEquality();
        }
        public StatementForeach CreateStatementForeach()
        {
            return new StatementForeach();
        }
        public StatementEndForeach CreateStatementEndForeach()
        {
            return new StatementEndForeach();
        }
        public StatementBreak CreateStatementBreak()
        {
            return new StatementBreak();
        }
        public StatementFor CreateStatementFor()
        {
            return new StatementFor();
        }
        public StatementEndFor CreateStatementEndFor()
        {
            return new StatementEndFor();
        }
        public StatementTry CreateStatementTry()
        {
            return new StatementTry();
        }
        public StatementCatch CreateStatementCatch()
        {
            return new StatementCatch();
        }

        public StatementProc CreateStatementProc()
        {
            return new StatementProc();
        }
        public StatementEndProc CreateStatementEndProc()
        {
            return new StatementEndProc();
        }
        public StatementCall CreateStatementCall()
        {
            return new StatementCall();
        }

        public StatementLBRACE CreateStatementLBRACE()
        {
            return new StatementLBRACE();
        }
        public StatementRBRACE CreateStatementRBRACE()
        {
            return new StatementRBRACE();
        }
    }

}
