using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;
using System.Collections.Generic;

namespace Feng.Script.FunctionContainer
{

    public class DebugMethodContainer : CBMethodContainer
    {
        public string FullName { get; set; }
        public const string Function_Category = "Debug";
        public const string Function_Description = "Debug";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }

        public DebugMethodContainer()
        {

            BaseMethod model = null;

            model = new BaseMethod();
            model.Name = "DebugBreak";
            model.Description = @"DebugBreak()";
            model.Eg = @"DebugBreak("")";
            model.Function = this.DebugBreak;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DebugLog";
            model.Description = @"DebugLog()";
            model.Eg = @"DebugLog("")";
            model.Function = this.DebugLog;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "DebugFunctionName";
            model.Description = @"DebugFunctionName()";
            model.Eg = @"DebugFunctionName("")";
            model.Function = this.DebugFunctionName;
            MethodList.Add(model);

        }

        public virtual object DebugBreak(params object[] args)
        {
            ICBContext cbcontext = args[0] as ICBContext;
            if (cbcontext != null)
            {
                if (cbcontext.Debug != null)
                {
                    cbcontext.Debug.SetCommand(DebugCommand.StepInto);
                    return base.OK;
                }
            }
            return base.Fail;
        }

        public virtual object DebugLog(params object[] args)
        {
            ICBContext cbcontext = args[0] as ICBContext;
            if (cbcontext != null)
            {
                if (cbcontext.Debug != null)
                {
                    string text = cbcontext.Debug.CurrentStatement.ToString();
                    Console.WriteLine(text);
                    return base.OK;
                }
            }
            return base.Fail;
        }

        public virtual object DebugFunctionName(params object[] args)
        {
            ICBContext cbcontext = args[0] as ICBContext;
            if (cbcontext != null)
            {
                return cbcontext.FunctionName;
            }
            return string.Empty;
        }
    }

}
 