using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using NCore.Extensions;
using static NCore.NCore;

namespace NCore.Monadas
{
    [DebuggerDisplay("NEither={debugDisplay}")]
    public struct NEither<TL, TR> : IEquatable<NEither<TL, TR>>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string debugDisplay => IsEmpty ? "Empty" 
            : IsLeft ? $"LeftValue={LeftValue}" : $"LeftValue={RightValue}";

        public bool IsEmpty { get; }
        public bool HasValue => !IsEmpty;
        public bool IsLeft { get; }
        public bool IsRight => IsEmpty ? false : !IsLeft;
        
        public TL LeftValue { get; }
        public TR RightValue { get; }

        public static NEither<TL, TR> Empty = new NEither<TL, TR>();

        public NEither(TL leftValue)
        {
            this.IsEmpty = false;
            this.IsLeft = true;
            this.LeftValue = leftValue;
            this.RightValue = default(TR)!;
        }

        public NEither(TR rightValue)
        {
            this.IsEmpty = false;
            this.IsLeft = true;
            this.LeftValue = default(TL)!;
            this.RightValue = rightValue;
        }

        public NEither()
        {
            this.IsEmpty = true;
            this.IsLeft = false;
            this.LeftValue = default(TL)!;
            this.RightValue = default(TR)!;
        }

        public static NEither<L, R> OfLeft<L, R>(L leftValue) => new NEither<L, R>(leftValue);
        public static NEither<L, R> OfRight<L, R>(R rightValue) => new NEither<L, R>(rightValue);
        public static NEither<L, R> OfEmpty<L, R>() => new NEither<L, R>();

        public NOptional<TResult> Match<TResult>(Func<TL, TResult> left, Func<TR, TResult> right)
            => this.IsEmpty ? NOptional<TResult>.Empty : this.IsLeft
                ? OfNullable(left(this.LeftValue)) : OfNullable(right(this.RightValue));

        public Type GetLeftInsideType() => typeof(TL);
        public Type GetRightInsideType() => typeof(TR);

        public NOptional<TResult> MatchRight<TResult>(Func<TR, TResult> right) 
            => this.HasValue && this.IsRight 
                ? OfNullable(right(this.RightValue)) 
                : NOptional<TResult>.Empty;

        public NOptional<TResult> MatchLeft<TResult>(Func<TL, TResult> left)
            => this.HasValue && this.IsLeft 
                ? OfNullable(left(this.LeftValue)) 
                : NOptional<TResult>.Empty;

        public NEither<NL, NR> Map<NL, NR>(Func<TL, NL> leftMapper, Func<TR, NR> rightMapper)
            => this.IsEmpty ? OfEmpty<NL, NR>() : this.IsLeft 
                ? OfLeft<NL, NR>(leftMapper(this.LeftValue)) : OfRight<NL, NR>(rightMapper(this.RightValue));

        public NEither<TL, NR> MapRight<NR>(Func<TR, NR> rightMapper) 
            => this.HasValue && this.IsRight 
                ? OfRight<TL, NR>(rightMapper(this.RightValue)) 
                : NEither<TL, NR>.Empty; 

        public NEither<NL, TR> MapLeft<NL>(Func<TL, NL> leftMapper) 
            => this.HasValue && this.IsLeft
                ? OfLeft<NL, TR>(leftMapper(this.LeftValue)) 
                : NEither<NL, TR>.Empty;  

        public NEither<NL, NR> Bind<NL, NR>(Func<TL, NEither<NL, NR>> leftMapper, Func<TR, NEither<NL, NR>> rightMapper)
            => this.IsEmpty ? OfEmpty<NL, NR>() : this.IsLeft 
                ? leftMapper(this.LeftValue) : rightMapper(this.RightValue);

        public NEither<NL, TR> BindLeft<NL>(Func<TL, NEither<NL, TR>> leftMapper) 
            => this.HasValue && this.IsLeft
                ? leftMapper(this.LeftValue) 
                : NEither<NL, TR>.Empty; 
                
        public NEither<TL, NR> BindRight<NR>(Func<TR, NEither<TL, NR>> rightMapper) 
            => this.HasValue && this.IsRight
                ? rightMapper(this.RightValue) 
                : NEither<TL, NR>.Empty;

        public NEither<TL, TR> Action(Action<TL, TR> action)
        {
            if (this.HasValue) action(this.LeftValue, this.RightValue);
            return this;
        }

        public NEither<TL, TR> Actions(Action<TL> leftAction, Action<TR> rightAction)
        {
            if (this.HasValue)
            {
                if (this.IsLeft) leftAction(this.LeftValue);
                else rightAction(this.RightValue);
            }
            return this;
        }

        public NEither<TL, TR> ActionLeft(Action<TL> leftAction)
        {
            if (this.HasValue && this.IsLeft) leftAction(this.LeftValue);
            return this;
        }

