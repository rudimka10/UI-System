using System;

namespace Utils
{
    public class ReactiveProperty<T>
    {
        /// <summary>
        /// Вызывается всякий раз, когда происходит запись значения в <see cref="Value"/>.
        /// </summary>
        public event Action ValueChanged;

        private T _value;

        /// <summary>
        /// Текущее значение.
        /// </summary>
        public T Value
        {
            get => _value;
            set
            {
                _value = value;
                ValueChanged?.Invoke();
            }
        }

        public ReactiveProperty()
        {
            _value = default;
        }

        public ReactiveProperty(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Принудительный вызов события <see cref="ValueChanged"/>.
        /// </summary>
        public void ForceFireValueChanged()
        {
            ValueChanged?.Invoke();
        }

        public static implicit operator T(ReactiveProperty<T> property)
        {
            if (property == null) throw new ArgumentNullException(nameof(property));
            return property.Value;
        }

        public override string ToString() => _value?.ToString() ?? "Null";
    }
}
