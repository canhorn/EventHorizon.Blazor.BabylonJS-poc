namespace EventHorizon.Game.Client.Systems.Local.Modules.MeshManagement.Set;

using System;

using EventHorizon.Observer.Model;

using MediatR;

public struct MeshSetEvent : INotification
{
    public long ClientId { get; }

    public MeshSetEvent(long clientId)
    {
        ClientId = clientId;
    }
}

public interface MeshSetEventObserver : ArgumentObserver<MeshSetEvent> { }
