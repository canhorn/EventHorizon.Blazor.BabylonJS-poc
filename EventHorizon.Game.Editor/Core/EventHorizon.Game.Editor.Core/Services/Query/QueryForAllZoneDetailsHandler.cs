namespace EventHorizon.Game.Editor.Core.Services.Query;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Core.Services.Api;
using EventHorizon.Game.Editor.Core.Services.Model;

using MediatR;

public class QueryForAllZoneDetailsHandler
    : IRequestHandler<
        QueryForAllZoneDetails,
        CommandResult<IEnumerable<CoreZoneDetails>>
    >
{
    private readonly CoreAdminServices _coreAdminServices;

    public QueryForAllZoneDetailsHandler(CoreAdminServices coreAdminServices)
    {
        _coreAdminServices = coreAdminServices;
    }

    public async Task<CommandResult<IEnumerable<CoreZoneDetails>>> Handle(
        QueryForAllZoneDetails request,
        CancellationToken cancellationToken
    )
    {
        return new CommandResult<IEnumerable<CoreZoneDetails>>(
            await _coreAdminServices.GetAllZones()
        );
    }
}
