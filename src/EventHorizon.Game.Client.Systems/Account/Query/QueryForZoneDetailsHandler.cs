namespace EventHorizon.Game.Client.Systems.Account.Query
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Client.Systems.Account.Api;
    using MediatR;

    public class QueryForZoneDetailsHandler
        : IRequestHandler<QueryForZoneDetails, CommandResult<IZoneDetails>>
    {
        private readonly IAccountState _state;

        public QueryForZoneDetailsHandler(
            IAccountState state
        )
        {
            _state = state;
        }

        public Task<CommandResult<IZoneDetails>> Handle(
            QueryForZoneDetails request, 
            CancellationToken cancellationToken
        )
        {
            var zoneDetails = _state.User?.Zone;
            if (zoneDetails == null)
            {
                return new CommandResult<IZoneDetails>(
                    "zone_details_not_found"
                ).FromResult();
            }
            return new CommandResult<IZoneDetails>(
                zoneDetails
            ).FromResult();
        }
    }
}
