using System.Collections.Concurrent;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using NCore.Expressions;
using static NCore.NCore;

namespace NCore.Monadas
{
    public struct NBuilder<T> : IDisposable
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ConcurrentDictionary<PropertyInfo, object> mapValue = new ConcurrentDictionary<PropertyInfo, object>();

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private ConcurrentBag<Action<T>> executeAcctionMap = new ConcurrentBag<Action<T>>();

        private Func<T> factory = null!;
        public NOptional<T> Value { get; private set; } = NOptional<T>.Empty;


        public NBuilder(T item)
        {
            this.Value = NOptional<T>.Of(item);
        }

        public NBuilder(Func<T> factory)
        {
            this.factory = factory;
        }

        public NBuilder()
        {
            this.factory = () => TypeInitializer.InitializeInstance<T>();
        }

        public NBuilder<T> With<TKey>(Expression<Func<T, TKey>> param, TKey value)
        {
            var memberSelectorExpression = param.Body as MemberExpression;
            if (memberSelectorExpression != null)
            {
                var property = memberSelectorExpression.Member as PropertyInfo;
                if (property != null)
                {
                    mapValue.AddOrUpdate(property, value!, (k,v) => value!);
                    //property.SetValue(this.Value, value, null);
                }
            }
            return this;
        }

        public NBuilder<T> WithAction(Action<T> action)
        {
            executeAcctionMap.Add(action);
            return this;
        }

        public T Build()
        {
            var result = this.Value.OrElse(factory());
            foreach (var item in mapValue)
            {
                item.Key.SetValue(result, item.Value, null);
            }
            foreach (var item in executeAcctionMap)
            {
                item(result);
            }
            return result;
        }

        public void Dispose()
        {
            this.mapValue.Clear();
            GC.SuppressFinalize(this.mapValue);
            GC.SuppressFinalize(this);
        }
    }
}