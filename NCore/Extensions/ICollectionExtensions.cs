namespace NCore.Extensions
{
    public static class ICollectionExtensions
    {
        public static void AddIfNotExists<T>(this ICollection<T> o, T item)
        {
            if(!o.Contains(item)) o.Add(item);
        }

        public static void AddRangeIfNotExists<T>(this ICollection<T> o, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                o.AddIfNotExists(item);
            }
        }

        public static bool AddIf<T>(this ICollection<T> o, Func<T, bool> predicate, T value)
        {
            if (predicate(value))
            {
                o.Add(value);
                return true;
            }

            return false;
        }

        public static void AddRange<T>(this ICollection<T> o, params T[] values)
        {
            foreach (T value in values)
            {
                o.Add(value);
            }
        }

        public static void AddRangeIf<T>(this ICollection<T> o, Func<T, bool> predicate, params T[] values)
        {
            foreach (var item in values)
            {
                if (predicate(item)) o.Add(item);
            }
        }

        public static bool ContainsAll<T>(this ICollection<T> o, params T[] values)
        {
            foreach (T value in values)
            {
                if (!o.Contains(value))
                {
                    return false;
                }
            }

            return true;
        }

        public static bool ContainsAny<T>(this ICollection<T> o, params T[] values)
        {
            foreach (T value in values)
            {
                if (o.Contains(value))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsEmpty<T>(this ICollection<T> o) => o.Count == 0;

        public static bool IsNotEmpty<T>(this ICollection<T> o) => !o.IsEmpty();

        public static void RemoveIf<T>(this ICollection<T> o, T value, Func<T, bool> predicate)
        {
            if (predicate(value)) o.Remove(value);
        }

        public static void RemoveRange<T>(this ICollection<T> o, params T[] values)
        {
            foreach (T value in values)
            {
                o.Remove(value);
            }
        }

        public static void RemoveRangeIf<T>(this ICollection<T> o, Func<T, bool> predicate, params T[] values)
        {
            foreach (T value in values)
            {
                if (predicate(value))
                {
                    o.Remove(value);
                }
            }
        }

        public static void RemoveWhere<T>(this ICollection<T> o, Func<T, bool> predicate)
        {
            List<T> list = o.Where(predicate).ToList();
            foreach (T item in list)
            {
                o.Remove(item);
            }
        }
    }
}