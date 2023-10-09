namespace EventHorizon.Game.Editor.Client.AssetManagement.Clicked;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.AssetManagement.Changed;

using MediatR;

public class AssetFileNodeClickedEventHandler
    : INotificationHandler<AssetFileNodeClickedEvent>
{
    private readonly IMediator _mediator;
    private readonly AssetManagementState _state;

    public AssetFileNodeClickedEventHandler(
        IMediator mediator,
        AssetManagementState state
    )
    {
        _mediator = mediator;
        _state = state;
    }

    public async Task Handle(
        AssetFileNodeClickedEvent notification,
        CancellationToken cancellationToken
    )
    {
        await _state.SetFileNode(notification.Node, cancellationToken);

        await _mediator.Publish(
            new AssetManagementStateChangedEvent(),
            cancellationToken
        );
    }
}
