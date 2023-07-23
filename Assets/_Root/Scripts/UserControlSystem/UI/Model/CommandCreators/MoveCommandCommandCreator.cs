using Abstractions;
using Abstractions.Commands.CommandsInterfaces;
using System;
using UnityEngine;
using UserControlSystem.CommandRealizations;
using Utils;
using Zenject;

namespace UserControlSystem
{
    public class MoveCommandCommandCreator : CommandCreatorBase<IMoveCommand>
    {
        [Inject] private AssetsContext _context;

        private Action<IMoveCommand> _creationCallback;


        [Inject]
        private void Init(ValueBase<Vector3> groundClicks)
        {
            groundClicks.OnNewValue += OnNewValue;
        }

        private void OnNewValue(Vector3 groundClick)
        {
            _creationCallback?.Invoke(_context.Inject(new MoveCommand(groundClick)));
            _creationCallback = null;
        }

        protected override void ClassSpecificCommandCreation(Action<IMoveCommand> creationCallback)
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