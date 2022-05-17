using NCore.Monadas;

namespace NCore.Extensions
{
    public static class NOptionalExtensions
    {
        public static T? ToNullable<T>(this NOptional<T> o)
            where T : struct
        {
            return o.HasValue
                ? (T?)o.Value
                : null;
        }

        public static NOptional<TResult> IfTureMap<TResult>(this NOptional<bool> o, Func<bool,TResult> mapper)
            => o.Filter(x => x).Map(mapper);

        public static NOptional<TResult> IfFalseMap<TResult>(this NOptional<bool> o, Func<bool,TResult> mapper)
            => o.Filter(x => !x).Map(mapper);

        public static void IfTruePresent(this NOptional<bool> o, Action<bool> action)
            => o.Filter(x => x).IfPresent(action);

        public static void IfFalsePresent(this NOptional<bool> o, Action<bool> action)
            => o.Filter(x => !x).IfPresent(action);

        public static NOptional<bool> IsTrueFilter(this NOptional<bool> o) => o.Filter(x => x);

        public static NOptional<bool> IsFalseFilter(this NOptional<bool> o) => o.Filter(x => !x);

        public static NOptional<string> IsNullOrEmpty(this NOptional<string> o) => o.Filter(x => string.IsNullOrEmpty(x));

        public static NOptional<string> IsNotNullOrEmpty(this NOptional<string> o) => o.Filter(x => !string.IsNullOrEmpty(x));
    }
}