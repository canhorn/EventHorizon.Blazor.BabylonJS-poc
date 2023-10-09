namespace EventHorizon.Game.Editor.Client.AssetManagement.Components.Providers;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.AssetManagement.Model;
using EventHorizon.Game.Editor.Client.AssetManagement.New;
using EventHorizon.Game.Editor.Client.AssetManagement.Reload;
using EventHorizon.Game.Editor.Client.Authentication.Model;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Components.TreeViewComponent.Model;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;

using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

public class AssetNewFolderProviderModel
    : ObservableComponentBase,
        AssetNewFolderTrggeredEventObserver
{
    [CascadingParameter]
    public AccessTokenModel AccessToken { get; set; } = null!;

    [CascadingParameter]
    public AssetManagementState State { get; set; } = null!;

    [Inject]
    public IJSRuntime JSRuntime { get; set; } = null!;

    [Inject]
    public AssetFileManagement AssetFileManagement { get; set; } = null!;

    public IJSObjectReference FocusElementModule { get; private set; } = null!;
    protected bool IsOpen { get; set; }
    protected string FolderName { get; set; } = string.Empty;

    private TreeViewNodeData? _node;
    private FileSystemDirectoryContent? _directoryContent;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        FocusElementModule = await JSRuntime.InvokeAsync<IJSObjectReference>(
            "import",
            "./js/FocusElement.js"
        );
    }

    protected async Task HandleModalShown()
    {
        await FocusElementModule.InvokeVoidAsync(
            "focusElementBySelector",
            ".create-new-asset-folder__folder-name"
        );
    }

    protected async Task HandleNewFolderSubmit()
    {
        if (AccessToken.IsFilled.IsNotTrue())
        {
            await ShowMessage(
                Localizer["New Asset Folder"],
                Localizer[
                    "Unable to create new Asset Folder. (Code = [{0}] | Message = '{1}')",
                    "NewAssetFolder:AccessToken",
                    Localizer["Access token was not filled."]
                ],
                MessageLevel.Error
            );
            return;
        }
        else if (_node.IsNull())
        {
            await ShowMessage(
                Localizer["New Asset Folder"],
                Localizer[
                    "Unable to create new Asset Folder. (Code = [{0}] | Message = '{1}')",
                    "NewAssetFolder:Node",
                    Localizer["Node is null."]
                ],
                MessageLevel.Error
            );
            return;
        }
        else if (_directoryContent.IsNull())
        {
            await ShowMessage(
                Localizer["New Asset Folder"],
                Localizer[
                    "Unable to create new Asset Folder. (Code = [{0}] | Message = '{1}')",
                    "NewAssetFolder:DirectoryContent",
                    Localizer["Directory Content is null."]
                ],
                MessageLevel.Error
            );
            return;
        }

        var result = await AssetFileManagement.CreateDirectory(
            AccessToken.AccessToken,
            FileSystemDirectoryContent.BuildPath(
                State.RootPath,
                _directoryContent
            ),
            FolderName,
            CancellationToken.None
        );

        if (result.Error.IsNull())
        {
            await Mediator.Send(
                new AssetReloadToNodeAndDirectoryContentCommand(
                    _node,
                    _directoryContent
                )
            );
            await ShowMessage(
                Localizer["New Asset Folder"],
                Localizer["Successfully Created Folder"]
            );
            IsOpen = false;
            return;
        }

        await ShowMessage(
            Localizer["New Asset Folder"],
            Localizer[
                "Failed to create new Asset Folder. (Code = [{0}] | Message = '{1}')",
                result.Error?.Code ?? 500,
                result.Error?.Message ?? Localizer["Server Exception"]
            ],
            MessageLevel.Error
        );
    }

    public async Task Handle(AssetNewFolderTrggeredEvent args)
    {
        IsOpen = true;
        FolderName = string.Empty;

        _node = args.Node;
        _directoryContent = args.DirectoryContent;

        await InvokeAsync(StateHasChanged);
    }
}
