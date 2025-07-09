namespace SampleToWav
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
            this._openFileButton = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this._saveFileButton = new System.Windows.Forms.Button();
            this._samplingRateTextBox = new System.Windows.Forms.TextBox();
            this._outputCurveCombo = new System.Windows.Forms.ComboBox();
            this._interpreterCombo = new System.Windows.Forms.ComboBox();
            this._startOffsetTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this._byteCountTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this._wholeFileCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this._previewButton = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this._sampleDepthCombo = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // _openFileButton
            // 
            this._openFileButton.Location = new System.Drawing.Point(6, 30);
            this._openFileButton.Name = "_openFileButton";
            this._openFileButton.Size = new System.Drawing.Size(171, 40);
            this._openFileButton.TabIndex = 0;
            this._openFileButton.Text = "Open file(s)...";
            this._openFileButton.UseVisualStyleBackColor = true;
            this._openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(203, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(1143, 31);
            this.textBox1.TabIndex = 0;
            // 
            // _saveFileButton
            // 
            this._saveFileButton.Location = new System.Drawing.Point(203, 176);
            this._saveFileButton.Name = "_saveFileButton";
            this._saveFileButton.Size = new System.Drawing.Size(171, 40);
            this._saveFileButton.TabIndex = 6;
            this._saveFileButton.Text = "Save file(s)";
            this._saveFileButton.UseVisualStyleBackColor = true;
            this._saveFileButton.Click += new System.EventHandler(this.saveFileButton_Click);
            // 
            // _samplingRateTextBox
            // 
            this._samplingRateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._samplingRateTextBox.Location = new System.Drawing.Point(202, 84);
            this._samplingRateTextBox.Name = "_samplingRateTextBox";
            this._samplingRateTextBox.Size = new System.Drawing.Size(1144, 31);
            this._samplingRateTextBox.TabIndex = 3;
            this._samplingRateTextBox.Text = "4000";
            // 
            // _outputCurveCombo
            // 
            this._outputCurveCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._outputCurveCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._outputCurveCombo.FormattingEnabled = true;
            this._outputCurveCombo.Location = new System.Drawing.Point(202, 37);
            this._outputCurveCombo.Name = "_outputCurveCombo";
            this._outputCurveCombo.Size = new System.Drawing.Size(1144, 33);
            this._outputCurveCombo.TabIndex = 1;
            // 
            // _interpreterCombo
            // 
            this._interpreterCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._interpreterCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._interpreterCombo.FormattingEnabled = true;
            this._interpreterCombo.Location = new System.Drawing.Point(202, 211);
            this._interpreterCombo.Name = "_interpreterCombo";
            this._interpreterCombo.Size = new System.Drawing.Size(1144, 33);
            this._interpreterCombo.TabIndex = 7;
            this._interpreterCombo.SelectedIndexChanged += new System.EventHandler(this._interpreterCombo_SelectedIndexChanged);
            // 
            // _startOffsetTextBox
            // 
            this._startOffsetTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._startOffsetTextBox.Location = new System.Drawing.Point(202, 121);
            this._startOffsetTextBox.Name = "_startOffsetTextBox";
            this._startOffsetTextBox.Size = new System.Drawing.Size(1144, 31);
            this._startOffsetTextBox.TabIndex = 3;
            this._startOffsetTextBox.Text = "0x000000";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 25);
            this.label2.TabIndex = 2;
            this.label2.Text = "Start offset";
            // 
            // _byteCountTextBox
            // 
            this._byteCountTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._byteCountTextBox.Location = new System.Drawing.Point(202, 166);
            this._byteCountTextBox.Name = "_byteCountTextBox";
            this._byteCountTextBox.Size = new System.Drawing.Size(1144, 31);
            this._byteCountTextBox.TabIndex = 5;
            this._byteCountTextBox.Text = "0xffffff";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 169);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 25);
            this.label3.TabIndex = 4;
            this.label3.Text = "Length";
            // 
            // _wholeFileCheckBox
            // 
            this._wholeFileCheckBox.AutoSize = true;
            this._wholeFileCheckBox.Location = new System.Drawing.Point(10, 84);
            this._wholeFileCheckBox.Name = "_wholeFileCheckBox";
            this._wholeFileCheckBox.Size = new System.Drawing.Size(139, 29);
            this._wholeFileCheckBox.TabIndex = 1;
            this._wholeFileCheckBox.Text = "Whole file";
            this._wholeFileCheckBox.UseVisualStyleBackColor = true;
            this._wholeFileCheckBox.CheckedChanged += new System.EventHandler(this.wholeFileCheckBox_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this._openFileButton);
            this.groupBox1.Controls.Add(this._byteCountTextBox);
            this.groupBox1.Controls.Add(this._interpreterCombo);
            this.groupBox1.Controls.Add(this._wholeFileCheckBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this._startOffsetTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(20, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1352, 260);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 214);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(123, 25);
            this.label5.TabIndex = 6;
            this.label5.Text = "Data format";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this._previewButton);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this._sampleDepthCombo);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this._outputCurveCombo);
            this.groupBox2.Controls.Add(this._saveFileButton);
            this.groupBox2.Controls.Add(this._samplingRateTextBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Location = new System.Drawing.Point(20, 272);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1352, 234);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Output";
            // 
            // _previewButton
            // 
            this._previewButton.Appearance = System.Windows.Forms.Appearance.Button;
            this._previewButton.Location = new System.Drawing.Point(381, 176);
            this._previewButton.Name = "_previewButton";
            this._previewButton.Size = new System.Drawing.Size(176, 40);
            this._previewButton.TabIndex = 7;
            this._previewButton.Text = "Preview";
            this._previewButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this._previewButton.UseVisualStyleBackColor = true;
            this._previewButton.CheckedChanged += new System.EventHandler(this._previewButton_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 25);
            this.label1.TabIndex = 4;
            this.label1.Text = "Sample depth";
            // 
            // _sampleDepthCombo
            // 
            this._sampleDepthCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this._sampleDepthCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this._sampleDepthCombo.FormattingEnabled = true;
            this._sampleDepthCombo.Location = new System.Drawing.Point(202, 129);
            this._sampleDepthCombo.Name = "_sampleDepthCombo";
            this._sampleDepthCombo.Size = new System.Drawing.Size(1144, 33);
            this._sampleDepthCombo.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(69, 25);
            this.label4.TabIndex = 0;
            this.label4.Text = "Curve";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(190, 25);
            this.label6.TabIndex = 2;
            this.label6.Text = "Sampling rate (Hz)";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 6);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(162, 73);
            this.button1.TabIndex = 2;
            this.button1.Text = "4-bit convert";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(174, 6);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(162, 73);
            this.button2.TabIndex = 3;
            this.button2.Text = "PDM convert";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(342, 6);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(162, 73);
            this.button3.TabIndex = 4;
            this.button3.Text = "1-bit convert";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(20, 512);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(162, 73);
            this.button4.TabIndex = 5;
            this.button4.Text = "Batch";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.batchButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Wave files (*.wav)|*.wav";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1400, 962);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(8, 39);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1384, 915);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ROM to WAV";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.button2);
            this.tabPage2.Controls.Add(this.button3);
            this.tabPage2.Location = new System.Drawing.Point(8, 39);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1019, 228);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "WAV to data";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 962);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "Sample to WAV converter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button _openFileButton;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button _saveFileButton;
        private System.Windows.Forms.TextBox _samplingRateTextBox;
        private System.Windows.Forms.ComboBox _outputCurveCombo;
        private System.Windows.Forms.ComboBox _interpreterCombo;
        private System.Windows.Forms.TextBox _startOffsetTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox _byteCountTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox _wholeFileCheckBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox _sampleDepthCombo;
        private System.Windows.Forms.CheckBox _previewButton;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

