namespace EventHorizon.Game.Server.Asset.Services;

using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using EventHorizon.Connection.Shared;
using EventHorizon.Connection.Shared.Unauthorized;
using EventHorizon.Game.Client.Core.Command.Model;
using EventHorizon.Game.Editor.Model;
using EventHorizon.Game.Server.Asset.Api;
using EventHorizon.Game.Server.Asset.Connection;
using EventHorizon.Game.Server.Asset.Finished;

using MediatR;

using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;

public class SignalrAssetServerAdminService
    : AssetServerAdminService,
      IAsyncDisposable
{
    private readonly ILogger _logger;
    private readonly IMediator _mediator;
    private readonly GamePlatformServiceSettings _settings;

    private bool _initializing = false;
    private bool _initialized = false;
    private HubConnection? _connection;

    public AssetServerCommonAdminApi CommonApi { get; private set; } =
        new SignalrAssetServerCommonAdminApi(null);
    public AssetServerExportAdminApi ExportApi { get; private set; } =
        new SignalrAssetServerExportAdminApi(null);
    public AssetServerFileManagementAdminApi FileManagementApi
    {
        get;
        private set;
    } = new SignalrAssetServerFileManagementAdminApi(null);
    public AssetServerBackupAdminApi BackupApi { get; private set; } =
        new SignalrAssetServerBackupAdminApi(null);

    public SignalrAssetServerAdminService(
        ILogger<SignalrAssetServerAdminService> logger,
        IMediator mediator,
        GamePlatformServiceSettings settings
    )
    {
        _logger = logger;
        _mediator = mediator;
        _settings = settings;
    }

    public async Task<StandardCommandResult> Connect(
        string accessToken,
        CancellationToken cancellationToken
    )
    {
        if (_initializing)
        {
            return new(ConnectionErrorTypes.NotInitialized);
        }
        else if (_initialized)
        {
            return new();
        }
        try
        {
            _initializing = true;
            _connection = new HubConnectionBuilder()
                .WithAutomaticReconnect(
                    new TimeSpan[]
                    {
                        TimeSpan.FromSeconds(0),
                        TimeSpan.FromSeconds(2),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(30),
                        TimeSpan.FromSeconds(30),
                    }
                )
                .WithUrl(
                    $"{_settings.AssetServer}/admin",
                    options =>
                    {
                        // options.LogLevel = SignalRLogLevel.Error;
                        options.AccessTokenProvider = () =>
                            Task.FromResult<string?>(accessToken);
                    }
                )
                .Build();

            SetupEvents(_connection);

            await _connection.StartAsync(cancellationToken);
            SetupApi(_connection);

            _initializing = false;
            _initialized = true;

            await _mediator.Publish(
                new ConnectedToAssetServerAdmin(),
                cancellationToken
            );
            return new();
        }
        catch (HttpRequestException ex)
        {
            await LogAndDispose(
                ex,
                "Failed to start connection to Asset Server Admin Service."
            );
            if (ex.Message.Contains("401 (Unauthorized)"))
            {
                await _mediator.Publish(
                    new ConnectionUnauthorizedEvent("asset-server-admin"),
                    cancellationToken
                );
                return new(ConnectionErrorTypes.Unauthorized);
            }
            return new(ConnectionErrorTypes.Unknown);
        }
        catch (InvalidOperationException ex)
        {
            await LogAndDispose(
                ex,
                "Operation Exception starting connection to Asset Server Admin Service."
            );
            return new(ConnectionErrorTypes.Operational);
        }
        catch (Exception ex)
        {
            await LogAndDispose(
                ex,
                "Generic Exception starting connection to Asset Server Admin Service."
            );
            return new(ConnectionErrorTypes.Unknown);
        }
    }

    private void SetupEvents(HubConnection connection)
    {
        connection.On<string, string>(
            "BackupUploadFinished",
            HandleCommonBackupUploadFinished
        );
        connection.On<string, string>(
            "ExportUploadFinished",
            HandleCommonExportUploadFinished
        );
        connection.On<string, string>(
            "ImportUploadFinished",
            HandleCommonImportUploadFinished
        );

        connection.On<string, string>(
            "AssetBackupFinished",
            HandleAssetBackupFinished
        );
        connection.On<string, string>(
            "AssetExportFinished",
            HandleAssetExportFinished
        );
    }

    private async Task HandleCommonBackupUploadFinished(
        string service,
        string exportPath
    )
    {
        await _mediator.Publish(
            new AssetServerBackupUploadedEvent(service, exportPath)
        );
    }

    private async Task HandleCommonExportUploadFinished(
        string service,
        string exportPath
    )
    {
        await _mediator.Publish(
            new AssetServerExportUploadedEvent(service, exportPath)
        );
    }

    private async Task HandleCommonImportUploadFinished(
        string service,
        string exportPath
    )
    {
        await _mediator.Publish(
            new AssetServerImportUploadedEvent(service, exportPath)
        );
    }

    private async Task HandleAssetBackupFinished(
        string referenceId,
        string backupPath
    )
    {
        await _mediator.Publish(
            new AssetServerBackupFinishedEvent(referenceId, backupPath)
        );
    }

    private async Task HandleAssetExportFinished(
        string referenceId,
        string exportPath
    )
    {
        await _mediator.Publish(
            new AssetServerExportFinishedEvent(referenceId, exportPath)
        );
    }

    private void SetupApi(HubConnection connection)
    {
        CommonApi = new SignalrAssetServerCommonAdminApi(connection);
        BackupApi = new SignalrAssetServerBackupAdminApi(connection);
        ExportApi = new SignalrAssetServerExportAdminApi(connection);
        FileManagementApi = new SignalrAssetServerFileManagementAdminApi(
            connection
        );
    }

    private async Task LogAndDispose(Exception ex, string message)
    {
        _logger.LogWarning(ex, message);
        await DisposeAsync();
    }

    public async ValueTask DisposeAsync()
    {
        if (_connection.IsNotNull())
        {
            await _connection.DisposeAsync();
            CommonApi = new SignalrAssetServerCommonAdminApi(null);
            BackupApi = new SignalrAssetServerBackupAdminApi(null);
            ExportApi = new SignalrAssetServerExportAdminApi(null);
            FileManagementApi = new SignalrAssetServerFileManagementAdminApi(
                null
            );

            _connection = null;
        }
        _initializing = false;
        _initialized = false;
    }
}
