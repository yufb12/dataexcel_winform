using System;

namespace Feng.Json
{
    /// <summary>
    /// &&
    /// </summary>
    public class JsonException : Exception
    {
        public JsonException()
        {
        }
        public JsonException(string msg):base(msg)
        {

        }
    }
}