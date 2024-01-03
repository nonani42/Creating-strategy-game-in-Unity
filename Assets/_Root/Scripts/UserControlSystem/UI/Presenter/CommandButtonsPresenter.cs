using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UserControlSystem
{
    internal class CommandButtonsPresenter : MonoBehaviour
    {
        [SerializeField] private CommandButtonsView _view;

        [Inject] private ValueBase<ISelectable> _selectable;
        [Inject] private CommandButtonsModel _model;

        [Inject] private ValueBase<Vector3> _groundClicksRMB;


        private ISelectable _currentSelectable;
        private Vector3 _currentGround;


        private void Start()
        {
            _view.OnClick += _model.OnCommandButtonClicked;

            _model.OnCommandSent += _view.UnblockAllInteractions;
            _model.OnCommandCancel += _view.UnblockAllInteractions;
            _model.OnCommandAccepted += _view.BlockInteractions;

            _selectable.OnNewValue += OnSelected;
            OnSelected(_selectable.CurrentValue);

            _groundClicksRMB.OnNewValue += OnGroundClicked;
            OnGroundClicked(_groundClicksRMB.CurrentValue);
        }

        private void OnSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable)
                return;

            if (_currentSelectable != null)
                _model.OnSelectionChanged();

            _currentSelectable = selectable;

            _view.Clear();

            if (selectable != null)
            {
                var commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());

                var queue = (selectable as Component).GetComponentInParent<ICommandsQueue>();
                _view.MakeLayout(commandExecutors, queue);
            }
        }

        private void OnGroundClicked(Vector3 ground)
        {
                if (_currentGround == ground)
                return;

            if (_currentGround != null)
                _currentGround = ground;

            if (_currentSelectable != null && ground != null)
            {
                var commandExecutor = (_currentSelectable as Component).GetComponentInParent<ICommandExecutor<IMoveCommand>>();

                var queue = (_currentSelectable as Component).GetComponentInParent<ICommandsQueue>();
                _model.OnCommandButtonClicked(commandExecutor, queue);
            }
        }
    }
}
