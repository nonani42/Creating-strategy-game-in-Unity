using Abstractions;
using Abstractions.Commands;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace UserControlSystem
{
    internal class CommandButtonsPresenter : MonoBehaviour
    {
        [SerializeField] private CommandButtonsView _view;

        [Inject] private ValueBase<ISelectable> _selectable;
        [Inject] private CommandButtonsModel _model;

        private ISelectable _currentSelectable;


        private void Start()
        {
            _view.OnClick += _model.OnCommandButtonClicked;

            _model.OnCommandSent += _view.UnblockAllInteractions;
            _model.OnCommandCancel += _view.UnblockAllInteractions;
            _model.OnCommandAccepted += _view.BlockInteractions;

            _selectable.OnNewValue += OnSelected;
            OnSelected(_selectable.CurrentValue);
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
                _view.MakeLayout(commandExecutors);
            }
        }
    }
}
