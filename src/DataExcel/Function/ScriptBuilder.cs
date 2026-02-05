using Feng.Excel.Interfaces;
using Feng.Excel.Script;
using Feng.Script.CBEexpress;
using System;
using System.Collections.Generic;

namespace Feng.Excel.Builder
{
    public class ScriptBuilder
    { 
        public static NetParser BuilderScript(DataExcel grid, ICell cell)
        {
            NetParser netParser= new NetParser();
            if (grid != null)
            {
                foreach (IMethod item in grid.Methods)
                {
                    netParser.AddFunction(item);
                }
            }
            FunctionArg proxy = new FunctionArg() {  CurrentCell =cell , Grid =grid };
            netParser.Entity = proxy;
            netParser.AddVar("this", grid);
            netParser.AddVar("ME", cell);
            return netParser;
        }

        public static object Exec(DataExcel grid, ICell cell, string txt, params object[] vaues)
        {
            NetParser script = null;
            script = ScriptBuilder.BuilderScript(grid, cell);
            if (vaues != null)
            {
                BuilderValue(script, vaues);
            } 
            return script.Exec(txt);
        }

        private static void BuilderValue(NetParser script, Feng.Collections.DictionaryEx<string, object> dics)
        {
            foreach (var item in dics)
            { 
                script.AddVar(item.Key, item.Value);
            }
        }
 
        private static void BuilderValue(NetParser script, object[] vaues)
        {
            for (int i = 0; i < vaues.Length; i++)
            {
                string key = Feng.Utils.ConvertHelper.ToString(vaues[i]);
                if ((i + 1) < vaues.Length)
                {
                    object value = vaues[i + 1];
                    script.AddVar(key, value);
                }
                i++;
            }
        }

        public static object Exec2(DataExcel grid, ICell cell, string txt, Feng.Collections.DictionaryEx<string, object> dics)
        {
            NetParser script = null;
            script = ScriptBuilder.BuilderScript(grid, cell);
            if (dics != null)
            {
                BuilderValue(script, dics);
            } 
            return script.Exec(txt);
        }

        public static bool LogOutWatch = true;
        public static object Exec(DataExcel grid, ICell cell, string txt)
        {
            NetParser script = null;
            script = ScriptBuilder.BuilderScript(grid, cell);
            return script.Exec(txt);

        }
        public static object ExecExpress(DataExcel grid, ICell cell, string txt)
        {
            NetParser script = null;
            script = ScriptBuilder.BuilderScript(grid, cell);
            return script.ExecExpress(txt);

        }
        public static object ExecExpress(DataExcel grid, ICell cell, string txt, params object[] vaues)
        {
            NetParser script = null;
            script = ScriptBuilder.BuilderScript(grid, cell);
            if (vaues != null)
            {
                BuilderValue(script, vaues);
            }
            return script.ExecExpress(txt);

        }
        public static string Debug(DataExcel grid, ICell cell, string txt)
        { 
            NetParser script = ScriptBuilder.BuilderScript(grid, cell);
            object result = script.Exec(txt);
            return Feng.Utils.ConvertHelper.ToString(result);
        }

 
    }
}