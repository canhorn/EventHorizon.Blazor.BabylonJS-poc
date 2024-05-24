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
using Microsoft.JSInterop;

/// <summary>
/// Monaco Language Completion: EventHorizon.Game.Editor\Client\wwwroot\js\monaco\monaco-language-support.js
/// </summary>
public class ZoneFileEditorComponentModel : EditorComponentBase, IAsyncDisposable
{
    private static string ZoneFileEditorPendingValueKey(string id) =>
        $"ZoneFileEditorComponent:{id}:PendingValue";

    [CascadingParameter]
    public required ZoneState ZoneState { get; set; }

    [CascadingParameter]
    public required FileEditorState FileEditorState { get; set; }

    [CascadingParameter]
    public required FileEditorSettings FileEditorSettings { get; set; }

    [Inject]
    public required ILocalStorageService LocalStorage { get; set; }

    [Inject]
    public required IFactory<IIntervalTimerService> IntervalTimerServiceFactory { get; set; }

    [Inject]
    public required IJSRuntime JSRuntime { get; set; }

    private string _currentEditingFileId = string.Empty;
    protected string EditorContent = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        _savePendingChangesIntervalTimerService = IntervalTimerServiceFactory
            .Create()
            .Setup(1000, HandleSavePendingChanges)
            .Start();

