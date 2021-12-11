namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Start;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public record StartZoneServerArtifactImportCommand()
    : IRequest<StandardCommandResult>;
