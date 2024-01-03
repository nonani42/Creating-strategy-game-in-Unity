
using Abstractions.Commands;

namespace Abstractions
{
    public interface ICommandsQueue
    {
        ICommand CurrentCommand { get; }
        void EnqueueCommand(object command);
        void Clear();
    }
}
