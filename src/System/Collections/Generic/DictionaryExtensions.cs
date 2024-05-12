namespace System.Collections.Generic
{
    public static class DictionaryExtensions
    {
        public static void AddQueryString(this Dictionary<string, object> dictionary, string key, object value)
        {
            switch (value)
            {
                case null:
                case string when string.IsNullOrWhiteSpace(value.ToString()):
                    return;
            }

            if (!dictionary.ContainsKey(key))
            {
                dictionary.Add(key, value);
            }
        }
    }
}
