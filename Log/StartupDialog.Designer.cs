namespace Log
{
    partial class StartupDialog
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
            btnSelect = new Button();
            btnAccept = new Button();
            openFileDialog1 = new OpenFileDialog();
            lblPath = new Label();
            btnFolder = new Button();
            folderBrowserDialog1 = new FolderBrowserDialog();
            SuspendLayout();
            // 
            // btnSelect
            // 
            btnSelect.Location = new Point(12, 12);
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(75, 23);
            btnSelect.TabIndex = 0;
            btnSelect.Text = "Tar File";
            btnSelect.UseVisualStyleBackColor = true;
            btnSelect.Click += btnSelect_Click;
            // 
            // btnAccept
            // 
            btnAccept.Enabled = false;
            btnAccept.Location = new Point(93, 12);
            btnAccept.Name = "btnAccept";
            btnAccept.Size = new Size(75, 23);
            btnAccept.TabIndex = 1;
            btnAccept.Text = "Accept";
            btnAccept.UseVisualStyleBackColor = true;
            btnAccept.Click += btnAccept_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // lblPath
            // 
            lblPath.AutoSize = true;
            lblPath.Location = new Point(12, 38);
            lblPath.Name = "lblPath";
            lblPath.Size = new Size(40, 15);
            lblPath.TabIndex = 2;
            lblPath.Text = "NONE";
            // 
            // btnFolder
            // 
            btnFolder.Location = new Point(174, 12);
            btnFolder.Name = "btnFolder";
            btnFolder.Size = new Size(75, 23);
            btnFolder.TabIndex = 3;
            btnFolder.Text = "Folder";
            btnFolder.UseVisualStyleBackColor = true;
            btnFolder.Click += btnFolder_Click;
            // 
            // StartupDialog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(584, 161);
            Controls.Add(btnFolder);
            Controls.Add(lblPath);
            Controls.Add(btnAccept);
            Controls.Add(btnSelect);
            Name = "StartupDialog";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnSelect;
        private Button btnAccept;
        private OpenFileDialog openFileDialog1;
        private Label lblPath;
        private Button btnFolder;
        private FolderBrowserDialog folderBrowserDialog1;
    }
}