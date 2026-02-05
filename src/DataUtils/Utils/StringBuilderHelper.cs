using System.Text;

namespace Feng.Utils
{
    public class StringBuilderHelper
    {
        public StringBuilder sb = new StringBuilder();
        public void RemoveChar(string value)
        {
            string text = sb.ToString();
            if (text .EndsWith (value ))
            {
                text.TrimStart(value.ToCharArray());
            }
        }
        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
