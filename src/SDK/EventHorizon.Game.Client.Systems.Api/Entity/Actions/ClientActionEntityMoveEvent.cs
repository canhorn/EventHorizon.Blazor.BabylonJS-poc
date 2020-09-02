namespace EventHorizon.Game.Client.Systems.Entity.Actions
{
    using System;
    using EventHorizon.Game.Client.Engine.Entity.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Api;
    using EventHorizon.Game.Client.Engine.Systems.ClientAction.Attributes;
    using EventHorizon.Observer.Model;
    using MediatR;

    [ClientAction("EntityClientMove")]
    public class ClientActionEntityMoveEvent
        : INotification,
        IClientAction
    {
        public long EntityId { get; }
        public IVector3 MoveTo { get; }

        public ClientActionEntityMoveEvent(
            IClientActionDataResolver resolver
        )
        {
            Console.WriteLine("Move Entity");
            EntityId = resolver.Resolve<long>("entityId");
            MoveTo = resolver.Resolve<IVector3>("moveTo");
        }
    }

    public interface ClientActionEntityMoveEventObserver
        : ArgumentObserver<ClientActionEntityMoveEvent>
    {
    }
}
