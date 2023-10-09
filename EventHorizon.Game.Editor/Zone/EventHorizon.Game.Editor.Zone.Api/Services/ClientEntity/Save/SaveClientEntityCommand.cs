namespace EventHorizon.Game.Editor.Zone.Services.ClientEntity.Save;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

using MediatR;

public struct SaveClientEntityCommand
    : IRequest<CommandResult<IObjectEntityDetails>>
{
    public IObjectEntityDetails Entity { get; }

    public SaveClientEntityCommand(IObjectEntityDetails entity)
    {
        Entity = entity;
    }
}
