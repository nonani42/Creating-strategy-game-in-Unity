using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;

        public override async Task ExecuteSpecificCommand(IAttackCommand command)
        {
            Debug.Log($"{name} attacks {command.Target}");
        }
    }
}
