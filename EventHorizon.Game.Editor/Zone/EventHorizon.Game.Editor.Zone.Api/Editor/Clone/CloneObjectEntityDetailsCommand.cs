namespace EventHorizon.Game.Editor.Zone.Editor.Clone;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Client.Engine.Systems.Entity.Model;

using MediatR;

public struct CloneObjectEntityDetailsCommand
    : IRequest<CommandResult<ObjectEntityDetailsModel>>
{
    public IObjectEntityDetails EntityDetails { get; }

    public CloneObjectEntityDetailsCommand(IObjectEntityDetails entityDetails)
    {
        EntityDetails = entityDetails;
    }
}
