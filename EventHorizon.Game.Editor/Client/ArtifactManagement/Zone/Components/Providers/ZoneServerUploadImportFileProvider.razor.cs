namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Components.Providers;

using System.Threading.Tasks;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Model;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Open;
using EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Upload;
using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.Authentication.Model;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

public class ZoneServerUploadImportFileProviderBase
    : ObservableComponentBase,
        OpenZoneServerImportFileUploaderEventObserver
{
    [CascadingParameter]
    public AccessTokenModel AccessToken { get; set; } = null!;

    [Inject]
    public IJSRuntime JSRuntime { get; set; } = null!;

    [Inject]
    public AssetServerService AssetServerService { get; set; } = null!;

    public string UploadFileId { get; } = "zone-server-upload-import-input-file";
    public IJSObjectReference? FileUploadClickModule { get; private set; }
    public InputFile UploadInputFile { get; set; } = null!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        FileUploadClickModule = await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import",
            "./js/FileUploadClick.js"
        );
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        if (FileUploadClickModule.IsNotNull())
        {
            await FileUploadClickModule.DisposeAsync();
        }
    }

    public async Task TriggerOpenForFileUpload()
    {
        if (FileUploadClickModule.IsNotNull())
        {
            await FileUploadClickModule.InvokeVoidAsync("openInputElement", UploadFileId);
        }
    }

    public async Task Handle(OpenZoneServerImportFileUploaderEvent _)
    {
        await TriggerOpenForFileUpload();
    }

    protected async Task HandleInputFileChange(InputFileChangeEventArgs args)
    {
        await UploadFrom(args.File);
    }

    private async Task UploadFrom(IBrowserFile file)
    {
        if (AccessToken.IsFilled.IsNotTrue())
        {
            return;
        }

        var result = await Sender.Send(
            new UploadZoneServerImportFileCommand(AccessToken.AccessToken, file)
        );

        if (result)
        {
            await ShowMessage(
                Localizer["Zone Server Import"],
                Localizer["Successfully uploaded Import File."]
            );
            return;
        }
        else if (
            result.ErrorCode == ZoneServerUploadErrorCodes.ZONE_SERVER_UPLOAD_PAYLOAD_TOO_LARGE
        )
        {
            await ShowMessage(
                Localizer["Zone Server Import"],
                Localizer[
                    "Failed to Upload Import File, file was larger than {0}MB.",
                    ZoneServerUploadConstants.ZONE_SERVER_MAX_FILE_SIZE_IN_MEGABYTES
                ],
                MessageLevel.Error
            );
            return;
        }

        await ShowMessage(
            Localizer["Zone Server Import"],
            Localizer["Failed to Import File: ErrorCode = {0}", result.ErrorCode],
            MessageLevel.Error
        );
    }
}
