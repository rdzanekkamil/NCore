using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using NCore.Extensions;
using NCore.Monadas.Exceptions;
using static NCore.NCore;

namespace NCore.Monadas
{
    [DebuggerDisplay("NResult={debugDisplay}")]
    public struct NTry<T> : IDisposable, IEquatable<NTry<T>>, IEquatable<T>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string debugDisplay => IsFailure ? "Error" : $"Value={Value}";

        public bool IsEmpty { get; }
        public bool HasValue => !IsEmpty;
        public bool IsSuccess { get; }
        public bool IsFailure => IsEmpty ? false : !IsSuccess;
        public T Value { get; }
        public Exception? Exception { get; }

        public static NTry<T> Empty = new NTry<T>();

        private NTry(T value, Exception? exception, bool isSuccess)
        {
            this.Value = value;
            this.Exception = exception;
            this.IsSuccess = isSuccess;
            this.IsEmpty = false;
        }

        public NTry()
        {
            this.IsEmpty = true;
            this.IsSuccess = false;
            this.Value = default(T)!;
            this.Exception = null;
        }

        public static NTry<TResult> OfEmpty<TResult>() => new NTry<TResult>();

        public static NTry<TResult> Apply<TResult>(Func<TResult> tryFunction)
        {
            _ = tryFunction ?? throw new ArgumentNullException(nameof(tryFunction));
            try
            {
                return FromSuccess<TResult>(tryFunction());
            }
            catch (Exception exc)
            {
                return FromFailure<TResult>(exc);
            }
        }

        public NOptional<TResult> Match<TResult>(Func<T, TResult> onSuccess, Func<Exception, TResult> onError)
        {
            if (this.IsEmpty) return OfEmptyOptional<TResult>();
            return this.IsSuccess switch
            {
                true => OfNullable(this.Value).Map(x => onSuccess(x)),
                false => OfNullable(this.Exception).Map(x => onError(x))
            };
        }

        public void March(Action<T> onSuccess, Action<Exception> onError)
        {
            var self = this;
            if (this.IsEmpty) return;
            this.IsSuccess.IfTrueOrElse(
                trueAction: () => OfNullable(self.Value).IfPresent(() => onSuccess(self.Value)),
                falseAction: () => OfNullable(self.Value).IfNotPresent(() => onError(self.Exception))
            );
        }

        public void March(Action onSuccess, Action onError)
        {
            if (this.IsEmpty) return;
            this.IsSuccess.IfTrueOrElse(
                trueAction: onSuccess,
                falseAction: onError
            );
        }

        public NTry<TResult> Map<TResult>(Func<T, TResult> mapper) => Bind(value => NTry<TResult>.Apply(() => mapper(value)));

        public NTry<TResult> Bind<TResult>(Func<T, NTry<TResult>> bind) 
            => IsFailure ? NTry<TResult>.FromFailure<TResult>(Exception!) : bind(Value);

        public NTry<T> Filter(Predicate<T> predicate)
            => this.IsEmpty ? this : predicate(this.Value) ? this : NTry<T>.Empty;

        public NTry<T> Action(Action<T> action)
        {
            if (this.HasValue) action(this.Value);
            return this;
        }

        public NTry<T> Or(Func<NTry<T>> supplier)
        {
            if (this.HasValue) return this;
            else
            {
                NTry<T> result = supplier();
                return result;
            }
        }

        public NTry<T> Or(NTry<T> other)
        {
            if (this.HasValue) return this;
            return other;
        }

        public T Recover(Func<Exception, T> recover) => this.IsFailure ? recover(Exception!) : this.Value;

        public NTry<T> RecoverWith(Func<Exception, NTry<T>> recovery) 
        {
            if (this.IsSuccess) return this;
            try
            {
                return recovery(this.Exception!);
            }
            catch (Exception exc)
            {
                return FromFailure<T>(exc);
            }
        }

        public T OrElse(T other) => this.IsSuccess ? this.Value : other;

        public T OrElseGet(Func<T> other) => this.IsSuccess ? this.Value : other();

        public T OrElseThrow<Ex>(Func<Ex> exceptionSupplier) where Ex : Exception
        {
            if (this.HasValue) return this.Value;
            throw exceptionSupplier();
        }

        public T OrElseThrow()
        {
            if (this.HasValue) return this.Value;
            throw Exception ?? throw new TryOfException();
        }

        public T Get() => this.Value;

        public T? GetOrDefault() => this.HasValue ? this.Value : default(T);

        public void IfSuccess(Action action)
        {
            if (this.HasValue && this.IsSuccess) action();
        }

        public void IfSuccess(Action<T> action)
        {
            if (this.HasValue && this.IsSuccess) action(this.Value);
        }

        public void IfSuccessOrElse(Action<T> firstAction, Action<T> secondAction)
        {
            if (this.HasValue && this.IsSuccess) firstAction(this.Value);
            secondAction(this.Value);
        }

        public void IfSuccessOrElse(Action<T> firstAction, Action secondAction)
        {
            if (this.HasValue && this.IsSuccess) firstAction(this.Value);
            secondAction();
        }

        public void IfSuccessOrElse(Action firstAction, Action secondAction)
        {
            if (this.HasValue && this.IsSuccess) firstAction();
            secondAction();
        }

        public void IfFailure(Action action)
        {
            if (this.HasValue && this.IsFailure) action();
        }

        public void IfFailure(Action<Exception> action)
        {
            if (this.HasValue && this.IsFailure) action(this.Exception!);
        }

        public void IfFailureThrow()
        {
            if (this.HasValue && this.IsFailure) 
                throw Exception ?? throw new TryOfException();
        }

        public NTry<T> OrElseTry(Func<T> tryFunction) 
            => this.HasValue && this.IsSuccess ? this : Apply(tryFunction);

        private static NTry<TResult> FromFailure<TResult>(Exception exc) => new NTry<TResult>(default!, exc, false);

        private static NTry<TResult> FromSuccess<TResult>(TResult? result) => new NTry<TResult>(result!, null, true);

        public void Dispose()
        {
            if (this.IsSuccess && this.Value != null && this.Value is IDisposable)
            {
                ((IDisposable)Value).Dispose();
                GC.SuppressFinalize(Value);
            }
            GC.SuppressFinalize(this);
        }

        public bool Equals(NTry<T> other)
            => this.EqualCustom(other,
                xx => xx.Exception!,
                xx => xx.IsFailure,
                xx => xx.IsSuccess,
                xx => xx.Value!);
            

        public bool Equals(T? other)
            => this.EqualCustom(other!,
            xx => xx.Exception!,
            xx => xx.HasValue,
            xx => xx.IsEmpty,
            xx => xx.IsFailure,
            xx => xx.IsSuccess,
            xx => xx.Value!);

        public override bool Equals([NotNullWhen(true)] object? obj)
            => obj is NTry<T> valid && this.Equals(valid);

        public override int GetHashCode() => HashCode.Combine(IsSuccess, Exception, Value);

        public static bool operator !=(NTry<T> left, NTry<T> right) => !left.Equals(right);
        public static bool operator ==(NTry<T> left, NTry<T> right) => left.Equals(right);
    }
}