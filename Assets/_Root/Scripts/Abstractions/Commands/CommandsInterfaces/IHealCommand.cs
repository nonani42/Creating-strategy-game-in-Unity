namespace Abstractions.Commands.CommandsInterfaces
{
    public interface IHealCommand : ICommand
    {
        IAttackable Target { get; }
    }
}
