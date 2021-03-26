namespace EventHorizon.Game.Client.Engine.Scripting.Services
{
    using EventHorizon.Game.Client.Core.I18n.Api;
    using EventHorizon.Observer.Model;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public interface ScriptServices
    {
        IMediator Mediator { get; }
        ILocalizer Localizer { get; }

        ILogger Logger<T>();

        T GetService<T>() where T : notnull;

        void RegisterObserver(
            ObserverBase observer
        );
        void UnRegisterObserver(
            ObserverBase observer
        );
    }
}
