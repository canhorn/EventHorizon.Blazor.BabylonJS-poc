namespace EventHorizon.Game.Client.Systems.ClientScripts.Services
{
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

        public StandardScriptServices(
            IMediator mediator,
            ILoggerFactory loggerFactory,
            ObserverState observerState
        )
        {
            _mediator = mediator;
            _loggerFactory = loggerFactory;
            _observerState = observerState;
        }

        public IMediator Mediator => _mediator;

        public ILogger Logger<T>()
        {
            return _loggerFactory.CreateLogger<T>();
        }

        public T GetService<T>()
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

        public string Translate(
            string key,
            params string[] replaces
        )
        {
            // TODO: [Localization] - Finish Implementation
            var translation = key;

            for (int i = 0; i < replaces.Length; i++)
            {
                translation = translation.Replace(
                    $"{{{i}}}",
                    replaces[i]
                );
            }

            return translation;
        }
    }
}