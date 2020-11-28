namespace EventHorizon.Game.Editor.Zone.Services.ClientEntity.Delete
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using MediatR;

    public class DeleteClientEntityCommandHandler
        : IRequestHandler<DeleteClientEntityCommand, StandardCommandResult>
    {
        private readonly ZoneAdminServices _zoneAdminServices;

        public DeleteClientEntityCommandHandler(
            ZoneAdminServices zoneAdminServices
        )
        {
            _zoneAdminServices = zoneAdminServices;
        }

        public async Task<StandardCommandResult> Handle(
            DeleteClientEntityCommand request,
            CancellationToken cancellationToken
        )
        {
            var result = await _zoneAdminServices.Api.ClientEntity.Delete(
                request.ClientEntityId
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
