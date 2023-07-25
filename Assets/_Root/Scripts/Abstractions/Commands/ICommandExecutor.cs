using System.Threading;

namespace Abstractions.Commands
{
    public interface ICommandExecutor
    {
        void ExecuteCommand(object command);
    }
}
