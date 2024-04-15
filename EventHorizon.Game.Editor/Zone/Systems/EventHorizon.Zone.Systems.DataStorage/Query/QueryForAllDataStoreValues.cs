namespace EventHorizon.Zone.Systems.DataStorage.Query;

using System.Collections.Generic;
using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;

public struct QueryForAllDataStoreValues : IRequest<CommandResult<IDictionary<string, object>>> { }
