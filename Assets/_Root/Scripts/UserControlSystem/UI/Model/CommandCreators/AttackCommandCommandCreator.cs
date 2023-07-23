using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using System;
using UserControlSystem.CommandRealizations;
using Utils;
using Zenject;

namespace UserControlSystem
{
    public class AttackCommandCommandCreator : CommandCreatorBase<IAttackCommand>
    {
        [Inject] private AssetsContext _context;

        private Action<IAttackCommand> _creationCallback;

        [Inject]
        private void Init(ValueBase<IAttackable> target)
        {
            target.OnNewValue += OnNewValue;
        }

        private void OnNewValue(IAttackable target)
        {
            _creationCallback?.Invoke(_context.Inject(new AttackCommand(target)));
            _creationCallback = null;
        }

        protected override void ClassSpecificCommandCreation(Action<IAttackCommand> creationCallback)
        {
            _creationCallback = creationCallback;
        }

        public override void ProcessCancel()
        {
            base.ProcessCancel();

            _creationCallback = null;
        }
    }
}
