namespace EventHorizon.Game.Editor.Client.AssetManagement.Delete
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Client.AssetManagement.Api;
    using EventHorizon.Game.Editor.Client.AssetManagement.Changed;
    using EventHorizon.Game.Editor.Client.AssetManagement.Model;
    using MediatR;

    public class AssetFileDeleteTriggeredEventHandler
        : INotificationHandler<AssetFileDeleteTriggeredEvent>
    {
        private readonly IMediator _mediator;
        private readonly AssetManagementState _assetManagementState;

        public AssetFileDeleteTriggeredEventHandler(
            IMediator mediator,
            AssetManagementState assetManagementState
        )
        {
            _mediator = mediator;
            _assetManagementState = assetManagementState;
        }

        public async Task Handle(
            AssetFileDeleteTriggeredEvent notification,
            CancellationToken cancellationToken
        )
        {
            await _assetManagementState.DeleteDirectoryContent(
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
