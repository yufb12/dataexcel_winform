using System;

namespace MindMap.Core
{
    /// <summary>
    /// 【SRP单一职责】思维导图节点 - 事件部分
    /// 负责：事件定义与触发
    /// 【设计模式】Observer观察者模式
    /// </summary>
    public partial class MindMapNode
    {
        #region 事件定义

        /// <summary>
        /// 属性变化事件
        /// </summary>
        [field: NonSerialized]
        public event EventHandler PropertyChanged;

        /// <summary>
        /// 子节点添加事件
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<NodeEventArgs> ChildAdded;

        /// <summary>
        /// 子节点移除事件
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<NodeEventArgs> ChildRemoved;

        /// <summary>
        /// 连接线添加事件
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<ConnectionEventArgs> ConnectionAdded;

        /// <summary>
        /// 连接线移除事件
        /// </summary>
        [field: NonSerialized]
        public event EventHandler<ConnectionEventArgs> ConnectionRemoved;

        #endregion

        #region 事件触发方法

        /// <summary>
        /// 触发属性变化事件
        /// </summary>
        protected virtual void OnPropertyChanged()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 触发子节点添加事件
        /// </summary>
        protected virtual void OnChildAdded(MindMapNode child)
        {
            if (ChildAdded != null)
            {
                ChildAdded(this, new NodeEventArgs(child));
            }
        }

        /// <summary>
        /// 触发子节点移除事件
        /// </summary>
        protected virtual void OnChildRemoved(MindMapNode child)
        {
            if (ChildRemoved != null)
            {
                ChildRemoved(this, new NodeEventArgs(child));
            }
        }

        /// <summary>
        /// 触发连接线添加事件
        /// </summary>
        protected virtual void OnConnectionAdded(Connection conn)
        {
            if (ConnectionAdded != null)
            {
                ConnectionAdded(this, new ConnectionEventArgs(conn));
            }
        }

        /// <summary>
        /// 触发连接线移除事件
        /// </summary>
        protected virtual void OnConnectionRemoved(Connection conn)
        {
            if (ConnectionRemoved != null)
            {
                ConnectionRemoved(this, new ConnectionEventArgs(conn));
            }
        }

        #endregion
    }
}
