using System.Linq.Expressions;

namespace NCore
{
    public static partial class NCore
    {
        public static Func<TResult> OfFunc<TResult>(Func<TResult> f) => f;
        public static Func<T1,TResult> OfFunc<T1,TResult>(Func<T1,TResult> f) => f;
        public static Func<T1,T2,TResult> OfFunc<T1,T2,TResult>(Func<T1,T2,TResult> f) => f;
        public static Func<T1,T2,T3,TResult> OfFunc<T1,T2,T3,TResult>(Func<T1,T2,T3,TResult> f) => f;
        public static Func<T1,T2,T3,T4,TResult> OfFunc<T1,T2,T3,T4,TResult>(Func<T1,T2,T3,T4,TResult> f) => f;
        public static Func<T1,T2,T3,T4,T5,TResult> OfFunc<T1,T2,T3,T4,T5,TResult>(Func<T1,T2,T3,T4,T5,TResult> f) => f;
        public static Func<T1,T2,T3,T4,T5,T6,TResult> OfFunc<T1,T2,T3,T4,T5,T6,TResult>(Func<T1,T2,T3,T4,T5,T6,TResult> f) => f;
        public static Func<T1,T2,T3,T4,T5,T6,T7,TResult> OfFunc<T1,T2,T3,T4,T5,T6,T7,TResult>(Func<T1,T2,T3,T4,T5,T6,T7,TResult> f) => f;

        public static Expression<Func<T,TResult>> OfExpFunc<T,TResult>(Expression<Func<T,TResult>> f) => f;
        public static Expression<Func<T1,T2,TResult>> OfExpFunc<T1,T2,TResult>(Expression<Func<T1,T2,TResult>> f) => f;
        public static Expression<Func<T1,T2,T3,TResult>> OfExpFunc<T1,T2,T3,TResult>(Expression<Func<T1,T2,T3,TResult>> f) => f;
        public static Expression<Func<T1,T2,T3,T4,TResult>> OfExpFunc<T1,T2,T3,T4,TResult>(Expression<Func<T1,T2,T3,T4,TResult>> f) => f;
        public static Expression<Func<T1,T2,T3,T4,T5,TResult>> OfExpFunc<T1,T2,T3,T4,T5,TResult>(Expression<Func<T1,T2,T3,T4,T5,TResult>> f) => f;
        public static Expression<Func<T1,T2,T3,T4,T5,T6,TResult>> OfExpFunc<T1,T2,T3,T4,T5,T6,TResult>(Expression<Func<T1,T2,T3,T4,T5,T6,TResult>> f) => f;
        public static Expression<Func<T1,T2,T3,T4,T5,T6,T7,TResult>> OfExpFunc<T1,T2,T3,T4,T5,T6,T7,TResult>(Expression<Func<T1,T2,T3,T4,T5,T6,T7,TResult>> f) => f;

        public static Action OfAction(Action action) => action;
        public static Action<T> OfAction<T>(Action<T> action) => action;
        public static Action<T1,T2> OfAction<T1,T2>(Action<T1,T2> action) => action;
        public static Action<T1,T2,T3> OfAction<T1,T2,T3>(Action<T1,T2,T3> action) => action;
        public static Action<T1,T2,T3,T4> OfAction<T1,T2,T3,T4>(Action<T1,T2,T3,T4> action) => action;
        public static Action<T1,T2,T3,T4,T5> OfAction<T1,T2,T3,T4,T5>(Action<T1,T2,T3,T4,T5> action) => action;
        public static Action<T1,T2,T3,T4,T5,T6> OfAction<T1,T2,T3,T4,T5,T6>(Action<T1,T2,T3,T4,T5,T6> action) => action;
        public static Action<T1,T2,T3,T4,T5,T6,T7> OfAction<T1,T2,T3,T4,T5,T6,T7>(Action<T1,T2,T3,T4,T5,T6,T7> action) => action;

        public static Expression<Action<T>> OfExpAction<T>(Expression<Action<T>> action) => action;
        public static Expression<Action<T1,T2>> OfExpAction<T1,T2>(Expression<Action<T1,T2>> action) => action;
        public static Expression<Action<T1,T2,T3>> OfExpAction<T1,T2,T3>(Expression<Action<T1,T2,T3>> action) => action;
        public static Expression<Action<T1,T2,T3,T4>> OfExpAction<T1,T2,T3,T4>(Expression<Action<T1,T2,T3,T4>> action) => action;
        public static Expression<Action<T1,T2,T3,T4,T5>> OfExpAction<T1,T2,T3,T4,T5>(Expression<Action<T1,T2,T3,T4,T5>> action) => action;
        public static Expression<Action<T1,T2,T3,T4,T5,T6>> OfExpAction<T1,T2,T3,T4,T5,T6>(Expression<Action<T1,T2,T3,T4,T5,T6>> action) => action;
        public static Expression<Action<T1,T2,T3,T4,T5,T6,T7>> OfExpAction<T1,T2,T3,T4,T5,T6,T7>(Expression<Action<T1,T2,T3,T4,T5,T6,T7>> action) => action;
    }
}