namespace EventHorizon.Game.Client.Systems.EntityModule.Register;

using System;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

using MediatR;

public class RegisterAllBaseModulesOnEntityCommand
    : IRequest<StandardCommandResult>
{
    public IObjectEntity Entity { get; }

    public RegisterAllBaseModulesOnEntityCommand(IObjectEntity entity)
    {
        Entity = entity;
    }
}
