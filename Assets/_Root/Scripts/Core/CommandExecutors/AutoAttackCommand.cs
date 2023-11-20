using Abstractions;
using Abstractions.Commands.CommandsInterfaces;

namespace Core.CommandExecutors
{
    public class AutoAttackCommand : IAttackCommand
    {
        public IAttackable Target { get; }

        public AutoAttackCommand(IAttackable target)
        {
            Target = target;
        }
    }
}
