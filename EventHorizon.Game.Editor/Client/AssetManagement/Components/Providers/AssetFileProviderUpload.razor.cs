namespace EventHorizon.Game.Editor.Client.AssetManagement.Components.Providers;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.AssetManagement.Model;
using EventHorizon.Game.Editor.Client.AssetManagement.Open;
using EventHorizon.Game.Editor.Client.AssetManagement.Reload;
using EventHorizon.Game.Editor.Client.Authentication.Model;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Components.TreeViewComponent.Model;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

public class AssetFileProviderUploadModel
    : ObservableComponentBase,
    AssetOpenFileUploadTrggeredEventObserver
{
    [CascadingParameter]
    public AssetManagementState State { get; set; } = null!;
    [CascadingParameter]
    public AccessTokenModel AccessToken { get; set; } = null!;

    [Inject]
    public IJSRuntime JSRuntime { get; set; } = null!;
    [Inject]
    public AssetFileManagement AssetFileManagement { get; set; } = null!;

    public string UploadFileId { get; } = "upload-input-file";
    public IJSObjectReference FileUploadClickModule { get; private set; } = null!;
    public InputFile UploadInputFile { get; set; } = null!;
    public TreeViewNodeData? FileUploadTreeViewNode { get; private set; }
    public FileSystemDirectoryContent? FileUploadWorkingDirectory { get; private set; }

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        FileUploadClickModule = await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import",
            "./js/FileUploadClick.js"
        );
    }

    public async Task TriggerOpenForFileUpload()
    {
        await FileUploadClickModule.InvokeVoidAsync(
            "openInputElement",
            UploadFileId
        );
    }

    public async Task Handle(
        AssetOpenFileUploadTrggeredEvent args
    )
    {
        FileUploadTreeViewNode = args.Node;
        FileUploadWorkingDirectory = args.DirectoryContent;
        await TriggerOpenForFileUpload();
    }

    protected async Task HandleInputFileChange(
        InputFileChangeEventArgs args
    )
    {
        if (FileUploadTreeViewNode is null
            || FileUploadWorkingDirectory is null)
        {
            await ShowMessage(
                Localizer["Asset File Upload"],
                Localizer["Invalid File Upload"],
                MessageLevel.Error
            );
            return;
        }

        await UploadFrom(
            args.File,
            FileUploadTreeViewNode,
            FileUploadWorkingDirectory
        );

        FileUploadTreeViewNode = null;
        FileUploadWorkingDirectory = null;
    }

    private async Task UploadFrom(
        IBrowserFile file,
        TreeViewNodeData node,
        FileSystemDirectoryContent directoryContent
    )
    {
        if (AccessToken.IsFilled.IsNotTrue())
        {
            return;
        }

        var result = await AssetFileManagement.Upload(
            AccessToken.AccessToken,
            file,
            FileSystemDirectoryContent.BuildPath(
                State.RootPath,
                directoryContent
            ),
            CancellationToken.None
        );

        if (result.Success)
        {
            await Mediator.Send(
                new AssetReloadToNodeAndDirectoryContentCommand(
                    node,
                    directoryContent
                )
            );
            await ShowMessage(
                Localizer["Asset File Upload"],
                Localizer["Successfully Uploaded"]
            );
            return;
        }

        await ShowMessage(
            Localizer["Asset File Upload"],
            Localizer[
                "Failed to Upload: Code = {0} | Message = '{1}'",
                result.Error?.Code ?? 500,
                Localizer[result.Error?.Message ?? "Server Exception"]
            ],
            MessageLevel.Error
        );
    }

    public override async ValueTask DisposeAsync()
    {
        await base.DisposeAsync();
        await FileUploadClickModule.DisposeAsync();
    }
}
