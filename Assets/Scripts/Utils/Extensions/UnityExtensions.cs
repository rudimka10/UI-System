using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Utils.Extensions
{
    public static class UnityExtensions
    {
        /// <summary>
        /// Проверка чтоб обьект не был такого же состояния.
        /// </summary>
        public static void SetActiveSafe(this GameObject self, bool state)
        {
            if (state != self.activeSelf) self.SetActive(state);
        }

        /// <summary>
        /// Обращается к gameObject и устанавливает его активность.
        /// </summary>
        public static void SetActive<T>(this T target, bool isActive) where T : Component
        {
            target.gameObject.SetActiveSafe(isActive);
        }

        public static void SetActive<T>(this T[] components, bool isActive) where T : Component
        {
            for (int i = 0; i < components.Length; i++)
            {
                components[i].SetActive(isActive);
            }
        }

        /// <summary>
        /// Уничтожает все дочерние объекты.
        /// </summary>
        /// <param name="transform"></param>
        public static void DestroyAllChildren(this Transform transform)
        {
            if (!transform) throw new ArgumentNullException(nameof(transform));

            var array = new GameObject[transform.childCount];

            for (var i = 0; i < array.Length; i++)
            {
                array[i] = transform.GetChild(i).gameObject;
            }

            foreach (var target in array)
            {
                Object.Destroy(target);
            }
        }

        /// <summary>
        /// Вращение вектора по оси Ox на заданный угол.
        /// </summary>
        /// <param name="vector">Вектор для вращения.</param>
        /// <param name="angleRad">Угол в радианах.</param>
        /// <returns></returns>
        public static Vector3 RotateX(this Vector3 vector, float angleRad)
        {
            return new Vector3
            {
                x = vector.x,
                y = vector.y * Mathf.Cos(angleRad) + vector.z * Mathf.Sin(angleRad),
                z = -vector.y * Mathf.Sin(angleRad) + vector.z * Mathf.Cos(angleRad)
            };
        }

        /// <summary>
        /// Вращение вектора по оси Ox на заданный угол.
        /// </summary>
        /// <param name="vector">Вектор для вращения.</param>
        /// <param name="angleRad">Угол в радианах.</param>
        /// <returns></returns>
        public static Vector3 RotateY(this Vector3 vector, float angleRad)
        {
            return new Vector3
            {
                x = vector.x * Mathf.Cos(angleRad) + vector.z * Mathf.Sin(angleRad),
                y = vector.y,
                z = -vector.x * Mathf.Sin(angleRad) + vector.z * Mathf.Cos(angleRad)
            };
        }

        /// <summary>
        /// Вращение вектора по оси Oz на заданный угол.
        /// </summary>
        /// <param name="vector">Вектор для вращения.</param>
        /// <param name="angleRad">Угол в радианах.</param>
        /// <returns></returns>
        public static Vector3 RotateZ(this Vector3 vector, float angleRad)
        {
            return new Vector3
            {
                x = vector.x * Mathf.Cos(angleRad) - vector.y * Mathf.Sin(angleRad),
                y = -vector.x * Mathf.Sin(angleRad) + vector.y * Mathf.Cos(angleRad),
                z = vector.z
            };
        }

        /// <summary>
        /// Возвращает заданный цвет, но с заданной альфой.
        /// </summary>
        public static Color ChangeAlpha(this Color c, float alpha) => new Color(c.r, c.g, c.b, alpha);

        /// <summary>
        /// Устанавливает альфу для компонента графики.
        /// </summary>
        /// <param name="graphic">Компонент отображения.</param>
        /// <param name="alpha">Требуемая альфа.</param>
        /// <exception cref="ArgumentNullException"><paramref name="graphic"/> - графика имела значение null.</exception>
        public static TGraphic SetAlpha<TGraphic>(this TGraphic graphic, float alpha) where TGraphic : Graphic
        {
            if (graphic == null)
            {
                throw new ArgumentNullException(nameof(graphic));
            }

            graphic.color = graphic.color.ChangeAlpha(alpha);

            return graphic;
        }

        /// <summary>
        /// Изменяет размер тектсуры с использованием билинейной фильтрации.
        /// <para/>
        /// </summary>
        /// <param name="source">Исходная текстура.</param>
        /// <param name="targetWidth">Новая ширина.</param>
        /// <param name="targetHeight">Новая высота.</param>
        /// <returns>Текстура с новым размером.</returns>
        public static Texture2D ScaleTexture(this Texture2D source, int targetWidth, int targetHeight)
        {
            // Источник: http://jon-martin.com/?p=114

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (targetWidth < 1 || targetHeight < 1)
            {
                throw new ArgumentException($"Недопустимые размеры: {targetWidth} * {targetHeight}");
            }

            var result = new Texture2D(targetWidth, targetHeight, source.format, true);

            var pixels = result.GetPixels(0);

            var incX = 1.0f / targetWidth;
            var incY = 1.0f / targetHeight;

            for (var px = 0; px < pixels.Length; px++)
            {
                pixels[px] = source.GetPixelBilinear(incX * ((float) px % targetWidth), incY * Mathf.Floor(px / targetWidth));
            }

            result.SetPixels(pixels, 0);
            result.Apply();

            return result;
        }
    }
}
