namespace EventHorizon.Game.Editor.Zone.Systems.Wizard.Query;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Services.Api;
using EventHorizon.Zone.Systems.Wizard.Model;
using EventHorizon.Zone.Systems.Wizard.Query;
using MediatR;

public class QueryForAllZoneWizardsHandler
    : IRequestHandler<QueryForAllZoneWizards, CommandResult<IEnumerable<WizardMetadata>>>
{
    private readonly ZoneAdminServices _zoneAdminServices;

    public QueryForAllZoneWizardsHandler(ZoneAdminServices zoneAdminServices)
    {
        _zoneAdminServices = zoneAdminServices;
    }

    public async Task<CommandResult<IEnumerable<WizardMetadata>>> Handle(
        QueryForAllZoneWizards request,
        CancellationToken cancellationToken
    )
    {
        var result = await _zoneAdminServices.Api.Wizard.All(cancellationToken);
        if (result.Success.IsNotTrue() || result.Result.IsNull())
        {
            return result.ErrorCode ?? ZoneAdminErrorCodes.BAD_API_REQUEST;
        }

        return result.Result;
    }
}
