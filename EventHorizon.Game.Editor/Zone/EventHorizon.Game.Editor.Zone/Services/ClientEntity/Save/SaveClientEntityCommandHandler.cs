namespace EventHorizon.Game.Editor.Zone.Services.ClientEntity.Save
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Engine.Systems.Entity.Api;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using MediatR;

    public class SaveClientEntityCommandHandler
        : IRequestHandler<SaveClientEntityCommand, CommandResult<IObjectEntityDetails>>
    {
        private readonly ZoneAdminServices _zoneAdminServices;

        public SaveClientEntityCommandHandler(
            ZoneAdminServices zoneAdminServices
        )
        {
            _zoneAdminServices = zoneAdminServices;
        }

        public async Task<CommandResult<IObjectEntityDetails>> Handle(
            SaveClientEntityCommand request,
            CancellationToken cancellationToken
        )
        {
            var result = await _zoneAdminServices.Api.ClientEntity.Save(
                request.Entity
            );
            if (result.Success.IsNotTrue()
                || result.ClientEntity.IsNull())
            {
                return result.ErrorCode
                    ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
            }

            return result.ClientEntity;
        }
    }
}
