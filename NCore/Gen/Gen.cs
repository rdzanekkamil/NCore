using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;

namespace NCore.Gen
{
    public class Gen : RandomNumberGenerator, IGen
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private readonly RandomNumberGenerator random = RandomNumberGenerator.Create();
        public char NextChar() => (char) (Sample() * char.MaxValue);
        public char NextChar(char maxValue)
        {
            return(char) (Sample() * maxValue);
        }

        public char NextChar(char minValue, char maxValue)
        {
            return(char) (Sample() * (maxValue - minValue) + minValue);
        }

        public sbyte NextSByte() => (sbyte) (Sample() * sbyte.MaxValue);
        public sbyte NextSByte(int maxValue)
        {
            return(sbyte) (Sample() * maxValue);
        }
        public sbyte NextSByte(int minValue, int maxValue)
        {
            return(sbyte) (Sample() * (maxValue - minValue) + minValue);
        }
        public byte NextByte() => (byte) (Sample() * byte.MaxValue);
        public byte NextByte(int maxValue)
        {
            return(byte) (Sample() * maxValue);
        }
        public byte NextByte(int minValue, int maxValue)
        {
            return(byte) (Sample() * (maxValue - minValue) + minValue);
        }
        public short NextShort() => (short) (Sample() * short.MaxValue);
        public short NextShort(int maxValue)
        {
            return(short) (Sample() * maxValue);
        }
        public short NextShort(int minValue, int maxValue)
        {
            return(short) (Sample() * (maxValue - minValue) + minValue);
        }
        public ushort NextUShort() => (ushort) (Sample() * ushort.MaxValue);
        public ushort NextUShort(int maxValue)
        {
            return(ushort) (Sample() * maxValue);
        }
        public ushort NextUShort(int minValue, int maxValue)
        {
            return(ushort) (Sample() * (maxValue - minValue) + minValue);
        }
        public int NextInt() => (int) (Sample() * int.MaxValue);
        public int NextInt(int maxValue)
        {
            return(int) (Sample() * maxValue);
        }
        public int NextInt(int minValue, int maxValue)
        {
            return(int) (Sample() * (maxValue - minValue) + minValue);
        }
        public uint NextUInt() => (uint) (Sample() * uint.MaxValue);
        public uint NextUInt(uint maxValue)
        {
            return(uint) (Sample() * maxValue);
        }
        public uint NextUInt(uint minValue, uint maxValue)
        {
            return(uint) (Sample() * (maxValue - minValue) + minValue);
        }
        public long NextLong() => (long) (Sample() * long.MaxValue);
        public long NextLong(long maxValue)
        {
            return(long) (Sample() * maxValue);
        }
        public long NextLong(long minValue, long maxValue)
        {
            return(long) (Sample() * (maxValue - minValue) + minValue);
        }
        public ulong NextULong() => (ulong) (Sample() * ulong.MaxValue);
        public ulong NextULong(ulong maxValue)
        {
            return(ulong) (Sample() * maxValue);
        }
        public ulong NextULong(ulong minValue, ulong maxValue)
        {
            return(ulong) (Sample() * (maxValue - minValue) + minValue);
        }
        
        public BigInteger NextBigInteger(int BitLength = 128) => BigSample(BitLength);
        public BigInteger NextBigUnsignedInteger(int BitLength = 128) => BigUSample(BitLength);
        public BigInteger NextBigInteger(int MinBitLength, int MaxBitLength) => BigSample(MinBitLength, MaxBitLength);
        public BigInteger NextBigUnsignedInteger(int MinBitLength, int MaxBitLength) => BigUSample(MinBitLength, MaxBitLength);

        public float NextFloat(float minValue, float maxValue)
        {
            while(true)
            {
                var value = (float) (Sample() * (maxValue - minValue) + minValue);
                if(!float.IsNaN(value) && !float.IsInfinity(value))
                    return value;
            }
        }
        public float NextFloat() => (float) Sample();
        public double NextDouble(double minValue, double maxValue)
        {
            while(true)
            {
                var value = Sample() * (maxValue - minValue) + minValue;
                if(!double.IsNaN(value) && !double.IsInfinity(value))
                    return value;
            }
        }
        public double NextDouble() => Sample();
        private double Sample()
        {
             byte[] bytes = new byte[sizeof(long)];
            random.GetBytes(bytes);
            long value = BitConverter.ToInt64(bytes, 0) & long.MaxValue;
            // Scale it to 0->1
            return (double)value / (((double)Int64.MaxValue) + 1025.0d);
        }
        private BigInteger BigSample(int BitLength)
        {
            var buf = new byte[BitLength >> 3];
            GetBytes(buf);
            var val = new BigInteger(buf);
            return val;
        }
        private BigInteger BigUSample(int BitLength)
        {
            BigInteger n;
            var        buf = new byte[BitLength >> 3];
            do
            {
                GetBytes(buf);
                n = new BigInteger(buf);
            } while(n.Sign == -1);
            return n;
        }
        private BigInteger BigSample(int MinBitLength, int MaxBitLength)
        {
            int BitLength;
            if(MinBitLength == MaxBitLength)
                BitLength = MaxBitLength;
            else
                BitLength = NextUShort(1, ((MaxBitLength - MinBitLength) >> 3) + 1) << 3;
            var byteLength = BitLength >> 3;
            if(byteLength <= 0)
                byteLength = 1;
            var buf = new byte[byteLength];
            GetBytes(buf);
            var val = new BigInteger(buf);
            return val;
        }

        private BigInteger BigUSample(int MinBitLength, int MaxBitLength)
        {
            int BitLength;
            if(MinBitLength == MaxBitLength)
                BitLength = MaxBitLength;
            else
                BitLength = NextUShort(1, ((MaxBitLength - MinBitLength) >> 3) + 1) << 3;
            var byteLength = BitLength >> 3;
            if(byteLength <= 0)
                byteLength = 1;
            var        buf = new byte[byteLength];
            BigInteger n;
            do
            {
                GetBytes(buf);
                n = new BigInteger(buf);
            } while(n.Sign == -1);
            return n;
        }
        public override void GetBytes(byte[] data) => random.GetBytes(data);
        public byte GetNextByte()
        {
            var xbc = new byte[1];
            random.GetBytes(xbc);
            return xbc[0];
        }
        public char GetNextChar()
        {
            var xbc = new byte[1];
            while(true)
            {
                random.GetBytes(xbc);
                var c = xbc[0];
                if(c >= 32 || c <= 127)
                    return(char) c;
            }
        }
        public byte[] GetNextByteArray(int size)
        {
            var ba = new byte[size];
            random.GetBytes(ba);
            return ba;
        }
        public char[] GetNextCharArray(int size)
        {
            var xbc = new byte[1];
            var ca  = new char[size];
            var ptr = 0;
            do
            {
                random.GetBytes(xbc);
                var c = xbc[0];
                if(c >= 32 || c <= 127)
                    ca[ptr++] = (char) c;
            } while(ptr < size);
            return ca;
        }
    }
}