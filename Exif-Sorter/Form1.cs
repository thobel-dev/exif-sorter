using System.Data;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace Exif_Sorter
{
    public partial class Form1 : Form
    {
        // source folder of images
        string sourcePathName = @"Z:\Bilder\S9\OpenCamera";

        // target folder of images
        string targetPathName = @"C:\Temp\Bilder_Sortiert";

        public Form1()
        {
            InitializeComponent();
            labelSourcePathName.Text = sourcePathName;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // disable button
            button1.Enabled = false;

            // Do performance measure for populating dataTable
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // Performance issues in DataGridView
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            // or even better, use .DisableResizing. Most time consuming enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders

            // set it to false if not needed
            dataGridView1.RowHeadersVisible = false;

            // Array of imagefiles
            string[] imageFiles;

            // ID-Code for ExifDT
            Int32 imageIndex = 36867;

            // datatable for DataGridView
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Elternordner");
            dataTable.Columns.Add("Dateiname");
            dataTable.Columns.Add("Aufnahmedatum");
            dataTable.Columns.Add("Zielordner");
            dataTable.Columns.Add("ZielordnerName");
            dataTable.Columns.Add("NeuerZielordner");

            // Put images into treeview - order
            TreeNode topNode = new TreeNode(sourcePathName);
            List<TreeNode> lstAufnahmedatum = new List<TreeNode>();
            List<TreeNode> lstImages = new List<TreeNode>();

            // list imagefiles of pathName
            if (Directory.Exists(sourcePathName))
            {
                // TODO - respect subfolders
                imageFiles = Directory.GetFiles(sourcePathName);
            }
            else
            {
                button1.Enabled = true;
                MessageBox.Show($"Folder {sourcePathName} not found.");
                throw new Exception($"Folder {sourcePathName} not found.", new DirectoryNotFoundException());
            }

            // Now we know how many images to process - set progressbar
            progressBarVerarbeitung.Minimum = 1;
            progressBarVerarbeitung.Maximum = imageFiles.Length;
            progressBarVerarbeitung.Value = 1;
            progressBarVerarbeitung.Step = 1;

            if (imageFiles.Length > 0)
            {
                // Globally define Encoding
                ASCIIEncoding enc = new ASCIIEncoding();

                foreach (var imageFile in imageFiles)
                {
                    //FileInfo fileInfo = new FileInfo(imageFile);
                    if (imageFile.ToLower().EndsWith(".jpg") || imageFile.ToLower().EndsWith(".jpeg"))
                    {
                        Image image = Image.FromFile(imageFile);
                        PropertyItem propertyItem = image.GetPropertyItem(imageIndex);
                        string dateTakenText = enc.GetString(propertyItem.Value, 0, propertyItem.Len - 1);
                        if (!String.IsNullOrEmpty(dateTakenText))
                        {
                            DateTime dateTaken;
                            if (DateTime.TryParseExact(dateTakenText, "yyyy:MM:dd HH:mm:ss",
                                CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTaken))
                            {
                                // write to grid
                                DataRow imageEntry = dataTable.NewRow();
                                imageEntry["Elternordner"] = sourcePathName;
                                imageEntry["Dateiname"] = imageFile;
                                imageEntry["Aufnahmedatum"] = dateTaken;
                                imageEntry["Zielordner"] = Path.Combine(targetPathName, dateTaken.Date.ToString("yyyy-MM-dd"));
                                dataTable.Rows.Add(imageEntry);
                                image.Dispose();

                                labelAnzahl.Text = $"Anzahl verarbeiteter Bilder: {dataTable.Rows.Count} von {imageFiles.Length}";
                                progressBarVerarbeitung.PerformStep();
                                if ((dataTable.Rows.Count % 5) == 0)
                                {
                                    Refresh();
                                }
                            }
                        }
                    }
                }

                // sort DataTable based on Aufnahmedatum
                dataTable.DefaultView.Sort = "Aufnahmedatum";

                //// Iterate dataSource for treeview
                treeViewImages.Nodes.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    // Add top level element
                    if (!treeViewImages.Nodes.ContainsKey(row["Zielordner"].ToString()))
                    {
                        TreeNode currentNode = new TreeNode();
                        currentNode.Name = row["Zielordner"].ToString();
                        currentNode.Text = row["Zielordner"].ToString();
                        treeViewImages.Nodes.Add(currentNode);
                    }

                    // add current image as child
                    TreeNode childNode = new TreeNode();
                    childNode.Name = row["Dateiname"].ToString();
                    childNode.Text = row["Dateiname"].ToString();

                    // tags for properties needed in datagridview
                    ImageEntryObject imageEntryObject = new ImageEntryObject(row["Aufnahmedatum"].ToString(),
                        row["Dateiname"].ToString(),
                        row["Elternordner"].ToString(), "", "");
                    childNode.Tag = imageEntryObject;

                    treeViewImages.Nodes[row["Zielordner"].ToString()].Nodes.Add(childNode);

                    labelStatusTreeview.Text = $"{treeViewImages.Nodes.Count} Aufnahmedaten mit {dataTable.Rows.Count} Bildern";
                }
            }
            else
            {
                MessageBox.Show($"No files to process found in {sourcePathName}");
                button1.Enabled = true;
                throw new Exception($"No files to process found in {sourcePathName}", new Exception());
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            labelBenoetigteZeit.Text = $"Benötigte Zeit: {(elapsedMs / 1000).ToString()}s";
            labelBenoetigteZeit.Visible = true;
            // dataGridView1.DataSource = dataTable;
            treeViewImages.CheckBoxes = true;
            button1.Enabled = true;
        }

        private void treeViewImages_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count > 0)
            {
                foreach (TreeNode node in e.Node.Nodes)
                {
                    node.Checked = e.Node.Checked;
                }
            }
        }

        private void treeViewImages_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            if (e.Node?.Nodes.Count > 0)
            {
                // No Datasource Set
                DataTable dataTable = new DataTable();
                if (dataGridView1.Rows.Count == 0)
                {
                    // Datatable for selected picture group
                    // datatable for DataGridView
                    dataTable.Columns.Add("Elternordner");
                    dataTable.Columns.Add("Dateiname");
                    dataTable.Columns.Add("Aufnahmedatum");
                    dataTable.Columns.Add("Zielordner");
                    dataTable.Columns.Add("NeuerZielordner");
                    dataTable.PrimaryKey = new DataColumn[] { dataTable.Columns["Dateiname"] };
                    textBoxZielordnername.Text = $"{e.Node.Name}";

                }
                else
                {
                    dataTable = dataGridView1.DataSource as DataTable;
                }

                foreach (TreeNode node in e.Node.Nodes)
                {
                    ImageEntryObject imageEntry = node.Tag as ImageEntryObject;
                    DataRow currentRowItem = dataTable.NewRow();
                    currentRowItem["Dateiname"] = node.Name;
                    currentRowItem["Zielordner"] = e.Node?.Name;
                    currentRowItem["Aufnahmedatum"] = imageEntry.Aufnahmedatum.Substring(0, 10);
                    currentRowItem["Elternordner"] = imageEntry.Elternordner;
                    currentRowItem["NeuerZielordner"] = textBoxZielordnername.Text;

                    if (!dataTable.Rows.Contains(node.Name))
                    {
                        dataTable.Rows.Add(currentRowItem);
                    }
                }

                dataGridView1.DataSource = dataTable;
                labelStatusleisteDataGridView.Text = $"Die Auswahl enthält {dataTable.Rows.Count} Bilder";
            }

        }

        private void treeViewImages_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            // Left click
            if (e.Button == MouseButtons.Left)
            {
                if (e.Node != null && e.Node.Nodes.Count > 0)
                {
                    // coordinates for first picturebox
                    int x = 20;
                    int y = 20;

                    imageList1.ImageSize = new Size(255, 135);
                    imageList1.Images.Clear();

                    // clear pictureboxes from panel
                    panelImages.Controls.Clear();

                    foreach (TreeNode node in e.Node.Nodes)
                    {
                        try
                        {
                            PictureBox pb = new PictureBox();
                            pb.Image = Image.FromFile(node.Name);
                            pb.Location = new Point(x, y);
                            pb.SizeMode = PictureBoxSizeMode.StretchImage;
                            pb.Size = new Size(255, 135);
                            pb.Click += (s, e) =>
                            {
                                pictureBox1.Load(node.Text);
                            };
                            x += pb.Width + 10;
                            panelImages.Controls.Add(pb);
                        }
                        catch { }
                    }
                    labelStatusleisteBildVorschau.Text = $"Das Aufnahmedatum {e.Node.Name} enthält {panelImages.Controls.Count} Bilder";
                }
                else
                {
                    try
                    {
                        pictureBox1.Load(e.Node.Text);
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void btnMoveToFolder_Click(object sender, EventArgs e)
        {
            // move checked images to targetFolder of parent
            foreach (TreeNode node in treeViewImages.Nodes)
            {
                recurseNodes(node);
            }
        }

        private void recurseNodes(TreeNode node)
        {
            if (node.Nodes.Count > 0)
            {
                foreach (TreeNode newNode in node.Nodes)
                {
                    recurseNodes(newNode);
                }

            }
            else
            {
                if (node.Checked)
                {
                    string fileName = Path.GetFileName(node.Name);
                    if (!Directory.Exists(node.Parent.Name))
                    {
                        Directory.CreateDirectory(node.Parent.Name);
                    }

                    File.Copy(node.Name, Path.Combine(node.Parent.Name, fileName), false);

                    // remove copied image from treeview
                    treeViewImages.Nodes.Remove(node);

                    // TODO - potentially save list of current state of uncopied images
                }
            }
        }

        private void ordnerÖffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            sourcePathName = folderBrowserDialog1.SelectedPath;
            labelSourcePathName.Text = sourcePathName;
        }

        private void buttonDeleteSelection_Click(object sender, EventArgs e)
        {
            DataTable dataTable = dataGridView1.DataSource as DataTable;
            dataTable.Rows.Clear();
            // dataGridView1.DataSource = dataTable;
            // dataTable.Dispose();
            labelStatusleisteDataGridView.Text = $"Keine Bilder ausgewählt";
        }

        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //dataGridViewMenu.Show(dataGridView1, new Point(e.X, e.Y));
                ContextMenuStrip cm = new ContextMenuStrip();

                ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem();

                // mark current row as selected row
                var hti = dataGridView1.HitTest(e.X, e.Y);
                dataGridView1.ClearSelection();
                dataGridView1.CurrentCell = null;
                dataGridView1.Rows[hti.RowIndex].Selected = true;

                this.ContextMenuStrip = cm;
                cm.Items.Add(new ToolStripMenuItem("&Entfernen", null, new System.EventHandler(this.entfernen_Click)));
                cm.Items.Add(new ToolStripMenuItem("&Aufnahmedatum entfernen", null, new System.EventHandler(this.aufnahmedatumEntfernen_Click)));

                cm.Show(dataGridView1, new Point(e.X, e.Y));
            }
        }

        private void entfernen_Click(object sender, EventArgs e)
        {
            int rowIndexToDelete = dataGridView1.Rows.GetFirstRow(DataGridViewElementStates.Selected);

            if (rowIndexToDelete >= 0 && rowIndexToDelete < dataGridView1.Rows.Count)
            {
                dataGridView1.Rows.RemoveAt(rowIndexToDelete);
            }

            dataGridView1.ClearSelection();

            labelStatusleisteDataGridView.Text = $"Die Auswahl enthält {dataGridView1.Rows.Count} Bilder";
        }

        private void aufnahmedatumEntfernen_Click(Object sender, EventArgs e)
        {
            // datasource of dataGridView
            DataTable dataTable = dataGridView1.DataSource as DataTable;

            // rowId of clicked row
            int rowIndexToDelete = dataGridView1.Rows.GetFirstRow(DataGridViewElementStates.Selected);

            // get Aufnahmedatum of clicked row
            string aufnahmedatum = dataGridView1.Rows[rowIndexToDelete].Cells["Aufnahmedatum"].Value.ToString();
            DataColumn dataColumn = dataTable.Columns[2];

            // since MS documentation specifies not to delete datatable-row while iterating - generate separate list
            List<DataRow> rowsToBeDeleted = new List<DataRow>();

            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                if (dataTable.Rows[i]["Aufnahmedatum"].ToString() == aufnahmedatum)
                {
                    //dataTable.Rows[i].Delete();
                    rowsToBeDeleted.Add(dataTable.Rows[i]);
                }
            }

            // iterate separate List and delete rows
            for (int i = 0; i < rowsToBeDeleted.Count; i++)
            {
                dataTable.Rows.Remove(rowsToBeDeleted[i]);
            }
            dataTable.AcceptChanges();

            labelStatusleisteDataGridView.Text = $"Die Auswahl enthält {dataTable.Rows.Count} Bilder";
        }

        private class ImageEntryObject
        {
            public ImageEntryObject(string aufnahmedatum, string dateiname, string elternordner, string zielordner, string neuerZielordner)
            {
                Aufnahmedatum = aufnahmedatum;
                Dateiname = dateiname;
                Elternordner = elternordner;
                Zielordner = zielordner;
                NeuerZielordner = neuerZielordner;
            }

            public string Aufnahmedatum { get; set; }
            public string Dateiname { get; set; }
            public string Elternordner { get; set; }
            public string Zielordner { get; set; }
            public string NeuerZielordner { get; set; }

        }

        private void buttonZielordnername_Click(object sender, EventArgs e)
        {
            changeZielordnernameByTextbox();
        }

        private void textBoxZielordnername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                changeZielordnernameByTextbox();
            }
        }

        private void changeZielordnernameByTextbox()
        {
            foreach (DataGridViewRow imageEntryRow in dataGridView1.Rows)
            {
                try
                {
                    imageEntryRow.Cells["NeuerZielordner"].Value = $"{Path.Combine(targetPathName, textBoxZielordnername.Text)}";
                }
                catch { }
            }
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            DataTable dataTable = dataGridView1.DataSource as DataTable;
            try
            {
                // List of nodes to be deleted from treeview after copy
                List<TreeNode> nodesToBeDeletedAfterCopy = new List<TreeNode>();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    string fileName = Path.GetFileName(dataTable.Rows[i]["Dateiname"].ToString());
                    if (!Directory.Exists(dataTable.Rows[i]["NeuerZielordner"].ToString()))
                    {
                        Directory.CreateDirectory(dataTable.Rows[i]["NeuerZielordner"].ToString());
                    }

                    string fileNameAndPath = Path.Combine(dataTable.Rows[i]["NeuerZielordner"].ToString(), fileName);
                    //File.Copy(dataTable.Rows[i]["Dateiname"].ToString(), Path.Combine(dataTable.Rows[i]["NeuerZielordner"].ToString(), fileName), false);

                    // TODO - delete copied images from treeview
                    nodesToBeDeletedAfterCopy.AddRange(treeViewImages.Nodes.Find(dataTable.Rows[i]["Dateiname"].ToString(), true));
                }

                // Delete all childnodes that have been copied
                foreach (var node in nodesToBeDeletedAfterCopy)
                {
                    node.Remove();
                }

                // delete all nodes without children - nodes should not be deleted in iteration
                // because it messes up the index and nodes get skipped
                nodesToBeDeletedAfterCopy.Clear();
                foreach (TreeNode node in treeViewImages.Nodes)
                {
                    if (node?.Nodes.Count == 0)
                    {
                        nodesToBeDeletedAfterCopy.Add(node);
                    }
                }

                foreach (TreeNode node in nodesToBeDeletedAfterCopy)
                {
                    node.Remove();
                }

                labelStatusTreeview.Text = $"{treeViewImages.Nodes.Count} Aufnahmedaten mit {treeViewImages.GetNodeCount(true) - treeViewImages.Nodes.Count} Bildern";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw new Exception(ex.Message);
            }
        }
    }
}