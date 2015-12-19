using System.Collections.Generic;
using System.Linq;

namespace SampleToWav
{
    public enum DataFormat
    {
        PSGVolume,
        EightBitUnsigned
    }

    interface IDataInterpreter
    {
        IEnumerable<int> GetSamples(IEnumerable<byte> data);
        DataFormat OutputFormat { get; }
    }

    class FourBitBigEndianInterpreter : IDataInterpreter
    {
        public IEnumerable<int> GetSamples(IEnumerable<byte> data)
        {
            foreach (var b in data)
            {
                yield return b >> 4;
                yield return b & 0xf;
            }
        }

        public DataFormat OutputFormat { get { return DataFormat.PSGVolume; } }

        public override string ToString()
        {
            return "4-bit big-endian";
        }
    }

    class FourBitLittleEndianInterpreter : IDataInterpreter
    {
        public IEnumerable<int> GetSamples(IEnumerable<byte> data)
        {
            foreach (var b in data)
            {
                yield return b & 0xf;
                yield return b >> 4;
            }
        }

        public DataFormat OutputFormat { get { return DataFormat.PSGVolume; } }

        public override string ToString()
        {
            return "4-bit little-endian";
        }
    }

    class EightBitUnsgnedInterpreter : IDataInterpreter
    {
        public IEnumerable<int> GetSamples(IEnumerable<byte> data)
        {
            return data.Select(b => (int) b);
        }

        public DataFormat OutputFormat { get { return DataFormat.EightBitUnsigned; } }

        public override string ToString()
        {
            return "8-bit unsigned";
        }
    }
}
