using System.Numerics;

namespace NCore.Gen
{
    public interface IGen
    {
        char NextChar();
        char NextChar(char maxValue);
        char NextChar(char minValue, char maxValue);
        sbyte NextSByte();
        sbyte NextSByte(int maxValue);
        sbyte NextSByte(int minValue, int maxValue);
        byte NextByte();
        byte NextByte(int maxValue);
        byte NextByte(int minValue, int maxValue);
        short NextShort();
        short NextShort(int maxValue);
        short NextShort(int minValue, int maxValue);
        ushort NextUShort();
        ushort NextUShort(int maxValue);
        ushort NextUShort(int minValue, int maxValue);
        int NextInt();
        int NextInt(int maxValue);
        int NextInt(int minValue, int maxValue);
        uint NextUInt();
        uint NextUInt(uint maxValue);
        uint NextUInt(uint minValue, uint maxValue);
        long NextLong();
        long NextLong(long maxValue);
        long NextLong(long minValue, long maxValue);
        ulong NextULong();
        ulong NextULong(ulong maxValue);
        ulong NextULong(ulong minValue, ulong maxValue);
        BigInteger NextBigInteger(int BitLength = 128);
        BigInteger NextBigUnsignedInteger(int BitLength = 128);
        BigInteger NextBigInteger(int MinBitLength, int MaxBitLength);
        BigInteger NextBigUnsignedInteger(int MinBitLength, int MaxBitLength);
        float NextFloat(float minValue, float maxValue);
        float NextFloat();
        double NextDouble(double minValue, double maxValue);
        double NextDouble();
        void GetBytes(byte[] data);
        byte GetNextByte();
        char GetNextChar();
        byte[] GetNextByteArray(int size);
        char[] GetNextCharArray(int size);
    }
}