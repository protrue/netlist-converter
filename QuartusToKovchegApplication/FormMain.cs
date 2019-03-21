using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace QuartusToKovchegApplication
{
    public partial class FormMain : Form
    {
        private readonly string _defaultQuartusFilePath = AppDomain.CurrentDomain.BaseDirectory + "quartusScheme.vo";
        private readonly string _defaultKovchegFilePath = AppDomain.CurrentDomain.BaseDirectory + "kovchegScheme.v";

        public FormMain()
        {
            InitializeComponent();

            openFileDialog.FileName = _defaultQuartusFilePath;
            saveFileDialog.FileName = _defaultKovchegFilePath;

            textBoxInputFilePath.Text = _defaultQuartusFilePath;
            textBoxOutputFilePath.Text = _defaultKovchegFilePath;
        }

        private void ButtonSetInputFileClick(object sender, EventArgs e)
        {
            var dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                textBoxInputFilePath.Text = openFileDialog.FileName;
                richTextBoxQuartusScheme.Text = File.ReadAllText(openFileDialog.FileName);
            }
        }

        private void ButtonSetOutputFileClick(object sender, EventArgs e)
        {
            var dialogResult = saveFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                textBoxOutputFilePath.Text = saveFileDialog.FileName;
            }
        }

        private string GetFullExceptionTrace(Exception exception)
        {
            var exceptionMessages = new List<string>() { exception.Message };
            var currentException = exception;

            while (currentException.InnerException != null)
            {
                exceptionMessages.Add(currentException.InnerException.Message);
                currentException = currentException.InnerException;
            }

            return string.Join(Environment.NewLine, exceptionMessages);
        }

        private void ButtonTranslateClick(object sender, EventArgs e)
        {
            try
            {
                var quartusText = richTextBoxQuartusScheme.Text;
                var kovchegText = QuartusToKovchegTranslator.Translator.Translate(quartusText);

                richTextBoxLog.Text = "Трансляция успешно завершена" + Environment.NewLine;

                richTextBoxKovchegScheme.Text = kovchegText;
                tabControlView.SelectedTab = tabPageKovcheg;

                File.WriteAllText(textBoxOutputFilePath.Text, kovchegText);
            }
            catch (Exception exception)
            {
                richTextBoxLog.Text += GetFullExceptionTrace(exception) + Environment.NewLine + Environment.NewLine;
                tabControlView.SelectedTab = tabPageLog;
            }

            richTextBoxLog.Text += string.Join(Environment.NewLine,
                $"Объектное представление схемы из Quartus:",
                JsonConvert.SerializeObject(QuartusToKovchegTranslator.Translator.LastQuartusScheme, Formatting.Indented),
                Environment.NewLine,
                $"Объектное представление схемы для Ковчег:",
                JsonConvert.SerializeObject(QuartusToKovchegTranslator.Translator.LastKovchegScheme, Formatting.Indented));

        }
    }
}
