using UnityEngine;
using Zenject;

namespace UserControlSystem
{
    public class GameObjectsInstaller : MonoInstaller
    {
        [SerializeField] private Transform _unitsParent;
        private string __unitsParentId = "Units";

        public override void InstallBindings()
        {
            Container.Bind<Transform>().WithId(__unitsParentId).FromInstance(_unitsParent);
        }
    }
}
