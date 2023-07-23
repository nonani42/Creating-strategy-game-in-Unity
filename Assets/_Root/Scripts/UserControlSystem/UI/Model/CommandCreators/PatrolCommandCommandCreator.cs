using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using System;
using UnityEngine;
using UserControlSystem.CommandRealizations;
using Utils;
using Zenject;

namespace UserControlSystem
{
    public class PatrolCommandCommandCreator : CommandCreatorBase<IPatrolCommand>
    {
        [Inject] private AssetsContext _context;
        [Inject] private ValueBase<ISelectable> _selectable;

        private Action<IPatrolCommand> _creationCallback;

        [Inject]
        private void Init(ValueBase<Vector3> groundClicks)
        {
            groundClicks.OnNewValue += OnNewValue;
        }

        private void OnNewValue(Vector3 groundClick)
        {
            _creationCallback?.Invoke(_context.Inject(new PatrolCommand(_selectable.CurrentValue.Position.position, groundClick)));
            _creationCallback = null;
        }

        protected override void ClassSpecificCommandCreation(Action<IPatrolCommand> creationCallback)
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
