namespace EventHorizon.Game.Server.Game.Updated
{
    using System;
    using EventHorizon.Observer.Model;
    using MediatR;

    public struct GameStateUpdatedEvent
        : INotification
    {

    }

    public interface GameStateUpdatedEventObserver
        : ArgumentObserver<GameStateUpdatedEvent>
    {
    }
}
