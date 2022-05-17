using NCore.Monadas;

namespace NCore.Extensions
{
    public static class NOptionalEnumeratbleExtensions
    {
        public static NOptional<T> TryGetFirst<T>(this IEnumerable<T> o) => NOptional<IEnumerable<T>>.OfNullable(o).Map(x => x.First());

        public static NOptional<T> TryGetLast<T>(this IEnumerable<T> o) => NOptional<IEnumerable<T>>.OfNullable(o).Map(x => x.Last());

        public static NOptional<IEnumerable<T>> Flatten<T>(this IEnumerable<NOptional<T>> o)
            => o.All(x => x.HasValue)
                ? NOptional<IEnumerable<T>>.Of(o.Select(x => x.Value))
                : NOptional<IEnumerable<T>>.Empty;

        public static IEnumerable<NOptional<T>> ToOptionalCollecion<T>(this IEnumerable<T> o)
            => o.Select(x => x.ToOptional());
    }
}