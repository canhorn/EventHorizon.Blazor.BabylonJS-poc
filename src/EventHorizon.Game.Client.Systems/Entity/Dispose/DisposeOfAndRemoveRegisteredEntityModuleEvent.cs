namespace EventHorizon.Game.Client.Systems.Entity.Dispose;

using EventHorizon.Observer.Model;

using MediatR;

public record DisposeOfAndRemoveRegisteredEntityModuleEvent(string ModuleName)
    : INotification;

public interface DisposeOfAndRemoveRegisteredEntityModuleEventObserver
    : ArgumentObserver<DisposeOfAndRemoveRegisteredEntityModuleEvent> { }
