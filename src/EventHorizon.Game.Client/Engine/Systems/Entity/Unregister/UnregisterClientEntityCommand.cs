namespace EventHorizon.Game.Client.Engine.Systems.Entity.Unregister;

using MediatR;

public struct UnregisterClientEntityCommand : IRequest<bool>
{
    public string GlobalId { get; }

    public UnregisterClientEntityCommand(string globalId)
    {
        GlobalId = globalId;
    }
}
