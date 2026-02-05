using System.Collections.Generic;


namespace Feng.Script.CBEexpress
{
    public class CBScript
    {
        private static FunctionBody functionBody = new FunctionBody();
        public static object Exec(string script)
        {
            if (functionBody==null)
            {
                functionBody = new FunctionBody();
                ExcuteProxy excuteProxy = new ExcuteProxy();
                functionBody.Script.ExecProxy = excuteProxy;
            }
            return functionBody.Exec(script);
        }
    }
}
