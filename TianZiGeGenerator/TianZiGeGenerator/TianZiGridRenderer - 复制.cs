//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Drawing.Printing;
//using System.IO;
//using System.Windows.Forms;

//namespace TianZiGeGenerator
//{
//    public class TianZiGridRenderer
//    {
//        private WorksheetConfig _config;

//        public TianZiGridRenderer(WorksheetConfig config) { _config = config; }

//        public PageLayout CalculateLayout()
//        {
//            PageLayout l = new PageLayout();
//            l.PageWidth = _config.GetPageWidth();
//            l.PageHeight = _config.GetPageHeight();
//            l.MarginLeft = 12f; l.MarginRight = 12f;
//            l.MarginTop = 10f; l.MarginBottom = 10f;
//            l.HeaderHeight = 8f; l.InfoBarHeight = 10f;
//            l.PinyinHeight = _config.ShowPinyin ? _config.CellSize * 0.32f : 0f;
//            l.RowHeight = l.PinyinHeight + _config.CellSize;

//            float aw = l.PageWidth - l.MarginLeft - l.MarginRight;
//            float ah = l.PageHeight - l.MarginTop - l.MarginBottom - l.HeaderHeight - l.InfoBarHeight;
//            l.Columns = (int)(aw / _config.CellSize);
//            l.Rows = (int)(ah / l.RowHeight);
//            if (l.Columns < 1) l.Columns = 1;
//            if (l.Rows < 1) l.Rows = 1;
//            float gw = l.Columns * _config.CellSize;
//            l.GridStartX = l.MarginLeft + (aw - gw) / 2f;
//            l.GridStartY = l.MarginTop + l.HeaderHeight + l.InfoBarHeight;
//            return l;
//        }

//        private List<string> ParseWords(string text)
//        {
//            List<string> words = new List<string>();
//            if (string.IsNullOrEmpty(text)) return words;
//            if (_config.WordMode)
//            {
//                string[] parts = text.Split(new char[] { ' ', '\t', '\n', '\r' });
//                for (int i = 0; i < parts.Length; i++)
//                {
//                    string w = parts[i].Trim();
//                    if (w.Length > 0) words.Add(w);
//                }
//            }
//            else
//            {
//                for (int i = 0; i < text.Length; i++)
//                    if (!char.IsWhiteSpace(text[i])) words.Add(text[i].ToString());
//            }
//            return words;
//        }

//        public int GetTotalPages()
//        {
//            if (string.IsNullOrEmpty(_config.InputText)) return 1;
//            List<string> words = ParseWords(_config.InputText);
//            if (words.Count == 0) return 1;
//            PageLayout l = CalculateLayout();
//            int rpw = 1 + _config.PracticeRows;
//            int tr = words.Count * rpw;
//            int p = tr / l.Rows;
//            if (tr % l.Rows != 0) p++;
//            return p < 1 ? 1 : p;
//        }

//        public void DrawPage(Graphics g, int pageIndex, bool isForPrint)
//        {
//            g.SmoothingMode = SmoothingMode.AntiAlias;
//            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
//            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
//            PageLayout l = CalculateLayout();
//            g.Clear(Color.White);
//            DrawHeader(g, l);
//            DrawInfoBar(g, l);
//            DrawGridArea(g, l, pageIndex);
//        }

//        private void DrawHeader(Graphics g, PageLayout l)
//        {
//            if (string.IsNullOrEmpty(_config.PageHeader)) return;
//            using (Font f = new Font("宋体", 9f))
//            using (SolidBrush b = new SolidBrush(Color.Black))
//            {
//                StringFormat sf = new StringFormat();
//                sf.Alignment = StringAlignment.Center;
//                sf.LineAlignment = StringAlignment.Center;
//                g.DrawString(_config.PageHeader, f, b,
//                    new RectangleF(l.MarginLeft, l.MarginTop, l.PageWidth - l.MarginLeft - l.MarginRight, l.HeaderHeight), sf);
//            }
//        }

