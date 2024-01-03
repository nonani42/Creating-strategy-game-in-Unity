using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Core.CommandExecutors
{
    public class MoveCommandExecutor : CommandExecutorBase<IMoveCommand>
    {
        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _navMesh;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;

        private readonly int Walk = Animator.StringToHash("Walk");
        private readonly int Idle = Animator.StringToHash("Idle");

        public override async Task ExecuteSpecificCommand(IMoveCommand command)
        {
            _navMesh = GetComponent<NavMeshAgent>();
            _navMesh.isStopped = false;
            _navMesh.destination = command.Target;

            _stopCommandExecutor.MoveCancellationTokenSource = new CancellationTokenSource();

            try
            {
                _animator.SetTrigger(Walk);
                await _stop.RunWithCancellation(_stopCommandExecutor.MoveCancellationTokenSource.Token);
                _navMesh.isStopped = true;
                _navMesh.ResetPath();
                _animator.SetTrigger(Idle);
            }

            catch
            {
                _navMesh.isStopped = true;
                _navMesh.ResetPath();
                _animator.SetTrigger(Idle);
            }
        }
    }
}
