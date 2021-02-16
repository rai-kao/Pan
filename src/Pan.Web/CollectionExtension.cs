using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Pan.Web
{
    public static class CollectionExtension
    {
        public static int Count(this IEnumerable source)
        {
            if (source.Equals(default)) return 0;

            var collection = source.Cast<object>().ToList();
            return collection.Aggregate(0, (current, t) => current + 1);
        }

        public static int Count<T>(this IEnumerable<T> source)
        {
            if (source.Equals(default)) return 0;

            if (source is ICollection<T> collection) return collection.Count;

            return source.Aggregate(0, (current, t) => current + 1);
        }
    }
}