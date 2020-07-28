using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Engine.Entity.Api;
using EventHorizon.Observer.Model;
using EventHorizon.Observer.State;
using MediatR;

namespace EventHorizon.Game.Client.Systems.Local.ScreenPointer.Mesh
{
    public struct PointerHitMeshEvent : INotification
    {
        public string MeshName { get; set; }
        public IVector3 Position { get; set; }
    }

    public interface PointerHitMeshEventObserver
        : ArgumentObserver<PointerHitMeshEvent>
    {
    }

    public class PointerHitMeshEventHandler
        : INotificationHandler<PointerHitMeshEvent>
    {
        private readonly ObserverState _observer;

        public PointerHitMeshEventHandler(
            ObserverState observer
        )
        {
            _observer = observer;
        }

        public Task Handle(
            PointerHitMeshEvent notification,
            CancellationToken cancellationToken
        ) => _observer.Trigger<PointerHitMeshEventObserver, PointerHitMeshEvent>(
            notification,
            cancellationToken
        );
    }
}
