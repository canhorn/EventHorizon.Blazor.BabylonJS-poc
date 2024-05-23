namespace EventHorizon.Game.Editor.Client.Zone.Components.FileEditor;

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
    public required ZoneState ZoneState { get; set; }

    [Parameter]
    public required string EncodedFileNodeId { get; set; }

    [Parameter]
    public required RenderFragment ChildContent { get; set; }

    public ComponentState DisplayState { get; set; } = ComponentState.Loading;
    public bool IsCompiling { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public FileEditorState State { get; set; } = new FileEditorState();

    public CancellationTokenSource _cancellationTokenSource = new();
    private bool _runningSetup = false;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        if (ZoneState.IsLoading || ZoneState.IsPendingReload)
        {
            return;
        }
        await Setup();
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();

        if (StateChanged())
        {
            return;
        }

        await Setup();
    }

    public async Task Handle(SavedEditorFileContentSuccessfulyEvent _)
    {
        DisplayState = ComponentState.Loading;
        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(ActiveZoneStateChangedEvent args)
    {
        if (StateChanged())
        {
            return;
        }

        await Setup();
        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(ServerScriptsSystemCompilingScriptsClientAction args)
    {
        IsCompiling = true;
        DisplayState = ComponentState.Loading;

        await InvokeAsync(StateHasChanged);
    }

    public async Task Handle(ServerScriptsSystemFinishedScriptsCompileClientAction args)
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

    private async Task Setup()
    {
        if (_runningSetup)
        {
            return;
        }

        var id = EncodedFileNodeId.Base64Decode();
        _runningSetup = true;
        DisplayState = ComponentState.Loading;
        ErrorMessage = string.Empty;
        var editorNodeResult = await Mediator.Send(
            new QueryForEditorNodeById(id),
            _cancellationTokenSource.Token
        );
        if (ZoneState.IsLoading || ZoneState.IsPendingReload)
        {
            _runningSetup = false;
            return;
        }
        else if (!editorNodeResult && IsLoadingErrorCode(editorNodeResult.ErrorCode))
        {
            _runningSetup = false;
            return;
        }
        else if (!editorNodeResult)
        {
            ErrorMessage = Localizer["File was not found."];
            DisplayState = ComponentState.Error;
            _runningSetup = false;
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
            _runningSetup = false;
            return;
        }

        var editorFileErrorDetails = new EditorFileErrorDetails(id).From(
            ZoneState
                .ScriptErrorDetails.ScriptErrorDetailsList?.Where(a => id.Contains(a.ScriptId))
                .FirstOrDefault()
        );

        State = new FileEditorState
        {
            EditorFile = result.Result,
            EditorNode = editorNode,
            FileErrorDetails = editorFileErrorDetails,
        };

        DisplayState = ComponentState.Content;
        if (IsCompiling || ZoneState.IsLoading || ZoneState.IsPendingReload)
        {
            DisplayState = ComponentState.Loading;
        }

        _cancellationTokenSource = new CancellationTokenSource();
        _runningSetup = false;
    }

    private bool StateChanged()
    {
        return DisplayState == ComponentState.Content
            && !ZoneState.IsPendingReload
            && !ZoneState.IsLoading
            && EncodedFileNodeId.Base64Decode() == State.EditorFile?.Id;
    }
}
