using System;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Utils.Extensions
{
    public static class FormattingExtensions
    {
        private static readonly char[] DIGIT_SYMBOLS = {'K', 'M', 'G', 'T', 'P'};

        #region Base 64

        public static string ToBase64(this string s) => string.IsNullOrEmpty(s) ? "" : Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(s));

        public static string FromBase64(this string s) => s.IsBase64String() ? System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(s)) : s;

        public static bool IsBase64String(this string s)
        {
            s = s.Trim();
            return s.Length % 4 == 0 && Regex.IsMatch(s, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        #endregion

        /// <summary>
        /// Оборачивает текущую строку в соответсвующий тэг.
        /// </summary>
        public static string TagWrap(this string target, string tagValue) => $"<{tagValue}>{target}</{tagValue}>";

        /// <summary>
        /// Оборачивает строку в тэг заданого цвета.
        /// </summary>
        public static string Paint(this string target, string hex) => $"<color=#{hex}>{target}</color>";

        /// <summary>
        /// Оборачивает строку в тэг заданого цвета.
        /// </summary>
        public static string Paint(this string target, Color color) => target.Paint(ColorUtility.ToHtmlStringRGBA(color));

        public static string ToShortPercent(this float value, bool isSigned = false, int fractionLimit = 2) => ToShortPercent((double) value, isSigned, fractionLimit);
        
        public static string ToShortPercent(this double value, bool isSigned = false, int fractionLimit = 2)
        {
            var sign = Math.Sign(value);
            var percentValue = Math.Abs(value * 100);
            var integralValue = (int) Math.Truncate(percentValue);
            var fractionValue = percentValue - integralValue;
            fractionValue = (int) Math.Truncate(Math.Round(fractionValue * (fractionLimit == 0 ? 0 : Math.Pow(10, fractionLimit))));
            while (fractionValue > 0 && fractionValue % 10 == 0)
                fractionValue /= 10;

            var integralValueString = integralValue.ToSeparatedString();
            var fractionValueString = fractionValue > 0
                ? $",{fractionValue.ToString()}"
                : "";
            var signString = sign > 0 ? "+" : "-";
            return $"{(isSigned ? signString : "")}{integralValueString}{fractionValueString}%";
        }
        
        /// <summary>
        /// Возвращает сокращенное строковое представление заданого числа с применением множителей (K, M, G, T, P).
        /// Не более 3 чисел в сумме до и после разделителя, с отбрасыванием нулей
        /// Например: 12.3К, 5К, 500К
        /// Но никогда: 12.31К, 5.00К, 500.69К
        /// </summary>
        public static string ToShortNumberV2(this int value, bool isSigned = false)
        {
            var signed = value < 0 ? "-" : "+";
            var unsigned = value < 0 ? "-" : "";

            var firstThousand = 0;
            var secondThousand = 0;
            var amount = 0;
            
            var valueCopy = Math.Abs(value);
            while (valueCopy > 0)
            {
                secondThousand = firstThousand;
                firstThousand = valueCopy % 1_000;
                valueCopy /= 1_000;
                amount++;
            }

            var letter = amount - 2 >= 0
                ? DIGIT_SYMBOLS[amount - 2].ToString()
                : "";
            
            var firstThousandString = firstThousand.ToString();
            var secondThousandString = secondThousand.ToString();
            var leadingZeroesToAdd = secondThousandString.Length == 1
                ? "00"
                : secondThousandString.Length == 2
                    ? "0"
                    : "";
            secondThousandString = secondThousandString.Insert(0, leadingZeroesToAdd);
            
            var amountToTake = Math.Min(3 - firstThousandString.Length, secondThousandString.Length);
            secondThousandString = amountToTake - leadingZeroesToAdd.Length > 0 && secondThousand > 0
                ? secondThousandString.Substring(0, amountToTake)
                : "";
            
            return string.IsNullOrEmpty(secondThousandString)
                ? $"{(isSigned ? signed : unsigned)}{firstThousandString}{letter}"
                : $"{(isSigned ? signed : unsigned)}{firstThousandString}.{secondThousandString}{letter}";
        }
        
        /// <summary>
        /// Возвращает сокращенное строковое представление заданого числа с применением множителей (K, M, G, T, P).
        /// </summary>
        public static string ToShortNumber(this int value)
        {
            var sign = value < 0 ? "-" : "";
            var val = Mathf.Abs(value).ToString();

            var rest = val.Length % 3;

            //определяем текущий старший разряд
            var curDigit = val.Length / 3 + (rest == 0 ? 0 : 1);

            //все что меньше 1000
            if (curDigit < 2) return $"{sign}{val}";

            var buf = new System.Text.StringBuilder();

            //если остатка нет - то количество чисел старшего разряда 3, если есть - то остаток
            var digitSymbolsCount = rest == 0 ? 3 : rest;
            var index = 0;
            //отделяем первые цифры старшего разряда числа
            for (var i = digitSymbolsCount; i > 0; i--, index++)
            {
                buf.Append(val[index]);
            }

            buf.Append('.')
                .Append(val[index]) // С точностью до десятых
                .Append(DIGIT_SYMBOLS[curDigit - 2]);

            return $"{sign}{buf}";
        }

        /// <summary>
        /// Возвращает сокращенное строковое представление заданого числа с применением множителей (K, M, G, T, P).
        /// </summary>
        public static string ToShortNumber(this float value) => Mathf.Abs(value) > 1_000 ? ((int) value).ToShortNumber() : value.ToString("F");

        public static string ToShortNumberV2(this float value, bool isSigned = false, bool useInteger = false) =>
            Mathf.Abs(value) > 1_000 || useInteger
                ? ((int) value).ToShortNumberV2(isSigned)
                : isSigned
                    ? $"{(Mathf.Sign(value) < 0 ? "-" : "+")}{Mathf.Abs(value):F}"
                    : value.ToString("F");

        /// <summary>
        /// Возвращает сокращенное строковое представление заданого числа с применением множителей (K, M, G, T, P).
        /// </summary>
        public static string ToShortNumber(this double value) => Math.Abs(value) > 1_000 ? ((int) value).ToShortNumber() : value.ToString("F");
        public static string ToShortNumberV2(this double value, bool isSigned = false, bool useInteger = false) => 
            Math.Abs(value) > 1_000 || useInteger
                ? ((int) value).ToShortNumberV2(isSigned)
                : isSigned
                    ? $"{(Math.Sign(value) < 0 ? "-" : "+")}{Math.Abs(value):F}"
                    : value.ToString("F");

        /// <summary>
        /// Заменяет все вхождения тєга на указаное значение.
        /// </summary>
        public static string ReplaceTagValue(this string target, string symbol, object value) => target.Replace($"<{symbol}>", value.ToString());

        /// <summary>
        /// Разделяет разряды числа заданным символом.
        /// </summary>
        public static string ToSeparatedString(this int value, char separator = ' ')
        {
            var s = value.ToString();

            // Исключаем обработку строк с процентами
            if (s.Contains("%")) return s;

            var buff = new System.Text.StringBuilder();

            var symbolsCount = s.Length;

            for (var i = 0; i < symbolsCount; i++)
            {
                buff.Append(s[i]);

                // Если симовл не последний, и он является концом разряда
                if (i != symbolsCount - 1 && (symbolsCount - (i + 1)) % 3 == 0)
                    buff.Append(separator);
            }

            return buff.ToString();
        }

        /// <summary>
        /// Формирует строку вида "x+", если значение больше заданого максимума.
        /// Например, "99+".
        /// </summary>
        /// <param name="value">Реальное значение.</param>
        /// <param name="max">Визуальный максимум.</param>
        /// <returns></returns>
        public static string ToClampedString(this int value, int max) => value <= max ? value.ToString() : $"{max}+";

        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input[0].ToString().ToUpper() + input.Substring(1);
            }
        }

        public static string FirstCharToLower(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input));
                default: return input[0].ToString().ToLower() + input.Substring(1);
            }
        }
    }
}
