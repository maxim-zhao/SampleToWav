using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleToWav
{
    interface IValueToSample
    {
        IEnumerable<ushort> ValuesToSamples(IEnumerable<int> values);
    }

    /// <summary>
    /// Converts PSG volumes to log samples
    /// </summary>
    class PSGVolumeToSampleLog : IValueToSample
    {
        internal static readonly ushort[] Lookup =
        {
            0,
            (ushort)(65535*Math.Pow(10.0, -0.1*14)),
            (ushort)(65535*Math.Pow(10.0, -0.1*13)),
            (ushort)(65535*Math.Pow(10.0, -0.1*12)),
            (ushort)(65535*Math.Pow(10.0, -0.1*11)),
            (ushort)(65535*Math.Pow(10.0, -0.1*10)),
            (ushort)(65535*Math.Pow(10.0, -0.1*9)),
            (ushort)(65535*Math.Pow(10.0, -0.1*8)),
            (ushort)(65535*Math.Pow(10.0, -0.1*7)),
            (ushort)(65535*Math.Pow(10.0, -0.1*6)),
            (ushort)(65535*Math.Pow(10.0, -0.1*5)),
            (ushort)(65535*Math.Pow(10.0, -0.1*4)),
            (ushort)(65535*Math.Pow(10.0, -0.1*3)),
            (ushort)(65535*Math.Pow(10.0, -0.1*2)),
            (ushort)(65535*Math.Pow(10.0, -0.1*1)),
            65535
        };

        public IEnumerable<ushort> ValuesToSamples(IEnumerable<int> values)
        {
            return values.Select(value => Lookup[value]);
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
        private static readonly ushort[] Lookup =
        {
            65535*0/15,
            65535*1/15,
            65535*2/15,
            65535*3/15,
            65535*4/15,
            65535*5/15,
            65535*6/15,
            65535*7/15,
            65535*8/15,
            65535*9/15,
            65535*11/15,
            65535*12/15,
            65535*13/15,
            65535*14/15,
            65535*14/15,
            65535*15/15
        };

        public IEnumerable<ushort> ValuesToSamples(IEnumerable<int> values)
        {
            return values.Select(value => Lookup[value]);
        }

        public override string ToString()
        {
            return "Linear";
        }
    }

    /// <summary>
    /// Converts 8-bit unsigned to samples
    /// </summary>
    class EightBitSignedToSample : IValueToSample
    {
        public IEnumerable<ushort> ValuesToSamples(IEnumerable<int> values)
        {
            // Extend from 8 to 16 bits
            return values.Select(value => (ushort) (value << 8 | value));
        }

        public override string ToString()
        {
            return "Convert to 16-bit";
        }
    }

    /// <summary>
    /// Converts 8-bit unsigned to logarithmic PSG volume as if ony the top 4 bits were used
    /// </summary>
    class EightBitSignedTopNibbleToLogSample : IValueToSample
    {
        public IEnumerable<ushort> ValuesToSamples(IEnumerable<int> values)
        {
            return values.Select(value => PSGVolumeToSampleLog.Lookup[value >> 4]);
        }

        public override string ToString()
        {
            return "Discard low bits, logarithmic";
        }
    }
}
