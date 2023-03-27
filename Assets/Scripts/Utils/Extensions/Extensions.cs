using System;

namespace Utils.Extensions
{
    public static class Extensions
    {
        public static bool ToBool(this int value) => value != 0;

        public static int ToInt(this bool value) => value ? 1 : 0;

        /// <summary>
        /// Проверяет принадлежность к промежутку [a;b].
        /// </summary>
        public static bool InRange<T>(this T x, T a, T b) where T : struct, IComparable, IFormattable, IConvertible, IComparable<T>, IEquatable<T>
        {
            return x.CompareTo(a) >= 0 && x.CompareTo(b) <= 0;
        }
    }
}
