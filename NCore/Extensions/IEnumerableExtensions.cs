using NCore.Monadas;
using static NCore.NCore;

namespace NCore.Extensions
{
    public static class IEnumerableExtensions
    {
        public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source) => source.ToHashSet(null);
        
        public static HashSet<TSource> ToHashSet<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource>? comparer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new HashSet<TSource>(source, comparer);
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source,
            IEqualityComparer<TKey>? comparer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return source.ToDictionary(e => e.Key, e => e.Value, comparer);
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source) =>
            source.ToDictionary(e => e.Key, e => e.Value, EqualityComparer<TKey>.Default);

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey Key, TValue Value)> source,
            IEqualityComparer<TKey>? comparer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return source.ToDictionary(e => e.Key, e => e.Value, comparer);
        }

        public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this IEnumerable<(TKey Key, TValue Value)> source) =>
            source.ToDictionary(e => e.Key, e => e.Value, EqualityComparer<TKey>.Default);
        
        public static NOptional<int> TryGetCollectionCount<T>(this IEnumerable<T> source)
            => OfNullable(source)
                .Map(x => {
                    if (x is ICollection<T>)
                        return ((ICollection<T>)x).Count;
                    if (x is IReadOnlyCollection<T>)
                        return ((IReadOnlyCollection<T>)x).Count;
                    return x.Count();
                });
            
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> o,
            Func<TSource, TKey> keySelector)
            => o.GroupBy(keySelector).Select(x => x.First());

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var element in source)
                action(element);
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T, int> action)
        {
            var index = 0;
            foreach (var element in source)
                action(element, index++);
        }

        public static IEnumerable<KeyValuePair<int, TSource>> Index<TSource>(this IEnumerable<TSource> source, int startIndex)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return source.Select((item, index) => new KeyValuePair<int, TSource>(startIndex + index, item));
        }

        public static IEnumerable<KeyValuePair<int, TSource>> Index<TSource>(this IEnumerable<TSource> source) => source.Index(0); 

        public static IEnumerable<T> SkipTake<T>(this IEnumerable<T> o, int skip, int take) => o.Skip(skip).Take(take);
        public static IEnumerable<T> DistictExt<T>(this IEnumerable<T> o) => o.GroupBy(x => x.GetHashCode()).Select(x => x.First());
        public static IEnumerable<T> DistictExt<T, S>(this IEnumerable<T> o, Func<T, S> keySelector) => o.GroupBy(keySelector).Select(x => x.First());
        public static List<TResult> ToOneList<TIn, TResult>(this IEnumerable<IEnumerable<TIn>> o, Func<TIn, TResult> convert)
        {
            List<TResult> results = new List<TResult>();
            foreach (var item in o)
            {
                results.AddRange(item.Select(convert));
            }
            return results;
        }
        public static List<T> ToOneList<T>(this IEnumerable<IEnumerable<T>> o)
        {
            List<T> results = new List<T>();
            foreach (var item in o)
            {
                results.AddRange(item);
            }
            return results;
        }

        public static bool ContainsAll<T>(this IEnumerable<T> o, params T[] values)
        {
            T[] list = o.ToArray();
            foreach (T value in values)
            {
                if (!list.Contains(value)) return false;
            }
            return true;
        }

        public static bool ContainsAny<T>(this IEnumerable<T> o, params T[] values)
        {
            T[] list = o.ToArray();
            foreach (T value in values)
            {
                if (list.Contains(value)) return true;
            }
            return false;
        }

        public static bool IsEmpty<T>(this IEnumerable<T> o) => !o.Any();

        public static void IfEmpty<T>(this IEnumerable<T> o, Action action)
        {
            if (o.IsEmpty()) action();
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> o) => o.Any();

        public static void IsNotEmpty<T>(this IEnumerable<T> o, Action action)
        {
            if (o.IsNotEmpty()) action();
        }

        public static string StringJoin(this IEnumerable<string> o, string separator)
            => string.Join(separator, o);

        public static string StringJoin(this IEnumerable<string> o, char separator)
            => string.Join(separator.ToString(), o);

        public static string PathCombine(this IEnumerable<string> o)
            => Path.Combine(o.ToArray());

                public static NOptional<T> TryFirst<T>(this IEnumerable<T> o)
            => o.Any() 
                ? NOptional<T>.OfNullable(o.FirstOrDefault()!)
                : NOptional<T>.Empty;

        public static NOptional<T> TryLast<T>(this IEnumerable<T> o)
            => o.Any()
                ? NOptional<T>.OfNullable(o.LastOrDefault()!)
                : NOptional<T>.Empty;

        public static IEnumerable<T> Choose<T>(this IEnumerable<NOptional<T>> items) =>
            items.Where(i => i.HasValue).Select(i => i.Value);

        public static NOptional<T> TryFind<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            var item = items.Where(predicate).Take(1).ToList();
            if (item.Any()) return OfNullable(item.First());
            return OfEmptyOptional<T>();
        }

        public static NOptional<T> TryElementAt<T>(this IEnumerable<T> items, int index)
        {
            int currentIndex = 0;
            foreach (var item in items)
            {
                if (currentIndex == index) return OfNullable(item);
            }
            return OfEmptyOptional<T>();
        }
    }
}