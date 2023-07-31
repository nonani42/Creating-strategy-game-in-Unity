using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading;

namespace Core.CommandExecutors
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public CancellationTokenSource MoveCancellationTokenSource { get; set; }
        public CancellationTokenSource PatrolCancellationTokenSource { get; set; }

        public override void ExecuteSpecificCommand(IStopCommand command)
        {
            MoveCancellationTokenSource?.Cancel();
            PatrolCancellationTokenSource?.Cancel();
        }
    }
}
