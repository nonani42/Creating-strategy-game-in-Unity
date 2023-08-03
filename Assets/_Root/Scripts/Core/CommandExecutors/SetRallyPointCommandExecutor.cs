using Abstractions.Commands.CommandsInterfaces;
using Abstractions.Commands;
using System.Threading.Tasks;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class SetRallyPointCommandExecutor : CommandExecutorBase<ISetRallyPointCommand>
    {
        public override async Task ExecuteSpecificCommand(ISetRallyPointCommand command)
        {
            GetComponent<MainBuilding>().RallyPoint = command.RallyPoint;
            Debug.Log($"{command.RallyPoint}");
        }
    }
}
