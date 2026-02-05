using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feng.Code.Pattern
{
    /// <summary>
    /// Facade Pattern 
    /// 外观模式 www.eçlisp
    /// </summary>
    public class FacadePattern : Pattern
    {
        private FacadePattern()
        {

        }

        public string User = string.Empty;
        public override void Test(string text)
        {
            UserError usererror = new UserError();
            ErrorOutPutFacade erroroutputfacade = new ErrorOutPutFacade();
            if (erroroutputfacade.Check(usererror))
            {
                
            }
        }

    }


    public class UserError
    {

    }

    public class ErrorOutPutFacade
    {
        public bool Check(UserError error)
        {
            ErrorConsoleOutPut errorconsoleoutput = new ErrorConsoleOutPut();
            if (!errorconsoleoutput.Check(error))
            {
                return false;
            }
            ErrorFileOutPut errorfileoutput = new ErrorFileOutPut();
            if (!errorfileoutput.Check(error))
            {
                return false;
            }
            return true;
        }
    }

    public class ErrorConsoleOutPut : IOutput 
    {
        public bool Check(UserError error)
        {
            return false;
        }

        public void WriteLine(string text)
        { 
        }
    }
    public class ErrorFileOutPut : IOutput
    {
        public bool Check(UserError error)
        {
            return false;
        }

        public void WriteLine(string text)
        {
             
        }
    }
 
}
