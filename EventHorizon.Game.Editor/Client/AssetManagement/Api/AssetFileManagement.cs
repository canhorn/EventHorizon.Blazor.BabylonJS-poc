namespace EventHorizon.Game.Editor.Client.AssetManagement.Api;

using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Game.Editor.Client.AssetManagement.Model;

using Microsoft.AspNetCore.Components.Forms;

public interface AssetFileManagement
{
    Task<FileSystemResponse> Search(
        string accessToken,
        string path,
        string searchString,
        CancellationToken cancellationToken
    );

    Task<FileSystemResponse> GetFiles(
        string accessToken,
        string path,
        CancellationToken cancellationToken
    );

    Task<FileSystemResponse> CreateDirectory(
        string accessToken,
        string path,
        string name,
        CancellationToken cancellationToken
    );

    Task<FileSystemResponse> Delete(
        string accessToken,
        string path,
        string name,
        CancellationToken cancellationToken
    );

    Task<FileSystemUploadResponse> Upload(
        string accessToken,
        IBrowserFile file,
        string filePath,
        CancellationToken cancellationToken
    );
}
