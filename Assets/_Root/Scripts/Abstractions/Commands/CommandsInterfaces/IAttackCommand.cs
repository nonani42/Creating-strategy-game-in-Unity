using System.Threading;

namespace Abstractions.Commands.CommandsInterfaces
{
    public interface IAttackCommand : ICommand
    {
        IAttackable Target { get; }
    }
}