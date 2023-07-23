using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using Zenject;

namespace Core.CommandExecutors
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<IProduceUnitCommand>
    {
        [Inject(Id = "Units")] private Transform _parent;
        
        public override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            Instantiate(command.UnitPrefab, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity, _parent);
        }
    }
}
