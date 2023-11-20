using Abstractions.Commands.CommandsInterfaces;
using Abstractions.Commands;
using UnityEngine;
using Zenject;
using Abstractions;

namespace Core.CommandExecutors
{
    public class MainBuildingCommandQueue : MonoBehaviour, ICommandsQueue
    {
        [Inject] CommandExecutorBase<IProduceUnitCommand> _produceUnitCommandExecutor;
        [Inject] CommandExecutorBase<ISetRallyPointCommand> _setRallyCommandExecutor;

        public ICommand CurrentCommand => default;

        public void Clear() { }

        public async void EnqueueCommand(object command)
        {
            await _produceUnitCommandExecutor.TryExecuteCommand(command);
            await _setRallyCommandExecutor.TryExecuteCommand(command);
        }
    }
}
