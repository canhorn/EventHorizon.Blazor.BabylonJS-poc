namespace EventHorizon.Game.Client.Engine.Scripting.Services
{
    using EventHorizon.Observer.Model;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public interface ScriptServices
    {
        IMediator Mediator { get; }

        ILogger Logger<T>();

        void RegisterObserver(
            ObserverBase observer
        );
        void UnRegisterObserver(
            ObserverBase observer
        );
    }
}
