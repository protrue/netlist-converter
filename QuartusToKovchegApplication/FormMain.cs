using System;
using System.IO;
using System.Windows.Forms;

namespace QuartusToKovchegApplication
{
    public partial class FormMain : Form
    {
        private string _pathToQuartusFile;
        private string _pathToKovchegFile;

        private readonly string _defaultQuartusFilePath = AppDomain.CurrentDomain.BaseDirectory + "quartusScheme.vo";
        private readonly string _defaultKovchegFilePath = AppDomain.CurrentDomain.BaseDirectory + "kovchegScheme.v";

        public FormMain()
        {
            InitializeComponent();

            openFileDialog.FileName = _defaultQuartusFilePath;
            saveFileDialog.FileName = _defaultKovchegFilePath;

            textBoxInputFilePath.Text = _defaultQuartusFilePath;
            textBoxOutputFilePath.Text = _defaultKovchegFilePath;

            _pathToQuartusFile = _defaultQuartusFilePath;
            _pathToKovchegFile = _defaultKovchegFilePath;
        }

        private string LoadFromFile(string path)
        {
            string text;

            using (var streamReader = new StreamReader(path))
                text = streamReader.ReadToEnd();

            return text;
        }

        private void WriteToFile(string text, string path)
        {
            using (var streamReader = new StreamWriter(path, false))
                streamReader.Write(text);
        }

        private void ButtonSetInputFileClick(object sender, EventArgs e)
        {
            var dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                _pathToQuartusFile = openFileDialog.FileName;
                textBoxInputFilePath.Text = openFileDialog.FileName;
            }
        }

        private void ButtonSetOutputFileClick(object sender, EventArgs e)
        {
            var dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                _pathToKovchegFile = saveFileDialog.FileName;
                textBoxInputFilePath.Text = saveFileDialog.FileName;
            }
        }

        private void ButtonTranslateClick(object sender, EventArgs e)
        {
            //try
            //{
                var quartusText = LoadFromFile(_pathToQuartusFile);

                var kovchegText = QuartusToKovchegTranslator.Translator.Translate(quartusText);

                WriteToFile(kovchegText, _pathToKovchegFile);
            //}
            //catch (Exception exception)
            //{
            //    richTextBox.Text += exception.Message + Environment.NewLine;
            //    //return;
            //    throw;
            //}

            //richTextBox.Text += "Трансляция успешно завершена";

            richTextBox.Text = $"{QuartusToKovchegTranslator.Translator.LastQuartusText}\n\n{QuartusToKovchegTranslator.Translator.LastQuartusScheme}\n\n{QuartusToKovchegTranslator.Translator.LastKovchegScheme}\n\n{QuartusToKovchegTranslator.Translator.LastKovchegText}";
        }
    }
}
