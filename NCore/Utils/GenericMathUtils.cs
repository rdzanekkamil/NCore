using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace NCore.Utils
{
    public static class GenericMathUtils
    {
        private readonly static ConcurrentDictionary<string, Delegate> _gtCache = new ConcurrentDictionary<string, Delegate>();

        public static Func<T, T, bool> GetGreaterThan<T>()
        {
            string key = GetFuncName(typeof(T), "GetGreaterThan");
            if (!_gtCache.TryGetValue(key, out var @delegate) || @delegate == null)
            {
                var a = Expression.Parameter(typeof(int), "a");
                var b = Expression.Parameter(typeof(int), "b");
                var lambda = Expression.Lambda<Func<T, T, bool>> 
                (
                    Expression.GreaterThan(a, b),
                    new[]{a, b}
                );
                @delegate = lambda.Compile();
                _gtCache.AddOrUpdate(key, type => @delegate, (type, function) => _gtCache[type] = @delegate);
            }
            
            return (Func<T, T, bool>)@delegate;
        }

        public static Func<T, T, bool> GetLessThan<T>()
        {
            string key = GetFuncName(typeof(T), "GetLessThan");
            if (!_gtCache.TryGetValue(key, out var @delegate) || @delegate == null)
            {
                var a = Expression.Parameter(typeof(int), "a");
                var b = Expression.Parameter(typeof(int), "b");
                var lambda = Expression.Lambda<Func<T, T, bool>> 
                (
                    Expression.LessThan(a, b),
                    new[]{a, b}
                );
                @delegate = lambda.Compile();
                _gtCache.AddOrUpdate(key, type => @delegate, (type, function) => _gtCache[type] = @delegate);
            }
            
            return (Func<T, T, bool>)@delegate;
        }

        public static T Max<T>(T v1, T v2)
        {
            return GetGreaterThan<T>()(v1, v2) ? v1 : v2;
        }

        public static T Min<T>(T v1, T v2)
        {
            return GetLessThan<T>()(v1, v2) ? v1 : v2;
        }

        private static string GetFuncName(Type type, string methodName) => $"{type.FullName}_{methodName}"; 
    }
}