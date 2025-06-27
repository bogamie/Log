using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
                TreeNode fileNode = new TreeNode(file.Name);

//                if (file.Extension == ".log")
//                {
                    try
                    {
                        string rulesPath = @"C:\Users\stara\Desktop\개발\log1\Log\Log\comparison\our_analysis\trunk.json"; // <-- rules.json 경로
                        Debug.WriteLine("룰 시도");
                        string result = LogAnalyzer.Analyze(file.FullName, rulesPath);

                        // ✅ 분석 결과를 콘솔에 출력
                        Debug.WriteLine($"🔍 분석 파일: {file.FullName}");
                        Debug.WriteLine(result);
                        Debug.WriteLine(new string('-', 40));

                        fileNode.Tag = result; // 결과를 TreeNode에 저장할 수도 있음
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"❌ {file.FullName} 분석 실패: {ex.Message}");
                    }
//                }

                directoryNode.Nodes.Add(fileNode);
            }

            return directoryNode;
        }

    }
}
