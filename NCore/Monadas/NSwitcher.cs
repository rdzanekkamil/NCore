using System.Collections;
using System.Collections.Concurrent;
using System.Diagnostics;
using NCore.Extensions;
using static NCore.NCore;

namespace NCore.Monadas
{
    public struct NSwitcher<T> : IEnumerable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ConcurrentDictionary<object, Func<object, T>> marcherCollecion = new();

        private Func<T> defaultFunction = null!;

        public static NSwitcher<T> Empty = new NSwitcher<T>();

        public NSwitcher()
        {
        }

        public NSwitcher(Func<T> defaultFunction)
        {
            this.defaultFunction = defaultFunction;
        } 

        public NSwitcher<T> WithCase<V>(V option, Func<V,T> function)
        {
            Add<V>((option, function));
            return this;
        }

        public NSwitcher<T> WithCase<V>(V option, T result)
        {
            Add<V>(option, result);
            return this;
        }

        public void Add<V>((V option, Func<V, T> function) item)
        {
            var func = new Func<object, T>(x => item.function(x.TryCast<V>().Get()));
            marcherCollecion.AddOrUpdate(item.option!, func, (o, f) => func);
        }

        public void Add<V>((V option, T result) item)
        {
            Add<V>((item.option, _ => item.result));
        }

        public void Add<V>(V option, Func<V, T> function)
        {
            Add<V>((option, function));
        }

        public void Add<V>(V option, T result)
        {
            Add<V>((option, _ => result));
        }

        public void Remove<V>(V option) => marcherCollecion.Remove(option!, out Func<object, T> func);

        public NOptional<T> Match(object value)
        {
            if (marcherCollecion.TryGetValue(value, out Func<object, T> function))
                return OfNullable(function.Invoke(value));
            return defaultFunction == null ? NOptional<T>.Empty : OfNullable(defaultFunction());
        }

        public IEnumerator GetEnumerator() => marcherCollecion.GetEnumerator();
    }
}