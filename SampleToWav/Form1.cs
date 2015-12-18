using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WavDotNet.Core;

namespace SampleToWav
{
    public partial class Form1 : Form
    {
        private string _file;

        private static readonly Dictionary<int, ushort> LinearSamples = new Dictionary<int, ushort>
        {
            {0, 65535*0/15},
            {1, 65535*1/15},
            {2, 65535*2/15},
            {3, 65535*3/15},
            {4, 65535*4/15},
            {5, 65535*5/15},
            {6, 65535*6/15},
            {7, 65535*7/15},
            {8, 65535*8/15},
            {9, 65535*9/15},
            {10, 65535*11/15},
            {11, 65535*12/15},
            {12, 65535*13/15},
            {13, 65535*14/15},
            {14, 65535*14/15},
            {15, 65535},
        };

        private static readonly Dictionary<int, ushort> LogSamples = new Dictionary<int, ushort>
        {
            {0, 0},
            {1, (ushort)(65535*Math.Pow(10.0, -0.1*14))},
            {2, (ushort)(65535*Math.Pow(10.0, -0.1*13))},
            {3, (ushort)(65535*Math.Pow(10.0, -0.1*12))},
            {4, (ushort)(65535*Math.Pow(10.0, -0.1*11))},
            {5, (ushort)(65535*Math.Pow(10.0, -0.1*10))},
            {6, (ushort)(65535*Math.Pow(10.0, -0.1*9))},
            {7, (ushort)(65535*Math.Pow(10.0, -0.1*8))},
            {8, (ushort)(65535*Math.Pow(10.0, -0.1*7))},
            {9, (ushort)(65535*Math.Pow(10.0, -0.1*6))},
            {10, (ushort)(65535*Math.Pow(10.0, -0.1*5))},
            {11, (ushort)(65535*Math.Pow(10.0, -0.1*4))},
            {12, (ushort)(65535*Math.Pow(10.0, -0.1*3))},
            {13, (ushort)(65535*Math.Pow(10.0, -0.1*2))},
            {14, (ushort)(65535*Math.Pow(10.0, -0.1*1))},
            {15, 65535},
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog {CheckFileExists = true, Filter = "All files|*.*"})
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    _file = dialog.FileName;
                    textBox1.Text = _file;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            uint startOffset = GetInt(textBox3.Text);
            uint count = GetInt(textBox4.Text);
            uint samplingRate = GetInt(textBox2.Text);
            bool linear = comboBox1.SelectedIndex == 0;
            bool bigEndian = comboBox2.SelectedIndex == 1;

            using (var dialog = new SaveFileDialog {Filter = "Wave files (*.wav)|*.wav"})
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    SaveWav(_file, startOffset, count, dialog.FileName, samplingRate, linear, bigEndian);
                }
            }
        }

        private uint GetInt(string text)
        {
            if (text.StartsWith("0x"))
            {
                return Convert.ToUInt32(text, 16);
            }
            else
            {
                return Convert.ToUInt32(text);
            }
        }

        private void SaveWav(string file, uint startOffset, uint count, string fileName, uint samplingRate, bool linear, bool bigEndian)
        {
            var samples = new List<ushort>();
            using (var input = new BinaryReader(new FileStream(_file, FileMode.Open)))
            {
                if (count > input.BaseStream.Length - startOffset)
                {
                    count = (uint) (input.BaseStream.Length - startOffset);
                }
                input.BaseStream.Seek(startOffset, SeekOrigin.Begin);
                for (uint i = 0; i < count; ++i)
                {
                    // Read a byte
                    byte b = input.ReadByte();
                    // Split it
                    int v1, v2;
                    if (bigEndian)
                    {
                        v1 = b >> 4;
                        v2 = b & 0xf;
                    }
                    else
                    {
                        v1 = b & 0xf;
                        v2 = b >> 4;
                    }
                    // Scale it
                    ushort s1, s2;
                    if (linear)
                    {
                        s1 = LinearSamples[v1];
                        s2 = LinearSamples[v2];
                    }
                    else
                    {
                        s1 = LogSamples[v1];
                        s2 = LogSamples[v2];
                    }
                    // Emit them
                    samples.Add(s1);
                    samples.Add(s2);
                }
            }
            using (var output = new WavWrite<ushort>(fileName, samplingRate, WavFormat.Pcm, 16, 16))
            {
                output.AudioData.Add(new Channel<ushort>(new Samples<ushort>(samples), ChannelPositions.Mono));
                output.Flush();
            }
        }
    }
}
