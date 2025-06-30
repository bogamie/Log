namespace Forensics_Tool.Forms {
    partial class FormStartup {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            tableLayoutPanel1 = new TableLayoutPanel();
            panelBottom = new Panel();
            btnApply = new Button();
            tableLayoutPanel2 = new TableLayoutPanel();
            panelRight = new Panel();
            btnExist = new Button();
            panelLeft = new Panel();
            btnNew = new Button();
            openFileDialog1 = new OpenFileDialog();
            tableLayoutPanel1.SuspendLayout();
            panelBottom.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            panelRight.SuspendLayout();
            panelLeft.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Controls.Add(panelBottom, 0, 1);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel1.Size = new Size(800, 450);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // panelBottom
            // 
            panelBottom.Controls.Add(btnApply);
            panelBottom.Dock = DockStyle.Fill;
            panelBottom.Location = new Point(3, 228);
            panelBottom.Name = "panelBottom";
            panelBottom.Size = new Size(794, 219);
            panelBottom.TabIndex = 0;
            // 
            // btnApply
            // 
            btnApply.Enabled = false;
            btnApply.Location = new Point(0, 0);
            btnApply.Name = "btnApply";
            btnApply.Size = new Size(150, 70);
            btnApply.TabIndex = 0;
            btnApply.Text = "Apply";
            btnApply.UseVisualStyleBackColor = true;
            btnApply.Click += btnApply_Click;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(panelRight, 1, 0);
            tableLayoutPanel2.Controls.Add(panelLeft, 0, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 3);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Size = new Size(794, 219);
            tableLayoutPanel2.TabIndex = 0;
            // 
            // panelRight
            // 
            panelRight.Controls.Add(btnExist);
            panelRight.Dock = DockStyle.Fill;
            panelRight.Location = new Point(400, 3);
            panelRight.Name = "panelRight";
            panelRight.Size = new Size(391, 213);
            panelRight.TabIndex = 1;
            // 
            // btnExist
            // 
            btnExist.Location = new Point(3, 6);
            btnExist.Name = "btnExist";
            btnExist.Size = new Size(150, 70);
            btnExist.TabIndex = 1;
            btnExist.Text = "Recent Case";
            btnExist.UseVisualStyleBackColor = true;
            btnExist.Click += btnExist_Click;
            // 
            // panelLeft
            // 
            panelLeft.Controls.Add(btnNew);
            panelLeft.Dock = DockStyle.Fill;
            panelLeft.Location = new Point(3, 3);
            panelLeft.Name = "panelLeft";
            panelLeft.Size = new Size(391, 213);
            panelLeft.TabIndex = 0;
            // 
            // btnNew
            // 
            btnNew.Location = new Point(3, 6);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(150, 70);
            btnNew.TabIndex = 0;
            btnNew.Text = "Select Tar file";
            btnNew.UseVisualStyleBackColor = true;
            btnNew.Click += btnNew_Click;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // FormStartup
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tableLayoutPanel1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormStartup";
            ShowIcon = false;
            Text = "FormStartup";
            tableLayoutPanel1.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            tableLayoutPanel2.ResumeLayout(false);
            panelRight.ResumeLayout(false);
            panelLeft.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private Panel panelLeft;
        private Button btnNew;
        private Panel panelBottom;
        private Panel panelRight;
        private Button btnApply;
        private Button btnExist;
        private OpenFileDialog openFileDialog1;
    }
}