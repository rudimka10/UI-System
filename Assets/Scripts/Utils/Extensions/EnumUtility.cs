using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.Extensions
{
    public static class EnumUtility
    {
        // Базовая версия кода взята с одного из ответов:
        // https://stackoverflow.com/questions/1339976/how-to-check-if-any-flags-of-a-flag-combination-are-set

        public static IEnumerable<T> GetValues<T>() where T : Enum
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        private static void CheckIsEnum<T>(bool withFlags) where T : Enum
        {
            if (withFlags && !Attribute.IsDefined(typeof(T), typeof(FlagsAttribute)))
                throw new ArgumentException(string.Format("Type '{0}' doesn't have the 'Flags' attribute", typeof(T).FullName));
        }

        /// <summary>
        /// Содержит ли <paramref name="value"/> значение <paramref name="flag"/>.
        /// </summary>
        public static bool IsFlagSet<T>(this T value, T flag) where T : Enum
        {
            CheckIsEnum<T>(true);
            long lValue = Convert.ToInt64(value);
            long lFlag = Convert.ToInt64(flag);
            return (lValue & lFlag) != 0;
        }

        public static IEnumerable<T> GetFlags<T>(this T value) where T : Enum
        {
            CheckIsEnum<T>(true);
            foreach (T flag in Enum.GetValues(typeof(T)).Cast<T>())
            {
                if (value.IsFlagSet(flag))
                    yield return flag;
            }
        }

        /// <summary>
        /// Изменить флаг.
        /// </summary>
        /// <param name="value">Текущее значение.</param>
        /// <param name="flags">Целевое значение.</param>
        /// <param name="on">Добавлять или убирать <paramref name="flags"/></param>
        /// <returns>Измененное значение учитывая <paramref name="flags"/> и <paramref name="on"/>.</returns>
        public static T SetFlags<T>(this T value, T flags, bool on) where T : Enum
        {
            CheckIsEnum<T>(true);
            long lValue = Convert.ToInt64(value);
            long lFlag = Convert.ToInt64(flags);
            if (on)
            {
                lValue |= lFlag;
            }
            else
            {
                lValue &= ~lFlag;
            }

            return (T) Enum.ToObject(typeof(T), lValue);
        }

        /// <summary>
        /// Добавить <paramref name="flags"/> к текущему значению.
        /// <para/>
        /// Вызов <see cref="SetFlags{T}(T, T, bool)"/> с on == true.
        /// </summary>
        /// <param name="value">Текущее значение.</param>
        /// <param name="flags">Новые флаги.</param>
        /// <returns>Измененное значение.</returns>
        public static T SetFlags<T>(this T value, T flags) where T : Enum
        {
            return value.SetFlags(flags, true);
        }

        /// <summary>
        /// Убирает <paramref name="flags"/> с текущего значения.
        /// <para/>
        /// Вызов <see cref="SetFlags{T}(T, T, bool)"/> с on == false.
        /// </summary>
        /// <param name="value">Текущее значение.</param>
        /// <param name="flags">Флаги, которые необходимо убрать.</param>
        /// <returns>Измененное значение.</returns>
        public static T ClearFlags<T>(this T value, T flags) where T : Enum
        {
            return value.SetFlags(flags, false);
        }

        /// <summary>
        /// Комбинирует флаги.
        /// </summary>
        /// <param name="flags">Флаги для комбинирования.</param>
        /// <returns>Флаг, содержащий все значения в <paramref name="flags"/>.</returns>
        public static T CombineFlags<T>(this IEnumerable<T> flags) where T : Enum
        {
            CheckIsEnum<T>(true);
            long lValue = 0;
            foreach (T flag in flags)
            {
                long lFlag = Convert.ToInt64(flag);
                lValue |= lFlag;
            }

            return (T) Enum.ToObject(typeof(T), lValue);
        }
    }
}
