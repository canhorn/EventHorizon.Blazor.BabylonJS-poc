namespace EventHorizon.Game.Editor.Client.ArtifactManagement.Zone.Upload;

using EventHorizon.Game.Client.Core.Command.Model;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;

public record UploadZoneServerImportFileCommand(string AccessToken, IBrowserFile File)
    : IRequest<StandardCommandResult>;
