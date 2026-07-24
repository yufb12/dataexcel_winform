using System;
using System.Drawing;
using System.Windows.Forms;
using MindMap.Core;
using MindMap.Rendering;
namespace MindMap.View
{
    /// <summary>
    /// MindMapView - 输入处理部分（SRP：单一职责原则）
    /// 职责：鼠标、键盘、滚轮输入处理
    /// </summary>
    public partial class MindMapView : Control
    {
        #region 连线选中（v2.1.7.2新增）
        /// <summary>
        /// 查找指定位置的连线
        /// </summary>
        private Connection FindConnectionAtPoint(PointF docPoint)
        {
            if (_document == null) return null;
            return FindConnectionAtPointRecursive(docPoint, _document.RootNode);
        }

        /// <summary>
        /// 递归查找指定位置的连线
        /// </summary>
        private Connection FindConnectionAtPointRecursive(PointF docPoint, MindMapNode node)
        {
            if (node == null) return null;

            // 检查当前节点与父节点的连线
            if (node.ParentNode != null)
            {
                using (Graphics g = CreateGraphics())
                {
                    RectangleF parentBounds = _renderer.CalculateNodeBounds(g, node.ParentNode);
                    RectangleF childBounds = _renderer.CalculateNodeBounds(g, node);
                    PointF start = ConnectionRenderer.CalculateDynamicConnectionPoint(
                        parentBounds, childBounds, ConnectionPoint.Auto, true);
                    PointF end = ConnectionRenderer.CalculateDynamicConnectionPoint(
                        childBounds, parentBounds, ConnectionPoint.Auto, false);

                    if (ConnectionRenderer.HitTestConnection(start, end, docPoint, 5f))
                    {
                        return new Connection(node.ParentNode, node);
                    }
                }
            }

            // 递归检查子节点
            foreach (MindMapNode child in node.ChildNodes)
            {
                Connection found = FindConnectionAtPointRecursive(docPoint, child);
                if (found != null) return found;
            }

            return null;
        }
        #endregion

