using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleToWav
{
    interface IValueToSample
    {
        IEnumerable<double> ValuesToSamples(IEnumerable<int> values);
    }

    /// <summary>
    /// Converts PSG volumes to log samples
    /// </summary>
    class PSGVolumeToSampleLog : IValueToSample
    {
        internal static readonly double[] Lookup =
        {
            0,
            Math.Pow(10.0, -0.1*14),
            Math.Pow(10.0, -0.1*13),
            Math.Pow(10.0, -0.1*12),
            Math.Pow(10.0, -0.1*11),
            Math.Pow(10.0, -0.1*10),
            Math.Pow(10.0, -0.1*9),
            Math.Pow(10.0, -0.1*8),
            Math.Pow(10.0, -0.1*7),
            Math.Pow(10.0, -0.1*6),
            Math.Pow(10.0, -0.1*5),
            Math.Pow(10.0, -0.1*4),
            Math.Pow(10.0, -0.1*3),
            Math.Pow(10.0, -0.1*2),
            Math.Pow(10.0, -0.1*1),
            1.0
        };

        public IEnumerable<double> ValuesToSamples(IEnumerable<int> values)
        {
            return values.Select(value => Lookup[value & 0xf]);
        }

        public override string ToString()
        {
            return "Logarithmic (real PSG)";
        }
    }

    /// <summary>
    /// Converts PSG volumes to linear samples
    /// </summary>
    class PSGVolumeToSampleLinear : IValueToSample
    {
        private static readonly double[] Lookup =
        {
            0.0/15.0,
            1.0/15.0,
            2.0/15.0,
            3.0/15.0,
            4.0/15.0,
            5.0/15.0,
            6.0/15.0,
            7.0/15.0,
            8.0/15.0,
            9.0/15.0,
            10.0/15.0,
            11.0/15.0,
            12.0/15.0,
            13.0/15.0,
            14.0/15.0,
            15.0/15.0
        };

        public IEnumerable<double> ValuesToSamples(IEnumerable<int> values)
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
    class EightBitUnsignedToSample : IValueToSample
    {
        public IEnumerable<double> ValuesToSamples(IEnumerable<int> values)
        {
            return values.Select(value => (double) (value & 0xff)/255.0);
        }

        public override string ToString()
        {
            return "Linear";
        }
    }

    /// <summary>
    /// Converts 8-bit unsigned to logarithmic PSG volume as if ony the top 4 bits were used
    /// </summary>
    class EightBitUnsignedTopNibbleToLogSample : IValueToSample
    {
        public IEnumerable<double> ValuesToSamples(IEnumerable<int> values)
        {
            return values.Select(value => PSGVolumeToSampleLog.Lookup[(value >> 4) & 0xf]);
        }

        public override string ToString()
        {
            return "Discard low nibble, logarithmic";
        }
    }

    class OneBitToSample : IValueToSample
    {
        public IEnumerable<double> ValuesToSamples(IEnumerable<int> values)
        {
            return values.Select(i => (double)i);
        }

        public override string ToString()
        {
            return "PDM pass-through";
        }
    }
}
