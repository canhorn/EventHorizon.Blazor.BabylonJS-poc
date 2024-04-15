namespace EventHorizon.Game.Client.Systems.Player.Changed;

using System;
using EventHorizon.Observer.Model;
using MediatR;

public struct PlayerDetailsChangedEvent : INotification { }

public interface PlayerDetailsChangedEventObserver : ArgumentObserver<PlayerDetailsChangedEvent> { }
