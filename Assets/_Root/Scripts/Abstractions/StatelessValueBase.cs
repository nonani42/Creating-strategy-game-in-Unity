using System;
using UniRx;

namespace Abstractions
{
    public abstract class StatelessValueBase<T> : ValueBase<T>, IObservable<T>
    {
        private Subject<T> _innerDataSource = new Subject<T>();

        public override void SetValue(T value)
        {
            base.SetValue(value);
            _innerDataSource.OnNext(value);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _innerDataSource.Subscribe(observer);
        }
    }
}
