namespace EventHorizon.Game.Client.Engine.Gui.Activate;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Gui.Api;
using MediatR;

public class ActivateGuiCommandHandler : IRequestHandler<ActivateGuiCommand, StandardCommandResult>
{
    private readonly IGuiDefinitionState _state;

    public ActivateGuiCommandHandler(IGuiDefinitionState state)
    {
        _state = state;
    }

    public Task<StandardCommandResult> Handle(
        ActivateGuiCommand request,
        CancellationToken cancellationToken
    )
    {
        var gui = _state.Get(request.GuiId);
        if (gui.HasValue)
        {
            gui.Value.Activate();
        }

        return new StandardCommandResult().FromResult();
    }
}
