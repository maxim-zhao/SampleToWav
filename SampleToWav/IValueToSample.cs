using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleToWav
{
    internal interface IValueToSample
    {
        IEnumerable<float> ValuesToSamples(IEnumerable<int> values);
    }

    /// <summary>
    /// Converts PSG attenuations to log samples
    /// </summary>
    internal class PSGAttenuationToSampleLog : IValueToSample
    {
        internal static readonly float[] Lookup =
        [
            1,
            (float)Math.Pow(10.0, -0.1*1),
            (float)Math.Pow(10.0, -0.1*2),
            (float)Math.Pow(10.0, -0.1*2),
            (float)Math.Pow(10.0, -0.1*3),
            (float)Math.Pow(10.0, -0.1*4),
            (float)Math.Pow(10.0, -0.1*5),
            (float)Math.Pow(10.0, -0.1*6),
            (float)Math.Pow(10.0, -0.1*7),
            (float)Math.Pow(10.0, -0.1*8),
            (float)Math.Pow(10.0, -0.1*9),
            (float)Math.Pow(10.0, -0.1*10),
            (float)Math.Pow(10.0, -0.1*11),
            (float)Math.Pow(10.0, -0.1*12),
            (float)Math.Pow(10.0, -0.1*13),
            0
        ];

        public IEnumerable<float> ValuesToSamples(IEnumerable<int> values)
        {
            return values.Select(value => Lookup[value & 0xf]);
        }

        public override string ToString()
        {
            return "Logarithmic (real PSG)";
        }
    }

    internal class InvertedPSGAttenuationToSampleLog: IValueToSample
    {
        public IEnumerable<float> ValuesToSamples(IEnumerable<int> values)
        {
            return values.Select(value => PSGAttenuationToSampleLog.Lookup[(value & 0xf) ^ 0xf]);
        }

        public override string ToString()
        {
            return "Logarithmic (real PSG) with inversion";
        }
    }

    /// <summary>
    /// Converts PSG attenuation values to linear samples
    /// </summary>
    internal class PSGAttenuationToSampleLinear : IValueToSample
    {
        private static readonly float[] Lookup =
        [
            15.0f/15.0f,
            14.0f/15.0f,
            13.0f/15.0f,
            12.0f/15.0f,
            11.0f/15.0f,
            10.0f/15.0f,
            9.0f/15.0f,
            8.0f/15.0f,
            7.0f/15.0f,
            6.0f/15.0f,
            5.0f/15.0f,
            4.0f/15.0f,
            3.0f/15.0f,
            2.0f/15.0f,
            1.0f/15.0f,
            0.0f/15.0f
        ];

        public IEnumerable<float> ValuesToSamples(IEnumerable<int> values)
        {
            return values.Select(value => Lookup[value & 0xf]);
        }

        public override string ToString()
        {
            return "Linear";
        }
    }

    /// <summary>
    /// Converts 8-bit unsigned to samples
    /// </summary>
    internal class EightBitUnsignedToSample : IValueToSample
    {
        public IEnumerable<float> ValuesToSamples(IEnumerable<int> values)
        {
            return values.Select(value => (value & 0xff)/255.0f);
        }

        public override string ToString()
        {
            return "Linear";
        }
    }

    /// <summary>
    /// Converts 8-bit unsigned to logarithmic PSG volume as if ony the top 4 bits were used
    /// </summary>
    internal class EightBitUnsignedTopNibbleToLogSample : IValueToSample
    {
        public IEnumerable<float> ValuesToSamples(IEnumerable<int> values)
        {
            return values.Select(value => PSGAttenuationToSampleLog.Lookup[(value >> 4) & 0xf]);
        }

        public override string ToString()
        {
            return "Discard low nibble, logarithmic";
        }
    }

    internal class OneBitToSample : IValueToSample
    {
        public IEnumerable<float> ValuesToSamples(IEnumerable<int> values)
        {
            return values.Select(i => (float)i);
        }

        public override string ToString()
        {
            return "PDM pass-through";
        }
    }

}
