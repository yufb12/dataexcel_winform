using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Excel.Extend
{
    public class RightKeyExtend
    {
        public void Init(DataExcel grid)
        {
            ShowRigthKeyMenu = true; 
            InitKey();
        }
        System.Windows.Forms.ContextMenuStrip popmenu = new System.Windows.Forms.ContextMenuStrip();
        public void InitKey()
        { 
            ArraryItem arr = new ArraryItem();
            arr.Init("RIGHTKEY", "文件", "新建", "CommandNew", "", "");
            arr.Init("RIGHTKEY", "文件", "打开", "CommandOpen", "", "");
            arr.Init("RIGHTKEY", "单元格", "合并单元格", "CommandMergeCell", "", "");
            arr.Init("RIGHTKEY", "单元格", "取消合并单元格", "CommandMergeCell", "", "");
            arr.Init("RIGHTKEY", "全局", "只读", "CommandGridReadOnly", "", "");
            Dictionary<string, ToolStripMenuKeyValueItem> dicsmenu = new Dictionary<string, ToolStripMenuKeyValueItem>();
            foreach (ArraryItem item in arr.Items)
            {
                ToolStripMenuKeyValueItem itemmenu = null;
                string key = item.Category + item.Key;
                if (!dicsmenu.ContainsKey(key))
                {
                    itemmenu = new ToolStripMenuKeyValueItem(item.Key);
                    popmenu.Items.Add(itemmenu);
                }
                else
                {
                    itemmenu = dicsmenu[key];
                }
                ToolStripMenuKeyValueItem menu = new ToolStripMenuKeyValueItem(item.Value);
                itemmenu.DropDownItems.Add(menu);
            }
        }
        public bool ShowRigthKeyMenu { get; set; }
        void grid_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {

            try
            {
                if (this.ShowRigthKeyMenu) 
                {
                    popmenu.Show(e.Location);
                }
            }
            catch (Exception ex)
            {
                Feng.Utils.ExceptionHelper.ShowError(ex);
            }
  
        }

        public class ToolStripMenuKeyValueItem : System.Windows.Forms.ToolStripMenuItem
        {
            public ToolStripMenuKeyValueItem(string text)
                : base(text)
            {

            }
            public string Key { get; set; }
            public string Command { get; set; }
        }

        public class ArraryItem
        {  
            public string Category { get; set; }
            public string Key { get; set; }
            public string Value { get; set; }
            public string Command { get; set; }
            public string Image { get; set; }
            public string Remark { get; set; }
            public void Init(string category, string key, string value, string command, string image, string remark)
            {
                ArraryItem item = null; 
                item = new ArraryItem();
                item.Category = category;
                item.Key = key;
                item.Value = value;
                item.Command = command;
                item.Image = image;
                item.Remark = remark;
                Items.Add(item);
            }
            public List<ArraryItem> Items = new List<ArraryItem>();
        }
 
  
    }
}