using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Utils;

namespace Core.CommandExecutors
{
    public partial class AttackCommandExecutor : CommandExecutorBase<IAttackCommand>
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private StopCommandExecutor _stopCommandExecutor;

        [Inject] private IHealthHolder _ourHealth;

        [Inject(Id = "AttackDistance")] private float _attackingDistance;
        [Inject(Id = "AttackPeriod")] private int _attackingPeriod;

        private Vector3 _ourPosition;
        private Vector3 _targetPosition;
        private Quaternion _ourRotation;

        private readonly Subject<Vector3> _targetPositions = new Subject<Vector3>();
        private readonly Subject<Quaternion> _targetRotations = new Subject<Quaternion>();
        private readonly Subject<IAttackable> _attackTargets = new Subject<IAttackable>();

        private Transform _targetTransform;
        private AttackOperation _currentAttackOp;

        private readonly int Idle = Animator.StringToHash("Idle");
        private readonly int Attack = Animator.StringToHash("Attack");
        private readonly int Walk = Animator.StringToHash("Walk");


        [Inject]
        private void Init()
        {
            _targetPositions
            .Select(value => new Vector3((float)Math.Round(value.x, 2),
            (float)Math.Round(value.y, 2), (float)Math.Round(value.z, 2)))
            .Distinct()
            .ObserveOnMainThread()
            .Subscribe(StartMovingToPosition);
            _attackTargets
            .ObserveOnMainThread()
            .Subscribe(StartAttackingTargets);
            _targetRotations
            .ObserveOnMainThread()
            .Subscribe(SetAttackRotation);
        }

        private void SetAttackRotation(Quaternion targetRotation)
        {
            transform.rotation = targetRotation;
        }

        private void StartAttackingTargets(IAttackable target)
        {
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<NavMeshAgent>().ResetPath();
            _animator.SetTrigger(Attack);
            target.RecieveDamage(GetComponent<IDamageDealer>().Damage);
        }

        private void StartMovingToPosition(Vector3 position)
        {
            GetComponent<NavMeshAgent>().destination = position;
            _animator.SetTrigger(Walk);
        }

        public override async Task ExecuteSpecificCommand(IAttackCommand command)
        {
            _targetTransform = (command.Target as Component).transform;
            _currentAttackOp = new AttackOperation(this, command.Target);

            //Update();

            _stopCommandExecutor.AttackCancellationTokenSource = new CancellationTokenSource();

            try
            {
                await
                _currentAttackOp.RunWithCancellation(_stopCommandExecutor.AttackCancellationTokenSource.Token);
            }
            catch
            {
                _currentAttackOp.Cancel();
            }

            _animator.SetTrigger(Idle);
            _currentAttackOp = null;
            _targetTransform = null;
            _stopCommandExecutor.AttackCancellationTokenSource = null;
        }

        private void Update()
        {
            if (_currentAttackOp == null)
            {
                return;
            }

            _ourPosition = transform.position;
            _ourRotation = transform.rotation;
            if (_targetTransform != null)
                _targetPosition = _targetTransform.position;
        }
    }
}
