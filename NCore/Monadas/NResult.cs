using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using NCore.Extensions;
using NCore.Monadas.Models;
using static NCore.NCore;

namespace NCore.Monadas
{
    [DebuggerDisplay("NResult={debugDisplay}")]
    public struct NResult<T> : IEquatable<NResult<T>>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string debugDisplay => IsError ? "Error" : $"Value={Value}";

        public T Value { get; }
        public IErrorValue ErrorValue { get; }

        public bool IsEmpty { get; }
        public bool HasValue => !IsEmpty;
        public bool IsError { get; }
        public bool IsSuccess => IsEmpty ? false : !IsError;

        public NResult(T value)
        {
            this.IsEmpty = false;
            this.IsError = false;
            this.Value = value;
            this.ErrorValue = default!;
        }

        public NResult(IErrorValue errorValue)
        {
            this.IsEmpty = false;
            this.IsError = true;
            this.Value = default!;
            this.ErrorValue = errorValue;
        }

        public NResult(T value, IErrorValue errorValue)
        {
            this.IsEmpty = false;
            if (value == null)
            {
                this.IsError = true;
                this.Value = default!;
                this.ErrorValue = errorValue;
            }
            else
            {
                this.IsError = false;
                this.Value = value;
                this.ErrorValue = default!;
            }
        }
        
        public static NResult<T> Empty = new NResult<T>();

        public static NResult<TResult> OfEmpty<TResult>() => new NResult<TResult>();

        public static NResult<TResult> OfError<TResult>(IErrorValue errorValue) => new NResult<TResult>(errorValue);

        public static NResult<TResult> OfSuccess<TResult>(TResult value) => new NResult<TResult>(value);

        public static NResult<TResult> OfResult<TResult>(TResult value, IErrorValue errorValue)
            => new NResult<TResult>(value, errorValue);

        public NResult<TResult> Map<TResult>(Func<T, TResult> mapper)
            => this.HasValue && this.IsSuccess
                ? OfResult<TResult>(mapper(this.Value), ErrorValue)
                : OfEmpty<TResult>();

        public NResult<TResult> Bind<TResult>(Func<T, NResult<TResult>> bind)
            => this.IsEmpty
                ? NResult<TResult>.Empty
                : bind(this.Value);

        public NResult<T> Filter(Predicate<T> predicate)
            => this.IsEmpty ? OfEmpty<T>() 
                : this.IsSuccess && predicate(this.Value) ? this : OfEmpty<T>();

        public NResult<T> Action(Action<T> action)
        {
            if (this.HasValue) action(Value);
            return this;
        }

        public NResult<T> ActionError(Action<IErrorValue> action)
        {
            if (this.HasValue) action(ErrorValue);
            return this;
        }

        public NResult<T> Or(Func<NResult<T>> supplier)
        {
            if (this.HasValue) return this;
            else
            {
                NResult<T> result = supplier();
                return result;
            }
        }

        public NResult<T> Or(NResult<T> other)
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

        public T? GetOrDefault() => this.HasValue && this.IsSuccess ? this.Value : default(T);

        public void IfSuccess(Action<T> action)
        {
            if (this.HasValue && this.IsSuccess) action(this.Value);
        }

        public void IfSuccess(Action action)
        {
            if (this.HasValue && this.IsSuccess) action();
        }

        public void IfSuccessOrElse(Action<T> firstAction, Action<T> secondAction)
        {
            if (this.HasValue && this.IsSuccess) firstAction(this.Value);
            else secondAction(this.Value);
        }

        public void IfSuccessOrElse(Action<T> firstAction, Action secondAction)
        {
            if (this.HasValue && this.IsSuccess) firstAction(this.Value);
            else secondAction();
        }

        public void IfSuccessOrElse(Action firstAction, Action secondAction)
        {
            if (this.HasValue && this.IsSuccess) firstAction();
            else secondAction();
        }

        public void IfError(Action<IErrorValue> action)
        {
            if (this.HasValue && this.IsError && ErrorValue != null)
                action(ErrorValue);
        }

        public void IfError(Action action)
        {
            if (this.HasValue && this.IsError)
                action();
        }

        public void IfErrorOrElse(Action<T> firstAction, Action<T> secondAction)
        {
            if (this.HasValue && this.IsError) firstAction(this.Value);
            else secondAction(this.Value);
        }

        public void IfErrorOrElse(Action<T> firstAction, Action secondAction)
        {
            if (this.HasValue && this.IsError) firstAction(this.Value);
            else secondAction();
        }

        public void IfErrorOrElse(Action firstAction, Action secondAction)
        {
            if (this.HasValue && this.IsError) firstAction();
            else secondAction();
        }

        public NOptional<TResult> Match<TResult>(Func<T, TResult> onSuccess, Func<IErrorValue, TResult> onError)
        {
            if (this.IsEmpty) return OfEmptyOptional<TResult>();
            return this.IsSuccess switch
            {
                true => OfNullable(this.Value).Map(x => onSuccess(x)),
                false => OfNullable(this.ErrorValue).Map(x => onError(x))
            };
        }

        public void March(Action<T> onSuccess, Action<IErrorValue> onError)
        {
            var self = this;
            if (this.IsEmpty) return;
            this.IsSuccess.IfTrueOrElse(
                trueAction: () => OfNullable(self.Value).IfPresent(() => onSuccess(self.Value)),
                falseAction: () => OfNullable(self.Value).IfNotPresent(() => onError(self.ErrorValue))
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

        public bool Equals(NResult<T> other)
            => this.EqualCustom(other,
                xx => xx.ErrorValue,
                xx => xx.HasValue,
                xx => xx.IsEmpty,
                xx => xx.IsError,
                xx => xx.IsSuccess,
                xx => xx.Value!);

        public override bool Equals([NotNullWhen(true)] object? obj)
            => obj is NResult<T> && Equals((NResult<T>)obj);

        public override int GetHashCode() 
            => HashCode.Combine(this.Value, this.ErrorValue, this.IsError);

        public static bool operator ==(in NResult<T> a, in NResult<T> b) => a.Equals(b);
        public static bool operator !=(in NResult<T> a, in NResult<T> b) => !a.Equals(b);
    }
}