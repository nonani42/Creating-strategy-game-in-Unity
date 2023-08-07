using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Core.CommandExecutors
{
    public class StopCommandExecutor : CommandExecutorBase<IStopCommand>
    {
        public CancellationTokenSource MoveCancellationTokenSource { get; set; }
        public CancellationTokenSource PatrolCancellationTokenSource { get; set; }
        public CancellationTokenSource AttackCancellationTokenSource { get; set; }

        public override async Task ExecuteSpecificCommand(IStopCommand command)
        {
            MoveCancellationTokenSource?.Cancel();
            PatrolCancellationTokenSource?.Cancel();
            AttackCancellationTokenSource?.Cancel();
        }
    }
}
