using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.UI
{
    internal class TreeViewBuilder
    {
        private readonly System.Windows.Forms.TreeView _treeView;
        private readonly TextUI _textUI;

        /// <summary>
        /// Initializes a new instance of the <see cref="TreeViewBuilder"/> class with the specified TreeView control.
        /// </summary>
        /// <param name="treeView">The TreeView control to populate with directory data.</param>
        /// <exception cref="ArgumentNullException">Thrown if the provided TreeView is null.</exception>
        public TreeViewBuilder(System.Windows.Forms.TreeView treeView, TextUI textUI)
        {
            _treeView = treeView ?? throw new ArgumentNullException(nameof(treeView));
            _treeView.NodeMouseDoubleClick += _treeView_NodeMouseDoubleClick;
            _textUI = textUI ?? throw new ArgumentNullException(nameof(textUI));
        }

        /// <summary>
        /// 트리 뷰 노드를 더블 클릭했을 때 발생하는 이벤트 핸들러.
        /// 선택된 파일이 텍스트 파일(.txt, .log, .dat)인 경우, 해당 파일의 내용을 TextUI에 표시한다.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _treeView_NodeMouseDoubleClick(object? sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode selectedNode = e.Node;
            string fileExt = Path.GetExtension(selectedNode.Text).ToLowerInvariant();

            if (1 == 1)
            {
                if (selectedNode.Tag is string filePath)
                {
                    _textUI.ShowFileContent(filePath);
                }
            }
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
                TreeNode fileNode = new TreeNode(file.Name);
                fileNode.Tag = file.FullName;
                directoryNode.Nodes.Add(fileNode);
            }

            return directoryNode;
        }
    }
}
