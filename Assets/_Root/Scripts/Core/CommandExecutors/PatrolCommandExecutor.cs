using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Core.CommandExecutors
{
    public class PatrolCommandExecutor : CommandExecutorBase<IPatrolCommand>
    {
        [SerializeField] private UnitMovementStop _stop;
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _navMesh;

        [SerializeField] private StopCommandExecutor _stopCommandExecutor;

        private readonly int Walk = Animator.StringToHash("Walk");
        private readonly int Idle = Animator.StringToHash("Idle");

        private int counter;

        private Vector3[] _patrolPoints = new Vector3[2];

        private bool _isMoving = false;

        public override async Task ExecuteSpecificCommand(IPatrolCommand command)
        {
            _patrolPoints[0] = command.From;
            _patrolPoints[1] = command.To;

            _navMesh = GetComponent<NavMeshAgent>();
            _navMesh.isStopped = false;
            ChangeDestination();

            _stopCommandExecutor.PatrolCancellationTokenSource = new CancellationTokenSource();

            _isMoving = true;

            while (_isMoving)
            {
                try
                {
                    _animator.SetTrigger(Walk);
                    await _stop.WithCancellation(_stopCommandExecutor.PatrolCancellationTokenSource.Token);
                    _animator.SetTrigger(Idle);
                    await Task.Delay(500);
                    ChangeDestination();
                }
                catch
                {
                    _isMoving = false;
                    _navMesh.isStopped = true;
                    _navMesh.ResetPath();
                    _animator.SetTrigger(Idle);
                }
            }
        }

        private void ChangeDestination()
        {
            counter++;

            if(counter >= _patrolPoints.Length)
                counter = 0;

            _navMesh.destination = _patrolPoints[counter];
        }
    }
}
