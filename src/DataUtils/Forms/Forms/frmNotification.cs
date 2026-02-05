using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Feng.Forms
{
    public partial class frmNotification : Form
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        public frmNotification()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception)
            { 
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                this.timer1.Enabled = false;
                this.Close();
            }
            catch (Exception)
            {
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Opacity = this.Opacity - 0.01;
            }
            catch (Exception)
            {
            }
        }

        private void txtMsg_Click(object sender, EventArgs e)
        {
            try
            {

                this.timer1.Enabled = false;
                this.timer2.Enabled = false;
                Clipboard.SetText(this.txtMsg.Text);
            }
            catch (Exception)
            { 
            }
        }

        private void txtMsg_MouseHover(object sender, EventArgs e)
        {
            try
            {
                this.timer1.Enabled = false;
                this.timer2.Enabled = false;
                this.Opacity = 1;
            }
            catch (Exception)
            { 
            }

        }

        private void frmNotification_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
            catch (Exception)
            { 
            }

        }

        private void frmNotification_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                this.timer1.Enabled = false;
                this.timer2.Enabled = false;
                this.Opacity = 1;
            }
            catch (Exception)
            {
            }
        }

        private void frmNotification_Load(object sender, EventArgs e)
        {
            try
            { 
            }
            catch (Exception)
            { 
            }
        }

        private void frmNotification_Shown(object sender, EventArgs e)
        {
            try
            { 
            }
            catch (Exception)
            {
            }
        }

        private void picMsgType_Click(object sender, EventArgs e)
        {
            try
            { 
            }
            catch (Exception)
            {
            }
        }

        private void txtTitle_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
            catch (Exception)
            {
            }
        }

        private void txtMsg_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {

                this.timer1.Enabled = false;
                this.timer2.Enabled = false;
                Clipboard.SetText(this.txtMsg.Text);
            }
            catch (Exception)
            {
            }
        }

        private void txtMsg_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Left)
                {
                    this.timer1.Enabled = false;
                    this.timer2.Enabled = false;
                    Clipboard.SetText(this.txtMsg.Text);
                    ReleaseCapture();
                    SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
                }
            }
            catch (Exception)
            {
            }
        }

        private void btnClose_MouseEnter(object sender, EventArgs e)
        {
            this.btnClose.BackColor = Color.Red;
        }

        private void btnClose_MouseLeave(object sender, EventArgs e)
        {
            this.btnClose.BackColor = Color.Azure;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            try
            {
                System.Windows.Forms.Clipboard.SetText(this.txtMsg.Text);
            }
            catch (Exception)
            { 
            }
        }
    }
    public class NotificationStyle
    {
        public Color BackColor { get; set; }
        public Color TitleColor { get; set; }
        public Color MsgColor { get; set; }
    }
    public enum NotificationType
    {
        SUCCESS = 1,
        ERROR = 2,
        WARN = 3,
        INFO = 9
    }
    public class NotificationTool
    {
        private static NotificationTool instance = null;
        public static NotificationTool Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new NotificationTool();
                }
                return instance;
            }
        }
        public NotificationTool()
        {
            NotificationStyle = new NotificationStyle();
            NotificationStyle.BackColor = Color.Empty;
            NotificationStyle.TitleColor = Color.Empty;
            NotificationStyle.MsgColor = Color.Empty;
            Forms = new List<frmNotification>();
        }
        private List<frmNotification> Forms { get; set; }
        public Form MainForm { get; set; }
        public NotificationStyle NotificationStyle { get; private set; }
        private Point GetPoint(frmNotification frm)
        {
            Rectangle rect = System.Windows.Forms.Screen.PrimaryScreen.Bounds;
            int left = rect.Right - frm.Width - 10;
            int top = 10;
            for (int i = 0; i <= 100; i++)
            {
                Point point = new Point(left, top + 10) { };
                bool res = HasForm(frm, point);
                if (!res)
                {
                    return point;
                }
                top = top + frm.Height+10;

            }
            return new Point(left, 10);
        }
        private bool HasForm(frmNotification frm,Point pts)
        { 
            foreach (Form item in Forms)
            {
                if (item == null)
                    continue;
                if (item.Disposing)
                    continue;
                if (item.IsDisposed)
                    continue;
                if (!item.Visible)
                    continue;
                Rectangle rectt = new Rectangle(item.Left, item.Top, item.Width, item.Height);
                if (rectt.Contains(pts))
                {
                    return true;
                } 
            }
            return false;
        }
        public void Error(Form form, string title, string msg)
        {
            NotificationType type = NotificationType.ERROR;
            Show(form, type, title, msg);
        }
        public void Info(Form form, string title, string msg)
        {
            NotificationType type = NotificationType.INFO;
            Show(form, type, title, msg);
        }
        public void Success(Form form, string title, string msg)
        {
            NotificationType type = NotificationType.SUCCESS;
            Show(form, type, title, msg);
        }
        public void Warn(Form form, string title, string msg)
        {
            NotificationType type = NotificationType.WARN;
            Show(form, type, title, msg);
        }
        public void Error(string title, string msg)
        {
            NotificationType type = NotificationType.ERROR;
            Show(null, type, title, msg);
        }
        public void Error(Exception ex)
        {
            NotificationType type = NotificationType.ERROR;
            Show(null, type, ex.Message, ex.StackTrace);
        }
        public void Info(string title, string msg)
        {
            NotificationType type = NotificationType.INFO;
            Show(null, type, title, msg);
        }
        public void Success(string title, string msg)
        {
            NotificationType type = NotificationType.SUCCESS;
            Show(null, type, title, msg);
        }
        public void Warn(string title, string msg)
        {
            NotificationType type = NotificationType.WARN;
            Show(null, type, title, msg);
        }
        public void Show(Form form, NotificationType type, string title, string msg)
        {
            frmNotification frm = Create(type, title, msg);
            Point point = GetPoint(frm);
            frm.WindowState = FormWindowState.Normal;
            frm.StartPosition = FormStartPosition.Manual;
            frm.Left = point.X;
            frm.Top = point.Y;
            frm.TopMost = true;
            switch (type)
            {
                case NotificationType.SUCCESS:
                    frm.picMsgType .Image = global::Feng.Utils.Properties.Resources.Notification_success;
                    break;
                case NotificationType.ERROR:
                    frm.picMsgType.Image = global::Feng.Utils.Properties.Resources.Notification_error;
                    frm.timer1.Interval = 1000 * 15;
                    frm.timer2.Enabled = false;
                    break;
                case NotificationType.WARN:
                    frm.picMsgType.Image = global::Feng.Utils.Properties.Resources.Notification_warn;
                    frm.timer1.Interval = 1000 * 10;
                    frm.timer2.Interval = 300;
                    break;
                case NotificationType.INFO:
                    frm.picMsgType.Image = global::Feng.Utils.Properties.Resources.Notification_info;
                    break;
                default:
                    break;
            }
            this.Forms.Add(frm);
            if (form != null)
            {
                frm.Show(form);
            }
            else if (this.MainForm != null)
            {
                frm.Show(form);
            }
            else
            {
                frm.Show();
            }
        }
        private frmNotification Create(NotificationType type, string title, string msg)
        {
            frmNotification frm = new frmNotification();
            if (NotificationStyle.BackColor != Color.Empty)
            {
                frm.BackColor = NotificationStyle.BackColor;
            }
            if (NotificationStyle.TitleColor != Color.Empty)
            {
                frm.txtTitle.BackColor = NotificationStyle.TitleColor;
            }
            if (NotificationStyle.MsgColor != Color.Empty)
            {
                frm.txtMsg.BackColor = NotificationStyle.MsgColor;
            }
            frm.txtTitle.Text = title;
            frm.txtMsg.Text = msg;
            switch (type)
            {
                case NotificationType.ERROR:
                    break;
                case NotificationType.INFO:
                    break;
                case NotificationType.SUCCESS:
                    break;
                case NotificationType.WARN:
                    break;
            }
            return frm;
        }
    }
}

