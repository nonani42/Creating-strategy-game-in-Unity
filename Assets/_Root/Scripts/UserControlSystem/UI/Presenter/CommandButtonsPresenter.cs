using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System;
using System.Collections.Generic;
using UnityEngine;
using UserControlSystem.CommandsRealization;
using UserControlSystem.UI.View;
using Utils;

namespace UserControlSystem.UI.Presenter
{
    internal class CommandButtonsPresenter : MonoBehaviour
    {
        [SerializeField] private SelectableValue _selectable;
        [SerializeField] private CommandButtonsView _view;
        [SerializeField] private AssetsContext _context;

        private ISelectable _currentSelectable;


        private void Start()
        {
            _selectable.OnSelected += OnSelected;
            OnSelected(_selectable.CurrentValue);

            _view.OnClick += OnButtonClick;
        }

        private void OnSelected(ISelectable selectable)
        {
            if (_currentSelectable == selectable)
                return;

            _currentSelectable = selectable;

            _view.Clear();

            if (selectable != null)
            {
                var commandExecutors = new List<ICommandExecutor>();
                commandExecutors.AddRange((selectable as Component).GetComponentsInParent<ICommandExecutor>());
                _view.MakeLayout(commandExecutors);
            }
        }

        private void OnButtonClick(ICommandExecutor commandExecutor)
        {
            if (commandExecutor is CommandExecutorBase<IProduceUnitCommand>)
            {
                var unitProducer = commandExecutor as CommandExecutorBase<IProduceUnitCommand>;
                unitProducer.ExecuteSpecificCommand(_context.Inject(new ProduceUnitCommand()));
            }

            else if (commandExecutor is CommandExecutorBase<IAttackComand>)
            {
                var unitProducer = commandExecutor as CommandExecutorBase<IAttackComand>;
                unitProducer.ExecuteSpecificCommand(new AttackCommand());
            }

            else if (commandExecutor is CommandExecutorBase<IPatrolCommand>)
            {
                var unitProducer = commandExecutor as CommandExecutorBase<IPatrolCommand>;
                unitProducer.ExecuteSpecificCommand(_context.Inject(new PatrolCommand()));
            }

            else if (commandExecutor is CommandExecutorBase<IStopCommand>)
            {
                var unitProducer = commandExecutor as CommandExecutorBase<IStopCommand>;
                unitProducer.ExecuteSpecificCommand(_context.Inject(new StopCommand()));
            }

            else if (commandExecutor is CommandExecutorBase<IMoveCommand>)
            {
                var unitProducer = commandExecutor as CommandExecutorBase<IMoveCommand>;
                unitProducer.ExecuteSpecificCommand(_context.Inject(new MoveCommand()));
            }

            else
            {
                throw new ApplicationException($"{nameof(CommandButtonsPresenter)}.{nameof(OnButtonClick)}: " +
                    $"Unknown type of commands executor: {commandExecutor.GetType().FullName}!");
            }
        }
    }
}
