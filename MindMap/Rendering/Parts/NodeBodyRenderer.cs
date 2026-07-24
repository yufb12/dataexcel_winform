using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using MindMap.Core;

namespace MindMap.Rendering
{
    /// <summary>
    /// 节点主体渲染器（v2.0架构扩展版 - SRP单一职责）
    /// 
    /// 【v2.0新增】
    /// - 顶部图片渲染：TopImage显示在节点主体上方（XMind风格主题图）
    /// - 背景图片渲染：BackgroundImage支持4种填充模式
    /// - 主标题+副标题：TitleWithSubtitle形状，两行文本
    /// 
    /// 【v1.9.1新增】
    /// - 多图标支持：循环绘制所有图标（带间距）
    /// - 命名修复：BorderStyle → NodeBorderStyle
    /// 
    /// 【架构设计】
    /// - 职责：专门负责绘制节点主体（背景、边框、图标、文本）
    /// - 开闭原则：新增样式只需修改此处，不影响其他渲染器
    /// </summary>
    internal static class NodeBodyRenderer
    {
        #region 常量定义
        private const float NODE_MIN_HEIGHT = 36f;
        private const float NODE_TEXT_PADDING_H = 20f;
        private const float NODE_TEXT_PADDING_V = 10f;
        private const float ICON_TEXT_SPACING = 6f;       // 图标与文本间距
        private const float EXPAND_BUTTON_SIZE = 14f;
        private const float EXPAND_BUTTON_OFFSET = 6f;
        #endregion

        #region 节点边界计算（v2.0扩展：顶部图片+副标题）
        /// <summary>
        /// 计算节点边界（v2.0扩展：考虑顶部图片+副标题）
        /// </summary>
        public static RectangleF CalculateNodeBounds(Graphics graphics, MindMapNode node)
        {
            if (graphics == null) throw new ArgumentNullException("graphics");
            if (node == null) throw new ArgumentNullException("node");

            // 图片节点：使用图片尺寸
            if (node.Style.Shape == NodeShape.Image && node.Style.Image != null)
            {
                float imgWidth = Math.Max(node.Style.Image.Width, 60f);
                float imgHeight = Math.Max(node.Style.Image.Height, 40f);
                return new RectangleF(node.Position.X, node.Position.Y, imgWidth, imgHeight);
            }

            NodeStyle style = node.Style;
            float bodyY = node.Position.Y;
            float totalHeight = 0f;

            // v2.0：顶部图片高度
            bool hasTopImage = style.TopImage != null;
            if (hasTopImage)
            {
                totalHeight += style.TopImageSize.Height + style.TopImageSpacing;
                bodyY += style.TopImageSize.Height + style.TopImageSpacing;
            }

            // 计算主体内容尺寸
            float bodyWidth, bodyHeight;
            CalculateBodyContentSize(graphics, node, out bodyWidth, out bodyHeight);

            totalHeight += bodyHeight;
            float totalWidth = bodyWidth;

            // v2.0：如果有顶部图片，宽度取最大值
            if (hasTopImage)
            {
                totalWidth = Math.Max(totalWidth, style.TopImageSize.Width);
            }

            return new RectangleF(node.Position.X, node.Position.Y, totalWidth, totalHeight);
        }

        /// <summary>
        /// 计算主体内容尺寸（文本+图标+副标题）
        /// </summary>
        private static void CalculateBodyContentSize(Graphics graphics, MindMapNode node, out float width, out float height)
        {
            NodeStyle style = node.Style;

            // 计算主标题尺寸
            SizeF textSize = graphics.MeasureString(node.Text, style.Font);
            float contentWidth = textSize.Width;
            float contentHeight = textSize.Height;

            // v2.0：副标题尺寸（TitleWithSubtitle形状）
            bool hasSubtitle = style.Shape == NodeShape.TitleWithSubtitle && 
                              !string.IsNullOrEmpty(style.Subtitle);
            if (hasSubtitle)
            {
                SizeF subtitleSize = graphics.MeasureString(style.Subtitle, style.SubtitleFont);
                contentWidth = Math.Max(contentWidth, subtitleSize.Width);
                contentHeight += subtitleSize.Height + style.SubtitleSpacing;
            }

            // 多图标尺寸
            int iconCount = style.Icons.Count;
            bool hasIcons = iconCount > 0 && style.IconPosition != IconPosition.None;
            if (hasIcons)
            {
                int totalIconsWidth = style.GetTotalIconsWidth();
                switch (style.IconPosition)
                {
                    case IconPosition.Left:
                    case IconPosition.Right:
                        // 左右排列：图标总宽度 + 间距 + 文本宽度
                        contentWidth += totalIconsWidth + ICON_TEXT_SPACING;
                        contentHeight = Math.Max(contentHeight, style.IconSize.Height);
                        break;
                    case IconPosition.Top:
                        // 上下排列：高度相加，宽度取最大值
                        contentHeight += style.IconSize.Height + ICON_TEXT_SPACING;
                        contentWidth = Math.Max(contentWidth, totalIconsWidth);
                        break;
                }
            }

            // 统一最小高度 + 大量内边距
            width = contentWidth + NODE_TEXT_PADDING_H * 2;
            height = Math.Max(contentHeight + NODE_TEXT_PADDING_V * 2, NODE_MIN_HEIGHT);
        }

