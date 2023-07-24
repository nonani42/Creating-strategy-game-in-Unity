using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using Utils;
using Zenject;

namespace UserControlSystem
{
    public class UiModelInstaller : MonoInstaller
    {
        [SerializeField] private Transform _groundTransform;
        private string _groundTransformId = "Ground";

        private AttackableValue _attackableValue;
        private Vector3Value _vector3Value;

        public override void InstallBindings()
        {
            Container.Bind<Transform>().WithId(_groundTransformId).FromInstance(_groundTransform);

            Container.Bind<ValueBase<ISelectable>>().To<SelectableValue>().AsSingle();

            _attackableValue = new AttackableValue();
            Container.Bind<ValueBase<IAttackable>>().FromInstance(_attackableValue);

            _vector3Value = new Vector3Value();
            Container.Bind<ValueBase<Vector3>>().FromInstance(_vector3Value);


            Container.Bind<IAwaitable<IAttackable>>().FromInstance(_attackableValue);
            Container.Bind<IAwaitable<Vector3>>().FromInstance(_vector3Value);


            Container.Bind<CommandCreatorBase<IProduceUnitCommand>>().To<ProduceUnitCommandCommandCreator>().AsSingle();

            Container.Bind<CommandCreatorBase<IAttackCommand>>().To<AttackCommandCommandCreator>().AsSingle();

            Container.Bind<CommandCreatorBase<IMoveCommand>>().To<MoveCommandCommandCreator>().AsSingle();

            Container.Bind<CommandCreatorBase<IPatrolCommand>>().To<PatrolCommandCommandCreator>().AsSingle();

            Container.Bind<CommandCreatorBase<IStopCommand>>().To<StopCommandCommandCreator>().AsSingle();

            Container.Bind<CommandButtonsModel>().AsSingle();
        }
    }
}
