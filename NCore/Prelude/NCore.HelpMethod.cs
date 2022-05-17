using System.Collections;
using NCore.Gen;

namespace NCore
{
    public static partial class NCore
    {
        public static TResult OfAlwaysReturn<TResult>(Action action, TResult result)
        {
            action();
            return result;
        } 

        public static TResult OfAlwaysReturn<TResult>(Action action, Func<TResult> result)
        {
            action();
            return result();
        } 

        public static TResult OfAlwaysReturn<TIn, TResult>(TIn obj, Action<TIn> action, TResult result)
        {
            action(obj);
            return result;
        }

        public static TResult OfAlwaysReturn<TIn, TResult>(TIn obj, Action<TIn> action, Func<TResult> result)
        {
            action(obj);
            return result();
        }

        public static TResult OfAlwaysReturn<TIn, TResult>(TIn obj, Action<TIn> action, Func<TIn, TResult> result)
        {
            action(obj);
            return result(obj);
        }

        public static void OfIfSinle(Func<bool> predicate, Action action)
        {
            if (predicate()) action();
        }

        public static void OfIfSinle(bool predicate, Action action)
        {
            if (predicate) action();
        }

        public static void OfIfSingle<T>(T obj, Predicate<T> predicate, Action action)
        {
            if (predicate(obj)) action();
        }

        public static void OfIfSingle<T>(T obj, Predicate<T> predicate, Action<T> action)
        {
            if (predicate(obj)) action(obj);
        }

        public static TResult OfIfElse<TIn, TResult>(TIn obj,
                                                       Predicate<TIn> predicate,
                                                       Func<TResult> ifTrue,
                                                       Func<TResult> ifFalse)
            => predicate(obj) ? ifTrue() : ifFalse();

        public static TResult OfIfElse<TIn, TResult>(TIn obj,
                                                       Predicate<TIn> predicate,
                                                       Func<TIn, TResult> ifTrue,
                                                       Func<TIn, TResult> ifFalse)
            => predicate(obj) ? ifTrue(obj) : ifFalse(obj);

        public static TResult OfIfElse<TIn, TResult>(TIn obj,
                                                       Predicate<TIn> predicate,
                                                       Func<TResult> ifTrue,
                                                       Func<TIn, TResult> ifFalse)
            => predicate(obj) ? ifTrue() : ifFalse(obj);

        public static TResult OfIfElse<TIn, TResult>(TIn obj,
                                                       Predicate<TIn> predicate,
                                                       Func<TIn, TResult> ifTrue,
                                                       Func<TResult> ifFalse)
            => predicate(obj) ? ifTrue(obj) : ifFalse();

        public static int OfHashCode(params object[] values)
        {
            HashCode hashCode = new HashCode();
            foreach (var item in values)
            {
                hashCode.Add(item);
            }
            return hashCode.ToHashCode();
        }

        public static bool IsNull(object o) => o == null;
        public static bool IsNotNull(object o) => o != null;
        public static bool IsEmpty(string o) => o == string.Empty;
        public static bool IsNotEmpty(string o) => o != string.Empty;
        public static bool IsNullOrEmpty(string o) => string.IsNullOrEmpty(o);
        public static bool IsNotNullOrEmpty(string o) => !IsNullOrEmpty(o);

        public static bool IsEmpty<T>(IEnumerable<T> o) => !o.Any();
        public static bool IsNotEmpty<T>(IEnumerable<T> o) => o.Any();
        public static bool IsNullOrEmpty<T>(IEnumerable<T> o) => o == null || !o.Any();
        public static bool IsNotNullOrEmpty<T>(IEnumerable<T> o) => o != null || o.Any();

        public static IGen OfGen() => new Gen.Gen();
    }
}