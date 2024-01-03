using System;
using UniRx;

namespace Abstractions
{
    public abstract class StatefulValueBase<T> : ValueBase<T>, IObservable<T>
    {
        private ReactiveProperty<T> _innerDataSource = new ReactiveProperty<T>();

        public override void SetValue(T value)
        {
            base.SetValue(value);
            _innerDataSource.Value = value;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return _innerDataSource.Subscribe(observer);
        }
    }
}
