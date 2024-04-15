namespace EventHorizon.Game.Client.Systems.EntityModule.Register;

using EventHorizon.Observer.Model;
using MediatR;

public record PlayerEntityModulesChangedEvent : INotification;

public interface PlayerEntityModulesChangedEventObserver
    : ArgumentObserver<PlayerEntityModulesChangedEvent> { }
