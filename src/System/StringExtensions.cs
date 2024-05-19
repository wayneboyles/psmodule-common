using Boyles.PowerShell.Common;

namespace System
{
    public static class StringExtensions
    {
        public static string EndsWith(this string str, char c, StringComparison comparisonType = StringComparison.Ordinal)
        {
            Ensure.NotNull(str, nameof(str));

            if (str.EndsWith(c.ToString(), comparisonType))
            {
                return str;
            }

            return str + c;
        }

        public static string StartsWith(this string str, char c, StringComparison comparisonType = StringComparison.Ordinal)
        {
            Ensure.NotNull(str, nameof(str));

            if (str.StartsWith(c.ToString(), comparisonType))
            {
                return str;
            }

            return c + str;
        }

        public static bool IsNullOrEmpty(this string? str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhiteSpace(this string? str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        public static string[] Split(this string str, string separator)
        {
            return str.Split(new[] { separator }, StringSplitOptions.None);
        }

        public static string[] Split(this string str, string separator, StringSplitOptions options)
        {
            return str.Split(new[] { separator }, options);
        }

        public static string[] SplitToLines(this string str)
        {
            return str.Split(Environment.NewLine);
        }

        public static string[] SplitToLines(this string str, StringSplitOptions options)
        {
            return str.Split(Environment.NewLine, options);
        }

        public static T ToEnum<T>(this string value)
            where T : struct
        {
            Ensure.NotNull(value, nameof(value));
            return (T)Enum.Parse(typeof(T), value);
        }

        public static T ToEnum<T>(this string value, bool ignoreCase)
            where T : struct
        {
            Ensure.NotNull(value, nameof(value));
            return (T)Enum.Parse(typeof(T), value, ignoreCase);
        }
    }
}
