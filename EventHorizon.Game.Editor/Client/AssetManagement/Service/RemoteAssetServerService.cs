namespace EventHorizon.Game.Editor.Client.AssetManagement.Service;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.AssetManagement.Model;
using EventHorizon.Game.Editor.Model;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Logging;

public class RemoteAssetServerService : AssetServerService
{
    private readonly ILogger _logger;
    private readonly string _baseApiUrl;
    private readonly HttpClient _httpClient;

    public RemoteAssetServerService(
        ILogger<RemoteAssetServerService> logger,
        HttpClient httpClient,
        GamePlatformServiceSettings settings
    )
    {
        _logger = logger;
        _httpClient = httpClient;
        _baseApiUrl = settings.AssetServer;
    }

    public async Task<StandardCommandResult> Upload(
        string accessToken,
        IBrowserFile file,
        CancellationToken cancellationToken
    )
    {
        var service = "Asset";
        var url = $"{_baseApiUrl}/api/Asset/Import/Upload";
        var result = await UploadFileToAssetServer(
            accessToken,
            file,
            service,
            url,
            cancellationToken
        );
        if (!result)
        {
            return result.ErrorCode;
        }

        return new();
    }

    public async Task<CommandResult<UploadImportFileResult>> UploadImportFile(
        string accessToken,
        string service,
        IBrowserFile file,
        CancellationToken cancellationToken
    )
    {
        var url = $"{_baseApiUrl}/api/Import/{service}/Upload";
        return await UploadFileToAssetServer(accessToken, file, service, url, cancellationToken);
    }

    private async Task<CommandResult<UploadImportFileResult>> UploadFileToAssetServer(
        string accessToken,
        IBrowserFile file,
        string service,
        string url,
        CancellationToken cancellationToken
    )
    {
        try
        {
            if (file.Size > AssetServerConstants.MAX_FILE_SIZE_IN_BYTES)
            {
                _logger.LogError(
                    "Asset Server Payload Too Large ({FileSize}) | ({MaxFileSizeInBytes} max) | Service: {UploadService}",
                    file.Size,
                    AssetServerConstants.MAX_FILE_SIZE_IN_BYTES,
                    service
                );
                return new(AssetServerErrorCodes.ASSET_SERVER_PAYLOAD_TOO_LARGE);
            }

            using var content = new MultipartFormDataContent();
            using var fileContent = new StreamContent(
                file.OpenReadStream(
                    AssetServerConstants.MAX_FILE_SIZE_IN_BYTES,
                    cancellationToken: cancellationToken
                )
            );

            content.Add(content: fileContent, name: "\"file\"", fileName: file.Name);

            using var httpMessage = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = content,
            };
            AddAuthorizationHeader(httpMessage, accessToken);

            using var response = await _httpClient.SendAsync(httpMessage, cancellationToken);

            if (response.IsSuccessStatusCode)
            {
                var successResult = await response.Content.ReadFromJsonAsync<UploadImportFileModel>(
                    cancellationToken: cancellationToken
                );
                if (successResult.IsNull())
                {
                    return AssetServerErrorCodes.ASSET_SERVER_GENERAL_ERROR;
                }

                return new(
                    new UploadImportFileResult(
                        successResult.Service,
                        $"{_baseApiUrl}{successResult.Path}"
                    )
                );
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                return AssetServerErrorCodes.ASSET_SERVER_NOT_AUTHORIZED;
            }

            var errorResult = await response.Content.ReadFromJsonAsync<ErrorDetails>(
                cancellationToken: cancellationToken
            );
            return errorResult?.ErrorCode ?? AssetServerErrorCodes.ASSET_SERVER_GENERAL_ERROR;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(
                ex,
                "Failed with HTTP Request Exception. Service: {UploadService}",
                service
            );

            return AssetServerErrorCodes.ASSET_SERVER_BAD_API_REQUEST;
        }
    }

    private static void AddAuthorizationHeader(HttpRequestMessage httpMessage, string accessToken)
    {
        httpMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
    }
}
