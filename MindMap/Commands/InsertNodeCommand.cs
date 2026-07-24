using System;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Commands
{
    /// <summary>
    /// 在两个节点之间插入新节点命令
    /// 
    /// 【功能说明】
    /// - 在选中节点与其父节点之间插入一个新的中间节点
    /// - 原节点成为新节点的子节点
    /// - 新节点成为原父节点的子节点
    /// - 支持撤销/重做
    /// 
    /// 【架构设计】
    /// - 遵循命令模式，完整记录状态变化
    /// - 原子操作，要么全部成功要么全部失败
    /// </summary>
    public class InsertNodeCommand : ICommand
    {
        private readonly MindMapDocument _document;
        private readonly MindMapNode _parentNode;      // 原父节点
        private readonly MindMapNode _targetNode;      // 原目标节点（选中的节点）
        private readonly MindMapNode _insertedNode;    // 新插入的节点
        private int _originalIndex;                    // 目标节点在原父节点中的索引

        /// <summary>
        /// 获取命令名称
        /// </summary>
        public string Name
        {
            get { return "插入节点"; }
        }

        /// <summary>
        /// 初始化插入节点命令
        /// </summary>
        /// <param name="document">目标文档</param>
        /// <param name="targetNode">要在其前面插入节点的目标节点</param>
        /// <param name="insertedNode">要插入的新节点</param>
        public InsertNodeCommand(MindMapDocument document, MindMapNode targetNode, MindMapNode insertedNode)
        {
            if (document == null)
                throw new ArgumentNullException("document");
            if (targetNode == null)
                throw new ArgumentNullException("targetNode");
            if (insertedNode == null)
                throw new ArgumentNullException("insertedNode");
            if (targetNode.ParentNode == null)
                throw new InvalidOperationException("根节点不能插入父节点");

            _document = document;
            _targetNode = targetNode;
            _parentNode = targetNode.ParentNode;
            _insertedNode = insertedNode;
            _originalIndex = _parentNode.ChildNodes.IndexOf(targetNode);
        }

        /// <summary>
        /// 执行插入操作
        /// 
        /// 【执行流程】
        /// 1. 从父节点移除目标节点
        /// 2. 将新节点添加到父节点的原位置
        /// 3. 将目标节点添加为新节点的子节点
        /// 4. 选中新插入的节点
        /// </summary>
        public void Execute()
        {
            // 1. 从父节点移除目标节点
            _parentNode.RemoveChild(_targetNode);

            // 2. 将新节点添加到父节点的原位置
            _parentNode.InsertChild(_originalIndex, _insertedNode);

            // 3. 将目标节点添加为新节点的子节点
            _insertedNode.AddChild(_targetNode);

            // 4. 选中新插入的节点
            _document.ClearSelection();
            _document.AddToSelection(_insertedNode);
        }

        /// <summary>
        /// 撤销插入操作
        /// 
        /// 【撤销流程】
        /// 1. 从新节点移除目标节点
        /// 2. 从父节点移除新节点
        /// 3. 将目标节点恢复到父节点的原位置
        /// 4. 重新选中目标节点
        /// </summary>
        public void Undo()
        {
            // 1. 从新节点移除目标节点
            _insertedNode.RemoveChild(_targetNode);

            // 2. 从父节点移除新节点
            _parentNode.RemoveChild(_insertedNode);

            // 3. 将目标节点恢复到父节点的原位置
            _parentNode.InsertChild(_originalIndex, _targetNode);

            // 4. 重新选中目标节点
            _document.ClearSelection();
            _document.AddToSelection(_targetNode);
        }
    }
}