        public NEither<TL, TR> ActionRight(Action<TR> rightAction)
        {
            if (this.HasValue && this.IsRight) rightAction(this.RightValue);
            return this;
        }

        public NEither<TL, TR> Or(Func<NEither<TL, TR>> supplier)
            => this.HasValue ? this : supplier();

        public NEither<TL, TR> Or(NEither<TL, TR> other)
            => this.HasValue ? this : other;

        public TL OrElseLeft(TL other) => this.HasValue && this.IsLeft ? this.LeftValue : other;
        public TL OrElseGetLeft(Func<TL> supplier) => this.HasValue && this.IsLeft ? this.LeftValue : supplier();
        public TL OrElseThrowLeft<Ex>(Func<Ex> exceptionSupplier) where Ex : Exception
        {
            if (this.HasValue && this.IsLeft) return this.LeftValue;
            throw exceptionSupplier.Invoke();
        }
        public TR OrElseRight(TR other) => this.HasValue && this.IsRight ? this.RightValue : other;
        public TR OrElseGetRight(Func<TR> supplier) => this.HasValue && this.IsRight ? this.RightValue : supplier();
        public TR OrElseThrowRight<Ex>(Func<Ex> exceptionSupplier) where Ex : Exception
        {
            if (this.HasValue && this.IsRight) return this.RightValue;
            throw exceptionSupplier.Invoke();
        }

        public TL? GetLeftOrDefault() => this.HasValue && this.IsLeft ? this.LeftValue : default(TL);
        public NOptional<TL> GetLeftSafe() => this.HasValue && this.IsLeft ? OfNullable(this.LeftValue) : OfEmptyOptional<TL>();
        public TR? GetRightOrDefault() => this.HasValue && this.IsRight ? this.RightValue : default(TR);
        public NOptional<TR> GetRightSafe() => this.HasValue && this.IsRight ? OfNullable(this.RightValue) : OfEmptyOptional<TR>();

        public void IfLeftPresent(Action<TL> action)
        {
            if (this.HasValue && this.IsLeft) action(this.LeftValue);
        }
        public void IfLeftPresent(Action action)
        {
            if (this.HasValue && this.IsLeft) action();
        }

        public void IfRightPresent(Action<TR> action)
        {
            if (this.HasValue && this.IsRight) action(this.RightValue);
        }
        public void IfRightPresent(Action action)
        {
            if (this.HasValue && this.IsRight) action();
        }

        public void IfLeftPresentOrElse(Action<TL> firstAction, Action<TL> secondAction)
        {
            if (this.HasValue && this.IsLeft) firstAction(this.LeftValue);
            else secondAction(this.LeftValue);
        }
        public void IfLeftPresentOrElse(Action<TL> firstAction, Action secondAction)
        {
            if (this.HasValue && this.IsLeft) firstAction(this.LeftValue);
            else secondAction();
        }

        public void IfLeftPresentOrElse(Action firstAction, Action secondAction)
        {
            if (this.HasValue && this.IsLeft) firstAction();
            else secondAction();
        }

        public void IfRightPresentOrElse(Action<TR> firstAction, Action<TR> secondAction)
        {
            if (this.HasValue && this.IsRight) firstAction(this.RightValue);
            else secondAction(this.RightValue);
        }
        public void IfRightPresentOrElse(Action<TR> firstAction, Action secondAction)
        {
            if (this.HasValue && this.IsRight) firstAction(this.RightValue);
            else secondAction();
        }

        public void IfRightPresentOrElse(Action firstAction, Action secondAction)
        {
            if (this.HasValue && this.IsRight) firstAction();
            else secondAction();
        }

        public bool Equals(NEither<TL, TR> other)
            => this.EqualCustom(other,
            x => x.HasValue,
            x => x.IsEmpty,
            x => x.IsLeft,
            x => x.IsRight,
            x => x.LeftValue!,
            x => x.RightValue!);

        public override bool Equals([NotNullWhen(true)] object? obj)
            => this.EqualCustom(obj!,
            x => x.HasValue,
            x => x.IsEmpty,
            x => x.IsLeft,
            x => x.IsRight,
            x => x.LeftValue!,
            x => x.RightValue!);

        public override int GetHashCode() 
            => HashCode.Combine(this.IsEmpty, this.IsLeft, this.LeftValue, this.RightValue);

        public override string? ToString() => IsEmpty ? "Empty" 
            : IsLeft ? LeftValue!.ToString() : RightValue!.ToString();

        public static bool operator ==(in NEither<TL, TR> a, in NEither<TL, TR> b) => a.Equals(b);
        public static bool operator !=(in NEither<TL, TR> a, in NEither<TL, TR> b) => !a.Equals(b); 
    }
}