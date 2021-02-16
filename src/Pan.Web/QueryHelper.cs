using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pan.Web
{
    public static class QueryHelper
    {
        public static string ToQueryString(this object obj, Func<object, string> convertor = default,
            string separator = default)
        {
            return obj.ToCustomQueryString(convertor, separator, default);
        }

        public static string ToEncodeQueryString(this object obj, Func<object, string> convertor = default,
            string separator = default)
        {
            return obj.ToCustomQueryString(convertor, separator, HttpUtility.UrlEncode);
        }

        public static string ToDateTimeFormattedQueryString<T>(this T data,
            string dateTimeFormatString = "yyyy-MM-dd HH:mm:ss") where T : class
        {
            return data.ToQueryString(x =>
            {
                return x switch
                {
                    DateTime d => d.ToString(dateTimeFormatString),
                    _ => default
                };
            });
        }

        public static string ToCollectionQueryString<T>(this IEnumerable<T> source, string name,
            string separator = default)
        {
            var query = source.ToCollectionQueryString(name, default, separator, default);
            return string.IsNullOrEmpty(query) ? string.Empty : $"?{query}";
        }

        private static string ToCollectionQueryString(this IEnumerable source, string name,
            Func<object, string> convertor, string separator, Func<string, string> encoder)
        {
            var collection = source.Cast<object>().ToList();
            return collection.Count <= 0
                ? string.Empty
                : $"{name}={string.Join(!string.IsNullOrEmpty(separator) ? separator : "&", from object v in collection select encoder != default ? encoder(v.ConvertValueToString(convertor)) : v.ConvertValueToString(convertor))}";
        }

        public static string ToCustomQueryString(this object obj, Func<object, string> convertor, string separator,
            Func<string, string> encoder)
        {
            switch (obj)
            {
                case null:
                    return string.Empty;
                case string str:
                    return str;
                default:
                    var result = new List<string>();
                    var props = obj.GetType().GetProperties().Where(p => p.GetValue(obj, null) != null);
                    foreach (var p in props)
                    {
                        var value = p.GetValue(obj, null);
                        if (value is IEnumerable enumerable && !(value is string))
                            result.Add(enumerable.ToCollectionQueryString(p.Name, convertor, separator, encoder));
                        else
                            result.Add(
                                $"{p.Name}={(encoder != default ? encoder(value.ConvertValueToString(convertor)) : value.ConvertValueToString(convertor))}");
                    }

                    var query = string.Join("&", result.Where(x => !string.IsNullOrEmpty(x)).ToArray());
                    return string.IsNullOrEmpty(query) ? string.Empty : $"?{query}";
            }
        }

        private static string ConvertValueToString<TSource>(this TSource parameter, Func<TSource, string> convertor)
        {
            if (parameter == null) return string.Empty;

            string result = default;
            if (convertor != default) result = convertor(parameter);

            if (!string.IsNullOrEmpty(result))
                return result;
            return parameter switch
            {
                Enum e => Convert.ChangeType(parameter, e.GetType().GetEnumUnderlyingType()).ToString(),
                _ => parameter.ToString()
            };
        }
    }
}