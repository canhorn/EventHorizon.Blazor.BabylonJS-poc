namespace EventHorizon.Game.Client.Systems.EntityModule.Register
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Systems.EntityModule.Api;
    using EventHorizon.Game.Client.Systems.EntityModule.Model;
    using MediatR;

    public class RegisterAllPlayerModulesOnEntityCommandHandler
        : IRequestHandler<RegisterAllPlayerModulesOnEntityCommand, StandardCommandResult>
    {
        private readonly EntityPlayerScriptModuleState _state;

        public RegisterAllPlayerModulesOnEntityCommandHandler(
            EntityPlayerScriptModuleState state
        )
        {
            _state = state;
        }

        public Task<StandardCommandResult> Handle(
            RegisterAllPlayerModulesOnEntityCommand request, 
            CancellationToken cancellationToken
        )
        {
            foreach (var scriptModule in _state.All())
            {
                request.Entity.RegisterModule(
                    scriptModule.Name,
                    new StandardEntityModule(
                        request.Entity,
                        scriptModule
                    )
                );
            }

            return new StandardCommandResult()
                .FromResult();
        }
    }
}
