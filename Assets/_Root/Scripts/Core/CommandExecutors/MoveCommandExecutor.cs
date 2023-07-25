﻿using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Core.CommandExecutors
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent navMesh;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;

        private readonly int Walk = Animator.StringToHash("Walk");
        private readonly int Idle = Animator.StringToHash("Idle");

        public override async void ExecuteSpecificCommand(IMoveCommand command)
        {
            navMesh = GetComponent<NavMeshAgent>();
            navMesh.destination = command.Target;

            _stopCommandExecutor.MoveCancellationTokenSource = new CancellationTokenSource();

            try
            {
                _animator.SetTrigger(Walk);
                await _stop.WithCancellation(_stopCommandExecutor.MoveCancellationTokenSource.Token);
                _animator.SetTrigger(Idle);
            }

            catch
            {
                navMesh.destination = gameObject.transform.position;
                _animator.SetTrigger(Idle);
            }
        }
    }
}
