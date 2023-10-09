namespace EventHorizon.Game.Editor.Core.Services.Query;

using System;
using System.Collections.Generic;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Core.Services.Model;

using MediatR;

public class QueryForAllZoneDetails
    : IRequest<CommandResult<IEnumerable<CoreZoneDetails>>> { }