        /// <summary>
        /// 获取节点主体区域（排除顶部图片）
        /// </summary>
        public static RectangleF GetNodeBodyBounds(RectangleF totalBounds, NodeStyle style)
        {
            bool hasTopImage = style.TopImage != null;
            if (!hasTopImage)
                return totalBounds;

            // 主体从顶部图片下方开始
            float bodyY = totalBounds.Y + style.TopImageSize.Height + style.TopImageSpacing;
            float bodyHeight = totalBounds.Height - style.TopImageSize.Height - style.TopImageSpacing;
            
            return new RectangleF(totalBounds.X, bodyY, totalBounds.Width, bodyHeight);
        }
        #endregion

        #region v2.0：顶部图片渲染
        /// <summary>
        /// 绘制节点顶部图片（XMind风格主题图）
        /// </summary>
        public static void DrawTopImage(Graphics graphics, RectangleF totalBounds, NodeStyle style)
        {
            if (graphics == null) throw new ArgumentNullException("graphics");
            if (style == null) throw new ArgumentNullException("style");
            if (style.TopImage == null) return;

            // 顶部图片居中显示
            float imgX = totalBounds.X + (totalBounds.Width - style.TopImageSize.Width) / 2f;
            float imgY = totalBounds.Y;

            RectangleF imgRect = new RectangleF(
                imgX,
                imgY,
                style.TopImageSize.Width,
                style.TopImageSize.Height
            );

            // 绘制图片阴影
            ShadowRenderer.DrawImageShadow(graphics, imgRect);

            // 绘制圆角图片
            using (GraphicsPath imgPath = CreateRoundedRectangle(imgRect, 6))
            {
                graphics.SetClip(imgPath);
                graphics.DrawImage(style.TopImage, imgRect);
                graphics.ResetClip();

                // 绘制图片边框
                using (Pen borderPen = new Pen(Color.FromArgb(100, Color.Gray), 1f))
                {
                    graphics.DrawPath(borderPen, imgPath);
                }
            }
        }
        #endregion

        #region v2.0：背景图片渲染
        /// <summary>
        /// 绘制节点背景图片（支持4种填充模式）
        /// </summary>
        public static void DrawBackgroundImage(Graphics graphics, RectangleF bounds, NodeStyle style)
        {
            if (graphics == null) throw new ArgumentNullException("graphics");
            if (style == null) throw new ArgumentNullException("style");
            if (style.BackgroundImage == null) return;

            Image bgImage = style.BackgroundImage;
            RectangleF destRect = bounds;

            switch (style.BackgroundImageMode)
            {
                case BackgroundImageMode.Stretch:
                    // 拉伸填充
                    graphics.DrawImage(bgImage, destRect);
                    break;

                case BackgroundImageMode.Tile:
                    // 平铺
                    using (TextureBrush brush = new TextureBrush(bgImage, WrapMode.Tile))
                    {
                        graphics.FillRectangle(brush, destRect);
                    }
                    break;

                case BackgroundImageMode.Center:
                    // 居中（不缩放）
                    float centerX = bounds.X + (bounds.Width - bgImage.Width) / 2f;
                    float centerY = bounds.Y + (bounds.Height - bgImage.Height) / 2f;
                    graphics.DrawImageUnscaled(bgImage, (int)centerX, (int)centerY);
                    break;

                case BackgroundImageMode.Zoom:
                    // 等比缩放（保持宽高比）
                    RectangleF zoomRect = CalculateZoomRect(bounds, bgImage.Size);
                    graphics.DrawImage(bgImage, zoomRect);
                    break;
            }
        }

