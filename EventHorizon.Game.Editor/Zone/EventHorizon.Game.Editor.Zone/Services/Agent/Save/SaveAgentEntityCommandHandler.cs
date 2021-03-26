namespace EventHorizon.Game.Editor.Zone.Services.Agent.Save
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using MediatR;

    public class SaveAgentEntityCommandHandler
        : IRequestHandler<SaveAgentEntityCommand, CommandResult<IObjectEntityDetails>>
    {
        private readonly ZoneAdminServices _zoneAdminServices;

        public SaveAgentEntityCommandHandler(
            ZoneAdminServices zoneAdminServices
        )
        {
            _zoneAdminServices = zoneAdminServices;
        }

        public async Task<CommandResult<IObjectEntityDetails>> Handle(
            SaveAgentEntityCommand request,
            CancellationToken cancellationToken
        )
        {
            var result = await _zoneAdminServices.Api.Agent.SaveEntity(
                request.Entity
            );
            if (result.Success.IsNotTrue()
                || result.AgentEntity.IsNull())
            {
                return result.ErrorCode
                    ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
            }

            return result.AgentEntity;
        }
    }
}
