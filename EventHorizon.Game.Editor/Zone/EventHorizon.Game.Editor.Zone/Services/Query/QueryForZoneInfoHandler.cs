namespace EventHorizon.Game.Editor.Zone.Services.Query
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Zone.Services.Api;
    using EventHorizon.Game.Editor.Zone.Services.Model;
    using MediatR;

    public class QueryForZoneInfoHandler
        : IRequestHandler<QueryForZoneInfo, CommandResult<ZoneInfo>>
    {
        private readonly ZoneAdminServices _zoneAdminServices;

        public QueryForZoneInfoHandler(
            ZoneAdminServices zoneAdminServices
        )
        {
            _zoneAdminServices = zoneAdminServices;
        }

        public async Task<CommandResult<ZoneInfo>> Handle(
            QueryForZoneInfo request,
            CancellationToken cancellationToken
        )
        {
            return new(
                await _zoneAdminServices
                    .Api
                    .GetZoneInfo()
            );
        }
    }
}
