using Abstractions.Commands.CommandsInterfaces;
using System;
using UserControlSystem.CommandRealizations;
using Utils;
using Zenject;

namespace UserControlSystem
{
    public class StopCommandCommandCreator : CommandCreatorBase<IStopCommand>
    {
        [Inject] private AssetsContext _context;

        protected override void ClassSpecificCommandCreation(Action<IStopCommand> creationCallback)
        {
            creationCallback?.Invoke(_context.Inject(new StopCommand()));
        }
    }
}
