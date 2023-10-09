namespace EventHorizon.Game.Client.Engine.Systems.Entity.Register;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;

using MediatR;

public struct RegisterClientEntity : IRequest<StandardCommandResult>
{
    public IObjectEntityDetails EntityDetails { get; }

    public RegisterClientEntity(IObjectEntityDetails entityDetails)
    {
        EntityDetails = entityDetails;
    }
}
