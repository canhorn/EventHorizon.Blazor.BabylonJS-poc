namespace EventHorizon.Game.Client.Engine.Gui.Clear;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Client.Engine.Gui.Api;
using EventHorizon.Game.Client.Engine.Gui.Dispose;
using EventHorizon.Game.Client.Engine.Gui.Model;
using MediatR;

public class ClearGuiSystemDataCommandHandler
    : IRequestHandler<ClearGuiSystemDataCommand, StandardCommandResult>
{
    private readonly ISender _sender;
    private readonly IGuiLayoutDataState _guiLayoutDataState;
    private readonly IGuiDefinitionState _guiDefinitionState;

    public ClearGuiSystemDataCommandHandler(
        ISender sender,
        IGuiLayoutDataState guiLayoutDataState,
        IGuiDefinitionState guiDefinitionState
    )
    {
        _sender = sender;
        _guiLayoutDataState = guiLayoutDataState;
        _guiDefinitionState = guiDefinitionState;
    }

    public async Task<StandardCommandResult> Handle(
        ClearGuiSystemDataCommand request,
        CancellationToken cancellationToken
    )
    {
        _guiLayoutDataState.Clear();

        foreach (var guiDefinition in _guiDefinitionState.All.ToList())
        {
            await _sender.Send(new DisposeOfGuiCommand(guiDefinition.GuiId), cancellationToken);
        }

        return new StandardCommandResult();
    }
}
