using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Runtime.Serialization;
using System.Security.Permissions;
using NCore.Extensions;

namespace NCore.NTypes
{
    [Serializable]
    public struct Position : IEquatable<Position>, ISerializable
    {
        public int X { get; }
        public int Y { get; }

        public static readonly Position Empty = new Position();

        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Position(Point point)
        {
            this.X = point.X;
            this.Y = point.Y;
        }

        public static implicit operator Position(Point point)
            => new Position(point);
        public static implicit operator Point(Position position)
            => new Point(position.X, position.Y);

        public override bool Equals([NotNullWhen(true)] object? obj)
            => (obj is Position) && Equals((Position)obj);

        public bool Equals(Position other)
            => ReferenceEquals(this, other) 
                || Equals(this.X, other.X) && Equals(this.Y, other.Y);

        public override int GetHashCode()
            => this.MakeHashCode(
                xx => xx.X, 
                xx => xx.Y
            );

        public override string? ToString() => $"X={X}; Y={Y}";

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("X", X);
            info.AddValue("Y", Y);
        }
    }
}