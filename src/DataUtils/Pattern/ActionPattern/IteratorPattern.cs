using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;

namespace Feng.Code.Pattern
{
    /// <summary>
    /// Iterator Pattern 
    /// 迭代器模式 
    /// </summary>
    public class IteratorPattern : Pattern
    {
        private IteratorPattern()
        {

        }

        public string User = string.Empty;
        public override void Test(string text)
        {
            NodeCollection list = new NodeCollection();
            ObjectIterator iterator = list.GetIterator();
            while (iterator.MoveNext())
            {
                object value = iterator.Next();
            }
        }

    }
     

    public interface IIterator
	{
        bool MoveNext();
        Node Next();
        Node Current { get; set; }
        void First();
    }

    public class NodeCollection
    {
        public List<Node> List = new List<Node>();
        public ObjectIterator GetIterator()
        {
            return new ObjectIterator(this);
        }
    }

    public class ObjectIterator : IIterator 
    {
        public int Index = 0;
        NodeCollection nodes = null;
        public ObjectIterator(NodeCollection list)
        {
            nodes = list;
        }

        public bool MoveNext()
        {
            if (Index < nodes.List.Count - 1)
            {
                return true;
            }
            return false;
        }

        public Node Next()
        {
            Index++;
            return nodes.List[Index];
        }

        public Node Current
        {
            get
            {
                return nodes.List[Index];
            }
            set
            {
                nodes.List[Index] = value;
            }
        }

        public void First()
        {
            Index = 0;
        }
    }

}
