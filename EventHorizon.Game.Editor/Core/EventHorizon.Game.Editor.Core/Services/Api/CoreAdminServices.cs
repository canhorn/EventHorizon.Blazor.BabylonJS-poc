namespace EventHorizon.Game.Editor.Core.Services.Api;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Core.Services.Model;

public interface CoreAdminServices
{
    Task<StandardCommandResult> Connect(
        string accessToken,
        CancellationToken cancellationToken
    );

    Task<IList<CoreZoneDetails>> GetAllZones();
}
