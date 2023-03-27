using System;
using System.Collections.Generic;
using System.Linq;
using Random = UnityEngine.Random;

namespace Utils
{
    public static class Extentions
    {
        /// <summary>
        /// ������� ��������� ����� ��� ������� � ���������� ��������� �������.
        /// ���� ������ ������ - ������� default
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        public static T GetRandomElement<T>(this IEnumerable<T> array)
        {
            if (array.Count() == 0)
            {
                return default;
            }

            var index = Random.Range(0, array.Count());
            return array.ToArray()[index];
        }

        /// <summary>
        /// ������� ��������� ����� ��� ������� � ���������� List ��������� ���������.
        /// ���� ������ ������ - ������� default.
        /// ���� �������� ������ ��������� > ����� ��������� - ������� ����������������� ������.
        /// </summary>
        /// <param name="array">�������� ������</param>
        /// <param name="countOfElements">������ ���������� ���������</param>
        /// <returns></returns>
        public static List<T> GetListOfRandomElements<T>(this IEnumerable<T> array, int countOfElements)
        {
            if (array.Count() == 0)
            {
                return default;
            }

            if (array.Count() < countOfElements)
            {
                countOfElements = array.Count();
            }

            var newArray = new List<T>();
            var tempArray = new List<T>();
            tempArray.AddRange(array);
            for (int i = 0; i < countOfElements; i++)
            {
                var index = Random.Range(0, tempArray.Count());
                newArray.Add(tempArray[index]);
                tempArray.RemoveAt(index);

            }

            return newArray;
        }


        /// <summary>
        /// ������� ��������� �������� �������� � ������� �� �����.
        /// ���� ���� ��� ���������� � ������� - ��������� ��� ��������.
        /// ���� ����� �� ���������� - ������ � ����� ��������.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="target">�������� �������</param>
        /// <param name="key">����</param>
        /// <param name="amount">�����, �� ������� ����� �������� �������� �����</param>
        /// <returns></returns>
        public static Dictionary<TKey, double> AddOrCreate<TKey>(this Dictionary<TKey, double> target, TKey key, double amount) where TKey : Enum
        {
            if (target.ContainsKey(key) == false)
                target.Add(key, amount);
            else
                target[key] += amount;

            return target;
        }


        /// <summary>
        /// ���������� ��� ������� �� additions � target.
        /// </summary>
        /// <param name="target">�������, � ������� ����� �������� ���������</param>
        /// <param name="additions">�������������� �������</param>
        public static Dictionary<TKey, double> Merge<TKey>(this Dictionary<TKey, double> target, params Dictionary<TKey, double>[] additions)
        {
            foreach (var addition in additions)
            {
                foreach (var pair in addition)
                {
                    if (target.ContainsKey(pair.Key))
                        target[pair.Key] += pair.Value;
                    else
                        target.Add(pair.Key, pair.Value);
                }
            }

            return target;
        }

        /// <summary>
        /// ������� �������� ������� � ���� �����.
        /// </summary>
        /// <param name="additions">������� ��� ������</param>
        public static Dictionary<TKey, double> MergeIntoNew<TKey>(params Dictionary<TKey, double>[] additions)
        {
            var result = new Dictionary<TKey, double>();
            foreach (var item in additions)
            {
                result.Merge(item);
            }
            return result;
        }

        public static double Clamp(this double value, double min, double max)
        {
            return value < min
                ? min
                : value > max
                    ? max
                    : value;
        }

        public static double ClampMin(this double value, double min)
        {
            return value < min
                ? min
                : value;
        }

        public static bool InStandartBetsValue(this int value)
        {
            int[] standartValues = { 10, 20, 50, 100, 200, 500 };
            return standartValues.Contains(value);
        }
        
    }
}