using System;
using System.Collections.Generic;
using System.Linq;
using WavDotNet.Core;

namespace SampleToWav
{
    // Wav.Net does conversion in a way that screws up :( so I had to roll my own - sort of, I don't just convert because its easier this way.
    interface IWavWriter
    {
        void Encode(string fileName, uint samplingRate, IEnumerable<double> samples);
    }

    class EightBitWavWriter : IWavWriter
    {
        public void Encode(string fileName, uint samplingRate, IEnumerable<double> samples)
        {
            using (var output = new WavWrite<byte>(fileName, samplingRate, WavFormat.Pcm, 8, 8))
            {
                output.AudioData.Add(new Channel<byte>(new Samples<byte>(samples.Select(x => (byte)Math.Round(x * 255))), ChannelPositions.Mono));
                output.Flush();
            }
        }

        public override string ToString()
        {
            return "8-bit unsigned";
        }
    }

    class SixteenBitWavWriter : IWavWriter
    {
        public void Encode(string fileName, uint samplingRate, IEnumerable<double> samples)
        {
            using (var output = new WavWrite<short>(fileName, samplingRate, WavFormat.Pcm, 16, 16))
            {
                output.AudioData.Add(new Channel<short>(new Samples<short>(samples.Select(x => (short)Math.Round(x * 65535 - 32768))), ChannelPositions.Mono));
                output.Flush();
            }
        }

        public override string ToString()
        {
            return "16-bit signed";
        }
    }

    class FloatWavWriter : IWavWriter
    {
        public void Encode(string fileName, uint samplingRate, IEnumerable<double> samples)
        {
            using (var output = new WavWrite<float>(fileName, samplingRate, WavFormat.FloatingPoint, 64, 64))
            {
                output.AudioData.Add(new Channel<float>(new Samples<float>(samples.Select(x => (float)(x * 2.0 - 1.0))), ChannelPositions.Mono));
                output.Flush();
            }
        }

        public override string ToString()
        {
            return "32-bit floating point";
        }
    }

}