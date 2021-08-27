namespace EventHorizon.Game.Editor.Client.AssetManagement.Service
{
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

    public class RemoteAssetServerService
        : AssetServerService
    {

        private readonly ILogger _logger;
        private readonly string _importApiUrl = "/api/Import";
        private readonly HttpClient _httpClient;

        public RemoteAssetServerService(
            ILogger<RemoteAssetServerService> logger,
            HttpClient httpClient,
            GamePlatformServiceSettings settings
        )
        {
            _logger = logger;
            _httpClient = httpClient;
            _importApiUrl = $"{settings.AssetServer}/api/Import";
        }

        public async Task<StandardCommandResult> Upload(
            string accessToken,
            IBrowserFile file,
            CancellationToken cancellationToken
        )
        {
            try
            {
                if (file.Size > AssetServerConstants.MAX_FILE_SIZE_IN_BYTES)
                {
                    _logger.LogError(
                        "Asset Server Payload Too Large ({FileSize}) | ({MaxFileSizeInBytes} max)",
                        file.Size,
                        AssetServerConstants.MAX_FILE_SIZE_IN_BYTES
                    );
                    return new(
                        AssetServerErrorCodes.ASSET_SERVER_PAYLOAD_TOOL_LARGE
                    );
                }

                using var content = new MultipartFormDataContent();
                using var fileContent = new StreamContent(
                    file.OpenReadStream(
                        AssetServerConstants.MAX_FILE_SIZE_IN_BYTES,
                        cancellationToken: cancellationToken
                    )
                );

                content.Add(content: fileContent, name: "\"file\"", fileName: file.Name);

                using var httpMessage = new HttpRequestMessage(
                    HttpMethod.Post,
                    $"{_importApiUrl}/Upload"
                )
                {
                    Content = content,
                };
                AddAuthorizationHeader(
                    httpMessage,
                    accessToken
                );

                using var response = await _httpClient.SendAsync(
                    httpMessage,
                    cancellationToken
                );

                if (response.IsSuccessStatusCode)
                {
                    return new();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return new(
                        AssetServerErrorCodes.ASSET_SERVER_NOT_AUTHORIZED
                    );
                }

                var errorResult = await response.Content.ReadFromJsonAsync<ErrorDetails>(
                    cancellationToken: cancellationToken
                );
                return new(
                    errorResult?.ErrorCode ?? AssetServerErrorCodes.ASSET_SERVER_GENERAL_ERROR
                );
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(
                    ex,
                    "Failed with HTTP Request Exception."
                );

                return new(
                    AssetServerErrorCodes.ASSET_SERVER_BAD_API_REQUEST
                );
            }
        }

        private static void AddAuthorizationHeader(
            HttpRequestMessage httpMessage,
            string accessToken
        )
        {
            httpMessage.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                accessToken
            );
        }
    }
}
