using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Utils
{
    public class MsgBox
    {
        public static void ShowInfomation(string text)
        {
            try
            {
                System.Windows.Forms.MessageBox.Show(text, Feng.App.Systeminfo.ApplicationName, System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information);
            }
            catch (Exception ex)
            { 
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        public static void ShowWarning(string text)
        {
            try
            {
                System.Windows.Forms.MessageBox.Show(text, Feng.App.Systeminfo.ApplicationName, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }

        }

        public static System.Windows.Forms.DialogResult ShowQuestion(string text)
        {
            try
            {
                System.Windows.Forms.DialogResult drl = System.Windows.Forms.MessageBox.Show(text,
                    Feng.App.Systeminfo.ApplicationName, 
                    System.Windows.Forms.MessageBoxButtons.OKCancel, 
                    System.Windows.Forms.MessageBoxIcon.Question);
                return drl;
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
                return System.Windows.Forms.DialogResult.Abort;
            }

        }

        public static System.Windows.Forms.DialogResult ShowOk(string text)
        {
            try
            {
                
                System.Windows.Forms.DialogResult drl = System.Windows.Forms.MessageBox.Show(text, Feng.App.Systeminfo.ApplicationName, System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Question);
                return drl;
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
                return System.Windows.Forms.DialogResult.Abort;
            }

        }
    }

}
