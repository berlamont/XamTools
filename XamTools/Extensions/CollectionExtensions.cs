using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using eoTouchx.Helpers;

namespace eoTouchx.Extensions
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

        public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> items) =>
            new ObservableCollection<T>(items);

  

        public static void AddRange<T>(this ObservableCollection<T> collection, IEnumerable<T> items)
        {
            foreach (var i in items)
                collection.Add(i);
        }

        public static void AddRange<T>(this IList<T> collection, IEnumerable<T> items)
        {
            foreach (var i in items)
                collection.Add(i);
        }

        /// <summary>
        /// Adds a range of IEnumerable of T to an ObservableCollectionGrouping grouped by a propertyName of type K.
        /// </summary>
        public static void AddRange<T,K>(this ObservableCollection<Grouping<T,K>> collection, IEnumerable<T> items, string propertyName)
        {
            if (typeof(T).GetRuntimeProperties().All(propertyInfo => propertyInfo.Name != propertyName))
                throw new ArgumentException($"Type '{typeof(T).Name}' does not have a property named '{propertyName}'");

            var groupings = items.GroupBy(t => t.GetType().GetRuntimeProperties().Single(propertyInfo => propertyInfo.Name == propertyName).GetValue(t, null));

            collection.AddRange(groupings.Select(grouping => new Grouping<T,K>(grouping, (K)grouping.Key)));
        }
    

         public static TModel FirstOrNew<TModel>(this List<TModel> collection, Func<TModel, bool> predicate = null)
        {
            var obj = collection.FirstOrDefault(predicate);
            if (obj == null)
                return Activator.CreateInstance<TModel>();

            return obj;
        }

        public static TModel FirstOrNew<TModel>(this IEnumerable<TModel> collection, Func<TModel, bool> predicate = null)
        {
            var obj = collection.FirstOrDefault(predicate);
            if (obj == null)
                return Activator.CreateInstance<TModel>();

            return obj;
        }

        public static List<TModel> AddOnEmpty<TModel>(this List<TModel> collection)
        {
            if (collection.Count().Equals(0))
                collection.Add(Activator.CreateInstance<TModel>());

            return collection;
        }

        public static void AddIfNotContains<T>(this IList<T> collection, T value)
        {
            if ((value != null) && !collection.Contains(value))
                collection.Add(value);
        }

        public static void AddRangeIfNotContains<T>(this IList<T> collection, IEnumerable<T> values)
        {
            if (values == null)
                return;

            foreach (var value in values)
                collection.AddIfNotContains(value);
        }

    }
}