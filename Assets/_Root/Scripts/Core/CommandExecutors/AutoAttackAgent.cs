using UnityEngine;
using UniRx;
using Core.CommandExecutors;
using Abstractions;

namespace Assets._Root.Scripts.Core.CommandExecutors
{
    public class AutoAttackAgent : MonoBehaviour
    {
        [SerializeField] private ChomperCommandsQueue _queue;
        private void Start()
        {
            AutoAttackEvaluator.AutoAttackCommands
            .ObserveOnMainThread()
            .Where(command => command.Attacker == gameObject)
            .Where(command => command.Attacker != null &&
            command.Target != null)
            .Subscribe(command => AutoAttack(command.Target))
            .AddTo(this);
        }

        private void AutoAttack(GameObject target)
        {
            _queue.Clear();
            _queue.EnqueueCommand(new
            AutoAttackCommand(target.GetComponent<IAttackable>()));
        }
    }
}
