namespace EventHorizon.Game.Client.Engine.Systems.Entity.Unregister;

using MediatR;

public struct ClientEntityUnregisteredEvent : INotification
{
    public string GlobalId { get; }

    public ClientEntityUnregisteredEvent(string globalId)
    {
        GlobalId = globalId;
    }
}
