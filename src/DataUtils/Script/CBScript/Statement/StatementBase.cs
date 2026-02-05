using Feng.Forms.Interface;
using System.Collections.Generic;

namespace Feng.Script.CBEexpress
{
    public abstract class StatementBase : IText, IIndex
    { 
        public virtual string Text { get; set; }
        public virtual int Index { get; set; }
        public virtual SymbolTable SymbolTable { get; set; }
        public abstract object Exec(IBreak parent,ICBEexpress script);
        public virtual void Build(StatementCollection statements)
        {
            statements.Pop();
        }
        public virtual string FormatText { get; set; }

        public virtual void Build(List<SymbolTable> List)
        {
        }
    }



}
