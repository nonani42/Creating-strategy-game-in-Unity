using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;
using Utils;

namespace UserControlSystem.CommandRealizations
{
    public class ProduceUnitCommand : IProduceUnitCommand
    {
        public GameObject UnitPrefab => _unitPrefab;
        [InjectAsset("Chomper")] private GameObject _unitPrefab;
    }

    public sealed class ProduceUnitCommandHeir : ProduceUnitCommand
    {
    }
}
