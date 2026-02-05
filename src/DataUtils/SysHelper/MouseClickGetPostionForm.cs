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
    public class MouseClickGetPostionForm : Form
    {
        public MouseClickGetPostionForm()
        {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // MouseClickGetPostionForm
            // 
            this.BackColor = System.Drawing.Color.Blue;
            this.ClientSize = new System.Drawing.Size(708, 397);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "MouseClickGetPostionForm";
            this.Opacity = 0.5D;
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseClickGetPostionForm_MouseClick);
            this.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MouseClickGetPostionForm_MouseDoubleClick);
            this.ResumeLayout(false);

        }
        frmSysEventSet frm = null;
        public void Init()
        {

        }
        protected override void OnLoad(EventArgs e)
        {
            try
            {

                base.OnLoad(e);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }
        private bool firstshow = false;
        protected override void OnVisibleChanged(EventArgs e)
        {
            try
            {
                if (!this.Visible)
                {
                    frm.Dispose();
                    frm = null;
                }
                base.OnVisibleChanged(e);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }
        protected override void OnShown(EventArgs e)
        {
            try
            {

                frm = new frmSysEventSet();
                frm.FormClosed += new FormClosedEventHandler(frm_FormClosed); 
                this.AddOwnedForm(frm);
                frm.MainForm = this;
                frm.TopMost = true;
                frm.TopLevel = true;
                frm.Show();

                base.OnShown(e);
                firstshow = true;
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        public void ShowSet()
        {

        }

        void frm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = frm.DialogResult;
            this.Close();
        }
 
        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
 
        private void MouseClickGetPostionForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            { 
                try
                {
                    MouseClickSysEvents keyevents = new MouseClickSysEvents();
                    keyevents.Point = System.Windows.Forms.Control.MousePosition;
                    listevents.Add(keyevents);
                    this.Invalidate();
                }
                catch (Exception ex)
                {
                    Feng.Utils.ExceptionHelper.ShowError(ex);
                }

            }
        }
        bool show = true;
        protected override void OnMouseClick(MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                show = !show;
                this.Invalidate();
            }
            base.OnMouseClick(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {

            try
            {

                Graphics g = e.Graphics;
                int i = 0;
                foreach (SysEvents se in this.listevents)
                {
                    i++;
                    if (se is MouseClickSysEvents)
                    {
                        MouseClickSysEvents p = se as MouseClickSysEvents;
                        Rectangle rect = new Rectangle(p.Point.X - 5, p.Point.Y - 5, 10, 10);
                        g.FillRectangle(Brushes.Red, rect);
                        e.Graphics.DrawString(i.ToString(), this.Font, Brushes.Black, rect.Location);
                    }
                    else if (se is MouseDoubleClickSysEvents)
                    {
                        MouseDoubleClickSysEvents p = se as MouseDoubleClickSysEvents;
                        Rectangle rect = new Rectangle(p.Point.X - 5, p.Point.Y - 5, 10, 10);
                        g.FillRectangle(Brushes.Green, rect);
                        e.Graphics.DrawString(i.ToString(), this.Font, Brushes.Black, rect.Location);
                    }
                }
                int x = 240;
                int y = 100;
                int xx = 280;
                int h1 = 20;
                if (show)
                { 
                    e.Graphics.DrawString("使用帮助：(点左键关闭显示)", this.Font, Brushes.Red, x, y += 40);
                    y += h1;
                    e.Graphics.DrawString("1：点右键获取选中点，以红色方框显示，触发单击", this.Font, Brushes.Red, xx, y);
                    y += h1;
                    e.Graphics.DrawString("2：单击按钮发送相应按钮", this.Font, Brushes.Red, xx, y);
                    y += h1;
                    e.Graphics.DrawString("3：单击Text按钮输入发送内容", this.Font, Brushes.Red, xx, y);
                    y += h1;
                    e.Graphics.DrawString("4：发送图片或其他内容，事选Copy内容，使用CTL+V铵钮", this.Font, Brushes.Red, xx, y);
                    y += h1;
                    e.Graphics.DrawString("5：双击左键获取选中点，以绿色方框显示，触发双击", this.Font, Brushes.Red, xx, y);
                    y += h1;
                    e.Graphics.DrawString("6：开始结束一对中的内容将重复执行，按ctrl键点击每次加5，Alt键每次减5", this.Font, Brushes.Red, xx, y);
                }
                try
                {
                    for (i = 0; i < listevents.Count; i++)
                    {
                        y += h1;
                        string text = (i + 1).ToString() + ".执行【" + listevents[i].ToString() + "】" + listevents[i].ToolTip;
                        e.Graphics.DrawString(text, this.Font, Brushes.Red, xx, y);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            catch (Exception ex)
            { 
            }
        
            base.OnPaint(e);
        }

        private void MouseClickGetPostionForm_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            { 
                MouseDoubleClickSysEvents keyevents = new MouseDoubleClickSysEvents();
                keyevents.Point = System.Windows.Forms.Control.MousePosition;
                listevents.Add(keyevents);
                this.Invalidate(); 
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            } 
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        public SysEventsCollection listevents = new SysEventsCollection();
        public void btnCtl_A_Click(object sender, EventArgs e)
        {

            try
            {
                KeySysEvents keyevents = new KeySysEvents();
                keyevents.Text = "^(a)";
                listevents.Add(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        
        }

        public void btnCtl_C_Click(object sender, EventArgs e)
        {

            try
            {
                KeySysEvents keyevents = new KeySysEvents();
                keyevents.Text = "^(c)";
                listevents.Add(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        public void btnAlt_C_Click(object sender, EventArgs e)
        {
            try
            {
                KeySysEvents keyevents = new KeySysEvents();
                keyevents.Text = "%(c)";
                listevents.Add(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        public void btnEnter_Click(object sender, EventArgs e)
        {
       try
            {
                KeySysEvents keyevents = new KeySysEvents();
                keyevents.Text = "{ENTER}";
                listevents.Add(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        public void btnDown_Click(object sender, EventArgs e)
        {
            try
            {
                KeySysEvents keyevents = new KeySysEvents();
                keyevents.Text = "{DOWN}";
                listevents.Add(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        public void btnTab_Click(object sender, EventArgs e)
        {
            try
            {
                KeySysEvents keyevents = new KeySysEvents();
                keyevents.Text = "{TAB}";
                listevents.Add(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        public void btnText_Click(object sender, EventArgs e)
        {
            try
            {
                using (InputDialogSendkeys dlg = new InputDialogSendkeys())
                {
                    dlg.TopMost = true;
                    if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        Feng.Forms.ClipboardHelper.SetText(dlg.txtInput.Text);
                        KeySysEvents keyevents = new KeySysEvents();
                        keyevents.Text = "^(v)";
                        listevents.Add(keyevents);
                    }
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            } 
        }

        public void btnAlt_S_Click(object sender, EventArgs e)
        {
            try
            {
                KeySysEvents keyevents = new KeySysEvents();
                keyevents.Text = "%(s)";
                listevents.Add(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }

        public void btnCtl_V_Click(object sender, EventArgs e)
        {

            try
            {
                KeySysEvents keyevents = new KeySysEvents();
                keyevents.Text = "^(v)";
                listevents.Add(keyevents);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
        }
 
 
    }


}
