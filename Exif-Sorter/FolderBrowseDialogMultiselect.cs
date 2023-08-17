using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Exif_Sorter
{
    public partial class FolderBrowseDialogMultiselect : Form
    {
        private string _folder = "";
        internal List<string> _folders = new List<string>();
        public FolderBrowseDialogMultiselect(string sourceFolder, List<string> openFolders)
        {
            InitializeComponent();
            //string pathNameFromMain = _folder;
            //pathNameFromMain = @"Z:\Bilder\S9";
            string[] folders = Directory.GetDirectories(sourceFolder);

            foreach (string folder in folders)
            {
                TreeNode newFolderNode = new TreeNode();
                newFolderNode.Text = folder;
                newFolderNode.Name = folder;
                newFolderNode.Checked = openFolders != null && openFolders.Contains(folder);
                treeViewFolderSelect.Nodes.Add(newFolderNode);
            }
        }

        private void buttonOpenFolders_Click(object sender, EventArgs e)
        {

            // Take all checked nodes or the currently selected
            foreach (TreeNode folderNode in treeViewFolderSelect.Nodes)
            {
                if (folderNode.Checked)
                {
                    _folders.Add(folderNode.Name.ToString());
                }

            }
            this.Visible = false;
        }

        private void buttonFolderCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        internal void setFolder(string folder)
        { 
            _folder = folder;
        }
    }
}
