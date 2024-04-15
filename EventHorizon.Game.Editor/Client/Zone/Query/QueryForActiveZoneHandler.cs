namespace EventHorizon.Game.Editor.Client.Zone.Query;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.Zone.Api;
using MediatR;

public class QueryForActiveZoneHandler
    : IRequestHandler<QueryForActiveZone, CommandResult<ZoneState>>
{
    private readonly ZoneStateCache _cache;

    public QueryForActiveZoneHandler(ZoneStateCache cache)
    {
        _cache = cache;
    }

    public Task<CommandResult<ZoneState>> Handle(
        QueryForActiveZone request,
        CancellationToken cancellationToken
    )
    {
        if (_cache.Active == null)
        {
            return new CommandResult<ZoneState>("NO_ACTIVE_ZONE").FromResult();
        }

        return new CommandResult<ZoneState>(_cache.Active).FromResult();
    }
}
