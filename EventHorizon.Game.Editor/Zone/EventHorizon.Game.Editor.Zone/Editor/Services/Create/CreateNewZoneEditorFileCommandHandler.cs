﻿namespace EventHorizon.Game.Editor.Zone.Editor.Services.Create;

using System;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Zone.Editor.Services.Api;
using EventHorizon.Game.Editor.Zone.Editor.Services.Model;

using MediatR;

using Microsoft.Extensions.Logging;

public class CreateNewZoneEditorFileCommandHandler
    : IRequestHandler<CreateNewZoneEditorFileCommand, EditorResponse>
{
    private readonly ILogger _logger;
    private readonly ZoneEditorServices _zoneEditorServices;

    public CreateNewZoneEditorFileCommandHandler(
        ILogger<CreateNewZoneEditorFileCommandHandler> logger,
        ZoneEditorServices zoneEditorServices
    )
    {
        _logger = logger;
        _zoneEditorServices = zoneEditorServices;
    }

    public Task<EditorResponse> Handle(
        CreateNewZoneEditorFileCommand request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            return _zoneEditorServices.Api.CreateEditorFile(
                request.Path,
                request.FileName
            );
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Failed to Create New Zone Editor File",
                request
            );
        }
        return new EditorResponse
        {
            Successful = false,
            ErrorCode = ZoneEditorErrorCodes.FAILED_CREATE_ZONE_EDITOR_FILE,
        }.FromResult();
    }
}
