namespace NCore.Net
{
    public class UserAgent
    {
        private string _value;

        public UserAgent(string value)
        {
            this._value = value;
        }

        public static bool IsNullOrEmpty(UserAgent userAgent) => string.IsNullOrEmpty(userAgent._value);

        public static implicit operator UserAgent(string o)
        {
            return new UserAgent(o);
        }
        public static implicit operator string(UserAgent o)
        {
            return o._value;
        }

        public static bool operator ==(UserAgent v1, UserAgent v2)
        {
            return v1._value == v2._value;
        }
        public static bool operator !=(UserAgent v1, UserAgent v2)
        {
            return v1._value != v2._value;
        }

        public override bool Equals(object obj)
        {
            return obj is UserAgent agent &&
                   _value == agent._value;
        }

        public override int GetHashCode() => HashCode.Combine(_value);

        public static UserAgent Empty => "";

        public static UserAgent FIREFIX_77_MAC = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.15; rv:77.0) Gecko/20100101 Firefox/77.0";
        public static UserAgent FIREFIX_77_LINUX = "Mozilla/5.0 (X11; Linux x86_64; rv:77.0) Gecko/20100101 Firefox/77.0";
        public static UserAgent FIREFIX_77_WINDOWS = "Mozilla/5.0 (Windows NT 6.1; Win64; x64; rv:77.0) Gecko/20100101 Firefox/77.0";
        public static UserAgent FIREFIX_77_UBUNTU = "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:77.0) Gecko/20100101 Firefox/77.0";
        public static UserAgent FIREFIX_77_WINDOWS2 = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:77.0) Gecko/20100101 Firefox/77.0";
    }
}