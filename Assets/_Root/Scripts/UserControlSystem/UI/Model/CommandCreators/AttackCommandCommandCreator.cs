﻿using Abstractions;
using Abstractions.Commands;
using Abstractions.Commands.CommandsInterfaces;
using UserControlSystem.CommandRealizations;

namespace UserControlSystem
{
    public class AttackCommandCommandCreator : CancellableCommandCreatorBase<IAttackCommand, IAttackable>
    {
        protected override IAttackCommand CreateCommand(IAttackable argument) => new AttackCommand(argument);
    }
}
