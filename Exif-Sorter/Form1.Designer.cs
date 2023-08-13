namespace Exif_Sorter
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            dataGridView1 = new DataGridView();
            dataGridViewMenu = new ContextMenuStrip(components);
            menuRemoveImage = new ToolStripMenuItem();
            menuRemoveAufnahmedatum = new ToolStripMenuItem();
            button1 = new Button();
            labelBenoetigteZeit = new Label();
            labelAnzahl = new Label();
            progressBarVerarbeitung = new ProgressBar();
            treeViewImages = new TreeView();
            pictureBox1 = new PictureBox();
            imageList1 = new ImageList(components);
            btnMoveToFolder = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            menuStrip1 = new MenuStrip();
            dateiToolStripMenuItem = new ToolStripMenuItem();
            ordnerÖffnenToolStripMenuItem = new ToolStripMenuItem();
            labelSourcePathName = new Label();
            buttonDeleteSelection = new Button();
            textBoxZielordnername = new TextBox();
            labelZielordner = new Label();
            buttonZielordnername = new Button();
            panelImages = new Panel();
            labelStatusTreeview = new Label();
            labelStatusleisteBildVorschau = new Label();
            labelStatusleisteDataGridView = new Label();
            buttonMove = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            dataGridViewMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(1078, 446);
            dataGridView1.Margin = new Padding(2);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.RowTemplate.Height = 33;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.Size = new Size(600, 290);
            dataGridView1.TabIndex = 0;
            dataGridView1.MouseClick += dataGridView1_MouseClick;
            // 
            // dataGridViewMenu
            // 
            dataGridViewMenu.ImageScalingSize = new Size(24, 24);
            dataGridViewMenu.Items.AddRange(new ToolStripItem[] { menuRemoveImage, menuRemoveAufnahmedatum });
            dataGridViewMenu.Name = "dataGridViewMenu";
            dataGridViewMenu.Size = new Size(268, 48);
            // 
            // menuRemoveImage
            // 
            menuRemoveImage.Name = "menuRemoveImage";
            menuRemoveImage.Size = new Size(267, 22);
            // 
            // menuRemoveAufnahmedatum
            // 
            menuRemoveAufnahmedatum.Name = "menuRemoveAufnahmedatum";
            menuRemoveAufnahmedatum.Size = new Size(267, 22);
            menuRemoveAufnahmedatum.Text = "Aufnahmedatum aus Liste entfernen";
            // 
            // button1
            // 
            button1.Location = new Point(18, 57);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(78, 20);
            button1.TabIndex = 1;
            button1.Text = "Aktualisieren";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // labelBenoetigteZeit
            // 
            labelBenoetigteZeit.AutoSize = true;
            labelBenoetigteZeit.Location = new Point(18, 28);
            labelBenoetigteZeit.Margin = new Padding(2, 0, 2, 0);
            labelBenoetigteZeit.Name = "labelBenoetigteZeit";
            labelBenoetigteZeit.Size = new Size(84, 15);
            labelBenoetigteZeit.TabIndex = 2;
            labelBenoetigteZeit.Text = "Benötigte Zeit:";
            labelBenoetigteZeit.Visible = false;
            // 
            // labelAnzahl
            // 
            labelAnzahl.AutoSize = true;
            labelAnzahl.Location = new Point(18, 43);
            labelAnzahl.Margin = new Padding(2, 0, 2, 0);
            labelAnzahl.Name = "labelAnzahl";
            labelAnzahl.Size = new Size(157, 15);
            labelAnzahl.TabIndex = 3;
            labelAnzahl.Text = "Anzahl verarbeiteter Bilder: 0";
            // 
            // progressBarVerarbeitung
            // 
            progressBarVerarbeitung.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBarVerarbeitung.Location = new Point(8, 770);
            progressBarVerarbeitung.Margin = new Padding(2);
            progressBarVerarbeitung.Name = "progressBarVerarbeitung";
            progressBarVerarbeitung.Size = new Size(1670, 22);
            progressBarVerarbeitung.TabIndex = 4;
            // 
            // treeViewImages
            // 
            treeViewImages.AllowDrop = true;
            treeViewImages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            treeViewImages.CheckBoxes = true;
            treeViewImages.FullRowSelect = true;
            treeViewImages.Location = new Point(18, 81);
            treeViewImages.Margin = new Padding(2);
            treeViewImages.Name = "treeViewImages";
            treeViewImages.Size = new Size(1056, 335);
            treeViewImages.TabIndex = 5;
            treeViewImages.AfterCheck += treeViewImages_AfterCheck;
            treeViewImages.NodeMouseClick += treeViewImages_NodeMouseClick;
            treeViewImages.NodeMouseDoubleClick += treeViewImages_NodeMouseDoubleClick;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            pictureBox1.Location = new Point(1078, 81);
            pictureBox1.Margin = new Padding(2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(600, 333);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // btnMoveToFolder
            // 
            btnMoveToFolder.Enabled = false;
            btnMoveToFolder.Location = new Point(100, 57);
            btnMoveToFolder.Margin = new Padding(2);
            btnMoveToFolder.Name = "btnMoveToFolder";
            btnMoveToFolder.Size = new Size(162, 20);
            btnMoveToFolder.TabIndex = 7;
            btnMoveToFolder.Text = "In Ordner verschieben";
            btnMoveToFolder.UseVisualStyleBackColor = true;
            btnMoveToFolder.Click += btnMoveToFolder_Click;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { dateiToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(4, 1, 0, 1);
            menuStrip1.Size = new Size(1687, 24);
            menuStrip1.TabIndex = 8;
            menuStrip1.Text = "menuStrip1";
            // 
            // dateiToolStripMenuItem
            // 
            dateiToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ordnerÖffnenToolStripMenuItem });
            dateiToolStripMenuItem.Name = "dateiToolStripMenuItem";
            dateiToolStripMenuItem.Size = new Size(46, 22);
            dateiToolStripMenuItem.Text = "Datei";
            // 
            // ordnerÖffnenToolStripMenuItem
            // 
            ordnerÖffnenToolStripMenuItem.Name = "ordnerÖffnenToolStripMenuItem";
            ordnerÖffnenToolStripMenuItem.Size = new Size(149, 22);
            ordnerÖffnenToolStripMenuItem.Text = "Ordner öffnen";
            ordnerÖffnenToolStripMenuItem.ToolTipText = "Quellordner öffnen";
            ordnerÖffnenToolStripMenuItem.Click += ordnerÖffnenToolStripMenuItem_Click;
            // 
            // labelSourcePathName
            // 
            labelSourcePathName.AutoSize = true;
            labelSourcePathName.Location = new Point(335, 60);
            labelSourcePathName.Margin = new Padding(2, 0, 2, 0);
            labelSourcePathName.Name = "labelSourcePathName";
            labelSourcePathName.Size = new Size(93, 15);
            labelSourcePathName.TabIndex = 9;
            labelSourcePathName.Text = "Quellverzeichnis";
            // 
            // buttonDeleteSelection
            // 
            buttonDeleteSelection.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonDeleteSelection.Location = new Point(1078, 419);
            buttonDeleteSelection.Name = "buttonDeleteSelection";
            buttonDeleteSelection.Size = new Size(75, 23);
            buttonDeleteSelection.TabIndex = 10;
            buttonDeleteSelection.Text = "Leeren";
            buttonDeleteSelection.UseVisualStyleBackColor = true;
            buttonDeleteSelection.Click += buttonDeleteSelection_Click;
            // 
            // textBoxZielordnername
            // 
            textBoxZielordnername.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            textBoxZielordnername.Location = new Point(1253, 419);
            textBoxZielordnername.Margin = new Padding(2);
            textBoxZielordnername.Name = "textBoxZielordnername";
            textBoxZielordnername.Size = new Size(299, 23);
            textBoxZielordnername.TabIndex = 11;
            textBoxZielordnername.KeyDown += textBoxZielordnername_KeyDown;
            // 
            // labelZielordner
            // 
            labelZielordner.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelZielordner.AutoSize = true;
            labelZielordner.Location = new Point(1158, 423);
            labelZielordner.Margin = new Padding(2, 0, 2, 0);
            labelZielordner.Name = "labelZielordner";
            labelZielordner.Size = new Size(91, 15);
            labelZielordner.TabIndex = 12;
            labelZielordner.Text = "Zielordnername";
            // 
            // buttonZielordnername
            // 
            buttonZielordnername.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonZielordnername.Location = new Point(1556, 422);
            buttonZielordnername.Margin = new Padding(2);
            buttonZielordnername.Name = "buttonZielordnername";
            buttonZielordnername.Size = new Size(31, 20);
            buttonZielordnername.TabIndex = 13;
            buttonZielordnername.Text = "Ok";
            buttonZielordnername.UseVisualStyleBackColor = true;
            buttonZielordnername.Click += buttonZielordnername_Click;
            // 
            // panelImages
            // 
            panelImages.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelImages.AutoScroll = true;
            panelImages.Location = new Point(18, 446);
            panelImages.Margin = new Padding(2);
            panelImages.Name = "panelImages";
            panelImages.Size = new Size(1055, 290);
            panelImages.TabIndex = 15;
            // 
            // labelStatusTreeview
            // 
            labelStatusTreeview.AutoSize = true;
            labelStatusTreeview.Location = new Point(18, 419);
            labelStatusTreeview.Margin = new Padding(2, 0, 2, 0);
            labelStatusTreeview.Name = "labelStatusTreeview";
            labelStatusTreeview.Size = new Size(0, 15);
            labelStatusTreeview.TabIndex = 16;
            // 
            // labelStatusleisteBildVorschau
            // 
            labelStatusleisteBildVorschau.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelStatusleisteBildVorschau.AutoSize = true;
            labelStatusleisteBildVorschau.Location = new Point(18, 738);
            labelStatusleisteBildVorschau.Name = "labelStatusleisteBildVorschau";
            labelStatusleisteBildVorschau.Size = new Size(187, 15);
            labelStatusleisteBildVorschau.TabIndex = 17;
            labelStatusleisteBildVorschau.Text = "Kein Aufnahmedatum ausgewählt";
            // 
            // labelStatusleisteDataGridView
            // 
            labelStatusleisteDataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelStatusleisteDataGridView.AutoSize = true;
            labelStatusleisteDataGridView.Location = new Point(1078, 738);
            labelStatusleisteDataGridView.Name = "labelStatusleisteDataGridView";
            labelStatusleisteDataGridView.Size = new Size(132, 15);
            labelStatusleisteDataGridView.TabIndex = 18;
            labelStatusleisteDataGridView.Text = "Keine Bilder ausgewählt";
            // 
            // buttonMove
            // 
            buttonMove.Location = new Point(1592, 419);
            buttonMove.Name = "buttonMove";
            buttonMove.Size = new Size(86, 23);
            buttonMove.TabIndex = 19;
            buttonMove.Text = "Verschieben";
            buttonMove.UseVisualStyleBackColor = true;
            buttonMove.Click += buttonMove_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1687, 799);
            Controls.Add(buttonMove);
            Controls.Add(labelStatusleisteDataGridView);
            Controls.Add(labelStatusleisteBildVorschau);
            Controls.Add(labelStatusTreeview);
            Controls.Add(panelImages);
            Controls.Add(buttonZielordnername);
            Controls.Add(labelZielordner);
            Controls.Add(textBoxZielordnername);
            Controls.Add(buttonDeleteSelection);
            Controls.Add(labelSourcePathName);
            Controls.Add(btnMoveToFolder);
            Controls.Add(pictureBox1);
            Controls.Add(treeViewImages);
            Controls.Add(progressBarVerarbeitung);
            Controls.Add(labelAnzahl);
            Controls.Add(labelBenoetigteZeit);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(2);
            Name = "Form1";
            Text = "Exif Sorter";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            dataGridViewMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public DataGridView dataGridView1;
        private Button button1;
        private Label labelBenoetigteZeit;
        private Label labelAnzahl;
        private ProgressBar progressBarVerarbeitung;
        private TreeView treeViewImages;
        private PictureBox pictureBox1;
        private ImageList imageList1;
        private Button btnMoveToFolder;
        private FolderBrowserDialog folderBrowserDialog1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem dateiToolStripMenuItem;
        private ToolStripMenuItem ordnerÖffnenToolStripMenuItem;
        private Label labelSourcePathName;
        private Button buttonDeleteSelection;
        private ContextMenuStrip dataGridViewMenu;
        private ToolStripMenuItem menuRemoveImage;
        private ToolStripMenuItem menuRemoveAufnahmedatum;
        private TextBox textBoxZielordnername;
        private Label labelZielordner;
        private Button buttonZielordnername;
        private Panel panelImages;
        private Label labelStatusTreeview;
        private Label labelStatusleisteBildVorschau;
        private Label labelStatusleisteDataGridView;
        private Button buttonMove;
    }
}