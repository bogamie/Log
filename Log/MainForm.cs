using System.Diagnostics;
using System.Formats.Tar;
using System.IO.Compression;
using Log.IO;
using Log.UI;
using Log.Utilities;

namespace Log
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private bool dialogShown = false;

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            if (dialogShown) return;

            dialogShown = true;
            var dialog = new StartupDialog();

            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                this.Close();
                return;
            }

            string selectedPath = dialog.SelectedTarPath;
            string extension = Path.GetExtension(selectedPath).ToLowerInvariant();

            string targetDir;

            if (extension == ".tar" || extension == ".gz" || extension == ".tgz" || extension == ".gzip")
            {
                targetDir = PathResolver.GetExtractionDirectoryPath(selectedPath);
                targetDir = PathResolver.MakeExtractionDirectory(targetDir);

                bool success = Extractor.ExtractRecursive(selectedPath, targetDir, isRoot: true);
                if (!success)
                {
                    MessageBox.Show("Failed to extract the archive.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                    return;
                }

                MessageBox.Show("Archive extracted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (Directory.Exists(selectedPath))
            {
                targetDir = selectedPath;
            }
            else
            {
                MessageBox.Show("Unsupported file format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            TreeViewBuilder treeViewBuilder = new TreeViewBuilder(DirectoryTree);
            treeViewBuilder.BuildFromDirectory(targetDir);
        }
    }
}
