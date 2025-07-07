using System.Collections.Generic;
using System.Linq;

namespace SampleToWav
{
    public enum DataFormat
    {
        /// <summary>
        /// Needs to be converted to PCM
        /// </summary>
        PSGAttenuation,

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

        public DataFormat OutputFormat => DataFormat.PSGAttenuation;

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

        public DataFormat OutputFormat => DataFormat.PSGAttenuation;

        public override string ToString()
        {
            return "4-bit little-endian";
        }
    }

    internal class EightBitUnsignedInterpreter : IDataInterpreter
    {
        public IEnumerable<int> GetSamples(IEnumerable<byte> data)
        {
            return data.Select(b => (int)b);
        }

        public DataFormat OutputFormat => DataFormat.EightBitUnsigned;

        public override string ToString()
        {
            return "8-bit unsigned";
        }
    }

    internal class EightBitSignedInterpreter : IDataInterpreter
    {
        public IEnumerable<int> GetSamples(IEnumerable<byte> data)
        {
            return data.Select(b => (sbyte)b + 128);
        }

        public DataFormat OutputFormat => DataFormat.EightBitUnsigned;

        public override string ToString()
        {
            return "8-bit signed";
        }
    }

    internal class OneBitBigEndianPdmInterpreter : IDataInterpreter
    {
        public IEnumerable<int> GetSamples(IEnumerable<byte> data)
        {
            foreach (var value in data)
            {
                var b = value;
                for (var i = 0; i < 8; ++i)
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

        public DataFormat OutputFormat => DataFormat.OneBit;

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
                for (var i = 0; i < 8; ++i)
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

        public DataFormat OutputFormat => DataFormat.OneBit;

        public override string ToString()
        {
            return "1-bit PDM (little-endian)";
        }
    }

    public class ChakanLZLittleEndian4BitInterpreter: IDataInterpreter
    {
        public DataFormat OutputFormat => DataFormat.PSGAttenuation;

        public IEnumerable<int> GetSamples(IEnumerable<byte> data)
        {
            // We decompress into memory because we need random access...
            var result = new List<byte>();
            var numBitsLeft = 0;
            byte flags = 0;
            // We pull out an enumerator in order to pull bytes as we want them
            using (var source = data.GetEnumerator())
            {
                for (;;)
                {
                    // if we ran out of bits, get another byte
                    if (numBitsLeft == 0)
                    {
                        if (!source.MoveNext())
                        {
                            // Out of bytes in source (shouldn't happen)
                            break;
                        }

                        flags = source.Current;
                        numBitsLeft = 8;
                    }

                    // Get a bit out of the byte
                    var flag = flags & 1;
                    flags >>= 1;
                    --numBitsLeft;
                    if (flag == 1)
                    {
                        // 1 = raw byte
                        if (!source.MoveNext()) break;
                        result.Add(source.Current);
                    }
                    else
                    {
                        // 0 = LZ
                        // Grab next two bytes
                        if (!source.MoveNext()) break;
                        int offset = source.Current;
                        if (!source.MoveNext()) break;
                        int count = source.Current;
                        offset |= (count >> 4) << 8;
                        if (offset == 0)
                        {
                            // Normal end of data
                            break;
                        }

                        var sourceOffset = result.Count - offset;
                        if (sourceOffset < 0)
                        {
                            // error, we just end
                            break;
                        }

                        count &= 0xf;
                        count += 3;
                        // Copy the run
                        for (var i = 0; i < count; ++i)
                        {
                            result.Add(result[sourceOffset++]);
                        }
                    }
                }
            }

            // Then we wrap it in a little-endian extractor
            var wrapper = new FourBitLittleEndianInterpreter();
            return wrapper.GetSamples(result);
        }

        public override string ToString()
        {
            return "Chakan LZ 4-bit little-endian";
        }
    }

    public class FourBitSignedInterpreter : IDataInterpreter
    {
        private readonly int[] _lookup =
        [
            136,
            153,
            170,
            187,
            204,
            221,
            238,
            255,
            0,
            17,
            34,
            51,
            68,
            85,
            102,
            119
        ];

        public IEnumerable<int> GetSamples(IEnumerable<byte> data)
        {
            foreach (var b in data)
            {
                yield return _lookup[b >> 4];
                yield return _lookup[b & 0xf];
            }
        }

        public DataFormat OutputFormat => DataFormat.EightBitUnsigned;

        public override string ToString()
        {
            return "4-bit big-endian signed";
        }
    }

    public class RLEFourBitBigEndianInterpreter : IDataInterpreter
    {
        public IEnumerable<int> GetSamples(IEnumerable<byte> data)
        {
            // We decompress to another list, and then unpack that
            var uncompressed = new List<byte>();

            var count = 0;
            var isRle = false;
            var isControlByte = true;
            foreach (var b in data)
            {
                if (isControlByte)
                {
                    if (b == 0)
                    {
                        break;
                    }

                    if (b > 0x80)
                    {
                        count = b & 0x7f;
                        isRle = true;
                    }
                    else
                    {
                        count = b;
                        isRle = false;
                    }

                    isControlByte = false;
                }
                else
                {
                    // Emit data
                    if (isRle)
                    {
                        uncompressed.AddRange(Enumerable.Repeat(b, count));
                        isControlByte = true;
                    }
                    else
                    {
                        --count;
                        if (count == 0)
                        {
                            isControlByte = true;
                        }
                        uncompressed.Add(b);
                    }
                }
            }

            return new FourBitBigEndianInterpreter().GetSamples(uncompressed);
        }

        public DataFormat OutputFormat => DataFormat.PSGAttenuation;

        public override string ToString()
        {
            return "4-bit RLE";
        }
    }

}