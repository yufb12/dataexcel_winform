using Feng.Script.CBEexpress;

namespace Feng.Script.Method
{
    public class CBMethodContainer : BaseMethodContainer
    {
        public CBMethodContainer()
        {

        }

        public override string Name { get { return "CBMethod"; }   }
        public override string Description
        {
            get { return string.Empty; }
        }
        public override object GetValue(int index, params object[] args)
        {
            object value = GetArgIndex(index, args);
            return value;

        }
        public readonly int OK = 1;
        public readonly int Cancel = 0;
        public readonly int Fail = 0;
        public readonly byte TRUE = 1;
        public readonly byte FALSE = 0;
    }
}

