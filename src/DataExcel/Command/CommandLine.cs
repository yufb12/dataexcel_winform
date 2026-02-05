using Feng.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Excel.Commands
{
    public class CommandLine
    {
        public System.Collections.Generic.Dictionary<string, CommandTextHandler> dics = new Dictionary<string, CommandTextHandler>();
        public void Regedit(string name, CommandTextHandler handler)
        {
            if (dics.ContainsKey(name))
            {
                dics.Add(name, handler);
            }
        }
        public bool Exists(string name)
        {
            return dics.ContainsKey(name);
        }
        public CommandTextHandler GetHandler(string name)
        {
            if (dics.ContainsKey(name))
            {
                return dics[name];
            }
            return null;
        }
        public void Execute(object sender, ICommandText cmd)
        {
            CommandTextHandler handler = GetHandler(cmd.Name);
            if (handler != null)
            {
                handler(sender, cmd.Name, cmd.Args);
            }
        }
    }
}