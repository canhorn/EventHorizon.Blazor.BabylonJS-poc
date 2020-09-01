namespace EventHorizon.Game.Client.Systems.ServerModule.Add
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Core.Api;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;
    using EventHorizon.Game.Client.Engine.Scripting.Api;
    using EventHorizon.Game.Client.Systems.ClientScripts.Get;
    using EventHorizon.Game.Client.Systems.ServerModule.Api;
    using EventHorizon.Game.Client.Systems.ServerModule.Dispose;
    using EventHorizon.Game.Client.Systems.ServerModule.Model;
    using MediatR;

    public class AddServerModuleScriptCommandHandler
        : IRequestHandler<AddServerModuleScriptCommand, StandardCommandResult>
    {
        private readonly IMediator _mediator;
        private readonly IIndexPool _indexPool;
        private readonly ServerModuleState _state;
        private readonly ServerModuleScriptsState _scriptsState;
        private readonly IRegisterInitializable _registerInitializable;
        private readonly IRegisterUpdatable _registerUpdatable;
        private readonly IRegisterDisposable _registerDisposable;

        public AddServerModuleScriptCommandHandler(
            IMediator mediator,
            IIndexPool indexPool,
            ServerModuleState state,
            ServerModuleScriptsState scriptsState,
            IRegisterInitializable registerInitializable,
            IRegisterUpdatable registerUpdatable,
            IRegisterDisposable registerDisposable
        )
        {
            _mediator = mediator;
            _indexPool = indexPool;
            _state = state;
            _scriptsState = scriptsState;
            _registerInitializable = registerInitializable;
            _registerUpdatable = registerUpdatable;
            _registerDisposable = registerDisposable;
        }

        public async Task<StandardCommandResult> Handle(
            AddServerModuleScriptCommand notification,
            CancellationToken cancellationToken
        )
        {
            var serverModuleScripts = notification.Scripts;
            var initializeScript = await _mediator.Send(
                new QueryForClientScriptById(
                    serverModuleScripts.InitializeScript
                )
            );
            var disposeScript = await _mediator.Send(
                new QueryForClientScriptById(
                    serverModuleScripts.InitializeScript
                )
            );
            var updateScript = await _mediator.Send(
                new QueryForClientScriptById(
                    serverModuleScripts.InitializeScript
                )
            );
            _scriptsState.Add(
                serverModuleScripts
            );

            // Create new ServerModule
            var serverModule = new StandardServerModule(
                _indexPool.NextIndex(),
                serverModuleScripts.Name,
                new Option<IClientScript>(
                    initializeScript.Result
                ),
                new Option<IClientScript>(
                    disposeScript.Result
                ),
                new Option<IClientScript>(
                    updateScript.Result
                )
            );
            // Set and get any existing
            var current = _state.Set(
                serverModule
            );
            if (current.HasValue)
            {
                // If existing, dispose of ServerModule
                await _mediator.Send(
                    new DisposeOfServerModuleCommand(
                        current.Value.Name
                    )
                );
                // Set and get existing (again)
                current = _state.Set(
                    serverModule
                );
                if (current.HasValue)
                {
                    return new StandardCommandResult(
                        "not_able_to_create_new_server_module"
                    );
                }
            }

            await _registerInitializable.Register(
                serverModule
            );
            await _registerDisposable.Register(
                serverModule
            );
            if (updateScript.Success)
            {
                await _registerUpdatable.Register(
                    serverModule
                );
            }

            return new StandardCommandResult();
        }
    }
}
