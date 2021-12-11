namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Trigger;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public record TriggerZoneServerArtifactBackupCommand()
    : IRequest<StandardCommandResult>;
