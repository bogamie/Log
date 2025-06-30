using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace Forensics_Tool.Forms {
    public partial class FormContent : Form {
        public FormContent() {
            InitializeComponent();
            BuildContentTree("C:/Users/Bogamie/Desktop/bogamie/자료/데이터/Content.json");
            contentTree.NodeMouseDoubleClick += contentTree_NodeMouseDoubleClick;
        }

        private void BuildContentTree(string jsonPath) {
            if (!Path.Exists(jsonPath)) {

            }
            contentTree.Nodes.Clear(); // 트리 초기화

            string jsonContent = File.ReadAllText(jsonPath); // JSON 파일 읽기
            JsonDocument jsonDoc = JsonDocument.Parse(jsonContent); // JSON 파싱

            JsonElement root = jsonDoc.RootElement; // 루트 엘리먼트 가져오기

            ClassifyNode(contentTree.Nodes, root);
        }   

        private void ClassifyNode(TreeNodeCollection nodes, JsonElement section) {
            foreach (JsonProperty prop in section.EnumerateObject()) {
                JsonElement element = prop.Value;

                switch (element.ValueKind) {
                    case JsonValueKind.Object:
                        JsonObject(nodes, element, prop);
                        break;
                    case JsonValueKind.Array:
                        JsonArray(nodes, element, prop);
                        break;
                    default:
                        //Debug.WriteLine($"[기타-Primitive] {prop.Name} → {element.GetRawText()}");
                        break;
                }
            }
        }

        private void JsonObject(TreeNodeCollection nodes, JsonElement element, JsonProperty prop) {
            TreeNode node;
            bool hasTag = element.TryGetProperty("__tag", out JsonElement tag);
            bool hasValue = element.TryGetProperty("value", out JsonElement value);

            if (hasTag && hasValue) {
                //Debug.WriteLine($"[값+태그] {prop.Name} → value={value.GetRawText()}, tag={tag.GetString()}");
                string label = $"{prop.Name} ({value.GetRawText()})";
                node = new TreeNode(label) { Tag = tag.GetString() };
                nodes.Add(node);
            }
            else if (hasTag) {
                //Debug.WriteLine($"[태그만] {prop.Name} → tag={tag.GetString()}");
                string label = $"{prop.Name}";
                node = new TreeNode(label) { Tag = tag.GetString() };
                nodes.Add(node);
            }
            else {
                //Debug.WriteLine($"[기타-Object] {prop.Name}");
                node = new TreeNode(prop.Name);
                nodes.Add(node);
                ClassifyNode(node.Nodes, element); // 재귀
            }
        }

        private void JsonArray(TreeNodeCollection nodes, JsonElement element, JsonProperty prop) {
            TreeNode arrayNode = new TreeNode($"{prop.Name} [Array]");
            nodes.Add(arrayNode);

            int index = 0;
            foreach (JsonElement item in element.EnumerateArray()) {
                TreeNode itemNode = new TreeNode($"[{index}]");
                arrayNode.Nodes.Add(itemNode);
                ClassifyNode(itemNode.Nodes, item); // 재귀
                index++;
            }
        }

        private void contentTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e) {
            if (e.Node.Tag != null) {
                string tag = e.Node.Tag.ToString();
                Debug.WriteLine($"Node double-clicked: {tag}");
                // 여기에 노드 더블 클릭 시 처리할 로직 추가
            }
        }
    }
}

