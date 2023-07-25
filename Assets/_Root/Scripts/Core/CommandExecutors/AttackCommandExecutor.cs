using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;

        public override void ExecuteSpecificCommand(IAttackCommand command)
        {
            Debug.Log($"{name} attacks {command.Target}");
        }
    }
}
