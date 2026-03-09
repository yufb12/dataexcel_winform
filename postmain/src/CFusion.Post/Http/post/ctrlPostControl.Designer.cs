
namespace CFusion.Http.post
{
    partial class ctrlPostControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlPostControl));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPageSetting = new System.Windows.Forms.TabPage();
            this.txtSetting = new System.Windows.Forms.RichTextBox();
            this.tabPageHeaders = new System.Windows.Forms.TabPage();
            this.dataExcelHeaders = new Feng.Excel.DataExcelControl();
            this.tabPageBody = new System.Windows.Forms.TabPage();
            this.tabControlBodyType = new System.Windows.Forms.TabControl();
            this.tabPageBodyFormData = new System.Windows.Forms.TabPage();
            this.dataExcelControlFormData = new Feng.Excel.DataExcelControl();
            this.tabPageBodyRaw = new System.Windows.Forms.TabPage();
            this.txtRawData = new System.Windows.Forms.RichTextBox();
            this.tabPageBodyFile = new System.Windows.Forms.TabPage();
            this.txtFilePath = new System.Windows.Forms.ComboBox();
            this.txtFileLocationPath = new System.Windows.Forms.ComboBox();
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.radioCheckfile = new System.Windows.Forms.RadioButton();
            this.radioCheckraw = new System.Windows.Forms.RadioButton();
            this.radioCheckformdata = new System.Windows.Forms.RadioButton();
            this.txtContentType = new System.Windows.Forms.ComboBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtScriptCode = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnDebugGenerateCode = new System.Windows.Forms.ToolStripButton();
            this.btnDebugStart = new System.Windows.Forms.ToolStripButton();
            this.btnDebugStep = new System.Windows.Forms.ToolStripButton();
            this.btnDebugStepOut = new System.Windows.Forms.ToolStripButton();
            this.btnDebugStepIn = new System.Windows.Forms.ToolStripButton();
            this.btnDebugPause = new System.Windows.Forms.ToolStripButton();
            this.btnDebugStop = new System.Windows.Forms.ToolStripButton();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.tabPageResponse = new System.Windows.Forms.TabPage();
            this.txtResponse = new System.Windows.Forms.RichTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnResponseSaveAs = new System.Windows.Forms.ToolStripButton();
            this.btnResponseClearAll = new System.Windows.Forms.ToolStripButton();
            this.chkResponseAutoClear = new System.Windows.Forms.ToolStripButton();
            this.txtResponseTime = new System.Windows.Forms.ToolStripButton();
            this.tabPageHistory = new System.Windows.Forms.TabPage();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.treeViewHistory = new System.Windows.Forms.TreeView();
            this.txtHistoryContents = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnPaste = new System.Windows.Forms.PictureBox();
            this.btnRename = new System.Windows.Forms.LinkLabel();
            this.btnImport = new System.Windows.Forms.LinkLabel();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtLocationPath = new CFusion.Http.post.TextBoxEx();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.txtTimeout = new System.Windows.Forms.ComboBox();
            this.txtMethod = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPageSetting.SuspendLayout();
            this.tabPageHeaders.SuspendLayout();
            this.tabPageBody.SuspendLayout();
            this.tabControlBodyType.SuspendLayout();
            this.tabPageBodyFormData.SuspendLayout();
            this.tabPageBodyRaw.SuspendLayout();
            this.tabPageBodyFile.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.tabPageResponse.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tabPageHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPaste)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Panel1Collapsed = true;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2.Controls.Add(this.panel2);
            this.splitContainer1.Size = new System.Drawing.Size(997, 626);
            this.splitContainer1.SplitterDistance = 206;
            this.splitContainer1.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 115);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.tabControl1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.tabControl2);
            this.splitContainer2.Size = new System.Drawing.Size(997, 511);
            this.splitContainer2.SplitterDistance = 263;
            this.splitContainer2.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPageSetting);
            this.tabControl1.Controls.Add(this.tabPageHeaders);
            this.tabControl1.Controls.Add(this.tabPageBody);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(997, 263);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPageSetting
            // 
            this.tabPageSetting.Controls.Add(this.txtSetting);
            this.tabPageSetting.Location = new System.Drawing.Point(4, 25);
            this.tabPageSetting.Name = "tabPageSetting";
            this.tabPageSetting.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageSetting.Size = new System.Drawing.Size(989, 234);
            this.tabPageSetting.TabIndex = 5;
            this.tabPageSetting.Text = "Note";
            this.tabPageSetting.UseVisualStyleBackColor = true;
            // 
            // txtSetting
            // 
            this.txtSetting.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtSetting.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSetting.Location = new System.Drawing.Point(3, 3);
            this.txtSetting.Name = "txtSetting";
            this.txtSetting.Size = new System.Drawing.Size(983, 228);
            this.txtSetting.TabIndex = 1;
            this.txtSetting.Text = "Timeout:\"30\" //second\n\nhttp://192.168.1.174:8086/sys/login\n\n{\n    \"username\": \"ad" +
    "min\",  \n    \"password\": \"PhTyf6kX+\"\n}";
            // 
            // tabPageHeaders
            // 
            this.tabPageHeaders.Controls.Add(this.dataExcelHeaders);
            this.tabPageHeaders.Location = new System.Drawing.Point(4, 25);
            this.tabPageHeaders.Name = "tabPageHeaders";
            this.tabPageHeaders.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHeaders.Size = new System.Drawing.Size(989, 234);
            this.tabPageHeaders.TabIndex = 2;
            this.tabPageHeaders.Text = "Headers";
            this.tabPageHeaders.UseVisualStyleBackColor = true;
            // 
            // dataExcelHeaders
            // 
            this.dataExcelHeaders.BackColor = System.Drawing.Color.White;
            this.dataExcelHeaders.BorderColor = System.Drawing.Color.Empty;
            this.dataExcelHeaders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataExcelHeaders.Font = new System.Drawing.Font("Tahoma", 9F);
            this.dataExcelHeaders.Location = new System.Drawing.Point(3, 3);
            this.dataExcelHeaders.Margin = new System.Windows.Forms.Padding(4);
            this.dataExcelHeaders.Name = "dataExcelHeaders";
            this.dataExcelHeaders.Size = new System.Drawing.Size(983, 228);
            this.dataExcelHeaders.TabIndex = 2;
            this.dataExcelHeaders.Text = "dataExcelControl1";
            // 
            // tabPageBody
            // 
            this.tabPageBody.Controls.Add(this.tabControlBodyType);
            this.tabPageBody.Controls.Add(this.panel3);
            this.tabPageBody.Location = new System.Drawing.Point(4, 25);
            this.tabPageBody.Name = "tabPageBody";
            this.tabPageBody.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBody.Size = new System.Drawing.Size(989, 234);
            this.tabPageBody.TabIndex = 3;
            this.tabPageBody.Text = "Body";
            this.tabPageBody.UseVisualStyleBackColor = true;
            // 
            // tabControlBodyType
            // 
            this.tabControlBodyType.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControlBodyType.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlBodyType.Controls.Add(this.tabPageBodyFormData);
            this.tabControlBodyType.Controls.Add(this.tabPageBodyRaw);
            this.tabControlBodyType.Controls.Add(this.tabPageBodyFile);
            this.tabControlBodyType.Location = new System.Drawing.Point(3, 36);
            this.tabControlBodyType.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.tabControlBodyType.Name = "tabControlBodyType";
            this.tabControlBodyType.SelectedIndex = 0;
            this.tabControlBodyType.Size = new System.Drawing.Size(984, 195);
            this.tabControlBodyType.TabIndex = 1;
            // 
            // tabPageBodyFormData
            // 
            this.tabPageBodyFormData.Controls.Add(this.dataExcelControlFormData);
            this.tabPageBodyFormData.Location = new System.Drawing.Point(4, 4);
            this.tabPageBodyFormData.Name = "tabPageBodyFormData";
            this.tabPageBodyFormData.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBodyFormData.Size = new System.Drawing.Size(976, 166);
            this.tabPageBodyFormData.TabIndex = 0;
            this.tabPageBodyFormData.Text = "        KEY-VALUE Data";
            this.tabPageBodyFormData.UseVisualStyleBackColor = true;
            // 
            // dataExcelControlFormData
            // 
            this.dataExcelControlFormData.BackColor = System.Drawing.Color.White;
            this.dataExcelControlFormData.BorderColor = System.Drawing.Color.Empty;
            this.dataExcelControlFormData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataExcelControlFormData.Font = new System.Drawing.Font("Tahoma", 9F);
            this.dataExcelControlFormData.Location = new System.Drawing.Point(3, 3);
            this.dataExcelControlFormData.Margin = new System.Windows.Forms.Padding(4);
            this.dataExcelControlFormData.Name = "dataExcelControlFormData";
            this.dataExcelControlFormData.Size = new System.Drawing.Size(970, 160);
            this.dataExcelControlFormData.TabIndex = 3;
            this.dataExcelControlFormData.Text = "dataExcelControl1";
            // 
            // tabPageBodyRaw
            // 
            this.tabPageBodyRaw.Controls.Add(this.txtRawData);
            this.tabPageBodyRaw.Location = new System.Drawing.Point(4, 4);
            this.tabPageBodyRaw.Name = "tabPageBodyRaw";
            this.tabPageBodyRaw.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBodyRaw.Size = new System.Drawing.Size(976, 166);
            this.tabPageBodyRaw.TabIndex = 1;
            this.tabPageBodyRaw.Text = "     CONTENTS   ";
            this.tabPageBodyRaw.UseVisualStyleBackColor = true;
            // 
            // txtRawData
            // 
            this.txtRawData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtRawData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRawData.Location = new System.Drawing.Point(3, 3);
            this.txtRawData.Name = "txtRawData";
            this.txtRawData.Size = new System.Drawing.Size(970, 160);
            this.txtRawData.TabIndex = 2;
            this.txtRawData.Text = "";
            // 
            // tabPageBodyFile
            // 
            this.tabPageBodyFile.Controls.Add(this.txtFilePath);
            this.tabPageBodyFile.Controls.Add(this.txtFileLocationPath);
            this.tabPageBodyFile.Controls.Add(this.btnSelectFile);
            this.tabPageBodyFile.Controls.Add(this.label2);
            this.tabPageBodyFile.Controls.Add(this.label1);
            this.tabPageBodyFile.Location = new System.Drawing.Point(4, 4);
            this.tabPageBodyFile.Name = "tabPageBodyFile";
            this.tabPageBodyFile.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBodyFile.Size = new System.Drawing.Size(976, 166);
            this.tabPageBodyFile.TabIndex = 2;
            this.tabPageBodyFile.Text = "    SELECT FILE";
            this.tabPageBodyFile.UseVisualStyleBackColor = true;
            // 
            // txtFilePath
            // 
            this.txtFilePath.FormattingEnabled = true;
            this.txtFilePath.Location = new System.Drawing.Point(155, 20);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(337, 23);
            this.txtFilePath.TabIndex = 3;
            // 
            // txtFileLocationPath
            // 
            this.txtFileLocationPath.FormattingEnabled = true;
            this.txtFileLocationPath.Location = new System.Drawing.Point(155, 60);
            this.txtFileLocationPath.Name = "txtFileLocationPath";
            this.txtFileLocationPath.Size = new System.Drawing.Size(776, 23);
            this.txtFileLocationPath.TabIndex = 3;
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(155, 92);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(83, 26);
            this.btnSelectFile.TabIndex = 2;
            this.btnSelectFile.Text = "Select";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 15);
            this.label2.TabIndex = 1;
            this.label2.Text = "Select File";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(39, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "File Path";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.radioCheckfile);
            this.panel3.Controls.Add(this.radioCheckraw);
            this.panel3.Controls.Add(this.radioCheckformdata);
            this.panel3.Controls.Add(this.txtContentType);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(983, 33);
            this.panel3.TabIndex = 0;
            // 
            // radioCheckfile
            // 
            this.radioCheckfile.AutoSize = true;
            this.radioCheckfile.Location = new System.Drawing.Point(196, 7);
            this.radioCheckfile.Name = "radioCheckfile";
            this.radioCheckfile.Size = new System.Drawing.Size(60, 19);
            this.radioCheckfile.TabIndex = 0;
            this.radioCheckfile.TabStop = true;
            this.radioCheckfile.Text = "file";
            this.radioCheckfile.UseVisualStyleBackColor = true;
            this.radioCheckfile.CheckedChanged += new System.EventHandler(this.radioCheckfile_CheckedChanged);
            // 
            // radioCheckraw
            // 
            this.radioCheckraw.AutoSize = true;
            this.radioCheckraw.Location = new System.Drawing.Point(119, 7);
            this.radioCheckraw.Name = "radioCheckraw";
            this.radioCheckraw.Size = new System.Drawing.Size(52, 19);
            this.radioCheckraw.TabIndex = 0;
            this.radioCheckraw.Text = "raw";
            this.radioCheckraw.UseVisualStyleBackColor = true;
            this.radioCheckraw.CheckedChanged += new System.EventHandler(this.radioCheckraw_CheckedChanged);
            // 
            // radioCheckformdata
            // 
            this.radioCheckformdata.AutoSize = true;
            this.radioCheckformdata.Checked = true;
            this.radioCheckformdata.Location = new System.Drawing.Point(6, 7);
            this.radioCheckformdata.Name = "radioCheckformdata";
            this.radioCheckformdata.Size = new System.Drawing.Size(92, 19);
            this.radioCheckformdata.TabIndex = 0;
            this.radioCheckformdata.TabStop = true;
            this.radioCheckformdata.Text = "formdata";
            this.radioCheckformdata.UseVisualStyleBackColor = true;
            this.radioCheckformdata.CheckedChanged += new System.EventHandler(this.radioCheckformdata_CheckedChanged);
            // 
            // txtContentType
            // 
            this.txtContentType.FormattingEnabled = true;
            this.txtContentType.Items.AddRange(new object[] {
            "JSON",
            "TEXT",
            "XML",
            "HTML"});
            this.txtContentType.Location = new System.Drawing.Point(323, 6);
            this.txtContentType.Name = "txtContentType";
            this.txtContentType.Size = new System.Drawing.Size(259, 23);
            this.txtContentType.TabIndex = 1;
            this.txtContentType.Text = "JSON";
            this.txtContentType.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtScriptCode);
            this.tabPage1.Controls.Add(this.panel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(989, 234);
            this.tabPage1.TabIndex = 7;
            this.tabPage1.Text = "UrlScipt";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtScriptCode
            // 
            this.txtScriptCode.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtScriptCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScriptCode.Location = new System.Drawing.Point(3, 33);
            this.txtScriptCode.Name = "txtScriptCode";
            this.txtScriptCode.Size = new System.Drawing.Size(983, 198);
            this.txtScriptCode.TabIndex = 1;
            this.txtScriptCode.Text = "";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(983, 30);
            this.panel1.TabIndex = 0;
            // 
            // toolStrip2
            // 
            this.toolStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnDebugGenerateCode,
            this.btnDebugStart,
            this.btnDebugStep,
            this.btnDebugStepOut,
            this.btnDebugStepIn,
            this.btnDebugPause,
            this.btnDebugStop});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(983, 25);
            this.toolStrip2.TabIndex = 0;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnDebugGenerateCode
            // 
            this.btnDebugGenerateCode.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDebugGenerateCode.Image = global::CFusion.Post.Properties.Resources.view_Scripts;
            this.btnDebugGenerateCode.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDebugGenerateCode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDebugGenerateCode.Name = "btnDebugGenerateCode";
            this.btnDebugGenerateCode.Size = new System.Drawing.Size(29, 22);
            this.btnDebugGenerateCode.Text = "Generate Code";
            this.btnDebugGenerateCode.Click += new System.EventHandler(this.btnDebugGenerateCode_Click);
            // 
            // btnDebugStart
            // 
            this.btnDebugStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDebugStart.Image = global::CFusion.Post.Properties.Resources.Action_Debug_Start;
            this.btnDebugStart.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDebugStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDebugStart.Name = "btnDebugStart";
            this.btnDebugStart.Size = new System.Drawing.Size(29, 22);
            this.btnDebugStart.Text = "Debug Start";
            this.btnDebugStart.Click += new System.EventHandler(this.btnDebugStart_Click);
            // 
            // btnDebugStep
            // 
            this.btnDebugStep.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDebugStep.Image = global::CFusion.Post.Properties.Resources.Action_Debug_Step;
            this.btnDebugStep.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDebugStep.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDebugStep.Name = "btnDebugStep";
            this.btnDebugStep.Size = new System.Drawing.Size(29, 22);
            this.btnDebugStep.Text = "Debug Step";
            this.btnDebugStep.Click += new System.EventHandler(this.btnDebugStep_Click);
            // 
            // btnDebugStepOut
            // 
            this.btnDebugStepOut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDebugStepOut.Image = global::CFusion.Post.Properties.Resources.Stepout_6327;
            this.btnDebugStepOut.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDebugStepOut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDebugStepOut.Name = "btnDebugStepOut";
            this.btnDebugStepOut.Size = new System.Drawing.Size(29, 22);
            this.btnDebugStepOut.Text = "Debug Step Out";
            this.btnDebugStepOut.Click += new System.EventHandler(this.btnDebugStepOut_Click);
            // 
            // btnDebugStepIn
            // 
            this.btnDebugStepIn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDebugStepIn.Image = global::CFusion.Post.Properties.Resources.StepIn_6326;
            this.btnDebugStepIn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDebugStepIn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDebugStepIn.Name = "btnDebugStepIn";
            this.btnDebugStepIn.Size = new System.Drawing.Size(29, 22);
            this.btnDebugStepIn.Text = "Debug Step In";
            this.btnDebugStepIn.Visible = false;
            // 
            // btnDebugPause
            // 
            this.btnDebugPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDebugPause.Image = global::CFusion.Post.Properties.Resources.Pause_16x;
            this.btnDebugPause.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDebugPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDebugPause.Name = "btnDebugPause";
            this.btnDebugPause.Size = new System.Drawing.Size(29, 22);
            this.btnDebugPause.Text = "Debug Pause";
            this.btnDebugPause.Visible = false;
            // 
            // btnDebugStop
            // 
            this.btnDebugStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDebugStop.Image = global::CFusion.Post.Properties.Resources.Action_Debug_Stop;
            this.btnDebugStop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnDebugStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDebugStop.Name = "btnDebugStop";
            this.btnDebugStop.Size = new System.Drawing.Size(29, 22);
            this.btnDebugStop.Text = "Debug Stop";
            this.btnDebugStop.Click += new System.EventHandler(this.btnDebugStop_Click);
            // 
            // tabControl2
            // 
            this.tabControl2.Controls.Add(this.tabPageResponse);
            this.tabControl2.Controls.Add(this.tabPageHistory);
            this.tabControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl2.Location = new System.Drawing.Point(0, 0);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(997, 244);
            this.tabControl2.TabIndex = 0;
            this.tabControl2.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl2_Selected);
            // 
            // tabPageResponse
            // 
            this.tabPageResponse.Controls.Add(this.txtResponse);
            this.tabPageResponse.Controls.Add(this.toolStrip1);
            this.tabPageResponse.Location = new System.Drawing.Point(4, 25);
            this.tabPageResponse.Name = "tabPageResponse";
            this.tabPageResponse.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageResponse.Size = new System.Drawing.Size(989, 215);
            this.tabPageResponse.TabIndex = 0;
            this.tabPageResponse.Text = "Response";
            this.tabPageResponse.UseVisualStyleBackColor = true;
            // 
            // txtResponse
            // 
            this.txtResponse.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtResponse.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtResponse.Location = new System.Drawing.Point(3, 28);
            this.txtResponse.Name = "txtResponse";
            this.txtResponse.Size = new System.Drawing.Size(983, 184);
            this.txtResponse.TabIndex = 0;
            this.txtResponse.Text = "";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnResponseSaveAs,
            this.btnResponseClearAll,
            this.chkResponseAutoClear,
            this.txtResponseTime});
            this.toolStrip1.Location = new System.Drawing.Point(3, 3);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(983, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnResponseSaveAs
            // 
            this.btnResponseSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnResponseSaveAs.Image = global::CFusion.Post.Properties.Resources.SaveFileAs;
            this.btnResponseSaveAs.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnResponseSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnResponseSaveAs.Name = "btnResponseSaveAs";
            this.btnResponseSaveAs.Size = new System.Drawing.Size(29, 22);
            this.btnResponseSaveAs.Text = "SaveAs";
            this.btnResponseSaveAs.Click += new System.EventHandler(this.btnResponseSaveAs_Click);
            // 
            // btnResponseClearAll
            // 
            this.btnResponseClearAll.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnResponseClearAll.Image = global::CFusion.Post.Properties.Resources.ClearAll;
            this.btnResponseClearAll.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnResponseClearAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnResponseClearAll.Name = "btnResponseClearAll";
            this.btnResponseClearAll.Size = new System.Drawing.Size(29, 22);
            this.btnResponseClearAll.Text = "Clear";
            this.btnResponseClearAll.Click += new System.EventHandler(this.btnResponseClearAll_Click);
            // 
            // chkResponseAutoClear
            // 
            this.chkResponseAutoClear.Checked = true;
            this.chkResponseAutoClear.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkResponseAutoClear.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.chkResponseAutoClear.Image = global::CFusion.Post.Properties.Resources.postmain_responsecheckrememberresult;
            this.chkResponseAutoClear.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.chkResponseAutoClear.ImageTransparentColor = System.Drawing.Color.Maroon;
            this.chkResponseAutoClear.Name = "chkResponseAutoClear";
            this.chkResponseAutoClear.Size = new System.Drawing.Size(29, 22);
            this.chkResponseAutoClear.Text = "AutoClear";
            this.chkResponseAutoClear.Click += new System.EventHandler(this.chkResponseAutoClear_Click);
            // 
            // txtResponseTime
            // 
            this.txtResponseTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.txtResponseTime.Image = ((System.Drawing.Image)(resources.GetObject("txtResponseTime.Image")));
            this.txtResponseTime.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.txtResponseTime.Name = "txtResponseTime";
            this.txtResponseTime.Size = new System.Drawing.Size(29, 22);
            // 
            // tabPageHistory
            // 
            this.tabPageHistory.Controls.Add(this.splitContainer3);
            this.tabPageHistory.Location = new System.Drawing.Point(4, 25);
            this.tabPageHistory.Name = "tabPageHistory";
            this.tabPageHistory.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageHistory.Size = new System.Drawing.Size(989, 215);
            this.tabPageHistory.TabIndex = 1;
            this.tabPageHistory.Text = "History";
            this.tabPageHistory.UseVisualStyleBackColor = true;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(3, 3);
            this.splitContainer3.Name = "splitContainer3";
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.treeViewHistory);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.txtHistoryContents);
            this.splitContainer3.Size = new System.Drawing.Size(983, 209);
            this.splitContainer3.SplitterDistance = 295;
            this.splitContainer3.TabIndex = 0;
            // 
            // treeViewHistory
            // 
            this.treeViewHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewHistory.Location = new System.Drawing.Point(0, 0);
            this.treeViewHistory.Name = "treeViewHistory";
            this.treeViewHistory.Size = new System.Drawing.Size(295, 209);
            this.treeViewHistory.TabIndex = 0;
            this.treeViewHistory.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewHistory_NodeMouseClick);
            // 
            // txtHistoryContents
            // 
            this.txtHistoryContents.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtHistoryContents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtHistoryContents.Location = new System.Drawing.Point(0, 0);
            this.txtHistoryContents.Name = "txtHistoryContents";
            this.txtHistoryContents.Size = new System.Drawing.Size(684, 209);
            this.txtHistoryContents.TabIndex = 1;
            this.txtHistoryContents.Text = "";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnPaste);
            this.panel2.Controls.Add(this.btnRename);
            this.panel2.Controls.Add(this.btnImport);
            this.panel2.Controls.Add(this.btnSave);
            this.panel2.Controls.Add(this.btnSend);
            this.panel2.Controls.Add(this.txtLocationPath);
            this.panel2.Controls.Add(this.txtUrl);
            this.panel2.Controls.Add(this.txtTimeout);
            this.panel2.Controls.Add(this.txtMethod);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(997, 115);
            this.panel2.TabIndex = 0;
            // 
            // btnPaste
            // 
            this.btnPaste.Image = global::CFusion.Post.Properties.Resources.Paste_16x16;
            this.btnPaste.Location = new System.Drawing.Point(174, 54);
            this.btnPaste.Name = "btnPaste";
            this.btnPaste.Size = new System.Drawing.Size(16, 16);
            this.btnPaste.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnPaste.TabIndex = 5;
            this.btnPaste.TabStop = false;
            this.btnPaste.Click += new System.EventHandler(this.btnPaste_Click);
            // 
            // btnRename
            // 
            this.btnRename.AutoSize = true;
            this.btnRename.Location = new System.Drawing.Point(36, 20);
            this.btnRename.Name = "btnRename";
            this.btnRename.Size = new System.Drawing.Size(23, 15);
            this.btnRename.TabIndex = 4;
            this.btnRename.TabStop = true;
            this.btnRename.Text = "Rn";
            this.btnRename.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnRename_LinkClicked);
            // 
            // btnImport
            // 
            this.btnImport.AutoSize = true;
            this.btnImport.Location = new System.Drawing.Point(114, 93);
            this.btnImport.Name = "btnImport";
            this.btnImport.Size = new System.Drawing.Size(55, 15);
            this.btnImport.TabIndex = 4;
            this.btnImport.TabStop = true;
            this.btnImport.Text = "Import";
            this.btnImport.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.btnImport_LinkClicked);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::CFusion.Post.Properties.Resources.SaveAsTemplate_16x16;
            this.btnSave.Location = new System.Drawing.Point(5, 8);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(31, 30);
            this.btnSave.TabIndex = 3;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(7, 78);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(92, 30);
            this.btnSend.TabIndex = 3;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtLocationPath
            // 
            this.txtLocationPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLocationPath.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtLocationPath.Font = new System.Drawing.Font("微软雅黑", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtLocationPath.Location = new System.Drawing.Point(61, 12);
            this.txtLocationPath.Name = "txtLocationPath";
            this.txtLocationPath.Size = new System.Drawing.Size(923, 24);
            this.txtLocationPath.TabIndex = 2;
            this.txtLocationPath.Text = "bejson";
            // 
            // txtUrl
            // 
            this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtUrl.Location = new System.Drawing.Point(196, 48);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(794, 25);
            this.txtUrl.TabIndex = 2;
            this.txtUrl.Text = "https://www.bejson.com/json/format/";
            this.txtUrl.TextChanged += new System.EventHandler(this.txtUrl_TextChanged);
            this.txtUrl.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.txtUrl_MouseDoubleClick);
            // 
            // txtTimeout
            // 
            this.txtTimeout.FormattingEnabled = true;
            this.txtTimeout.Items.AddRange(new object[] {
            "10s",
            "20s",
            "30s",
            "60s",
            "90s"});
            this.txtTimeout.Location = new System.Drawing.Point(102, 49);
            this.txtTimeout.Name = "txtTimeout";
            this.txtTimeout.Size = new System.Drawing.Size(66, 23);
            this.txtTimeout.TabIndex = 1;
            this.txtTimeout.Text = "30s";
            this.toolTip1.SetToolTip(this.txtTimeout, "timeout");
            this.txtTimeout.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // txtMethod
            // 
            this.txtMethod.FormattingEnabled = true;
            this.txtMethod.Items.AddRange(new object[] {
            "GET",
            "POST",
            "PUT",
            "DELETE",
            "HEAD",
            "OPTIONS",
            "TRACE",
            "PATCH"});
            this.txtMethod.Location = new System.Drawing.Point(7, 49);
            this.txtMethod.Name = "txtMethod";
            this.txtMethod.Size = new System.Drawing.Size(92, 23);
            this.txtMethod.TabIndex = 1;
            this.txtMethod.Text = "POST";
            this.txtMethod.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ctrlPostControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Name = "ctrlPostControl";
            this.Size = new System.Drawing.Size(997, 626);
            this.Load += new System.EventHandler(this.frmPostMain_Load);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPageSetting.ResumeLayout(false);
            this.tabPageHeaders.ResumeLayout(false);
            this.tabPageBody.ResumeLayout(false);
            this.tabControlBodyType.ResumeLayout(false);
            this.tabPageBodyFormData.ResumeLayout(false);
            this.tabPageBodyRaw.ResumeLayout(false);
            this.tabPageBodyFile.ResumeLayout(false);
            this.tabPageBodyFile.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.tabPageResponse.ResumeLayout(false);
            this.tabPageResponse.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tabPageHistory.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnPaste)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.ComboBox txtMethod;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage tabPageResponse;
        private System.Windows.Forms.TabPage tabPageHistory;
        private System.Windows.Forms.TabPage tabPageHeaders;
        private System.Windows.Forms.TabPage tabPageBody;
        private System.Windows.Forms.RichTextBox txtResponse;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.TreeView treeViewHistory;
        private System.Windows.Forms.RichTextBox txtHistoryContents;
        private Feng.Excel.DataExcelControl dataExcelHeaders;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton radioCheckformdata;
        private System.Windows.Forms.RadioButton radioCheckraw;
        private System.Windows.Forms.RadioButton radioCheckfile;
        private System.Windows.Forms.ComboBox txtContentType;
        private System.Windows.Forms.TabPage tabPageSetting;
        private System.Windows.Forms.RichTextBox txtSetting;
        private System.Windows.Forms.TabControl tabControlBodyType;
        private System.Windows.Forms.TabPage tabPageBodyFormData;
        private System.Windows.Forms.TabPage tabPageBodyRaw;
        private System.Windows.Forms.RichTextBox txtRawData;
        private System.Windows.Forms.TabPage tabPageBodyFile;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.ComboBox txtFilePath;
        private System.Windows.Forms.ComboBox txtFileLocationPath;
        private Feng.Excel.DataExcelControl dataExcelControlFormData;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox txtScriptCode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox txtTimeout;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button btnSave;
        private TextBoxEx txtLocationPath;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnResponseSaveAs;
        private System.Windows.Forms.ToolStripButton btnResponseClearAll;
        private System.Windows.Forms.ToolStripButton chkResponseAutoClear;
        private System.Windows.Forms.ToolStripButton txtResponseTime;
        private System.Windows.Forms.LinkLabel btnImport;
        private System.Windows.Forms.LinkLabel btnRename;
        private System.Windows.Forms.PictureBox btnPaste;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnDebugGenerateCode;
        private System.Windows.Forms.ToolStripButton btnDebugStart;
        private System.Windows.Forms.ToolStripButton btnDebugPause;
        private System.Windows.Forms.ToolStripButton btnDebugStop;
        private System.Windows.Forms.ToolStripButton btnDebugStepIn;
        private System.Windows.Forms.ToolStripButton btnDebugStep;
        private System.Windows.Forms.ToolStripButton btnDebugStepOut;
    }
}