
namespace Feng.Script.CB
{
    public abstract class NetOperatorBase
    {
        public abstract object Exec(object value1, object value2, NetVarCollection varStack, IMethodProxy methodProxy);
        public abstract ushort Index { get; }
    }
}
