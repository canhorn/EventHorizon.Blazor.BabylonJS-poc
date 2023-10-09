namespace EventHorizon.Game.Client.Engine.Gui.Create;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Gui.Api;
using EventHorizon.Game.Client.Engine.Gui.Model;
using EventHorizon.Game.Client.Engine.Lifecycle.Register.Register;

using MediatR;

public class CreateGuiCommandHandler
    : IRequestHandler<CreateGuiCommand, StandardCommandResult>
{
    private readonly IMediator _mediator;
    private readonly IGuiLayoutDataState _layoutState;
    private readonly IGuiDefinitionState _guiState;

    public CreateGuiCommandHandler(
        IMediator mediator,
        IGuiLayoutDataState layoutState,
        IGuiDefinitionState guiState
    )
    {
        _mediator = mediator;
        _layoutState = layoutState;
        _guiState = guiState;
    }

    public async Task<StandardCommandResult> Handle(
        CreateGuiCommand request,
        CancellationToken cancellationToken
    )
    {
        var layoutDataOption = _layoutState.Get(request.LayoutId);
        if (!layoutDataOption.HasValue)
        {
            return new StandardCommandResult("layout_data_not_found");
        }

        var guiFromData = new GuiDefinitionFromData(
            request.GuiId,
            layoutDataOption.Value,
            request.ControlDataList,
            request.ParentControlId
        );
        _guiState.Set(guiFromData);
        await _mediator.Publish(
            new RegisterEntityEvent(guiFromData),
            cancellationToken
        );

        return new StandardCommandResult();
    }
}
