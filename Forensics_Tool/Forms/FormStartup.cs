using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forensics_Tool.Forms {
    public partial class FormStartup : Form {
        public int SelectedIndex { get; private set; } = -1;
        public string? SelectedPath { get; private set; }

        public FormStartup() {
            InitializeComponent();
            Rectangle screen = Screen.PrimaryScreen.WorkingArea;
            this.Size = new Size(screen.Width / 3, screen.Height / 3);
            CenterButtonInPanel(panelLeft, btnNew);
            CenterButtonInPanel(panelRight, btnExist);
            CenterButtonInPanel(panelBottom, btnApply);
        }

        private void CenterButtonInPanel(Panel panel, Button button) {
            button.Location = new Point(
                (panel.Width - button.Width) / 2,
                (panel.Height - button.Height) / 2
            );

            // 크기 변경 시에도 항상 가운데에 있도록
            panel.Controls.Add(button);
            panel.Resize += (s, e) => {
                button.Location = new Point(
                    (panel.Width - button.Width) / 2,
                    (panel.Height - button.Height) / 2
                );
            };
        }

        private void btnNew_Click(object sender, EventArgs e) {
            using (OpenFileDialog dlg = new OpenFileDialog {
                Filter = "Tar files (*.tar)|*.tar"
            }) {
                if (dlg.ShowDialog() == DialogResult.OK) {
                    SelectedPath = dlg.FileName;
                    SelectedIndex = 0; // New tar file selected
                    btnApply.Enabled = true;
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e) {
            if (string.IsNullOrEmpty(SelectedPath)) {
                MessageBox.Show("Please select a valid tar file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnExist_Click(object sender, EventArgs e) {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog()) {
                if (dlg.ShowDialog() == DialogResult.OK) {
                    SelectedPath = dlg.SelectedPath;
                    SelectedIndex = 1; // Existing folder selected
                    btnApply.Enabled = true;
                }
            }
        }
    }
}
