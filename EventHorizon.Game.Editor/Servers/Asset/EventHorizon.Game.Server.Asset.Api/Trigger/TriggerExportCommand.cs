namespace EventHorizon.Game.Server.Asset.Trigger
{
    using EventHorizon.Game.Client.Core.Command.Model;
    using EventHorizon.Game.Server.Asset.Model;
    using MediatR;

    public struct TriggerExportCommand
        : IRequest<CommandResult<ExportTriggerResult>>
    {
    }
}
