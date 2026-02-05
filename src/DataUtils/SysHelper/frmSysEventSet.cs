using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Feng.App;
using Feng.Utils;
using Feng.Forms.Dialogs;

namespace Feng.Assistant
{
    public partial class frmSysEventSet : Form
    { 
        public frmSysEventSet()
        {
            InitializeComponent();
        }
 

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        public List<Point> Plist = new List<Point>();

        private void MouseClickGetPostionForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Plist.Add(System.Windows.Forms.Control.MousePosition);
                this.Invalidate();
                try
                {
                    MouseClickSysEvents keyevents = new MouseClickSysEvents();
                    keyevents.Point = System.Windows.Forms.Control.MousePosition;
                    AddEvent(keyevents);
                }
                catch (Exception ex)
                {
                    Feng.Utils.ExceptionHelper.ShowError(ex);
                }

            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (Point p in Plist)
            {
                Rectangle rect = new Rectangle(p.X - 5, p.Y - 5, 10, 10);
                g.FillRectangle(Brushes.Red, rect);
            }
            base.OnPaint(e);
        }

        private void MouseClickGetPostionForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        public MouseClickGetPostionForm MainForm { get; set; }
        public SysEventsCollection listevents
        {
            get
            {
                return MainForm.listevents;
            }
        }
        public void AddEvent(SysEvents sysevent)
        {
            listevents.Add(sysevent);
            MainForm.Invalidate();
        }
        private void btnCtl_A_Click(object sender, EventArgs e)
        {

            try
            {
                KeySysEvents keyevents = new KeySysEvents();
                keyevents.Text = "^(a)";
                keyevents.ToolTip = (sender as Control).Text;
                AddEvent(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void btnCtl_C_Click(object sender, EventArgs e)
        {

            try
            {
                KeySysEvents keyevents = new KeySysEvents();
                keyevents.Text = "^(c)";
                keyevents.ToolTip = (sender as Control).Text;
                AddEvent(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void btnAlt_C_Click(object sender, EventArgs e)
        {
            try
            {
                KeySysEvents keyevents = new KeySysEvents();
                keyevents.Text = "%(c)";
                keyevents.ToolTip = (sender as Control).Text;
                AddEvent(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            try
            {
                KeySysEvents keyevents = new KeySysEvents();
                keyevents.Text = "{ENTER}";
                keyevents.ToolTip = (sender as Control).Text;
                AddEvent(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                KeySysEvents keyevents = new KeySysEvents();
                keyevents.Text = "{DOWN}";
                keyevents.ToolTip = (sender as Control).Text;
                AddEvent(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void btnTab_Click(object sender, EventArgs e)
        {
            try
            {
                KeySysEvents keyevents = new KeySysEvents();
                keyevents.Text = "{TAB}";
                keyevents.ToolTip = (sender as Control).Text;
                AddEvent(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void btnText_Click(object sender, EventArgs e)
        {
            try
            {
                using (InputDialogSendkeys dlg = new InputDialogSendkeys())
                {
                    dlg.TopMost = true;
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        KeySysEvents keyevents = new KeySysEvents();
                        keyevents.Text = dlg.txtInput.Text;
                        keyevents.ToolTip = dlg.txtInput.Text;
                        AddEvent(keyevents);
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void btnAlt_S_Click(object sender, EventArgs e)
        {
            try
            {
                KeySysEvents keyevents = new KeySysEvents();
                keyevents.Text = "%(s)";
                keyevents.ToolTip = (sender as Control).Text;
                AddEvent(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void btnCtl_V_Click(object sender, EventArgs e)
        {

            try
            {
                KeySysEvents keyevents = new KeySysEvents();
                keyevents.Text = "^(v)";
                keyevents.ToolTip = (sender as Control).Text;
                AddEvent(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }
        public void Init()
        {
            //UnsafeNativeMethods.GetWindowLong(this.Handle, UnsafeNativeMethods.GWL_EXSTYLE);
            //UnsafeNativeMethods.SetWindowLong(this.Handle, UnsafeNativeMethods.GWL_EXSTYLE, UnsafeNativeMethods.WS_EX_LAYERED);
            //UnsafeNativeMethods.SetLayeredWindowAttributes(this.Handle, System.Drawing.ColorTranslator.ToWin32(this.BackColor), 255, UnsafeNativeMethods.LWA_ALPHA | UnsafeNativeMethods.LWA_COLORKEY);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Init();
        }

        private void button11_Click(object sender, EventArgs e)
        {

            try
            {
                Button btn = sender as Button;
                if (btn != null)
                {
                    KeySysEvents keyevents = new KeySysEvents();
                    keyevents.Text = ConvertHelper.ToString(btn.Tag);
                    keyevents.ToolTip = (sender as Control).Text;
                    AddEvent(keyevents);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void button17_Click(object sender, EventArgs e)
        {
            try
            {

                MouseWheelUpSysEvents keyevents = new MouseWheelUpSysEvents();
                keyevents.ToolTip = (sender as Control).Text;
                AddEvent(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            try
            {

                MouseWheelDownSysEvents keyevents = new MouseWheelDownSysEvents();
                keyevents.ToolTip = (sender as Control).Text;
                AddEvent(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }
        int index = 0;
        private void button19_Click(object sender, EventArgs e)
        {
            try
            {

                MainForm.Hide();
                MainForm.Visible = false;
                this.Hide();
                this.Visible = false;
                for (int i = index; i < this.listevents.Count; i++)
                {
                    this.listevents[i].Excute();
                    index = i;
                }
                MainForm.Show();
                MainForm.Visible = true;
                this.Show();
                this.Visible = true;
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void button20_Click(object sender, EventArgs e)
        {
            try
            {

                SpaceEvents keyevents = new SpaceEvents();
                keyevents.Interval = 1;
                keyevents.ToolTip = (sender as Control).Text;
                AddEvent(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            try
            {

                SpaceEvents keyevents = new SpaceEvents();
                keyevents.Interval = 5;
                keyevents.ToolTip = (sender as Control).Text;
                AddEvent(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.Hide();
                MainForm.Visible = false;
                this.Hide();
                this.Visible = false;
                using (frmRandom frm = new frmRandom())
                {
                    frm.TopMost = true;
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        RandomEvents keyevents = new RandomEvents();
                        if (frm.radCount.Checked)
                        {
                            keyevents.RandomType = 1;
                        }
                        if (frm.radRandom.Checked)
                        {
                            keyevents.RandomType = 2;
                        }
                        if (frm.radTime.Checked)
                        {
                            keyevents.RandomType = 3;
                        }

                        if (frm.radText.Checked)
                        {
                            keyevents.RandomType = 4;
                        }
                        AddEvent(keyevents);
                    }
                }
                MainForm.Show();
                MainForm.Visible = true;
                this.Show();
                this.Visible = true;
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void btnVersion_Click(object sender, EventArgs e)
        {
            try
            {
                using (InputTextDialog dlg = new InputTextDialog())
                {
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        CloseProcessEvents keyevents = new CloseProcessEvents();
                        keyevents.ProcessName = dlg.txtInput.Text;
                        keyevents.ToolTip = (sender as Control).Text;
                        AddEvent(keyevents);
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void btnSpit_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmTimeSet dlg = new frmTimeSet())
                {
                    dlg.TopMost = true;
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        SpaceEvents keyevents = new SpaceEvents();
                        int time = ConvertHelper.ToInt32(dlg.txtTimes.Text, 1) * ConvertHelper.ToInt32(dlg.txtUnit.Text, 30);
                        keyevents.Interval = time;
                        keyevents.ToolTip = (sender as Control).Text;
                        AddEvent(keyevents);
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void btnCurrentTime_Click(object sender, EventArgs e)
        {
            try
            {
                CurrentTimeEvents keyevents = new CurrentTimeEvents();
                keyevents.ToolTip = (sender as Control).Text;
                AddEvent(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            try
            {

                SpaceEvents keyevents = new SpaceEvents();
                keyevents.Interval = 3;
                keyevents.ToolTip = (sender as Control).Text;
                AddEvent(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            try
            {

                SpaceEvents keyevents = new SpaceEvents();
                keyevents.Interval = 5;
                keyevents.ToolTip = (sender as Control).Text;
                AddEvent(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            try
            {
                ShutDownEvents keyevents = new ShutDownEvents();
                keyevents.ToolTip = (sender as Control).Text;
                AddEvent(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        private void button33_Click(object sender, EventArgs e)
        {
            try
            {
                MainForm.Visible = false;
                this.Visible = false;
                ImageClickSysEvent keyevents = new ImageClickSysEvent();
                using (Feng.Forms.ScreenForm dlg = new Forms.ScreenForm())
                {
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        keyevents.ClickImage = dlg.GetSelectImage();
                        keyevents.ToolTip = (sender as Control).Text;
                        AddEvent(keyevents);
                    }
                } 
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
            finally
            { 
                MainForm.Visible = true;
                this.Visible = true;
            }
        }

        private void buttonHide_Click(object sender, EventArgs e)
        {
            try
            {
                if (buttonHide.Text != "显示")
                {
                    MainForm.Hide();
                }
                else
                {
                    buttonHide.Text = "显示";
                    MainForm.Show();
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }
        SysBeginEvent currentBeginEvent = null;
        private void button34_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentBeginEvent == null)
                {
                    currentBeginEvent = new SysBeginEvent();
                    AddEvent(currentBeginEvent);
                }
                if (System.Windows.Forms.Control.ModifierKeys == Keys.Control)
                {
                    currentBeginEvent.Count = currentBeginEvent.Count + 5;
                }
                else if (System.Windows.Forms.Control.ModifierKeys == Keys.Shift)
                {
                    currentBeginEvent.Count = currentBeginEvent.Count + 15;
                }
                else if (System.Windows.Forms.Control.ModifierKeys == Keys.Alt)
                {
                    currentBeginEvent.Count = currentBeginEvent.Count - 5;
                }
                else
                {
                    currentBeginEvent.Count = currentBeginEvent.Count + 1;
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentBeginEvent != null)
                {
                    SysEndEvent endevent = new  SysEndEvent();
                    AddEvent(endevent);
                    currentBeginEvent = null;
                } 
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }
    }


}
