namespace EventHorizon.Game.Editor.Client.Zone.Components.FileEditor;

using System;
using System.Threading.Tasks;

using Blazored.LocalStorage;

using BlazorMonaco;
using BlazorMonaco.Editor;

using EventHorizon.Game.Client.Core.Factory.Api;
using EventHorizon.Game.Client.Core.Timer.Api;
using EventHorizon.Game.Editor.Client.Shared.Components;
using EventHorizon.Game.Editor.Client.Shared.Components.Containers;
using EventHorizon.Game.Editor.Client.Shared.Toast.Model;
using EventHorizon.Game.Editor.Client.Zone.Api;
using EventHorizon.Game.Editor.Client.Zone.Components.FileEditor.Model;
using EventHorizon.Game.Editor.Zone.Editor.Services.Save;

using Microsoft.AspNetCore.Components;

public class ZoneFileEditorComponentModel
    : EditorComponentBase,
        IAsyncDisposable
{
    private static string ZoneFileEditorPendingValueKey(string id) =>
        $"ZoneFileEditorComponent:{id}:PendingValue";

    [CascadingParameter]
    public ZoneState ZoneState { get; set; } = null!;

    [CascadingParameter]
    public FileEditorState FileEditorState { get; set; } = null!;

    [Inject]
    public ILocalStorageService LocalStorage { get; set; } = null!;

    [Inject]
    public IFactory<IIntervalTimerService> IntervalTimerServiceFactory { get; set; } =
        null!;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _savePendingChangesIntervalTimerService = IntervalTimerServiceFactory
            .Create()
            .Setup(1000, HandleSavePendingChanges)
            .Start();
    }

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        CompilerErrorsDisplayState = ComponentState.Loading;
        FileErrorDetailsDisplayState = ComponentState.Loading;

        if (
            MonacoEditor.IsNotNull()
            && FileEditorState.EditorNode.IsNotNull()
            && FileEditorState.EditorFile.IsNotNull()
        )
        {
            await MonacoEditor.SetValue(await GetEditorValue());
        }

        if (ZoneState.ScriptErrorDetails.HasErrors)
        {
            CompilerErrorsDisplayState = ComponentState.Content;
        }
        if (FileEditorState.FileErrorDetails.HasError)
        {
            FileErrorDetailsDisplayState = ComponentState.Content;
        }
    }

    public ValueTask DisposeAsync()
    {
        _savePendingChangesIntervalTimerService?.Dispose();

        return ValueTask.CompletedTask;
    }

    public async Task HandleSaveFile()
    {
        _savePendingChangesIntervalTimerService?.Pause();
        var value = await MonacoEditor.GetValue();
        var result = await Mediator.Send(
            new SaveEditorFileContentCommand(
                FileEditorState.EditorNode.Path,
                FileEditorState.EditorNode.Name,
                value
            )
        );
        if (result.Successful.IsNotTrue())
        {
            await ShowMessage(
                Localizer["File Save Status"],
                Localizer[
                    "File failed to save, Error Code of '{0}'",
                    result.ErrorCode
                ],
                MessageLevel.Error
            );
            return;
        }
        _savePendingChangesIntervalTimerService?.Start();
        await LocalStorage.RemoveItemAsync(
            ZoneFileEditorPendingValueKey(FileEditorState.EditorNode.Id)
        );
        await ShowMessage(
            Localizer["File Save Status"],
            Localizer["File was Successfully Saved"]
        );
    }

    #region Pending Changes
    private IIntervalTimerService? _savePendingChangesIntervalTimerService;
    private string _pendingSaveValue = string.Empty;

    protected ComponentState FileChangesPendingDisplayState { get; set; } =
        ComponentState.Loading;

    private async Task HandleSavePendingChanges()
    {
        var editorValue = await MonacoEditor.GetValue();
        if (FileEditorState.EditorFile.Content == editorValue)
        {
            return;
        }

        await LocalStorage.SetItemAsync(
            ZoneFileEditorPendingValueKey(FileEditorState.EditorNode.Id),
            new EditorFilePendingContent(editorValue ?? string.Empty)
        );
        FileChangesPendingDisplayState = ComponentState.Content;

        await InvokeAsync(StateHasChanged);
    }

    public async Task HandleClearFileChanges()
    {
        _pendingSaveValue = FileEditorState.EditorFile.Content;
        FileChangesPendingDisplayState = ComponentState.Loading;
        await MonacoEditor.SetValue(FileEditorState.EditorFile.Content);
        await LocalStorage.RemoveItemAsync(
            ZoneFileEditorPendingValueKey(FileEditorState.EditorNode.Id)
        );
    }
    #endregion

    #region Editor
    public StandaloneCodeEditor MonacoEditor { get; set; } = null!;

    public async Task HandleDidInit()
    {
        await MonacoEditor.AddCommand(
            (int)KeyMod.CtrlCmd | (int)KeyMod.Shift | (int)KeyCode.KeyS,
            (args) =>
            {
                _ = HandleSaveFile();
            }
        );
        await MonacoEditor.SetValue(await GetEditorValue());
    }

    private async Task<string> GetEditorValue()
    {
        if (_pendingSaveValue.IsNullOrEmpty())
        {
            _pendingSaveValue =
                (
                    await LocalStorage.GetItemAsync<EditorFilePendingContent>(
                        ZoneFileEditorPendingValueKey(
                            FileEditorState.EditorNode.Id
                        )
                    )
                )?.Content ?? string.Empty;
        }

        var value = FileEditorState.EditorFile.Content;
        if (
            _pendingSaveValue.IsNotNullOrEmpty()
            && _pendingSaveValue != FileEditorState.EditorFile.Content
        )
        {
            value = _pendingSaveValue;
            await ShowMessage(
                Localizer["Editor File"],
                Localizer["Pending changes for this file were found."],
                MessageLevel.Warning
            );
            FileChangesPendingDisplayState = ComponentState.Content;
        }

        return value;
    }

    public StandaloneEditorConstructionOptions BuildConstructionOptions(
        StandaloneCodeEditor _
    )
    {
        return new StandaloneEditorConstructionOptions
        {
            Theme = "vs-dark",
            Language = FileEditorState.EditorNode.Properties.Language,
            Value = FileEditorState.EditorFile.Content,
            AutomaticLayout = true,
        };
    }
    #endregion

    #region Details Modal
    public bool IsDetailsModalOpen { get; set; }

    public void HandleOpenDetails()
    {
        IsDetailsModalOpen = true;
    }

    public void HandleCloseDeatils()
    {
        IsDetailsModalOpen = false;
    }
    #endregion

    #region File Error Details
    public ComponentState FileErrorDetailsDisplayState { get; set; } =
        ComponentState.Loading;
    public bool IsErrorDetailsModalOpen { get; set; }

    public void HandleOpenErrorDetails()
    {
        IsErrorDetailsModalOpen = true;
    }

    public void HandleCloseErrorDeatils()
    {
        IsErrorDetailsModalOpen = false;
    }
    #endregion

    #region Compiler Errors
    public ComponentState CompilerErrorsDisplayState { get; set; } =
        ComponentState.Loading;
    public bool IsCompilerErrorsModalOpen { get; set; }

    public void HandleOpenCompilerErrors()
    {
        IsCompilerErrorsModalOpen = true;
    }

    public void HandleCloseCompilerErrors()
    {
        IsCompilerErrorsModalOpen = false;
    }
    #endregion
}
