namespace Exif_Sorter
{
    partial class FolderBrowseDialogMultiselect
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            treeViewFolderSelect = new TreeView();
            buttonOpenFolders = new Button();
            buttonFolderCancel = new Button();
            SuspendLayout();
            // 
            // treeViewFolderSelect
            // 
            treeViewFolderSelect.CheckBoxes = true;
            treeViewFolderSelect.Location = new Point(2, 34);
            treeViewFolderSelect.Name = "treeViewFolderSelect";
            treeViewFolderSelect.Size = new Size(1108, 648);
            treeViewFolderSelect.TabIndex = 0;
            // 
            // buttonOpenFolders
            // 
            buttonOpenFolders.Location = new Point(12, 777);
            buttonOpenFolders.Name = "buttonOpenFolders";
            buttonOpenFolders.Size = new Size(96, 23);
            buttonOpenFolders.TabIndex = 1;
            buttonOpenFolders.Text = "Übernehmen";
            buttonOpenFolders.UseVisualStyleBackColor = true;
            buttonOpenFolders.Click += buttonOpenFolders_Click;
            // 
            // buttonFolderCancel
            // 
            buttonFolderCancel.Location = new Point(114, 777);
            buttonFolderCancel.Name = "buttonFolderCancel";
            buttonFolderCancel.Size = new Size(96, 23);
            buttonFolderCancel.TabIndex = 2;
            buttonFolderCancel.Text = "Abbrechen";
            buttonFolderCancel.UseVisualStyleBackColor = true;
            buttonFolderCancel.Click += buttonFolderCancel_Click;
            // 
            // FolderBrowseDialogMultiselect
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1112, 812);
            Controls.Add(buttonFolderCancel);
            Controls.Add(buttonOpenFolders);
            Controls.Add(treeViewFolderSelect);
            Name = "FolderBrowseDialogMultiselect";
            ShowInTaskbar = false;
            Text = "Ordner auswählen";
            ResumeLayout(false);
        }

        #endregion

        private TreeView treeViewFolderSelect;
        private Button buttonOpenFolders;
        private Button buttonFolderCancel;
    }
}