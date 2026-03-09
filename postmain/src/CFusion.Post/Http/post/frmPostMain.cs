using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tools;

namespace CFusion.Http.post
{
    public partial class frmPostMain : Form
    {
        public frmPostMain()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        string projectpath = "Projects";
 
        public void InitTree()
        {
            ProjectItem rootitem = new ProjectItem() { Name ="ROOT", Category = "ROOT", Text = "ROOT", Path= projectpath };
            InitProject(rootitem, projectpath);
            InitTree(rootitem);
            this.treeProject.ExpandAll();
        }
        public void InitTree(ProjectItem rootitem)
        {
            try
            {
                this.treeProject.Nodes.Clear();
                TreeNode treeNode = this.treeProject.Nodes.Add(rootitem.Text);
                treeNode.ImageKey = rootitem.Category;
                treeNode.Tag = rootitem;
                foreach (ProjectItem item in rootitem.Items)
                {
                    TreeNode node = treeNode.Nodes.Add(item.Text);
                    node.ImageKey = item.Category;
                    node.Tag = item;
                    InitTree(node, item);
                }
            }
            catch (Exception)
            { 
            }
        }
        public void InitTree(TreeNode treeNode,ProjectItem itemp)
        {
            try
            { 
                foreach (ProjectItem item in itemp.Items)
                {
                    TreeNode node = treeNode.Nodes.Add(item.Text);
                    node.ImageKey = item.Category;
                    node.Tag = item;
                    InitTree(node, item);
                }
            }
            catch (Exception)
            {
            }
        }
        private void InitProject(ProjectItem rootitem,string projectpath)
        {
            try
            {
                if (!System.IO.Directory.Exists(projectpath))
                    return;
                string[] directories = System.IO.Directory.GetDirectories(projectpath);
                foreach (string directory in directories)
                {
                    System.IO.DirectoryInfo directoryInfo = new System.IO.DirectoryInfo(directory);
                    ProjectItem projectItem = new ProjectItem()
                    {
                        Category = "DIRECTORY",
                        Name = directoryInfo.Name,
                        Path = directory,
                        Tag = null,
                        Text = directoryInfo.Name
                    };
                    rootitem.Items.Add(projectItem);
                    InitProject(projectItem, directory);
                }

                string[] files = System.IO.Directory.GetFiles(projectpath);
                foreach (string file in files)
                {
                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);
                    if (fileInfo.Extension != ".json")
                    {
                        continue;
                    }
                    if (!fileInfo.Name.EndsWith ( ".pro.json"))
                    {
                        continue;
                    }
                    ProjectItem projectItem = new ProjectItem()
                    {
                        Category = "FILE",
                        Name = fileInfo.Name,
                        Path = file,
                        Tag = null,
                        Text =fileInfo .Name .Substring (0, fileInfo.Name.Length - ".pro.json".Length)
                    };
                    rootitem.Items.Add(projectItem);
                }
            }
            catch (Exception)
            {
            }

        }

        private void frmPostMain_Load(object sender, EventArgs e)
        {
            try
            {
                ctrlPostControl.RootPath = projectpath; 
                InitTree();
                ClearPage();
                btnNodeAddFile_Click(sender ,e);
                new DsUpdater().Run();
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void ClearPage()
        {
            this.tabControlPage.TabPages.Clear();
        }
        private void AddNode(TreeNode treeNode)
        {
            ProjectItem item = new ProjectItem();
            TreeNode node = treeNode.Nodes.Add(item.Text);
            node.Tag = item;
        }

        private void btnNodeAddFile_Click(object sender, EventArgs e)
        {
            try
            { 
                System.Windows.Forms.TabPage tabPage = new TabPage();
                tabPage.Text = "New *";
                this.tabControlPage.Controls.Add(tabPage);
                ctrlPostControl ctrlPostControl = new ctrlPostControl();
                ctrlPostControl.SaveClick += CtrlPostControl_SaveClick;
                ctrlPostControl.Dock = DockStyle.Fill;
                tabPage.Controls.Add(ctrlPostControl);
                ProjectItem newfileitem = new ProjectItem()
                {
                    Category = string.Empty,
                    Name = string.Empty, 
                    FullPath = string.Empty,
                    Tag = null,
                    Text = string.Empty
                };
                ctrlPostControl.Init(newfileitem);
                this.tabControlPage.SelectedTab = tabPage;
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void CtrlPostControl_SaveClick(object sender, EventArgs e)
        {
            try
            {
                ctrlPostControl ctrlPostControl = sender as ctrlPostControl;
                System.Windows.Forms.TabPage tabPage = ctrlPostControl.Parent as System.Windows.Forms.TabPage;
                if (tabPage == null)
                    return;
                tabPage.Text = ctrlPostControl.GetTitle();
                btnProjectTreeRefresh_Click(sender, e);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(projectpath);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }
        public void Log(Exception ex)
        {

        }

        private void btnProjectTreeRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                ctrlPostControl.RootPath = projectpath;
                InitTree();
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void btnClosePage_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControlPage.SelectedTab == null)
                    return;
                tabControlPage.TabPages.Remove(tabControlPage.SelectedTab);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void treeProject_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                TreeNode node = e.Node;
                if (node == null)
                    return;
                ProjectItem item = node.Tag as ProjectItem;
                if (item.Category != "FILE")
                    return;
                System.Windows.Forms.TabPage tabPage = new TabPage();
                tabPage.ImageIndex = 2;
                tabPage.Text = item.Text;
                this.tabControlPage.Controls.Add(tabPage);
                ctrlPostControl ctrlPostControl = new ctrlPostControl();

                ctrlPostControl.SaveClick += CtrlPostControl_SaveClick;
                ctrlPostControl.Dock = DockStyle.Fill;
                tabPage.Controls.Add(ctrlPostControl); 
                this.tabControlPage.SelectedTab = tabPage;
                ctrlPostControl.Init(item);
            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void btnNodeRemove_Click(object sender, EventArgs e)
        {
            try
            {
                TreeNode node = this.treeProject.SelectedNode;
                if (node == null)
                    return;
                ProjectItem item = node.Tag as ProjectItem;
                if (item.Category == "DIRECTORY")
                {
                    if (MessageBox.Show("Are you sure  want to delete the folder:" + item.Path + " ?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        bool res = RecycleHelper.DeleteToRecycleBin(item.Path);
                        if (res)
                        {
                            node.Remove();
                        }
                    }
                }
                if (item.Category == "FILE")
                {
                    if (MessageBox.Show("Are you sure  want to delete the file:" + item.Path + " ?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        bool res = RecycleHelper.DeleteToRecycleBin(item.Path);
                        if (res)
                        {
                            node.Remove();
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Log(ex);
            }
        }

        private void btnUrl_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start(btnUrl.Text);
            }
            catch (Exception)
            { 
            }
        }

        private void toolStripStatusLabel5_Click(object sender, EventArgs e)
        {
            try
            {
                System.Diagnostics.Process.Start("https://github.com/yufb12/dataexcel_winform");
            }
            catch (Exception)
            {
            }
        }
    }

    public class ProjectItem
    {
        public ProjectItem()
        {
            Items = new List<ProjectItem>();
        }
        public string Name { get; set;}
        public string Text { get; set; }
        public string Path { get; set; }
        public string Category { get; set; }
        public object Tag { get; set; }

        public List<ProjectItem> Items { get; private set; }
        public string FullPath { get; internal set; }
    }
}
