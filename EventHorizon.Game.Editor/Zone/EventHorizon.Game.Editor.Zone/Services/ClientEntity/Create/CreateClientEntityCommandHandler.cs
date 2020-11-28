namespace EventHorizon.Game.Editor.Zone.Services.ClientEntity.Create
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using MediatR;

    public class CreateClientEntityCommandHandler
        : IRequestHandler<CreateClientEntityCommand, CommandResult<IObjectEntityDetails>>
    {
        private readonly ZoneAdminServices _zoneAdminServices;

        public CreateClientEntityCommandHandler(
            ZoneAdminServices zoneAdminServices
        )
        {
            _zoneAdminServices = zoneAdminServices;
        }

        public async Task<CommandResult<IObjectEntityDetails>> Handle(
            CreateClientEntityCommand request,
            CancellationToken cancellationToken
        )
        {
            var result = await _zoneAdminServices.Api.ClientEntity.Create(
                request.Entity
            );
            if (result.Success.IsNotTrue())
            {
                return new(
                    result.ErrorCode
                );
            }

            return new(
                result.ClientEntity
            );
        }
    }
}
