using NCore.Extensions;
using NCore.Monadas;
using NCore.Monadas.Models;
using NCore.Validator;
using NCore.Validator.Fluent;

namespace NCore
{
    public static partial class NCore
    {
        public static IThrowSelector<T> OfValid<T>(T value) => Validatable<T>.Valid(value);
        public static NTry<T> OfTryApplay<T>(Func<T> tryFunction)
            => NTry<T>.Apply<T>(tryFunction);

        public static NTry<T> OfTryEmpty<T>() => new NTry<T>();

        public static NSwitcher<T> OfSwitcher<T>() => new NSwitcher<T>();
        public static NSwitcher<T> OfSwitcher<T>(Func<T> defautlFunction) => new NSwitcher<T>(defautlFunction);
        public static NSwitcher<T> OfSwitcher<T>(params (object option, T result)[] items) 
        {
            var switchData = new NSwitcher<T>();
            items.ForEach(item => switchData.Add(item.option, item.result));
            return switchData;
        }

        public static NOptional<T> OfNullable<T>(T value) => NOptional<T>.OfNullable(value);
        public static NOptional<T> OfOptional<T>(T value) => NOptional<T>.Of(value);
        public static NOptional<T> OfEmptyOptional<T>() => NOptional<T>.Empty;

        public static NEither<TLeft, TRight> OfEitherLeft<TLeft, TRight>(TLeft leftValue)
            => NEither<TLeft, TRight>.OfLeft<TLeft, TRight>(leftValue);

        public static NEither<TLeft, TRight> OfEitherRight<TLeft, TRight>(TRight rightValue)
            => NEither<TLeft, TRight>.OfRight<TLeft, TRight>(rightValue);

        public static NEither<TLeft, TRight> OfEmptyEither<TLeft, TRight>(TRight rightValue)
            => NEither<TLeft, TRight>.OfEmpty<TLeft, TRight>();

        public static NNone OfNone() => NNone.Value;

        public static NResult<T> OfResultError<T>(ErrorValue errorValue)
            => NResult<T>.OfError<T>(errorValue);

        public static NResult<T> OfEmptyResult<T>(ErrorValue errorValue) => NResult<T>.OfEmpty<T>();

        public static NResult<T> OfResultSuccess<T>(T value) => NResult<T>.OfSuccess<T>(value);

        public static NResult<T> OfResultSuccess<T>(T value, ErrorValue errorValue) 
            => NResult<T>.OfResult<T>(value, errorValue);

        public static NBuilder<T> OfBuilder<T>() => new NBuilder<T>();

        public static NBuilder<T> OfBuilder<T>(T item) => new NBuilder<T>(item);

        public static NBuilder<T> OfBuilder<T>(Func<T> factory) => new NBuilder<T>(factory);
    }
}