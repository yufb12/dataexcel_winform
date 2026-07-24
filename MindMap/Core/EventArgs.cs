using System;

namespace MindMap.Core
{
    /// <summary>
    /// 节点事件参数基类
    /// </summary>
    public class NodeEventArgs : EventArgs
    {
        /// <summary>
        /// 获取关联的节点
        /// </summary>
        public MindMapNode Node { get; private set; }

        /// <summary>
        /// 初始化节点事件参数
        /// </summary>
        /// <param name="node">关联的节点</param>
        public NodeEventArgs(MindMapNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");
            
            Node = node;
        }
    }

    /// <summary>
    /// 节点文本变化事件参数
    /// </summary>
    /// <summary>
    /// 连接线事件参数
    /// </summary>
    public class ConnectionEventArgs : EventArgs
    {
        /// <summary>
        /// 获取关联的连接线
        /// </summary>
        public Connection Connection { get; private set; }

        /// <summary>
        /// 初始化连接线事件参数
        /// </summary>
        /// <param name="connection">关联的连接线</param>
        public ConnectionEventArgs(Connection connection)
        {
            if (connection == null)
                throw new ArgumentNullException("connection");
            
            Connection = connection;
        }
    }


    public class NodeTextChangedEventArgs : NodeEventArgs
    {
        /// <summary>
        /// 获取变化前的文本
        /// </summary>
        public string OldText { get; private set; }

        /// <summary>
        /// 获取变化后的文本
        /// </summary>
        public string NewText { get; private set; }

        /// <summary>
        /// 初始化节点文本变化事件参数
        /// </summary>
        /// <param name="node">关联的节点</param>
        /// <param name="oldText">变化前的文本</param>
        /// <param name="newText">变化后的文本</param>
        public NodeTextChangedEventArgs(MindMapNode node, string oldText, string newText)
            : base(node)
        {
            OldText = oldText;
            NewText = newText;
        }
    }

    /// <summary>
    /// 节点选中变化事件参数
    /// </summary>
    public class SelectionChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 获取之前选中的节点
        /// </summary>
        public MindMapNode OldSelectedNode { get; private set; }

        /// <summary>
        /// 获取当前选中的节点
        /// </summary>
        public MindMapNode NewSelectedNode { get; private set; }

        /// <summary>
        /// 初始化选中变化事件参数
        /// </summary>
        /// <param name="oldNode">之前选中的节点</param>
        /// <param name="newNode">当前选中的节点</param>
        public SelectionChangedEventArgs(MindMapNode oldNode, MindMapNode newNode)
        {
            OldSelectedNode = oldNode;
            NewSelectedNode = newNode;
        }
    }

    /// <summary>
    /// 文档变更事件参数
    /// </summary>
    public class DocumentChangedEventArgs : EventArgs
    {
        /// <summary>
        /// 获取变更类型
        /// </summary>
        public string ChangeType { get; private set; }

        /// <summary>
        /// 初始化文档变更事件参数
        /// </summary>
        /// <param name="changeType">变更类型</param>
        public DocumentChangedEventArgs(string changeType)
        {
            ChangeType = changeType;
        }
    }

    /// <summary>
    /// 命中测试结果
    /// </summary>
    public class HitTestResult
    {
        /// <summary>
        /// 获取或设置命中类型
        /// </summary>
        public HitTestResultType ResultType { get; set; }

        /// <summary>
        /// 获取或设置命中的节点
        /// </summary>
        public MindMapNode Node { get; set; }

        /// <summary>
        /// 获取一个值，指示是否命中了任何元素
        /// </summary>
        public bool IsHit
        {
            get { return ResultType != HitTestResultType.None; }
        }

        /// <summary>
        /// 创建一个未命中的结果
        /// </summary>
        public static HitTestResult None
        {
            get { return new HitTestResult { ResultType = HitTestResultType.None }; }
        }

        /// <summary>
        /// 创建一个命中节点的结果
        /// </summary>
        /// <param name="node">命中的节点</param>
        public static HitTestResult NodeHit(MindMapNode node)
        {
            return new HitTestResult
            {
                ResultType = HitTestResultType.Node,
                Node = node
            };
        }

        /// <summary>
        /// 创建一个命中展开按钮的结果
        /// </summary>
        /// <param name="node">关联的节点</param>
        public static HitTestResult ExpandButtonHit(MindMapNode node)
        {
            return new HitTestResult
            {
                ResultType = HitTestResultType.ExpandButton,
                Node = node
            };
        }
    }
}
