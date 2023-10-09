namespace EventHorizon.Game.Client.Engine.Gui.Generate;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Engine.Gui.Api;

using MediatR;

public class GetGeneratedGuiControlIdHandler
    : IRequestHandler<GetGeneratedGuiControlId, string>
{
    private readonly IGuiControlState _state;

    public GetGeneratedGuiControlIdHandler(IGuiControlState state)
    {
        _state = state;
    }

    public Task<string> Handle(
        GetGeneratedGuiControlId request,
        CancellationToken cancellationToken
    ) => _state.GenerateId(request.GuiId, request.ControlId).FromResult();
}
