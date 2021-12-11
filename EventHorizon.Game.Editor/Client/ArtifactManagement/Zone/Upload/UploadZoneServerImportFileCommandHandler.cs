namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Upload;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Model;
using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.AssetManagement.Model;

using MediatR;

public class UploadZoneServerImportFileCommandHandler
    : IRequestHandler<UploadZoneServerImportFileCommand, StandardCommandResult>
{
    private readonly IPublisher _publisher;
    private readonly AssetServerService _assetServerService;

    public UploadZoneServerImportFileCommandHandler(
        IPublisher publisher,
        AssetServerService assetServerService
    )
    {
        _publisher = publisher;
        _assetServerService = assetServerService;
    }

    public async Task<StandardCommandResult> Handle(
        UploadZoneServerImportFileCommand request,
        CancellationToken cancellationToken
    )
    {
        var result = await _assetServerService.UploadImportFile(
            request.AccessToken,
            "Zone".ToLowerInvariant(),
            request.File,
            cancellationToken
        );

        if (result.ErrorCode == AssetServerErrorCodes.ASSET_SERVER_PAYLOAD_TOO_LARGE)
        {
            return ZoneServerUploadErrorCodes.ZONE_SERVER_UPLOAD_PAYLOAD_TOO_LARGE;
        }

        await _publisher.Publish(
            new SuccessfullyUploadedZoneServerImportFileEvent(
                result.Result.Url
            ),
            cancellationToken
        );

        return new();
    }
}
