using System;
using Utils;

namespace Abstractions
{
    public abstract class ValueBase<T> : IAwaitable<T>
    {
        public class NewValueNotifier<TAwaited> : IAwaiter<TAwaited>
        {
            private readonly ValueBase<TAwaited> _valueBase;
            private TAwaited _result;
            private Action _continuation;
            private bool _isCompleted;

            public NewValueNotifier(ValueBase<TAwaited> scriptableObjectValueBase)
            {
                _valueBase = scriptableObjectValueBase;
                _valueBase.OnNewValue += onNewValue;
            }

            private void onNewValue(TAwaited obj)
            {
                _valueBase.OnNewValue -= onNewValue;
                _result = obj;
                _isCompleted = true;
                _continuation?.Invoke();
            }

            public void OnCompleted(Action continuation)
            {
                if (_isCompleted)
                    continuation?.Invoke();
                else
                    _continuation = continuation;
            }

            public bool IsCompleted => _isCompleted;
            public TAwaited GetResult() => _result;
        }

        public T CurrentValue { get; private set; }

        public event Action<T> OnNewValue;

        public void SetValue(T value)
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