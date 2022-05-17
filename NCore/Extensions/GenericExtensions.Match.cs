using NCore.Monadas;
using static NCore.NCore;

namespace NCore.Extensions
{
    public static partial class GenericExtensions
    {
        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            params (TIn option, Func<TIn, TResult> resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (Equals(o, item.option)) return OfNullable(item.resultOption(o)); 
            }
            return OfEmptyOptional<TResult>();
        }

        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            params (TIn option, Func<TResult> resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (Equals(o, item.option)) return OfNullable(item.resultOption()); 
            }
            return OfEmptyOptional<TResult>();
        }

        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            params (TIn option, TResult resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (Equals(o, item.option)) return OfNullable(item.resultOption); 
            }
            return OfEmptyOptional<TResult>();
        }

        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            params (Func<TIn, bool> option, Func<TIn, TResult> resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (item.option(o)) return OfNullable(item.resultOption(o)); 
            }
            return OfEmptyOptional<TResult>();
        }

        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            params (Func<TIn, bool> option, Func<TResult> resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (item.option(o)) return OfNullable(item.resultOption()); 
            }
            return OfEmptyOptional<TResult>();
        }

        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            params (Func<TIn, bool> option, TResult resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (item.option(o)) return OfNullable(item.resultOption); 
            }
            return OfEmptyOptional<TResult>();
        }

        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            Func<TResult> defaultAction,
            params (TIn option, Func<TIn, TResult> resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (Equals(o, item.option)) return OfNullable(item.resultOption(o)); 
            }
            return defaultAction != null ? OfNullable(defaultAction()) : OfEmptyOptional<TResult>();
        }

        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            Func<TResult> defaultAction,
            params (TIn option, Func<TResult> resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (Equals(o, item.option)) return OfNullable(item.resultOption()); 
            }
            return defaultAction != null ? OfNullable(defaultAction()) : OfEmptyOptional<TResult>();
        }

        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            Func<TResult> defaultAction,
            params (TIn option, TResult resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (Equals(o, item.option)) return OfNullable(item.resultOption); 
            }
            return defaultAction != null ? OfNullable(defaultAction()) : OfEmptyOptional<TResult>();
        }

        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            TResult defaultOption,
            params (TIn option, Func<TIn, TResult> resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (Equals(o, item.option)) return OfNullable(item.resultOption(o)); 
            }
            return defaultOption != null ? OfNullable(defaultOption) : OfEmptyOptional<TResult>();
        }

        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            TResult defaultOption,
            params (TIn option, Func<TResult> resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (Equals(o, item.option)) return OfNullable(item.resultOption()); 
            }
            return defaultOption != null ? OfNullable(defaultOption) : OfEmptyOptional<TResult>();
        }

        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            TResult defaultOption,
            params (TIn option, TResult resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (Equals(o, item.option)) return OfNullable(item.resultOption); 
            }
            return defaultOption != null ? OfNullable(defaultOption) : OfEmptyOptional<TResult>();
        }

        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            Func<TResult> defaultAction,
            params (Func<TIn, bool> option, Func<TIn, TResult> resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (item.option(o)) return OfNullable(item.resultOption(o)); 
            }
            return defaultAction != null ? OfNullable(defaultAction()) : OfEmptyOptional<TResult>();
        }

        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            Func<TResult> defaultAction,
            params (Func<TIn, bool> option, Func<TResult> resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (item.option(o)) return OfNullable(item.resultOption()); 
            }
            return defaultAction != null ? OfNullable(defaultAction()) : OfEmptyOptional<TResult>();
        }

        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            Func<TResult> defaultAction,
            params (Func<TIn, bool> option, TResult resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (item.option(o)) return OfNullable(item.resultOption); 
            }
            return defaultAction != null ? OfNullable(defaultAction()) : OfEmptyOptional<TResult>();
        }

        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            TResult defaultOption,
            params (Func<TIn, bool> option, Func<TIn, TResult> resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (item.option(o)) return OfNullable(item.resultOption(o)); 
            }
            return defaultOption != null ? OfNullable(defaultOption) : OfEmptyOptional<TResult>();
        }

        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            TResult defaultOption,
            params (Func<TIn, bool> option, Func<TResult> resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (item.option(o)) return OfNullable(item.resultOption()); 
            }
            return defaultOption != null ? OfNullable(defaultOption) : OfEmptyOptional<TResult>();
        }

        public static NOptional<TResult> Match<TIn, TResult>(this TIn o,
            TResult defaultOption,
            params (Func<TIn, bool> option, TResult resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (item.option(o)) return OfNullable(item.resultOption); 
            }
            return defaultOption != null ? OfNullable(defaultOption) : OfEmptyOptional<TResult>();
        }

        public static void Match<T>(this T o,
            params (T option, Action<T> resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (Equals(o, item.option))
                {
                    item.resultOption(o);
                    break;
                }  
            }
        }

        public static void Match<T>(this T o,
            params (Func<T, bool> option, Action<T> resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (item.option(o))
                {
                    item.resultOption(o); 
                    break;
                }
            }
        }

        public static void Match<T>(this T o,
            Action defaultAction,
            params (T option, Action<T> resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (Equals(o, item.option)) 
                {
                    item.resultOption(o); 
                    return;
                }
            }
            defaultAction();
        }

        public static void Match<T>(this T o,
            Action defaultAction,
            params (Func<T, bool> option, Action<T> resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (item.option(o)) 
                {
                    item.resultOption(o); 
                    return;
                }
            }
            defaultAction();
        }

        public static void Match<T>(this T o,
            params (Func<T, bool> option, Action resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (item.option(o)) 
                {
                    item.resultOption(); 
                    break;
                }
            }
        }

        public static void Match<T>(this T o,
            params (T option, Action resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (Equals(o, item.option)) 
                {
                    item.resultOption(); 
                    break;
                }
            }
        }

        public static void Match<T>(this T o,
            Action defaultAction,
            params (T option, Action resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (Equals(o, item.option)) 
                {
                    item.resultOption(); 
                    return;
                }
            }
            defaultAction();
        }

        public static void Match<T>(this T o,
            Action defaultAction,
            params (Func<T, bool> option, Action resultOption)[] items)
        {
            foreach (var item in items)
            {
                if (item.option(o)) 
                {
                    item.resultOption(); 
                    return;
                }
            }
            defaultAction();
        }
    }
}