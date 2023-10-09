namespace EventHorizon.Game.Client.Systems.Local.ScreenPointer.Mesh;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;

using MediatR;

public struct PointerHitMeshEvent : INotification
{
    public string MeshName { get; }
    public IVector3 Position { get; }

    public PointerHitMeshEvent(string meshName, IVector3 position)
    {
        MeshName = meshName;
        Position = position;
    }
}

public interface PointerHitMeshEventObserver
    : ArgumentObserver<PointerHitMeshEvent> { }

public class PointerHitMeshEventHandler
    : INotificationHandler<PointerHitMeshEvent>
{
    private readonly ObserverState _observer;

    public PointerHitMeshEventHandler(ObserverState observer)
    {
        _observer = observer;
    }

    public Task Handle(
        PointerHitMeshEvent notification,
        CancellationToken cancellationToken
    ) =>
        _observer.Trigger<PointerHitMeshEventObserver, PointerHitMeshEvent>(
            notification,
            cancellationToken
        );
}
