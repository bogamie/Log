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
            splitContainer1 = new SplitContainer();
            contentBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // DirectoryTree
            // 
            DirectoryTree.Dock = DockStyle.Fill;
            DirectoryTree.Location = new Point(0, 0);
            DirectoryTree.Name = "DirectoryTree";
            DirectoryTree.Size = new Size(266, 450);
            DirectoryTree.TabIndex = 2;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(DirectoryTree);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(contentBox);
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 266;
            splitContainer1.TabIndex = 4;
            // 
            // contentBox
            // 
            contentBox.Dock = DockStyle.Fill;
            contentBox.Font = new Font("D2Coding ligature", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            contentBox.Location = new Point(0, 0);
            contentBox.Multiline = true;
            contentBox.Name = "contentBox";
            contentBox.ReadOnly = true;
            contentBox.Size = new Size(530, 450);
            contentBox.TabIndex = 0;
            contentBox.WordWrap = false;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Name = "MainForm";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TreeView DirectoryTree = new TreeView();
        private SplitContainer splitContainer1;
        private TextBox contentBox;
    }
}
