﻿using Abstractions.Commands.CommandsInterfaces;
using Abstractions.Commands;
using System;
using Zenject;
using Abstractions;
using UnityEngine;

namespace UserControlSystem
{
    public class CommandButtonsModel
    {
        public event Action<ICommandExecutor> OnCommandAccepted;
        public event Action OnCommandSent;
        public event Action OnCommandCancel;

        [Inject] private CommandCreatorBase<IProduceUnitCommand> _unitProducer;
        [Inject] private CommandCreatorBase<IHealCommand> _attacker;
        [Inject] private CommandCreatorBase<IStopCommand> _stopper;
        [Inject] private CommandCreatorBase<IMoveCommand> _mover;
        [Inject] private CommandCreatorBase<IPatrolCommand> _patroller;
        [Inject] private CommandCreatorBase<ISetRallyPointCommand> _rallySetter;
        [Inject] private CommandCreatorBase<IHealCommand> _healer;

        private bool _commandIsPending;

        public void OnCommandButtonClicked(ICommandExecutor commandExecutor, ICommandsQueue commandsQueue)
        {
            if (_commandIsPending)
            {
                ProcessOnCancel();
            }
            _commandIsPending = true;

            OnCommandAccepted?.Invoke(commandExecutor);

            _unitProducer.ProcessCommandExecutor(commandExecutor, command =>
                ExecuteCommandWrapper(command, commandsQueue));

            _attacker.ProcessCommandExecutor(commandExecutor, command =>
                ExecuteCommandWrapper(command, commandsQueue));

            _healer.ProcessCommandExecutor(commandExecutor, command =>
                ExecuteCommandWrapper(command, commandsQueue));

            _stopper.ProcessCommandExecutor(commandExecutor, command =>
                ExecuteCommandWrapper(command, commandsQueue));

            _mover.ProcessCommandExecutor(commandExecutor, command =>
                ExecuteCommandWrapper(command, commandsQueue));

            _patroller.ProcessCommandExecutor(commandExecutor, command =>
                ExecuteCommandWrapper(command, commandsQueue));

            _rallySetter.ProcessCommandExecutor(commandExecutor, command =>
                ExecuteCommandWrapper(command, commandsQueue));
        }

        public void ExecuteCommandWrapper(object command, ICommandsQueue commandsQueue)
        {
            if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                commandsQueue.Clear();

            commandsQueue.EnqueueCommand(command);
            _commandIsPending = false;
            OnCommandSent?.Invoke();
        }

        public void OnSelectionChanged()
        {
            _commandIsPending = false;
            ProcessOnCancel();
        }

        private void ProcessOnCancel()
        {
            _unitProducer.ProcessCancel();
            _attacker.ProcessCancel();
            _stopper.ProcessCancel();
            _mover.ProcessCancel();
            _patroller.ProcessCancel();
            _rallySetter.ProcessCancel();
            OnCommandCancel?.Invoke();
        }
    }
}
