namespace Forensics_Tool
{
    partial class FormMain
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
            panel1 = new Panel();
            panel2 = new Panel();
            btnTimeline = new Button();
            btnSearch = new Button();
            btnContent = new Button();
            mainPanel = new Panel();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.Control;
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1108, 24);
            panel1.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = SystemColors.ActiveCaptionText;
            panel2.Controls.Add(btnTimeline);
            panel2.Controls.Add(btnSearch);
            panel2.Controls.Add(btnContent);
            panel2.Dock = DockStyle.Left;
            panel2.Location = new Point(0, 24);
            panel2.Name = "panel2";
            panel2.Size = new Size(80, 663);
            panel2.TabIndex = 0;
            // 
            // btnTimeline
            // 
            btnTimeline.Dock = DockStyle.Top;
            btnTimeline.FlatAppearance.BorderSize = 0;
            btnTimeline.FlatStyle = FlatStyle.Flat;
            btnTimeline.Font = new Font("D2Coding", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnTimeline.ForeColor = SystemColors.MenuBar;
            btnTimeline.Location = new Point(0, 128);
            btnTimeline.Name = "btnTimeline";
            btnTimeline.Size = new Size(80, 64);
            btnTimeline.TabIndex = 2;
            btnTimeline.Text = "Timeline";
            btnTimeline.UseVisualStyleBackColor = true;
            btnTimeline.Click += btnTimeline_Click;
            // 
            // btnSearch
            // 
            btnSearch.Dock = DockStyle.Top;
            btnSearch.FlatAppearance.BorderSize = 0;
            btnSearch.FlatStyle = FlatStyle.Flat;
            btnSearch.Font = new Font("D2Coding", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnSearch.ForeColor = SystemColors.MenuBar;
            btnSearch.Location = new Point(0, 64);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(80, 64);
            btnSearch.TabIndex = 1;
            btnSearch.Text = "Search";
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // btnContent
            // 
            btnContent.Dock = DockStyle.Top;
            btnContent.FlatAppearance.BorderSize = 0;
            btnContent.FlatStyle = FlatStyle.Flat;
            btnContent.Font = new Font("D2Coding", 12F, FontStyle.Bold, GraphicsUnit.Point, 129);
            btnContent.ForeColor = SystemColors.MenuBar;
            btnContent.Location = new Point(0, 0);
            btnContent.Name = "btnContent";
            btnContent.Size = new Size(80, 64);
            btnContent.TabIndex = 0;
            btnContent.Text = "Content";
            btnContent.UseVisualStyleBackColor = true;
            btnContent.Click += btnContent_Click;
            // 
            // mainPanel
            // 
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(80, 24);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(1028, 663);
            mainPanel.TabIndex = 1;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonHighlight;
            ClientSize = new Size(1108, 687);
            Controls.Add(mainPanel);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Name = "MainWindow";
            Text = "Form1";
            panel2.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private Button btnContent;
        private Button btnTimeline;
        private Button btnSearch;
        private Panel mainPanel;
    }
}
