using System;
using System.Drawing;
using System.Windows.Forms;
using MindMap.Core;

namespace MindMap.View
{
    /// <summary>
    /// 【SRP单一职责】节点样式设置事件处理
    /// 负责：颜色/字体/边框/图标/图片/副标题
    /// </summary>
    public partial class MindMapView
    {
        #region 颜色/字体

        private void SetBackColorItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNode == null) return;

            using (ColorDialog dialog = new ColorDialog())
            {
                dialog.Color = _document.SelectedNode.Style.BackColor;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ApplyToSelectedNodes(node =>
                    {
                        node.Style.BackColor = dialog.Color;
                        node.Style.UseGradient = false;
                    });
                    Invalidate();
                }
            }
        }

        private void SetForeColorItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNode == null) return;

            using (ColorDialog dialog = new ColorDialog())
            {
                dialog.Color = _document.SelectedNode.Style.ForeColor;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ApplyToSelectedNodes(node => node.Style.ForeColor = dialog.Color);
                    Invalidate();
                }
            }
        }

        private void SetFontItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNode == null) return;

            using (FontDialog dialog = new FontDialog())
            {
                dialog.Font = _document.SelectedNode.Style.Font;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ApplyToSelectedNodes(node => node.Style.Font = (Font)dialog.Font.Clone());
                    Invalidate();
                }
            }
        }

        #endregion

        #region 边框设置

        private void SetBorderColorItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNode == null) return;

            using (ColorDialog dialog = new ColorDialog())
            {
                dialog.Color = _document.SelectedNode.Style.BorderColor;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    ApplyToSelectedNodes(node => node.Style.BorderColor = dialog.Color);
                    Invalidate();
                }
            }
        }

        private void BorderStyleItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null && _document != null && _document.SelectedNode != null)
            {
                NodeBorderStyle style = (NodeBorderStyle)item.Tag;
                ApplyToSelectedNodes(node => node.Style.BorderStyle = style);
                Invalidate();
            }
        }

        private void ShowBorderItem_Click(object sender, EventArgs e)
        {
            ApplyToSelectedNodes(node => node.Style.ShowBorder = true);
            Invalidate();
        }

        private void HideBorderItem_Click(object sender, EventArgs e)
        {
            ApplyToSelectedNodes(node => node.Style.ShowBorder = false);
            Invalidate();
        }

        #endregion

        #region 图标设置

        private void AddIconItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNode == null) return;

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "图片文件|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image icon = Image.FromFile(dialog.FileName);
                        ApplyToSelectedNodes(node => node.Style.AddIcon(icon));
                        Invalidate();
                    }
                    catch { }
                }
            }
        }

        private void IconPositionItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null && _document != null && _document.SelectedNode != null)
            {
                IconPosition pos = (IconPosition)item.Tag;
                ApplyToSelectedNodes(node => node.Style.IconPosition = pos);
                Invalidate();
            }
        }

        private void ClearIconItem_Click(object sender, EventArgs e)
        {
            ApplyToSelectedNodes(node => node.Style.ClearIcons());
            Invalidate();
        }

        #endregion

        #region 图片设置

        private void SetTopImageItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNode == null) return;

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "图片文件|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image img = Image.FromFile(dialog.FileName);
                        ApplyToSelectedNodes(node => node.Style.TopImage = img);
                        Invalidate();
                    }
                    catch { }
                }
            }
        }

        private void ClearTopImageItem_Click(object sender, EventArgs e)
        {
            ApplyToSelectedNodes(node => node.Style.TopImage = null);
            Invalidate();
        }

        private void SetBackgroundImageItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNode == null) return;

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "图片文件|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        Image img = Image.FromFile(dialog.FileName);
                        ApplyToSelectedNodes(node => node.Style.BackgroundImage = img);
                        Invalidate();
                    }
                    catch { }
                }
            }
        }

        private void ClearBackgroundImageItem_Click(object sender, EventArgs e)
        {
            ApplyToSelectedNodes(node => node.Style.BackgroundImage = null);
            Invalidate();
        }

        #endregion

        #region 副标题/形状

        private void SetSubtitleItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNode == null) return;

            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "请输入副标题：",
                "设置副标题",
                _document.SelectedNode.Style.Subtitle ?? "");

            if (input != null)
            {
                ApplyToSelectedNodes(node => node.Style.Subtitle = input);
                Invalidate();
            }
        }

        private void ShapeItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null && _document != null && _document.SelectedNode != null)
            {
                NodeShape shape = (NodeShape)item.Tag;
                ApplyToSelectedNodes(node => node.Style.Shape = shape);
                Invalidate();
            }
        }

        #endregion

        #region Tooltip

        private void SetTooltipItem_Click(object sender, EventArgs e)
        {
            if (_document == null || _document.SelectedNode == null) return;

            string input = Microsoft.VisualBasic.Interaction.InputBox(
                "请输入节点提示文本：",
                "设置节点Tooltip",
                _document.SelectedNode.Tooltip ?? "");

            if (input != null)
            {
                ApplyToSelectedNodes(node => node.Tooltip = input);
            }
        }

        private void ClearTooltipItem_Click(object sender, EventArgs e)
        {
            ApplyToSelectedNodes(node => node.Tooltip = null);
        }

        #endregion
    }
}
