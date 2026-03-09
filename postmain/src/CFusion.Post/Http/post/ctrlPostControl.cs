using Feng.Excel.Interfaces;
using Feng.Net.Http;
using Feng.Script.CBEexpress;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Tools;

namespace CFusion.Http.post
{
    public partial class ctrlPostControl : UserControl
    {
        public static string RootPath { get; set; }
        public static string LogPath { get; set; } = "ResponseLog";
        public ctrlPostControl()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void frmPostMain_Load(object sender, EventArgs e)
        {
            try
            {
                Initheaderexcel();
                Initheaderexcel2();
                InitializeContentTypeComboBox();
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                PostMainHttpHelper postMainHttp = new PostMainHttpHelper();
                postMainHttp.ShowDebugInfo = true;
                postMainHttp.Timeout = Feng.Utils.ConvertHelper.ToInt(this.txtTimeout.Text.Trim('s'), 30) * 1000;
                string url = this.txtUrl.Text;
                System.Uri uri = new Uri(url);
                Feng.Forms.WaitingForm2.BeginWaiting(url);
                if (string.IsNullOrWhiteSpace(this.txtLocationPath.Text))
                {
                    string path = uri.Host.Replace('.', '_');
                    path = path + "__" + uri.Port;
                    path = path + "\\" + uri.LocalPath.Replace('/', '_');
                    txtLocationPath.Text = path;
                }
                Dictionary<string, string> headers = GetHeaders2();
                string contentType = GetSelectedContentType();
                string data = this.txtRawData.Text;
                string response = string.Empty;
                switch (txtMethod.Text)
                {
                    case "GET":
                        response = postMainHttp.Get(url, headers);
                        break;
                    case "POST":
                        if (radioCheckfile.Checked)
                        {
                            string serverpath = this.txtFilePath.Text;
                            string locationpath = this.txtFileLocationPath.Text;
                            response = postMainHttp.PostFile(url, locationpath, serverpath, headers);
                        }
                        if (this.radioCheckformdata.Checked)
                        {
                            Dictionary<string, string> formData = GetFormData();
                            response = postMainHttp.PostFormData(url, formData, headers);
                        }
                        if (this.radioCheckraw.Checked)
                        {
                            response = postMainHttp.PostRaw(url, data, contentType, headers);
                        }
                        break;
                    case "PUT":
                        response = postMainHttp.Put(url, data, contentType, headers);
                        break;
                    case "DELETE":
                        response = postMainHttp.Delete(url, data, contentType, headers);
                        break;
                    case "HEAD":
                        response = postMainHttp.Head(url, headers);
                        break;
                    case "OPTIONS":
                        response = postMainHttp.Options(url, headers);
                        break;
                    case "TRACE":
                        response = postMainHttp.Trace(url, headers);
                        break;
                    case "PATCH":
                        response = postMainHttp.Patch(url, data, contentType, headers);
                        break;
                    default:
                        break;
                }
                if (this.chkResponseAutoClear.Checked)
                {
                    ClearResponse();
                }
                this.txtResponseTime.Text = postMainHttp.ResponseTime + "ms";
                //AppendText(url);
                AppendText(response);
                SaveLog();
            }
            catch (Exception ex)
            {
                AppendText(ex.Message);
            }
            finally
            {
                Feng.Forms.WaitingForm2.EndWaiting();
            }
        }
        private void LoadHistory()
        {
            try
            {
                treeViewHistory.Nodes.Clear();
                string logpath = Feng.IO.FileHelper.Combine(LogPath, this.txtLocationPath.Text);
                if (System.IO.Directory.Exists(logpath))
                {
                    string[] files = System.IO.Directory.GetFiles(logpath);
                    foreach (string file in files)
                    {
                        System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
                        TreeNode treeNode = treeViewHistory.Nodes.Add(fileInfo.Name);
                        treeNode.Tag = file;
                    }
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }

        }
        public void SaveLog()
        {
            try
            {
                ModelPostData modelPostData = GetModelPostData();
                string json = JsonHelper.SerializeObject(modelPostData);

                string txt = json + "\r\n" + this.txtResponse.Text;
                string logpath = Feng.IO.FileHelper.Combine(LogPath, this.txtLocationPath.Text);
                logpath = Feng.IO.FileHelper.Combine(logpath, DateTime.Now.ToString("yyyyMMddHHmmss") + ".log");
                Feng.IO.FileHelper.WriteAllText(logpath, txt);
            }
            catch (Exception ex)
            {
                Log(ex);
            }

        }
        public void ClearResponse()
        {
            this.txtResponse.Clear();
        }
        public void AppendText(string txt)
        {
            if (this.txtResponse.InvokeRequired)
            {
                this.txtResponse.Invoke(new Action(() =>
                {
                    this.txtResponse.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    this.txtResponse.AppendText(System.Environment.NewLine);
                    this.txtResponse.AppendText(txt);
                    this.txtResponse.AppendText(System.Environment.NewLine);
                }));
            }
            else
            {
                this.txtResponse.AppendText(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                this.txtResponse.AppendText(System.Environment.NewLine);
                this.txtResponse.AppendText(txt);
                this.txtResponse.AppendText(System.Environment.NewLine);
            }

        }

        private void InitializeContentTypeComboBox()
        {
            txtContentType.Items.Clear();
            txtContentType.Items.Add("--text");
            txtContentType.Items.Add("text/plain");
            txtContentType.Items.Add("text/html");
            txtContentType.Items.Add("text/css");
            txtContentType.Items.Add("text/javascript");
            txtContentType.Items.Add("text/xml");


            txtContentType.Items.Add("--application");
            txtContentType.Items.Add("application/json");
            txtContentType.Items.Add("application/xml");
            txtContentType.Items.Add("application/x-www-form-urlencoded");
            txtContentType.Items.Add("application/octet-stream");
            txtContentType.Items.Add("application/pdf");
            txtContentType.Items.Add("application/zip");
            txtContentType.Items.Add("application/msword");
            txtContentType.Items.Add("application/vnd.ms-excel");
            txtContentType.Items.Add("application/vnd.ms-powerpoint");

            //txtContentType.Items.Add("--image"); 
            //txtContentType.Items.Add("image/jpeg");
            //txtContentType.Items.Add("image/png");
            //txtContentType.Items.Add("image/gif");
            //txtContentType.Items.Add("image/svg+xml");
            //txtContentType.Items.Add("image/bmp");

            //txtContentType.Items.Add("--multipart");
            //txtContentType.Items.Add("multipart/form-data");

            //txtContentType.Items.Add("--media");
            //txtContentType.Items.Add("audio/mpeg");
            //txtContentType.Items.Add("video/mp4");

            // 设置默认选项
            txtContentType.Text = "application/json";
        }

        private string GetSelectedContentType()
        {
            if (string.IsNullOrWhiteSpace(txtContentType.Text))
                return "text/plain";

            string selectedText = txtContentType.SelectedItem.ToString();
            // 提取实际的Content-Type值（空格前的部分）
            return selectedText.Split(' ')[0];
        }
        private ProjectItem CurrnetProjectItem = null;
        internal void Init(ProjectItem projectitem)
        {
            CurrnetProjectItem = projectitem;
            this.txtLocationPath.Text = CurrnetProjectItem.Path;
            if (System.IO.File.Exists(CurrnetProjectItem.Path))
            {
                string json = Feng.IO.FileHelper.ReadAllText(CurrnetProjectItem.Path);
                ModelPostData item = JsonHelper.DeserializeObject<ModelPostData>(json);
                LoadItem(item);
            }
        }

        private void txtUrl_TextChanged(object sender, EventArgs e)
        {

        }

        private ModelPostData GetModelPostData()
        {
            ModelPostData modelPostData = new ModelPostData();
            InitItem(modelPostData);
            GetHeaders(modelPostData);
            GetBody(modelPostData);
            return modelPostData;
        }
        public event EventHandler SaveClick;
        private void OnSaveClick()
        {
            if (SaveClick != null)
            {
                SaveClick(this, new EventArgs());
            }
        }
        public string GetTitle()
        {
            return this.txtLocationPath.Text;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                ModelPostData modelPostData = new ModelPostData();
                InitItem(modelPostData);
                GetHeaders(modelPostData);
                GetBody(modelPostData);
                string json = JsonHelper.SerializeObject(modelPostData);
                string file = Feng.IO.FileHelper.Combine(RootPath, this.txtLocationPath.Text + ".pro.json");
                Feng.IO.FileHelper.WriteAllText(file, json);
                OnSaveClick();
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void Initheaderexcel()
        {
            this.dataExcelHeaders.EditView.Clear();
            int keyindex = 2;
            int valueindex = 3;
            int remarkindex = 4;
            this.dataExcelHeaders.EditView[1, 1].Value = "Index";
            this.dataExcelHeaders.EditView[1, keyindex].Value = "Key";
            this.dataExcelHeaders.EditView[1, valueindex].Value = "Value";
            this.dataExcelHeaders.EditView[1, remarkindex].Value = "Remark";


            this.dataExcelHeaders.EditView[1, valueindex].AutoMultiline = true;

            this.dataExcelHeaders.EditView.Columns[remarkindex].Width = 300;
            this.dataExcelHeaders.EditView.ReFreshFirstDisplayRowIndex();
            this.dataExcelHeaders.EditView.ReFreshFirstDisplayColumnIndex();
            this.dataExcelHeaders.EditView.CellValueChanged += EditView_CellValueChanged;
        }

        private void EditView_CellValueChanged(object sender, Feng.Excel.Args.CellValueChangedArgs e)
        {
            try
            {
                e.Cell.AutoMultiline = true;
            }
            catch (Exception)
            {
            }
        }

        private void Initheaderexcel2()
        {
            this.dataExcelControlFormData.EditView.Clear();
            int keyindex = 2;
            int valueindex = 3;
            int remarkindex = 4;
            this.dataExcelControlFormData.EditView[1, 1].Value = "Index";
            this.dataExcelControlFormData.EditView[1, keyindex].Value = "Key";
            this.dataExcelControlFormData.EditView[1, valueindex].Value = "Value";
            this.dataExcelControlFormData.EditView[1, remarkindex].Value = "Remark";


            this.dataExcelControlFormData.EditView[1, valueindex].AutoMultiline = true;

            this.dataExcelControlFormData.EditView.Columns[remarkindex].Width = 300;
            this.dataExcelControlFormData.EditView.ReFreshFirstDisplayRowIndex();
            this.dataExcelControlFormData.EditView.ReFreshFirstDisplayColumnIndex();
            this.dataExcelControlFormData.EditView.CellValueChanged += EditView_CellValueChanged;
        }

        private Dictionary<string, string> GetHeaders2()
        {
            Dictionary<string, string> dics = new Dictionary<string, string>();
            try
            {
                int keyindex = 2;
                int valueindex = 3;
                int remarkindex = 4;
                foreach (IRow item in this.dataExcelHeaders.EditView.Rows)
                {
                    if (item.Index < 2)
                        continue;
                    string key = item[keyindex].Text;
                    if (string.IsNullOrWhiteSpace(key))
                        continue;
                    string value = item[valueindex].Text;
                    string remark = item[remarkindex].Text;
                    dics.Add(key, value);
                }
            }
            catch (Exception ex)
            {

            }
            return dics;
        }

        private void GetHeaders(ModelPostData postData)
        {
            postData.Headers = new List<ModelDictItem>();
            int keyindex = 2;
            int valueindex = 3;
            int remarkindex = 4;
            foreach (IRow item in this.dataExcelHeaders.EditView.Rows)
            {
                if (item.Index < 2)
                    continue;
                string key = item[keyindex].Text;
                if (string.IsNullOrWhiteSpace(key))
                    continue;
                string value = item[valueindex].Text;
                string remark = item[remarkindex].Text;
                postData.Headers.Add(new ModelDictItem() { Key = key, Value = value, Remark = remark });
            }
        }

        private void LoadHeaders(ModelPostData postData)
        {
            int keyindex = 2;
            int valueindex = 3;
            int remarkindex = 4;
            foreach (IRow item in this.dataExcelHeaders.EditView.Rows)
            {
                if (item.Index < 2)
                    continue;
                item[keyindex].Value = string.Empty;
                item[keyindex].Text = string.Empty;
                item[valueindex].Value = string.Empty;
                item[valueindex].Text = string.Empty;
                item[remarkindex].Value = string.Empty;
                item[remarkindex].Text = string.Empty;
            }
            for (int i = 0; i < postData.Headers.Count; i++)
            {
                ModelDictItem modelDictItem = postData.Headers[i];
                IRow item = this.dataExcelHeaders.EditView.GetRow(i + 2);
                item[keyindex].Value = modelDictItem.Key;
                item[keyindex].Text = modelDictItem.Key;

                item[valueindex].Value = modelDictItem.Value;
                item[valueindex].Text = modelDictItem.Value;

                item[remarkindex].Value = modelDictItem.Remark;
                item[remarkindex].Text = modelDictItem.Remark;
            }
            this.dataExcelHeaders.EditView.ReFreshFirstDisplayRowIndex();
            this.dataExcelHeaders.EditView.ReFreshFirstDisplayColumnIndex();

        }

        private void GetBody(ModelPostData postData)
        {
            postData.ModelPostDataBody = new ModelPostDataBody();
            postData.ModelPostDataBody.Dataes = new List<ModelDictItem>();
            postData.ModelPostDataBody.RawData = this.txtRawData.Text;
            postData.ModelPostDataBody.FileLocationPath = this.txtFileLocationPath.Text;
            postData.ModelPostDataBody.FileSeverPath = this.txtFilePath.Text;
            if (this.radioCheckfile.Checked)
            {
                postData.ModelPostDataBody.BodyType = "file";
            }
            if (this.radioCheckformdata.Checked)
            {
                postData.ModelPostDataBody.BodyType = "formdata";
            }
            if (this.radioCheckraw.Checked)
            {
                postData.ModelPostDataBody.BodyType = "raw";
            }
            int keyindex = 2;
            int valueindex = 3;
            int remarkindex = 4;
            foreach (IRow item in this.dataExcelControlFormData.EditView.Rows)
            {
                if (item.Index < 2)
                    continue;
                string key = item[keyindex].Text;
                if (string.IsNullOrWhiteSpace(key))
                    continue;
                string value = item[valueindex].Text;
                string remark = item[remarkindex].Text;
                postData.ModelPostDataBody.Dataes.Add(new ModelDictItem() { Key = key, Value = value, Remark = remark });
            }
        }
        private Dictionary<string, string> GetFormData()
        {
            Dictionary<string, string> dics = new Dictionary<string, string>();
            try
            {
                int keyindex = 2;
                int valueindex = 3;
                int remarkindex = 4;
                foreach (IRow item in this.dataExcelControlFormData.EditView.Rows)
                {
                    if (item.Index < 1)
                        continue;
                    string key = item[keyindex].Text;
                    if (string.IsNullOrWhiteSpace(key))
                        continue;
                    string value = item[valueindex].Text;
                    string remark = item[remarkindex].Text;
                    dics.Add(key, value);
                }
            }
            catch (Exception ex)
            {
            }
            return dics;
        }
        private void LoadBody(ModelPostData postData)
        {

            this.txtRawData.Text = postData.ModelPostDataBody.RawData;
            this.txtFileLocationPath.Text = postData.ModelPostDataBody.FileLocationPath;
            this.txtFilePath.Text = postData.ModelPostDataBody.FileSeverPath;
            if (postData.ModelPostDataBody.BodyType == "file")
            {
                this.radioCheckfile.Checked = true;
            }
            if (postData.ModelPostDataBody.BodyType == "formdata")
            {
                this.radioCheckformdata.Checked = true;
            }
            if (postData.ModelPostDataBody.BodyType == "raw")
            {
                this.radioCheckraw.Checked = true;
            }
            int keyindex = 2;
            int valueindex = 3;
            int remarkindex = 4;
            foreach (IRow item in this.dataExcelControlFormData.EditView.Rows)
            {
                if (item.Index < 1)
                    continue;
                item[keyindex].Value = string.Empty;
                item[keyindex].Text = string.Empty;


                item[valueindex].Value = string.Empty;
                item[valueindex].Text = string.Empty;


                item[remarkindex].Value = string.Empty;
                item[remarkindex].Text = string.Empty;
            }

            for (int i = 0; i < postData.ModelPostDataBody.Dataes.Count; i++)
            {
                ModelDictItem modelDictItem = postData.ModelPostDataBody.Dataes[i];
                IRow item = this.dataExcelControlFormData.EditView.GetRow(i + 1);
                item[keyindex].Value = modelDictItem.Key;
                item[keyindex].Text = modelDictItem.Key;


                item[valueindex].Value = modelDictItem.Value;
                item[valueindex].Text = modelDictItem.Value;


                item[remarkindex].Value = modelDictItem.Remark;
                item[remarkindex].Text = modelDictItem.Remark;
            }
            this.dataExcelControlFormData.EditView.ReFreshFirstDisplayRowIndex();
            this.dataExcelControlFormData.EditView.ReFreshFirstDisplayColumnIndex();
        }

        public void LoadItem(ModelPostData item)
        {
            this.txtLocationPath.Text = item.LocationPath;
            this.txtUrl.Text = item.Url;
            this.txtContentType.Text = item.ContentType;
            this.txtFileLocationPath.Text = item.FileLocationPath;
            this.txtFilePath.Text = item.FileSeverPath;
            this.txtMethod.Text = item.Method;
            this.txtRawData.Text = item.RawData;
            this.txtTimeout.Text = item.Timeout;
            this.txtSetting.Text = item.Setting;
            this.txtScriptCode.Text = item.Script;
            LoadHeaders(item);
            LoadBody(item);
        }

        public void InitItem(ModelPostData item)
        {
            item.LocationPath = this.txtLocationPath.Text;
            item.Url = this.txtUrl.Text;
            item.ContentType = this.txtContentType.Text;
            item.FileLocationPath = this.txtFileLocationPath.Text;
            item.FileSeverPath = this.txtFilePath.Text;
            item.Method = this.txtMethod.Text;
            item.RawData = this.txtRawData.Text;
            item.Timeout = this.txtTimeout.Text;
            item.Setting = this.txtSetting.Text;
            item.Script = this.txtScriptCode.Text;
        }

        public void InitHeader(List<ModelDictItem> list)
        {
            for (int i = 2; i < 100; i++)
            {
                this.dataExcelHeaders.EditView[i, 1].Value = string.Empty;
                this.dataExcelHeaders.EditView[i, 1].Text = string.Empty;
                this.dataExcelHeaders.EditView[i, 2].Value = string.Empty;
                this.dataExcelHeaders.EditView[i, 2].Text = string.Empty;
            }
            for (int i = 0; i < list.Count; i++)
            {
                ModelDictItem modelPostHeaderItem = list[i];
                this.dataExcelHeaders.EditView[i + 2, 1].Value = modelPostHeaderItem.Key;
                this.dataExcelHeaders.EditView[i + 2, 1].Text = modelPostHeaderItem.Key;
                this.dataExcelHeaders.EditView[i + 2, 2].Value = modelPostHeaderItem.Value;
                this.dataExcelHeaders.EditView[i + 2, 2].Text = modelPostHeaderItem.Value;
            }
        }

        public List<ModelDictItem> GetHeaders()
        {
            List<ModelDictItem> list = new List<ModelDictItem>();
            for (int i = 2; i < 100; i++)
            {
                string key = this.dataExcelHeaders.EditView[i, 1].Text;
                string value = this.dataExcelHeaders.EditView[i, 2].Text;
                ModelDictItem modelPostHeaderItem = new ModelDictItem()
                {
                    Key = key,
                    Value = value
                };
                list.Add(modelPostHeaderItem);
            }
            return list;
        }

        public void Log(Exception ex)
        {

        }

        private void btnResponseSaveAs_Click(object sender, EventArgs e)
        {
            try
            {
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "*.txt|*.txt";
                    dlg.FileName = DateTime.Now.ToString("yyyyMMddHHmmss");
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        Feng.IO.FileHelper.WriteAllText(dlg.FileName, this.txtResponse.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                AppendText(ex.Message);
            }
        }

        private void btnResponseClearAll_Click(object sender, EventArgs e)
        {
            try
            {
                this.txtResponse.Text = string.Empty;
            }
            catch (Exception ex)
            {
                AppendText(ex.Message);
            }
        }

        private void radioCheckraw_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                this.txtContentType.Visible = radioCheckraw.Checked;
                tabControlBodyType.SelectedTab = this.tabPageBodyRaw;
            }
            catch (Exception ex)
            {
                AppendText(ex.Message);
            }
        }

        private void radioCheckformdata_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioCheckformdata.Checked)
                {
                    tabControlBodyType.SelectedTab = this.tabPageBodyFormData;
                }
            }
            catch (Exception ex)
            {
                AppendText(ex.Message);
            }
        }

