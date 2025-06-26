using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.UI
{
    internal class TreeViewBuilder
    {
        private readonly System.Windows.Forms.TreeView _treeView;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewBuilder"/> class with the specified TreeView control.
        /// </summary>
        /// <param name="treeView">The TreeView control to populate with directory data.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided TreeView is null.</exception>
        public TreeViewBuilder(System.Windows.Forms.TreeView treeView)
        {
            _treeView = treeView ?? throw new ArgumentNullException(nameof(treeView));
        }

        /// <summary>
        /// Clears the TreeView and builds a node hierarchy from the specified root directory path.
        /// </summary>
        /// <param name="rootPath">The full path to the root directory to visualize.</param>
        public void BuildFromDirectory(string rootPath)
        {
            _treeView.BeginUpdate();
            _treeView.Nodes.Clear();

            TreeNode rootNode = CreateDirectoryNode(new DirectoryInfo(rootPath));
            _treeView.Nodes.Add(rootNode);

            _treeView.EndUpdate();
        }

        /// <summary>
        /// Recursively creates a TreeNode representing the directory and all of its subdirectories and files.
        /// </summary>
        /// <param name="dirInfo">A <see cref="DirectoryInfo"/> object representing the directory to process.</param>
        /// <returns>A TreeNode that reflects the directory and its contents.</returns>
        private TreeNode CreateDirectoryNode(DirectoryInfo dirInfo)
        {
            TreeNode directoryNode = new TreeNode(dirInfo.Name);

            foreach (var dir in dirInfo.GetDirectories())
            {
                directoryNode.Nodes.Add(CreateDirectoryNode(dir));
            }

            foreach (var file in dirInfo.GetFiles())
            {
                directoryNode.Nodes.Add(new TreeNode(file.Name));
            }

            return directoryNode;
        }
    }
}
