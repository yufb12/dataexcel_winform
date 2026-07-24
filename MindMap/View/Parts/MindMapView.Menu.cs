using System;
using System.Windows.Forms;
using MindMap.Core;

namespace MindMap.View
{
    /// <summary>
    /// MindMapView 右键菜单部分类
    /// 【架构设计】按SRP原则拆分为6个partial文件，每个<300行
    /// 
    /// 职责拆分：
    /// 1. MindMapView.Menu.cs              - 主文件（本文件）
    /// 2. MindMapView.Menu.Initialize.cs   - 菜单初始化
    /// 3. MindMapView.Menu.Actions.cs      - 节点操作
    /// 4. MindMapView.Menu.Style.cs        - 节点样式
    /// 5. MindMapView.Menu.Connection.cs   - 连接线设置
    /// 6. MindMapView.Menu.Alignment.cs    - 对齐/布局/层级
    /// </summary>
    public partial class MindMapView
    {
        //private ContextMenuStrip _contextMenu;
    }
}