        /// <summary>
        /// 计算等比缩放矩形
        /// </summary>
        private static RectangleF CalculateZoomRect(RectangleF bounds, Size imageSize)
        {
            float scaleX = bounds.Width / imageSize.Width;
            float scaleY = bounds.Height / imageSize.Height;
            float scale = Math.Min(scaleX, scaleY);

            float newWidth = imageSize.Width * scale;
            float newHeight = imageSize.Height * scale;
            float x = bounds.X + (bounds.Width - newWidth) / 2f;
            float y = bounds.Y + (bounds.Height - newHeight) / 2f;

            return new RectangleF(x, y, newWidth, newHeight);
        }
        #endregion

        #region 节点主体绘制（v2.0扩展）
        /// <summary>
        /// 绘制节点主体（v2.0扩展：支持背景图片）
        /// </summary>
        public static GraphicsPath DrawNodeBody(Graphics graphics, RectangleF totalBounds, MindMapNode node)
        {
            if (graphics == null) throw new ArgumentNullException("graphics");
            if (node == null) throw new ArgumentNullException("node");

            NodeStyle style = node.Style;

            // v2.0：先绘制顶部图片
            DrawTopImage(graphics, totalBounds, style);

            // 获取主体区域
            RectangleF bodyBounds = GetNodeBodyBounds(totalBounds, style);

            // 图片节点特殊处理
            if (style.Shape == NodeShape.Image)
            {
                return DrawImageNode(graphics, bodyBounds, style);
            }

            // v2.1.7：下划线样式（XMind风格：文字+底部横线，无背景无边框）
            if (style.Shape == NodeShape.Underline)
            {
                // 不绘制背景，只在底部绘制一条横线
                float lineY = bodyBounds.Bottom - 4f;
                using (Pen linePen = new Pen(style.ForeColor, 1.5f))
                {
                    linePen.StartCap = LineCap.Round;
                    graphics.DrawLine(linePen, bodyBounds.X + 5f, lineY, bodyBounds.Right - 5f, lineY);
                }
                return null;  // 下划线样式无闭合路径
            }

            GraphicsPath path = CreateNodeShapePath(bodyBounds, style);

            // v2.0：先绘制背景图片（如果有）
            if (style.BackgroundImage != null)
            {
                graphics.SetClip(path);
                DrawBackgroundImage(graphics, bodyBounds, style);
                graphics.ResetClip();
            }

            // 绘制背景（渐变或纯色）
            if (style.UseGradient && style.BackColor2 != Color.Empty)
            {
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    bodyBounds, style.BackColor, style.BackColor2, LinearGradientMode.Vertical))
                {
                    graphics.FillPath(brush, path);
                }
            }
            else
            {
                using (SolidBrush brush = new SolidBrush(style.BackColor))
                {
                    graphics.FillPath(brush, path);
                }
            }

            // v2.1.6：绘制边框（ShowBorder属性控制）
            if (style.ShowBorder && style.BorderStyle != NodeBorderStyle.None && style.BorderWidth > 0)
            {
                using (Pen borderPen = new Pen(style.BorderColor, style.BorderWidth))
                {
                    borderPen.LineJoin = LineJoin.Round;
                    borderPen.DashStyle = style.GetDashStyle();
                    graphics.DrawPath(borderPen, path);
                }
            }