//        private void DrawInfoBar(Graphics g, PageLayout l)
//        {
//            if (string.IsNullOrEmpty(_config.InfoFields)) return;
//            string[] fields = _config.InfoFields.Split(new char[] { ',', '，' });
//            if (fields.Length == 0) return;
//            float y = l.MarginTop + l.HeaderHeight + 2f;
//            float tw = l.PageWidth - l.MarginLeft - l.MarginRight;
//            float fw = tw / fields.Length;
//            using (Font lf = new Font("宋体", 8f))
//            using (SolidBrush b = new SolidBrush(Color.Black))
//            using (Pen lp = new Pen(Color.Gray, 0.15f))
//            {
//                for (int i = 0; i < fields.Length; i++)
//                {
//                    float x = l.MarginLeft + i * fw;
//                    string name = fields[i].Trim() + "：";
//                    g.DrawString(name, lf, b, x, y);
//                    SizeF ls = g.MeasureString(name, lf);
//                    float sx = x + ls.Width, ex = x + fw - 3f, ly = y + ls.Height - 1f;
//                    if (ex > sx) g.DrawLine(lp, sx, ly, ex, ly);
//                }
//            }
//        }

//        private void DrawGridArea(Graphics g, PageLayout l, int pageIndex)
//        {
//            List<string> words = ParseWords(_config.InputText);
//            if (words.Count == 0) return;

//            int rpw = 1 + _config.PracticeRows;
//            int sr = pageIndex * l.Rows;
//            int er = sr + l.Rows;

//            int gray = _config.TraceGrayLevel;
//            if (gray < 0) gray = 0; if (gray > 255) gray = 255;
//            Color tc = Color.FromArgb(gray, gray, gray);

//            float cs = _config.CellSize * 0.72f * 2.835f;
//            if (cs < 4f) cs = 4f;
//            float ps = _config.CellSize * 0.2f * 2.835f;
//            if (ps < 3f) ps = 3f;

//            using (Font cf = CreateFont(_config.FontName, cs))
//            using (Font pf = new Font("Times New Roman", ps))
//            using (SolidBrush tb = new SolidBrush(tc))
//            using (SolidBrush sb = new SolidBrush(_config.SampleColor))
//            using (SolidBrush pb = new SolidBrush(_config.PinyinColor))
//            {
//                StringFormat cfmt = new StringFormat();
//                cfmt.Alignment = StringAlignment.Center;
//                cfmt.LineAlignment = StringAlignment.Center;
//                StringFormat pfmt = new StringFormat();
//                pfmt.Alignment = StringAlignment.Center;
//                pfmt.LineAlignment = StringAlignment.Center;

//                for (int row = sr; row < er && row < words.Count * rpw; row++)
//                {
//                    int lr = row - sr;
//                    int wi = row / rpw;
//                    int riw = row % rpw;
//                    string word = words[wi];
//                    float ry = l.GridStartY + lr * l.RowHeight;
//                    float gy = ry + l.PinyinHeight;

//                    if (riw == 0)
//                        DrawStrokeDemoRow(g, l, word, ry, gy, cf, pf, tb, sb, pb, cfmt, pfmt);
//                    else
//                        DrawPracticeRow(g, l, word, ry, gy, cf, pf, tb, pb, cfmt, pfmt);
//                }
//            }
//        }

//        /// <summary>绘制笔顺演示行：[样例字]+[笔顺演示格]+[描红重复格]</summary>
//        private void DrawStrokeDemoRow(Graphics g, PageLayout l, string word,
//            float ry, float gy, Font cf, Font pf, Brush tb, Brush sb, Brush pb,
//            StringFormat cfmt, StringFormat pfmt)
//        {
//            int col = 0;
//            int demo = _config.StrokeDemoCount;
//            int rep = _config.TraceRepeatCount < 1 ? 1 : _config.TraceRepeatCount;

//            for (int ci = 0; ci < word.Length && col < l.Columns; ci++)
//            {
//                char c = word[ci];

//                // 样例字列
//                if (_config.ShowSample)
//                {
//                    float sx = l.GridStartX + col * _config.CellSize;
//                    DrawCell(g, l, sx, ry, gy);
//                    DrawPinyin(g, sx, ry, c, pf, pb, pfmt);
//                    g.DrawString(c.ToString(), cf, sb,
//                        new RectangleF(sx, gy, _config.CellSize, _config.CellSize), cfmt);
//                    col++;
//                }

