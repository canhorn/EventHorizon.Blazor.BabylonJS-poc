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
        // TODO: Move this into a Tier Variable.
        // A Tier variable is part of an Owners subscription,
        // when a Platform Owner is part of higher tiers they will get more benefits.
        private const long MAX_FILE_SIZE = 1024 * 1024 * 15 * 10; // 150MB

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
                if (file.Size > MAX_FILE_SIZE)
                {
                    _logger.LogError(
                        "Asset Server Payload Too Large ({FileSize}) | ({MaxFileSize} max)",
                        file.Size,
                        MAX_FILE_SIZE
                    );
                    return new(
                        AssetServerErrorCodes.ASSET_SERVER_PAYLOAD_TOOL_LARGE
                    );
                }

                using var content = new MultipartFormDataContent();
                using var fileContent = new StreamContent(
                    file.OpenReadStream(
                        MAX_FILE_SIZE,
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
