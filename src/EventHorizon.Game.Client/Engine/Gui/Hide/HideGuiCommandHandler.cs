namespace EventHorizon.Game.Client.Engine.Gui.Hide;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Gui.Api;
using MediatR;

public class HideGuiCommandHandler : IRequestHandler<HideGuiCommand, StandardCommandResult>
{
    private readonly IGuiDefinitionState _state;

    public HideGuiCommandHandler(IGuiDefinitionState state)
    {
        _state = state;
    }

    public Task<StandardCommandResult> Handle(
        HideGuiCommand request,
        CancellationToken cancellationToken
    )
    {
        var gui = _state.Get(request.Id);
        if (gui.HasValue)
        {
            gui.Value.Hide();
        }

        return new StandardCommandResult().FromResult();
    }
}
