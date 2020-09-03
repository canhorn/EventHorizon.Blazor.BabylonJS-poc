namespace EventHorizon.Game.Client.Systems.Player.Changed
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;

    public struct PlayerDetailsChangedEvent 
        : INotification
    {

    }

    public interface PlayerDetailsChangedEventObserver
        : ArgumentObserver<PlayerDetailsChangedEvent>
    {
    }
}
