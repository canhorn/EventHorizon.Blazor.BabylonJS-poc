namespace EventHorizon.Game.Editor.Client.AssetManagement.Service;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

using EventHorizon.Game.Editor.Client.AssetManagement.Api;
using EventHorizon.Game.Editor.Client.AssetManagement.Model;
using EventHorizon.Game.Editor.Model;

using Microsoft.AspNetCore.Components.Forms;

public class RemoteAssetFileManagement : AssetFileManagement
{
    private static readonly long MAX_FILE_SIZE_IN_MAGABYTES = 15;
    private static readonly long MAX_FILE_SIZE_IN_BYTES =
        1024 * 1024 * MAX_FILE_SIZE_IN_MAGABYTES;

    private readonly string _fileManagementUrl = "/api/FileManagement";
    private readonly HttpClient _httpClient;

    public RemoteAssetFileManagement(
        HttpClient httpClient,
        GamePlatformServiceSettings settings
    )
    {
        _httpClient = httpClient;
        _fileManagementUrl = $"{settings.AssetServer}/api/FileManagement";
    }

    public async Task<FileSystemResponse> GetFiles(
        string accessToken,
        string path,
        CancellationToken cancellationToken
    )
    {
        try
        {
            using var response = await MakeHttpClientRequest(
                HttpMethod.Get,
                $"{_fileManagementUrl}/GetFiles?path={HttpUtility.UrlEncode(path)}",
                accessToken,
                cancellationToken
            );

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<FileSystemResponse>(
                        cancellationToken: cancellationToken
                    ) ?? InvalidFileErrorResponse();
            }
            else if (
                response.StatusCode == System.Net.HttpStatusCode.Unauthorized
            )
            {
                return CreateNotAuthorizedErrorResponse();
            }

            return new FileSystemResponse
            {
                Error =
                    await response.Content.ReadFromJsonAsync<ErrorDetails>(
                        cancellationToken: cancellationToken
                    ) ?? InvalidResponseError()
            };
        }
        catch (HttpRequestException ex)
        {
            return new FileSystemResponse
            {
                Error = InvalidResponseError(
                    500,
                    $"HttpRequestException: {ex.Message}"
                ),
            };
        }
    }

    public async Task<FileSystemResponse> Search(
        string accessToken,
        string path,
        string searchString,
        CancellationToken cancellationToken
    )
    {
        try
        {
            using var response = await MakeHttpClientRequest(
                HttpMethod.Get,
                $"{_fileManagementUrl}/Search?path={HttpUtility.UrlEncode(path)}&searchString={HttpUtility.UrlEncode(searchString)}",
                accessToken,
                cancellationToken
            );

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<FileSystemResponse>(
                        cancellationToken: cancellationToken
                    ) ?? InvalidFileErrorResponse();
            }
            else if (
                response.StatusCode == System.Net.HttpStatusCode.Unauthorized
            )
            {
                return CreateNotAuthorizedErrorResponse();
            }

            return new FileSystemResponse
            {
                Error =
                    await response.Content.ReadFromJsonAsync<ErrorDetails>(
                        cancellationToken: cancellationToken
                    ) ?? InvalidResponseError()
            };
        }
        catch (HttpRequestException ex)
        {
            return new FileSystemResponse
            {
                Error = InvalidResponseError(
                    500,
                    $"HttpRequestException: {ex.Message}"
                ),
            };
        }
    }

    public async Task<FileSystemResponse> CreateDirectory(
        string accessToken,
        string path,
        string name,
        CancellationToken cancellationToken
    )
    {
        try
        {
            using var response = await MakeHttpClientRequest(
                HttpMethod.Post,
                $"{_fileManagementUrl}/CreateDirectory?path={HttpUtility.UrlEncode(path)}&name={HttpUtility.UrlEncode(name)}",
                accessToken,
                cancellationToken
            );

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<FileSystemResponse>(
                        cancellationToken: cancellationToken
                    ) ?? InvalidFileErrorResponse();
            }
            else if (
                response.StatusCode == System.Net.HttpStatusCode.Unauthorized
            )
            {
                return CreateNotAuthorizedErrorResponse();
            }

            return new FileSystemResponse
            {
                Error =
                    await response.Content.ReadFromJsonAsync<ErrorDetails>(
                        cancellationToken: cancellationToken
                    ) ?? InvalidResponseError()
            };
        }
        catch (HttpRequestException ex)
        {
            return new FileSystemResponse
            {
                Error = InvalidResponseError(
                    500,
                    $"HttpRequestException: {ex.Message}"
                ),
            };
        }
    }

    public async Task<FileSystemResponse> Delete(
        string accessToken,
        string path,
        string name,
        CancellationToken cancellationToken
    )
    {
        try
        {
            using var response = await MakeHttpClientRequest(
                HttpMethod.Delete,
                $"{_fileManagementUrl}/Delete?path={HttpUtility.UrlEncode(path)}&names={HttpUtility.UrlEncode(name)}",
                accessToken,
                cancellationToken
            );

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<FileSystemResponse>(
                        cancellationToken: cancellationToken
                    ) ?? InvalidFileErrorResponse();
            }
            else if (
                response.StatusCode == System.Net.HttpStatusCode.Unauthorized
            )
            {
                return CreateNotAuthorizedErrorResponse();
            }

            return new FileSystemResponse
            {
                Error =
                    await response.Content.ReadFromJsonAsync<ErrorDetails>(
                        cancellationToken: cancellationToken
                    ) ?? InvalidResponseError()
            };
        }
        catch (HttpRequestException ex)
        {
            return new FileSystemResponse
            {
                Error = InvalidResponseError(
                    500,
                    $"HttpRequestException: {ex.Message}"
                ),
            };
        }
    }

    public async Task<FileSystemUploadResponse> Upload(
        string accessToken,
        IBrowserFile file,
        string filePath,
        CancellationToken cancellationToken
    )
    {
        try
        {
            if (file.Size > MAX_FILE_SIZE_IN_BYTES)
            {
                return new FileSystemUploadResponse
                {
                    Error = new ErrorDetails
                    {
                        Code = 413,
                        Message =
                            $"Payload Too Large ({MAX_FILE_SIZE_IN_MAGABYTES}MB max)",
                    },
                };
            }

            using var content = new MultipartFormDataContent();
            using var fileContent = new StreamContent(
                file.OpenReadStream(
                    MAX_FILE_SIZE_IN_BYTES,
                    cancellationToken: cancellationToken
                )
            );

            content.Add(new StringContent(filePath), "file-path");
            content.Add(
                content: fileContent,
                name: "\"file\"",
                fileName: file.Name
            );

            using var httpMessage = new HttpRequestMessage(
                HttpMethod.Post,
                $"{_fileManagementUrl}/Upload"
            )
            {
                Content = content,
            };
            AddAuthorizationHeader(httpMessage, accessToken);

            using var response = await _httpClient.SendAsync(
                httpMessage,
                cancellationToken
            );

            if (response.IsSuccessStatusCode)
            {
                return new() { Success = true, };
            }
            else if (
                response.StatusCode == System.Net.HttpStatusCode.Unauthorized
            )
            {
                return new FileSystemUploadResponse
                {
                    Error = InvalidResponseError(401, "Not Authorized"),
                };
            }

            return new FileSystemUploadResponse
            {
                Error =
                    await response.Content.ReadFromJsonAsync<ErrorDetails>(
                        cancellationToken: cancellationToken
                    )
                    ?? InvalidResponseError(
                        500,
                        "Error Status Code (with no ErrorDetails)"
                    ),
            };
        }
        catch (HttpRequestException ex)
        {
            return new FileSystemUploadResponse
            {
                Error = InvalidResponseError(
                    500,
                    $"HttpRequestException: {ex.Message}"
                ),
            };
        }
    }

    private async Task<HttpResponseMessage> MakeHttpClientRequest(
        HttpMethod method,
        string requestUri,
        string accessToken,
        CancellationToken cancellationToken
    )
    {
        using var httpMessage = new HttpRequestMessage(method, requestUri);
        AddAuthorizationHeader(httpMessage, accessToken);

        return await _httpClient.SendAsync(httpMessage, cancellationToken);
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

    private static FileSystemResponse CreateNotAuthorizedErrorResponse() =>
        new() { Error = InvalidResponseError(401, "Not Authorized"), };

    private static FileSystemResponse InvalidFileErrorResponse() =>
        new() { Error = InvalidResponseError(), };

    private static ErrorDetails InvalidResponseError(
        int code = 500,
        string message = "Invalid Response"
    ) => new() { Code = code, Message = message, };
}