        FileEditorSettings.OnStateChange += HandleAdvanceEditorEnabled;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await base.OnAfterRenderAsync(firstRender);
        if (firstRender)
        {
            LocalEditorSettings =
                await LocalStorage.GetItemAsync<LocalEditorSettingsModel>(
                    ZoneFileEditorPendingValueKey(FileEditorState.EditorNode.Id)
                ) ?? new LocalEditorSettingsModel(string.Empty);
        }
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
            && FileEditorState.EditorFile.Id != _currentEditingFileId
        )
        {
            await MonacoEditor.SetValue(await GetEditorValue());
            _currentEditingFileId = FileEditorState.EditorFile.Id;
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
        MonacoEditor?.DisposeEditor();
        MonacoEditor = null;
        FileEditorSettings.OnStateChange -= HandleAdvanceEditorEnabled;

        return ValueTask.CompletedTask;
    }

    private async ValueTask HandleAdvanceEditorEnabled(FileEditorSettingChangeStates state)
    {
        Console.WriteLine("HandleAdvanceEditorEnabled: " + state);
        if (state == FileEditorSettingChangeStates.AdvanceEditorEnabled)
        {
            // AdvanceEditorEnabled = FileEditorSettings.AdvanceEditorEnabled;
            await HandleClearFileChanges();
        }
    }

    public async Task HandleSaveFile()
    {
        if (MonacoEditor.IsNull())
        {
            return;
        }
        _savePendingChangesIntervalTimerService?.Pause();
        var value = await MonacoEditor.GetValue();
        var result = await Mediator.Send(
            new SaveEditorFileContentCommand(
                FileEditorState.EditorNode.Path,
                FileEditorState.EditorNode.Name,
                FileEditorState.EditorFile.BuildFromSimpleContent(value, true)
            )
        );
        if (result.Successful.IsNotTrue())
        {
            await ShowMessage(
                Localizer["File Save Status"],
                Localizer["File failed to save, Error Code of '{0}'", result.ErrorCode],
                MessageLevel.Error
            );
            return;
        }
        _savePendingChangesIntervalTimerService?.Start();
        await LocalStorage.RemoveItemAsync(
            ZoneFileEditorPendingValueKey(FileEditorState.EditorNode.Id)
        );
        await ShowMessage(Localizer["File Save Status"], Localizer["File was Successfully Saved"]);
    }

    // protected async Task HandleAdvanceEditorEnabled()
    // {
    //     AdvanceEditorEnabled = !AdvanceEditorEnabled;
    //     await HandleClearFileChanges();
    // }

    #region Pending Changes
    private IIntervalTimerService? _savePendingChangesIntervalTimerService;
    private string _pendingSaveValue = string.Empty;

    protected ComponentState FileChangesPendingDisplayState { get; set; } = ComponentState.Loading;

    private async Task HandleSavePendingChanges()
    {
        if (MonacoEditor.IsNull())
        {
            return;
        }

        var editorValue = await MonacoEditor.GetValue();
        var (IsSimpleContent, Content) = FileEditorState.EditorFile.GetContent(
            FileEditorSettings.AdvanceEditorEnabled
        );
        if (Content == editorValue)
        {
            return;
        }

        await LocalStorage.SetItemAsync(
            ZoneFileEditorPendingValueKey(FileEditorState.EditorNode.Id),
            new LocalEditorSettingsModel(editorValue ?? string.Empty)
            {
                IsSimpleContent = IsSimpleContent,
            }
        );
        FileChangesPendingDisplayState = ComponentState.Content;

        await InvokeAsync(StateHasChanged);
    }

    public async Task HandleClearFileChanges()
    {
        if (MonacoEditor.IsNull())
        {
            return;
        }

        var (_, Content) = FileEditorState.EditorFile.GetContent(
            FileEditorSettings.AdvanceEditorEnabled
        );
        _pendingSaveValue = Content;
        FileChangesPendingDisplayState = ComponentState.Loading;
        await MonacoEditor.SetValue(Content);
        await LocalStorage.RemoveItemAsync(
            ZoneFileEditorPendingValueKey(FileEditorState.EditorNode.Id)
        );
    }
    #endregion

    #region Editor
    public StandaloneCodeEditor? MonacoEditor { get; set; } = null!;
    public LocalEditorSettingsModel LocalEditorSettings { get; private set; } = new(string.Empty);

    public async Task HandleDidInit()
    {
        if (MonacoEditor.IsNull())
        {
            return;
        }

        await MonacoEditor.AddCommand(
            (int)KeyMod.CtrlCmd | (int)KeyMod.Shift | (int)KeyCode.KeyS,
            (args) =>
            {
                _ = HandleSaveFile();
            }
        );
        await MonacoEditor.SetValue(await GetEditorValue());
        _currentEditingFileId = FileEditorState.EditorFile.Id;
    }

    private async Task<string> GetEditorValue()
    {
        if (_pendingSaveValue.IsNullOrEmpty())
        {
            _pendingSaveValue = LocalEditorSettings.LocalContent;
        }

        var (IsSimpleContent, Content) = FileEditorState.EditorFile.GetContent(
            FileEditorSettings.AdvanceEditorEnabled
        );
        var value = Content;

        if (
            _pendingSaveValue.IsNotNullOrEmpty()
            && LocalEditorSettings.IsSimpleContent == IsSimpleContent
            && _pendingSaveValue != value
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

    public StandaloneEditorConstructionOptions BuildConstructionOptions(StandaloneCodeEditor _)
    {
        EditorContent = FileEditorState
            .EditorFile.GetContent(FileEditorSettings.AdvanceEditorEnabled)
            .Content;
        return new StandaloneEditorConstructionOptions
        {
            Theme = "vs-dark",
            Language = FileEditorState.EditorNode.Properties.Language,
            Value = EditorContent,
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

    public void HandleCloseDetails()
    {
        IsDetailsModalOpen = false;
    }
    #endregion

    #region File Error Details
    public ComponentState FileErrorDetailsDisplayState { get; set; } = ComponentState.Loading;
    public bool IsErrorDetailsModalOpen { get; set; }

    public void HandleOpenErrorDetails()
    {
        IsErrorDetailsModalOpen = true;
    }

    public void HandleCloseErrorDetails()
    {
        IsErrorDetailsModalOpen = false;
    }
    #endregion

    #region Compiler Errors
    public ComponentState CompilerErrorsDisplayState { get; set; } = ComponentState.Loading;
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

    public record LocalEditorSettingsModel(string LocalContent)
    {
        public bool IsSimpleContent { get; set; }
    }
}
