using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.Extensions.CollectionExtentions
{
    public static partial class CollectionExtensions
    {
        public static Random SystemRandom { get; private set; }

        static CollectionExtensions() => SystemRandom = new Random();

        /// <summary>
        /// Создает массив содержащий эл-ты текущего и нового массива.
        /// (Новый массив добавляется в конец).
        /// </summary>
        public static T[] AddArray<T>(this T[] self, T[] array)
        {
            if (array.IsNullOrEmpty()) return self;

            T[] res = new T[self.Length + array.Length];

            Array.Copy(self, res, self.Length);
            Array.Copy(array, 0, res, self.Length, array.Length);

            return res;
        }

        /// <summary>
        /// Является ли коллекция <see cref="null"/> или не содержит эл-ов.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection) => collection == null || !collection.Any();

        /// <summary>
        /// Берет элемент из словаря, если он присутствует или же создает и добавляет эл-т использу <paramref name="constructor"/> и возвращает эл-т.
        /// </summary>
        /// <param name="source">Источник.</param>
        /// <param name="key">Ключ поиска.</param>
        /// <param name="constructor">Метод создания нового эл-та.</param>
        public static TValue GetValueOrCreate<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, Func<TKey, TValue> constructor) where TValue : class
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (constructor == null)
            {
                throw new ArgumentNullException(nameof(constructor), $"Метод создания элемента для ключа '{key}' равен null.");
            }

            if (source.TryGetValue(key, out var value))
            {
                return value;
            }

            return source[key] = constructor(key);
        }

        /// <summary>
        /// Переставляет объекты с индексами <i>i</i> и <i>j</i> местами.
        /// </summary>
        public static IList<T> Swap<T>(this IList<T> list, int i, int j)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }
            
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;

            return list;
        }

        public static int FindIndexOf<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            if (items == null) throw new ArgumentNullException(nameof(items));
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            var index = 0;
            foreach (var item in items)
            {
                if (predicate(item)) return index;
                index++;
            }

            return -1;
        }

        public static int IndexOf<T>(this IEnumerable<T> items, T item) => items.FindIndexOf(i => EqualityComparer<T>.Default.Equals(item, i));
    }
}
