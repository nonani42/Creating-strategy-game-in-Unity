using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;
using System;

namespace Core.CommandExecutors
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;

        [Inject(Id = "Units")] private Transform _parent;
        [SerializeField] private int _maximumUnitsInQueue = 6;

        private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();
        [Inject] private DiContainer _diContainer;

        private System.Random rnd = new System.Random();

        public override async Task ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            if (_queue.Count == _maximumUnitsInQueue)
                return;

            _queue.Add(new UnitProductionTask(command.ProductionTime, command.Icon, command.UnitPrefab, command.UnitName));
        }

        private void Update()
        {
            if (_queue.Count == 0)
                return;

            var innerTask = (UnitProductionTask)_queue[0];

            innerTask.TimeLeft -= Time.deltaTime;

            if (innerTask.TimeLeft <= 0)
            {
                RemoveTaskAtIndex(0);
                Vector3 point = new Vector3(transform.position.x + rnd.Next(-10, -5), transform.position.y, transform.position.z + rnd.Next(-10, -5));
                var instance = _diContainer.InstantiatePrefab(innerTask.UnitPrefab, point, Quaternion.identity, _parent);

                var factionMember = instance.GetComponent<FactionMember>();
                factionMember.SetFaction(GetComponent<FactionMember>().FactionId);

                var queue = instance.GetComponent<ICommandsQueue>();
                var mainBuilding = GetComponent<MainBuilding>();

                queue.EnqueueCommand(new MoveCommand(mainBuilding.RallyPoint));
            }
        }

        public void Cancel(int index) => RemoveTaskAtIndex(index);

        private void RemoveTaskAtIndex(int index)
        {
            for (int i = index; i < _queue.Count-1; i++)
            { 
                _queue[i] = _queue[i + 1];
            }
            _queue.RemoveAt(_queue.Count-1);
        }
    }
}
