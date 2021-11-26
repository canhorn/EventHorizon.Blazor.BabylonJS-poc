namespace EventHorizon.Game.Editor.Client.AssetManagement.Trigger;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public struct TriggerAssetServerBackupCommand
    : IRequest<StandardCommandResult>
{
}
