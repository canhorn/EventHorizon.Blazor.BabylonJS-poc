namespace EventHorizon.Game.Editor.Zone.Editor.Services.Delete
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Api;
    using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
    using MediatR;
    using Microsoft.Extensions.Logging;

    public class DeleteZoneEditorFolderCommandHandler
        : IRequestHandler<DeleteZoneEditorFolderCommand, EditorResponse>
    {
        private readonly ILogger _logger;
        private readonly ZoneEditorServices _zoneEditorServices;

        public DeleteZoneEditorFolderCommandHandler(
            ILogger<DeleteZoneEditorFolderCommandHandler> logger,
            ZoneEditorServices zoneEditorServices
        )
        {
            _logger = logger;
            _zoneEditorServices = zoneEditorServices;
        }

        public Task<EditorResponse> Handle(
            DeleteZoneEditorFolderCommand request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                return _zoneEditorServices.Api.DeleteEditorFolder(
                    request.Path,
                    request.FolderName
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Failed to Delete Zone Editor Folder",
                    request
                );
            }
            return new EditorResponse
            {
                Successful = false,
                ErrorCode = ZoneEditorErrorCodes.FAILED_DELETE_ZONE_EDITOR_FOLDER,
            }.FromResult();
        }
    }
}
