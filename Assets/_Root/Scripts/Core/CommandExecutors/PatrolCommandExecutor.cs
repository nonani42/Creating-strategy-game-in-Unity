using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;


        public override void ExecuteSpecificCommand(IPatrolCommand command)
        {
        }
    }
}
