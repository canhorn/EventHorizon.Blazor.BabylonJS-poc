namespace EventHorizon.Zone.Systems.ArtifactManagement.Trigger;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Zone.Systems.ArtifactManagement.Model;
using MediatR;

public record TriggerZoneArtifactExportCommand()
    : IRequest<CommandResult<TriggerZoneArtifactExportResult>>;
