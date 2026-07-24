using System;
using System.Collections.Generic;

namespace MindMap.Core
{
    /// <summary>
    /// 【SRP单一职责】思维导图节点 - 分方向折叠部分
    /// 负责：4个方向独立的展开/折叠状态管理
    /// 【设计模式】Strategy策略模式 - 每个方向独立策略
    /// </summary>
    public partial class MindMapNode
    {
        #region 字段 - 分方向折叠

        private Dictionary<NodeDirection, bool> _expandedByDirection;

        #endregion

        #region partial 方法实现

        /// <summary>
        /// 初始化展开状态
        /// </summary>
        partial void InitializeExpandState()
        {
            EnsureExpandedDictionaryInitialized();
        }

        #endregion

        #region 分方向折叠核心方法

        /// <summary>
        /// 确保字典已初始化（延迟初始化）
        /// </summary>
        private void EnsureExpandedDictionaryInitialized()
        {
            if (_expandedByDirection == null)
            {
                _expandedByDirection = new Dictionary<NodeDirection, bool>();
                foreach (NodeDirection dir in Enum.GetValues(typeof(NodeDirection)))
                {
                    _expandedByDirection[dir] = true;  // 默认展开
                }
            }
        }

        /// <summary>
        /// 获取指定方向的展开状态
        /// </summary>
        public bool IsExpandedInDirection(NodeDirection direction)
        {
            EnsureExpandedDictionaryInitialized();
            return _expandedByDirection.ContainsKey(direction) && _expandedByDirection[direction];
        }

        /// <summary>
        /// 设置指定方向的展开状态
        /// </summary>
        public void SetExpandedInDirection(NodeDirection direction, bool expanded)
        {
            EnsureExpandedDictionaryInitialized();
            _expandedByDirection[direction] = expanded;
        }

        /// <summary>
        /// 切换指定方向的展开状态
        /// </summary>
        public void ToggleExpandedInDirection(NodeDirection direction)
        {
            SetExpandedInDirection(direction, !IsExpandedInDirection(direction));
        }

        /// <summary>
        /// 检查指定方向是否有子节点（根据子节点位置动态计算）
        /// 【重要】一个子节点只能属于一个方向，不会重复计算
        /// </summary>
        public bool HasChildrenInDirection(NodeDirection direction)
        {
            foreach (Connection conn in _childConnections)
            {
                NodeDirection? childDir = GetChildDirection(conn);
                if (childDir == direction)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 获取子节点所属的方向（一个子节点只属于一个方向）
        /// 优先使用Connection.Direction属性，未设置时根据位置动态计算
        /// </summary>
        private NodeDirection? GetChildDirection(Connection conn)
        {
            if (conn == null || conn.ChildNode == null)
                return null;

            // 如果Connection显式设置了方向（非默认值Right），则使用该方向
            if (conn.Direction != NodeDirection.Right)
            {
                return conn.Direction;
            }

            // 否则根据位置动态计算方向
            return CalculateChildDirection(conn.ChildNode);
        }

        /// <summary>
        /// 计算子节点相对于父节点的方向
        /// </summary>
        private NodeDirection? CalculateChildDirection(MindMapNode child)
        {
            if (child == null) return null;

            float dx = child.Center.X - Center.X;
            float dy = child.Center.Y - Center.Y;

            // 根据角度判断方向
            float angle = (float)Math.Atan2(dy, dx) * 180f / (float)Math.PI;

            // -45° ~ 45°: 右
            // 45° ~ 135°: 下
            // 135° ~ 180° 或 -180° ~ -135°: 左
            // -135° ~ -45°: 上
            if (angle >= -45f && angle < 45f)
                return NodeDirection.Right;
            else if (angle >= 45f && angle < 135f)
                return NodeDirection.Bottom;
            else if (angle >= 135f || angle < -135f)
                return NodeDirection.Left;
            else
                return NodeDirection.Top;
        }

        /// <summary>
        /// 获取指定方向的子连接线
        /// </summary>
        public IList<Connection> GetChildConnections(NodeDirection direction)
        {
            List<Connection> result = new List<Connection>();
            foreach (Connection conn in _childConnections)
            {
                if (conn.Direction == direction)
                {
                    result.Add(conn);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取指定方向已展开的子连接线（支持动态计算方向）
        /// 【重要】一个子节点只能属于一个方向，不会重复计算
        /// </summary>
        public IList<Connection> GetExpandedChildConnections(NodeDirection direction)
        {
            if (!IsExpandedInDirection(direction))
            {
                return new List<Connection>();
            }

            List<Connection> result = new List<Connection>();
            foreach (Connection conn in _childConnections)
            {
                // 使用统一的方向判断方法，确保一个子节点只属于一个方向
                NodeDirection? childDir = GetChildDirection(conn);
                if (childDir == direction)
                {
                    result.Add(conn);
                }
            }
            return result;
        }

        /// <summary>
        /// 获取所有方向已展开的子连接线
        /// </summary>
        /// <summary>
        /// 获取所有方向已展开的子连接线
        /// </summary>
        public IList<Connection> GetAllExpandedChildConnections()
        {
            List<Connection> result = new List<Connection>();
            foreach (NodeDirection dir in Enum.GetValues(typeof(NodeDirection)))
            {
                if (IsExpandedInDirection(dir))
                {
                    result.AddRange(GetExpandedChildConnections(dir));
                }
            }
            return result;
        }

        /// <summary>
        /// 获取所有方向已展开的子节点
        /// </summary>
        public IList<MindMapNode> GetAllExpandedChildNodes()
        {
            List<MindMapNode> result = new List<MindMapNode>();
            foreach (Connection conn in GetAllExpandedChildConnections())
            {
                result.Add(conn.ChildNode);
            }
            return result;
        }

        #endregion

        #region 兼容旧API（单一展开状态）

        /// <summary>
        /// 兼容旧API（默认操作Right方向）
        /// </summary>
        [Obsolete("请使用 IsExpandedInDirection 方法")]
        public bool IsExpanded
        {
            get { return IsExpandedInDirection(NodeDirection.Right); }
            set { SetExpandedInDirection(NodeDirection.Right, value); }
        }

        #endregion

        #region 批量操作

        /// <summary>
        /// 展开所有方向
        /// </summary>
        public void ExpandAll()
        {
            foreach (NodeDirection dir in Enum.GetValues(typeof(NodeDirection)))
            {
                SetExpandedInDirection(dir, true);
            }
        }

        /// <summary>
        /// 折叠所有方向
        /// </summary>
        public void CollapseAll()
        {
            foreach (NodeDirection dir in Enum.GetValues(typeof(NodeDirection)))
            {
                SetExpandedInDirection(dir, false);
            }
        }

        #endregion
    }
}
