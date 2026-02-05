//using Feng.Utils;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading;
//using System.Windows.Forms;

//namespace Feng.Assistant
//{
//    public partial class frmSysEventMainFormTree : Form
//    {
//        public frmSysEventMainFormTree()
//        {
//            InitializeComponent();
//        }


//        List<SysEventsFile> listevents = new List<SysEventsFile>();
//        public SysEventsFile GetSelectEventFile()
//        {
//            if (this.treeView1.SelectedNode == null)
//                return null;
//            SysEventsFile node = this.treeView1.SelectedNode.Tag as SysEventsFile;
//            return node;
//        }

//        public void Save(string filename)
//        {
// listevents.
//        }

//        private void btnNewScript_Click(object sender, EventArgs e)
//        { 
//            try
//            {
//                this.Hide();
//                using (MouseClickGetPostionForm frm = new MouseClickGetPostionForm())
//                {
//                    frm.TopMost = true;
//                    frm.Init();
//                    SysEventsFile node = new SysEventsFile();

//                    if (node == null)
//                        return;
//                    if (frm.ShowDialog() == DialogResult.OK)
//                    {
//                        listevents.Clear();
//                        node.Times = 500;
//                        node.Events = frm.listevents;
//                        listevents.Add(node);
//                        int index = Feng.IO.Setting.Default.GetInt("FILEINDEX");
//                        index++;
//                        node.Name = "Eve" + index.ToString();
//                        Feng.IO.Setting.Default.SetValue("FILEINDEX",index);
//                        Feng.IO.Setting.Default.Save();
//                        ReFreshList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                this.Visible = true;
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }
//            this.Visible = true;
//        }
//        public const string fileexExten = ".ffff";
//        public const string evetfileExten = ".evef";

//        private void btnSave_Click(object sender, EventArgs e)
//        {

//            try
//            {
//                using (SaveFileDialog dlg = new SaveFileDialog())
//                {
//                    dlg.Filter = "*" + fileexExten + "|*" + fileexExten + "";
//                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
//                    {
//                        Save(dlg.FileName);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }

//        }

//        public void ReFreshList()
//        {
//            this.treeView1.Nodes.Clear();
//            foreach (SysEventsFile fevent in listevents)
//            {
//                TreeNode fnode = this.treeView1.Nodes.Add(fevent.ToString());
//                fnode.Tag = fevent;
//                foreach (SysEvents eve in fevent.Events)
//                {
//                    TreeNode node = fnode.Nodes.Add(eve.ToString());
//                    node.Tag = eve;
//                }
//            }
//        }

//        public void LoadLastFile()
//        {
//            string file = Feng.IO.Setting.Default.GetString("LastFile");
//            if (!string.IsNullOrWhiteSpace(file))
//            {
//                OpenFile(file);
//            }
//        }

//        public void Read(string file)
//        {
//            byte[] data = System.IO.File.ReadAllBytes(file);
//            using (Feng.IO.BufferReader read = new IO.BufferReader(data))
//            {
//                int fcount = read.ReadInt32();
//                for (int fi = 0; fi < fcount; fi++)
//                {
//                    SysEventsFile fevet = new SysEventsFile();
//                    int count = read.ReadInt32();
//                    byte[] fdata = read.ReadBytes();
//                    fevet.Read(fdata);
//                    listevents.Add(fevet);
//                    for (int i = 0; i < count; i++)
//                    {
//                        string text = read.ReadString();
//                        SysEvents eve = this.GetType().Assembly.CreateInstance(text) as SysEvents;
//                        byte[] buffer = read.ReadBytes();
//                        if (eve != null)
//                        {
//                            eve.ReadData(buffer);
//                            fevet.Events.Add(eve);
//                        }
//                    }
//                }
//            }
//        }

//        public void OpenFile(string file)
//        {

//            try
//            {

//                listevents.Clear();
//                Feng.IO.Setting.Default.SetValue("LastFile", file);
//                Feng.IO.Setting.Default.Save();
//                Read(file);
//                ReFreshList();
//            }
//            catch (Exception ex)
//            {
//                listevents.Clear();
//                Feng.IO.LogHelper.Log(ex);
//            }
  
//        }

//        private void btnExport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
//        {

//            try
//            {
//                using (OpenFileDialog dlg = new OpenFileDialog())
//                {
//                    dlg.Filter = "*" + fileexExten + "|*" + fileexExten + "";
//                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
//                    {
//                        OpenFile(dlg.FileName);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }

//        }
 
//        private void btnAddAction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
//        {
//            try
//            {
//                this.Hide();
//                using (MouseClickGetPostionForm frm = new MouseClickGetPostionForm())
//                {
//                    frm.TopMost = true;
//                    frm.Init();
//                    SysEventsFile node = GetSelectEventFile();
//                    if (node == null)
//                        return;
//                    if (frm.ShowDialog() == DialogResult.OK)
//                    {
//                        node.Events.AddRange(frm.listevents.ToArray());
//                        ReFreshList();
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                this.Visible = true;
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }
//        }

//        private void ToolStripMenuItemInsertAction_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                ////////if (this.listView1.SelectedItems.Count > 0)
//                ////////{
//                ////////    SysEvents eve = this.listView1.SelectedItems[0].Tag as SysEvents;
//                ////////    this.Hide();

