namespace EventHorizon.Game.Client.Engine.Gui.Clear;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public record ClearGuiSystemDataCommand
    : IRequest<StandardCommandResult>;
