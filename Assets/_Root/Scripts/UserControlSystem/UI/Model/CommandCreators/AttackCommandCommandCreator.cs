using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UserControlSystem.CommandRealizations;

namespace UserControlSystem
{
    public class AttackCommandCommandCreator : CancellableCommandCreatorBase<IHealCommand, IAttackable>
    {
        protected override IHealCommand CreateCommand(IAttackable argument) => new AttackCommand(argument);
    }
}
