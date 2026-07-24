using System;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Commands
{
    /// <summary>
    /// 添加节点命令（v1.8.2修复：不自动触发全局布局）
    /// 
    /// 【用户体验优化】
    /// - 移除自动Relayout：用户手动调整的位置不会被覆盖
    /// - 新节点位置由MindMapView预先计算好合理的初始位置
    /// - 用户可通过菜单"重新布局"主动触发全局布局
    /// </summary>
    public class AddNodeCommand : ICommand
    {
        private readonly MindMapDocument _document;
        private readonly MindMapNode _parentNode;
        private readonly MindMapNode _newNode;

        /// <summary>
        /// 获取命令名称
        /// </summary>
        public string Name
        {
            get { return "添加节点"; }
        }

        /// <summary>
        /// 初始化添加节点命令
        /// </summary>
        /// <param name="document">目标文档</param>
        /// <param name="parentNode">父节点</param>
        /// <param name="newNode">要添加的新节点（位置已预先设置好）</param>
        public AddNodeCommand(MindMapDocument document, MindMapNode parentNode, MindMapNode newNode)
        {
            if (document == null)
                throw new ArgumentNullException("document");
            if (parentNode == null)
                throw new ArgumentNullException("parentNode");
            if (newNode == null)
                throw new ArgumentNullException("newNode");
            _document = document;
            _parentNode = parentNode;
            _newNode = newNode;
        }

        /// <summary>
        /// 执行添加（v1.8.2：不触发全局布局，保留用户手动调整的位置）
        /// </summary>
        public void Execute()
        {
            _parentNode.AddChild(_newNode);
            // v1.8.2移除：_document.Relayout(_layoutEngine);
            // 不自动重新布局，保留用户已调整的所有节点位置
            _document.SelectedNode = _newNode;
        }

        /// <summary>
        /// 撤销添加（v1.8.2：不触发全局布局）
        /// </summary>
        public void Undo()
        {
            _parentNode.RemoveChild(_newNode);
            // v1.8.2移除：_document.Relayout(_layoutEngine);
            // 不自动重新布局
            _document.SelectedNode = _parentNode;
        }
    }
}
