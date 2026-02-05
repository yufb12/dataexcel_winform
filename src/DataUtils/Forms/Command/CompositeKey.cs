using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Feng.Forms.Command
{
    public delegate void SimpleCommand(string arg);
    public class CommandObjectTool
    {
        public static CommandObject GetCommandObject(string parentgroupname, string groupname,
            string grouptitle, string commandname, string description, SimpleCommand command)
        {
            string commandtext = commandname;
            Keys firstkey = Keys.None;
            Keys sencondkey = Keys.None;
            string firstkeytext = string.Empty;
            string sencondkeytext = string.Empty;
            CommandObject commandObject = new CommandObject(parentgroupname, groupname,
              grouptitle, commandname, commandtext, firstkey, sencondkey,
              firstkeytext, sencondkeytext, description, command);
            return commandObject;
        }
    }

    public class CommandObject
    {
        public CommandObject()
        { }
        public CommandObject(string parentgroupname, string groupname, string grouptitle, string commandname, string commandtext, Keys firstkey, Keys sencondkey,
            string firstkeytext, string sencondkeytext, string description, SimpleCommand command)
        {
            ParentGroupName = parentgroupname;
            GroupName = groupname;
            GroupTitle = grouptitle;
            CommandName = commandname;
            FirstKey = firstkey;
            SencondKey = sencondkey;
            FirstKeyText = firstkeytext;
            SencondKeyText = sencondkeytext;
            CommandText = commandtext;
            Description = description;
            Command = command;
        }
        public CommandObject(string parentgroupname, string groupname, string grouptitle, string commandname, string commandtext, Keys firstkey, Keys sencondkey,
    string firstkeytext, string sencondkeytext, string description, SimpleCommand command,bool undoredo)
        {
            ParentGroupName = parentgroupname;
            GroupName = groupname;
            GroupTitle = grouptitle;
            CommandName = commandname;
            FirstKey = firstkey;
            SencondKey = sencondkey;
            FirstKeyText = firstkeytext;
            SencondKeyText = sencondkeytext;
            CommandText = commandtext;
            Description = description;
            Command = command;
            UndoRendo = undoredo;
        }
        public string GroupName { get; set; }
        public string GroupTitle { get; set; }
        public string ParentGroupName { get; set; }
        public string CommandName { get; set; }
        public Keys FirstKey { get; set; }
        public Keys SencondKey { get; set; }
        public string FirstKeyText { get; set; }
        public string SencondKeyText { get; set; }
        public string CommandText { get; set; }
        public string Description { get; set; }
        public SimpleCommand Command { get; set; }
        public string CommandArgs { get; set; }
        private bool undorendo = true;
        public bool UndoRendo { get { return undorendo; } set { undorendo = value; } }
        private bool visible = true;
        public bool Visible { get { return visible; } set { visible = value; } }

        public override string ToString()
        {
            return string.Format("Text={0},Args={1},Description={2},FirstKeyText={3},SencondKey={4}", CommandText, CommandArgs, Description, FirstKeyText, SencondKey);
        }
    }

    public class CompositeKey2
    {
        private List<CommandObject> commands = null;
        public List<CommandObject> Commands
        {
            get
            {
                if (commands == null)
                {
                    commands = new List<CommandObject>();
                }
                return commands;
            }
        }

        public CommandObject GetCommand(Keys key)
        {
            CommandObject cmd = null;
            foreach (CommandObject command in Commands)
            {
                if (command.FirstKey == key)
                {
                    if (cmd != null)
                    {
                        if (System.Diagnostics.Debugger.IsAttached)
                        {
                            Feng.Utils.MsgBox.ShowInfomation("快捷键重复!");
                            //return null;
                        }
                    }
                    cmd = command;
                }
            }
            return cmd;
        }
        public CommandObject GetCommand(Keys key, Keys keymul)
        {
            foreach (CommandObject command in Commands)
            {
                if (command.CommandText == "CommandCopyAll")
                {

                }
                //Keys k1 = command.FirstKey & key;
                //Keys k2 =  keymul;
                if (command.FirstKey == key && command.SencondKey == keymul)
                    return command;
            }
            return null;
        }
        public CommandObject GetCommand(string commandtext)
        {
            foreach (CommandObject command in Commands)
            {
                if (command.CommandText == commandtext)
                    return command;
            }
            return null;
        }
        public virtual void Add(CommandObject command)
        {
            Commands.Add(command);

        }
        [System.Diagnostics.Conditional("DEBUG")]
        public void CommandCheck()
        {
            List<string> list = new List<string>();
            List<CommandObject> listcommands = new List<CommandObject>();
            foreach (CommandObject command in Commands)
            {
                string text = command.FirstKey.ToString() + command.SencondKey.ToString();
                if (list.Contains(text))
                {
                    string txt = command.CommandText;
                    //System.Diagnostics.Debugger.Break();
                    listcommands.Add(command);
                }
                list.Add(text);
            }
            foreach (CommandObject command in Commands)
            {
                foreach (CommandObject command2 in listcommands)
                {
                    if (command.FirstKey == command2.FirstKey && command.SencondKey == command2.SencondKey)
                    {
                        //Feng.Utils.TraceHelper.WriteTrace("DEBUG", "CommandCheck", command.CommandName, command2.CommandName);
                        listcommands.Remove(command2);
                        break;
                    }
                }
            }
        }
        public void Exec(string commandtext)
        {
            CommandObject command = GetCommand(commandtext);
            if (command != null)
            {
                command.Command(string.Empty);
            }
        }

        public void Exec(string commandtext, string arg)
        {
            CommandObject command = GetCommand(commandtext);
            if (command != null)
            {
                command.Command(arg);
            }
        }

        public void Exec(CommandObject command, string arg)
        {
            if (command != null)
            {
                command.Command(arg);
            }
        }
        private List<CommandObject> remembercommanditems = new List<CommandObject>();
        public virtual List<CommandObject> RememberCommandItems
        {
            get
            {
                return remembercommanditems;
            }
        }
        private bool beginremembercommands = false;
        public virtual bool RememberCommands
        {
            get
            {
                return beginremembercommands;
            }
        }
        public virtual void BeginRememberCommands()
        {
            RememberCommandItems.Clear();
            beginremembercommands = true;
        }
        public virtual void EndRememberCommands()
        {
            beginremembercommands = false;
        }

        public virtual CommandObject CommandExcute(string commandtext, string arg)
        {
            Feng.Forms.Command.CommandObject cmd = this.GetCommand(commandtext);
            if (cmd == null)
            {
                Utils.TraceHelper.WriteTrace("DataUtils", "CompositeKey", "CommandExcute", true, commandtext);
                return null;
            }
            if (cmd.UndoRendo)
            {

            }
            cmd.Command(arg);
            if (RememberCommands)
            {
                RememberCommandItems.Add(cmd);
            }
            CommandHistory.Add(cmd);
            return cmd;
        }
        private List<CommandObject> commandhistory = new List<CommandObject>();
        public virtual List<CommandObject> CommandHistory
        {
            get
            {
                return commandhistory;
            }
        }
        public string GetInfo()
        {
            System.Text.StringBuilder sb = new StringBuilder();

            foreach (CommandObject command in Commands)
            {
                sb.AppendLine(string.Format("{0}  {1}  {2}  {3}", command.Description,
                    command.CommandText,
                    command.CommandArgs,
                    command.FirstKeyText + "," + command.SencondKeyText));

            }
            return sb.ToString();
        }
    }


}
