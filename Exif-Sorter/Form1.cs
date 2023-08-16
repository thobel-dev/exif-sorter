using System.ComponentModel;
using System.Data;
using System.Drawing.Imaging;
using System.Globalization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Exif_Sorter.Models;

namespace Exif_Sorter
{
    public partial class Form1 : Form
    {
        // source folder of images
        string sourcePathName = @"Z:\Bilder\S9\";

        // target folder of images
        string targetPathName = @"C:\Temp\Bilder_Sortiert";

        // global dataTable with current state of treeview
        DataTable treeviewDataTable = new DataTable();

        // list of opened folders
        List<string> _openFolders;

        public Form1()
        {
            InitializeComponent();
            InitializeBackgroundWorker();
            labelSourcePathName.Text = sourcePathName;

            // Backgroundworker process for loading images
            backgroundWorkerGetImages.WorkerReportsProgress = true;
            backgroundWorkerGetImages.WorkerSupportsCancellation = true;

        }

        private void buttonRefreshImages_Click(object sender, EventArgs e)
        {
            // changed to worker and helper function
            if (!backgroundWorkerGetImages.IsBusy)
            {
                // disable button
                button1.Enabled = false;

                // Performance issues in DataGridView
                dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
                // or even better, use .DisableResizing. Most time consuming enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders

                // set it to false if not needed
                dataGridView1.RowHeadersVisible = false;

                backgroundWorkerGetImages.RunWorkerAsync();
            }

            //// disable button
            //button1.Enabled = false;

            //// Do performance measure for populating dataTable
            //var watch = System.Diagnostics.Stopwatch.StartNew();

            //// Performance issues in DataGridView
            //dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            //// or even better, use .DisableResizing. Most time consuming enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders

            //// set it to false if not needed
            //dataGridView1.RowHeadersVisible = false;

            //// Array of imagefiles
            //string[] imageFiles;

            //// ID-Code for ExifDT
            //Int32 imageIndex = 36867;

            //// datatable for DataGridView
            //DataTable dataTable = new DataTable();
            //dataTable.Columns.Add("Elternordner");
            //dataTable.Columns.Add("Dateiname");
            //dataTable.Columns.Add("Aufnahmedatum");
            //dataTable.Columns.Add("Zielordner");
            //dataTable.Columns.Add("ZielordnerName");
            //dataTable.Columns.Add("NeuerZielordner");

            //// Put images into treeview - order
            //TreeNode topNode = new TreeNode(sourcePathName);
            //List<TreeNode> lstAufnahmedatum = new List<TreeNode>();
            //List<TreeNode> lstImages = new List<TreeNode>();

            //// list imagefiles of pathName
            //if (Directory.Exists(sourcePathName))
            //{
            //    // TODO - respect subfolders
            //    imageFiles = Directory.GetFiles(sourcePathName);
            //}
            //else
            //{
            //    button1.Enabled = true;
            //    MessageBox.Show($"Folder {sourcePathName} not found.");
            //    throw new Exception($"Folder {sourcePathName} not found.", new DirectoryNotFoundException());
            //}

            //// Now we know how many images to process - set progressbar
            //progressBarVerarbeitung.Minimum = 1;
            //progressBarVerarbeitung.Maximum = imageFiles.Length;
            //progressBarVerarbeitung.Value = 1;
            //progressBarVerarbeitung.Step = 1;

            //if (imageFiles.Length > 0)
            //{
            //    // Globally define Encoding
            //    ASCIIEncoding enc = new ASCIIEncoding();

            //    foreach (var imageFile in imageFiles)
            //    {
            //        //FileInfo fileInfo = new FileInfo(imageFile);
            //        if (imageFile.ToLower().EndsWith(".jpg") || imageFile.ToLower().EndsWith(".jpeg"))
            //        {
            //            Image image = Image.FromFile(imageFile);
            //            PropertyItem propertyItem = image.GetPropertyItem(imageIndex);
            //            string dateTakenText = enc.GetString(propertyItem.Value, 0, propertyItem.Len - 1);
            //            if (!String.IsNullOrEmpty(dateTakenText))
            //            {
            //                DateTime dateTaken;
            //                if (DateTime.TryParseExact(dateTakenText, "yyyy:MM:dd HH:mm:ss",
            //                    CultureInfo.CurrentCulture, DateTimeStyles.None, out dateTaken))
            //                {
            //                    // write to grid
            //                    DataRow imageEntry = dataTable.NewRow();
            //                    imageEntry["Elternordner"] = sourcePathName;
            //                    imageEntry["Dateiname"] = imageFile;
            //                    imageEntry["Aufnahmedatum"] = dateTaken;
            //                    imageEntry["Zielordner"] = Path.Combine(targetPathName, dateTaken.Date.ToString("yyyy-MM-dd"));
            //                    dataTable.Rows.Add(imageEntry);
            //                    image.Dispose();

            //                    progressBarVerarbeitung.PerformStep();
            //                    if ((dataTable.Rows.Count % 5) == 0)
            //                    {
            //                        // Do performance measure for populating dataTable
            //                        var elapsedTimeSinceStart = watch.Elapsed;
            //                        var timePerPicture = (elapsedTimeSinceStart.TotalMilliseconds / 1000) / dataTable.Rows.Count;
            //                        var timeRemaining = timePerPicture * (imageFiles.Length - dataTable.Rows.Count);

            //                        labelAnzahl.Text = $"Anzahl verarbeiteter Bilder: {dataTable.Rows.Count} von {imageFiles.Length} - geschätzte verbleibende Zeit: {Math.Round(timeRemaining, 0)}";
            //                        Refresh();
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    // sort DataTable based on Aufnahmedatum
            //    dataTable.DefaultView.Sort = "Aufnahmedatum";

            //    //// Iterate dataSource for treeview
            //    treeViewImages.Nodes.Clear();
            //    foreach (DataRow row in dataTable.Rows)
            //    {
            //        // Add top level element
            //        if (!treeViewImages.Nodes.ContainsKey(row["Zielordner"].ToString()))
            //        {
            //            TreeNode currentNode = new TreeNode();
            //            currentNode.Name = row["Zielordner"].ToString();
            //            currentNode.Text = row["Zielordner"].ToString();
            //            treeViewImages.Nodes.Add(currentNode);
            //        }

            //        // add current image as child
            //        TreeNode childNode = new TreeNode();
            //        childNode.Name = row["Dateiname"].ToString();
            //        childNode.Text = row["Dateiname"].ToString();

            //        // tags for properties needed in datagridview
            //        ImageEntryObject imageEntryObject = new ImageEntryObject(row["Aufnahmedatum"].ToString(),
            //            row["Dateiname"].ToString(),
            //            row["Elternordner"].ToString(), "", "");
            //        childNode.Tag = imageEntryObject;

            //        treeViewImages.Nodes[row["Zielordner"].ToString()].Nodes.Add(childNode);

            //        labelStatusTreeview.Text = $"{treeViewImages.Nodes.Count} Aufnahmedaten mit {dataTable.Rows.Count} Bildern";
            //    }
            //}
            //else
            //{
            //    MessageBox.Show($"No files to process found in {sourcePathName}");
            //    button1.Enabled = true;
            //    throw new Exception($"No files to process found in {sourcePathName}", new Exception());
            //}

            //watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;
            //labelBenoetigteZeit.Text = $"Benötigte Zeit: {(elapsedMs / 1000).ToString()}s";
            //labelBenoetigteZeit.Visible = true;
            //// dataGridView1.DataSource = dataTable;
            //treeViewImages.CheckBoxes = true;
            //button1.Enabled = true;
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
            // check if images were copied and list needs to be cleared
            if (buttonMove.Enabled == false)
            {
                DataTable dataTable = dataGridView1.DataSource as DataTable;
                dataTable.Rows.Clear();
                buttonMove.Enabled = true;
            }

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

        private void ordnerOeffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////folderBrowserDialog1.ShowDialog();
            ////sourcePathName = folderBrowserDialog1.SelectedPath;
            //openFileDialogTreeviewState.Multiselect = true;
            //openFileDialogTreeviewState.ShowDialog();
            ////string[] files;
            //openFolders = openFileDialogTreeviewState.FileNames;

            ////labelSourcePathName.Text = sourcePathName;
            ///
            FolderBrowseDialogMultiselect folderBrowseDialogMultiselect = new FolderBrowseDialogMultiselect(sourcePathName);
            // folderBrowseDialogMultiselect.setFolder(sourcePathName);
            folderBrowseDialogMultiselect.ShowDialog();
            _openFolders = folderBrowseDialogMultiselect._folders;
            folderBrowseDialogMultiselect.Close();

        }

