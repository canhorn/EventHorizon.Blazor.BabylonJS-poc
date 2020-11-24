namespace EventHorizon.Game.Editor.Zone.Services.Api
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Editor.Core.Services.Model;

    public interface ZoneAdminServices
    {
        ZoneAdminApi Api { get; }

        Task<StandardCommandResult> Connect(
            string accessToken,
            CoreZoneDetails zoneDetails,
            CancellationToken cancellationToken
        );
        Task<StandardCommandResult> Disconnect();
    }
}
