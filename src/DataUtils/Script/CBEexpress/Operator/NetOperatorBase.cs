namespace Feng.Script.CBEexpress
{

    public abstract class NetOperatorBase
    {
        public abstract object Exec(object value1, object value2, NetVarCollection varStack, ICBContext methodProxy);
        public abstract ushort Index { get; }
    }
}