using Abstractions;
using Abstractions.Commands.CommandsInterfaces;

namespace UserControlSystem.CommandRealizations
{
    public sealed class HealCommand : IHealCommand
    {
        public IAttackable Target { get; }

        public HealCommand(IAttackable target)
        {
            Target = target;
        }
    }
}

