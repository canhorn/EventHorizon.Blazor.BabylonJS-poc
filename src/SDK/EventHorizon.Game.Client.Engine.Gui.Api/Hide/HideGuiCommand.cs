namespace EventHorizon.Game.Client.Engine.Gui.Hide;

using EventHorizon.Game.Client.Core.Command.Model;

using MediatR;

public struct HideGuiCommand : IRequest<StandardCommandResult>
{
    public string Id { get; }

    public HideGuiCommand(string id)
    {
        Id = id;
    }
}