        private void treeviewSpeichernToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialogTreeviewState.ShowDialog();
            
            string saveFilename = saveFileDialogTreeviewState.FileName;

            //DataSet dataSet = new DataSet();
            //dataSet.Tables.Add(treeviewDataTable);
            //dataSet.WriteXml(saveFilename);
            treeviewDataTable.WriteXml(saveFilename);

            //TextWriter writer = new StreamWriter(saveFilename);
            //try
            //{
            //    //using (Stream file = File.Open(saveFilename, FileMode.Create))
            //    //{
            //    //    BinaryFormatter bf = new BinaryFormatter();
            //    //    bf.Serialize(file, treeViewImages.Nodes.Cast<TreeNode>().ToList());
            //    //}
            //    //string xmlSerialization = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n";
            //    writer.Write("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\n<root>\n");
            //    // iterate root nodes
            //    foreach (TreeNode node in treeViewImages.Nodes)
            //    {
            //        serializeTreeNodeInclTags(node, 1, writer);
            //    }
            //    writer.Write("</root>");
                
            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //finally
            //{
            //    writer.Close();
            //}
            
        }

        private void treeviewLadenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialogTreeviewState.ShowDialog();
            string openFilename = openFileDialogTreeviewState.FileName;

            //DataSet dataSet = new DataSet();

