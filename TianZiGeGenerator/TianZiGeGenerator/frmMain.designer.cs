using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
namespace TianZiGeGenerator
{
    partial class TianZiGeGeneratorForm2
    {
        private SplitContainer _mainSplit;
        private TextBox _txtInput;
        private NumericUpDown _numCellSize;
        private ComboBox _cboGridType, _cboCharMode, _cboOrientation, _cboPaperType, _cboFontName;
        private Button _btnGridColor, _btnPinyinColor, _btnSampleColor;
        private NumericUpDown _numTraceGray, _numTraceRepeat, _numPracticeRows;
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

        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TianZiGeGeneratorForm2));
            this._mainSplit = new System.Windows.Forms.SplitContainer();
            this.pnlLeftContainer = new System.Windows.Forms.Panel();
            this.lblTitleSetting = new System.Windows.Forms.Label();
            this.lblInputText = new System.Windows.Forms.Label();
            this._txtInput = new System.Windows.Forms.TextBox();
            this.lblWordMode = new System.Windows.Forms.Label();
            this._chkWordMode = new System.Windows.Forms.CheckBox();
            this.lblCellSize = new System.Windows.Forms.Label();
            this._numCellSize = new System.Windows.Forms.NumericUpDown();
            this.lblGridType = new System.Windows.Forms.Label();
            this._cboGridType = new System.Windows.Forms.ComboBox();
            this.lblCharMode = new System.Windows.Forms.Label();
            this._cboCharMode = new System.Windows.Forms.ComboBox();
            this.lblTraceRepeat = new System.Windows.Forms.Label();
            this._numTraceRepeat = new System.Windows.Forms.NumericUpDown();
            this.lblPracticeRows = new System.Windows.Forms.Label();
            this._numPracticeRows = new System.Windows.Forms.NumericUpDown();
            this.lblShowPinyin = new System.Windows.Forms.Label();
            this._chkShowPinyin = new System.Windows.Forms.CheckBox();
            this.lblShowSample = new System.Windows.Forms.Label();
            this._chkShowSample = new System.Windows.Forms.CheckBox();
            this.lblOrientation = new System.Windows.Forms.Label();
            this._cboOrientation = new System.Windows.Forms.ComboBox();
            this.lblPaperType = new System.Windows.Forms.Label();
            this._cboPaperType = new System.Windows.Forms.ComboBox();
            this.lblGridColor = new System.Windows.Forms.Label();
            this._btnGridColor = new System.Windows.Forms.Button();
            this.lblPinyinColor = new System.Windows.Forms.Label();
            this._btnPinyinColor = new System.Windows.Forms.Button();
            this.lblSampleColor = new System.Windows.Forms.Label();
            this._btnSampleColor = new System.Windows.Forms.Button();
            this.lblTraceGray = new System.Windows.Forms.Label();
            this._numTraceGray = new System.Windows.Forms.NumericUpDown();
            this.lblFontName = new System.Windows.Forms.Label();
            this._cboFontName = new System.Windows.Forms.ComboBox();
            this.lblInfoFields = new System.Windows.Forms.Label();
            this._txtInfoFields = new System.Windows.Forms.TextBox();
            this.lblPageHeader = new System.Windows.Forms.Label();
            this._txtPageHeader = new System.Windows.Forms.TextBox();
            this._btnGenerate = new System.Windows.Forms.Button();
            this.lblSeparator = new System.Windows.Forms.Label();
            this.lblOutputTitle = new System.Windows.Forms.Label();
            this._btnPrint = new System.Windows.Forms.Button();
            this._btnPrintPreview = new System.Windows.Forms.Button();
            this._btnExportImage = new System.Windows.Forms.Button();
            this.pnlRightContainer = new System.Windows.Forms.Panel();
            this.pnlToolBar = new System.Windows.Forms.Panel();
            this.lblPagePrefix = new System.Windows.Forms.Label();
            this._numPage = new System.Windows.Forms.NumericUpDown();
            this.lblPageMiddle = new System.Windows.Forms.Label();
            this._lblTotalPages = new System.Windows.Forms.Label();
            this.lblPageSuffix = new System.Windows.Forms.Label();
            this._pnlPreview = new System.Windows.Forms.Panel();
            this._pnlPreviewPictureBox = new System.Windows.Forms.PictureBox();
            this._colorDialog = new System.Windows.Forms.ColorDialog();
            this._printDoc = new System.Drawing.Printing.PrintDocument();
            this._printPreviewDlg = new System.Windows.Forms.PrintPreviewDialog();
            this._saveFileDlg = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this._mainSplit)).BeginInit();
            this._mainSplit.Panel1.SuspendLayout();
            this._mainSplit.Panel2.SuspendLayout();
            this._mainSplit.SuspendLayout();
            this.pnlLeftContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numCellSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numTraceRepeat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numPracticeRows)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._numTraceGray)).BeginInit();
            this.pnlRightContainer.SuspendLayout();
            this.pnlToolBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numPage)).BeginInit();
            this._pnlPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._pnlPreviewPictureBox)).BeginInit();
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
            // _mainSplit.Panel1
            // 
            this._mainSplit.Panel1.Controls.Add(this.pnlLeftContainer);
            // 
            // _mainSplit.Panel2
            // 
            this._mainSplit.Panel2.Controls.Add(this.pnlRightContainer);
            this._mainSplit.Panel2.SizeChanged += new System.EventHandler(this._mainSplit_Panel2_SizeChanged);
            this._mainSplit.Size = new System.Drawing.Size(1132, 733);
            this._mainSplit.SplitterDistance = 269;
            this._mainSplit.TabIndex = 0;
            // 
            // pnlLeftContainer
            // 
            this.pnlLeftContainer.AutoScroll = true;
            this.pnlLeftContainer.Controls.Add(this.lblTitleSetting);
            this.pnlLeftContainer.Controls.Add(this.lblInputText);
            this.pnlLeftContainer.Controls.Add(this._txtInput);
            this.pnlLeftContainer.Controls.Add(this.lblWordMode);
            this.pnlLeftContainer.Controls.Add(this._chkWordMode);
            this.pnlLeftContainer.Controls.Add(this.lblCellSize);
            this.pnlLeftContainer.Controls.Add(this._numCellSize);
            this.pnlLeftContainer.Controls.Add(this.lblGridType);
            this.pnlLeftContainer.Controls.Add(this._cboGridType);
            this.pnlLeftContainer.Controls.Add(this.lblCharMode);
            this.pnlLeftContainer.Controls.Add(this._cboCharMode);
            this.pnlLeftContainer.Controls.Add(this.lblTraceRepeat);
            this.pnlLeftContainer.Controls.Add(this._numTraceRepeat);
            this.pnlLeftContainer.Controls.Add(this.lblPracticeRows);
            this.pnlLeftContainer.Controls.Add(this._numPracticeRows);
            this.pnlLeftContainer.Controls.Add(this.lblShowPinyin);
            this.pnlLeftContainer.Controls.Add(this._chkShowPinyin);
            this.pnlLeftContainer.Controls.Add(this.lblShowSample);
            this.pnlLeftContainer.Controls.Add(this._chkShowSample);
            this.pnlLeftContainer.Controls.Add(this.lblOrientation);
            this.pnlLeftContainer.Controls.Add(this._cboOrientation);
            this.pnlLeftContainer.Controls.Add(this.lblPaperType);
            this.pnlLeftContainer.Controls.Add(this._cboPaperType);
            this.pnlLeftContainer.Controls.Add(this.lblGridColor);
            this.pnlLeftContainer.Controls.Add(this._btnGridColor);
            this.pnlLeftContainer.Controls.Add(this.lblPinyinColor);
            this.pnlLeftContainer.Controls.Add(this._btnPinyinColor);
            this.pnlLeftContainer.Controls.Add(this.lblSampleColor);
            this.pnlLeftContainer.Controls.Add(this._btnSampleColor);
            this.pnlLeftContainer.Controls.Add(this.lblTraceGray);
            this.pnlLeftContainer.Controls.Add(this._numTraceGray);
            this.pnlLeftContainer.Controls.Add(this.lblFontName);
            this.pnlLeftContainer.Controls.Add(this._cboFontName);
            this.pnlLeftContainer.Controls.Add(this.lblInfoFields);
            this.pnlLeftContainer.Controls.Add(this._txtInfoFields);
            this.pnlLeftContainer.Controls.Add(this.lblPageHeader);
            this.pnlLeftContainer.Controls.Add(this._txtPageHeader);
            this.pnlLeftContainer.Controls.Add(this._btnGenerate);
            this.pnlLeftContainer.Controls.Add(this.lblSeparator);
            this.pnlLeftContainer.Controls.Add(this.lblOutputTitle);
            this.pnlLeftContainer.Controls.Add(this._btnPrint);
            this.pnlLeftContainer.Controls.Add(this._btnPrintPreview);
            this.pnlLeftContainer.Controls.Add(this._btnExportImage);
            this.pnlLeftContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeftContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlLeftContainer.Name = "pnlLeftContainer";
            this.pnlLeftContainer.Padding = new System.Windows.Forms.Padding(10);
            this.pnlLeftContainer.Size = new System.Drawing.Size(267, 731);
            this.pnlLeftContainer.TabIndex = 0;
            // 
            // lblTitleSetting
            // 
            this.lblTitleSetting.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitleSetting.Location = new System.Drawing.Point(10, 10);
            this.lblTitleSetting.Name = "lblTitleSetting";
            this.lblTitleSetting.Size = new System.Drawing.Size(200, 25);
            this.lblTitleSetting.TabIndex = 0;
            this.lblTitleSetting.Text = "字帖参数设置";
            // 
            // lblInputText
            // 
            this.lblInputText.Location = new System.Drawing.Point(10, 42);
            this.lblInputText.Name = "lblInputText";
            this.lblInputText.Size = new System.Drawing.Size(95, 20);
            this.lblInputText.TabIndex = 1;
            this.lblInputText.Text = "汉字内容：";
            // 
            // _txtInput
            // 
            this._txtInput.Location = new System.Drawing.Point(10, 62);
            this._txtInput.Multiline = true;
            this._txtInput.Name = "_txtInput";
            this._txtInput.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._txtInput.Size = new System.Drawing.Size(210, 61);
            this._txtInput.TabIndex = 2;
            // 
            // lblWordMode
            // 
            this.lblWordMode.Location = new System.Drawing.Point(10, 132);
            this.lblWordMode.Name = "lblWordMode";
            this.lblWordMode.Size = new System.Drawing.Size(95, 20);
            this.lblWordMode.TabIndex = 3;
            this.lblWordMode.Text = "词组模式(空格分词)：";
            // 
            // _chkWordMode
            // 
            this._chkWordMode.Location = new System.Drawing.Point(105, 129);
            this._chkWordMode.Name = "_chkWordMode";
            this._chkWordMode.Size = new System.Drawing.Size(100, 20);
            this._chkWordMode.TabIndex = 4;
            // 
            // lblCellSize
            // 
            this.lblCellSize.Location = new System.Drawing.Point(10, 160);
            this.lblCellSize.Name = "lblCellSize";
            this.lblCellSize.Size = new System.Drawing.Size(95, 20);
            this.lblCellSize.TabIndex = 5;
            this.lblCellSize.Text = "格子大小(mm)：";
            // 
            // _numCellSize
            // 
            this._numCellSize.DecimalPlaces = 1;
            this._numCellSize.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this._numCellSize.Location = new System.Drawing.Point(105, 157);
            this._numCellSize.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this._numCellSize.Minimum = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this._numCellSize.Name = "_numCellSize";
            this._numCellSize.Size = new System.Drawing.Size(70, 25);
            this._numCellSize.TabIndex = 6;
            this._numCellSize.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            // 
            // lblGridType
            // 
            this.lblGridType.Location = new System.Drawing.Point(10, 188);
            this.lblGridType.Name = "lblGridType";
            this.lblGridType.Size = new System.Drawing.Size(95, 20);
            this.lblGridType.TabIndex = 7;
            this.lblGridType.Text = "格子类型：";
            // 
            // _cboGridType
            // 
            this._cboGridType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cboGridType.Items.AddRange(new object[] {
            "田字格",
            "米字格",
            "回宫格",
            "方格"});
            this._cboGridType.Location = new System.Drawing.Point(105, 185);
            this._cboGridType.Name = "_cboGridType";
            this._cboGridType.Size = new System.Drawing.Size(115, 23);
            this._cboGridType.TabIndex = 8;
            // 
            // lblCharMode
            // 
            this.lblCharMode.Location = new System.Drawing.Point(10, 216);
            this.lblCharMode.Name = "lblCharMode";
            this.lblCharMode.Size = new System.Drawing.Size(95, 20);
            this.lblCharMode.TabIndex = 9;
            this.lblCharMode.Text = "排列模式：";
            // 
            // _cboCharMode
            // 
            this._cboCharMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cboCharMode.Items.AddRange(new object[] {
            "描红+空白行交替",
            "全部描红"});
            this._cboCharMode.Location = new System.Drawing.Point(105, 213);
            this._cboCharMode.Name = "_cboCharMode";
            this._cboCharMode.Size = new System.Drawing.Size(115, 23);
            this._cboCharMode.TabIndex = 10;
            // 
            // lblTraceRepeat
            // 
            this.lblTraceRepeat.Location = new System.Drawing.Point(10, 244);
            this.lblTraceRepeat.Name = "lblTraceRepeat";
            this.lblTraceRepeat.Size = new System.Drawing.Size(95, 20);
            this.lblTraceRepeat.TabIndex = 11;
            this.lblTraceRepeat.Text = "每字描红次数：";
            // 
            // _numTraceRepeat
            // 
            this._numTraceRepeat.Location = new System.Drawing.Point(105, 241);
            this._numTraceRepeat.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this._numTraceRepeat.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numTraceRepeat.Name = "_numTraceRepeat";
            this._numTraceRepeat.Size = new System.Drawing.Size(70, 25);
            this._numTraceRepeat.TabIndex = 12;
            this._numTraceRepeat.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblPracticeRows
            // 
            this.lblPracticeRows.Location = new System.Drawing.Point(10, 272);
            this.lblPracticeRows.Name = "lblPracticeRows";
            this.lblPracticeRows.Size = new System.Drawing.Size(95, 20);
            this.lblPracticeRows.TabIndex = 13;
            this.lblPracticeRows.Text = "每字练习行数：";
            // 
            // _numPracticeRows
            // 
            this._numPracticeRows.Location = new System.Drawing.Point(105, 269);
            this._numPracticeRows.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._numPracticeRows.Name = "_numPracticeRows";
            this._numPracticeRows.Size = new System.Drawing.Size(70, 25);
            this._numPracticeRows.TabIndex = 14;
            // 
            // lblShowPinyin
            // 
            this.lblShowPinyin.Location = new System.Drawing.Point(10, 300);
            this.lblShowPinyin.Name = "lblShowPinyin";
            this.lblShowPinyin.Size = new System.Drawing.Size(95, 20);
            this.lblShowPinyin.TabIndex = 15;
            this.lblShowPinyin.Text = "显示拼音：";
            // 
            // _chkShowPinyin
            // 
            this._chkShowPinyin.Location = new System.Drawing.Point(105, 297);
            this._chkShowPinyin.Name = "_chkShowPinyin";
            this._chkShowPinyin.Size = new System.Drawing.Size(100, 20);
            this._chkShowPinyin.TabIndex = 16;
            // 
            // lblShowSample
            // 
            this.lblShowSample.Location = new System.Drawing.Point(10, 328);
            this.lblShowSample.Name = "lblShowSample";
            this.lblShowSample.Size = new System.Drawing.Size(95, 20);
            this.lblShowSample.TabIndex = 17;
            this.lblShowSample.Text = "显示样例字列：";
            // 
            // _chkShowSample
            // 
            this._chkShowSample.Location = new System.Drawing.Point(105, 325);
            this._chkShowSample.Name = "_chkShowSample";
            this._chkShowSample.Size = new System.Drawing.Size(100, 20);
            this._chkShowSample.TabIndex = 18;
            // 
            // lblOrientation
            // 
            this.lblOrientation.Location = new System.Drawing.Point(10, 356);
            this.lblOrientation.Name = "lblOrientation";
            this.lblOrientation.Size = new System.Drawing.Size(95, 20);
            this.lblOrientation.TabIndex = 19;
            this.lblOrientation.Text = "页面方向：";
            // 
            // _cboOrientation
            // 
            this._cboOrientation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cboOrientation.Items.AddRange(new object[] {
            "竖版",
            "横版"});
            this._cboOrientation.Location = new System.Drawing.Point(105, 353);
            this._cboOrientation.Name = "_cboOrientation";
            this._cboOrientation.Size = new System.Drawing.Size(115, 23);
            this._cboOrientation.TabIndex = 20;
            // 
            // lblPaperType
            // 
            this.lblPaperType.Location = new System.Drawing.Point(10, 384);
            this.lblPaperType.Name = "lblPaperType";
            this.lblPaperType.Size = new System.Drawing.Size(95, 20);
            this.lblPaperType.TabIndex = 21;
            this.lblPaperType.Text = "纸张类型：";
            // 
            // _cboPaperType
            // 
            this._cboPaperType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cboPaperType.Items.AddRange(new object[] {
            "A4",
            "Letter"});
            this._cboPaperType.Location = new System.Drawing.Point(105, 381);
            this._cboPaperType.Name = "_cboPaperType";
            this._cboPaperType.Size = new System.Drawing.Size(115, 23);
            this._cboPaperType.TabIndex = 22;
            // 
            // lblGridColor
            // 
            this.lblGridColor.Location = new System.Drawing.Point(10, 412);
            this.lblGridColor.Name = "lblGridColor";
            this.lblGridColor.Size = new System.Drawing.Size(95, 20);
            this.lblGridColor.TabIndex = 23;
            this.lblGridColor.Text = "田格颜色：";
            // 
            // _btnGridColor
            // 
            this._btnGridColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnGridColor.Location = new System.Drawing.Point(105, 409);
            this._btnGridColor.Name = "_btnGridColor";
            this._btnGridColor.Size = new System.Drawing.Size(55, 22);
            this._btnGridColor.TabIndex = 24;
            this._btnGridColor.Text = "选择";
            this._btnGridColor.Click += new System.EventHandler(this.BtnGridColor_Click);
            // 
            // lblPinyinColor
            // 
            this.lblPinyinColor.Location = new System.Drawing.Point(10, 440);
            this.lblPinyinColor.Name = "lblPinyinColor";
            this.lblPinyinColor.Size = new System.Drawing.Size(95, 20);
            this.lblPinyinColor.TabIndex = 25;
            this.lblPinyinColor.Text = "拼音颜色：";
            // 
            // _btnPinyinColor
            // 
            this._btnPinyinColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnPinyinColor.Location = new System.Drawing.Point(105, 437);
            this._btnPinyinColor.Name = "_btnPinyinColor";
            this._btnPinyinColor.Size = new System.Drawing.Size(55, 22);
            this._btnPinyinColor.TabIndex = 26;
            this._btnPinyinColor.Text = "选择";
            this._btnPinyinColor.Click += new System.EventHandler(this.BtnPinyinColor_Click);
            // 
            // lblSampleColor
            // 
            this.lblSampleColor.Location = new System.Drawing.Point(10, 468);
            this.lblSampleColor.Name = "lblSampleColor";
            this.lblSampleColor.Size = new System.Drawing.Size(95, 20);
            this.lblSampleColor.TabIndex = 27;
            this.lblSampleColor.Text = "样例字颜色：";
            // 
            // _btnSampleColor
            // 
            this._btnSampleColor.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnSampleColor.Location = new System.Drawing.Point(105, 465);
            this._btnSampleColor.Name = "_btnSampleColor";
            this._btnSampleColor.Size = new System.Drawing.Size(55, 22);
            this._btnSampleColor.TabIndex = 28;
            this._btnSampleColor.Text = "选择";
            this._btnSampleColor.Click += new System.EventHandler(this.BtnSampleColor_Click);
            // 
            // lblTraceGray
            // 
            this.lblTraceGray.Location = new System.Drawing.Point(10, 496);
            this.lblTraceGray.Name = "lblTraceGray";
            this.lblTraceGray.Size = new System.Drawing.Size(95, 20);
            this.lblTraceGray.TabIndex = 29;
            this.lblTraceGray.Text = "描红深浅(50-255)：";
            // 
            // _numTraceGray
            // 
            this._numTraceGray.Location = new System.Drawing.Point(105, 493);
            this._numTraceGray.Maximum = new decimal(new int[] {
            255,
            0,
            0,
            0});
            this._numTraceGray.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this._numTraceGray.Name = "_numTraceGray";
            this._numTraceGray.Size = new System.Drawing.Size(70, 25);
            this._numTraceGray.TabIndex = 30;
            this._numTraceGray.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // lblFontName
            // 
            this.lblFontName.Location = new System.Drawing.Point(10, 524);
            this.lblFontName.Name = "lblFontName";
            this.lblFontName.Size = new System.Drawing.Size(95, 20);
            this.lblFontName.TabIndex = 31;
            this.lblFontName.Text = "字体：";
            // 
            // _cboFontName
            // 
            this._cboFontName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._cboFontName.Items.AddRange(new object[] {
            "楷体",
            "宋体",
            "黑体",
            "仿宋",
            "微软雅黑"});
            this._cboFontName.Location = new System.Drawing.Point(105, 521);
            this._cboFontName.Name = "_cboFontName";
            this._cboFontName.Size = new System.Drawing.Size(115, 23);
            this._cboFontName.TabIndex = 32;
            // 
            // lblInfoFields
            // 
            this.lblInfoFields.Location = new System.Drawing.Point(10, 552);
            this.lblInfoFields.Name = "lblInfoFields";
            this.lblInfoFields.Size = new System.Drawing.Size(95, 20);
            this.lblInfoFields.TabIndex = 33;
            this.lblInfoFields.Text = "信息栏字段：";
            // 
            // _txtInfoFields
            // 
            this._txtInfoFields.Location = new System.Drawing.Point(105, 549);
            this._txtInfoFields.Name = "_txtInfoFields";
            this._txtInfoFields.Size = new System.Drawing.Size(115, 25);
            this._txtInfoFields.TabIndex = 34;
            // 
            // lblPageHeader
            // 
            this.lblPageHeader.Location = new System.Drawing.Point(10, 580);
            this.lblPageHeader.Name = "lblPageHeader";
            this.lblPageHeader.Size = new System.Drawing.Size(95, 20);
            this.lblPageHeader.TabIndex = 35;
            this.lblPageHeader.Text = "页头文字：";
            // 
            // _txtPageHeader
            // 
            this._txtPageHeader.Location = new System.Drawing.Point(105, 577);
            this._txtPageHeader.Name = "_txtPageHeader";
            this._txtPageHeader.Size = new System.Drawing.Size(115, 25);
            this._txtPageHeader.TabIndex = 36;
            // 
            // _btnGenerate
            // 
            this._btnGenerate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(200)))), ((int)(((byte)(80)))));
            this._btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this._btnGenerate.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this._btnGenerate.Location = new System.Drawing.Point(10, 608);
            this._btnGenerate.Name = "_btnGenerate";
            this._btnGenerate.Size = new System.Drawing.Size(210, 32);
            this._btnGenerate.TabIndex = 37;
            this._btnGenerate.Text = "生成预览";
            this._btnGenerate.UseVisualStyleBackColor = false;
            this._btnGenerate.Click += new System.EventHandler(this.BtnGenerate_Click);
            // 
            // lblSeparator
            // 
            this.lblSeparator.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSeparator.Location = new System.Drawing.Point(10, 648);
            this.lblSeparator.Name = "lblSeparator";
            this.lblSeparator.Size = new System.Drawing.Size(210, 2);
            this.lblSeparator.TabIndex = 38;
            // 
            // lblOutputTitle
            // 
            this.lblOutputTitle.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.lblOutputTitle.Location = new System.Drawing.Point(10, 660);
            this.lblOutputTitle.Name = "lblOutputTitle";
            this.lblOutputTitle.Size = new System.Drawing.Size(200, 22);
            this.lblOutputTitle.TabIndex = 39;
            this.lblOutputTitle.Text = "输出操作";
            // 
            // _btnPrint
            // 
            this._btnPrint.Location = new System.Drawing.Point(10, 686);
            this._btnPrint.Name = "_btnPrint";
            this._btnPrint.Size = new System.Drawing.Size(95, 30);
            this._btnPrint.TabIndex = 40;
            this._btnPrint.Text = "打印";
            this._btnPrint.Click += new System.EventHandler(this.BtnPrint_Click);
            // 
            // _btnPrintPreview
            // 
            this._btnPrintPreview.Location = new System.Drawing.Point(115, 686);
            this._btnPrintPreview.Name = "_btnPrintPreview";
            this._btnPrintPreview.Size = new System.Drawing.Size(95, 30);
            this._btnPrintPreview.TabIndex = 41;
            this._btnPrintPreview.Text = "打印预览";
            this._btnPrintPreview.Click += new System.EventHandler(this.BtnPrintPreview_Click);
            // 
            // _btnExportImage
            // 
            this._btnExportImage.Location = new System.Drawing.Point(10, 724);
            this._btnExportImage.Name = "_btnExportImage";
            this._btnExportImage.Size = new System.Drawing.Size(200, 30);
            this._btnExportImage.TabIndex = 42;
            this._btnExportImage.Text = "导出为图片";
            this._btnExportImage.Click += new System.EventHandler(this.BtnExportImage_Click);
            // 
            // pnlRightContainer
            // 
            this.pnlRightContainer.BackColor = System.Drawing.Color.LightGray;
            this.pnlRightContainer.Controls.Add(this.pnlToolBar);
            this.pnlRightContainer.Controls.Add(this._pnlPreview);
            this.pnlRightContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRightContainer.Location = new System.Drawing.Point(0, 0);
            this.pnlRightContainer.Name = "pnlRightContainer";
            this.pnlRightContainer.Padding = new System.Windows.Forms.Padding(10);
            this.pnlRightContainer.Size = new System.Drawing.Size(857, 731);
            this.pnlRightContainer.TabIndex = 0;
            // 
            // pnlToolBar
            // 
            this.pnlToolBar.BackColor = System.Drawing.Color.Gainsboro;
            this.pnlToolBar.Controls.Add(this.lblPagePrefix);
            this.pnlToolBar.Controls.Add(this._numPage);
            this.pnlToolBar.Controls.Add(this.lblPageMiddle);
            this.pnlToolBar.Controls.Add(this._lblTotalPages);
            this.pnlToolBar.Controls.Add(this.lblPageSuffix);
            this.pnlToolBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolBar.Location = new System.Drawing.Point(10, 10);
            this.pnlToolBar.Name = "pnlToolBar";
            this.pnlToolBar.Size = new System.Drawing.Size(837, 36);
            this.pnlToolBar.TabIndex = 0;
            // 
            // lblPagePrefix
            // 
            this.lblPagePrefix.Location = new System.Drawing.Point(10, 10);
            this.lblPagePrefix.Name = "lblPagePrefix";
            this.lblPagePrefix.Size = new System.Drawing.Size(20, 20);
            this.lblPagePrefix.TabIndex = 0;
            this.lblPagePrefix.Text = "第";
            // 
            // _numPage
            // 
            this._numPage.Location = new System.Drawing.Point(30, 7);
            this._numPage.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numPage.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numPage.Name = "_numPage";
            this._numPage.Size = new System.Drawing.Size(55, 25);
            this._numPage.TabIndex = 1;
            this._numPage.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numPage.ValueChanged += new System.EventHandler(this.NumPage_ValueChanged);
            // 
            // lblPageMiddle
            // 
            this.lblPageMiddle.Location = new System.Drawing.Point(90, 10);
            this.lblPageMiddle.Name = "lblPageMiddle";
            this.lblPageMiddle.Size = new System.Drawing.Size(50, 20);
            this.lblPageMiddle.TabIndex = 2;
            this.lblPageMiddle.Text = "页 / 共";
            // 
            // _lblTotalPages
            // 
            this._lblTotalPages.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold);
            this._lblTotalPages.Location = new System.Drawing.Point(140, 10);
            this._lblTotalPages.Name = "_lblTotalPages";
            this._lblTotalPages.Size = new System.Drawing.Size(30, 20);
            this._lblTotalPages.TabIndex = 3;
            this._lblTotalPages.Text = "1";
            // 
            // lblPageSuffix
            // 
            this.lblPageSuffix.Location = new System.Drawing.Point(170, 10);
            this.lblPageSuffix.Name = "lblPageSuffix";
            this.lblPageSuffix.Size = new System.Drawing.Size(20, 20);
            this.lblPageSuffix.TabIndex = 4;
            this.lblPageSuffix.Text = "页";
            // 
            // _pnlPreview
            // 
            this._pnlPreview.AutoScroll = true;
            this._pnlPreview.BackColor = System.Drawing.Color.DarkGray;
            this._pnlPreview.Controls.Add(this._pnlPreviewPictureBox);
            this._pnlPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pnlPreview.Location = new System.Drawing.Point(10, 10);
            this._pnlPreview.Name = "_pnlPreview";
            this._pnlPreview.Size = new System.Drawing.Size(837, 711);
            this._pnlPreview.TabIndex = 1;
            this._pnlPreview.Scroll += new System.Windows.Forms.ScrollEventHandler(this._pnlPreview_Scroll);
            this._pnlPreview.Paint += new System.Windows.Forms.PaintEventHandler(this.PnlPreview_Paint);
            // 
            // _pnlPreviewPictureBox
            // 
            this._pnlPreviewPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this._pnlPreviewPictureBox.Location = new System.Drawing.Point(0, 0);
            this._pnlPreviewPictureBox.Name = "_pnlPreviewPictureBox";
            this._pnlPreviewPictureBox.Size = new System.Drawing.Size(837, 711);
            this._pnlPreviewPictureBox.TabIndex = 0;
            this._pnlPreviewPictureBox.TabStop = false;
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
            // TianZiGeGeneratorForm2
            // 
            this.ClientSize = new System.Drawing.Size(1132, 733);
            this.Controls.Add(this._mainSplit);
            this.Font = new System.Drawing.Font("宋体", 9F);
            this.MinimumSize = new System.Drawing.Size(950, 650);
            this.Name = "TianZiGeGeneratorForm2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "田字格字帖生成器（笔顺+拼音版）";
            this._mainSplit.Panel1.ResumeLayout(false);
            this._mainSplit.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._mainSplit)).EndInit();
            this._mainSplit.ResumeLayout(false);
            this.pnlLeftContainer.ResumeLayout(false);
            this.pnlLeftContainer.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numCellSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numTraceRepeat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numPracticeRows)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._numTraceGray)).EndInit();
            this.pnlRightContainer.ResumeLayout(false);
            this.pnlToolBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._numPage)).EndInit();
            this._pnlPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this._pnlPreviewPictureBox)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private Panel pnlLeftContainer;
        private Label lblTitleSetting;
        private Label lblInputText;
        private Label lblWordMode;
        private Label lblCellSize;
        private Label lblGridType;
        private Label lblCharMode;
        private Label lblTraceRepeat;
        private Label lblPracticeRows;
        private Label lblShowPinyin;
        private Label lblShowSample;
        private Label lblOrientation;
        private Label lblPaperType;
        private Label lblGridColor;
        private Label lblPinyinColor;
        private Label lblSampleColor;
        private Label lblTraceGray;
        private Label lblFontName;
        private Label lblInfoFields;
        private Label lblPageHeader;
        private Label lblSeparator;
        private Label lblOutputTitle;
        private Panel pnlRightContainer;
        private Panel pnlToolBar;
        private Label lblPagePrefix;
        private Label lblPageMiddle;
        private Label lblPageSuffix;
    }
}