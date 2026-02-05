using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Feng.Code.Pattern
{
    /// <summary>
    /// Composite Pattern 
    /// 组合模式
    /// </summary>
    public class CompositePattern : Pattern
    {
        private CompositePattern()
        {

        }

        public string User = string.Empty;
        public override void Test(string text)
        {
            TreeNode root = new TreeNode();
            root.Add(new TreeNode() { Name = "Node1" });
            root.Add(new TreeNode() { Name = "Node2" });
            root.Add(new TreeNode() { Name = "Node3" });
            root.Add(new TreeNode() { Name = "Node4" });
            root.Add(new TreeLeaf() { Name = "Node4" });
        }

    }
    public interface IPaint
    {
        void Draw();
    }
    public abstract class Node : IPaint
    {
        public string Name { get; set; }

        public abstract void Draw();
    }
    public class TreeNode : Node
    {
        public List<Node> Nodes = new List<Node>();
        public void Add(Node node)
        {
            Nodes.Add(node);
        }
        public void Remove(Node node)
        {
            Nodes.Remove(node);
        }
        public override void Draw()
        {
            Console.WriteLine("node");
            foreach (TreeNode node in Nodes)
            {
                node.Draw();
            }
        }
    }
    public class TreeLeaf : Node
    {
        public override void Draw()
        {
            Console.WriteLine("Leaf");
        }
    }
}
