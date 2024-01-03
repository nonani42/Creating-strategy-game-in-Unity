using System;
using Utils;

namespace Abstractions
{
    public abstract class ValueBase<T> : IAwaitable<T>
    {
        public class NewValueNotifier<TAwaited> : AwaiterBase<TAwaited>
        {
            protected readonly ValueBase<TAwaited> _valueBase;

            public NewValueNotifier(ValueBase<TAwaited> valueBase)
            {
                _valueBase = valueBase;
                _valueBase.OnNewValue += OnChangeValue;
            }

            public void OnChangeValue(TAwaited obj)
            {
                _valueBase.OnNewValue -= OnChangeValue;
                OnNewValue(obj);
            }
        }
            
        public T CurrentValue { get; private set; }

        public event Action<T> OnNewValue;

        public virtual void SetValue(T value)
        {
            CurrentValue = value;
            OnNewValue?.Invoke(value);
        }

        public IAwaiter<T> GetAwaiter()
        {
            return new NewValueNotifier<T>(this);
        }
    }
}