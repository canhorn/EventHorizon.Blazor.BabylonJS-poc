namespace EventHorizon.Game.Server.Asset.Services
{
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

        public AssetServerExportAdminApi ExportApi { get; private set; } = new SignalrAssetServerExportAdminApi(null);
        public AssetServerFileManagementAdminApi FileManagementApi { get; private set; } = new SignalrAssetServerFileManagementAdminApi(null);

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
                return new(
                    ConnectionErrorTypes.NotInitialized
                );
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
                    ).WithUrl(
                        $"{_settings.AssetServer}/admin",
                        options =>
                        {
                            // options.LogLevel = SignalRLogLevel.Error;
                            options.AccessTokenProvider = () => Task.FromResult(
                                accessToken
                            );
                        }
                    ).Build();

                _connection.On<string, string>(
                    "ExportFinished",
                    HandleExportFinished
                );

                await _connection.StartAsync(
                    cancellationToken
                );
                ExportApi = new SignalrAssetServerExportAdminApi(
                    _connection
                );
                FileManagementApi = new SignalrAssetServerFileManagementAdminApi(
                    _connection
                );

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
                        new ConnectionUnauthorizedEvent(
                            "asset-server-admin"
                        ),
                        cancellationToken
                    );
                    return new(
                        ConnectionErrorTypes.Unauthorized
                    );
                }
                return new(
                    ConnectionErrorTypes.Unknown
                );
            }
            catch (InvalidOperationException ex)
            {
                await LogAndDispose(
                    ex,
                    "Operation Exception starting connection to Asset Server Admin Service."
                );
                return new(
                    ConnectionErrorTypes.Operational
                );
            }
            catch (Exception ex)
            {
                await LogAndDispose(
                    ex,
                    "Generic Exception starting connection to Asset Server Admin Service."
                );
                return new(
                    ConnectionErrorTypes.Unknown
                );
            }
        }

        private async Task HandleExportFinished(
            string referenceId,
            string exportPath
        )
        {
            await _mediator.Publish(
                new AssetServerExportFinishedEvent(
                    referenceId,
                    exportPath
                )
            );
        }

        private async Task LogAndDispose(
            Exception ex,
            string message
        )
        {
            _logger.LogWarning(
                ex,
                message
            );
            await DisposeAsync();
        }

        public async ValueTask DisposeAsync()
        {
            if (_connection.IsNotNull())
            {
                await _connection.DisposeAsync();
                ExportApi = new SignalrAssetServerExportAdminApi(null);
                FileManagementApi = new SignalrAssetServerFileManagementAdminApi(null);

                _connection = null;
            }
            _initializing = false;
            _initialized = false;
        }
    }
}
