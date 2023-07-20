﻿using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UnityEngine;

namespace Core.CommandExecutors
{
    public class ProduceUnitCommandExecutor : CommandExecutorBase<IProduceUnitCommand>
    {
        public override void ExecuteSpecificCommand(IProduceUnitCommand command)
        {
            GameObject parent = GameObject.Find("Units");
            Transform parentTransform = (parent != null)? parent.transform : null;
            Instantiate(command.UnitPrefab, new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10)), Quaternion.identity, parentTransform);
        }
    }
}
