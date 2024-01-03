using UnityEngine;
using Zenject;

namespace UserControlSystem
{
    public class GameObjectsInstaller : MonoInstaller
    {
        [SerializeField] private Transform _unitsParent;
        private string _unitsParentId = "Units";

        public override void InstallBindings()
        {
            Container.Bind<Transform>().WithId(_unitsParentId).FromInstance(_unitsParent);
        }
    }
}
