namespace EventHorizon.Game.Client.Systems.EntityModule.Register;

using EventHorizon.Observer.Model;

using MediatR;

public record BaseEntityModulesChangedEvent
    : INotification;

public interface BaseEntityModulesChangedEventObserver
    : ArgumentObserver<BaseEntityModulesChangedEvent>
{
}
