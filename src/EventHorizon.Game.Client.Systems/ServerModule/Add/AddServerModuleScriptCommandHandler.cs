namespace EventHorizon.Game.Client.Systems.ServerModule.Add
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Lifecycle.Register.Api;
    using EventHorizon.Game.Client.Systems.EntityModule.Create;
    using EventHorizon.Game.Client.Systems.ServerModule.Api;
    using EventHorizon.Game.Client.Systems.ServerModule.Dispose;
    using MediatR;

    public class AddServerModuleScriptCommandHandler
        : IRequestHandler<AddServerModuleScriptCommand, StandardCommandResult>
    {
        private readonly IMediator _mediator;
        private readonly ServerModuleState _state;
        private readonly ServerModuleScriptsState _scriptsState;
        private readonly IRegisterInitializable _registerInitializable;
        private readonly IRegisterUpdatable _registerUpdatable;
        private readonly IRegisterDisposable _registerDisposable;

        public AddServerModuleScriptCommandHandler(
            IMediator mediator,
            ServerModuleState state,
            ServerModuleScriptsState scriptsState,
            IRegisterInitializable registerInitializable,
            IRegisterUpdatable registerUpdatable,
            IRegisterDisposable registerDisposable
        )
        {
            _mediator = mediator;
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
            _scriptsState.Add(
                serverModuleScripts
            );

            var serverModuleResult = await _mediator.Send(
                new CreateEntityModuleCommand(
                    serverModuleScripts
                ),
                CancellationToken.None
            );
            if (!serverModuleResult.Success)
            {
                return new StandardCommandResult(
                    "failed_to_create_server_module"
                );
            }
            var serverModule = serverModuleResult.Result;
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
            if (serverModule.IsUpdatable)
            {
                await _registerUpdatable.Register(
                    serverModule
                );
            }

            return new StandardCommandResult();
        }
    }
}
