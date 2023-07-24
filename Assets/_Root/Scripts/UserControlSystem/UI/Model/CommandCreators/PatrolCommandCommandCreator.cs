using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using UserControlSystem.CommandRealizations;
using Zenject;

namespace UserControlSystem
{
    public class PatrolCommandCommandCreator : CancellableCommandCreatorBase<IPatrolCommand, Vector3>
    {
        [Inject] private ValueBase<ISelectable> _selectable;

        protected override IPatrolCommand СreateCommand(Vector3 argument) => new PatrolCommand(_selectable.CurrentValue.Position.position, argument);
    }
}
