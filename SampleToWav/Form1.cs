using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SampleToWav
{
    public partial class Form1 : Form
    {
        private List<string> _files;

        public Form1()
        {
            InitializeComponent();
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog {CheckFileExists = true, Filter = "All files|*.*", Multiselect = true})
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) return;
                _files = new List<string>(dialog.FileNames);
                if (_files.Count == 1)
                {
                    textBox1.Text = _files[0];
                }
                else
                {
                    textBox1.Text = string.Format("{0} files selected", _files.Count);
                }
            }
        }

        private void saveFileButton_Click(object sender, EventArgs e)
        {
            var startOffset = GetInt(_startOffsetTextBox.Text);
            var count = GetInt(_byteCountTextBox.Text);
            var samplingRate = GetInt(_samplingRateTextBox.Text);
            var interpreter = _interpreterCombo.SelectedItem as IDataInterpreter;
            var sampleGenerator = _outputCurveCombo.SelectedItem as IValueToSample;
            var wavWriter = _sampleDepthCombo.SelectedItem as IWavWriter;

            if (_wholeFileCheckBox.Checked)
            {
                startOffset = 0;
                count = Int32.MaxValue;
            }

            if (_files.Count == 1)
            {
                using (var dialog = new SaveFileDialog {Filter = "Wave files (*.wav)|*.wav"})
                {
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        SaveWav(_files[0], startOffset, count, dialog.FileName, samplingRate, interpreter, sampleGenerator, wavWriter);
                    }
                }
            }
            else
            {
                foreach (var file in _files)
                {
                    SaveWav(file, startOffset, count, file + ".wav", samplingRate, interpreter, sampleGenerator, wavWriter);
                }
            }

            MessageBox.Show("Save complete");
        }

        private uint GetInt(string text)
        {
            return text.StartsWith("0x") ? Convert.ToUInt32(text, 16) : Convert.ToUInt32(text);
        }

        private static void SaveWav(string inputFilename, uint startOffset, uint count, string outputFilename, uint samplingRate, IDataInterpreter interpreter, IValueToSample sampleRenderer, IWavWriter writer)
        {
            using (var fileStream = new FileStream(inputFilename, FileMode.Open))
            using (var input = new BinaryReader(fileStream))
            {
                writer.Encode(
                    outputFilename, 
                    samplingRate, 
                    sampleRenderer.ValuesToSamples(
                        interpreter.GetSamples(
                            GetBytes(
                                input, 
                                startOffset, 
                                count))));
            }
        }

        // Makes a BinaryReader into an IEnumerable<byte> over a range
        private static IEnumerable<byte> GetBytes(BinaryReader input, uint startOffset, uint count)
        {
            input.BaseStream.Seek(startOffset, SeekOrigin.Begin);
            return input.ReadBytes(Math.Min((int)count, (int)(input.BaseStream.Length - startOffset)));
        }

        private void wholeFileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            _startOffsetTextBox.Enabled = _byteCountTextBox.Enabled = !_wholeFileCheckBox.Checked;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Populate the combos
            _interpreterCombo.Items.AddRange(new object[]
            {
                new FourBitBigEndianInterpreter(),
                new FourBitLittleEndianInterpreter(),
                new EightBitUnsgnedInterpreter()
            });
            _interpreterCombo.SelectedIndex = 0;
            _sampleDepthCombo.Items.AddRange(new object[]
            {
                new EightBitWavWriter(),
                new SixteenBitWavWriter(),
                new FloatWavWriter()
            });
            _sampleDepthCombo.SelectedIndex = 0;
        }

        private void _interpreterCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            var interpreter = _interpreterCombo.SelectedItem as IDataInterpreter;
            if (interpreter == null)
            {
                return;
            }
            _outputCurveCombo.Items.Clear();
            switch (interpreter.OutputFormat)
            {
                case DataFormat.PSGVolume:
                    _outputCurveCombo.Items.AddRange(new object[]
                    {
                        new PSGVolumeToSampleLinear(),
                        new PSGVolumeToSampleLog()
                    });
                    break;
                case DataFormat.EightBitUnsigned:
                    _outputCurveCombo.Items.AddRange(new object[]
                    {
                        new EightBitSignedToSample(),
                        new EightBitSignedTopNibbleToLogSample()
                    });
                    break;
            }
            _outputCurveCombo.SelectedIndex = 0;
        }
    }
}
