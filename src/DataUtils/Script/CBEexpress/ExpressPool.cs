using System.Collections.Generic;
using System.Text;

namespace Feng.Script.CBEexpress
{
    public class ExpressPool
    {
        public ExpressPool()
        {
            Expresss = new List<NetExpressBase>();
        }
        public List<NetExpressBase> Expresss { get; set; }
        int position = 0;
        public NetExpressBase Pop()
        {
            if (position < Expresss.Count)
            {
                NetExpressBase token = Expresss[position];
                position++;
                return token;
            }
            throw new SyntacticException(null, "语法解析错误，已琶最后", CBEexpressExCode.ERRORCODE_11222);
        }
        internal void ReSet()
        {
            position = 0;
        }
        public NetExpressBase Peek()
        {
            if (position < Expresss.Count)
            {
                NetExpressBase token = Expresss[position];
                return token;
            }
            throw new SyntacticException(null, "语法解析错误，已至最后", CBEexpressExCode.ERRORCODE_11223);
        }
        public NetExpressBase Back()
        {
            if (position < Expresss.Count)
            {
                NetExpressBase token = Expresss[position];
                position--;
                return token;
            }
            throw new SyntacticException(null, "语法解析错误，已琶最后", CBEexpressExCode.ERRORCODE_11225);
        }
        public void Push(NetExpressBase expres)
        {
            Expresss.Add(expres);
        }
        public NetExpressBase Current
        {
            get
            {
                if (position < Expresss.Count)
                {
                    NetExpressBase token = Expresss[position];
                    return token;
                }
                throw new SyntacticException(null, "语法解析错误，已琶最后", CBEexpressExCode.ERRORCODE_11226);
            }
        }
        public virtual int Count
        {
            get
            {
                return Expresss.Count;
            }
        }
        public bool HasNext()
        {
            return position < this.Expresss.Count;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (NetExpressBase item in Expresss)
            {
                sb.Append(item.ToString());
            }
            return sb.ToString();
        }
    }
}
