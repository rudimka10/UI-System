using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.Extensions.CollectionExtentions
{
    public static partial class CollectionExtensions
    {
        /// <summary>
        /// Произвольное переставление объектов в коллекции. [используя <see cref="System.Random"/>]
        /// </summary>
        /// <exception cref="ArgumentNullException"><paramref name="list"/> - входной список null.</exception>
        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            for (var i = list.Count - 1; i >= 1; i--)
            {
                var j = SystemRandom.Next(i + 1);
                list.Swap(i, j);
            }

            return list;
        }
    }
}
