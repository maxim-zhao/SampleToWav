using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Linq;

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
            openFileDialog1.Filter = "All files|*.*";
            if (openFileDialog1.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }
            _files = new List<string>(openFileDialog1.FileNames);
            if (_files.Count == 1)
            {
                textBox1.Text = _files[0];
            }
            else
            {
                textBox1.Text = string.Format("{0} files selected", _files.Count);
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
                count = int.MaxValue;
            }

            if (_files.Count == 1)
            {
                if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
                {
                    SaveWav(_files[0], startOffset, count, saveFileDialog1.FileName, samplingRate, interpreter, sampleGenerator, wavWriter);
                }
            }
            else
            {
                foreach (var file in _files)
                {
                    SaveWav(file, startOffset, count, file + ".wav", samplingRate, interpreter, sampleGenerator, wavWriter);
                }
            }
        }

        private uint GetInt(string text)
        {
            var startIndex = 0;
            if (text.StartsWith("0x"))
            {
                startIndex = 2;
            }
            else if (text.StartsWith("$"))
            {
                startIndex = 1;
            }
            return startIndex > 0 ? Convert.ToUInt32(text.Substring(startIndex), 16) : Convert.ToUInt32(text);
        }

        private static void SaveWav(string inputFilename, uint startOffset, uint count, string outputFilename, uint samplingRate, IDataInterpreter interpreter, IValueToSample sampleRenderer, IWavWriter writer)
        {
            try
            {
                using var input = new BinaryReader(new FileStream(inputFilename, FileMode.Open));
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
            catch (Exception e)
            {
                MessageBox.Show($"Exception decoding {inputFilename} at {startOffset:x}\n" + e.StackTrace, e.Message);
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
            _interpreterCombo.Items.AddRange([
                new FourBitBigEndianInterpreter(),
                new FourBitLittleEndianInterpreter(),
                new EightBitUnsignedInterpreter(),
                new EightBitSignedInterpreter(),
                new OneBitBigEndianPdmInterpreter(),
                new OneBitLittleEndianPdmInterpreter(),
                new ChakanLZLittleEndian4BitInterpreter(),
                new FourBitSignedInterpreter(), 
                new RLEFourBitBigEndianInterpreter(),
                new LZFourBitBigEndianInterpreter(),
                new LZFourBitBigEndianInterpreterNoLength()
            ]);
            _interpreterCombo.SelectedIndex = 0;
            _sampleDepthCombo.Items.AddRange([
                new EightBitWavWriter(),
                new SixteenBitWavWriter(),
                new FloatWavWriter()
            ]);
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
                case DataFormat.PSGAttenuation:
                    _outputCurveCombo.Items.AddRange([
                        new PSGAttenuationToSampleLinear(),
                        new PSGAttenuationToSampleLog(),
                        new InvertedPSGAttenuationToSampleLog()
                    ]);
                    break;
                case DataFormat.EightBitUnsigned:
                    _outputCurveCombo.Items.AddRange([
                        new EightBitUnsignedToSample(),
                        new EightBitUnsignedTopNibbleToLogSample()
                    ]);
                    break;
                case DataFormat.OneBit:
                    _outputCurveCombo.Items.AddRange([
                        new OneBitToSample()
                    ]);
                    break;
            }
            _outputCurveCombo.SelectedIndex = 0;
        }

        private class PreviewSampleProvider : ISampleProvider
        {
            private readonly IEnumerator<float> _samples;

            public PreviewSampleProvider(int samplingRate, IEnumerable<float> samples)
            {
                WaveFormat = new WaveFormat(samplingRate, 32, 1);
                _samples = samples.GetEnumerator();
            }

            public WaveFormat WaveFormat { get; }

            public int Read(float[] buffer, int offset, int count)
            {
                for (var i = 0; i < count; ++i)
                {
                    if (!_samples.MoveNext())
                    {
                        return i;
                    }
                    buffer[offset + i] = _samples.Current;
                }
                return count;
            }
        }

        private void _previewButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_files.Count != 1)
            {
                return;
            }

            var startOffset = GetInt(_startOffsetTextBox.Text);
            var count = GetInt(_byteCountTextBox.Text);
            var samplingRate = GetInt(_samplingRateTextBox.Text);
            var interpreter = _interpreterCombo.SelectedItem as IDataInterpreter;
            var sampleGenerator = _outputCurveCombo.SelectedItem as IValueToSample;
            var inputFilename = _files[0];

            if (_wholeFileCheckBox.Checked)
            {
                startOffset = 0;
                count = int.MaxValue;
            }

            using var output = new WaveOut();
            using var input = new BinaryReader(new FileStream(inputFilename, FileMode.Open));
            output.Init(
                new PreviewSampleProvider(
                    (int)samplingRate, 
                    sampleGenerator.ValuesToSamples(
                        interpreter.GetSamples(
                            GetBytes(
                                input,
                                startOffset,
                                count)))));
            output.Play();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.wav|*.wav";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            foreach (var filename in openFileDialog1.FileNames)
            {
                // Open file
                var input = new WaveFileReader(filename);
                var f = new WaveFormatConversionStream(
                    new WaveFormat(input.WaveFormat.SampleRate, 8, 1), input);
                var outputFilename = filename + ".4bitraw";
                using var output = new FileStream(outputFilename, FileMode.Create);
                var high = true;
                byte value = 0;
                var buffer = new byte[f.Length];
                f.Read(buffer, 0, (int)f.Length);
                foreach (var sample in buffer)
                {
                    /*
                        // Convert to 4-bit (dumb)
                        if (high)
                        {
                            value = sample;
                            value &= 0xf0;
                            high = false;
                        }
                        else
                        {
                            value |= (byte)(sample >> 4);
                            output.WriteByte(value);
                            high = true;
                        }
                        */
                    // Convert to 4-bit (log)
                    if (high)
                    {
                        value = getValueForSample(sample);
                        value <<= 4;
                        high = false;
                    }
                    else
                    {
                        value |= getValueForSample(sample);
                        output.WriteByte(value);
                        high = true;
                    }
                }
                output.Close();
            }
        }

        private byte getValueForSample(byte sample)
        {
            // Find the nearest value in the log lookup
            var value = sample / 255.0;
            var l = PSGAttenuationToSampleLog.Lookup;
            for (var i = 0; i < 16; ++i)
            {
                if (l[i] <= value)
                {
                    // We've found the item that matches, or is smaller, than what we want
                    if (i == 15)
                    {
                        return (byte)i;
                    }
                    var diffI = Math.Abs(l[i] - value);
                    var diffIPlus1 = Math.Abs(l[i+1] - value);
                    if (diffI < diffIPlus1)
                    {
                        return (byte)i;
                    }
                    return (byte)(i + 1);
                }
            }
            return 15;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.wav|*.wav";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            foreach (var filename in openFileDialog1.FileNames)
            {
                // Open file
                var samples = File.ReadAllBytes(filename);
                var outputFilename = filename + ".pdmraw";
                using var output = new FileStream(outputFilename, FileMode.Create);
                var error = 0;
                var b = 0;
                var bitCounter = 0;
                foreach (var sample in samples.Skip(0x2c))
                {
                    // Add the error to the sample to get the new vale
                    var value = error + sample;
                    // If it's over 128, emit 1, else emit a 0
                    var bit = value > 127 ? 1 : 0;
                    // Emit it
                    b = (b << 1) + bit;
                    if (++bitCounter == 8)
                    {
                        output.WriteByte((byte)b);
                        b = 0;
                        bitCounter = 0;
                        // We drop trailing bits
                    }
                    // Update the running error
                    // We wanted value, we emitted (equivalently) 0 or 255
                    error = value - bit * 255;
                }
                output.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "*.wav|*.wav";
            if (openFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            foreach (var filename in openFileDialog1.FileNames)
            {
                // Open file
                var samples = File.ReadAllBytes(filename);
                var outputFilename = filename + ".1bitraw";
                using var output = new FileStream(outputFilename, FileMode.Create);
                var b = 0;
                var bitCounter = 0;
                foreach (var sample in samples.Skip(0x2c))
                {
                    // We simply clamp everything
                    // TODO: silence is not handled well
                    var bit = sample > 127 ? 1 : 0;
                    // Emit it
                    b = (b << 1) + bit;
                    if (++bitCounter == 8)
                    {
                        output.WriteByte((byte)b);
                        b = 0;
                        bitCounter = 0;
                        // We drop trailing bits
                    }
                }
                output.Close();
            }
        }

        private void batchButton_Click(object sender, EventArgs e)
        {
            // Paste from the clipboard
            // Expect offset, count, (optional) sampling rate
            var text = Clipboard.GetText();
            var tokensList = text.Split('\r', '\n')
                .Select(s => s.Trim())
                .Where(s => s.Length > 0)
                .Select(s => s.Split('\t'))
                .Where(t => t.Length >= 2);
            if (!tokensList.Any())
            {
                return;
            }

            saveFileDialog1.Filter = "*.wav|*.wav";
            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            var defaultSamplingRate = GetInt(_samplingRateTextBox.Text);
            var interpreter = _interpreterCombo.SelectedItem as IDataInterpreter;
            var sampleGenerator = _outputCurveCombo.SelectedItem as IValueToSample;
            var wavWriter = _sampleDepthCombo.SelectedItem as IWavWriter;

            foreach (var tokens in tokensList)
            {
                var startOffset = GetInt(tokens[0]);
                var count = GetInt(tokens[1]);
                var samplingRate = tokens.Length > 2 ? GetInt(tokens[2]) : defaultSamplingRate;
                var fileName = Path.ChangeExtension(saveFileDialog1.FileName, tokens[0] + ".wav");
                SaveWav(_files[0], startOffset, count, fileName, samplingRate, interpreter, sampleGenerator, wavWriter);
            }
        }
    }
}
