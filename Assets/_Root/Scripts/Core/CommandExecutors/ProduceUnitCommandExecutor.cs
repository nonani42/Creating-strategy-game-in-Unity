using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using Zenject;

namespace Core.CommandExecutors
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<IProduceUnitCommand>, IUnitProducer
    {
        public IReadOnlyReactiveCollection<IUnitProductionTask> Queue => _queue;

        [Inject(Id = "Units")] private Transform _parent;
        [SerializeField] private int _maximumUnitsInQueue = 6;

        private ReactiveCollection<IUnitProductionTask> _queue = new ReactiveCollection<IUnitProductionTask>();
        [Inject] private DiContainer _diContainer;

        public override async Task ExecuteSpecificCommand(IProduceUnitCommand command)
        {
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
                Vector3 point = new Vector3(transform.position.x + 2, transform.position.y, transform.position.z + 2);
                var instance = _diContainer.InstantiatePrefab(innerTask.UnitPrefab, point, Quaternion.identity, _parent);
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
