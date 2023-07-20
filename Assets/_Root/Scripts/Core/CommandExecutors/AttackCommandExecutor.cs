using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackComand>
    {
        public override void ExecuteSpecificCommand(IAttackComand command)
        {
            Debug.Log($"{command.GetType()}");
        }
    }
}
