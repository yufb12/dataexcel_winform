using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MindMap.Core;
using MindMap.Interfaces;
using MindMap.View;

namespace MindMap.Services
{
    /// <summary>
    /// 文件服务实现
    /// </summary>
    public class FileService : IFileService
    {
        /// <summary>
        /// 保存文档到文件
        /// </summary>
        public void SaveDocument(MindMapDocument document, string path)
        {
            if (document == null)
                throw new ArgumentNullException("document");
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("路径不能为空", "path");

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                formatter.Serialize(stream, document);
            }
        }

        /// <summary>
        /// 从文件加载文档
        /// </summary>
        public MindMapDocument LoadDocument(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("路径不能为空", "path");
            if (!File.Exists(path))
                throw new FileNotFoundException("文件不存在", path);

            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                return (MindMapDocument)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// 将视图导出为图片
        /// </summary>
        public void ExportToImage(MindMapView view, string path)
        {
            if (view == null)
                throw new ArgumentNullException("view");
            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("路径不能为空", "path");

            // 计算所有节点的边界
            RectangleF bounds = CalculateDocumentBounds(view.Document);
            
            // 添加边距
            const float margin = 20f;
            int width = (int)Math.Ceiling(bounds.Width + margin * 2);
            int height = (int)Math.Ceiling(bounds.Height + margin * 2);

            // 创建位图
            using (Bitmap bitmap = new Bitmap(width, height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(Color.White);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

                    // 平移坐标系
                    g.TranslateTransform(-bounds.X + margin, -bounds.Y + margin);

                    // 绘制所有节点
                    DrawAllNodes(g, view.Document.RootNode, view.Renderer);
                }

                // 保存图片
                bitmap.Save(path, ImageFormat.Png);
            }
        }

        /// <summary>
        /// 计算文档的边界矩形
        /// </summary>
        private static RectangleF CalculateDocumentBounds(MindMapDocument document)
        {
            if (document == null || document.RootNode == null)
                return RectangleF.Empty;

            float minX = float.MaxValue;
            float minY = float.MaxValue;
            float maxX = float.MinValue;
            float maxY = float.MinValue;

            CalculateBoundsRecursive(document.RootNode, ref minX, ref minY, ref maxX, ref maxY);

            return new RectangleF(minX, minY, maxX - minX, maxY - minY);
        }

        /// <summary>
        /// 递归计算边界
        /// </summary>
        private static void CalculateBoundsRecursive(MindMapNode node, ref float minX, ref float minY, ref float maxX, ref float maxY)
        {
            RectangleF bounds = node.Bounds;
            minX = Math.Min(minX, bounds.X);
            minY = Math.Min(minY, bounds.Y);
            maxX = Math.Max(maxX, bounds.Right);
            maxY = Math.Max(maxY, bounds.Bottom);

            if (node.IsExpanded)
            {
                for (int i = 0; i < node.ChildCount; i++)
                {
                    CalculateBoundsRecursive(node.ChildNodes[i], ref minX, ref minY, ref maxX, ref maxY);
                }
            }
        }

        /// <summary>
        /// 递归绘制所有节点
        /// </summary>
        private static void DrawAllNodes(Graphics g, MindMapNode node, INodeRenderer renderer)
        {
            // 先绘制连接线
            if (node.ParentNode != null)
            {
                renderer.DrawConnection(g, node);
            }

            // 绘制节点本身
            renderer.DrawNode(g, node, false);

            // 递归绘制子节点
            if (node.IsExpanded)
            {
                for (int i = 0; i < node.ChildCount; i++)
                {
                    DrawAllNodes(g, node.ChildNodes[i], renderer);
                }
            }
        }
    }
}
