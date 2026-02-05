using Feng.Script.CBEexpress;

namespace Feng.Script.Method
{
    public class BaseMethod : IMethodInfo
    {
        public virtual string Name
        {
            get;
            set;
        }

        public virtual string Description
        {
            get;
            set;
        }
        public virtual string Eg
        {
            get;
            set;
        }
        public delegate object FunctionHandler(params object[] args);
        public FunctionHandler Function { get; set; }
        private bool _isMethod = false;
        public virtual bool IsMethod
        {
            get
            {
                return this._isMethod;
            }
            set { this._isMethod = value; }
        }
        public virtual object Exec(params object[] args)
        {
            if (Function != null)
            {
                object value = Function(args);
                return value;
            }
            throw new CBException("Function " + this.Name + " Not Found!");
        }
        public bool HasMethod(string method)
        {
            if (this.Name.ToUpper() == method)
            {
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return Name + "__" + Description;
        }
    }
}