        private void radioCheckfile_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (radioCheckfile.Checked)
                {
                    tabControlBodyType.SelectedTab = this.tabPageBodyFile;
                }
            }
            catch (Exception ex)
            {
                AppendText(ex.Message);
            }
        }

        private void chkResponseAutoClear_Click(object sender, EventArgs e)
        {
            try
            {
                chkResponseAutoClear.Checked = !chkResponseAutoClear.Checked;
                if (chkResponseAutoClear.Checked)
                {
                    this.chkResponseAutoClear.Image = global::CFusion.Post.Properties.Resources.postmain_responsecheckrememberresult;
                }
                else
                {
                    this.chkResponseAutoClear.Image = global::CFusion.Post.Properties.Resources.postmain_responsecheckrememberresultUn;
                }
            }
            catch (Exception ex)
            {
                AppendText(ex.Message);
            }

        }

        private void tabControl2_Selected(object sender, TabControlEventArgs e)
        {
            try
            {
                if (e.TabPage == tabPageHistory)
                {
                    LoadHistory();
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void treeViewHistory_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                string file = e.Node.Tag.ToString();
                if (System.IO.File.Exists(file))
                {
                    this.txtHistoryContents.Text = Feng.IO.FileHelper.ReadAllText(file);
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void btnImport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                using (Feng.Forms.Dialogs.InputMultilineDialog dlg = new Feng.Forms.Dialogs.InputMultilineDialog())
                {
                    dlg.ShowIcon = true;
                    dlg.Icon = this.FindForm().Icon;
                    dlg.btnOk.Text = "Ok(&O)";
                    dlg.btnCancel.Text = "Cancel(&C)";
                    dlg.Text = "Input Json";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        ModelPostData modelPostData = JsonHelper.DeserializeObject<ModelPostData>(dlg.txtInput.Text);
                        this.LoadItem(modelPostData);
                    }
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void btnRename_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string url = this.txtUrl.Text.TrimEnd().TrimEnd('/');
                System.Uri uri = new Uri(url);
                string path = uri.Host.Replace('.', '_');
                if (!(uri.Port == 443 || uri.Port == 80))
                {
                    path = path + "__" + uri.Port;
                }
                path = path + "\\" + uri.LocalPath.Replace('/', '_');
                txtLocationPath.Text = path;
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void txtUrl_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            txtUrl.Multiline = true;
            txtUrl.Height = txtUrl.Height * 2;
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog dlg = new OpenFileDialog())
                {
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        this.txtFilePath.Text = System.IO.Path.GetFileName(dlg.FileName);
                        this.txtFileLocationPath.Text = dlg.FileName;
                    }
                }
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void btnPaste_Click(object sender, EventArgs e)
        {
            try
            {
                string url = System.Windows.Forms.Clipboard.GetText();
                this.txtUrl.Text = url;
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        public string GenerateCode(ModelPostData modelPostData)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("var url=" + "\"" + StringEscapeUtility.EscapeSpecialCharacters(modelPostData.Url) + "\"" + ";");
            sb.AppendLine("var contentType=" + "\"" + StringEscapeUtility.EscapeSpecialCharacters(modelPostData.ContentType) + "\"" + ";");
            sb.AppendLine("var content=" + "\"" + StringEscapeUtility.EscapeSpecialCharacters(modelPostData.RawData) + "\"" + ";");
            sb.AppendLine("var dictheader=dictnew();");
            foreach (var item in modelPostData.Headers)
            {
                sb.AppendLine("dictadd(dictheader," + item.Key + "," + item.Value + ");");
            }
            sb.AppendLine("var dictformdata=dictnew();");
            foreach (var item in modelPostData.ModelPostDataBody.Dataes)
            {
                sb.AppendLine("dictadd(dictformdata,\"" + item.Key + "\",\"" + item.Value + "\");");
            }
            switch (txtMethod.Text)
            {
                case "GET":
                    sb.AppendLine("var response=HttpGet(url,dictheader);");
                    break;
                case "POST":
                    if (radioCheckfile.Checked)
                    {
                        sb.AppendLine("var serverpath=" + "\"" + StringEscapeUtility.EscapeSpecialCharacters(modelPostData.ModelPostDataBody.FileSeverPath) + "\"" + ";");
                        sb.AppendLine("var locationpath=" + "\"" + StringEscapeUtility.EscapeSpecialCharacters(modelPostData.ModelPostDataBody.FileLocationPath) + "\"" + ";");
                        sb.AppendLine("var response=HttpPostFile(url,dictheader,locationpath,serverpath);");
                    }
                    if (this.radioCheckformdata.Checked)
                    {
                        sb.AppendLine("var response=HttpPostFormData(url,dictheader,dictformdata);");
                    }
                    if (this.radioCheckraw.Checked)
                    {
                        sb.AppendLine("var response=HttpPostRaw(url,dictheader,content,contentType);");
                    }
                    break;
                case "PUT":
                    break;
                    sb.AppendLine("var response=HttpPut(url,dictheader,content,contentType);");
                case "DELETE":
                    sb.AppendLine("var response=HttpDelete(url,dictheader,content,contentType);");
                    break;
                case "HEAD":
                    sb.AppendLine("var response=HttpHead(url,dictheader);");
                    break;
                case "OPTIONS":
                    sb.AppendLine("var response=HttpOptions(url,dictheader);");
                    break;
                case "TRACE":
                    sb.AppendLine("var response=HttpTrace(url,dictheader);");
                    break;
                case "PATCH":
                    sb.AppendLine("var response=HttpPatch(url,dictheader,content,contentType);");
                    break;
                default:
                    break;
            }
            string code = sb.ToString();
            return code;
        }

        private void btnDebugGenerateCode_Click(object sender, EventArgs e)
        {
            try
            {
                ModelPostData modelPostData = GetModelPostData();
                string script = GenerateCode(modelPostData);
                txtScriptCode.Text = script;
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        NetParser netParser = new NetParser();
        private bool initscriptparser = false;
        public void InitScriptParser()
        {
            if (initscriptparser)
                return;
            initscriptparser = true;
            netParser.AddFunction(new Feng.Script.FunctionContainer.DebugMethodContainer());
            netParser.AddFunction(new Feng.Script.FunctionContainer.NotificationMethodContainer());
            netParser.AddFunction(new Feng.Script.FunctionContainer.CFusionMethodContainer());
            netParser.AddFunction(new Feng.Script.FunctionContainer.DateTimeFunctionContainer());
            netParser.AddFunction(new Feng.Script.FunctionContainer.CollectionFunctionContainer());
            netParser.AddFunction(new Feng.Script.FunctionContainer.HttpToolMethodContainer());

        }
        private void btnDebugStart_Click(object sender, EventArgs e)
        {
            try
            {
                InitScriptParser();
                netParser.Debug(this.txtScriptCode.Text);
                netParser.debug.DebugEvent -= Debug_DebugEvent;
                netParser.debug.DebugEvent += Debug_DebugEvent;


                netParser.debug.DebugExceptionEvent -= Debug_DebugExceptionEvent;
                netParser.debug.DebugExceptionEvent += Debug_DebugExceptionEvent;
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void Debug_DebugExceptionEvent(object sender, Exception ex)
        {
            try
            {
                AppendText(ex.Message);
            }
            catch (Exception e)
            {
                Log(ex);
            }
        }

        private void Debug_DebugEvent(object sender, DebugEventArgs e)
        {
            try
            {
                AppendText(e.CurrentStatement.ToString() + "\r\n" + e.VarName + ":" + e.Value);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void btnDebugStep_Click(object sender, EventArgs e)
        {
            try
            {
                netParser.debug.SetCommand(DebugCommand.StepInto);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void btnDebugStepOut_Click(object sender, EventArgs e)
        {
            try
            {
                netParser.debug.SetCommand(DebugCommand.StepOut);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void btnDebugStop_Click(object sender, EventArgs e)
        {
            try
            {
                netParser.debug.SetCommand(DebugCommand.Stop);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }
    }


    public class ModelDictItem
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public string Remark { get; set; }
    }

    public class ModelPostData
    {
        public string Url { get; set; }
        public string LocationPath { get; set; }
        public string Method { get; set; }
        public string Timeout { get; set; }
        public string Setting { get; set; }
        public string RawData { get; set; }
        public string ContentType { get; set; }
        public string Script { get; set; }
        public List<ModelDictItem> Headers { get; set; }

        public ModelPostDataBody ModelPostDataBody { get; set; }
        public string FileSeverPath { get; set; }
        public string FileLocationPath { get; set; }
    }

    public class ModelPostDataBody
    {
        public string BodyType { get; set; }
        public List<ModelDictItem> Dataes { get; set; }
        public string RawData { get; set; }
        public string FileSeverPath { get; set; }
        public string FileLocationPath { get; set; }

    }


}
