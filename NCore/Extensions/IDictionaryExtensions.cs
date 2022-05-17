using NCore.Monadas;

namespace NCore.Extensions
{
    public static class IDictionaryExtensions
    {
        public static void AddRange<TKey, TValue>(this IDictionary<TKey, TValue> o, IDictionary<TKey, TValue> entities)
        {
            foreach (var item in entities)
            {
                o.Add(item.Key, item.Value);
            }
        }

        public static void AddRangeIfNotExists<TKey, TValue>(this IDictionary<TKey, TValue> o, IDictionary<TKey, TValue> entities)
        {
            foreach (var item in entities)
            {
                if(!o.ContainsKey(item.Key))
                {
                    o.Add(item.Key, item.Value);
                }
            }
        }

        public static void AddIfNotExists<TKey, TValue>(this IDictionary<TKey, TValue> o, TKey key, TValue value)
        {
            if(!o.ContainsKey(key)) o.Add(key, value);
        }

        public static void AddElement<TKey, TValue>(this IDictionary<TKey, ICollection<TValue>> o, TKey key, TValue value)
        {
            if(o.ContainsKey(key)) o[key].Add(value);
            else o.Add(key, new List<TValue>() { value });
        }

        public static void AddElementIfNotExists<TKey, TValue>(this IDictionary<TKey, ICollection<TValue>> o, TKey key, TValue value)
        {
            if(o.ContainsKey(key)) o[key].AddIfNotExists(value);
            else o.Add(key, new List<TValue>() { value });
        }

        public static void AddRangeElementIfNotExists<TKey, TValue>(this IDictionary<TKey, ICollection<TValue>> o,
                                                                    TKey key,
                                                                    ICollection<TValue> value)
        {
            if(o.ContainsKey(key)) o[key].AddRangeIfNotExists(value);
            else o.Add(key, new List<TValue>(value));
        }

        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> o, TKey key, TValue value)
        {
            if (!o.ContainsKey(key)) o.Add(new KeyValuePair<TKey, TValue>(key, value));
            else o[key] = value; 
            return o[key];
        }

        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> o,
                                                       TKey key,
                                                       TValue addValue,
                                                       Func<TKey, TValue, TValue> updateValueFactory)
        {
            if (!o.ContainsKey(key)) o.Add(new KeyValuePair<TKey, TValue>(key, addValue));
            else o[key] = updateValueFactory(key, o[key]);
            return o[key];
        }

        public static TValue AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> o,
                                                       TKey key,
                                                       Func<TKey, TValue> addValueFactory,
                                                       Func<TKey, TValue, TValue> updateValueFactory)
        {
            if (!o.ContainsKey(key)) o.Add(new KeyValuePair<TKey, TValue>(key, addValueFactory(key)));
            else o[key] = updateValueFactory(key, o[key]);
            return o[key];
        }

        public static bool ContainsAllKey<TKey, TValue>(this IDictionary<TKey, TValue> o, params TKey[] keys)
        {
            foreach (TKey value in keys)
            {
                if (!o.ContainsKey(value)) return false;
            }
            return true;
        }

        public static bool ContainsAnyKey<TKey, TValue>(this IDictionary<TKey, TValue> o, params TKey[] keys)
        {
            foreach (TKey value in keys)
            {
                if (o.ContainsKey(value)) return true;
            }
            return false;
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> o, TKey key, TValue value)
        {
            if (!o.ContainsKey(key)) o.Add(new KeyValuePair<TKey, TValue>(key, value));
            return o[key];
        }

        public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> o, TKey key, Func<TKey, TValue> valueFactory)
        {
            if (!o.ContainsKey(key)) o.Add(new KeyValuePair<TKey, TValue>(key, valueFactory(key)));
            return o[key];
        }

        public static NOptional<TValue> TryGetValue<TKey,TValue>(this IDictionary<TKey,TValue> o, TKey key)
            =>  o.TryGetValue(key, out TValue value) ? NOptional<TValue>.OfNullable(value) : NOptional<TValue>.Empty; 
    }
}