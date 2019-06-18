namespace QuartusToKovchegApplication
{
    partial class FormMain
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.textBoxInputFilePath = new System.Windows.Forms.TextBox();
            this.textBoxOutputFilePath = new System.Windows.Forms.TextBox();
            this.buttonSetInputFile = new System.Windows.Forms.Button();
            this.buttonSetOutputFile = new System.Windows.Forms.Button();
            this.buttonTranslate = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.tableLayoutPanelMain = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxQuartusFile = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelQuartusFile = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxKovchegFile = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelKovchegFile = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxView = new System.Windows.Forms.GroupBox();
            this.tabControlView = new System.Windows.Forms.TabControl();
            this.tabPageQuartus = new System.Windows.Forms.TabPage();
            this.richTextBoxQuartusScheme = new System.Windows.Forms.RichTextBox();
            this.tabPageKovcheg = new System.Windows.Forms.TabPage();
            this.richTextBoxKovchegScheme = new System.Windows.Forms.RichTextBox();
            this.tabPageLog = new System.Windows.Forms.TabPage();
            this.richTextBoxLog = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanelMain.SuspendLayout();
            this.groupBoxQuartusFile.SuspendLayout();
            this.tableLayoutPanelQuartusFile.SuspendLayout();
            this.groupBoxKovchegFile.SuspendLayout();
            this.tableLayoutPanelKovchegFile.SuspendLayout();
            this.groupBoxView.SuspendLayout();
            this.tabControlView.SuspendLayout();
            this.tabPageQuartus.SuspendLayout();
            this.tabPageKovcheg.SuspendLayout();
            this.tabPageLog.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(784, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 539);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(784, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // textBoxInputFilePath
            // 
            this.textBoxInputFilePath.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxInputFilePath.Location = new System.Drawing.Point(3, 3);
            this.textBoxInputFilePath.Name = "textBoxInputFilePath";
            this.textBoxInputFilePath.Size = new System.Drawing.Size(766, 20);
            this.textBoxInputFilePath.TabIndex = 3;
            // 
            // textBoxOutputFilePath
            // 
            this.textBoxOutputFilePath.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxOutputFilePath.Location = new System.Drawing.Point(3, 3);
            this.textBoxOutputFilePath.Name = "textBoxOutputFilePath";
            this.textBoxOutputFilePath.Size = new System.Drawing.Size(766, 20);
            this.textBoxOutputFilePath.TabIndex = 5;
            // 
            // buttonSetInputFile
            // 
            this.buttonSetInputFile.Location = new System.Drawing.Point(3, 40);
            this.buttonSetInputFile.Name = "buttonSetInputFile";
            this.buttonSetInputFile.Size = new System.Drawing.Size(100, 25);
            this.buttonSetInputFile.TabIndex = 7;
            this.buttonSetInputFile.Text = "Выбрать";
            this.buttonSetInputFile.UseVisualStyleBackColor = true;
            this.buttonSetInputFile.Click += new System.EventHandler(this.ButtonSetInputFileClick);
            // 
            // buttonSetOutputFile
            // 
            this.buttonSetOutputFile.Location = new System.Drawing.Point(3, 40);
            this.buttonSetOutputFile.Name = "buttonSetOutputFile";
            this.buttonSetOutputFile.Size = new System.Drawing.Size(100, 25);
            this.buttonSetOutputFile.TabIndex = 8;
            this.buttonSetOutputFile.Text = "Выбрать";
            this.buttonSetOutputFile.UseVisualStyleBackColor = true;
            this.buttonSetOutputFile.Click += new System.EventHandler(this.ButtonSetOutputFileClick);
            // 
            // buttonTranslate
            // 
            this.buttonTranslate.Location = new System.Drawing.Point(3, 203);
            this.buttonTranslate.Name = "buttonTranslate";
            this.buttonTranslate.Size = new System.Drawing.Size(150, 30);
            this.buttonTranslate.TabIndex = 9;
            this.buttonTranslate.Text = "Конвертировать";
            this.buttonTranslate.UseVisualStyleBackColor = true;
            this.buttonTranslate.Click += new System.EventHandler(this.ButtonTranslateClick);
            // 
            // openFileDialog
            // 
            this.openFileDialog.DefaultExt = "vo";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.DefaultExt = "v";
            // 
            // tableLayoutPanelMain
            // 
            this.tableLayoutPanelMain.ColumnCount = 1;
            this.tableLayoutPanelMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxQuartusFile, 0, 0);
            this.tableLayoutPanelMain.Controls.Add(this.buttonTranslate, 0, 2);
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxKovchegFile, 0, 1);
            this.tableLayoutPanelMain.Controls.Add(this.groupBoxView, 0, 3);
            this.tableLayoutPanelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelMain.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanelMain.Name = "tableLayoutPanelMain";
            this.tableLayoutPanelMain.RowCount = 4;
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanelMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelMain.Size = new System.Drawing.Size(784, 515);
            this.tableLayoutPanelMain.TabIndex = 10;
            // 
            // groupBoxQuartusFile
            // 
            this.groupBoxQuartusFile.Controls.Add(this.tableLayoutPanelQuartusFile);
            this.groupBoxQuartusFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxQuartusFile.Location = new System.Drawing.Point(3, 3);
            this.groupBoxQuartusFile.Name = "groupBoxQuartusFile";
            this.groupBoxQuartusFile.Size = new System.Drawing.Size(778, 94);
            this.groupBoxQuartusFile.TabIndex = 0;
            this.groupBoxQuartusFile.TabStop = false;
            this.groupBoxQuartusFile.Text = "Файл схемы из Quartus";
            // 
            // tableLayoutPanelQuartusFile
            // 
            this.tableLayoutPanelQuartusFile.ColumnCount = 1;
            this.tableLayoutPanelQuartusFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelQuartusFile.Controls.Add(this.textBoxInputFilePath, 0, 0);
            this.tableLayoutPanelQuartusFile.Controls.Add(this.buttonSetInputFile, 0, 1);
            this.tableLayoutPanelQuartusFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelQuartusFile.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanelQuartusFile.Name = "tableLayoutPanelQuartusFile";
            this.tableLayoutPanelQuartusFile.RowCount = 2;
            this.tableLayoutPanelQuartusFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelQuartusFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelQuartusFile.Size = new System.Drawing.Size(772, 75);
            this.tableLayoutPanelQuartusFile.TabIndex = 8;
            // 
            // groupBoxKovchegFile
            // 
            this.groupBoxKovchegFile.Controls.Add(this.tableLayoutPanelKovchegFile);
            this.groupBoxKovchegFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxKovchegFile.Location = new System.Drawing.Point(3, 103);
            this.groupBoxKovchegFile.Name = "groupBoxKovchegFile";
            this.groupBoxKovchegFile.Size = new System.Drawing.Size(778, 94);
            this.groupBoxKovchegFile.TabIndex = 1;
            this.groupBoxKovchegFile.TabStop = false;
            this.groupBoxKovchegFile.Text = "Файл схемы для Ковчег";
            // 
            // tableLayoutPanelKovchegFile
            // 
            this.tableLayoutPanelKovchegFile.ColumnCount = 1;
            this.tableLayoutPanelKovchegFile.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelKovchegFile.Controls.Add(this.textBoxOutputFilePath, 0, 0);
            this.tableLayoutPanelKovchegFile.Controls.Add(this.buttonSetOutputFile, 0, 1);
            this.tableLayoutPanelKovchegFile.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelKovchegFile.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanelKovchegFile.Name = "tableLayoutPanelKovchegFile";
            this.tableLayoutPanelKovchegFile.RowCount = 2;
            this.tableLayoutPanelKovchegFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelKovchegFile.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanelKovchegFile.Size = new System.Drawing.Size(772, 75);
            this.tableLayoutPanelKovchegFile.TabIndex = 9;
            // 
            // groupBoxView
            // 
            this.groupBoxView.Controls.Add(this.tabControlView);
            this.groupBoxView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxView.Location = new System.Drawing.Point(3, 243);
            this.groupBoxView.Name = "groupBoxView";
            this.groupBoxView.Size = new System.Drawing.Size(778, 269);
            this.groupBoxView.TabIndex = 2;
            this.groupBoxView.TabStop = false;
            this.groupBoxView.Text = "Просмотр";
            // 
            // tabControlView
            // 
            this.tabControlView.Controls.Add(this.tabPageQuartus);
            this.tabControlView.Controls.Add(this.tabPageKovcheg);
            this.tabControlView.Controls.Add(this.tabPageLog);
            this.tabControlView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlView.Location = new System.Drawing.Point(3, 16);
            this.tabControlView.Name = "tabControlView";
            this.tabControlView.SelectedIndex = 0;
            this.tabControlView.Size = new System.Drawing.Size(772, 250);
            this.tabControlView.TabIndex = 0;
            // 
            // tabPageQuartus
            // 
            this.tabPageQuartus.Controls.Add(this.richTextBoxQuartusScheme);
            this.tabPageQuartus.Location = new System.Drawing.Point(4, 22);
            this.tabPageQuartus.Name = "tabPageQuartus";
            this.tabPageQuartus.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageQuartus.Size = new System.Drawing.Size(764, 224);
            this.tabPageQuartus.TabIndex = 0;
            this.tabPageQuartus.Text = "Схема из Quartus";
            this.tabPageQuartus.UseVisualStyleBackColor = true;
            // 
            // richTextBoxQuartusScheme
            // 
            this.richTextBoxQuartusScheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxQuartusScheme.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxQuartusScheme.Name = "richTextBoxQuartusScheme";
            this.richTextBoxQuartusScheme.ReadOnly = true;
            this.richTextBoxQuartusScheme.Size = new System.Drawing.Size(758, 218);
            this.richTextBoxQuartusScheme.TabIndex = 0;
            this.richTextBoxQuartusScheme.Text = "";
            // 
            // tabPageKovcheg
            // 
            this.tabPageKovcheg.Controls.Add(this.richTextBoxKovchegScheme);
            this.tabPageKovcheg.Location = new System.Drawing.Point(4, 22);
            this.tabPageKovcheg.Name = "tabPageKovcheg";
            this.tabPageKovcheg.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageKovcheg.Size = new System.Drawing.Size(822, 188);
            this.tabPageKovcheg.TabIndex = 1;
            this.tabPageKovcheg.Text = "Схема для Ковчег";
            this.tabPageKovcheg.UseVisualStyleBackColor = true;
            // 
            // richTextBoxKovchegScheme
            // 
            this.richTextBoxKovchegScheme.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxKovchegScheme.Location = new System.Drawing.Point(3, 3);
            this.richTextBoxKovchegScheme.Name = "richTextBoxKovchegScheme";
            this.richTextBoxKovchegScheme.ReadOnly = true;
            this.richTextBoxKovchegScheme.Size = new System.Drawing.Size(816, 182);
            this.richTextBoxKovchegScheme.TabIndex = 0;
            this.richTextBoxKovchegScheme.Text = "";
            // 
            // tabPageLog
            // 
            this.tabPageLog.Controls.Add(this.richTextBoxLog);
            this.tabPageLog.Location = new System.Drawing.Point(4, 22);
            this.tabPageLog.Name = "tabPageLog";
            this.tabPageLog.Size = new System.Drawing.Size(822, 188);
            this.tabPageLog.TabIndex = 2;
            this.tabPageLog.Text = "Лог";
            this.tabPageLog.UseVisualStyleBackColor = true;
            // 
            // richTextBoxLog
            // 
            this.richTextBoxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxLog.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxLog.Name = "richTextBoxLog";
            this.richTextBoxLog.ReadOnly = true;
            this.richTextBoxLog.Size = new System.Drawing.Size(822, 188);
            this.richTextBoxLog.TabIndex = 5;
            this.richTextBoxLog.Text = "";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.tableLayoutPanelMain);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Преобразователь схем";
            this.tableLayoutPanelMain.ResumeLayout(false);
            this.groupBoxQuartusFile.ResumeLayout(false);
            this.tableLayoutPanelQuartusFile.ResumeLayout(false);
            this.tableLayoutPanelQuartusFile.PerformLayout();
            this.groupBoxKovchegFile.ResumeLayout(false);
            this.tableLayoutPanelKovchegFile.ResumeLayout(false);
            this.tableLayoutPanelKovchegFile.PerformLayout();
            this.groupBoxView.ResumeLayout(false);
            this.tabControlView.ResumeLayout(false);
            this.tabPageQuartus.ResumeLayout(false);
            this.tabPageKovcheg.ResumeLayout(false);
            this.tabPageLog.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.TextBox textBoxInputFilePath;
        private System.Windows.Forms.TextBox textBoxOutputFilePath;
        private System.Windows.Forms.Button buttonSetInputFile;
        private System.Windows.Forms.Button buttonSetOutputFile;
        private System.Windows.Forms.Button buttonTranslate;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelMain;
        private System.Windows.Forms.GroupBox groupBoxQuartusFile;
        private System.Windows.Forms.GroupBox groupBoxKovchegFile;
        private System.Windows.Forms.GroupBox groupBoxView;
        private System.Windows.Forms.TabControl tabControlView;
        private System.Windows.Forms.TabPage tabPageQuartus;
        private System.Windows.Forms.TabPage tabPageKovcheg;
        private System.Windows.Forms.RichTextBox richTextBoxQuartusScheme;
        private System.Windows.Forms.RichTextBox richTextBoxKovchegScheme;
        private System.Windows.Forms.TabPage tabPageLog;
        private System.Windows.Forms.RichTextBox richTextBoxLog;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelQuartusFile;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelKovchegFile;
    }
}

