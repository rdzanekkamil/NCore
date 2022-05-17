using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace NCore.Monadas
{
    [DebuggerDisplay(NONE)]
    public readonly struct NNone : IEquatable<NNone>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private const string NONE = "None";
        public static NNone Value => new NNone();

        public bool Equals(NNone other) => true;

        public T Match<T>(Func<NNone, T> selector) => selector(Value);

        public void Match(Action<NNone> operation) => operation(Value);

        public override bool Equals([NotNullWhen(true)] object? obj) => obj is NNone;

        public override int GetHashCode() => 00;
        
        public override string ToString() => NONE;
    }
}