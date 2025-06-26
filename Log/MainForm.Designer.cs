namespace Log
{
    partial class MainForm
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
            DirectoryTree = new TreeView();
            textBox = new TextBox();
            SuspendLayout();
            // 
            // DirectoryTree
            // 
            DirectoryTree.Dock = DockStyle.Left;
            DirectoryTree.Location = new Point(0, 0);
            DirectoryTree.Name = "DirectoryTree";
            DirectoryTree.Size = new Size(242, 450);
            DirectoryTree.TabIndex = 2;
            // 
            // textBox
            // 
            textBox.Dock = DockStyle.Right;
            textBox.Location = new Point(248, 0);
            textBox.Multiline = true;
            textBox.Name = "textBox";
            textBox.ReadOnly = true;
            textBox.Size = new Size(552, 450);
            textBox.TabIndex = 3;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox);
            Controls.Add(DirectoryTree);
            Name = "MainForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TreeView DirectoryTree = new TreeView();
        private TextBox textBox;
    }
}