//                // 笔顺演示格
//                string[] strokes = StrokeData.GetStrokes(c);
//                int sc = (strokes != null) ? strokes.Length : 0;
//                int dc = (sc > 0 && demo > sc) ? sc : demo;
//                for (int d = 0; d < dc && col < l.Columns; d++)
//                {
//                    float sx = l.GridStartX + col * _config.CellSize;
//                    DrawCell(g, l, sx, ry, gy);
//                    DrawPinyin(g, sx, ry, c, pf, pb, pfmt);
//                    DrawInner(g, new RectangleF(sx, gy, _config.CellSize, _config.CellSize));
//                    if (strokes != null)
//                        DrawStrokes(g, strokes, d + 1, sx, gy, tb);
//                    else
//                        g.DrawString(c.ToString(), cf, tb,
//                            new RectangleF(sx, gy, _config.CellSize, _config.CellSize), cfmt);
//                    col++;
//                }

//                // 描红重复格
//                for (int r = 0; r < rep && col < l.Columns; r++)
//                {
//                    float sx = l.GridStartX + col * _config.CellSize;
//                    DrawCell(g, l, sx, ry, gy);
//                    DrawPinyin(g, sx, ry, c, pf, pb, pfmt);
//                    DrawInner(g, new RectangleF(sx, gy, _config.CellSize, _config.CellSize));
//                    g.DrawString(c.ToString(), cf, tb,
//                        new RectangleF(sx, gy, _config.CellSize, _config.CellSize), cfmt);
//                    col++;
//                }
//            }
//            // 剩余空格
//            for (; col < l.Columns; col++)
//            {
//                float sx = l.GridStartX + col * _config.CellSize;
//                DrawCell(g, l, sx, ry, gy);
//                DrawInner(g, new RectangleF(sx, gy, _config.CellSize, _config.CellSize));
//            }
//        }

//        /// <summary>绘制描红练习行</summary>
//        private void DrawPracticeRow(Graphics g, PageLayout l, string word,
//            float ry, float gy, Font cf, Font pf, Brush tb, Brush pb,
//            StringFormat cfmt, StringFormat pfmt)
//        {
//            int col = 0;
//            int rep = _config.TraceRepeatCount < 1 ? 1 : _config.TraceRepeatCount;

//            if (_config.ShowSample && word.Length > 0)
//            {
//                char c = word[0];
//                float sx = l.GridStartX + col * _config.CellSize;
//                DrawCell(g, l, sx, ry, gy);
//                DrawPinyin(g, sx, ry, c, pf, pb, pfmt);
//                DrawInner(g, new RectangleF(sx, gy, _config.CellSize, _config.CellSize));
//                col++;
//            }

//            int ci = 0, rc = 0;
//            while (col < l.Columns)
//            {
//                char c = word[ci % word.Length];
//                float sx = l.GridStartX + col * _config.CellSize;
//                DrawCell(g, l, sx, ry, gy);
//                DrawPinyin(g, sx, ry, c, pf, pb, pfmt);
//                DrawInner(g, new RectangleF(sx, gy, _config.CellSize, _config.CellSize));
//                g.DrawString(c.ToString(), cf, tb,
//                    new RectangleF(sx, gy, _config.CellSize, _config.CellSize), cfmt);
//                col++; rc++;
//                if (rc >= rep) { rc = 0; ci++; }
//            }
//        }

//        private void DrawCell(Graphics g, PageLayout l, float x, float ry, float gy)
//        {
//            using (Pen p = new Pen(_config.GridColor, 0.2f))
//            {
//                g.DrawRectangle(p, x, gy, _config.CellSize, _config.CellSize);
//                if (l.PinyinHeight > 0.1f)
//                {
//                    g.DrawLine(p, x, ry, x, gy);
//                    g.DrawLine(p, x + _config.CellSize, ry, x + _config.CellSize, gy);
//                    g.DrawLine(p, x, ry, x + _config.CellSize, ry);
//                }
//            }
//        }

