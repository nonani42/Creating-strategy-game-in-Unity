using System;
using System.Threading;
using Utils;
using Zenject;

namespace Abstractions.Commands
{
    public abstract class CancellableCommandCreatorBase<TCommand, TArgument> : CommandCreatorBase<TCommand> where TCommand : ICommand
    {
        [Inject] private AssetsContext _context;
        [Inject] private ValueBase<TArgument> _awaitableArgument;

        private CancellationTokenSource _ctSource;

        protected override sealed async void ClassSpecificCommandCreation(Action<TCommand> creationCallback)
        {
            _ctSource = new CancellationTokenSource();

            try
            {
                var argument = await _awaitableArgument.WithCancellation(_ctSource.Token);
                creationCallback?.Invoke(_context.Inject(СreateCommand(argument)));
            }

            catch { }
        }

        protected abstract TCommand СreateCommand(TArgument argument);

        public override void ProcessCancel()
        {
            base.ProcessCancel();
            if (_ctSource != null)
            {
                _ctSource.Cancel();
                _ctSource.Dispose();
                _ctSource = null;
            }
        }
    }
}
