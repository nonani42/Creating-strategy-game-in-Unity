using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System;
using UserControlSystem.CommandRealizations;
using Utils;
using Zenject;

namespace UserControlSystem
{
    public class ProduceUnitCommandCommandCreator : CommandCreatorBase<IProduceUnitCommand>
    {
        [Inject] private AssetsContext _context;
        [Inject] private DiContainer _diContainer;

        protected override void ClassSpecificCommandCreation(Action<IProduceUnitCommand> creationCallback)
        {
            var produceUnitCommand = _context.Inject(new ProduceUnitCommandHeir());
            _diContainer.Inject(produceUnitCommand);

            creationCallback?.Invoke(produceUnitCommand);
        }
    }
}
