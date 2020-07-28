namespace EventHorizon.Game.Client.Engine.Entity.Tracking.State
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Entity.Tracking.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Model;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Dispose;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Disposed;
    using MediatR;

    public class StandardServerEntityTrackingState
        : IServerEntityTrackingState
    {
        private IDictionary<long, ServerLifecycleEntityBase> _entityMap = new Dictionary<long, ServerLifecycleEntityBase>();

        public async Task DisposeOfTracked()
        {
            var mediator = GameServiceProvider.GetService<IMediator>();
            foreach (var entity in _entityMap.Values)
            {
                await mediator.Send(
                    new DisposeOfEntityCommand(
                        entity
                    )
                );
            }
        }

        public async Task DisposeOfTrackedEntity(
            long clientId
        )
        {
            if (_entityMap.TryGetValue(clientId, out var entity))
            {
                var mediator = GameServiceProvider.GetService<IMediator>();
                await mediator.Send(
                    new DisposeOfEntityCommand(
                        entity
                    )
                );
            }
        }

        public IEnumerable<T> QueryByNotTag<T>(
            string tag
        ) where T : ServerLifecycleEntityBase
        {
            return _entityMap.Values.Where(
                a => !a.Tags.Contains(tag)
            ).Cast<T>();
        }

        public IEnumerable<T> QueryByTag<T>(
            string tag
        ) where T : ServerLifecycleEntityBase
        {
            return _entityMap.Values.Where(
                a => a.Tags.Contains(tag)
            ).Cast<T>();
        }

        public void Track(
            ServerLifecycleEntityBase entity
        )
        {
            _entityMap[entity.ClientId] = entity;
        }

        public void Untrack(
            long clientId
        )
        {
            _entityMap.Remove(
                clientId
            );
        }
    }
}
