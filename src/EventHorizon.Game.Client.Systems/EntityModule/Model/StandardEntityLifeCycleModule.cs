namespace EventHorizon.Game.Client.Systems.EntityModule.Model
{
    using System;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Engine.Entity.Model;
    using EventHorizon.Game.Client.Engine.Scripting.Api;
    using EventHorizon.Game.Client.Engine.Scripting.Data;
    using EventHorizon.Game.Client.Engine.Scripting.Services;
    using EventHorizon.Game.Client.Systems.EntityModule.Api;

    public class StandardEntityLifeCycleModule
        : ClientEntityBase,
        IEntityLifeCycleModule
    {
        private readonly ScriptServices _scriptServices;
        private readonly Option<IClientScript> _initializeScript;
        private readonly Option<IClientScript> _disposeScript;
        private readonly Option<IClientScript> _updateScript;
        private readonly ScriptData _scriptData;

        private Func<ScriptServices, ScriptData, Task> _runnableUpdateScript = (_, __) => Task.CompletedTask;

        public string Name { get; }
        public bool IsInitializable => _initializeScript.HasValue;
        public bool IsDisposable => _disposeScript.HasValue;
        public bool IsUpdatable => _updateScript.HasValue;

        public StandardEntityLifeCycleModule(
            long clientId,
            string name,
            Option<IClientScript> initializeScript,
            Option<IClientScript> disposeScript,
            Option<IClientScript> updateScript,
            ScriptData scriptData
        ) : base(clientId)
        {
            Name = name;

            _scriptServices = GameServiceProvider.GetService<ScriptServices>();
            _initializeScript = initializeScript;
            _disposeScript = disposeScript;
            _updateScript = updateScript;
            _scriptData = scriptData;
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
                _runnableUpdateScript = _updateScript.Value.Run;
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
            return _runnableUpdateScript(
                _scriptServices,
                _scriptData
            );
        }
    }
}