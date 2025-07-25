using System;
using System.Collections.Generic;
using System.Linq;
using WavDotNet.Core;

namespace SampleToWav
{
    // Wav.Net does conversion in a way that screws up :( so I had to roll my own that correctly handles values like 1.0.
    internal interface IWavWriter
    {
        void Encode(string fileName, uint samplingRate, IEnumerable<float> samples);
    }

    internal class EightBitWavWriter : IWavWriter
    {
        public void Encode(string fileName, uint samplingRate, IEnumerable<float> samples)
        {
            using var output = new WavWrite<byte>(fileName, samplingRate, WavFormat.Pcm, 8, 8);
            output.AudioData.Add(new Channel<byte>(new Samples<byte>(samples.Select(x => (byte)Math.Round(x * 255))), ChannelPositions.Mono));
            output.Flush();
        }

        public override string ToString()
        {
            return "8-bit unsigned";
        }
    }

    internal class SixteenBitWavWriter : IWavWriter
    {
        public void Encode(string fileName, uint samplingRate, IEnumerable<float> samples)
        {
            using var output = new WavWrite<short>(fileName, samplingRate, WavFormat.Pcm, 16, 16);
            output.AudioData.Add(new Channel<short>(new Samples<short>(samples.Select(x => (short)Math.Round(x * 65535 - 32768))), ChannelPositions.Mono));
            output.Flush();
        }

        public override string ToString()
        {
            return "16-bit signed";
        }
    }

    internal class FloatWavWriter : IWavWriter
    {
        public void Encode(string fileName, uint samplingRate, IEnumerable<float> samples)
        {
            using var output = new WavWrite<float>(fileName, samplingRate, WavFormat.FloatingPoint, 64, 64);
            // Convert from [0..1] to [-1..+1]
            output.AudioData.Add(new Channel<float>(new Samples<float>(samples.Select(x => x * 2 - 1)), ChannelPositions.Mono));
            output.Flush();
        }

        public override string ToString()
        {
            return "32-bit floating point";
        }
    }

}