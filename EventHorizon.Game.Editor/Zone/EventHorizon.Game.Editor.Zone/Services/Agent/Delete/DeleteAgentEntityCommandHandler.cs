namespace EventHorizon.Game.Editor.Zone.Services.Agent.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using MediatR;

    public class DeleteAgentEntityCommandHandler
        : IRequestHandler<DeleteAgentEntityCommand, StandardCommandResult>
    {
        private readonly ZoneAdminServices _zoneAdminServices;

        public DeleteAgentEntityCommandHandler(
            ZoneAdminServices zoneAdminServices
        )
        {
            _zoneAdminServices = zoneAdminServices;
        }

        public async Task<StandardCommandResult> Handle(
            DeleteAgentEntityCommand request,
            CancellationToken cancellationToken
        )
        {
            var result = await _zoneAdminServices.Api.Agent.DeleteEntity(
                request.EntityId
            );
            if (result.Success.IsNotTrue())
            {
                return new(
                    result.ErrorCode
                );
            }

            return new();
        }
    }
}
