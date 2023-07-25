using Abstractions;
using System;
using UnityEngine;
using UnityEngine.AI;
using Utils;

namespace Core
{
    public class UnitMovementStop : MonoBehaviour, IAwaitable<AsyncExtensions.Void>
    {
        public class StopAwaiter : AwaiterBase<AsyncExtensions.Void>
        {
            protected readonly UnitMovementStop _unitStop;

            public StopAwaiter(UnitMovementStop unitStop)
            {
                _unitStop = unitStop;
                _unitStop.OnStop += OnStop;
            }

            public void OnStop()
            {
                _unitStop.OnStop -= OnStop;
                OnNewValue(new AsyncExtensions.Void());
            }
        }            

        [SerializeField] private NavMeshAgent _agent;

        public event Action OnStop;

        void Update()
        {
            if (!_agent.pathPending)
            {
                if (_agent.remainingDistance <= _agent.stoppingDistance)
                {
                    if (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f)
                        OnStop?.Invoke();
                }
            }
        }

        public IAwaiter<AsyncExtensions.Void> GetAwaiter() => new StopAwaiter(this);
    }
}
