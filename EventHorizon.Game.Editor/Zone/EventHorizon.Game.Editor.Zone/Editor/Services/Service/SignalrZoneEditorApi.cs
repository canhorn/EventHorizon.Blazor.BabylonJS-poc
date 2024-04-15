namespace EventHorizon.Game.Editor.Zone.Editor.Services.Service;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Zone.Editor.Services.Api;
using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

public class SignalrZoneEditorApi : ZoneEditorApi
{
    private readonly ILogger _logger;
    private readonly HubConnection? _hubConnection;

    internal SignalrZoneEditorApi(
        ILogger<SignalrZoneEditorApi> logger,
        HubConnection? hubConnection
    )
    {
        _logger = logger;
        _hubConnection = hubConnection;
    }

    public async Task<CommandResult<EditorNodeList>> GetEditorZoneList()
    {
        try
        {
            if (_hubConnection.IsNotConnected())
            {
                return new(ZoneEditorErrorCodes.NOT_CONNECTED);
            }
            return new(await _hubConnection.InvokeAsync<EditorNodeList>("GetEditorState"));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed Zone Editor API Call: {ActionName}",
                nameof(GetEditorZoneList)
            );
            return new(ZoneEditorErrorCodes.EDITOR_API_ERROR);
        }
    }

    public async Task<EditorFile?> GetEditorFileContent(IList<string> path, string fileName)
    {
        try
        {
            if (_hubConnection.IsNotConnected())
            {
                return null;
            }

            return await _hubConnection.InvokeAsync<EditorFile?>(
                "GetEditorFileContent",
                path,
                fileName
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed Zone Editor API Call: {ActionName}",
                nameof(GetEditorFileContent)
            );
            return null;
        }
    }

    public Task<EditorResponse> SaveEditorFileContent(
        IList<string> path,
        string fileName,
        string content
    ) =>
        LogOnError(
            action: async (hubConnection) =>
                await hubConnection.InvokeAsync<EditorResponse>(
                    "SaveEditorFileContent",
                    path,
                    fileName,
                    content
                ),
            onError: () =>
                new EditorResponse
                {
                    Successful = false,
                    ErrorCode = ZoneEditorErrorCodes.EDITOR_API_ERROR,
                }
        );

    public Task<EditorResponse> CreateEditorFile(IList<string> path, string fileName) =>
        LogOnError(
            action: async (hubConnection) =>
                await hubConnection.InvokeAsync<EditorResponse>("CreateEditorFile", path, fileName),
            onError: () =>
                new EditorResponse
                {
                    Successful = false,
                    ErrorCode = ZoneEditorErrorCodes.EDITOR_API_ERROR,
                }
        );

    public Task<EditorResponse> CreateEditorFolder(IList<string> path, string folderName) =>
        LogOnError(
            action: async (hubConnection) =>
                await hubConnection.InvokeAsync<EditorResponse>(
                    "CreateEditorFolder",
                    path,
                    folderName
                ),
            onError: () =>
                new EditorResponse
                {
                    Successful = false,
                    ErrorCode = ZoneEditorErrorCodes.EDITOR_API_ERROR,
                }
        );

    public Task<EditorResponse> DeleteEditorFile(IList<string> path, string fileName) =>
        LogOnError(
            action: async (hubConnection) =>
                await hubConnection.InvokeAsync<EditorResponse>("DeleteEditorFile", path, fileName),
            onError: () =>
                new EditorResponse
                {
                    Successful = false,
                    ErrorCode = ZoneEditorErrorCodes.EDITOR_API_ERROR,
                }
        );

    public Task<EditorResponse> DeleteEditorFolder(IList<string> path, string folderName) =>
        LogOnError(
            action: async (hubConnection) =>
                await hubConnection.InvokeAsync<EditorResponse>(
                    "DeleteEditorFolder",
                    path,
                    folderName
                ),
            onError: () =>
                new EditorResponse
                {
                    Successful = false,
                    ErrorCode = ZoneEditorErrorCodes.EDITOR_API_ERROR,
                }
        );

    public async Task<EditorResponse> LogOnError(
        Func<HubConnection, Task<EditorResponse>> action,
        Func<EditorResponse> onError,
        [CallerMemberName] string actionName = ""
    )
    {
        try
        {
            if (_hubConnection.IsNotConnected())
            {
                return new()
                {
                    Successful = false,
                    ErrorCode = ZoneEditorErrorCodes.NOT_CONNECTED,
                };
            }

            return await action(_hubConnection);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed Zone Editor API Call: {ActionName}", actionName);
            return onError();
        }
    }
}
