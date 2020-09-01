namespace EventHorizon.Game.Client.Systems.ServerModule.Model
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Scripting.Api;
    using EventHorizon.Game.Client.Engine.Scripting.Data;
    using EventHorizon.Game.Client.Engine.Scripting.Services;
    using EventHorizon.Game.Client.Systems.ServerModule.Api;

    public class StandardServerModule
        : ClientEntityBase,
        IServerModule
    {
        private readonly ScriptServices _scriptServices;
        private readonly ScriptData _scriptData;
        private readonly Option<IClientScript> _initializeScript;
        private readonly Option<IClientScript> _disposeScript;
        private readonly Option<IClientScript> _updateScript;

        private Option<IClientScript> _runnableUpdateScript = new Option<IClientScript>(
            null
        );

        public string Name { get; }

        public StandardServerModule(
            long clientId,
            string name,
            Option<IClientScript> initializeScript,
            Option<IClientScript> disposeScript,
            Option<IClientScript> updateScript
        ) : base(clientId)
        {
            Name = name;

            _scriptServices = GameServiceProvider.GetService<ScriptServices>();
            _scriptData = new ScriptData(
                new Dictionary<string, object>()
            );
            _initializeScript = initializeScript;
            _disposeScript = disposeScript;
            _updateScript = updateScript;
        }

        public Task Initialize()
        {
            if (_initializeScript.HasValue)
            {
                return _initializeScript.Value.Run(
                    _scriptServices,
                    _scriptData
                );
            }
            return Task.CompletedTask;
        }

        public Task PostInitialize()
        {
            if (_updateScript.HasValue)
            {
                _runnableUpdateScript = _updateScript;
            }
            return Task.CompletedTask;
        }

        public Task Dispose()
        {
            if (_disposeScript.HasValue)
            {
                return _disposeScript.Value.Run(
                    _scriptServices,
                    _scriptData
                );
            }
            return Task.CompletedTask;
        }

        public Task Update()
        {
            if (_runnableUpdateScript.HasValue)
            {
                return _runnableUpdateScript.Value.Run(
                    _scriptServices,
                    _scriptData
                );
            }
            return Task.CompletedTask;
        }
    }
}