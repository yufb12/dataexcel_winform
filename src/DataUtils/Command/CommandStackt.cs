using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Commands
{
    public interface ICommand
    {
        ICommand Execute();
    }
    public class CommandStackt
    {
        Stack<ICommand> Unstack;
        Stack<ICommand> Restack;
        bool lck = false;
        public CommandStackt()
        {
            Unstack = new Stack<ICommand>();
            Restack = new Stack<ICommand>();
        }

        public void Add(ICommand obj)
        {
            if (lck)
                return;
            Unstack.Push(obj);
            Restack.Clear();
        }

        public void Clear()
        {
            Unstack.Clear();
            Restack.Clear();
        }

        public void Execute()
        {
            try
            {
                lck = true;
                if (this.Unstack.Count < 1)
                {
                    return;
                }
                ICommand obj = Unstack.Pop();
                Restack.Push(obj.Execute());
            } 
            finally
            {
                lck = false;
            }

        }

        public void Redo()
        {
            try
            {
                lck = true;
                if (this.Restack.Count < 1)
                {
                    return;
                }
                ICommand obj = Restack.Pop();
                Unstack.Push(obj.Execute());
            }
            finally
            {
                lck = false;
            }
        }
    }
}
