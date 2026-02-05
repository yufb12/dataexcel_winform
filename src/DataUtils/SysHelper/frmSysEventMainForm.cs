using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Feng.Assistant
{
    public partial class frmSysEventMainForm : Form
    {
        public frmSysEventMainForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
        SysEventsCollection listevents = new SysEventsCollection();
        private void button3_Click(object sender, EventArgs e)
        {

            try
            {
                this.Hide();
                using (MouseClickGetPostionForm frm = new MouseClickGetPostionForm())
                {
                    frm.TopMost = true;
                    frm.Init();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        listevents = frm.listevents;
                        ReFreshList();
                        string filename =  Feng.IO.FileHelper.GetStartUpFileUSER("SimulatedMouse", "\\" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".tfd");
                        listevents.Save(filename); 
                    }
                }
                this.Visible = true;
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }


        }

        bool lck = false;
        int icount = 0;
        void timer1_Tick(object sender, EventArgs e)
        {
            if (lck)
                return;
            try
            {
                if (icount >= this.txtEventCount.Value)
                {
                    return;
                }
                icount++;
                lck = true;
                if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
                {
                    System.Threading.Thread.Sleep(3000);
                    return;
                }
                if ((DateTime.Now - dtStartTime).TotalMinutes > 30)
                {
                    dtStartTime = dtStartTime.AddMinutes(2);
                }
                foreach (SysEvents p in listevents)
                {
                    p.Excute();
                    System.Threading.Thread.Sleep(300);
                }
                System.Threading.Thread.Sleep(300);
                SendKeys.Flush();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            finally
            {
                lck = false;
            }
        }
        bool lckn = false;
        private void txtEventCount_ValueChanged(object sender, EventArgs e)
        {

            try
            {
                if (lckn)
                    return;
                lckn = true;
                if (this.txtEventCount.Value >= this.txtEventCount.Maximum)
                {

                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

            lckn = false;
        }
        DateTime dtStartTime = DateTime.Now;
        private void MouseClickTestForm_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "*.tfd|*.tfd";
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        listevents.Save(dlg.FileName); 
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }
        public void ReFreshList()
        {
            this.listView1.Items.Clear();
            int index = 1;
            foreach (SysEvents eve in listevents)
            {
                ListViewItem item = this.listView1.Items.Add((index++).ToString() + ":" + eve.ToString());
                item.Tag = eve;
            }
        }
        public void LoadLastFile()
        {
            string file = Feng.IO.Setting.Default.GetString("LastFile");
            if (!string.IsNullOrWhiteSpace(file))
            {
                OpenFile(file);
            }
        }
        public void OpenFile(string file)
        {
            listevents.Clear(); 
            listevents.Read(file);
            Feng.IO.Setting.Default.SetValue("LastFile", file);
            Feng.IO.Setting.Default.Save(); 
            ReFreshList();
        }
        private void btnExport_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {
                System.Diagnostics.Process.Start("http://www.dataexcel.cn/");
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void btnMKSys_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {

            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void MouseClickTestForm_Click(object sender, EventArgs e)
        {

            try
            {
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void btnAddAction_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                this.Hide();
                using (MouseClickGetPostionForm frm = new MouseClickGetPostionForm())
                {
                    frm.TopMost = true;
                    frm.Init();
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        listevents.AddRange(frm.listevents.ToArray());
                        ReFreshList();
                    }
                }
                this.Visible = true;
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void ToolStripMenuItemInsertAction_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listView1.SelectedItems.Count > 0)
                {
                    SysEvents eve = this.listView1.SelectedItems[0].Tag as SysEvents;
                    this.Hide();

                    using (MouseClickGetPostionForm frm = new MouseClickGetPostionForm())
                    {
                        frm.TopMost = true;
                        frm.Init();
                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            int index = listevents.IndexOf(eve);
                            listevents.InsertRange(index, frm.listevents.ToArray());
                            ReFreshList();
                        }
                    }
                    this.Visible = true;
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void ToolStripMenuItemDelAction_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.listView1.SelectedItems.Count > 0)
                {
                    SysEvents eve = this.listView1.SelectedItems[0].Tag as SysEvents;
                    listevents.Remove(eve);
                    ReFreshList();

                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void panel1_Click(object sender, EventArgs e)
        {

            try
            {
                //System.Threading.Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
                //System.Windows.Forms.WebBrowser web = new WebBrowser();
                //this.panel1.Controls.Add(web);
                //web.Navigate("www.baidu.com");
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void ToolStripMenuItemExit_Click(object sender, EventArgs e)
        {
            Feng.IO.LogHelper.Log("Exit", "10004");
            Application.Exit();
        }

        private void ToolStripMenuItemHide_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ToolStripMenuItemShow_Click(object sender, EventArgs e)
        {
            this.Show();
        }
        //73-68-75-74-64-6F-77-6E
        //

        int allcount = 100;
        Thread threadCurrent = null;
        public void Begin()
        {
            if (threadCurrent != null)
            {
                if (threadCurrent.IsAlive)
                {
                    threadCurrent.Abort();
                }
            }
            threadCurrent = new Thread(StartProc);
            threadCurrent.IsBackground = true;
            threadCurrent.Start();
        }
        private bool Pause = false;
        private bool Stop = false;
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Pause = true;
            Stop = true;
            this.timer1.Enabled = false;
            base.OnFormClosing(e);
        }
        void StartProc()
        {
            while (!Stop)
            {
                try
                {
                    if (icount >= allcount)
                    {
                        return;
                    }
                    if (Pause)
                    {
                        System.Threading.Thread.Sleep(300);
                        continue;
                    }
                    if (System.Windows.Forms.Control.ModifierKeys == Keys.Control
     || System.Windows.Forms.Control.ModifierKeys == Keys.Shift
     || System.Windows.Forms.Control.ModifierKeys == Keys.Alt)
                    {
                        System.Threading.Thread.Sleep(1000 * 10);
                        continue;
                    }
                    icount++;

                    foreach (SysEvents p in listevents)
                    {
                        if (System.Windows.Forms.Control.ModifierKeys == Keys.Control
                            || System.Windows.Forms.Control.ModifierKeys == Keys.Shift
                            || System.Windows.Forms.Control.ModifierKeys == Keys.Alt)
                        {
                            System.Threading.Thread.Sleep(1000 * 10);
                            break;
                        }
                        if (Stop)
                            return;
                        p.Excute();
                        System.Threading.Thread.Sleep(300);
                        if (Pause)
                        {
                            break;
                        }
                    }
                    System.Threading.Thread.Sleep(300);
                    SendKeys.Flush();
                }
                catch (Exception ex)
                {
                    Feng.IO.LogHelper.Log(ex);
                }
            }
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {

            try
            {
                Pause = false;
                btnBegin.Enabled = false;
                btnPause.Enabled = true;
                labStartTime.Text = DateTime.Now.ToString("MM-dd HH:mm");
                allcount = (int)txtEventCount.Value;
                Begin();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void btnImort_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            try
            {
                this.btnSave_Click(null, null);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            btnBegin.Enabled = true;
            Pause = true;
            btnPause.Enabled = false;
        }

        private void frmSysEventMainForm_Load(object sender, EventArgs e)
        {

            try
            {
                LoadLastFile();
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }
    }
}
