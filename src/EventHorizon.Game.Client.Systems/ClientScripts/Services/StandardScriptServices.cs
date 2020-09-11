namespace EventHorizon.Game.Client.Systems.ClientScripts.Services
{
    using System.Collections.Generic;
    using EventHorizon.Game.Client.Core.I18n.Api;
    using EventHorizon.Game.Client.Engine.Scripting.Services;
    using EventHorizon.Observer.Model;
    using EventHorizon.Observer.State;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class StandardScriptServices
        : ScriptServices
    {
        public readonly IMediator _mediator;
        public readonly ILoggerFactory _loggerFactory;
        private readonly ObserverState _observerState;
        private readonly ILocalizer _localizer;

        public StandardScriptServices(
            IMediator mediator,
            ILoggerFactory loggerFactory,
            ObserverState observerState,
            ILocalizer localizer
        )
        {
            _mediator = mediator;
            _loggerFactory = loggerFactory;
            _observerState = observerState;
            _localizer = localizer;
        }

        public IMediator Mediator => _mediator;
        public ILocalizer Localizer => _localizer;

        public ILogger Logger<T>()
        {
            return _loggerFactory.CreateLogger<T>();
        }

        public T GetService<T>() where T : notnull
        {
            return GameServiceProvider.GetService<T>();
        }

        public void RegisterObserver(
            ObserverBase observer
        ) => _observerState.Register(
            observer
        );

        public void UnRegisterObserver(
            ObserverBase observer
        ) => _observerState.Remove(
            observer
        );
    }
}