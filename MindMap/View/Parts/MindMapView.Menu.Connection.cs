using System;
using System.Drawing;
using System.Windows.Forms;
using MindMap.Core;

namespace MindMap.View
{
    /// <summary>
    /// 【SRP单一职责】连接线设置事件处理
    /// 负责：连接线类型/颜色/宽度/连接点
    /// 
    /// 【语义规范】所有设置只修改【当前节点 ↔ 父节点】之间的连线
    /// </summary>
    public partial class MindMapView
    {
        #region 连接线类型/颜色/宽度

        /// <summary>
        /// 设置连接线类型
        /// 只修改：当前节点 与 父节点 之间的连线
        /// </summary>
        private void ConnectionLineTypeItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_document == null) return;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                if (item == null || !(item.Tag is ConnectionLineType)) return;

                ConnectionLineType lineType = (ConnectionLineType)item.Tag;
                
                foreach (MindMapNode node in _document.SelectedNodes)
                {
                    if (node.ParentConnection != null)
                    {
                        node.ParentConnection.LineType = lineType;
                    }
                }
                Invalidate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ConnectionLineType error: " + ex.Message);
            }
        }

        /// <summary>
        /// 设置连接线颜色
        /// 只修改：当前节点 与 父节点 之间的连线
        /// </summary>
        private void SetConnectionLineColorItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_document == null || _document.SelectedNodes.Count == 0) return;

                Color defaultColor = Color.Gray;
                if (_document.SelectedNode != null && _document.SelectedNode.ParentConnection != null)
                {
                    defaultColor = _document.SelectedNode.ParentConnection.LineColor;
                }

                using (ColorDialog dialog = new ColorDialog())
                {
                    dialog.Color = defaultColor;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        foreach (MindMapNode node in _document.SelectedNodes)
                        {
                            if (node.ParentConnection != null)
                            {
                                node.ParentConnection.LineColor = dialog.Color;
                            }
                        }
                        Invalidate();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("SetConnectionLineColor error: " + ex.Message);
            }
        }

        /// <summary>
        /// 设置连接线宽度
        /// 只修改：当前节点 与 父节点 之间的连线
        /// </summary>
        private void ConnectionLineWidthItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_document == null) return;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                if (item == null || !(item.Tag is float)) return;

                float width = (float)item.Tag;
                
                foreach (MindMapNode node in _document.SelectedNodes)
                {
                    if (node.ParentConnection != null)
                    {
                        node.ParentConnection.LineWidth = width;
                    }
                }
                Invalidate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ConnectionLineWidth error: " + ex.Message);
            }
        }

        #endregion

        #region 连接点设置

        /// <summary>
        /// 设置连接点
        /// 
        /// 【父节点连接点】：设置【当前节点 ↔ 父级节点】之间连线的【父级节点】的连接位置
        /// 【本节点连接点】：设置【当前节点 ↔ 父级节点】之间连线的【本节点】的连接位置
        /// </summary>
        private void ConnectionPointItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (_document == null) return;
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                if (item == null || !(item.Tag is Tuple<ConnectionPoint, bool>)) return;

                Tuple<ConnectionPoint, bool> tag = (Tuple<ConnectionPoint, bool>)item.Tag;
                ConnectionPoint point = tag.Item1;
                bool isParentConnection = tag.Item2;

                foreach (MindMapNode node in _document.SelectedNodes)
                {
                    if (node.ParentConnection == null) continue;
                    
                    if (isParentConnection)
                    {
                        // ==================== 父节点连接点 ====================
                        // 设置：【当前节点 ↔ 父级节点】之间连线的【父级节点】的连接位置
                        node.ParentConnection.ParentConnectionPoint = point;
                    }
                    else
                    {
                        // ==================== 本节点连接点 ====================
                        // 设置：【当前节点 ↔ 父级节点】之间连线的【本节点】的连接位置
                        node.ParentConnection.ChildConnectionPoint = point;
                    }
                }
                Invalidate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ConnectionPoint error: " + ex.Message);
            }
        }

        #endregion
    }
}
