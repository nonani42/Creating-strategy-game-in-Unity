using System;
using Zenject;
using UniRx;
using UnityEngine;
using Abstractions;

namespace Assets._Root.Scripts.UserControlSystem.UI.Model
{
    public class BottomCenterModel
    {
        public IObservable<IUnitProducer> UnitProducers { get; private set; }

        [Inject]
        public void Init(IObservable<ISelectable> currentlySelected)
        {
            UnitProducers = currentlySelected.Select(selectable => selectable as Component).Select(component => component?.GetComponent<IUnitProducer>());
        }
    }
}
