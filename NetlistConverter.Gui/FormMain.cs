using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Generator;
using NetlistConverter.Analysis;
using NetlistConverter.Converter;
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

            if (File.Exists(textBoxInputFilePath.Text))
                richTextBoxQuartusScheme.Text = File.ReadAllText(textBoxInputFilePath.Text);
        }

        private void ButtonSetInputFileClick(object sender, EventArgs e)
        {
            var dialogResult = openFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                textBoxInputFilePath.Text = openFileDialog.FileName;
                //richTextBoxQuartusScheme.Text = File.ReadAllText(openFileDialog.FileName);
                
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
            //try
            //{
            var quartusText = richTextBoxQuartusScheme.Text;
            //var kovchegText = QuartusToKovchegTranslator.Translator.Translate(quartusText);

            var quartusNetlist = File.ReadAllText(textBoxInputFilePath.Text);

            richTextBoxLog.Text += "Файл считан успешно" + Environment.NewLine;

            var analyzer = new NetlistAnalyzer();
            var quartusScheme = analyzer.Analyze(quartusNetlist);

            richTextBoxLog.Text += "Анализ завершён успешно" + Environment.NewLine;

            var transformer = new NetlistTransformer();
            var kovchegScheme = transformer.Transform(quartusScheme);

            richTextBoxLog.Text += "Преобразование завершено успешно" + Environment.NewLine;

            var generator = new NetlistGenerator();
            var kovchegNetlist = generator.GenerateNetlist(kovchegScheme);

            richTextBoxLog.Text += "Генерация завершена успешно" + Environment.NewLine;

            richTextBoxKovchegScheme.Text = kovchegNetlist;
            tabControlView.SelectedTab = tabPageKovcheg;

            File.WriteAllText(textBoxOutputFilePath.Text, kovchegNetlist);

            richTextBoxLog.Text += "Запись в файл завершена успешно" + Environment.NewLine;
            //}
            //catch (Exception exception)
            //{
            //    richTextBoxLog.Text += GetFullExceptionTrace(exception) + Environment.NewLine + Environment.NewLine;
            //    tabControlView.SelectedTab = tabPageLog;
            //}

            richTextBoxLog.Text += string.Join(Environment.NewLine,
                $"Объектное представление схемы из Quartus:",
                JsonConvert.SerializeObject(quartusScheme, Formatting.Indented),
                Environment.NewLine,
                $"Объектное представление схемы для Ковчег:",
                JsonConvert.SerializeObject(kovchegScheme, Formatting.Indented));

            listBoxQuartusInstances.Items.Clear();
            listBoxQuartusNets.Items.Clear();
            listBoxKovchegInstances.Items.Clear();
            listBoxKovchegNets.Items.Clear();

            listBoxQuartusInstances.Items.AddRange(quartusScheme.Instances.Select(i => $"{i.ModuleIdentifier} {i.Identifier}").ToArray());
            listBoxQuartusNets.Items.AddRange(quartusScheme.Nets.Select(i => $"{i.NetType} {i.Identifier} {i.ConnectedNet}").ToArray());
            listBoxKovchegInstances.Items.AddRange(kovchegScheme.Instances.Select(i => $"{i.ModuleIdentifier} {i.Identifier}").ToArray());
            listBoxKovchegNets.Items.AddRange(kovchegScheme.Nets.Select(i => $"{i.NetType} {i.Identifier} {i.ConnectedNet}").ToArray());
        }
    }
}
