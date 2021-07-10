namespace EventHorizon.Game.Editor.Client.AssetManagement.Clicked
{
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.AssetManagement.Api;
    using EventHorizon.Game.Editor.Client.AssetManagement.Changed;
    using MediatR;

    public class AssetFileDirectoryContentClickedEventHandler
        : INotificationHandler<AssetFileDirectoryContentClickedEvent>
    {
        private readonly IMediator _mediator;
        private readonly AssetManagementState _state;

        public AssetFileDirectoryContentClickedEventHandler(
            IMediator mediator,
            AssetManagementState state
        )
        {
            _mediator = mediator;
            _state = state;
        }

        public async Task Handle(
            AssetFileDirectoryContentClickedEvent notification,
            CancellationToken cancellationToken
        )
        {
            await _state.SetFileDirectoryContent(
                notification.DirectoryContent,
                cancellationToken
            );

            await _mediator.Publish(
                new AssetManagementStateChangedEvent(),
                cancellationToken
            );
        }
    }
}
