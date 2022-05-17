using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using NCore.Monadas;
using NCore.Utils;

namespace NCore.Extensions
{
    public static class StringExtensions
    {
        public static string CalculateMd5Hash(this string value)
        {
            string result;
            using (MD5 hash = MD5.Create())
            {
                result = String.Join
                (
                    "",
                    from ba in hash.ComputeHash
                    (
                        Encoding.UTF8.GetBytes(value)
                    ) 
                    select ba.ToString("x2")
                );
            }
            return result;
        }

        public static bool ConrainsAll(this string o, params string[] args)
        {
            foreach (string value in args)
            {
                if (o.IndexOf(value) == -1) return false;
            }
            return true;
        }

        public static bool ContainsAll(this string @this, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value, comparisonType) == -1) return false;
            }
            return true;
        }

        public static bool ContainsAny(this string @this, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value) != -1) return true;
            }
            return false;
        }

        public static bool ContainsAny(this string @this, StringComparison comparisonType, params string[] values)
        {
            foreach (string value in values)
            {
                if (@this.IndexOf(value, comparisonType) != -1) return true;
            }
            return false;
        }

        public static bool StartsWithIgnoreCases(this string o, string value)
            => o.StartsWith(value, StringComparison.OrdinalIgnoreCase);

        public static bool EqualIgnoreCases(this string o, string value) 
            => o.Equals(value, StringComparison.OrdinalIgnoreCase);

        public static bool ContainsIgnoreCases(this string o, string value) 
            => o.Contains(value, StringComparison.OrdinalIgnoreCase);

        public static string Extract(this string @this, Func<char, bool> predicate)
            => new string(@this.ToCharArray().Where(predicate).ToArray());

        public static string FormatWith(this string @this, params object[] values) => String.Format(@this, values);

        public static string IfEmpty(this string value, string defaultValue) => value.IsNotEmpty() ? value : defaultValue;

        public static void IfEmpty(this string value, Action action)
        {
            if (value.IsNotEmpty()) action();
        }

        public static bool IsAlpha(this string @this) => !Regex.IsMatch(@this, "[^a-zA-Z]");
        
        public static bool IsAlphaNumeric(this string @this) => !Regex.IsMatch(@this, "[^a-zA-Z0-9]");

        public static bool IsAnagram(this string @this, string otherString) 
            => @this.OrderBy(c => c).SequenceEqual(otherString.OrderBy(c => c));

        public static bool IsLike(this string @this, string pattern)
        {
            // Turn the pattern into regex pattern, and match the whole string with ^$
            string regexPattern = "^" + Regex.Escape(pattern) + "$";

            // Escape special character ?, #, *, [], and [!]
            regexPattern = regexPattern.Replace(@"\[!", "[^")
                .Replace(@"\[", "[")
                .Replace(@"\]", "]")
                .Replace(@"\?", ".")
                .Replace(@"\*", ".*")
                .Replace(@"\#", @"\d");

            return Regex.IsMatch(@this, regexPattern);
        }

        public static bool IsNumeric(this string @this) => !Regex.IsMatch(@this, "[^0-9]");

        public static bool IsPalindrome(this string @this)
        {
            var rgx = new Regex("[^a-zA-Z0-9]");
            @this = rgx.Replace(@this, "");
            return @this.SequenceEqual(@this.Reverse());
        }

        public static string Left(this string @this, int length) => @this.Substring(0, length);

        public static string LeftSafe(this string @this, int length) => @this.Substring(0, Math.Min(length, @this.Length));

        public static string RemoveWhere(this string @this, Func<char, bool> predicate) 
            => new string(@this.ToCharArray().Where(x => !predicate(x)).ToArray());

        public static string Right(this string @this, int length) => @this.Substring(@this.Length - length);

        public static string RightSafe(this string @this, int length) => @this.Substring(Math.Max(0, @this.Length - length));

        public static string[] Split(this string @this, string separator, StringSplitOptions option = StringSplitOptions.None)
            => @this.Split(new[] {separator}, option);

        public static String HtmlDecode(this String s) => HttpUtility.HtmlDecode(s);

        public static String HtmlEncode(this String s) => HttpUtility.HtmlEncode(s);

        public static Boolean IsMatch(this String input, String pattern) => Regex.IsMatch(input, pattern);

        public static Boolean IsMatch(this String input, String pattern, RegexOptions options) => Regex.IsMatch(input, pattern, options);

        public static Match Match(this String input, String pattern) => Regex.Match(input, pattern);

        public static Match Match(this String input, String pattern, RegexOptions options) => Regex.Match(input, pattern, options);

        public static MatchCollection Matches(this String input, String pattern) => Regex.Matches(input, pattern);

        public static MatchCollection Matches(this String input, String pattern, RegexOptions options) 
            => Regex.Matches(input, pattern, options);

        public static String Join(this String separator, String[] value) => String.Join(separator, value);

        public static String Join(this String separator, Object[] values) => String.Join(separator, values);

        public static String Join<T>(this String separator, IEnumerable<T> values) => String.Join(separator, values);

        public static String Join(this String separator, IEnumerable<String> values) => String.Join(separator, values);

        public static String Join(this String separator, String[] value, Int32 startIndex, Int32 count) 
            => String.Join(separator, value, startIndex, count);

        public static string GetBetween(this string @this, string before, string after)
        {
            int beforeStartIndex = @this.IndexOf(before);
            int startIndex = beforeStartIndex + before.Length;
            int afterStartIndex = @this.IndexOf(after, startIndex);

            if (beforeStartIndex == -1 || afterStartIndex == -1)
            {
                return "";
            }

            return @this.Substring(startIndex, afterStartIndex - startIndex);
        }

        public static bool ContainsAllIgnoreCases(this string o, IEnumerable<string> values)
        {
            foreach (var item in values)
            {
                if (!o.ContainsIgnoreCases(item)) return false;
            }
            return true;
        }

        public static bool ContainsAllIgnoreCases(this string o, params string[] args)
            => o.ContainsAllIgnoreCases(args.ToList());

        public static bool ContainsAnyIgnoreCases(this string o, IEnumerable<string> values)
        {
            foreach (var item in values)
            {
                if (o.ContainsIgnoreCases(item)) return true;
            }
            return false;
        }

        public static bool ContainsAnyIgnoreCases(this string o, params string[] args) 
            => ContainsAnyIgnoreCases(o, args.ToList());

        public static string Remove(this string o, string value) => o.Replace(value, String.Empty);

        public static string RemoveAll(this string o, params string[] args)
        {
            string result = o;
            foreach (var item in args)
            {
                result = result.Remove(item);
            }
            return result;
        }

        public static string[] Split(this string o, params string[] args) 
            => o.Split(args, StringSplitOptions.RemoveEmptyEntries);

        public static T DeserializeBinary<T>(this string o)
        {
            using (var stream = new MemoryStream(Encoding.Default.GetBytes(o)))
            {
                var binaryRead = new BinaryFormatter();
                return (T) binaryRead.Deserialize(stream);
            }
        }

        public static T DeserializeBinary<T>(this string o, Encoding encoding)
        {
            using (var stream = new MemoryStream(encoding.GetBytes(o)))
            {
                var binaryRead = new BinaryFormatter();
                return (T) binaryRead.Deserialize(stream);
            }
        }

        public static string SerializeBinary<T>(this T o)
        {
            var binaryWrite = new BinaryFormatter();

            using (var memoryStream = new MemoryStream())
            {
                binaryWrite.Serialize(memoryStream, o);
                return Encoding.Default.GetString(memoryStream.ToArray());
            }
        }

        public static string SerializeBinary<T>(this T o, Encoding encoding)
        {
            var binaryWrite = new BinaryFormatter();

            using (var memoryStream = new MemoryStream())
            {
                binaryWrite.Serialize(memoryStream, o);
                return encoding.GetString(memoryStream.ToArray());
            }
        }

        public static NOptional<TEnum> TryParseEnum<TEnum>(this string o, bool ignoreCase) where TEnum : struct
            => EnumUtil.TryParse<TEnum>(o, ignoreCase);

        public static NOptional<TEnum> TryParseEnum<TEnum>(this string o) where TEnum : struct
            => EnumUtil.TryParse<TEnum>(o);
    }
}