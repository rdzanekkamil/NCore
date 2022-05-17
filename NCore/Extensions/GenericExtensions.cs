using NCore.Monadas;
using NCore.Utils;
using static NCore.NCore;

namespace NCore.Extensions
{
    public static partial class GenericExtensions
    {
        public static T IfSingle<T>(this T o, Func<bool> predicate, Func<T, T> transformFunc)
            => o.IfSingle(_ => predicate(), transformFunc);

        public static T IfSingle<T>(this T o, bool predicate, Func<T, T> transformFunc)
            => o.IfSingle(() => predicate, transformFunc);

        public static T IfSingle<T>(this T o, Func<T, bool> predicate, Func<T, T> transformFunc)
        {
            return predicate(o) ? transformFunc(o) : o;
        }

        public static void OfIfSingle<T>(this T obj, Predicate<T> predicate, Action action)
        {
            if (predicate(obj)) action();
        }

        public static void OfIfSingle<T>(this T obj, Predicate<T> predicate, Action<T> action)
        {
            if (predicate(obj)) action(obj);
        }

        public static T IfElse<T>(this T o, Func<bool> predicate, Func<T, T> ifFunc, Func<T, T> elseFunc)
            => o.IfElse(_ => predicate(), ifFunc, elseFunc);

        public static T IfElse<T>(this T o, bool condition, Func<T, T> ifFunc, Func<T, T> elseFunc)
            => o.IfElse(_ => condition, ifFunc, elseFunc);

        public static T IfElse<T>(this T o, Func<T, bool> predicate, Func<T, T> ifFunc, Func<T, T> elseFunc)
            => o.Map(predicate(o) ? ifFunc : elseFunc);

        public static T IfElse<T>(this T o, Func<bool> predicate, T ifResult, T elseResult)
            => o.IfElseT(_ => predicate(), ifResult, elseResult);

        public static T IfElse<T>(this T o, bool condition, T ifResult, T elseResult)
            => o.IfElse(_ => condition, ifResult, elseResult);

        public static T IfElse<T>(this T o, Func<T, bool> predicate, T ifResult, T elseResult)
            => IfElseT(o, predicate, ifResult, elseResult);

        public static void IfElse<T>(this T o, Func<T, bool> predicate, Action<T> ifFunc, Action<T> elseFunc)
        {
            if (predicate(o)) ifFunc(o);
            else elseFunc(o); 
        }

        public static TResult IfElseT<T, TResult>(this T o, Func<bool> predicate, Func<T, TResult> ifFunc, Func<T, TResult> elseFunc)
            => o.IfElseT(_ => predicate(), ifFunc, elseFunc);

        public static TResult IfElseT<T, TResult>(this T o, bool condition, Func<T, TResult> ifFunc, Func<T, TResult> elseFunc)
            => o.IfElseT(_ => condition, ifFunc, elseFunc);

        public static TResult IfElseT<T, TResult>(this T o, Func<T, bool> predicate, Func<T, TResult> ifFunc, Func<T, TResult> elseFunc)
            => o.Map(predicate(o) ? ifFunc : elseFunc);

        public static TResult IfElseT<T, TResult>(this T o, Func<bool> predicate, TResult ifResult, TResult elseResult)
            => o.IfElseT(_ => predicate(), ifResult, elseResult);

        public static TResult IfElseT<T, TResult>(this T o, bool condition, TResult ifResult, TResult elseResult)
            => o.IfElseT(_ => condition, ifResult, elseResult);

        public static TResult IfElseT<T, TResult>(this T o, Func<T, bool> predicate, TResult ifResult, TResult elseResult)
            => predicate(o) ? ifResult : elseResult;

        public static TResult IfElseT<TIn, TResult>(this TIn obj,
                                                        Predicate<TIn> predicate,
                                                        Func<TResult> ifTrue,
                                                        Func<TResult> ifFalse)
            => predicate(obj) ? ifTrue() : ifFalse();

        public static TResult IfElseT<TIn, TResult>(this TIn obj,
                                                        Predicate<TIn> predicate,
                                                        Func<TResult> ifTrue,
                                                        Func<TIn, TResult> ifFalse)
            => predicate(obj) ? ifTrue() : ifFalse(obj);