            //dataSet.ReadXml(openFilename);
            //treeviewDataTable = dataSet.Tables[0];
            treeviewDataTable.ReadXml(openFilename);
            populateTreeViewByDataTable(treeviewDataTable);

            //// Copied from Stackoverflow - to be customized
            //XmlDataDocument xmldoc = new XmlDataDocument();
            //XmlNodeList xmlnodeList;
            //FileStream fs = new FileStream(openFilename, FileMode.Open, FileAccess.Read);
            //xmldoc.Load(fs);
            //xmlnodeList = xmldoc.ChildNodes[1].ChildNodes;

            //treeViewImages.Nodes.Clear();
            //foreach (var item in xmlnodeList)
            //{
            //    var test = item;
            //    //TreeNode newNode = new TreeNode();
            //    //treeViewImages.Nodes.Add(newNode);
            //}
            
            
            //List<TreeNodeWithTags> listOfNodes = new List<TreeNodeWithTags>();

            //XmlSerializer serializer = new XmlSerializer(typeof(TreeNodeWithTags));

            //using (XmlReader reader = new XmlTextReader(openFilename))
            //{
            //    var listOfNodes = serializer.Deserialize(reader);
            //}

            //using (Stream file = File.Open(openFilename, FileMode.Open))
            //{
            //    BinaryFormatter bf = new BinaryFormatter();
            //    object obj = bf.Deserialize(file);

