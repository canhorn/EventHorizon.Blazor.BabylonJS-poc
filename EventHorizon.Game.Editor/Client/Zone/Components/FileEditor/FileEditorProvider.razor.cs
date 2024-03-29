﻿namespace EventHorizon.Game.Editor.Client.Zone.Components.FileEditor;

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Components.Containers;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Client.Zone.Change;
using EventHorizon.Game.Editor.Client.Zone.ClientActions.Reload;
using EventHorizon.Game.Editor.Client.Zone.Components.FileEditor.Model;
using EventHorizon.Game.Editor.Client.Zone.Model;
using EventHorizon.Game.Editor.Client.Zone.Query;
using EventHorizon.Game.Editor.Zone.Editor.Services.Query;
using EventHorizon.Game.Editor.Zone.Editor.Services.Save;

using Microsoft.AspNetCore.Components;

public class FileEditorProviderModel
    : ObservableComponentBase,
        ActiveZoneStateChangedEventObserver,
        SavedEditorFileContentSuccessfulyEventObserver,
        ServerScriptsSystemCompilingScriptsClientActionObserver,
        ServerScriptsSystemFinishedScriptsCompileClientActionObserver
{
    private static bool IsLoadingErrorCode(string errorCode) =>
        errorCode == ZoneClientEditorErrorCodes.ZONE_STATE_PENDING_RELOAD
        || errorCode == ZoneClientEditorErrorCodes.ZONE_STATE_IS_LOADING;

    [CascadingParameter]
    public ZoneState ZoneState { get; set; } = null!;

    [Parameter]
    public string EncodedFileNodeId { get; set; } = string.Empty;

    [Parameter]
    public RenderFragment ChildContent { get; set; } = null!;

    public ComponentState DisplayState { get; set; } = ComponentState.Loading;
    public bool IsCompiling { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public FileEditorState State { get; set; } = new FileEditorState();

    public CancellationTokenSource _cancellationTokenSource = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        await Setup();
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (
            !ZoneState.IsPendingReload
            && !ZoneState.IsLoading
            && EncodedFileNodeId.Base64Decode() == State.EditorFile?.Id
        )
        {
            return;
        }

        await Setup();
    }

    private async Task Setup()
    {
        var id = EncodedFileNodeId.Base64Decode();
        DisplayState = ComponentState.Loading;
        ErrorMessage = string.Empty;
        var editorNodeResult = await Mediator.Send(
            new QueryForEditorNodeById(id),
            _cancellationTokenSource.Token
        );
        if (ZoneState.IsLoading || ZoneState.IsPendingReload)
        {
            return;
        }
        else if (
            !editorNodeResult && IsLoadingErrorCode(editorNodeResult.ErrorCode)
        )
        {
            return;
        }
        else if (!editorNodeResult)
        {
            ErrorMessage = Localizer["File was not found."];
            DisplayState = ComponentState.Error;
            return;
        }

        var editorNode = editorNodeResult.Result;
        var result = await Mediator.Send(
            new QueryForEditorFile(editorNode.Path, editorNode.Name),
            _cancellationTokenSource.Token
        );
        if (!result)
        {
            ErrorMessage = Localizer[
                "Editor File Content retrieval failed with Error Code: {0}",
                result.ErrorCode
            ];
            DisplayState = ComponentState.Error;
            return;
        }

        var editorFileErrorDetails = new EditorFileErrorDetails(id).From(
            ZoneState.ScriptErrorDetails.ScriptErrorDetailsList
                ?.Where(a => id.Contains(a.ScriptId))
                .FirstOrDefault()
        );

        State = new FileEditorState
        {
            EditorFile = result.Result,
            EditorNode = editorNode,
            FileErrorDetails = editorFileErrorDetails,
        };

        DisplayState = ComponentState.Content;
        if (IsCompiling)
        {
            DisplayState = ComponentState.Loading;
        }
        _cancellationTokenSource = new CancellationTokenSource();
    }

    public async Task Handle(SavedEditorFileContentSuccessfulyEvent _)
    {
        DisplayState = ComponentState.Loading;
        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(ActiveZoneStateChangedEvent args)
    {
        await Setup();
        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(
        ServerScriptsSystemCompilingScriptsClientAction args
    )
    {
        IsCompiling = true;
        DisplayState = ComponentState.Loading;

        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(
        ServerScriptsSystemFinishedScriptsCompileClientAction args
    )
    {
        IsCompiling = false;
        DisplayState = ComponentState.Content;

        await InvokeAsync(StateHasChanged);
    }

    public string GetLoadingText()
    {
        if (IsCompiling)
        {
            return Localizer["Compiling..."];
        }

        return Localizer["Loading..."];
    }
}
