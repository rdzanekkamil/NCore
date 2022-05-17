using System.Diagnostics;
using NCore.Extensions;

namespace NCore.Monadas
{
    [DebuggerDisplay("NOptional={debugDisplay}")]
    public struct NOptional<T> : IComparable<NOptional<T>>, IComparable<T>, IEquatable<NOptional<T>>, IEquatable<T>, IDisposable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string debugDisplay => IsEmpty ? "null" : $"Value={Value}";
        
        public T Value { get; } = default(T)!;
        public static NOptional<T> Empty { get; } = new NOptional<T>();
        public bool IsEmpty { get; }
        public bool HasValue => !IsEmpty;

        public NOptional() => this.IsEmpty = true;

        public NOptional(T value)
        {
            this.IsEmpty = false;
            this.Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        public Type GetInsideType() => typeof(T);

        public static NOptional<T> Of(T value) => new NOptional<T>(value);

        public static NOptional<T> OfNullable(T value) =>
            value == null ? NOptional<T>.Empty : NOptional<T>.Of(value);

        public NOptional<T> Filter(Predicate<T> predicate)
            => this.IsEmpty ? this : predicate(this.Value) ? this : NOptional<T>.Empty;

        public NOptional<TResult> Map<TResult>(Func<T, TResult> mapper)
            => this.IsEmpty
                ? NOptional<TResult>.Empty
                : NOptional<TResult>.OfNullable(mapper(this.Value));

        public NOptional<TResult> Bind<TResult>(Func<T, NOptional<TResult>> bind)
            => this.IsEmpty
                ? NOptional<TResult>.Empty
                : bind(this.Value);

        public NOptional<T> Action(Action<T> action)
        {
            if (this.HasValue) action(Value);
            return this;
        }

        public IEnumerable<T> ToList() => this.IsEmpty ? Enumerable.Empty<T>() : new List<T>() { this.Value };

        public IEnumerable<T> ToEnumerable()
        {
            if (HasValue)
            {
                if (Value is IEnumerable<T>)
                {
                    foreach (var item in ((IEnumerable<T>)Value))
                    {
                        yield return item;
                    }
                }
                yield return Value;
            }
            yield break;
        }

        public NOptional<T> Or(Func<NOptional<T>> supplier)
        {
            if (this.HasValue) return this;
            else
            {
                NOptional<T> result = supplier();
                return result;
            }
        }

        public NOptional<T> Or(NOptional<T> other)
        {
            if (this.HasValue) return this;
            return other;
        }

        public T OrElse(T other) => this.HasValue ? this.Value : other;

        public T OrElseGet(Func<T> supplier) => this.HasValue ? this.Value : supplier();

        public T OrElseThrow<Ex>(Func<Ex> exceptionSupplier) where Ex : Exception
        {
            if (this.HasValue) return this.Value;
            throw exceptionSupplier();
        }

        public T Get() => this.Value;

        public T? GetOrDefault() => this.HasValue ? this.Value : default(T);

        public void IfPresent(Action<T> action)
        {
            if (this.HasValue) action(this.Value);
        }

        public void IfPresent(Action action)
        {
            if (this.HasValue) action();
        }

        public void IfPresentOrElse(Action<T> firstAction, Action<T> secondAction)
        {
            if (this.HasValue) firstAction(this.Value);
            else secondAction(this.Value);
        }

        public void IfPresentOrElse(Action<T> firstAction, Action secondAction)
        {
            if (this.HasValue) firstAction(this.Value);
            else secondAction();
        }

        public void IfPresentOrElse(Action firstAction, Action secondAction)
        {
            if (this.HasValue) firstAction();
            else secondAction();
        }

        public void IfNotPresent(Action<T> action)
        {
            if (this.IsEmpty) action(this.Value);
        }

        public void IfNotPresent(Action action)
        {
            if (this.IsEmpty) action();
        }

        public int CompareTo(NOptional<T> other) => IsEmpty && other.IsEmpty ? 0
            : Comparer<T>.Default.Compare(Value, other.Value);

        public int CompareTo(T? other) => IsEmpty ? -1
            : Comparer<T>.Default.Compare(Value, other);

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj == null || this.GetType() != obj.GetType()) return false;
            NOptional<T> Other = (NOptional<T>)obj;
            return Equals(this.Value, Other.Value);
        }

        public bool Equals(T? other) =>
            HasValue && other != null ? Equals(Value, other) : false;

        public bool Equals(NOptional<T> other) 
            => this.EqualCustom(other,
                xx => xx.HasValue,
                xx => xx.IsEmpty,
                xx => xx.Value!);

        public override string ToString() =>
            this.Value != null ? String.Format("Optional[{0}]", this.Value) : "Optional.empty";

        public override int GetHashCode() => HashCode.Combine(this, this.Value);

        public void Dispose()
        {
            if (HasValue && Value is IDisposable)
            {
                ((IDisposable)Value).Dispose();
                GC.SuppressFinalize(Value);
            }
            GC.SuppressFinalize(this);
        }

        public static bool operator ==(NOptional<T> left, NOptional<T> right)
        {
            if (left == Empty && right == Empty) return true;
            if (left != Empty) return left.EqualCustom(right,
                xx => xx.HasValue,
                xx => xx.IsEmpty,
                xx => xx.Value!);
            return false;
        }

        public static bool operator !=(NOptional<T> left, NOptional<T> right) => !(left == right);

        public static bool operator ==(NOptional<T> left, T right)
            => left == Empty ? false : left.EqualCustomParam(right!, x => x.Value!);

        public static bool operator !=(NOptional<T> left, T right) 
            => left == Empty ? false : !left.EqualCustomParam(right!, x => x.Value!);

        public static bool operator ==(T left, NOptional<T> right)
            => right == Empty ? false : right.EqualCustomParam(right!, x => x.Value!);

        public static bool operator !=(T left, NOptional<T> right)
            => right == Empty ? false : !right.EqualCustomParam(right!, x => x.Value!);
    }
}