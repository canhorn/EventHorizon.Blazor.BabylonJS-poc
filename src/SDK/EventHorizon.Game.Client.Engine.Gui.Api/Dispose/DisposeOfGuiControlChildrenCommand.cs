namespace EventHorizon.Game.Client.Engine.Gui.Dispose;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public struct DisposeOfGuiControlChildrenCommand
    : IRequest<StandardCommandResult>
{
    public string ControlId { get; }

    public DisposeOfGuiControlChildrenCommand(string controlId)
    {
        ControlId = controlId;
    }
}
