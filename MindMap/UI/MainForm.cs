using System;
using System.Windows.Forms;
using MindMap.Core;
using MindMap.Interfaces;
using MindMap.Layout;
using MindMap.Services;
using MindMap.View;

namespace MindMap.UI
{
    /// <summary>
    /// 主窗口
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly MindMapView _mindMapView;
        private readonly IFileService _fileService;
        private string _currentFilePath;
        private bool _isModified;

        /// <summary>
        /// 初始化主窗口
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            _mindMapView = new MindMapView();
            _mindMapView.Dock = DockStyle.Fill;
            Controls.Add(_mindMapView);
            _mindMapView.BringToFront();

            _fileService = new FileService();
            _currentFilePath = string.Empty;
            _isModified = false;

            NewDocument();
            SubscribeEvents();
        }

        /// <summary>
        /// 订阅事件
        /// </summary>
        private void SubscribeEvents()
        {
            _mindMapView.CommandManager.CommandExecuted += (sender, e) =>
            {
                _isModified = true;
                UpdateTitle();
                UpdateToolbar();
            };

            _mindMapView.CommandManager.UndoPerformed += (sender, e) =>
            {
                _isModified = true;
                UpdateTitle();
                UpdateToolbar();
            };

            _mindMapView.CommandManager.RedoPerformed += (sender, e) =>
            {
                UpdateToolbar();
            };
        }

        /// <summary>
        /// 新建文档
        /// </summary>
        private void NewDocument()
        {
            _mindMapView.Document = new MindMapDocument();
            _currentFilePath = string.Empty;
            _isModified = false;
            UpdateTitle();
            UpdateToolbar();
            
            // 直接重置视图（控件已添加到Controls集合）
            _mindMapView.ResetView();
        }

        /// <summary>
        /// 重置视图（居中显示）
        /// </summary>
        private void ResetView()
        {
            _mindMapView.ResetView();
        }

        /// <summary>
        /// 打开文档
        /// </summary>
        private void OpenDocument()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "思维导图文件 (*.mmap)|*.mmap|所有文件 (*.*)|*.*";
                dialog.Title = "打开思维导图";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _mindMapView.Document = _fileService.LoadDocument(dialog.FileName);
                        _currentFilePath = dialog.FileName;
                        _isModified = false;
                        UpdateTitle();
                        UpdateToolbar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("打开文件失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// 保存文档
        /// </summary>
        private void SaveDocument()
        {
            if (string.IsNullOrEmpty(_currentFilePath))
            {
                SaveDocumentAs();
            }
            else
            {
                try
                {
                    _fileService.SaveDocument(_mindMapView.Document, _currentFilePath);
                    _isModified = false;
                    UpdateTitle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("保存文件失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// 另存为
        /// </summary>
        private void SaveDocumentAs()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "思维导图文件 (*.mmap)|*.mmap|所有文件 (*.*)|*.*";
                dialog.Title = "保存思维导图";
                dialog.FileName = _mindMapView.Document.Title;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _fileService.SaveDocument(_mindMapView.Document, dialog.FileName);
                        _currentFilePath = dialog.FileName;
                        _isModified = false;
                        UpdateTitle();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("保存文件失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// 导出为图片
        /// </summary>
        private void ExportImage()
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "PNG图片 (*.png)|*.png|所有文件 (*.*)|*.*";
                dialog.Title = "导出为图片";
                dialog.FileName = _mindMapView.Document.Title;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _fileService.ExportToImage(_mindMapView, dialog.FileName);
                        MessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出失败：" + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// 切换到放射状布局
        /// </summary>
        private void SwitchRadialLayout()
        {
            _mindMapView.SwitchLayout(new RadialLayoutEngine());
        }

        /// <summary>
        /// 切换到树状布局（组织结构图）
        /// </summary>
        private void SwitchTreeLayout()
        {
            _mindMapView.SwitchLayout(new TreeLayoutEngine());
        }

        /// <summary>
        /// 切换到左右布局（标准思维导图）
        /// </summary>
        private void SwitchLeftRightLayout()
        {
            _mindMapView.SwitchLayout(new LeftRightLayoutEngine());
        }

        /// <summary>
        /// 设置经典蓝绿主题
        /// </summary>
        private void SetThemeClassic()
        {
            _mindMapView.SetTheme(Theme.CreateDefaultTheme());
        }

        /// <summary>
        /// 设置暖橙活力主题
        /// </summary>
        private void SetThemeWarmOrange()
        {
            _mindMapView.SetTheme(Theme.CreateWarmOrangeTheme());
        }

        /// <summary>
        /// 设置清新薄荷主题
        /// </summary>
        private void SetThemeMint()
        {
            _mindMapView.SetTheme(Theme.CreateMintTheme());
        }

        /// <summary>
        /// 设置商务深蓝主题
        /// </summary>
        private void SetThemeBusiness()
        {
            _mindMapView.SetTheme(Theme.CreateBusinessTheme());
        }

        /// <summary>
        /// 设置粉色浪漫主题
        /// </summary>
        private void SetThemePink()
        {
            _mindMapView.SetTheme(Theme.CreatePinkTheme());
        }

        /// <summary>
        /// 设置暗夜黑主题
        /// </summary>
        private void SetThemeDark()
        {
            _mindMapView.SetTheme(Theme.CreateDarkTheme());
        }

        /// <summary>
        /// 设置森林绿主题
        /// </summary>
        private void SetThemeForest()
        {
            _mindMapView.SetTheme(Theme.CreateForestTheme());
        }

        /// <summary>
        /// 设置海洋蓝主题
        /// </summary>
        private void SetThemeOcean()
        {
            _mindMapView.SetTheme(Theme.CreateOceanTheme());
        }

        /// <summary>
        /// 设置日落橙主题
        /// </summary>
        private void SetThemeSunset()
        {
            _mindMapView.SetTheme(Theme.CreateSunsetTheme());
        }

        /// <summary>
        /// 设置极简灰主题
        /// </summary>
        private void SetThemeMinimal()
        {
            _mindMapView.SetTheme(Theme.CreateMinimalTheme());
        }

        /// <summary>
        /// 更新窗口标题
        /// </summary>
        private void UpdateTitle()
        {
            string title = "思维导图";
            if (!string.IsNullOrEmpty(_currentFilePath))
            {
                title += " - " + System.IO.Path.GetFileName(_currentFilePath);
            }
            else
            {
                title += " - " + _mindMapView.Document.Title;
            }
            if (_isModified)
            {
                title += " *";
            }
            Text = title;
        }

        /// <summary>
        /// 更新工具栏状态
        /// </summary>
        private void UpdateToolbar()
        {
            undoToolStripMenuItem.Enabled = _mindMapView.CommandManager.CanUndo;
            redoToolStripMenuItem.Enabled = _mindMapView.CommandManager.CanRedo;
            deleteNodeToolStripButton.Enabled = _mindMapView.Document != null && _mindMapView.Document.SelectedNode != null;
        }

        #region 菜单事件处理

        private void NewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewDocument();
        }

        private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDocument();
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocument();
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveDocumentAs();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UndoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.CommandManager.Undo();
        }

        private void RedoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.CommandManager.Redo();
        }

        private void AddNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.AddChildNode();
        }

        private void DeleteNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.DeleteSelectedNode();
        }
        private void AddChildNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.AddChildNode();
        }
        private void AddSiblingNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.AddSiblingNode();
        }
        private void InsertNodeBeforeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.InsertNodeBefore();
        }
        private void EditNodeTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_mindMapView.Document != null && _mindMapView.Document.SelectedNode != null)
                _mindMapView.BeginEditNode(_mindMapView.Document.SelectedNode);
        }
        private void ExpandCollapseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_mindMapView.Document != null && _mindMapView.Document.SelectedNode != null)
            {
                _mindMapView.Document.SelectedNode.IsExpanded = !_mindMapView.Document.SelectedNode.IsExpanded;
            }
        }
        private void CopyNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.CopySelectedNode();
        }
        private void PasteNodeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.PasteNode();
        }
        private void RelayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.Relayout();
        }
        private void AlignLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.AlignSelectedNodes(AlignmentType.Left);
        }
        private void AlignCenterHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.AlignSelectedNodes(AlignmentType.CenterHorizontal);
        }
        private void AlignRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.AlignSelectedNodes(AlignmentType.Right);
        }
        private void AlignTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.AlignSelectedNodes(AlignmentType.Top);
        }
        private void AlignCenterVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.AlignSelectedNodes(AlignmentType.CenterVertical);
        }
        private void AlignBottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.AlignSelectedNodes(AlignmentType.Bottom);
        }
        private void DistributeHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.AlignSelectedNodes(AlignmentType.DistributeHorizontal);
        }
        private void DistributeVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.AlignSelectedNodes(AlignmentType.DistributeVertical);
        }
        private void SameWidthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.AlignSelectedNodes(AlignmentType.SameWidth);
        }
        private void SameHeightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.AlignSelectedNodes(AlignmentType.SameHeight);
        }
        private void SameSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.AlignSelectedNodes(AlignmentType.SameSize);
        }
        private void BringToFrontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_mindMapView.Document != null)
            {
                _mindMapView.ApplyToSelectedNodes(node => node.ZOrder = int.MaxValue);
            }
        }
        private void SendToBackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_mindMapView.Document != null)
            {
                _mindMapView.ApplyToSelectedNodes(node => node.ZOrder = int.MinValue);
            }
        }
        private void BringForwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_mindMapView.Document != null)
            {
                _mindMapView.ApplyToSelectedNodes(node => node.ZOrder++);
            }
        }
        private void SendBackwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_mindMapView.Document != null)
            {
                _mindMapView.ApplyToSelectedNodes(node => node.ZOrder--);
            }
        }

        private void ResetViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetView();
        }

        private void ExportImageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportImage();
        }

        private void RadialLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchRadialLayout();
        }

        private void TreeLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchTreeLayout();
        }

        private void LeftRightLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SwitchLeftRightLayout();
        }

        private void FishboneLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.SwitchLayout(new FishboneLayoutEngine());
        }

        private void TimelineLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.SwitchLayout(new TimelineLayoutEngine());
        }

        private void OrgChartLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.SwitchLayout(new OrgChartLayoutEngine());
        }

        private void HorizontalLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.SwitchLayout(new MindMapHorizontalLayout());
        }

        private void VerticalLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.SwitchLayout(new MindMapVerticalLayout());
        }

        private void SpiralLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.SwitchLayout(new SpiralLayoutEngine());
        }

        private void FanLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.SwitchLayout(new FanLayoutEngine());
        }

        private void CircleLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.SwitchLayout(new CircleLayoutEngine());
        }

        private void WaterfallLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.SwitchLayout(new WaterfallLayoutEngine());
        }

        private void SymmetricLayoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _mindMapView.SwitchLayout(new SymmetricLayoutEngine());
        }

        private void ThemeClassicToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetThemeClassic();
        }

        private void ThemeWarmOrangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetThemeWarmOrange();
        }

        private void ThemeMintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetThemeMint();
        }

        private void ThemeBusinessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetThemeBusiness();
        }

        private void ThemePinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetThemePink();
        }

        private void ThemeDarkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetThemeDark();
        }

        private void ThemeForestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetThemeForest();
        }

        private void ThemeOceanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetThemeOcean();
        }

        private void ThemeSunsetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetThemeSunset();
        }

        private void ThemeMinimalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetThemeMinimal();
        }

        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "思维导图软件 v1.2 (专业增强版)\n\n" +
                "基于 C# WinForm .NET 4.0 开发\n\n" +
                "【核心功能】\n" +
                "• 节点添加、删除、编辑（就地编辑）\n" +
                "• 节点拖拽移动（支持撤销）\n" +
                "• 展开/折叠子节点\n" +
                "• 鼠标跟随缩放（滚轮）\n" +
                "• 画布平移（中键拖拽，手型光标）\n" +
                "• 一键重置视图（Ctrl+Home）\n" +
                "• 完整撤销/重做（Ctrl+Z/Ctrl+Y）\n" +
                "• 文件保存/打开（.mmap格式）\n" +
                "• 导出PNG图片\n\n" +
                "【视觉增强】\n" +
                "• 节点渐变填充 + 阴影效果\n" +
                "• 平滑贝塞尔曲线连接线\n" +
                "• 圆形展开/折叠按钮\n" +
                "• 5种专业主题配色\n\n" +
                "【多种布局】\n" +
                "• 放射状布局（360°展开）\n" +
                "• 树状布局（组织结构图）\n" +
                "• 左右布局（标准思维导图）\n\n" +
                "【快捷键】\n" +
                "  Delete - 删除节点  |  Insert - 添加节点\n" +
                "  F2 - 编辑文本     |  Enter - 添加子节点\n" +
                "  Ctrl+Home - 重置视图",
                "关于",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        #endregion
    }
}
