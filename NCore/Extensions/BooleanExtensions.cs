namespace NCore.Extensions
{
    public static class BooleanExtensions
    {
        public static bool And(this bool o, bool value) => o && value;
        public static bool And(this bool o, Func<bool> value) => o && value();
        public static bool Or(this bool o, bool value) => o || value;
        public static bool Or(this bool o, Func<bool> value) => o || value();
        public static bool IsTrue(this bool o) => o == true;
        public static bool IsFalse(this bool o) => o == false;
        public static void IfTrue(this bool o, Action action)
        {
            if (o) action();
        }
        public static void IfFalse(this bool o, Action action)
        {
            if (!o) action();
        }
        public static void IfTrueOrElse(this bool o, Action trueAction, Action falseAction)
        {
            if (o) trueAction();
            else falseAction();
        }
        public static T IfTrueOrElse<T>(this bool o, Func<T> trueAction, Func<T> falseAction)
        {
            if (o) return trueAction();
            return falseAction();
        }
        public static void IfFalseOrElse(this bool o, Action trueAction, Action falseAction)
        {
            if (!o) trueAction();
            else falseAction();
        }
        public static T IfFalseOrElse<T>(this bool o, Func<T> trueAction, Func<T> falseAction)
        {
            if (!o) return trueAction();
            return falseAction();
        }

        public static byte ToBinary(this bool o) => Convert.ToByte(o);

        public static string ToString(this bool o, string trueValue, string falseValue) => o ? trueValue : falseValue;
    }
}