namespace EventHorizon.Game.Client.Engine.Gui.Register;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Core.Exceptions;
using EventHorizon.Game.Client.Engine.Gui.Api;
using EventHorizon.Game.Client.Engine.Gui.Changed;
using MediatR;
using Microsoft.Extensions.Logging;

public class RegisterGuiLayoutDataCommandHandler
    : IRequestHandler<RegisterGuiLayoutDataCommand, StandardCommandResult>
{
    private readonly IMediator _mediator;
    private readonly IGuiLayoutDataState _state;

    public RegisterGuiLayoutDataCommandHandler(IMediator mediator, IGuiLayoutDataState state)
    {
        _mediator = mediator;
        _state = state;
    }

    public async Task<StandardCommandResult> Handle(
        RegisterGuiLayoutDataCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            _state.Set(request.LayoutData);

            await _mediator.Publish(new GuiLayoutDataChangedEvent(request.LayoutData.Id));

            return new StandardCommandResult();
        }
        catch (GameException ex)
        {
            GameServiceProvider
                .GetService<ILogger<RegisterGuiLayoutDataCommandHandler>>()
                .LogError(ex, "Game Exception");

            return new StandardCommandResult(ex.ErrorCode);
        }
        catch (Exception ex)
        {
            GameServiceProvider
                .GetService<ILogger<RegisterGuiLayoutDataCommandHandler>>()
                .LogError(ex, "General Exception");

            return new StandardCommandResult("general_exception");
        }
    }
}
