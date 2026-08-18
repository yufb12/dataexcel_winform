using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace TianZiGeGenerator
{    

    #region 主窗体

    /// <summary>主窗体</summary>
    public class TianZiGeGeneratorForm : Form
    {
        private SplitContainer _mainSplit;
        private TextBox _txtInput;
        private NumericUpDown _numCellSize;
        private ComboBox _cboGridType, _cboCharMode, _cboOrientation, _cboPaperType, _cboFontName;
        private Button _btnGridColor, _btnPinyinColor, _btnSampleColor;
        private NumericUpDown _numTraceGray, _numStrokeDemo, _numTraceRepeat, _numPracticeRows;
        private TextBox _txtInfoFields, _txtPageHeader;
        private CheckBox _chkShowPinyin, _chkShowSample, _chkWordMode;
        private Panel _pnlPreview;
        private PictureBox _pnlPreviewPictureBox;
        private Button _btnGenerate, _btnPrint, _btnPrintPreview, _btnExportImage;
        private NumericUpDown _numPage;
        private Label _lblTotalPages;
        private ColorDialog _colorDialog;
        private PrintDocument _printDoc;
        private PrintPreviewDialog _printPreviewDlg;
        private SaveFileDialog _saveFileDlg;
        private WorksheetConfig _config;
        private TianZiGridRenderer _renderer;
        private int _currentPreviewPage, _printPageIndex;

        public TianZiGeGeneratorForm()
        {
            _config = new WorksheetConfig();
            _renderer = new TianZiGridRenderer(_config);
            _currentPreviewPage = 0; _printPageIndex = 0;
            InitializeComponent();
            InitializeControls();
            UpdatePreview();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TianZiGeGeneratorForm));
            this._mainSplit = new System.Windows.Forms.SplitContainer();
            this._colorDialog = new System.Windows.Forms.ColorDialog();
            this._printDoc = new System.Drawing.Printing.PrintDocument();
            this._printPreviewDlg = new System.Windows.Forms.PrintPreviewDialog();
            this._saveFileDlg = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this._mainSplit)).BeginInit();
            this._mainSplit.SuspendLayout();
            this.SuspendLayout();
            // 
            // _mainSplit
            // 
            this._mainSplit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this._mainSplit.Dock = System.Windows.Forms.DockStyle.Fill;
            this._mainSplit.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this._mainSplit.Location = new System.Drawing.Point(0, 0);
            this._mainSplit.Name = "_mainSplit";
            // 
            // _mainSplit.Panel2
            // 
            this._mainSplit.Panel2.SizeChanged += new System.EventHandler(this._mainSplit_Panel2_SizeChanged);
            this._mainSplit.Size = new System.Drawing.Size(1132, 733);
            this._mainSplit.SplitterDistance = 269;
            this._mainSplit.TabIndex = 0;
            // 
            // _colorDialog
            // 
            this._colorDialog.FullOpen = true;
            // 
            // _printDoc
            // 
            this._printDoc.BeginPrint += new System.Drawing.Printing.PrintEventHandler(this.PrintDoc_BeginPrint);
            this._printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.PrintDoc_PrintPage);
            // 
            // _printPreviewDlg
            // 
            this._printPreviewDlg.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this._printPreviewDlg.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this._printPreviewDlg.ClientSize = new System.Drawing.Size(882, 653);
            this._printPreviewDlg.Document = this._printDoc;
            this._printPreviewDlg.Enabled = true;
            this._printPreviewDlg.Icon = ((System.Drawing.Icon)(resources.GetObject("_printPreviewDlg.Icon")));
            this._printPreviewDlg.Name = "_printPreviewDlg";
            this._printPreviewDlg.Visible = false;
            // 
            // _saveFileDlg
            // 
            this._saveFileDlg.DefaultExt = "png";
            this._saveFileDlg.FileName = "田字格字帖.png";
            this._saveFileDlg.Filter = "PNG(*.png)|*.png|JPEG(*.jpg)|*.jpg|BMP(*.bmp)|*.bmp";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(1132, 733);
            this.Controls.Add(this._mainSplit);
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.MinimumSize = new System.Drawing.Size(950, 650);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "田字格字帖生成器（笔顺+拼音版）";
            ((System.ComponentModel.ISupportInitialize)(this._mainSplit)).EndInit();
            this._mainSplit.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private void _mainSplit_Panel2_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                if (_pnlPreview == null)
                    return;
                UpdatePreview();
            }
            catch (Exception ex)
            { 
            }
            
        }

        private void InitializeControls()
        {
            Panel pnl = new Panel();
            pnl.Dock = DockStyle.Fill;
            pnl.AutoScroll = true;
            pnl.Padding = new Padding(10);
            _mainSplit.Panel1.Controls.Add(pnl);

            int y = 10, lw = 95, cw = 210, rh = 28;

            Label t = new Label();
            t.Text = "字帖参数设置";
            t.Font = new Font("宋体", 11f, FontStyle.Bold);
            t.Location = new Point(10, y); t.Size = new Size(200, 25);
            pnl.Controls.Add(t); y += 32;

            AddLabel(pnl, "汉字内容：", 10, y, lw); y += 20;
            _txtInput = new TextBox();
            _txtInput.Location = new Point(10, y);
            _txtInput.Size = new Size(cw, 70);
            _txtInput.Multiline = true;
            _txtInput.ScrollBars = ScrollBars.Vertical;
            _txtInput.Text = _config.InputText;
            pnl.Controls.Add(_txtInput); y += 80;

            AddLabel(pnl, "词组模式(空格分词)：", 10, y, lw);
            _chkWordMode = new CheckBox();
            _chkWordMode.Location = new Point(10 + lw, y - 3);
            _chkWordMode.Size = new Size(100, 20);
            _chkWordMode.Checked = _config.WordMode;
            pnl.Controls.Add(_chkWordMode); y += rh;

            AddLabel(pnl, "格子大小(mm)：", 10, y, lw);
            _numCellSize = new NumericUpDown();
            _numCellSize.Location = new Point(10 + lw, y - 3);
            _numCellSize.Size = new Size(70, 20);
            _numCellSize.Minimum = 8; _numCellSize.Maximum = 30;
            _numCellSize.DecimalPlaces = 1; _numCellSize.Increment = 0.5m;
            _numCellSize.Value = (decimal)_config.CellSize;
            pnl.Controls.Add(_numCellSize); y += rh;

            AddLabel(pnl, "格子类型：", 10, y, lw);
            _cboGridType = new ComboBox();
            _cboGridType.Location = new Point(10 + lw, y - 3);
            _cboGridType.Size = new Size(cw - lw, 20);
            _cboGridType.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboGridType.Items.AddRange(new object[] { "田字格", "米字格", "回宫格", "方格" });
            _cboGridType.SelectedIndex = 0;
            pnl.Controls.Add(_cboGridType); y += rh;

            AddLabel(pnl, "排列模式：", 10, y, lw);
            _cboCharMode = new ComboBox();
            _cboCharMode.Location = new Point(10 + lw, y - 3);
            _cboCharMode.Size = new Size(cw - lw, 20);
            _cboCharMode.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboCharMode.Items.AddRange(new object[] { "描红+空白行交替", "全部描红" });
            _cboCharMode.SelectedIndex = 0;
            pnl.Controls.Add(_cboCharMode); y += rh;

            //AddLabel(pnl, "笔顺演示格数：", 10, y, lw);
            //_numStrokeDemo = new NumericUpDown();
            //_numStrokeDemo.Location = new Point(10 + lw, y - 3);
            //_numStrokeDemo.Size = new Size(70, 20);
            //_numStrokeDemo.Minimum = 0; _numStrokeDemo.Maximum = 20;
            //_numStrokeDemo.Value = _config.StrokeDemoCount;
            //pnl.Controls.Add(_numStrokeDemo); y += rh;

            AddLabel(pnl, "每字描红次数：", 10, y, lw);
            _numTraceRepeat = new NumericUpDown();
            _numTraceRepeat.Location = new Point(10 + lw, y - 3);
            _numTraceRepeat.Size = new Size(70, 20);
            _numTraceRepeat.Minimum = 1; _numTraceRepeat.Maximum = 20;
            _numTraceRepeat.Value = _config.TraceRepeatCount;
            pnl.Controls.Add(_numTraceRepeat); y += rh;

            AddLabel(pnl, "每字练习行数：", 10, y, lw);
            _numPracticeRows = new NumericUpDown();
            _numPracticeRows.Location = new Point(10 + lw, y - 3);
            _numPracticeRows.Size = new Size(70, 20);
            _numPracticeRows.Minimum = 0; _numPracticeRows.Maximum = 10;
            _numPracticeRows.Value = _config.PracticeRows;
            pnl.Controls.Add(_numPracticeRows); y += rh;

            AddLabel(pnl, "显示拼音：", 10, y, lw);
            _chkShowPinyin = new CheckBox();
            _chkShowPinyin.Location = new Point(10 + lw, y - 3);
            _chkShowPinyin.Size = new Size(100, 20);
            _chkShowPinyin.Checked = _config.ShowPinyin;
            pnl.Controls.Add(_chkShowPinyin); y += rh;

            AddLabel(pnl, "显示样例字列：", 10, y, lw);
            _chkShowSample = new CheckBox();
            _chkShowSample.Location = new Point(10 + lw, y - 3);
            _chkShowSample.Size = new Size(100, 20);
            _chkShowSample.Checked = _config.ShowSample;
            pnl.Controls.Add(_chkShowSample); y += rh;

            AddLabel(pnl, "页面方向：", 10, y, lw);
            _cboOrientation = new ComboBox();
            _cboOrientation.Location = new Point(10 + lw, y - 3);
            _cboOrientation.Size = new Size(cw - lw, 20);
            _cboOrientation.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboOrientation.Items.AddRange(new object[] { "竖版", "横版" });
            _cboOrientation.SelectedIndex = 0;
            pnl.Controls.Add(_cboOrientation); y += rh;

            AddLabel(pnl, "纸张类型：", 10, y, lw);
            _cboPaperType = new ComboBox();
            _cboPaperType.Location = new Point(10 + lw, y - 3);
            _cboPaperType.Size = new Size(cw - lw, 20);
            _cboPaperType.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboPaperType.Items.AddRange(new object[] { "A4", "Letter" });
            _cboPaperType.SelectedIndex = 0;
            pnl.Controls.Add(_cboPaperType); y += rh;

            AddLabel(pnl, "田格颜色：", 10, y, lw);
            _btnGridColor = new Button();
            _btnGridColor.Location = new Point(10 + lw, y - 3);
            _btnGridColor.Size = new Size(55, 22);
            _btnGridColor.Text = "选择";
            _btnGridColor.BackColor = _config.GridColor;
            _btnGridColor.FlatStyle = FlatStyle.Flat;
            _btnGridColor.Click += new EventHandler(BtnGridColor_Click);
            pnl.Controls.Add(_btnGridColor); y += rh;

            AddLabel(pnl, "拼音颜色：", 10, y, lw);
            _btnPinyinColor = new Button();
            _btnPinyinColor.Location = new Point(10 + lw, y - 3);
            _btnPinyinColor.Size = new Size(55, 22);
            _btnPinyinColor.Text = "选择";
            _btnPinyinColor.BackColor = _config.PinyinColor;
            _btnPinyinColor.FlatStyle = FlatStyle.Flat;
            _btnPinyinColor.Click += new EventHandler(BtnPinyinColor_Click);
            pnl.Controls.Add(_btnPinyinColor); y += rh;

            AddLabel(pnl, "样例字颜色：", 10, y, lw);
            _btnSampleColor = new Button();
            _btnSampleColor.Location = new Point(10 + lw, y - 3);
            _btnSampleColor.Size = new Size(55, 22);
            _btnSampleColor.Text = "选择";
            _btnSampleColor.BackColor = _config.SampleColor;
            _btnSampleColor.FlatStyle = FlatStyle.Flat;
            _btnSampleColor.Click += new EventHandler(BtnSampleColor_Click);
            pnl.Controls.Add(_btnSampleColor); y += rh;

            AddLabel(pnl, "描红深浅(50-255)：", 10, y, lw);
            _numTraceGray = new NumericUpDown();
            _numTraceGray.Location = new Point(10 + lw, y - 3);
            _numTraceGray.Size = new Size(70, 20);
            _numTraceGray.Minimum = 50; _numTraceGray.Maximum = 255;
            _numTraceGray.Value = _config.TraceGrayLevel;
            pnl.Controls.Add(_numTraceGray); y += rh;

            AddLabel(pnl, "字体：", 10, y, lw);
            _cboFontName = new ComboBox();
            _cboFontName.Location = new Point(10 + lw, y - 3);
            _cboFontName.Size = new Size(cw - lw, 20);
            _cboFontName.DropDownStyle = ComboBoxStyle.DropDownList;
            _cboFontName.Items.AddRange(new object[] { "楷体", "宋体", "黑体", "仿宋", "微软雅黑" });
            _cboFontName.SelectedIndex = 0;
            pnl.Controls.Add(_cboFontName); y += rh;

            AddLabel(pnl, "信息栏字段：", 10, y, lw);
            _txtInfoFields = new TextBox();
            _txtInfoFields.Location = new Point(10 + lw, y - 3);
            _txtInfoFields.Size = new Size(cw - lw, 20);
            _txtInfoFields.Text = _config.InfoFields;
            pnl.Controls.Add(_txtInfoFields); y += rh;

            AddLabel(pnl, "页头文字：", 10, y, lw);
            _txtPageHeader = new TextBox();
            _txtPageHeader.Location = new Point(10 + lw, y - 3);
            _txtPageHeader.Size = new Size(cw - lw, 20);
            _txtPageHeader.Text = _config.PageHeader;
            pnl.Controls.Add(_txtPageHeader); y += rh + 8;

            _btnGenerate = new Button();
            _btnGenerate.Location = new Point(10, y);
            _btnGenerate.Size = new Size(cw, 32);
            _btnGenerate.Text = "生成预览";
            _btnGenerate.Font = new Font("宋体", 10f, FontStyle.Bold);
            _btnGenerate.BackColor = Color.FromArgb(255, 200, 80);
            _btnGenerate.FlatStyle = FlatStyle.Flat;
            _btnGenerate.Click += new EventHandler(BtnGenerate_Click);
            pnl.Controls.Add(_btnGenerate); y += 40;

            Label sep = new Label();
            sep.BorderStyle = BorderStyle.Fixed3D;
            sep.Location = new Point(10, y); sep.Size = new Size(cw, 2);
            pnl.Controls.Add(sep); y += 12;

            Label ot = new Label();
            ot.Text = "输出操作";
            ot.Font = new Font("宋体", 10f, FontStyle.Bold);
            ot.Location = new Point(10, y); ot.Size = new Size(200, 22);
            pnl.Controls.Add(ot); y += 26;

            _btnPrint = new Button();
            _btnPrint.Location = new Point(10, y);
            _btnPrint.Size = new Size(95, 30);
            _btnPrint.Text = "打印";
            _btnPrint.Click += new EventHandler(BtnPrint_Click);
            pnl.Controls.Add(_btnPrint);

            _btnPrintPreview = new Button();
            _btnPrintPreview.Location = new Point(115, y);
            _btnPrintPreview.Size = new Size(95, 30);
            _btnPrintPreview.Text = "打印预览";
            _btnPrintPreview.Click += new EventHandler(BtnPrintPreview_Click);
            pnl.Controls.Add(_btnPrintPreview); y += 38;

            _btnExportImage = new Button();
            _btnExportImage.Location = new Point(10, y);
            _btnExportImage.Size = new Size(200, 30);
            _btnExportImage.Text = "导出为图片";
            _btnExportImage.Click += new EventHandler(BtnExportImage_Click);
            pnl.Controls.Add(_btnExportImage);

            // 右侧预览
            Panel right = new Panel();
            right.Dock = DockStyle.Fill;
            right.Padding = new Padding(10);
            right.BackColor = Color.LightGray;
            _mainSplit.Panel2.Controls.Add(right);

            Panel tb = new Panel();
            tb.Dock = DockStyle.Top;
            tb.Height = 36;
            tb.BackColor = Color.Gainsboro;
            right.Controls.Add(tb);

            Label lp = new Label(); lp.Text = "第"; lp.Location = new Point(10, 10); lp.Size = new Size(20, 20);
            tb.Controls.Add(lp);
            _numPage = new NumericUpDown();
            _numPage.Location = new Point(30, 7); _numPage.Size = new Size(55, 20);
            _numPage.Minimum = 1; _numPage.Maximum = 1; _numPage.Value = 1;
            _numPage.ValueChanged += new EventHandler(NumPage_ValueChanged);
            tb.Controls.Add(_numPage);
            Label lp2 = new Label(); lp2.Text = "页 / 共"; lp2.Location = new Point(90, 10); lp2.Size = new Size(50, 20);
            tb.Controls.Add(lp2);
            _lblTotalPages = new Label();
            _lblTotalPages.Text = "1"; _lblTotalPages.Location = new Point(140, 10);
            _lblTotalPages.Size = new Size(30, 20);
            _lblTotalPages.Font = new Font("宋体", 9f, FontStyle.Bold);
            tb.Controls.Add(_lblTotalPages);
            Label lp3 = new Label(); lp3.Text = "页"; lp3.Location = new Point(170, 10); lp3.Size = new Size(20, 20);
            tb.Controls.Add(lp3);

            _pnlPreview = new Panel();
            _pnlPreview.Dock = DockStyle.Fill;
            _pnlPreview.AutoScroll = true;
            _pnlPreview.BackColor = Color.DarkGray;
            _pnlPreview.Paint += new PaintEventHandler(PnlPreview_Paint);
            _pnlPreview.Scroll += _pnlPreview_Scroll;

            _pnlPreviewPictureBox = new PictureBox();
            _pnlPreviewPictureBox.Dock = DockStyle.Fill;
            _pnlPreview.Controls.Add(_pnlPreviewPictureBox);

            right.Controls.Add(_pnlPreview);
        }

        private void _pnlPreview_Scroll(object sender, ScrollEventArgs e)
        {
            //_pnlPreview.Invalidate();
        }

        private void AddLabel(Control p, string text, int x, int y, int w)
        {
            Label l = new Label();
            l.Text = text; l.Location = new Point(x, y);
            l.Size = new Size(w, 20);
            l.TextAlign = ContentAlignment.MiddleLeft;
            p.Controls.Add(l);
        }

        private void BtnGridColor_Click(object sender, EventArgs e)
        {
            _colorDialog.Color = _config.GridColor;
            if (_colorDialog.ShowDialog() == DialogResult.OK)
            {
                _config.GridColor = _colorDialog.Color;
                _btnGridColor.BackColor = _colorDialog.Color;
            }
        }
        private void BtnPinyinColor_Click(object sender, EventArgs e)
        {
            _colorDialog.Color = _config.PinyinColor;
            if (_colorDialog.ShowDialog() == DialogResult.OK)
            {
                _config.PinyinColor = _colorDialog.Color;
                _btnPinyinColor.BackColor = _colorDialog.Color;
            }
        }
        private void BtnSampleColor_Click(object sender, EventArgs e)
        {
            _colorDialog.Color = _config.SampleColor;
            if (_colorDialog.ShowDialog() == DialogResult.OK)
            {
                _config.SampleColor = _colorDialog.Color;
                _btnSampleColor.BackColor = _colorDialog.Color;
            }
        }
        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            ApplySettings();
            _currentPreviewPage = 0;
            UpdatePreview();
        }
        private void NumPage_ValueChanged(object sender, EventArgs e)
        {
            _currentPreviewPage = (int)_numPage.Value - 1; 
            UpdatePreview();
        }
        private void PnlPreview_Paint(object sender, PaintEventArgs e) {
            //DrawPreview(e.Graphics); 
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            ApplySettings();
            PrintDialog dlg = new PrintDialog();
            dlg.Document = _printDoc;
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try { _printDoc.Print(); }
                catch (Exception ex) { MessageBox.Show("打印失败：" + ex.Message); }
            }
        }
        private void BtnPrintPreview_Click(object sender, EventArgs e)
        {
            ApplySettings();
            try { _printPreviewDlg.ShowDialog(); }
            catch (Exception ex) { MessageBox.Show("打印预览失败：" + ex.Message); }
        }
        private void BtnExportImage_Click(object sender, EventArgs e)
        {
            ApplySettings();
            if (_saveFileDlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _renderer.ExportToImage(_saveFileDlg.FileName, _currentPreviewPage, 150f);
                    MessageBox.Show("已导出：\n" + _saveFileDlg.FileName);
                }
                catch (Exception ex) { MessageBox.Show("导出失败：" + ex.Message); }
            }
        }
        private void PrintDoc_BeginPrint(object sender, PrintEventArgs e) { _printPageIndex = 0; }
        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            _printDoc.DefaultPageSettings.Landscape = (_config.Orientation == PageOrientation.Landscape);
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Millimeter;
            _renderer.DrawPage(g, _printPageIndex, true);
            _printPageIndex++;
            e.HasMorePages = (_printPageIndex < _renderer.GetTotalPages());
        }

        private void ApplySettings()
        {
            _config.InputText = _txtInput.Text;
            _config.CellSize = (float)_numCellSize.Value;
            switch (_cboGridType.SelectedIndex)
            {
                case 0: _config.GridType = GridType.TianZi; break;
                case 1: _config.GridType = GridType.MiZi; break;
                case 2: _config.GridType = GridType.HuiGong; break;
                case 3: _config.GridType = GridType.FangGe; break;
            }
            _config.CharMode = (_cboCharMode.SelectedIndex == 0) ? CharMode.TraceWithBlank : CharMode.AllTrace;
            _config.WordMode = _chkWordMode.Checked;
            //_config.StrokeDemoCount = (int)_numStrokeDemo.Value;
            _config.StrokeDemoCount = 0;
            _config.TraceRepeatCount = (int)_numTraceRepeat.Value;
            _config.PracticeRows = (int)_numPracticeRows.Value;
            _config.ShowPinyin = _chkShowPinyin.Checked;
            _config.ShowSample = _chkShowSample.Checked;
            _config.Orientation = (_cboOrientation.SelectedIndex == 0) ? PageOrientation.Portrait : PageOrientation.Landscape;
            _config.PaperType = (_cboPaperType.SelectedIndex == 0) ? PaperSizeType.A4 : PaperSizeType.Letter;
            _config.TraceGrayLevel = (int)_numTraceGray.Value;
            _config.FontName = (_cboFontName.SelectedItem != null) ? _cboFontName.SelectedItem.ToString() : "楷体";
            _config.InfoFields = _txtInfoFields.Text;
            _config.PageHeader = _txtPageHeader.Text;
            _renderer = new TianZiGridRenderer(_config);
        }

        private void UpdatePreview()
        {
            Graphics g = this._pnlPreview.CreateGraphics();
            this.bitmapTemp = null;
            DrawPreview(g);
            int total = _renderer.GetTotalPages();
            _lblTotalPages.Text = total.ToString();
            _numPage.Maximum = total;
            if (_currentPreviewPage >= total) _currentPreviewPage = total - 1;
            if (_currentPreviewPage < 0) _currentPreviewPage = 0;
            _numPage.Value = _currentPreviewPage + 1;
            _pnlPreview.Invalidate();
        }
        public Bitmap bitmapTemp = null;
        private void DrawPreview(Graphics g)
        {
            try
            {
                PageLayout l = _renderer.CalculateLayout();
                float pw = _pnlPreview.ClientSize.Width - 30;
                float ph = _pnlPreview.ClientSize.Height - 30;
                if (pw < 100) pw = 100;
                if (ph < 100) ph = 100;
                float sx = pw / l.PageWidth, sy = ph / l.PageHeight;
                float scale = sx < sy ? sx : sy;
                if (scale < 0.1f) scale = 0.1f;
                scale = 5;
                int pW = (int)(l.PageWidth * scale), pH = (int)(l.PageHeight * scale);
                _pnlPreview.AutoScrollMinSize = new Size(pW + 20, pH + 20);
                int ox = (_pnlPreview.ClientSize.Width - pW) / 2;
                if (ox < 0) ox = 10;
                int oy = 10;
                ox -= _pnlPreview.AutoScrollPosition.X;
                oy -= _pnlPreview.AutoScrollPosition.Y;

                if (bitmapTemp == null)
                {
                    bitmapTemp = new Bitmap(pW, pH);
                    {
                        float dpi = scale * 25.4f;
                        if (dpi < 10f) dpi = 10f;
                        bitmapTemp.SetResolution(dpi, dpi);
                        using (Graphics mg = Graphics.FromImage(bitmapTemp))
                        {
                            mg.PageUnit = GraphicsUnit.Millimeter;
                            _renderer.DrawPage(mg, _currentPreviewPage, false);
                        }
                    }
                    _pnlPreviewPictureBox.Image = bitmapTemp;
                    //using (SolidBrush sb = new SolidBrush(Color.FromArgb(80, 0, 0, 0)))
                    //    g.FillRectangle(sb, ox + 3, oy + 3, pW, pH);
                    //g.DrawImage(bitmapTemp, ox, oy, pW, pH);
                    //using (Pen bp = new Pen(Color.Black, 1f))
                    //    g.DrawRectangle(bp, ox, oy, pW, pH);
                }
            }
            catch (Exception ex)
            { 
            }
        
        }
    }

    #endregion
     
 
}
