using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Log.UI
{
    internal class TextUI
    {
        private readonly TextBox _textBox;

        public TextUI(TextBox textBox)
        {
            _textBox = textBox ?? throw new ArgumentNullException(nameof(textBox));

            _textBox.Multiline = true;
            _textBox.ReadOnly = true;
            _textBox.WordWrap = false;
            _textBox.ScrollBars = ScrollBars.Both;
        }

        public void Clear()
        {
            _textBox.Clear();
        }

        public void ShowFileContent(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("File path cannot be null or empty.", nameof(filePath));
            }

            try
            {
                if (File.Exists(filePath))
                {
                    string[] lines = File.ReadAllLines(filePath); // 줄로 읽기
                    _textBox.Text = string.Join(Environment.NewLine, lines);
                }
                else
                {
                    _textBox.Text = $"파일을 찾을 수 없습니다: {filePath}";
                }
            }
            catch (Exception ex)
            {
                _textBox.Text = $"Error reading file: {ex.Message}";
            }
        }
    }
}
