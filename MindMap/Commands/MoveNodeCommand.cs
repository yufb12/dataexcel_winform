using System;
using System.Drawing;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Commands
{
    /// <summary>
    /// 移动节点命令
    /// </summary>
    public class MoveNodeCommand : ICommand
    {
        private readonly MindMapNode _targetNode;
        private readonly PointF _oldPosition;
        private readonly PointF _newPosition;

        /// <summary>
        /// 获取命令名称
        /// </summary>
        public string Name
        {
            get { return "移动节点"; }
        }

        /// <summary>
        /// 初始化移动节点命令
        /// </summary>
        /// <param name="targetNode">目标节点</param>
        /// <param name="oldPosition">旧位置</param>
        /// <param name="newPosition">新位置</param>
        public MoveNodeCommand(MindMapNode targetNode, PointF oldPosition, PointF newPosition)
        {
            if (targetNode == null)
                throw new ArgumentNullException("targetNode");

            _targetNode = targetNode;
            _oldPosition = oldPosition;
            _newPosition = newPosition;
        }

        /// <summary>
        /// 执行移动
        /// </summary>
        public void Execute()
        {
            _targetNode.Position = _newPosition;
        }

        /// <summary>
        /// 撤销移动
        /// </summary>
        public void Undo()
        {
            _targetNode.Position = _oldPosition;
        }
    }
}
