namespace EventHorizon.Game.Client.Systems.EntityModule.Register;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Systems.EntityModule.Api;
using EventHorizon.Game.Client.Systems.EntityModule.Model;

using MediatR;

public class RegisterAllBaseModulesOnEntityCommandHandler
    : IRequestHandler<RegisterAllBaseModulesOnEntityCommand,StandardCommandResult>
{
    private readonly EntityBaseScriptModuleState _state;

    public RegisterAllBaseModulesOnEntityCommandHandler(
        EntityBaseScriptModuleState state
    )
    {
        _state = state;
    }

    public Task<StandardCommandResult> Handle(
        RegisterAllBaseModulesOnEntityCommand request,
        CancellationToken cancellationToken
    )
    {
        request.Entity.RegisterModule(
            EntityBaseModuleManagementModule.MODULE_NAME,
            new StandardEntityBaseModuleManagementModule(request.Entity)
        );

        foreach (var scriptModule in _state.All())
        {
            request.Entity.RegisterModule(
                scriptModule.Name,
                new StandardEntityModule(request.Entity, scriptModule)
            );
        }

        return new StandardCommandResult().FromResult();
    }
}
