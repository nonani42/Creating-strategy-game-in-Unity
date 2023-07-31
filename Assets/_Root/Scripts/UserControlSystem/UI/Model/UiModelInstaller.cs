using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using System;
using UnityEngine;
using Utils;
using Zenject;

namespace UserControlSystem
{
    public class UiModelInstaller : MonoInstaller
    {
        [SerializeField] private Transform _groundTransform;
        private string _groundTransformId = "Ground";

        public override void InstallBindings()
        {
            Container.Bind<Transform>().WithId(_groundTransformId).FromInstance(_groundTransform);

            Container.Bind(
                typeof(IObservable<ISelectable>),
                typeof(ValueBase<ISelectable>)).
                To<SelectableValue>().AsSingle();

            Container.Bind(
                //typeof(IAwaitable<IAttackable>),
                typeof(ValueBase<IAttackable>)).
                To<AttackableValue>().AsSingle();

            Container.Bind(
                //typeof(IAwaitable<Vector3>),
                typeof(ValueBase<Vector3>)).
                To<Vector3Value>().AsSingle();

            Container.Bind<CommandCreatorBase<IProduceUnitCommand>>().To<ProduceUnitCommandCommandCreator>().AsSingle();

            Container.Bind<CommandCreatorBase<IAttackCommand>>().To<AttackCommandCommandCreator>().AsSingle();

            Container.Bind<CommandCreatorBase<IMoveCommand>>().To<MoveCommandCommandCreator>().AsSingle();

            Container.Bind<CommandCreatorBase<IPatrolCommand>>().To<PatrolCommandCommandCreator>().AsSingle();

            Container.Bind<CommandCreatorBase<IStopCommand>>().To<StopCommandCommandCreator>().AsSingle();

            Container.Bind<CommandButtonsModel>().AsSingle();
        }
    }
}
