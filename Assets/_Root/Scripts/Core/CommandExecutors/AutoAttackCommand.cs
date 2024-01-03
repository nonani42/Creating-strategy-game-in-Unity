using Abstractions;
using Abstractions.Commands.CommandsInterfaces;

namespace Core.CommandExecutors
{
    public class AutoAttackCommand : IHealCommand
    {
        public IAttackable Target { get; }

        public AutoAttackCommand(IAttackable target)
        {
            Target = target;
        }
    }
}
