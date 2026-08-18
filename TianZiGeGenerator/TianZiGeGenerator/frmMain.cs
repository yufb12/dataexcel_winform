using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace TianZiGeGenerator
{
    #region 主窗体（业务逻辑）
    /// <summary>主窗体</summary>
    public partial class TianZiGeGeneratorForm2 : Form
    {
        private WorksheetConfig _config;
        private TianZiGridRenderer _renderer;
        private int _currentPreviewPage, _printPageIndex;
        public Bitmap bitmapTemp = null;

        public TianZiGeGeneratorForm2()
        {
            _config = new WorksheetConfig();
            _renderer = new TianZiGridRenderer(_config);
            _currentPreviewPage = 0; _printPageIndex = 0;
            InitializeComponent(); 
            UpdatePreview();
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
        private void PnlPreview_Paint(object sender, PaintEventArgs e)
        {
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
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
    #endregion
}