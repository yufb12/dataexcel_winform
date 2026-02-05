using Feng.Script.CBEexpress;

namespace Feng.Script.Method
{
    public class CBMethod : IRunMethod
    {
        public CBMethod()
        {

        }

        private IMethodCollection _Methods = null;

        public IMethodCollection Methods
        {
            get
            {
                if (this._Methods == null)
                {
                    this._Methods = new MethodCollection();
                }
                return _Methods;
            }

            set { this._Methods = value; }
        }

        public object RunMethod(object item, string method, object[] args)
        {
            bool hasMethod = false;
            object value = this.Methods.RunMethod(method, ref hasMethod, args);
            return value;
        }
    }
}

