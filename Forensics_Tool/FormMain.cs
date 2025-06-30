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
                Debug.WriteLine($"������ ���: {selected}");
                Debug.WriteLine($"������ �ε���: {selectedIndex}");

                if (selectedIndex == 0) {
                    // �� tar ���� ����
                    Extractor.Extract(selectedIndex, selected);
                }
                else if (selectedIndex == 1) {
                    // ���� �м� ���� ����
                    Extractor.Extract(selectedIndex, selected);
                }
                else {
                    // ���� ��������� MainForm�� �ݾƹ����� ���� �Ǵ�
                    this.Close(); // �Ǵ� ����
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
