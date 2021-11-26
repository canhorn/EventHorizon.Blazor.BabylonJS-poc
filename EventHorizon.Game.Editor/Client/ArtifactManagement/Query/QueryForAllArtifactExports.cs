namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Query;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Components.Model;
using System.Collections.Generic;

using MediatR;

// TODO: Caching Point
public struct QueryForAllArtifactExports
    : IRequest<CommandResult<IEnumerable<ArtifactViewModel>>>
{
}
