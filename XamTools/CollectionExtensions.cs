using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace XamTools
{
    public static class CollectionExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            foreach (var item in items)
                action(item);
        }

        public static ObservableCollection<T> ToObservable<T>(this IList list)
        {
            var collection = new ObservableCollection<T>();
            for (var x = 0; x < list.Count; x++)
                collection.Add((T) list[x]);

            return collection;
        }

        public static ObservableCollection<T> ToObservable<T>(this IQueryable<T> query)
        {
            var collection = new ObservableCollection<T>();
            foreach (var item in query.AsEnumerable())
                collection.Add(item);

            return collection;
        }

        public static ObservableCollection<T> ToObservable<T>(this T[] array)
        {
            var collection = new ObservableCollection<T>();
            foreach (var item in array)
                collection.Add(item);

            return collection;
        }


        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> items) => new ObservableCollection<T>(items);

        public static bool Compare<T>(this ICollection<T> collection, ICollection<T> other, bool sameOrderRequired = false)
        {
            if (ReferenceEquals(collection, other))
                return true;

            if (other == null)
                throw new ArgumentNullException(nameof(other));

            // Not the same number of elements.  No match
            if (collection.Count != other.Count)
                return false;

            // Require same-order; just defer to existing LINQ match
            if (sameOrderRequired)
                return collection.SequenceEqual(other);

            // Otherwise allow it to be any order, but require same count of each item type.
            var comparer = EqualityComparer<T>.Default;
            return !(from item in collection
                     let thisItem = item
                     where !other.Contains(item, comparer) || (collection.Count(check => comparer.Equals(thisItem, check)) !=
                                                               other.Count(check => comparer.Equals(thisItem, check)))
                     select item).Any();
        }
    }
}
