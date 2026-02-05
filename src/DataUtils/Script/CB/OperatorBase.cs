
namespace Feng.Script.CB
{

    /// <summary>
    /// !
    /// </summary>
    public abstract class OperatorBase
    {
        public abstract object Exec(object value1, object value2, Token token);
    }
    public abstract class ConvertBase
    {
        public abstract object ToObject(object value);
        public abstract decimal ToNumber(object value);
        public abstract decimal ToString(object value);
    }
}
