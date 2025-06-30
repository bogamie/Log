using Forensics_Tool.Forms;
using System.Diagnostics;
using Forensics_Tool.Utilities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Forensics_Tool {
    public partial class FormMain : Form {
        private Form? _activeForm;
        private FormContent? _formContent;
        private FormSearch? _formSearch;
        private FormTimeline? _formTimeline;

        public FormMain() {
            InitializeComponent();
            this.Shown += new System.EventHandler(this.FormMain_Shown);
        }

        private void FormMain_Shown(object sender, EventArgs e) {
            FormStartup formStartup = new FormStartup();

            formStartup.StartPosition = FormStartPosition.Manual;
            formStartup.Location = new Point(
                this.Location.X + (this.Width - formStartup.Width) / 2,
                this.Location.Y + (this.Height - formStartup.Height) / 2
            );

            var result = formStartup.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrEmpty(formStartup.SelectedPath)) {
                int selectedIndex = formStartup.SelectedIndex;
                string selected = formStartup.SelectedPath;
                Debug.WriteLine($"선택한 경로: {selected}");
                Debug.WriteLine($"선택한 인덱스: {selectedIndex}");

                if (selectedIndex == 0) {
                    // 새 tar 파일 선택
                    Extractor.Extract(selectedIndex, selected);
                }
                else if (selectedIndex == 1) {
                    // 기존 분석 폴더 선택
                    Extractor.Extract(selectedIndex, selected);
                }
                else {
                    // 선택 취소했으면 MainForm도 닫아버릴지 여부 판단
                    this.Close(); // 또는 무시
                }
            }
        }

        private void btnContent_Click(object sender, EventArgs e) {
            if (_formContent?.IsDisposed ?? true) {
                _formContent = new FormContent();
            }
            OpenChildForm(_formContent, sender);
        }

        private void btnSearch_Click(object sender, EventArgs e) {
            if (_formSearch?.IsDisposed ?? true) {
                _formSearch = new FormSearch();
            }
            OpenChildForm(_formSearch, sender);
        }

        private void btnTimeline_Click(object sender, EventArgs e) {
            if (_formTimeline?.IsDisposed ?? true) {
                _formTimeline = new FormTimeline();
            }
            OpenChildForm(_formTimeline, sender);
        }

        private void OpenChildForm(Form childForm, object btnSender) {
            if (_activeForm != null && _activeForm != childForm) {
                _activeForm.Hide();
            }

            _activeForm = childForm;

            if (!mainPanel.Controls.Contains(childForm)) {
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                this.mainPanel.Controls.Add(childForm);
            }

            childForm.BringToFront();
            childForm.Show();
        }
    }
}
