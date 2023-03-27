using System;
using System.Collections.Generic;

namespace Utils.Extensions.CollectionExtentions
{
    public static partial class CollectionExtensions
    {
        /// <summary>
        /// Вызов события для каждого элемента коллекции.
        /// </summary>
        public static IEnumerable<T> CallForEach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            foreach (var item in collection)
            {
                action.Invoke(item);
            }

            return collection;
        }
    }
}