            //    TreeNode[] nodeList = (obj as IEnumerable<TreeNode>).ToArray();
            //    treeViewImages.Nodes.AddRange(nodeList);
            //}
        }

        private void buttonDeleteSelection_Click(object sender, EventArgs e)
        {
            DataTable dataTable = dataGridView1.DataSource as DataTable;
            dataTable.Rows.Clear();
            // dataGridView1.DataSource = dataTable;
            // dataTable.Dispose();
            labelStatusleisteDataGridView.Text = $"Keine Bilder ausgewählt";
            buttonMove.Enabled = true;
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
            DataTable dataTable = dataGridView1.DataSource as DataTable;

            foreach (DataRow imageEntryRow in dataTable.Rows)
            {
                try
                {
                    imageEntryRow["NeuerZielordner"] = $"{Path.Combine(targetPathName, textBoxZielordnername.Text)}";
                }
                catch { }
            }
        }

        private void buttonMove_Click(object sender, EventArgs e)
        {
            buttonMove.Enabled = false;
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
                    File.Copy(dataTable.Rows[i]["Dateiname"].ToString(), Path.Combine(dataTable.Rows[i]["NeuerZielordner"].ToString(), fileName), false);

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

        #region internal functions
        internal void setFoldersFromList(List<string> folders)
        {
            _openFolders = folders;
        }

        #endregion

        #region HelperFunctions

        private DataTable getImages()
        {
            // Do performance measure for populating dataTable
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // Array of imagefiles
            string[] imageFiles;
            List<string> imagesFromFolders = new List<string>();

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
            //TreeNode topNode = new TreeNode(sourcePathName);
            //List<TreeNode> lstAufnahmedatum = new List<TreeNode>();
            //List<TreeNode> lstImages = new List<TreeNode>();


            // load files for selected folders
            if (_openFolders.Count > 0) {
                foreach (string folder in _openFolders)
                {
                    // list imagefiles of pathName
                    if (Directory.Exists(folder))
                    {
                        // TODO - respect subfolders
                        //imageFiles = Directory.GetFiles(folder);
                        
                        imagesFromFolders.AddRange(Directory.GetFiles(folder, "*", SearchOption.AllDirectories).ToList());
                    }
                    else
                    {
                        // button1.Enabled = true;
                        MessageBox.Show($"Folder {folder} not found.");
                        throw new Exception($"Folder {folder} not found.", new DirectoryNotFoundException());
                    }
                }
            }
            // Now we know how many images to process - set progressbar
            //progressBarVerarbeitung.Minimum = 1;
            //progressBarVerarbeitung.Maximum = imageFiles.Length;
            //progressBarVerarbeitung.Value = 1;
            //progressBarVerarbeitung.Step = 1;

            //if (imageFiles?.Length > 0)
            if (imagesFromFolders.Count > 0)
            {
                // Globally define Encoding
                ASCIIEncoding enc = new ASCIIEncoding();

                foreach (var imageFile in imagesFromFolders)
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

                                //progressBarVerarbeitung.PerformStep();
                                // report progress via backgroundworker
                                float percentageFinished = ((float)dataTable.Rows.Count / (float)imagesFromFolders.Count) * 100;

                                // Do performance measure for populating dataTable
                                var elapsedTimeSinceStart = watch.Elapsed;
                                var timePerPicture = (elapsedTimeSinceStart.TotalMilliseconds / 1000) / dataTable.Rows.Count;
                                var timeRemaining = Math.Round(timePerPicture * (imagesFromFolders.Count - dataTable.Rows.Count), 0);

                                backgroundWorkerGetImages.ReportProgress(Convert.ToInt32(percentageFinished), new { timeRemaining });

                                //if ((dataTable.Rows.Count % 5) == 0)
                                //{
                                //    //labelAnzahl.Text = $"Anzahl verarbeiteter Bilder: {dataTable.Rows.Count} von {imageFiles.Length} - geschätzte verbleibende Zeit: {Math.Round(timeRemaining, 0)}";
                                //    //Refresh();
                                //}
                            }
                        }
                    }
                }

                // sort DataTable based on Aufnahmedatum
                dataTable.DefaultView.Sort = "Aufnahmedatum";

                return dataTable;

                ////// Iterate dataSource for treeview
                //treeViewImages.Nodes.Clear();
                //foreach (DataRow row in dataTable.Rows)
                //{
                //    // Add top level element
                //    if (!treeViewImages.Nodes.ContainsKey(row["Zielordner"].ToString()))
                //    {
                //        TreeNode currentNode = new TreeNode();
                //        currentNode.Name = row["Zielordner"].ToString();
                //        currentNode.Text = row["Zielordner"].ToString();
                //        treeViewImages.Nodes.Add(currentNode);
                //    }

                //    // add current image as child
                //    TreeNode childNode = new TreeNode();
                //    childNode.Name = row["Dateiname"].ToString();
                //    childNode.Text = row["Dateiname"].ToString();

                //    // tags for properties needed in datagridview
                //    ImageEntryObject imageEntryObject = new ImageEntryObject(row["Aufnahmedatum"].ToString(),
                //        row["Dateiname"].ToString(),
                //        row["Elternordner"].ToString(), "", "");
                //    childNode.Tag = imageEntryObject;

                //    treeViewImages.Nodes[row["Zielordner"].ToString()].Nodes.Add(childNode);

                //    labelStatusTreeview.Text = $"{treeViewImages.Nodes.Count} Aufnahmedaten mit {dataTable.Rows.Count} Bildern";
                //}
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

        /// <summary>
        /// Serialize TreeNode incl. Tags recursively
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private string serializeTreeNodeInclTags( TreeNode node, int level, TextWriter writer)
        {
            string xmlSerialization = "";

            // tags for level and node properties
            string tabsForLevel = new string(Convert.ToChar("\t"), level);
            string tabsForProperties = new string(Convert.ToChar("\t"), level + 1);
            string tabsForTags = new string(Convert.ToChar("\t"), level + 2);

            if (node.Nodes.Count > 0)
            {
                // serialize node incl. Tags
                ImageEntryObject imageEntry = node.Tag as ImageEntryObject;

                //xmlSerialization += $"{tabsForLevel}<Node>\n{tabsForProperties}<Name>{node.Name}</Name>\n{tabsForProperties}<Text>{node.Text}</Text>\n{tabsForProperties}<Tag>\n{tabsForTags}<Aufnamedatum>{imageEntry?.Aufnahmedatum}</Aufnahmedatum>\n{tabsForTags}<Dateiname>{imageEntry?.Dateiname}</Dateiname>\n{tabsForTags}<Elternordner>{imageEntry?.Elternordner}>/Elternordner>\n{tabsForProperties}</Tag>\n{tabsForProperties}<Nodes>\n";
                writer.Write($"{tabsForLevel}<Node>\n{tabsForProperties}<Name>{node.Name}</Name>\n{tabsForProperties}<Text>{node.Text}</Text>\n{tabsForProperties}<Tag>\n{tabsForTags}<Aufnahmedatum>{imageEntry?.Aufnahmedatum}</Aufnahmedatum>\n{tabsForTags}<Dateiname>{imageEntry?.Dateiname}</Dateiname>\n{tabsForTags}<Elternordner>{imageEntry?.Elternordner}</Elternordner>\n{tabsForProperties}</Tag>\n{tabsForProperties}<Nodes>\n");
                // iterate child nodes
                foreach (TreeNode childNode in node.Nodes)
                {
                    //xmlSerialization += serializeTreeNodeInclTags(childNode, level + 2);
                    serializeTreeNodeInclTags(childNode, level + 2, writer);
                }
                //xmlSerialization += $"{tabsForProperties}</Nodes>\n{tabsForLevel}</Node>\n";
                writer.Write($"{tabsForProperties}</Nodes>\n{tabsForLevel}</Node>\n");
            }
            else
            {
                // serialize node incl. tags without children
                ImageEntryObject imageEntry = node.Tag as ImageEntryObject;
                //xmlSerialization += $"{tabsForLevel}<Node>\n{tabsForProperties}<Name>{node.Name}</Name>\n{tabsForProperties}<Text>{node.Text}</Text>\n{tabsForProperties}<Tag>\n{tabsForTags}<Aufnamedatum>{imageEntry?.Aufnahmedatum}</Aufnahmedatum>\n{tabsForTags}<Dateiname>{imageEntry?.Dateiname}</Dateiname>\n{tabsForTags}<Elternordner>{imageEntry?.Elternordner}>/Elternordner>\n{tabsForProperties}</Tag>\n{tabsForProperties}<Nodes>\n{tabsForProperties}</Nodes>\n{tabsForLevel}</Node>\n";
                writer.Write($"{tabsForLevel}<Node>\n{tabsForProperties}<Name>{node.Name}</Name>\n{tabsForProperties}<Text>{node.Text}</Text>\n{tabsForProperties}<Tag>\n{tabsForTags}<Aufnahmedatum>{imageEntry?.Aufnahmedatum}</Aufnahmedatum>\n{tabsForTags}<Dateiname>{imageEntry?.Dateiname}</Dateiname>\n{tabsForTags}<Elternordner>{imageEntry?.Elternordner}</Elternordner>\n{tabsForProperties}</Tag>\n{tabsForProperties}<Nodes>\n{tabsForProperties}</Nodes>\n{tabsForLevel}</Node>\n");

            }
            return xmlSerialization;
        }


        private void populateTreeViewByDataTable(DataTable dataTable)
        {

            treeViewImages.Nodes.Clear();
            //DataTable dataTable = treeviewDataTable;
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
            treeViewImages.Sort();
        }

        #endregion

        #region Backgroundworker

        private void backgroundWorkerGetImages_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            e.Result = getImages();
        }

        // This event handler deals with the results of the
        // background operation.
        private void backgroundWorkerGetImages_RunWorkerCompleted(
            object sender, RunWorkerCompletedEventArgs e)
        {
            // save currentDataTable to global variable - TEMPORARY WORKAROUND
            treeviewDataTable = e.Result as DataTable;

            // populate TreeView
            populateTreeViewByDataTable(treeviewDataTable);

            //// Iterate dataSource for treeview

            button1.Enabled = true;
        }

        // This event handler updates the progress bar.
        private void backgroundWorkerGetImages_ProgressChanged(object sender,
            ProgressChangedEventArgs e)
        {
            var test = e.UserState;
            progressBarVerarbeitung.Value = e.ProgressPercentage;
            labelBenoetigteZeit.Text = $"Benötigte Zeit: {test}s";
            labelBenoetigteZeit.Visible = true;
        }

        // Set up the BackgroundWorker object by 
        // attaching event handlers. 
        private void InitializeBackgroundWorker()
        {

            backgroundWorkerGetImages.DoWork +=
                new DoWorkEventHandler(backgroundWorkerGetImages_DoWork);
            backgroundWorkerGetImages.RunWorkerCompleted +=
                new RunWorkerCompletedEventHandler(
            backgroundWorkerGetImages_RunWorkerCompleted);
            backgroundWorkerGetImages.ProgressChanged +=
                new ProgressChangedEventHandler(
            backgroundWorkerGetImages_ProgressChanged);
        }
        #endregion
    }
}