//                ////////    using (MouseClickGetPostionForm frm = new MouseClickGetPostionForm())
//                ////////    {
//                ////////        frm.TopMost = true;
//                ////////        frm.Init();
//                ////////        if (frm.ShowDialog() == DialogResult.OK)
//                ////////        {
//                ////////            int index = listevents.IndexOf(eve);
//                ////////            listevents.InsertRange(index, frm.listevents.ToArray());
//                ////////            ReFreshList();
//                ////////        }
//                ////////    }
//                ////////    this.Visible = true;
//                ////////}
//            }
//            catch (Exception ex)
//            {
//                this.Visible = true;
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }
//        }

//        private void ToolStripMenuItemDelAction_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                ////////if (this.listView1.SelectedItems.Count > 0)
//                ////////{
//                ////////    SysEvents eve = this.listView1.SelectedItems[0].Tag as SysEvents;
//                ////////    listevents.Remove(eve);
//                ////////    ReFreshList();

//                ////////}
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }
//        }

//        private void panel1_Click(object sender, EventArgs e)
//        {

//            try
//            {
//                //System.Threading.Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
//                //System.Windows.Forms.WebBrowser web = new WebBrowser();
//                //this.panel1.Controls.Add(web);
//                //web.Navigate("www.baidu.com");
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }

//        }

//        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
//        {
//            Application.Exit();
//        }

//        private void ToolStripMenuItemHide_Click(object sender, EventArgs e)
//        {
//            this.Hide();
//        }

//        private void ToolStripMenuItemShow_Click(object sender, EventArgs e)
//        {
//            this.Show();
//        }

//        Thread threadCurrent = null;
//        public void Begin()
//        {
//            if (threadCurrent != null)
//            {
//                if (threadCurrent.IsAlive)
//                {
//                    threadCurrent.Abort();
//                }
//            }
//            threadCurrent = new Thread(StartProc);
//            threadCurrent.IsBackground = true;
//            threadCurrent.Start();
//        }
 
//        void StartProc()
//        {
//            try
//            {  
//                foreach (SysEventsFile evefile in listevents)
//                {
//                    if (evefile.OnTime)
//                    {
//                        evefile.Start();
//                    }
//                }
//                foreach (SysEventsFile evefile in listevents)
//                {
//                    evefile.Start();
//                    while (!evefile.Finished)
//                    {
//                        System.Threading.Thread.Sleep(1300);
//                    }
//                } 
//            }
//            catch (Exception ex)
//            {
//                Feng.IO.LogHelper.Log(ex);
//            } 
//        }

//        private void btnBegin_Click(object sender, EventArgs e)
//        {

//            try
//            {
//                SysEventsFile.Pause = false;
//                btnBegin.Enabled = false;
//                btnPause.Enabled = true;
//                labStartTime.Text = DateTime.Now.ToString("MM-dd HH:mm");
//                Begin();
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }

//        }

//        private void btnImort_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
//        {

//            try
//            {
//                this.btnSave_Click(null, null);
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }

//        }

//        private void btnPause_Click(object sender, EventArgs e)
//        {
//            btnBegin.Enabled = true;
//            SysEventsFile.Pause = true;
//            btnPause.Enabled = false;
//        }

//        private void frmSysEventMainForm_Load(object sender, EventArgs e)
//        {

//            try
//            {
//                LoadLastFile();
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }

//        }

//        private void ToolStripMenuItemExecTimes_Click(object sender, EventArgs e)
//        {

//            try
//            {
//                if (this.treeView1.SelectedNode != null)
//                {
//                    SysEventsFile eventfile = this.treeView1.SelectedNode.Tag as SysEventsFile;
//                    using (InputTextDialog dlg = new InputTextDialog())
//                    {
//                        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
//                        {
//                            eventfile.Times = ConvertHelper.ToInt32(dlg.txtInput.Text, 30);
//                            this.treeView1.SelectedNode.Text = eventfile.ToString();
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex); 
//            }
  
//        }

//        private void ToolStripMenuItemExportScript_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (this.treeView1.SelectedNode != null)
//                {
//                    SysEventsFile eventfile = this.treeView1.SelectedNode.Tag as SysEventsFile;
//                    using (SaveFileDialog dlg = new SaveFileDialog())
//                    {
//                        dlg.Filter = "*" + evetfileExten + "|*" + evetfileExten + "";
//                        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
//                        {
//                            byte[] data = eventfile.GetData();
//                            System.IO.File.WriteAllBytes(dlg.FileName, data);
//                        }
//                    } 
//                }
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }
//        }

//        private void ToolStripMenuItemEditName_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (this.treeView1.SelectedNode != null)
//                {
//                    SysEventsFile eventfile = this.treeView1.SelectedNode.Tag as SysEventsFile;
//                    using (InputTextDialog dlg = new InputTextDialog())
//                    {
//                        if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
//                        {
//                            eventfile.Name = dlg.txtInput.Text;
//                            this.treeView1.SelectedNode.Text = eventfile.ToString();
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                Feng.Utils.ExceptionHelper.ShowError(ex);
//            }
//        }
//    }
//}
