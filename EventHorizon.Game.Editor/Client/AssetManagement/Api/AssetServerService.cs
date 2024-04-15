namespace EventHorizon.Game.Editor.Client.AssetManagement.Api;

using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.AssetManagement.Model;
using Microsoft.AspNetCore.Components.Forms;

public interface AssetServerService
{
    Task<StandardCommandResult> Upload(
        string accessToken,
        IBrowserFile file,
        CancellationToken cancellationToken
    );

    Task<CommandResult<UploadImportFileResult>> UploadImportFile(
        string accessToken,
        string service,
        IBrowserFile file,
        CancellationToken cancellationToken
    );
}
