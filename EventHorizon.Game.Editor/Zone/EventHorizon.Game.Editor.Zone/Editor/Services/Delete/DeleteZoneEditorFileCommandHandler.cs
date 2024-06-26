﻿namespace EventHorizon.Game.Editor.Zone.Editor.Services.Delete;

using System;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Editor.Zone.Editor.Services.Api;
using EventHorizon.Game.Editor.Zone.Editor.Services.Model;
using MediatR;
using Microsoft.Extensions.Logging;

public class DeleteZoneEditorFileCommandHandler
    : IRequestHandler<DeleteZoneEditorFileCommand, EditorResponse>
{
    private readonly ILogger _logger;
    private readonly ZoneEditorServices _zoneEditorServices;

    public DeleteZoneEditorFileCommandHandler(
        ILogger<DeleteZoneEditorFileCommandHandler> logger,
        ZoneEditorServices zoneEditorServices
    )
    {
        _logger = logger;
        _zoneEditorServices = zoneEditorServices;
    }

    public Task<EditorResponse> Handle(
        DeleteZoneEditorFileCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return _zoneEditorServices.Api.DeleteEditorFile(request.Path, request.FileName);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to Delete Zone Editor File: {EditorFilePath} | {EditorFileName}",
                request.Path,
                request.FileName
            );
        }
        return new EditorResponse
        {
            Successful = false,
            ErrorCode = ZoneEditorErrorCodes.FAILED_DELETE_ZONE_EDITOR_FILE,
        }.FromResult();
    }
}
