namespace EventHorizon.Game.Editor.Client.Zone.Components.FileExplorer;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.Authentication.Api;
using EventHorizon.Game.Editor.Client.Authentication.Set;
using EventHorizon.Game.Editor.Client.Localization;
using EventHorizon.Game.Editor.Client.Localization.Api;
using EventHorizon.Game.Editor.Client.Shared.Components.TreeViewComponent.Model;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Client.Zone.Components.FileExplorer.Model;
using EventHorizon.Game.Editor.Client.Zone.Query;
using EventHorizon.Game.Editor.Client.Zone.Reload;
using EventHorizon.Game.Editor.Zone.Editor.Services.Create;
using EventHorizon.Game.Editor.Zone.Editor.Services.Delete;
using EventHorizon.Game.Editor.Zone.Editor.Services.Model;

using MediatR;

using Microsoft.AspNetCore.Components;

public class EditorFileExplorerModel
    : ComponentBase
{
    [CascadingParameter]
    public ZoneState ZoneState { get; set; } = null!;
    [CascadingParameter]
    public SessionValues SessionValues { get; set; } = null!;

    [Inject]
    public IMediator Mediator { get; set; } = null!;
    [Inject]
    public Localizer<SharedResource> Localizer { get; set; } = null!;

    public bool IsEnabled { get; set; }

    public TreeViewNodeData EditorTreeView { get; set; } = new TreeViewNodeData();

    public EditorFileExplorerModalState ModalState { get; set; } = new EditorFileExplorerModalState();

    protected override Task OnInitializedAsync()
    {
        return HandleZoneStateChange();
    }

    protected override Task OnParametersSetAsync()
    {
        return HandleZoneStateChange();
    }

    public async Task HandleTreeViewChanged()
    {
        // Save Session Value for Currently expanded Nodes
        var expandedList = new List<string>();
        FillExpandedList(
            EditorTreeView,
            ref expandedList
        );
        await Mediator.Send(
            new SetSessionValueCommand(
                "editorFileExplorer",
                string.Join(
                    ',',
                    expandedList
                )
            )
        );
    }

    private void FillExpandedList(
        TreeViewNodeData editorTreeView,
        ref List<string> expandedList
    )
    {
        if (editorTreeView.IsExpanded)
        {
            expandedList.Add(
                editorTreeView.Id
            );
        }
        foreach (var child in editorTreeView.Children)
        {
            FillExpandedList(
                child,
                ref expandedList
            );
        }
    }

    private async Task HandleZoneStateChange()
    {
        var expandedList = SessionValues.Get(
            "editorFileExplorer",
            ""
        ).Split(",");

        var result = await Mediator.Send(
            new QueryForActiveEditorNodeTreeView(
                EditorTreeView,
                expandedList,
                HandleOpenWithModalType
            )
        );
        if (!result.Success)
        {
            // TODO: Show Error
            return;
        }
        EditorTreeView = result.Result;
        IsEnabled = true;
        await InvokeAsync(StateHasChanged);
    }

    private void HandleOpenWithModalType(
        EditorNode node,
        EditorFileModalType modalType,
        bool triggerInputFocus = false,
        bool triggerButtonFocus = false
    )
    {
        ModalState.Reset();

        ModalState.Node = node;
        ModalState.ModalType = modalType;
        ModalState.IsOpen = true;
        ModalState.TriggerInputFocus = triggerInputFocus;
        ModalState.TriggerButtonFocus = triggerButtonFocus;
        InvokeAsync(StateHasChanged);
    }

    public async Task SubmitModal()
    {
        switch (ModalState.ModalType)
        {
            case EditorFileModalType.AddFolder:
                await CreateNewFolder(
                    ModalState.Node
                );
                break;
            case EditorFileModalType.DeleteFolder:
                await DeleteFolder(
                    ModalState.Node
                );
                break;
            case EditorFileModalType.AddFile:
                await CreateNewFile(
                    ModalState.Node
                );
                break;
            case EditorFileModalType.DeleteFile:
                await DeleteFile(
                    ModalState.Node
                );
                break;
            default:
                break;
        }
    }

    public void CloseModal()
    {
        ModalState.IsOpen = false;
    }

    private async Task CreateNewFile(
        EditorNode node
    )
    {
        // Path
        var path = node.Path.ToList();
        if (node.IsFolder)
        {
            path.Add(node.Name);
        }
        // FileName
        var fileName = ModalState.TextInput;

        await ValidateResponse(
            await Mediator.Send(
                new CreateNewZoneEditorFileCommand(
                    fileName,
                    path
                )
            )
        );
    }

    private async Task CreateNewFolder(
        EditorNode node
    ) => await ValidateResponse(
        await Mediator.Send(
            new CreateNewZoneEditorFolderCommand(
                // FolderName
                ModalState.TextInput,
                // Path
                new List<string>(
                    node.Path
                )
                {
                        node.Name
                }
            )
        )
    );

    private async Task DeleteFile(
        EditorNode node
    ) => await ValidateResponse(
        await Mediator.Send(
            new DeleteZoneEditorFileCommand(
                // FolderName
                node.Name,
                // Path
                new List<string>(
                    node.Path
                )
            )
        )
    );

    private async Task DeleteFolder(
        EditorNode folderNode
    ) => await ValidateResponse(
        await Mediator.Send(
            new DeleteZoneEditorFolderCommand(
                // FolderName
                folderNode.Name,
                // Path
                folderNode.Path.ToList()
            )
        )
    );

    private async Task ValidateResponse(
        EditorResponse response
    )
    {
        if (response.Successful)
        {
            ModalState.IsOpen = false;
            var result = await Mediator.Send(
                new ReloadActiveZoneStateCommand()
            );
            if (!result.Success)
            {
                ModalState.ErrorMessage = Localizer[
                    "Failed to Reload Active State: {0}",
                    Localizer[
                        result.ErrorCode
                    ]
                ];
            }
        }
        else
        {
            ModalState.ErrorMessage = Localizer[
                "Error received from Server: {0}",
                Localizer[
                    response.ErrorCode
                ]
            ];
        }
    }
}
