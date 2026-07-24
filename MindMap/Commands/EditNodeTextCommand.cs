using System;
using MindMap.Core;
using MindMap.Interfaces;

namespace MindMap.Commands
{
    /// <summary>
    /// 编辑节点文本命令
    /// </summary>
    public class EditNodeTextCommand : ICommand
    {
        private readonly MindMapNode _targetNode;
        private readonly string _oldText;
        private readonly string _newText;

        /// <summary>
        /// 获取命令名称
        /// </summary>
        public string Name
        {
            get { return "编辑节点文本"; }
        }

        /// <summary>
        /// 初始化编辑节点文本命令
        /// </summary>
        /// <param name="targetNode">目标节点</param>
        /// <param name="oldText">旧文本</param>
        /// <param name="newText">新文本</param>
        public EditNodeTextCommand(MindMapNode targetNode, string oldText, string newText)
        {
            if (targetNode == null)
                throw new ArgumentNullException("targetNode");

            _targetNode = targetNode;
            _oldText = oldText;
            _newText = newText;
        }

        /// <summary>
        /// 执行编辑
        /// </summary>
        public void Execute()
        {
            _targetNode.Text = _newText;
            _targetNode.AutoCalculateSize();
        }

        /// <summary>
        /// 撤销编辑
        /// </summary>
        public void Undo()
        {
            _targetNode.Text = _oldText;
            _targetNode.AutoCalculateSize();
        }
    }
}
