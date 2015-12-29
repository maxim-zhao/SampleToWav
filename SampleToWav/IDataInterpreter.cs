using System.Collections.Generic;
using System.Linq;

namespace SampleToWav
{
    public enum DataFormat
    {
        /// <summary>
        /// Needs to be converted to PCM
        /// </summary>
        PSGVolume,

        /// <summary>
        /// Raw pass-through
        /// </summary>
        EightBitUnsigned,

        /// <summary>
        /// PDM
        /// </summary>
        OneBit
    }

    internal interface IDataInterpreter
    {
        IEnumerable<int> GetSamples(IEnumerable<byte> data);
        DataFormat OutputFormat { get; }
    }

    internal class FourBitBigEndianInterpreter : IDataInterpreter
    {
        public IEnumerable<int> GetSamples(IEnumerable<byte> data)
        {
            foreach (var b in data)
            {
                yield return b >> 4;
                yield return b & 0xf;
            }
        }

        public DataFormat OutputFormat
        {
            get { return DataFormat.PSGVolume; }
        }

        public override string ToString()
        {
            return "4-bit big-endian";
        }
    }

    internal class FourBitLittleEndianInterpreter : IDataInterpreter
    {
        public IEnumerable<int> GetSamples(IEnumerable<byte> data)
        {
            foreach (var b in data)
            {
                yield return b & 0xf;
                yield return b >> 4;
            }
        }

        public DataFormat OutputFormat
        {
            get { return DataFormat.PSGVolume; }
        }

        public override string ToString()
        {
            return "4-bit little-endian";
        }
    }

    internal class EightBitUnsignedInterpreter : IDataInterpreter
    {
        public IEnumerable<int> GetSamples(IEnumerable<byte> data)
        {
            return data.Select(b => (int) b);
        }

        public DataFormat OutputFormat
        {
            get { return DataFormat.EightBitUnsigned; }
        }

        public override string ToString()
        {
            return "8-bit unsigned";
        }
    }

    internal class OneBitBigEndianPdmInterpreter : IDataInterpreter
    {
        public IEnumerable<int> GetSamples(IEnumerable<byte> data)
        {
            foreach (var value in data)
            {
                var b = value;
                for (int i = 0; i < 8; ++i)
                {
                    // Get bit
                    var bit = b >> 7;
                    // Shift
                    b <<= 1;
                    // Emit it
                    yield return bit;
                }
            }
        }

        public DataFormat OutputFormat
        {
            get { return DataFormat.OneBit; }
        }

        public override string ToString()
        {
            return "1-bit PDM (big-endian)";
        }
    }

    internal class OneBitLittleEndianPdmInterpreter : IDataInterpreter
    {
        public IEnumerable<int> GetSamples(IEnumerable<byte> data)
        {
            foreach (var value in data)
            {
                var b = value;
                for (int i = 0; i < 8; ++i)
                {
                    // Get bit
                    var bit = b & 1;
                    // Shift
                    b >>= 1;
                    // Emit it
                    yield return bit;
                }
            }
        }

        public DataFormat OutputFormat
        {
            get { return DataFormat.OneBit; }
        }

        public override string ToString()
        {
            return "1-bit PDM (little-endian)";
        }
    }
}