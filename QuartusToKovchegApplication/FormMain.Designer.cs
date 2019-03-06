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
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.labelInput = new System.Windows.Forms.Label();
            this.textBoxInputFilePath = new System.Windows.Forms.TextBox();
            this.richTextBox = new System.Windows.Forms.RichTextBox();
            this.textBoxOutputFilePath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonSetInputFile = new System.Windows.Forms.Button();
            this.buttonSetOutputFile = new System.Windows.Forms.Button();
            this.buttonTranslate = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(559, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip";
            // 
            // statusStrip
            // 
            this.statusStrip.Location = new System.Drawing.Point(0, 352);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(559, 22);
            this.statusStrip.TabIndex = 1;
            this.statusStrip.Text = "statusStrip";
            // 
            // labelInput
            // 
            this.labelInput.AutoSize = true;
            this.labelInput.Location = new System.Drawing.Point(12, 24);
            this.labelInput.Name = "labelInput";
            this.labelInput.Size = new System.Drawing.Size(60, 13);
            this.labelInput.TabIndex = 2;
            this.labelInput.Text = "Quartus file";
            // 
            // textBoxInputFilePath
            // 
            this.textBoxInputFilePath.Location = new System.Drawing.Point(12, 40);
            this.textBoxInputFilePath.Name = "textBoxInputFilePath";
            this.textBoxInputFilePath.Size = new System.Drawing.Size(500, 20);
            this.textBoxInputFilePath.TabIndex = 3;
            // 
            // richTextBox
            // 
            this.richTextBox.Location = new System.Drawing.Point(12, 134);
            this.richTextBox.Name = "richTextBox";
            this.richTextBox.Size = new System.Drawing.Size(531, 200);
            this.richTextBox.TabIndex = 4;
            this.richTextBox.Text = "";
            // 
            // textBoxOutputFilePath
            // 
            this.textBoxOutputFilePath.Location = new System.Drawing.Point(12, 79);
            this.textBoxOutputFilePath.Name = "textBoxOutputFilePath";
            this.textBoxOutputFilePath.Size = new System.Drawing.Size(500, 20);
            this.textBoxOutputFilePath.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Kovcheg file";
            // 
            // buttonSetInputFile
            // 
            this.buttonSetInputFile.Location = new System.Drawing.Point(518, 38);
            this.buttonSetInputFile.Name = "buttonSetInputFile";
            this.buttonSetInputFile.Size = new System.Drawing.Size(25, 23);
            this.buttonSetInputFile.TabIndex = 7;
            this.buttonSetInputFile.Text = "...";
            this.buttonSetInputFile.UseVisualStyleBackColor = true;
            this.buttonSetInputFile.Click += new System.EventHandler(this.ButtonSetInputFileClick);
            // 
            // buttonSetOutputFile
            // 
            this.buttonSetOutputFile.Location = new System.Drawing.Point(518, 77);
            this.buttonSetOutputFile.Name = "buttonSetOutputFile";
            this.buttonSetOutputFile.Size = new System.Drawing.Size(25, 23);
            this.buttonSetOutputFile.TabIndex = 8;
            this.buttonSetOutputFile.Text = "...";
            this.buttonSetOutputFile.UseVisualStyleBackColor = true;
            this.buttonSetOutputFile.Click += new System.EventHandler(this.ButtonSetOutputFileClick);
            // 
            // buttonTranslate
            // 
            this.buttonTranslate.Location = new System.Drawing.Point(12, 105);
            this.buttonTranslate.Name = "buttonTranslate";
            this.buttonTranslate.Size = new System.Drawing.Size(100, 23);
            this.buttonTranslate.TabIndex = 9;
            this.buttonTranslate.Text = "Translate";
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
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 374);
            this.Controls.Add(this.buttonTranslate);
            this.Controls.Add(this.buttonSetOutputFile);
            this.Controls.Add(this.buttonSetInputFile);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxOutputFilePath);
            this.Controls.Add(this.richTextBox);
            this.Controls.Add(this.textBoxInputFilePath);
            this.Controls.Add(this.labelInput);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.MainMenuStrip = this.menuStrip;
            this.Name = "FormMain";
            this.Text = "QuartusToKovcheg";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.Label labelInput;
        private System.Windows.Forms.TextBox textBoxInputFilePath;
        private System.Windows.Forms.RichTextBox richTextBox;
        private System.Windows.Forms.TextBox textBoxOutputFilePath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonSetInputFile;
        private System.Windows.Forms.Button buttonSetOutputFile;
        private System.Windows.Forms.Button buttonTranslate;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
    }
}

