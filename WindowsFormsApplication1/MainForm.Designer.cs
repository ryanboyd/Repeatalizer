namespace WindowsFormsApplication1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.WordWindowSizeTextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.BgWorker = new System.ComponentModel.BackgroundWorker();
            this.ScanSubfolderCheckbox = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.FolderBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.label2 = new System.Windows.Forms.Label();
            this.PunctuationBox = new System.Windows.Forms.TextBox();
            this.FilenameLabel = new System.Windows.Forms.Label();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.FunctionWordTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.EncodingDropdown = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.PhraseLengthTextbox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.BigWordTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // WordWindowSizeTextbox
            // 
            this.WordWindowSizeTextbox.Location = new System.Drawing.Point(206, 35);
            this.WordWindowSizeTextbox.Name = "WordWindowSizeTextbox";
            this.WordWindowSizeTextbox.Size = new System.Drawing.Size(53, 20);
            this.WordWindowSizeTextbox.TabIndex = 0;
            this.WordWindowSizeTextbox.Text = "50";
            this.WordWindowSizeTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.WordWindowSizeTextbox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.WordWindowSizeTextbox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(191, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(101, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Word Window Size:";
            // 
            // BgWorker
            // 
            this.BgWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BgWorker_DoWork);
            this.BgWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BgWorker_RunWorkerCompleted);
            // 
            // ScanSubfolderCheckbox
            // 
            this.ScanSubfolderCheckbox.AutoSize = true;
            this.ScanSubfolderCheckbox.Location = new System.Drawing.Point(240, 285);
            this.ScanSubfolderCheckbox.Name = "ScanSubfolderCheckbox";
            this.ScanSubfolderCheckbox.Size = new System.Drawing.Size(108, 17);
            this.ScanSubfolderCheckbox.TabIndex = 2;
            this.ScanSubfolderCheckbox.Text = "Scan subfolders?";
            this.ScanSubfolderCheckbox.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(218, 232);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 47);
            this.button1.TabIndex = 3;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FolderBrowser
            // 
            this.FolderBrowser.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.FolderBrowser.ShowNewFolderButton = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(203, 122);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Characters to Remove:";
            // 
            // PunctuationBox
            // 
            this.PunctuationBox.AcceptsTab = true;
            this.PunctuationBox.Location = new System.Drawing.Point(204, 138);
            this.PunctuationBox.Name = "PunctuationBox";
            this.PunctuationBox.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.PunctuationBox.Size = new System.Drawing.Size(180, 20);
            this.PunctuationBox.TabIndex = 4;
            this.PunctuationBox.Text = ";:\"@#$%^&\t*(){}\\|,/<>`~[].?!";
            // 
            // FilenameLabel
            // 
            this.FilenameLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FilenameLabel.Location = new System.Drawing.Point(25, 313);
            this.FilenameLabel.Name = "FilenameLabel";
            this.FilenameLabel.Size = new System.Drawing.Size(359, 15);
            this.FilenameLabel.TabIndex = 6;
            this.FilenameLabel.Text = "Waiting to Repeatalize texts...";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.FileName = "Repeatalizer.csv";
            this.saveFileDialog.Filter = "CSV Files|*.csv";
            this.saveFileDialog.Title = "Please choose where to save your output";
            // 
            // FunctionWordTextBox
            // 
            this.FunctionWordTextBox.Location = new System.Drawing.Point(25, 35);
            this.FunctionWordTextBox.Multiline = true;
            this.FunctionWordTextBox.Name = "FunctionWordTextBox";
            this.FunctionWordTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.FunctionWordTextBox.Size = new System.Drawing.Size(143, 263);
            this.FunctionWordTextBox.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(99, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Function Word List:";
            // 
            // EncodingDropdown
            // 
            this.EncodingDropdown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.EncodingDropdown.FormattingEnabled = true;
            this.EncodingDropdown.Location = new System.Drawing.Point(204, 189);
            this.EncodingDropdown.Name = "EncodingDropdown";
            this.EncodingDropdown.Size = new System.Drawing.Size(180, 21);
            this.EncodingDropdown.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(203, 173);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Text Encoding:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(243, 71);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(102, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "Max Phrase Length:";
            // 
            // PhraseLengthTextbox
            // 
            this.PhraseLengthTextbox.Location = new System.Drawing.Point(258, 87);
            this.PhraseLengthTextbox.Name = "PhraseLengthTextbox";
            this.PhraseLengthTextbox.Size = new System.Drawing.Size(53, 20);
            this.PhraseLengthTextbox.TabIndex = 12;
            this.PhraseLengthTextbox.Text = "3";
            this.PhraseLengthTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(307, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Big Word Size:";
            // 
            // BigWordTextBox
            // 
            this.BigWordTextBox.Location = new System.Drawing.Point(312, 35);
            this.BigWordTextBox.Name = "BigWordTextBox";
            this.BigWordTextBox.Size = new System.Drawing.Size(53, 20);
            this.BigWordTextBox.TabIndex = 13;
            this.BigWordTextBox.Text = "6";
            this.BigWordTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.BigWordTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.BigWordTextBox_KeyPress);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.MediumSeaGreen;
            this.ClientSize = new System.Drawing.Size(401, 336);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.BigWordTextBox);
            this.Controls.Add(this.PhraseLengthTextbox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.EncodingDropdown);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.FunctionWordTextBox);
            this.Controls.Add(this.FilenameLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.PunctuationBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ScanSubfolderCheckbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.WordWindowSizeTextbox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(417, 375);
            this.MinimumSize = new System.Drawing.Size(417, 375);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Repeatalizer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox WordWindowSizeTextbox;
        private System.Windows.Forms.Label label1;
        private System.ComponentModel.BackgroundWorker BgWorker;
        private System.Windows.Forms.CheckBox ScanSubfolderCheckbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FolderBrowserDialog FolderBrowser;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox PunctuationBox;
        private System.Windows.Forms.Label FilenameLabel;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TextBox FunctionWordTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox EncodingDropdown;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox PhraseLengthTextbox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox BigWordTextBox;
    }
}