//        private void DrawPinyin(Graphics g, float x, float ry, char c, Font f, Brush b, StringFormat sf)
//        {
//            if (!_config.ShowPinyin) return;
//            string py = PinyinHelper.GetPinyin(c);
//            if (py.Length == 0) return;
//            g.DrawString(py, f, b, new RectangleF(x, ry, _config.CellSize, _config.CellSize * 0.32f), sf);
//        }

//        private void DrawStrokes(Graphics g, string[] strokes, int count, float x, float y, Brush b)
//        {
//            float scale = _config.CellSize * 0.9f / 900f;
//            float ox = x + _config.CellSize * 0.05f;
//            float oy = y + _config.CellSize * 0.05f;
//            using (Pen sp = new Pen(b, _config.CellSize * 0.06f))
//            {
//                sp.StartCap = LineCap.Round;
//                sp.EndCap = LineCap.Round;
//                sp.LineJoin = LineJoin.Round;
//                for (int i = 0; i < count && i < strokes.Length; i++)
//                {
//                    using (GraphicsPath path = SvgPathParser.Parse(strokes[i], scale, ox, oy))
//                    {
//                        g.DrawPath(sp, path);
//                    }
//                }
//            }
//        }

//        private void DrawInner(Graphics g, RectangleF r)
//        {
//            switch (_config.GridType)
//            {
//                case GridType.TianZi: DrawTianZi(g, r); break;
//                case GridType.MiZi: DrawMiZi(g, r); break;
//                case GridType.HuiGong: DrawHuiGong(g, r); break;
//            }
//        }

//        private void DrawTianZi(Graphics g, RectangleF r)
//        {
//            using (Pen p = new Pen(_config.GridColor, 0.1f))
//            {
//                p.DashStyle = DashStyle.Dash;
//                float cx = r.X + r.Width / 2f, cy = r.Y + r.Height / 2f;
//                g.DrawLine(p, r.X, cy, r.X + r.Width, cy);
//                g.DrawLine(p, cx, r.Y, cx, r.Y + r.Height);
//            }
//        }

//        private void DrawMiZi(Graphics g, RectangleF r)
//        {
//            using (Pen p = new Pen(_config.GridColor, 0.1f))
//            {
//                p.DashStyle = DashStyle.Dash;
//                float cx = r.X + r.Width / 2f, cy = r.Y + r.Height / 2f;
//                g.DrawLine(p, r.X, cy, r.X + r.Width, cy);
//                g.DrawLine(p, cx, r.Y, cx, r.Y + r.Height);
//                g.DrawLine(p, r.X, r.Y, r.X + r.Width, r.Y + r.Height);
//                g.DrawLine(p, r.X + r.Width, r.Y, r.X, r.Y + r.Height);
//            }
//        }

//        private void DrawHuiGong(Graphics g, RectangleF r)
//        {
//            using (Pen p = new Pen(_config.GridColor, 0.1f))
//            {
//                float s = r.Width * 0.7f, o = (r.Width - s) / 2f;
//                g.DrawRectangle(p, r.X + o, r.Y + o, s, s);
//            }
//        }

//        private Font CreateFont(string name, float size)
//        {
//            try { return new Font(name, size); }
//            catch { return new Font("宋体", size); }
//        }

//        public void ExportToImage(string filePath, int pageIndex, float dpi)
//        {
//            PageLayout l = CalculateLayout();
//            int pw = (int)(l.PageWidth * dpi / 25.4f);
//            int ph = (int)(l.PageHeight * dpi / 25.4f);
//            using (Bitmap bmp = new Bitmap(pw, ph))
//            {
//                bmp.SetResolution(dpi, dpi);
//                using (Graphics g = Graphics.FromImage(bmp))
//                {
//                    g.PageUnit = GraphicsUnit.Millimeter;
//                    DrawPage(g, pageIndex, false);
//                }
//                string ext = Path.GetExtension(filePath).ToLower();
//                if (ext == ".jpg" || ext == ".jpeg")
//                    bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Jpeg);
//                else if (ext == ".bmp")
//                    bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Bmp);
//                else
//                    bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
//            }
//        }
//    }
//}
