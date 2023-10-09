namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Query;

using System.Collections.Generic;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Components.Model;

using MediatR;

// TODO: Caching Point
public struct QueryForAllArtifactExports
    : IRequest<CommandResult<IEnumerable<ArtifactViewModel>>> { }
