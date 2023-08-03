using Abstractions.Commands.CommandsInterfaces;
using Abstractions.Commands;
using UnityEngine;
using UserControlSystem.CommandRealizations;

namespace UserControlSystem
{
    public class SetRallyPointCommandCreator : CancellableCommandCreatorBase<ISetRallyPointCommand, Vector3>
    {
        protected override ISetRallyPointCommand CreateCommand(Vector3 argument) => new SetRallyPointCommand(argument);
    }
}
