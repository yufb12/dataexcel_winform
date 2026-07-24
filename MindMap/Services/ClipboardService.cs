using System;
using System.Windows.Forms;
using MindMap.Core;

namespace MindMap.Services
{
    /// <summary>
    /// 剪贴板服务
    /// </summary>
    public class ClipboardService
    {
        private const string ClipboardFormat = "MindMapNodeData";

        /// <summary>
        /// 复制节点到剪贴板
        /// </summary>
        /// <param name="node">要复制的节点</param>
        public void CopyNode(MindMapNode node)
        {
            if (node == null)
                throw new ArgumentNullException("node");

            // 简单实现：复制文本
            Clipboard.SetText(node.Text);
        }

        /// <summary>
        /// 从剪贴板粘贴文本
        /// </summary>
        /// <returns>粘贴的文本</returns>
        public string PasteText()
        {
            if (Clipboard.ContainsText())
            {
                return Clipboard.GetText();
            }
            return string.Empty;
        }

        /// <summary>
        /// 检查剪贴板是否包含文本
        /// </summary>
        public bool ContainsText()
        {
            return Clipboard.ContainsText();
        }
    }
}
