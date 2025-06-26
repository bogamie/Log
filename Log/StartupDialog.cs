using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Log
{
    public partial class StartupDialog : Form
    {
        public string SelectedTarPath { get; private set; } = "";

        public StartupDialog()
        {
            InitializeComponent();
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Tar files (*.tar)|*.tar"
            })
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    SelectedTarPath = dlg.FileName;
                    lblPath.Text = SelectedTarPath;
                    btnAccept.Enabled = true;
                }
            }
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(SelectedTarPath))
            {
                MessageBox.Show("Please select a valid tar file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog dlg = new FolderBrowserDialog())
            {
                dlg.Description = "Select Folder.";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    SelectedTarPath = dlg.SelectedPath;
                    lblPath.Text = SelectedTarPath;
                    btnAccept.Enabled = true;
                }
                else
                {
                    MessageBox.Show("Please select a valid folder.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

    }
}
