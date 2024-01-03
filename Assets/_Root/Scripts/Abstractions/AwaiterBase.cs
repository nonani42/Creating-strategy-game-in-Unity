using System;
using Utils;

namespace Abstractions
{
    public abstract class AwaiterBase<TAwaited> : IAwaiter<TAwaited>
    {
        protected Action _continuation;

        private TAwaited _result;

        protected bool _isCompleted;

        public bool IsCompleted => _isCompleted;

        public void OnNewValue(TAwaited obj)
        {
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

        public TAwaited GetResult() => _result;
    }
}
