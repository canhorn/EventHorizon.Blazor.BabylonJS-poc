namespace EventHorizon.Game.Client.Systems.Entity.Modules.ModelLoader.Loaded;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Systems.Mesh.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

public struct MeshLoadedEvent : INotification
{
    public long ClientId { get; }
    public IEngineMesh Mesh { get; }

    public MeshLoadedEvent(long clientId, IEngineMesh mesh)
    {
        ClientId = clientId;
        Mesh = mesh;
    }
}

public interface MeshLoadedEventObserver : ArgumentObserver<MeshLoadedEvent> { }

public class MeshLoadedEventHandler : INotificationHandler<MeshLoadedEvent>
{
    private readonly ObserverState _observer;

    public MeshLoadedEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(MeshLoadedEvent notification, CancellationToken cancellationToken) =>
        _observer.Trigger<MeshLoadedEventObserver, MeshLoadedEvent>(
            notification,
            cancellationToken
        );
}
