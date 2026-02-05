using Feng.Script.CBEexpress;
using Feng.Script.Method;
using System;
using System.Collections.Generic;

namespace Feng.Script.FunctionContainer
{
    public class CFusionMethodContainer : CBMethodContainer
    {
        public string FullName { get; set; }
        public const string Function_Category = "CFusion";
        public const string Function_Description = "CFusion";
        public override string Name
        {
            get { return Function_Category; }

        }
        public override string Description
        {
            get { return Function_Description; }
        }

        public CFusionMethodContainer()
        {

            BaseMethod model = null;

            model = new BaseMethod();
            model.Name = "CFusion";
            model.Description = @"CFusion()";
            model.Eg = @"CFusion("")";
            model.Function = this.CFusion;
            MethodList.Add(model);


            model = new BaseMethod();
            model.Name = "CFOpen";
            model.Description = @"CFOpen()";
            model.Eg = @"CFOpen("")";
            model.Function = this.CFOpen;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CFGet";
            model.Description = @"CFGet()";
            model.Eg = @"CFGet("")";
            model.Function = this.CFGet;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CFSet";
            model.Description = @"CFSet()";
            model.Eg = @"CFSet("")";
            model.Function = this.CFSet;
            MethodList.Add(model);

            model = new BaseMethod();
            model.Name = "CFRun";
            model.Description = @"CFRun()";
            model.Eg = @"CFRun("")";
            model.Function = this.CFRun;
            MethodList.Add(model);



            model = new BaseMethod();
            model.Name = "ZJF";
            model.Description = @"ZJF()";
            model.Eg = @"ZJF("")";
            model.Function = this.CFusion;
            MethodList.Add(model);


        }

        public virtual object CFusion(params object[] args)
        {
            string txt = base.GetTextValue(1, args);
            CFusion cfusion = Feng.Script.CFusion.Load(txt);
            return cfusion;
        }
        public virtual object CFOpen(params object[] args)
        {
            ICBContext cbcontext = args[0] as ICBContext;
            string file = base.GetTextValue(1, args);
            string txt = System.IO.File.ReadAllText(file, System.Text.Encoding.Unicode);
            CFusion cfusion = Feng.Script.CFusion.Load(txt);
            cfusion.File = file;
            return cfusion;
        }
        public virtual object CFGet(params object[] args)
        {
            ICBContext cbcontext = args[0] as ICBContext;
            CFusion cfusion = base.GetValue(1, args) as CFusion;
            if (cfusion == null)
                throw new CBException("参数不正确");//The parameter is incorrect.
            string name = base.GetTextValue(1, args);
            object obj = cfusion.GetValue(name);
            return obj;
        }
        public virtual object CFSet(params object[] args)
        {
            ICBContext cbcontext = args[0] as ICBContext;
            CFusion cfusion = base.GetValue(1, args) as CFusion;
            if (cfusion == null)
                throw new CBException("参数不正确");//The parameter is incorrect.
            string name = base.GetTextValue(2, args);
            object obj = base.GetValue(3, args);
            cfusion.SetValue(name, obj);
            return base.OK;
        }
        public virtual object CFRun(params object[] args)
        {
            ICBContext cbcontext = args[0] as ICBContext;
            CFusion cfusion = base.GetValue(1, args) as CFusion;
            if (cfusion == null)
                throw new CBException("参数不正确");//The parameter is incorrect.
            string methodname = base.GetTextValue(2, args);
            List<object> list = new List<object>();
            for (int i = 3; i < args.Length; i++)
            {
                object ob = base.GetValue(i, args);
                list.Add(ob);
            }
            object obj = cfusion.Run(cbcontext, methodname, list);
            return obj;
        }
        public virtual object CFVersion(params object[] args)
        {
            return "2.6.7.6";
        }
    }

}
namespace Feng.Script
{

    public class CFusion
    {

        public CFusion()
        {
            File = string.Empty;
        }
        public string File { get; set; }
        private List<NetStatementBase> NetStatements { get; set; }
        private NetVarCollection varStack = new NetVarCollection();
        NetSkip netInterrupt = new NetSkip();
        ICBContext Context { get; set; }
        public static CFusion Load(string script)
        {
            CFusion fusion = new CFusion();
            fusion.Init(script);
            return fusion;
        }
        private void Init(string script)
        {
            TokenPool tokenPool = GetTokenPool(script);
            List<NetStatementBase> list = NetStatementBuilder.GetNetStatements(tokenPool);
            NetStatements = list;
        }
        internal object GetValue(string name)
        {
            object result = varStack.GetVarValue(name, null);
            return result;
        }

        internal void SetValue(string name, object obj)
        {
            varStack.SetVarValue(name, obj);
        }
        internal static TokenPool GetTokenPool(string txt)
        {
            Lexer lexer = new Lexer(txt);
            lexer.Parse();
            return lexer.TokenPool;
        }

        internal object Run(ICBContext cbcontext, string methodname, List<object> list)
        {
            object result = null;
            try
            {

                NetVarList varlist = new NetVarList("ROOT");
                varStack.Add(varlist);
                cbcontext.Clear();
                bool findfunction = false;
                for (int i = 0; i < NetStatements.Count; i++)
                {
                    NetStatementBase netStatement = NetStatements[i];
                    NetStatementFunction netStatementFunction = netStatement as NetStatementFunction;
                    if (netStatementFunction == null)
                        continue;
                    string functionname = netStatementFunction.GetFunctionName();
                    if (functionname != methodname)
                        continue;
                    findfunction = true;
                    result = netStatementFunction.Run(varStack, cbcontext, list);
                    break;
                }
                if (!findfunction)
                {
                    throw new Exception("未找到函数:" + methodname);
                }
                if (varStack.HasVarValue("netresult"))
                {
                    result = varStack.GetVarValue("netresult", null);
                }
            }
            catch (SyntacticException ex)
            {
                string msg = ex.Message + "\r\n";
                ex.SetMsg(msg);
                throw ex;
            }
            return result;
        }
    }
}