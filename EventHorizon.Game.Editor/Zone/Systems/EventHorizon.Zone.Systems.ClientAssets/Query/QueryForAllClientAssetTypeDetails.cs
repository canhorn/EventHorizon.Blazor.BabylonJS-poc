namespace EventHorizon.Zone.Systems.ClientAssets.Query;

using System.Collections.Generic;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Zone.Systems.ClientAssets.Model;
using MediatR;

public struct QueryForAllClientAssetTypeDetails
    : IRequest<CommandResult<IEnumerable<ClientAssetTypeDetails>>> { }