            return path;
        }

        /// <summary>
        /// 绘制图片节点
        /// </summary>
        private static GraphicsPath DrawImageNode(Graphics graphics, RectangleF bounds, NodeStyle style)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(bounds);

            // 绘制阴影
            ShadowRenderer.DrawImageShadow(graphics, bounds);

            // 绘制圆角矩形背景
            using (GraphicsPath bgPath = CreateRoundedRectangle(bounds, 6))
            {
                using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(240, 240, 240)))
                {
                    graphics.FillPath(bgBrush, bgPath);
                }
                using (Pen borderPen = new Pen(Color.FromArgb(180, style.BorderColor), style.BorderWidth))
                {
                    borderPen.LineJoin = LineJoin.Round;
                    graphics.DrawPath(borderPen, bgPath);
                }
            }

            // 绘制图片（居中缩放）
            if (style.Image != null)
            {
                RectangleF imgRect = CalculateImageRect(bounds, style.Image.Size);
                graphics.DrawImage(style.Image, imgRect);
            }

            return path;
        }

        /// <summary>
        /// 创建节点形状路径
        /// </summary>
        private static GraphicsPath CreateNodeShapePath(RectangleF bounds, NodeStyle style)
        {
            GraphicsPath path = new GraphicsPath();
            switch (style.Shape)
            {
                case NodeShape.RoundedRectangle:
                case NodeShape.TitleWithSubtitle:  // v2.0：副标题形状使用圆角矩形
                    AddRoundedRectangle(path, bounds, style.CornerRadius);
                    break;
                case NodeShape.Rectangle:
                    path.AddRectangle(bounds);
                    break;
                case NodeShape.Ellipse:
                    path.AddEllipse(bounds);
                    break;
                case NodeShape.Diamond:
                    AddDiamond(path, bounds);
                    break;
                case NodeShape.Parallelogram:
                    AddParallelogram(path, bounds);
                    break;
                case NodeShape.Hexagon:
                    AddHexagon(path, bounds);
                    break;
                case NodeShape.Octagon:
                    AddOctagon(path, bounds);
                    break;
                case NodeShape.Pill:  // v2.1.6：胶囊形状（两边半圆）
                    // 胶囊形状：高度的一半作为圆角半径，形成两端半圆
                    float pillRadius = bounds.Height / 2f;
                    AddRoundedRectangle(path, bounds, (int)pillRadius);
                    break;
                case NodeShape.Underline:  // v2.1.7：下划线样式（文字+底部横线，XMind风格）
                    // 下划线样式：不绘制闭合路径，在DrawNodeBody中单独处理
                    // 这里返回空路径，因为背景和边框都不绘制
                    break;
                default:
                    AddRoundedRectangle(path, bounds, style.CornerRadius);
                    break;
            }
            return path;
        }
        #endregion

        #region v2.0：主标题+副标题绘制
        /// <summary>
        /// 绘制节点多图标+文本（v2.0扩展：支持主标题+副标题）
        /// </summary>
        public static void DrawNodeIconAndText(Graphics graphics, RectangleF totalBounds, MindMapNode node)
        {
            if (graphics == null) throw new ArgumentNullException("graphics");
            if (node == null) throw new ArgumentNullException("node");

            // 图片节点不绘制文本
            if (node.Style.Shape == NodeShape.Image)
                return;

            NodeStyle style = node.Style;
            RectangleF bodyBounds = GetNodeBodyBounds(totalBounds, style);

            // v2.0：主标题+副标题形状
            if (style.Shape == NodeShape.TitleWithSubtitle)
            {
                DrawTitleWithSubtitle(graphics, bodyBounds, node);
                return;
            }

            // 普通形状：图标+文本
            int iconCount = style.Icons.Count;
            bool hasIcons = iconCount > 0 && style.IconPosition != IconPosition.None;

            // 计算内容区域（减去内边距）
            RectangleF contentRect = new RectangleF(
                bodyBounds.X + NODE_TEXT_PADDING_H,
                bodyBounds.Y + NODE_TEXT_PADDING_V,
                bodyBounds.Width - NODE_TEXT_PADDING_H * 2,
                bodyBounds.Height - NODE_TEXT_PADDING_V * 2
            );

            if (!hasIcons)
            {
                // 无图标：文本居中
                DrawCenteredText(graphics, contentRect, node.Text, style.Font, style.ForeColor);
                return;
            }

            // 多图标支持
            switch (style.IconPosition)
            {
                case IconPosition.Left:
                    DrawIconsLeftTextRight(graphics, contentRect, style, node.Text, style.Font, style.ForeColor);
                    break;
                case IconPosition.Right:
                    DrawTextLeftIconsRight(graphics, contentRect, style, node.Text, style.Font, style.ForeColor);
                    break;
                case IconPosition.Top:
                    DrawIconsTopTextBottom(graphics, contentRect, style, node.Text, style.Font, style.ForeColor);
                    break;
            }
        }

        /// <summary>
        /// v2.0：绘制主标题+副标题（两行文本）
        /// </summary>
        private static void DrawTitleWithSubtitle(Graphics graphics, RectangleF bodyBounds, MindMapNode node)
        {
            NodeStyle style = node.Style;

            // 计算内容区域
            RectangleF contentRect = new RectangleF(
                bodyBounds.X + NODE_TEXT_PADDING_H,
                bodyBounds.Y + NODE_TEXT_PADDING_V,
                bodyBounds.Width - NODE_TEXT_PADDING_H * 2,
                bodyBounds.Height - NODE_TEXT_PADDING_V * 2
            );

            SizeF titleSize = graphics.MeasureString(node.Text, style.Font);
            bool hasSubtitle = !string.IsNullOrEmpty(style.Subtitle);

            if (!hasSubtitle)
            {
                // 只有主标题
                DrawCenteredText(graphics, contentRect, node.Text, style.Font, style.ForeColor);
                return;
            }

            SizeF subtitleSize = graphics.MeasureString(style.Subtitle, style.SubtitleFont);

            // 两行文本整体垂直居中
            float totalHeight = titleSize.Height + style.SubtitleSpacing + subtitleSize.Height;
            float startY = contentRect.Y + (contentRect.Height - totalHeight) / 2f;

            // 主标题（加粗，居中）
            RectangleF titleRect = new RectangleF(
                contentRect.X,
                startY,
                contentRect.Width,
                titleSize.Height
            );
            DrawHorizontallyCenteredText(graphics, titleRect, node.Text, style.Font, style.ForeColor);

            // 副标题（灰色，小号，居中）
            RectangleF subtitleRect = new RectangleF(
                contentRect.X,
                startY + titleSize.Height + style.SubtitleSpacing,
                contentRect.Width,
                subtitleSize.Height
            );
            DrawHorizontallyCenteredText(graphics, subtitleRect, style.Subtitle, style.SubtitleFont, style.SubtitleColor);
        }

        /// <summary>
        /// 多图标在左，文本在右（XMind默认风格）
        /// [图标1] [图标2] [图标3] 节点文本
        /// </summary>
        private static void DrawIconsLeftTextRight(Graphics g, RectangleF rect, NodeStyle style, string text, Font font, Color textColor)
        {
            int totalIconsWidth = style.GetTotalIconsWidth();
            SizeF textSize = g.MeasureString(text, font);

            // 整体居中
            float totalWidth = totalIconsWidth + ICON_TEXT_SPACING + textSize.Width;
            float startX = rect.X + (rect.Width - totalWidth) / 2f;
            float centerY = rect.Y + rect.Height / 2f;

            // 循环绘制所有图标（带间距）
            float iconX = startX;
            foreach (Image icon in style.Icons)
            {
                RectangleF iconRect = new RectangleF(
                    iconX,
                    centerY - style.IconSize.Height / 2f,
                    style.IconSize.Width,
                    style.IconSize.Height
                );
                g.DrawImage(icon, iconRect);
                iconX += style.IconSize.Width + style.IconSpacing;
            }

            // 绘制文本（垂直居中）
            RectangleF textRect = new RectangleF(
                startX + totalIconsWidth + ICON_TEXT_SPACING,
                rect.Y,
                rect.Width - totalIconsWidth - ICON_TEXT_SPACING,
                rect.Height
            );
            DrawVerticallyCenteredText(g, textRect, text, font, textColor);
        }

        /// <summary>
        /// 文本在左，多图标在右
        /// 节点文本 [图标1] [图标2] [图标3]
        /// </summary>
        private static void DrawTextLeftIconsRight(Graphics g, RectangleF rect, NodeStyle style, string text, Font font, Color textColor)
        {
            int totalIconsWidth = style.GetTotalIconsWidth();
            SizeF textSize = g.MeasureString(text, font);

            float totalWidth = textSize.Width + ICON_TEXT_SPACING + totalIconsWidth;
            float startX = rect.X + (rect.Width - totalWidth) / 2f;
            float centerY = rect.Y + rect.Height / 2f;

            // 绘制文本
            RectangleF textRect = new RectangleF(
                startX,
                rect.Y,
                textSize.Width,
                rect.Height
            );
            DrawVerticallyCenteredText(g, textRect, text, font, textColor);

            // 循环绘制所有图标
            float iconX = startX + textSize.Width + ICON_TEXT_SPACING;
            foreach (Image icon in style.Icons)
            {
                RectangleF iconRect = new RectangleF(
                    iconX,
                    centerY - style.IconSize.Height / 2f,
                    style.IconSize.Width,
                    style.IconSize.Height
                );
                g.DrawImage(icon, iconRect);
                iconX += style.IconSize.Width + style.IconSpacing;
            }
        }

        /// <summary>
        /// 多图标在上，文本在下
        /// [图标1] [图标2] [图标3]
        ///      节点文本
        /// </summary>
        private static void DrawIconsTopTextBottom(Graphics g, RectangleF rect, NodeStyle style, string text, Font font, Color textColor)
        {
            int totalIconsWidth = style.GetTotalIconsWidth();
            SizeF textSize = g.MeasureString(text, font);

            float totalHeight = style.IconSize.Height + ICON_TEXT_SPACING + textSize.Height;
            float startY = rect.Y + (rect.Height - totalHeight) / 2f;
            float centerX = rect.X + rect.Width / 2f;

            // 绘制所有图标（水平居中排列）
            float iconX = centerX - totalIconsWidth / 2f;
            foreach (Image icon in style.Icons)
            {
                RectangleF iconRect = new RectangleF(
                    iconX,
                    startY,
                    style.IconSize.Width,
                    style.IconSize.Height
                );
                g.DrawImage(icon, iconRect);
                iconX += style.IconSize.Width + style.IconSpacing;
            }

            // 绘制文本（水平居中）
            RectangleF textRect = new RectangleF(
                rect.X,
                startY + style.IconSize.Height + ICON_TEXT_SPACING,
                rect.Width,
                rect.Height - style.IconSize.Height - ICON_TEXT_SPACING
            );
            DrawHorizontallyCenteredText(g, textRect, text, font, textColor);
        }

        /// <summary>
        /// 绘制居中文本（无图标时）
        /// </summary>
        private static void DrawCenteredText(Graphics g, RectangleF rect, string text, Font font, Color color)
        {
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags = StringFormatFlags.NoWrap;
                using (SolidBrush textBrush = new SolidBrush(color))
                {
                    g.DrawString(text, font, textBrush, rect, sf);
                }
            }
        }

        /// <summary>
        /// 绘制垂直居中文本
        /// </summary>
        private static void DrawVerticallyCenteredText(Graphics g, RectangleF rect, string text, Font font, Color color)
        {
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags = StringFormatFlags.NoWrap;
                using (SolidBrush textBrush = new SolidBrush(color))
                {
                    g.DrawString(text, font, textBrush, rect, sf);
                }
            }
        }

        /// <summary>
        /// 绘制水平居中文本
        /// </summary>
        private static void DrawHorizontallyCenteredText(Graphics g, RectangleF rect, string text, Font font, Color color)
        {
            using (StringFormat sf = new StringFormat())
            {
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Near;
                sf.Trimming = StringTrimming.EllipsisCharacter;
                sf.FormatFlags = StringFormatFlags.NoWrap;
                using (SolidBrush textBrush = new SolidBrush(color))
                {
                    g.DrawString(text, font, textBrush, rect, sf);
                }
            }
        }
        #endregion

        #region 展开按钮绘制
        /// <summary>
        /// 获取展开按钮边界
        /// </summary>
        public static RectangleF GetExpandButtonBounds(RectangleF nodeBounds)
        {
            float buttonX = nodeBounds.Right + EXPAND_BUTTON_OFFSET;
            float buttonY = nodeBounds.Y + nodeBounds.Height / 2f - EXPAND_BUTTON_SIZE / 2f;
            return new RectangleF(buttonX, buttonY, EXPAND_BUTTON_SIZE, EXPAND_BUTTON_SIZE);
        }

        /// <summary>
        /// 绘制迷你展开按钮（在节点右侧外部）
        /// </summary>
        public static void DrawExpandButton(Graphics graphics, RectangleF nodeBounds, bool isExpanded)
        {
            if (graphics == null) throw new ArgumentNullException("graphics");

            // 按钮位置：节点右侧外部6px处，垂直居中
            float buttonX = nodeBounds.Right + EXPAND_BUTTON_OFFSET;
            float buttonY = nodeBounds.Y + nodeBounds.Height / 2f - EXPAND_BUTTON_SIZE / 2f;
            RectangleF buttonBounds = new RectangleF(buttonX, buttonY, EXPAND_BUTTON_SIZE, EXPAND_BUTTON_SIZE);

            // 浅灰垂直渐变背景
            using (LinearGradientBrush bgBrush = new LinearGradientBrush(
                buttonBounds, Color.FromArgb(245, 245, 245), Color.FromArgb(220, 220, 220), LinearGradientMode.Vertical))
            {
                using (GraphicsPath buttonPath = CreateRoundedRectangle(buttonBounds, 3))
                {
                    graphics.FillPath(bgBrush, buttonPath);
                    using (Pen borderPen = new Pen(Color.FromArgb(180, 180, 180), 1f))
                    {
                        graphics.DrawPath(borderPen, buttonPath);
                    }
                }
            }

            // 绘制 +/- 符号
            float centerX = buttonBounds.X + buttonBounds.Width / 2f;
            float centerY = buttonBounds.Y + buttonBounds.Height / 2f;
            float lineHalf = 4f;

            using (Pen symbolPen = new Pen(Color.FromArgb(100, 100, 100), 1.5f))
            {
                // 横线
                graphics.DrawLine(symbolPen, centerX - lineHalf, centerY, centerX + lineHalf, centerY);

                // 竖线（折叠状态）
                if (!isExpanded)
                {
                    graphics.DrawLine(symbolPen, centerX, centerY - lineHalf, centerX, centerY + lineHalf);
                }
            }
        }
        #endregion

        #region v2.3新增：分方向展开按钮（4个方向独立）
        /// <summary>
        /// 【v2.3新增】获取指定方向的展开按钮边界
        /// </summary>
        /// <param name="nodeBounds">节点边界</param>
        /// <param name="direction">方向（右/左/上/下）</param>
        /// <returns>按钮边界</returns>
        public static RectangleF GetExpandButtonBounds(RectangleF nodeBounds, NodeDirection direction)
        {
            const float offset = EXPAND_BUTTON_OFFSET;
            const float size = EXPAND_BUTTON_SIZE;

            switch (direction)
            {
                case NodeDirection.Right:
                    // 节点右侧外部
                    return new RectangleF(
                        nodeBounds.Right + offset,
                        nodeBounds.Y + nodeBounds.Height / 2f - size / 2f,
                        size, size);

                case NodeDirection.Left:
                    // 节点左侧外部
                    return new RectangleF(
                        nodeBounds.Left - offset - size,
                        nodeBounds.Y + nodeBounds.Height / 2f - size / 2f,
                        size, size);

                case NodeDirection.Top:
                    // 节点顶部外部
                    return new RectangleF(
                        nodeBounds.X + nodeBounds.Width / 2f - size / 2f,
                        nodeBounds.Top - offset - size,
                        size, size);

                case NodeDirection.Bottom:
                    // 节点底部外部
                    return new RectangleF(
                        nodeBounds.X + nodeBounds.Width / 2f - size / 2f,
                        nodeBounds.Bottom + offset,
                        size, size);

                default:
                    return RectangleF.Empty;
            }
        }

        /// <summary>
        /// 【v2.3新增】绘制指定方向的展开按钮
        /// </summary>
        /// <param name="graphics">绘图对象</param>
        /// <param name="nodeBounds">节点边界</param>
        /// <param name="direction">方向</param>
        /// <param name="isExpanded">是否展开</param>
        public static void DrawExpandButton(Graphics graphics, RectangleF nodeBounds, NodeDirection direction, bool isExpanded)
        {
            if (graphics == null) throw new ArgumentNullException("graphics");

            RectangleF buttonBounds = GetExpandButtonBounds(nodeBounds, direction);
            if (buttonBounds.IsEmpty) return;

            // 浅灰垂直渐变背景
            using (LinearGradientBrush bgBrush = new LinearGradientBrush(
                buttonBounds, Color.FromArgb(245, 245, 245), Color.FromArgb(220, 220, 220), LinearGradientMode.Vertical))
            {
                using (GraphicsPath buttonPath = CreateRoundedRectangle(buttonBounds, 3))
                {
                    graphics.FillPath(bgBrush, buttonPath);
                    using (Pen borderPen = new Pen(Color.FromArgb(180, 180, 180), 1f))
                    {
                        graphics.DrawPath(borderPen, buttonPath);
                    }
                }
            }

            // 绘制 +/- 符号
            float centerX = buttonBounds.X + buttonBounds.Width / 2f;
            float centerY = buttonBounds.Y + buttonBounds.Height / 2f;
            float lineHalf = 4f;

            using (Pen symbolPen = new Pen(Color.FromArgb(100, 100, 100), 1.5f))
            {
                // 横线
                graphics.DrawLine(symbolPen, centerX - lineHalf, centerY, centerX + lineHalf, centerY);

                // 竖线（折叠状态）
                if (!isExpanded)
                {
                    graphics.DrawLine(symbolPen, centerX, centerY - lineHalf, centerX, centerY + lineHalf);
                }
            }
        }

        /// <summary>
        /// 【v2.3新增】命中测试：检测点击了哪个方向的展开按钮
        /// </summary>
        /// <param name="nodeBounds">节点边界</param>
        /// <param name="point">点击位置</param>
        /// <returns>点击的方向，null表示没有点击任何按钮</returns>
        public static NodeDirection? HitTestExpandButton(RectangleF nodeBounds, PointF point)
        {
            foreach (NodeDirection direction in System.Enum.GetValues(typeof(NodeDirection)))
            {
                RectangleF buttonBounds = GetExpandButtonBounds(nodeBounds, direction);
                if (buttonBounds.Contains(point))
                {
                    return direction;
                }
            }
            return null;
        }
        #endregion


        #region 辅助形状方法
        private static void AddRoundedRectangle(GraphicsPath path, RectangleF rect, int radius)
        {
            float x = rect.X;
            float y = rect.Y;
            float w = rect.Width;
            float h = rect.Height;
            float r = Math.Min(radius, Math.Min(w / 2f, h / 2f));

            path.AddArc(x, y, r * 2, r * 2, 180, 90);
            path.AddArc(x + w - r * 2, y, r * 2, r * 2, 270, 90);
            path.AddArc(x + w - r * 2, y + h - r * 2, r * 2, r * 2, 0, 90);
            path.AddArc(x, y + h - r * 2, r * 2, r * 2, 90, 90);
            path.CloseFigure();
        }

        private static GraphicsPath CreateRoundedRectangle(RectangleF rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            AddRoundedRectangle(path, rect, radius);
            return path;
        }

        private static void AddDiamond(GraphicsPath path, RectangleF rect)
        {
            // 菱形：四个顶点在矩形四条边的中点，填满整个矩形
            float cx = rect.X + rect.Width / 2f;
            float cy = rect.Y + rect.Height / 2f;
            path.AddPolygon(new PointF[] {
                new PointF(cx, rect.Y),          // 上顶点
                new PointF(rect.Right, cy),      // 右顶点
                new PointF(cx, rect.Bottom),     // 下顶点
                new PointF(rect.X, cy)           // 左顶点
            });
        }

        private static void AddParallelogram(GraphicsPath path, RectangleF rect)
        {
            float skew = rect.Width * 0.15f;
            path.AddPolygon(new PointF[] {
                new PointF(rect.X + skew, rect.Y),
                new PointF(rect.Right, rect.Y),
                new PointF(rect.Right - skew, rect.Bottom),
                new PointF(rect.X, rect.Bottom)
            });
        }

        private static void AddHexagon(GraphicsPath path, RectangleF rect)
        {
            // 六边形：填满整个矩形，横向为长轴
            float cx = rect.X + rect.Width / 2f;
            float cy = rect.Y + rect.Height / 2f;
            float rx = rect.Width / 2f;   // 水平半径
            float ry = rect.Height / 2f;  // 垂直半径
            PointF[] points = new PointF[6];
            for (int i = 0; i < 6; i++)
            {
                double angle = Math.PI / 3 * i - Math.PI / 2;
                points[i] = new PointF(
                    cx + (float)(rx * Math.Cos(angle)),
                    cy + (float)(ry * Math.Sin(angle))
                );
            }
            path.AddPolygon(points);
        }

        private static void AddOctagon(GraphicsPath path, RectangleF rect)
        {
            // 八角形：填满整个矩形，使用椭圆半径
            float cx = rect.X + rect.Width / 2f;
            float cy = rect.Y + rect.Height / 2f;
            float rx = rect.Width / 2f;   // 水平半径
            float ry = rect.Height / 2f;  // 垂直半径
            PointF[] points = new PointF[8];
            for (int i = 0; i < 8; i++)
            {
                double angle = Math.PI / 4 * i - Math.PI / 8;  // 偏移角度，让顶点在上下左右
                points[i] = new PointF(
                    cx + (float)(rx * Math.Cos(angle)),
                    cy + (float)(ry * Math.Sin(angle))
                );
            }
            path.AddPolygon(points);
        }

        private static RectangleF CalculateImageRect(RectangleF bounds, Size imageSize)
        {
            float padding = 8f;
            RectangleF innerRect = new RectangleF(
                bounds.X + padding,
                bounds.Y + padding,
                bounds.Width - padding * 2,
                bounds.Height - padding * 2
            );
            return CalculateZoomRect(innerRect, imageSize);
        }
        #endregion
    }
}
