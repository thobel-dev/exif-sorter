using System.Data;
using System.Drawing.Imaging;
using System.Globalization;
using System.Text;

namespace Exif_Sorter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Do performance measure for populating dataTable
            var watch = System.Diagnostics.Stopwatch.StartNew();

            // Performance issues in DataGridView
            dataGridView1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            // or even better, use .DisableResizing. Most time consuming enum is DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders

            // set it to false if not needed
            dataGridView1.RowHeadersVisible = false;

            // source folder of images
            string sourcePathName = @"Z:\Bilder\S9\Keller";

            // target folder of images
            string targetPathName = @"C:\Temp\Bilder_Sortiert";

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

            // list imagefiles of pathName
            if (Directory.Exists(sourcePathName))
            {
                // TODO - respect subfolders
                imageFiles = Directory.GetFiles(sourcePathName);
            }
            else
            {
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
            }
            else
            {
                throw new Exception($"No files to process found in {sourcePathName}", new Exception());
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            labelBenoetigteZeit.Text = $"Benötigte Zeit: {(elapsedMs / 1000).ToString()}s";
            labelBenoetigteZeit.Visible = true;
            dataGridView1.DataSource = dataTable;

            // Grouping on TargetFolder for easier readability
            
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 4)
            {
                int rowIndexChanged = e.RowIndex;
                string newTargetFolderName = dataGridView1.Rows[rowIndexChanged].Cells[4].Value.ToString();
                string rowChangedDate = dataGridView1.Rows[rowIndexChanged].Cells[3].Value.ToString();
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    if (dataGridView1.Rows[i].Cells[3].Value.ToString() == rowChangedDate)
                    {
                        dataGridView1.Rows[i].Cells[4].Value = newTargetFolderName;
                        dataGridView1.Rows[i].Cells[5].Value = $"{dataGridView1.Rows[i].Cells[3].Value} - {newTargetFolderName}";
                    }
                }
            }

        }
    }
}