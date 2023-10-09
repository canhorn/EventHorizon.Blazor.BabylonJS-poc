namespace EventHorizon.Game.Editor.Zone.Services.Agent.Create;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
using EventHorizon.Game.Editor.Zone.Services.Api;

using MediatR;

public class CreateAgentEntityCommandHandler
    : IRequestHandler<
        CreateAgentEntityCommand,
        CommandResult<IObjectEntityDetails>
    >
{
    private readonly ZoneAdminServices _zoneAdminServices;

    public CreateAgentEntityCommandHandler(ZoneAdminServices zoneAdminServices)
    {
        _zoneAdminServices = zoneAdminServices;
    }

    public async Task<CommandResult<IObjectEntityDetails>> Handle(
        CreateAgentEntityCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _zoneAdminServices.Api.Agent.CreateEntity(
            request.Entity
        );
        if (result.Success.IsNotTrue() || result.AgentEntity.IsNull())
        {
            return result.ErrorCode ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
        }

        return result.AgentEntity;
    }
}
