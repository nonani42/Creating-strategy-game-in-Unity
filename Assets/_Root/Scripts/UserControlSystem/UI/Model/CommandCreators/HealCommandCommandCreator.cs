using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UserControlSystem.CommandRealizations;

namespace UserControlSystem
{
    public class HealCommandCommandCreator : CancellableCommandCreatorBase<IHealCommand, IAttackable>
    {
        protected override IHealCommand CreateCommand(IAttackable argument) => new HealCommand(argument);
    }
}

