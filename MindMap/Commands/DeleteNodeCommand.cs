using System;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Commands
{
    /// <summary>
    /// 删除节点命令（v1.8.2修复：不自动触发全局布局）
    /// 
    /// 【用户体验优化】
    /// - 移除自动Relayout：用户手动调整的位置不会被覆盖
    /// - 删除节点不影响其他节点的位置
    /// - 用户可通过菜单"重新布局"主动触发全局布局
    /// </summary>
    public class DeleteNodeCommand : ICommand
    {
        private readonly MindMapDocument _document;
        private readonly MindMapNode _parentNode;
        private readonly MindMapNode _targetNode;
        private readonly int _originalIndex;

        /// <summary>
        /// 获取命令名称
        /// </summary>
        public string Name
        {
            get { return "删除节点"; }
        }

        /// <summary>
        /// 初始化删除节点命令
        /// </summary>
        public DeleteNodeCommand(MindMapDocument document, MindMapNode parentNode, MindMapNode targetNode)
        {
            if (document == null)
                throw new ArgumentNullException("document");
            if (parentNode == null)
                throw new ArgumentNullException("parentNode");
            if (targetNode == null)
                throw new ArgumentNullException("targetNode");
            _document = document;
            _parentNode = parentNode;
            _targetNode = targetNode;
            _originalIndex = parentNode.ChildNodes.IndexOf(targetNode);
        }

        /// <summary>
        /// 执行删除（v1.8.2：不触发全局布局，保留其他节点位置）
        /// </summary>
        public void Execute()
        {
            _parentNode.RemoveChild(_targetNode);
            // v1.8.2移除：_document.Relayout(_layoutEngine);
            // 不自动重新布局，保留用户已调整的所有节点位置
            _document.SelectedNode = _parentNode;
        }

        /// <summary>
        /// 撤销删除（v1.8.2：不触发全局布局，恢复到原位置）
        /// </summary>
        public void Undo()
        {
            // 恢复到原来的位置
            _parentNode.InsertChild(_originalIndex, _targetNode);
            // v1.8.2移除：_document.Relayout(_layoutEngine);
            // 不自动重新布局，节点位置保持不变
            _document.SelectedNode = _targetNode;
        }
    }
}