        public static TResult IfElseT<TIn, TResult>(this TIn obj,
                                                        Predicate<TIn> predicate,
                                                        Func<TIn, TResult> ifTrue,
                                                        Func<TResult> ifFalse)
            => predicate(obj) ? ifTrue(obj) : ifFalse();

        public static T Actions<T>(this T o, params Action<T>[] actions)
        {
            actions.ForEach(action => action(o));
            return o;
        }

        public static TReturn Map<T, TReturn>(this T o, Func<T, TReturn> func) => func(o);

        public static T Action<T>(this T o, Action<T> action)
        {
            action(o);
            return o;
        }

        public static NOptional<T> ToOptional<T>(this T obj) => NOptional<T>.OfNullable(obj);

        public static NEither<TLeft, TRight> ToLeftEither<TLeft, TRight>(TLeft leftValue) => new NEither<TLeft, TRight>(leftValue);

        public static NEither<TLeft, TRight> ToRightEither<TLeft, TRight>(TRight rightValue) => new NEither<TLeft, TRight>(rightValue);

        public static bool In<T>(this T o, params T[] values) => Array.IndexOf(values, o) != -1;

        public static bool NotIn<T>(this T o, params T[] values) => Array.IndexOf(values, o) == -1;

        public static bool Between<T>(this T o, T minValue, T maxValue) where T: IComparable<T> 
            => minValue.CompareTo(o) == -1 && o.CompareTo(maxValue) == -1;

        public static bool InRange<T>(this T o, T minValue, T maxValue) where T: IComparable<T> 
            => o.CompareTo(minValue) >= 0 && o.CompareTo(maxValue) <= 0;

        public static T Max<T>(this T o, T value) => GenericMathUtils.Max(o, value);

        public static T Min<T>(this T o, T value) => GenericMathUtils.Min(o, value);

        public static NOptional<TResult> MapToOptional<T, TResult>(this T o, Func<T, TResult> mapper)
            => NOptional<T>.OfNullable(o).Map(mapper);

        public static int MakeHashCode<T>(this T o, params object[] objects)
        {
            HashCode hashCode = new HashCode();
            objects.ForEach(x => hashCode.Add(x));
            return hashCode.ToHashCode();
        }

        public static int MakeHashCode<T>(this T o, params Func<T, object>[] selectors)
        {
            HashCode hashCode = new HashCode();
            selectors.ForEach(x => hashCode.Add(x(o)));
            return hashCode.ToHashCode();
        }

        public static bool EqualCustomParam<T>(this T o, object param, Func<T, object> paramSelector)
        {
            var value1 = paramSelector(o);
            if (ReferenceEquals(param, value1)) return true;
            return Equals(param, value1);
        }

        public static bool EqualCustom<T>(this T o, object other,
            params Func<T, object>[] selectors)
        {
            if (other == null) return false;
            if (ReferenceEquals(o, other)) return true;
            if (other is T valid)
            {
                foreach (var item in selectors)
                {
                    if (!EqualByFunction(o, valid, item)) return false;
                }
            }
            return true;
        }

        public static bool EqualCustom<T>(this T o, object other,
            params (Func<T, object> thisField, Func<T, object> otherField)[] selectors)
        {
            if (other == null) return false;
            if (ReferenceEquals(o, other)) return true;
            if (other is T valid)
            {
                foreach (var item in selectors)
                {
                    if (!EqualByFunctions(o, valid, item)) return false;
                }
            }
            return true;
        }

        public static bool EqualCustom<T>(this T o, object other,
            params (object item1, object item2)[] fields)
        {
            if (other == null) return false;
            if (ReferenceEquals(o, other)) return true;
            if (other is T valid)
            {
                foreach (var item in fields)
                {
                    if (!Equals(item.item1, item.item2)) return false;
                }
            }
            return true;
        }

        private static bool EqualByFunctions<T>(T o, T valid, (Func<T, object> thisField, Func<T, object> otherField) param)
        {
            var value1 = param.thisField(o);
            var value2 = param.otherField(valid);
            return Equals(value1, value2);
        }

        private static bool EqualByFunction<T>(T o, T valid, Func<T, object> paramExpression)
        {
            var value1 = paramExpression(o);
            var value2 = paramExpression(valid);
            return Equals(value1, value2);
        }
    }
}