using System;
using UnityEngine;
using System.Collections.Generic;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine.UI;
using System.Linq;
using Abstractions;

namespace UserControlSystem
{
    public class CommandButtonsView : MonoBehaviour
    {
        public Action<ICommandExecutor, ICommandsQueue> OnClick;

        [SerializeField] private GameObject _attackButton;
        [SerializeField] private GameObject _moveButton;
        [SerializeField] private GameObject _patrolButton;
        [SerializeField] private GameObject _stopButton;
        [SerializeField] private GameObject _produceUnitButton;
        [SerializeField] private GameObject _setRallyButton;

        private Dictionary<Type, GameObject> _buttonsByExecutorType;

        private void Start()
        {
            _buttonsByExecutorType = new Dictionary<Type, GameObject>
            {
                { typeof(ICommandExecutor<IAttackCommand>), _attackButton },
                { typeof(ICommandExecutor<IMoveCommand>), _moveButton },
                { typeof(ICommandExecutor<IPatrolCommand>), _patrolButton },
                { typeof(ICommandExecutor<IStopCommand>), _stopButton },
                { typeof(ICommandExecutor<IProduceUnitCommand>), _produceUnitButton },
                { typeof(ICommandExecutor<ISetRallyPointCommand>), _setRallyButton }
            };

            Clear();
        }

        public void MakeLayout(List<ICommandExecutor> commandExecutors, ICommandsQueue queue)
        {
            foreach (var currentExecutor in commandExecutors)
            {
                var buttonGameObject = GetButtonGameObjectByType(currentExecutor.GetType());
                buttonGameObject.SetActive(true);
                var button = buttonGameObject.GetComponent<Button>();
                button.onClick.AddListener(() => OnClick?.Invoke(currentExecutor, queue));
            }
        }

        public void Clear()
        {
            foreach (var kvp in _buttonsByExecutorType)
            {
                kvp.Value.GetComponent<Button>().onClick.RemoveAllListeners();
                kvp.Value.SetActive(false);
            }
        }

        public void UnblockAllInteractions() => SetInteractable(true);

        public void BlockInteractions(ICommandExecutor executor)
        {
            UnblockAllInteractions();
            GetButtonGameObjectByType(executor.GetType()).GetComponent<Selectable>().interactable = false;
        }

        private void SetInteractable(bool value)
        {
            _attackButton.GetComponent<Selectable>().interactable = value;
            _moveButton.GetComponent<Selectable>().interactable = value;
            _patrolButton.GetComponent<Selectable>().interactable = value;
            _stopButton.GetComponent<Selectable>().interactable = value;
            _produceUnitButton.GetComponent<Selectable>().interactable = value;
            _setRallyButton.GetComponent<Selectable>().interactable = value;
        }

        private GameObject GetButtonGameObjectByType(Type executorInstanceType)
        {
            return _buttonsByExecutorType.Where(type => type.Key.IsAssignableFrom(executorInstanceType)).First().Value;
        }
    }
}