        #region 鼠标事件处理
        protected override void OnMouseDown(MouseEventArgs e)
        {
            try
            {
                base.OnMouseDown(e);
                if (_document == null) return;

                PointF docPoint = _document.ViewSettings.ScreenToDocument(e.Location);
                HitTestResult hit = _hitTester.HitTest(docPoint, _document.RootNode);

                if (e.Button == MouseButtons.Left)
                {
                    bool ctrlPressed = (ModifierKeys & Keys.Control) == Keys.Control;

                    if (hit.ResultType == HitTestResultType.Node)
                    {
                        // Ctrl+拖拽复制（v3.0新增，支持多选批量复制）
                        if (ctrlPressed && _document.SelectionCount >= 1 && _document.IsNodeSelected(hit.Node))
                        {
                            // 进入复制拖拽模式
                            _isDragCopying = true;
                            _dragCopyOriginals = new System.Collections.Generic.List<MindMapNode>();
                            _dragCopyNodes = new System.Collections.Generic.List<MindMapNode>();
                            
                            // 保存所有选中节点作为原始节点
                            foreach (MindMapNode node in _document.SelectedNodes)
                            {
                                _dragCopyOriginals.Add(node);
                            }
                            
                            _operationMode = MouseOperationMode.DragNode;
                            _dragStartPoint = docPoint;
                            
                            // 保存原始位置（用于计算偏移）
                            _originalNodePositions = new System.Collections.Generic.Dictionary<MindMapNode, PointF>();
                            foreach (MindMapNode node in _document.SelectedNodes)
                            {
                                _originalNodePositions[node] = node.Position;
                            }
                        }
                        // Ctrl+点击多选
                        else if (ctrlPressed)
                        {
                            _document.ToggleSelection(hit.Node);
                            _operationMode = MouseOperationMode.None;
                        }
                        else
                        {
                            if (!_document.IsNodeSelected(hit.Node))
                            {
                                _document.ClearSelection();
                                _document.AddToSelection(hit.Node);
                            }
                            _operationMode = MouseOperationMode.DragNode;
                            _dragStartPoint = docPoint;
                            
                            // v2.1.7.2：保存所有选中节点的原始位置（支持多选整体移动）
                            _originalNodePositions = new System.Collections.Generic.Dictionary<MindMapNode, PointF>();
                            foreach (MindMapNode node in _document.SelectedNodes)
                            {
                                _originalNodePositions[node] = node.Position;
                            }
                        }
                    }
                    else if (hit.ResultType == HitTestResultType.ExpandButton)
                    {
                        // v2.3：检测点击了哪个方向的展开按钮，只切换该方向
                        using (Graphics g = CreateGraphics())
                        {
                            RectangleF nodeBounds = _renderer.CalculateNodeBounds(g, hit.Node);
                            NodeDirection? clickedDirection = NodeBodyRenderer.HitTestExpandButton(nodeBounds, docPoint);
                            if (clickedDirection.HasValue)
                            {
                                hit.Node.ToggleExpandedInDirection(clickedDirection.Value);
                            }
                        }
                        Invalidate();
                    }
                    else
                    {
                        Connection clickedConnection = FindConnectionAtPoint(docPoint);
                        if (clickedConnection != null)
                        {
                            // 选中连线
                            _document.ClearSelection();
                            _document.SelectedConnection = clickedConnection;
                            _operationMode = MouseOperationMode.None;
                        }
                        else
                        {
                            // 空白处点击开始框选
                            if (!ctrlPressed) _document.ClearSelection();
                            _operationMode = MouseOperationMode.MarqueeSelect;
                            _marqueeStart = e.Location;
                            _marqueeRect = RectangleF.Empty;
                        }
                    }
                }
                else if (e.Button == MouseButtons.Middle)
                {
                    _operationMode = MouseOperationMode.Pan;
                    _dragStartPoint = e.Location;
                    _originalOffset = _document.ViewSettings.Offset;
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (hit.ResultType == HitTestResultType.Node)
                    {
                        if (!_document.IsNodeSelected(hit.Node))
                        {
                            _document.ClearSelection();
                            _document.AddToSelection(hit.Node);
                        }
                        _contextMenu.Show(this, e.Location);
                    }
                    else
                    {
                        ContextMenuStrip = null;
                    }
                }
                else
                {
                    // 点击空白处
                    ContextMenuStrip = null;
                }


            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("OnMouseDown error: " + ex.Message);
            }
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            try
            {
                base.OnMouseMove(e);
                if (_document == null) return;

                PointF docPoint = _document.ViewSettings.ScreenToDocument(e.Location);

                // v2.1.4：Tooltip鼠标悬停检测
                HitTestResult hit = _hitTester.HitTest(docPoint, _document.RootNode);
                if (hit.ResultType == HitTestResultType.Node && hit.Node != null)
                {
                    if (_lastHoverNode != hit.Node)
                    {
                        _lastHoverNode = hit.Node;
                        if (!string.IsNullOrEmpty(hit.Node.Tooltip))
                        {
                            _toolTip.Show(hit.Node.Tooltip, this, e.X + 15, e.Y + 15);
                        }
                        else
                        {
                            _toolTip.Hide(this);
                        }
                    }
                }
                else
                {
                    if (_lastHoverNode != null)
                    {
                        _lastHoverNode = null;
                        _toolTip.Hide(this);
                    }
                }

                if (_operationMode == MouseOperationMode.DragNode && _originalNodePositions != null)
                {
                    // v3.0：Ctrl+拖拽复制 - 拖拽开始时创建副本（支持多选批量复制）
                    if (_isDragCopying && _dragCopyNodes.Count == 0 && _dragCopyOriginals.Count > 0)
                    {
                        // 计算拖拽距离，只有移动一定距离才创建副本
                        float dx = docPoint.X - _dragStartPoint.X;
                        float dy = docPoint.Y - _dragStartPoint.Y;
                        float distance = (float)Math.Sqrt(dx * dx + dy * dy);
                        
                        if (distance > 5f)  // 移动超过5像素才开始复制
                        {
                            // 创建所有选中节点的副本
                            System.Collections.Generic.Dictionary<MindMapNode, MindMapNode> originalToCloneMap = 
                                new System.Collections.Generic.Dictionary<MindMapNode, MindMapNode>();
                            
                            foreach (MindMapNode original in _dragCopyOriginals)
                            {
                                // 跳过根节点（不支持复制根节点）
                                if (original.ParentNode == null) continue;
                                
                                // 创建节点副本（深拷贝，包含子节点）
                                MindMapNode clone = original.DeepClone();
                                
                                // 添加到父节点
                                original.ParentNode.AddChildNode(clone);
                                
                                // 保存映射关系
                                originalToCloneMap[original] = clone;
                                _dragCopyNodes.Add(clone);
                            }
                            
                            if (_dragCopyNodes.Count > 0)
                            {
                                // 选中所有副本节点
                                _document.ClearSelection();
                                foreach (MindMapNode clone in _dragCopyNodes)
                                {
                                    _document.AddToSelection(clone);
                                }
                                
                                // 更新原始位置字典（用副本的位置）
                                _originalNodePositions.Clear();
                                foreach (MindMapNode clone in _dragCopyNodes)
                                {
                                    _originalNodePositions[clone] = clone.Position;
                                }
                                
                                // 偏移副本位置，让它们跟随鼠标
                                foreach (System.Collections.Generic.KeyValuePair<MindMapNode, MindMapNode> kv in originalToCloneMap)
                                {
                                    MindMapNode original = kv.Key;
                                    MindMapNode clone = kv.Value;
                                    clone.Position = new PointF(
                                        original.Position.X + dx,
                                        original.Position.Y + dy);
                                }
                            }
                            else
                            {
                                // 没有可复制的节点（比如都是根节点），取消复制模式
                                _isDragCopying = false;
                            }
                        }
                    }
                    
                    // v2.1.7.2：多选节点整体移动（复制模式下只移动副本节点）
                    if (!_isDragCopying || _dragCopyNodes.Count > 0)
                    {
                        float dx = docPoint.X - _dragStartPoint.X;
                        float dy = docPoint.Y - _dragStartPoint.Y;
                        
                        foreach (System.Collections.Generic.KeyValuePair<MindMapNode, PointF> kv in _originalNodePositions)
                        {
                            kv.Key.Position = new PointF(
                                kv.Value.X + dx,
                                kv.Value.Y + dy);
                        }
                    }
                    Invalidate();
                }
                else if (_operationMode == MouseOperationMode.Pan)
                {
                    float dx = e.X - _dragStartPoint.X;
                    float dy = e.Y - _dragStartPoint.Y;
                    _document.ViewSettings.Offset = new PointF(
                        _originalOffset.X + dx,
                        _originalOffset.Y + dy);
                    Invalidate();
                }
                else if (_operationMode == MouseOperationMode.MarqueeSelect)
                {
                    _marqueeRect = new RectangleF(
                        Math.Min(_marqueeStart.X, e.X),
                        Math.Min(_marqueeStart.Y, e.Y),
                        Math.Abs(e.X - _marqueeStart.X),
                        Math.Abs(e.Y - _marqueeStart.Y));
                    Invalidate();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("OnMouseMove error: " + ex.Message);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            try
            {
                base.OnMouseUp(e);

                if (_operationMode == MouseOperationMode.MarqueeSelect && !_marqueeRect.IsEmpty)
                {
                    RectangleF docRect = _document.ViewSettings.ScreenRectToDocument(_marqueeRect);
                    SelectNodesInRect(docRect);
                }
                
                // v3.0：Ctrl+拖拽复制结束（支持多选批量复制）
                if (_isDragCopying)
                {
                    if (_dragCopyNodes.Count > 0)
                    {
                        // 复制成功，选中所有副本节点
                        _document.ClearSelection();
                        foreach (MindMapNode clone in _dragCopyNodes)
                        {
                            _document.AddToSelection(clone);
                        }
                        
                        // TODO: 添加到撤销重做栈
                        // 由于复制的是多个节点，需要创建复合命令
                        // _commandManager.AddCommand(new CompositeCommand(...));
                    }
                    
                    // 重置复制状态
                    _isDragCopying = false;
                    _dragCopyOriginals = null;
                    _dragCopyNodes = null;
                }

                _operationMode = MouseOperationMode.None;
                _marqueeRect = RectangleF.Empty;
                Invalidate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("OnMouseUp error: " + ex.Message);
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            try
            {
                base.OnMouseWheel(e);
                if (_document == null) return;

                float oldZoom = _document.ViewSettings.Zoom;
                float newZoom = oldZoom + (e.Delta > 0 ? 0.1f : -0.1f);
                newZoom = Math.Max(0.2f, Math.Min(3f, newZoom));

                // 鼠标跟随缩放
                PointF mouseDoc = _document.ViewSettings.ScreenToDocument(e.Location);
                _document.ViewSettings.Zoom = newZoom;
                PointF mouseDocAfter = _document.ViewSettings.ScreenToDocument(e.Location);

                _document.ViewSettings.Offset = new PointF(
                    _document.ViewSettings.Offset.X + (mouseDocAfter.X - mouseDoc.X) * newZoom,
                    _document.ViewSettings.Offset.Y + (mouseDocAfter.Y - mouseDoc.Y) * newZoom);

                Invalidate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("OnMouseWheel error: " + ex.Message);
            }
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            try
            {
                base.OnDoubleClick(e);
                if (_document == null || _document.SelectedNode == null) return;
                BeginEditNode(_document.SelectedNode);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("OnDoubleClick error: " + ex.Message);
            }
        }
        #endregion

        #region 键盘事件处理
        protected override void OnKeyDown(KeyEventArgs e)
        {
            try
            {
                base.OnKeyDown(e);
                if (_document == null) return;

                if (e.Control && e.KeyCode == Keys.Z)
                {
                    _commandManager.Undo();
                    e.Handled = true;
                }
                else if (e.Control && e.KeyCode == Keys.Y)
                {
                    _commandManager.Redo();
                    e.Handled = true;
                }
                else if (e.Control && e.KeyCode == Keys.A)
                {
                    // Ctrl+A 全选所有节点
                    SelectAllNodes();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Delete)
                {
                    DeleteSelectedNode();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Tab)
                {
                    AddChildNode();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    AddSiblingNode();
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.F2 || e.KeyCode == Keys.Space)
                {
                    if (_document.SelectedNode != null)
                    {
                        BeginEditNode(_document.SelectedNode);
                    }
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Add || e.KeyCode == Keys.Oemplus)
                {
                    if (_document.SelectedNode != null)
                    {
                        _document.SelectedNode.IsExpanded = true;
                    }
                    e.Handled = true;
                }
                else if (e.KeyCode == Keys.Subtract || e.KeyCode == Keys.OemMinus)
                {
                    if (_document.SelectedNode != null)
                    {
                        _document.SelectedNode.IsExpanded = false;
                    }
                    e.Handled = true;
                }
                Invalidate();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("OnKeyDown error: " + ex.Message);
            }
        }

        /// <summary>
        /// 【关键修复】捕获 Tab 键
        /// WinForms 默认 Tab 键用于焦点切换，不会触发 OnKeyDown
        /// 必须重写 ProcessCmdKey 来捕获 Tab 键
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                // Tab 键：添加子节点
                if (keyData == Keys.Tab)
                {
                    AddChildNode();
                    return true;  // 已处理，阻止默认焦点切换行为
                }

                // Shift+Tab：可以预留其他功能
                if (keyData == (Keys.Tab | Keys.Shift))
                {
                    // 可扩展：添加同级节点在前面
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("ProcessCmdKey error: " + ex.Message);
            }

            // 其他键交给默认处理
            return base.ProcessCmdKey(ref msg, keyData);
        }
        #endregion
    }
}