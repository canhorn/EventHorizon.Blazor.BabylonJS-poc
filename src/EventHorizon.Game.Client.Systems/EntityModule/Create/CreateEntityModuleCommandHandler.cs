namespace EventHorizon.Game.Client.Systems.EntityModule.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Core.Api;
    using EventHorizon.Game.Client.Engine.Scripting.Api;
    using EventHorizon.Game.Client.Engine.Scripting.Get;
    using EventHorizon.Game.Client.Systems.EntityModule.Api;
    using EventHorizon.Game.Client.Systems.EntityModule.Model;
    using MediatR;

    public class CreateEntityModuleCommandHandler
        : IRequestHandler<CreateEntityModuleCommand, CommandResult<IEntityModule>>
    {
        private readonly IMediator _mediator;
        private readonly IIndexPool _indexPool;

        public CreateEntityModuleCommandHandler(
            IMediator mediator,
            IIndexPool indexPool
        )
        {
            _mediator = mediator;
            _indexPool = indexPool;
        }

        public async Task<CommandResult<IEntityModule>> Handle(
            CreateEntityModuleCommand request, 
            CancellationToken cancellationToken
        )
        {
            var scripts = request.Scripts;
            var initializeScript = await GetClientScript(
                scripts.InitializeScript
            );
            var disposeScript = await GetClientScript(
                scripts.DisposeScript
            );
            var updateScript = await GetClientScript(
                scripts.UpdateScript
            );

            // Create new ServerModule
            var scriptModule = new StandardEntityScriptModule(
                _indexPool.NextIndex(),
                scripts.Name,
                new Option<IClientScript>(
                    initializeScript
                ),
                new Option<IClientScript>(
                    disposeScript
                ),
                new Option<IClientScript>(
                    updateScript
                ),
                request.ScriptData
            );

            return new CommandResult<IEntityModule>(
                scriptModule
            );
        }

        private async Task<IClientScript?> GetClientScript(
            string scriptId
        )
        {
            if (string.IsNullOrWhiteSpace(
                scriptId
            ))
            {
                return default;
            }
            var queryResult = await _mediator.Send(
                new QueryForClientScriptById(
                    scriptId
                )
            );
            if (!queryResult.Success)
            {
                return default;
            }

            return queryResult.Result;
        }
    }
}
