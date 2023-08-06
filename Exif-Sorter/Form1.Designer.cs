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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            dataGridView1 = new DataGridView();
            button1 = new Button();
            labelBenoetigteZeit = new Label();
            labelAnzahl = new Label();
            progressBarVerarbeitung = new ProgressBar();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(12, 70);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 62;
            dataGridView1.RowTemplate.Height = 33;
            dataGridView1.Size = new Size(1414, 711);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
            // 
            // button1
            // 
            button1.Location = new Point(25, 22);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 1;
            button1.Text = "Aktualisieren";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // labelBenoetigteZeit
            // 
            labelBenoetigteZeit.AutoSize = true;
            labelBenoetigteZeit.Location = new Point(234, 27);
            labelBenoetigteZeit.Name = "labelBenoetigteZeit";
            labelBenoetigteZeit.Size = new Size(126, 25);
            labelBenoetigteZeit.TabIndex = 2;
            labelBenoetigteZeit.Text = "Benötigte Zeit:";
            labelBenoetigteZeit.Visible = false;
            // 
            // labelAnzahl
            // 
            labelAnzahl.AutoSize = true;
            labelAnzahl.Location = new Point(505, 27);
            labelAnzahl.Name = "labelAnzahl";
            labelAnzahl.Size = new Size(237, 25);
            labelAnzahl.TabIndex = 3;
            labelAnzahl.Text = "Anzahl verarbeiteter Bilder: 0";
            // 
            // progressBarVerarbeitung
            // 
            progressBarVerarbeitung.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            progressBarVerarbeitung.Location = new Point(12, 789);
            progressBarVerarbeitung.Name = "progressBarVerarbeitung";
            progressBarVerarbeitung.Size = new Size(1414, 34);
            progressBarVerarbeitung.TabIndex = 4;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1438, 835);
            Controls.Add(progressBarVerarbeitung);
            Controls.Add(labelAnzahl);
            Controls.Add(labelBenoetigteZeit);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "Exif Sorter";
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public DataGridView dataGridView1;
        private Button button1;
        private Label labelBenoetigteZeit;
        private Label labelAnzahl;
        private ProgressBar progressBarVerarbeitung;
    }
}